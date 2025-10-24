Imports System.Data.OleDb

Public Class UserUnpaidList
    Private Sub UserUnpaidList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        CustomizeDataGridView(dgvUserUnpaidList)
        LoadUserUnpaidList()
    End Sub
    Private Sub LoadUserUnpaidList()
        Try
            OpenDB()

            Dim sql As String = "SELECT DISTINCT [User Name] " &
                           "FROM Penalties " &
                           "WHERE [Penalty Status] = 'Unpaid' " &
                           "ORDER BY [User Name]"


            daUserUnpaidList = New OleDbDataAdapter(sql, con)
            dsUserUnpaidList = New DataSet()
            daUserUnpaidList.Fill(dsUserUnpaidList, "UserUnpaidList")

            dgvUserUnpaidList.DataSource = dsUserUnpaidList.Tables("UserUnpaidList")

            If dgvUserUnpaidList.Columns.Count > 0 Then
                dgvUserUnpaidList.Columns(0).HeaderText = "User Name"
            End If

            dgvUserUnpaidList.ClearSelection()

            If dsUserUnpaidList.Tables("UserUnpaidList").Rows.Count = 0 Then
                MsgBox("No users with unpaid penalties found.", MsgBoxStyle.Information, "No Data")
            End If

        Catch ex As Exception
            MsgBox("Error loading user list: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub PickUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PickUserToolStripMenuItem.Click
        SelectUser()
    End Sub

    Private Sub SelectUser()
        If dgvUserUnpaidList.SelectedRows.Count = 0 Then
            MsgBox("Please select a user first.", MsgBoxStyle.Exclamation, "Select User")
            Return
        End If

        Dim selectedRow As DataGridViewRow = dgvUserUnpaidList.SelectedRows(0)
        Dim userName As String = selectedRow.Cells("User Name").Value.ToString()

        Dim penaltyForm As PenaltyForm = CType(Application.OpenForms("PenaltyForm"), PenaltyForm)
        If penaltyForm IsNot Nothing Then
            penaltyForm.StartMarkAsPaidWithUser(userName)
        End If

        Me.Close()
    End Sub

    Private Sub dgvUserUnpaidList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUserUnpaidList.CellDoubleClick
        If e.RowIndex >= 0 Then
            SelectUser()
        End If
    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub SearchUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchUserToolStripMenuItem.Click
        Dim searchTerm As String = InputBox("Enter user name to search:", "Search User")

        If String.IsNullOrEmpty(searchTerm) Then
            Exit Sub
        End If

        For Each row As DataGridViewRow In dgvUserUnpaidList.Rows
            If row.Cells("Borrower Name").Value.ToString().ToLower().Contains(searchTerm.ToLower()) Then
                dgvUserUnpaidList.ClearSelection()
                row.Selected = True
                dgvUserUnpaidList.FirstDisplayedScrollingRowIndex = row.Index
                Exit Sub
            End If
        Next

        MsgBox("User '" & searchTerm & "' not found.", MsgBoxStyle.Information, "Search Result")
    End Sub
End Class