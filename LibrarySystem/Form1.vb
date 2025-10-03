Imports System.Data.OleDb
Imports System.Threading
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine


Public Class Form1
    Private isAdding As Boolean = False
    Private isEditing As Boolean = False
    Private trec As Integer = 0
    Private recpointer As Integer = -1
    Private isEditingOrAdding As Boolean = False

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        txtpw.UseSystemPasswordChar = True
        txtrpw.UseSystemPasswordChar = True
        UpdateButtonStates()
        menuDelete.Enabled = False

        Call CenterToScreen()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        OpenDB()
        SQLQueryFortbluser()
        lock()

        dg.DataSource = dbds.Tables("tbluser")
        dg.ReadOnly = True
        dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dg.ClearSelection()

        trec = dbds.Tables("tbluser").Rows.Count - 1

        cbopv.Visible = False
        LinkLabel1.Visible = False

        cbopv.SelectedIndex = -1
        cbopost.SelectedIndex = -1
    End Sub

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        OpenDB()
    End Sub

    Private Sub UpdateButtonStates()
        If isEditingOrAdding Then
            menuSave.Enabled = True
            menuCancel.Enabled = True
            menuNew.Enabled = False
            menuEdit.Enabled = False
            menuDelete.Enabled = False
            menuClose.Enabled = False
            menuPrint.Enabled = False
            menuRefresh.Enabled = False
            menuSearch.Enabled = False
            menuFirst.Enabled = False
            menuPrevious.Enabled = False
            menuNext.Enabled = False
            menuLast.Enabled = False

            txtid.BackColor = Color.White
            txtun.BackColor = Color.White
            txtpw.BackColor = Color.White
            txtrpw.BackColor = Color.White
            cbopost.BackColor = Color.White
            txtpv.BackColor = Color.White
            cbopv.BackColor = Color.White
        Else
            menuSave.Enabled = False
            menuCancel.Enabled = False
            menuNew.Enabled = True
            menuEdit.Enabled = (dg.Rows.Count > 0 And recpointer >= 0)
            menuDelete.Enabled = (dg.Rows.Count > 0 And recpointer >= 0)
            menuClose.Enabled = True
            menuSearch.Enabled = True
            menuRefresh.Enabled = True

            Dim hasRecords As Boolean = (dg.Rows.Count > 0)
            Dim hasMultipleRecords As Boolean = (dg.Rows.Count > 1)

            menuFirst.Enabled = (hasMultipleRecords And recpointer > 0)
            menuPrevious.Enabled = (hasMultipleRecords And recpointer > 0)
            menuNext.Enabled = (hasMultipleRecords And recpointer < trec)
            menuLast.Enabled = (hasMultipleRecords And recpointer < trec)

            txtid.BackColor = Color.WhiteSmoke
            txtun.BackColor = Color.WhiteSmoke
            txtpw.BackColor = Color.WhiteSmoke
            txtrpw.BackColor = Color.WhiteSmoke
            cbopost.BackColor = Color.WhiteSmoke
            txtpv.BackColor = Color.WhiteSmoke
            cbopv.BackColor = Color.WhiteSmoke
        End If

        cbopost.FlatStyle = FlatStyle.Flat
        cbopv.FlatStyle = FlatStyle.Flat
    End Sub

    Sub txtclear()
        txtid.Clear()
        txtun.Clear()
        txtpw.Clear()
        txtrpw.Clear()
        cbopost.SelectedIndex = -1
        txtpv.Clear()
        cbopv.Text = ""
        dg.ClearSelection()
        idpict.Image = Nothing
    End Sub

    Private Sub lock()
        txtid.ReadOnly = True
        txtun.ReadOnly = True
        txtpw.ReadOnly = True
        txtrpw.ReadOnly = True
        cbopost.Enabled = False
        txtpv.ReadOnly = True
        chbShowPassword.Enabled = False
        chbShowRetypePassword.Enabled = False
    End Sub

    Private Sub unlock()
        txtun.ReadOnly = False
        txtpw.ReadOnly = False
        txtrpw.ReadOnly = False
        cbopost.Enabled = True
        txtpv.ReadOnly = False
        chbShowPassword.Enabled = True
        chbShowRetypePassword.Enabled = True
    End Sub


    Private Function GenerateUserID() As String
        Dim maxNumber As Integer = 0
        Dim prefix As String = "DRDF-"

        For Each row As DataRow In dbds.Tables("tbluser").Rows
            Dim idStr As String = row("User ID").ToString()
            If idStr.StartsWith(prefix) Then
                Dim suffix As String = idStr.Substring(prefix.Length)
                Dim number As Integer
                If Integer.TryParse(suffix, number) Then
                    If number > maxNumber Then
                        maxNumber = number
                    End If
                End If
            End If
        Next

        Dim nextNumber As Integer = maxNumber + 1

        Return prefix & nextNumber.ToString("D5")
    End Function



    Private Sub txtun_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtun.Leave
        If isAdding AndAlso txtun.Text.Trim <> "" Then
            txtid.Text = GenerateUserID()
        End If
    End Sub

    Sub display()
        Try
            If recpointer >= 0 AndAlso recpointer <= trec Then
                Dim row = dbds.Tables("tbluser").Rows(recpointer)
                txtid.Text = row("User ID").ToString()
                txtun.Text = row("User Name").ToString()
                cbopost.Text = row("Position").ToString()
                txtpv.Text = row("Privileges").ToString()

                txtpw.Clear()
                txtrpw.Clear()

                If cbopv.Visible Then
                    cbopv.Text = row("Privileges").ToString()
                End If
            Else
                txtclear()
            End If
        Catch ex As Exception
            MsgBox("Display error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

Sub loadImage()
        Try
            If idpict.Image IsNot Nothing Then
                idpict.Image.Dispose()
                idpict.Image = Nothing
            End If

            Dim actualUserID As String = GetUserIdByUsername(txtun.Text.Trim)
            If String.IsNullOrEmpty(actualUserID) Then
                actualUserID = txtid.Text.Trim
            End If

            Dim imgPath As String = Application.StartupPath & "\images\" & actualUserID & ".jpg"

            If IO.File.Exists(imgPath) Then
                Using fs As New IO.FileStream(imgPath, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim img As Image = Image.FromStream(fs)
                    idpict.Image = New Bitmap(img)
                End Using
            Else
                idpict.Image = Nothing
            End If
        Catch ex As Exception
            MsgBox("Error in loadImage: " & ex.Message)
            idpict.Image = Nothing
        End Try
    End Sub


    Private Sub NavigateRecord(ByVal index As Integer)
        If index < 0 OrElse index > trec Then Exit Sub

        Try
            recpointer = index
            dg.ClearSelection()
            dg.Rows(recpointer).Selected = True
            display()
            loadImage()
            UpdateButtonStates()


            If Not isEditingOrAdding Then
                cbopv.Visible = False
                txtpv.Visible = True
                cbopost.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("Navigation error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub menuFirst_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuFirst.Click
        If trec > 0 Then
            NavigateRecord(0)
        End If
    End Sub

    Private Sub menuNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuNext.Click
        If trec > 0 AndAlso recpointer + 1 <= trec Then
            NavigateRecord(recpointer + 1)
        End If
    End Sub

    Private Sub menuPrevious_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuPrevious.Click
        If trec > 0 AndAlso recpointer - 1 >= 0 Then
            NavigateRecord(recpointer - 1)
        End If
    End Sub

    Private Sub menuLast_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuLast.Click
        If trec > 0 Then
            NavigateRecord(trec)
        End If
    End Sub

    Private Sub dg_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dg.CellClick
        If e.RowIndex < 0 Or e.RowIndex >= dg.Rows.Count Then Exit Sub

        recpointer = e.RowIndex
        dg.Rows(recpointer).Selected = True
        display()
        loadImage()
        UpdateButtonStates()
        If Not isEditingOrAdding Then
            cbopv.Visible = False
            txtpv.Visible = True
            cbopost.Enabled = False
        End If
    End Sub

    Private Sub menuAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuNew.Click
        isAdding = True
        isEditingOrAdding = True
        unlock()
        txtclear()
        txtid.Text = GenerateUserID()
        dg.Enabled = False
        cbopv.Visible = True
        cbopv.Enabled = True
        cbopost.Enabled = False
        txtpv.Visible = False
        LinkLabel1.Visible = True
        txtun.Focus()
        UpdateButtonStates()
    End Sub

    Private Sub menuEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuEdit.Click
        If recpointer < 0 Then
            MsgBox("Please select a user to edit.", MsgBoxStyle.Exclamation)
            Return
        End If

        isEditing = True
        isEditingOrAdding = True
        unlock()

        Dim currentPrivilege As String = txtpv.Text

        cbopv.Visible = True
        cbopv.Text = currentPrivilege
        txtpv.Visible = False
        LinkLabel1.Visible = True
        dg.Enabled = False

        cbopost.Enabled = True
        cbopv.Enabled = True

        cbopost.Items.Clear()

        Select Case currentPrivilege
            Case "Admin"
                cbopost.Items.Add("Administrator")
                cbopost.Items.Add("Librarian")
            Case "User"
                cbopost.Items.Add("Teacher")
                cbopost.Items.Add("Student")
        End Select

        Dim currentPosition As String = dbds.Tables("tbluser").Rows(recpointer)("Position").ToString()
        cbopost.Text = currentPosition

        UpdateButtonStates()
    End Sub

    Private Sub menuSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSave.Click

        Select Case cbopv.Text
            Case "Admin"
                If cbopost.Text <> "Administrator" AndAlso cbopost.Text <> "Librarian" Then
                    MsgBox("Admin privileges can only be assigned to Administrators or Librarians.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Case "User"
                If cbopost.Text <> "Teacher" AndAlso cbopost.Text <> "Student" Then
                    MsgBox("User privileges can only be assigned to Teachers or Students.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
        End Select

        If String.IsNullOrWhiteSpace(txtun.Text) OrElse
           String.IsNullOrWhiteSpace(cbopost.Text) OrElse
           String.IsNullOrWhiteSpace(cbopv.Text) Then

            MsgBox("Please fill in all required fields.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If isAdding Then
            If String.IsNullOrWhiteSpace(txtpw.Text) OrElse String.IsNullOrWhiteSpace(txtrpw.Text) Then
                MsgBox("Please enter and confirm password for new user.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            If txtpw.Text.Trim <> txtrpw.Text.Trim Then
                MsgBox("Passwords do not match.", MsgBoxStyle.Exclamation)
                txtpw.Clear()
                txtrpw.Clear()
                txtpw.Focus()
                Exit Sub
            End If
        Else
            If Not String.IsNullOrWhiteSpace(txtpw.Text) OrElse Not String.IsNullOrWhiteSpace(txtrpw.Text) Then
                If String.IsNullOrWhiteSpace(txtpw.Text) OrElse String.IsNullOrWhiteSpace(txtrpw.Text) Then
                    MsgBox("Please both enter and confirm password if you want to change it.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                If txtpw.Text.Trim <> txtrpw.Text.Trim Then
                    MsgBox("Passwords do not match.", MsgBoxStyle.Exclamation)
                    txtpw.Clear()
                    txtrpw.Clear()
                    txtpw.Focus()
                    Exit Sub
                End If
            End If
        End If

        If isAdding Then
            Dim imgPath As String = Application.StartupPath & "\images\" & txtid.Text.Trim & ".jpg"
            If Not IO.File.Exists(imgPath) Then
                MsgBox("Please upload an image before saving.", MsgBoxStyle.Exclamation)
                Return
            End If
        End If

        If isAdding Or (isEditing AndAlso Not txtun.Text.Equals(dbds.Tables("tbluser").Rows(recpointer)("User Name").ToString())) Then
            For Each row As DataRow In dbds.Tables("tbluser").Rows
                If row("User Name").ToString().Trim.ToLower() = txtun.Text.Trim.ToLower() Then
                    MsgBox("Username already exists.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Next
        End If

        Try
            If isAdding Then
                cmd = New OleDbCommand("INSERT INTO tbluser([User ID],[User Name],[Password],[Position],[Privileges]) VALUES (?,?,?,?,?)", con)
                cmd.Parameters.AddWithValue("@id", txtid.Text.Trim)
                cmd.Parameters.AddWithValue("@un", txtun.Text.Trim)
                cmd.Parameters.AddWithValue("@pw", txtpw.Text.Trim)
                cmd.Parameters.AddWithValue("@pos", cbopost.Text.Trim)
                cmd.Parameters.AddWithValue("@pv", cbopv.Text.Trim)
                cmd.ExecuteNonQuery()
                MsgBox("New user saved successfully!", MsgBoxStyle.Information)
            Else
                If String.IsNullOrWhiteSpace(txtpw.Text) Then
                    cmd = New OleDbCommand("UPDATE tbluser SET [User Name]=?, [Position]=?, [Privileges]=? WHERE [User ID]=?", con)
                    cmd.Parameters.AddWithValue("@un", txtun.Text.Trim)
                    cmd.Parameters.AddWithValue("@pos", cbopost.Text.Trim)
                    cmd.Parameters.AddWithValue("@pv", cbopv.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", txtid.Text.Trim)
                Else
                    cmd = New OleDbCommand("UPDATE tbluser SET [User Name]=?, [Password]=?, [Position]=?, [Privileges]=? WHERE [User ID]=?", con)
                    cmd.Parameters.AddWithValue("@un", txtun.Text.Trim)
                    cmd.Parameters.AddWithValue("@pw", txtpw.Text.Trim)
                    cmd.Parameters.AddWithValue("@pos", cbopost.Text.Trim)
                    cmd.Parameters.AddWithValue("@pv", cbopv.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", txtid.Text.Trim)
                End If
                cmd.ExecuteNonQuery()
                MsgBox("User updated successfully!", MsgBoxStyle.Information)
            End If

            SQLQueryFortbluser()
            dg.DataSource = dbds.Tables("tbluser")
            trec = dbds.Tables("tbluser").Rows.Count - 1

            Dim savedID As String = txtid.Text.Trim
            Dim foundIndex As Integer = -1

            For i As Integer = 0 To trec
                If dbds.Tables("tbluser").Rows(i)("User ID").ToString() = savedID Then
                    foundIndex = i
                    Exit For
                End If
            Next

            If foundIndex >= 0 Then
                recpointer = foundIndex
            Else
                recpointer = trec
            End If

            lock()
            dg.Enabled = True
            cbopv.Visible = False
            txtpv.Visible = True
            LinkLabel1.Visible = False
            isAdding = False
            isEditing = False
            isEditingOrAdding = False
            chbShowPassword.Checked = False
            chbShowRetypePassword.Checked = False

            txtpw.Clear()
            txtrpw.Clear()

            If recpointer >= 0 Then
                dg.ClearSelection()
                dg.Rows(recpointer).Selected = True
                display()
                loadImage()
            End If

            UpdateButtonStates()

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub menuCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuCancel.Click
        If isAdding Or isEditing Then
            isAdding = False
            isEditing = False
            isEditingOrAdding = False
            txtclear()
            lock()
            dg.Enabled = True
            cbopv.Visible = False
            txtpv.Visible = True
            LinkLabel1.Visible = False
            cbopost.Enabled = False
            cbopv.Enabled = True

            cbopost.Items.Clear()
            cbopost.Items.Add("Administrator")
            cbopost.Items.Add("Librarian")
            cbopost.Items.Add("Teacher")
            cbopost.Items.Add("Student")

            txtpw.Clear()
            txtrpw.Clear()
            chbShowPassword.Checked = False
            chbShowRetypePassword.Checked = False

            If recpointer >= 0 AndAlso recpointer < dbds.Tables("tbluser").Rows.Count Then
                dg.ClearSelection()
                dg.Rows(recpointer).Selected = True
                display()
                loadImage()
            End If

            UpdateButtonStates()
        End If
    End Sub

    Private Sub menuDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuDelete.Click
        If recpointer < 0 Then
            MsgBox("Please select a user to delete.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim userIDToDelete As String = dbds.Tables("tbluser").Rows(recpointer)("User ID").ToString()
        Dim usernameToDelete As String = dbds.Tables("tbluser").Rows(recpointer)("User Name").ToString()

        If usernameToDelete = XName Then
            Dim confirm As MsgBoxResult = MsgBox(
                "You are about to delete your own account (" & XName & ")." & vbCrLf &
                "This will log you out immediately. Do you want to proceed?",
                MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "Delete Own Account"
            )

            If confirm = MsgBoxResult.No Then
                Return
            End If
        Else
            If MsgBox("This will permanently delete the record. Proceed?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical) = MsgBoxResult.No Then
                Return
            End If
        End If

        Try
            Dim imgPath As String = Application.StartupPath & "\images\" & userIDToDelete & ".jpg"
            If IO.File.Exists(imgPath) Then
                Try
                    IO.File.Delete(imgPath)
                Catch ex As Exception
                    MsgBox("Warning: Could not delete user image file.", MsgBoxStyle.Exclamation)
                End Try
            End If

            cmd = New OleDbCommand("DELETE FROM tbluser WHERE [User ID] = ?", con)
            cmd.Parameters.AddWithValue("@id", userIDToDelete)
            cmd.ExecuteNonQuery()

            SQLQueryFortbluser()
            dg.DataSource = dbds.Tables("tbluser")
            trec = dbds.Tables("tbluser").Rows.Count - 1
            txtclear()

            If recpointer > trec Then recpointer = trec
            If recpointer >= 0 Then
                dg.Rows(recpointer).Selected = True
                display()
            End If

            UpdateButtonStates()

            If usernameToDelete = XName Then
                MsgBox("Your account has been deleted. You will now be logged out.", MsgBoxStyle.Information)
                Me.Close()
                Login.Show()
            End If
        Catch ex As Exception
            MsgBox("Delete failed: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub




    Private Sub menuClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuClose.Click
        If MsgBox("Close the window?", MsgBoxStyle.YesNo, "Closing") = MsgBoxResult.Yes Then
            For opacity As Double = 1.0 To 0 Step -0.2
                Me.Opacity = opacity
                Me.Refresh()
                Thread.Sleep(10)
            Next
            Application.Exit()
        End If
    End Sub


    Private Sub cbopv_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbopv.SelectedIndexChanged
        If isEditingOrAdding Then
            cbopost.Items.Clear()

            Select Case cbopv.Text
                Case "Admin"
                    cbopost.Items.Add("Administrator")
                    cbopost.Items.Add("Librarian")
                Case "User"
                    cbopost.Items.Add("Teacher")
                    cbopost.Items.Add("Student")
                Case Else
            End Select

            If isAdding Then
                If cbopost.Items.Count = 1 Then
                    cbopost.SelectedIndex = 0
                Else
                    cbopost.SelectedIndex = -1
                End If
            Else
                Dim currentPosition As String = dbds.Tables("tbluser").Rows(recpointer)("Position").ToString()

                Dim isValidPosition As Boolean = False
                For Each item As String In cbopost.Items
                    If item = currentPosition Then
                        isValidPosition = True
                        Exit For
                    End If
                Next

                If isValidPosition Then
                    cbopost.Text = currentPosition
                ElseIf cbopost.Items.Count > 0 Then
                    cbopost.SelectedIndex = 0
                End If
            End If

            cbopost.Enabled = True
        End If
    End Sub

    Private Sub chbShowPassword_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowPassword.CheckedChanged
        txtpw.UseSystemPasswordChar = Not chbShowPassword.Checked
    End Sub

    Private Sub chbShowRetypePassword_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowRetypePassword.CheckedChanged
        txtrpw.UseSystemPasswordChar = Not chbShowRetypePassword.Checked
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If txtid.Text.Trim.Length = 0 Then
            MsgBox("Please generate a User ID first.", MsgBoxStyle.Information)
            Return
        End If

        ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim imgDir As String = Application.StartupPath & "\images"
                If Not IO.Directory.Exists(imgDir) Then
                    IO.Directory.CreateDirectory(imgDir)
                End If

                Dim actualUserID As String = GetUserIdByUsername(txtun.Text.Trim)
                If String.IsNullOrEmpty(actualUserID) Then
                    actualUserID = txtid.Text.Trim
                End If

                Dim targetPath As String = imgDir & "\" & actualUserID & ".jpg"

                If idpict.Image IsNot Nothing Then
                    idpict.Image.Dispose()
                    idpict.Image = Nothing
                End If

                If IO.File.Exists(targetPath) Then
                    Try
                        IO.File.Delete(targetPath)
                    Catch deleteEx As Exception
                        Threading.Thread.Sleep(100)
                        Try
                            IO.File.Delete(targetPath)
                        Catch
                            MsgBox("Could not replace existing image. Please try again.", MsgBoxStyle.Exclamation)
                            Return
                        End Try
                    End Try
                End If
                IO.File.Copy(ofd.FileName, targetPath, True)

                loadImage()
                MsgBox("Image uploaded successfully!", MsgBoxStyle.Information)

            Catch ex As Exception
                MsgBox("Image upload error: " & ex.Message & vbCrLf & "Please make sure the image is not open in another program.", MsgBoxStyle.Critical)
            End Try
        End If
    End Sub


    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseDB()
    End Sub

    Private Sub SearchUserByID()
        Dim searchTerm As String = InputBox("Enter User ID to search:", "Search User")
        If String.IsNullOrWhiteSpace(searchTerm) Then
            MsgBox("Search cancelled.", MsgBoxStyle.Information)
            Return
        End If
        searchTerm = searchTerm.Trim()

        Dim originalData As DataSet = dbds.Copy()
        Dim originalPointer As Integer = recpointer
        Dim originalSelection As Integer = If(dg.SelectedRows.Count > 0, dg.SelectedRows(0).Index, -1)

        Try
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If

            Using cmd As New OleDbCommand("SELECT * FROM tbluser WHERE [User ID] LIKE ?", con)
                cmd.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")

                Using adapter As New OleDbDataAdapter(cmd)
                    Dim searchResults As New DataSet()
                    adapter.Fill(searchResults, "tbluser")

                    If searchResults.Tables("tbluser").Rows.Count > 0 Then
                        dbds = searchResults
                        dg.DataSource = dbds.Tables("tbluser")
                        trec = dbds.Tables("tbluser").Rows.Count - 1
                        recpointer = 0

                        dg.ClearSelection()
                        If dg.Rows.Count > 0 Then
                            dg.Rows(0).Selected = True
                            display()
                            loadImage()
                        End If

                        Dim resultCount As Integer = dbds.Tables("tbluser").Rows.Count
                        Dim message As String = resultCount.ToString() & " record(s) found."
                        If resultCount > 1 Then
                            message += vbCrLf & "Showing first match. Use navigation buttons to view others."
                        End If
                        MsgBox(message, MsgBoxStyle.Information, "Search Results")
                    Else
                        MsgBox("No records found matching '" & searchTerm & "'.", MsgBoxStyle.Information, "Search Results")
                        RestoreOriginalState(originalData, originalPointer, originalSelection)
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Search error: " & ex.Message, MsgBoxStyle.Critical, "Search Error")
            RestoreOriginalState(originalData, originalPointer, originalSelection)
        Finally
            UpdateButtonStates()
        End Try
    End Sub

    Private Sub RestoreOriginalState(ByVal originalData As DataSet, ByVal originalPointer As Integer, ByVal originalSelection As Integer)
        dbds = originalData
        dg.DataSource = dbds.Tables("tbluser")
        trec = dbds.Tables("tbluser").Rows.Count - 1

        If originalSelection >= 0 And originalSelection <= trec Then
            recpointer = originalSelection
            dg.ClearSelection()
            dg.Rows(originalSelection).Selected = True
            display()
            loadImage()
        Else
            recpointer = -1
            txtclear()
        End If
    End Sub

    Private Sub menuSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSearch.Click
        SearchUserByID()
    End Sub

    Private Sub menuRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRefresh.Click
        RefreshData()
    End Sub


    Private Sub RefreshData()
        Dim selectedID As String = If(recpointer >= 0 AndAlso recpointer <= trec,
                                     dbds.Tables("tbluser").Rows(recpointer)("User ID").ToString(),
                                     String.Empty)

        Try
            txtclear()

            SQLQueryFortbluser()
            dg.DataSource = dbds.Tables("tbluser")
            trec = dbds.Tables("tbluser").Rows.Count - 1
            recpointer = -1

            If Not String.IsNullOrEmpty(selectedID) Then
                For i As Integer = 0 To trec
                    If dbds.Tables("tbluser").Rows(i)("User ID").ToString() = selectedID Then
                        recpointer = i
                        dg.ClearSelection()
                        dg.Rows(i).Selected = True
                        display()
                        loadImage()
                        Exit For
                    End If
                Next
            End If

            lock()
            cbopv.Visible = False
            txtpv.Visible = True
            LinkLabel1.Visible = False
            isAdding = False
            isEditing = False
            isEditingOrAdding = False
            cbopost.Enabled = False

            UpdateButtonStates()
            dg.Refresh()

        Catch ex As Exception
            MsgBox("Error refreshing data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MainFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainFormToolStripMenuItem.Click
        Dim main As New frmmain
        main.Show()
        Me.Close()
    End Sub

    Private Sub cbopost_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbopost.SelectedIndexChanged
        If isEditingOrAdding Then
            Select Case cbopost.Text
                Case "Administrator", "Librarian"
                    cbopv.Text = "Admin"
                    cbopv.Enabled = False
                Case "Teacher", "Student"
                    If cbopv.Text = "Admin" Then
                        cbopv.Text = "User"
                    End If
                    cbopv.Enabled = True
            End Select
        End If
    End Sub


    Private Sub CrystalReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrystalReportToolStripMenuItem.Click
        Try
            Dim rpt As New ReportDocument()
            rpt.Load(Application.StartupPath & "\Reports\UserForm.rpt")

            rpt.SetDatabaseLogon("", "", Application.StartupPath, con.Database)
            rpt.SetDataSource(dbds.Tables("tbluser"))

            rpt.PrintToPrinter(1, False, 0, 0)

            MsgBox("Report sent to printer successfully.", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox("Error printing Crystal Report: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Try
            If dg.Rows.Count = 0 Then
                MsgBox("No records to export.", MsgBoxStyle.Exclamation)
                Return
            End If

            Dim excelApp As New Excel.Application
            Dim workbook As Excel.Workbook = excelApp.Workbooks.Add(Type.Missing)
            Dim worksheet As Excel.Worksheet = Nothing
            worksheet = workbook.Sheets("sheet1")
            worksheet = workbook.ActiveSheet
            worksheet.Name = "Users"

            For i As Integer = 1 To dg.Columns.Count
                worksheet.Cells(1, i) = dg.Columns(i - 1).HeaderText
            Next

            For i As Integer = 0 To dg.Rows.Count - 1
                For j As Integer = 0 To dg.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = dg.Rows(i).Cells(j).Value?.ToString()
                Next
            Next

            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx"
            saveDialog.FileName = "UserList.xlsx"
            If saveDialog.ShowDialog() = DialogResult.OK Then
                workbook.SaveAs(saveDialog.FileName)
                MsgBox("Exported successfully to Excel!", MsgBoxStyle.Information)
            End If

            workbook.Close()
            excelApp.Quit()

        Catch ex As Exception
            MsgBox("Error exporting to Excel: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

End Class