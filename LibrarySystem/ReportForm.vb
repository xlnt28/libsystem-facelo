
Public Class ReportForm

    Private Sub ReportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub menuLogout_Click(sender As Object, e As EventArgs) Handles menuLogout.Click
        Me.Close()
    End Sub

    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Load

    End Sub
End Class
