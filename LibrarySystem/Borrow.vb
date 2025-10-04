Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class Borrow
    Private trec As Integer = 0
    Private recpointer As Integer = 0
    Public Shared userReturnDate As DateTime = DateTime.Today.AddDays(7)
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
                dtpBorrowDate.Visible = True
                dtpDueDate.Visible = True
                txtPendingApprovalText.Visible = False
                txtPendingApprovalText2.Visible = False
            Else
                dtpDueDate.Value = userReturnDate
                Label6.Visible = True
                ChangeBorrowerToolStripMenuItem.Enabled = False
                dtpBorrowDate.Visible = True
                dtpDueDate.Visible = False
                txtPendingApprovalText.Visible = True
                txtPendingApprovalText2.Visible = True
                txtPendingApprovalText.Text = "For Approval "
                txtPendingApprovalText2.Text = "For Approval"
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
            dtpDueDate.Value = userReturnDate
            dtpDueDate.Enabled = True
            dtpBorrowDate.Enabled = True
            dtpBorrowDate.Visible = True
            dtpDueDate.Visible = True
            txtPendingApprovalText.Visible = False
            txtPendingApprovalText2.Visible = False
            Label6.Visible = True
        Else
            dtpDueDate.Value = userReturnDate
            dtpDueDate.Enabled = False
            dtpBorrowDate.Enabled = False
            dtpBorrowDate.Visible = False
            dtpDueDate.Visible = False
            txtPendingApprovalText.Visible = True
            txtPendingApprovalText2.Visible = True
            Label6.Visible = True
            txtPendingApprovalText.Text = "For Approval"
            txtPendingApprovalText2.Text = "For Approval"
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
            Return
        End If

        If selectedBooks.Count = 0 Then
            MsgBox("Please add at least one book to borrow.", MsgBoxStyle.Exclamation)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MsgBox("Please enter borrower's name.", MsgBoxStyle.Exclamation)
            txtName.Focus()
            Return
        End If

        Dim userID As String = ""
        Dim userPosition As String = ""
        Dim userPrivileges As String = ""

        Try
            If con.State <> ConnectionState.Open Then OpenDB()
            cmd = New OleDbCommand("SELECT [User ID], [Position], [Privileges] FROM tbluser WHERE [User Name] = ?", con)
            cmd.Parameters.AddWithValue("?", txtName.Text)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                userID = reader("User ID").ToString()
                userPosition = reader("Position").ToString()
                userPrivileges = reader("Privileges").ToString()
            Else
                MsgBox("Borrower not found in user table.", MsgBoxStyle.Exclamation)
                reader.Close()
                Return
            End If
            reader.Close()
        Catch ex As Exception
            MsgBox("Error retrieving user data: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Return
        End Try

        Dim hasStopped As Boolean = False
        Try
            For Each bookID As String In selectedBooks.Keys
                Dim quantityToBorrow As Integer = selectedBooks(bookID)
                Dim borrowID As String = GenerateBorrowID()
                Dim status As String = If(userPrivileges = "Admin", "Borrowed", "Requested")

                Dim borrowDate As Object = If(userPrivileges = "Admin", dtpBorrowDate.Value.ToString("MM/dd/yyyy"), DBNull.Value)
                Dim dueDate As Object = If(userPrivileges = "Admin", dtpDueDate.Value.ToString("MM/dd/yyyy"), DBNull.Value)
                Dim requestDate As Object = If(userPrivileges = "User", DateTime.Now.ToString("MM/dd/yyyy"), DBNull.Value)

                cmd = New OleDbCommand("INSERT INTO borrowings([Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Current Returned], [Borrow Date], [Due Date], [Status], [Has Requested Return], [Request Date]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", con)

                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.Parameters.AddWithValue("?", bookID)
                cmd.Parameters.AddWithValue("?", userID)
                cmd.Parameters.AddWithValue("?", txtName.Text)
                cmd.Parameters.AddWithValue("?", userPosition)
                cmd.Parameters.AddWithValue("?", userPrivileges)
                cmd.Parameters.AddWithValue("?", quantityToBorrow)
                cmd.Parameters.AddWithValue("?", 0)
                cmd.Parameters.AddWithValue("?", borrowDate)
                cmd.Parameters.AddWithValue("?", dueDate)
                cmd.Parameters.AddWithValue("?", status)
                cmd.Parameters.AddWithValue("?", "No")
                cmd.Parameters.AddWithValue("?", requestDate)

                cmd.ExecuteNonQuery()

                If userPrivileges = "Admin" Then
                    cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", bookID)
                    Dim currentQuantity As Integer = CInt(cmd.ExecuteScalar())
                    Dim newQuantity As Integer = currentQuantity - quantityToBorrow
                    cmd = New OleDbCommand("UPDATE books SET [Quantity] = ?, [Status] = IIF(? > 0, 'Available', 'Unavailable') WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", newQuantity)
                    cmd.Parameters.AddWithValue("?", newQuantity)
                    cmd.Parameters.AddWithValue("?", bookID)
                    cmd.ExecuteNonQuery()

                    If hasStopped = False Then
                        GenerateBorrowReceipt(selectedBooks, userID, txtName.Text, userPosition, userPrivileges)
                        hasStopped = True
                    End If
                End If
            Next

            isOnBorrowMode = False
            UpdateBorrowModeUI()
            LoadBookData()

            If xpriv = "Admin" Then
                MsgBox("Borrowed successfully.", MsgBoxStyle.Information)
            Else
                MsgBox("Borrow requested successfully.", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox("Failed to save borrow request. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub GenerateBorrowReceipt(ByVal selectedBooks As Dictionary(Of String, Integer), ByVal userID As String, ByVal userName As String, ByVal userPosition As String, ByVal userPrivileges As String)
        Try
            Dim result As DialogResult = MsgBox("Generating receipt, please wait..",
                               MsgBoxStyle.OkOnly + MsgBoxStyle.Information,
                               "Generate Receipts")

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

            Dim status As String = If(userPrivileges = "Admin", "Borrowed", "Requested")

            For Each bookID As String In selectedBooks.Keys
                Dim quantityToBorrow As Integer = selectedBooks(bookID)
                Dim borrowID As String = GenerateBorrowID()
                Dim borrowDate As String = ""
                Dim dueDate As String = ""
                Dim requestDate As String = ""

                If userPrivileges = "Admin" Then
                    borrowDate = dtpBorrowDate.Value.ToString("MM/dd/yyyy")
                    dueDate = dtpDueDate.Value.ToString("MM/dd/yyyy")
                    requestDate = DBNull.Value.ToString()
                Else
                    borrowDate = DBNull.Value.ToString()
                    dueDate = DBNull.Value.ToString()
                    requestDate = DateTime.Now.ToString("MM/dd/yyyy")
                End If

                dt.Rows.Add(
                borrowID,
                bookID,
                userID,
                userName,
                userPosition,
                userPrivileges,
                quantityToBorrow,
                borrowDate,
                dueDate,
                status,
                0,
                "No",
                requestDate
            )
            Next

            Dim report As New ReportDocument()

            Dim reportPath As String = Path.Combine(Application.StartupPath, "Reports\CrystalReport2.rpt")
            report.Load(reportPath)

            report.SetDataSource(dt)

            report.PrintToPrinter(1, False, 0, 0)

            MsgBox("Borrow slips printed successfully!", MsgBoxStyle.Information, "Print Receipts")

            report.Close()
            report.Dispose()

        Catch ex As Exception
            MsgBox("Error generating borrow receipts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
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

            cmd = New OleDbCommand("SELECT [Book ID], [ISBN], [Title], [Author], [Publisher], " &
                                  "[Publication Year], [Category], [Quantity], " &
                                  "IIF(Val([Quantity]) > 0, 'Available', 'Unavailable') AS [Status] " &
                                  "FROM books WHERE ([Title] LIKE ? OR " &
                                  "[Book ID] LIKE ? OR " &
                                  "[ISBN] LIKE ? OR " &
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

            Dim sql As String = "SELECT [Book ID], [ISBN], [Title], [Author], [Publisher], " &
                              "[Publication Year], [Category], [Quantity], " &
                              "IIF(Val([Quantity]) > 0, 'Available', 'Unavailable') AS [Status] " &
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

    Private Sub txtPendingApprovalText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPendingApprovalText.TextChanged

    End Sub

    Private Function GetBookTitleByID(ByVal bookID As String) As String
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT [Title] FROM books WHERE [Book ID] = ?", con)
            cmd.Parameters.AddWithValue("?", bookID)
            Dim result As Object = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return result.ToString()
            Else
                Return "Unknown Title"
            End If
        Catch ex As Exception
            Return "Unknown Title"
        End Try
    End Function

End Class