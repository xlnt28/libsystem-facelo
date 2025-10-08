<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ForgotPasswordRequest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ForgotPasswordRequest))
        Me.panRequest = New System.Windows.Forms.Panel()
        Me.chkHideUserID = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPhoneNumber = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUserNameForRequest = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RequestCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.llContactLibrarian = New System.Windows.Forms.LinkLabel()
        Me.panResetPassword = New System.Windows.Forms.Panel()
        Me.chkHidePassword = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtUserNameForReset = New System.Windows.Forms.TextBox()
        Me.btnChangePassword = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.panRequest.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.panResetPassword.SuspendLayout()
        Me.SuspendLayout()
        '
        'panRequest
        '
        Me.panRequest.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panRequest.Controls.Add(Me.chkHideUserID)
        Me.panRequest.Controls.Add(Me.Label7)
        Me.panRequest.Controls.Add(Me.txtPhoneNumber)
        Me.panRequest.Controls.Add(Me.Button1)
        Me.panRequest.Controls.Add(Me.Label5)
        Me.panRequest.Controls.Add(Me.Label2)
        Me.panRequest.Controls.Add(Me.Label1)
        Me.panRequest.Controls.Add(Me.txtUserNameForRequest)
        Me.panRequest.Controls.Add(Me.txtUserID)
        Me.panRequest.Location = New System.Drawing.Point(40, 72)
        Me.panRequest.Name = "panRequest"
        Me.panRequest.Size = New System.Drawing.Size(338, 363)
        Me.panRequest.TabIndex = 6
        '
        'chkHideUserID
        '
        Me.chkHideUserID.AutoSize = True
        Me.chkHideUserID.Location = New System.Drawing.Point(261, 129)
        Me.chkHideUserID.Name = "chkHideUserID"
        Me.chkHideUserID.Size = New System.Drawing.Size(15, 14)
        Me.chkHideUserID.TabIndex = 22
        Me.chkHideUserID.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(51, 230)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 25)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Phone Number"
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhoneNumber.Location = New System.Drawing.Point(48, 256)
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.Size = New System.Drawing.Size(235, 35)
        Me.txtPhoneNumber.TabIndex = 20
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(0, 304)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(336, 57)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Request"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(88, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 30)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Request Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(43, 164)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 25)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "User Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(43, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "User ID"
        '
        'txtUserNameForRequest
        '
        Me.txtUserNameForRequest.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserNameForRequest.Location = New System.Drawing.Point(48, 192)
        Me.txtUserNameForRequest.Name = "txtUserNameForRequest"
        Me.txtUserNameForRequest.Size = New System.Drawing.Size(235, 35)
        Me.txtUserNameForRequest.TabIndex = 2
        '
        'txtUserID
        '
        Me.txtUserID.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.Location = New System.Drawing.Point(48, 117)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(235, 35)
        Me.txtUserID.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(431, 33)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GoBackToolStripMenuItem, Me.RequestCodeToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(73, 29)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'GoBackToolStripMenuItem
        '
        Me.GoBackToolStripMenuItem.Image = CType(resources.GetObject("GoBackToolStripMenuItem.Image"), System.Drawing.Image)
        Me.GoBackToolStripMenuItem.Name = "GoBackToolStripMenuItem"
        Me.GoBackToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.GoBackToolStripMenuItem.Size = New System.Drawing.Size(320, 30)
        Me.GoBackToolStripMenuItem.Text = "Go Back"
        '
        'RequestCodeToolStripMenuItem
        '
        Me.RequestCodeToolStripMenuItem.Image = CType(resources.GetObject("RequestCodeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RequestCodeToolStripMenuItem.Name = "RequestCodeToolStripMenuItem"
        Me.RequestCodeToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RequestCodeToolStripMenuItem.Size = New System.Drawing.Size(320, 30)
        Me.RequestCodeToolStripMenuItem.Text = "Request Code"
        '
        'llContactLibrarian
        '
        Me.llContactLibrarian.AutoSize = True
        Me.llContactLibrarian.Location = New System.Drawing.Point(253, 452)
        Me.llContactLibrarian.Name = "llContactLibrarian"
        Me.llContactLibrarian.Size = New System.Drawing.Size(124, 13)
        Me.llContactLibrarian.TabIndex = 5
        Me.llContactLibrarian.TabStop = True
        Me.llContactLibrarian.Text = "Contact Librarian instead"
        '
        'panResetPassword
        '
        Me.panResetPassword.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panResetPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panResetPassword.Controls.Add(Me.chkHidePassword)
        Me.panResetPassword.Controls.Add(Me.Label8)
        Me.panResetPassword.Controls.Add(Me.txtUserNameForReset)
        Me.panResetPassword.Controls.Add(Me.btnChangePassword)
        Me.panResetPassword.Controls.Add(Me.Label6)
        Me.panResetPassword.Controls.Add(Me.Label3)
        Me.panResetPassword.Controls.Add(Me.Label4)
        Me.panResetPassword.Controls.Add(Me.txtNewPassword)
        Me.panResetPassword.Controls.Add(Me.txtCode)
        Me.panResetPassword.Location = New System.Drawing.Point(40, 72)
        Me.panResetPassword.Name = "panResetPassword"
        Me.panResetPassword.Size = New System.Drawing.Size(338, 362)
        Me.panResetPassword.TabIndex = 7
        '
        'chkHidePassword
        '
        Me.chkHidePassword.AutoSize = True
        Me.chkHidePassword.Location = New System.Drawing.Point(259, 253)
        Me.chkHidePassword.Name = "chkHidePassword"
        Me.chkHidePassword.Size = New System.Drawing.Size(15, 14)
        Me.chkHidePassword.TabIndex = 20
        Me.chkHidePassword.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(43, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 25)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "User Name"
        '
        'txtUserNameForReset
        '
        Me.txtUserNameForReset.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserNameForReset.Location = New System.Drawing.Point(48, 92)
        Me.txtUserNameForReset.Name = "txtUserNameForReset"
        Me.txtUserNameForReset.Size = New System.Drawing.Size(235, 35)
        Me.txtUserNameForReset.TabIndex = 18
        '
        'btnChangePassword
        '
        Me.btnChangePassword.BackColor = System.Drawing.Color.Firebrick
        Me.btnChangePassword.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChangePassword.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangePassword.ForeColor = System.Drawing.Color.White
        Me.btnChangePassword.Location = New System.Drawing.Point(0, 303)
        Me.btnChangePassword.Name = "btnChangePassword"
        Me.btnChangePassword.Size = New System.Drawing.Size(336, 57)
        Me.btnChangePassword.TabIndex = 17
        Me.btnChangePassword.Text = "Change Password"
        Me.btnChangePassword.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(87, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 30)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Reset Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(43, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 25)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "New Password"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(43, 139)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 25)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Code"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassword.Location = New System.Drawing.Point(48, 242)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.Size = New System.Drawing.Size(235, 35)
        Me.txtNewPassword.TabIndex = 2
        '
        'txtCode
        '
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(48, 167)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(235, 35)
        Me.txtCode.TabIndex = 1
        '
        'ForgotPasswordRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(431, 502)
        Me.ControlBox = False
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.llContactLibrarian)
        Me.Controls.Add(Me.panRequest)
        Me.Controls.Add(Me.panResetPassword)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ForgotPasswordRequest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.panRequest.ResumeLayout(False)
        Me.panRequest.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.panResetPassword.ResumeLayout(False)
        Me.panResetPassword.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents panRequest As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtUserNameForRequest As TextBox
    Friend WithEvents txtUserID As TextBox
    Friend WithEvents llContactLibrarian As LinkLabel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GoBackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents panResetPassword As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents txtCode As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents btnChangePassword As Button
    Friend WithEvents RequestCodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label7 As Label
    Friend WithEvents txtPhoneNumber As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtUserNameForReset As TextBox
    Friend WithEvents chkHidePassword As CheckBox
    Friend WithEvents chkHideUserID As CheckBox
End Class
