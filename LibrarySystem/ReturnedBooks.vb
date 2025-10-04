Imports System.Data.OleDb

Public Class ReturnedBooks

    Private Sub ReturnedBooks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        CustomizeDataGridView(dgv)
        loadReturnLogData()
    End Sub

    Private Sub loadReturnLogData()
        Try
            If con.State <> ConnectionState.Open Then OpenDB()
            SQLQueryForHistoryUser()
            dgv.DataSource = Nothing
            dgv.DataSource = dbdsBorrowHistory.Tables("returnLog")
            dgv.ReadOnly = True
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.ClearSelection()
        Catch ex As Exception
            MsgBox("Failed to load borrow history data. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Public Sub SQLQueryForHistoryUser()
        Try
            Dim sql As String = "SELECT * FROM returnLog WHERE [Borrower Name] = ?"

            daBorrowHistory = New OleDbDataAdapter(sql, con)

            daBorrowHistory.SelectCommand.Parameters.AddWithValue("?", XName.Trim())

            If dbdsBorrowHistory Is Nothing Then
                dbdsBorrowHistory = New DataSet()
            Else
                If dbdsBorrowHistory.Tables.Contains("returnLog") Then
                    dbdsBorrowHistory.Tables("returnLog").Clear()
                End If
            End If

            daBorrowHistory.Fill(dbdsBorrowHistory, "returnLog")
        Catch ex As Exception
            MsgBox("Error retrieving return log data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub GoBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Show()
        Me.Close()

    End Sub
End Class