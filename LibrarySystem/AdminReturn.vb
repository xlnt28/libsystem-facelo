Imports System.Data.OleDb

Public Class AdminReturn


    Private Sub AdminReturn_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        CustomizeDataGridView(dgv)
    End Sub

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        OpenDB()
    End Sub


End Class