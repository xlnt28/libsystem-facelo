Imports System.Data.OleDb

Public Class ForgotPasswordRequest
    Private isResetPasswordVisible As Boolean = True
    Private generatedResetCode As String = ""

    Private Sub RequestCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequestCodeToolStripMenuItem.Click
        isResetPasswordVisible = Not isResetPasswordVisible

        If isResetPasswordVisible Then
            panResetPassword.Visible = True
            panRequest.Visible = False
            RequestCodeToolStripMenuItem.Text = "Request Code"
        Else
            panResetPassword.Visible = False
            panRequest.Visible = True
            RequestCodeToolStripMenuItem.Text = "Reset Password"
        End If
    End Sub

    Private Sub btnRequestCode_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrEmpty(txtUserID.Text) OrElse
           String.IsNullOrEmpty(txtUserNameForRequest.Text) OrElse
           String.IsNullOrEmpty(txtPhoneNumber.Text) Then
            MsgBox("Please enter User ID, User Name, and Phone Number.")
            Return
        End If

        If Not IsValidPhoneNumber(txtPhoneNumber.Text) Then
            MsgBox("Please enter a valid phone number.")
            Return
        End If

        If Not VerifyUserExists(txtUserID.Text, txtUserNameForRequest.Text) Then
            MsgBox("Invalid User ID or User Name.")
            Return
        End If

        generatedResetCode = GenerateResetCode()

        If SaveResetRequest(txtUserID.Text, txtUserNameForRequest.Text, txtPhoneNumber.Text, generatedResetCode) Then
            MsgBox("Reset request submitted successfully!" & vbCrLf &
                   "Reset code: " & generatedResetCode & vbCrLf &
                   "An administrator will review your request." & vbCrLf &
                   "If approved, the code will be valid for 24 hours.")
            panResetPassword.Visible = False
            panRequest.Visible = True
            isResetPasswordVisible = False
            RequestCodeToolStripMenuItem.Text = "Reset Password"
        Else
            MsgBox("Error generating reset code.")
        End If
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        If String.IsNullOrEmpty(txtCode.Text) OrElse String.IsNullOrEmpty(txtNewPassword.Text) Then
            MsgBox("Please enter reset code and new password.")
            Return
        End If

        If txtNewPassword.Text.Length < 6 Then
            MsgBox("New password must be at least 6 characters long.")
            Return
        End If

        If ValidateResetCode(txtCode.Text, txtUserNameForReset.Text) Then
            If UpdateUserPassword(txtUserNameForReset.Text, txtNewPassword.Text) Then
                MarkResetAsUsed(txtCode.Text, txtUserNameForReset.Text)
                MsgBox("Password changed successfully!")
                Me.Close()
            Else
                MsgBox("Error updating password.")
            End If
        Else
            MsgBox("Invalid or expired reset code.")
        End If
    End Sub

    Private Function GenerateResetCode() As String
        Try
            OpenDB()
            Dim rnd As New Random()
            Dim resetCode As String = ""
            Dim unique As Boolean = False

            Do
                resetCode = rnd.Next(100000, 999999).ToString()
                Dim q As String = "SELECT COUNT(*) FROM PasswordResetRequests WHERE ResetCode = ? AND Status IN ('Pending','Approved')"
                Using cmd As New OleDbCommand(q, con)
                    cmd.Parameters.AddWithValue("@ResetCode", resetCode)
                    Dim c As Integer = CInt(cmd.ExecuteScalar())
                    If c = 0 Then unique = True
                End Using
            Loop Until unique

            Return resetCode
        Catch ex As Exception
            MsgBox("Error generating unique reset code: " & ex.Message)
            Return "000000"
        End Try
    End Function

    Private Function IsValidPhoneNumber(phoneNumber As String) As Boolean
        Dim digits As String = New String(phoneNumber.Where(Function(c) Char.IsDigit(c)).ToArray())
        Return digits.Length >= 10
    End Function

    Private Function VerifyUserExists(userID As String, userName As String) As Boolean
        Try
            OpenDB()
            Dim q As String = "SELECT COUNT(*) FROM tbluser WHERE [User ID] = ? AND [User Name] = ?"
            Using cmd As New OleDbCommand(q, con)
                cmd.Parameters.AddWithValue("@UserID", userID)
                cmd.Parameters.AddWithValue("@UserName", userName)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        Catch ex As Exception
            MsgBox("Error verifying user: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function SaveResetRequest(userID As String, userName As String, phoneNumber As String, resetCode As String) As Boolean
        Try
            OpenDB()
            Dim checkQuery As String = "SELECT COUNT(*) FROM PasswordResetRequests WHERE UserID = ? AND Status = 'Pending'"
            Using checkCmd As New OleDbCommand(checkQuery, con)
                checkCmd.Parameters.AddWithValue("@UserID", userID)
                If CInt(checkCmd.ExecuteScalar()) > 0 Then
                    MsgBox("You already have a pending password reset request. Please wait until it is processed.", vbExclamation)
                    Return False
                End If
            End Using

            Dim insertQ As String = "INSERT INTO PasswordResetRequests (UserID, UserName, PhoneNumber, ResetCode, RequestDate, Status) VALUES (?, ?, ?, ?, ?, ?)"
            Using cmd As New OleDbCommand(insertQ, con)
                cmd.Parameters.AddWithValue("@UserID", userID)
                cmd.Parameters.AddWithValue("@UserName", userName)
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber)
                cmd.Parameters.AddWithValue("@ResetCode", resetCode)
                cmd.Parameters.Add("@RequestDate", OleDbType.Date).Value = Now
                cmd.Parameters.AddWithValue("@Status", "Pending")
                cmd.ExecuteNonQuery()
                Return True
            End Using
        Catch ex As Exception
            MsgBox("Error saving reset request: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function ValidateResetCode(resetCode As String, userName As String) As Boolean
        Try
            OpenDB()
            Dim q As String = "
                SELECT COUNT(*) 
                FROM PasswordResetRequests 
                WHERE ResetCode = ? 
                  AND UserName = ? 
                  AND Status = 'Approved' 
                  AND DateAdd('h', 24, [ProcessedDate]) > ?
            "
            Using cmd As New OleDbCommand(q, con)
                cmd.Parameters.AddWithValue("@ResetCode", resetCode.Trim())
                cmd.Parameters.AddWithValue("@UserName", userName.Trim())
                cmd.Parameters.Add("@Now", OleDbType.Date).Value = Now
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        Catch ex As Exception
            MsgBox("Error validating reset code: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function UpdateUserPassword(userName As String, newPassword As String) As Boolean
        Try
            OpenDB()
            Dim q As String = "UPDATE tbluser SET [Password] = ? WHERE [User Name] = ?"
            Using cmd As New OleDbCommand(q, con)
                cmd.Parameters.AddWithValue("@Password", newPassword)
                cmd.Parameters.AddWithValue("@UserName", userName)
                cmd.ExecuteNonQuery()
                Return True
            End Using
        Catch ex As Exception
            MsgBox("Error updating password: " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub MarkResetAsUsed(resetCode As String, userName As String)
        Try
            OpenDB()
            Dim q As String = "UPDATE PasswordResetRequests SET Status = 'Used', ProcessedDate = ? WHERE ResetCode = ? AND UserName = ?"
            Using cmd As New OleDbCommand(q, con)
                cmd.Parameters.Add("@ProcessedDate", OleDbType.Date).Value = Now
                cmd.Parameters.AddWithValue("@ResetCode", resetCode)
                cmd.Parameters.AddWithValue("@UserName", userName)
                cmd.ExecuteNonQuery()
            End Using
        Catch
        End Try
    End Sub

    Private Sub ForgotPasswordRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        panResetPassword.Visible = True
        panRequest.Visible = False
        isResetPasswordVisible = True
        RequestCodeToolStripMenuItem.Text = "Request Code"
        txtNewPassword.UseSystemPasswordChar = True
        txtUserID.UseSystemPasswordChar = True
        chkHidePassword.Checked = False
        chkHideUserID.Checked = False
    End Sub

    Private Sub llContactLibrarian_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llContactLibrarian.LinkClicked
        MsgBox("Please contact 09610090028 or Email us at Dexterfacelo101306@gmail.com for assistance.")
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoBackToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub chkHidePassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkHidePassword.CheckedChanged
        txtNewPassword.UseSystemPasswordChar = Not chkHidePassword.Checked
    End Sub

    Private Sub chkHideUserID_CheckedChanged(sender As Object, e As EventArgs) Handles chkHideUserID.CheckedChanged
        txtUserID.UseSystemPasswordChar = Not chkHideUserID.Checked
    End Sub
End Class
