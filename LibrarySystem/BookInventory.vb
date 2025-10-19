Imports System.Data.OleDb
Imports System.Threading
Imports ClosedXML.Excel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class BookInventory
    Private isAdding As Boolean = False
    Private isEditing As Boolean = False
    Private trec As Integer = 0
    Private isEditingOrAdding As Boolean = False
    Private lastSearchTerm As String = String.Empty
    Private currentBookID As String = String.Empty

    Private Sub BookInventory_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            OpenDB()
            CenterToScreen()
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized

            InitializeComboBoxes()
            LoadBookData()
            UpdateButtonStates()
            LockFields()
            loadmsgPanel()
            bookdgv.AllowUserToOrderColumns = True
            CustomizeDataGridView(bookdgv)
            bookdgv.Columns("ISBN").Width = 140

        Catch ex As Exception
            MessageBox.Show("Failed to initialize form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BookInventory_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub InitializeComboBoxes()
        cmbCategory.Items.AddRange(New String() {
         "Fiction",
        "Non-Fiction",
        "Science",
        "Mathematics",
         "History",
         "Geography",
         "Biography",
         "Technology",
         "Art",
         "Literature",
         "Philosophy",
         "Economics",
         "Languages",
        "Social Studies",
         "Health & Wellness"
 })

        cmbStatus.Items.AddRange(New String() {"Available", "Unavailable"})
        cmbStatus.Enabled = False

        nudQuantity.Value = 1
        nudQuantity.Minimum = 0
        nudQuantity.Maximum = 1000
    End Sub

    Public Sub loadmsgPanel()
        If msgPanel.Visible = True Then
            msgPanel.Visible = False
        Else
            msgPanel.Visible = True
        End If
    End Sub

    Private Sub LoadBookData()
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            If dbdsbooks IsNot Nothing AndAlso dbdsbooks.Tables.Contains("books") Then
                dbdsbooks.Tables("books").Clear()
            End If

            SQLQueryForBooks()

            bookdgv.DataSource = Nothing
            bookdgv.DataSource = dbdsbooks.Tables("books")

            bookdgv.ReadOnly = True
            bookdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            bookdgv.ClearSelection()

            trec = dbdsbooks.Tables("books").Rows.Count - 1

            If trec >= 0 Then
                recpointer = 0
                bookdgv.Rows(recpointer).Selected = True
                DisplayCurrentRecord()
            Else
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Failed to load book data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateButtonStates()
        menuSave.Enabled = isEditingOrAdding
        menuCancel.Enabled = isEditingOrAdding
        menuNew.Enabled = Not isEditingOrAdding
        menuEdit.Enabled = (Not isEditingOrAdding AndAlso bookdgv.Rows.Count > 0 And recpointer >= 0)
        menuDelete.Enabled = (Not isEditingOrAdding AndAlso bookdgv.Rows.Count > 0 And recpointer >= 0)
        menuClose.Enabled = Not isEditingOrAdding
        menuPrint.Enabled = (Not isEditingOrAdding AndAlso bookdgv.Rows.Count > 0)
        menuRefresh.Enabled = Not isEditingOrAdding
        menuSearch.Enabled = Not isEditingOrAdding

        Dim hasRecords As Boolean = (bookdgv.Rows.Count > 0)
        Dim hasMultipleRecords As Boolean = (bookdgv.Rows.Count > 1)

        menuFirst.Enabled = (Not isEditingOrAdding And hasMultipleRecords And recpointer > 0)
        menuPrevious.Enabled = (Not isEditingOrAdding And hasMultipleRecords And recpointer > 0)
        menuNext.Enabled = (Not isEditingOrAdding And hasMultipleRecords And recpointer < trec)
        menuLast.Enabled = (Not isEditingOrAdding And hasMultipleRecords And recpointer < trec)

        UpdateFieldColors()
        browseLinkLabel.Visible = isEditingOrAdding

        If isEditingOrAdding Then
            pbBookImage.BorderStyle = BorderStyle.Fixed3D
        Else
            pbBookImage.BorderStyle = BorderStyle.None
        End If
    End Sub

    Private Sub UpdateFieldColors()
        Dim editableColor As Color = If(isEditingOrAdding, Color.White, Color.WhiteSmoke)
        txtBookID.BackColor = Color.WhiteSmoke
        txtIsbn.BackColor = editableColor
        txtTitle.BackColor = editableColor
        txtAuthor.BackColor = editableColor
        txtPublisher.BackColor = editableColor
        txtPublicationYear.BackColor = editableColor
        cmbCategory.BackColor = editableColor
        nudQuantity.BackColor = editableColor
        cmbStatus.BackColor = Color.WhiteSmoke ' Status is read-only
    End Sub

    Private Sub ClearFields()
        txtBookID.Clear()
        txtIsbn.Clear()
        txtTitle.Clear()
        txtAuthor.Clear()
        txtPublisher.Clear()
        txtPublicationYear.Clear()
        cmbCategory.SelectedIndex = -1
        nudQuantity.Value = 1
        cmbStatus.SelectedIndex = -1
        bookdgv.ClearSelection()
        currentBookID = String.Empty
        pbBookImage.ImageLocation = Application.StartupPath & "\bookImages\book_default.jpg"
    End Sub

    Private Sub LockFields()
        txtBookID.ReadOnly = True
        txtIsbn.ReadOnly = True
        txtTitle.ReadOnly = True
        txtAuthor.ReadOnly = True
        txtPublisher.ReadOnly = True
        txtPublicationYear.ReadOnly = True
        cmbCategory.Enabled = False
        nudQuantity.Enabled = False
        cmbStatus.Enabled = False ' Status should always be read-only
    End Sub

    Private Sub UnlockFields()
        txtBookID.ReadOnly = True
        txtIsbn.ReadOnly = False
        txtTitle.ReadOnly = False
        txtAuthor.ReadOnly = False
        txtPublisher.ReadOnly = False
        txtPublicationYear.ReadOnly = False
        cmbCategory.Enabled = True
        nudQuantity.Enabled = True
        cmbStatus.Enabled = False ' Status should always be read-only
    End Sub

    Private Function GenerateBookID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "BK-"

        If dbdsbooks.Tables("books") IsNot Nothing AndAlso dbdsbooks.Tables("books").Rows.Count > 0 Then
            For Each row As DataRow In dbdsbooks.Tables("books").Rows
                Dim idStr As String = row("Book ID").ToString()
                If idStr.StartsWith(prefix) Then
                    Dim suffix As String = idStr.Substring(prefix.Length)
                    Dim currentNumber As Integer
                    If Integer.TryParse(suffix, currentNumber) Then
                        If currentNumber > maxNumber Then
                            maxNumber = currentNumber
                        End If
                    End If
                End If
            Next
        End If

        Return prefix & (maxNumber + 1).ToString("D5")
    End Function

    Private Sub DisplayCurrentRecord()
        Try
            If bookdgv.Rows.Count = 0 OrElse bookdgv.Rows.Count = 1 AndAlso bookdgv.Rows(0).IsNewRow Then
                ClearFields()
                recpointer = -1
                Exit Sub
            End If

            If bookdgv.SelectedRows.Count > 0 Then
                Dim selectedRow = bookdgv.SelectedRows(0)

                If Not selectedRow.IsNewRow Then
                    If selectedRow.DataBoundItem IsNot Nothing Then
                        Dim rowView As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
                        Dim row As DataRow = rowView.Row
                        UpdateFieldsFromRow(row)
                        recpointer = dbdsbooks.Tables("books").Rows.IndexOf(row)
                    End If
                End If
            Else
                If recpointer >= 0 AndAlso recpointer < bookdgv.Rows.Count Then
                    bookdgv.Rows(recpointer).Selected = True
                    Dim selectedRow = bookdgv.Rows(recpointer)

                    If selectedRow.DataBoundItem IsNot Nothing Then
                        Dim rowView As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
                        Dim row As DataRow = rowView.Row
                        UpdateFieldsFromRow(row)
                    End If
                Else
                    If bookdgv.Rows.Count > 0 AndAlso Not bookdgv.Rows(0).IsNewRow Then
                        bookdgv.Rows(0).Selected = True
                        Dim selectedRow = bookdgv.Rows(0)

                        If selectedRow.DataBoundItem IsNot Nothing Then
                            Dim rowView As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
                            Dim row As DataRow = rowView.Row
                            UpdateFieldsFromRow(row)
                            recpointer = dbdsbooks.Tables("books").Rows.IndexOf(row)
                        End If
                    Else
                        ClearFields()
                        recpointer = -1
                    End If
                End If
            End If
            LoadBookImage()
            trec = dbdsbooks.Tables("books").Rows.Count - 1
        Catch ex As Exception
            MessageBox.Show("Failed to display record: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateFieldsFromRow(ByVal row As DataRow)
        currentBookID = If(IsDBNull(row("Book ID")), "", row("Book ID").ToString())
        txtBookID.Text = currentBookID
        txtIsbn.Text = If(IsDBNull(row("ISBN")), "", row("ISBN").ToString().Trim())
        txtTitle.Text = If(IsDBNull(row("Title")), "", row("Title").ToString())
        txtAuthor.Text = If(IsDBNull(row("Author")), "", row("Author").ToString())
        txtPublisher.Text = If(IsDBNull(row("Publisher")), "", row("Publisher").ToString())
        txtPublicationYear.Text = If(IsDBNull(row("Publication Year")), "", row("Publication Year").ToString())
        cmbCategory.Text = If(IsDBNull(row("Category")), "", row("Category").ToString())

        ' Handle Quantity field
        If Not IsDBNull(row("Quantity")) Then
            Dim quantityValue As Integer
            If Integer.TryParse(row("Quantity").ToString(), quantityValue) Then
                nudQuantity.Value = Math.Max(nudQuantity.Minimum, Math.Min(nudQuantity.Maximum, quantityValue))
            Else
                nudQuantity.Value = 1
            End If
        Else
            nudQuantity.Value = 1
        End If

        ' Auto-set status based on quantity (like in Borrow class)
        If nudQuantity.Value > 0 Then
            cmbStatus.Text = "Available"
        Else
            cmbStatus.Text = "Unavailable"
        End If
    End Sub

    Private Sub bookdgv_Sorted(ByVal sender As Object, ByVal e As EventArgs) Handles bookdgv.Sorted
        If bookdgv.SelectedRows.Count > 0 Then
            Dim selectedBookID As String = currentBookID

            For Each row As DataGridViewRow In bookdgv.Rows
                If row.Cells("Book ID").Value.ToString() = selectedBookID Then
                    bookdgv.ClearSelection()
                    row.Selected = True
                    bookdgv.FirstDisplayedScrollingRowIndex = row.Index
                    Exit For
                End If
            Next
        End If

        If bookdgv.SelectedRows.Count > 0 Then
            Dim selectedRow = bookdgv.SelectedRows(0)
            Dim rowView As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
            recpointer = dbdsbooks.Tables("books").Rows.IndexOf(rowView.Row)
        End If

        DisplayCurrentRecord()
        UpdateButtonStates()
    End Sub

    Private Sub NavigateRecord(ByVal index As Integer)
        If index < 0 OrElse index > trec Then Return

        Try
            index = Math.Max(0, Math.Min(index, bookdgv.Rows.Count - 1))
            bookdgv.ClearSelection()
            bookdgv.Rows(index).Selected = True
            bookdgv.FirstDisplayedScrollingRowIndex = index
            DisplayCurrentRecord()
            UpdateButtonStates()
        Catch ex As Exception
            MessageBox.Show("Navigation error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub bookdgv_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles bookdgv.CellClick
        If e.RowIndex < 0 Or e.RowIndex >= bookdgv.Rows.Count Then Return

        bookdgv.ClearSelection()
        bookdgv.Rows(e.RowIndex).Selected = True
        DisplayCurrentRecord()
        UpdateButtonStates()
    End Sub

    Private Sub menuNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuNew.Click
        isAdding = True
        isEditingOrAdding = True
        UnlockFields()
        ClearFields()

        txtBookID.Text = GenerateBookID()
        txtIsbn.Focus()

        pbBookImage.ImageLocation = Application.StartupPath & "\bookImages\book_default.jpg"

        bookdgv.Enabled = False
        UpdateButtonStates()
        loadmsgPanel()

        nudQuantity.Value = 1
        cmbStatus.Text = "Available"

        browseLinkLabel.Visible = True
    End Sub

    Private Sub menuEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuEdit.Click
        If bookdgv.Rows.Count = 0 OrElse bookdgv.SelectedRows.Count = 0 Then
            MsgBox("Please select a book to edit.", MsgBoxStyle.Exclamation)
            Return
        End If

        If recpointer < 0 OrElse recpointer > trec Then
            MsgBox("Please select a valid book to edit.", MsgBoxStyle.Exclamation)
            Return
        End If

        isEditing = True
        isEditingOrAdding = True
        UnlockFields()
        bookdgv.Enabled = False
        txtTitle.Focus()
        UpdateButtonStates()
        loadmsgPanel()

        browseLinkLabel.Visible = True
    End Sub

    Private Sub menuSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSave.Click
        If Not ValidateBookFields() Then Exit Sub

        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
        Try
            If isAdding Then
                SaveNewBook()
            Else
                UpdateExistingBook()
            End If

            isAdding = False
            isEditing = False
            isEditingOrAdding = False
            bookdgv.Enabled = True
            LockFields()
            UpdateButtonStates()
            loadmsgPanel()

            browseLinkLabel.Visible = False
        Catch ex As Exception
            MessageBox.Show("Failed to save book: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateBookFields() As Boolean
        If String.IsNullOrWhiteSpace(txtTitle.Text) Then
            MsgBox("Title is required.", MsgBoxStyle.Exclamation, "Missing Information")
            txtTitle.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtAuthor.Text) Then
            MsgBox("Author is required.", MsgBoxStyle.Exclamation, "Missing Information")
            txtAuthor.Focus()
            Return False
        End If

        If cmbCategory.SelectedIndex = -1 Then
            MsgBox("Please select a category.", MsgBoxStyle.Exclamation, "Missing Information")
            cmbCategory.Focus()
            Return False
        End If

        If Not String.IsNullOrWhiteSpace(txtIsbn.Text) Then
            If Not IsIsbnUnique(txtIsbn.Text, If(isAdding, "", txtBookID.Text)) Then
                MsgBox("This ISBN already exists in the system.", MsgBoxStyle.Exclamation, "Duplicate ISBN")
                txtIsbn.Focus()
                Return False
            End If
        End If

        Dim pubYear As Integer
        If Not Integer.TryParse(txtPublicationYear.Text, pubYear) OrElse pubYear < 1000 OrElse pubYear > DateTime.Now.Year + 1 Then
            MsgBox("Please enter a valid publication year (1000-" & (DateTime.Now.Year + 1).ToString() & ").", MsgBoxStyle.Exclamation, "Invalid Year")
            txtPublicationYear.Focus()
            Return False
        End If

        ' Quantity validation
        If nudQuantity.Value < 0 Then
            MsgBox("Quantity cannot be negative.", MsgBoxStyle.Exclamation, "Invalid Quantity")
            nudQuantity.Focus()
            Return False
        End If

        Dim defaultImagePath As String = Application.StartupPath & "\bookImages\book_default.jpg"
        If pbBookImage.ImageLocation = defaultImagePath OrElse String.IsNullOrEmpty(pbBookImage.ImageLocation) Then
            MsgBox("You must upload a book cover image before saving.", MsgBoxStyle.Exclamation, "Missing Image")
            browseLinkLabel.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub SaveNewBook()
        If con.State <> ConnectionState.Open Then
            OpenDB()
        End If

        Dim status As String = If(nudQuantity.Value > 0, "Available", "Unavailable")

        cmd = New OleDbCommand("INSERT INTO books([Book ID], [ISBN], [Title], [Author], [Publisher], " &
                              "[Publication Year], [Category], [Quantity], [Status]) " &
                              "VALUES (?,?,?,?,?,?,?,?,?)", con)

        With cmd.Parameters
            .AddWithValue("?", txtBookID.Text.Trim())
            .AddWithValue("?", If(String.IsNullOrWhiteSpace(txtIsbn.Text), DBNull.Value, txtIsbn.Text.Trim()))
            .AddWithValue("?", txtTitle.Text.Trim())
            .AddWithValue("?", txtAuthor.Text.Trim())
            .AddWithValue("?", If(String.IsNullOrWhiteSpace(txtPublisher.Text), DBNull.Value, txtPublisher.Text.Trim()))
            .AddWithValue("?", txtPublicationYear.Text.Trim())
            .AddWithValue("?", cmbCategory.Text.Trim())
            .AddWithValue("?", CInt(nudQuantity.Value))
            .AddWithValue("?", status)
        End With

        cmd.ExecuteNonQuery()
        MsgBox("Book saved successfully!", MsgBoxStyle.Information)

        Dim newRow As DataRow = dbdsbooks.Tables("books").NewRow()
        newRow("Book ID") = txtBookID.Text.Trim()
        newRow("ISBN") = If(String.IsNullOrWhiteSpace(txtIsbn.Text), DBNull.Value, txtIsbn.Text.Trim())
        newRow("Title") = txtTitle.Text.Trim()
        newRow("Author") = txtAuthor.Text.Trim()
        newRow("Publisher") = If(String.IsNullOrWhiteSpace(txtPublisher.Text), DBNull.Value, txtPublisher.Text.Trim())
        newRow("Publication Year") = txtPublicationYear.Text.Trim()
        newRow("Category") = cmbCategory.Text.Trim()
        newRow("Quantity") = CInt(nudQuantity.Value)
        newRow("Status") = status ' Auto-set status

        dbdsbooks.Tables("books").Rows.Add(newRow)

        trec = dbdsbooks.Tables("books").Rows.Count - 1
        recpointer = trec

        bookdgv.DataSource = dbdsbooks.Tables("books")
        bookdgv.ClearSelection()
        bookdgv.Rows(recpointer).Selected = True
        DisplayCurrentRecord()
    End Sub

    Private Sub UpdateExistingBook()
        If con.State <> ConnectionState.Open Then
            OpenDB()
        End If

        Dim status As String = If(nudQuantity.Value > 0, "Available", "Unavailable")

        cmd = New OleDbCommand("UPDATE books SET " &
                              "[ISBN]=?, " &
                              "[Title]=?, " &
                              "[Author]=?, " &
                              "[Publisher]=?, " &
                              "[Publication Year]=?, " &
                              "[Category]=?, " &
                              "[Quantity]=?, " &
                              "[Status]=? " &
                              "WHERE [Book ID]=?", con)

        With cmd.Parameters
            .AddWithValue("?", If(String.IsNullOrWhiteSpace(txtIsbn.Text), DBNull.Value, txtIsbn.Text.Trim()))
            .AddWithValue("?", txtTitle.Text.Trim())
            .AddWithValue("?", txtAuthor.Text.Trim())
            .AddWithValue("?", If(String.IsNullOrWhiteSpace(txtPublisher.Text), DBNull.Value, txtPublisher.Text.Trim()))
            .AddWithValue("?", txtPublicationYear.Text.Trim())
            .AddWithValue("?", cmbCategory.Text.Trim())
            .AddWithValue("?", CInt(nudQuantity.Value))
            .AddWithValue("?", status) ' Auto-set status
            .AddWithValue("?", txtBookID.Text.Trim())
        End With

        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

        If rowsAffected = 0 Then
            MsgBox("No changes were saved. The record may have been modified by another user.", MsgBoxStyle.Exclamation)
        Else
            MsgBox("Book updated successfully!", MsgBoxStyle.Information)

            Dim rowToUpdate As DataRow = dbdsbooks.Tables("books").Rows(recpointer)
            rowToUpdate("ISBN") = If(String.IsNullOrWhiteSpace(txtIsbn.Text), DBNull.Value, txtIsbn.Text.Trim())
            rowToUpdate("Title") = txtTitle.Text.Trim()
            rowToUpdate("Author") = txtAuthor.Text.Trim()
            rowToUpdate("Publisher") = If(String.IsNullOrWhiteSpace(txtPublisher.Text), DBNull.Value, txtPublisher.Text.Trim())
            rowToUpdate("Publication Year") = txtPublicationYear.Text.Trim()
            rowToUpdate("Category") = cmbCategory.Text.Trim()
            rowToUpdate("Quantity") = CInt(nudQuantity.Value)
            rowToUpdate("Status") = status ' Auto-set status

            bookdgv.DataSource = dbdsbooks.Tables("books")
            bookdgv.ClearSelection()
            bookdgv.Rows(recpointer).Selected = True
            DisplayCurrentRecord()
        End If
    End Sub

    Private Sub RefreshAfterSave()
        Dim previousBookID As String = txtBookID.Text
        Dim wasSearching As Boolean = Not String.IsNullOrEmpty(lastSearchTerm)

        If wasSearching Then
            SearchBook(lastSearchTerm, True)
        Else
            LoadBookData()
        End If

        If Not String.IsNullOrEmpty(previousBookID) Then
            For i As Integer = 0 To trec
                If dbdsbooks.Tables("books").Rows(i)("Book ID").ToString() = previousBookID Then
                    recpointer = i
                    bookdgv.ClearSelection()
                    bookdgv.Rows(i).Selected = True
                    DisplayCurrentRecord()
                    Exit For
                End If
            Next
        End If



        isAdding = False
        isEditing = False
        isEditingOrAdding = False
        bookdgv.Enabled = True
        UpdateButtonStates()
    End Sub

    Private Sub menuCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuCancel.Click
        If isAdding Then
            ClearFields()
        ElseIf isEditing AndAlso recpointer >= 0 Then
            DisplayCurrentRecord()
        End If

        isAdding = False
        isEditing = False
        isEditingOrAdding = False
        bookdgv.Enabled = True
        LockFields()
        UpdateButtonStates()
        loadmsgPanel()

        browseLinkLabel.Visible = False
    End Sub

    Private Sub menuDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuDelete.Click
        If con.State <> ConnectionState.Open Then
            OpenDB()
        End If

        If recpointer < 0 OrElse recpointer > trec Then
            MsgBox("Please select a book to delete.", MsgBoxStyle.Exclamation)
            Return
        End If

        If bookdgv.Rows.Count = 0 OrElse bookdgv.SelectedRows.Count = 0 Then
            MsgBox("Please select a book to Delete.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim bookTitle As String = dbdsbooks.Tables("books").Rows(recpointer)("Title").ToString()
        Dim bookID As String = dbdsbooks.Tables("books").Rows(recpointer)("Book ID").ToString()

        cmd = New OleDbCommand("SELECT COUNT(*) FROM borrowings WHERE [Book ID] = ? AND [Status] = 'Borrowed'", con)
        cmd.Parameters.AddWithValue("?", bookID)
        Dim borrowedCount As Integer = CInt(cmd.ExecuteScalar())

        If borrowedCount > 0 Then
            MsgBox("Cannot delete this book. It is currently borrowed by " & borrowedCount.ToString() & " user(s).", MsgBoxStyle.Exclamation, "Cannot Delete")
            Return
        End If

        Dim result As DialogResult = MsgBox("Are you sure you want to delete this book?" & vbCrLf & vbCrLf &
                                          "Book ID: " & bookID & vbCrLf &
                                          "Title: " & bookTitle,
                                          MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo,
                                          "Confirm Deletion")

        If result <> DialogResult.Yes Then Return

        Try
            cmd = New OleDbCommand("DELETE FROM books WHERE [Book ID] = ?", con)
            cmd.Parameters.AddWithValue("?", bookID)
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected = 0 Then
                MsgBox("Book not found or already deleted.", MsgBoxStyle.Exclamation)
                Return
            End If

            DeleteBookImage(bookID)

            MsgBox("Book deleted successfully.", MsgBoxStyle.Information)

            LoadBookData()

            If trec >= 0 Then
                If recpointer > trec Then recpointer = trec
                If recpointer >= 0 Then
                    bookdgv.Rows(recpointer).Selected = True
                    DisplayCurrentRecord()
                Else
                    ClearFields()
                End If
            Else
                recpointer = -1
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Failed to delete book: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub menuClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuClose.Click
        If MsgBox("Close the window?", MsgBoxStyle.YesNo, "Closing") = MsgBoxResult.Yes Then
            FadeOutAndClose()
        End If
    End Sub

    Private Sub FadeOutAndClose()
        For opacity As Double = 1.0 To 0 Step -0.1
            Me.Opacity = opacity
            Thread.Sleep(30)
        Next
        Application.Exit()
    End Sub

    Private Sub menuFirst_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuFirst.Click
        If trec > 0 Then NavigateRecord(0)
    End Sub

    Private Sub menuNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuNext.Click
        If recpointer + 1 <= trec Then NavigateRecord(recpointer + 1)
    End Sub

    Private Sub menuPrevious_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuPrevious.Click
        If recpointer - 1 >= 0 Then NavigateRecord(recpointer - 1)
    End Sub

    Private Sub menuLast_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuLast.Click
        If trec > 0 Then NavigateRecord(trec)
    End Sub

    Private Sub menuRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuRefresh.Click
        lastSearchTerm = String.Empty
        LoadBookData()
    End Sub

    Private Sub menuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSearch.Click
        Dim searchTerm As String = InputBox("Enter Book Title, Book ID or ISBN to search:", "Search Book")
        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            SearchBook(searchTerm, False)
        Else
            MsgBox("Search cancelled.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SearchBook(ByVal searchTerm As String, ByVal silent As Boolean)
        lastSearchTerm = searchTerm.Trim()

        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT [Book ID], [ISBN], [Title], [Author], [Publisher], " &
                                  "[Publication Year], [Category], [Quantity], [Status]" &
                                  "FROM books WHERE " &
                                  "[Title] LIKE ? OR " &
                                  "[Book ID] LIKE ? OR " &
                                  "[ISBN] LIKE ?", con)

            cmd.Parameters.AddWithValue("?", "%" & lastSearchTerm & "%")
            cmd.Parameters.AddWithValue("?", "%" & lastSearchTerm & "%")
            cmd.Parameters.AddWithValue("?", "%" & lastSearchTerm & "%")

            Dim searchResults As New DataSet()
            daBooks = New OleDbDataAdapter(cmd)
            daBooks.Fill(searchResults, "books")

            If searchResults.Tables("books").Rows.Count > 0 Then
                dbdsbooks = searchResults
                bookdgv.DataSource = dbdsbooks.Tables("books")

                trec = dbdsbooks.Tables("books").Rows.Count - 1
                recpointer = 0
                bookdgv.ClearSelection()
                bookdgv.Rows(0).Selected = True
                DisplayCurrentRecord()

                If Not silent Then
                    MsgBox(dbdsbooks.Tables("books").Rows.Count.ToString() & " record(s) found.", MsgBoxStyle.Information)
                End If
            Else
                If Not silent Then
                    MsgBox("No matching records found.", MsgBoxStyle.Information)
                End If
                LoadBookData()
            End If
        Catch ex As Exception
            MessageBox.Show("Search failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            UpdateButtonStates()
        End Try
    End Sub

    Private Function IsIsbnUnique(ByVal isbn As String, Optional ByVal excludeBookID As String = "") As Boolean
        If String.IsNullOrWhiteSpace(isbn) Then Return True

        Dim cleanIsbn As String = isbn.Trim().ToUpper()

        For Each row As DataRow In dbdsbooks.Tables("books").Rows
            If row("Book ID").ToString() <> excludeBookID Then
                Dim dbIsbn As String = If(IsDBNull(row("ISBN")), "", row("ISBN").ToString().Trim().ToUpper())
                If dbIsbn = cleanIsbn Then
                    Return False
                End If
            End If
        Next

        Try
            If con.State <> ConnectionState.Open Then OpenDB()

            cmd = New OleDbCommand("SELECT COUNT(*) FROM books WHERE UCase(Trim([ISBN])) = ? AND [Book ID] <> ?", con)
            cmd.Parameters.AddWithValue("?", cleanIsbn)
            cmd.Parameters.AddWithValue("?", If(String.IsNullOrEmpty(excludeBookID), DBNull.Value, excludeBookID))

            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return count = 0
        Catch ex As Exception
            MessageBox.Show("Failed to verify ISBN uniqueness: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function


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

            Dim imgPath As String = bookSpecificDir & "\" & txtBookID.Text.Trim & ".jpg"

            If IO.File.Exists(imgPath) Then
                pbBookImage.ImageLocation = imgPath
            Else
                pbBookImage.ImageLocation = defaultImagePath
            End If
        Catch ex As Exception
            pbBookImage.ImageLocation = Application.StartupPath & "\bookImages\book_default.jpg "
        End Try
    End Sub

    Private Sub browseLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles browseLinkLabel.LinkClicked
        If String.IsNullOrEmpty(txtBookID.Text) Then
            MsgBox("Please generate a Book ID first.", MsgBoxStyle.Information)
            Return
        End If

        ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim bookImagesDir As String = Application.StartupPath & "\bookImages"
                Dim booksDir As String = bookImagesDir & "\books"

                If Not IO.Directory.Exists(bookImagesDir) Then
                    IO.Directory.CreateDirectory(bookImagesDir)
                End If
                If Not IO.Directory.Exists(booksDir) Then
                    IO.Directory.CreateDirectory(booksDir)
                End If

                Dim targetPath As String = booksDir & "\" & txtBookID.Text.Trim & ".jpg"

                Using originalImage As Image = Image.FromFile(ofd.FileName)
                    originalImage.Save(targetPath, Imaging.ImageFormat.Jpeg)
                End Using

                LoadBookImage()
            Catch ex As Exception
                MsgBox("Image upload error: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Sub DeleteBookImage(ByVal bookID As String)
        Try
            Dim BookImagesPath As String = Application.StartupPath & "\bookImages\books"

            Dim imagePath As String = System.IO.Path.Combine(BookImagesPath, bookID & ".jpg")
            If System.IO.File.Exists(imagePath) Then
                System.IO.File.Delete(imagePath)
            End If
        Catch ex As Exception
            Debug.WriteLine("Failed to delete book image: " & ex.Message)
        End Try
    End Sub

    Private Sub MainFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainFormToolStripMenuItem.Click
        frmmain.Show()
        Me.Hide()
    End Sub

    Private Sub nudQuantity_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudQuantity.ValueChanged
        If nudQuantity.Value < 0 Then
            MessageBox.Show("Quantity cannot be negative. Setting to minimum value.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            nudQuantity.Value = 0
        ElseIf nudQuantity.Value > 1000 Then
            MessageBox.Show("Quantity cannot exceed 1000. Setting to maximum value.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            nudQuantity.Value = 1000
        End If

        If isEditingOrAdding Then
            UpdateStatusBasedOnQuantity()
        End If
    End Sub

    Private Sub UpdateStatusBasedOnQuantity()
        If nudQuantity.Value > 0 Then
            cmbStatus.Text = "Available"
        ElseIf nudQuantity.Value = 0 Then
            cmbStatus.Text = "Unavailable"
        End If
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub bookdgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles bookdgv.CellContentClick

    End Sub


    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Try
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx"
            sfd.Title = "Save Excel File"
            sfd.FileName = "BooksExport.xlsx"

            If sfd.ShowDialog() = DialogResult.OK Then
                Dim dt As New DataTable()

                Using cmd As New OleDbCommand("SELECT * FROM books ORDER BY [Book ID]", con)
                    Using da As New OleDbDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using

                Using wb As New XLWorkbook()
                    Dim ws = wb.Worksheets.Add(dt, "Books")

                    ws.Columns().AdjustToContents()

                    With ws.PageSetup
                        .PageOrientation = XLPageOrientation.Landscape
                        .FitToPages(1, 0)
                    End With

                    wb.SaveAs(sfd.FileName)
                End Using

                MsgBox("Excel file saved to: " & sfd.FileName, MsgBoxStyle.Information)

                Dim result As DialogResult = MessageBox.Show("Do you want to print the Excel file?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Process.Start(sfd.FileName)
                End If
            End If

        Catch ex As Exception
            MsgBox("Error exporting: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub CrystalReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrystalReportToolStripMenuItem.Click
        Try
            Dim dbPath As String = Application.StartupPath & "\Database\library.mdb"

            Dim fullDataSet As New DataSet()

            Using cmd As New OleDbCommand("SELECT * FROM books ORDER BY [Book ID]", con)
                Using da As New OleDbDataAdapter(cmd)
                    da.Fill(fullDataSet, "books")
                End Using
            End Using

            Dim rpt As New ReportDocument()
            rpt.Load(Application.StartupPath & "\Reports\BookInventoryReport.rpt")
            rpt.SetDataSource(fullDataSet)

            Dim frm As New ReportForm()
            frm.CrystalReportViewer1.ReportSource = rpt
            frm.CrystalReportViewer1.Refresh()
            frm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BookInventory_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
    End Sub


End Class