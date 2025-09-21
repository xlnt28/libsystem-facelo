Imports System.Data.OleDb
Imports System.Drawing

Public Class PenaltyForm

    Private isAdmin As Boolean = False
    Private currentViewMode As String = "Your"

    Private Sub PenaltyForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        OpenDB()

        isAdmin = (xpriv = "Admin")

        MarkAsPaidToolStripMenuItem.Enabled = False
        ViewPaidToolStripMenuItem.Enabled = True
        ViewUnpaidToolStripMenuItem.Enabled = True
        ViewAllToolStripMenuItem.Enabled = True
        SearchToolStripMenuItem.Enabled = True
        RefreshToolStripMenuItem.Enabled = True
        SwitchViewToolStripMenuItem.Enabled = isAdmin
        SwitchViewToolStripMenuItem.Text = "View All Users"

        LoadPenaltyData()
        ToggleViewButtons("All")

        AddHandler dgvPenalty.SelectionChanged, AddressOf dgvPenalty_SelectionChanged
    End Sub

    Private Sub LoadPenaltyData(Optional ByVal status As String = "")
        Try
            Dim sql As String
            Dim da As OleDbDataAdapter

            If isAdmin AndAlso currentViewMode = "All" Then
                If String.IsNullOrEmpty(status) OrElse status = "All" Then
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID List], [Total Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date] " &
                          "FROM Penalties " &
                          "ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                Else
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID List], [Total Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date] " &
                          "FROM Penalties WHERE [Penalty Status] = ? " &
                          "ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                    da.SelectCommand.Parameters.AddWithValue("?", status)
                End If
            Else
                Dim userFilter As String = XName
                If String.IsNullOrEmpty(status) OrElse status = "All" Then
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID List], [Total Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date] " &
                          "FROM Penalties WHERE [User Name] = ? " &
                          "ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                    da.SelectCommand.Parameters.AddWithValue("?", userFilter)
                Else
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID List], [Total Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date] " &
                          "FROM Penalties WHERE [User Name] = ? AND [Penalty Status] = ? " &
                          "ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                    da.SelectCommand.Parameters.AddWithValue("?", userFilter)
                    da.SelectCommand.Parameters.AddWithValue("?", status)
                End If
            End If

            Dim ds As New DataSet()
            da.Fill(ds, "Penalties")
            dgvPenalty.DataSource = ds.Tables("Penalties")
            dgvPenalty.ClearSelection()
        Catch ex As Exception
            MsgBox("Error loading penalty data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub dgvPenalty_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Not isAdmin Then
            MarkAsPaidToolStripMenuItem.Enabled = False
            Return
        End If

        If dgvPenalty.SelectedRows.Count = 0 Then
            MarkAsPaidToolStripMenuItem.Enabled = False
        Else
            Dim status As String = dgvPenalty.SelectedRows(0).Cells("Penalty Status").Value.ToString()
            MarkAsPaidToolStripMenuItem.Enabled = (status <> "Paid")
        End If
    End Sub

    Private Sub MarkAsPaidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MarkAsPaidToolStripMenuItem.Click
        If Not isAdmin Then Return
        If dgvPenalty.SelectedRows.Count = 0 Then
            MsgBox("Please select a penalty record to mark as paid.", MsgBoxStyle.Exclamation, "Select Record")
            Return
        End If

        Try
            Dim penaltyID As String = dgvPenalty.SelectedRows(0).Cells("PenaltyID").Value.ToString()
            cmd = New OleDbCommand("UPDATE Penalties SET [Penalty Status] = 'Paid' WHERE [PenaltyID] = ?", con)
            cmd.Parameters.AddWithValue("?", penaltyID)
            cmd.ExecuteNonQuery()
            MsgBox("Penalty marked as Paid.", MsgBoxStyle.Information, "Success")
            LoadPenaltyData()
        Catch ex As Exception
            MsgBox("Error updating penalty status: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ViewPaidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewPaidToolStripMenuItem.Click
        LoadPenaltyData("Paid")
        ToggleViewButtons("Paid")
    End Sub

    Private Sub ViewUnpaidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewUnpaidToolStripMenuItem.Click
        LoadPenaltyData("Unpaid")
        ToggleViewButtons("Unpaid")
    End Sub

    Private Sub ViewAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewAllToolStripMenuItem.Click
        LoadPenaltyData()
        ToggleViewButtons("All")
    End Sub

    Private Sub ToggleViewButtons(ByVal view As String)
        Select Case view
            Case "Paid"
                ViewPaidToolStripMenuItem.Enabled = False
                ViewUnpaidToolStripMenuItem.Enabled = True
                ViewAllToolStripMenuItem.Enabled = True
            Case "Unpaid"
                ViewPaidToolStripMenuItem.Enabled = True
                ViewUnpaidToolStripMenuItem.Enabled = False
                ViewAllToolStripMenuItem.Enabled = True
            Case "All"
                ViewPaidToolStripMenuItem.Enabled = True
                ViewUnpaidToolStripMenuItem.Enabled = True
                ViewAllToolStripMenuItem.Enabled = False
        End Select
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchToolStripMenuItem.Click
        Dim inputValue As String = InputBox("Enter Penalty ID or Borrow ID to search:", "Search Penalty")
        If String.IsNullOrEmpty(inputValue) Then Return

        Try
            Dim sql As String
            Dim da As OleDbDataAdapter
            Dim ds As New DataSet()

            If isAdmin AndAlso currentViewMode = "All" Then
                sql = "SELECT * FROM Penalties WHERE [PenaltyID] LIKE ? OR [Borrow ID] LIKE ? ORDER BY [PenaltyID] DESC"
                da = New OleDbDataAdapter(sql, con)
                da.SelectCommand.Parameters.AddWithValue("?", "%" & inputValue.Trim() & "%")
                da.SelectCommand.Parameters.AddWithValue("?", "%" & inputValue.Trim() & "%")
            Else
                sql = "SELECT * FROM Penalties WHERE ([PenaltyID] LIKE ? OR [Borrow ID] LIKE ?) AND [User Name] = ? ORDER BY [PenaltyID] DESC"
                da = New OleDbDataAdapter(sql, con)
                da.SelectCommand.Parameters.AddWithValue("?", "%" & inputValue.Trim() & "%")
                da.SelectCommand.Parameters.AddWithValue("?", "%" & inputValue.Trim() & "%")
                da.SelectCommand.Parameters.AddWithValue("?", XName)
            End If

            da.Fill(ds, "Penalties")

            If ds.Tables("Penalties").Rows.Count > 0 Then
                dgvPenalty.DataSource = ds.Tables("Penalties")
                dgvPenalty.ClearSelection()
            Else
                MsgBox("No penalties found for '" & inputValue & "'.", MsgBoxStyle.Information, "No Results")
            End If
        Catch ex As Exception
            MsgBox("Error searching penalties: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RefreshToolStripMenuItem.Click
        LoadPenaltyData()
        ToggleViewButtons("All")
        MsgBox("Data refreshed successfully.", MsgBoxStyle.Information, "Refreshed")
    End Sub

    Private Sub SwitchViewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SwitchViewToolStripMenuItem.Click
        If currentViewMode = "Your" Then
            currentViewMode = "All"
            SwitchViewToolStripMenuItem.Text = "View Your Transactions"
        Else
            currentViewMode = "Your"
            SwitchViewToolStripMenuItem.Text = "View All Users"
        End If

        LoadPenaltyData()
        ToggleViewButtons("All")
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Show()
        Me.Close()
    End Sub

    Private Sub PenaltyForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub dgvPenalty_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPenalty.CellContentClick

    End Sub
End Class
