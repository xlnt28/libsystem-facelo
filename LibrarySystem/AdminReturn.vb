Imports System.Data.OleDb

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
        OpenDB()
    End Sub

    Private Sub LoadAllBorrowedItems()
        Try
            SQLQueryForAllBorrowed()
            If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("borrowings") AndAlso admindbds.Tables("borrowings").Rows.Count > 0 Then
                dgv.DataSource = admindbds.Tables("borrowings")
                dgv.ClearSelection()
            Else
                dgv.DataSource = admindbds.Tables("borrowings")
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
                sql = "SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], " &
                      "[Borrower Privileges], [Copies], [Current Returned], [Request Date], [Borrow Date], " &
                      "[Due Date], [Status], [Has Requested Return] " &
                      "FROM borrowings " &
                      "WHERE ([Status] = 'Borrowed' AND [Has Requested Return] = 'Yes')"
            Else
                sql = "SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], " &
                      "[Borrower Privileges], [Copies], [Current Returned], [Request Date], [Borrow Date], " &
                      "[Due Date], [Status], [Has Requested Return] " &
                      "FROM borrowings " &
                      "WHERE ([Status] = 'Borrowed' OR [Status] = 'Requested')"
            End If

            If Not String.IsNullOrEmpty(searchName) Then
                sql &= " AND [Borrower Name] LIKE ?"
            End If

            sql &= " ORDER BY [Borrow ID] DESC"

            daBorrowHistory = New OleDbDataAdapter(sql, con)

            If admindbds Is Nothing Then
                admindbds = New DataSet()
            Else
                If admindbds.Tables.Contains("borrowings") Then
                    admindbds.Tables("borrowings").Clear()
                End If
            End If

            If Not String.IsNullOrEmpty(searchName) Then
                daBorrowHistory.SelectCommand.Parameters.AddWithValue("?", "%" & searchName.Trim() & "%")
            End If

            daBorrowHistory.Fill(admindbds, "borrowings")

        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
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
        Dim bookID As String = selectedRow.Cells("Book ID").Value.ToString()
        Dim copies As String = selectedRow.Cells("Copies").Value.ToString()
        Dim currentReturned As String = If(selectedRow.Cells("Current Returned").Value IsNot Nothing, selectedRow.Cells("Current Returned").Value.ToString(), "0")

        Dim bookIDs() As String = {bookID.Trim()}

        Dim totalCopiesInt(0) As Integer
        If Not Integer.TryParse(copies, totalCopiesInt(0)) Then
            MsgBox("Invalid total copies value.", MsgBoxStyle.Critical, "Data Error")
            Return
        End If

        Dim currentReturnedInt(0) As Integer
        If Not Integer.TryParse(currentReturned, currentReturnedInt(0)) Then
            currentReturnedInt(0) = 0
        End If

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
        Dim requiredFields() As String = {"Borrow ID", "Book ID", "Copies"}
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
            Dim newCurrentReturned As Integer = currentReturned(0) + copiesToReturn(0)
            Dim allReturned As Boolean = (newCurrentReturned >= totalCopies(0))

            cmd = New OleDbCommand("UPDATE borrowings SET [Current Returned] = ?, [Status] = ?, [Has Requested Return] = ? WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", newCurrentReturned.ToString())
            cmd.Parameters.AddWithValue("?", If(allReturned, "Completed", "Borrowed"))
            cmd.Parameters.AddWithValue("?", "No")
            cmd.Parameters.AddWithValue("?", borrowID)
            cmd.ExecuteNonQuery()

            For i As Integer = 0 To bookIDs.Length - 1
                If copiesToReturn(i) > 0 Then
                    Dim bookID As String = bookIDs(i)

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

            MsgBox("Return approved successfully.", MsgBoxStyle.Information, "Success")
            LoadAllBorrowedItems()

        Catch ex As Exception
            MsgBox("Error approving return: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub CreateReturnLog(ByVal borrowID As String, ByVal bookID As String, ByVal returnedQuantity As Integer)
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim returnID As String = GenerateNextReturnID()
            Dim returnDate As DateTime = DateTime.Now

            Dim borrowerName As String = ""
            Dim getNameCmd As New OleDbCommand("SELECT [Borrower Name] FROM borrowings WHERE [Borrow ID] = ?", con)
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

            Using cmdName As New OleDbCommand("SELECT [Borrower Name], [Due Date] FROM borrowings WHERE [Borrow ID] = ?", con)
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

        If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("borrowings") Then
            dgv.DataSource = admindbds.Tables("borrowings")
            dgv.ClearSelection()
        End If
    End Sub

    Private Sub SearchUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchUserToolStripMenuItem.Click
        Dim inputName As String = InputBox("Enter Borrower Name to search:", "Search Borrower")
        If String.IsNullOrEmpty(inputName) Then Return

        Try
            SQLQueryForAllBorrowed(showOnlyReturnRequests, inputName)

            If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("borrowings") AndAlso admindbds.Tables("borrowings").Rows.Count > 0 Then
                dgv.DataSource = admindbds.Tables("borrowings")
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

    Private Sub ApproveUserReturnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApproveUserReturnsToolStripMenuItem.Click
        Dim userInput As String = InputBox("Enter User Name or User ID:", "User Search")

        If String.IsNullOrEmpty(userInput) Then
            Return
        End If

        Dim userBooks As List(Of PartialReturnForm2.BookRecord) = GetUserBorrowedBooksForMultiReturn(userInput)

        If userBooks Is Nothing OrElse userBooks.Count = 0 Then
            MsgBox("No borrowed books found for user: " & userInput, MsgBoxStyle.Information, "No Results")
            Return
        End If

        Using multiReturnForm As New PartialReturnForm2(userBooks)
            If multiReturnForm.ShowDialog() = DialogResult.OK Then
                ProcessMultipleReturns(multiReturnForm.GetSelectedBooks(), multiReturnForm.GetTotalPenalty())
            End If
        End Using
    End Sub

    Private Function GetUserBorrowedBooksForMultiReturn(ByVal userInput As String) As List(Of PartialReturnForm2.BookRecord)
        Dim userBooks As New List(Of PartialReturnForm2.BookRecord)()

        Try
            Dim sql As String = "SELECT [Borrow ID], [Book ID], [Copies], [Current Returned], [Borrow Date], [Due Date] " &
                          "FROM borrowings " &
                          "WHERE ([Borrower Name] LIKE ? OR [User ID] = ?) " &
                          "AND [Status] IN ('Borrowed', 'Requested') " &
                          "ORDER BY [Due Date]"

            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", "%" & userInput & "%")
            cmd.Parameters.AddWithValue("?", userInput)

            Using reader As OleDbDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Try
                        Dim borrowID As String = reader("Borrow ID").ToString()
                        Dim bookID As String = reader("Book ID").ToString()

                        Dim bookTitle As String = GetBookTitleByID(bookID)

                        Dim bookRecord As New PartialReturnForm2.BookRecord With {
                        .BorrowID = borrowID,
                        .BookID = bookID,
                        .BookTitle = bookTitle,
                        .TotalCopies = Convert.ToInt32(reader("Copies")),
                        .CurrentReturned = If(IsDBNull(reader("Current Returned")), 0, Convert.ToInt32(reader("Current Returned"))),
                        .BorrowDate = Convert.ToDateTime(reader("Borrow Date")),
                        .DueDate = Convert.ToDateTime(reader("Due Date"))
                    }
                        bookRecord.DaysLate = Math.Max(0, (DateTime.Today - bookRecord.DueDate).Days)
                        userBooks.Add(bookRecord)
                    Catch ex As Exception
                        Continue While
                    End Try
                End While
            End Using

        Catch ex As Exception
            MsgBox("Error getting user books: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Return Nothing
        End Try

        Return userBooks
    End Function

    Private Function GetBookTitleByID(ByVal bookID As String) As String
        Try
            Dim sql As String = "SELECT [Title] FROM books WHERE [Book ID] = ?"
            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", bookID)

            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return result.ToString()
            Else
                Return "Unknown Book"
            End If
        Catch ex As Exception
            Return "Unknown Book"
        End Try
    End Function

    Private Sub ProcessMultipleReturns(ByVal selectedBooks As List(Of PartialReturnForm2.SelectedBook), ByVal totalPenalty As Decimal)
        If selectedBooks.Count = 0 Then
            MsgBox("No books selected for return.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Dim returnDate As DateTime = DateTime.Now
        Dim successCount As Integer = 0

        For Each selectedBook In selectedBooks
            If ProcessSingleBookReturn(selectedBook, returnDate) Then
                successCount += 1
            End If
        Next

        If successCount > 0 Then
            MsgBox($"Successfully processed {successCount} book returns. Total Penalty: ₱{totalPenalty:N2}", MsgBoxStyle.Information, "Success")
            LoadAllBorrowedItems()
        Else
            MsgBox("No books were successfully processed.", MsgBoxStyle.Exclamation, "No Processing")
        End If
    End Sub

    Private Function ProcessSingleBookReturn(ByVal selectedBook As PartialReturnForm2.SelectedBook, ByVal returnDate As DateTime) As Boolean
        Try
            cmd = New OleDbCommand("SELECT [Copies], [Current Returned], [Borrower Name], [Due Date] FROM borrowings WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
            cmd.Parameters.AddWithValue("?", selectedBook.BorrowID)
            cmd.Parameters.AddWithValue("?", selectedBook.BookID)

            Using reader As OleDbDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    Dim totalCopies As Integer = Convert.ToInt32(reader("Copies"))
                    Dim currentReturned As Integer = If(IsDBNull(reader("Current Returned")), 0, Convert.ToInt32(reader("Current Returned")))
                    Dim borrowerName As String = reader("Borrower Name").ToString()
                    Dim dueDate As DateTime = Convert.ToDateTime(reader("Due Date"))

                    Dim newCurrentReturned As Integer = currentReturned + selectedBook.ReturnQuantity
                    Dim allReturned As Boolean = (newCurrentReturned >= totalCopies)

                    cmd = New OleDbCommand("UPDATE borrowings SET [Current Returned] = ?, [Status] = ?, [Has Requested Return] = ? WHERE [Borrow ID] = ? AND [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", newCurrentReturned.ToString())
                    cmd.Parameters.AddWithValue("?", If(allReturned, "Completed", "Borrowed"))
                    cmd.Parameters.AddWithValue("?", "No")
                    cmd.Parameters.AddWithValue("?", selectedBook.BorrowID)
                    cmd.Parameters.AddWithValue("?", selectedBook.BookID)
                    cmd.ExecuteNonQuery()

                    CreateReturnLog(selectedBook.BorrowID, selectedBook.BookID, selectedBook.ReturnQuantity)

                    If selectedBook.ConditionType = "Normal" Then
                        cmd = New OleDbCommand("UPDATE books SET [Quantity] = [Quantity] + ? WHERE [Book ID] = ?", con)
                        cmd.Parameters.AddWithValue("?", selectedBook.ReturnQuantity)
                        cmd.Parameters.AddWithValue("?", selectedBook.BookID)
                        cmd.ExecuteNonQuery()
                    ElseIf selectedBook.ConditionType = "Lost" Then
                        cmd = New OleDbCommand("UPDATE books SET [Quantity] = [Quantity] - ? WHERE [Book ID] = ?", con)
                        cmd.Parameters.AddWithValue("?", selectedBook.ReturnQuantity)
                        cmd.Parameters.AddWithValue("?", selectedBook.BookID)
                        cmd.ExecuteNonQuery()
                    End If

                    cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", selectedBook.BookID)
                    Dim currentQtyObj = cmd.ExecuteScalar()
                    Dim currentQty As Integer = If(currentQtyObj IsNot Nothing AndAlso Not IsDBNull(currentQtyObj), Convert.ToInt32(currentQtyObj), 0)

                    cmd = New OleDbCommand("UPDATE books SET [Status] = ? WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", If(currentQty > 0, "Available", "Unavailable"))
                    cmd.Parameters.AddWithValue("?", selectedBook.BookID)
                    cmd.ExecuteNonQuery()

                    If selectedBook.PenaltyAmount > 0 Then
                        InsertPenaltyRecordForMultiReturn(selectedBook, borrowerName, dueDate, returnDate)
                    End If

                    Return True
                End If
            End Using

        Catch ex As Exception
            MsgBox($"Error processing return for book {selectedBook.BookID}: {ex.Message}", MsgBoxStyle.Critical, "Error")
        End Try

        Return False
    End Function

    Private Sub InsertPenaltyRecordForMultiReturn(ByVal selectedBook As PartialReturnForm2.SelectedBook, ByVal borrowerName As String, ByVal dueDate As DateTime, ByVal returnDate As DateTime)
        Try
            Dim penaltyID As String = GenerateNextPenaltyID()
            Dim daysLate As Integer = Math.Max(0, (returnDate - dueDate).Days)



            Using insertCmd As New OleDbCommand("
            INSERT INTO Penalties 
            ([PenaltyID], [Borrow ID], [Book ID], [Quantity], [Book Condition], [Condition Penalty], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [User Name]) 
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", con)

                insertCmd.Parameters.AddWithValue("?", penaltyID)
                insertCmd.Parameters.AddWithValue("?", selectedBook.BorrowID)
                insertCmd.Parameters.AddWithValue("?", selectedBook.BookID)
                insertCmd.Parameters.AddWithValue("?", selectedBook.ReturnQuantity)
                insertCmd.Parameters.AddWithValue("?", selectedBook.ConditionType)
                insertCmd.Parameters.AddWithValue("?", selectedBook.PenaltyAmount)
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Integer) With {.Value = daysLate})
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Decimal) With {.Value = selectedBook.PenaltyAmount})
                insertCmd.Parameters.AddWithValue("?", "Unpaid")
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = dueDate})
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = returnDate})
                insertCmd.Parameters.AddWithValue("?", borrowerName)

                insertCmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MsgBox("Error creating penalty record: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub
End Class

