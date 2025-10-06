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

            RemoveHandler dg.CellFormatting, AddressOf dg_CellFormatting
            AddHandler dg.CellFormatting, AddressOf dg_CellFormatting

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

    Private Sub dg_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return

        Try
            If e.Value IsNot Nothing AndAlso TypeOf e.Value Is DateTime Then
                e.Value = CType(e.Value, DateTime).ToString("MM/dd/yyyy")
                e.FormattingApplied = True
            ElseIf e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
                Dim dateValue As DateTime
                If DateTime.TryParse(e.Value.ToString(), dateValue) Then
                    e.Value = dateValue.ToString("MM/dd/yyyy")
                    e.FormattingApplied = True
                End If
            End If
        Catch ex As Exception
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

        ' Get the book IDs and copies from the borrowings table instead of transactions
        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            ' Get all books for this borrow ID from borrowings table
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

            cmd = New OleDbCommand("UPDATE transactions SET [Status] = 'Borrowed', [Borrow Date] = ?, [Due Date] = ? WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", formattedCurrentDate)
            cmd.Parameters.AddWithValue("?", formattedDueDate)
            cmd.Parameters.AddWithValue("?", borrowID)
            cmd.ExecuteNonQuery()

            While reader.Read()
                Dim bookID As String = reader("Book ID").ToString()
                Dim quantityToBorrow As Integer = Integer.Parse(reader("Copies").ToString())

                cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Borrowed', [Borrow Date] = ?, [Due Date] = ? WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
                cmd.Parameters.AddWithValue("?", formattedCurrentDate)
                cmd.Parameters.AddWithValue("?", formattedDueDate)
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

                cmd = New OleDbCommand("UPDATE transactions SET [Status] = 'Declined' WHERE [Borrow ID] = ?", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()

                cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Declined' WHERE [Borrow ID] = ?", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()

                MsgBox("Borrow request declined successfully!", MsgBoxStyle.Information, "Decline Complete")
                LoadPendingRequests()

            Catch ex As Exception
                MsgBox("Error rejecting request: " + ex.Message, MsgBoxStyle.Critical, "Decline Error")
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
            Dim dt As New DataTable("BorrowReceipt")
            dt.Columns.Add("Borrow ID", GetType(String))
            dt.Columns.Add("Book ID", GetType(String))
            dt.Columns.Add("User ID", GetType(String))
            dt.Columns.Add("Borrower Name", GetType(String))
            dt.Columns.Add("Borrower Position", GetType(String))
            dt.Columns.Add("Borrower Privileges", GetType(String))
            dt.Columns.Add("Copies", GetType(Integer))
            dt.Columns.Add("Borrow Date", GetType(String))
            dt.Columns.Add("Due Date", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Current Returned", GetType(Integer))
            dt.Columns.Add("Has Requested Return", GetType(String))
            dt.Columns.Add("Request Date", GetType(String))

            Dim query As String = "SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Borrow Date], [Due Date], [Status], [Current Returned], [Has Requested Return], [Request Date] FROM borrowings WHERE [Borrow ID] = ?"
            Using cmd As New OleDbCommand(query, con)
                cmd.Parameters.AddWithValue("?", borrowID)
                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        dt.Rows.Add(
                        reader("Borrow ID").ToString(),
                        reader("Book ID").ToString(),
                        reader("User ID").ToString(),
                        reader("Borrower Name").ToString(),
                        reader("Borrower Position").ToString(),
                        reader("Borrower Privileges").ToString(),
                        If(IsDBNull(reader("Copies")), 0, Convert.ToInt32(reader("Copies"))),
                        If(IsDBNull(reader("Borrow Date")), "", Convert.ToDateTime(reader("Borrow Date")).ToString("MM/dd/yyyy")),
                        If(IsDBNull(reader("Due Date")), "", Convert.ToDateTime(reader("Due Date")).ToString("MM/dd/yyyy")),
                        reader("Status").ToString(),
                        If(IsDBNull(reader("Current Returned")), 0, Convert.ToInt32(reader("Current Returned"))),
                        reader("Has Requested Return").ToString(),
                        If(IsDBNull(reader("Request Date")), "", Convert.ToDateTime(reader("Request Date")).ToString("MM/dd/yyyy"))
                    )
                    End While
                End Using
            End Using

            Dim report As New ReportDocument()
            Dim reportPath As String = Path.Combine(Application.StartupPath, "Reports\CrystalReport2.rpt")

            If Not File.Exists(reportPath) Then
                MsgBox("Report not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                Return
            End If

            report.Load(reportPath)
            report.SetDataSource(dt)
            report.PrintToPrinter(1, False, 0, 0)

        Catch ex As Exception
            MsgBox("Error generating borrow receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class