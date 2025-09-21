Imports System.Data.OleDb
Imports System.Drawing

Public Class PartialReturnForm
    Public Property BookIDs() As String()
    Public Property TotalCopies() As Integer()
    Public Property CurrentReturned() As Integer()
    Public Property BorrowID As String

    Private numericControls As New List(Of NumericUpDown)()
    Private bookTitles As New List(Of String)()
    Private bookPanels As New List(Of Panel)()
    Private penaltyLabels As New List(Of Label)()

    Private BorrowDate As DateTime
    Public DueDate As DateTime
    Public daysLate As Integer = 0
    Private totalPenalty As Decimal = 0
    Private totalQuantity As Integer = 0
    Private totalPenaltyLabel As Label
    Private ReadOnly Property ReturnDate As DateTime
        Get
            Return DateTime.Today
        End Get
    End Property

    Public Sub LoadBorrowInfoFromDGV(ByVal dgv As DataGridView, ByVal borrowID As String)
        Me.BorrowID = borrowID
        Dim row = dgv.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(Function(r) r.Cells("Borrow ID").Value.ToString() = borrowID)
        If row Is Nothing Then
            MessageBox.Show("Borrow record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Try
            If row.Cells("Borrow Date").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("Borrow Date").Value) Then
                Dim borrowDateValue = row.Cells("Borrow Date").Value
                If TypeOf borrowDateValue Is DateTime Then
                    BorrowDate = CType(borrowDateValue, DateTime)
                Else
                    Dim borrowDateText = borrowDateValue.ToString()
                    If Not DateTime.TryParse(borrowDateText, BorrowDate) Then
                        BorrowDate = DateTime.Today
                    End If
                End If
            Else
                BorrowDate = DateTime.Today
            End If
            If row.Cells("Due Date").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("Due Date").Value) Then
                Dim dueDateValue = row.Cells("Due Date").Value
                If TypeOf dueDateValue Is DateTime Then
                    DueDate = CType(dueDateValue, DateTime)
                Else
                    Dim dueDateText = dueDateValue.ToString()
                    If Not DateTime.TryParse(dueDateText, DueDate) Then
                        DueDate = BorrowDate.AddDays(7)
                    End If
                End If
            Else
                DueDate = BorrowDate.AddDays(7)
            End If
        Catch ex As Exception
            BorrowDate = DateTime.Today
            DueDate = BorrowDate.AddDays(7)
        End Try
    End Sub

    Public Function GetReturnQuantities() As Integer()
        Dim results(BookIDs.Length - 1) As Integer
        For i As Integer = 0 To BookIDs.Length - 1
            If i < numericControls.Count Then
                results(i) = CInt(numericControls(i).Value)
            Else
                results(i) = 0
            End If
        Next
        Return results
    End Function

    Public Function GetTotalPenalty() As Decimal
        Return totalPenalty
    End Function

    Private Sub PartialReturnForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.White
        Me.Font = New Font("Segoe UI", 9)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "Partial Return"
        Me.Padding = New Padding(20)
        Me.MinimumSize = New Size(700, 400)

        If BorrowDate = Nothing Then
            BorrowDate = DateTime.Today
        End If
        If DueDate = Nothing Then
            DueDate = BorrowDate.AddDays(7)
        End If

        daysLate = Math.Max(0, (ReturnDate - DueDate).Days)

        GetBookTitles()

        Dim buttonPanel As New Panel With {.Dock = DockStyle.Bottom, .Height = 70, .BackColor = Color.White}
        Me.Controls.Add(buttonPanel)

        Dim mainPanel As New Panel With {.Dock = DockStyle.Fill, .AutoScroll = True, .BackColor = Color.White}
        Me.Controls.Add(mainPanel)
        mainPanel.BringToFront()

        Dim btnOK As New Button With {.Text = "OK", .Size = New Size(100, 35), .Location = New Point(buttonPanel.Width - 220, 20), .BackColor = Color.FromArgb(0, 123, 255), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler btnOK.Click, AddressOf btnOK_Click
        buttonPanel.Controls.Add(btnOK)

        Dim btnCancel As New Button With {.Text = "Cancel", .Size = New Size(100, 35), .Location = New Point(buttonPanel.Width - 110, 20), .BackColor = Color.FromArgb(108, 117, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
        buttonPanel.Controls.Add(btnCancel)

        totalPenaltyLabel = New Label With {.Text = "Total Penalty: ₱0.00", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .ForeColor = Color.Red, .AutoSize = True, .Location = New Point(20, 25)}
        buttonPanel.Controls.Add(totalPenaltyLabel)

        Dim headerLabel As New Label With {.Text = "Select Copies to Return - Due Date: " & DueDate.ToString("yyyy-MM-dd"), .Font = New Font("Segoe UI", 12, FontStyle.Bold), .ForeColor = Color.FromArgb(45, 45, 45), .AutoSize = True, .Location = New Point(0, 10)}
        mainPanel.Controls.Add(headerLabel)

        Dim infoLabel As New Label With {.Text = "Borrow Date: " & BorrowDate.ToString("yyyy-MM-dd") & " | Return Date: " & ReturnDate.ToString("yyyy-MM-dd") & " | Days Late: " & daysLate.ToString(), .Font = New Font("Segoe UI", 9), .ForeColor = If(daysLate > 0, Color.Red, Color.FromArgb(100, 100, 100)), .AutoSize = True, .Location = New Point(0, headerLabel.Bottom + 5)}
        mainPanel.Controls.Add(infoLabel)

        Dim booksContainer As New Panel With {.Location = New Point(0, infoLabel.Bottom + 20), .Size = New Size(650, 0), .BackColor = Color.White, .AutoSize = True}
        mainPanel.Controls.Add(booksContainer)

        CreateTableHeaders(booksContainer)

        Dim yPos As Integer = 30
        numericControls.Clear()
        bookPanels.Clear()
        penaltyLabels.Clear()

        For i As Integer = 0 To BookIDs.Length - 1
            Dim bookPanel As New Panel With {.BackColor = If(i Mod 2 = 0, Color.FromArgb(250, 250, 250), Color.FromArgb(240, 245, 250)), .Size = New Size(630, 70), .Location = New Point(0, yPos)}
            booksContainer.Controls.Add(bookPanel)
            bookPanels.Add(bookPanel)

            Dim titleLabel As New Label With {.Text = bookTitles(i), .Font = New Font("Segoe UI", 9), .ForeColor = Color.FromArgb(60, 60, 60), .Location = New Point(10, 10), .MaximumSize = New Size(250, 40), .AutoSize = True, .Tag = "Title"}
            bookPanel.Controls.Add(titleLabel)

            Dim bookIDLabel As New Label With {.Text = "ID: " & BookIDs(i), .Font = New Font("Segoe UI", 8), .ForeColor = Color.FromArgb(120, 120, 120), .Location = New Point(10, titleLabel.Bottom + 2), .AutoSize = True}
            bookPanel.Controls.Add(bookIDLabel)

            Dim copiesInfo As String = CurrentReturned(i).ToString() & "/" & TotalCopies(i).ToString() & " returned"
            Dim copiesLabel As New Label With {.Text = copiesInfo, .Font = New Font("Segoe UI", 9), .ForeColor = Color.FromArgb(100, 100, 100), .AutoSize = True, .Location = New Point(270, 15)}
            bookPanel.Controls.Add(copiesLabel)

            Dim remaining As Integer = Math.Max(0, TotalCopies(i) - CurrentReturned(i))
            Dim numUpDown As New NumericUpDown With {.Minimum = 0, .Maximum = remaining, .Size = New Size(70, 25), .Location = New Point(400, 22), .Value = remaining, .BorderStyle = BorderStyle.FixedSingle, .ForeColor = Color.FromArgb(45, 45, 45)}
            AddHandler numUpDown.ValueChanged, AddressOf NumericUpDown_ValueChanged
            bookPanel.Controls.Add(numUpDown)
            numericControls.Add(numUpDown)

            Dim penaltyLabel As New Label With {.Text = "Penalty: ₱0.00", .Font = New Font("Segoe UI", 9), .ForeColor = If(daysLate > 0, Color.Red, Color.FromArgb(100, 100, 100)), .AutoSize = True, .Location = New Point(480, 25)}
            bookPanel.Controls.Add(penaltyLabel)
            penaltyLabels.Add(penaltyLabel)

            yPos += bookPanel.Height + 5
        Next

        booksContainer.Height = yPos

        NumericUpDown_ValueChanged(Nothing, EventArgs.Empty)
    End Sub

    Private Sub NumericUpDown_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        totalPenalty = 0
        totalQuantity = 0
        For i As Integer = 0 To numericControls.Count - 1
            Dim qty As Integer = CInt(numericControls(i).Value)
            Dim penalty As Decimal = daysLate * 10 * qty

            penaltyLabels(i).Text = "Penalty: ₱" & penalty.ToString("N2")
            totalPenalty += penalty
            totalQuantity += qty
        Next
        If totalPenaltyLabel IsNot Nothing Then
            totalPenaltyLabel.Text = "Total Penalty: ₱" & totalPenalty.ToString("N2")
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CreateTableHeaders(ByVal container As Panel)
        container.Controls.Clear()
        Dim titleHeader As New Label With {.Text = "Book Details", .Font = New Font("Segoe UI", 9, FontStyle.Bold), .ForeColor = Color.FromArgb(70, 70, 70), .Location = New Point(10, 5), .AutoSize = True}
        container.Controls.Add(titleHeader)
        Dim copiesHeader As New Label With {.Text = "Copies Status", .Font = New Font("Segoe UI", 9, FontStyle.Bold), .ForeColor = Color.FromArgb(70, 70, 70), .Location = New Point(270, 5), .AutoSize = True}
        container.Controls.Add(copiesHeader)
        Dim returnHeader As New Label With {.Text = "Return Qty", .Font = New Font("Segoe UI", 9, FontStyle.Bold), .ForeColor = Color.FromArgb(70, 70, 70), .Location = New Point(400, 5), .AutoSize = True}
        container.Controls.Add(returnHeader)
        Dim penaltyHeader As New Label With {.Text = "Penalty", .Font = New Font("Segoe UI", 9, FontStyle.Bold), .ForeColor = Color.FromArgb(70, 70, 70), .Location = New Point(480, 5), .AutoSize = True}
        container.Controls.Add(penaltyHeader)
        Dim separator As New Label With {.BorderStyle = BorderStyle.Fixed3D, .Height = 2, .Width = 630, .Location = New Point(0, 25)}
        container.Controls.Add(separator)
    End Sub

    Private Sub GetBookTitles()
        bookTitles.Clear()
        For Each bookID In BookIDs
            bookTitles.Add(GetBookTitleByID(bookID))
        Next
    End Sub

    Private Sub PartialReturnForm_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        If bookPanels Is Nothing OrElse bookPanels.Count = 0 Then Return
        For i As Integer = 0 To bookPanels.Count - 1
            For Each ctrl As Control In bookPanels(i).Controls
                If TypeOf ctrl Is Label AndAlso ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString() = "Title" Then
                    ctrl.MaximumSize = New Size(Me.ClientSize.Width - 300, 40)
                End If
            Next
        Next
    End Sub
End Class