Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class BorrowPendingRequest
    Private selectedRequests As New List(Of String)()
    Private isApproveMode As Boolean = False
    Private currentSelectedUser As String = ""
    Private currentSelectedUserID As String = ""
    Private approvePanel As Panel

    Private Sub BorrowPendingRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
            CenterToScreen()

            If dg IsNot Nothing Then
                dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                dg.MultiSelect = True
                CustomizeDataGridView(dg)
            Else
                MsgBox("DataGridView 'dg' is not initialized.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            InitializeApproveModeControls()
            OpenDB()
            LoadPendingRequests()

        Catch ex As Exception
            MsgBox("Error initializing form: " & ex.Message, MsgBoxStyle.Critical, "Initialization Error")
        End Try
    End Sub

    Private Sub InitializeApproveModeControls()
        Try
            approvePanel = New Panel()
            approvePanel.Dock = DockStyle.Bottom
            approvePanel.Height = 80
            approvePanel.BackColor = Color.FromArgb(230, 240, 255)
            approvePanel.Visible = False

            Dim lblCurrentUser As New Label()
            lblCurrentUser.Name = "lblCurrentUser"
            lblCurrentUser.Text = "User: None"
            lblCurrentUser.Size = New Size(300, 20)
            lblCurrentUser.Location = New Point(20, 10)
            lblCurrentUser.ForeColor = Color.FromArgb(0, 51, 102)
            lblCurrentUser.Font = New Font(lblCurrentUser.Font, FontStyle.Bold)

            Dim lblSelectedCount As New Label()
            lblSelectedCount.Name = "lblSelectedCount"
            lblSelectedCount.Text = "Selected: 0 requests"
            lblSelectedCount.Size = New Size(200, 20)
            lblSelectedCount.Location = New Point(20, 35)
            lblSelectedCount.ForeColor = Color.FromArgb(0, 51, 102)

            Dim btnApproveSelected As New Button()
            btnApproveSelected.Text = "Approve Selected"
            btnApproveSelected.Size = New Size(150, 30)
            btnApproveSelected.Location = New Point(350, 10)
            btnApproveSelected.BackColor = Color.FromArgb(0, 102, 204)
            btnApproveSelected.ForeColor = Color.White
            AddHandler btnApproveSelected.Click, AddressOf btnApproveSelected_Click

            Dim btnCancelApprove As New Button()
            btnCancelApprove.Text = "Cancel"
            btnCancelApprove.Size = New Size(100, 30)
            btnCancelApprove.Location = New Point(510, 10)
            btnCancelApprove.BackColor = Color.FromArgb(192, 57, 43)
            btnCancelApprove.ForeColor = Color.White
            AddHandler btnCancelApprove.Click, AddressOf btnCancelApprove_Click

            approvePanel.Controls.Add(lblCurrentUser)
            approvePanel.Controls.Add(lblSelectedCount)
            approvePanel.Controls.Add(btnApproveSelected)
            approvePanel.Controls.Add(btnCancelApprove)

            Me.Controls.Add(approvePanel)
            approvePanel.BringToFront()

        Catch ex As Exception
            MsgBox("Error initializing approve mode controls: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub adminHistory_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        LoadPendingRequests()
    End Sub

    Private Sub LoadPendingRequests()
        Try
            If dg Is Nothing Then
                MsgBox("DataGridView is not initialized.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            If isApproveMode AndAlso Not String.IsNullOrEmpty(currentSelectedUserID) Then
                SQLQueryForBorrowPendingRequestByUser(currentSelectedUserID)
            Else
                SQLQueryForBorrowPendingRequest()
            End If

            If admindbds IsNot Nothing AndAlso admindbds.Tables.Contains("borrowings") Then
                dg.DataSource = admindbds.Tables("borrowings")
            Else
                dg.DataSource = Nothing
            End If

            dg.ReadOnly = True
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dg.MultiSelect = True
            dg.ClearSelection()

            RemoveHandler dg.CellFormatting, AddressOf dg_CellFormatting
            RemoveHandler dg.SelectionChanged, AddressOf dg_SelectionChanged
            RemoveHandler dg.CellDoubleClick, AddressOf dg_CellDoubleClick

            AddHandler dg.CellFormatting, AddressOf dg_CellFormatting
            AddHandler dg.SelectionChanged, AddressOf dg_SelectionChanged

            If isApproveMode Then
                AddSelectionColumn()
            Else
                RemoveSelectionColumn()
            End If

            UpdateApprovePanelInfo()
            UpdateButtonStates()

        Catch ex As Exception
            MsgBox("Error loading pending requests: " & ex.Message, MsgBoxStyle.Critical, "Load Error")
        End Try
    End Sub

    Private Sub dg_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Not isApproveMode Then
            selectedRequests.Clear()
            For Each row As DataGridViewRow In dg.SelectedRows
                If row.Cells("Borrow ID").Value IsNot Nothing Then
                    selectedRequests.Add(row.Cells("Borrow ID").Value.ToString())
                End If
            Next
            UpdateButtonStates()
        End If
    End Sub

    Private Sub UpdateButtonStates()
        If ApproveToolStripMenuItem IsNot Nothing Then
            ApproveToolStripMenuItem.Enabled = (selectedRequests.Count > 0 AndAlso Not isApproveMode)
        End If
        If RejectToolStripMenuItem IsNot Nothing Then
            RejectToolStripMenuItem.Enabled = (selectedRequests.Count > 0 AndAlso Not isApproveMode)
        End If

        If SearchToolStripMenuItem IsNot Nothing Then
            SearchToolStripMenuItem.Enabled = Not isApproveMode
        End If

    End Sub

    Private Sub AddSelectionColumn()
        If dg Is Nothing Then Return

        Try
            If dg.Columns.Contains("Selected") Then
                dg.Columns.Remove("Selected")
            End If

            Dim selectionColumn As New DataGridViewTextBoxColumn()
            selectionColumn.Name = "Selected"
            selectionColumn.HeaderText = "Selected"
            selectionColumn.Width = 80
            dg.Columns.Insert(0, selectionColumn)

            For Each row As DataGridViewRow In dg.Rows
                If Not row.IsNewRow AndAlso row.Cells("Borrow ID").Value IsNot Nothing Then
                    Dim borrowID As String = row.Cells("Borrow ID").Value.ToString()
                    Dim isSelected As Boolean = selectedRequests.Contains(borrowID)
                    If isSelected Then
                        row.Cells("Selected").Value = "✓"
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                    Else
                        row.Cells("Selected").Value = ""
                        row.DefaultCellStyle.BackColor = Color.White
                    End If
                End If
            Next

            AddHandler dg.CellDoubleClick, AddressOf dg_CellDoubleClick

        Catch ex As Exception
            MsgBox("Error adding selection column: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub RemoveSelectionColumn()
        If dg Is Nothing Then Return

        Try
            If dg.Columns.Contains("Selected") Then
                RemoveHandler dg.CellDoubleClick, AddressOf dg_CellDoubleClick
                dg.Columns.Remove("Selected")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dg_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return

        If isApproveMode Then
            Try
                Dim row = dg.Rows(e.RowIndex)
                If row.Cells("Borrow ID").Value Is Nothing Then Return

                Dim borrowID As String = row.Cells("Borrow ID").Value.ToString()

                If selectedRequests.Contains(borrowID) Then
                    selectedRequests.Remove(borrowID)
                    row.Cells("Selected").Value = ""
                    row.DefaultCellStyle.BackColor = Color.White
                Else
                    selectedRequests.Add(borrowID)
                    row.Cells("Selected").Value = "✓"
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                End If

                UpdateApprovePanelInfo()
                UpdateButtonStates()

            Catch ex As Exception
                MsgBox("Error updating selection: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
            End Try
        End If
    End Sub

    Private Sub UpdateApprovePanelInfo()
        Try
            Dim lblCurrentUser = approvePanel?.Controls.Find("lblCurrentUser", True).FirstOrDefault()
            Dim lblSelectedCount = approvePanel?.Controls.Find("lblSelectedCount", True).FirstOrDefault()

            If lblCurrentUser IsNot Nothing Then
                lblCurrentUser.Text = "User: " & If(String.IsNullOrEmpty(currentSelectedUser), "None", currentSelectedUser)
            End If

            If lblSelectedCount IsNot Nothing Then
                lblSelectedCount.Text = "Selected: " & selectedRequests.Count & " requests"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SQLQueryForBorrowPendingRequest()
        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            If daBorrowHistory Is Nothing Then
                daBorrowHistory = New OleDbDataAdapter()
            End If

            daBorrowHistory.SelectCommand = New OleDbCommand("SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Request Date] FROM borrowings WHERE status = 'Requested' ORDER BY [Request Date] DESC", con)

            If admindbds Is Nothing Then
                admindbds = New DataSet()
            Else
                If admindbds.Tables.Contains("borrowings") Then
                    admindbds.Tables("borrowings").Clear()
                End If
            End If

            daBorrowHistory.Fill(admindbds, "borrowings")

        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Public Sub SQLQueryForBorrowPendingRequestByUser(ByVal userID As String)
        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            If daBorrowHistory Is Nothing Then
                daBorrowHistory = New OleDbDataAdapter()
            End If

            daBorrowHistory.SelectCommand = New OleDbCommand("SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Request Date] FROM borrowings WHERE status = 'Requested' AND [User ID] = ? ORDER BY [Request Date] DESC", con)
            daBorrowHistory.SelectCommand.Parameters.AddWithValue("?", userID)

            If admindbds Is Nothing Then
                admindbds = New DataSet()
            Else
                If admindbds.Tables.Contains("borrowings") Then
                    admindbds.Tables("borrowings").Clear()
                End If
            End If

            daBorrowHistory.Fill(admindbds, "borrowings")

        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub dg_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return

        Try
            If e.Value IsNot Nothing AndAlso TypeOf e.Value Is DateTime Then
                e.Value = CType(e.Value, DateTime).ToString("MM/dd/yyyy")
                e.FormattingApplied = True
            ElseIf e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
                Dim dateValue As DateTime
                If DateTime.TryParse(e.Value.ToString(), dateValue) Then
                    e.Value = dateValue.ToString("MM/dd/yyyy")
                    e.FormattingApplied = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        If isApproveMode Then
            MsgBox("Please exit Approve mode first.", MsgBoxStyle.Exclamation, "Approve Mode Active")
            Return
        End If

        Dim searchTerm As String = InputBox("Enter Borrower Name to search:", "Search Borrower")
        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            SearchBorrower(searchTerm)
        ElseIf searchTerm IsNot Nothing Then
            MsgBox("Search cancelled.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SearchBorrower(ByVal searchTerm As String)
        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim query As String = "SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Request Date] FROM borrowings WHERE status = 'Requested' AND [Borrower Name] LIKE ? ORDER BY [Request Date] DESC"

            cmd = New OleDbCommand(query, con)
            cmd.Parameters.AddWithValue("?", "%" & searchTerm & "%")

            Dim searchResults As New DataSet()
            daBorrowHistory = New OleDbDataAdapter(cmd)
            daBorrowHistory.Fill(searchResults, "borrowings")

            If searchResults.Tables("borrowings").Rows.Count > 0 Then
                admindbds = searchResults
                dg.DataSource = admindbds.Tables("borrowings")
                dg.ClearSelection()
                selectedRequests.Clear()
                UpdateButtonStates()

                MsgBox(searchResults.Tables("borrowings").Rows.Count.ToString() & " request(s) found for borrower: " & searchTerm, MsgBoxStyle.Information)
            Else
                MsgBox("No requests found for borrower: " & searchTerm, MsgBoxStyle.Information)
                LoadPendingRequests()
            End If
        Catch ex As Exception
            MsgBox("Search failed. " & ex.Message, MsgBoxStyle.Critical, "Error")
            LoadPendingRequests()
        End Try
    End Sub

    Private Sub ApproveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApproveToolStripMenuItem.Click
        If isApproveMode Then
            ExitApproveMode()
        Else
            StartApproveProcess()
        End If
    End Sub

    Private Sub StartApproveProcess()
        If selectedRequests.Count = 0 Then
            MsgBox("Please select request(s) to approve first.", MsgBoxStyle.Exclamation, "Select Requests")
            Return
        End If

        Dim userName As String = ""
        Dim userID As String = ""
        For Each row As DataGridViewRow In dg.SelectedRows
            If row.Cells("Borrower Name").Value IsNot Nothing Then
                userName = row.Cells("Borrower Name").Value.ToString()
            End If
            If row.Cells("User ID").Value IsNot Nothing Then
                userID = row.Cells("User ID").Value.ToString()
            End If
            If Not String.IsNullOrEmpty(userName) AndAlso Not String.IsNullOrEmpty(userID) Then
                Exit For
            End If
        Next

        If String.IsNullOrEmpty(userName) Then
            MsgBox("Could not determine user name from selected requests.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        If String.IsNullOrEmpty(userID) Then
            MsgBox("Could not determine user ID from selected requests.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        EnterApproveMode(userName, userID)
    End Sub

    Private Sub EnterApproveMode(ByVal userName As String, ByVal userID As String)
        isApproveMode = True
        currentSelectedUser = userName
        currentSelectedUserID = userID
        approvePanel.Visible = True
        ApproveToolStripMenuItem.Text = "Exit Approve Mode"

        DisableOtherMenuItems()

        Dim tempSelectedRequests As New List(Of String)(selectedRequests)

        LoadPendingRequests()

        selectedRequests = tempSelectedRequests

        UpdateApprovePanelInfo()
        UpdateButtonStates()

        MsgBox("Approve Mode Activated for user: " + userName + " (ID: " + userID + ")" + vbCrLf + "Double-click rows to select requests for approval.", MsgBoxStyle.Information, "Approve Mode")
    End Sub

    Private Sub ExitApproveMode()
        isApproveMode = False
        currentSelectedUser = ""
        currentSelectedUserID = ""
        selectedRequests.Clear()
        approvePanel.Visible = False
        ApproveToolStripMenuItem.Text = "Approve"

        EnableOtherMenuItems()
        LoadPendingRequests()
    End Sub

    Private Sub DisableOtherMenuItems()
        If SearchToolStripMenuItem IsNot Nothing Then SearchToolStripMenuItem.Enabled = False
        If RejectToolStripMenuItem IsNot Nothing Then RejectToolStripMenuItem.Enabled = False
    End Sub

    Private Sub EnableOtherMenuItems()
        If SearchToolStripMenuItem IsNot Nothing Then SearchToolStripMenuItem.Enabled = True
        If RejectToolStripMenuItem IsNot Nothing Then RejectToolStripMenuItem.Enabled = True
    End Sub

    Private Sub btnApproveSelected_Click(ByVal sender As Object, ByVal e As EventArgs)
        If selectedRequests.Count = 0 Then
            MsgBox("Please select at least one request to approve.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Dim result As DialogResult = MsgBox("Approve " + selectedRequests.Count.ToString() + " selected borrow request(s) for user '" + currentSelectedUser + "'?" + vbCrLf + "This action cannot be undone.", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Approval")

        If result <> DialogResult.Yes Then
            Return
        End If

        Try
            If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Dim currentDate As DateTime = DateTime.Now
            Dim dueDate As DateTime = GetUserReturnDate()

            Dim formattedCurrentDate As String = currentDate.ToString("MM/dd/yyyy")
            Dim formattedDueDate As String = dueDate.ToString("MM/dd/yyyy")

            Dim approvedCount As Integer = 0

            For Each borrowID As String In selectedRequests
                Dim row As DataGridViewRow = GetRowByBorrowID(borrowID)
                If row IsNot Nothing AndAlso row.Cells("Book ID").Value IsNot Nothing AndAlso row.Cells("Copies").Value IsNot Nothing Then
                    Dim bookID As String = row.Cells("Book ID").Value.ToString()
                    Dim quantityToBorrow As Integer = Integer.Parse(row.Cells("Copies").Value.ToString())

                    cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Borrowed', [Borrow Date] = ?, [Due Date] = ? WHERE [Borrow ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", formattedCurrentDate)
                    cmd.Parameters.AddWithValue("?", formattedDueDate)
                    cmd.Parameters.AddWithValue("?", borrowID)
                    cmd.ExecuteNonQuery()

                    cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", bookID)
                    Dim currentQuantity As Integer = 0
                    Dim resultQuery = cmd.ExecuteScalar()
                    If resultQuery IsNot Nothing AndAlso Not IsDBNull(resultQuery) Then
                        currentQuantity = CInt(resultQuery)
                    End If

                    Dim newQuantity As Integer = currentQuantity - quantityToBorrow
                    cmd = New OleDbCommand("UPDATE books SET [Quantity] = ?, [Status] = IIF(? > 0, 'Available', 'Unavailable') WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", newQuantity)
                    cmd.Parameters.AddWithValue("?", newQuantity)
                    cmd.Parameters.AddWithValue("?", bookID)
                    cmd.ExecuteNonQuery()

                    approvedCount += 1
                End If
            Next

            GenerateBorrowReceipt(selectedRequests)

            MsgBox(approvedCount.ToString() + " borrow request(s) approved successfully!", MsgBoxStyle.Information, "Approval Complete")

            ExitApproveMode()
            LoadPendingRequests()

        Catch ex As Exception
            MsgBox("Error approving request(s): " + ex.Message, MsgBoxStyle.Critical, "Approval Error")
        End Try
    End Sub

    Private Sub btnCancelApprove_Click(ByVal sender As Object, ByVal e As EventArgs)
        ExitApproveMode()
        MsgBox("Approve mode cancelled.", MsgBoxStyle.Information, "Cancelled")
    End Sub

    Private Sub RejectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectToolStripMenuItem.Click
        If isApproveMode Then
            MsgBox("Please exit Approve mode first.", MsgBoxStyle.Exclamation, "Approve Mode Active")
            Return
        End If

        If selectedRequests.Count = 0 Then
            MsgBox("Please select request(s) to reject.", MsgBoxStyle.Exclamation, "Select Request")
            Return
        End If

        If MessageBox.Show("Are you sure you want to decline " + selectedRequests.Count.ToString() + " selected borrow request(s)?", "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                If con Is Nothing OrElse con.State <> ConnectionState.Open Then
                    OpenDB()
                End If

                For Each borrowID As String In selectedRequests
                    cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Declined' WHERE [Borrow ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", borrowID)
                    cmd.ExecuteNonQuery()
                Next

                MsgBox(selectedRequests.Count.ToString() + " borrow request(s) declined successfully!", MsgBoxStyle.Information, "Decline Complete")
                LoadPendingRequests()

            Catch ex As Exception
                MsgBox("Error rejecting request(s): " + ex.Message, MsgBoxStyle.Critical, "Decline Error")
            End Try
        End If
    End Sub

    Private Function GetRowByBorrowID(ByVal borrowID As String) As DataGridViewRow
        If dg Is Nothing Then Return Nothing

        For Each row As DataGridViewRow In dg.Rows
            If Not row.IsNewRow AndAlso row.Cells("Borrow ID").Value IsNot Nothing AndAlso row.Cells("Borrow ID").Value.ToString() = borrowID Then
                Return row
            End If
        Next
        Return Nothing
    End Function

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        If isApproveMode Then ExitApproveMode()
        If frmmain IsNot Nothing Then
            frmmain.Show()
        End If
        Me.Hide()
    End Sub


    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If isApproveMode Then
            MsgBox("Please exit Approve mode first.", MsgBoxStyle.Exclamation, "Approve Mode Active")
            Return
        End If

        LoadPendingRequests()
    End Sub

    Private Sub BorrowPendingRequest_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

    Private Function GetUserReturnDate() As DateTime
        Return DateTime.Now.AddDays(7)
    End Function

    Private Sub GenerateBorrowReceipt(selectedBorrowIDs As List(Of String))
        Try
            Dim dt As New DataTable("BorrowReceipt")
            dt.Columns.Add("Borrow ID", GetType(String))
            dt.Columns.Add("Book ID", GetType(String))
            dt.Columns.Add("User ID", GetType(String))
            dt.Columns.Add("Borrower Name", GetType(String))
            dt.Columns.Add("Borrower Position", GetType(String))
            dt.Columns.Add("Borrower Privileges", GetType(String))
            dt.Columns.Add("Copies", GetType(Integer))
            dt.Columns.Add("Borrow Date", GetType(String))
            dt.Columns.Add("Due Date", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Current Returned", GetType(Integer))
            dt.Columns.Add("Has Requested Return", GetType(String))
            dt.Columns.Add("Request Date", GetType(String))

            For Each borrowID As String In selectedBorrowIDs
                Dim query As String = "SELECT [Borrow ID], [Book ID], [User ID], [Borrower Name], [Borrower Position], [Borrower Privileges], [Copies], [Borrow Date], [Due Date], [Status], [Current Returned], [Has Requested Return], [Request Date] FROM borrowings WHERE [Borrow ID] = ?"
                Using cmd As New OleDbCommand(query, con)
                    cmd.Parameters.AddWithValue("?", borrowID)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            dt.Rows.Add(
                            reader("Borrow ID").ToString(),
                            reader("Book ID").ToString(),
                            reader("User ID").ToString(),
                            reader("Borrower Name").ToString(),
                            reader("Borrower Position").ToString(),
                            reader("Borrower Privileges").ToString(),
                            If(IsDBNull(reader("Copies")), 0, Convert.ToInt32(reader("Copies"))),
                            If(IsDBNull(reader("Borrow Date")), "", Convert.ToDateTime(reader("Borrow Date")).ToString("MM/dd/yyyy")),
                            If(IsDBNull(reader("Due Date")), "", Convert.ToDateTime(reader("Due Date")).ToString("MM/dd/yyyy")),
                            reader("Status").ToString(),
                            If(IsDBNull(reader("Current Returned")), 0, Convert.ToInt32(reader("Current Returned"))),
                            reader("Has Requested Return").ToString(),
                            If(IsDBNull(reader("Request Date")), "", Convert.ToDateTime(reader("Request Date")).ToString("MM/dd/yyyy"))
                        )
                        End If
                    End Using
                End Using
            Next

            Dim report As New ReportDocument()
            Dim reportPath As String = Path.Combine(Application.StartupPath, "Reports\CrystalReport2.rpt")

            If Not File.Exists(reportPath) Then
                MsgBox("Report not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                Return
            End If

            report.Load(reportPath)
            report.SetDataSource(dt)
            report.PrintToPrinter(1, False, 0, 0)

        Catch ex As Exception
            MsgBox("Error generating borrow receipts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Function GenerateBorrowID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "BR-"

        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT MAX([Borrow ID]) FROM borrowings WHERE [Borrow ID] LIKE '" & prefix & "%'", con)
            Dim result As Object = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Dim lastID As String = result.ToString()
                If lastID.StartsWith(prefix) Then
                    Dim numberPart As String = lastID.Substring(prefix.Length)
                    If Integer.TryParse(numberPart, maxNumber) Then
                        maxNumber += 1
                    Else
                        maxNumber = 1
                    End If
                Else
                    maxNumber = 1
                End If
            Else
                maxNumber = 1
            End If

            Return prefix & maxNumber.ToString("D5")
        Catch ex As Exception
            Return prefix & "00001"
        End Try
    End Function

    Private Sub dg_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub
End Class