<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PenaltyForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PenaltyForm))
        Me.dgvPenalty = New System.Windows.Forms.DataGridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MarkAsPaidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewPaidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewUnpaidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SwitchViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dgvPenalty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvPenalty
        '
        Me.dgvPenalty.AllowUserToAddRows = False
        Me.dgvPenalty.AllowUserToDeleteRows = False
        Me.dgvPenalty.AllowUserToResizeColumns = False
        Me.dgvPenalty.AllowUserToResizeRows = False
        Me.dgvPenalty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPenalty.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvPenalty.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPenalty.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPenalty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPenalty.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPenalty.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPenalty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPenalty.GridColor = System.Drawing.Color.Gainsboro
        Me.dgvPenalty.Location = New System.Drawing.Point(0, 33)
        Me.dgvPenalty.MultiSelect = False
        Me.dgvPenalty.Name = "dgvPenalty"
        Me.dgvPenalty.ReadOnly = True
        Me.dgvPenalty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPenalty.Size = New System.Drawing.Size(1370, 716)
        Me.dgvPenalty.TabIndex = 12
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.SwitchViewToolStripMenuItem, Me.GoBackToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1370, 33)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MarkAsPaidToolStripMenuItem, Me.ViewPaidToolStripMenuItem, Me.ViewUnpaidToolStripMenuItem, Me.ViewAllToolStripMenuItem, Me.SearchToolStripMenuItem, Me.RefreshToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(73, 29)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'MarkAsPaidToolStripMenuItem
        '
        Me.MarkAsPaidToolStripMenuItem.Image = CType(resources.GetObject("MarkAsPaidToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MarkAsPaidToolStripMenuItem.Name = "MarkAsPaidToolStripMenuItem"
        Me.MarkAsPaidToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.MarkAsPaidToolStripMenuItem.Size = New System.Drawing.Size(342, 30)
        Me.MarkAsPaidToolStripMenuItem.Text = "Process Payment"
        '
        'ViewPaidToolStripMenuItem
        '
        Me.ViewPaidToolStripMenuItem.Image = CType(resources.GetObject("ViewPaidToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ViewPaidToolStripMenuItem.Name = "ViewPaidToolStripMenuItem"
        Me.ViewPaidToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ViewPaidToolStripMenuItem.Size = New System.Drawing.Size(342, 30)
        Me.ViewPaidToolStripMenuItem.Text = "View Paid Only"
        '
        'ViewUnpaidToolStripMenuItem
        '
        Me.ViewUnpaidToolStripMenuItem.Image = CType(resources.GetObject("ViewUnpaidToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ViewUnpaidToolStripMenuItem.Name = "ViewUnpaidToolStripMenuItem"
        Me.ViewUnpaidToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.ViewUnpaidToolStripMenuItem.Size = New System.Drawing.Size(342, 30)
        Me.ViewUnpaidToolStripMenuItem.Text = "View Unpaid Only"
        '
        'ViewAllToolStripMenuItem
        '
        Me.ViewAllToolStripMenuItem.Image = CType(resources.GetObject("ViewAllToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ViewAllToolStripMenuItem.Name = "ViewAllToolStripMenuItem"
        Me.ViewAllToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.ViewAllToolStripMenuItem.Size = New System.Drawing.Size(342, 30)
        Me.ViewAllToolStripMenuItem.Text = "View All Penalty"
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Image = CType(resources.GetObject("SearchToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(342, 30)
        Me.SearchToolStripMenuItem.Text = "Search"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = CType(resources.GetObject("RefreshToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(342, 30)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'SwitchViewToolStripMenuItem
        '
        Me.SwitchViewToolStripMenuItem.Image = CType(resources.GetObject("SwitchViewToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SwitchViewToolStripMenuItem.Name = "SwitchViewToolStripMenuItem"
        Me.SwitchViewToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.SwitchViewToolStripMenuItem.Size = New System.Drawing.Size(98, 29)
        Me.SwitchViewToolStripMenuItem.Text = "Viewer"
        '
        'GoBackToolStripMenuItem
        '
        Me.GoBackToolStripMenuItem.Name = "GoBackToolStripMenuItem"
        Me.GoBackToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.GoBackToolStripMenuItem.Size = New System.Drawing.Size(92, 29)
        Me.GoBackToolStripMenuItem.Text = "Go back"
        '
        'PenaltyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 749)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgvPenalty)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "PenaltyForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "n"
        CType(Me.dgvPenalty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvPenalty As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoBackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MarkAsPaidToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewPaidToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewUnpaidToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SwitchViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
