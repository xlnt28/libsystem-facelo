
Public Class Main
    Private selectedUserID As String = ""
    Private selectedUserName As String = ""

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
    End Sub

    Private Sub LoadUsers()
        Try
            AdminDB.SQLQueryFortbluser()
            dgvUser.DataSource = AdminDB.dbds.Tables("tbluser")
        Catch ex As Exception
            MsgBox("Failed to load users: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub dgvUser_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUser.CellClick
        If e.RowIndex >= 0 Then
            selectedUserID = dgvUser.Rows(e.RowIndex).Cells("User ID").Value.ToString()
            selectedUserName = dgvUser.Rows(e.RowIndex).Cells("User Name").Value.ToString()
        End If
    End Sub

    Private Sub ResetPasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetPasswordToolStripMenuItem.Click
        If String.IsNullOrEmpty(selectedUserID) Then
            MessageBox.Show("Please select a user first.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim frm As New frmChangePassword
        frm.UserID = selectedUserID
        frm.UserName = selectedUserName
        frm.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

End Class
