Imports System.Data.OleDb

Public Class UserSearchForm
    Public SelectedUserID As String = ""
    Public SelectedUserName As String = ""

    Private Sub UserSearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "Search User"
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchUsers(txtSearch.Text.Trim())
    End Sub

    Private Sub SearchUsers(searchTerm As String)
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim sql As String = "SELECT [User ID], [User Name], [Position], [Privileges] FROM tbluser " &
                              "WHERE [User Name] LIKE ? OR [User ID] LIKE ?"

            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")

            Dim dt As New DataTable()
            Dim da As New OleDbDataAdapter(cmd)
            da.Fill(dt)

            dgvUsers.DataSource = dt

            If dt.Rows.Count = 0 Then
                MsgBox("No users found.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Search failed: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub dgvUsers_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellDoubleClick
        If e.RowIndex >= 0 Then
            SelectUser()
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If dgvUsers.SelectedRows.Count > 0 Then
            SelectUser()
        Else
            MsgBox("Please select a user.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub SelectUser()
        If dgvUsers.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = dgvUsers.SelectedRows(0)
            SelectedUserID = row.Cells("User ID").Value.ToString()
            SelectedUserName = row.Cells("User Name").Value.ToString()
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class