Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmmain

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub MainForm_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        OpenDB()
        lblUsername.Text = XName
        txtPrivilege.Text = "Privilege : " & xpriv
        txtPosition.Text = "Position : " & xpost
        LoadUserImage()

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
            Me.Hide()
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

                '
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

                Dim totalUnpaidCountQuery As String = "
                  SELECT COUNT(*) 
                    FROM Penalties 
                     WHERE [Penalty Status] = 'Unpaid' 
                     AND [User Name] = ?"
                cmd = New OleDbCommand(totalUnpaidCountQuery, con)
                cmd.Parameters.AddWithValue("?", XName)
                Dim totalUnpaidCountObj As Object = cmd.ExecuteScalar()
                Dim totalUnpaidCount As Integer = If(IsDBNull(totalUnpaidCountObj), 0, CInt(totalUnpaidCountObj))
                lblUnpaidBorrowedBooks.Text = totalUnpaidCount.ToString()


                Dim totalReturnedQuery As String = "
                SELECT COUNT(*) 
                FROM borrowings 
                WHERE [Borrower Name] = ? 
                AND [Status] = 'Completed'"
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


    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        Dim changePasswordForm As New ChangePassword()
        changePasswordForm.ShowDialog()
    End Sub

    Private Sub BorrowReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrowReceiptToolStripMenuItem.Click
        GenerateBorrowReceipt()
    End Sub

    Private Sub ReturnReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnReceiptToolStripMenuItem.Click
        GenerateReturnReceipt()
    End Sub

    Private Sub PenaltyReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenaltyReceiptToolStripMenuItem.Click
        GeneratePenaltyReceipt()
    End Sub







    Private Sub GenerateBorrowReceipt()
        Try
            Dim borrowID As String = InputBox("Enter Borrow ID to generate receipt:", "Borrow Receipt")

            If String.IsNullOrEmpty(borrowID) Then
                MsgBox("Operation cancelled.", MsgBoxStyle.Information, "Cancelled")
                Return
            End If

            borrowID = borrowID.Trim()

            ' Verify the borrow ID exists
            OpenDB()
            cmd = New OleDbCommand("SELECT COUNT(*) FROM transactions WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim count As Integer = CInt(cmd.ExecuteScalar())

            If count = 0 Then
                MsgBox("Borrow ID '" & borrowID & "' not found.", MsgBoxStyle.Exclamation, "Not Found")
                CloseDB()
                Return
            End If

            ' Get borrow transaction data
            cmd = New OleDbCommand("SELECT * FROM transactions WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim dt As New DataTable("BorrowReceipt")
                ' Match the columns exactly with your Crystal Report
                dt.Columns.Add("Borrow ID", GetType(String))
                dt.Columns.Add("Book ID", GetType(String))
                dt.Columns.Add("Book Title", GetType(String))
                dt.Columns.Add("User ID", GetType(String))
                dt.Columns.Add("Borrower Name", GetType(String))
                dt.Columns.Add("Borrower Position", GetType(String))
                dt.Columns.Add("Borrower Privileges", GetType(String))
                dt.Columns.Add("Copies", GetType(Integer))
                dt.Columns.Add("Borrow Date", GetType(String))
                dt.Columns.Add("Due Date", GetType(String))
                dt.Columns.Add("Status", GetType(String))

                Dim bookIDs As String = reader("Book ID List").ToString()
                Dim copyList As String = reader("Copy List").ToString()

                Dim bookIDArray() As String = bookIDs.Split(","c)
                Dim copyArray() As String = copyList.Split(","c)

                For i As Integer = 0 To bookIDArray.Length - 1
                    Dim bookID As String = bookIDArray(i).Trim()
                    Dim copies As Integer = Integer.Parse(copyArray(i).Trim())
                    Dim bookTitle As String = GetBookTitleByID(bookID)

                    dt.Rows.Add(
                        reader("Borrow ID").ToString(),
                        bookID,
                        bookTitle,
                        reader("User ID").ToString(),
                        reader("Borrower Name").ToString(),
                        reader("Borrower Position").ToString(),
                        reader("Borrower Privileges").ToString(),
                        copies,
                        If(IsDBNull(reader("Borrow Date")), "N/A", Convert.ToDateTime(reader("Borrow Date")).ToString("MM/dd/yyyy")),
                        If(IsDBNull(reader("Due Date")), "N/A", Convert.ToDateTime(reader("Due Date")).ToString("MM/dd/yyyy")),
                        reader("Status").ToString()
                    )
                Next

                reader.Close()

                ' Generate report
                Dim reportForm As New ReportForm()
                Dim report As New ReportDocument()
                Dim reportPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CrystalReport2.rpt")

                If Not System.IO.File.Exists(reportPath) Then
                    MsgBox("Borrow receipt report template not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                    CloseDB()
                    Return
                End If

                report.Load(reportPath)
                report.SetDataSource(dt)
                reportForm.CrystalReportViewer1.ReportSource = report
                reportForm.ShowDialog()

                report.Close()
                report.Dispose()

            Else
                reader.Close()
                MsgBox("No transaction found for Borrow ID: " & borrowID, MsgBoxStyle.Exclamation, "Not Found")
            End If

            CloseDB()

        Catch ex As Exception
            MsgBox("Error generating borrow receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
            CloseDB()
        End Try
    End Sub

    Private Sub GenerateReturnReceipt()
        Try
            Dim borrowID As String = InputBox("Enter Borrow ID to generate return receipt:", "Return Receipt")

            If String.IsNullOrEmpty(borrowID) Then
                MsgBox("Operation cancelled.", MsgBoxStyle.Information, "Cancelled")
                Return
            End If

            borrowID = borrowID.Trim()

            ' Verify the borrow ID exists in return log
            OpenDB()
            cmd = New OleDbCommand("SELECT COUNT(*) FROM returnLog WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim count As Integer = CInt(cmd.ExecuteScalar())

            If count = 0 Then
                MsgBox("No return records found for Borrow ID '" & borrowID & "'.", MsgBoxStyle.Exclamation, "Not Found")
                CloseDB()
                Return
            End If

            ' First, get the borrower name from transactions table
            Dim borrowerName As String = ""
            cmd = New OleDbCommand("SELECT [Borrower Name] FROM transactions WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim nameResult As Object = cmd.ExecuteScalar()
            If nameResult IsNot Nothing AndAlso Not IsDBNull(nameResult) Then
                borrowerName = nameResult.ToString()
            End If

            ' Get return data - get ALL return records for this borrow ID
            cmd = New OleDbCommand("SELECT * FROM returnLog WHERE [Borrow ID] = ? ORDER BY [Return Date]", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            Dim dt As New DataTable("ReturnReceipt")
            dt.Columns.Add("Return Date", GetType(String))
            dt.Columns.Add("Borrow ID", GetType(String))
            dt.Columns.Add("Book ID", GetType(String))
            dt.Columns.Add("Book Title", GetType(String))
            dt.Columns.Add("Quantity Returned", GetType(Integer))
            dt.Columns.Add("Processed By", GetType(String))
            dt.Columns.Add("Borrower Name", GetType(String))

            Dim totalPenalty As Decimal = 0
            Dim hasRecords As Boolean = False

            While reader.Read()
                hasRecords = True
                Dim bookID As String = reader("Book ID").ToString()
                Dim bookTitle As String = GetBookTitleByID(bookID)

                dt.Rows.Add(
                Convert.ToDateTime(reader("Return Date")).ToString("MM/dd/yyyy"),
                reader("Borrow ID").ToString(),
                bookID,
                bookTitle,
                reader("Returned Quantity"),
                reader("Processed By").ToString(),
                borrowerName ' Use the borrower name we retrieved earlier
            )
            End While

            reader.Close()

            ' Get total penalties for this borrow ID if any
            If hasRecords Then
                cmd = New OleDbCommand("SELECT SUM([Penalty Amount]) as TotalPenalty FROM Penalties WHERE [Borrow ID] = ? AND [Penalty Status] = 'Paid'", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                Dim totalPenaltyObj As Object = cmd.ExecuteScalar()
                totalPenalty = If(IsDBNull(totalPenaltyObj), 0, Convert.ToDecimal(totalPenaltyObj))

                If totalPenalty > 0 Then
                    ' Add penalty summary row
                    dt.Rows.Add("", "", "", "TOTAL PENALTY PAID", 0, "", "$" & totalPenalty.ToString("F2"))
                End If

                ' Generate report
                Dim reportForm As New ReportForm()
                Dim report As New ReportDocument()
                Dim reportPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CRReturnBook.rpt")

                If Not System.IO.File.Exists(reportPath) Then
                    MsgBox("Return receipt report template not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                    CloseDB()
                    Return
                End If

                report.Load(reportPath)
                report.SetDataSource(dt)
                reportForm.CrystalReportViewer1.ReportSource = report
                reportForm.ShowDialog()

                report.Close()
                report.Dispose()
            Else
                MsgBox("No return records found for Borrow ID: " & borrowID, MsgBoxStyle.Exclamation, "Not Found")
            End If

            CloseDB()

        Catch ex As Exception
            MsgBox("Error generating return receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
            CloseDB()
        End Try
    End Sub
    Private Sub GeneratePenaltyReceipt()
        Try
            Dim borrowID As String = InputBox("Enter Borrow ID to generate penalty receipt:", "Penalty Receipt")

            If String.IsNullOrEmpty(borrowID) Then
                MsgBox("Operation cancelled.", MsgBoxStyle.Information, "Cancelled")
                Return
            End If

            borrowID = borrowID.Trim()

            OpenDB()
            cmd = New OleDbCommand("SELECT COUNT(*) FROM Penalties WHERE [Borrow ID] = ?", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim count As Integer = CInt(cmd.ExecuteScalar())

            If count = 0 Then
                MsgBox("No penalty records found for Borrow ID '" & borrowID & "'.", MsgBoxStyle.Exclamation, "Not Found")
                CloseDB()
                Return
            End If

            ' Get ALL penalty records for this borrow ID
            cmd = New OleDbCommand("SELECT * FROM Penalties WHERE [Borrow ID] = ? ORDER BY [PenaltyID]", con)
            cmd.Parameters.AddWithValue("?", borrowID)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            Dim dt As New DataTable("PenaltyReceipt")
            ' Match EXACTLY with your Crystal Report fields
            dt.Columns.Add("Borrow ID", GetType(String))
            dt.Columns.Add("Book ID", GetType(String))
            dt.Columns.Add("Quantity", GetType(Integer))
            dt.Columns.Add("Condition", GetType(String))
            dt.Columns.Add("Days Late", GetType(Integer))
            dt.Columns.Add("Penalty Amount", GetType(Decimal))

            While reader.Read()
                Dim bookID As String = ""
                If Not IsDBNull(reader("Book ID")) Then bookID = reader("Book ID").ToString()

                Dim amount As Decimal = 0
                If Not IsDBNull(reader("Penalty Amount")) Then
                    Decimal.TryParse(reader("Penalty Amount").ToString(), amount)
                End If

                dt.Rows.Add(
                borrowID,
                bookID,
                If(IsDBNull(reader("Quantity")), 0, reader("Quantity")),
                If(IsDBNull(reader("Book Condition")), "Normal", reader("Book Condition").ToString()),
                If(IsDBNull(reader("Days Late")), 0, reader("Days Late")),
                amount
            )
            End While

            reader.Close()

            ' Generate report - NO TOTAL ROW ADDED since Crystal Report handles it
            Dim reportForm As New ReportForm()
            Dim report As New ReportDocument()
            Dim reportPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CrystalReport1.rpt")

            If Not System.IO.File.Exists(reportPath) Then
                MsgBox("Penalty receipt report template not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                CloseDB()
                Return
            End If

            report.Load(reportPath)
            report.SetDataSource(dt)
            reportForm.CrystalReportViewer1.ReportSource = report
            reportForm.ShowDialog()

            report.Close()
            report.Dispose()

            CloseDB()

        Catch ex As Exception
            MsgBox("Error generating penalty receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
            CloseDB()
        End Try
    End Sub

    Private Function GetBookTitleByID(ByVal bookID As String) As String
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            cmd = New OleDbCommand("SELECT [Title] FROM books WHERE [Book ID] = ?", con)
            cmd.Parameters.AddWithValue("?", bookID)
            Dim result As Object = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return result.ToString()
            Else
                Return "Unknown Title"
            End If
        Catch ex As Exception
            Return "Unknown Title"
        End Try
    End Function

    Private Sub pbProfile_Click(sender As Object, e As EventArgs) Handles pbProfile.Click

    End Sub
End Class