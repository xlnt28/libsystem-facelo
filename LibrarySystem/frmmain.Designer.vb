<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmmain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmmain))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ManageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuUserForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuBookInventory = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuTransactions = New System.Windows.Forms.ToolStripMenuItem()
        Me.BorrowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenaltyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransactionHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BorrowPendingRequestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UFCRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UFEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BookInventoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BICRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BIEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReceiptsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLogout = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pbProfile = New System.Windows.Forms.PictureBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtPrivilege = New System.Windows.Forms.Label()
        Me.txtPosition = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblCurrentBorrowings = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblCurrentRequestedBook = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.lblUnpaidBorrowedBooks = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lblBooksReturned = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.dashUserpanel = New System.Windows.Forms.Panel()
        Me.dashAdminPan = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.lblTotalBookCopies = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.btnCurrentRequestedReturn = New System.Windows.Forms.Button()
        Me.lblTotalBorrowedRequest = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.btnviewTotalUnpaidPenalties = New System.Windows.Forms.Button()
        Me.lblTotalUnpaidPenalties = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTotalBorrowedBooks = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.lblTotalBooks = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.lblForgotPasswordRequest = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.panReceipts = New System.Windows.Forms.Panel()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BorrowReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenaltyReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.pbProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.dashUserpanel.SuspendLayout()
        Me.dashAdminPan.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.panReceipts.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManageToolStripMenuItem, Me.menuTransactions, Me.ReportsToolStripMenuItem, Me.menuLogout})
        Me.MenuStrip1.Location = New System.Drawing.Point(8, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(506, 40)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ManageToolStripMenuItem
        '
        Me.ManageToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuUserForm, Me.menuBookInventory, Me.ChangePasswordToolStripMenuItem})
        Me.ManageToolStripMenuItem.Name = "ManageToolStripMenuItem"
        Me.ManageToolStripMenuItem.Size = New System.Drawing.Size(116, 36)
        Me.ManageToolStripMenuItem.Text = "Manage"
        '
        'menuUserForm
        '
        Me.menuUserForm.Image = CType(resources.GetObject("menuUserForm.Image"), System.Drawing.Image)
        Me.menuUserForm.Name = "menuUserForm"
        Me.menuUserForm.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.menuUserForm.Size = New System.Drawing.Size(433, 36)
        Me.menuUserForm.Text = "User Form"
        '
        'menuBookInventory
        '
        Me.menuBookInventory.Image = CType(resources.GetObject("menuBookInventory.Image"), System.Drawing.Image)
        Me.menuBookInventory.Name = "menuBookInventory"
        Me.menuBookInventory.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.menuBookInventory.Size = New System.Drawing.Size(433, 36)
        Me.menuBookInventory.Text = "Book Inventory"
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Image = CType(resources.GetObject("ChangePasswordToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(433, 36)
        Me.ChangePasswordToolStripMenuItem.Text = "Change Password"
        '
        'menuTransactions
        '
        Me.menuTransactions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrowToolStripMenuItem, Me.ReturnToolStripMenuItem, Me.PenaltyToolStripMenuItem, Me.TransactionHistoryToolStripMenuItem, Me.BorrowPendingRequestToolStripMenuItem})
        Me.menuTransactions.Name = "menuTransactions"
        Me.menuTransactions.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.menuTransactions.Size = New System.Drawing.Size(151, 36)
        Me.menuTransactions.Text = "Transaction"
        '
        'BorrowToolStripMenuItem
        '
        Me.BorrowToolStripMenuItem.Image = CType(resources.GetObject("BorrowToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BorrowToolStripMenuItem.Name = "BorrowToolStripMenuItem"
        Me.BorrowToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BorrowToolStripMenuItem.Size = New System.Drawing.Size(511, 36)
        Me.BorrowToolStripMenuItem.Text = "Borrow Book"
        '
        'ReturnToolStripMenuItem
        '
        Me.ReturnToolStripMenuItem.Image = CType(resources.GetObject("ReturnToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ReturnToolStripMenuItem.Name = "ReturnToolStripMenuItem"
        Me.ReturnToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.ReturnToolStripMenuItem.Size = New System.Drawing.Size(511, 36)
        Me.ReturnToolStripMenuItem.Text = "Return Book"
        '
        'PenaltyToolStripMenuItem
        '
        Me.PenaltyToolStripMenuItem.Image = CType(resources.GetObject("PenaltyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PenaltyToolStripMenuItem.Name = "PenaltyToolStripMenuItem"
        Me.PenaltyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PenaltyToolStripMenuItem.Size = New System.Drawing.Size(511, 36)
        Me.PenaltyToolStripMenuItem.Text = "Penalty"
        '
        'TransactionHistoryToolStripMenuItem
        '
        Me.TransactionHistoryToolStripMenuItem.Image = CType(resources.GetObject("TransactionHistoryToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TransactionHistoryToolStripMenuItem.Name = "TransactionHistoryToolStripMenuItem"
        Me.TransactionHistoryToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.TransactionHistoryToolStripMenuItem.Size = New System.Drawing.Size(511, 36)
        Me.TransactionHistoryToolStripMenuItem.Text = "Transaction History"
        '
        'BorrowPendingRequestToolStripMenuItem
        '
        Me.BorrowPendingRequestToolStripMenuItem.Image = CType(resources.GetObject("BorrowPendingRequestToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BorrowPendingRequestToolStripMenuItem.Name = "BorrowPendingRequestToolStripMenuItem"
        Me.BorrowPendingRequestToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.BorrowPendingRequestToolStripMenuItem.Size = New System.Drawing.Size(511, 36)
        Me.BorrowPendingRequestToolStripMenuItem.Text = "Borrow Pending Request"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserFormToolStripMenuItem, Me.BookInventoryToolStripMenuItem, Me.ReceiptsToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(111, 36)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'UserFormToolStripMenuItem
        '
        Me.UserFormToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UFCRToolStripMenuItem, Me.UFEToolStripMenuItem})
        Me.UserFormToolStripMenuItem.Image = CType(resources.GetObject("UserFormToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UserFormToolStripMenuItem.Name = "UserFormToolStripMenuItem"
        Me.UserFormToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.UserFormToolStripMenuItem.Size = New System.Drawing.Size(336, 36)
        Me.UserFormToolStripMenuItem.Text = "User Form"
        '
        'UFCRToolStripMenuItem
        '
        Me.UFCRToolStripMenuItem.Image = CType(resources.GetObject("UFCRToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UFCRToolStripMenuItem.Name = "UFCRToolStripMenuItem"
        Me.UFCRToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.UFCRToolStripMenuItem.Size = New System.Drawing.Size(401, 36)
        Me.UFCRToolStripMenuItem.Text = "Crystal Report"
        '
        'UFEToolStripMenuItem
        '
        Me.UFEToolStripMenuItem.Image = CType(resources.GetObject("UFEToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UFEToolStripMenuItem.Name = "UFEToolStripMenuItem"
        Me.UFEToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.UFEToolStripMenuItem.Size = New System.Drawing.Size(401, 36)
        Me.UFEToolStripMenuItem.Text = "Excel"
        '
        'BookInventoryToolStripMenuItem
        '
        Me.BookInventoryToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BICRToolStripMenuItem, Me.BIEToolStripMenuItem})
        Me.BookInventoryToolStripMenuItem.Image = CType(resources.GetObject("BookInventoryToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BookInventoryToolStripMenuItem.Name = "BookInventoryToolStripMenuItem"
        Me.BookInventoryToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BookInventoryToolStripMenuItem.Size = New System.Drawing.Size(336, 36)
        Me.BookInventoryToolStripMenuItem.Text = "Book Inventory"
        '
        'BICRToolStripMenuItem
        '
        Me.BICRToolStripMenuItem.Image = CType(resources.GetObject("BICRToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BICRToolStripMenuItem.Name = "BICRToolStripMenuItem"
        Me.BICRToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BICRToolStripMenuItem.Size = New System.Drawing.Size(399, 36)
        Me.BICRToolStripMenuItem.Text = "Crystal Report"
        '
        'BIEToolStripMenuItem
        '
        Me.BIEToolStripMenuItem.Image = CType(resources.GetObject("BIEToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BIEToolStripMenuItem.Name = "BIEToolStripMenuItem"
        Me.BIEToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BIEToolStripMenuItem.Size = New System.Drawing.Size(399, 36)
        Me.BIEToolStripMenuItem.Text = "Excel"
        '
        'ReceiptsToolStripMenuItem
        '
        Me.ReceiptsToolStripMenuItem.Image = CType(resources.GetObject("ReceiptsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ReceiptsToolStripMenuItem.Name = "ReceiptsToolStripMenuItem"
        Me.ReceiptsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.ReceiptsToolStripMenuItem.Size = New System.Drawing.Size(336, 36)
        Me.ReceiptsToolStripMenuItem.Text = "Receipts"
        '
        'menuLogout
        '
        Me.menuLogout.Image = CType(resources.GetObject("menuLogout.Image"), System.Drawing.Image)
        Me.menuLogout.Name = "menuLogout"
        Me.menuLogout.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.menuLogout.Size = New System.Drawing.Size(120, 36)
        Me.menuLogout.Text = "Logout"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 656)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1384, 103)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.pbProfile)
        Me.Panel2.Location = New System.Drawing.Point(36, 130)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(331, 353)
        Me.Panel2.TabIndex = 5
        '
        'pbProfile
        '
        Me.pbProfile.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pbProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbProfile.Location = New System.Drawing.Point(13, 13)
        Me.pbProfile.Name = "pbProfile"
        Me.pbProfile.Size = New System.Drawing.Size(301, 325)
        Me.pbProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbProfile.TabIndex = 2
        Me.pbProfile.TabStop = False
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUsername.Location = New System.Drawing.Point(368, 117)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(196, 50)
        Me.lblUsername.TabIndex = 5
        Me.lblUsername.Text = "Username"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 48.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(11, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(418, 78)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "WELCOME,"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SeaShell
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(36, 489)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(331, 165)
        Me.Panel3.TabIndex = 6
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Linen
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.txtPrivilege)
        Me.Panel5.Controls.Add(Me.txtPosition)
        Me.Panel5.Location = New System.Drawing.Point(36, 505)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(331, 163)
        Me.Panel5.TabIndex = 7
        '
        'txtPrivilege
        '
        Me.txtPrivilege.BackColor = System.Drawing.Color.SeaShell
        Me.txtPrivilege.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrivilege.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrivilege.ForeColor = System.Drawing.Color.Black
        Me.txtPrivilege.Location = New System.Drawing.Point(-1, 16)
        Me.txtPrivilege.Name = "txtPrivilege"
        Me.txtPrivilege.Size = New System.Drawing.Size(331, 60)
        Me.txtPrivilege.TabIndex = 3
        Me.txtPrivilege.Text = "User's Privilege"
        Me.txtPrivilege.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPosition
        '
        Me.txtPosition.BackColor = System.Drawing.Color.SeaShell
        Me.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPosition.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPosition.ForeColor = System.Drawing.Color.Black
        Me.txtPosition.Location = New System.Drawing.Point(-1, 85)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(331, 62)
        Me.txtPosition.TabIndex = 2
        Me.txtPosition.Text = "User's Position"
        Me.txtPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.MenuStrip1)
        Me.Panel4.Controls.Add(Me.PictureBox1)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1384, 174)
        Me.Panel4.TabIndex = 7
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1227, 48)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(119, 118)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.SeaShell
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.lblCurrentBorrowings)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Location = New System.Drawing.Point(21, 27)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(302, 193)
        Me.Panel6.TabIndex = 8
        '
        'lblCurrentBorrowings
        '
        Me.lblCurrentBorrowings.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentBorrowings.Location = New System.Drawing.Point(-1, 40)
        Me.lblCurrentBorrowings.Name = "lblCurrentBorrowings"
        Me.lblCurrentBorrowings.Size = New System.Drawing.Size(302, 151)
        Me.lblCurrentBorrowings.TabIndex = 3
        Me.lblCurrentBorrowings.Text = "0"
        Me.lblCurrentBorrowings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-1, -1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(302, 41)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Current Record Borrowings"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.SeaShell
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.lblCurrentRequestedBook)
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Location = New System.Drawing.Point(352, 27)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(302, 193)
        Me.Panel7.TabIndex = 9
        '
        'lblCurrentRequestedBook
        '
        Me.lblCurrentRequestedBook.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentRequestedBook.Location = New System.Drawing.Point(-1, 49)
        Me.lblCurrentRequestedBook.Name = "lblCurrentRequestedBook"
        Me.lblCurrentRequestedBook.Size = New System.Drawing.Size(302, 142)
        Me.lblCurrentRequestedBook.TabIndex = 4
        Me.lblCurrentRequestedBook.Text = "0"
        Me.lblCurrentRequestedBook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(-1, -1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(302, 40)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Current Requested Book"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.SeaShell
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.lblUnpaidBorrowedBooks)
        Me.Panel8.Controls.Add(Me.Label6)
        Me.Panel8.Location = New System.Drawing.Point(352, 226)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(302, 193)
        Me.Panel8.TabIndex = 9
        '
        'lblUnpaidBorrowedBooks
        '
        Me.lblUnpaidBorrowedBooks.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnpaidBorrowedBooks.Location = New System.Drawing.Point(-1, 50)
        Me.lblUnpaidBorrowedBooks.Name = "lblUnpaidBorrowedBooks"
        Me.lblUnpaidBorrowedBooks.Size = New System.Drawing.Size(302, 142)
        Me.lblUnpaidBorrowedBooks.TabIndex = 5
        Me.lblUnpaidBorrowedBooks.Text = "0"
        Me.lblUnpaidBorrowedBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(-1, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(302, 40)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Unpaid Borrowed Books"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.SeaShell
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.lblBooksReturned)
        Me.Panel9.Controls.Add(Me.Label5)
        Me.Panel9.Controls.Add(Me.Label4)
        Me.Panel9.Location = New System.Drawing.Point(21, 226)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(302, 193)
        Me.Panel9.TabIndex = 9
        '
        'lblBooksReturned
        '
        Me.lblBooksReturned.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold)
        Me.lblBooksReturned.Location = New System.Drawing.Point(-1, 42)
        Me.lblBooksReturned.Name = "lblBooksReturned"
        Me.lblBooksReturned.Size = New System.Drawing.Size(302, 150)
        Me.lblBooksReturned.TabIndex = 5
        Me.lblBooksReturned.Text = "0"
        Me.lblBooksReturned.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(-1, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(302, 41)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Books Returned"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(118, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 13)
        Me.Label4.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.PeachPuff
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(684, 27)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(280, 193)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Your Borrow Requests"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Honeydew
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Location = New System.Drawing.Point(684, 228)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(280, 193)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Returned Book History"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'dashUserpanel
        '
        Me.dashUserpanel.Controls.Add(Me.Button2)
        Me.dashUserpanel.Controls.Add(Me.Button1)
        Me.dashUserpanel.Controls.Add(Me.Panel8)
        Me.dashUserpanel.Controls.Add(Me.Panel9)
        Me.dashUserpanel.Controls.Add(Me.Panel7)
        Me.dashUserpanel.Controls.Add(Me.Panel6)
        Me.dashUserpanel.Location = New System.Drawing.Point(387, 194)
        Me.dashUserpanel.Name = "dashUserpanel"
        Me.dashUserpanel.Size = New System.Drawing.Size(985, 439)
        Me.dashUserpanel.TabIndex = 12
        '
        'dashAdminPan
        '
        Me.dashAdminPan.Controls.Add(Me.Panel15)
        Me.dashAdminPan.Controls.Add(Me.Panel14)
        Me.dashAdminPan.Controls.Add(Me.Panel13)
        Me.dashAdminPan.Controls.Add(Me.Panel12)
        Me.dashAdminPan.Controls.Add(Me.Panel11)
        Me.dashAdminPan.Controls.Add(Me.Panel10)
        Me.dashAdminPan.Location = New System.Drawing.Point(387, 194)
        Me.dashAdminPan.Name = "dashAdminPan"
        Me.dashAdminPan.Size = New System.Drawing.Size(985, 439)
        Me.dashAdminPan.TabIndex = 13
        '
        'Panel15
        '
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Controls.Add(Me.lblTotalBookCopies)
        Me.Panel15.Controls.Add(Me.Label8)
        Me.Panel15.Location = New System.Drawing.Point(56, 233)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(277, 185)
        Me.Panel15.TabIndex = 18
        '
        'lblTotalBookCopies
        '
        Me.lblTotalBookCopies.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalBookCopies.Location = New System.Drawing.Point(-1, 38)
        Me.lblTotalBookCopies.Name = "lblTotalBookCopies"
        Me.lblTotalBookCopies.Size = New System.Drawing.Size(276, 146)
        Me.lblTotalBookCopies.TabIndex = 19
        Me.lblTotalBookCopies.Text = "0"
        Me.lblTotalBookCopies.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(-1, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(276, 31)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Total Book Copies"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel14
        '
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Controls.Add(Me.btnCurrentRequestedReturn)
        Me.Panel14.Controls.Add(Me.lblTotalBorrowedRequest)
        Me.Panel14.Controls.Add(Me.Label16)
        Me.Panel14.Location = New System.Drawing.Point(676, 27)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(278, 185)
        Me.Panel14.TabIndex = 18
        '
        'btnCurrentRequestedReturn
        '
        Me.btnCurrentRequestedReturn.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnCurrentRequestedReturn.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnCurrentRequestedReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCurrentRequestedReturn.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCurrentRequestedReturn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCurrentRequestedReturn.Location = New System.Drawing.Point(0, 127)
        Me.btnCurrentRequestedReturn.Name = "btnCurrentRequestedReturn"
        Me.btnCurrentRequestedReturn.Size = New System.Drawing.Size(276, 56)
        Me.btnCurrentRequestedReturn.TabIndex = 16
        Me.btnCurrentRequestedReturn.Text = "View"
        Me.btnCurrentRequestedReturn.UseVisualStyleBackColor = False
        '
        'lblTotalBorrowedRequest
        '
        Me.lblTotalBorrowedRequest.Font = New System.Drawing.Font("Segoe UI", 48.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalBorrowedRequest.Location = New System.Drawing.Point(0, 34)
        Me.lblTotalBorrowedRequest.Name = "lblTotalBorrowedRequest"
        Me.lblTotalBorrowedRequest.Size = New System.Drawing.Size(276, 90)
        Me.lblTotalBorrowedRequest.TabIndex = 15
        Me.lblTotalBorrowedRequest.Text = "0"
        Me.lblTotalBorrowedRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(0, -1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(276, 31)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Current Borrow Request"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel13
        '
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Controls.Add(Me.btnviewTotalUnpaidPenalties)
        Me.Panel13.Controls.Add(Me.lblTotalUnpaidPenalties)
        Me.Panel13.Controls.Add(Me.Label14)
        Me.Panel13.Location = New System.Drawing.Point(368, 27)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(278, 186)
        Me.Panel13.TabIndex = 17
        '
        'btnviewTotalUnpaidPenalties
        '
        Me.btnviewTotalUnpaidPenalties.BackColor = System.Drawing.Color.IndianRed
        Me.btnviewTotalUnpaidPenalties.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnviewTotalUnpaidPenalties.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnviewTotalUnpaidPenalties.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnviewTotalUnpaidPenalties.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnviewTotalUnpaidPenalties.Location = New System.Drawing.Point(0, 128)
        Me.btnviewTotalUnpaidPenalties.Name = "btnviewTotalUnpaidPenalties"
        Me.btnviewTotalUnpaidPenalties.Size = New System.Drawing.Size(276, 56)
        Me.btnviewTotalUnpaidPenalties.TabIndex = 16
        Me.btnviewTotalUnpaidPenalties.Text = "View"
        Me.btnviewTotalUnpaidPenalties.UseVisualStyleBackColor = False
        '
        'lblTotalUnpaidPenalties
        '
        Me.lblTotalUnpaidPenalties.Font = New System.Drawing.Font("Segoe UI", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalUnpaidPenalties.Location = New System.Drawing.Point(3, 34)
        Me.lblTotalUnpaidPenalties.Name = "lblTotalUnpaidPenalties"
        Me.lblTotalUnpaidPenalties.Size = New System.Drawing.Size(270, 90)
        Me.lblTotalUnpaidPenalties.TabIndex = 15
        Me.lblTotalUnpaidPenalties.Text = "0"
        Me.lblTotalUnpaidPenalties.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(0, -1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(276, 31)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "Total Unpaid Penalties"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.Label12)
        Me.Panel12.Controls.Add(Me.lblTotalBorrowedBooks)
        Me.Panel12.Location = New System.Drawing.Point(368, 232)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(278, 185)
        Me.Panel12.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(276, 31)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Total Borrowed Books"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalBorrowedBooks
        '
        Me.lblTotalBorrowedBooks.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalBorrowedBooks.Location = New System.Drawing.Point(3, 34)
        Me.lblTotalBorrowedBooks.Name = "lblTotalBorrowedBooks"
        Me.lblTotalBorrowedBooks.Size = New System.Drawing.Size(270, 146)
        Me.lblTotalBorrowedBooks.TabIndex = 15
        Me.lblTotalBorrowedBooks.Text = "0"
        Me.lblTotalBorrowedBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel11
        '
        Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel11.Controls.Add(Me.lblTotalBooks)
        Me.Panel11.Controls.Add(Me.Label10)
        Me.Panel11.Location = New System.Drawing.Point(676, 231)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(278, 185)
        Me.Panel11.TabIndex = 13
        '
        'lblTotalBooks
        '
        Me.lblTotalBooks.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalBooks.Location = New System.Drawing.Point(3, 34)
        Me.lblTotalBooks.Name = "lblTotalBooks"
        Me.lblTotalBooks.Size = New System.Drawing.Size(270, 149)
        Me.lblTotalBooks.TabIndex = 15
        Me.lblTotalBooks.Text = "0"
        Me.lblTotalBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(-1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(278, 30)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Total Books"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.Button4)
        Me.Panel10.Controls.Add(Me.lblForgotPasswordRequest)
        Me.Panel10.Controls.Add(Me.Label7)
        Me.Panel10.Location = New System.Drawing.Point(55, 27)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(278, 184)
        Me.Panel10.TabIndex = 12
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.LightSeaGreen
        Me.Button4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button4.Location = New System.Drawing.Point(0, 126)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(276, 56)
        Me.Button4.TabIndex = 16
        Me.Button4.Text = "View"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'lblForgotPasswordRequest
        '
        Me.lblForgotPasswordRequest.Font = New System.Drawing.Font("Segoe UI", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForgotPasswordRequest.Location = New System.Drawing.Point(3, 34)
        Me.lblForgotPasswordRequest.Name = "lblForgotPasswordRequest"
        Me.lblForgotPasswordRequest.Size = New System.Drawing.Size(270, 90)
        Me.lblForgotPasswordRequest.TabIndex = 15
        Me.lblForgotPasswordRequest.Text = "0"
        Me.lblForgotPasswordRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(16, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(235, 25)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Forgot password request"
        '
        'panReceipts
        '
        Me.panReceipts.Controls.Add(Me.dgv)
        Me.panReceipts.Controls.Add(Me.MenuStrip2)
        Me.panReceipts.Location = New System.Drawing.Point(387, 194)
        Me.panReceipts.Name = "panReceipts"
        Me.panReceipts.Size = New System.Drawing.Size(954, 439)
        Me.panReceipts.TabIndex = 14
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeColumns = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.GridColor = System.Drawing.Color.Gainsboro
        Me.dgv.Location = New System.Drawing.Point(0, 33)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(954, 406)
        Me.dgv.TabIndex = 13
        '
        'MenuStrip2
        '
        Me.MenuStrip2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem1, Me.MenuToolStripMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip2.ShowItemToolTips = True
        Me.MenuStrip2.Size = New System.Drawing.Size(954, 33)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'MenuToolStripMenuItem1
        '
        Me.MenuToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchToolStripMenuItem, Me.PrintReceiptToolStripMenuItem, Me.RefreshToolStripMenuItem})
        Me.MenuToolStripMenuItem1.Name = "MenuToolStripMenuItem1"
        Me.MenuToolStripMenuItem1.Size = New System.Drawing.Size(73, 29)
        Me.MenuToolStripMenuItem1.Text = "Menu"
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Image = CType(resources.GetObject("SearchToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(256, 30)
        Me.SearchToolStripMenuItem.Text = "Search"
        '
        'PrintReceiptToolStripMenuItem
        '
        Me.PrintReceiptToolStripMenuItem.Image = CType(resources.GetObject("PrintReceiptToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintReceiptToolStripMenuItem.Name = "PrintReceiptToolStripMenuItem"
        Me.PrintReceiptToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintReceiptToolStripMenuItem.Size = New System.Drawing.Size(256, 30)
        Me.PrintReceiptToolStripMenuItem.Text = "Print Receipt"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = CType(resources.GetObject("RefreshToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(256, 30)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrowReceiptToolStripMenuItem, Me.ReturnReceiptToolStripMenuItem, Me.PenaltyReceiptToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(139, 29)
        Me.MenuToolStripMenuItem.Text = "View Receipts"
        '
        'BorrowReceiptToolStripMenuItem
        '
        Me.BorrowReceiptToolStripMenuItem.Name = "BorrowReceiptToolStripMenuItem"
        Me.BorrowReceiptToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BorrowReceiptToolStripMenuItem.Size = New System.Drawing.Size(270, 30)
        Me.BorrowReceiptToolStripMenuItem.Text = "Borrow Receipt"
        '
        'ReturnReceiptToolStripMenuItem
        '
        Me.ReturnReceiptToolStripMenuItem.Name = "ReturnReceiptToolStripMenuItem"
        Me.ReturnReceiptToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.ReturnReceiptToolStripMenuItem.Size = New System.Drawing.Size(270, 30)
        Me.ReturnReceiptToolStripMenuItem.Text = "Return Receipt"
        '
        'PenaltyReceiptToolStripMenuItem
        '
        Me.PenaltyReceiptToolStripMenuItem.Name = "PenaltyReceiptToolStripMenuItem"
        Me.PenaltyReceiptToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PenaltyReceiptToolStripMenuItem.Size = New System.Drawing.Size(270, 30)
        Me.PenaltyReceiptToolStripMenuItem.Text = "Penalty Receipt"
        '
        'frmmain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SeaShell
        Me.ClientSize = New System.Drawing.Size(1384, 759)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.panReceipts)
        Me.Controls.Add(Me.dashAdminPan)
        Me.Controls.Add(Me.dashUserpanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmmain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.pbProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.dashUserpanel.ResumeLayout(False)
        Me.dashAdminPan.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.panReceipts.ResumeLayout(False)
        Me.panReceipts.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents menuLogout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuUserForm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuBookInventory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuTransactions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BorrowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReturnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PenaltyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pbProfile As System.Windows.Forms.PictureBox
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentBorrowings As System.Windows.Forms.Label
    Friend WithEvents lblCurrentRequestedBook As System.Windows.Forms.Label
    Friend WithEvents lblBooksReturned As System.Windows.Forms.Label
    Friend WithEvents TransactionHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblUnpaidBorrowedBooks As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BorrowPendingRequestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtPrivilege As Label
    Friend WithEvents txtPosition As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents dashUserpanel As Panel
    Friend WithEvents dashAdminPan As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents lblForgotPasswordRequest As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Panel11 As Panel
    Friend WithEvents lblTotalBooks As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents btnviewTotalUnpaidPenalties As Button
    Friend WithEvents lblTotalUnpaidPenalties As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel12 As Panel
    Friend WithEvents lblTotalBorrowedBooks As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel14 As Panel
    Friend WithEvents btnCurrentRequestedReturn As Button
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents lblTotalBorrowedRequest As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblTotalBookCopies As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UserFormToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UFCRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UFEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BookInventoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BICRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BIEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReceiptsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents panReceipts As Panel
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents MenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BorrowReceiptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReturnReceiptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PenaltyReceiptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dgv As DataGridView
    Friend WithEvents MenuToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintReceiptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
End Class
