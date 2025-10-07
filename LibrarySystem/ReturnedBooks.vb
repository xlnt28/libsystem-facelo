Imports System.Data.OleDb

Public Class ReturnedBooks

    Private dsReturnLog As DataSet
    Private daReturnLog As OleDbDataAdapter

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
            dgv.DataSource = dsReturnLog.Tables("returnLog")
            dgv.ReadOnly = True
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.ClearSelection()
        Catch ex As Exception
            MsgBox("Failed to load borrow history data. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub SQLQueryForHistoryUser()
        Try
            Dim sql As String = "SELECT [ReturnID], [Borrower Name], [Borrow ID], [Book ID], [Returned Quantity], [Return Date], [Processed By] FROM returnLog WHERE [Borrower Name] = ?"

            daReturnLog = New OleDbDataAdapter(sql, con)
            daReturnLog.SelectCommand.Parameters.AddWithValue("?", XName.Trim())

            If dsReturnLog Is Nothing Then
                dsReturnLog = New DataSet()
            Else
                If dsReturnLog.Tables.Contains("returnLog") Then
                    dsReturnLog.Tables("returnLog").Clear()
                End If
            End If

            daReturnLog.Fill(dsReturnLog, "returnLog")
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

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub ReturnedBooks_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
    End Sub

    Private Sub a_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub
End Class