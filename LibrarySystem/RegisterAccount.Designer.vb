<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegisterAccount
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegisterAccount))
        Me.dg = New System.Windows.Forms.DataGridView()
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
        Me.CrystalReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NavigationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFirst = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPrevious = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuNext = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLast = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuClose = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.dg.Location = New System.Drawing.Point(771, 0)
        Me.dg.MultiSelect = False
        Me.dg.Name = "dg"
        Me.dg.ReadOnly = True
        Me.dg.RowHeadersVisible = False
        Me.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dg.Size = New System.Drawing.Size(585, 712)
        Me.dg.TabIndex = 9
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.NavigationToolStripMenuItem, Me.MainToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(771, 40)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
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
        'MainFormToolStripMenuItem
        '
        Me.MainFormToolStripMenuItem.Image = CType(resources.GetObject("MainFormToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MainFormToolStripMenuItem.Name = "MainFormToolStripMenuItem"
        Me.MainFormToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.MainFormToolStripMenuItem.Size = New System.Drawing.Size(305, 36)
        Me.MainFormToolStripMenuItem.Text = "Main Form"
        '
        'MainToolStripMenuItem
        '
        Me.MainToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MainFormToolStripMenuItem})
        Me.MainToolStripMenuItem.Name = "MainToolStripMenuItem"
        Me.MainToolStripMenuItem.Size = New System.Drawing.Size(83, 36)
        Me.MainToolStripMenuItem.Text = "Form"
        '
        'menuClose
        '
        Me.menuClose.Image = CType(resources.GetObject("menuClose.Image"), System.Drawing.Image)
        Me.menuClose.Name = "menuClose"
        Me.menuClose.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.menuClose.Size = New System.Drawing.Size(298, 36)
        Me.menuClose.Text = "Close"
        '
        'RegisterAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1356, 712)
        Me.ControlBox = False
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.dg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "RegisterAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RegisterAccount"
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dg As DataGridView
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents menuNew As ToolStripMenuItem
    Friend WithEvents menuSave As ToolStripMenuItem
    Friend WithEvents menuCancel As ToolStripMenuItem
    Friend WithEvents menuEdit As ToolStripMenuItem
    Friend WithEvents menuDelete As ToolStripMenuItem
    Friend WithEvents menuRefresh As ToolStripMenuItem
    Friend WithEvents menuSearch As ToolStripMenuItem
    Friend WithEvents menuPrint As ToolStripMenuItem
    Friend WithEvents CrystalReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NavigationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents menuFirst As ToolStripMenuItem
    Friend WithEvents menuPrevious As ToolStripMenuItem
    Friend WithEvents menuNext As ToolStripMenuItem
    Friend WithEvents menuLast As ToolStripMenuItem
    Friend WithEvents MainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MainFormToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents menuClose As ToolStripMenuItem
End Class
