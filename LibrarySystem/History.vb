Imports System.Data.OleDb
Imports System.Drawing

Public Class History

    Private Property ShowAllUsers As Boolean = False

    ' Determine if the logged-in user is admin
    Private ReadOnly Property IsAdmin As Boolean
        Get
            Return xpriv = "Admin"
        End Get
    End Property

    Private Sub History_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenDB()
        LoadBorrowData()
        CustomizeDataGridView(borrowdgv)

        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        ViewAllToolStripMenuItem.Enabled = IsAdmin
        SearchToolStripMenuItem.Enabled = IsAdmin AndAlso ShowAllUsers
        menuCheckTransaction.Enabled = True
    End Sub

    Private Sub History_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        ShowAllUsers = False

        ViewAllToolStripMenuItem.Enabled = IsAdmin
        SearchToolStripMenuItem.Enabled = False
        menuCheckTransaction.Enabled = True

        If IsAdmin Then
            CancelBorrowRequestToolStripMenuItem.Visible = False
        Else
            CancelBorrowRequestToolStripMenuItem.Visible = True
        End If

        ViewAllToolStripMenuItem.Text = "View All Users"

        LoadBorrowData()
    End Sub


    Private Sub LoadBorrowData(Optional ByVal searchName As String = "")
        Try
            If con.State <> ConnectionState.Open Then OpenDB()
            SQLQueryForHistoryUser(searchName)
            borrowdgv.DataSource = Nothing
            borrowdgv.DataSource = dbdsBorrowHistory.Tables("borrowings")
            borrowdgv.ReadOnly = True
            borrowdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            borrowdgv.ClearSelection()
        Catch ex As Exception
            MsgBox("Failed to load borrow history data. " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        If wasOnBorrowOrReturn Then
            Borrow.Visible = True
        Else
            frmmain.Show()
        End If
        wasOnBorrowOrReturn = False
        Me.Hide()
    End Sub

    Private Sub History_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

    Public Sub SQLQueryForHistoryUser(Optional ByVal searchName As String = "")
        Try
            Dim sql As String = "SELECT [Borrow ID], [Book ID List], [Borrower Name], [Borrower Position], [Borrower Privileges], " &
                                "[Copies], [Current Returned], [Borrow Date], [Due Date], [Return Date], [Status] FROM borrowings WHERE 1=1 "

            If IsAdmin AndAlso ShowAllUsers AndAlso Not String.IsNullOrEmpty(searchName) Then
                sql &= " AND [Borrower Name] LIKE ? "
            ElseIf Not IsAdmin OrElse (IsAdmin AndAlso Not ShowAllUsers) Then
                sql &= " AND [Borrower Name] = ? "
            End If

            If wasOnBorrowOrReturn Then
                sql &= " AND [Status] IN ('Requested', 'Borrowed') "
            Else
                sql &= " AND [Status] IN ('Requested','Borrowed','Completed','Cancelled') "
            End If

            sql &= " ORDER BY [Borrow ID] DESC"

            daBorrowHistory = New OleDbDataAdapter(sql, con)

            If IsAdmin AndAlso ShowAllUsers AndAlso Not String.IsNullOrEmpty(searchName) Then
                daBorrowHistory.SelectCommand.Parameters.AddWithValue("?", "%" & searchName.Trim() & "%")
            Else
                daBorrowHistory.SelectCommand.Parameters.AddWithValue("?", XName.Trim())
            End If

            If dbdsBorrowHistory Is Nothing Then
                dbdsBorrowHistory = New DataSet()
            Else
                If dbdsBorrowHistory.Tables.Contains("borrowings") Then
                    dbdsBorrowHistory.Tables("borrowings").Clear()
                End If
            End If

            daBorrowHistory.Fill(dbdsBorrowHistory, "borrowings")
        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Private Sub borrowdgv_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles borrowdgv.CellFormatting
        If e.ColumnIndex = borrowdgv.Columns("Book ID List").Index AndAlso e.Value IsNot Nothing Then
            Dim bookIDList As String = e.Value.ToString()
            If Not String.IsNullOrEmpty(bookIDList) Then
                Dim bookIDs() As String = bookIDList.Split(","c)
                e.Value = String.Join(vbCrLf, bookIDs)
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub menuCheckTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCheckTransaction.Click
        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)
            borrowID = selectedRow.Cells("Borrow ID").Value.ToString()
            names = selectedRow.Cells("Borrower Name").Value.ToString()
            position = selectedRow.Cells("Borrower Position").Value.ToString()
            privilege = selectedRow.Cells("Borrower Privileges").Value.ToString()
            borrowDate = selectedRow.Cells("Borrow Date").Value.ToString()
            dueDate = selectedRow.Cells("Due Date").Value.ToString()
            returnDate = If(selectedRow.Cells("Return Date").Value Is DBNull.Value, "", selectedRow.Cells("Return Date").Value.ToString())
            status = selectedRow.Cells("Status").Value.ToString()

            Dim transactionChecker As New TransactionChecker()
            transactionChecker.Show()
        Else
            MsgBox("Please select a transaction to check.", MsgBoxStyle.Exclamation, "Select Transaction")
        End If
    End Sub

    Private Sub menuCancelRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsAdmin Then
            MsgBox("Admins cannot cancel requests.", MsgBoxStyle.Exclamation, "Action Not Allowed")
            Exit Sub
        End If

        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)
            Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()
            Dim status As String = selectedRow.Cells("Status").Value.ToString()

            If status = "Requested" Then
                Dim confirm = MsgBox("Are you sure you want to cancel this request?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Cancel")
                If confirm = MsgBoxResult.Yes Then
                    Try
                        If con.State <> ConnectionState.Open Then OpenDB()
                        Dim sql As String = "UPDATE borrowings SET [Status] = 'Cancelled' WHERE [Borrow ID] = ?"
                        Using cmd As New OleDbCommand(sql, con)
                            cmd.Parameters.AddWithValue("?", borrowID)
                            cmd.ExecuteNonQuery()
                        End Using
                        MsgBox("Request cancelled successfully.", MsgBoxStyle.Information, "Cancelled")
                        LoadBorrowData()
                    Catch ex As Exception
                        MsgBox("Failed to cancel request. " & ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
            Else
                MsgBox("This transaction cannot be cancelled because it is already " & status & ".", MsgBoxStyle.Exclamation, "Not Allowed")
            End If
        Else
            MsgBox("Please select a request to cancel.", MsgBoxStyle.Exclamation, "Select Request")
        End If
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchToolStripMenuItem.Click
        If Not ShowAllUsers Then Exit Sub
        Dim inputName As String = InputBox("Enter Borrower Name to search:", "Search Borrower")
        If Not String.IsNullOrEmpty(inputName) Then
            LoadBorrowData(inputName)
        End If
    End Sub

    Private Sub ViewAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewAllToolStripMenuItem.Click
        ShowAllUsers = Not ShowAllUsers
        ViewAllToolStripMenuItem.Text = If(ShowAllUsers, "View Only Your Transactions", "View All Users")
        SearchToolStripMenuItem.Enabled = IsAdmin AndAlso ShowAllUsers

        LoadBorrowData()
    End Sub


    Private Sub CancelBorrowRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelBorrowRequestToolStripMenuItem.Click
        If IsAdmin Then
            MsgBox("Admins cannot cancel requests.", MsgBoxStyle.Exclamation, "Action Not Allowed")
            Exit Sub
        End If

        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)
            Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()
            Dim status As String = selectedRow.Cells("Status").Value.ToString()

            If status = "Requested" Then
                Dim confirm = MsgBox("Are you sure you want to cancel this request?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Cancel")
                If confirm = MsgBoxResult.Yes Then
                    Try
                        If con.State <> ConnectionState.Open Then OpenDB()
                        Dim sql As String = "UPDATE borrowings SET [Status] = 'Cancelled' WHERE [Borrow ID] = ?"
                        Using cmd As New OleDbCommand(sql, con)
                            cmd.Parameters.AddWithValue("?", borrowID)
                            cmd.ExecuteNonQuery()
                        End Using

                        MsgBox("Request cancelled successfully.", MsgBoxStyle.Information, "Cancelled")
                        LoadBorrowData()
                    Catch ex As Exception
                        MsgBox("Failed to cancel request. " & ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
            Else
                MsgBox("This transaction cannot be cancelled because it is already " & status & ".", MsgBoxStyle.Exclamation, "Not Allowed")
            End If
        Else
            MsgBox("Please select a request to cancel.", MsgBoxStyle.Exclamation, "Select Request")
        End If
    End Sub

    Private Sub borrowdgv_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles borrowdgv.SelectionChanged
        If borrowdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = borrowdgv.SelectedRows(0)
            Dim status As String = selectedRow.Cells("Status").Value.ToString()

            If Not IsAdmin AndAlso status = "Requested" Then
                CancelBorrowRequestToolStripMenuItem.Enabled = True
            Else
                CancelBorrowRequestToolStripMenuItem.Enabled = False
            End If
        Else
            CancelBorrowRequestToolStripMenuItem.Enabled = False
        End If
    End Sub


End Class
