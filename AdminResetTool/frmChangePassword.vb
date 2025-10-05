Imports System.Data.OleDb

Public Class frmChangePassword
    Public Property UserID As String
    Public Property UserName As String

    Private Sub frmChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = "Changing password for: " & UserName
        txtNewPassword.Clear()
        txtReEnterNewPassword.Clear()
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Dim newPass As String = txtNewPassword.Text.Trim()
        Dim rePass As String = txtReEnterNewPassword.Text.Trim()

        If newPass = "" Or rePass = "" Then
            MessageBox.Show("Please fill in both password fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If newPass <> rePass Then
            MessageBox.Show("Passwords do not match.", "Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using con As OleDbConnection = AdminDB.GetAdminConnection()
                con.Open()
                Dim cmd As New OleDbCommand("UPDATE tbluser SET [Password]=@pass WHERE [User ID]=@uid", con)
                cmd.Parameters.AddWithValue("@pass", newPass)
                cmd.Parameters.AddWithValue("@uid", UserID)
                Dim rows As Integer = cmd.ExecuteNonQuery()

                If rows > 0 Then
                    MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                Else
                    MessageBox.Show("Failed to update password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
