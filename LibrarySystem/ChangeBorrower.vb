Imports System.Data.OleDb

Public Class ChangeBorrower
    Private userDataset As New DataSet()
    Private userAdapter As OleDbDataAdapter

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
            borrowdgv.DataSource = userDataset.Tables("tbluser")

            If borrowdgv.Columns.Count > 0 Then
                borrowdgv.Columns("User Name").Width = 200
                borrowdgv.Columns("Position").Width = 150
                borrowdgv.Columns("Privileges").Width = 100
            End If

        Catch ex As Exception
            MsgBox("Failed to load users: " & ex.Message)
        End Try
    End Sub

    Public Sub SQLQueryFortbluser()
        Try
            If userDataset Is Nothing Then
                userDataset = New DataSet()
            Else
                If userDataset.Tables.Contains("tbluser") Then
                    userDataset.Tables("tbluser").Clear()
                End If
            End If

            Dim sql As String = "SELECT [User ID], [User Name], [Position], [Privileges] FROM tbluser ORDER BY [User Name]"
            userAdapter = New OleDbDataAdapter(sql, con)
            userAdapter.Fill(userDataset, "tbluser")

        Catch ex As Exception
            MsgBox("Error loading users: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub


    Private Function FindBorrowForm() As Borrow
        Try
            For Each form As Form In Application.OpenForms
                If TypeOf form Is Borrow Then
                    Return DirectCast(form, Borrow)
                End If
            Next

            If Me.Owner IsNot Nothing AndAlso TypeOf Me.Owner Is Borrow Then
                Return DirectCast(Me.Owner, Borrow)
            End If

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub borrowdgv_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles borrowdgv.CellDoubleClick
        If e.RowIndex >= 0 Then
            Try
                Dim selectedRow As DataGridViewRow = borrowdgv.Rows(e.RowIndex)
                Dim userName As String = selectedRow.Cells("User Name").Value.ToString()

                Dim borrowForm As Borrow = FindBorrowForm()

                If borrowForm IsNot Nothing Then
                    borrowForm.txtName.Text = userName
                    Me.Close()
                Else
                    MsgBox("Could not find the Borrow form. Please make sure it's open.", MsgBoxStyle.Exclamation)
                End If
            Catch ex As Exception
                MsgBox("Error selecting user: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Sub SearchUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchUserToolStripMenuItem.Click
        Dim searchTerm As String = InputBox("Enter user name to search:", "Search User")
        If Not String.IsNullOrEmpty(searchTerm) Then
            SearchUser(searchTerm)
        ElseIf searchTerm IsNot Nothing Then
            MsgBox("Search cancelled.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SearchUser(ByVal searchTerm As String)
        Try
            OpenDB()
            Dim sql As String = "SELECT [User ID],[User Name],[Position],[Privileges] FROM tbluser WHERE [User Name] LIKE ? ORDER BY [User Name]"
            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")
            Dim searchAdapter As New OleDbDataAdapter(cmd)
            Dim searchDataset As New DataSet()
            searchAdapter.Fill(searchDataset, "searchResults")
            borrowdgv.DataSource = searchDataset.Tables("searchResults")

            If searchDataset.Tables("searchResults").Rows.Count = 0 Then
                MsgBox("No users found matching: " & searchTerm, MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Search failed: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        LoadUsers()
        MsgBox("User list refreshed.", MsgBoxStyle.Information)
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ChangeBorrower_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
    End Sub

    Private Sub PickBorrowerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PickBorrowerToolStripMenuItem.Click
        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)
            Dim userName As String = selectedRow.Cells("User Name").Value.ToString()
            Dim userPosition As String = If(selectedRow.Cells("Position").Value IsNot Nothing, selectedRow.Cells("Position").Value.ToString(), "N/A")
            Dim userPrivileges As String = If(selectedRow.Cells("Privileges").Value IsNot Nothing, selectedRow.Cells("Privileges").Value.ToString(), "N/A")

            Dim userInfo As String = $"User Information:" & vbCrLf &
                                   $"Name: {userName}" & vbCrLf &
                                   $"Position: {userPosition}" & vbCrLf &
                                   $"Privileges: {userPrivileges}"

            Dim result As DialogResult = MessageBox.Show(userInfo & vbCrLf & vbCrLf & "Use this user as borrower?",
                                                       "Confirm Borrower Selection",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Dim borrowForm As Borrow = FindBorrowForm()
                If borrowForm IsNot Nothing Then
                    borrowForm.txtName.Text = userName
                    Me.Close()
                Else
                    MsgBox("Could not find the Borrow form.", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox("Please select a user first by clicking on a row.", MsgBoxStyle.Information, "Select User")
        End If
    End Sub

    Private Sub FilterByPrivilegeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilterByPrivilegeToolStripMenuItem.Click
        Dim privilege As String = InputBox("Enter privilege to filter by (Admin/User) or leave empty to show all:", "Filter by Privilege")

        If privilege Is Nothing Then
            Return
        End If

        Try
            OpenDB()
            Dim sql As String = "SELECT [User ID], [User Name], [Position], [Privileges] FROM tbluser"

            If Not String.IsNullOrEmpty(privilege) Then
                sql &= " WHERE [Privileges] = ? ORDER BY [User Name]"
                cmd = New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("?", privilege.Trim())
            Else
                sql &= " ORDER BY [User Name]"
                cmd = New OleDbCommand(sql, con)
            End If

            Dim filterAdapter As New OleDbDataAdapter(cmd)
            Dim filterDataset As New DataSet()
            filterAdapter.Fill(filterDataset, "filteredUsers")
            borrowdgv.DataSource = filterDataset.Tables("filteredUsers")

            Dim message As String = filterDataset.Tables("filteredUsers").Rows.Count.ToString() & " user(s) found"
            If Not String.IsNullOrEmpty(privilege) Then
                message &= " with privilege: " & privilege
            End If
            MsgBox(message, MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox("Filter failed: " & ex.Message, MsgBoxStyle.Critical)
            LoadUsers()
        End Try
    End Sub

End Class