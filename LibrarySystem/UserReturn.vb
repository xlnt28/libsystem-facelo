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
            Dim sql As String = "SELECT [Borrow ID], [Book ID], [User ID],[Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Borrow Date], [Due Date], [Status], [Has Requested Return]" &
                                "FROM borrowings " &
                                "WHERE [Borrower Name] = ? " &
                                "AND ([Status] = 'Borrowed') " &
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

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadReturnedAndLostItems()
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
            OpenDB()
            Dim sql As String = "UPDATE borrowings SET [Has Requested Return] = 'Yes' WHERE [Borrow ID] = ?"
            Using cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Return request has been submitted successfully.", MsgBoxStyle.Information, "Success")

            LoadReturnedAndLostItems()

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
            OpenDB()
            Dim sql As String = "UPDATE borrowings SET [Has Requested Return] = 'No' WHERE [Borrow ID] = ?"
            Using cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Return request has been canceled successfully.", MsgBoxStyle.Information, "Success")

            LoadReturnedAndLostItems()

        Catch ex As Exception
            MsgBox("Error canceling return request: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


End Class
