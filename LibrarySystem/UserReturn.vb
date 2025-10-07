Imports System.Data.OleDb

Public Class UserReturn

    Private Sub UserReturn_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        CustomizeDataGridView(dg)
    End Sub

    Private Sub UserReturn_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
        LoadBorrowedItems()
    End Sub

    Private Sub LoadBorrowedItems()
        Try
            SQLQueryForBorrowedItemss()

            If dsUserReturn IsNot Nothing AndAlso dsUserReturn.Tables.Contains("transactions") AndAlso dsUserReturn.Tables("transactions").Rows.Count > 0 Then
                dg.DataSource = dsUserReturn.Tables("transactions")
                dg.ClearSelection()
            Else
                dg.DataSource = Nothing
            End If

        Catch ex As Exception
            MsgBox("Error loading items: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub SQLQueryForBorrowedItemss()
        Try
            Dim sql As String = "SELECT [Transaction ID] ,[Borrow ID], [Book ID List], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copy List], [Borrow Date], [Due Date], [Status], [Has Requested Return] " &
                                "FROM transactions " &
                                "WHERE [Borrower Name] = ? " &
                                "AND [Status] = 'Borrowed' " &
                                "ORDER BY [Transaction ID] DESC"

            daUserReturn = New OleDbDataAdapter(sql, con)
            daUserReturn.SelectCommand.Parameters.AddWithValue("?", XName.Trim())

            If dsUserReturn Is Nothing Then
                dsUserReturn = New DataSet()
            Else
                dsUserReturn.Tables.Clear()
            End If

            daUserReturn.Fill(dsUserReturn, "transactions")

        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        frmmain.Visible = True
        Me.Close()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadBorrowedItems()
    End Sub

    Private Sub a_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

    Private Sub RequestReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestReturnToolStripMenuItem.Click
        If dg.SelectedRows.Count = 0 Then
            MsgBox("Please select a record first.", MsgBoxStyle.Exclamation, "No Selection")
            Exit Sub
        End If

        Dim borrowID As String = dg.SelectedRows(0).Cells("Borrow ID").Value.ToString()

        Try
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If

            Dim sql As String = "UPDATE transactions SET [Has Requested Return] = 'Yes' WHERE [Transaction ID] = ?"
            Using cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Return request has been submitted successfully.", MsgBoxStyle.Information, "Success")
            LoadBorrowedItems()

        Catch ex As Exception
            MsgBox("Error requesting return: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    End Sub

    Private Sub CancelReturnRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelReturnRequestToolStripMenuItem.Click
        If dg.SelectedRows.Count = 0 Then
            MsgBox("Please select a record first.", MsgBoxStyle.Exclamation, "No Selection")
            Exit Sub
        End If

        Dim borrowID As String = dg.SelectedRows(0).Cells("Borrow ID").Value.ToString()

        Try
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If

            Dim sql As String = "UPDATE transactions SET [Has Requested Return] = 'No' WHERE [Transaction ID] = ?"
            Using cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Return request has been canceled successfully.", MsgBoxStyle.Information, "Success")
            LoadBorrowedItems()

        Catch ex As Exception
            MsgBox("Error canceling return request: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub GoBackToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles GoBackToolStripMenuItem.Click
        Me.Close()
        frmmain.Show()

    End Sub

    Private Sub ViewTransactionDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewTransactionDetailToolStripMenuItem.Click
        If dg.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dg.SelectedRows(0)

            selectedBorrowID = selectedRow.Cells("Borrow ID").Value.ToString()
            selectedUserID = selectedRow.Cells("User ID").Value.ToString()
            selectedBorrowerName = selectedRow.Cells("Borrower Name").Value.ToString()
            selectedBorrowerPosition = selectedRow.Cells("Borrower Position").Value.ToString()
            selectedBorrowerPrivilege = selectedRow.Cells("Borrower Privileges").Value.ToString()
            selectedBorrowDate = selectedRow.Cells("Borrow Date").Value.ToString()
            selectedDueDate = selectedRow.Cells("Due Date").Value.ToString()
            selectedStatus = selectedRow.Cells("Status").Value.ToString()
            selectedBookIDList = selectedRow.Cells("Book ID List").Value.ToString()
            selectedCopyList = selectedRow.Cells("Copy List").Value.ToString()

            Dim viewer As New TransactionViewer()
            viewer.Text = "Transaction Details - Borrow ID: " & selectedBorrowID
            viewer.Show()
        Else
            MsgBox("Please select a transaction to view details.", MsgBoxStyle.Exclamation, "Select Transaction")
        End If
    End Sub
End Class