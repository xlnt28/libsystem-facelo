Imports System.Data.OleDb

Public Class ChangePassword
    Private Sub ChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        txtCurrentPassword.UseSystemPasswordChar = True
        txtNewPassword.UseSystemPasswordChar = True
        txtConfirmNewPassword.UseSystemPasswordChar = True

        txtCurrentPassword.Clear()
        txtNewPassword.Clear()
        txtConfirmNewPassword.Clear()

        txtCurrentPassword.Focus()
    End Sub

    Private Sub chkReEnterCurrentPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkReEnterCurrentPassword.CheckedChanged
        txtCurrentPassword.UseSystemPasswordChar = Not chkReEnterCurrentPassword.Checked
    End Sub

    Private Sub chkReEnterPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkReEnterPassword.CheckedChanged
        txtNewPassword.UseSystemPasswordChar = Not chkReEnterPassword.Checked
    End Sub

    Private Sub chkConfirmNewPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkConfirmNewPassword.CheckedChanged
        txtConfirmNewPassword.UseSystemPasswordChar = Not chkConfirmNewPassword.Checked
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        If String.IsNullOrWhiteSpace(txtCurrentPassword.Text) Then
            MsgBox("Please enter your current password.", MsgBoxStyle.Exclamation)
            txtCurrentPassword.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtNewPassword.Text) Then
            MsgBox("Please enter your new password.", MsgBoxStyle.Exclamation)
            txtNewPassword.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtConfirmNewPassword.Text) Then
            MsgBox("Please confirm your new password.", MsgBoxStyle.Exclamation)
            txtConfirmNewPassword.Focus()
            Return
        End If

        If txtNewPassword.Text <> txtConfirmNewPassword.Text Then
            MsgBox("New password and confirmation do not match.", MsgBoxStyle.Exclamation)
            txtNewPassword.Clear()
            txtConfirmNewPassword.Clear()
            txtNewPassword.Focus()
            Return
        End If

        If txtNewPassword.Text = txtCurrentPassword.Text Then
            MsgBox("New password cannot be the same as current password.", MsgBoxStyle.Exclamation)
            txtNewPassword.Clear()
            txtConfirmNewPassword.Clear()
            txtNewPassword.Focus()
            Return
        End If

        Try
            OpenDB()

            Dim verifyQuery As String = "SELECT [Password] FROM tbluser WHERE [User Name] = ?"
            cmd = New OleDbCommand(verifyQuery, con)
            cmd.Parameters.AddWithValue("?", XName)

            Dim currentPassword As String = cmd.ExecuteScalar()?.ToString()

            If currentPassword <> txtCurrentPassword.Text Then
                MsgBox("Current password is incorrect.", MsgBoxStyle.Exclamation)
                txtCurrentPassword.Clear()
                txtCurrentPassword.Focus()
                Return
            End If

            Dim updateQuery As String = "UPDATE tbluser SET [Password] = ? WHERE [User Name] = ?"
            cmd = New OleDbCommand(updateQuery, con)
            cmd.Parameters.AddWithValue("?", txtNewPassword.Text)
            cmd.Parameters.AddWithValue("?", XName)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MsgBox("Password changed successfully!", MsgBoxStyle.Information)
                Me.Close()
            Else
                MsgBox("Failed to change password. Please try again.", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox("Error changing password: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseDB()
        End Try
    End Sub


    Private Sub txtConfirmNewPassword_TextChanged(sender As Object, e As EventArgs) Handles txtConfirmNewPassword.TextChanged
        If txtNewPassword.Text.Length > 0 AndAlso txtConfirmNewPassword.Text.Length > 0 Then
            If txtNewPassword.Text = txtConfirmNewPassword.Text Then
            Else
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles GoBackToolStripMenuItem.Click
        Me.Close()
    End Sub


End Class