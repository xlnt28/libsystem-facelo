Imports System.Data.OleDb

Public Class UserReturn



    Private Sub UserReturn_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        CustomizeDataGridView(dg)
    End Sub

    Private Sub UserReturn_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        LoadReturnedAndLostItems()
    End Sub

    Private Sub LoadReturnedAndLostItems()
        Try
            SQLQueryForReturningAndLost()

            If dsUserReturn IsNot Nothing AndAlso dsUserReturn.Tables.Contains("borrowings") AndAlso dsUserReturn.Tables("borrowings").Rows.Count > 0 Then
                dg.DataSource = dsUserReturn.Tables("borrowings")
                dg.ClearSelection()
            End If

        Catch ex As Exception
            MsgBox("Error loading items: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub



    Public Sub SQLQueryForReturningAndLost()
        Try
            Dim sql As String = "SELECT [Borrow ID], [Book ID List], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Borrow Date], [Due Date], [Return Date], [Status] " & _
                                "FROM borrowings " & _
                                "WHERE [Borrower Name] = ? " & _
                                "AND ([Status] = 'Completed' OR [Status] = 'Lost') " & _
                                "ORDER BY [Borrow ID] DESC"

            daUserReturn = New OleDbDataAdapter(sql, con)
            daUserReturn.SelectCommand.Parameters.AddWithValue("?", XName.Trim())

            If dsUserReturn Is Nothing Then
                dsUserReturn = New DataSet()
            Else
                If dsUserReturn.Tables.Contains("borrowings") Then dsUserReturn.Tables("borrowings").Clear()
            End If

            daUserReturn.Fill(dsUserReturn, "borrowings")

        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Visible = True
        Me.Close()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RefreshToolStripMenuItem.Click
        LoadReturnedAndLostItems()
    End Sub

    Private Sub a_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub
End Class
