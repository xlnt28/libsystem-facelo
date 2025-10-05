Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class PenaltyForm

    Private isAdmin As Boolean = False
    Private currentViewMode As String = "Your"
    Private isMarkAsPaidMode As Boolean = False
    Private selectedPenalties As New List(Of String)
    Private receiptPanel As Panel
    Private currentSelectedUser As String = ""

    Private Sub PenaltyForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        OpenDB()

        isAdmin = (xpriv = "Admin")

        If isAdmin Then
            currentViewMode = "All"
            SwitchViewToolStripMenuItem.Enabled = True
            SwitchViewToolStripMenuItem.Text = "View Your Transactions"
        Else
            currentViewMode = "Your"
            SwitchViewToolStripMenuItem.Enabled = False
        End If

        MarkAsPaidToolStripMenuItem.Enabled = isAdmin
        ViewPaidToolStripMenuItem.Enabled = True
        ViewUnpaidToolStripMenuItem.Enabled = True
        ViewAllToolStripMenuItem.Enabled = True
        SearchToolStripMenuItem.Enabled = True
        RefreshToolStripMenuItem.Enabled = True

        InitializeReceiptModeControls()

        CustomizeDataGridView(dgvPenalty)
        LoadPenaltyData()
        ToggleViewButtons("All")
    End Sub

    Private Sub InitializeReceiptModeControls()
        receiptPanel = New Panel()
        receiptPanel.Dock = DockStyle.Bottom
        receiptPanel.Height = 80
        receiptPanel.BackColor = Color.LightBlue
        receiptPanel.Visible = False

        Dim lblCurrentUser As New Label()
        lblCurrentUser.Name = "lblCurrentUser"
        lblCurrentUser.Text = "User: None"
        lblCurrentUser.Size = New Size(300, 20)
        lblCurrentUser.Location = New Point(20, 10)
        lblCurrentUser.ForeColor = Color.DarkBlue
        lblCurrentUser.Font = New Font(lblCurrentUser.Font, FontStyle.Bold)

        Dim lblSelectedCount As New Label()
        lblSelectedCount.Name = "lblSelectedCount"
        lblSelectedCount.Text = "Selected: 0 penalties"
        lblSelectedCount.Size = New Size(200, 20)
        lblSelectedCount.Location = New Point(20, 35)
        lblSelectedCount.ForeColor = Color.DarkBlue

        Dim btnSaveReceipt As New Button()
        btnSaveReceipt.Text = "Save Receipt and Print"
        btnSaveReceipt.Size = New Size(150, 30)
        btnSaveReceipt.Location = New Point(350, 10)
        btnSaveReceipt.BackColor = Color.Green
        btnSaveReceipt.ForeColor = Color.White
        AddHandler btnSaveReceipt.Click, AddressOf btnSaveReceipt_Click

        Dim btnCancelReceipt As New Button()
        btnCancelReceipt.Text = "Cancel"
        btnCancelReceipt.Size = New Size(100, 30)
        btnCancelReceipt.Location = New Point(510, 10)
        btnCancelReceipt.BackColor = Color.Red
        btnCancelReceipt.ForeColor = Color.White
        AddHandler btnCancelReceipt.Click, AddressOf btnCancelReceipt_Click

        receiptPanel.Controls.Add(lblCurrentUser)
        receiptPanel.Controls.Add(lblSelectedCount)
        receiptPanel.Controls.Add(btnSaveReceipt)
        receiptPanel.Controls.Add(btnCancelReceipt)

        Me.Controls.Add(receiptPanel)
        receiptPanel.BringToFront()
    End Sub

    Private Sub LoadPenaltyData(Optional ByVal status As String = "", Optional ByVal userName As String = "")
        Try
            Dim sql As String
            Dim da As OleDbDataAdapter

            If isAdmin AndAlso currentViewMode = "All" AndAlso String.IsNullOrEmpty(userName) Then
                If String.IsNullOrEmpty(status) OrElse status = "All" Then
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID], [Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [Book Condition], [Condition Penalty] FROM Penalties ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                Else
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID], [Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [Book Condition], [Condition Penalty] FROM Penalties WHERE [Penalty Status] = ? ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                    da.SelectCommand.Parameters.AddWithValue("?", status)
                End If
            Else
                Dim userFilter As String = If(Not String.IsNullOrEmpty(userName), userName, XName)
                If String.IsNullOrEmpty(status) OrElse status = "All" Then
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID], [Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [Book Condition], [Condition Penalty] FROM Penalties WHERE [User Name] = ? ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                    da.SelectCommand.Parameters.AddWithValue("?", userFilter)
                Else
                    sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID], [Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [Book Condition], [Condition Penalty] FROM Penalties WHERE [User Name] = ? AND [Penalty Status] = ? ORDER BY [PenaltyID] DESC"
                    da = New OleDbDataAdapter(sql, con)
                    da.SelectCommand.Parameters.AddWithValue("?", userFilter)
                    da.SelectCommand.Parameters.AddWithValue("?", status)
                End If
            End If

            Dim ds As New DataSet()
            da.Fill(ds, "Penalties")
            dgvPenalty.DataSource = ds.Tables("Penalties")

            If isMarkAsPaidMode Then
                AddSelectionColumn()
            Else
                RemoveSelectionColumn()
            End If

            UpdateReceiptPanelInfo()
        Catch ex As Exception
            MsgBox("Error loading penalty data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AddSelectionColumn()
        If dgvPenalty.Columns.Contains("Selected") Then
            Return
        End If

        Dim selectionColumn As New DataGridViewTextBoxColumn()
        selectionColumn.Name = "Selected"
        selectionColumn.HeaderText = "Selected"
        selectionColumn.Width = 80
        dgvPenalty.Columns.Insert(0, selectionColumn)

        For Each row As DataGridViewRow In dgvPenalty.Rows
            If Not row.IsNewRow Then
                Dim penaltyID As String = row.Cells("PenaltyID").Value.ToString()
                If selectedPenalties.Contains(penaltyID) Then
                    row.Cells("Selected").Value = "✓"
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                Else
                    row.Cells("Selected").Value = ""
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            End If
        Next

        AddHandler dgvPenalty.CellDoubleClick, AddressOf dgvPenalty_CellDoubleClick
    End Sub

    Private Sub RemoveSelectionColumn()
        If dgvPenalty.Columns.Contains("Selected") Then
            RemoveHandler dgvPenalty.CellDoubleClick, AddressOf dgvPenalty_CellDoubleClick
            dgvPenalty.Columns.Remove("Selected")
        End If
    End Sub

    Private Sub dgvPenalty_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then
            Return
        End If

        If isMarkAsPaidMode Then
            Dim penaltyID As String = dgvPenalty.Rows(e.RowIndex).Cells("PenaltyID").Value.ToString()

            If selectedPenalties.Contains(penaltyID) Then
                selectedPenalties.Remove(penaltyID)
                dgvPenalty.Rows(e.RowIndex).Cells("Selected").Value = ""
                dgvPenalty.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
            Else
                selectedPenalties.Add(penaltyID)
                dgvPenalty.Rows(e.RowIndex).Cells("Selected").Value = "✓"
                dgvPenalty.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
            End If

            UpdateReceiptPanelInfo()
        End If
    End Sub

    Private Sub UpdateReceiptPanelInfo()
        Dim lblCurrentUser = receiptPanel.Controls.Find("lblCurrentUser", True).FirstOrDefault()
        Dim lblSelectedCount = receiptPanel.Controls.Find("lblSelectedCount", True).FirstOrDefault()

        If lblCurrentUser IsNot Nothing Then
            lblCurrentUser.Text = "User: " & If(String.IsNullOrEmpty(currentSelectedUser), "None", currentSelectedUser)
        End If

        If lblSelectedCount IsNot Nothing Then
            lblSelectedCount.Text = "Selected: " & selectedPenalties.Count & " penalties"
        End If
    End Sub

    Private Sub MarkAsPaidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MarkAsPaidToolStripMenuItem.Click
        If Not isAdmin Then
            Return
        End If

        If isMarkAsPaidMode Then
            ExitMarkAsPaidMode()
        Else
            StartMarkAsPaidProcess()
        End If
    End Sub

    Private Sub StartMarkAsPaidProcess()
        Dim userName As String = InputBox("Enter the User Name to process payments for:", "Select User")

        If String.IsNullOrEmpty(userName) Then
            Return
        End If

        If Not VerifyUserHasUnpaidPenalties(userName) Then
            MsgBox("User '" & userName & "' either doesn't exist or has no unpaid penalties.", MsgBoxStyle.Exclamation, "No Unpaid Penalties")
            Return
        End If

        EnterMarkAsPaidMode(userName)
    End Sub

    Private Function VerifyUserHasUnpaidPenalties(ByVal userName As String) As Boolean
        Try
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM Penalties WHERE [User Name] = ? AND [Penalty Status] = 'Unpaid'", con)
                cmd.Parameters.AddWithValue("?", userName)
                Dim count As Integer = CInt(cmd.ExecuteScalar())
                Return count > 0
            End Using
        Catch ex As Exception
            MsgBox("Error verifying user: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub EnterMarkAsPaidMode(ByVal userName As String)
        isMarkAsPaidMode = True
        currentSelectedUser = userName
        selectedPenalties.Clear()

        LoadPenaltyData("Unpaid", userName)

        receiptPanel.Visible = True
        MarkAsPaidToolStripMenuItem.Text = "Exit Mark as Paid Mode"

        DisableOtherMenuItems()

        MsgBox("Mark as Paid Mode Activated for user: " & userName & vbCrLf &
               "Double-click rows to select penalties for payment.", MsgBoxStyle.Information, "Mark as Paid Mode")
    End Sub

    Private Sub ExitMarkAsPaidMode()
        isMarkAsPaidMode = False
        currentSelectedUser = ""
        selectedPenalties.Clear()
        receiptPanel.Visible = False
        MarkAsPaidToolStripMenuItem.Text = "Mark as Paid"

        EnableOtherMenuItems()

        LoadPenaltyData()
    End Sub

    Private Sub DisableOtherMenuItems()
        SearchToolStripMenuItem.Enabled = False
        RefreshToolStripMenuItem.Enabled = False
        ViewPaidToolStripMenuItem.Enabled = False
        ViewUnpaidToolStripMenuItem.Enabled = False
        ViewAllToolStripMenuItem.Enabled = False
        SwitchViewToolStripMenuItem.Enabled = False
    End Sub

    Private Sub EnableOtherMenuItems()
        SearchToolStripMenuItem.Enabled = True
        RefreshToolStripMenuItem.Enabled = True
        ViewPaidToolStripMenuItem.Enabled = True
        ViewUnpaidToolStripMenuItem.Enabled = True
        ViewAllToolStripMenuItem.Enabled = True

        SwitchViewToolStripMenuItem.Enabled = isAdmin
    End Sub

    Private Sub btnSaveReceipt_Click(ByVal sender As Object, ByVal e As EventArgs)
        If selectedPenalties.Count = 0 Then
            MsgBox("Please select at least one penalty to mark as paid.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Dim result As DialogResult = MsgBox("Mark " & selectedPenalties.Count & " penalties as paid for user '" & currentSelectedUser & "'?" &
                                       vbCrLf & "This action cannot be undone.",
                                       MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Payment")

        If result <> DialogResult.Yes Then
            Return
        End If

        Try
            For Each penaltyID In selectedPenalties
                Using cmd As New OleDbCommand("UPDATE Penalties SET [Penalty Status] = 'Paid', [Payment Date] = @PaymentDate WHERE [PenaltyID] = @PenaltyID", con)
                    cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now.Date)
                    cmd.Parameters.AddWithValue("@PenaltyID", penaltyID)
                    cmd.ExecuteNonQuery()
                End Using
            Next

            GenerateReceipt(selectedPenalties)

            MsgBox(selectedPenalties.Count & " penalties marked as paid and receipt saved successfully.", MsgBoxStyle.Information, "Success")

            ExitMarkAsPaidMode()
            LoadPenaltyData()

        Catch ex As Exception
            MsgBox("Error updating penalties: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnCancelReceipt_Click(ByVal sender As Object, ByVal e As EventArgs)
        ExitMarkAsPaidMode()
        MsgBox("Mark as Paid mode cancelled.", MsgBoxStyle.Information, "Cancelled")
    End Sub

    Private Sub GenerateReceipt(ByVal penaltyIDs As List(Of String))
        Try
            If con Is Nothing Then
                MsgBox("Database connection is not initialized.", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
            If con.State <> ConnectionState.Open Then con.Open()

            Dim dt As New DataTable("Penalties")
            dt.Columns.Add("User Name", GetType(String))
            dt.Columns.Add("Borrow ID", GetType(String))
            dt.Columns.Add("Book ID", GetType(String))
            dt.Columns.Add("Quantity", GetType(Integer))
            dt.Columns.Add("Payment Date", GetType(String))
            dt.Columns.Add("Book Condition", GetType(String))
            dt.Columns.Add("Days Late", GetType(Integer))
            dt.Columns.Add("Penalty Amount", GetType(Decimal))

            For Each penaltyID As String In penaltyIDs
                Using cmd As New OleDbCommand("
            SELECT 
                [User Name],
                [Borrow ID],
                [Book ID],
                Quantity,
                [Payment Date],
                [Book Condition],
                [Days Late],
                [Penalty Amount]
            FROM Penalties
            WHERE [PenaltyID] = ?", con)

                    cmd.Parameters.AddWithValue("?", penaltyID)

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            dt.Rows.Add(
                        reader("User Name").ToString(),
                        reader("Borrow ID").ToString(),
                        reader("Book ID").ToString(),
                        If(IsDBNull(reader("Quantity")), 0, Convert.ToInt32(reader("Quantity"))),
                        DateTime.Now.ToString("MM/dd/yyyy"),
                        reader("Book Condition").ToString(),
                        If(IsDBNull(reader("Days Late")), 0, Convert.ToInt32(reader("Days Late"))),
                        If(IsDBNull(reader("Penalty Amount")), 0D, Convert.ToDecimal(reader("Penalty Amount")))
                    )
                        End If
                    End Using
                End Using
            Next

            Dim report As New ReportDocument()
            report.Load(Application.StartupPath & "\Reports\CrystalReport1.rpt")
            report.SetDataSource(dt)

            report.PrintToPrinter(1, False, 0, 0)

            report.Close()
            report.Dispose()

        Catch ex As Exception
            MsgBox("Error generating receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ViewPaidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewPaidToolStripMenuItem.Click
        If isMarkAsPaidMode Then
            MsgBox("Please exit Mark as Paid mode first.", MsgBoxStyle.Exclamation, "Mark as Paid Mode Active")
            Return
        End If

        LoadPenaltyData("Paid")
        ToggleViewButtons("Paid")
    End Sub

    Private Sub ViewUnpaidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewUnpaidToolStripMenuItem.Click
        If isMarkAsPaidMode Then
            MsgBox("Please exit Mark as Paid mode first.", MsgBoxStyle.Exclamation, "Mark as Paid Mode Active")
            Return
        End If

        LoadPenaltyData("Unpaid")
        ToggleViewButtons("Unpaid")
    End Sub

    Private Sub ViewAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewAllToolStripMenuItem.Click
        If isMarkAsPaidMode Then
            MsgBox("Please exit Mark as Paid mode first.", MsgBoxStyle.Exclamation, "Mark as Paid Mode Active")
            Return
        End If

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
        If isMarkAsPaidMode Then
            MsgBox("Please exit Mark as Paid mode first.", MsgBoxStyle.Exclamation, "Mark as Paid Mode Active")
            Return
        End If

        Dim userName As String = InputBox("Enter User Name to search:", "Search by User Name")
        If String.IsNullOrEmpty(userName) Then Return

        Try
            Dim sql As String
            Dim da As OleDbDataAdapter
            Dim ds As New DataSet()

            If isAdmin AndAlso currentViewMode = "All" Then
                sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID], [Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [Book Condition], [Condition Penalty] FROM Penalties WHERE [User Name] LIKE ? ORDER BY [PenaltyID] DESC"
                da = New OleDbDataAdapter(sql, con)
                da.SelectCommand.Parameters.AddWithValue("?", "%" & userName.Trim() & "%")
            Else
                sql = "SELECT [PenaltyID], [Borrow ID], [User Name], [Book ID], [Quantity], [Days Late], [Penalty Amount], [Penalty Status], [Due Date], [Return Date], [Book Condition], [Condition Penalty] FROM Penalties WHERE [User Name] = ? ORDER BY [PenaltyID] DESC"
                da = New OleDbDataAdapter(sql, con)
                da.SelectCommand.Parameters.AddWithValue("?", XName)
            End If

            da.Fill(ds, "Penalties")

            If ds.Tables("Penalties").Rows.Count > 0 Then
                dgvPenalty.DataSource = ds.Tables("Penalties")
                MsgBox("Found " & ds.Tables("Penalties").Rows.Count & " penalties for user '" & userName & "'.", MsgBoxStyle.Information, "Search Results")
            Else
                MsgBox("No penalties found for user '" & userName & "'.", MsgBoxStyle.Information, "No Results")
                LoadPenaltyData()
            End If
        Catch ex As Exception
            MsgBox("Error searching penalties: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RefreshToolStripMenuItem.Click
        If isMarkAsPaidMode Then
            MsgBox("Please exit Mark as Paid mode first.", MsgBoxStyle.Exclamation, "Mark as Paid Mode Active")
            Return
        End If

        LoadPenaltyData()
        ToggleViewButtons("All")
        MsgBox("Data refreshed successfully.", MsgBoxStyle.Information, "Refreshed")
    End Sub

    Private Sub SwitchViewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SwitchViewToolStripMenuItem.Click
        If isMarkAsPaidMode Then
            MsgBox("Please exit Mark as Paid mode first.", MsgBoxStyle.Exclamation, "Mark as Paid Mode Active")
            Return
        End If

        If Not isAdmin Then
            Return
        End If

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
        If isMarkAsPaidMode Then ExitMarkAsPaidMode()
        frmmain.Show()
        Me.Close()
    End Sub

    Private Sub PenaltyForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub dgvPenalty_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvPenalty.SelectionChanged
        If isMarkAsPaidMode Then
            Return
        End If

        If Not isAdmin Then
            MarkAsPaidToolStripMenuItem.Enabled = False
            Return
        End If

        MarkAsPaidToolStripMenuItem.Enabled = True

        If dgvPenalty.SelectedRows.Count = 0 Then
        Else
            Dim status As String = dgvPenalty.SelectedRows(0).Cells("Penalty Status").Value.ToString()
        End If
    End Sub

    Private Sub PenaltyForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If xpriv = "Admin" Then
            SearchToolStripMenuItem.Visible = True
        Else
            SearchToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub dgvPenalty_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPenalty.CellContentClick

    End Sub
End Class