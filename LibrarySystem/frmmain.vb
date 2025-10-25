Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmmain
    Private receiptCurrentType As String = "Borrow"

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        LoadReceiptData("Borrow")
        UpdateReceiptMenuStatus()
    End Sub

    Private Sub MainForm_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        OpenDB()
        lblUsername.Text = XName
        txtPrivilege.Text = "Privilege : " & xpriv
        txtPosition.Text = "Position : " & xpost
        LoadUserImage()
        panReceipts.Visible = isPanReceiptOpen
        LoadReceiptData(receiptCurrentType)

        LoadStatistics()

        If xpriv = "User" Then
            ReportsToolStripMenuItem.Visible = False
            BorrowToolStripMenuItem.Enabled = True
            ReturnToolStripMenuItem.Enabled = True
            PenaltyToolStripMenuItem.Enabled = True
            BookInventory.Enabled = False
            menuTransactions.Enabled = True
            menuUserForm.Enabled = False
            menuBookInventory.Enabled = False
            BorrowPendingRequestToolStripMenuItem.Enabled = False
            dashAdminPan.Visible = False
            dashUserpanel.Visible = True
            ReportsToolStripMenuItem.Visible = False
        End If

        If xpriv = "Admin" Then
            ReportsToolStripMenuItem.Visible = True
            BorrowToolStripMenuItem.Enabled = True
            ReturnToolStripMenuItem.Enabled = True
            PenaltyToolStripMenuItem.Enabled = True
            BookInventory.Enabled = True
            menuTransactions.Enabled = True
            menuUserForm.Enabled = True
            menuBookInventory.Enabled = True
            BorrowPendingRequestToolStripMenuItem.Enabled = True
            dashAdminPan.Visible = True
            dashUserpanel.Visible = False
            ReportsToolStripMenuItem.Visible = True
        End If

        If xpost = "Administrator" And xpriv = "Admin" Then
            ReportsToolStripMenuItem.Visible = True
            BorrowToolStripMenuItem.Enabled = False
            ReturnToolStripMenuItem.Enabled = False
            PenaltyToolStripMenuItem.Enabled = False
            BookInventory.Enabled = True
            menuTransactions.Enabled = True
            menuUserForm.Enabled = True
            menuBookInventory.Enabled = True
            BorrowPendingRequestToolStripMenuItem.Enabled = True
            dashAdminPan.Visible = True
            dashUserpanel.Visible = False
            ReportsToolStripMenuItem.Visible = True
        End If
    End Sub

    Private Sub LoadUserImage()
        Try
            If pbProfile.Image IsNot Nothing Then
                pbProfile.Image.Dispose()
                pbProfile.Image = Nothing
            End If

            Dim userId As String = GetUserIdByUsername(XName)

            If Not String.IsNullOrEmpty(userId) Then
                Dim imagePath As String = Application.StartupPath & "\images\" & userId & ".jpg"
                If System.IO.File.Exists(imagePath) Then
                    Using fs As New IO.FileStream(imagePath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pbProfile.Image = New Bitmap(img)
                    End Using
                Else
                    pbProfile.Image = Nothing
                    pbProfile.BackColor = Color.LightGray
                End If
            Else
                pbProfile.Image = Nothing
                pbProfile.BackColor = Color.LightGray
            End If

        Catch ex As Exception
            pbProfile.Image = Nothing
            pbProfile.BackColor = Color.LightGray
        End Try
    End Sub

    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("This action will terminate the user from using the system? Would you like to proceed?", MsgBoxStyle.YesNo, "closing...") = MsgBoxResult.Yes Then
            Dim opacityCounter As Integer
            For opacityCounter = 90 To 10 Step -20
                Me.Opacity = opacityCounter / 100
                Me.Refresh()
                Threading.Thread.Sleep(5)
            Next opacityCounter
            Application.Exit()
        Else
            Me.Focus()
        End If
    End Sub

    Private Sub menuUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuUserForm.Click
        Dim userfrm As New Form1
        userfrm.Show()
        Me.Close()
    End Sub

    Private Sub menuBookInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuBookInventory.Click
        BookInventory.Show()
        Me.Hide()
    End Sub

    Private Sub menuLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuLogout.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo, "Logout") = MsgBoxResult.Yes Then
            Me.Close()
            Login.Visible = True
        End If
    End Sub

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        OpenDB()
    End Sub

    Private Sub BorrowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrowToolStripMenuItem.Click
        Dim borrow As New Borrow()
        borrow.Show()
        Me.Hide()
    End Sub

    Private Sub TransactionHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransactionHistoryToolStripMenuItem.Click
        Me.Hide()
        History.Show()
    End Sub

    Private Sub BorrowPendingRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrowPendingRequestToolStripMenuItem.Click
        BorrowPendingRequest.Show()
        Me.Hide()
    End Sub

    Private Sub ReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnToolStripMenuItem.Click
        If xpriv = "User" Then
            UserReturn.Show()
        Else
            AdminReturn.Show()
        End If
        Me.Close()
    End Sub

    Private Sub lblBooksReturned_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBooksReturned.Click
    End Sub

    Private Sub PenaltyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenaltyToolStripMenuItem.Click
        Dim penalty As New PenaltyForm
        penalty.Show()
        Me.Hide()
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs)
        Try
            Dim reportForm As New ReportForm()
            reportForm.Show()
        Catch ex As Exception
            MessageBox.Show("Error opening report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BorrowRequest.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ReturnedBooks.Show()
        Me.Hide()
    End Sub

    Private Sub LoadStatistics()
        Try
            OpenDB()

            If xpriv = "Admin" Then
                Dim totalBorrowedQuery As String = "SELECT COUNT(*) FROM borrowings WHERE [Status] = 'Borrowed'"
                cmd = New OleDbCommand(totalBorrowedQuery, con)
                Dim totalBorrowedCount As Integer = CInt(cmd.ExecuteScalar())
                lblTotalBorrowedBooks.Text = totalBorrowedCount.ToString()

                Dim totalOverdueQuery As String = "SELECT COUNT(*) FROM transactions WHERE [Status] = 'Requested'"
                cmd = New OleDbCommand(totalOverdueQuery, con)
                Dim totalOverdueCount As Integer = CInt(cmd.ExecuteScalar())
                lblTotalBorrowedRequest.Text = totalOverdueCount.ToString()

                Dim totalBooksQuery As String = "SELECT COUNT(*) FROM books"
                cmd = New OleDbCommand(totalBooksQuery, con)
                Dim totalBooksCount As Integer = CInt(cmd.ExecuteScalar())
                lblTotalBooks.Text = totalBooksCount.ToString()

                Dim totalUnpaidPenaltiesQuery As String = "SELECT COUNT(*) FROM Penalties WHERE [Penalty Status] = 'Unpaid'"
                cmd = New OleDbCommand(totalUnpaidPenaltiesQuery, con)
                Dim totalUnpaidPenaltiesCount As Integer = CInt(cmd.ExecuteScalar())
                lblTotalUnpaidPenalties.Text = totalUnpaidPenaltiesCount.ToString()

                Dim forgotPasswordQuery As String = "SELECT COUNT(*) FROM PasswordResetRequests WHERE [Status] = 'Pending'"
                cmd = New OleDbCommand(forgotPasswordQuery, con)
                Dim forgotPasswordCount As Integer = CInt(cmd.ExecuteScalar())
                lblForgotPasswordRequest.Text = forgotPasswordCount.ToString()

                Dim totalBookCopiesQuery As String = "SELECT SUM([Quantity]) FROM books"
                cmd = New OleDbCommand(totalBookCopiesQuery, con)
                Dim result = cmd.ExecuteScalar()
                Dim totalBookCopiesCount As Integer = If(result Is DBNull.Value, 0, CInt(result))
                lblTotalBookCopies.Text = totalBookCopiesCount.ToString()
            Else
                Dim pendingBorrowedQuery As String = "SELECT COUNT(*) FROM transactions WHERE [Borrower Name] = @UserName AND [Status] = 'Borrowed'"
                cmd = New OleDbCommand(pendingBorrowedQuery, con)
                cmd.Parameters.AddWithValue("@UserName", XName)
                Dim pendingBorrowedCount As Integer = CInt(cmd.ExecuteScalar())
                lblCurrentBorrowings.Text = pendingBorrowedCount.ToString()

                Dim requestedBooksQuery As String = "SELECT COUNT(*) FROM borrowings WHERE [Borrower Name] = @UserName AND [Status] = 'Requested'"
                cmd = New OleDbCommand(requestedBooksQuery, con)
                cmd.Parameters.AddWithValue("@UserName", XName)
                Dim requestedBooksCount As Integer = CInt(cmd.ExecuteScalar())
                lblCurrentRequestedBook.Text = requestedBooksCount.ToString()

                Dim totalUnpaidCountQuery As String = "SELECT COUNT(*) FROM Penalties WHERE [Penalty Status] = 'Unpaid' AND [User Name] = ?"
                cmd = New OleDbCommand(totalUnpaidCountQuery, con)
                cmd.Parameters.AddWithValue("?", XName)
                Dim totalUnpaidCountObj As Object = cmd.ExecuteScalar()
                Dim totalUnpaidCount As Integer = If(IsDBNull(totalUnpaidCountObj), 0, CInt(totalUnpaidCountObj))
                lblUnpaidBorrowedBooks.Text = totalUnpaidCount.ToString()

                Dim totalReturnedQuery As String = "SELECT COUNT(*) FROM borrowings WHERE [Borrower Name] = ? AND [Status] = 'Completed'"
                cmd = New OleDbCommand(totalReturnedQuery, con)
                cmd.Parameters.AddWithValue("?", XName)
                Dim totalReturnedCountObj As Object = cmd.ExecuteScalar()
                Dim totalReturnedCount As Integer = If(IsDBNull(totalReturnedCountObj), 0, CInt(totalReturnedCountObj))
                lblBooksReturned.Text = totalReturnedCount.ToString()
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message)

            lblCurrentBorrowings.Text = "Error"
            lblBooksReturned.Text = "Error"
            lblCurrentRequestedBook.Text = "Error"
            lblUnpaidBorrowedBooks.Text = "Error"
            lblTotalBorrowedBooks.Text = "Error"
            lblTotalBorrowedRequest.Text = "Error"
            lblTotalBooks.Text = "Error"
            lblTotalUnpaidPenalties.Text = "Error"
            lblForgotPasswordRequest.Text = "Error"

        Finally
            CloseDB()
        End Try
    End Sub

    Private Sub btnviewTotalUnpaidPenalties_Click(sender As Object, e As EventArgs) Handles btnviewTotalUnpaidPenalties.Click
        PenaltyForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnCurrentRequestedReturn_Click(sender As Object, e As EventArgs) Handles btnCurrentRequestedReturn.Click
        BorrowPendingRequest.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        RequestedChangePassword.Show()
        Me.Hide()
    End Sub


    Private Sub BorrowReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrowReceiptToolStripMenuItem.Click
        LoadReceiptData("Borrow")
        receiptCurrentType = "Borrow"
        UpdateReceiptMenuStatus()
    End Sub

    Private Sub ReturnReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnReceiptToolStripMenuItem.Click
        LoadReceiptData("Return")
        receiptCurrentType = "Return"
        UpdateReceiptMenuStatus()
    End Sub

    Private Sub PenaltyReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenaltyReceiptToolStripMenuItem.Click
        LoadReceiptData("Penalty")
        receiptCurrentType = "Penalty"
        UpdateReceiptMenuStatus()
    End Sub

    Private Sub PrintReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintReceiptToolStripMenuItem.Click
        If dgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dgv.SelectedRows(0)
            Dim receiptID As String = selectedRow.Cells("ReceiptID").Value.ToString()
            GenerateAndDisplayReceipt(receiptID, receiptCurrentType)
        Else
            MsgBox("Please select a receipt to print.", MsgBoxStyle.Exclamation, "Select Receipt")
        End If
    End Sub

    Private Sub LoadReceiptData(Optional receiptType As String = "Borrow")
        Try
            OpenDB()

            Dim sql As String = ""
            receiptCurrentType = receiptType

            Select Case receiptType
                Case "Borrow"
                    sql = "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [User Name] as [UserName], " &
                      "[Receipt Date] as [Date], [Processed By] as [ProcessedBy] " &
                      "FROM borrowReceipts ORDER BY [Receipt Date] DESC"

                Case "Return"
                    sql = "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [Borrower Name] as [UserName], " &
                      "[Return Date] as [Date], [Processed By] as [ProcessedBy] " &
                      "FROM returnReceipts ORDER BY [Return Date] DESC"

                Case "Penalty"
                    sql = "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [User Name] as [UserName], " &
                      "[Payment Date] as [Date], [Processed By] as [ProcessedBy] " &
                      "FROM paymentReceipts ORDER BY [Payment Date] DESC"
            End Select

            Dim receiptDataSet As New DataSet()
            Dim da As New OleDbDataAdapter(sql, con)
            da.Fill(receiptDataSet, "Receipts")

            dgv.DataSource = receiptDataSet.Tables("Receipts")

            CustomizeReceiptDataGridView(dgv)

            Me.Text = "Library Management System - " & receiptType & " Receipts"

        Catch ex As Exception
            MsgBox("Error loading receipt data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            CloseDB()
        End Try
    End Sub



    Private Sub CustomizeReceiptDataGridView(dgv As DataGridView)
        Try
            dgv.ReadOnly = True
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgv.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9)
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray

            If dgv.Columns.Contains("ReceiptID") Then
                dgv.Columns("ReceiptID").Width = 150
                dgv.Columns("ReceiptID").HeaderText = "Receipt ID"
            End If
            If dgv.Columns.Contains("TransactionID") Then
                dgv.Columns("TransactionID").Width = 120
                dgv.Columns("TransactionID").HeaderText = "Borrow ID"
            End If
            If dgv.Columns.Contains("UserName") Then
                dgv.Columns("UserName").Width = 150
                dgv.Columns("UserName").HeaderText = "User Name"
            End If
            If dgv.Columns.Contains("Date") Then
                dgv.Columns("Date").Width = 100
                dgv.Columns("Date").HeaderText = "Date"
            End If
            If dgv.Columns.Contains("ProcessedBy") Then
                dgv.Columns("ProcessedBy").Width = 120
                dgv.Columns("ProcessedBy").HeaderText = "Processed By"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdateReceiptMenuStatus()
        BorrowReceiptToolStripMenuItem.Checked = (receiptCurrentType = "Borrow")
        ReturnReceiptToolStripMenuItem.Checked = (receiptCurrentType = "Return")
        PenaltyReceiptToolStripMenuItem.Checked = (receiptCurrentType = "Penalty")
    End Sub

    Private Sub GenerateAndDisplayReceipt(receiptID As String, receiptType As String)
        Try
            OpenDB()

            Dim reportForm As New ReportForm()
            Dim report As New ReportDocument()
            Dim reportPath As String = ""

            Select Case receiptType
                Case "Borrow"
                    reportPath = Path.Combine(Application.StartupPath, "Reports\CrystalReport2.rpt")
                    Dim query As String = "SELECT " &
                    "b.[ID], b.[Borrow ID], b.[Book ID], b.[ISBN], b.[Title], " &
                    "b.[User ID], b.[Borrower Name], b.[Borrower Position], b.[Borrower Privileges], " &
                    "b.[Copies], b.[Current Returned], b.[Borrow Date], b.[Due Date], " &
                    "b.[Status], b.[Request Date], b.[Processed By] as [BorrowProcessedBy], " &
                    "br.[Receipt Date], br.[Processed By] " &
                    "FROM borrowings b " &
                    "INNER JOIN borrowReceipts br ON b.[Borrow ID] = br.[Borrow ID] " &
                    "WHERE br.[Receipt ID] = ?"

                    Using da As New OleDbDataAdapter(query, con)
                        da.SelectCommand.Parameters.AddWithValue("?", receiptID)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            report.Load(reportPath)
                            report.SetDataSource(dt)
                            reportForm.CrystalReportViewer1.ReportSource = report
                            reportForm.ShowDialog()
                        Else
                            MsgBox("Receipt data not found.", MsgBoxStyle.Exclamation)
                        End If
                    End Using

                Case "Return"
                    reportPath = Path.Combine(Application.StartupPath, "Reports\CRReturnBook.rpt")
                    Dim query As String = "SELECT * FROM returnReceipts WHERE [Receipt ID] = ?"

                    Using da As New OleDbDataAdapter(query, con)
                        da.SelectCommand.Parameters.AddWithValue("?", receiptID)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            report.Load(reportPath)
                            report.SetDataSource(dt)
                            reportForm.CrystalReportViewer1.ReportSource = report
                            reportForm.ShowDialog()
                        Else
                            MsgBox("Receipt data not found.", MsgBoxStyle.Exclamation)
                        End If
                    End Using

                Case "Penalty"
                    reportPath = Path.Combine(Application.StartupPath, "Reports\CrystalReport1.rpt")
                    Dim query As String = "SELECT * FROM paymentReceipts WHERE [Receipt ID] = ?"

                    Using da As New OleDbDataAdapter(query, con)
                        da.SelectCommand.Parameters.AddWithValue("?", receiptID)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            report.Load(reportPath)
                            report.SetDataSource(dt)
                            reportForm.CrystalReportViewer1.ReportSource = report
                            reportForm.ShowDialog()
                        Else
                            MsgBox("Receipt data not found.", MsgBoxStyle.Exclamation)
                        End If
                    End Using
            End Select

            If Not File.Exists(reportPath) Then
                MsgBox("Report template not found: " & reportPath, MsgBoxStyle.Exclamation)
                Return
            End If

            report.Close()
            report.Dispose()
            reportForm.Dispose()

        Catch ex As Exception
            MsgBox("Error generating receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            CloseDB()
        End Try
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Try
            Dim selectedRow As DataGridViewRow = dgv.Rows(e.RowIndex)
            Dim receiptID As String = selectedRow.Cells("ReceiptID").Value.ToString()
            GenerateAndDisplayReceipt(receiptID, receiptCurrentType)

        Catch ex As Exception
            MsgBox("Error viewing receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Dim isPanReceiptOpen As Boolean = False

    Private Sub ReceiptsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceiptsToolStripMenuItem.Click
        isPanReceiptOpen = Not isPanReceiptOpen
        panReceipts.Visible = isPanReceiptOpen

        receiptCurrentType = "Borrow"
        Me.Text = ""
    End Sub

    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click
        Dim searchTerm As String = InputBox("Enter Receipt ID, Borrow ID, or User Name to search:", "Search Receipts")

        If String.IsNullOrWhiteSpace(searchTerm) Then
            Return
        End If

        SearchReceipts(searchTerm.Trim())
    End Sub

    Private Sub SearchReceipts(searchTerm As String)
        Try
            OpenDB()

            Dim sql As String = ""
            Dim detectedType As String = ""

            If searchTerm.StartsWith("BOR-") Then
                detectedType = "Borrow"
                sql = "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [User Name] as [UserName], " &
                  "[Receipt Date] as [Date], [Processed By] as [ProcessedBy] " &
                  "FROM borrowReceipts " &
                  "WHERE [Receipt ID] LIKE ? OR [Borrow ID] LIKE ? OR [User Name] LIKE ? " &
                  "ORDER BY [Receipt Date] DESC"
            ElseIf searchTerm.StartsWith("RET-") Then
                detectedType = "Return"
                sql = "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [Borrower Name] as [UserName], " &
                  "[Return Date] as [Date], [Processed By] as [ProcessedBy] " &
                  "FROM returnReceipts " &
                  "WHERE [Receipt ID] LIKE ? OR [Borrow ID] LIKE ? OR [Borrower Name] LIKE ? " &
                  "ORDER BY [Return Date] DESC"
            ElseIf searchTerm.StartsWith("PEN-") Then
                detectedType = "Penalty"
                sql = "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [User Name] as [UserName], " &
                  "[Payment Date] as [Date], [Processed By] as [ProcessedBy] " &
                  "FROM paymentReceipts " &
                  "WHERE [Receipt ID] LIKE ? OR [Borrow ID] LIKE ? OR [User Name] LIKE ? " &
                  "ORDER BY [Payment Date] DESC"
            Else
                detectedType = "All"
                sql = "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [User Name] as [UserName], " &
                  "[Receipt Date] as [Date], [Processed By] as [ProcessedBy], 'Borrow' as [Type] " &
                  "FROM borrowReceipts " &
                  "WHERE [Receipt ID] LIKE ? OR [Borrow ID] LIKE ? OR [User Name] LIKE ? " &
                  "UNION ALL " &
                  "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [Borrower Name] as [UserName], " &
                  "[Return Date] as [Date], [Processed By] as [ProcessedBy], 'Return' as [Type] " &
                  "FROM returnReceipts " &
                  "WHERE [Receipt ID] LIKE ? OR [Borrow ID] LIKE ? OR [Borrower Name] LIKE ? " &
                  "UNION ALL " &
                  "SELECT DISTINCT [Receipt ID] as [ReceiptID], [Borrow ID] as [TransactionID], [User Name] as [UserName], " &
                  "[Payment Date] as [Date], [Processed By] as [ProcessedBy], 'Penalty' as [Type] " &
                  "FROM paymentReceipts " &
                  "WHERE [Receipt ID] LIKE ? OR [Borrow ID] LIKE ? OR [User Name] LIKE ? " &
                  "ORDER BY [Date] DESC"
            End If

            Dim receiptDataSet As New DataSet()
            Dim da As New OleDbDataAdapter(sql, con)

            If detectedType = "All" Then
                For i As Integer = 1 To 9
                    da.SelectCommand.Parameters.AddWithValue("?", "%" & searchTerm & "%")
                Next
            Else
                For i As Integer = 1 To 3
                    da.SelectCommand.Parameters.AddWithValue("?", "%" & searchTerm & "%")
                Next
            End If

            da.Fill(receiptDataSet, "Receipts")

            dgv.DataSource = receiptDataSet.Tables("Receipts")

            If receiptDataSet.Tables("Receipts").Rows.Count > 0 Then
                If detectedType = "All" Then
                    Dim firstRowType As String = receiptDataSet.Tables("Receipts").Rows(0)("Type").ToString()
                    receiptCurrentType = firstRowType
                    Me.Text = "Library Management System - Search Results (" & receiptDataSet.Tables("Receipts").Rows.Count & " found)"
                Else
                    receiptCurrentType = detectedType
                    Me.Text = "Library Management System - " & detectedType & " Receipts - Search Results (" & receiptDataSet.Tables("Receipts").Rows.Count & " found)"
                End If

                If Not panReceipts.Visible Then
                    panReceipts.Visible = True
                    isPanReceiptOpen = True
                End If
            Else
                Me.Text = "Library Management System - No Results Found"
                MsgBox("No receipts found for: " & searchTerm, MsgBoxStyle.Information, "Search Results")
            End If

            UpdateReceiptMenuStatus()

        Catch ex As Exception
            MsgBox("Error searching receipts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            CloseDB()
        End Try
    End Sub
End Class