
Public Class PhnList
    Dim Da As New SqlDataAdapter
    Private Sub FillRowMax()

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Phn.PhnList WHERE (RowPhn = " & txtRowPhn.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.PhnList")
        objDataview = New DataView(objDataset.Tables("Phn.PhnList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")
        If objDataview.Count > 0 Then
            If Label6.Text = "" Then
                txtRow.Text = 1
            Else
                txtRow.Text = Label6.Text + 1
            End If
        Else
            txtRow.Text = 1
        End If
        Label6.Text = ""
        Label6.DataBindings.Clear()
    End Sub
    Private Sub FillCboBank()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bank_Code, Bank_Name FROM Bas.Bank ORDER BY Bank_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Bank")
        cboBank.DataSource = objDataset.Tables("Bas.Bank")
        cboBank.DisplayMember = "Bank_Name"
        cboBank.ValueMember = "Bank_Code"
    End Sub
    Private Sub FillCboShob()
        Dim BCode As String

        BCode = cboBank.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.BankShob WHERE(Bank_Code = " & BCode & ") ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.BankShob")
        cboShob.DataSource = objDataset.Tables("Bas.BankShob")
        cboShob.DisplayMember = "Shob_Name"
        cboShob.ValueMember = "Shob_Code"
    End Sub
    Private Sub FillDatasetAndDataview()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Phn.ViewPhoneList WHERE (RowPhn = " & txtRowPhn.Text & ") AND (Shob = " & Shob & ") ORDER BY Row")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Phn.ViewPhoneList")
        objDataview = New DataView(objDataset.Tables("Phn.ViewPhoneList"))
        objConnection.Close()
    End Sub

    Private Sub FillDataGridView()
        DataGridView1.AutoGenerateColumns = True
        DataGridView1.DataSource = objDataview

        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).HeaderText = "ردیف"
        DataGridView1.Columns(2).HeaderText = "تلفن 1"
        DataGridView1.Columns(3).HeaderText = "تلفن 2"
        DataGridView1.Columns(4).HeaderText = "داخلی"
        DataGridView1.Columns(5).HeaderText = "فاکس"
        DataGridView1.Columns(6).HeaderText = "موبایل"
        DataGridView1.Columns(7).Visible = False
        DataGridView1.Columns(8).HeaderText = "بانک"
        DataGridView1.Columns(9).Visible = False
        DataGridView1.Columns(10).HeaderText = "شعبه"
        DataGridView1.Columns(11).HeaderText = "شماره حساب"
        DataGridView1.Columns(12).HeaderText = "شماره کارت"
        DataGridView1.Columns(13).HeaderText = "شماره شبا"
        DataGridView1.Columns(14).Visible = False

        DataGridView1.Columns(1).Width = 50
        DataGridView1.Columns(2).Width = 120
        DataGridView1.Columns(3).Width = 120
        DataGridView1.Columns(4).Width = 60
        DataGridView1.Columns(5).Width = 120
        DataGridView1.Columns(6).Width = 120
        DataGridView1.Columns(8).Width = 100
        DataGridView1.Columns(10).Width = 100
        DataGridView1.Columns(11).Width = 120
        DataGridView1.Columns(12).Width = 200
        DataGridView1.Columns(13).Width = 200
    End Sub

    Private Sub Clear()
        Me.txtPhn1.Clear()
        Me.txtPhn2.Clear()
        Me.txtDakheli.Clear()
        Me.txtFax.Clear()
        Me.txtMobil.Clear()
        Me.txtHesab.Clear()
        Me.txtC1.Clear()
        Me.txtC2.Clear()
        Me.txtC3.Clear()
        Me.txtC4.Clear()
        Me.txtS1.Clear()
        Me.txtS2.Clear()
        Me.txtS3.Clear()
        Me.txtS4.Clear()
        Me.txtS5.Clear()
        Me.txtS6.Clear()
        Me.txtPhn1.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Single

        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(1).Value
        txtPhn1.Text = DataGridView1.Rows(X).Cells(2).Value
        txtPhn2.Text = DataGridView1.Rows(X).Cells(3).Value
        txtDakheli.Text = DataGridView1.Rows(X).Cells(4).Value
        txtFax.Text = DataGridView1.Rows(X).Cells(5).Value
        txtMobil.Text = DataGridView1.Rows(X).Cells(6).Value
        cboBank.SelectedValue = DataGridView1.Rows(X).Cells(7).Value
        cboShob.SelectedValue = DataGridView1.Rows(X).Cells(9).Value
        txtHesab.Text = DataGridView1.Rows(X).Cells(11).Value

        txtC1.Text = Mid(DataGridView1.Rows(X).Cells(12).Value, 1, 4)
        txtC2.Text = Mid(DataGridView1.Rows(X).Cells(12).Value, 6, 4)
        txtC3.Text = Mid(DataGridView1.Rows(X).Cells(12).Value, 11, 4)
        txtC4.Text = Mid(DataGridView1.Rows(X).Cells(12).Value, 16, 4)

        txtS1.Text = Mid(DataGridView1.Rows(X).Cells(13).Value, 1, 4)
        txtS2.Text = Mid(DataGridView1.Rows(X).Cells(13).Value, 6, 4)
        txtS3.Text = Mid(DataGridView1.Rows(X).Cells(13).Value, 11, 4)
        txtS4.Text = Mid(DataGridView1.Rows(X).Cells(13).Value, 16, 4)
        txtS5.Text = Mid(DataGridView1.Rows(X).Cells(13).Value, 21, 4)
        txtS6.Text = Mid(DataGridView1.Rows(X).Cells(13).Value, 26, 4)
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub PhnList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub PhnList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Phone.btnRef.PerformClick()

        txtRowPhn.Text = Phone.txtRow.Text
        FillRowMax()
        FillDatasetAndDataview()
        FillDataGridView()
        Me.KeyPreview = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub InsertUpdate()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtPhn1.Text = "" Then
            txtPhn1.Text = 0
        End If

        If txtPhn2.Text = "" Then
            txtPhn2.Text = 0
        End If

        If txtDakheli.Text = "" Then
            txtDakheli.Text = 0
        End If

        If txtFax.Text = "" Then
            txtFax.Text = 0
        End If

        If txtMobil.Text = "" Then
            txtMobil.Text = 0
        End If

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Phn.InsUpdPhnList"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@RowPhn", txtRowPhn.Text)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Phone1", txtPhn1.Text)
            objCommand.Parameters.AddWithValue("@Phone2", txtPhn2.Text)
            objCommand.Parameters.AddWithValue("@Dakheli", txtDakheli.Text)
            objCommand.Parameters.AddWithValue("@Fax", txtFax.Text)
            objCommand.Parameters.AddWithValue("@Mobil", txtMobil.Text)
            objCommand.Parameters.AddWithValue("@Bcode", cboBank.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Shcode", cboShob.SelectedValue.ToString)
            If txtHesab.Text = "" Then
                txtHesab.Text = 0
            End If
            objCommand.Parameters.AddWithValue("@Hesab", txtHesab.Text)
            objCommand.Parameters.AddWithValue("@Cart", txtC1.Text + "-" + txtC2.Text + "-" + txtC3.Text + "-" + txtC4.Text)
            objCommand.Parameters.AddWithValue("@Shaba", txtS1.Text + "-" + txtS2.Text + "-" + txtS3.Text + "-" + txtS4.Text + "-" + txtS5.Text + "-" + txtS6.Text)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtPhn1.Text = "" And txtPhn2.Text = "" And txtMobil.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تلفن و یا موبایل را وارد کنید ."
            MsgbOK.ShowDialog()
            txtPhn1.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT RowPhn, Row, Shob FROM Phn.PhnList WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ") AND (RowPhn = " & txtRowPhn.Text & ") ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.PhnList")
        objDataview = New DataView(objDataset.Tables("Phn.PhnList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " ردیف  " & Trim(Me.txtRow.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRowMax()
            Btn = 1
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            FillRowMax()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtPhn1.Text = "" And txtPhn2.Text = "" And txtMobil.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تلفن و یا موبایل را وارد کنید ."
            MsgbOK.ShowDialog()
            txtPhn1.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT RowPhn, Row, Shob FROM Phn.PhnList WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ") AND (RowPhn = " & txtRowPhn.Text & ") ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.PhnList")
        objDataview = New DataView(objDataset.Tables("Phn.PhnList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف  " & Trim(Me.txtRow.Text) & "  قبلا در سیستم ثبت نشده است  . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            FillRowMax()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtPhn1.Text = "" And txtPhn2.Text = "" And txtMobil.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تلفن و یا موبایل را وارد کنید ."
            MsgbOK.ShowDialog()
            txtPhn1.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT RowPhn, Row, Shob FROM Phn.PhnList WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ") AND (RowPhn = " & txtRowPhn.Text & ") ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.PhnList")
        objDataview = New DataView(objDataset.Tables("Phn.PhnList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف  " & Trim(Me.txtRow.Text) & "  قبلا در سیستم ثبت نشده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtPhn1.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
                FillRowMax()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
        FillRowMax()
    End Sub

    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub txtPhn1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhn1.GotFocus
        txtPhn1.SelectionStart = 0
        txtPhn1.SelectionLength = Len(txtPhn1.Text)
    End Sub

    Private Sub txtPhn1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhn1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPhn2.Focus()
        End If
    End Sub

    Private Sub txtFax_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFax.GotFocus
        txtFax.SelectionStart = 0
        txtFax.SelectionLength = Len(txtFax.Text)
    End Sub

    Private Sub txtMobil_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMobil.GotFocus
        txtMobil.SelectionStart = 0
        txtMobil.SelectionLength = Len(txtMobil.Text)
    End Sub

    Private Sub txtFax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFax.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMobil.Focus()
        End If
    End Sub

    Private Sub txtMobil_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobil.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboBank.Focus()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtHesab_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHesab.GotFocus
        Me.txtHesab.SelectionStart = 0
        Me.txtHesab.SelectionLength = Len(Me.txtHesab.Text)
    End Sub

    Private Sub txtHesab_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHesab.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtC1.Focus()
        End If
    End Sub

    Private Sub txtHesab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHesab.TextChanged

    End Sub
    Private Sub txtC4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtC4.GotFocus
        Me.txtC4.SelectionStart = 0
        Me.txtC4.SelectionLength = Len(Me.txtC4.Text)
    End Sub

    Private Sub txtC4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtC4.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtS1.Focus()
        End If
    End Sub

    Private Sub txtC4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtC4.TextChanged
        If Len(txtC4.Text) = 4 Then
            txtS1.Focus()
        End If
    End Sub

    Private Sub txtC3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtC3.GotFocus
        Me.txtC3.SelectionStart = 0
        Me.txtC3.SelectionLength = Len(Me.txtC3.Text)
    End Sub

    Private Sub txtC3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtC3.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtC4.Focus()
        End If
    End Sub

    Private Sub txtC3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtC3.TextChanged
        If Len(txtC3.Text) = 4 Then
            txtC4.Focus()
        End If
    End Sub

    Private Sub txtC2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtC2.GotFocus
        Me.txtC2.SelectionStart = 0
        Me.txtC2.SelectionLength = Len(Me.txtC2.Text)
    End Sub

    Private Sub txtC2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtC2.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtC3.Focus()
        End If
    End Sub

    Private Sub txtC2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtC2.TextChanged
        If Len(txtC2.Text) = 4 Then
            txtC3.Focus()
        End If
    End Sub

    Private Sub txtC1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtC1.GotFocus
        Me.txtC1.SelectionStart = 0
        Me.txtC1.SelectionLength = Len(Me.txtC1.Text)
    End Sub

    Private Sub txtC1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtC1.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtC2.Focus()
        End If
    End Sub

    Private Sub txtC1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtC1.TextChanged
        If Len(txtC1.Text) = 4 Then
            txtC2.Focus()
        End If
    End Sub

    Private Sub txtS1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtS1.GotFocus
        Me.txtS1.SelectionStart = 0
        Me.txtS1.SelectionLength = Len(Me.txtS1.Text)
    End Sub

    Private Sub txtS1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtS1.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtS2.Focus()
        End If
    End Sub

    Private Sub txtS1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtS1.TextChanged
        If Len(txtS1.Text) = 4 Then
            txtS2.Focus()
        End If
    End Sub

    Private Sub txtS2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtS2.GotFocus
        Me.txtS2.SelectionStart = 0
        Me.txtS2.SelectionLength = Len(Me.txtS2.Text)
    End Sub

    Private Sub txtS2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtS2.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtS3.Focus()
        End If
    End Sub

    Private Sub txtS2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtS2.TextChanged
        If Len(txtS2.Text) = 4 Then
            txtS3.Focus()
        End If
    End Sub

    Private Sub txtS3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtS3.GotFocus
        Me.txtS3.SelectionStart = 0
        Me.txtS3.SelectionLength = Len(Me.txtS3.Text)
    End Sub

    Private Sub txtS3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtS3.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtS4.Focus()
        End If
    End Sub

    Private Sub txtS3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtS3.TextChanged
        If Len(txtS3.Text) = 4 Then
            txtS4.Focus()
        End If
    End Sub

    Private Sub txtS4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtS4.GotFocus
        Me.txtS4.SelectionStart = 0
        Me.txtS4.SelectionLength = Len(Me.txtS4.Text)
    End Sub

    Private Sub txtS4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtS4.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtS5.Focus()
        End If
    End Sub

    Private Sub txtS4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtS4.TextChanged
        If Len(txtS4.Text) = 4 Then
            txtS5.Focus()
        End If
    End Sub

    Private Sub txtS5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtS5.GotFocus
        Me.txtS5.SelectionStart = 0
        Me.txtS5.SelectionLength = Len(Me.txtS5.Text)
    End Sub

    Private Sub txtS5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtS5.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtS6.Focus()
        End If
    End Sub

    Private Sub txtS5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtS5.TextChanged
        If Len(txtS5.Text) = 4 Then
            txtS6.Focus()
        End If
    End Sub

    Private Sub txtS6_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtS6.GotFocus
        Me.txtS6.SelectionStart = 0
        Me.txtS6.SelectionLength = Len(Me.txtS6.Text)
    End Sub

    Private Sub txtS6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtS6.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDakheli_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDakheli.GotFocus
        txtDakheli.SelectionStart = 0
        txtDakheli.SelectionLength = Len(txtDakheli.Text)
    End Sub

    Private Sub txtDakheli_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDakheli.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtFax.Focus()
        End If
    End Sub

    Private Sub txtDakheli_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDakheli.TextChanged

    End Sub

    Private Sub txtPhn2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhn2.GotFocus
        txtPhn2.SelectionStart = 0
        txtPhn2.SelectionLength = Len(txtPhn2.Text)
    End Sub

    Private Sub txtPhn2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhn2.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDakheli.Focus()
        End If
    End Sub

    Private Sub txtPhn2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhn2.TextChanged

    End Sub

    Private Sub txtPhn1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhn1.TextChanged

    End Sub

    Private Sub PhnList_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        FillCboBank()
        FillCboShob()
    End Sub

    Private Sub cboBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBank.SelectedIndexChanged

    End Sub

    Private Sub cboBank_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboBank.SelectedValueChanged
        If Me.cboBank.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillCboShob()
        End If
    End Sub

    Private Sub cboBank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboBank.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboShob.Focus()
        End If
    End Sub

    Private Sub cboShob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboShob.SelectedIndexChanged

    End Sub

    Private Sub cboShob_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboShob.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtHesab.Focus()
        End If
    End Sub
End Class