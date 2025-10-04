Imports System.Data.OleDb

Public Class Login

    Private loginAttempts As Integer = 0

    Private Sub frmlogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenDB()
        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        txtun.Focus()
        txtpw.UseSystemPasswordChar = True
    End Sub

    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        If MsgBox("This action will terminate the user from using the system. Would you like to proceed?", MsgBoxStyle.YesNo, "Closing...") = MsgBoxResult.Yes Then
            Application.Exit()
        Else
            Me.Focus()
        End If
    End Sub

    Private Sub menuLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuLogin.Click
        Try
            Dim username As String = txtun.Text.Trim()
            Dim password As String = txtpw.Text.Trim()

            If username = "" OrElse password = "" Then
                MsgBox("Please enter both username and password.", MsgBoxStyle.Exclamation, "Missing Fields")
                txtun.Focus()
                Exit Sub
            End If

            Dim sql As String = "SELECT * FROM tbluser WHERE [User Name] = @uname AND [Password] = @pw"
            Dim cmd As New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("@uname", username)
            cmd.Parameters.AddWithValue("@pw", password)

            Dim da As New OleDbDataAdapter(cmd)

            Dim tempDS As New DataSet()
            da.Fill(tempDS, "tbluser")

            If tempDS.Tables("tbluser").Rows.Count > 0 Then
                Dim row As DataRow = tempDS.Tables("tbluser").Rows(0)
                XName = row("User Name").ToString()
                xpost = row("Position").ToString()
                xpriv = row("Privileges").ToString()

                txtun.Clear()
                txtpw.Clear()
                loginAttempts = 0

                frmmain.Show()

                Me.Visible = False
            Else
                MsgBox("The username or password is incorrect. Please try again.", MsgBoxStyle.Exclamation, "Login Failed")

                loginAttempts += 1
                If loginAttempts >= 3 Then
                    MsgBox("Maximum login attempts reached. Closing application.", MsgBoxStyle.Critical, "Login Failed")
                    Application.Exit()
                End If

                txtun.Clear()
                txtpw.Clear()
                txtun.Focus()
            End If

        Catch ex As Exception
            MsgBox("Error during login: " & ex.Message, MsgBoxStyle.Critical, "Login Error")
        End Try
    End Sub


    Private Sub chkShowPassword_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowPassword.CheckedChanged
        If chkShowPassword.Checked Then
            txtpw.UseSystemPasswordChar = False
        Else
            txtpw.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub
End Class