Imports System.Data.OleDb

Public Class PartialReturnForm2
    Public Property BookRecords As New List(Of BookRecord)()
    Public Property SelectedBooks As New List(Of SelectedBook)()
    Private checkBoxes As New List(Of CheckBox)()
    Private numericControls As New List(Of NumericUpDown)()
    Private conditionComboBoxes As New List(Of ComboBox)()
    Private penaltyAmountControls As New List(Of NumericUpDown)()
    Private penaltyLabels As New List(Of Label)()
    Private bookPanels As New List(Of Panel)()
    Private bookTitles As New List(Of String)()

    Private totalPenalty As Decimal = 0
    Private totalPenaltyLabel As Label
    Private totalQuantity As Integer = 0

    ' ====== Classes ======
    Public Class BookRecord
        Public Property BorrowID As String
        Public Property BookID As String
        Public Property BookTitle As String
        Public Property TotalCopies As Integer
        Public Property CurrentReturned As Integer
        Public Property BorrowDate As DateTime
        Public Property DueDate As DateTime
        Public Property DaysLate As Integer
    End Class

    Public Class SelectedBook
        Public Property BorrowID As String
        Public Property BookID As String
        Public Property ReturnQuantity As Integer
        Public Property ConditionType As String
        Public Property PenaltyAmount As Decimal
        Public Property DaysLate As Integer
    End Class

    ' ====== Constructor ======
    Public Sub New(ByVal bookRecords As List(Of BookRecord))
        InitializeComponent()
        Me.BookRecords = bookRecords
    End Sub

    ' ====== Form Load ======
    Private Sub PartialReturnForm2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.White
        Me.Font = New Font("Segoe UI", 9)
        Me.Padding = New Padding(20)
        CenterToScreen()
        SetupFormLayout()
    End Sub

    Private Sub SetupFormLayout()
        Dim buttonPanel As New Panel With {
        .Dock = DockStyle.Bottom,
        .Height = 70,
        .BackColor = Color.White
    }
        Me.Controls.Add(buttonPanel)

        Dim mainPanel As New Panel With {
        .Dock = DockStyle.Fill,
        .AutoScroll = True,
        .BackColor = Color.White
    }
        Me.Controls.Add(mainPanel)
        mainPanel.BringToFront()

        ' Header
        Dim headerLabel As New Label With {
        .Font = New Font("Segoe UI", 12, FontStyle.Bold),
        .ForeColor = Color.FromArgb(45, 45, 45),
        .AutoSize = True,
        .Location = New Point(0, 10)
    }
        mainPanel.Controls.Add(headerLabel)

        ' Info summary - FIXED: Correct way to count late books
        Dim totalBooks As Integer = BookRecords.Count
        Dim totalLateBooks As Integer = 0
        For Each record As BookRecord In BookRecords
            If record.DaysLate > 0 Then
                totalLateBooks += 1
            End If
        Next

        ' Get the latest due date from all records
        Dim maxDueDate As DateTime = DateTime.Today
        If BookRecords.Count > 0 Then
            maxDueDate = BookRecords(0).DueDate
            For Each record As BookRecord In BookRecords
                If record.DueDate > maxDueDate Then
                    maxDueDate = record.DueDate
                End If
            Next
        End If

        Dim infoLabel As New Label With {
        .Text = $"Total Books: {totalBooks}",
        .Font = New Font("Segoe UI", 9),
        .ForeColor = If(totalLateBooks > 0, Color.Red, Color.FromArgb(100, 100, 100)),
        .AutoSize = True,
        .Location = New Point(0, headerLabel.Bottom + 5)
    }
        mainPanel.Controls.Add(infoLabel)

        Dim booksContainer As New Panel With {
        .Location = New Point(0, infoLabel.Bottom + 20),
        .Size = New Size(850, 0),
        .BackColor = Color.White,
        .AutoSize = True
    }
        mainPanel.Controls.Add(booksContainer)

        CreateTableHeaders(booksContainer)

        LoadBooksIntoTable(booksContainer)

        SetupBottomButtons(buttonPanel)
    End Sub

    Private Sub CreateTableHeaders(ByVal container As Panel)
        container.Controls.Clear()

        Dim selectHeader As New Label With {
            .Text = "Select",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(10, 5),
            .AutoSize = True
        }
        container.Controls.Add(selectHeader)

        Dim titleHeader As New Label With {
            .Text = "Book Details",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(60, 5),
            .AutoSize = True
        }
        container.Controls.Add(titleHeader)

        Dim borrowInfoHeader As New Label With {
            .Text = "Borrow Info",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(270, 5),
            .AutoSize = True
        }
        container.Controls.Add(borrowInfoHeader)

        Dim copiesHeader As New Label With {
            .Text = "Copies Status",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(400, 5),
            .AutoSize = True
        }
        container.Controls.Add(copiesHeader)

        Dim returnHeader As New Label With {
            .Text = "Return Qty",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(500, 5),
            .AutoSize = True
        }
        container.Controls.Add(returnHeader)

        Dim conditionHeader As New Label With {
            .Text = "Condition",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(580, 5),
            .AutoSize = True
        }
        container.Controls.Add(conditionHeader)

        Dim penaltyAmountHeader As New Label With {
            .Text = "Penalty Amount",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(670, 5),
            .AutoSize = True
        }
        container.Controls.Add(penaltyAmountHeader)

        Dim penaltyHeader As New Label With {
            .Text = "Penalty",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(70, 70, 70),
            .Location = New Point(770, 5),
            .AutoSize = True
        }
        container.Controls.Add(penaltyHeader)

        ' Separator line
        Dim separator As New Label With {
            .BorderStyle = BorderStyle.Fixed3D,
            .Height = 2,
            .Width = 830,
            .Location = New Point(0, 25)
        }
        container.Controls.Add(separator)
    End Sub

    ' ====== Load Book Rows (Same style as PartialReturnForm) ======
    Private Sub LoadBooksIntoTable(ByVal container As Panel)
        Dim yPos As Integer = 30
        checkBoxes.Clear()
        numericControls.Clear()
        conditionComboBoxes.Clear()
        penaltyAmountControls.Clear()
        penaltyLabels.Clear()
        bookPanels.Clear()

        For i As Integer = 0 To BookRecords.Count - 1
            Dim record = BookRecords(i)

            ' Create book panel with alternating colors (same as PartialReturnForm)
            Dim bookPanel As New Panel With {
                .BackColor = If(i Mod 2 = 0, Color.FromArgb(250, 250, 250), Color.FromArgb(240, 245, 250)),
                .Size = New Size(830, 70),
                .Location = New Point(0, yPos)
            }
            container.Controls.Add(bookPanel)
            bookPanels.Add(bookPanel)

            ' Checkbox for selection (NEW for multi-select)
            Dim chkSelect As New CheckBox With {
                .Location = New Point(15, 25),
                .Size = New Size(20, 20),
                .Tag = i
            }
            AddHandler chkSelect.CheckedChanged, AddressOf CheckBox_CheckedChanged
            bookPanel.Controls.Add(chkSelect)
            checkBoxes.Add(chkSelect)

            ' Book details (Title and ID) - same as PartialReturnForm
            Dim titleLabel As New Label With {
                .Text = record.BookTitle,
                .Font = New Font("Segoe UI", 9),
                .ForeColor = Color.FromArgb(60, 60, 60),
                .Location = New Point(45, 10),
                .MaximumSize = New Size(200, 40),
                .AutoSize = True,
                .Tag = "Title"
            }
            bookPanel.Controls.Add(titleLabel)

            Dim bookIDLabel As New Label With {
                .Text = "ID: " & record.BookID,
                .Font = New Font("Segoe UI", 8),
                .ForeColor = Color.FromArgb(120, 120, 120),
                .Location = New Point(45, titleLabel.Bottom + 2),
                .AutoSize = True
            }
            bookPanel.Controls.Add(bookIDLabel)

            ' Borrow info (Dates) - NEW for multiple records
            Dim borrowInfo As String = $"Borrowed: {record.BorrowDate:MM/dd/yy}" & vbCrLf & $"Due: {record.DueDate:MM/dd/yy}"
            Dim borrowLabel As New Label With {
                .Text = borrowInfo,
                .Font = New Font("Segoe UI", 8),
                .ForeColor = Color.FromArgb(100, 100, 100),
                .Location = New Point(260, 15),
                .Size = New Size(120, 35),
                .TextAlign = ContentAlignment.TopLeft
            }
            bookPanel.Controls.Add(borrowLabel)

            ' Days late indicator
            Dim daysLateLabel As New Label With {
                .Text = $"{record.DaysLate} day(s) late",
                .Font = New Font("Segoe UI", 8),
                .ForeColor = If(record.DaysLate > 0, Color.Red, Color.Green),
                .Location = New Point(260, 50),
                .AutoSize = True
            }
            bookPanel.Controls.Add(daysLateLabel)

            ' Copies status - same as PartialReturnForm
            Dim copiesInfo As String = $"{record.CurrentReturned}/{record.TotalCopies} returned"
            Dim copiesLabel As New Label With {
                .Text = copiesInfo,
                .Font = New Font("Segoe UI", 9),
                .ForeColor = Color.FromArgb(100, 100, 100),
                .AutoSize = True,
                .Location = New Point(400, 25)
            }
            bookPanel.Controls.Add(copiesLabel)

            ' Return quantity - same as PartialReturnForm but disabled initially
            Dim remaining As Integer = Math.Max(0, record.TotalCopies - record.CurrentReturned)
            Dim numUpDown As New NumericUpDown With {
                .Minimum = 0,
                .Maximum = remaining,
                .Size = New Size(60, 25),
                .Location = New Point(500, 22),
                .Value = remaining,
                .Enabled = False, ' Disabled until checked
                .BorderStyle = BorderStyle.FixedSingle,
                .ForeColor = Color.FromArgb(45, 45, 45),
                .Tag = i
            }
            AddHandler numUpDown.ValueChanged, AddressOf NumericUpDown_ValueChanged
            bookPanel.Controls.Add(numUpDown)
            numericControls.Add(numUpDown)

            ' Condition combo - same as PartialReturnForm but disabled initially
            Dim conditionCombo As New ComboBox With {
                .Size = New Size(80, 25),
                .Location = New Point(580, 22),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Enabled = False ' Disabled until checked
            }
            conditionCombo.Items.AddRange({"Normal", "Damaged", "Lost"})
            conditionCombo.SelectedIndex = 0
            AddHandler conditionCombo.SelectedIndexChanged, AddressOf ConditionCombo_SelectedIndexChanged
            bookPanel.Controls.Add(conditionCombo)
            conditionComboBoxes.Add(conditionCombo)

            ' Penalty amount control - same as PartialReturnForm but disabled initially
            Dim penaltyAmount As New NumericUpDown With {
                .Minimum = 0,
                .Maximum = 10000,
                .DecimalPlaces = 2,
                .Size = New Size(80, 25),
                .Location = New Point(670, 22),
                .Value = 0,
                .Enabled = False, ' Disabled until checked and condition is not Normal
                .BorderStyle = BorderStyle.FixedSingle,
                .ForeColor = Color.FromArgb(45, 45, 45),
                .Tag = i
            }
            AddHandler penaltyAmount.ValueChanged, AddressOf PenaltyAmount_ValueChanged
            bookPanel.Controls.Add(penaltyAmount)
            penaltyAmountControls.Add(penaltyAmount)

            ' Penalty label - same as PartialReturnForm
            Dim penaltyLabel As New Label With {
                .Text = "₱0.00",
                .Font = New Font("Segoe UI", 9),
                .ForeColor = If(record.DaysLate > 0, Color.Red, Color.FromArgb(100, 100, 100)),
                .AutoSize = True,
                .Location = New Point(770, 25)
            }
            bookPanel.Controls.Add(penaltyLabel)
            penaltyLabels.Add(penaltyLabel)

            yPos += bookPanel.Height + 5
        Next

        container.Height = yPos
    End Sub

    Private Sub SetupBottomButtons(ByVal buttonPanel As Panel)
        totalPenaltyLabel = New Label With {
            .Text = "Total Penalty: ₱0.00",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.Black,
            .AutoSize = True,
            .Location = New Point(20, 25)
        }
        buttonPanel.Controls.Add(totalPenaltyLabel)

        Dim btnOK As New Button With {
            .Text = "OK",
            .Size = New Size(100, 35),
            .Location = New Point(buttonPanel.Width - 220, 20),
            .BackColor = Color.FromArgb(0, 123, 255),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        AddHandler btnOK.Click, AddressOf btnOK_Click
        buttonPanel.Controls.Add(btnOK)

        Dim btnCancel As New Button With {
            .Text = "Cancel",
            .Size = New Size(100, 35),
            .Location = New Point(buttonPanel.Width - 110, 20),
            .BackColor = Color.FromArgb(108, 117, 125),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
        buttonPanel.Controls.Add(btnCancel)
    End Sub

    ' ====== Event Handlers ======
    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim index As Integer = CInt(chk.Tag)

        numericControls(index).Enabled = chk.Checked
        conditionComboBoxes(index).Enabled = chk.Checked

        penaltyAmountControls(index).Enabled = chk.Checked AndAlso conditionComboBoxes(index).SelectedItem.ToString() <> "Normal"

        If chk.Checked Then
            Dim selectedBook As New SelectedBook With {
                .BorrowID = BookRecords(index).BorrowID,
                .BookID = BookRecords(index).BookID,
                .ReturnQuantity = CInt(numericControls(index).Value),
                .ConditionType = conditionComboBoxes(index).SelectedItem.ToString(),
                .PenaltyAmount = 0,
                .DaysLate = BookRecords(index).DaysLate
            }
            SelectedBooks.Add(selectedBook)
        Else
            ' Remove from selected books
            SelectedBooks.RemoveAll(Function(sb) sb.BookID = BookRecords(index).BookID AndAlso sb.BorrowID = BookRecords(index).BorrowID)

            ' Reset values when unchecked
            conditionComboBoxes(index).SelectedIndex = 0
            penaltyAmountControls(index).Value = 0
        End If

        CalculateTotalPenalty()
    End Sub

    Private Sub NumericUpDown_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim numUpDown As NumericUpDown = CType(sender, NumericUpDown)
        Dim index As Integer = CInt(numUpDown.Tag)

        ' Update selected book quantity
        Dim selectedBook = SelectedBooks.FirstOrDefault(Function(sb) sb.BookID = BookRecords(index).BookID AndAlso sb.BorrowID = BookRecords(index).BorrowID)
        If selectedBook IsNot Nothing Then
            selectedBook.ReturnQuantity = CInt(numUpDown.Value)
        End If

        CalculateTotalPenalty()
    End Sub

    Private Sub ConditionCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim combo As ComboBox = CType(sender, ComboBox)
        Dim index As Integer = conditionComboBoxes.IndexOf(combo)

        If index >= 0 AndAlso index < penaltyAmountControls.Count Then
            Dim penaltyAmountControl As NumericUpDown = penaltyAmountControls(index)

            ' Enable penalty amount control only for Damaged/Lost conditions
            penaltyAmountControl.Enabled = combo.Enabled AndAlso combo.SelectedItem.ToString() <> "Normal"

            ' Set default penalty amounts
            If combo.SelectedItem.ToString() = "Normal" Then
                penaltyAmountControl.Value = 0
            ElseIf combo.SelectedItem.ToString() = "Damaged" Then
                If penaltyAmountControl.Value = 0 Then penaltyAmountControl.Value = 300
            ElseIf combo.SelectedItem.ToString() = "Lost" Then
                If penaltyAmountControl.Value = 0 Then penaltyAmountControl.Value = 1000
            End If

            ' Update selected book condition
            Dim selectedBook = SelectedBooks.FirstOrDefault(Function(sb) sb.BookID = BookRecords(index).BookID AndAlso sb.BorrowID = BookRecords(index).BorrowID)
            If selectedBook IsNot Nothing Then
                selectedBook.ConditionType = combo.SelectedItem.ToString()
            End If
        End If

        CalculateTotalPenalty()
    End Sub

    Private Sub PenaltyAmount_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        CalculateTotalPenalty()
    End Sub

    ' ====== Penalty Calculation (Same logic as PartialReturnForm) ======
    Private Sub CalculateTotalPenalty()
        totalPenalty = 0
        totalQuantity = 0

        For i As Integer = 0 To BookRecords.Count - 1
            If checkBoxes(i).Checked Then
                Dim record = BookRecords(i)
                Dim qty As Integer = CInt(numericControls(i).Value)
                Dim condition As String = conditionComboBoxes(i).SelectedItem.ToString()
                Dim penalty As Decimal = 0

                ' Calculate overdue penalty (same as PartialReturnForm)
                Dim overduePenalty As Decimal = record.DaysLate * 10 * qty

                ' Calculate condition penalty (same as PartialReturnForm)
                Dim conditionPenalty As Decimal = 0
                If condition = "Damaged" OrElse condition = "Lost" Then
                    conditionPenalty = penaltyAmountControls(i).Value * qty
                End If

                penalty = overduePenalty + conditionPenalty

                ' Update individual penalty label (same as PartialReturnForm)
                penaltyLabels(i).Text = "₱" & penalty.ToString("N2")
                penaltyLabels(i).ForeColor = If(penalty > 0, Color.Red, Color.FromArgb(100, 100, 100))

                ' Update selected book penalty
                Dim selectedBook = SelectedBooks.FirstOrDefault(Function(sb) sb.BookID = record.BookID AndAlso sb.BorrowID = record.BorrowID)
                If selectedBook IsNot Nothing Then
                    selectedBook.PenaltyAmount = penalty
                End If

                totalPenalty += penalty
                totalQuantity += qty
            Else
                ' Reset penalty display for unchecked items
                penaltyLabels(i).Text = "₱0.00"
                penaltyLabels(i).ForeColor = Color.FromArgb(100, 100, 100)
            End If
        Next

        ' Update total penalty label (same as PartialReturnForm)
        If totalPenaltyLabel IsNot Nothing Then
            totalPenaltyLabel.Text = "Total Penalty: ₱" & totalPenalty.ToString("N2")
            totalPenaltyLabel.ForeColor = If(totalPenalty > 0, Color.Red, Color.Black)
        End If
    End Sub

    ' ====== Button Click Handlers (Same as PartialReturnForm) ======
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs)
        If SelectedBooks.Count = 0 Then
            MessageBox.Show("Please select at least one book to return.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validate penalty amounts for damaged/lost books (same as PartialReturnForm)
        For i As Integer = 0 To BookRecords.Count - 1
            If checkBoxes(i).Checked AndAlso numericControls(i).Value > 0 Then
                Dim condition As String = conditionComboBoxes(i).SelectedItem.ToString()
                If (condition = "Damaged" OrElse condition = "Lost") AndAlso penaltyAmountControls(i).Value = 0 Then
                    MessageBox.Show("Please set penalty amount for " & condition & " book: " & BookRecords(i).BookTitle, "Penalty Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If
        Next

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' ====== Public Methods ======
    Public Function GetSelectedBooks() As List(Of SelectedBook)
        Return SelectedBooks
    End Function

    Public Function GetTotalPenalty() As Decimal
        Return totalPenalty
    End Function

    Public Function GetTotalQuantity() As Integer
        Return totalQuantity
    End Function

    ' ====== Form Resize Handler (Same as PartialReturnForm) ======
    Private Sub PartialReturnForm2_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        If bookPanels Is Nothing OrElse bookPanels.Count = 0 Then Return

        For i As Integer = 0 To bookPanels.Count - 1
            For Each ctrl As Control In bookPanels(i).Controls
                If TypeOf ctrl Is Label AndAlso ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString() = "Title" Then
                    ctrl.MaximumSize = New Size(Me.ClientSize.Width - 500, 40)
                End If
            Next
        Next
    End Sub
End Class