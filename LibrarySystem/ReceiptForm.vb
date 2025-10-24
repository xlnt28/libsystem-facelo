Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine

Public Class ReceiptForm
    Private Sub ReceiptForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BorrowReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrowReceiptToolStripMenuItem.Click
        GenerateBorrowReceipt()
    End Sub

    Private Sub ReturnReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnReceiptToolStripMenuItem.Click
        GenerateReturnReceipt()
    End Sub

    Private Sub PenaltyReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenaltyReceiptToolStripMenuItem.Click
        GeneratePenaltyReceipt()
    End Sub

    Private Sub GenerateBorrowReceipt()
        Try
            Dim receiptID As String = InputBox("Enter Receipt ID to generate borrow receipt:", "Borrow Receipt")

            If String.IsNullOrEmpty(receiptID) Then
                MsgBox("Operation cancelled.", MsgBoxStyle.Information, "Cancelled")
                Return
            End If

            receiptID = receiptID.Trim()

            OpenDB()

            ' Fetch from borrowReceipts table
            Dim query As String = "SELECT * FROM borrowReceipts WHERE [Receipt ID] = ?"
            Using da As New OleDbDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("?", receiptID)
                Dim dt As New DataTable()
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Dim reportForm As New ReportForm()
                    Dim report As New ReportDocument()
                    Dim reportPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CrystalReport2.rpt")

                    If Not System.IO.File.Exists(reportPath) Then
                        MsgBox("Borrow receipt report template not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                        CloseDB()
                        Return
                    End If

                    report.Load(reportPath)
                    report.SetDataSource(dt)
                    reportForm.CrystalReportViewer1.ReportSource = report
                    reportForm.ShowDialog()

                    report.Close()
                    report.Dispose()
                Else
                    MsgBox("Borrow Receipt ID '" & receiptID & "' not found.", MsgBoxStyle.Exclamation, "Not Found")
                End If
            End Using

            CloseDB()

        Catch ex As Exception
            MsgBox("Error generating borrow receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
            CloseDB()
        End Try
    End Sub

    Private Sub GenerateReturnReceipt()
        Try
            Dim receiptID As String = InputBox("Enter Receipt ID to generate return receipt:", "Return Receipt")

            If String.IsNullOrEmpty(receiptID) Then
                MsgBox("Operation cancelled.", MsgBoxStyle.Information, "Cancelled")
                Return
            End If

            receiptID = receiptID.Trim()

            OpenDB()

            ' Fetch from returnReceipts table
            Dim query As String = "SELECT * FROM returnReceipts WHERE [Receipt ID] = ?"
            Using da As New OleDbDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("?", receiptID)
                Dim dt As New DataTable()
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Dim reportForm As New ReportForm()
                    Dim report As New ReportDocument()
                    Dim reportPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CRReturnBook.rpt")

                    If Not System.IO.File.Exists(reportPath) Then
                        MsgBox("Return receipt report template not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                        CloseDB()
                        Return
                    End If

                    report.Load(reportPath)
                    report.SetDataSource(dt)
                    reportForm.CrystalReportViewer1.ReportSource = report
                    reportForm.ShowDialog()

                    report.Close()
                    report.Dispose()
                Else
                    MsgBox("Return Receipt ID '" & receiptID & "' not found.", MsgBoxStyle.Exclamation, "Not Found")
                End If
            End Using

            CloseDB()

        Catch ex As Exception
            MsgBox("Error generating return receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
            CloseDB()
        End Try
    End Sub

    Private Sub GeneratePenaltyReceipt()
        Try
            Dim receiptID As String = InputBox("Enter Receipt ID to generate penalty receipt:", "Penalty Receipt")

            If String.IsNullOrEmpty(receiptID) Then
                MsgBox("Operation cancelled.", MsgBoxStyle.Information, "Cancelled")
                Return
            End If

            receiptID = receiptID.Trim()

            OpenDB()

            ' Fetch from paymentReceipts table
            Dim query As String = "SELECT * FROM paymentReceipts WHERE [Receipt ID] = ?"
            Using da As New OleDbDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("?", receiptID)
                Dim dt As New DataTable()
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Dim reportForm As New ReportForm()
                    Dim report As New ReportDocument()
                    Dim reportPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CrystalReport1.rpt")

                    If Not System.IO.File.Exists(reportPath) Then
                        MsgBox("Penalty receipt report template not found: " & reportPath, MsgBoxStyle.Critical, "Error")
                        CloseDB()
                        Return
                    End If

                    report.Load(reportPath)
                    report.SetDataSource(dt)
                    reportForm.CrystalReportViewer1.ReportSource = report
                    reportForm.ShowDialog()

                    report.Close()
                    report.Dispose()
                Else
                    MsgBox("Penalty Receipt ID '" & receiptID & "' not found.", MsgBoxStyle.Exclamation, "Not Found")
                End If
            End Using

            CloseDB()

        Catch ex As Exception
            MsgBox("Error generating penalty receipt: " & ex.Message, MsgBoxStyle.Critical, "Error")
            CloseDB()
        End Try
    End Sub

    Private Sub menuLogout_Click(sender As Object, e As EventArgs) Handles menuLogout.Click
        frmmain.Show()
        Me.Close()
    End Sub
End Class