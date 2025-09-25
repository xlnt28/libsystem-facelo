Imports System.Data.OleDb

Public Class BorrowPendingRequest

    Private Sub BorrowPendingRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenDB()
        LoadPendingRequests()
        CustomizeDataGridView(dg)
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub adminHistory_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        LoadPendingRequests()
    End Sub

    Private Sub LoadPendingRequests()
        Try
            SQLQueryForBorrowPendingRequest()
            dg.DataSource = admindbds.Tables("borrowings")
            dg.ReadOnly = True
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dg.ClearSelection()

            AddHandler dg.CellFormatting, AddressOf dg_CellFormatting
        Catch ex As Exception
            MsgBox("Error loading pending requests: " & ex.Message, MsgBoxStyle.Critical, "Load Error")
        End Try
    End Sub

    Public Sub SQLQueryForBorrowPendingRequest()
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
                                                "[Borrow Date], " & _
                                                "[Due Date], " & _
                                                "[Status], " & _
                                                "[Request Date] " & _
                                                "FROM borrowings " & _
                                                "WHERE status = 'Requested' " & _
                                                "ORDER BY [Request Date] DESC", con)

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
            If e.ColumnIndex = dg.Columns("Book ID List").Index AndAlso e.Value IsNot Nothing Then
                Dim bookIDList As String = e.Value.ToString()
                If Not String.IsNullOrEmpty(bookIDList) Then
                    Dim bookIDs() As String = bookIDList.Split(","c)
                    e.Value = String.Join(vbCrLf, bookIDs)
                    e.FormattingApplied = True
                End If
            End If

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

    Private Sub ApproveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApproveToolStripMenuItem.Click
        If dg.SelectedRows.Count = 0 Then
            MsgBox("Please select a request to approve.", MsgBoxStyle.Exclamation, "Select Request")
            Return
        End If

        Dim selectedRow As DataGridViewRow = dg.SelectedRows(0)
        Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()
        Dim bookIDList As String = selectedRow.Cells("Book ID List").Value.ToString()
        Dim copiesList As String = selectedRow.Cells("Copies").Value.ToString()

        If MessageBox.Show("Are you sure you want to approve this borrow request?" & vbCrLf & "Borrow ID: " & borrowID, "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim currentDate As DateTime = DateTime.Now
                Dim dueDate As DateTime = Borrow.userReturnDate

                Dim formattedCurrentDate As String = currentDate.ToString("MM/dd/yyyy hh:mm tt")
                Dim formattedDueDate As String = dueDate.ToString("MM/dd/yyyy hh:mm tt")

                cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Borrowed', [Borrow Date] = ?, [Due Date] = ? WHERE [Borrow ID] = ?", con)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = formattedCurrentDate
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = formattedDueDate
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = borrowID
                cmd.ExecuteNonQuery()


                Dim bookIDs() As String = bookIDList.Split(","c)
                Dim quantities() As String = copiesList.Split(","c)

                For i As Integer = 0 To bookIDs.Length - 1
                    Dim bookID As String = bookIDs(i).Trim()
                    Dim quantityToBorrow As Integer = Integer.Parse(quantities(i).Trim())

                    cmd = New OleDbCommand("SELECT [Quantity] FROM books WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", bookID)
                    Dim currentQuantity As Integer = 0
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        currentQuantity = CInt(result)
                    End If

                    Dim newQuantity As Integer = currentQuantity - quantityToBorrow
                    cmd = New OleDbCommand("UPDATE books SET [Quantity] = ?, [Status] = IIF(? > 0, 'Available', 'Unavailable') WHERE [Book ID] = ?", con)
                    cmd.Parameters.AddWithValue("?", newQuantity)
                    cmd.Parameters.AddWithValue("?", newQuantity)
                    cmd.Parameters.AddWithValue("?", bookID)
                    cmd.ExecuteNonQuery()
                Next

                MsgBox("Borrow request approved successfully!", MsgBoxStyle.Information, "Approval Complete")
                LoadPendingRequests()

            Catch ex As Exception
                MsgBox("Error approving request: " & ex.Message, MsgBoxStyle.Critical, "Approval Error")
            End Try
        End If
    End Sub

    Private Sub RejectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectToolStripMenuItem.Click
        If dg.SelectedRows.Count = 0 Then
            MsgBox("Please select a request to reject.", MsgBoxStyle.Exclamation, "Select Request")
            Return
        End If

        Dim selectedRow As DataGridViewRow = dg.SelectedRows(0)
        Dim borrowID As String = selectedRow.Cells("Borrow ID").Value.ToString()

        If MessageBox.Show("Are you sure you want to decline this borrow request?" & vbCrLf & "Borrow ID: " & borrowID, "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                cmd = New OleDbCommand("UPDATE borrowings SET [Status] = 'Declined' WHERE [Borrow ID] = ?", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                cmd.ExecuteNonQuery()

                MsgBox("Borrow request declined successfully!", MsgBoxStyle.Information, "Decline Complete")
                LoadPendingRequests()

            Catch ex As Exception
                MsgBox("Error rejecting request: " & ex.Message, MsgBoxStyle.Critical, "Decline Error")
            End Try
        End If
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Show()
        Me.Hide()
    End Sub

    Private Sub ViewDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewDetailsToolStripMenuItem.Click
        If dg.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dg.SelectedRows(0)

            borrowID = If(selectedRow.Cells("Borrow ID").Value IsNot Nothing, selectedRow.Cells("Borrow ID").Value.ToString(), "")
            names = If(selectedRow.Cells("Borrower Name").Value IsNot Nothing, selectedRow.Cells("Borrower Name").Value.ToString(), "")
            position = If(selectedRow.Cells("Borrower Position").Value IsNot Nothing, selectedRow.Cells("Borrower Position").Value.ToString(), "")
            privilege = If(selectedRow.Cells("Borrower Privileges").Value IsNot Nothing, selectedRow.Cells("Borrower Privileges").Value.ToString(), "")

            storedCopiesList = If(selectedRow.Cells("Copies").Value IsNot Nothing, selectedRow.Cells("Copies").Value.ToString(), "")
            borrowDate = If(selectedRow.Cells("Borrow Date").Value IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells("Borrow Date").Value),
                           Convert.ToDateTime(selectedRow.Cells("Borrow Date").Value).ToString("MM/dd/yyyy"), "")

            dueDate = If(selectedRow.Cells("Due Date").Value IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells("Due Date").Value),
                        Convert.ToDateTime(selectedRow.Cells("Due Date").Value).ToString("MM/dd/yyyy"), "")

            status = If(selectedRow.Cells("Status").Value IsNot Nothing, selectedRow.Cells("Status").Value.ToString(), "")

            Dim transactionChecker As New TransactionChecker()
            transactionChecker.Show()
        Else
            MsgBox("Please select a request to view details.", MsgBoxStyle.Exclamation, "Select Request")
        End If
    End Sub

    Private Sub BorrowPendingRequest_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

End Class