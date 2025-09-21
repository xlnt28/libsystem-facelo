Imports System.Data.OleDb
Imports System.IO

Public Class Borrow
    Private trec As Integer = 0
    Private recpointer As Integer = 0
    Private userReturnDate As DateTime = DateTime.Today.AddDays(7)
    Private availableQuantity As Integer = 0
    Public isOnBorrowMode As Boolean = False
    Private selectedBooks As New Dictionary(Of String, Integer)()

    Private Sub Borrow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            OpenDB()

            CenterToScreen()
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized

            LoadBookData()
            LockFields()
            UpdateButtonStates()

            dtpBorrowDate.Value = DateTime.Today
            If xpriv = "Admin" Then
                dtpDueDate.Value = userReturnDate
                dtpDueDate.Enabled = True
                ChangeBorrowerToolStripMenuItem.Enabled = True
            Else
                dtpDueDate.Value = userReturnDate
                Label6.Visible = True
                ChangeBorrowerToolStripMenuItem.Enabled = False
            End If

            txtName.Text = XName
            nudCopies.Minimum = 1
            nudCopies.Value = 1

            CustomizeDataGridView(bookdgv)
            bookdgv.Columns("ISBN").Width = 140

            UpdateBorrowModeUI()
        Catch ex As Exception
            MsgBox("Failed to initialize form. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub LoadBookData()
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            SQLQueryForBorrowBooks()

            bookdgv.DataSource = Nothing
            bookdgv.DataSource = dbdsBorrowBooks.Tables("books")

            bookdgv.ReadOnly = True
            bookdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            bookdgv.ClearSelection()

            trec = dbdsBorrowBooks.Tables("books").Rows.Count - 1

            If trec >= 0 Then
                recpointer = 0
                bookdgv.Rows(recpointer).Selected = True
                bookdgv.FirstDisplayedScrollingRowIndex = 0
                DisplayCurrentRecord()
            Else
                recpointer = -1
                ClearFields()
                MsgBox("No books found in the database.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Failed to load book data. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub DisplayCurrentRecord()
        Try
            If bookdgv.Rows.Count = 0 OrElse (bookdgv.Rows.Count = 1 AndAlso bookdgv.Rows(0).IsNewRow) Then
                ClearFields()
                recpointer = -1
                Exit Sub
            End If

            If bookdgv.SelectedRows.Count = 0 Then
                If bookdgv.Rows.Count > 0 AndAlso Not bookdgv.Rows(0).IsNewRow Then
                    bookdgv.Rows(0).Selected = True
                Else
                    ClearFields()
                    recpointer = -1
                    Exit Sub
                End If
            End If

            Dim selectedRow As DataGridViewRow = bookdgv.SelectedRows(0)

            If selectedRow.IsNewRow Then
                ClearFields()
                recpointer = -1
                Exit Sub
            End If

            If selectedRow.DataBoundItem IsNot Nothing Then
                Dim rowView As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
                Dim row As DataRow = rowView.Row

                UpdateFieldsFromRow(row)
                recpointer = selectedRow.Index
            Else
                ClearFields()
            End If

            LoadBookImage()
            trec = dbdsBorrowBooks.Tables("books").Rows.Count - 1
            UpdateButtonStates()

        Catch ex As Exception
            MsgBox("Failed to display record. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub UpdateFieldsFromRow(ByVal row As DataRow)
        txtBookID.Text = If(IsDBNull(row("Book ID")), "", row("Book ID").ToString())
        txtIsbn.Text = If(IsDBNull(row("ISBN")), "", row("ISBN").ToString().Trim())
        txtTitle.Text = If(IsDBNull(row("Title")), "", row("Title").ToString())
        txtAuthor.Text = If(IsDBNull(row("Author")), "", row("Author").ToString())
        txtPublicationYear.Text = If(IsDBNull(row("Publication Year")), "", row("Publication Year").ToString())
        cmbCategory.Text = If(IsDBNull(row("Category")), "", row("Category").ToString())
        cmbStatus.Text = If(IsDBNull(row("Status")), "", row("Status").ToString())

        If Not IsDBNull(row("Quantity")) Then
            Dim quantityValue As Integer
            If Integer.TryParse(row("Quantity").ToString(), quantityValue) Then
                txtQuantity.Text = quantityValue.ToString()
                availableQuantity = quantityValue
            Else
                txtQuantity.Text = "0"
                availableQuantity = 0
            End If
        Else
            txtQuantity.Text = "0"
            availableQuantity = 0
        End If

        nudCopies.Value = 1
        nudCopies.Enabled = True
    End Sub

    Private Sub LoadBookImage()
        Try
            pbBookImage.ImageLocation = ""
            pbBookImage.Refresh()

            Dim bookImagesDir As String = Application.StartupPath & "\bookImages"
            Dim defaultImagePath As String = bookImagesDir & "\book_default.jpg"
            Dim bookSpecificDir As String = bookImagesDir & "\books"

            If String.IsNullOrEmpty(txtBookID.Text) Then
                pbBookImage.ImageLocation = defaultImagePath
                Return
            End If

            Dim imgPath As String = bookSpecificDir & "\" & txtBookID.Text.Trim() & ".jpg"

            If IO.File.Exists(imgPath) Then
                pbBookImage.ImageLocation = imgPath
            Else
                pbBookImage.ImageLocation = defaultImagePath
            End If
        Catch ex As Exception
            pbBookImage.ImageLocation = Application.StartupPath & "\bookImages\book_default.jpg"
        End Try
    End Sub

    Private Sub ClearFields()
        txtBookID.Clear()
        txtIsbn.Clear()
        txtTitle.Clear()
        txtAuthor.Clear()
        txtPublicationYear.Clear()
        txtQuantity.Clear()
        cmbCategory.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1
        nudCopies.Value = 1
        dtpBorrowDate.Value = DateTime.Today

        If xpriv = "Admin" Then
            dtpDueDate.Enabled = True
        Else
            dtpDueDate.Value = userReturnDate
            Label6.Visible = True
        End If

        pbBookImage.ImageLocation = Application.StartupPath & "\bookImages\book_default.jpg"
    End Sub

    Private Sub LockFields()
        txtName.ReadOnly = True
        txtBookID.ReadOnly = True
        txtIsbn.ReadOnly = True
        txtTitle.ReadOnly = True
        txtAuthor.ReadOnly = True
        txtPublicationYear.ReadOnly = True
        txtQuantity.ReadOnly = True
        cmbCategory.Enabled = False
        cmbStatus.Enabled = False
        dtpBorrowDate.Enabled = False
        dtpDueDate.Enabled = False
        nudCopies.Enabled = True
    End Sub

    Private Sub UpdateButtonStates()
        Dim hasRecords As Boolean = (bookdgv.Rows.Count > 0 AndAlso Not (bookdgv.Rows.Count = 1 AndAlso bookdgv.Rows(0).IsNewRow))
        Dim hasMultipleRecords As Boolean = (hasRecords AndAlso bookdgv.Rows.Count > 1)
        Dim isAvailable As Boolean = (cmbStatus.Text = "Available" AndAlso availableQuantity > 0)
        Dim copiesToBorrow As Integer = CInt(nudCopies.Value)

        menuBorrow.Enabled = True
        menuAddBooks.Enabled = (hasRecords AndAlso isAvailable AndAlso copiesToBorrow > 0 AndAlso isOnBorrowMode)
        menuRemoveBook.Enabled = isOnBorrowMode
        menuCancelBorrowing.Enabled = isOnBorrowMode

        menuSearch.Enabled = True
        menuFilter.Enabled = True
        menuRefresh.Enabled = True
        menuHistory.Enabled = True
        menuMainForm.Enabled = True

        menuFirst.Enabled = (hasMultipleRecords AndAlso recpointer > 0)
        menuPrevious.Enabled = (hasMultipleRecords AndAlso recpointer > 0)
        menuNext.Enabled = (hasMultipleRecords AndAlso recpointer < trec)
        menuLast.Enabled = (hasMultipleRecords AndAlso recpointer < trec)
    End Sub

    Private Sub UpdateBorrowModeUI()
        If isOnBorrowMode Then
            menuBorrow.Text = "Save Borrowing"
            menuAddBooks.Enabled = True
            menuRemoveBook.Enabled = True
            rtxtSelectedBooks.Visible = True
            Label16.Visible = True

            UpdateSelectedBooksLabel()
        Else
            menuBorrow.Text = "Start Borrowing"
            menuAddBooks.Enabled = False
            menuRemoveBook.Enabled = False
            rtxtSelectedBooks.Visible = False
            Label16.Visible = False
            rtxtSelectedBooks.Text = ""
            selectedBooks.Clear()
        End If
        UpdateButtonStates()
    End Sub

    Private Sub UpdateSelectedBooksLabel()
        If selectedBooks.Count = 0 Then
        Else
            Dim booksList As New List(Of String)
            For Each kvp As KeyValuePair(Of String, Integer) In selectedBooks
                Dim bookTitle As String = GetBookTitleByID(kvp.Key)
                booksList.Add(bookTitle & " (" & kvp.Value & " copies)")
            Next
            rtxtSelectedBooks.Text = String.Join(vbCrLf, booksList)
        End If
    End Sub

    Private Sub bookdgv_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bookdgv.SelectionChanged
        If bookdgv.SelectedRows.Count > 0 Then
            DisplayCurrentRecord()
        End If
    End Sub

    Private Sub menuBorrow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuBorrow.Click
        If Not isOnBorrowMode Then
            isOnBorrowMode = True
            UpdateBorrowModeUI()
        Else
            If selectedBooks.Count = 0 Then
                MsgBox("Please add at least one book to borrow.", MsgBoxStyle.Exclamation)
                Return
            End If

            If String.IsNullOrWhiteSpace(txtName.Text) Then
                MsgBox("Please enter borrower's name.", MsgBoxStyle.Exclamation)
                txtName.Focus()
                Return
            End If

            If xpriv <> "Admin" Then
                If dtpDueDate.Value <= dtpBorrowDate.Value Then
                    MsgBox("Return date must be after borrow date.", MsgBoxStyle.Exclamation)
                    dtpDueDate.Focus()
                    Return
                End If
            Else
                If dtpDueDate.Value < dtpBorrowDate.Value Then
                    MsgBox("Return date cannot be before borrow date.", MsgBoxStyle.Exclamation)
                    dtpDueDate.Focus()
                    Return
                End If
            End If

            Try
                If con.State <> ConnectionState.Open Then
                    OpenDB()
                End If

                Dim borrowID As String = GenerateBorrowID()
                Dim bookIDList As String = String.Join(",", selectedBooks.Keys)
                Dim quantityList As String = String.Join(",", selectedBooks.Values)
                Dim status As String = If(xpriv = "Admin", "Borrowed", "Requested")


                cmd = New OleDbCommand("INSERT INTO borrowings([Borrow ID], [Book ID List], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Borrow Date], [Due Date], [Return Date], [Status], [Has Requested Return]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ? ,?)", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.Parameters.AddWithValue("?", bookIDList)
                cmd.Parameters.AddWithValue("?", txtName.Text)
                cmd.Parameters.AddWithValue("?", xpost)
                cmd.Parameters.AddWithValue("?", xpriv)
                cmd.Parameters.AddWithValue("?", quantityList)
                cmd.Parameters.AddWithValue("?", dtpBorrowDate.Value)
                cmd.Parameters.AddWithValue("?", dtpDueDate.Value)
                cmd.Parameters.AddWithValue("?", DBNull.Value)
                cmd.Parameters.AddWithValue("?", status)
                cmd.Parameters.AddWithValue("?", "No")
                cmd.ExecuteNonQuery()

                If xpriv = "Admin" Then
                    For Each bookID As String In selectedBooks.Keys
                        Dim quantityToBorrow As Integer = selectedBooks(bookID)

                        cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                        cmd.Parameters.AddWithValue("?", bookID)
                        Dim currentQuantity As Integer = CInt(cmd.ExecuteScalar())

                        Dim newQuantity As Integer = currentQuantity - quantityToBorrow
                        cmd = New OleDbCommand("UPDATE books SET [Quantity] = ?, [Status] = IIF(? > 0, 'Available', 'Unavailable') WHERE [Book ID] = ?", con)
                        cmd.Parameters.AddWithValue("?", newQuantity)
                        cmd.Parameters.AddWithValue("?", newQuantity)
                        cmd.Parameters.AddWithValue("?", bookID)
                        cmd.ExecuteNonQuery()
                    Next
                End If

                MsgBox("Borrow request saved successfully!" & vbCrLf & "Borrow ID: " & borrowID & vbCrLf, MsgBoxStyle.Information)

                isOnBorrowMode = False
                UpdateBorrowModeUI()
                LoadBookData()

            Catch ex As Exception
                MsgBox("Failed to save borrow request. " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub menuAddBooks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAddBooks.Click
        If Not isOnBorrowMode Then
            MsgBox("Please start borrow mode first.", MsgBoxStyle.Exclamation)
            Return
        End If

        If bookdgv.Rows.Count = 0 OrElse (bookdgv.Rows.Count = 1 AndAlso bookdgv.Rows(0).IsNewRow) Then
            MsgBox("Please select a book to add.", MsgBoxStyle.Exclamation)
            Return
        End If

        If cmbStatus.Text <> "Available" Or availableQuantity <= 0 Then
            MsgBox("This book is not available for borrowing.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim copiesToBorrow As Integer = CInt(nudCopies.Value)
        If copiesToBorrow <= 0 Then
            MsgBox("Please select at least 1 copy to borrow.", MsgBoxStyle.Exclamation)
            nudCopies.Focus()
            Return
        End If

        If copiesToBorrow > availableQuantity Then
            MsgBox("Cannot borrow " & copiesToBorrow & " copies. Only " & availableQuantity & " copies are available.", MsgBoxStyle.Exclamation)
            nudCopies.Focus()
            Return
        End If

        Dim bookID As String = txtBookID.Text.Trim()

        If selectedBooks.ContainsKey(bookID) Then
            selectedBooks(bookID) = copiesToBorrow
            MsgBox("Updated quantity for book: " & txtTitle.Text & " to " & copiesToBorrow & " copies", MsgBoxStyle.Information)
        Else
            selectedBooks.Add(bookID, copiesToBorrow)
            MsgBox("Book added to borrow list: " & txtTitle.Text & " (" & copiesToBorrow & " copies)", MsgBoxStyle.Information)
        End If

        UpdateSelectedBooksLabel()
    End Sub

    Private Function GenerateBorrowID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "BR-"

        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT MAX([Borrow ID]) FROM borrowings WHERE [Borrow ID] LIKE '" & prefix & "%'", con)
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

    Private Sub menuSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSearch.Click
        Dim searchTerm As String = InputBox("Enter Book Title, Book ID or ISBN to search:", "Search Book")
        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            SearchBook(searchTerm)
        ElseIf searchTerm IsNot Nothing Then
            MsgBox("Search cancelled.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SearchBook(ByVal searchTerm As String)
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT [Book ID], [ISBN], [Title], [Author], [Publisher], " & _
                                  "[Publication Year], [Category], [Quantity], " & _
                                  "IIF(Val([Quantity]) > 0, 'Available', 'Unavailable') AS [Status] " & _
                                  "FROM books WHERE ([Title] LIKE ? OR " & _
                                  "[Book ID] LIKE ? OR " & _
                                  "[ISBN] LIKE ? OR " & _
                                  "[Author] LIKE ?)", con)

            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")

            Dim searchResults As New DataSet()
            daBorrowBooks = New OleDbDataAdapter(cmd)
            daBorrowBooks.Fill(searchResults, "books")

            If searchResults.Tables("books").Rows.Count > 0 Then
                dbdsBorrowBooks = searchResults
                bookdgv.DataSource = dbdsBorrowBooks.Tables("books")

                trec = dbdsBorrowBooks.Tables("books").Rows.Count - 1
                recpointer = 0
                bookdgv.ClearSelection()
                If bookdgv.Rows.Count > 0 Then
                    bookdgv.Rows(0).Selected = True
                End If
                DisplayCurrentRecord()

                MsgBox(dbdsBorrowBooks.Tables("books").Rows.Count.ToString() & " book(s) found.", MsgBoxStyle.Information)
            Else
                MsgBox("No matching books found.", MsgBoxStyle.Information)
                LoadBookData()
            End If
        Catch ex As Exception
            MsgBox("Search failed. " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            UpdateButtonStates()
        End Try
    End Sub

    Private Sub menuFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuFilter.Click
        Dim category As String = InputBox("Enter category to filter by (leave empty to show all books):", "Filter by Category")

        If category Is Nothing Then
            Return
        End If

        category = category.Trim()

        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim sql As String = "SELECT [Book ID], [ISBN], [Title], [Author], [Publisher], " & _
                              "[Publication Year], [Category], [Quantity], " & _
                              "IIF(Val([Quantity]) > 0, 'Available', 'Unavailable') AS [Status] " & _
                              "FROM books"

            If Not String.IsNullOrEmpty(category) Then
                sql &= " WHERE [Category] = ?"
                cmd = New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("?", category)
            Else
                cmd = New OleDbCommand(sql, con)
            End If

            Dim filterResults As New DataSet()
            daBorrowBooks = New OleDbDataAdapter(cmd)
            daBorrowBooks.Fill(filterResults, "books")

            If filterResults.Tables("books").Rows.Count > 0 Then
                dbdsBorrowBooks = filterResults
                bookdgv.DataSource = dbdsBorrowBooks.Tables("books")

                trec = dbdsBorrowBooks.Tables("books").Rows.Count - 1
                recpointer = 0
                bookdgv.ClearSelection()
                If bookdgv.Rows.Count > 0 AndAlso Not bookdgv.Rows(0).IsNewRow Then
                    bookdgv.Rows(0).Selected = True
                End If
                DisplayCurrentRecord()

                Dim bookCount As Integer = dbdsBorrowBooks.Tables("books").Rows.Count
                Dim message As String = bookCount.ToString() & " book(s) found"
                If Not String.IsNullOrEmpty(category) Then
                    message &= " in category: " & category
                End If
                MsgBox(message, MsgBoxStyle.Information)
            Else
                Dim message As String = "No books found"
                If Not String.IsNullOrEmpty(category) Then
                    message &= " in category: " & category
                End If
                MsgBox(message, MsgBoxStyle.Information)
                LoadBookData()
            End If
        Catch ex As Exception
            MsgBox("Filter failed. " & ex.Message, MsgBoxStyle.Critical, "Error")
            LoadBookData()
        Finally
            UpdateButtonStates()
        End Try
    End Sub

    Private Sub menuRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRefresh.Click
        LoadBookData()
        ClearFields()

        If bookdgv.Rows.Count > 0 AndAlso Not bookdgv.Rows(0).IsNewRow Then
            bookdgv.ClearSelection()
            bookdgv.Rows(0).Selected = True
            bookdgv.FirstDisplayedScrollingRowIndex = 0
            recpointer = 0
            DisplayCurrentRecord()
        Else
            recpointer = -1
            ClearFields()
        End If

        UpdateButtonStates()
    End Sub

    Private Sub menuHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuHistory.Click
        wasOnBorrowOrReturn = True
        History.Show()
        Me.Visible = False
    End Sub

    Private Sub menuFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuFirst.Click
        If trec > 0 Then NavigateRecord(0)
    End Sub

    Private Sub menuPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuPrevious.Click
        If recpointer > 0 Then NavigateRecord(recpointer - 1)
    End Sub

    Private Sub menuNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuNext.Click
        If recpointer < trec Then NavigateRecord(recpointer + 1)
    End Sub

    Private Sub menuLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuLast.Click
        If trec > 0 Then NavigateRecord(trec)
    End Sub

    Private Sub NavigateRecord(ByVal index As Integer)
        If index < 0 OrElse index >= bookdgv.Rows.Count Then Return
        Try
            bookdgv.ClearSelection()
            bookdgv.Rows(index).Selected = True
            bookdgv.FirstDisplayedScrollingRowIndex = index

            If bookdgv.SelectedRows.Count > 0 Then
                Dim selectedRow As DataGridViewRow = bookdgv.SelectedRows(0)
                If selectedRow.DataBoundItem IsNot Nothing Then
                    Dim rowView As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
                    Dim row As DataRow = rowView.Row
                    UpdateFieldsFromRow(row)
                    recpointer = index
                End If
            End If

            LoadBookImage()
            UpdateButtonStates()
        Catch ex As Exception
            MsgBox("Navigation error. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub menuMainForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuMainForm.Click
        frmmain.Show()
        Me.Hide()
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        UpdateButtonStates()
    End Sub

    Private Sub nudCopies_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudCopies.ValueChanged
        UpdateButtonStates()
    End Sub

    Private Sub menuCancelBorrowing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCancelBorrowing.Click
        If isOnBorrowMode Then
            Dim result As DialogResult = MsgBox("Are you sure you want to cancel the current borrowing session? All selected books will be removed.", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Cancel Borrowing")

            If result = DialogResult.Yes Then
                isOnBorrowMode = False
                selectedBooks.Clear()
                rtxtSelectedBooks.Text = ""
                rtxtSelectedBooks.Visible = False
                Label16.Visible = False
                menuBorrow.Text = "Start Borrowing"
                UpdateButtonStates()

                If bookdgv.SelectedRows.Count > 0 Then
                    DisplayCurrentRecord()
                End If

                MsgBox("Borrowing session cancelled.", MsgBoxStyle.Information, "Cancelled")
            End If
        Else
            MsgBox("No active borrowing session to cancel.", MsgBoxStyle.Information, "No Session")
        End If
    End Sub

    Private Sub menuRemoveBook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRemoveBook.Click
        If Not isOnBorrowMode Then
            MsgBox("Please start borrow mode first.", MsgBoxStyle.Exclamation)
            Return
        End If

        If selectedBooks.Count = 0 Then
            MsgBox("No books have been added to the borrow list yet.", MsgBoxStyle.Information)
            Return
        End If

        Dim availableBooks As String = "Books in borrow list:" & vbCrLf
        For Each kvp As KeyValuePair(Of String, Integer) In selectedBooks
            Dim bookTitle As String = GetBookTitleByID(kvp.Key)
            availableBooks &= "- ID: " & kvp.Key & " | Title: " & bookTitle & " (" & kvp.Value & " copies)" & vbCrLf
        Next

        Dim bookIDToRemove As String = InputBox(availableBooks & vbCrLf & "Enter the exact Book ID to remove:", "Remove Book from Borrow List")

        If String.IsNullOrWhiteSpace(bookIDToRemove) Then
            Return
        End If

        bookIDToRemove = bookIDToRemove.Trim()

        If selectedBooks.ContainsKey(bookIDToRemove) Then
            Dim bookTitle As String = GetBookTitleByID(bookIDToRemove)
            Dim result As DialogResult = MsgBox("Are you sure you want to remove '" & bookTitle & "' (" & selectedBooks(bookIDToRemove) & " copies) from the borrow list?",
                                               MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                                               "Confirm Removal")

            If result = DialogResult.Yes Then
                selectedBooks.Remove(bookIDToRemove)
                MsgBox("Book removed from borrow list.", MsgBoxStyle.Information)
                UpdateSelectedBooksLabel()

                UpdateButtonStates()
            End If
        Else
            Dim foundKey As String = Nothing
            For Each key As String In selectedBooks.Keys
                If String.Equals(key, bookIDToRemove, StringComparison.OrdinalIgnoreCase) Then
                    foundKey = key
                    Exit For
                End If
            Next

            If foundKey IsNot Nothing Then
                Dim bookTitle As String = GetBookTitleByID(foundKey)
                Dim result As DialogResult = MsgBox("Are you sure you want to remove '" & bookTitle & "' (" & selectedBooks(foundKey) & " copies) from the borrow list?",
                                                   MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                                                   "Confirm Removal")

                If result = DialogResult.Yes Then
                    selectedBooks.Remove(foundKey)
                    MsgBox("Book removed from borrow list.", MsgBoxStyle.Information)
                    UpdateSelectedBooksLabel()
                    UpdateButtonStates()
                End If
            Else
                MsgBox("Book ID '" & bookIDToRemove & "' not found in the borrow list.", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub ChangeBorrowerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeBorrowerToolStripMenuItem.Click
        Dim changeBorrowerForm As New ChangeBorrower()
        changeBorrowerForm.ShowDialog()
    End Sub
End Class