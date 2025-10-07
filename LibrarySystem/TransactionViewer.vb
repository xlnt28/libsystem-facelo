Imports System.Data.OleDb

Public Class TransactionViewer

    Private Sub TransactionViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dgv.Columns.Clear()

        dgv.Columns.Add("colTitle", "Book Title")
        dgv.Columns.Add("colCopies", "Copies")

        dgv.ReadOnly = True
        dgv.RowHeadersVisible = False
        CustomizeDataGridView(dgv)

        LoadTransactionDetails()
        LoadBooksData()
    End Sub

    Private Sub LoadTransactionDetails()
        txtBorrowID.Text = selectedBorrowID
        txtUserID.Text = selectedUserID
        txtBorrowerName.Text = selectedBorrowerName
        txtBorrowerPosition.Text = selectedBorrowerPosition
        txtBorrowerPrivilege.Text = selectedBorrowerPrivilege
        txtBorrowDate.Text = selectedBorrowDate
        txtDueDate.Text = selectedDueDate
        txtStatus.Text = selectedStatus
    End Sub

    Private Sub LoadBooksData()
        dgv.Rows.Clear()

        If String.IsNullOrEmpty(selectedBookIDList) Then
            Exit Sub
        End If

        Dim bookIDs() As String = selectedBookIDList.Split(","c)
        Dim copies() As String = selectedCopyList.Split(","c)

        For i As Integer = 0 To bookIDs.Length - 1
            Dim bookID As String = bookIDs(i).Trim()
            If String.IsNullOrEmpty(bookID) Then Continue For

            Dim copy As String = If(i < copies.Length, copies(i).Trim(), "1")
            Dim title As String = GetBookTitle(bookID)

            dgv.Rows.Add(title, copy)
        Next
    End Sub

    Private Function GetBookTitle(bookID As String) As String
        Try
            If con.State <> ConnectionState.Open Then OpenDB()

            Dim sql As String = "SELECT [Title] FROM books WHERE [Book ID] = ?"
            Using cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("?", bookID)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return result.ToString()
                End If
            End Using
        Catch ex As Exception
        End Try

        Return "Unknown Title"
    End Function

    Private Sub GoBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoBackToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class