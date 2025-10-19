<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BookInventory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BookInventory))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtBookID = New System.Windows.Forms.TextBox()
        Me.txtPublicationYear = New System.Windows.Forms.TextBox()
        Me.txtPublisher = New System.Windows.Forms.TextBox()
        Me.txtAuthor = New System.Windows.Forms.TextBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.txtIsbn = New System.Windows.Forms.TextBox()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrystalReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.NavigationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFirst = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPrevious = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuNext = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLast = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.bookdgv = New System.Windows.Forms.DataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.nudQuantity = New System.Windows.Forms.NumericUpDown()
        Me.pbBookImage = New System.Windows.Forms.PictureBox()
        Me.browseLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.msgPanel = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.menuStrip1.SuspendLayout()
        CType(Me.bookdgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.nudQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBookImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.msgPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(338, 142)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 30)
        Me.Label1.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(11, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 21)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Book ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(334, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 21)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Category"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(24, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 21)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Publication Year"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(24, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 21)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Author"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(24, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 21)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Publisher"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(11, 145)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 21)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Title"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(11, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 21)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "ISBN"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(332, 33)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(57, 21)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "Status"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(332, 89)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(77, 21)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "Quantity"
        '
        'txtBookID
        '
        Me.txtBookID.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtBookID.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBookID.Location = New System.Drawing.Point(15, 57)
        Me.txtBookID.Name = "txtBookID"
        Me.txtBookID.Size = New System.Drawing.Size(311, 29)
        Me.txtBookID.TabIndex = 26
        '
        'txtPublicationYear
        '
        Me.txtPublicationYear.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtPublicationYear.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPublicationYear.Location = New System.Drawing.Point(28, 169)
        Me.txtPublicationYear.Name = "txtPublicationYear"
        Me.txtPublicationYear.Size = New System.Drawing.Size(268, 29)
        Me.txtPublicationYear.TabIndex = 32
        '
        'txtPublisher
        '
        Me.txtPublisher.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtPublisher.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPublisher.Location = New System.Drawing.Point(28, 113)
        Me.txtPublisher.Name = "txtPublisher"
        Me.txtPublisher.Size = New System.Drawing.Size(268, 29)
        Me.txtPublisher.TabIndex = 33
        '
        'txtAuthor
        '
        Me.txtAuthor.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtAuthor.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthor.Location = New System.Drawing.Point(28, 57)
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.Size = New System.Drawing.Size(268, 29)
        Me.txtAuthor.TabIndex = 34
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(15, 169)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(311, 29)
        Me.txtTitle.TabIndex = 35
        '
        'txtIsbn
        '
        Me.txtIsbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtIsbn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIsbn.Location = New System.Drawing.Point(15, 116)
        Me.txtIsbn.Name = "txtIsbn"
        Me.txtIsbn.Size = New System.Drawing.Size(311, 29)
        Me.txtIsbn.TabIndex = 36
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuNew, Me.menuSave, Me.menuCancel, Me.menuEdit, Me.menuDelete, Me.menuRefresh, Me.menuSearch, Me.menuPrint, Me.menuClose})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(64, 36)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'menuNew
        '
        Me.menuNew.Image = CType(resources.GetObject("menuNew.Image"), System.Drawing.Image)
        Me.menuNew.Name = "menuNew"
        Me.menuNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.menuNew.Size = New System.Drawing.Size(298, 36)
        Me.menuNew.Text = "New"
        '
        'menuSave
        '
        Me.menuSave.Image = CType(resources.GetObject("menuSave.Image"), System.Drawing.Image)
        Me.menuSave.Name = "menuSave"
        Me.menuSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.menuSave.Size = New System.Drawing.Size(298, 36)
        Me.menuSave.Text = "Save"
        '
        'menuCancel
        '
        Me.menuCancel.Image = CType(resources.GetObject("menuCancel.Image"), System.Drawing.Image)
        Me.menuCancel.Name = "menuCancel"
        Me.menuCancel.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.menuCancel.Size = New System.Drawing.Size(298, 36)
        Me.menuCancel.Text = "Cancel"
        '
        'menuEdit
        '
        Me.menuEdit.Image = CType(resources.GetObject("menuEdit.Image"), System.Drawing.Image)
        Me.menuEdit.Name = "menuEdit"
        Me.menuEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.menuEdit.Size = New System.Drawing.Size(298, 36)
        Me.menuEdit.Text = "Edit"
        '
        'menuDelete
        '
        Me.menuDelete.Image = CType(resources.GetObject("menuDelete.Image"), System.Drawing.Image)
        Me.menuDelete.Name = "menuDelete"
        Me.menuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.menuDelete.Size = New System.Drawing.Size(298, 36)
        Me.menuDelete.Text = "Delete"
        '
        'menuRefresh
        '
        Me.menuRefresh.Image = CType(resources.GetObject("menuRefresh.Image"), System.Drawing.Image)
        Me.menuRefresh.Name = "menuRefresh"
        Me.menuRefresh.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.menuRefresh.Size = New System.Drawing.Size(298, 36)
        Me.menuRefresh.Text = "Refresh"
        '
        'menuSearch
        '
        Me.menuSearch.Image = CType(resources.GetObject("menuSearch.Image"), System.Drawing.Image)
        Me.menuSearch.Name = "menuSearch"
        Me.menuSearch.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.menuSearch.Size = New System.Drawing.Size(298, 36)
        Me.menuSearch.Text = "Search"
        '
        'menuPrint
        '
        Me.menuPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CrystalReportToolStripMenuItem, Me.ExcelToolStripMenuItem})
        Me.menuPrint.Image = CType(resources.GetObject("menuPrint.Image"), System.Drawing.Image)
        Me.menuPrint.Name = "menuPrint"
        Me.menuPrint.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.menuPrint.Size = New System.Drawing.Size(298, 36)
        Me.menuPrint.Text = "Print"
        '
        'CrystalReportToolStripMenuItem
        '
        Me.CrystalReportToolStripMenuItem.Image = CType(resources.GetObject("CrystalReportToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CrystalReportToolStripMenuItem.Name = "CrystalReportToolStripMenuItem"
        Me.CrystalReportToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.CrystalReportToolStripMenuItem.Size = New System.Drawing.Size(403, 36)
        Me.CrystalReportToolStripMenuItem.Text = "Crystal Report"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Image = CType(resources.GetObject("ExcelToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(403, 36)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'menuClose
        '
        Me.menuClose.Image = CType(resources.GetObject("menuClose.Image"), System.Drawing.Image)
        Me.menuClose.Name = "menuClose"
        Me.menuClose.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.menuClose.Size = New System.Drawing.Size(298, 36)
        Me.menuClose.Text = "Close"
        '
        'NavigationToolStripMenuItem
        '
        Me.NavigationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFirst, Me.menuPrevious, Me.menuNext, Me.menuLast})
        Me.NavigationToolStripMenuItem.Name = "NavigationToolStripMenuItem"
        Me.NavigationToolStripMenuItem.Size = New System.Drawing.Size(146, 36)
        Me.NavigationToolStripMenuItem.Text = "Navigation"
        '
        'menuFirst
        '
        Me.menuFirst.Image = CType(resources.GetObject("menuFirst.Image"), System.Drawing.Image)
        Me.menuFirst.Name = "menuFirst"
        Me.menuFirst.Size = New System.Drawing.Size(186, 36)
        Me.menuFirst.Text = "First"
        '
        'menuPrevious
        '
        Me.menuPrevious.Image = CType(resources.GetObject("menuPrevious.Image"), System.Drawing.Image)
        Me.menuPrevious.Name = "menuPrevious"
        Me.menuPrevious.Size = New System.Drawing.Size(186, 36)
        Me.menuPrevious.Text = "Previous"
        '
        'menuNext
        '
        Me.menuNext.Image = CType(resources.GetObject("menuNext.Image"), System.Drawing.Image)
        Me.menuNext.Name = "menuNext"
        Me.menuNext.Size = New System.Drawing.Size(186, 36)
        Me.menuNext.Text = "Next"
        '
        'menuLast
        '
        Me.menuLast.Image = CType(resources.GetObject("menuLast.Image"), System.Drawing.Image)
        Me.menuLast.Name = "menuLast"
        Me.menuLast.Size = New System.Drawing.Size(186, 36)
        Me.menuLast.Text = "Last"
        '
        'menuStrip1
        '
        Me.menuStrip1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.menuStrip1.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.menuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.NavigationToolStripMenuItem, Me.FormToolStripMenuItem})
        Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.menuStrip1.Name = "menuStrip1"
        Me.menuStrip1.Size = New System.Drawing.Size(1384, 40)
        Me.menuStrip1.TabIndex = 11
        Me.menuStrip1.Text = "MenuStrip1"
        '
        'FormToolStripMenuItem
        '
        Me.FormToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MainFormToolStripMenuItem})
        Me.FormToolStripMenuItem.Name = "FormToolStripMenuItem"
        Me.FormToolStripMenuItem.Size = New System.Drawing.Size(83, 36)
        Me.FormToolStripMenuItem.Text = "Form"
        '
        'MainFormToolStripMenuItem
        '
        Me.MainFormToolStripMenuItem.Image = CType(resources.GetObject("MainFormToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MainFormToolStripMenuItem.Name = "MainFormToolStripMenuItem"
        Me.MainFormToolStripMenuItem.Size = New System.Drawing.Size(212, 36)
        Me.MainFormToolStripMenuItem.Text = "Main Form"
        '
        'cmbCategory
        '
        Me.cmbCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmbCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(336, 169)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(290, 29)
        Me.cmbCategory.TabIndex = 42
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmbStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(336, 57)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(290, 29)
        Me.cmbStatus.TabIndex = 43
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.bookdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.bookdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.bookdgv.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.bookdgv.DefaultCellStyle = DataGridViewCellStyle2
        Me.bookdgv.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bookdgv.GridColor = System.Drawing.Color.Gainsboro
        Me.bookdgv.Location = New System.Drawing.Point(0, 486)
        Me.bookdgv.MultiSelect = False
        Me.bookdgv.Name = "bookdgv"
        Me.bookdgv.ReadOnly = True
        Me.bookdgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.bookdgv.Size = New System.Drawing.Size(1384, 300)
        Me.bookdgv.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 27.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DimGray
        Me.Label9.Location = New System.Drawing.Point(116, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(350, 50)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "BOOK INVENTORY"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SeaShell
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(21, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(480, 79)
        Me.Panel1.TabIndex = 46
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(10, 10)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(120, 64)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 53
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Bisque
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.pbBookImage)
        Me.Panel2.Controls.Add(Me.browseLinkLabel)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Location = New System.Drawing.Point(50, 138)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1241, 304)
        Me.Panel2.TabIndex = 47
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.BurlyWood
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.nudQuantity)
        Me.Panel3.Controls.Add(Me.txtBookID)
        Me.Panel3.Controls.Add(Me.txtIsbn)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.cmbStatus)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.txtTitle)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.cmbCategory)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Location = New System.Drawing.Point(263, 16)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(640, 235)
        Me.Panel3.TabIndex = 50
        '
        'nudQuantity
        '
        Me.nudQuantity.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.nudQuantity.Location = New System.Drawing.Point(336, 113)
        Me.nudQuantity.Name = "nudQuantity"
        Me.nudQuantity.Size = New System.Drawing.Size(290, 29)
        Me.nudQuantity.TabIndex = 45
        '
        'pbBookImage
        '
        Me.pbBookImage.BackColor = System.Drawing.Color.White
        Me.pbBookImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbBookImage.Location = New System.Drawing.Point(-1, -1)
        Me.pbBookImage.Name = "pbBookImage"
        Me.pbBookImage.Size = New System.Drawing.Size(258, 304)
        Me.pbBookImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBookImage.TabIndex = 54
        Me.pbBookImage.TabStop = False
        '
        'browseLinkLabel
        '
        Me.browseLinkLabel.AutoSize = True
        Me.browseLinkLabel.DisabledLinkColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.browseLinkLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.browseLinkLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.browseLinkLabel.LinkColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.browseLinkLabel.Location = New System.Drawing.Point(263, 263)
        Me.browseLinkLabel.Name = "browseLinkLabel"
        Me.browseLinkLabel.Size = New System.Drawing.Size(160, 30)
        Me.browseLinkLabel.TabIndex = 54
        Me.browseLinkLabel.TabStop = True
        Me.browseLinkLabel.Text = "INSERT IMAGE"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.BurlyWood
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.txtAuthor)
        Me.Panel4.Controls.Add(Me.txtPublicationYear)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.txtPublisher)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Location = New System.Drawing.Point(909, 16)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(317, 235)
        Me.Panel4.TabIndex = 49
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Panel5.Location = New System.Drawing.Point(0, 464)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1384, 22)
        Me.Panel5.TabIndex = 50
        '
        'Label10
        '
        Me.Label10.AutoEllipsis = True
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(10, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(252, 37)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = "Please fill all fields."
        '
        'msgPanel
        '
        Me.msgPanel.Controls.Add(Me.Label10)
        Me.msgPanel.Location = New System.Drawing.Point(551, 88)
        Me.msgPanel.Name = "msgPanel"
        Me.msgPanel.Size = New System.Drawing.Size(270, 44)
        Me.msgPanel.TabIndex = 52
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Bisque
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel6.Location = New System.Drawing.Point(1325, 40)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(59, 424)
        Me.Panel6.TabIndex = 53
        '
        'ofd
        '
        Me.ofd.FileName = "OpenFileDialog1"
        '
        'BookInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1384, 786)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.menuStrip1)
        Me.Controls.Add(Me.bookdgv)
        Me.Controls.Add(Me.msgPanel)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "BookInventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.menuStrip1.ResumeLayout(False)
        Me.menuStrip1.PerformLayout()
        CType(Me.bookdgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.nudQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBookImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.msgPanel.ResumeLayout(False)
        Me.msgPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtBookID As System.Windows.Forms.TextBox
    Friend WithEvents txtPublicationYear As System.Windows.Forms.TextBox
    Friend WithEvents txtPublisher As System.Windows.Forms.TextBox
    Friend WithEvents txtAuthor As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtIsbn As System.Windows.Forms.TextBox
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSearch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NavigationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFirst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPrevious As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLast As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents bookdgv As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents msgPanel As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents FormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents browseLinkLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents pbBookImage As System.Windows.Forms.PictureBox
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MainFormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents nudQuantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents CrystalReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
End Class
