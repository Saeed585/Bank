Public Class BankSrch
    Dim ColumnSrch As Integer
    Private Sub FillMaxRow()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Code) AS Expr1 FROM Bnk.BankSrch", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.BankSrch")
        objDataview = New DataView(objDataset.Tables("Bnk.BankSrch"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Label5.DataBindings.Clear()
        Label5.DataBindings.Add("text", objDataview, "Expr1")
        If Label5.Text <> "" Then
            txtCode.Text = Label5.Text + 1
        Else
            txtCode.Text = 1
        End If
        Label5.Text = ""
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Code, NameF, Name FROM  Bnk.BankSrch ORDER BY Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, " Bnk.BankSrch")
        objDataview = New DataView(objDataset.Tables(" Bnk.BankSrch"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام فلدر"
        Me.DataGridView1.Columns(2).HeaderText = "شرح"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 200
        Me.DataGridView1.Columns(2).Width = 1200
    End Sub

    Private Sub Clear()
        Me.txtNameF.Clear()
        txtNameF.Enabled = True
        Me.txtName.Clear()
        Me.txtNameF.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        txtNameF.Enabled = False
        Me.txtNameF.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(2).Value
    End Sub


    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillMaxRow()
        Clear()
    End Sub

    Private Sub Del()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید کد " & Trim(Me.txtCode.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtCode.Focus()
            Exit Sub
        Else
            Btn = 3
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub Edt()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtNameF.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام فلدر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtNameF.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Code, NameF FROM Bnk.BankSrch WHERE (Code = " & txtCode.Text & ") AND (NameF = N'" & txtNameF.Text & "')", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, " Bnk.BankSrch")
            objDataview = New DataView(objDataset.Tables(" Bnk.BankSrch"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                Btn = 2
                InsertUpdate()

                Ref.PerformClick()
            Else
                MsgbOK.Label1.Text = " نام فلدر تغییر کرده است لطفا اصلاح نمائید . "
                MsgbOK.ShowDialog()
                Me.txtNameF.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdBankSrch"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Code", txtCode.Text)
            objCommand.Parameters.AddWithValue("@NameF", txtNameF.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)
            objCommand.Parameters.AddWithValue("@Shob", Shob)
            objCommand.Parameters.AddWithValue("@Sal", Sal)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtNameF.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام فلدر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtNameF.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtCode.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillMaxRow()
            Btn = 1
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        If ColumnSrch = 1 Or ColumnSrch = 2 Then
            Label4.Text = Me.DataGridView1.Columns(e.ColumnIndex).HeaderText
        Else
            Label4.Text = "---------"
        End If
        txtSrch.Text = ""
        txtSrch.Focus()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub BankSrch_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillMaxRow()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub BankSrch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Then
            If btnSave.Enabled = True Then
                Sav()
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            If btnEdit.Enabled = True Then
                Edt()
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            If btnDelete.Enabled = True Then
                Del()
            End If
        ElseIf e.KeyCode = Keys.Insert Then
            Clear()
        End If
    End Sub

    Private Sub BankSrch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        txtFile.SendToBack()
        Label5.SendToBack()
        'cboFile.SelectedIndex = 0
        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub BankSrch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        Me.txtCode.SelectionStart = 0
        Me.txtCode.SelectionLength = Len(Me.txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub btnCreateDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateDir.Click
        Dim FBD As New FolderBrowserDialog
        FBD.RootFolder = Environment.SpecialFolder.MyComputer
        FBD.Description = "مسیر ایجاد دایرکتوری : ITsystem\BankDoc"
        FBD.ShowNewFolderButton = True
        FBD.ShowDialog()
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillMaxRow()
        Clear()
        Label4.Text = "---------"
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub txtNameF_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNameF.GotFocus
        Me.txtNameF.SelectionStart = 0
        Me.txtNameF.SelectionLength = Len(Me.txtNameF.Text)
    End Sub

    Private Sub txtNameF_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameF.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtNameF_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameF.TextChanged

    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        Select Case ColumnSrch
            Case "1"
                objDataview.RowFilter = "NameF like '" & "**" & Me.txtSrch.Text & "**" & "'"
            Case "2"
                objDataview.RowFilter = "Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End Select
        FillDataGridView()
    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim X As Integer
        ' Dim Rw As String
        Dim Folder As String

        X = DataGridView1.CurrentCell.RowIndex
        ' Rw = DataGridView1.Rows(X).Cells(0).Value
        Folder = DataGridView1.Rows(X).Cells(1).Value
        Try
            'If txtFile.Text = 0 Then
            '    FileFormat = Trim(Rw) + "." + "xlsx"
            'ElseIf txtFile.Text = 1 Then
            '    FileFormat = Trim(Rw) + "." + "docx"
            'ElseIf txtFile.Text = 2 Then
            '    FileFormat = Trim(Rw) + "." + "pdf"
            'ElseIf txtFile.Text = 3 Then
            '    FileFormat = Trim(Rw) + "." + "jpg"
            'End If

            Me.DirectoryEntry1.Path = My.Application.Info.DirectoryPath & "\BankDoc\" + Folder ' جستجوی مسیر
            'System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path + "\" + FileFormat) ' اجرای فایل
            System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path) ' باز کردن مسیر
        Catch ex As Exception
            MsgbOK.Label1.Text = " مسیر  " & Me.DirectoryEntry1.Path & " پیدا نشد ."
            MsgbOK.ShowDialog()
        End Try
    End Sub
End Class