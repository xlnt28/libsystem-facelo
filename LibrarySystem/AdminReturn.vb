Imports System.Data.OleDb
Imports System.Drawing
Imports System.Linq

Public Class AdminReturn
    Private showOnlyReturnRequests As Boolean = False

    Private Sub AdminReturn_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        CustomizeDataGridView(dgv)
        LoadAllReturnedAndLostItems()
    End Sub

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        OpenDB()
    End Sub

    Private Sub LoadAllReturnedAndLostItems()
        Try
            SQLQueryForAllReturnedAndLost()
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

    Public Sub SQLQueryForAllReturnedAndLost(Optional ByVal onlyReturnRequests As Boolean = False, Optional ByVal searchName As String = "")
        Try
            Dim sql As String

            If onlyReturnRequests Then
                sql = "SELECT [Borrow ID], [Book ID List], [Borrower Name], [Borrower Position], " &
                      "[Borrower Privileges], [Copies], [Current Returned], [Borrow Date], " &
                      "[Due Date], [Return Date], [Status], [Has Requested Return] " &
                      "FROM borrowings " &
                       "WHERE ([Status] = 'Borrowed' AND [Has Requested Return] = 'Yes')"
            Else
                sql = "SELECT [Borrow ID], [Book ID List], [Borrower Name], [Borrower Position], " &
                      "[Borrower Privileges], [Copies], [Current Returned], [Borrow Date], " &
                      "[Due Date], [Return Date], [Status], [Has Requested Return] " &
                      "FROM borrowings " &
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
        Dim bookIDList As String = selectedRow.Cells("Book ID List").Value.ToString()
        Dim copiesList As String = selectedRow.Cells("Copies").Value.ToString()
        Dim currentReturnedList As String = If(selectedRow.Cells("Current Returned").Value IsNot Nothing, selectedRow.Cells("Current Returned").Value.ToString(), "")
        Dim bookIDs() As String = bookIDList.Split(","c)
        For i As Integer = 0 To bookIDs.Length - 1
            bookIDs(i) = bookIDs(i).Trim()
        Next
        Dim totalCopies() As String = copiesList.Split(","c)
        For i As Integer = 0 To totalCopies.Length - 1
            totalCopies(i) = totalCopies(i).Trim()
        Next
        Dim currentReturned() As String
        If String.IsNullOrEmpty(currentReturnedList) Then
            currentReturned = New String(bookIDs.Length - 1) {}
            For i As Integer = 0 To bookIDs.Length - 1
                currentReturned(i) = "0"
            Next
        Else
            currentReturned = currentReturnedList.Split(","c)
            For i As Integer = 0 To currentReturned.Length - 1
                currentReturned(i) = currentReturned(i).Trim()
            Next
        End If
        If bookIDs.Length <> totalCopies.Length OrElse bookIDs.Length <> currentReturned.Length Then
            MsgBox("Data inconsistency detected. Please contact administrator.", MsgBoxStyle.Critical, "Data Error")
            Return
        End If
        Dim totalCopiesInt(bookIDs.Length - 1) As Integer
        Dim currentReturnedInt(bookIDs.Length - 1) As Integer
        For i As Integer = 0 To bookIDs.Length - 1
            If Not Integer.TryParse(totalCopies(i), totalCopiesInt(i)) Then
                MsgBox("Invalid total copies value for Book ID " & bookIDs(i), MsgBoxStyle.Critical, "Data Error")
                Return
            End If
            If i < currentReturned.Length Then
                If Not Integer.TryParse(currentReturned(i), currentReturnedInt(i)) Then
                    currentReturnedInt(i) = 0
                End If
            Else
                currentReturnedInt(i) = 0
            End If
            If currentReturnedInt(i) > totalCopiesInt(i) Then
                MsgBox("Invalid data: Returned copies exceed total copies for Book ID " & bookIDs(i), MsgBoxStyle.Critical, "Data Error")
                Return
            End If
        Next
        Using prf As New PartialReturnForm()
            prf.LoadBorrowInfoFromDGV(dgv, borrowID)
            prf.BookIDs = bookIDs
            prf.TotalCopies = totalCopiesInt
            prf.CurrentReturned = currentReturnedInt
            If prf.ShowDialog() = DialogResult.OK Then
                ProcessReturnQuantities(borrowID, bookIDs, totalCopiesInt, currentReturnedInt, prf.GetReturnQuantities(), prf.GetTotalPenalty())
            End If
        End Using
    End Sub

    Private Function ValidateRowData(ByVal row As DataGridViewRow) As Boolean
        Dim requiredFields() As String = {"Borrow ID", "Book ID List", "Copies"}
        For Each field In requiredFields
            If row.Cells(field).Value Is Nothing OrElse String.IsNullOrEmpty(row.Cells(field).Value.ToString()) Then
                MsgBox("Missing required data in field: " & field, MsgBoxStyle.Critical, "Data Error")
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub ProcessReturnQuantities(ByVal borrowID As String, ByVal bookIDs() As String, ByVal totalCopies() As Integer, ByVal currentReturned() As Integer, ByVal copiesToReturn() As Integer, ByVal totalPenalty As Decimal)
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
        Dim returnDate As String = DateTime.Now.ToString("MM/dd/yyyy")
        Try
            Dim newCurrentReturned(bookIDs.Length - 1) As Integer
            Dim allReturned As Boolean = True
            For i As Integer = 0 To bookIDs.Length - 1
                newCurrentReturned(i) = currentReturned(i) + copiesToReturn(i)
                If newCurrentReturned(i) < totalCopies(i) Then
                    allReturned = False
                End If
            Next
            cmd = New OleDbCommand("UPDATE borrowings SET [Current Returned] = ?, [Status] = ?, [Return Date] = ? WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", String.Join(",", newCurrentReturned))
            cmd.Parameters.AddWithValue("?", If(allReturned, "Completed", "Borrowed"))
            cmd.Parameters.AddWithValue("?", If(allReturned, returnDate, DBNull.Value))
            cmd.Parameters.AddWithValue("?", borrowID)
            cmd.ExecuteNonQuery()
            For i As Integer = 0 To bookIDs.Length - 1
                If copiesToReturn(i) > 0 Then
                    Dim bookID As String = bookIDs(i)
                    cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", bookID)
                    Dim currentQtyObj = cmd.ExecuteScalar()
                    Dim currentQty As Integer = If(currentQtyObj IsNot Nothing AndAlso Not IsDBNull(currentQtyObj), Convert.ToInt32(currentQtyObj), 0)
                    Dim newQty As Integer = currentQty + copiesToReturn(i)
                    cmd = New OleDbCommand("UPDATE books SET [Quantity] = ?, [Status] = ? WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", newQty)
                    cmd.Parameters.AddWithValue("?", If(newQty > 0, "Available", "Unavailable"))
                    cmd.Parameters.AddWithValue("?", bookID)
                    cmd.ExecuteNonQuery()
                End If
            Next
            If totalPenalty > 0 Then
                InsertPenaltyRecord(borrowID, bookIDs, copiesToReturn, totalPenalty)
            End If
            MsgBox("Return approved successfully.", MsgBoxStyle.Information, "Success")
            LoadAllReturnedAndLostItems()
        Catch ex As Exception
            MsgBox("Error approving return: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub InsertPenaltyRecord(ByVal borrowID As String, ByVal bookIDs() As String, ByVal copiesToReturn() As Integer, ByVal totalPenalty As Decimal)
        Try
            ' Get Borrower Name from borrowings table
            Dim borrowerName As String = ""
            Using cmdName As New OleDbCommand("SELECT [Borrower Name] FROM borrowings WHERE [Borrow ID] = ?", con)
                cmdName.Parameters.AddWithValue("?", borrowID)
                Dim result = cmdName.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    borrowerName = result.ToString()
                End If
            End Using

            Dim penaltyID As String = GenerateNextPenaltyID()
            Dim dueDate As DateTime = DateTime.Now
            Dim returnDate As DateTime = DateTime.Now
            Dim daysLate As Integer = 0

            Using selectCmd As New OleDbCommand("SELECT [Due Date], [Return Date] FROM borrowings WHERE [Borrow ID] = ?", con)
                selectCmd.Parameters.AddWithValue("?", borrowID)

                Using reader As OleDbDataReader = selectCmd.ExecuteReader()
                    If reader.Read() Then
                        If Not IsDBNull(reader("Due Date")) Then dueDate = Convert.ToDateTime(reader("Due Date"))
                        If Not IsDBNull(reader("Return Date")) Then returnDate = Convert.ToDateTime(reader("Return Date"))
                    End If
                End Using
            End Using

            daysLate = Math.Max(0, (returnDate - dueDate).Days)

            Dim bookIDListStr As String = String.Join(",", bookIDs)
            Dim totalQuantityStr As String = String.Join(",", copiesToReturn)

            Using insertCmd As New OleDbCommand("INSERT INTO Penalties ([PenaltyID], [Borrow ID], [User Name], [Book ID List], [Total Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", con)
                insertCmd.Parameters.AddWithValue("?", penaltyID)
                insertCmd.Parameters.AddWithValue("?", borrowID)
                insertCmd.Parameters.AddWithValue("?", borrowerName)
                insertCmd.Parameters.AddWithValue("?", bookIDListStr)
                insertCmd.Parameters.AddWithValue("?", totalQuantityStr)
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Integer) With {.Value = daysLate})
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Decimal) With {.Value = totalPenalty})
                insertCmd.Parameters.AddWithValue("?", "Unpaid")
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = dueDate})
                insertCmd.Parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = returnDate})

                insertCmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MsgBox("Error creating penalty record: " & ex.Message & Environment.NewLine & "Stack Trace: " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadAllReturnedAndLostItems()
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Show()
        Me.Close()
    End Sub

    Private Function GenerateNextPenaltyID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "PI-"

        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

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

        SQLQueryForAllReturnedAndLost(showOnlyReturnRequests)

        If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("borrowings") Then
            dgv.DataSource = admindbds.Tables("borrowings")
            dgv.ClearSelection()
        End If
    End Sub

    Private Sub SearchUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchUserToolStripMenuItem.Click
        Dim inputName As String = InputBox("Enter Borrower Name to search:", "Search Borrower")
        If String.IsNullOrEmpty(inputName) Then Return

        Try
            SQLQueryForAllReturnedAndLost(showOnlyReturnRequests, inputName)

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
            LoadAllReturnedAndLostItems()
            MsgBox("Data refreshed successfully.", MsgBoxStyle.Information, "Refreshed")
        Catch ex As Exception
            MsgBox("Error refreshing data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub
End Class