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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ManageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuUserForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuBookInventory = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuTransactions = New System.Windows.Forms.ToolStripMenuItem()
        Me.BorrowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenaltyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransactionHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BorrowPendingRequestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblCurrentBorrowings = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.lblCurrentRequestedBook = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.lblUnpaidBorrowedBooks = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lblBooksReturned = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.pbProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Snow
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManageToolStripMenuItem, Me.menuTransactions, Me.menuLogout})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1384, 33)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ManageToolStripMenuItem
        '
        Me.ManageToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuUserForm, Me.menuBookInventory})
        Me.ManageToolStripMenuItem.Name = "ManageToolStripMenuItem"
        Me.ManageToolStripMenuItem.Size = New System.Drawing.Size(93, 29)
        Me.ManageToolStripMenuItem.Text = "Manage"
        '
        'menuUserForm
        '
        Me.menuUserForm.Image = CType(resources.GetObject("menuUserForm.Image"), System.Drawing.Image)
        Me.menuUserForm.Name = "menuUserForm"
        Me.menuUserForm.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.menuUserForm.Size = New System.Drawing.Size(270, 30)
        Me.menuUserForm.Text = "User Form"
        '
        'menuBookInventory
        '
        Me.menuBookInventory.Image = CType(resources.GetObject("menuBookInventory.Image"), System.Drawing.Image)
        Me.menuBookInventory.Name = "menuBookInventory"
        Me.menuBookInventory.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.menuBookInventory.Size = New System.Drawing.Size(270, 30)
        Me.menuBookInventory.Text = "Book Inventory"
        '
        'menuTransactions
        '
        Me.menuTransactions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrowToolStripMenuItem, Me.ReturnToolStripMenuItem, Me.PenaltyToolStripMenuItem, Me.TransactionHistoryToolStripMenuItem, Me.BorrowPendingRequestToolStripMenuItem})
        Me.menuTransactions.Name = "menuTransactions"
        Me.menuTransactions.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.menuTransactions.Size = New System.Drawing.Size(120, 29)
        Me.menuTransactions.Text = "Transaction"
        '
        'BorrowToolStripMenuItem
        '
        Me.BorrowToolStripMenuItem.Image = CType(resources.GetObject("BorrowToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BorrowToolStripMenuItem.Name = "BorrowToolStripMenuItem"
        Me.BorrowToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BorrowToolStripMenuItem.Size = New System.Drawing.Size(407, 30)
        Me.BorrowToolStripMenuItem.Text = "Borrow Book"
        '
        'ReturnToolStripMenuItem
        '
        Me.ReturnToolStripMenuItem.Image = CType(resources.GetObject("ReturnToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ReturnToolStripMenuItem.Name = "ReturnToolStripMenuItem"
        Me.ReturnToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.ReturnToolStripMenuItem.Size = New System.Drawing.Size(407, 30)
        Me.ReturnToolStripMenuItem.Text = "Return Book"
        '
        'PenaltyToolStripMenuItem
        '
        Me.PenaltyToolStripMenuItem.Image = CType(resources.GetObject("PenaltyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PenaltyToolStripMenuItem.Name = "PenaltyToolStripMenuItem"
        Me.PenaltyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PenaltyToolStripMenuItem.Size = New System.Drawing.Size(407, 30)
        Me.PenaltyToolStripMenuItem.Text = "Penalty"
        '
        'TransactionHistoryToolStripMenuItem
        '
        Me.TransactionHistoryToolStripMenuItem.Image = CType(resources.GetObject("TransactionHistoryToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TransactionHistoryToolStripMenuItem.Name = "TransactionHistoryToolStripMenuItem"
        Me.TransactionHistoryToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.TransactionHistoryToolStripMenuItem.Size = New System.Drawing.Size(407, 30)
        Me.TransactionHistoryToolStripMenuItem.Text = "Transaction History"
        '
        'BorrowPendingRequestToolStripMenuItem
        '
        Me.BorrowPendingRequestToolStripMenuItem.Image = CType(resources.GetObject("BorrowPendingRequestToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BorrowPendingRequestToolStripMenuItem.Name = "BorrowPendingRequestToolStripMenuItem"
        Me.BorrowPendingRequestToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.BorrowPendingRequestToolStripMenuItem.Size = New System.Drawing.Size(407, 30)
        Me.BorrowPendingRequestToolStripMenuItem.Text = "Borrow Pending Request"
        '
        'menuLogout
        '
        Me.menuLogout.Image = CType(resources.GetObject("menuLogout.Image"), System.Drawing.Image)
        Me.menuLogout.Name = "menuLogout"
        Me.menuLogout.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.menuLogout.Size = New System.Drawing.Size(99, 29)
        Me.menuLogout.Text = "Logout"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 639)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1384, 120)
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
        Me.Label1.Location = New System.Drawing.Point(11, 16)
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
        Me.txtPrivilege.Location = New System.Drawing.Point(-1, 35)
        Me.txtPrivilege.Name = "txtPrivilege"
        Me.txtPrivilege.Size = New System.Drawing.Size(331, 41)
        Me.txtPrivilege.TabIndex = 3
        Me.txtPrivilege.Text = "Text"
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
        Me.txtPosition.Size = New System.Drawing.Size(331, 39)
        Me.txtPosition.TabIndex = 2
        Me.txtPosition.Text = "Text"
        Me.txtPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Location = New System.Drawing.Point(0, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1380, 156)
        Me.Panel4.TabIndex = 7
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.SeaShell
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.lblCurrentBorrowings)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Location = New System.Drawing.Point(547, 205)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(331, 193)
        Me.Panel6.TabIndex = 8
        '
        'lblCurrentBorrowings
        '
        Me.lblCurrentBorrowings.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentBorrowings.Location = New System.Drawing.Point(-1, 40)
        Me.lblCurrentBorrowings.Name = "lblCurrentBorrowings"
        Me.lblCurrentBorrowings.Size = New System.Drawing.Size(331, 151)
        Me.lblCurrentBorrowings.TabIndex = 3
        Me.lblCurrentBorrowings.Text = "0"
        Me.lblCurrentBorrowings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, -1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(323, 41)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Current Record Borrowings"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.SeaShell
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.btnReports)
        Me.Panel7.Controls.Add(Me.lblCurrentRequestedBook)
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Location = New System.Drawing.Point(895, 205)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(331, 193)
        Me.Panel7.TabIndex = 9
        '
        'btnReports
        '
        Me.btnReports.BackColor = System.Drawing.Color.PeachPuff
        Me.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReports.Font = New System.Drawing.Font("Verdana", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReports.ForeColor = System.Drawing.Color.Black
        Me.btnReports.Location = New System.Drawing.Point(-1, -1)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(331, 193)
        Me.btnReports.TabIndex = 10
        Me.btnReports.Text = "Reports"
        Me.btnReports.UseVisualStyleBackColor = False
        '
        'lblCurrentRequestedBook
        '
        Me.lblCurrentRequestedBook.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentRequestedBook.Location = New System.Drawing.Point(-1, 49)
        Me.lblCurrentRequestedBook.Name = "lblCurrentRequestedBook"
        Me.lblCurrentRequestedBook.Size = New System.Drawing.Size(331, 142)
        Me.lblCurrentRequestedBook.TabIndex = 4
        Me.lblCurrentRequestedBook.Text = "0"
        Me.lblCurrentRequestedBook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, -1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(323, 40)
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
        Me.Panel8.Location = New System.Drawing.Point(923, 413)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(331, 193)
        Me.Panel8.TabIndex = 9
        '
        'lblUnpaidBorrowedBooks
        '
        Me.lblUnpaidBorrowedBooks.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnpaidBorrowedBooks.Location = New System.Drawing.Point(-1, 50)
        Me.lblUnpaidBorrowedBooks.Name = "lblUnpaidBorrowedBooks"
        Me.lblUnpaidBorrowedBooks.Size = New System.Drawing.Size(331, 142)
        Me.lblUnpaidBorrowedBooks.TabIndex = 5
        Me.lblUnpaidBorrowedBooks.Text = "0"
        Me.lblUnpaidBorrowedBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(323, 40)
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
        Me.Panel9.Location = New System.Drawing.Point(575, 413)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(331, 193)
        Me.Panel9.TabIndex = 9
        '
        'lblBooksReturned
        '
        Me.lblBooksReturned.Font = New System.Drawing.Font("Segoe UI", 72.0!, System.Drawing.FontStyle.Bold)
        Me.lblBooksReturned.Location = New System.Drawing.Point(-1, 42)
        Me.lblBooksReturned.Name = "lblBooksReturned"
        Me.lblBooksReturned.Size = New System.Drawing.Size(331, 150)
        Me.lblBooksReturned.TabIndex = 5
        Me.lblBooksReturned.Text = "0"
        Me.lblBooksReturned.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(323, 41)
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
        'frmmain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SeaShell
        Me.ClientSize = New System.Drawing.Size(1384, 759)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
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
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
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
    Friend WithEvents btnReports As System.Windows.Forms.Button
    Friend WithEvents txtPrivilege As Label
    Friend WithEvents txtPosition As Label
End Class
