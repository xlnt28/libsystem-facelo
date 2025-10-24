Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class AdminReturn
    Private showOnlyReturnRequests As Boolean = False
    Private receiptIdForDisplay As String = ""
    Private Sub AdminReturn_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        CustomizeDataGridView(dgv)
        LoadAllBorrowedItems()
    End Sub

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
    End Sub

    Private Sub LoadAllBorrowedItems()
        Try
            SQLQueryForAllBorrowed()
            If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("transactions") AndAlso admindbds.Tables("transactions").Rows.Count > 0 Then
                dgv.DataSource = admindbds.Tables("transactions")
                dgv.ClearSelection()
            Else
                dgv.DataSource = admindbds.Tables("transactions")
                dgv.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox("Error loading items: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub SQLQueryForAllBorrowed(Optional ByVal onlyReturnRequests As Boolean = False, Optional ByVal searchName As String = "")
        Try
            Dim sql As String

            If onlyReturnRequests Then
                sql = "SELECT [Transaction ID], [Borrow ID], [Book ID List], [User ID], [Borrower Name], [Borrower Position], " &
          "[Borrower Privileges], [Copy List], [Current Returned], [Borrow Date], " &
          "[Due Date], [Status], [Has Requested Return] " &
          "FROM transactions " &
          "WHERE ([Status] = 'Borrowed' AND [Has Requested Return] = 'Yes')"
            Else
                sql = "SELECT [Transaction ID], [Borrow ID], [Book ID List], [User ID], [Borrower Name], [Borrower Position], " &
          "[Borrower Privileges], [Copy List], [Current Returned], [Borrow Date], " &
          "[Due Date], [Status], [Has Requested Return] " &
          "FROM transactions " &
          "WHERE ([Status] = 'Borrowed')"
            End If

            If Not String.IsNullOrEmpty(searchName) Then
                sql &= " AND [Borrower Name] LIKE ?"
            End If

            sql &= " ORDER BY [Borrow ID] DESC"

            daBorrowHistory = New OleDbDataAdapter(sql, con)

            If admindbds Is Nothing Then
                admindbds = New DataSet()
            Else
                If admindbds.Tables.Contains("transactions") Then
                    admindbds.Tables("transactions").Clear()
                End If
            End If

            If Not String.IsNullOrEmpty(searchName) Then
                daBorrowHistory.SelectCommand.Parameters.AddWithValue("?", "%" & searchName.Trim() & "%")
            End If

            daBorrowHistory.Fill(admindbds, "transactions")

        Catch ex As Exception
            MsgBox("Error retrieving transactions data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub ApproveReturnToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ApproveReturnToolStripMenuItem.Click
        If dgv.SelectedRows.Count = 0 Then
            MsgBox("Please select a record to approve.", MsgBoxStyle.Exclamation, "Select Record")
            Return
        End If
        Dim selectedRow As DataGridViewRow = dgv.SelectedRows(0)
        ProcessReturn(selectedRow)
    End Sub

    Private Sub ProcessReturn(ByVal selectedRow As DataGridViewRow)
        If Not ValidateRowData(selectedRow) Then Return

        Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()
        Dim bookIDList As String = selectedRow.Cells("Book ID List").Value.ToString()
        Dim copyList As String = selectedRow.Cells("Copy List").Value.ToString()
        Dim currentReturned As String = If(selectedRow.Cells("Current Returned").Value IsNot Nothing, selectedRow.Cells("Current Returned").Value.ToString(), "0")

        Dim bookIDs() As String = bookIDList.Split(","c)
        Dim totalCopies() As String = copyList.Split(","c)
        Dim currentReturnedArray() As String = currentReturned.Split(","c)

        If bookIDs.Length <> totalCopies.Length Then
            MsgBox("Data inconsistency: Book IDs and Copies count mismatch", MsgBoxStyle.Critical, "Data Error")
            Return
        End If

        Dim totalCopiesInt(bookIDs.Length - 1) As Integer
        Dim currentReturnedInt(bookIDs.Length - 1) As Integer

        For i As Integer = 0 To bookIDs.Length - 1
            If Not Integer.TryParse(totalCopies(i).Trim(), totalCopiesInt(i)) Then
                MsgBox("Invalid total copies value for book: " & bookIDs(i), MsgBoxStyle.Critical, "Data Error")
                Return
            End If

            If i < currentReturnedArray.Length Then
                If Not Integer.TryParse(currentReturnedArray(i).Trim(), currentReturnedInt(i)) Then
                    currentReturnedInt(i) = 0
                End If
            Else
                currentReturnedInt(i) = 0
            End If
        Next

        Using prf As New PartialReturnForm()
            prf.LoadBorrowInfoFromDGV(dgv, borrowID)
            prf.BookIDs = bookIDs
            prf.TotalCopies = totalCopiesInt
            prf.CurrentReturned = currentReturnedInt

            If prf.ShowDialog() = DialogResult.OK Then
                ProcessReturnQuantities(borrowID, bookIDs, totalCopiesInt, currentReturnedInt,
                          prf.GetReturnQuantities(), prf.GetConditionTypes(),
                          prf.GetPenaltyAmounts(), prf.GetTotalPenalty())
            End If
        End Using
    End Sub

    Private Function ValidateRowData(ByVal row As DataGridViewRow) As Boolean
        Dim requiredFields() As String = {"Borrow ID", "Book ID List", "Copy List"}
        For Each field In requiredFields
            If row.Cells(field).Value Is Nothing OrElse String.IsNullOrEmpty(row.Cells(field).Value.ToString()) Then
                MsgBox("Missing required data in field: " & field, MsgBoxStyle.Critical, "Data Error")
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub ProcessReturnQuantities(ByVal borrowID As String, ByVal bookIDs() As String,
                  ByVal totalCopies() As Integer, ByVal currentReturned() As Integer,
                  ByVal copiesToReturn() As Integer, ByVal conditionTypes() As String,
                  ByVal penaltyAmounts() As Decimal, ByVal totalPenalty As Decimal)

        Dim totalReturnCount As Integer = 0
        For i As Integer = 0 To copiesToReturn.Length - 1
            totalReturnCount += copiesToReturn(i)
        Next

        If totalReturnCount = 0 Then
            MsgBox("No copies selected to return.", MsgBoxStyle.Exclamation, "No Return")
            Return
        End If

        For i As Integer = 0 To bookIDs.Length - 1
            If copiesToReturn(i) < 0 OrElse copiesToReturn(i) > (totalCopies(i) - currentReturned(i)) Then
                MsgBox("Invalid return quantity for Book ID " & bookIDs(i), MsgBoxStyle.Critical, "Validation Error")
                Return
            End If
        Next

        Using transaction As OleDbTransaction = con.BeginTransaction()
            Try
                Dim returnDate As DateTime = DateTime.Now

                Dim newCurrentReturnedArray(bookIDs.Length - 1) As String
                Dim allBooksReturned As Boolean = True

                For i As Integer = 0 To bookIDs.Length - 1
                    Dim newCurrentReturned As Integer = currentReturned(i) + copiesToReturn(i)
                    newCurrentReturnedArray(i) = newCurrentReturned.ToString()

                    If newCurrentReturned < totalCopies(i) Then
                        allBooksReturned = False
                    End If
                Next

                Dim newCurrentReturnedString As String = String.Join(",", newCurrentReturnedArray)
                Dim newStatus As String = If(allBooksReturned, "Completed", "Borrowed")

                Using cmd As New OleDbCommand("UPDATE transactions SET [Current Returned] = ?, [Status] = ?, [Has Requested Return] = ? WHERE [Borrow ID] = ?", con, transaction)
                    cmd.Parameters.AddWithValue("?", newCurrentReturnedString)
                    cmd.Parameters.AddWithValue("?", newStatus)
                    cmd.Parameters.AddWithValue("?", "No")
                    cmd.Parameters.AddWithValue("?", borrowID)
                    cmd.ExecuteNonQuery()
                End Using

                For i As Integer = 0 To bookIDs.Length - 1
                    If copiesToReturn(i) > 0 Then
                        Try
                            Dim bookID As String = bookIDs(i).Trim()

                            CreateReturnLog(borrowID, bookID, copiesToReturn(i), transaction)

                            Dim individualCurrentReturned As Integer = GetIndividualCurrentReturned(borrowID, bookID)
                            Dim newIndividualReturned As Integer = individualCurrentReturned + copiesToReturn(i)
                            Dim individualAllReturned As Boolean = (newIndividualReturned >= GetIndividualTotalCopies(borrowID, bookID))

                            Using cmd As New OleDbCommand("UPDATE borrowings SET [Current Returned] = ?, [Status] = ? WHERE [Borrow ID] = ? AND [Book ID] = ?", con, transaction)
                                cmd.Parameters.AddWithValue("?", newIndividualReturned.ToString())
                                cmd.Parameters.AddWithValue("?", If(individualAllReturned, "Completed", "Borrowed"))
                                cmd.Parameters.AddWithValue("?", borrowID)
                                cmd.Parameters.AddWithValue("?", bookID)
                                cmd.ExecuteNonQuery()
                            End Using

                            If conditionTypes(i) = "Normal" Then
                                Using cmd As New OleDbCommand("UPDATE books SET [Quantity] = [Quantity] + ? WHERE [Book ID] = ?", con, transaction)
                                    cmd.Parameters.AddWithValue("?", copiesToReturn(i))
                                    cmd.Parameters.AddWithValue("?", bookID)
                                    cmd.ExecuteNonQuery()
                                End Using
                            ElseIf conditionTypes(i) = "Damaged" Then
                                Using cmd As New OleDbCommand("UPDATE books SET [Quantity] = [Quantity] + ? WHERE [Book ID] = ?", con, transaction)
                                    cmd.Parameters.AddWithValue("?", copiesToReturn(i))
                                    cmd.Parameters.AddWithValue("?", bookID)
                                    cmd.ExecuteNonQuery()
                                End Using
                            End If

                            Using cmd As New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con, transaction)
                                cmd.Parameters.AddWithValue("?", bookID)
                                Dim currentQtyObj = cmd.ExecuteScalar()
                                Dim currentQty As Integer = If(currentQtyObj IsNot Nothing AndAlso Not IsDBNull(currentQtyObj), Convert.ToInt32(currentQtyObj), 0)

                                Using updateCmd As New OleDbCommand("UPDATE books SET [Status] = ? WHERE [Book ID] = ?", con, transaction)
                                    updateCmd.Parameters.AddWithValue("?", If(currentQty > 0, "Available", "Unavailable"))
                                    updateCmd.Parameters.AddWithValue("?", bookID)
                                    updateCmd.ExecuteNonQuery()
                                End Using
                            End Using
                        Catch ex As Exception
                            Throw New Exception("Error processing book " & bookIDs(i) & ": " & ex.Message)
                        End Try
                    End If
                Next

                If totalPenalty > 0 Then
                    InsertPenaltyRecord(borrowID, bookIDs, copiesToReturn, conditionTypes, penaltyAmounts, totalPenalty, returnDate, transaction)
                End If

                GenerateReturnReceipt(borrowID, bookIDs, copiesToReturn, conditionTypes, penaltyAmounts, totalPenalty, returnDate, transaction)

                transaction.Commit()

                If Not String.IsNullOrEmpty(receiptIdForDisplay) Then
                    DisplayReturnReceipt(receiptIdForDisplay)
                End If

                MsgBox("Return approved successfully.", MsgBoxStyle.Information, "Success")
                LoadAllBorrowedItems()

            Catch ex As Exception
                transaction.Rollback()
                MsgBox("Error approving return: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Using
    End Sub

    Private Sub CreateReturnLog(ByVal borrowID As String, ByVal bookID As String, ByVal returnedQuantity As Integer, ByVal transaction As OleDbTransaction)
        Try
            Dim returnID As String = GenerateNextReturnID(transaction)
            Dim returnDate As DateTime = DateTime.Now

            Dim bookISBN As String = ""
            Dim bookTitle As String = ""
            Dim borrowerName As String = ""

            Using cmdDetails As New OleDbCommand("SELECT [ISBN], [Title], [Borrower Name] FROM borrowings WHERE [Borrow ID] = ? AND [Book ID] = ?", con, transaction)
                cmdDetails.Parameters.AddWithValue("?", borrowID)
                cmdDetails.Parameters.AddWithValue("?", bookID)
                Using reader As OleDbDataReader = cmdDetails.ExecuteReader()
                    If reader.Read() Then
                        bookISBN = If(IsDBNull(reader("ISBN")), "", reader("ISBN").ToString())
                        bookTitle = If(IsDBNull(reader("Title")), "", reader("Title").ToString())
                        borrowerName = If(IsDBNull(reader("Borrower Name")), "", reader("Borrower Name").ToString())
                    End If
                End Using
            End Using

            Using cmd As New OleDbCommand("
    INSERT INTO returnLog 
    ([ReturnID], [Borrow ID], [Book ID], [ISBN], [Title], [Returned Quantity], [Return Date], [Processed By], [Borrower Name]) 
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", con, transaction)

                cmd.Parameters.AddWithValue("?", returnID)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.Parameters.AddWithValue("?", bookID)
                cmd.Parameters.AddWithValue("?", bookISBN)
                cmd.Parameters.AddWithValue("?", bookTitle)

                Dim param As New OleDbParameter("?", OleDbType.Integer)
                param.Value = returnedQuantity
                cmd.Parameters.Add(param)

                Dim dateParam As New OleDbParameter("?", OleDbType.Date)
                dateParam.Value = returnDate
                cmd.Parameters.Add(dateParam)

                cmd.Parameters.AddWithValue("?", XName)
                cmd.Parameters.AddWithValue("?", borrowerName)

                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw New Exception("Error creating return log: " & ex.Message)
        End Try
    End Sub

    Private Function GenerateNextReturnID(ByVal transaction As OleDbTransaction) As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "RTN-"

        Try
            Using cmd As New OleDbCommand("SELECT MAX([ReturnID]) FROM returnLog WHERE [ReturnID] LIKE '" & prefix & "%'", con, transaction)
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Dim lastID As String = result.ToString()
                    If lastID.StartsWith(prefix) Then
                        Dim numberPart As String = lastID.Substring(prefix.Length)
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

                Return prefix & maxNumber.ToString("D5")
            End Using
        Catch ex As Exception
            Return prefix & DateTime.Now.ToString("yyyyMMddHHmmss") & Guid.NewGuid().ToString("N").Substring(0, 4)
        End Try
    End Function

    Private Function GenerateNextReturnID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "RTN-"

        Try
            Using cmd As New OleDbCommand("SELECT MAX([ReturnID]) FROM returnLog WHERE [ReturnID] LIKE '" & prefix & "%'", con)
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Dim lastID As String = result.ToString()
                    If lastID.StartsWith(prefix) Then
                        Dim numberPart As String = lastID.Substring(prefix.Length)
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

                Return prefix & maxNumber.ToString("D5")
            End Using
        Catch ex As Exception
            Return prefix & "00001"
        End Try
    End Function

    Private Sub GenerateReturnReceipt(ByVal borrowID As String, ByVal bookIDs() As String,
              ByVal copiesToReturn() As Integer, ByVal conditionTypes() As String,
              ByVal penaltyAmounts() As Decimal, ByVal totalPenalty As Decimal,
              ByVal returnDate As DateTime, ByVal transaction As OleDbTransaction)
        Try
            Dim receiptId As String = GenerateReturnReceiptID()

            Dim borrowerName As String = ""
            Dim userID As String = ""
            Using cmdBorrower As New OleDbCommand("SELECT [Borrower Name], [User ID] FROM transactions WHERE [Borrow ID] = ?", con, transaction)
                cmdBorrower.Parameters.AddWithValue("?", borrowID)
                Using reader As OleDbDataReader = cmdBorrower.ExecuteReader()
                    If reader.Read() Then
                        borrowerName = If(IsDBNull(reader("Borrower Name")), "", reader("Borrower Name").ToString())
                        userID = If(IsDBNull(reader("User ID")), "", reader("User ID").ToString())
                    End If
                End Using
            End Using

            For i As Integer = 0 To bookIDs.Length - 1
                If copiesToReturn(i) > 0 Then
                    Dim bookID As String = bookIDs(i).Trim()

                    Dim bookISBN As String = ""
                    Dim bookTitle As String = ""
                    Using cmdBook As New OleDbCommand("SELECT [ISBN], [Title] FROM books WHERE [Book ID] = ?", con, transaction)
                        cmdBook.Parameters.AddWithValue("?", bookID)
                        Using bookReader As OleDbDataReader = cmdBook.ExecuteReader()
                            If bookReader.Read() Then
                                bookISBN = If(IsDBNull(bookReader("ISBN")), "", bookReader("ISBN").ToString())
                                bookTitle = If(IsDBNull(bookReader("Title")), "", bookReader("Title").ToString())
                            End If
                        End Using
                    End Using

                    Using cmd As New OleDbCommand("
    INSERT INTO returnReceipts 
    ([Receipt ID], [Borrow ID], [Book ID], [Copy], [User ID], [Borrower Name], [Processed By], [Return Date], [Title], [ISBN], [Condition]) 
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", con, transaction)

                        cmd.Parameters.AddWithValue("?", receiptId)
                        cmd.Parameters.AddWithValue("?", borrowID)
                        cmd.Parameters.AddWithValue("?", bookID)
                        cmd.Parameters.AddWithValue("?", copiesToReturn(i).ToString())
                        cmd.Parameters.AddWithValue("?", userID)
                        cmd.Parameters.AddWithValue("?", borrowerName)
                        cmd.Parameters.AddWithValue("?", XName)
                        cmd.Parameters.AddWithValue("?", returnDate.ToString("MM/dd/yyyy"))
                        cmd.Parameters.AddWithValue("?", bookTitle)
                        cmd.Parameters.AddWithValue("?", bookISBN)
                        cmd.Parameters.AddWithValue("?", conditionTypes(i))

                        cmd.ExecuteNonQuery()
                    End Using
                End If
            Next

            receiptIdForDisplay = receiptId

        Catch ex As Exception
            Throw New Exception("Error generating return receipt: " & ex.Message)
        End Try
    End Sub

    Private Sub DisplayReturnReceipt(ByVal receiptId As String)
        Try
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If

            Dim reportForm As New ReportForm()
            Dim report As New ReportDocument()
            Dim reportPath As String = Path.Combine(Application.StartupPath, "Reports\CRReturnBook.rpt")

            If Not File.Exists(reportPath) Then
                MsgBox("Return receipt report not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                Return
            End If

            Dim query As String = "SELECT * FROM returnReceipts WHERE [Receipt ID] = ?"
            Using da As New OleDbDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("?", receiptId)
                Dim dt As New DataTable()
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    report.Load(reportPath)
                    report.SetDataSource(dt)
                    reportForm.CrystalReportViewer1.ReportSource = report
                    reportForm.ShowDialog()
                Else
                    MsgBox("Receipt data not found.", MsgBoxStyle.Exclamation)
                End If
            End Using

            report.Close()
            report.Dispose()
            reportForm.Dispose()

        Catch ex As Exception
            MsgBox("Error displaying return receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GetIndividualCurrentReturned(ByVal borrowID As String, ByVal bookID As String) As Integer
        Try
            Using cmd As New OleDbCommand("SELECT [Current Returned] FROM borrowings WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.Parameters.AddWithValue("?", bookID)
                Dim result As Object = cmd.ExecuteScalar()
                Dim parsedValue As Integer
                If result IsNot Nothing AndAlso Not IsDBNull(result) AndAlso Integer.TryParse(result.ToString(), parsedValue) Then
                    Return parsedValue
                End If
                Return 0
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetIndividualTotalCopies(ByVal borrowID As String, ByVal bookID As String) As Integer
        Try
            Using cmd As New OleDbCommand("SELECT [Copies] FROM borrowings WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.Parameters.AddWithValue("?", bookID)
                Dim result As Object = cmd.ExecuteScalar()
                Dim parsedValue As Integer
                If result IsNot Nothing AndAlso Not IsDBNull(result) AndAlso Integer.TryParse(result.ToString(), parsedValue) Then
                    Return parsedValue
                End If
                Return 0
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub InsertPenaltyRecord(ByVal borrowID As String, ByVal bookIDs() As String,
                      ByVal copiesToReturn() As Integer, ByVal conditionTypes() As String,
                      ByVal penaltyAmounts() As Decimal, ByVal totalPenalty As Decimal,
                      ByVal returnDate As DateTime, ByVal transaction As OleDbTransaction)
        Try
            Dim borrowerName As String = ""
            Dim dueDate As DateTime = DateTime.Now
            Dim borrowerPrivilege As String = ""

            Using cmdName As New OleDbCommand("SELECT [Borrower Name], [Due Date], [Borrower Privileges] FROM transactions WHERE [Borrow ID] = ?", con, transaction)
                cmdName.Parameters.AddWithValue("?", borrowID)
                Using reader As OleDbDataReader = cmdName.ExecuteReader()
                    If reader.Read() Then
                        If Not IsDBNull(reader("Borrower Name")) Then borrowerName = reader("Borrower Name").ToString()
                        If Not IsDBNull(reader("Due Date")) Then dueDate = Convert.ToDateTime(reader("Due Date"))
                        If Not IsDBNull(reader("Borrower Privileges")) Then borrowerPrivilege = reader("Borrower Privileges").ToString()
                    End If
                End Using
            End Using

            Dim daysLate As Integer = Math.Max(0, (returnDate - dueDate).Days)
            Dim isAdminPrivilege As Boolean = (borrowerPrivilege.ToUpper() = "ADMIN")

            For i As Integer = 0 To bookIDs.Length - 1
                If copiesToReturn(i) > 0 Then
                    Dim penaltyAmount As Decimal = 0

                    If conditionTypes(i) = "Normal" Then
                        If isAdminPrivilege Then
                            penaltyAmount = 0
                        Else
                            penaltyAmount = daysLate * 10 * copiesToReturn(i)
                        End If
                    Else
                        penaltyAmount = penaltyAmounts(i) * copiesToReturn(i)
                    End If

                    If penaltyAmount > 0 Then
                        Dim penaltyID As String = GenerateNextPenaltyID()

                        Using insertCmd As New OleDbCommand("
            INSERT INTO Penalties 
            ([PenaltyID], [Borrow ID], [Book ID], [Quantity], [Book Condition], [Condition Penalty], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [User Name], [Processed By]) 
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", con, transaction)

                            insertCmd.Parameters.AddWithValue("?", penaltyID)
                            insertCmd.Parameters.AddWithValue("?", borrowID)
                            insertCmd.Parameters.AddWithValue("?", bookIDs(i))
                            insertCmd.Parameters.AddWithValue("?", copiesToReturn(i))
                            insertCmd.Parameters.AddWithValue("?", conditionTypes(i))
                            insertCmd.Parameters.AddWithValue("?", penaltyAmounts(i))
                            insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Integer) With {.Value = daysLate})
                            insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Decimal) With {.Value = penaltyAmount})
                            insertCmd.Parameters.AddWithValue("?", "Unpaid")
                            insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = dueDate})
                            insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = returnDate})
                            insertCmd.Parameters.AddWithValue("?", borrowerName)
                            insertCmd.Parameters.AddWithValue("?", XName)
                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End If
            Next

        Catch ex As Exception
            Throw New Exception("Error creating penalty record: " & ex.Message)
        End Try
    End Sub

    Private Function GenerateNextPenaltyID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "PI-"

        Try
            Using cmd As New OleDbCommand("SELECT MAX([PenaltyID]) FROM Penalties WHERE [PenaltyID] LIKE '" & prefix & "%'", con)
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Dim lastID As String = result.ToString()
                    If lastID.StartsWith(prefix) Then
                        Dim numberPart As String = lastID.Substring(prefix.Length)
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

                Return prefix & maxNumber.ToString("D5")
            End Using
        Catch ex As Exception
            Dim random As New Random()
            Return prefix & DateTime.Now.ToString("HHmmss") & random.Next(10, 99).ToString()
        End Try
    End Function

    Private Sub ShowReturnRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowReturnRequestToolStripMenuItem.Click
        showOnlyReturnRequests = Not showOnlyReturnRequests

        If showOnlyReturnRequests Then
            ShowReturnRequestToolStripMenuItem.Text = "Show All Borrowed Books"
        Else
            ShowReturnRequestToolStripMenuItem.Text = "Show Return Requests Only"
        End If

        SQLQueryForAllBorrowed(showOnlyReturnRequests)

        If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("transactions") Then
            dgv.DataSource = admindbds.Tables("transactions")
            dgv.ClearSelection()
        End If
    End Sub

    Private Sub SearchUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchUserToolStripMenuItem.Click
        Dim inputName As String = InputBox("Enter Borrower Name to search:", "Search Borrower")
        If String.IsNullOrEmpty(inputName) Then Return

        Try
            SQLQueryForAllBorrowed(showOnlyReturnRequests, inputName)

            If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("transactions") AndAlso admindbds.Tables("transactions").Rows.Count > 0 Then
                dgv.DataSource = admindbds.Tables("transactions")
                dgv.ClearSelection()
            Else
                MsgBox("No records found for '" & inputName & "'.", MsgBoxStyle.Information, "No Results")
            End If
        Catch ex As Exception
            MsgBox("Error searching for user: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Try
            LoadAllBorrowedItems()
            MsgBox("Data refreshed successfully.", MsgBoxStyle.Information, "Refreshed")
        Catch ex As Exception
            MsgBox("Error refreshing data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Show()
        Me.Close()
    End Sub

    Private Sub ViewTransactionDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewTransactionDetailToolStripMenuItem.Click
        If dgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dgv.SelectedRows(0)

            selectedBorrowID = selectedRow.Cells("Borrow ID").Value.ToString()
            selectedUserID = selectedRow.Cells("User ID").Value.ToString()
            selectedBorrowerName = selectedRow.Cells("Borrower Name").Value.ToString()
            selectedBorrowerPosition = selectedRow.Cells("Borrower Position").Value.ToString()
            selectedBorrowerPrivilege = selectedRow.Cells("Borrower Privileges").Value.ToString()
            selectedBorrowDate = selectedRow.Cells("Borrow Date").Value.ToString()
            selectedDueDate = selectedRow.Cells("Due Date").Value.ToString()
            selectedStatus = selectedRow.Cells("Status").Value.ToString()
            selectedBookIDList = selectedRow.Cells("Book ID List").Value.ToString()
            selectedCopyList = selectedRow.Cells("Copy List").Value.ToString()

            Dim viewer As New TransactionViewer()
            viewer.Text = "Transaction Details - Borrow ID: " & selectedBorrowID
            viewer.Show()
        Else
            MsgBox("Please select a transaction to view details.", MsgBoxStyle.Exclamation, "Select Transaction")
        End If
    End Sub

    Public Function GenerateReturnReceiptID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "RET"
        Dim datePart As String = DateTime.Now.ToString("yyyyMMdd")
        Dim fullPrefix As String = prefix & "-" & datePart & "-"

        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" &
                                     Application.StartupPath & "\Database\library.mdb"

        Using separateCon As New OleDbConnection(connStr)
            Try
                separateCon.Open()

                Using cmd As New OleDbCommand("SELECT MAX([Receipt ID]) FROM returnReceipts WHERE [Receipt ID] LIKE '" & fullPrefix & "%'", separateCon)
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
                End Using

                Return fullPrefix & maxNumber.ToString("D3")

            Catch ex As Exception
                Return fullPrefix & DateTime.Now.ToString("HHmmss") & "-" & Guid.NewGuid().ToString("N").Substring(0, 4)
            End Try
        End Using
    End Function

End Class