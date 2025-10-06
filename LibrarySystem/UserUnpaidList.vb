Imports System.Data.OleDb

Public Class UserUnpaidList
    Private Sub UserUnpaidList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        CustomizeDataGridView(dgvUserUnpaidList)
        LoadUserUnpaidList()

        MsgBox("Double-click rows to select user to process payment.", MsgBoxStyle.Information,)
    End Sub

    Private Sub LoadUserUnpaidList()
        Try
            OpenDB()

            Dim sql As String = "SELECT DISTINCT t.[Borrower Name], t.[Borrower Position], t.[Borrower Privileges] " &
                               "FROM transactions t " &
                               "INNER JOIN Penalties p ON t.[Borrower Name] = p.[User Name] " &
                               "WHERE t.[Status] = 'Completed' AND p.[Penalty Status] = 'Unpaid' " &
                               "ORDER BY t.[Borrower Name]"

            daUserUnpaidList = New OleDbDataAdapter(sql, con)
            dsUserUnpaidList = New DataSet()
            daUserUnpaidList.Fill(dsUserUnpaidList, "UserUnpaidList")

            dgvUserUnpaidList.DataSource = dsUserUnpaidList.Tables("UserUnpaidList")
            dgvUserUnpaidList.ClearSelection()

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
        Dim userName As String = selectedRow.Cells("Borrower Name").Value.ToString()

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

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked
    End Sub

    Private Sub dgvUserUnpaidList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUserUnpaidList.CellContentClick
    End Sub
End Class