Public Class PersIns
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

    Public Sub FillMax()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(ID) FROM bas.PersIns", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.PersIns")
        objDataview = New DataView(objDataset.Tables("bas.PersIns"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "column1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "column1")
        If Label3.Text <> "" Then
            txtMax.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtMax.Text = 1
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub
    Public Sub FillMaxComp()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(comp_code) FROM bas.CompIns", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.CompIns")
        objDataview = New DataView(objDataset.Tables("bas.CompIns"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "column1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "column1")
        If Label3.Text <> "" Then
            maskCode.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            maskCode.Text = 1
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub
    Private Sub FillCboGpers()
        Text1.Text = ""
        If Me.RButton1.Checked = True Then
            Text1.Text = 1
        ElseIf Me.RButton2.Checked = True Then
            Text1.Text = 2
        End If
        Dim objDataAdapter = New SqlDataAdapter _
            (" SELECT Gpers_code, Gpers_name FROM bas.GPers WHERE (Grp_code = " & Text1.Text & ") ORDER BY Gpers_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.GPers")
        cboGpers.DataSource = objDataset.Tables("bas.GPers")
        cboGpers.DisplayMember = "Gpers_Name"
        cboGpers.ValueMember = "Gpers_Code"
    End Sub
    Private Sub FillCboMove()
        Text1.Text = ""
        If Me.RButton1.Checked = True Then
            Text1.Text = 1
        ElseIf Me.RButton2.Checked = True Then
            Text1.Text = 2
        End If
        Dim objDataAdapter = New SqlDataAdapter _
            (" SELECT Gpers_code, Gpers_name FROM bas.GPers WHERE (Grp_code = " & Text1.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.GPers")
        cboMove.DataSource = objDataset.Tables("bas.GPers")
        cboMove.DisplayMember = "Gpers_Name"
        cboMove.ValueMember = "Gpers_Code"
    End Sub
    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter
        Dim Gpcode As String

        Gpcode = cboGpers.SelectedValue.ToString
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Text1.Text = 1

        If CheckBox2.Checked = False Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bas.View_PersIns WHERE (Grp_code = " & Text1.Text & ") AND (Gpers_code = " & Gpcode & ") AND (Shob = " & Shob & ") ORDER BY ID")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bas.View_PersIns")
            objDataview = New DataView(objDataset.Tables("Bas.View_PersIns"))
            objConnection.Close()
        ElseIf CheckBox2.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bas.View_PersIns WHERE (Grp_code = " & Text1.Text & ") AND (Shob = " & Shob & ") ORDER BY ID")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bas.View_PersIns")
            objDataview = New DataView(objDataset.Tables("Bas.View_PersIns"))
            objConnection.Close()
        End If
    End Sub
    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد گروه"
        Me.DataGridView1.Columns(1).Visible = False
        Me.DataGridView1.Columns(2).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(3).HeaderText = "نام"
        Me.DataGridView1.Columns(4).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(5).HeaderText = "تاریخ  تولد"
        Me.DataGridView1.Columns(6).HeaderText = "نام پدر"
        Me.DataGridView1.Columns(7).HeaderText = "ش.ش"
        Me.DataGridView1.Columns(8).HeaderText = "جنسیت"
        Me.DataGridView1.Columns(9).Visible = False
        Me.DataGridView1.Columns(10).HeaderText = "شهر"
        Me.DataGridView1.Columns(11).HeaderText = "تلفن"
        Me.DataGridView1.Columns(12).HeaderText = "فاکس"
        Me.DataGridView1.Columns(13).HeaderText = "موبایل"
        Me.DataGridView1.Columns(14).HeaderText = "کد ملی"
        Me.DataGridView1.Columns(15).HeaderText = "شماره بیمه"
        Me.DataGridView1.Columns(16).HeaderText = "آدرس"
        Me.DataGridView1.Columns(17).HeaderText = "Email"
        Me.DataGridView1.Columns(18).Visible = False
        Me.DataGridView1.Columns(19).Visible = False
        Me.DataGridView1.Columns(20).HeaderText = "حساب حقوق"
        Me.DataGridView1.Columns(21).HeaderText = "بن کارت"
        Me.DataGridView1.Columns(22).HeaderText = "حساب پوز"
        Me.DataGridView1.Columns(23).Visible = False ' Grp_code
        Me.DataGridView1.Columns(24).Visible = False ' Shob

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(2).Width = 70
        Me.DataGridView1.Columns(3).Width = 80
        Me.DataGridView1.Columns(4).Width = 100
        Me.DataGridView1.Columns(5).Width = 70
        Me.DataGridView1.Columns(6).Width = 80
        Me.DataGridView1.Columns(7).Width = 80
        Me.DataGridView1.Columns(8).Width = 50
        Me.DataGridView1.Columns(10).Width = 80
        Me.DataGridView1.Columns(11).Width = 80
        Me.DataGridView1.Columns(12).Width = 80
        Me.DataGridView1.Columns(13).Width = 100
        Me.DataGridView1.Columns(14).Width = 100
        Me.DataGridView1.Columns(15).Width = 100
        Me.DataGridView1.Columns(16).Width = 130
        Me.DataGridView1.Columns(17).Width = 130

    End Sub

    Private Sub FillDatasetAndDataview2()
        Dim Da As New SqlDataAdapter
        Dim Gpcode As String
        Gpcode = cboGpers.SelectedValue.ToString

        Text1.Text = 2
        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bas.View_CompIns WHERE (Grp_code = " & Text1.Text & ") AND (GComp_code = " & Gpcode & ") ORDER BY Comp_code")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bas.View_CompIns")
        objDataview = New DataView(objDataset.Tables("Bas.View_CompIns"))
        objConnection.Close()
    End Sub
    Private Sub FillDataGridView2()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).HeaderText = "کد گروه"
        Me.DataGridView2.Columns(1).HeaderText = "کد"
        Me.DataGridView2.Columns(2).HeaderText = "نام"
        Me.DataGridView2.Columns(3).HeaderText = "کد اقتصادی"
        Me.DataGridView2.Columns(4).Visible = False
        Me.DataGridView2.Columns(5).HeaderText = "شهر"
        Me.DataGridView2.Columns(6).HeaderText = "شماره ثبت"
        Me.DataGridView2.Columns(7).HeaderText = "تاریخ ثبت"
        Me.DataGridView2.Columns(8).HeaderText = "آدرس 1"
        Me.DataGridView2.Columns(9).HeaderText = "Email1"
        Me.DataGridView2.Columns(10).HeaderText = "آدرس 2"
        Me.DataGridView2.Columns(11).HeaderText = "Email2"
        Me.DataGridView2.Columns(12).HeaderText = "آدرس 3"
        Me.DataGridView2.Columns(13).HeaderText = "Email3"

        Me.DataGridView2.Columns(0).Width = 50
        Me.DataGridView2.Columns(1).Width = 50
        Me.DataGridView2.Columns(2).Width = 130
        Me.DataGridView2.Columns(3).Width = 80
        Me.DataGridView2.Columns(5).Width = 80
        Me.DataGridView2.Columns(6).Width = 80
        Me.DataGridView2.Columns(7).Width = 80
        Me.DataGridView2.Columns(8).Width = 100
        Me.DataGridView2.Columns(9).Width = 100
        Me.DataGridView2.Columns(10).Width = 50
        Me.DataGridView2.Columns(11).Width = 50
        Me.DataGridView2.Columns(12).Width = 50
        Me.DataGridView2.Columns(13).Width = 50
    End Sub

    Private Sub MFMDscr()
        Dim X As Integer
        Dim Cnt As String
        Cnt = DataGridView1.RowCount - 1
        If Cnt >= 0 Then
            For X = 0 To Cnt
                If Me.DataGridView1.Rows(X).Cells(8).Value = "0" Then
                    Me.DataGridView1.Rows(X).Cells(8).Value = "مرد"
                ElseIf Me.DataGridView1.Rows(X).Cells(8).Value = "1" Then
                    Me.DataGridView1.Rows(X).Cells(8).Value = "زن"
                End If
            Next X
        End If
    End Sub
    Private Sub FillCboCity()
        Dim objDataAdapter = New SqlDataAdapter _
            (" SELECT City_Code, City_Name FROM bas.City ORDER BY City_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.City")
        If RButton1.Checked = True Then
            cboCity.DataSource = objDataset.Tables("bas.City")
            cboCity.DisplayMember = "City_Name"
            cboCity.ValueMember = "City_Code"
        ElseIf RButton2.Checked = True Then
            cboCity1.DataSource = objDataset.Tables("bas.City")
            cboCity1.DisplayMember = "City_Name"
            cboCity1.ValueMember = "City_Code"
        End If
    End Sub
    Private Sub DGVMove()
        Dim X As Integer
        Dim Jns As String
        Dim Pstat As String

        If RButton1.Checked = True Then
            X = Me.DataGridView1.CurrentCell.RowIndex
            txtMax.Text = Me.DataGridView1.Rows(X).Cells(1).Value
            txtPcode.Text = Me.DataGridView1.Rows(X).Cells(2).Value
            txtName.Text = Me.DataGridView1.Rows(X).Cells(3).Value
            txtFamily.Text = Me.DataGridView1.Rows(X).Cells(4).Value
            maskBirth.Text = Me.DataGridView1.Rows(X).Cells(5).Value
            txtFather.Text = Me.DataGridView1.Rows(X).Cells(6).Value
            txtIDno.Text = Me.DataGridView1.Rows(X).Cells(7).Value
            Jns = Me.DataGridView1.Rows(X).Cells(8).Value
            If Jns = "مرد" Then
                txtJens.Text = 0
            ElseIf Jns = "زن" Then
                txtJens.Text = 1
            End If
            cboCity.SelectedValue = Me.DataGridView1.Rows(X).Cells(9).Value
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(11).Value) Then
                txtPhone.Text = Me.DataGridView1.Rows(X).Cells(11).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(12).Value) Then
                txtFax.Text = Me.DataGridView1.Rows(X).Cells(12).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(13).Value) Then
                txtMobil.Text = Me.DataGridView1.Rows(X).Cells(13).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(14).Value) Then
                txtMeli.Text = Me.DataGridView1.Rows(X).Cells(14).Value
                MeliChk()
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(15).Value) Then
                txtBimeh.Text = Me.DataGridView1.Rows(X).Cells(15).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(16).Value) Then
                txtAdr.Text = Me.DataGridView1.Rows(X).Cells(16).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(17).Value) Then
                txtEmail.Text = Me.DataGridView1.Rows(X).Cells(17).Value
            End If
            If Not IsDBNull(Me.DataGridView1.Rows(X).Cells(18).Value) Then
                Pstat = Me.DataGridView1.Rows(X).Cells(18).Value
                If Pstat = "True" Then
                    CheckBox3.Checked = True
                Else
                    CheckBox3.Checked = False
                End If
            End If
            txtIDpcode.Text = Me.DataGridView1.Rows(X).Cells(19).Value
            txtHesabNoHGH.Text = Me.DataGridView1.Rows(X).Cells(20).Value
            txtBonC.Text = Me.DataGridView1.Rows(X).Cells(21).Value
            txtHesabNoPos.Text = Me.DataGridView1.Rows(X).Cells(22).Value
        ElseIf RButton2.Checked = True Then
            X = Me.DataGridView2.CurrentCell.RowIndex
            maskCode.Text = Me.DataGridView2.Rows(X).Cells(1).Value
            txtComp.Text = Me.DataGridView2.Rows(X).Cells(2).Value
            txtEgh.Text = Me.DataGridView2.Rows(X).Cells(3).Value
            cboCity1.SelectedValue = Me.DataGridView2.Rows(X).Cells(4).Value
            txtSabt.Text = Me.DataGridView2.Rows(X).Cells(6).Value
            maskDate.Text = Me.DataGridView2.Rows(X).Cells(7).Value
            txtAdr1.Text = Me.DataGridView2.Rows(X).Cells(8).Value
            txtEmail1.Text = Me.DataGridView2.Rows(X).Cells(9).Value
            txtAdr2.Text = Me.DataGridView2.Rows(X).Cells(10).Value
            txtEmail2.Text = Me.DataGridView2.Rows(X).Cells(11).Value
            txtAdr3.Text = Me.DataGridView2.Rows(X).Cells(12).Value
            txtEmail3.Text = Me.DataGridView2.Rows(X).Cells(13).Value
        End If
    End Sub
    Private Sub Clear1()
        txtName.Text = ""
        txtFamily.Text = ""
        txtFather.Text = ""
        txtPhone.Text = ""
        txtMobil.Text = ""
        txtIDno.Text = ""
        txtFax.Text = ""
        maskBirth.Text = "13  /  /"
        txtMeli.Text = ""
        txtBimeh.Text = ""
        txtAdr.Text = ""
        txtEmail.Text = ""
        CheckBox3.Checked = False
        FillMax()
        btnJPers.Enabled = False
        btnCPers.Enabled = False
        txtPcode.Text = 0
        txtIDpcode.Text = ""
        txtHesabNoHGH.Text = ""
        txtBonC.Text = ""
        txtHesabNoPos.Text = ""
        txtMeli.ForeColor = Color.Black
        txtName.Focus()
    End Sub
    Private Sub Clear2()
        maskCode.Text = "      "
        txtComp.Text = ""
        txtEgh.Text = ""
        txtSabt.Text = ""
        maskDate.Text = "13  /  /  "
        txtAdr1.Text = ""
        txtEmail1.Text = ""
        txtAdr2.Text = ""
        txtEmail2.Text = ""
        txtAdr3.Text = ""
        txtEmail3.Text = ""
        btnJComp.Enabled = False
        btnCComp.Enabled = False
        maskCode.Focus()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If RButton1.Checked = True Then
            Clear1()
            FillMax()
        ElseIf RButton2.Checked = True Then
            Clear2()
            FillMaxComp()
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub PersIns_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillMax()
        FillCboGpers()
        FillCboMove()
        FillCboCity()
        MFMDscr()
    End Sub

    Private Sub PersIns_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            If RButton1.Checked = True Then
                Clear1()
                FillMax()
            ElseIf RButton2.Checked = True Then
                Clear2()
                FillMaxComp()
            End If
        End If
    End Sub

    Private Sub PersIns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Label25.Width = My.Computer.Screen.WorkingArea.Width - 12
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        cboJens.SelectedIndex = 0
        txtMax.SendToBack()
        Text1.SendToBack()
        txtIDpcode.SendToBack()
        txtPcode.SendToBack()
        txtBtn.SendToBack()
        txtRb.SendToBack()
        txt1.SendToBack()
        txtA.SendToBack()
        txtB.SendToBack()
        txtC.SendToBack()
        txtJens.SendToBack()
        DataGridView3.SendToBack()
        KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub
    Private Sub PersIns_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 345
    End Sub

    Private Sub InsertUpdate()
        ' Dim pcode As String
        Dim Ccode As String
        Dim Gcode As String

        ' pcode = 0
        Gcode = cboGpers.SelectedValue.ToString
        Ccode = cboCity.SelectedValue.ToString
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If RButton1.Checked = True Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bas.InsUpdPersIns"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", txtBtn.Text)
                objCommand.Parameters.AddWithValue("@Rb", txtRb.Text)
                objCommand.Parameters.AddWithValue("@Grpcode", Text1.Text)
                objCommand.Parameters.AddWithValue("@Gpcode", Gcode)
                If txtBtn.Text = 1 Then
                    objCommand.Parameters.AddWithValue("@IDPcode", txtMax.Text)
                Else
                    objCommand.Parameters.AddWithValue("@IDPcode", txtIDpcode.Text)
                End If
                objCommand.Parameters.AddWithValue("@ID", txtMax.Text)
                objCommand.Parameters.AddWithValue("@pers_code", txtPcode.Text)
                objCommand.Parameters.AddWithValue("@pers_name", Trim(txtName.Text))
                objCommand.Parameters.AddWithValue("@pers_family", Trim(txtFamily.Text))
                objCommand.Parameters.AddWithValue("@Birth", maskBirth.Text)
                objCommand.Parameters.AddWithValue("@father", Trim(txtFather.Text))
                objCommand.Parameters.AddWithValue("@Idno", Trim(txtIDno.Text))
                objCommand.Parameters.AddWithValue("@mfm", txtJens.Text)
                objCommand.Parameters.AddWithValue("@city_Code", Ccode)
                objCommand.Parameters.AddWithValue("@phone", txtPhone.Text)
                objCommand.Parameters.AddWithValue("@fax", txtFax.Text)
                objCommand.Parameters.AddWithValue("@mobil", txtMobil.Text)
                objCommand.Parameters.AddWithValue("@Shaba", 0)
                objCommand.Parameters.AddWithValue("@meli", txtMeli.Text)
                objCommand.Parameters.AddWithValue("@bimeh", txtBimeh.Text)
                objCommand.Parameters.AddWithValue("@adr", txtAdr.Text)
                objCommand.Parameters.AddWithValue("@email", txtEmail.Text)
                objCommand.Parameters.AddWithValue("@pstat", CheckBox3.CheckState)
                objCommand.Parameters.AddWithValue("@HsbNoHGH", txtHesabNoHGH.Text)
                objCommand.Parameters.AddWithValue("@BonC", txtBonC.Text)
                objCommand.Parameters.AddWithValue("@HsbNoPos", txtHesabNoPos.Text)
                objCommand.Parameters.AddWithValue("@Chk", 0)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf RButton2.Checked = True Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bas.InsUpdCompIns"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", txtBtn.Text)
                objCommand.Parameters.AddWithValue("@Rb", txtRb.Text)
                objCommand.Parameters.AddWithValue("@Grpcode", Text1.Text)
                objCommand.Parameters.AddWithValue("@Gcomp_code", Gcode)
                objCommand.Parameters.AddWithValue("@comp_code", maskCode.Text)
                objCommand.Parameters.AddWithValue("@comp_name", Trim(txtComp.Text))
                objCommand.Parameters.AddWithValue("@No_egh", txtEgh.Text)
                objCommand.Parameters.AddWithValue("@city_Code", Ccode)
                objCommand.Parameters.AddWithValue("@NO_Sabt", txtSabt.Text)
                objCommand.Parameters.AddWithValue("@DatSabt", maskDate.Text)
                objCommand.Parameters.AddWithValue("@Adr1", txtAdr1.Text)
                objCommand.Parameters.AddWithValue("@email1", txtEmail1.Text)
                objCommand.Parameters.AddWithValue("@Adr2", txtAdr2.Text)
                objCommand.Parameters.AddWithValue("@email2", txtEmail2.Text)
                objCommand.Parameters.AddWithValue("@Adr3", txtAdr3.Text)
                objCommand.Parameters.AddWithValue("@email3", txtEmail3.Text)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        End If
    End Sub

    Private Sub Sav()
        Dim Gcode As String

        Gcode = cboGpers.SelectedValue.ToString
        If RButton1.Checked = True Then
            If Me.txtName.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtName.Focus()
                Exit Sub
            End If

            If Me.txtFamily.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtFamily.Focus()
                Exit Sub
            End If

            If Me.txtPhone.Text = "" Then
                MsgbOK.Label1.Text = " لطفا تلفن را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtPhone.Focus()
                Exit Sub
            End If

            FillMax()
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT ID, phone FROM bas.PersIns WHERE (ID = " & txtMax.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.PersIns")
            objDataview = New DataView(objDataset.Tables("bas.PersIns"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "ID"

            IntPosition = objDataview.Find(txtMax.Text)
            If IntPosition <> -1 Then
                MsgbOK.Label1.Text = " اطلاعات فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            Else
                Rb = 1
                Btn = 1
                txtRb.Text = Rb
                txtBtn.Text = Btn
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                MFMDscr()
                Clear1()
            End If
        ElseIf RButton2.Checked = True Then
            If Me.maskCode.Text = "      " Then
                MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
                MsgbOK.ShowDialog()
                maskCode.Focus()
                Exit Sub
            End If

            If Me.txtComp.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
                MsgbOK.ShowDialog()
                txtComp.Focus()
                Exit Sub
            End If

            If Me.txtEgh.Text = "" Then
                MsgbOK.Label1.Text = " لطفا کد اقتصادی را وارد کنید . "
                MsgbOK.ShowDialog()
                txtEgh.Focus()
                Exit Sub
            End If

            If Me.txtSabt.Text = "" Then
                MsgbOK.Label1.Text = " لطفا شماره ثبت را وارد کنید . "
                MsgbOK.ShowDialog()
                txtSabt.Focus()
                Exit Sub
            End If

            If Me.maskDate.Text = "" Then
                MsgbOK.Label1.Text = " لطفا تاریخ ثبت را وارد کنید . "
                MsgbOK.ShowDialog()
                maskDate.Focus()
                Exit Sub
            End If

            FillMaxComp()
            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT comp_code, GComp_code FROM bas.CompIns WHERE (comp_code = " & maskCode.Text & ") AND (GComp_code = " & Gcode & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.CompIns")
            objDataview = New DataView(objDataset.Tables("bas.CompIns"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Comp_code"

            IntPosition = objDataview.Find(maskCode.Text)
            If IntPosition <> -1 Then
                MsgbOK.Label1.Text = " اطلاعات فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            Else
                Rb = 2
                Btn = 1
                txtRb.Text = Rb
                txtBtn.Text = Btn
                InsertUpdate()

                FillDatasetAndDataview2()
                FillDataGridView2()
                Clear2()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        txtClm.Text = e.ColumnIndex
        If e.ColumnIndex = 3 Then
            Label27.Text = "نام"
        ElseIf e.ColumnIndex = 4 Then
            Label27.Text = "نام خانوادگی"
        ElseIf e.ColumnIndex = 8 Then
            Label27.Text = "تلفن"
        End If
        txtSrch.Text = ""
        txtSrch.Focus()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
            btnJPers.Enabled = True
            btnCPers.Enabled = True
        End If
    End Sub

    Private Sub cboGpers_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboGpers.KeyPress
        If e.KeyChar = ChrW(13) Then
            If RButton1.Checked = True Then
                txtName.Focus()
            ElseIf RButton2.Checked = True Then
                maskCode.Focus()
            End If
        End If
    End Sub

    Private Sub cboGpers_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGpers.SelectedValueChanged
        If Me.cboGpers.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            If Me.RButton1.Checked = True Then
                FillDatasetAndDataview()
                FillDataGridView()
                MFMDscr()
            ElseIf Me.RButton2.Checked = True Then
                FillDatasetAndDataview2()
                FillDataGridView2()
            End If
        End If
    End Sub

    Private Sub cboGpers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGpers.SelectedIndexChanged

    End Sub

    Private Sub Edt()
        Dim Btn As String
        Dim pcode As Integer
        'Dim Ccode As Integer
        Dim Gcode As Integer
        Dim X As String

        If DataGridView1.RowCount > 0 Then
            X = Me.DataGridView1.CurrentCell.RowIndex
            pcode = Me.DataGridView1.Rows(X).Cells(2).Value
            ' pcode = 0
            Gcode = cboGpers.SelectedValue.ToString
            If RButton1.Checked = True Then
                If Me.txtName.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtName.Focus()
                    Exit Sub
                End If

                If Me.txtFamily.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtFamily.Focus()
                    Exit Sub
                End If

                If Me.txtPhone.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا تلفن را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtPhone.Focus()
                    Exit Sub
                End If

                'Ccode = cboCity.SelectedValue.ToString
                Dim objDataAdapter As New SqlDataAdapter _
                    (" SELECT ID, phone FROM bas.PersIns WHERE (ID = " & txtMax.Text & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "bas.PersIns")
                objDataview = New DataView(objDataset.Tables("bas.PersIns"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "ID"

                IntPosition = objDataview.Find(txtMax.Text)
                If IntPosition = -1 Then
                    MsgbOK.Label1.Text = " اطلاعات فوق در سیستم ثبت نشده است . "
                    MsgbOK.ShowDialog()
                    Me.btnSave.Focus()
                    Exit Sub
                Else
                    Rb = 1
                    Btn = 2
                    txtRb.Text = Rb
                    txtBtn.Text = Btn
                    InsertUpdate()

                    FillDatasetAndDataview()
                    FillDataGridView()
                    FillMax()
                    MFMDscr()
                    Clear1()
                End If
            ElseIf RButton2.Checked = True Then
                If Me.maskCode.Text = "      " Then
                    MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
                    MsgbOK.ShowDialog()
                    maskCode.Focus()
                    Exit Sub
                End If

                If Me.txtComp.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtComp.Focus()
                    Exit Sub
                End If

                If Me.txtEgh.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا کد اقتصادی را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtEgh.Focus()
                    Exit Sub
                End If

                If Me.txtSabt.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا شماره ثبت را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtSabt.Focus()
                    Exit Sub
                End If

                If Me.maskDate.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا تاریخ ثبت را وارد کنید . "
                    MsgbOK.ShowDialog()
                    maskDate.Focus()
                    Exit Sub
                End If

                'Ccode = cboCity1.SelectedValue.ToString
                Dim objDataAdapter As New SqlDataAdapter _
                    ("SELECT comp_code, GComp_code FROM bas.CompIns WHERE (comp_code = " & maskCode.Text & ") AND (GComp_code = " & Gcode & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "bas.CompIns")
                objDataview = New DataView(objDataset.Tables("bas.CompIns"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Comp_code"

                IntPosition = objDataview.Find(maskCode.Text)
                If IntPosition = -1 Then
                    MsgbOK.Label1.Text = " اطلاعات فوق قبلا در سیستم ثبت نشده است . "
                    MsgbOK.ShowDialog()
                    btnEdit.Focus()
                    Exit Sub
                Else
                    Rb = 2
                    Btn = 2
                    txtRb.Text = Rb
                    txtBtn.Text = Btn
                    InsertUpdate()

                    FillDatasetAndDataview2()
                    FillDataGridView2()
                    Clear2()
                    FillMaxComp()
                End If
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Dim objCommand As New SqlCommand
        Dim Gpcode As String
        Dim Rb As String

        Gpcode = cboGpers.SelectedValue.ToString
        If RButton1.Checked = True Then
            If Me.txtName.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtName.Focus()
                Exit Sub
            End If

            If Me.txtFamily.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtFamily.Focus()
                Exit Sub
            End If

            If Me.txtPhone.Text = "" Then
                MsgbOK.Label1.Text = " لطفا تلفن را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtPhone.Focus()
                Exit Sub
            End If

            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT ID, phone FROM bas.PersIns WHERE (ID = " & txtMax.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.PersIns")
            objDataview = New DataView(objDataset.Tables("bas.PersIns"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "ID"

            IntPosition = objDataview.Find(txtMax.Text)
            If IntPosition = -1 Then
                MsgbOK.Label1.Text = " اطلاعات فوق در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                Me.btnSave.Focus()
                Exit Sub
            Else
                MsgbYN.Label1.Text = "  آیا می خواهید اطلاعات فوق از سیستم حذف شود  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    txtName.Focus()
                    Exit Sub
                Else
                    Dim objdataadapter1 As New SqlDataAdapter _
                        (" SELECT pers_code FROM Bnk.Computer WHERE (pers_code = " & txtIDpcode.Text & ")", objConnection)
                    objDataset = New DataSet
                    objdataadapter1.Fill(objDataset, "Bnk.Computer")
                    objDataview = New DataView(objDataset.Tables("Bnk.Computer"))
                    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                    objDataview.Sort = "pers_code"

                    If objDataview.Count > 0 Then
                        MsgbOK.Label1.Text = " پرسنل فوق در جدول کامپیوتر دارای گردش عملیاتی میباشد . "
                        MsgbOK.ShowDialog()
                        txtName.Focus()
                        Exit Sub
                    Else
                        Dim objdataadapter2 As New SqlDataAdapter _
                            (" SELECT pers_code FROM Bnk.LapTop WHERE (pers_code = " & txtIDpcode.Text & ")", objConnection)
                        objDataset = New DataSet
                        objdataadapter2.Fill(objDataset, "Bnk.LapTop")
                        objDataview = New DataView(objDataset.Tables("Bnk.LapTop"))
                        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                        objDataview.Sort = "pers_code"

                        If objDataview.Count > 0 Then
                            MsgbOK.Label1.Text = " پرسنل فوق در جدول لپ تاپ دارای گردش عملیاتی میباشد . "
                            MsgbOK.ShowDialog()
                            txtName.Focus()
                            Exit Sub
                        Else
                            Dim objdataadapter3 As New SqlDataAdapter _
                                (" SELECT pers_code FROM Bnk.Mobil WHERE (pers_code = " & txtIDpcode.Text & ")", objConnection)
                            objDataset = New DataSet
                            objdataadapter3.Fill(objDataset, "Bnk.Mobil")
                            objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
                            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                            objDataview.Sort = "pers_code"

                            If objDataview.Count > 0 Then
                                MsgbOK.Label1.Text = " پرسنل فوق در جدول موبایل دارای گردش عملیاتی میباشد . "
                                MsgbOK.ShowDialog()
                                txtName.Focus()
                                Exit Sub
                            Else
                                Dim objdataadapter4 As New SqlDataAdapter _
                                    (" SELECT pcode FROM Bnk.KalaOut WHERE (pcode = " & txtIDpcode.Text & ")", objConnection)
                                objDataset = New DataSet
                                objdataadapter4.Fill(objDataset, "Bnk.KalaOut")
                                objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
                                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                                objDataview.Sort = "pcode"

                                If objDataview.Count > 0 Then
                                    MsgbOK.Label1.Text = " پرسنل فوق در جدول صدور برگه خروج دارای گردش عملیاتی میباشد . "
                                    MsgbOK.ShowDialog()
                                    txtName.Focus()
                                    Exit Sub
                                Else
                                    Rb = 1
                                    Btn = 3
                                    txtRb.Text = Rb
                                    txtBtn.Text = Btn
                                    InsertUpdate()
                                End If
                            End If
                        End If
                    End If

                    FillDatasetAndDataview()
                    FillDataGridView()
                    FillMax()
                    MFMDscr()
                    Clear1()
                End If
            End If
        ElseIf RButton2.Checked = True Then
            If Me.maskCode.Text = "      " Then
                MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
                MsgbOK.ShowDialog()
                maskCode.Focus()
                Exit Sub
            End If

            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT comp_code, GComp_code FROM bas.CompIns WHERE (comp_code = " & maskCode.Text & ") AND (GComp_code = " & Gpcode & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.CompIns")
            objDataview = New DataView(objDataset.Tables("bas.CompIns"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Comp_code"

            IntPosition = objDataview.Find(maskCode.Text)
            If IntPosition = -1 Then
                MsgbOK.Label1.Text = " اطلاعات فوق در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            Else
                MsgbYN.Label1.Text = "  آیا می خواهید اطلاعات فوق از سیستم حذف شود  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    maskCode.Focus()
                    Exit Sub
                Else
                    Rb = 2
                    Btn = 3
                    txtRb.Text = Rb
                    txtBtn.Text = Btn
                    InsertUpdate()

                    FillDatasetAndDataview2()
                    FillDataGridView2()
                    Clear2()
                    FillMaxComp()
                End If
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub RButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RButton1.Click
        Group3.Visible = True
        Group4.Visible = False
        CheckBox2.Visible = True
        CheckBox2.Checked = False
        DataGridView1.Visible = True
        DataGridView2.Visible = False
        FillCboGpers()
        FillCboMove()
        FillMax()
        MFMDscr()
        FillCboCity()
        Clear1()
        btnJPers.Enabled = False
        cboGpers.Focus()
    End Sub

    Private Sub RButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RButton2.CheckedChanged

    End Sub

    Private Sub RButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RButton2.Click
        Group3.Visible = False
        Group4.Visible = True
        CheckBox2.Visible = False
        DataGridView1.Visible = False
        DataGridView2.Visible = True
        FillCboGpers()
        FillCboMove()
        FillCboCity()
        Clear2()
        FillMaxComp()
        btnJComp.Enabled = False
        cboGpers.Focus()
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

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboJens.Focus()
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged

    End Sub

    Private Sub txtPhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhone.GotFocus
        txtPhone.SelectionStart = 0
        txtPhone.SelectionLength = Len(txtPhone.Text)
    End Sub

    Private Sub txtPhone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMobil.Focus()
        End If
    End Sub

    Private Sub txtPhone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhone.TextChanged

    End Sub

    Private Sub txtMobil_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMobil.GotFocus
        txtMobil.SelectionStart = 0
        txtMobil.SelectionLength = Len(txtMobil.Text)
    End Sub

    Private Sub txtMobil_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobil.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIDno.Focus()
        End If
    End Sub

    Private Sub txtMobil_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobil.TextChanged

    End Sub

    Private Sub txtFax_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFax.GotFocus
        txtFax.SelectionStart = 0
        txtFax.SelectionLength = Len(txtFax.Text)
    End Sub

    Private Sub txtFax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFax.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskBirth.Focus()
        End If
    End Sub

    Private Sub txtFax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFax.TextChanged

    End Sub

    Private Sub txtAdr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdr.GotFocus
        txtAdr.SelectionStart = 0
        txtAdr.SelectionLength = Len(txtAdr.Text)
    End Sub

    Private Sub txtAdr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdr.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtEmail.Focus()
        End If
    End Sub

    Private Sub txtAdr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdr.TextChanged

    End Sub

    Private Sub txtEmail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.GotFocus
        txtEmail.SelectionStart = 0
        txtEmail.SelectionLength = Len(txtEmail.Text)
    End Sub

    Private Sub txtEmail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtHesabNoHGH.Focus()
        End If
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged

    End Sub

    Private Sub cboJens_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboJens.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtFather.Focus()
        End If
    End Sub

    Private Sub cboJens_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboJens.SelectedIndexChanged
        txtJens.Text = cboJens.SelectedIndex.ToString
    End Sub

    Private Sub txtJens_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtJens.Text = 0 Then
            cboJens.Text = "مرد"
        ElseIf txtJens.Text = 1 Then
            cboJens.Text = "زن"
        End If
    End Sub

    Private Sub cboCity_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboCity.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPhone.Focus()
        End If
    End Sub

    Private Sub cboCity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCity.SelectedIndexChanged

    End Sub

    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        maskCode.SelectionStart = 0
        maskCode.SelectionLength = Len(maskCode.Text)
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        Dim objCommand As New SqlCommand
        Dim a As String
        If e.KeyChar = ChrW(13) Then
            If Me.maskCode.Text <> "   " Then
                FillDatasetAndDataview2()
                IntPosition = objDataview.Find(Me.maskCode.Text)
                If IntPosition = -1 Then
                    a = maskCode.Text
                    Clear2()
                    maskCode.Text = a
                    txtComp.Focus()
                Else
                    objCurrencyManager.Position = IntPosition
                    txtComp.DataBindings.Clear()
                    txtEgh.DataBindings.Clear()
                    txtSabt.DataBindings.Clear()
                    maskDate.DataBindings.Clear()
                    txtAdr1.DataBindings.Clear()
                    txtEmail1.DataBindings.Clear()
                    txtAdr2.DataBindings.Clear()
                    txtEmail2.DataBindings.Clear()
                    txtAdr3.DataBindings.Clear()
                    txtEmail3.DataBindings.Clear()
                    txtAdr1.DataBindings.Clear()
                    Label3.DataBindings.Clear()

                    txtComp.DataBindings.Add("text", objDataview, "comp_name")
                    txtEgh.DataBindings.Add("text", objDataview, "no_egh")
                    Label3.DataBindings.Add("Text", objDataview, "City_Code")
                    cboCity1.SelectedValue = Me.Label3.Text
                    Label3.Text = ""
                    txtSabt.DataBindings.Add("text", objDataview, "no_sabt")
                    maskDate.DataBindings.Add("text", objDataview, "date_sabt")
                    txtAdr1.DataBindings.Add("text", objDataview, "adr1")
                    txtEmail1.DataBindings.Add("text", objDataview, "email1")
                    txtAdr2.DataBindings.Add("text", objDataview, "adr2")
                    txtEmail2.DataBindings.Add("text", objDataview, "email2")
                    txtAdr3.DataBindings.Add("text", objDataview, "adr3")
                    txtEmail3.DataBindings.Add("text", objDataview, "email3")
                    txtComp.Focus()
                End If
            End If
        End If
        FillDatasetAndDataview2()
        FillDataGridView2()
    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskCode.MaskInputRejected

    End Sub

    Private Sub txtComp_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComp.GotFocus
        txtComp.SelectionStart = 0
        txtComp.SelectionLength = Len(txtComp.Text)
    End Sub

    Private Sub txtComp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtComp.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtEgh.Focus()
        End If
    End Sub

    Private Sub txtComp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComp.TextChanged

    End Sub

    Private Sub txtEgh_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEgh.GotFocus
        txtEgh.SelectionStart = 0
        txtEgh.SelectionLength = Len(txtEgh.Text)
    End Sub

    Private Sub txtEgh_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEgh.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboCity1.Focus()
        End If
    End Sub

    Private Sub txtEgh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEgh.TextChanged

    End Sub

    Private Sub txtSabt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSabt.GotFocus
        txtSabt.SelectionStart = 0
        txtSabt.SelectionLength = Len(txtSabt.Text)
    End Sub

    Private Sub txtSabt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSabt.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDate.Focus()
        End If
    End Sub


    Private Sub txtSabt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSabt.TextChanged

    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.GotFocus
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAdr1.Focus()
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub txtAdr1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdr1.GotFocus
        txtAdr1.SelectionStart = 0
        txtAdr1.SelectionLength = Len(txtAdr1.Text)
    End Sub

    Private Sub txtAdr1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdr1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtEmail1.Focus()
        End If
    End Sub

    Private Sub txtAdr1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdr1.TextChanged

    End Sub

    Private Sub txtAdr2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdr2.GotFocus
        txtAdr2.SelectionStart = 0
        txtAdr2.SelectionLength = Len(txtAdr2.Text)
    End Sub

    Private Sub txtAdr2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdr2.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtEmail2.Focus()
        End If
    End Sub

    Private Sub txtAdr2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdr2.TextChanged

    End Sub

    Private Sub txtAdr3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdr3.GotFocus
        txtAdr3.SelectionStart = 0
        txtAdr3.SelectionLength = Len(txtAdr3.Text)
    End Sub

    Private Sub txtAdr3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdr3.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtEmail3.Focus()
        End If
    End Sub

    Private Sub txtAdr3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdr3.TextChanged

    End Sub

    Private Sub txtEmail1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail1.GotFocus
        txtEmail1.SelectionStart = 0
        txtEmail1.SelectionLength = Len(txtEmail1.Text)
    End Sub

    Private Sub txtEmail1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAdr2.Focus()
        End If
    End Sub

    Private Sub txtEmail1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail1.TextChanged

    End Sub

    Private Sub txtEmail2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail2.GotFocus
        txtEmail2.SelectionStart = 0
        txtEmail2.SelectionLength = Len(txtEmail2.Text)
    End Sub

    Private Sub txtEmail2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail2.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAdr3.Focus()
        End If
    End Sub

    Private Sub txtEmail2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail2.TextChanged

    End Sub

    Private Sub txtEmail3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail3.GotFocus
        txtEmail3.SelectionStart = 0
        txtEmail3.SelectionLength = Len(txtEmail3.Text)
    End Sub

    Private Sub txtEmail3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail3.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtEmail3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail3.TextChanged

    End Sub

    Private Sub cboCity1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboCity1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtSabt.Focus()
        End If
    End Sub

    Private Sub cboCity1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCity1.SelectedIndexChanged

    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub RButton1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RButton1.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboGpers.Focus()
        End If
    End Sub

    Private Sub RButton2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RButton2.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboGpers.Focus()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            cboMove.Enabled = True
            btnMove.Enabled = True
            GroupBox1.Enabled = False
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
        ElseIf CheckBox1.Checked = False Then
            cboMove.Enabled = False
            btnMove.Enabled = False
            GroupBox1.Enabled = True
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnDelete.Enabled = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.ColumnHeaderMouseClick
        txtClm.Text = e.ColumnIndex
        If e.ColumnIndex = 2 Then
            Label27.Text = "کد پرسنلی"
        End If
        txtSrch.Text = ""
        txtSrch.Focus()
    End Sub

    Private Sub DataGridView2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView2.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
            btnJComp.Enabled = True
            btnCComp.Enabled = True
        End If
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim objCommand As New SqlCommand
        Dim Ccode As String
        Dim Gcode As String
        Dim x As Integer
        Dim cnt As String

        If RButton1.Checked = True Then
            cnt = Me.DataGridView1.Rows.Count - 1
            For x = 0 To cnt
                If DataGridView1.Rows(x).Selected = True Then
                    If Me.DataGridView1.Rows(x).Cells(0).Value <> Me.cboMove.SelectedValue Then
                        Gcode = cboMove.SelectedValue
                        Ccode = DataGridView1.Rows(x).Cells(1).Value
                        IntPosition = objCurrencyManager.Position
                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE bas.PersIns SET Gpers_code =" & Gcode & " WHERE (ID = " & Ccode & ")"
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
                    End If
                End If
            Next x
            FillDatasetAndDataview()
            FillDataGridView()
        ElseIf RButton2.Checked = True Then
            cnt = Me.DataGridView2.Rows.Count - 1
            For x = 0 To cnt
                If DataGridView2.Rows(x).Selected = True Then
                    If Me.DataGridView2.Rows(x).Cells(0).Value <> Me.cboMove.SelectedValue Then
                        Gcode = cboMove.SelectedValue
                        Ccode = DataGridView2.Rows(x).Cells(1).Value
                        IntPosition = objCurrencyManager.Position
                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE bas.CompIns SET GComp_code = " & Gcode & " WHERE (comp_code = " & Ccode & ")"
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
                    End If
                End If
            Next x
            FillDatasetAndDataview2()
            FillDataGridView2()
        End If
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        If txtClm.Text <> "" Then
            If RButton1.Checked = True Then
                FillDatasetAndDataview()
                If txtSrch.Text <> "" Then
                    If txtClm.Text = 2 Then
                        objDataview.RowFilter = "pers_code = '" & txtSrch.Text & "'"
                    ElseIf txtClm.Text = 3 Then
                        objDataview.RowFilter = "pers_Name like '" & "**" & txtSrch.Text & "**" & "'"
                    ElseIf txtClm.Text = 4 Then
                        objDataview.RowFilter = "pers_Family like '" & "**" & txtSrch.Text & "**" & "'"
                    ElseIf txtClm.Text = 8 Then
                        objDataview.RowFilter = "phone like '" & "**" & txtSrch.Text & "**" & "'"
                    End If
                End If
                FillDataGridView()
            ElseIf RButton2.Checked = True Then
                FillDatasetAndDataview2()
                If txtSrch.Text <> "" Then
                    If txtClm.Text = 2 Then
                        objDataview.RowFilter = "comp_name like '" & "**" & txtSrch.Text & "**" & "'"
                    End If
                End If
                FillDataGridView2()
            End If
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub CheckBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox2.Click
        If CheckBox2.Checked = True Then
            cboGpers.Enabled = False
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
        Else
            cboGpers.Enabled = True
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnDelete.Enabled = True
        End If
        FillDatasetAndDataview()
        FillDataGridView()
        MFMDscr()
    End Sub

    Private Sub txtFather_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFather.GotFocus
        txtFather.SelectionStart = 0
        txtFather.SelectionLength = Len(txtFather.Text)
    End Sub

    Private Sub txtFather_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFather.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboCity.Focus()
        End If
    End Sub

    Private Sub txtFather_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFather.TextChanged

    End Sub

    Private Sub txtIDno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIDno.GotFocus
        txtIDno.SelectionStart = 0
        txtIDno.SelectionLength = Len(txtIDno.Text)
    End Sub

    Private Sub txtIDno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIDno.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtFax.Focus()
        End If
    End Sub

    Private Sub txtIDno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIDno.TextChanged

    End Sub

    Private Sub DataGridView2_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseClick
        If CheckBox1.Checked = False Then
            DGVMove()
            maskCode.Focus()
        ElseIf CheckBox1.Checked = True Then
            Dim x As Integer
            x = Me.DataGridView2.CurrentCell.RowIndex
            Me.DataGridView2.Rows(x).Cells(0).Value = Me.cboMove.SelectedValue.ToString
        End If
        btnJComp.Enabled = True
        btnCComp.Enabled = True
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        If CheckBox1.Checked = False Then
            DGVMove()
            txtName.Focus()
        ElseIf CheckBox1.Checked = True Then
            Dim x As Integer
            x = Me.DataGridView1.CurrentCell.RowIndex
            Me.DataGridView1.Rows(x).Cells(0).Value = Me.cboMove.SelectedValue.ToString
        End If
        btnJPers.Enabled = True
        btnCPers.Enabled = True
    End Sub

    Private Sub maskBirth_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskBirth.GotFocus
        maskBirth.SelectionStart = 0
        maskBirth.SelectionLength = Len(maskBirth.Text)
    End Sub

    Private Sub maskBirth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskBirth.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskBirth.Text
            Chkdate1()
            If Datstat = 1 Then
                maskBirth.Focus()
                Exit Sub
            Else
                txtMeli.Focus()
            End If
        End If
    End Sub

    Private Sub maskBirth_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskBirth.MaskInputRejected

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub btnJPers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJPers.Click
    '    If txtMax.Text = "" Then
    '        MsgbOK.Label1.Text = " لطفا از جدول زیر یک رکورد را انتخاب نمائید ."
    '        MsgbOK.ShowDialog()
    '        txtMax.Focus()
    '        Exit Sub
    '    Else
    '        JariSayer.ShowDialog()
    '    End If

    'End Sub

    'Private Sub btnJComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJComp.Click
    '    If maskCode.Text = "   " Then
    '        MsgbOK.Label1.Text = " لطفا از جدول زیر یک رکورد را انتخاب نمائید ."
    '        MsgbOK.ShowDialog()
    '        maskCode.Focus()
    '        Exit Sub
    '    Else
    '        JariSayer.ShowDialog()
    '    End If
    'End Sub

    'Private Sub btnCPers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPers.Click
    '    If txtMax.Text = "" Then
    '        MsgbOK.Label1.Text = " لطفا از جدول زیر یک رکورد را انتخاب نمائید ."
    '        MsgbOK.ShowDialog()
    '        txtMax.Focus()
    '        Exit Sub
    '    Else
    '        CheckSayer.ShowDialog()
    '    End If
    'End Sub

    'Private Sub btnCComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCComp.Click
    '    If maskCode.Text = "   " Then
    '        MsgbOK.Label1.Text = " لطفا از جدول زیر یک رکورد را انتخاب نمائید ."
    '        MsgbOK.ShowDialog()
    '        maskCode.Focus()
    '        Exit Sub
    '    Else
    '        CheckSayer.ShowDialog()
    '    End If
    'End Sub

    Private Sub txtMeli_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMeli.GotFocus
        txtMeli.SelectionStart = 0
        txtMeli.SelectionLength = Len(txtMeli.Text)
    End Sub

    Private Sub txtMeli_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMeli.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtBimeh.Focus()
        End If
    End Sub

    Private Sub txtMeli_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMeli.LostFocus
        MeliChk()
    End Sub

    Private Sub txtMeli_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMeli.TextChanged

    End Sub

    Private Sub txtBimeh_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBimeh.GotFocus
        txtBimeh.SelectionStart = 0
        txtBimeh.SelectionLength = Len(txtBimeh.Text)
    End Sub

    Private Sub txtBimeh_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBimeh.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtBonC.Focus()
        End If
    End Sub

    Private Sub txtBimeh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBimeh.TextChanged

    End Sub

    Private Sub txtHesabNoHGH_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHesabNoHGH.GotFocus
        txtHesabNoHGH.SelectionStart = 0
        txtHesabNoHGH.SelectionLength = Len(txtHesabNoHGH.Text)
    End Sub

    Private Sub txtHesabNoHGH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHesabNoHGH.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtHesabNoHGH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHesabNoHGH.TextChanged

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
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
                .Range("A1").Value = "کد پرسنلی"
                .Range("A1").Font.Bold = True
                ' .Range("A1").Font.Color = 140
                .Range("B1").Value = "نام"
                .Range("B1").Font.Bold = True
                .Range("C1").Value = "نام خانوادگی"
                .Range("C1").Font.Bold = True
                .Range("D1").Value = "تاریخ تولد"
                .Range("D1").Font.Bold = True
                .Range("E1").Value = "نام پدر"
                .Range("E1").Font.Bold = True
                .Range("F1").Value = "شماره شناسنامه"
                .Range("F1").Font.Bold = True
                .Range("G1").Value = "جنسیت"
                .Range("G1").Font.Bold = True
                .Range("H1").Value = "شهر"
                .Range("H1").Font.Bold = True
                .Range("I1").Value = "تلفن"
                .Range("I1").Font.Bold = True
                .Range("J1").Value = "فاکس"
                .Range("J1").Font.Bold = True
                .Range("K1").Value = "موبایل"
                .Range("K1").Font.Bold = True
                .Range("L1").Value = "کد ملی"
                .Range("L1").Font.Bold = True
                .Range("M1").Value = "شماره بیمه"
                .Range("M1").Font.Bold = True
                .Range("N1").Value = "آدرس"
                .Range("N1").Font.Bold = True
                .Range("O1").Value = "ایمیل"
                .Range("O1").Font.Bold = True
                .Range("P1").Value = "حساب حقوق"
                .Range("P1").Font.Bold = True
                .Range("Q1").Value = "بن کارت"
                .Range("Q1").Font.Bold = True
                .Range("R1").Value = "حساب پوز"
                .Range("R1").Font.Bold = True

                ' For X = 0 To ds.Tables(0).Rows.Count - 1
                For X = 0 To DataGridView1.RowCount - 1
                    '  X = Me.DataGridView1.CurrentCell.RowIndex
                    If DataGridView1.Rows(X).Selected = True Then
                        .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                        .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                        .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                        .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                        .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                        .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                        .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                        .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                        .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                        .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                        .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(13).Value
                        .Range("L" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
                        .Range("M" & i.ToString).Value = DataGridView1.Rows(X).Cells(15).Value
                        .Range("N" & i.ToString).Value = DataGridView1.Rows(X).Cells(16).Value
                        .Range("O" & i.ToString).Value = DataGridView1.Rows(X).Cells(17).Value
                        .Range("P" & i.ToString).Value = DataGridView1.Rows(X).Cells(20).Value
                        .Range("Q" & i.ToString).Value = DataGridView1.Rows(X).Cells(21).Value
                        .Range("R" & i.ToString).Value = DataGridView1.Rows(X).Cells(22).Value
                        i += 1
                    End If
                Next X
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtBonC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBonC.GotFocus
        txtBonC.SelectionStart = 0
        txtBonC.SelectionLength = Len(txtBonC.Text)
    End Sub

    Private Sub txtBonC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBonC.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtHesabNoPos.Focus()
        End If
    End Sub

    Private Sub txtBonC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBonC.TextChanged

    End Sub

    Private Sub Group4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Group4.Enter

    End Sub

    Private Sub txtHesabNoPos_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHesabNoPos.GotFocus
        txtHesabNoPos.SelectionStart = 0
        txtHesabNoPos.SelectionLength = Len(txtHesabNoPos.Text)
    End Sub

    Private Sub txtHesabNoPos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHesabNoPos.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAdr.Focus()
        End If
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillMax()
        FillCboGpers()
        FillCboMove()
        FillCboCity()
        MFMDscr()
        If Me.RButton1.Checked = True Then
            FillDatasetAndDataview()
            FillDataGridView()
            MFMDscr()
        ElseIf Me.RButton2.Checked = True Then
            FillDatasetAndDataview2()
            FillDataGridView2()
        End If
    End Sub

    Private Sub FormDetailChk()
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Sav, Edt, Del, Sayer FROM Bas.Form_Sec WHERE (U_Code = " & User_Code & ") AND (F_Code = " & Code_Form & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Form_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Form_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        DataGridView1.ClearSelection()
        Me.DataGridView3.AutoGenerateColumns = True
        Me.DataGridView3.DataSource = objDataview

        Me.DataGridView3.Columns(0).HeaderText = "ذخیره"
        Me.DataGridView3.Columns(1).HeaderText = "اصلاح"
        Me.DataGridView3.Columns(2).HeaderText = "حذف"
        Me.DataGridView3.Columns(3).HeaderText = "سایر"
    End Sub

    Private Sub btnPersPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPersPay.Click
        Code_Soft = 1
        Code_Form = 121
        Name_Form = "PersinfoPay"

        FormDetailChk()
        If DataGridView3.RowCount > 0 Then
            If Not IsDBNull(DataGridView3.Rows(0).Cells(0).Value) Then
                txt1.Text = DataGridView3.Rows(0).Cells(0).Value
                If Trim(txt1.Text) = "True" Then
                    PersinfoPay.btnSave.Enabled = True
                Else
                    PersinfoPay.btnSave.Enabled = False
                End If
            Else
                PersinfoPay.btnSave.Enabled = False
            End If
            txt1.Text = ""

            If Not IsDBNull(DataGridView3.Rows(0).Cells(1).Value) Then
                txt1.Text = DataGridView3.Rows(0).Cells(1).Value
                If Trim(txt1.Text) = "True" Then
                    PersinfoPay.btnEdit.Enabled = True
                Else
                    PersinfoPay.btnEdit.Enabled = False
                End If
            Else
                PersinfoPay.btnEdit.Enabled = False
            End If
            txt1.Text = ""

            If Not IsDBNull(DataGridView3.Rows(0).Cells(2).Value) Then
                txt1.Text = DataGridView3.Rows(0).Cells(2).Value
                If Trim(txt1.Text) = "True" Then
                    PersinfoPay.btnDelete.Enabled = True
                Else
                    PersinfoPay.btnDelete.Enabled = False
                End If
            Else
                PersinfoPay.btnDelete.Enabled = False
            End If
            txt1.Text = ""

            ' savCodeForm()
            PersinfoPay.Show()
            PersinfoPay.Activate()
        Else
            MsgbOK.Label1.Text = " دسترسی به فرم اطلاعات پرسنلی مقدور نمی باشد . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If
    End Sub
End Class