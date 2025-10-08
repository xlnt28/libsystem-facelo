Imports System.Data.OleDb

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

    Private Sub dashAdminPan_Paint(sender As Object, e As PaintEventArgs) Handles dashAdminPan.Paint

    End Sub

    Private Sub ReturnReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnReceiptToolStripMenuItem.Click

    End Sub

    Private Sub BorrowReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrowReceiptToolStripMenuItem.Click

    End Sub

    Private Sub PenaltyReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenaltyReceiptToolStripMenuItem.Click

    End Sub
End Class