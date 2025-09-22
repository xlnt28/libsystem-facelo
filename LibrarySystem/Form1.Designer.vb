<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.NavigationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFirst = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPrevious = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuNext = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLast = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbopost = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chbShowRetypePassword = New System.Windows.Forms.CheckBox()
        Me.cbopv = New System.Windows.Forms.ComboBox()
        Me.chbShowPassword = New System.Windows.Forms.CheckBox()
        Me.idpict = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtrpw = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtpw = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtun = New System.Windows.Forms.TextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.txtid = New System.Windows.Forms.TextBox()
        Me.txtpv = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.idpict, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.NavigationToolStripMenuItem, Me.MainToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1380, 33)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuNew, Me.menuSave, Me.menuCancel, Me.menuEdit, Me.menuDelete, Me.menuRefresh, Me.menuSearch, Me.menuPrint, Me.menuClose})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(53, 29)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'menuNew
        '
        Me.menuNew.Image = CType(resources.GetObject("menuNew.Image"), System.Drawing.Image)
        Me.menuNew.Name = "menuNew"
        Me.menuNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.menuNew.Size = New System.Drawing.Size(245, 30)
        Me.menuNew.Text = "New"
        '
        'menuSave
        '
        Me.menuSave.Image = CType(resources.GetObject("menuSave.Image"), System.Drawing.Image)
        Me.menuSave.Name = "menuSave"
        Me.menuSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.menuSave.Size = New System.Drawing.Size(245, 30)
        Me.menuSave.Text = "Save"
        '
        'menuCancel
        '
        Me.menuCancel.Image = CType(resources.GetObject("menuCancel.Image"), System.Drawing.Image)
        Me.menuCancel.Name = "menuCancel"
        Me.menuCancel.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.menuCancel.Size = New System.Drawing.Size(245, 30)
        Me.menuCancel.Text = "Cancel"
        '
        'menuEdit
        '
        Me.menuEdit.Image = CType(resources.GetObject("menuEdit.Image"), System.Drawing.Image)
        Me.menuEdit.Name = "menuEdit"
        Me.menuEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.menuEdit.Size = New System.Drawing.Size(245, 30)
        Me.menuEdit.Text = "Edit"
        '
        'menuDelete
        '
        Me.menuDelete.Image = CType(resources.GetObject("menuDelete.Image"), System.Drawing.Image)
        Me.menuDelete.Name = "menuDelete"
        Me.menuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.menuDelete.Size = New System.Drawing.Size(245, 30)
        Me.menuDelete.Text = "Delete"
        '
        'menuRefresh
        '
        Me.menuRefresh.Image = CType(resources.GetObject("menuRefresh.Image"), System.Drawing.Image)
        Me.menuRefresh.Name = "menuRefresh"
        Me.menuRefresh.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.menuRefresh.Size = New System.Drawing.Size(245, 30)
        Me.menuRefresh.Text = "Refresh"
        '
        'menuSearch
        '
        Me.menuSearch.Image = CType(resources.GetObject("menuSearch.Image"), System.Drawing.Image)
        Me.menuSearch.Name = "menuSearch"
        Me.menuSearch.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.menuSearch.Size = New System.Drawing.Size(245, 30)
        Me.menuSearch.Text = "Search"
        '
        'menuPrint
        '
        Me.menuPrint.Image = CType(resources.GetObject("menuPrint.Image"), System.Drawing.Image)
        Me.menuPrint.Name = "menuPrint"
        Me.menuPrint.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.menuPrint.Size = New System.Drawing.Size(245, 30)
        Me.menuPrint.Text = "Print"
        '
        'menuClose
        '
        Me.menuClose.Image = CType(resources.GetObject("menuClose.Image"), System.Drawing.Image)
        Me.menuClose.Name = "menuClose"
        Me.menuClose.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.menuClose.Size = New System.Drawing.Size(245, 30)
        Me.menuClose.Text = "Close"
        '
        'NavigationToolStripMenuItem
        '
        Me.NavigationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFirst, Me.menuPrevious, Me.menuNext, Me.menuLast})
        Me.NavigationToolStripMenuItem.Name = "NavigationToolStripMenuItem"
        Me.NavigationToolStripMenuItem.Size = New System.Drawing.Size(116, 29)
        Me.NavigationToolStripMenuItem.Text = "Navigation"
        '
        'menuFirst
        '
        Me.menuFirst.Image = CType(resources.GetObject("menuFirst.Image"), System.Drawing.Image)
        Me.menuFirst.Name = "menuFirst"
        Me.menuFirst.Size = New System.Drawing.Size(160, 30)
        Me.menuFirst.Text = "First"
        '
        'menuPrevious
        '
        Me.menuPrevious.Image = CType(resources.GetObject("menuPrevious.Image"), System.Drawing.Image)
        Me.menuPrevious.Name = "menuPrevious"
        Me.menuPrevious.Size = New System.Drawing.Size(160, 30)
        Me.menuPrevious.Text = "Previous"
        '
        'menuNext
        '
        Me.menuNext.Image = CType(resources.GetObject("menuNext.Image"), System.Drawing.Image)
        Me.menuNext.Name = "menuNext"
        Me.menuNext.Size = New System.Drawing.Size(160, 30)
        Me.menuNext.Text = "Next"
        '
        'menuLast
        '
        Me.menuLast.Image = CType(resources.GetObject("menuLast.Image"), System.Drawing.Image)
        Me.menuLast.Name = "menuLast"
        Me.menuLast.Size = New System.Drawing.Size(160, 30)
        Me.menuLast.Text = "Last"
        '
        'MainToolStripMenuItem
        '
        Me.MainToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MainFormToolStripMenuItem})
        Me.MainToolStripMenuItem.Name = "MainToolStripMenuItem"
        Me.MainToolStripMenuItem.Size = New System.Drawing.Size(67, 29)
        Me.MainToolStripMenuItem.Text = "Form"
        '
        'MainFormToolStripMenuItem
        '
        Me.MainFormToolStripMenuItem.Image = CType(resources.GetObject("MainFormToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MainFormToolStripMenuItem.Name = "MainFormToolStripMenuItem"
        Me.MainFormToolStripMenuItem.Size = New System.Drawing.Size(179, 30)
        Me.MainFormToolStripMenuItem.Text = "Main Form"
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.AllowUserToDeleteRows = False
        Me.dg.AllowUserToResizeColumns = False
        Me.dg.AllowUserToResizeRows = False
        Me.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dg.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dg.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dg.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Cursor = System.Windows.Forms.Cursors.Cross
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dg.DefaultCellStyle = DataGridViewCellStyle2
        Me.dg.Dock = System.Windows.Forms.DockStyle.Right
        Me.dg.GridColor = System.Drawing.Color.Gainsboro
        Me.dg.Location = New System.Drawing.Point(795, 33)
        Me.dg.MultiSelect = False
        Me.dg.Name = "dg"
        Me.dg.ReadOnly = True
        Me.dg.RowHeadersVisible = False
        Me.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dg.Size = New System.Drawing.Size(585, 726)
        Me.dg.TabIndex = 8
        '
        'ofd
        '
        Me.ofd.FileName = "OpenFileDialog1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(795, 726)
        Me.Panel1.TabIndex = 18
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.cbopost)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.chbShowRetypePassword)
        Me.Panel2.Controls.Add(Me.cbopv)
        Me.Panel2.Controls.Add(Me.chbShowPassword)
        Me.Panel2.Controls.Add(Me.idpict)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txtrpw)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtpw)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtun)
        Me.Panel2.Controls.Add(Me.LinkLabel1)
        Me.Panel2.Controls.Add(Me.txtid)
        Me.Panel2.Controls.Add(Me.txtpv)
        Me.Panel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel2.Location = New System.Drawing.Point(171, 111)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(496, 567)
        Me.Panel2.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.GhostWhite
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(50, 244)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 21)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "User name:"
        '
        'cbopost
        '
        Me.cbopost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbopost.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbopost.FormattingEnabled = True
        Me.cbopost.Items.AddRange(New Object() {"Administrator", "Librarian", "Student", "Teacher"})
        Me.cbopost.Location = New System.Drawing.Point(54, 508)
        Me.cbopost.Name = "cbopost"
        Me.cbopost.Size = New System.Drawing.Size(387, 33)
        Me.cbopost.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.GhostWhite
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(50, 424)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 21)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Privilege"
        '
        'chbShowRetypePassword
        '
        Me.chbShowRetypePassword.AutoSize = True
        Me.chbShowRetypePassword.Location = New System.Drawing.Point(416, 400)
        Me.chbShowRetypePassword.Name = "chbShowRetypePassword"
        Me.chbShowRetypePassword.Size = New System.Drawing.Size(15, 14)
        Me.chbShowRetypePassword.TabIndex = 20
        Me.chbShowRetypePassword.UseVisualStyleBackColor = True
        '
        'cbopv
        '
        Me.cbopv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbopv.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbopv.FormattingEnabled = True
        Me.cbopv.Items.AddRange(New Object() {"User", "Admin"})
        Me.cbopv.Location = New System.Drawing.Point(54, 448)
        Me.cbopv.Name = "cbopv"
        Me.cbopv.Size = New System.Drawing.Size(387, 33)
        Me.cbopv.TabIndex = 7
        '
        'chbShowPassword
        '
        Me.chbShowPassword.AutoSize = True
        Me.chbShowPassword.Location = New System.Drawing.Point(416, 340)
        Me.chbShowPassword.Name = "chbShowPassword"
        Me.chbShowPassword.Size = New System.Drawing.Size(15, 14)
        Me.chbShowPassword.TabIndex = 19
        Me.chbShowPassword.UseVisualStyleBackColor = True
        '
        'idpict
        '
        Me.idpict.BackColor = System.Drawing.Color.Transparent
        Me.idpict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.idpict.Location = New System.Drawing.Point(182, 69)
        Me.idpict.Name = "idpict"
        Me.idpict.Size = New System.Drawing.Size(137, 112)
        Me.idpict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.idpict.TabIndex = 9
        Me.idpict.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.GhostWhite
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(177, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(146, 30)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Profile Picture"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.GhostWhite
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(50, 304)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 21)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.GhostWhite
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(50, 364)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 21)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Re-type Password"
        '
        'txtrpw
        '
        Me.txtrpw.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrpw.Location = New System.Drawing.Point(54, 388)
        Me.txtrpw.Name = "txtrpw"
        Me.txtrpw.Size = New System.Drawing.Size(387, 33)
        Me.txtrpw.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.GhostWhite
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(50, 184)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 21)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "User ID:"
        '
        'txtpw
        '
        Me.txtpw.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpw.Location = New System.Drawing.Point(54, 328)
        Me.txtpw.Name = "txtpw"
        Me.txtpw.Size = New System.Drawing.Size(387, 33)
        Me.txtpw.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.GhostWhite
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(50, 484)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 21)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Position:"
        '
        'txtun
        '
        Me.txtun.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtun.Location = New System.Drawing.Point(54, 268)
        Me.txtun.Name = "txtun"
        Me.txtun.Size = New System.Drawing.Size(387, 33)
        Me.txtun.TabIndex = 2
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.Color.GhostWhite
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.ForeColor = System.Drawing.Color.Black
        Me.LinkLabel1.LinkColor = System.Drawing.Color.Blue
        Me.LinkLabel1.Location = New System.Drawing.Point(263, 184)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(56, 16)
        Me.LinkLabel1.TabIndex = 17
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Browse"
        '
        'txtid
        '
        Me.txtid.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtid.Location = New System.Drawing.Point(54, 208)
        Me.txtid.Name = "txtid"
        Me.txtid.Size = New System.Drawing.Size(387, 33)
        Me.txtid.TabIndex = 1
        '
        'txtpv
        '
        Me.txtpv.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpv.Location = New System.Drawing.Point(54, 448)
        Me.txtpv.Name = "txtpv"
        Me.txtpv.Size = New System.Drawing.Size(387, 33)
        Me.txtpv.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(795, 74)
        Me.Panel3.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label8.Location = New System.Drawing.Point(162, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(472, 45)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "REGISTRATION FORM"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 699)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(795, 27)
        Me.Panel4.TabIndex = 20
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1380, 759)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.idpict, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents NavigationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFirst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPrevious As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLast As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSearch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents menuRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainFormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbopost As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chbShowRetypePassword As System.Windows.Forms.CheckBox
    Friend WithEvents cbopv As System.Windows.Forms.ComboBox
    Friend WithEvents chbShowPassword As System.Windows.Forms.CheckBox
    Friend WithEvents idpict As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtrpw As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtpw As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtun As System.Windows.Forms.TextBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents txtid As System.Windows.Forms.TextBox
    Friend WithEvents txtpv As System.Windows.Forms.TextBox

End Class
