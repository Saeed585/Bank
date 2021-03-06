
Public Class Phone
    Private Sub FillRowMax()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Phn.Phone", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.Phone")
        objDataview = New DataView(objDataset.Tables("Phn.Phone"))
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

    Private Sub FillcboGSales()
        Dim objDataAdapter As New SqlDataAdapter _
             (" SELECT Gpers_code, Gpers_name FROM Bas.GPers WHERE(Grp_code = 1) ORDER BY Gpers_code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.GPers")
        Me.cboGSales.DataSource = objDataset.Tables("Bas.GPers")
        Me.cboGSales.DisplayMember = "Gpers_name"
        Me.cboGSales.ValueMember = "Gpers_code"
    End Sub

    Private Sub FillcboMove()
        Dim objDataAdapter As New SqlDataAdapter _
             (" SELECT Gpers_code, Gpers_name FROM Bas.GPers WHERE(Grp_code = 1) ORDER BY Gpers_code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.GPers")
        Me.cboMove.DataSource = objDataset.Tables("Bas.GPers")
        Me.cboMove.DisplayMember = "Gpers_name"
        Me.cboMove.ValueMember = "Gpers_code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Shob As String
        Dim GScode As String

        GScode = cboGSales.SelectedValue.ToString
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If CheckBox1.Checked = True Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Name, Family, Comp, Country, City, Fax, Mobil, Post, Meli, ShMeli, Egh, Email, Web, Adr, Dscr, Job, GScode, Shob_Code FROM Phn.Phone WHERE (Shob_Code = " & Shob & ") AND (U_code = " & User_Code & ") AND (GSCode <> 0)", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Phn.Phone")
            objDataview = New DataView(objDataset.Tables("Phn.Phone"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Row"
        Else
            If RB1.Checked = True Then
                Dim objDataAdapter As New SqlDataAdapter _
                    (" SELECT Row, Name, Family, Comp, Country, City, Fax, Mobil, Post, Meli, ShMeli, Egh, Email, Web, Adr, Dscr, Job, GScode, Shob_Code FROM Phn.Phone WHERE (GSCode = 0)", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "Phn.Phone")
                objDataview = New DataView(objDataset.Tables("Phn.Phone"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Row"
            ElseIf RB2.Checked = True Then
                Dim objDataAdapter As New SqlDataAdapter _
                    (" SELECT Row, Name, Family, Comp, Country, City, Fax, Mobil, Post, Meli, ShMeli, Egh, Email, Web, Adr, Dscr, Job, GScode, Shob_Code FROM Phn.Phone WHERE (Shob_Code = " & Shob & ") AND (U_code = " & User_Code & ") AND (GSCode = " & GScode & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "Phn.Phone")
                objDataview = New DataView(objDataset.Tables("Phn.Phone"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Row"
            End If
        End If
        txt1.Text = ""
    End Sub

    Private Sub FillDataGridView()
        If objDataview.Count > 0 Then
            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview
        Else
            Me.DataGridView1.AutoGenerateColumns = True
            objDataview.Dispose()
            Me.DataGridView1.DataSource = objDataview
        End If
        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "نام"
        Me.DataGridView1.Columns(2).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(3).HeaderText = "شرکت"
        Me.DataGridView1.Columns(4).HeaderText = "کد کشور"
        Me.DataGridView1.Columns(5).HeaderText = "کد شهر"
        Me.DataGridView1.Columns(6).HeaderText = "فاکس"
        Me.DataGridView1.Columns(7).HeaderText = "موبایل"
        Me.DataGridView1.Columns(8).HeaderText = "کد پستی"
        Me.DataGridView1.Columns(9).HeaderText = "کد ملی"
        Me.DataGridView1.Columns(10).HeaderText = "شناسه ملی"
        Me.DataGridView1.Columns(11).HeaderText = "کد اقتصادی"
        Me.DataGridView1.Columns(12).HeaderText = "ایمیل"
        Me.DataGridView1.Columns(13).HeaderText = "وب سایت"
        Me.DataGridView1.Columns(14).HeaderText = "آدرس"
        Me.DataGridView1.Columns(15).HeaderText = "شرح"
        Me.DataGridView1.Columns(16).HeaderText = "شغل"
        Me.DataGridView1.Columns(17).Visible = False ' GScode
        Me.DataGridView1.Columns(18).Visible = False ' Shob

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 80
        Me.DataGridView1.Columns(2).Width = 120
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(4).Width = 60
        Me.DataGridView1.Columns(5).Width = 60
        Me.DataGridView1.Columns(12).Width = 200
        Me.DataGridView1.Columns(13).Width = 200
        Me.DataGridView1.Columns(14).Width = 200
        Me.DataGridView1.Columns(15).Width = 200
    End Sub

    Private Sub Clear()
        txtName.Clear()
        txtFamily.Clear()
        txtComp.Clear()
        txtCountry.Clear()
        txtCity.Clear()
        txtFax.Clear()
        txtMobil.Clear()
        txtPost.Clear()
        txtMeli.Clear()
        txtShMeli.Clear()
        txtEgh.Clear()
        txtEmail.Clear()
        txtWeb.Clear()
        txtAdr.Clear()
        txtDscr.Clear()
        txtJob.Clear()
        txtName.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Single

        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        txtName.Text = DataGridView1.Rows(X).Cells(1).Value
        txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
        txtComp.Text = DataGridView1.Rows(X).Cells(3).Value
        txtCountry.Text = DataGridView1.Rows(X).Cells(4).Value
        txtCity.Text = DataGridView1.Rows(X).Cells(5).Value
        txtFax.Text = DataGridView1.Rows(X).Cells(6).Value
        txtMobil.Text = DataGridView1.Rows(X).Cells(7).Value
        txtPost.Text = DataGridView1.Rows(X).Cells(8).Value
        txtMeli.Text = DataGridView1.Rows(X).Cells(9).Value
        txtShMeli.Text = DataGridView1.Rows(X).Cells(10).Value
        txtEgh.Text = DataGridView1.Rows(X).Cells(11).Value
        txtEmail.Text = DataGridView1.Rows(X).Cells(12).Value
        txtWeb.Text = DataGridView1.Rows(X).Cells(13).Value
        txtAdr.Text = DataGridView1.Rows(X).Cells(14).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(15).Value
        txtJob.Text = DataGridView1.Rows(X).Cells(16).Value
        If RB2.Checked = True Then
            cboGSales.SelectedValue = DataGridView1.Rows(X).Cells(17).Value
        End If
    End Sub
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Phone_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRowMax()
        FillcboGSales()
        FillcboMove()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub Phone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            FillRowMax()
            Clear()
        End If
    End Sub

    Private Sub Phone_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        Gscode = cboGSales.SelectedValue.ToString
        txtClm.SendToBack()
        txt1.SendToBack()
        txtA.SendToBack()
        txtB.SendToBack()
        txtC.SendToBack()
        KeyPreview = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRowMax()
        Clear()
    End Sub

    Private Sub InsertUpdate()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If RB1.Checked = True Then
            Rb = 1
            Gscode = 0
        ElseIf RB2.Checked = True Then
            Rb = 2
            Gscode = cboGSales.SelectedValue.ToString
        End If

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdPhone"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Ucode", User_Code)
            objCommand.Parameters.AddWithValue("@Rb", Rb)
            objCommand.Parameters.AddWithValue("@Gscode", Gscode)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)
            objCommand.Parameters.AddWithValue("@Family", txtFamily.Text)
            objCommand.Parameters.AddWithValue("@Comp", txtComp.Text)
            objCommand.Parameters.AddWithValue("@Country", txtCountry.Text)
            objCommand.Parameters.AddWithValue("@City", txtCity.Text)
            objCommand.Parameters.AddWithValue("@Fax", txtFax.Text)
            objCommand.Parameters.AddWithValue("@Mobil", txtMobil.Text)
            objCommand.Parameters.AddWithValue("@Post", txtPost.Text)
            objCommand.Parameters.AddWithValue("@Meli", txtMeli.Text)
            objCommand.Parameters.AddWithValue("@ShMeli", txtShMeli.Text)
            objCommand.Parameters.AddWithValue("@Egh", txtEgh.Text)
            objCommand.Parameters.AddWithValue("@Email", txtEmail.Text)
            objCommand.Parameters.AddWithValue("@Web", txtWeb.Text)
            objCommand.Parameters.AddWithValue("@Adr", txtAdr.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Job", txtJob.Text)
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
        Dim objCommand As New SqlCommand
        Dim Shob As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید ."
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        ElseIf Me.txtFamily.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید ."
            MsgbOK.ShowDialog()
            txtFamily.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Phn.Phone WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.Phone")
        objDataview = New DataView(objDataset.Tables("Phn.Phone"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillRowMax()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        Dim objCommand As New SqlCommand
        Dim Shob As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید ."
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        ElseIf Me.txtFamily.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید ."
            MsgbOK.ShowDialog()
            txtFamily.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Phn.Phone WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.Phone")
        objDataview = New DataView(objDataset.Tables("Phn.Phone"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillRowMax()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        txtClm.Text = e.ColumnIndex
        If txtClm.Text <> 0 Then
            Me.Label18.Text = Me.DataGridView1.Columns(e.ColumnIndex).HeaderText
        End If
        txtSrch.Text = ""
        txtSrch.Focus()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub Phone_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.SelectionStart = 0
        txtName.SelectionLength = Len(txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtFamily.Focus()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtComp.Focus()
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged

    End Sub

    Private Sub txtComp_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComp.GotFocus
        txtComp.SelectionStart = 0
        txtComp.SelectionLength = Len(txtComp.Text)
    End Sub

    Private Sub txtComp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtComp.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCountry.Focus()
        End If
    End Sub

    Private Sub txtComp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComp.TextChanged

    End Sub

    Private Sub txtCountry_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountry.GotFocus
        txtCountry.SelectionStart = 0
        txtCountry.SelectionLength = Len(txtCountry.Text)
    End Sub

    Private Sub txtCountry_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCountry.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCity.Focus()
        End If
    End Sub

    Private Sub txtCountry_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountry.TextChanged

    End Sub

    Private Sub txtCity_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCity.GotFocus
        txtCity.SelectionStart = 0
        txtCity.SelectionLength = Len(txtCity.Text)
    End Sub

    Private Sub txtCity_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtFax.Focus()
        End If
    End Sub

    Private Sub txtFax_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFax.GotFocus
        txtFax.SelectionStart = 0
        txtFax.SelectionLength = Len(txtFax.Text)
    End Sub

    Private Sub txtFax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFax.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMobil.Focus()
        End If
    End Sub

    Private Sub txtFax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFax.TextChanged

    End Sub

    Private Sub txtMobil_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMobil.GotFocus
        txtMobil.SelectionStart = 0
        txtMobil.SelectionLength = Len(txtMobil.Text)
    End Sub

    Private Sub txtMobil_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobil.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPost.Focus()
        End If
    End Sub

    Private Sub txtMobil_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobil.TextChanged

    End Sub

    Private Sub txtPost_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPost.GotFocus
        txtPost.SelectionStart = 0
        txtPost.SelectionLength = Len(txtPost.Text)
    End Sub

    Private Sub txtPost_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPost.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMeli.Focus()
        End If
    End Sub

    Private Sub txtPost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPost.TextChanged

    End Sub

    Private Sub txtEmail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.GotFocus
        txtEmail.SelectionStart = 0
        txtEmail.SelectionLength = Len(txtEmail.Text)
    End Sub

    Private Sub txtEmail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtWeb.Focus()
        End If
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged

    End Sub

    Private Sub txtWeb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeb.GotFocus
        txtWeb.SelectionStart = 0
        txtWeb.SelectionLength = Len(txtWeb.Text)
    End Sub

    Private Sub txtWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeb.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtJob.Focus()
        End If
    End Sub

    Private Sub txtWeb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeb.TextChanged

    End Sub

    Private Sub txtAdr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdr.GotFocus
        txtAdr.SelectionStart = 0
        txtAdr.SelectionLength = Len(txtAdr.Text)
    End Sub

    Private Sub txtAdr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdr.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtAdr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdr.TextChanged

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

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub Del()
        Dim objCommand As New SqlCommand
        Dim Shob As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Phn.Phone WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.Phone")
        objDataview = New DataView(objDataset.Tables("Phn.Phone"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtName.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                FillRowMax()
                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        If txtSrch.Text <> "" Then
            Select Case txtClm.Text
                Case "1"
                    objDataview.RowFilter = "Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "2"
                    objDataview.RowFilter = "Family like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "3"
                    objDataview.RowFilter = "Comp like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "4"
                    objDataview.RowFilter = "Country like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "5"
                    objDataview.RowFilter = "City like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "6"
                    objDataview.RowFilter = "Phn1 like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "7"
                    objDataview.RowFilter = "Phn2 like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "8"
                    objDataview.RowFilter = "Phn3 like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "9"
                    objDataview.RowFilter = "Fax like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "10"
                    objDataview.RowFilter = "Mobil like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "11"
                    objDataview.RowFilter = "Post like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "12"
                    objDataview.RowFilter = "Email like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "13"
                    objDataview.RowFilter = "Web like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "14"
                    objDataview.RowFilter = "Adr like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "15"
                    objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "16"
                    objDataview.RowFilter = "Job like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End Select
        Else
            FillDatasetAndDataview()
        End If
        FillDataGridView()
    End Sub

    Private Sub btnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRef.Click
        FillRowMax()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub MeliChk()
        Dim Ln As String
        Dim X As Integer

        Ln = Len(txtMeli.Text)
        If txtMeli.Text <> "" And Ln = 10 Then
            txtA.Text = Mid(txtMeli.Text, 10, 1)
            txtB.Text = 0
            For X = 1 To Ln - 1
                If X = 1 Then
                    txt1.Text = Mid(txtMeli.Text, 1, 1) * 10
                ElseIf X = 2 Then
                    txt1.Text = Mid(txtMeli.Text, 2, 1) * 9
                ElseIf X = 3 Then
                    txt1.Text = Mid(txtMeli.Text, 3, 1) * 8
                ElseIf X = 4 Then
                    txt1.Text = Mid(txtMeli.Text, 4, 1) * 7
                ElseIf X = 5 Then
                    txt1.Text = Mid(txtMeli.Text, 5, 1) * 6
                ElseIf X = 6 Then
                    txt1.Text = Mid(txtMeli.Text, 6, 1) * 5
                ElseIf X = 7 Then
                    txt1.Text = Mid(txtMeli.Text, 7, 1) * 4
                ElseIf X = 8 Then
                    txt1.Text = Mid(txtMeli.Text, 8, 1) * 3
                ElseIf X = 9 Then
                    txt1.Text = Mid(txtMeli.Text, 9, 1) * 2
                End If
                txtB.Text = Val(txtB.Text) + Val(txt1.Text)
            Next
            txtC.Text = Val(txtB.Text) - Int(txtB.Text / 11) * 11

            If txtC.Text = 0 And txtA.Text = txtC.Text Then
                txtMeli.ForeColor = Color.Green
            ElseIf txtC.Text = 1 And txtA.Text = 1 Then
                txtMeli.ForeColor = Color.Green
            ElseIf txtC.Text > 1 And txtA.Text = (11 - txtC.Text) Then
                txtMeli.ForeColor = Color.Green
            Else
                txtMeli.ForeColor = Color.Red
            End If
        Else
            txtMeli.ForeColor = Color.Red
        End If
    End Sub

    Private Sub txtMeli_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMeli.GotFocus
        txtMeli.SelectionStart = 0
        txtMeli.SelectionLength = Len(txtMeli.Text)
    End Sub

    Private Sub txtMeli_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMeli.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtShMeli.Focus()
        End If
    End Sub

    Private Sub txtMeli_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMeli.LostFocus
        MeliChk()
    End Sub

    Private Sub txtMeli_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMeli.TextChanged

    End Sub

    Private Sub txtShMeli_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShMeli.GotFocus
        txtShMeli.SelectionStart = 0
        txtShMeli.SelectionLength = Len(txtShMeli.Text)
    End Sub

    Private Sub txtShMeli_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShMeli.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtEgh.Focus()
        End If
    End Sub

    Private Sub txtShMeli_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShMeli.TextChanged

    End Sub

    Private Sub txtEgh_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEgh.GotFocus
        txtEgh.SelectionStart = 0
        txtEgh.SelectionLength = Len(txtEgh.Text)
    End Sub

    Private Sub txtEgh_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEgh.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtEmail.Focus()
        End If
    End Sub

    Private Sub txtEgh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEgh.TextChanged

    End Sub

    Private Sub btnVisit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisit.Click
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_code FROM Phn.Phone WHERE (Row = " & txtRow.Text & ") AND (Shob_code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.Phone")
        objDataview = New DataView(objDataset.Tables("Phn.Phone"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            Cartvisit.Show()
            Cartvisit.Activate()
        Else
            MsgbOK.Label1.Text = " رکورد مورد نظر را انتخاب نمائید . "
            MsgbOK.ShowDialog()
            btnVisit.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub btnPhnList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhnList.Click
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_code FROM Phn.Phone WHERE (Row = " & txtRow.Text & ") AND (Shob_code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Phn.Phone")
        objDataview = New DataView(objDataset.Tables("Phn.Phone"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            PhnList.Show()
            PhnList.Activate()
        Else
            MsgbOK.Label1.Text = " شماره برگه " & txtRow.Text & " در سیستم ثبت نشده است ."
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            cboMove.Enabled = True
            btnMove.Enabled = True
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
        Else
            cboMove.Enabled = False
            btnMove.Enabled = False
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnDelete.Enabled = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        btnRef.PerformClick()
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim objCommand As New SqlCommand
        Dim x As Integer
        Dim Rw As String
        Dim cnt As String

        cnt = Me.DataGridView1.Rows.Count - 1

        MsgbYN.Label1.Text = "  آیا می خواهید رکورد فوق به گروه  " & cboMove.Text & "  منتقل شود ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtName.Focus()
            Exit Sub
        End If

        For x = 0 To cnt
            If DataGridView1.Rows(x).Selected = True Then
                Rw = Me.DataGridView1.Rows(x).Cells(0).Value
                Gscode = Me.DataGridView1.Rows(x).Cells(20).Value

                If Gscode <> Me.cboMove.SelectedValue.ToString Then
                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                       " UPDATE Phn.Phone SET GSCode = " & Me.cboMove.SelectedValue.ToString & " WHERE (Row = " & Rw & ") AND (GSCode = " & Gscode & ")"
                    objCommand.CommandType = CommandType.Text

                    objConnection.Open()
                    Try
                        objCommand.ExecuteNonQuery()
                    Catch SqlExceptionErr As SqlException
                        MessageBox.Show(SqlExceptionErr.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()
                Else
                    MsgbOK.Label1.Text = "رکورد فوق در گروه '" & cboMove.Text & "' می باشد . "
                    MsgbOK.ShowDialog()
                    Me.txtName.Focus()
                    Exit Sub
                End If
            End If
        Next x

        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub cboGSales_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGSales.SelectedIndexChanged

    End Sub

    Private Sub cboGSales_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGSales.SelectedValueChanged
        If Me.cboGSales.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            Gscode = cboGSales.SelectedValue.ToString
            FillRowMax()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub LinkMail_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkMail.LinkClicked
        If txtEmail.Text <> "" Then
            System.Diagnostics.Process.Start(txtEmail.Text)
        End If
    End Sub

    Private Sub LinkWeb_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkWeb.LinkClicked
        If txtWeb.Text <> "" Then
            System.Diagnostics.Process.Start(txtWeb.Text)
        End If
    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim X As Integer
        Dim Row As String

        If DataGridView1.RowCount <> 0 Then
            X = DataGridView1.CurrentCell.RowIndex
            Row = DataGridView1.Rows(X).Cells(0).Value
            Row = "Ph" & Row
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

                Me.DirectoryEntry1.Path = My.Application.Info.DirectoryPath & "\BankDoc\PhoneD\" + Row  ' جستجوی مسیر
                'System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path + "\" + FileFormat) ' اجرای فایل
                System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path) ' باز کردن مسیر
            Catch ex As Exception
                MsgbOK.Label1.Text = " فایل مورد نظر در مسیر " & Me.DirectoryEntry1.Path & " پیدا نشد ."
                MsgbOK.ShowDialog()
            End Try
        Else
            MsgbOK.Label1.Text = "جدول اطلاعات خالی میباشد . "
            MsgbOK.ShowDialog()
            btnFile.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtJob_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJob.GotFocus
        txtJob.SelectionStart = 0
        txtJob.SelectionLength = Len(txtJob.Text)
    End Sub

    Private Sub txtJob_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtJob.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAdr.Focus()
        End If
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB1.Click
        cboGSales.Enabled = False
        CheckBox1.Enabled = False
        btnRef.PerformClick()
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        cboGSales.Enabled = True
        CheckBox1.Enabled = True
        btnRef.PerformClick()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If DataGridView1.RowCount > 0 Then
            Try
                Dim xlapp As New Excel.Application()
                Dim xlbook As Excel.Workbook
                Dim xlsheet As Excel.Worksheet

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

                xlapp = CType(CreateObject("excel.application"), Excel.Application)
                xlbook = CType(xlapp.Workbooks.Add, Excel.Workbook)
                xlsheet = CType(xlbook.Worksheets(1), Excel.Worksheet)
                xlsheet.Application.Visible = True
                Dim i As Integer = 2
                Dim X As Integer
                With xlsheet
                    .DisplayRightToLeft = True
                    .Range("A1").Value = "ردیف"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "نام"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "نام خانوادگی"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "شرکت"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "کد کشور"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "کد شهر"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "فاکس"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "موبایل"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "کد پستی"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "کد ملی"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "شناسه ملی"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "کد اقتصادی"
                    .Range("L1").Font.Bold = True
                    .Range("M1").Value = "ایمیل"
                    .Range("M1").Font.Bold = True
                    .Range("N1").Value = "وب سایت"
                    .Range("N1").Font.Bold = True
                    .Range("O1").Value = "آدرس"
                    .Range("O1").Font.Bold = True
                    .Range("P1").Value = "شرح"
                    .Range("P1").Font.Bold = True
                    .Range("Q1").Value = "شغل"
                    .Range("Q1").Font.Bold = True
                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                            .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                            .Range("L" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            .Range("M" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                            .Range("N" & i.ToString).Value = DataGridView1.Rows(X).Cells(13).Value
                            .Range("O" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
                            .Range("P" & i.ToString).Value = DataGridView1.Rows(X).Cells(15).Value
                            .Range("Q" & i.ToString).Value = DataGridView1.Rows(X).Cells(16).Value
                            i += 1
                        End If
                    Next X
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgbOK.Label1.Text = " جدول اطلاعات خالی میباشد . "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub
        End If
    End Sub
End Class