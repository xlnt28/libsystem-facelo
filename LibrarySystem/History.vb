Imports System.Data.OleDb

Public Class History

    Private Sub BorrowHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenDB()
        LoadBorrowData()
        CustomizeDataGridView(borrowdgv)

        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BorrowHistory_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        LoadBorrowData()
        If xpriv = "Admin" Then
            menuCancelRequest.Enabled = False
        Else
            menuCancelRequest.Enabled = True
        End If
    End Sub

    Private Sub LoadBorrowData()
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            SQLQueryForHistoryUser()

            borrowdgv.DataSource = Nothing
            borrowdgv.DataSource = dbdsBorrowHistory.Tables("borrowings")

            borrowdgv.ReadOnly = True
            borrowdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            borrowdgv.ClearSelection()
        Catch ex As Exception
            MsgBox("Failed to load borrow history data. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        If wasOnBorrowOrReturn = True Then
            Borrow.Visible = True
        Else
            frmmain.Show()
        End If
        wasOnBorrowOrReturn = False
        Me.Hide()
    End Sub

    Private Sub BorrowHistory_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

    Private Sub borrowdgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles borrowdgv.CellContentClick

    End Sub

    Public Sub SQLQueryForHistoryUser()
        Try
            Dim sql As String = "SELECT [Borrow ID], [Book ID List], [Borrower Name], [Borrower Position], [Borrower Privileges], " & _
                                "[Copies], [Borrow Date], [Due Date], [Return Date], [Status] " & _
                                "FROM borrowings " & _
                                "WHERE [Borrower Name] = ? "

            If wasOnBorrowOrReturn Then
                sql &= "AND [Status] IN ('Requested', 'Borrowed') "
            Else
                sql &= "AND [Status] IN ('Requested','Borrowed','Completed', 'Cancelled') "
            End If

            sql &= "ORDER BY [Borrow ID] DESC"

            daBorrowHistory = New OleDbDataAdapter(sql, con)
            daBorrowHistory.SelectCommand.Parameters.AddWithValue("?", XName.Trim())

            If dbdsBorrowHistory Is Nothing Then
                dbdsBorrowHistory = New DataSet()
            Else
                If dbdsBorrowHistory.Tables.Contains("borrowings") Then
                    dbdsBorrowHistory.Tables("borrowings").Clear()
                End If
            End If

            daBorrowHistory.Fill(dbdsBorrowHistory, "borrowings")

        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub menuCheckTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCheckTransaction.Click
        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)

            borrowID = selectedRow.Cells("Borrow ID").Value.ToString()
            names = selectedRow.Cells("Borrower Name").Value.ToString()
            position = selectedRow.Cells("Borrower Position").Value.ToString()
            privilege = selectedRow.Cells("Borrower Privileges").Value.ToString()
            borrowDate = selectedRow.Cells("Borrow Date").Value.ToString()
            dueDate = selectedRow.Cells("Due Date").Value.ToString()
            returnDate = If(selectedRow.Cells("Return Date").Value Is DBNull.Value, "", selectedRow.Cells("Return Date").Value.ToString())
            status = selectedRow.Cells("Status").Value.ToString()

            Dim transactionChecker As New TransactionChecker()
            transactionChecker.Show()
        Else
            MsgBox("Please select a transaction to check.", MsgBoxStyle.Exclamation, "Select Transaction")
        End If
    End Sub

    Private Sub borrowdgv_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles borrowdgv.CellFormatting
        If e.ColumnIndex = borrowdgv.Columns("Book ID List").Index AndAlso e.Value IsNot Nothing Then
            Dim bookIDList As String = e.Value.ToString()

            If Not String.IsNullOrEmpty(bookIDList) Then
                Dim bookIDs() As String = bookIDList.Split(","c)
                e.Value = String.Join(vbCrLf, bookIDs)
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub menuCancelRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCancelRequest.Click
        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)
            Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()
            Dim status As String = selectedRow.Cells("Status").Value.ToString()

            If status = "Requested" Then
                Dim confirm = MsgBox("Are you sure you want to cancel this request?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Cancel")
                If confirm = MsgBoxResult.Yes Then
                    Try
                        If con.State <> ConnectionState.Open Then
                            OpenDB()
                        End If

                        Dim sql As String = "UPDATE borrowings SET [Status] = 'Cancelled' WHERE [Borrow ID] = ?"
                        Using cmd As New OleDbCommand(sql, con)
                            cmd.Parameters.AddWithValue("?", borrowID)
                            cmd.ExecuteNonQuery()
                        End Using

                        MsgBox("Request cancelled successfully.", MsgBoxStyle.Information, "Cancelled")
                        LoadBorrowData()
                    Catch ex As Exception
                        MsgBox("Failed to cancel request. " & ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
            Else
                MsgBox("This transaction cannot be cancelled because it is already " & status & ".", MsgBoxStyle.Exclamation, "Not Allowed")
            End If
        Else
            MsgBox("Please select a request to cancel.", MsgBoxStyle.Exclamation, "Select Request")
        End If
    End Sub

End Class