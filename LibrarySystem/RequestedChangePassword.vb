Imports System.Data.OleDb

Public Class RequestedChangePassword
    Private selectedRequestID As Integer = -1
    Private pendingOnlyView As Boolean = False

    Private Sub RequestedChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        UpdateExpiredRequests()
        LoadRequests()
        CustomizeDataGridView(dgv)

    End Sub

    Private Sub AutoSelectFirstRow()
        If dgv.Rows.Count > 0 Then
            dgv.ClearSelection()
            dgv.Rows(0).Selected = True
            selectedRequestID = GetRequestIDFromRow(0)
        Else
            selectedRequestID = -1
        End If
    End Sub

    Private Function GetRequestIDFromRow(rowIndex As Integer) As Integer
        If rowIndex >= 0 AndAlso rowIndex < dgv.Rows.Count Then
            Try
                Dim val = dgv.Rows(rowIndex).Cells("RequestID").Value
                If Not IsDBNull(val) AndAlso val IsNot Nothing Then
                    Return CInt(val)
                End If
            Catch ex As Exception
            End Try
        End If
        Return -1
    End Function

    Private Sub UpdateExpiredRequests()
        Try
            OpenDB()
            Dim query As String =
                "UPDATE PasswordResetRequests SET Status = 'Expired' " &
                "WHERE ExpiryDate < ? AND Status = 'Approved'"

            Using cmd As New OleDbCommand(query, con)
                cmd.Parameters.Add("@Now", OleDbType.Date).Value = Now
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MsgBox("Error updating expired requests: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub LoadRequests(Optional filter As String = "")
        Try
            OpenDB()

            Dim baseQuery As String =
                "SELECT RequestID, UserID, UserName, PhoneNumber, ResetCode, RequestDate, ExpiryDate, Status " &
                "FROM PasswordResetRequests WHERE 1=1"

            If pendingOnlyView Then
                baseQuery &= " AND Status = 'Pending'"
            Else
                baseQuery &= " AND Status IN ('Pending','Approved')"
            End If

            If Not String.IsNullOrEmpty(filter) Then
                baseQuery &= " AND (UserID LIKE ? OR UserName LIKE ?)"
            End If

            baseQuery &= " ORDER BY RequestDate DESC"

            Using cmd As New OleDbCommand(baseQuery, con)
                If Not String.IsNullOrEmpty(filter) Then
                    cmd.Parameters.AddWithValue("@Search1", "%" & filter & "%")
                    cmd.Parameters.AddWithValue("@Search2", "%" & filter & "%")
                End If

                Dim da As New OleDbDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgv.DataSource = dt
            End Using

            ' Auto-select first row after loading data
            AutoSelectFirstRow()

        Catch ex As Exception
            MsgBox("Error loading reset requests: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex >= 0 Then
            selectedRequestID = GetRequestIDFromRow(e.RowIndex)
        End If
    End Sub

    Private Sub ApproveRequestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApproveRequestToolStripMenuItem.Click
        If selectedRequestID = -1 Then
            MsgBox("Please select a request first.", vbExclamation)
            Return
        End If

        Try
            OpenDB()
            Dim query As String =
                "UPDATE PasswordResetRequests " &
                "SET Status = 'Approved', ProcessedDate = ?, ExpiryDate = ? " &
                "WHERE RequestID = ? AND Status = 'Pending'"

            Using cmd As New OleDbCommand(query, con)
                cmd.Parameters.Add("@ProcessedDate", OleDbType.Date).Value = Now
                cmd.Parameters.Add("@ExpiryDate", OleDbType.Date).Value = DateAdd("h", 24, Now)
                cmd.Parameters.Add("@RequestID", OleDbType.Integer).Value = selectedRequestID

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox("Request approved successfully. Code valid for 24 hours.", vbInformation)
                Else
                    MsgBox("Request is already processed or not found.", vbExclamation)
                End If
            End Using

            UpdateExpiredRequests()
            LoadRequests()

        Catch ex As Exception
            MsgBox("Error approving request: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DeclineRequestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeclineRequestToolStripMenuItem.Click
        If selectedRequestID = -1 Then
            MsgBox("Please select a request first.", vbExclamation)
            Return
        End If

        Try
            OpenDB()
            Dim query As String =
                "UPDATE PasswordResetRequests " &
                "SET Status = 'Declined', ProcessedDate = ? " &
                "WHERE RequestID = ? AND Status = 'Pending'"

            Using cmd As New OleDbCommand(query, con)
                cmd.Parameters.Add("@ProcessedDate", OleDbType.Date).Value = Now
                cmd.Parameters.Add("@RequestID", OleDbType.Integer).Value = selectedRequestID

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox("Request declined successfully.", vbInformation)
                Else
                    MsgBox("Request is already processed or not found.", vbExclamation)
                End If
            End Using

            UpdateExpiredRequests()
            LoadRequests()

        Catch ex As Exception
            MsgBox("Error declining request: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click
        Dim input As String = InputBox("Enter User ID or Name to search:", "Search Requests")
        If input.Trim() <> "" Then
            LoadRequests(input)
        Else
            LoadRequests()
        End If
    End Sub

    Private Sub ViewPendingOnlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewPendingOnlyToolStripMenuItem.Click
        pendingOnlyView = Not pendingOnlyView
        ViewPendingOnlyToolStripMenuItem.Checked = pendingOnlyView
        LoadRequests()
    End Sub

    Private Sub GoBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoBackToolStripMenuItem.Click
        frmmain.Show()
        Me.Hide()
    End Sub

    Private Sub RequestedChangePassword_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        LoadRequests()
    End Sub
End Class