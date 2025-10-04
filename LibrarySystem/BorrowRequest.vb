Imports System.Data.OleDb

Public Class BorrowRequest
    Private Sub BorrowRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenDB()

        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        CustomizeDataGridView(borrowdgv)
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Show()
        Me.Close()
    End Sub

    Private Sub CancelBorrowRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelBorrowRequestToolStripMenuItem.Click

        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)
            Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()
            Dim status As String = selectedRow.Cells("Status").Value.ToString()

            If status = "Requested" Then
                Dim confirm = MsgBox("Are you sure you want to cancel this request?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Cancel")
                If confirm = MsgBoxResult.Yes Then
                    Try
                        If con.State <> ConnectionState.Open Then OpenDB()
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


    Private Sub LoadBorrowData(Optional ByVal searchName As String = "")
        Try
            If con.State <> ConnectionState.Open Then OpenDB()
            SQLQueryForHistoryUser(searchName)
            borrowdgv.DataSource = Nothing
            borrowdgv.DataSource = dbdsBorrowHistory.Tables("borrowings")
            borrowdgv.ReadOnly = True
            borrowdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            borrowdgv.ClearSelection()
        Catch ex As Exception
            MsgBox("Failed to load borrow history data. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Public Sub SQLQueryForHistoryUser(Optional ByVal searchName As String = "")
        Try
            Dim sql As String = "SELECT [Borrow ID], [Book ID],[User ID] ,[Borrower Name], [Borrower Position], [Borrower Privileges], " &
                                "[Copies], [Current Returned], [Request Date] ,[Borrow Date], [Due Date], [Status] FROM borrowings WHERE [Borrower Name] = ? AND [Status] = 'Requested'"


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

    Private Sub borrowdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles borrowdgv.CellContentClick

    End Sub

    Private Sub BorrowRequest_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        LoadBorrowData()
    End Sub
End Class