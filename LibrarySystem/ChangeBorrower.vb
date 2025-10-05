Imports System.Data.OleDb

Public Class ChangeBorrower
    Private Sub ChangeBorrower_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        LoadUsers()
    End Sub

    Private Sub LoadUsers()
        Try
            OpenDB()
            SQLQueryFortbluser()
            borrowdgv.DataSource = dbds.Tables("tbluser")
        Catch ex As Exception
            MsgBox("Failed to load users: " & ex.Message)
        End Try
    End Sub

    Private Sub borrowdgv_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles borrowdgv.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.Rows(e.RowIndex)
            Dim userName As String = selectedRow.Cells("User Name").Value.ToString()
            Dim borrowForm As Borrow = TryCast(Application.OpenForms("Borrow"), Borrow)
            If borrowForm IsNot Nothing Then
                borrowForm.txtName.Text = userName
            End If
            Me.Close()
        End If
    End Sub


    Private Sub SearchUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchUserToolStripMenuItem.Click
        Dim searchTerm As String = InputBox("Enter user name to search:", "Search User")
        If Not String.IsNullOrEmpty(searchTerm) Then
            SearchUser(searchTerm)
        End If
    End Sub

    Private Sub SearchUser(ByVal searchTerm As String)
        Try
            OpenDB()
            Dim sql As String = "SELECT [User ID],[User Name],[Position],[Privileges] FROM tbluser WHERE [User Name] LIKE ? ORDER BY [User Name]"
            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")
            Dim da As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            borrowdgv.DataSource = dt
        Catch ex As Exception
            MsgBox("Search failed: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        LoadUsers()
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub borrowdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles borrowdgv.CellContentClick

    End Sub

    Private Sub ChangeBorrower_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
    End Sub

    Private Sub ChangeBorrower_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub
End Class
