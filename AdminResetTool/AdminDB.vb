Imports System.Data.OleDb
Imports System.IO

Module AdminDB
    Public daUser As OleDbDataAdapter
    Public dbds As DataSet

    Public Function GetAdminConnection() As OleDbConnection
        Dim exeFolder As String = Application.StartupPath

        Dim dbPath As String = Path.Combine(exeFolder, "Database", "library.mdb")

        If Not File.Exists(dbPath) Then
            MsgBox("Database not found at: " & dbPath, MsgBoxStyle.Critical, "Database Error")
            Return Nothing
        End If

        Dim conStr As String = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={dbPath};Persist Security Info=False;"

        Return New OleDbConnection(conStr)
    End Function


    Public Sub SQLQueryFortbluser()
        Try
            daUser = New OleDbDataAdapter("SELECT [User ID],[User Name],[Position],[Privileges] FROM tbluser", GetAdminConnection())
            dbds = New DataSet()
            daUser.Fill(dbds, "tbluser")
        Catch ex As Exception
            MsgBox("Error retrieving user data: " & ex.Message, MsgBoxStyle.Critical, "Query Error")
        End Try
    End Sub
End Module
