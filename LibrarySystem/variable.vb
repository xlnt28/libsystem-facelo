Imports System.Data.OleDb

Module variables
    Public con As OleDbConnection
    Public daUser As OleDbDataAdapter
    Public daBooks As OleDbDataAdapter
    Public daBorrowBooks As OleDbDataAdapter
    Public daBorrowHistory As OleDbDataAdapter
    Public dbds As DataSet
    Public dbdsbooks As DataSet
    Public dbdsBorrowBooks As DataSet
    Public dbdsBorrowHistory As DataSet
    Public cmd As OleDbCommand
    Public recpointer As Integer = 0

    Public admindbds As DataSet
    Public adminhistoryda As OleDbDataAdapter

    Public dsUserUnpaidList As DataSet
    Public daUserUnpaidList As OleDbDataAdapter

    Public XName As String
    Public xpost As String
    Public xpriv As String
    Public wasOnBorrowOrReturn As Boolean = False

    Public storedBookIDList As String
    Public storedCopiesList As String

    Public daUserReturn As OleDbDataAdapter
    Public dsUserReturn As DataSet

    Public Sub CustomizeDataGridView(ByVal dg As DataGridView)

        dg.EnableHeadersVisualStyles = False
        dg.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
        dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dg.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dg.RowTemplate.Height = 10

        Dim cellStyle As New DataGridViewCellStyle()
        cellStyle.Padding = New Padding(7)
        cellStyle.WrapMode = DataGridViewTriState.True
        cellStyle.Font = New Font("Verdana", 8.5, FontStyle.Regular)
        dg.DefaultCellStyle = cellStyle
        dg.AlternatingRowsDefaultCellStyle.BackColor = Color.SeaShell

    End Sub

    Public Sub OpenDB()
        Try
            Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" &
                                     Application.StartupPath & "\Database\library.mdb"
            con = New OleDbConnection(connStr)

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
        Catch ex As Exception
            MsgBox("Error opening database: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    Public Sub SQLQueryFortbluser()
        Try
            daUser = New OleDbDataAdapter("SELECT [User ID],[User Name],[Position],[Privileges] FROM tbluser", con)
            dbds = New DataSet()
            daUser.Fill(dbds, "tbluser")
        Catch ex As Exception
            MsgBox("Error retrieving user data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Public Sub SQLQueryForBooks()
        Try
            daBooks = New OleDbDataAdapter("SELECT " & _
                                         "[Book ID], " & _
                                         "[ISBN], " & _
                                         "[Title], " & _
                                         "[Author], " & _
                                         "[Publisher], " & _
                                         "[Publication Year], " & _
                                         "[Category], " & _
                                         "[Quantity], " & _
                                         "[Status] " & _
                                         "FROM books ORDER BY [Book ID] ASC", con)
            If dbdsbooks Is Nothing Then
                dbdsbooks = New DataSet()
            Else
                dbdsbooks.Tables("books").Clear()
            End If
            daBooks.Fill(dbdsbooks, "books")
        Catch ex As Exception
            MsgBox("Error retrieving books data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Public Sub SQLQueryForBorrowBooks()
        Try
            daBorrowBooks = New OleDbDataAdapter("SELECT " & _
                                               "[Book ID], " & _
                                               "[ISBN], " & _
                                               "[Title], " & _
                                               "[Author], " & _
                                               "[Publisher], " & _
                                               "[Publication Year], " & _
                                               "[Category], " & _
                                               "[Quantity], " & _
                                               "[Status] " & _
                                               "FROM books " & _
                                               "ORDER BY [Book ID] ASC", con)

            If dbdsBorrowBooks Is Nothing Then
                dbdsBorrowBooks = New DataSet()
            Else
                dbdsBorrowBooks.Tables("books").Clear()
            End If
            daBorrowBooks.Fill(dbdsBorrowBooks, "books")
        Catch ex As Exception
            MsgBox("Error retrieving borrowable books data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub

    Public Function GetUserIdByUsername(ByVal username As String) As String
        Dim userId As String = ""
        Try

            OpenDB()
            Dim query As String = "SELECT [User ID] FROM tbluser WHERE [User Name] = ?"
            cmd = New OleDbCommand(query, con)
            cmd.Parameters.AddWithValue("User Name", username)

            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                userId = result.ToString()
            End If

        Catch ex As Exception
            MessageBox.Show("Error retrieving user ID: " & ex.Message)
        Finally

            CloseDB()
        End Try

        Return userId
    End Function

    Public Sub CloseDB()
        If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Public Sub SQLQueryForBorrowings()
        Try
            daBorrowHistory = New OleDbDataAdapter("SELECT " & _
                                                "[Borrow ID], " & _
                                                "[Book ID List], " & _
                                                "[Borrower Name], " & _
                                                "[Borrower Position], " & _
                                                "[Borrower Privileges], " & _
                                                "[Copies], " & _
                                                "[Borrow Date], " & _
                                                "[Due Date], " & _
                                                "[Return Date], " & _
                                                "[Status] " & _
                                                "FROM borrowings " & _
                                                "WHERE [Borrower Name] = '" & XName & "' " & _
                                                "ORDER BY [Borrow ID] DESC", con)
            If dbdsBorrowHistory Is Nothing Then
                dbdsBorrowHistory = New DataSet()
            Else
                dbdsBorrowHistory.Tables("borrowings").Clear()
            End If
            daBorrowHistory.Fill(dbdsBorrowHistory, "borrowings")
        Catch ex As Exception
            MsgBox("Error retrieving borrowings data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
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

    Public Function GetBookTitleByID(ByVal bookID As String) As String
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
            Return "Error retrieving title"
        End Try
    End Function
End Module