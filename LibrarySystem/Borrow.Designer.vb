<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Borrow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Borrow))
        Me.bookdgv = New System.Windows.Forms.DataGridView()
        Me.pbBookImage = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nudCopies = New System.Windows.Forms.NumericUpDown()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAuthor = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtIsbn = New System.Windows.Forms.TextBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBookID = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpBorrowDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuBorrow = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuAddBooks = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuRemoveBook = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCancelBorrowing = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeBorrowerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFirst = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPrevious = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuNext = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLast = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuMainForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.rtxtSelectedBooks = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtPublicationYear = New System.Windows.Forms.TextBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        CType(Me.bookdgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBookImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCopies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'bookdgv
        '
        Me.bookdgv.AllowUserToAddRows = False
        Me.bookdgv.AllowUserToDeleteRows = False
        Me.bookdgv.AllowUserToResizeColumns = False
        Me.bookdgv.AllowUserToResizeRows = False
        Me.bookdgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.bookdgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.bookdgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.bookdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.bookdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.bookdgv.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.RoyalBlue
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.bookdgv.DefaultCellStyle = DataGridViewCellStyle4
        Me.bookdgv.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bookdgv.GridColor = System.Drawing.Color.Gainsboro
        Me.bookdgv.Location = New System.Drawing.Point(0, 439)
        Me.bookdgv.MultiSelect = False
        Me.bookdgv.Name = "bookdgv"
        Me.bookdgv.ReadOnly = True
        Me.bookdgv.RowTemplate.Height = 30
        Me.bookdgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.bookdgv.Size = New System.Drawing.Size(1380, 320)
        Me.bookdgv.TabIndex = 10
        '
        'pbBookImage
        '
        Me.pbBookImage.BackColor = System.Drawing.Color.White
        Me.pbBookImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbBookImage.Location = New System.Drawing.Point(24, 103)
        Me.pbBookImage.Name = "pbBookImage"
        Me.pbBookImage.Size = New System.Drawing.Size(255, 281)
        Me.pbBookImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBookImage.TabIndex = 54
        Me.pbBookImage.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(31, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 21)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Copies of Book"
        '
        'nudCopies
        '
        Me.nudCopies.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.nudCopies.Location = New System.Drawing.Point(31, 264)
        Me.nudCopies.Name = "nudCopies"
        Me.nudCopies.Size = New System.Drawing.Size(367, 29)
        Me.nudCopies.TabIndex = 44
        Me.nudCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Items.AddRange(New Object() {"Fiction", "Non-Fiction", "Science", "Mathematics", "History", "Geography", "Biography", "Technology", "Art", "Literature", "Philosophy", "Economics", "Languages", "Social Studies", "Health & Wellness"})
        Me.cmbCategory.Location = New System.Drawing.Point(376, 49)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(203, 29)
        Me.cmbCategory.TabIndex = 42
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"Available", "Borrowed"})
        Me.cmbStatus.Location = New System.Drawing.Point(376, 161)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(203, 29)
        Me.cmbStatus.TabIndex = 43
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(372, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 21)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Category"
        '
        'txtAuthor
        '
        Me.txtAuthor.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthor.Location = New System.Drawing.Point(16, 217)
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.Size = New System.Drawing.Size(354, 29)
        Me.txtAuthor.TabIndex = 34
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(12, 193)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 21)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Author"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(372, 137)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(57, 21)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "Status"
        '
        'txtIsbn
        '
        Me.txtIsbn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIsbn.Location = New System.Drawing.Point(16, 105)
        Me.txtIsbn.Name = "txtIsbn"
        Me.txtIsbn.Size = New System.Drawing.Size(354, 29)
        Me.txtIsbn.TabIndex = 36
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(16, 161)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(354, 29)
        Me.txtTitle.TabIndex = 35
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 21)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Book ID"
        '
        'txtBookID
        '
        Me.txtBookID.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBookID.Location = New System.Drawing.Point(16, 49)
        Me.txtBookID.Name = "txtBookID"
        Me.txtBookID.Size = New System.Drawing.Size(354, 29)
        Me.txtBookID.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(12, 81)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 21)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "ISBN"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(12, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 21)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Title"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(31, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 21)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Due Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(31, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 21)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Borrow Date"
        '
        'dtpBorrowDate
        '
        Me.dtpBorrowDate.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBorrowDate.Location = New System.Drawing.Point(31, 144)
        Me.dtpBorrowDate.Name = "dtpBorrowDate"
        Me.dtpBorrowDate.Size = New System.Drawing.Size(367, 33)
        Me.dtpBorrowDate.TabIndex = 0
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CalendarFont = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDueDate.Checked = False
        Me.dtpDueDate.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDueDate.Location = New System.Drawing.Point(31, 204)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(367, 33)
        Me.dtpDueDate.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.GoBackToolStripMenuItem, Me.FormToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1380, 33)
        Me.MenuStrip1.TabIndex = 51
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuBorrow, Me.menuAddBooks, Me.menuRemoveBook, Me.menuCancelBorrowing, Me.ChangeBorrowerToolStripMenuItem, Me.menuSearch, Me.menuFilter, Me.menuRefresh, Me.menuHistory})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(73, 29)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'menuBorrow
        '
        Me.menuBorrow.Image = CType(resources.GetObject("menuBorrow.Image"), System.Drawing.Image)
        Me.menuBorrow.Name = "menuBorrow"
        Me.menuBorrow.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.menuBorrow.Size = New System.Drawing.Size(349, 30)
        Me.menuBorrow.Text = "Borrow"
        '
        'menuAddBooks
        '
        Me.menuAddBooks.Image = CType(resources.GetObject("menuAddBooks.Image"), System.Drawing.Image)
        Me.menuAddBooks.Name = "menuAddBooks"
        Me.menuAddBooks.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuAddBooks.Size = New System.Drawing.Size(349, 30)
        Me.menuAddBooks.Text = "Add Books"
        '
        'menuRemoveBook
        '
        Me.menuRemoveBook.Name = "menuRemoveBook"
        Me.menuRemoveBook.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.menuRemoveBook.Size = New System.Drawing.Size(349, 30)
        Me.menuRemoveBook.Text = "Remove Book"
        '
        'menuCancelBorrowing
        '
        Me.menuCancelBorrowing.Image = CType(resources.GetObject("menuCancelBorrowing.Image"), System.Drawing.Image)
        Me.menuCancelBorrowing.Name = "menuCancelBorrowing"
        Me.menuCancelBorrowing.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.menuCancelBorrowing.Size = New System.Drawing.Size(349, 30)
        Me.menuCancelBorrowing.Text = "Cancel Borrowing"
        '
        'ChangeBorrowerToolStripMenuItem
        '
        Me.ChangeBorrowerToolStripMenuItem.Name = "ChangeBorrowerToolStripMenuItem"
        Me.ChangeBorrowerToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.ChangeBorrowerToolStripMenuItem.Size = New System.Drawing.Size(349, 30)
        Me.ChangeBorrowerToolStripMenuItem.Text = "Change Borrower"
        '
        'menuSearch
        '
        Me.menuSearch.Image = CType(resources.GetObject("menuSearch.Image"), System.Drawing.Image)
        Me.menuSearch.Name = "menuSearch"
        Me.menuSearch.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.menuSearch.Size = New System.Drawing.Size(349, 30)
        Me.menuSearch.Text = "Search"
        '
        'menuFilter
        '
        Me.menuFilter.Image = CType(resources.GetObject("menuFilter.Image"), System.Drawing.Image)
        Me.menuFilter.Name = "menuFilter"
        Me.menuFilter.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.menuFilter.Size = New System.Drawing.Size(349, 30)
        Me.menuFilter.Text = "Filter"
        '
        'menuRefresh
        '
        Me.menuRefresh.Image = CType(resources.GetObject("menuRefresh.Image"), System.Drawing.Image)
        Me.menuRefresh.Name = "menuRefresh"
        Me.menuRefresh.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.menuRefresh.Size = New System.Drawing.Size(349, 30)
        Me.menuRefresh.Text = "Refresh"
        '
        'menuHistory
        '
        Me.menuHistory.Image = CType(resources.GetObject("menuHistory.Image"), System.Drawing.Image)
        Me.menuHistory.Name = "menuHistory"
        Me.menuHistory.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.menuHistory.Size = New System.Drawing.Size(349, 30)
        Me.menuHistory.Text = "Borrow History"
        '
        'GoBackToolStripMenuItem
        '
        Me.GoBackToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFirst, Me.menuPrevious, Me.menuNext, Me.menuLast})
        Me.GoBackToolStripMenuItem.Name = "GoBackToolStripMenuItem"
        Me.GoBackToolStripMenuItem.Size = New System.Drawing.Size(116, 29)
        Me.GoBackToolStripMenuItem.Text = "Navigation"
        '
        'menuFirst
        '
        Me.menuFirst.Image = CType(resources.GetObject("menuFirst.Image"), System.Drawing.Image)
        Me.menuFirst.Name = "menuFirst"
        Me.menuFirst.Size = New System.Drawing.Size(156, 30)
        Me.menuFirst.Text = "First"
        '
        'menuPrevious
        '
        Me.menuPrevious.Image = CType(resources.GetObject("menuPrevious.Image"), System.Drawing.Image)
        Me.menuPrevious.Name = "menuPrevious"
        Me.menuPrevious.Size = New System.Drawing.Size(156, 30)
        Me.menuPrevious.Text = "Previous"
        '
        'menuNext
        '
        Me.menuNext.Image = CType(resources.GetObject("menuNext.Image"), System.Drawing.Image)
        Me.menuNext.Name = "menuNext"
        Me.menuNext.Size = New System.Drawing.Size(156, 30)
        Me.menuNext.Text = "Next"
        '
        'menuLast
        '
        Me.menuLast.Image = CType(resources.GetObject("menuLast.Image"), System.Drawing.Image)
        Me.menuLast.Name = "menuLast"
        Me.menuLast.Size = New System.Drawing.Size(156, 30)
        Me.menuLast.Text = "Last"
        '
        'FormToolStripMenuItem
        '
        Me.FormToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuMainForm})
        Me.FormToolStripMenuItem.Name = "FormToolStripMenuItem"
        Me.FormToolStripMenuItem.Size = New System.Drawing.Size(67, 29)
        Me.FormToolStripMenuItem.Text = "Form"
        '
        'menuMainForm
        '
        Me.menuMainForm.Name = "menuMainForm"
        Me.menuMainForm.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.menuMainForm.Size = New System.Drawing.Size(247, 30)
        Me.menuMainForm.Text = "Main Form"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Beige
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.nudCopies)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.dtpDueDate)
        Me.Panel4.Controls.Add(Me.dtpBorrowDate)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.txtName)
        Me.Panel4.Location = New System.Drawing.Point(22, 133)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(426, 313)
        Me.Panel4.TabIndex = 50
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(-1, -2)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(426, 37)
        Me.Label13.TabIndex = 57
        Me.Label13.Text = "Borrower Information"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(-1, 35)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(426, 25)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Borrower Name"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(31, 81)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(367, 35)
        Me.txtName.TabIndex = 44
        Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.SeaShell
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.PictureBox1)
        Me.Panel5.Location = New System.Drawing.Point(22, 43)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(426, 79)
        Me.Panel5.TabIndex = 54
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 27.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DimGray
        Me.Label10.Location = New System.Drawing.Point(100, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(301, 50)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "BORROW BOOK"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(-1, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(121, 79)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 53
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Beige
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.rtxtSelectedBooks)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.pbBookImage)
        Me.Panel1.Location = New System.Drawing.Point(468, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(900, 403)
        Me.Panel1.TabIndex = 51
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(427, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(90, 15)
        Me.Label16.TabIndex = 63
        Me.Label16.Text = "Selected Books:"
        '
        'rtxtSelectedBooks
        '
        Me.rtxtSelectedBooks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtSelectedBooks.Location = New System.Drawing.Point(430, 24)
        Me.rtxtSelectedBooks.Name = "rtxtSelectedBooks"
        Me.rtxtSelectedBooks.Size = New System.Drawing.Size(454, 73)
        Me.rtxtSelectedBooks.TabIndex = 62
        Me.rtxtSelectedBooks.Text = ""
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtPublicationYear)
        Me.Panel2.Controls.Add(Me.txtQuantity)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtBookID)
        Me.Panel2.Controls.Add(Me.cmbCategory)
        Me.Panel2.Controls.Add(Me.cmbStatus)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtIsbn)
        Me.Panel2.Controls.Add(Me.txtTitle)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtAuthor)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Location = New System.Drawing.Point(285, 103)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(599, 281)
        Me.Panel2.TabIndex = 61
        '
        'txtPublicationYear
        '
        Me.txtPublicationYear.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPublicationYear.Location = New System.Drawing.Point(376, 217)
        Me.txtPublicationYear.Name = "txtPublicationYear"
        Me.txtPublicationYear.Size = New System.Drawing.Size(203, 29)
        Me.txtPublicationYear.TabIndex = 61
        '
        'txtQuantity
        '
        Me.txtQuantity.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.Location = New System.Drawing.Point(376, 105)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(203, 29)
        Me.txtQuantity.TabIndex = 60
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(372, 193)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(135, 21)
        Me.Label15.TabIndex = 59
        Me.Label15.Text = "Publication Year"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(372, 81)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(77, 21)
        Me.Label12.TabIndex = 56
        Me.Label12.Text = "Quantity"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(16, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(316, 84)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "Book Information"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Borrow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Linen
        Me.ClientSize = New System.Drawing.Size(1380, 759)
        Me.ControlBox = False
        Me.Controls.Add(Me.bookdgv)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Panel4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Borrow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.bookdgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBookImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCopies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bookdgv As System.Windows.Forms.DataGridView
    Friend WithEvents txtIsbn As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtBookID As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuBorrow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSearch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoBackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuMainForm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAuthor As System.Windows.Forms.TextBox
    Friend WithEvents dtpBorrowDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pbBookImage As System.Windows.Forms.PictureBox
    Friend WithEvents menuFirst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPrevious As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLast As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents nudCopies As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtPublicationYear As System.Windows.Forms.TextBox
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents menuAddBooks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rtxtSelectedBooks As System.Windows.Forms.RichTextBox
    Friend WithEvents menuCancelBorrowing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents menuRemoveBook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeBorrowerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
