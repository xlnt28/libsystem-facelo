Imports System.Data.OleDb

Public Class AdminResetTool

    Private Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        Dim adminUserID As String = txtUserID.Text.Trim()
        Dim adminPosition As String = cmbPosition.SelectedItem.ToString()

        Try
            Using con As OleDbConnection = AdminDB.GetAdminConnection()
                con.Open()

                Dim cmd As New OleDbCommand("SELECT COUNT(*) FROM tbluser WHERE [User ID] = ? AND [Position] = ?", con)
                cmd.Parameters.AddWithValue("?", adminUserID)
                cmd.Parameters.AddWithValue("?", adminPosition)

                Dim result As Integer = CInt(cmd.ExecuteScalar())

                If result > 0 Then
                    MessageBox.Show("Verified successfully!")
                    Dim frm As New Main()
                    frm.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("User ID or Position is invalid.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error connecting to database: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()

    End Sub

    Private Sub AdminResetTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
