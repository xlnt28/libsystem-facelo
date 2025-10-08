Public Class Reports
    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.WindowState = FormWindowState.Maximized
        ' Initialize report viewer settings
        CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    End Sub

    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Load
        ' Crystal Report Viewer initialization
        CrystalReportViewer1.ReuseAppDomain = False
        CrystalReportViewer1.ReuseParameterValuesOnRefresh = True
    End Sub

    Private Sub GetReturnReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Try
            Dim transactionID As String = InputBox("Enter Transaction ID for Return Receipt:", "Return Receipt")

            If String.IsNullOrEmpty(transactionID) Then
                MessageBox.Show("Transaction ID is required.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Validate if transaction exists and is a return transaction
            If ValidateReturnTransaction(transactionID) Then
                GenerateReturnReceipt(transactionID)
            Else
                MessageBox.Show("Invalid transaction ID or not a return transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error generating return receipt: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetPenaltyReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Try
            Dim transactionID As String = InputBox("Enter Transaction ID for Penalty Receipt:", "Penalty Receipt")

            If String.IsNullOrEmpty(transactionID) Then
                MessageBox.Show("Transaction ID is required.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Validate if transaction has penalty
            If ValidatePenaltyTransaction(transactionID) Then
                GeneratePenaltyReceipt(transactionID)
            Else
                MessageBox.Show("No penalty found for this transaction.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error generating penalty receipt: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetBorrowReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Try
            Dim transactionID As String = InputBox("Enter Transaction ID for Borrow Receipt:", "Borrow Receipt")

            If String.IsNullOrEmpty(transactionID) Then
                MessageBox.Show("Transaction ID is required.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Validate if transaction exists and is a borrow transaction
            If ValidateBorrowTransaction(transactionID) Then
                GenerateBorrowReceipt(transactionID)
            Else
                MessageBox.Show("Invalid transaction ID or not a borrow transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error generating borrow receipt: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateReturnTransaction(transactionID As String) As Boolean
        Try
            OpenDB()
            Dim sql As String = "SELECT COUNT(*) FROM tbltransaction WHERE [Transaction ID] = ? AND [Status] = 'Returned'"
            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", transactionID)
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ValidatePenaltyTransaction(transactionID As String) As Boolean
        Try
            OpenDB()
            Dim sql As String = "SELECT COUNT(*) FROM tblpenalty WHERE [Transaction ID] = ? AND [Status] = 'Unpaid'"
            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", transactionID)
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ValidateBorrowTransaction(transactionID As String) As Boolean
        Try
            OpenDB()
            Dim sql As String = "SELECT COUNT(*) FROM tbltransaction WHERE [Transaction ID] = ? AND [Status] = 'Borrowed'"
            cmd = New OleDbCommand(sql, con)
            cmd.Parameters.AddWithValue("?", transactionID)
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub GenerateReturnReceipt(transactionID As String)
        Try
            Dim report As New ReturnReceiptReport()

            ' Set parameter for the report
            report.SetParameterValue("@TransactionID", transactionID)

            ' Set the report source
            CrystalReportViewer1.ReportSource = report
            CrystalReportViewer1.Refresh()

            MessageBox.Show("Return receipt generated for Transaction ID: " & transactionID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Throw New Exception("Failed to generate return receipt: " & ex.Message)
        End Try
    End Sub

    Private Sub GeneratePenaltyReceipt(transactionID As String)
        Try
            Dim report As New PenaltyReceiptReport()

            ' Set parameter for the report
            report.SetParameterValue("@TransactionID", transactionID)

            ' Set the report source
            CrystalReportViewer1.ReportSource = report
            CrystalReportViewer1.Refresh()

            MessageBox.Show("Penalty receipt generated for Transaction ID: " & transactionID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Throw New Exception("Failed to generate penalty receipt: " & ex.Message)
        End Try
    End Sub

    Private Sub GenerateBorrowReceipt(transactionID As String)
        Try
            Dim report As New BorrowReceiptReport()

            ' Set parameter for the report
            report.SetParameterValue("@TransactionID", transactionID)

            ' Set the report source
            CrystalReportViewer1.ReportSource = report
            CrystalReportViewer1.Refresh()

            MessageBox.Show("Borrow receipt generated for Transaction ID: " & transactionID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Throw New Exception("Failed to generate borrow receipt: " & ex.Message)
        End Try
    End Sub

    Private Sub menuLogout_Click(sender As Object, e As EventArgs) Handles menuLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Clear any report being displayed
            CrystalReportViewer1.ReportSource = Nothing

            ' Show login form or main form based on your application structure
            Dim loginForm As New Login() ' Adjust to your login form name
            loginForm.Show()
            Me.Close()
        End If
    End Sub

    ' Method to clear current report
    Public Sub ClearReport()
        CrystalReportViewer1.ReportSource = Nothing
        CrystalReportViewer1.Refresh()
    End Sub

    ' Method to handle form closing
    Private Sub Reports_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Clean up Crystal Report resources
        If CrystalReportViewer1.ReportSource IsNot Nothing Then
            CrystalReportViewer1.ReportSource.Close()
            CrystalReportViewer1.ReportSource.Dispose()
        End If
    End Sub
End Class