<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReceiptForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReceiptForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BorrowReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenaltyReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLogout = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportsToolStripMenuItem, Me.menuLogout})
        Me.MenuStrip1.Location = New System.Drawing.Point(9, 9)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(220, 40)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrowReceiptToolStripMenuItem, Me.ReturnReceiptToolStripMenuItem, Me.PenaltyReceiptToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(111, 36)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'BorrowReceiptToolStripMenuItem
        '
        Me.BorrowReceiptToolStripMenuItem.Image = CType(resources.GetObject("BorrowReceiptToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BorrowReceiptToolStripMenuItem.Name = "BorrowReceiptToolStripMenuItem"
        Me.BorrowReceiptToolStripMenuItem.Size = New System.Drawing.Size(303, 36)
        Me.BorrowReceiptToolStripMenuItem.Text = "Get Borrow Receipt"
        '
        'ReturnReceiptToolStripMenuItem
        '
        Me.ReturnReceiptToolStripMenuItem.Image = CType(resources.GetObject("ReturnReceiptToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ReturnReceiptToolStripMenuItem.Name = "ReturnReceiptToolStripMenuItem"
        Me.ReturnReceiptToolStripMenuItem.Size = New System.Drawing.Size(303, 36)
        Me.ReturnReceiptToolStripMenuItem.Text = "Get Return Receipt"
        '
        'PenaltyReceiptToolStripMenuItem
        '
        Me.PenaltyReceiptToolStripMenuItem.Image = CType(resources.GetObject("PenaltyReceiptToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PenaltyReceiptToolStripMenuItem.Name = "PenaltyReceiptToolStripMenuItem"
        Me.PenaltyReceiptToolStripMenuItem.Size = New System.Drawing.Size(303, 36)
        Me.PenaltyReceiptToolStripMenuItem.Text = "Get Penalty Receipt"
        '
        'menuLogout
        '
        Me.menuLogout.Image = CType(resources.GetObject("menuLogout.Image"), System.Drawing.Image)
        Me.menuLogout.Name = "menuLogout"
        Me.menuLogout.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.menuLogout.Size = New System.Drawing.Size(101, 36)
        Me.menuLogout.Text = "Close"
        '
        'ReceiptForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1101, 707)
        Me.ControlBox = False
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ReceiptForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReceiptForm"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BorrowReceiptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReturnReceiptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PenaltyReceiptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents menuLogout As ToolStripMenuItem
End Class
