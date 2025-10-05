Imports System.Data.OleDb

Public Class MultiReturnApproval
    Private userBorrowings As New DataTable()
    Private selectedBooks As New Dictionary(Of String, Integer)()

    Public Property SelectedUserID As String = ""
    Public Property SelectedUserName As String = ""

    Private Sub MultiReturnApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        SetupDataGridView()
        ClearForm()
    End Sub

    Private Sub SetupDataGridView()
        dgvUserBorrowings.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvUserBorrowings.ReadOnly = True
        dgvUserBorrowings.AllowUserToAddRows = False
        dgvUserBorrowings.MultiSelect = True
    End Sub

    Private Sub ClearForm()
        txtSearchUser.Clear()
        lblSelectedUser.Text = "No user selected"
        dgvUserBorrowings.DataSource = Nothing
        lstSelectedBooks.Items.Clear()
        selectedBooks.Clear()
        btnApproveReturns.Enabled = False
        SelectedUserID = ""
        SelectedUserName = ""
    End Sub

    Private Sub btnSearchUser_Click(sender As Object, e As EventArgs) Handles btnSearchUser.Click
        Dim searchTerm As String = txtSearchUser.Text.Trim()
        If String.IsNullOrEmpty(searchTerm) Then
            MsgBox("Please enter a username or user ID to search.", MsgBoxStyle.Exclamation)
            Return
        End If

        SearchUser(searchTerm)
    End Sub

    Private Sub SearchUser(searchTerm As String)
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim sql As String = "SELECT [User ID], [User Name], [Position] FROM tbluser WHERE [User Name] LIKE ? OR [User ID] LIKE ?"
            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")

            Dim da As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MsgBox("No users found with the specified search term.", MsgBoxStyle.Information)
                ClearForm()
                Return
            End If

            If dt.Rows.Count = 1 Then
                ' Auto-select if only one user found
                SelectedUserID = dt.Rows(0)("User ID").ToString()
                SelectedUserName = dt.Rows(0)("User Name").ToString()
                LoadUserBorrowings()
            Else
                ' Show selection dialog if multiple users found
                ShowUserSelectionDialog(dt)
            End If

        Catch ex As Exception
            MsgBox("Search failed: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ShowUserSelectionDialog(usersTable As DataTable)
        Using selectionForm As New Form()
            selectionForm.Text = "Select User"
            selectionForm.Size = New Size(400, 300)
            selectionForm.StartPosition = FormStartPosition.CenterScreen

            Dim dgv As New DataGridView()
            dgv.DataSource = usersTable
            dgv.Dock = DockStyle.Fill
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.ReadOnly = True
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            Dim btnSelect As New Button()
            btnSelect.Text = "Select"
            btnSelect.Dock = DockStyle.Bottom
            btnSelect.Height = 40

            selectionForm.Controls.Add(dgv)
            selectionForm.Controls.Add(btnSelect)

            AddHandler btnSelect.Click, Sub()
                                            If dgv.SelectedRows.Count > 0 Then
                                                SelectedUserID = dgv.SelectedRows(0).Cells("User ID").Value.ToString()
                                                SelectedUserName = dgv.SelectedRows(0).Cells("User Name").Value.ToString()
                                                selectionForm.DialogResult = DialogResult.OK
                                                selectionForm.Close()
                                                LoadUserBorrowings()
                                            Else
                                                MsgBox("Please select a user.", MsgBoxStyle.Exclamation)
                                            End If
                                        End Sub

            If selectionForm.ShowDialog() = DialogResult.OK Then
                ' User selected, borrowings will be loaded
            End If
        End Using
    End Sub

    Private Sub LoadUserBorrowings()
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim sql As String = "SELECT b.[Borrow ID], b.[Book ID], bk.[Title], bk.[ISBN], " &
                               "b.[Copies], b.[Current Returned], " &
                               "(b.[Copies] - b.[Current Returned]) AS [Remaining], " &
                               "b.[Borrow Date], b.[Due Date], b.[Status] " &
                               "FROM borrowings b " &
                               "INNER JOIN books bk ON b.[Book ID] = bk.[Book ID] " &
                               "WHERE b.[User ID] = ? AND b.[Status] IN ('Borrowed', 'Return Requested') " &
                               "AND (b.[Copies] - b.[Current Returned]) > 0"

            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", SelectedUserID)

            Dim da As New OleDbDataAdapter(cmd)
            userBorrowings.Clear()
            da.Fill(userBorrowings)

            dgvUserBorrowings.DataSource = userBorrowings

            If userBorrowings.Rows.Count = 0 Then
                lblSelectedUser.Text = $"User: {SelectedUserName} - No active borrowings found"
                MsgBox("This user has no active borrowings to return.", MsgBoxStyle.Information)
            Else
                lblSelectedUser.Text = $"User: {SelectedUserName} - {userBorrowings.Rows.Count} active borrowing(s)"
            End If

            UpdateButtonStates()

        Catch ex As Exception
            MsgBox("Failed to load user borrowings: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnAddSelected_Click(sender As Object, e As EventArgs) Handles btnAddSelected.Click
        If dgvUserBorrowings.SelectedRows.Count = 0 Then
            MsgBox("Please select at least one book to return.", MsgBoxStyle.Exclamation)
            Return
        End If

        For Each row As DataGridViewRow In dgvUserBorrowings.SelectedRows
            Dim borrowID As String = row.Cells("Borrow ID").Value.ToString()
            Dim bookID As String = row.Cells("Book ID").Value.ToString()
            Dim title As String = row.Cells("Title").Value.ToString()
            Dim remaining As Integer = Convert.ToInt32(row.Cells("Remaining").Value)

            ' Show quantity input dialog for each selected book
            ShowQuantityDialog(borrowID, bookID, title, remaining)
        Next

        UpdateSelectedBooksList()
        UpdateButtonStates()
    End Sub

    Private Sub ShowQuantityDialog(borrowID As String, bookID As String, title As String, maxQuantity As Integer)
        Dim quantityInput As String = InputBox(
            $"Book: {title}" & vbCrLf &
            $"Maximum returnable: {maxQuantity} copies" & vbCrLf & vbCrLf &
            "Enter quantity to return:",
            "Return Quantity",
            maxQuantity.ToString())

        If String.IsNullOrEmpty(quantityInput) Then
            Return ' User cancelled
        End If

        Dim returnQuantity As Integer
        If Integer.TryParse(quantityInput, returnQuantity) AndAlso returnQuantity > 0 AndAlso returnQuantity <= maxQuantity Then
            Dim key As String = $"{borrowID}|{bookID}"
            selectedBooks(key) = returnQuantity
            MsgBox($"Added {returnQuantity} copy/copies of '{title}' to return list.", MsgBoxStyle.Information)
        Else
            MsgBox($"Please enter a valid quantity between 1 and {maxQuantity}.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub UpdateSelectedBooksList()
        lstSelectedBooks.Items.Clear()

        For Each kvp As KeyValuePair(Of String, Integer) In selectedBooks
            Dim parts() As String = kvp.Key.Split("|"c)
            Dim borrowID As String = parts(0)
            Dim bookID As String = parts(1)
            Dim title As String = GetBookTitle(bookID)

            lstSelectedBooks.Items.Add($"{title} (Borrow ID: {borrowID}) - {kvp.Value} copies")
        Next
    End Sub

    Private Function GetBookTitle(bookID As String) As String
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT [Title] FROM books WHERE [Book ID] = ?", con)
            cmd.Parameters.AddWithValue("?", bookID)
            Dim result As Object = cmd.ExecuteScalar()

            Return If(result IsNot Nothing, result.ToString(), "Unknown Title")
        Catch ex As Exception
            Return "Unknown Title"
        End Try
    End Function

    Private Sub btnRemoveSelected_Click(sender As Object, e As EventArgs) Handles btnRemoveSelected.Click
        If lstSelectedBooks.SelectedIndex = -1 Then
            MsgBox("Please select an item to remove.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim selectedIndex As Integer = lstSelectedBooks.SelectedIndex
        Dim keys As List(Of String) = selectedBooks.Keys.ToList()
        selectedBooks.Remove(keys(selectedIndex))

        UpdateSelectedBooksList()
        UpdateButtonStates()
    End Sub

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        If selectedBooks.Count > 0 Then
            Dim result As DialogResult = MsgBox("Clear all selected books from return list?",
                                              MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                                              "Clear All")
            If result = DialogResult.Yes Then
                selectedBooks.Clear()
                UpdateSelectedBooksList()
                UpdateButtonStates()
            End If
        End If
    End Sub

    Private Sub btnApproveReturns_Click(sender As Object, e As EventArgs) Handles btnApproveReturns.Click
        If selectedBooks.Count = 0 Then
            MsgBox("No books selected for return.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim result As DialogResult = MsgBox($"Are you sure you want to process returns for {selectedBooks.Count} book(s)?",
                                          MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                                          "Confirm Returns")

        If result = DialogResult.Yes Then
            ProcessReturns()
        End If
    End Sub

    Private Sub ProcessReturns()
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim transaction As OleDbTransaction = con.BeginTransaction()

            Try
                For Each kvp As KeyValuePair(Of String, Integer) In selectedBooks
                    Dim parts() As String = kvp.Key.Split("|"c)
                    Dim borrowID As String = parts(0)
                    Dim bookID As String = parts(1)
                    Dim returnQuantity As Integer = kvp.Value

                    ' Update borrowings table
                    UpdateBorrowingRecord(borrowID, returnQuantity, transaction)

                    ' Update books table
                    UpdateBookQuantity(bookID, returnQuantity, transaction)

                    ' Add to returnLog
                    AddReturnLog(borrowID, bookID, returnQuantity, transaction)
                Next

                transaction.Commit()

                ' Generate receipt
                GenerateReturnReceipt()

                MsgBox($"Successfully processed {selectedBooks.Count} return(s).", MsgBoxStyle.Information)

                ' Close form or reset
                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                transaction.Rollback()
                Throw ex
            End Try

        Catch ex As Exception
            MsgBox("Failed to process returns: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub UpdateBorrowingRecord(borrowID As String, returnQuantity As Integer, transaction As OleDbTransaction)
        ' Get current values
        cmd = New OleDbCommand("SELECT [Copies], [Current Returned] FROM borrowings WHERE [Borrow ID] = ?", con, transaction)
        cmd.Parameters.AddWithValue("?", borrowID)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        reader.Read()
        Dim copies As Integer = Convert.ToInt32(reader("Copies"))
        Dim currentReturned As Integer = Convert.ToInt32(reader("Current Returned"))
        reader.Close()

        Dim newReturned As Integer = currentReturned + returnQuantity
        Dim newStatus As String = If(newReturned >= copies, "Returned", "Borrowed")

        ' Update record
        cmd = New OleDbCommand("UPDATE borrowings SET [Current Returned] = ?, [Status] = ?, [Has Requested Return] = 'No' WHERE [Borrow ID] = ?", con, transaction)
        cmd.Parameters.AddWithValue("?", newReturned)
        cmd.Parameters.AddWithValue("?", newStatus)
        cmd.Parameters.AddWithValue("?", borrowID)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub UpdateBookQuantity(bookID As String, returnQuantity As Integer, transaction As OleDbTransaction)
        ' Update quantity
        cmd = New OleDbCommand("UPDATE books SET [Quantity] = [Quantity] + ? WHERE [Book ID] = ?", con, transaction)
        cmd.Parameters.AddWithValue("?", returnQuantity)
        cmd.Parameters.AddWithValue("?", bookID)
        cmd.ExecuteNonQuery()

        ' Update status if needed
        cmd = New OleDbCommand("UPDATE books SET [Status] = 'Available' WHERE [Book ID] = ? AND [Quantity] > 0", con, transaction)
        cmd.Parameters.AddWithValue("?", bookID)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub AddReturnLog(borrowID As String, bookID As String, returnQuantity As Integer, transaction As OleDbTransaction)
        Dim returnID As String = GenerateReturnID()
        cmd = New OleDbCommand("INSERT INTO returnLog ([ReturnID], [Borrow ID], [Book ID], [Returned Quantity], [Return Date], [Processed By]) VALUES (?, ?, ?, ?, ?, ?)", con, transaction)
        cmd.Parameters.AddWithValue("?", returnID)
        cmd.Parameters.AddWithValue("?", borrowID)
        cmd.Parameters.AddWithValue("?", bookID)
        cmd.Parameters.AddWithValue("?", returnQuantity)
        cmd.Parameters.AddWithValue("?", DateTime.Now.ToString("MM/dd/yyyy"))
        cmd.Parameters.AddWithValue("?", XName)
        cmd.ExecuteNonQuery()
    End Sub

    Private Function GenerateReturnID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "RET-"

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

    Private Sub GenerateReturnReceipt()
        Try
            Dim receiptMessage As String = $"MULTI-RETURN RECEIPT" & vbCrLf &
                                         $"======================" & vbCrLf &
                                         $"User: {SelectedUserName}" & vbCrLf &
                                         $"Date: {DateTime.Now.ToString("MM/dd/yyyy HH:mm")}" & vbCrLf &
                                         $"Processed By: {XName}" & vbCrLf &
                                         $"======================" & vbCrLf

            For Each kvp As KeyValuePair(Of String, Integer) In selectedBooks
                Dim parts() As String = kvp.Key.Split("|"c)
                Dim borrowID As String = parts(0)
                Dim bookID As String = parts(1)
                Dim title As String = GetBookTitle(bookID)

                receiptMessage &= $"• {title}" & vbCrLf &
                               $"  Borrow ID: {borrowID} | Quantity: {kvp.Value}" & vbCrLf & vbCrLf
            Next

            receiptMessage &= $"======================" & vbCrLf &
                           $"Total Books: {selectedBooks.Count}"

            MsgBox(receiptMessage, MsgBoxStyle.Information, "Return Receipt")

        Catch ex As Exception
            MsgBox("Error generating receipt: " & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub UpdateButtonStates()
        btnApproveReturns.Enabled = (selectedBooks.Count > 0)
        btnRemoveSelected.Enabled = (lstSelectedBooks.SelectedIndex <> -1)
        btnClearAll.Enabled = (selectedBooks.Count > 0)
        btnAddSelected.Enabled = (dgvUserBorrowings.SelectedRows.Count > 0 AndAlso userBorrowings.Rows.Count > 0)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lstSelectedBooks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSelectedBooks.SelectedIndexChanged
        UpdateButtonStates()
    End Sub

    Private Sub dgvUserBorrowings_SelectionChanged(sender As Object, e As EventArgs) Handles dgvUserBorrowings.SelectionChanged
        UpdateButtonStates()
    End Sub
End Class