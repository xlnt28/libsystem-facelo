Imports System.Data.OleDb

Public Class adminHistory
    Private Sub adminHistory_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CloseDB()
    End Sub

    Private Sub adminHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenDB()
        loadAllBorrowedHistory()
        CustomizeDataGridView(bookdgv)
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub adminHistory_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        loadAllBorrowedHistory()
    End Sub


    Public Sub loadAllBorrowedHistory()
        Try
            SQLQueryForBorrowBooksHistory()
            bookdgv.DataSource = admindbds.Tables("borrowings")
            bookdgv.ReadOnly = True
            bookdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            bookdgv.ClearSelection()
        Catch ex As Exception
            MsgBox("Error loading borrowed history: " & ex.Message, MsgBoxStyle.Critical, "Load Error")
        End Try
    End Sub

    Public Sub SQLQueryForBorrowBooksHistory()
        Try
            If daBorrowHistory Is Nothing Then
                daBorrowHistory = New OleDbDataAdapter()
            End If

            daBorrowHistory.SelectCommand = New OleDbCommand("SELECT " & _
                                                "[Borrow ID], " & _
                                                "[Book ID List], " & _
                                                "[Borrower Name], " & _
                                                "[Borrower Position], " & _
                                                "[Borrower Privileges], " & _
                                                "[Copies], " & _
                                                "[Current Returned], " & _
                                                "[Borrow Date], " & _
                                                "[Due Date], " & _
                                                "[Return Date], " & _
                                                "[Status] " & _
                                                "FROM borrowings " & _
                                                "ORDER BY [Borrow ID] DESC", con)

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

    Private Sub menuGoBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuGoBack.Click
        frmmain.Show()
        Me.Hide()
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        PerformSearch()
    End Sub

    Private Sub PerformSearch()
        Dim searchTerm As String = InputBox("Enter Borrower Name or Book ID to search:", "Search Borrowing History")

        If String.IsNullOrEmpty(searchTerm) Then
            Return
        End If

        Try
            Dim dv As New DataView(admindbds.Tables("borrowings"))
            dv.RowFilter = String.Format("[Borrower Name] LIKE '%{0}%' OR [Book ID List] LIKE '%{0}%'", searchTerm.Replace("'", "''"))

            If dv.Count = 0 Then
                MsgBox("No records found for: " & searchTerm, MsgBoxStyle.Information, "Search Results")
                loadAllBorrowedHistory()
            Else
                bookdgv.DataSource = dv
            End If

        Catch ex As Exception
            MsgBox("Error performing search: " & ex.Message, MsgBoxStyle.Critical, "Search Error")
        End Try
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        loadAllBorrowedHistory()
    End Sub

    Private Sub bookdgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles bookdgv.CellContentClick

    End Sub

    Private Sub menuCheckTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCheckTransaction.Click
        If bookdgv.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = bookdgv.SelectedRows(0)

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

    Private Sub bookdgv_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles bookdgv.CellFormatting
        If e.ColumnIndex = bookdgv.Columns("Book ID List").Index AndAlso e.Value IsNot Nothing Then
            Dim bookIDList As String = e.Value.ToString()

            If Not String.IsNullOrEmpty(bookIDList) Then
                Dim bookIDs() As String = bookIDList.Split(","c)
                e.Value = String.Join(vbCrLf, bookIDs)
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub bookdgv_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles bookdgv.CellDoubleClick
        If e.RowIndex >= 0 Then
            menuCheckTransaction_Click(Nothing, Nothing)
        End If
    End Sub
End Class