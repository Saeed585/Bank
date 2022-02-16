Public Class Bank
    Private Sub FillCboBank()
        Dim objDataAdapter = New SqlDataAdapter _
            (" SELECT Bank_code, Bank_name FROM bas.Bank ORDER BY Bank_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Bank")
        cboBank.DataSource = objDataset.Tables("bas.Bank")
        cboBank.DisplayMember = "Bank_name"
        cboBank.ValueMember = "Bank_code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Bnk As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT bank_code, bank_name, PhonBank, Site, Dscr FROM Bas.Bank WHERE (Shob = " & Shob & ") ", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Bank")
            objDataview = New DataView(objDataset.Tables("bas.Bank"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "bank_Code"
        ElseIf Flag = 2 Then
            If cboBank.SelectedValue <> Nothing Then
                Bnk = cboBank.SelectedValue.ToString
                If Bnk <> Nothing Then
                    Dim objDataAdapter As New SqlDataAdapter _
                        (" SELECT Bas.BankShob.Shob_Code, Bas.BankShob.shob_name, Bas.BankShob.Phon1, Bas.Bank.PhonBank, Bas.Bank.Site, Bas.BankShob.Adr" & _
                        " FROM Bas.BankShob INNER JOIN Bas.Bank ON Bas.BankShob.bank_code = Bas.Bank.bank_code WHERE (Bas.BankShob.bank_code = " & Bnk & ") AND (Bas.BankShob.Shob = " & Shob & ")", objConnection)
                    objDataset = New DataSet
                    objDataAdapter.Fill(objDataset, "bas.Bankshob")
                    objDataview = New DataView(objDataset.Tables("bas.Bankshob"))
                    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                    objDataview.Sort = "shob_code"
                End If
            End If
        End If
    End Sub
    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        If Flag = 1 Then
            Me.DataGridView1.Columns(0).HeaderText = "کد"
            Me.DataGridView1.Columns(1).HeaderText = "بانک"
            Me.DataGridView1.Columns(2).HeaderText = "تلفن بانک"
            Me.DataGridView1.Columns(3).HeaderText = "آدرس سایت"
            Me.DataGridView1.Columns(4).HeaderText = "توضیحات"

            Me.DataGridView1.Columns(0).Width = 60
            Me.DataGridView1.Columns(1).Width = 150
            Me.DataGridView1.Columns(2).Width = 100
            Me.DataGridView1.Columns(3).Width = 250
            Me.DataGridView1.Columns(4).Width = 500
        ElseIf Flag = 2 Then
            If objDataview.Count > 0 Then
                Me.DataGridView1.Columns(0).HeaderText = "کد"
                Me.DataGridView1.Columns(1).HeaderText = "شعبه"
                Me.DataGridView1.Columns(2).HeaderText = "تلفن"
                Me.DataGridView1.Columns(3).HeaderText = "فاکس"
                Me.DataGridView1.Columns(4).HeaderText = "آدرس"

                Me.DataGridView1.Columns(0).Width = 70
                Me.DataGridView1.Columns(1).Width = 150
                Me.DataGridView1.Columns(2).Width = 80
                Me.DataGridView1.Columns(3).Width = 80
                Me.DataGridView1.Columns(4).Width = 300
            End If
        End If
    End Sub

    Private Sub Clear()
        If Flag = 1 Then
            txtCode.Text = ""
            txtName.Text = ""
            txtPhon2.Text = ""
            txtSite.Text = ""
            txtDscr.Text = ""
            txtName.Enabled = True
            txtCode.Focus()
        ElseIf Flag = 2 Then
            txtShobCode.Text = ""
            txtShob.Text = ""
            txtPhon1.Text = ""
            txtFax.Text = ""
            txtAdr.Text = ""
            txtShobCode.Focus()
        End If
    End Sub

    Private Sub DGVMove()
        Dim X As String

        X = Me.DataGridView1.CurrentCell.RowIndex
        If Flag = 1 Then
            txtCode.Text = Me.DataGridView1.Rows(X).Cells(0).Value
            txtName.Text = Me.DataGridView1.Rows(X).Cells(1).Value
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(2).Value) Then
                txtPhon2.Text = Me.DataGridView1.Rows(X).Cells(2).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(3).Value) Then
                txtSite.Text = Me.DataGridView1.Rows(X).Cells(3).Value
            End If
            txtDscr.Text = Me.DataGridView1.Rows(X).Cells(4).Value
        ElseIf Flag = 2 Then
            txtShobCode.Text = Me.DataGridView1.Rows(X).Cells(0).Value
            txtShob.Text = Me.DataGridView1.Rows(X).Cells(1).Value
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(2).Value) Then
                txtPhon1.Text = Me.DataGridView1.Rows(X).Cells(2).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(3).Value) Then
                txtFax.Text = Me.DataGridView1.Rows(X).Cells(3).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(4).Value) Then
                txtAdr.Text = Me.DataGridView1.Rows(X).Cells(4).Value
            End If
        End If
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.SelectionStart = 0
        txtName.SelectionLength = Len(txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPhon2.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub Bank_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Flag = 1
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub Bank_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub Bank_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        txtJar.SendToBack()
        txtKndcode.SendToBack()
        txtAct.SendToBack()
        txtMabna.SendToBack()
        txtJr.SendToBack()

        KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Bank_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub txtShob_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShob.GotFocus
        txtShob.SelectionStart = 0
        txtShob.SelectionLength = Len(txtShob.Text)
    End Sub

    Private Sub txtShob_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShob.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPhon1.Focus()
        End If
    End Sub

    Private Sub InsertUpdate()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            If Flag = 1 Then
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bas.InsUpdBank"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Code", txtCode.Text)
                objCommand.Parameters.AddWithValue("@Name", txtName.Text)
                objCommand.Parameters.AddWithValue("@PhnBnk", txtPhon2.Text)
                objCommand.Parameters.AddWithValue("@Site", txtSite.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)
            ElseIf Flag = 2 Then
                Dim Bnk As String
                Bnk = cboBank.SelectedValue.ToString

                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bas.InsUpdShob"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Code", Bnk)
                objCommand.Parameters.AddWithValue("@Shcode", txtShobCode.Text)
                objCommand.Parameters.AddWithValue("@Shname", txtShob.Text)
                objCommand.Parameters.AddWithValue("@Phn1", txtPhon1.Text)
                objCommand.Parameters.AddWithValue("@Fax", txtFax.Text)
                objCommand.Parameters.AddWithValue("@Adr", txtAdr.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)
            End If

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        Dim objCommand As New SqlCommand
        Dim Bnk As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            If txtCode.Text = "" Then
                MsgbOK.Label1.Text = " لطفا کد را وارد کنید ."
                MsgbOK.ShowDialog()
                txtCode.Focus()
                Exit Sub
            End If

            If txtName.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام بانک را وارد کنید ."
                MsgbOK.ShowDialog()
                txtName.Focus()
                Exit Sub
            End If

            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT bank_code FROM Bas.Bank WHERE (bank_code = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Bank")
            objDataview = New DataView(objDataset.Tables("bas.Bank"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "bank_Code"

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " کد بانک " & Trim(txtCode.Text) & "  قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            Else
                Btn = 1
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        ElseIf Flag = 2 Then
            If txtShob.Text = "" Then
                MsgbOK.Label1.Text = " لطفا شعبه را وارد کنید ."
                MsgbOK.ShowDialog()
                txtShob.Focus()
                Exit Sub
            End If

            Bnk = cboBank.SelectedValue.ToString
            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT bank_code, shob_code FROM Bas.BankShob WHERE (bank_code = " & Bnk & ") AND (shob_code = '" & txtShobCode.Text & "') AND (Shob = " & Shob & ")", objConnection)

            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Bankshob")
            objDataview = New DataView(objDataset.Tables("bas.Bankshob"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "bank_Code"

            If objDataview.Count <> 0 Then
                MsgbOK.Label1.Text = " کد شعبه " & txtShobCode.Text & "  قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            Else
                Btn = 1
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        Dim objCommand As New SqlCommand
        Dim Bnk As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            If txtCode.Text = "" Then
                MsgbOK.Label1.Text = " لطفا کد را وارد کنید ."
                MsgbOK.ShowDialog()
                txtCode.Focus()
                Exit Sub
            End If

            If txtName.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام بانک را وارد کنید ."
                MsgbOK.ShowDialog()
                txtName.Focus()
                Exit Sub
            End If

            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT bank_code FROM Bas.Bank WHERE (bank_code = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Bank")
            objDataview = New DataView(objDataset.Tables("bas.Bank"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "bank_Code"

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " کد بانک " & Trim(txtCode.Text) & "  قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            Else
                Btn = 2
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        ElseIf Flag = 2 Then
            If txtShob.Text = "" Then
                MsgbOK.Label1.Text = " لطفا شعبه را وارد کنید ."
                MsgbOK.ShowDialog()
                txtShob.Focus()
                Exit Sub
            End If

            Bnk = cboBank.SelectedValue.ToString
            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT bank_code, shob_code FROM Bas.BankShob WHERE (bank_code = " & Bnk & ") AND (shob_code = '" & txtShobCode.Text & "') AND (Shob = " & Shob & ")", objConnection)

            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Bankshob")
            objDataview = New DataView(objDataset.Tables("bas.Bankshob"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "bank_Code"

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " شعبه " & txtShob.Text & "  قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            Else
                Btn = 2
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub Del()
        Dim Bnk As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            If txtCode.Text = "" Then
                MsgbOK.Label1.Text = " لطفا کد را وارد کنید ."
                MsgbOK.ShowDialog()
                txtCode.Focus()
                Exit Sub
            End If

            If txtName.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام بانک را وارد کنید ."
                MsgbOK.ShowDialog()
                txtName.Focus()
                Exit Sub
            End If

            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT bank_code FROM Bas.Bank WHERE (bank_code = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Bank")
            objDataview = New DataView(objDataset.Tables("bas.Bank"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "bank_Code"

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " کد بانک " & Trim(txtCode.Text) & "  قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            Else
                MsgbYN.Label1.Text = "  آیا می خواهید کد بانک  " & Trim(txtCode.Text) & "  از سیستم حذف شود  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    txtCode.Focus()
                    Exit Sub
                Else
                    Dim objDataAdapter1 As New SqlDataAdapter _
                        (" SELECT Bcode, Shob FROM Phn.PhnList WHERE (Bcode = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
                    objDataset = New DataSet
                    objDataAdapter1.Fill(objDataset, "Phn.PhnList")
                    objDataview = New DataView(objDataset.Tables("Phn.PhnList"))
                    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

                    If objDataview.Count > 0 Then
                        MsgbOK.Label1.Text = " بانک فوق در دفترچه تلفن دارای گردش عملیاتی میباشد ."
                        MsgbOK.ShowDialog()
                        txtCode.Focus()
                        Exit Sub
                    Else
                        Btn = 3
                        InsertUpdate()

                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                    End If
                End If
            End If
        ElseIf Flag = 2 Then
            If txtShobCode.Text = "" Then
                MsgbOK.Label1.Text = " لطفا کد شعبه را وارد کنید ."
                MsgbOK.ShowDialog()
                txtShobCode.Focus()
                Exit Sub
            End If

            Bnk = cboBank.SelectedValue.ToString
            Dim objDataAdapter1 As New SqlDataAdapter _
                ("SELECT bank_code, shob_code FROM Bas.BankShob WHERE (bank_code = " & Bnk & ") AND (shob_code = '" & txtShobCode.Text & "') AND (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter1.Fill(objDataset, "bas.Bankshob")
            objDataview = New DataView(objDataset.Tables("bas.Bankshob"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "bank_Code"

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " شعبه " & txtShob.Text & "  قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            Else
                MsgbYN.Label1.Text = "  آیا می خواهید شعبه  " & txtShob.Text & "  از سیستم حذف شود  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    txtShobCode.Focus()
                    Exit Sub
                Else
                    Dim objDataAdapter2 As New SqlDataAdapter _
                        (" SELECT Bcode, Shob FROM Phn.PhnList WHERE (Bcode = " & Bnk & ") AND (Shob = " & Shob & ")", objConnection)
                    objDataset = New DataSet
                    objDataAdapter2.Fill(objDataset, "Phn.PhnList")
                    objDataview = New DataView(objDataset.Tables("Phn.PhnList"))
                    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

                    If objDataview.Count > 0 Then
                        MsgbOK.Label1.Text = " بانک فوق در دفترچه تلفن دارای گردش عملیاتی میباشد ."
                        MsgbOK.ShowDialog()
                        txtCode.Focus()
                        Exit Sub
                    Else
                        Btn = 3
                        InsertUpdate()

                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        Me.txtCode.SelectionStart = 0
        Me.txtCode.SelectionLength = Len(Me.txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text <> "" Then
                objDataview.Sort = "bank_code"
                IntPosition = objDataview.Find(txtCode.Text)
                If IntPosition = -1 Then
                    txtName.Text = ""
                    txtName.Focus()
                Else
                    If txtName.Enabled = True Then
                        objCurrencyManager.Position = IntPosition
                        txtName.DataBindings.Clear()
                        txtPhon2.DataBindings.Clear()
                        txtSite.DataBindings.Clear()
                        txtName.DataBindings.Add("Text", objDataview, "bank_name")
                        txtPhon2.DataBindings.Add("Text", objDataview, "PhonBank")
                        txtSite.DataBindings.Add("Text", objDataview, "site")
                        txtName.Focus()
                    Else
                        txtPhon2.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub cboBank_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBank.SelectedValueChanged
        If cboBank.Text <> Nothing Then
            If Me.cboBank.SelectedValue.ToString = "System.Data.DataRowView" Then
                Exit Sub
            Else
                FillDatasetAndDataview()
                FillDataGridView()
            End If
        End If
    End Sub

    Private Sub txtShobCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShobCode.GotFocus
        txtShobCode.SelectionStart = 0
        txtShobCode.SelectionLength = Len(txtShobCode.Text)
    End Sub

    Private Sub txtShobCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShobCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtShob.Focus()
        End If
    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB1.Click
        If RB1.Checked = True Then
            Flag = 1
            GRP1.Visible = True
            GRP2.Visible = False
            txtCode.Focus()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub RB2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB2.Click
        If RB2.Checked = True Then
            Flag = 2
            FillCboBank()
            GRP1.Visible = False
            GRP2.Visible = True
            FillCboBank()
            cboBank.Focus()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        If Flag = 1 Then
            objDataview.RowFilter = "bank_Name like '" & "**" & txtSrch.Text & "**" & "'"
        ElseIf Flag = 2 Then
            objDataview.RowFilter = "shob_Name like '" & "**" & txtSrch.Text & "**" & "'"
        End If
        FillDataGridView()
    End Sub

    Private Sub txtPhon1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhon1.GotFocus
        txtPhon1.SelectionStart = 0
        txtPhon1.SelectionLength = Len(txtPhon1.Text)
    End Sub

    Private Sub txtPhon1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhon1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtFax.Focus()
        End If
    End Sub

    Private Sub txtPhon1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhon1.TextChanged

    End Sub

    Private Sub txtPhon2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhon2.GotFocus
        txtPhon2.SelectionStart = 0
        txtPhon2.SelectionLength = Len(txtPhon2.Text)
    End Sub

    Private Sub txtPhon2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhon2.KeyPress
        If e.KeyChar = ChrW(13) Then
            Dim myCulture As New Globalization.CultureInfo("EN-US")
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(myCulture)
            txtSite.Focus()
        End If
    End Sub

    Private Sub txtPhon2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhon2.TextChanged

    End Sub

    Private Sub txtEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSite.Click
        Dim myCulture As New Globalization.CultureInfo("EN-US")
        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(myCulture)
    End Sub

    Private Sub txtSite_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSite.GotFocus
        txtSite.SelectionStart = 0
        txtSite.SelectionLength = Len(txtSite.Text)
    End Sub

    Private Sub txtSite_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSite.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtSite_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSite.LostFocus
        Dim myCulture As New Globalization.CultureInfo("FA-IR")
        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(myCulture)
    End Sub

    Private Sub txtSite_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSite.TextChanged

    End Sub

    Private Sub txtAdr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdr.GotFocus
        txtAdr.SelectionStart = 0
        txtAdr.SelectionLength = Len(txtAdr.Text)
    End Sub

    Private Sub txtAdr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtFax_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFax.GotFocus
        txtFax.SelectionStart = 0
        txtFax.SelectionLength = Len(txtFax.Text)
    End Sub

    Private Sub txtFax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFax.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAdr.Focus()
        End If
    End Sub

    Private Sub txtFax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFax.TextChanged

    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        txtDscr.SelectionStart = 0
        txtDscr.SelectionLength = Len(txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged

    End Sub

    Private Sub btnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRef.Click
        Flag = 1
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub
End Class