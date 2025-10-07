Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class AdminReturn
    Private showOnlyReturnRequests As Boolean = False

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

        If hasClickedTheCurrentRequestedReturn Then
            ShowReturnRequestToolStripMenuItem.PerformClick()
            hasClickedTheCurrentRequestedReturn = False
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
                MsgBox("No borrowed items found.", MsgBoxStyle.Information, "Information")
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
                      "[Borrower Privileges], [Copy List], [Current Returned], [Request Date], [Borrow Date], " &
                      "[Due Date], [Status], [Has Requested Return] " &
                      "FROM transactions " &
                      "WHERE ([Status] = 'Borrowed' AND [Has Requested Return] = 'Yes')"
            Else
                sql = "SELECT [Transaction ID], [Borrow ID], [Book ID List], [User ID], [Borrower Name], [Borrower Position], " &
                      "[Borrower Privileges], [Copy List], [Current Returned], [Request Date], [Borrow Date], " &
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

            Dim returnDate As DateTime = DateTime.Now

            Try
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

                cmd = New OleDbCommand("UPDATE transactions SET [Current Returned] = ?, [Status] = ?, [Has Requested Return] = ? WHERE [Borrow ID] = ?", con)
                cmd.Parameters.AddWithValue("?", newCurrentReturnedString)
                cmd.Parameters.AddWithValue("?", newStatus)
                cmd.Parameters.AddWithValue("?", "No")
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()

                For i As Integer = 0 To bookIDs.Length - 1
                    If copiesToReturn(i) > 0 Then
                        Dim bookID As String = bookIDs(i).Trim()

                        Dim individualCurrentReturned As Integer = GetIndividualCurrentReturned(borrowID, bookID)
                        Dim newIndividualReturned As Integer = individualCurrentReturned + copiesToReturn(i)
                        Dim individualAllReturned As Boolean = (newIndividualReturned >= GetIndividualTotalCopies(borrowID, bookID))

                        cmd = New OleDbCommand("UPDATE borrowings SET [Current Returned] = ?, [Status] = ?, [Has Requested Return] = ? WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
                        cmd.Parameters.AddWithValue("?", newIndividualReturned.ToString())
                        cmd.Parameters.AddWithValue("?", If(individualAllReturned, "Completed", "Borrowed"))
                        cmd.Parameters.AddWithValue("?", "No")
                        cmd.Parameters.AddWithValue("?", borrowID)
                        cmd.Parameters.AddWithValue("?", bookID)
                        cmd.ExecuteNonQuery()

                        CreateReturnLog(borrowID, bookID, copiesToReturn(i))

                        If conditionTypes(i) = "Normal" Then
                            cmd = New OleDbCommand("UPDATE books SET [Quantity] = [Quantity] + ? WHERE [Book ID] = ?", con)
                            cmd.Parameters.AddWithValue("?", copiesToReturn(i))
                            cmd.Parameters.AddWithValue("?", bookID)
                            cmd.ExecuteNonQuery()
                        ElseIf conditionTypes(i) = "Lost" Then
                            cmd = New OleDbCommand("UPDATE books SET [Quantity] = [Quantity] - ? WHERE [Book ID] = ?", con)
                            cmd.Parameters.AddWithValue("?", copiesToReturn(i))
                            cmd.Parameters.AddWithValue("?", bookID)
                            cmd.ExecuteNonQuery()
                        End If

                        cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                        cmd.Parameters.AddWithValue("?", bookID)
                        Dim currentQtyObj = cmd.ExecuteScalar()
                        Dim currentQty As Integer = If(currentQtyObj IsNot Nothing AndAlso Not IsDBNull(currentQtyObj), Convert.ToInt32(currentQtyObj), 0)

                        cmd = New OleDbCommand("UPDATE books SET [Status] = ? WHERE [Book ID] = ?", con)
                        cmd.Parameters.AddWithValue("?", If(currentQty > 0, "Available", "Unavailable"))
                        cmd.Parameters.AddWithValue("?", bookID)
                        cmd.ExecuteNonQuery()
                    End If
                Next

                If totalPenalty > 0 Then
                    InsertPenaltyRecord(borrowID, bookIDs, copiesToReturn, conditionTypes, penaltyAmounts, totalPenalty, returnDate)
                End If

                GenerateReturnReceipt(borrowID, bookIDs, copiesToReturn, conditionTypes, penaltyAmounts, totalPenalty, returnDate)

                MsgBox("Return approved successfully.", MsgBoxStyle.Information, "Success")
                LoadAllBorrowedItems()

            Catch ex As Exception
                MsgBox("Error approving return: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Sub

    Private Sub GenerateReturnReceipt(ByVal borrowID As String, ByVal bookIDs() As String,
                                ByVal copiesToReturn() As Integer, ByVal conditionTypes() As String,
                                ByVal penaltyAmounts() As Decimal, ByVal totalPenalty As Decimal,
                                ByVal returnDate As DateTime)
        Try
            Dim dt As New DataTable("ReturnReceipt")
            dt.Columns.Add("Return Date", GetType(String))
            dt.Columns.Add("Borrow ID", GetType(String))
            dt.Columns.Add("Book ID", GetType(String))
            dt.Columns.Add("Book Title", GetType(String))
            dt.Columns.Add("Quantity Returned", GetType(Integer))
            dt.Columns.Add("Condition", GetType(String))
            dt.Columns.Add("Penalty Amount", GetType(Decimal))
            dt.Columns.Add("Borrower Name", GetType(String))
            dt.Columns.Add("Processed By", GetType(String))

            Dim borrowerName As String = ""
            cmd = New OleDbCommand("SELECT [Borrower Name] FROM transactions WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim nameResult As Object = cmd.ExecuteScalar()
            If nameResult IsNot Nothing AndAlso Not IsDBNull(nameResult) Then
                borrowerName = nameResult.ToString()
            End If

            For i As Integer = 0 To bookIDs.Length - 1
                If copiesToReturn(i) > 0 Then
                    Dim bookID As String = bookIDs(i)
                    Dim bookTitle As String = ""

                    ' Get book title
                    cmd = New OleDbCommand("SELECT [Title] FROM books WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", bookID)
                    Dim titleResult As Object = cmd.ExecuteScalar()
                    If titleResult IsNot Nothing AndAlso Not IsDBNull(titleResult) Then
                        bookTitle = titleResult.ToString()
                    End If

                    dt.Rows.Add(
                    returnDate.ToString("MM/dd/yyyy"),
                    borrowID,
                    bookID,
                    bookTitle,
                    copiesToReturn(i),
                    conditionTypes(i),
                    penaltyAmounts(i),
                    borrowerName,
                    XName
                )
                End If
            Next

            If totalPenalty > 0 Then
                dt.Rows.Add(
                "", "", "", "TOTAL PENALTY", 0, "", totalPenalty, "", ""
            )
            End If

            Dim reportForm As New ReportForm()
            Dim report As New ReportDocument()
            Dim reportPath As String = Path.Combine(Application.StartupPath, "Reports\CRReturnBook.rpt")

            If Not File.Exists(reportPath) Then
                MsgBox("Return receipt report not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                Return
            End If

            report.Load(reportPath)
            report.SetDataSource(dt)
            reportForm.CrystalReportViewer1.ReportSource = report
            reportForm.ShowDialog()

            report.Close()
            report.Dispose()

        Catch ex As Exception
            MsgBox("Error generating return receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GetIndividualCurrentReturned(ByVal borrowID As String, ByVal bookID As String) As Integer
        Try
            cmd = New OleDbCommand("SELECT [Current Returned] FROM borrowings WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            cmd.Parameters.AddWithValue("?", bookID)
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) AndAlso Integer.TryParse(result.ToString(), Nothing) Then
                Return Convert.ToInt32(result)
            End If
            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetIndividualTotalCopies(ByVal borrowID As String, ByVal bookID As String) As Integer
        Try
            cmd = New OleDbCommand("SELECT [Copies] FROM borrowings WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            cmd.Parameters.AddWithValue("?", bookID)
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) AndAlso Integer.TryParse(result.ToString(), Nothing) Then
                Return Convert.ToInt32(result)
            End If
            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub CreateReturnLog(ByVal borrowID As String, ByVal bookID As String, ByVal returnedQuantity As Integer)
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim returnID As String = GenerateNextReturnID()
            Dim returnDate As DateTime = DateTime.Now

            Dim borrowerName As String = ""
            Dim getNameCmd As New OleDbCommand("SELECT [Borrower Name] FROM transactions WHERE [Borrow ID] = ?", con)
            getNameCmd.Parameters.AddWithValue("?", borrowID)

            Dim result As Object = getNameCmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                borrowerName = result.ToString()
            Else
                borrowerName = "Unknown"
            End If
            getNameCmd.Dispose()

            cmd = New OleDbCommand("
            INSERT INTO returnLog 
            ([ReturnID], [Borrow ID], [Book ID], [Returned Quantity], [Return Date], [Processed By], [Borrower Name]) 
            VALUES (?, ?, ?, ?, ?, ?, ?)", con)

            cmd.Parameters.AddWithValue("?", returnID)
            cmd.Parameters.AddWithValue("?", borrowID)
            cmd.Parameters.AddWithValue("?", bookID)

            Dim param As New OleDbParameter("?", OleDbType.Integer)
            param.Value = returnedQuantity
            cmd.Parameters.Add(param)

            Dim dateParam As New OleDbParameter("?", OleDbType.Date)
            dateParam.Value = returnDate
            cmd.Parameters.Add(dateParam)

            cmd.Parameters.AddWithValue("?", XName)
            cmd.Parameters.AddWithValue("?", borrowerName)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                Console.WriteLine("Successfully inserted into returnLog.")
            Else
                MsgBox("Failed to insert into returnLog", MsgBoxStyle.Exclamation, "Insert Failed")
            End If

        Catch ex As Exception
            MsgBox($"Error creating return log: {ex.Message}", MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    Private Sub InsertPenaltyRecord(ByVal borrowID As String, ByVal bookIDs() As String,
                                  ByVal copiesToReturn() As Integer, ByVal conditionTypes() As String,
                                  ByVal penaltyAmounts() As Decimal, ByVal totalPenalty As Decimal,
                                  ByVal returnDate As DateTime)
        Try
            Dim borrowerName As String = ""
            Dim dueDate As DateTime = DateTime.Now

            Using cmdName As New OleDbCommand("SELECT [Borrower Name], [Due Date] FROM transactions WHERE [Borrow ID] = ?", con)
                cmdName.Parameters.AddWithValue("?", borrowID)
                Using reader As OleDbDataReader = cmdName.ExecuteReader()
                    If reader.Read() Then
                        If Not IsDBNull(reader("Borrower Name")) Then borrowerName = reader("Borrower Name").ToString()
                        If Not IsDBNull(reader("Due Date")) Then dueDate = Convert.ToDateTime(reader("Due Date"))
                    End If
                End Using
            End Using

            Dim daysLate As Integer = Math.Max(0, (returnDate - dueDate).Days)

            For i As Integer = 0 To bookIDs.Length - 1
                If copiesToReturn(i) > 0 Then
                    Dim penaltyID As String = GenerateNextPenaltyID()
                    Dim penaltyAmount As Decimal = 0

                    If conditionTypes(i) = "Normal" Then
                        penaltyAmount = daysLate * 10 * copiesToReturn(i)
                    Else
                        penaltyAmount = penaltyAmounts(i) * copiesToReturn(i)
                    End If

                    Using insertCmd As New OleDbCommand("
                    INSERT INTO Penalties 
                    ([PenaltyID], [Borrow ID], [Book ID], [Quantity], [Book Condition], [Condition Penalty], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [User Name]) 
                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", con)

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

                        insertCmd.ExecuteNonQuery()
                    End Using
                End If
            Next

        Catch ex As Exception
            MsgBox("Error creating penalty record: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GenerateNextReturnID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "RTN-"

        Try
            cmd = New OleDbCommand("SELECT MAX([ReturnID]) FROM returnLog WHERE [ReturnID] LIKE '" & prefix & "%'", con)
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
        Catch ex As Exception
            Return prefix & "00001"
        End Try
    End Function

    Private Function GenerateNextPenaltyID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "PI-"

        Try
            cmd = New OleDbCommand("SELECT MAX([PenaltyID]) FROM Penalties WHERE [PenaltyID] LIKE '" & prefix & "%'", con)
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
        Catch ex As Exception
            Return prefix & "00001"
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
End Class