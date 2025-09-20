Imports System.Data.OleDb

Public Class TransactionChecker
    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        txtName.Clear()
        txtPosition.Clear()
        txtPrivilege.Clear()
        txtBorrowDate.Clear()
        txtDueDate.Clear()
        txtReturnDate.Clear()
        txtBorrowID.Clear()
        txtStatus.Clear()
        rtxtAllBorrowed.Clear()
        Me.Close()
    End Sub

    Private Sub TransactionChecker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        LoadBorrowerDetails()
        LoadBorrowedBooks()
    End Sub

    Private Sub LoadBorrowerDetails()
        txtName.Text = names
        txtPosition.Text = position
        txtPrivilege.Text = privilege
        txtBorrowDate.Text = borrowDate
        txtDueDate.Text = dueDate
        txtReturnDate.Text = returnDate
        txtBorrowID.Text = borrowID
        txtStatus.Text = status
    End Sub

    Private Sub LoadBorrowedBooks()
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Using cmd As New OleDbCommand("SELECT [Book ID List], [Copies] FROM borrowings WHERE [Borrow ID] = ?", con)
                cmd.Parameters.AddWithValue("?", borrowID)
                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim bookIDList As String = reader("Book ID List").ToString()
                        Dim copiesList As String = reader("Copies").ToString()

                        Dim bookIDs() As String = bookIDList.Split(","c)
                        Dim copies() As String = copiesList.Split(","c)

                        rtxtAllBorrowed.Clear()

                        For i As Integer = 0 To bookIDs.Length - 1
                            Dim bookID As String = bookIDs(i).Trim()
                            Dim title As String = GetBookTitleByID(bookID)

                            If i < copies.Length Then
                                Dim copyCount As String = copies(i).Trim()
                                rtxtAllBorrowed.AppendText(title & " (" & copyCount & ")" & Environment.NewLine & Environment.NewLine)
                            Else
                                rtxtAllBorrowed.AppendText(title & " (0)" & Environment.NewLine & Environment.NewLine)
                            End If
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            rtxtAllBorrowed.Text = "Error loading borrowed books: " & ex.Message
        End Try
    End Sub

    Private Function GetBookTitleByID(ByVal bookID As String) As String
        Try
            If con.State <> ConnectionState.Open Then
                OpenDB()
            End If

            Using cmd As New OleDbCommand("SELECT [Title] FROM books WHERE [Book ID] = ?", con)
                cmd.Parameters.AddWithValue("?", bookID)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return result.ToString()
                End If
            End Using
        Catch
        End Try
        Return "Book ID: " & bookID
    End Function
End Class
