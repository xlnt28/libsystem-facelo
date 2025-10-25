Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class BorrowPendingRequest

    Private Sub BorrowPendingRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
            CenterToScreen()

            If dg IsNot Nothing Then
                dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                dg.MultiSelect = False
                CustomizeDataGridView(dg)
            Else
                MsgBox("DataGridView 'dg' is not initialized.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            OpenDB()
            LoadPendingRequests()

        Catch ex As Exception
            MsgBox("Error initializing form: " & ex.Message, MsgBoxStyle.Critical, "Initialization Error")
        End Try
    End Sub

    Private Sub adminHistory_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        OpenDB()
        LoadPendingRequests()
    End Sub

    Private Sub LoadPendingRequests()
        Try

            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If
            If dg Is Nothing Then
                MsgBox("DataGridView is not initialized.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            SQLQueryForBorrowPendingRequest()

            If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("transactions") Then
                dg.DataSource = admindbds.Tables("transactions")
            Else
                dg.DataSource = Nothing
            End If

            dg.ReadOnly = True
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dg.MultiSelect = False
            dg.ClearSelection()

        Catch ex As Exception
            MsgBox("Error loading pending requests: " & ex.Message, MsgBoxStyle.Critical, "Load Error")
        End Try
    End Sub

    Public Sub SQLQueryForBorrowPendingRequest()
        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            If daBorrowHistory Is Nothing Then
                daBorrowHistory = New OleDbDataAdapter()
            End If

            daBorrowHistory.SelectCommand = New OleDbCommand("SELECT [Transaction ID], [Borrow ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Book ID List], [Copy List], [Request Date] FROM transactions WHERE status = 'Requested' ORDER BY [Request Date] DESC", con)

            If admindbds Is Nothing Then
                admindbds = New DataSet()
            Else
                If admindbds.Tables.Contains("transactions") Then
                    admindbds.Tables("transactions").Clear()
                End If
            End If

            daBorrowHistory.Fill(admindbds, "transactions")

        Catch ex As Exception
            MsgBox("Error retrieving transactions data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        Dim searchTerm As String = InputBox("Enter Borrower Name to search:", "Search Borrower")
        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            SearchBorrower(searchTerm)
        ElseIf searchTerm IsNot Nothing Then
            MsgBox("Search cancelled.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SearchBorrower(ByVal searchTerm As String)
        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim query As String = "SELECT [Transaction ID], [Borrow ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Book ID List], [Copy List], [Request Date] FROM transactions WHERE status = 'Requested' AND [Borrower Name] LIKE ? ORDER BY [Request Date] DESC"

            cmd = New OleDbCommand(query, con)
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")

            Dim searchResults As New DataSet()
            daBorrowHistory = New OleDbDataAdapter(cmd)
            daBorrowHistory.Fill(searchResults, "transactions")

            If searchResults.Tables("transactions").Rows.Count > 0 Then
                admindbds = searchResults
                dg.DataSource = admindbds.Tables("transactions")
                dg.ClearSelection()

                MsgBox(searchResults.Tables("transactions").Rows.Count.ToString() & " request(s) found for borrower: " & searchTerm, MsgBoxStyle.Information)
            Else
                MsgBox("No requests found for borrower: " & searchTerm, MsgBoxStyle.Information)
                LoadPendingRequests()
            End If
        Catch ex As Exception
            MsgBox("Search failed. " & ex.Message, MsgBoxStyle.Critical, "Error")
            LoadPendingRequests()
        End Try
    End Sub

    Private Sub ApproveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApproveToolStripMenuItem.Click
        If dg.SelectedRows.Count = 0 Then
            MsgBox("Please select a request to approve.", MsgBoxStyle.Exclamation, "Select Request")
            Return
        End If

        Dim selectedRow As DataGridViewRow = dg.SelectedRows(0)
        Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()

        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT [Book ID], [Copies] FROM borrowings WHERE [Borrow ID] = ? AND [Status] = 'Requested'", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            If Not reader.HasRows Then
                reader.Close()
                MsgBox("No requested books found for this borrow ID.", MsgBoxStyle.Exclamation, "Error")
                Return
            End If

            Dim result As DialogResult = MsgBox("Approve this borrow request?" + vbCrLf + "This action cannot be undone.", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Approval")

            If result <> DialogResult.Yes Then
                reader.Close()
                Return
            End If

            Dim currentDate As DateTime = DateTime.Now
            Dim dueDate As DateTime = GetUserReturnDate()

            Dim formattedCurrentDate As String = currentDate.ToString("MM/dd/yyyy")
            Dim formattedDueDate As String = dueDate.ToString("MM/dd/yyyy")

            cmd = New OleDbCommand("UPDATE transactions SET [Status] = 'Borrowed', [Borrow Date] = ?, [Due Date] = ?, [Processed By] = ? WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", formattedCurrentDate)
            cmd.Parameters.AddWithValue("?", formattedDueDate)
            cmd.Parameters.AddWithValue("?", XName)
            cmd.Parameters.AddWithValue("?", borrowID)
            cmd.ExecuteNonQuery()

            While reader.Read()
                Dim bookID As String = reader("Book ID").ToString()
                Dim quantityToBorrow As Integer = Integer.Parse(reader("Copies").ToString())

                cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Borrowed', [Borrow Date] = ?, [Due Date] = ?, [Processed By] = ? WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
                cmd.Parameters.AddWithValue("?", formattedCurrentDate)
                cmd.Parameters.AddWithValue("?", formattedDueDate)
                cmd.Parameters.AddWithValue("?", XName)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.Parameters.AddWithValue("?", bookID)
                cmd.ExecuteNonQuery()

                cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                cmd.Parameters.AddWithValue("?", bookID)
                Dim currentQuantity As Integer = 0
                Dim resultQuery = cmd.ExecuteScalar()
                If resultQuery IsNot Nothing AndAlso Not IsDBNull(resultQuery) Then
                    currentQuantity = CInt(resultQuery)
                End If

                Dim newQuantity As Integer = currentQuantity - quantityToBorrow
                cmd = New OleDbCommand("UPDATE books SET [Quantity] = ?, [Status] = IIF(? > 0, 'Available', 'Unavailable') WHERE [Book ID] = ?", con)
                cmd.Parameters.AddWithValue("?", newQuantity)
                cmd.Parameters.AddWithValue("?", newQuantity)
                cmd.Parameters.AddWithValue("?", bookID)
                cmd.ExecuteNonQuery()
            End While

            reader.Close()

            Dim userID As String = GetUserIDFromBorrowID(borrowID)
            Dim userName As String = GeBorrowerNameFromBorrowID(borrowID)

            AddToTransactionReceipt(borrowID, userID, userName)

            GenerateBorrowReceipt(borrowID)

            MsgBox("Borrow request approved successfully!", MsgBoxStyle.Information, "Approval Complete")

            LoadPendingRequests()
        Catch ex As Exception
            MsgBox("Error approving request: " + ex.Message, MsgBoxStyle.Critical, "Approval Error")
        End Try
    End Sub


    Private Sub RejectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectToolStripMenuItem.Click
        If dg.SelectedRows.Count = 0 Then
            MsgBox("Please select a request to reject.", MsgBoxStyle.Exclamation, "Select Request")
            Return
        End If

        Dim selectedRow As DataGridViewRow = dg.SelectedRows(0)
        Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()

        If MessageBox.Show("Are you sure you want to decline this borrow request?", "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                    OpenDB()
                End If

                cmd = New OleDbCommand("UPDATE transactions SET [Status] = 'Declined', [Processed By] = ? WHERE [Borrow ID] = ?", con)
                cmd.Parameters.AddWithValue("?", XName)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()

                cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Declined', [Processed By] = ? WHERE [Borrow ID] = ?", con)
                cmd.Parameters.AddWithValue("?", XName)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()

                MsgBox("Borrow request declined successfully!", MsgBoxStyle.Information, "Decline Complete")
                LoadPendingRequests()

            Catch ex As Exception
                MsgBox("Error rejecting request: " & ex.Message, MsgBoxStyle.Critical, "Decline Error")
            End Try
        End If
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        If frmmain IsNot Nothing Then
            frmmain.Show()
        End If
        Me.Hide()
    End Sub

    Private Sub BorrowPendingRequest_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

    Private Function GetUserReturnDate() As DateTime
        Return DateTime.Now.AddDays(7)
    End Function


    Private Sub GenerateBorrowReceipt(borrowID As String)
        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim dt As New DataTable("BorrowReceipt")
            dt.Columns.Add("Borrow ID", GetType(String))
            dt.Columns.Add("Book ID", GetType(String))
            dt.Columns.Add("Title", GetType(String))
            dt.Columns.Add("ISBN", GetType(String))
            dt.Columns.Add("User ID", GetType(String))
            dt.Columns.Add("Borrower Name", GetType(String))
            dt.Columns.Add("Borrower Position", GetType(String))
            dt.Columns.Add("Borrower Privileges", GetType(String))
            dt.Columns.Add("Copies", GetType(Integer))
            dt.Columns.Add("Borrow Date", GetType(String))
            dt.Columns.Add("Due Date", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Current Returned", GetType(Integer))
            dt.Columns.Add("Processed By", GetType(String))
            dt.Columns.Add("Request Date", GetType(String))

            Dim borrowQuery As String = "
        SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], 
               [Borrower Privileges], [Copies], [Borrow Date], [Due Date], [Status], 
               [Current Returned], [Processed By], [Request Date]
        FROM borrowings 
        WHERE [Borrow ID] = ?"

            Using cmd As New OleDbCommand(borrowQuery, con)
                cmd.Parameters.AddWithValue("?", borrowID)

                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim bookID As String = reader("Book ID").ToString()
                        Dim title As String = ""
                        Dim isbn As String = ""

                        If Not String.IsNullOrEmpty(bookID) Then
                            Dim bookDetails = GetBookDetails(bookID)
                            title = bookDetails.Title
                            isbn = bookDetails.ISBN
                        End If

                        dt.Rows.Add(
                        reader("Borrow ID").ToString(),
                        bookID,
                        title,
                        isbn,
                        reader("User ID").ToString(),
                        reader("Borrower Name").ToString(),
                        reader("Borrower Position").ToString(),
                        reader("Borrower Privileges").ToString(),
                        If(IsDBNull(reader("Copies")), 0, Convert.ToInt32(reader("Copies"))),
                        If(IsDBNull(reader("Borrow Date")), "", Convert.ToDateTime(reader("Borrow Date")).ToString("MM/dd/yyyy")),
                        If(IsDBNull(reader("Due Date")), "", Convert.ToDateTime(reader("Due Date")).ToString("MM/dd/yyyy")),
                        reader("Status").ToString(),
                        If(IsDBNull(reader("Current Returned")), 0, Convert.ToInt32(reader("Current Returned"))),
                        reader("Processed By").ToString(),
                        If(IsDBNull(reader("Request Date")), "", Convert.ToDateTime(reader("Request Date")).ToString("MM/dd/yyyy"))
                    )
                    End While
                End Using
            End Using

            Dim reportPath As String = Path.Combine(Application.StartupPath, "Reports\CrystalReport2.rpt")

            If Not File.Exists(reportPath) Then
                MsgBox("Report not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                Return
            End If

            Dim report As New ReportDocument()
            report.Load(reportPath)
            report.SetDataSource(dt)

            Dim reportForm As New ReportForm()
            reportForm.CrystalReportViewer1.ReportSource = report
            reportForm.ShowDialog()

            report.Close()
            report.Dispose()

        Catch ex As Exception
            MsgBox("Error generating borrow receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Class BookDetails
        Public Property Title As String
        Public Property ISBN As String
    End Class

    Private Function GetBookDetails(bookID As String) As BookDetails
        Dim details As New BookDetails()

        Try
            Dim bookQuery As String = "SELECT Title, ISBN FROM books WHERE [Book ID] = ?"
            Using cmd As New OleDbCommand(bookQuery, con)
                cmd.Parameters.AddWithValue("?", bookID)
                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        details.Title = If(IsDBNull(reader("Title")), "", reader("Title").ToString())
                        details.ISBN = If(IsDBNull(reader("ISBN")), "", reader("ISBN").ToString())
                    Else
                        details.Title = "Book not found"
                        details.ISBN = "N/A"
                    End If
                End Using
            End Using
        Catch ex As Exception
            details.Title = "Error retrieving title"
            details.ISBN = "Error retrieving ISBN"
        End Try

        Return details
    End Function

    Private Sub ViewTransactionDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewTransactionDetailsToolStripMenuItem.Click
        If dg.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dg.SelectedRows(0)

            selectedBorrowID = selectedRow.Cells("Borrow ID").Value.ToString()
            selectedUserID = selectedRow.Cells("User ID").Value.ToString()
            selectedBorrowerName = selectedRow.Cells("Borrower Name").Value.ToString()
            selectedBorrowerPosition = selectedRow.Cells("Borrower Position").Value.ToString()
            selectedBorrowerPrivilege = selectedRow.Cells("Borrower Privileges").Value.ToString()
            selectedBorrowDate = "Not yet approved"
            selectedDueDate = "Not yet approved"
            selectedStatus = "Requested"
            selectedBookIDList = selectedRow.Cells("Book ID List").Value.ToString()
            selectedCopyList = selectedRow.Cells("Copy List").Value.ToString()

            Dim viewer As New TransactionViewer()
            viewer.Text = "Transaction Details - Borrow ID: " & selectedBorrowID
            viewer.Show()
        Else
            MsgBox("Please select a transaction to view details.", MsgBoxStyle.Exclamation, "Select Transaction")
        End If
    End Sub

    Private Sub AddToTransactionReceipt(ByVal borrowID As String, ByVal userID As String, ByVal userName As String)
        Try
            cmd = New OleDbCommand("SELECT [Book ID List], [Copy List] FROM transactions WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim bookIDList As String = reader("Book ID List").ToString()
                Dim copyList As String = reader("Copy List").ToString()
                reader.Close()

                Dim receiptID As String = GenerateBorrowReceiptID()
                Dim receiptDate As String = DateTime.Now.ToString("MM/dd/yyyy")

                cmd = New OleDbCommand("INSERT INTO borrowReceipts ([Receipt ID], [Borrow ID], [Book ID List], [Copy List], [Receipt Date], [User ID], [User Name], [Processed By]) VALUES (?, ?, ?, ?, ?, ?, ? ,?)", con)
                cmd.Parameters.AddWithValue("?", receiptID)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.Parameters.AddWithValue("?", bookIDList)
                cmd.Parameters.AddWithValue("?", copyList)
                cmd.Parameters.AddWithValue("?", receiptDate)
                cmd.Parameters.AddWithValue("?", userID)
                cmd.Parameters.AddWithValue("?", userName)
                cmd.Parameters.AddWithValue("?", XName)

                cmd.ExecuteNonQuery()

                MsgBox("Borrow receipt created! Receipt ID: " & receiptID, MsgBoxStyle.Information, "Receipt Generated")
            Else
                reader.Close()
                MsgBox("Could not find transaction details for Borrow ID: " & borrowID, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error creating transaction receipt: " & ex.Message, MsgBoxStyle.Exclamation, "Warning")
        End Try
    End Sub


    Private Function GetUserIDFromBorrowID(ByVal borrowID As String) As String
        Try
            Dim userID As String = ""
            cmd = New OleDbCommand("SELECT [User ID] FROM transactions WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                userID = result.ToString()
            End If
            Return userID
        Catch ex As Exception
            Return ""
        End Try
    End Function



    Private Function GeBorrowerNameFromBorrowID(ByVal borrowID As String) As String
        Try
            Dim userID As String = ""
            cmd = New OleDbCommand("SELECT [Borrower Name] FROM transactions WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                userID = result.ToString()
            End If
            Return userID
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GenerateBorrowReceiptID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "BOR"
        Dim datePart As String = DateTime.Now.ToString("yyyyMMdd")
        Dim table As String = "borrowReceipts"
        Dim attempts As Integer = 0
        Dim maxAttempts As Integer = 3

        Dim fullPrefix As String = prefix & "-" & datePart & "-"

        While attempts < maxAttempts
            Try
                If con.State <> ConnectionState.Open Then
                    OpenDB()
                End If

                Dim transaction As OleDbTransaction = con.BeginTransaction()

                Try
                    cmd = New OleDbCommand("SELECT MAX([Receipt ID]) FROM " & table & " WHERE [Receipt ID] LIKE '" & fullPrefix & "%'", con, transaction)
                    Dim result As Object = cmd.ExecuteScalar()

                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Dim lastID As String = result.ToString()
                        If lastID.StartsWith(fullPrefix) Then
                            Dim numberPart As String = lastID.Substring(fullPrefix.Length)
                            If Integer.TryParse(numberPart, maxNumber) Then
                                maxNumber += 1
                            Else
                                maxNumber = 1
                            End If
                        Else
                            maxNumber = 1
                        End If
                    Else
                        maxNumber = 1
                    End If

                    Dim newReceiptID As String = fullPrefix & maxNumber.ToString("D3")
                    cmd = New OleDbCommand("SELECT COUNT(*) FROM " & table & " WHERE [Receipt ID] = @newID", con, transaction)
                    cmd.Parameters.AddWithValue("@newID", newReceiptID)
                    Dim count As Integer = CInt(cmd.ExecuteScalar())

                    If count = 0 Then
                        transaction.Commit()
                        Return newReceiptID
                    Else
                        maxNumber += 1
                    End If

                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try

            Catch ex As Exception
                attempts += 1
                If attempts >= maxAttempts Then
                    Return fullPrefix & DateTime.Now.ToString("HHmmss") & "-" & Guid.NewGuid().ToString("N").Substring(0, 4)
                End If
                System.Threading.Thread.Sleep(100)
            End Try
        End While

        Return fullPrefix & "001"
    End Function
End Class