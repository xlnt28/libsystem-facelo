Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb

Public Class ReportForm

    Private Sub ReportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized


        Try
            Dim dbPath As String = Application.StartupPath & "\Database\library.mdb"
            Dim connStr As String = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={dbPath};"
            Dim dt As New DataTable()

            Using conn As New OleDbConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT [PenaltyID], [Borrow ID], [Book ID], [Quantity], [Days Late], " &
                      "[Penalty Amount], [Due Date], [Return Date], [User Name], [Book Condition], [Condition Penalty] " &
                      "FROM Penalties " &
                      "ORDER BY [PenaltyID] ASC"



                Dim adapter As New OleDbDataAdapter(query, conn)
                adapter.Fill(dt)
            End Using

            Dim rpt As New CrystalReport1()

            rpt.SetDataSource(dt)
            For Each subRpt As ReportDocument In rpt.Subreports
                subRpt.SetDataSource(dt)
            Next

            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.RefreshReport()

        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub menuLogout_Click(sender As Object, e As EventArgs) Handles menuLogout.Click
        Me.Close()
    End Sub
End Class
