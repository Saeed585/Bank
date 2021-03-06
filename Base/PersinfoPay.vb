Imports System.Data.SqlClient
Imports System.IO
Public Class PersinfoPay
    Dim Str_7 As String = ""
    Dim Pic As Byte()
    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bas.View_PersPay WHERE (Shob = " & Shob & ") ORDER BY pers_code")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bas.View_PersPay")
        objDataview = New DataView(objDataset.Tables("Bas.View_PersPay"))
        objDataview.Sort = "Pers_code"
        objConnection.Close()
    End Sub

    Private Sub filldatagridview()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد سیستم"
        Me.DataGridView1.Columns(1).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(2).HeaderText = "نام"
        Me.DataGridView1.Columns(3).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(4).HeaderText = "تلفن محل کار"
        Me.DataGridView1.Columns(5).HeaderText = "تاریخ عضویت"
        Me.DataGridView1.Columns(6).Visible = False
        Me.DataGridView1.Columns(7).HeaderText = "پست سازمانی"
        Me.DataGridView1.Columns(8).Visible = False
        Me.DataGridView1.Columns(9).HeaderText = "واحد سازمانی"
        Me.DataGridView1.Columns(10).HeaderText = "وضعیت"
        Me.DataGridView1.Columns(11).HeaderText = "تاهل"
        Me.DataGridView1.Columns(12).Visible = False
        Me.DataGridView1.Columns(13).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(14).HeaderText = "پایان عضویت"
        Me.DataGridView1.Columns(15).HeaderText = "کد فرضی"
        Me.DataGridView1.Columns(16).Visible = False ' Shob

        Me.DataGridView1.Columns(0).Width = 70
        Me.DataGridView1.Columns(1).Width = 65
        Me.DataGridView1.Columns(2).Width = 100
        Me.DataGridView1.Columns(3).Width = 140
        Me.DataGridView1.Columns(4).Width = 90
        Me.DataGridView1.Columns(5).Width = 70
        Me.DataGridView1.Columns(7).Width = 90
        Me.DataGridView1.Columns(9).Width = 80
        Me.DataGridView1.Columns(10).Width = 80
        Me.DataGridView1.Columns(11).Width = 80
        Me.DataGridView1.Columns(13).Width = 100
        Me.DataGridView1.Columns(14).Width = 70
        Me.DataGridView1.Columns(15).Width = 60
    End Sub
    Private Sub FillDatasetAndDataviewPersIns()
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT ID, Gpers_code, pers_code FROM bas.PersIns WHERE (ID = " & txtID.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.PersIns")
        objDataview = New DataView(objDataset.Tables("bas.PersIns"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "ID"
    End Sub

    Private Sub FillCboArea()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Area_code, Area_name FROM Bas.Area ORDER BY Area_code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Area")
        Me.cboArea.DataSource = objDataset.Tables("Bas.Area")
        Me.cboArea.DisplayMember = "Area_name"
        Me.cboArea.ValueMember = "Area_code"
    End Sub

    Private Sub FillCboSemat()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Semat_Code, Semat_Name FROM bas.Semat ORDER BY Semat_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Semat")
        Me.cboSemat.DataSource = objDataset.Tables("bas.Semat")
        Me.cboSemat.DisplayMember = "Semat_Name"
        Me.cboSemat.ValueMember = "Semat_Code"
    End Sub
    Private Sub FillCboVahed()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Vahed_Code, Vahed_Name FROM bas.Vahed ORDER BY Vahed_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Vahed")
        Me.cboVahed.DataSource = objDataset.Tables("bas.Vahed")
        Me.cboVahed.DisplayMember = "Vahed_Name"
        Me.cboVahed.ValueMember = "Vahed_Code"
    End Sub
    Private Sub FillDatasetAndDataView2()
        Dim Da As New SqlDataAdapter
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bas.View_PersPayInfo WHERE (ID = " & txtID.Text & ") AND (Shob = " & Shob & ") ORDER BY ID")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bas.View_PersPayInfo")
        objDataview = New DataView(objDataset.Tables("Bas.View_PersPayInfo"))
        objConnection.Close()
    End Sub
    Private Sub Filldatagridview2()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).Visible = False
        Me.DataGridView2.Columns(1).HeaderText = "جنسیت"
        Me.DataGridView2.Columns(2).Visible = False
        Me.DataGridView2.Columns(3).HeaderText = "شهر"
        Me.DataGridView2.Columns(4).HeaderText = "تلفن"
        Me.DataGridView2.Columns(5).HeaderText = "فاکس"
        Me.DataGridView2.Columns(6).HeaderText = "موبایل"
        Me.DataGridView2.Columns(7).HeaderText = "آدرس"
        Me.DataGridView2.Columns(8).HeaderText = "Email"
        Me.DataGridView2.Columns(9).Visible = False ' Shob

        Me.DataGridView2.Columns(1).Width = 60
        Me.DataGridView2.Columns(3).Width = 60
        Me.DataGridView2.Columns(4).Width = 70
        Me.DataGridView2.Columns(5).Width = 70
        Me.DataGridView2.Columns(6).Width = 90
        Me.DataGridView2.Columns(7).Width = 400
        Me.DataGridView2.Columns(8).Width = 200
    End Sub
    Private Sub ClearDataGridView2()
        Me.DataGridView2.AutoGenerateColumns = True
        objDataview.Dispose()
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).Visible = False
        Me.DataGridView2.Columns(1).HeaderText = "جنسیت"
        Me.DataGridView2.Columns(2).Visible = False
        Me.DataGridView2.Columns(3).HeaderText = "شهر"
        Me.DataGridView2.Columns(4).HeaderText = "تلفن"
        Me.DataGridView2.Columns(5).HeaderText = "فاکس"
        Me.DataGridView2.Columns(6).HeaderText = "موبایل"
        Me.DataGridView2.Columns(7).HeaderText = "آدرس"
        Me.DataGridView2.Columns(8).HeaderText = "Email"

        Me.DataGridView2.Columns(1).Width = 60
        Me.DataGridView2.Columns(3).Width = 60
        Me.DataGridView2.Columns(4).Width = 70
        Me.DataGridView2.Columns(5).Width = 70
        Me.DataGridView2.Columns(6).Width = 90
        Me.DataGridView2.Columns(7).Width = 400
        Me.DataGridView2.Columns(8).Width = 200
    End Sub
    Private Sub MFMDscr()
        Dim X As Integer
        Dim Cnt As String
        Cnt = DataGridView2.RowCount - 1
        If Cnt >= 0 Then
            For X = 0 To Cnt
                If Me.DataGridView2.Rows(X).Cells(1).Value = "0" Then
                    Me.DataGridView2.Rows(X).Cells(1).Value = "مرد"
                ElseIf Me.DataGridView2.Rows(X).Cells(1).Value = "1" Then
                    Me.DataGridView2.Rows(X).Cells(1).Value = "زن"
                End If
            Next X
        End If
    End Sub
    Private Sub StatTahol()
        Dim X As Integer
        Dim Cnt As String
        Cnt = DataGridView1.RowCount - 1
        If Cnt >= 0 Then
            For X = 0 To Cnt
                If Me.DataGridView1.Rows(X).Cells(10).Value = "0" Then
                    Me.DataGridView1.Rows(X).Cells(10).Value = "فعال"
                ElseIf Me.DataGridView1.Rows(X).Cells(10).Value = "1" Then
                    Me.DataGridView1.Rows(X).Cells(10).Value = "غیر فعال"
                End If

                If Me.DataGridView1.Rows(X).Cells(11).Value = "0" Then
                    Me.DataGridView1.Rows(X).Cells(11).Value = "متاهل"
                ElseIf Me.DataGridView1.Rows(X).Cells(11).Value = "1" Then
                    Me.DataGridView1.Rows(X).Cells(11).Value = "مجرد"
                End If
            Next X
        End If
    End Sub

    Private Sub Clear()
        maskCode.Clear()
        txtPhnwrk.Clear()
        txtID.Clear()
        maskDatein.Text = ConvD.Class1.ConvDate()
        maskDateout.Text = "1   /  /"
        RB1.Checked = True
        CheckBox2.Checked = False
        maskCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim x As String

        x = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtID.Text = Me.DataGridView1.Rows(x).Cells(0).Value
        Me.maskCode.Text = Me.DataGridView1.Rows(x).Cells(1).Value
        Me.txtName.Text = Me.DataGridView1.Rows(x).Cells(2).Value
        Me.txtFamily.Text = Me.DataGridView1.Rows(x).Cells(3).Value
        Me.txtPhnwrk.Text = Me.DataGridView1.Rows(x).Cells(4).Value
        If Not IsDBNull(Me.DataGridView1.Rows(x).Cells(5).Value) Then
            Me.maskDatein.Text = Me.DataGridView1.Rows(x).Cells(5).Value
        End If
        Me.cboSemat.SelectedValue = Me.DataGridView1.Rows(x).Cells(6).Value
        Me.cboVahed.SelectedValue = Me.DataGridView1.Rows(x).Cells(8).Value

        If Me.DataGridView1.Rows(x).Cells(10).Value = "فعال" Then
            Me.RB1.Checked = True
        ElseIf Me.DataGridView1.Rows(x).Cells(10).Value = "غیر فعال" Then
            Me.RB2.Checked = True
        End If

        If Me.DataGridView1.Rows(x).Cells(11).Value = "متاهل" Then
            Me.RB3.Checked = True
        ElseIf Me.DataGridView1.Rows(x).Cells(11).Value = "مجرد" Then
            Me.RB4.Checked = True
        End If
        Me.cboArea.SelectedValue = Me.DataGridView1.Rows(x).Cells(12).Value
        maskDateout.Text = Me.DataGridView1.Rows(x).Cells(14).Value
        CheckBox2.CheckState = Me.DataGridView1.Rows(x).Cells(15).Value
    End Sub

    Private Sub PersinfoPay_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillCboArea()
        FillCboSemat()
        FillCboVahed()
        FillDatasetAndDataview()
        filldatagridview()
        StatTahol()
    End Sub

    Private Sub PersinfoPay_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            If CheckBox1.Checked = True Then
                ClearDataGridView2()
            End If
        End If
    End Sub

    Private Sub PersinfoPay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.maskDatein.Text = ConvD.Class1.ConvDate()
        Me.MdiParent = Mainfrm
        Me.tsHeader.Cursor = Cursors.Hand
        txtID.SendToBack()
        txtDat.SendToBack()
        txtPic.SendToBack()
        FormName = "PersinfoPay"
        KeyPreview = True
    End Sub

    Private Sub PersinfoPay_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        Me.maskCode.SelectionStart = 0
        Me.maskCode.SelectionLength = Len(Me.maskCode.Text)
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            FillDatasetAndDataview()
            If maskCode.Text <> "        " Then
                IntPosition = objDataview.Find(Me.maskCode.Text)
            Else
                txtFamily.Focus()
                Exit Sub
            End If
            If IntPosition = -1 Then
                Me.Label6.Text = Me.maskCode.Text
                Clear()
                Me.txtFamily.Focus()
                Me.maskCode.Text = Me.Label6.Text
                Me.Label6.Text = ""
                Exit Sub
            Else
                objCurrencyManager.Position = IntPosition
                Me.txtID.DataBindings.Clear()
                Me.txtID.DataBindings.Add("Text", objDataview, "ID")
                Me.txtName.DataBindings.Clear()
                Me.txtName.DataBindings.Add("Text", objDataview, "Pers_Name")
                Me.txtFamily.DataBindings.Clear()
                Me.txtFamily.DataBindings.Add("Text", objDataview, "Pers_Family")
                Me.txtPhnwrk.DataBindings.Clear()
                Me.txtPhnwrk.DataBindings.Add("Text", objDataview, "work_phn")
                Me.maskDatein.DataBindings.Clear()
                Me.maskDatein.DataBindings.Add("Text", objDataview, "datein")
                Me.maskDateout.DataBindings.Clear()
                Me.maskDateout.DataBindings.Add("Text", objDataview, "dateout")
                Me.Label6.DataBindings.Clear()
                Me.Label6.DataBindings.Add("Text", objDataview, "semat_code")
                Me.cboSemat.SelectedValue = Me.Label6.Text
                Me.Label6.DataBindings.Clear()
                Me.Label6.DataBindings.Add("Text", objDataview, "Vahed_code")
                Me.cboVahed.SelectedValue = Me.Label6.Text
                Me.Label6.DataBindings.Clear()
                Me.Label6.DataBindings.Add("Text", objDataview, "stat")
                If Label6.Text = 0 Then
                    RB1.Checked = True
                ElseIf Label6.Text = 1 Then
                    RB2.Checked = True
                End If
                Me.Label6.Text = ""

                Me.txtFamily.Focus()
                FillDatasetAndDataView2()
                Filldatagridview2()
                MFMDscr()
            End If
        End If
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtFamily.Focus()
        End If
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        Me.txtFamily.SelectionStart = 0
        Me.txtFamily.SelectionLength = Len(Me.txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            FormName = "PersinfoPay"
            If txtID.Text = "" Then
                FillPers1()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                txtPhnwrk.Focus()
            End If
        End If
    End Sub

    Private Sub txtPhnwrk_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhnwrk.GotFocus
        Me.txtPhnwrk.SelectionStart = 0
        Me.txtPhnwrk.SelectionLength = Len(Me.txtPhnwrk.Text)
    End Sub

    Private Sub txtPhnwrk_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhnwrk.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.maskDatein.Focus()
        End If
    End Sub

    Private Sub maskDatein_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDatein.GotFocus
        Me.maskDatein.SelectionStart = 0
        Me.maskDatein.SelectionLength = Len(Me.maskDatein.Text)
    End Sub

    Private Sub maskDatein_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDatein.KeyPress
        If e.KeyChar = ChrW(13) Then
            'Datin = maskDatein.Text
            'Chkdate()
            'If Datstat = 1 Then
            '    maskDatein.Focus()
            '    Exit Sub
            'Else
            maskDateout.Focus()
            'End If
        End If
    End Sub

    Private Sub cboSemat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboSemat.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.cboVahed.Focus()
        End If
    End Sub

    Private Sub cboVahed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboVahed.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPhnwrk.Focus()
        End If
    End Sub

    Private Sub RB1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RB1.KeyPress
        If e.KeyChar = ChrW(13) Then
            If Me.btnSave.Enabled = True Then
                Me.btnSave.Focus()
            End If
        End If
    End Sub

    Private Sub RB2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RB2.KeyPress
        If e.KeyChar = ChrW(13) Then
            If Me.btnSave.Enabled = True Then
                Me.btnSave.Focus()
            End If
        End If
    End Sub

    Private Sub Sav()
        Dim objCommand As New SqlCommand
        Dim Gpcode As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Me.maskCode.Text = "        " Then
            MsgbOK.Label1.Text = " لطفا کد پرسنل را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtID.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtFamily.Focus()
            Exit Sub
        End If

        ' Datin = maskDatein.Text
        ' Chkdate()
        ' If Datstat = 1 Then
        ' maskDatein.Focus()
        ' Exit Sub
        ' End If

        'FillDatasetAndDataview()
        'IntPosition = objDataview.Find(Me.maskCode.Text)
        Dim objdataadapter As New SqlDataAdapter _
        (" SELECT pers_code FROM Bas.PersPay WHERE (pers_code = " & maskCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.PersPay")
        objDataview = New DataView(objDataset.Tables("bas.PersPay"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "pers_code"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " کد پرسنلی  " & Trim(maskCode.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO bas.PersPay (Type_End, Type_Day, ID, vam_code, pers_code, work_phn, datein, dateout, Area_code, semat_code, Vahed_Code, stat, tahol, CodeF, Shob)" & _
               " VALUES (@TypEnd, @TypDay, @ID, @vam_code, @pers_code, @work_phn, @datein, @dateout, @Areacode, @semat_code, @Vahed_Code, @stat, @tahol, @CodeF, @Shob)"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@TypEnd", Mid(maskDateout.Text, 3, 5))
            objCommand.Parameters.AddWithValue("@TypDay", Mid(maskDateout.Text, 9, 2))
            objCommand.Parameters.AddWithValue("@ID", Me.txtID.Text)
            objCommand.Parameters.AddWithValue("@pers_code", Me.maskCode.Text)
            objCommand.Parameters.AddWithValue("@vam_code", "99")
            objCommand.Parameters.AddWithValue("@work_phn", Me.txtPhnwrk.Text)
            objCommand.Parameters.AddWithValue("@datein", Me.maskDatein.Text)
            objCommand.Parameters.AddWithValue("@dateout", Me.maskDateout.Text)
            objCommand.Parameters.AddWithValue("@Areacode", Me.cboArea.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@semat_code", Me.cboSemat.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Vahed_Code", Me.cboVahed.SelectedValue.ToString)
            If Me.RB1.Checked = True Then
                objCommand.Parameters.AddWithValue("@stat", "0")
            ElseIf Me.RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@stat", "1")
            End If

            If Me.RB3.Checked = True Then
                objCommand.Parameters.AddWithValue("@tahol", "0")
            ElseIf Me.RB4.Checked = True Then
                objCommand.Parameters.AddWithValue("@tahol", "1")
            End If
            objCommand.Parameters.AddWithValue("@CodeF", CheckBox2.CheckState)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
            '-------------------------ثبت تاریخ ورود و پایان خدمت
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO Bas.PersInOut (pers_code, datein, dateout, Shob)" & _
               " VALUES (@pers_code, @datein, @dateout, @Shob)"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@pers_code", Me.maskCode.Text)
            objCommand.Parameters.AddWithValue("@datein", Me.maskDatein.Text)
            objCommand.Parameters.AddWithValue("@dateout", Me.maskDateout.Text)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            '=============================================================
            FillDatasetAndDataviewPersIns()
            IntPosition = objDataview.Find(txtID.Text)
            If IntPosition <> -1 Then
                IntPosition = objCurrencyManager.Position
                Gpcode = 2
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                    " UPDATE bas.PersIns SET Gpers_code = @Gpcode, IDpcode = @IDPcode, pers_code = @pcode WHERE (ID = " & txtID.Text & ") AND (Shob = " & Shob & ")"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Gpcode", Gpcode)
                objCommand.Parameters.AddWithValue("@pcode", maskCode.Text)
                objCommand.Parameters.AddWithValue("@IDPcode", maskCode.Text)
            End If

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            FillDatasetAndDataview()
            filldatagridview()
            StatTahol()
            Clear()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'Dim objCommand As New SqlCommand
        'Dim Pcode As String
        'Dim stat As String
        'Dim Tahol As String
        'Dim X As Integer
        'X = Me.DataGridView1.CurrentCell.RowIndex
        'Pcode = Me.DataGridView1.Rows(X).Cells(1).Value
        'If e.ColumnIndex = 10 Then
        '    If Me.DataGridView1.Rows(X).Cells(10).Value = "فعال" Then
        '        stat = 1
        '        Me.DataGridView1.Rows(X).Cells(10).Value = "غیر فعال"
        '    ElseIf Me.DataGridView1.Rows(X).Cells(10).Value = "غیر فعال" Then
        '        stat = 0
        '        Me.DataGridView1.Rows(X).Cells(10).Value = "فعال"
        '    End If

        '    objCommand.Connection = objConnection
        '    objCommand.CommandText = _
        '       " UPDATE bas.PersPay SET stat ='" & stat & "' WHERE (pers_code = " & Pcode & ")"

        '    objConnection.Open()
        '    Try
        '        objCommand.ExecuteNonQuery()
        '    Catch SqlExceptionErr As SqlException
        '        MessageBox.Show(SqlExceptionErr.Message)
        '        objConnection.Close()
        '        Exit Sub
        '    End Try
        '    objConnection.Close()
        'ElseIf e.ColumnIndex = 11 Then
        '    If Me.DataGridView1.Rows(X).Cells(11).Value = "متاهل" Then
        '        Tahol = 1
        '        Me.DataGridView1.Rows(X).Cells(11).Value = "مجرد"
        '    ElseIf Me.DataGridView1.Rows(X).Cells(11).Value = "مجرد" Then
        '        Tahol = 0
        '        Me.DataGridView1.Rows(X).Cells(11).Value = "متاهل"
        '    End If

        '    objCommand.Connection = objConnection
        '    objCommand.CommandText = _
        '       " UPDATE bas.PersPay SET tahol ='" & Tahol & "' WHERE (pers_code = " & Pcode & ")"

        '    objConnection.Open()
        '    Try
        '        objCommand.ExecuteNonQuery()
        '    Catch SqlExceptionErr As SqlException
        '        MessageBox.Show(SqlExceptionErr.Message)
        '        objConnection.Close()
        '        Exit Sub
        '    End Try
        '    objConnection.Close()
        'End If
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If e.ColumnIndex = 1 Or e.ColumnIndex = 2 Or e.ColumnIndex = 3 Then
            Me.Label17.Text = Me.DataGridView1.Columns(e.ColumnIndex).HeaderText
        End If
        txtClm.Text = e.ColumnIndex
        If e.ColumnIndex = 1 Then
            Me.txtSrch.Visible = False
            Me.txtPSrch.Visible = True
            Me.txtPSrch.Focus()
        ElseIf e.ColumnIndex = 2 Or e.ColumnIndex = 3 Then
            Me.txtSrch.Visible = True
            Me.txtPSrch.Visible = False
            Me.txtSrch.Focus()
        End If
        Me.txtPSrch.Text = ""
        Me.txtSrch.Text = ""
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
            FillDatasetAndDataView2()
            Filldatagridview2()
            MFMDscr()
            Me.DataGridView1.Focus()
            ReadPicture()
        End If
    End Sub

    Private Sub Edt()
        Dim objCommand As New SqlCommand
        Dim Gpcode As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Me.maskCode.Text = "        " Then
            MsgbOK.Label1.Text = " لطفا کد پرسنل را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtFamily.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtFamily.Focus()
            Exit Sub
        End If

        ' Datin = maskDatein.Text
        ' Chkdate()
        ' If Datstat = 1 Then
        ' maskDatein.Focus()
        ' Exit Sub
        ' End If

        Dim objdataadapter As New SqlDataAdapter _
        (" SELECT pers_code FROM Bas.PersPay WHERE (pers_code = " & maskCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.PersPay")
        objDataview = New DataView(objDataset.Tables("bas.PersPay"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "pers_code"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " کد پرسنلی  " & Trim(Me.maskCode.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            'Dim objdataadapter As New SqlDataAdapter _
            '    (" SELECT MAX(type_code) AS Expr1 FROM Vam.Ghest GROUP BY pers_code HAVING (pers_code = " & maskCode.Text & ") ", objConnection)
            'objDataset = New DataSet
            'objdataadapter.Fill(objDataset, "Vam.Ghest")
            'objDataview = New DataView(objDataset.Tables("Vam.Ghest"))
            'objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            'objDataview.Sort = "Expr1"

            'txtDat.DataBindings.Clear()
            'txtDat.DataBindings.Add("Text", objDataview, "Expr1")
            'Mh = Mid(txtDat.Text, 4, 2)
            'Mah()

            'If txtDat.Text >= Mid(maskDateout.Text, 3, 5) And maskDateout.Text <> "1   /  /" Then
            '    MsgbOK.Label1.Text = " آخرین کارکرد پرسنل فوق در ماه " & Mh & " می باشد . "
            '    MsgbOK.ShowDialog()
            '    Me.txtFamily.Focus()
            '    Exit Sub
            'End If
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " UPDATE bas.PersPay SET Type_End = @TypEnd, Type_Day = @TypDay, work_phn =@work_phn, datein =@datein, dateout = @Dateout, Area_code = @Areacode, semat_code =@semat_code, Vahed_Code =@Vahed_Code, stat =@stat, tahol =@tahol, CodeF = @CodeF WHERE (pers_code = '" & Me.maskCode.Text & "') AND (Shob = " & Shob & ")"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@TypEnd", Mid(maskDateout.Text, 3, 5))
            objCommand.Parameters.AddWithValue("@TypDay", Mid(maskDateout.Text, 9, 2))
            objCommand.Parameters.AddWithValue("@work_phn", Me.txtPhnwrk.Text)
            objCommand.Parameters.AddWithValue("@datein", Me.maskDatein.Text)

            objCommand.Parameters.AddWithValue("@dateout", Me.maskDateout.Text)
            objCommand.Parameters.AddWithValue("@Areacode", Me.cboArea.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@semat_code", Me.cboSemat.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Vahed_Code", Me.cboVahed.SelectedValue.ToString)

            If Me.RB1.Checked = True Then
                objCommand.Parameters.AddWithValue("@stat", "0")
            ElseIf Me.RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@stat", "1")
            End If

            If Me.RB3.Checked = True Then
                objCommand.Parameters.AddWithValue("@tahol", "0")
            ElseIf Me.RB4.Checked = True Then
                objCommand.Parameters.AddWithValue("@tahol", "1")
            End If
            objCommand.Parameters.AddWithValue("@CodeF", CheckBox2.CheckState)

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            '-------------------------ثبت تاریخ ورود و پایان خدمت
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO Bas.PersInOut (pers_code, datein, dateout, Shob)" & _
               " VALUES (@pers_code, @datein, @dateout, @Shob)"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@pers_code", Me.maskCode.Text)
            objCommand.Parameters.AddWithValue("@datein", Me.maskDatein.Text)
            objCommand.Parameters.AddWithValue("@dateout", Me.maskDateout.Text)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            '=============================================================
            FillDatasetAndDataviewPersIns()
            IntPosition = objDataview.Find(txtID.Text)
            If IntPosition <> -1 Then
                IntPosition = objCurrencyManager.Position
                Gpcode = 2
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                    " UPDATE bas.PersIns SET Gpers_code = @Gpcode, IDPcode = @IDPcode, pers_code = @pcode WHERE (ID = " & txtID.Text & ") AND (Shob = " & Shob & ")"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Gpcode", Gpcode)
                objCommand.Parameters.AddWithValue("@pcode", maskCode.Text)
                objCommand.Parameters.AddWithValue("@IDPcode", maskCode.Text)
            End If

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            FillDatasetAndDataview()
            filldatagridview()
            StatTahol()
            Clear()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
        If CheckBox1.Checked = True Then
            ClearDataGridView2()
        End If
        Me.PictureBox1.Image = Nothing
    End Sub

    Private Sub Del()
        Dim objcommand As New SqlCommand
        Dim Gpcode As String
        Dim pcode As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Me.maskCode.Text = "        " Then
            MsgbOK.Label1.Text = " لطفا کد پرسنل را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        Dim objdataadapter As New SqlDataAdapter _
        (" SELECT pers_code FROM Bas.PersPay WHERE (pers_code = " & maskCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.PersPay")
        objDataview = New DataView(objDataset.Tables("bas.PersPay"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "pers_code"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " کد پرسنلی  " & Trim(Me.maskCode.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد پرسنلی  " & Trim(Me.maskCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.maskCode.Focus()
                Exit Sub
            Else
                Dim objdataadapter1 As New SqlDataAdapter _
                        (" SELECT pers_code FROM Bnk.Computer WHERE (pers_code = " & maskCode.Text & ")", objConnection)
                objDataset = New DataSet
                objdataadapter1.Fill(objDataset, "Bnk.Computer")
                objDataview = New DataView(objDataset.Tables("Bnk.Computer"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "pers_code"

                If objDataview.Count > 0 Then
                    MsgbOK.Label1.Text = " پرسنل فوق در جدول کامپیوتر دارای گردش عملیاتی میباشد . "
                    MsgbOK.ShowDialog()
                    maskCode.Focus()
                    Exit Sub
                Else
                    Dim objdataadapter2 As New SqlDataAdapter _
                        (" SELECT pers_code FROM Bnk.LapTop WHERE (pers_code = " & maskCode.Text & ")", objConnection)
                    objDataset = New DataSet
                    objdataadapter2.Fill(objDataset, "Bnk.LapTop")
                    objDataview = New DataView(objDataset.Tables("Bnk.LapTop"))
                    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                    objDataview.Sort = "pers_code"

                    If objDataview.Count > 0 Then
                        MsgbOK.Label1.Text = " پرسنل فوق در جدول لپ تاپ دارای گردش عملیاتی میباشد . "
                        MsgbOK.ShowDialog()
                        maskCode.Focus()
                        Exit Sub
                    Else
                        Dim objdataadapter3 As New SqlDataAdapter _
                            (" SELECT pers_code FROM Bnk.Mobil WHERE (pers_code = " & maskCode.Text & ")", objConnection)
                        objDataset = New DataSet
                        objdataadapter3.Fill(objDataset, "Bnk.Mobil")
                        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
                        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                        objDataview.Sort = "pers_code"

                        If objDataview.Count > 0 Then
                            MsgbOK.Label1.Text = " پرسنل فوق در جدول موبایل دارای گردش عملیاتی میباشد . "
                            MsgbOK.ShowDialog()
                            maskCode.Focus()
                            Exit Sub
                        Else
                            Dim objdataadapter4 As New SqlDataAdapter _
                                (" SELECT pcode FROM Bnk.KalaOut WHERE (pcode = " & maskCode.Text & ")", objConnection)
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
                                IntPosition = objCurrencyManager.Position
                                objcommand.Connection = objConnection
                                objcommand.CommandText = _
                                   " DELETE FROM bas.PersPay WHERE(pers_code =  '" & Me.maskCode.Text & "') AND (Shob = " & Shob & ")"
                                objcommand.CommandType = CommandType.Text
                                objcommand.Parameters.Clear()

                                objConnection.Open()
                                Try
                                    objcommand.ExecuteNonQuery()
                                Catch SqlExceptionErr As SqlException
                                    MessageBox.Show(SqlExceptionErr.Message)
                                    objConnection.Close()
                                    Exit Sub
                                End Try
                                objConnection.Close()

                                FillDatasetAndDataviewPersIns()
                                IntPosition = objDataview.Find(txtID.Text)
                                If IntPosition <> -1 Then
                                    IntPosition = objCurrencyManager.Position
                                    objcommand.Connection = objConnection
                                    Gpcode = 1
                                    pcode = 0
                                    objcommand.CommandText = _
                                        " UPDATE bas.PersIns SET Gpers_code = @Gpcode, pers_code = @pcode, IDPcode = " & txtID.Text & " WHERE (ID = " & txtID.Text & ") AND (Shob = " & Shob & ")"
                                    objcommand.CommandType = CommandType.Text
                                    objcommand.Parameters.Clear()

                                    objcommand.Parameters.AddWithValue("@Gpcode", Gpcode)
                                    objcommand.Parameters.AddWithValue("@pcode", pcode)
                                End If

                                objConnection.Open()
                                Try
                                    objcommand.ExecuteNonQuery()
                                Catch SqlExceptionErr As SqlException
                                    MessageBox.Show(SqlExceptionErr.Message)
                                    objConnection.Close()
                                    Exit Sub
                                End Try
                                objConnection.Close()

                                FillDatasetAndDataview()
                                filldatagridview()
                                StatTahol()
                                Clear()
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub txtSrch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.GotFocus
        Me.txtSrch.SelectionStart = 0
        Me.txtSrch.SelectionLength = Len(Me.txtSrch.Text)
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        If txtClm.Text = 2 Then
            objDataview.RowFilter = "Pers_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        ElseIf txtClm.Text = 3 Then
            objDataview.RowFilter = "Pers_Family like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End If
        filldatagridview()
        StatTahol()
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtName.Text = ""
            txtID.Text = ""
        End If
    End Sub

    Private Sub maskCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.TextChanged
        If maskCode.Text = "        " Then
            txtName.Text = ""
            txtFamily.Text = ""
        End If
    End Sub

    Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.Click
        If CheckBox1.Checked = True Then
            DataGridView2.Visible = True
        ElseIf CheckBox1.Checked = False Then
            DataGridView2.Visible = False
        End If
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
                .Range("A1").Value = "کد سیستم"
                .Range("A1").Font.Bold = True
                .Range("B1").Value = "کد پرسنلی"
                .Range("B1").Font.Bold = True
                .Range("C1").Value = "نام"
                .Range("C1").Font.Bold = True
                .Range("D1").Value = "نام خانوادگی"
                .Range("D1").Font.Bold = True
                .Range("E1").Value = "تلفن محل کار"
                .Range("E1").Font.Bold = True
                .Range("F1").Value = "تاربخ عضویت"
                .Range("F1").Font.Bold = True
                .Range("G1").Value = "پست سازمانی"
                .Range("G1").Font.Bold = True
                .Range("H1").Value = "واحد سازمانی"
                .Range("H1").Font.Bold = True
                .Range("I1").Value = "وضعیت"
                .Range("I1").Font.Bold = True
                .Range("J1").Value = "تاهل"
                .Range("J1").Font.Bold = True

                X = Me.DataGridView1.CurrentCell.RowIndex
                For X = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Rows(X).Selected = True Then
                        .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                        .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                        .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                        .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                        .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                        .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                        .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                        .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                        .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                        .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                        i += 1
                    End If
                Next X
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
        FillDatasetAndDataView2()
        Filldatagridview2()
        MFMDscr()
        Me.maskCode.Focus()
        ReadPicture()
    End Sub

    Private Sub cboMarkaz_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboArea.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboSemat.Focus()
        End If
    End Sub

    Private Sub cboMarkaz_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArea.SelectedIndexChanged

    End Sub

    Private Sub cboSemat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSemat.SelectedIndexChanged

    End Sub

    Private Sub cboVahed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVahed.SelectedIndexChanged

    End Sub

    Private Sub txtPhnwrk_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhnwrk.TextChanged

    End Sub

    Private Sub maskDatein_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDatein.MaskInputRejected

    End Sub

    Private Sub maskDateout_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateout.GotFocus
        maskDateout.SelectionStart = 0
        maskDateout.SelectionLength = Len(maskDateout.Text)
    End Sub

    Private Sub maskDateout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateout.KeyPress
        If e.KeyChar = ChrW(13) Then
            'Datin = maskDatein.Text
            'Chkdate()
            'If Datstat = 1 Then
            '    maskDatein.Focus()
            '    Exit Sub
            'Else
            btnSave.Focus()
            'End If
        End If
    End Sub

    Private Sub txtPSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPSrch.TextChanged
        If txtPSrch.Text <> "" Then
            FillDatasetAndDataview()
            objDataview.RowFilter = "Pers_Code = '" & txtPSrch.Text & "'"
            Me.txtPSrch.Focus()
        Else
            FillDatasetAndDataview()
            filldatagridview()
            StatTahol()
        End If
        filldatagridview()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btnInsJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsJPG.Click
        If maskCode.Text <> "        " Then
            '----------------------------------درج تصویر
            Dim open As New OpenFileDialog
            open.Filter = "Jpge|*.jpg|Gif|*.gif|Bitmap|*.bmp|PNG|*.png|All Images|*.bmp;*.gif;*.jpg;*.png"
            open.Title = "الصاق تصویر"
            open.Multiselect = False
            open.RestoreDirectory = True

            If open.ShowDialog = Windows.Forms.DialogResult.OK Then
                Str_7 = open.FileName
                PictureBox1.Image = Image.FromFile(Str_7)
                Randomize()
                txtPic.Text = Int((1 + 500) * Rnd())
            End If
        Else
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            maskCode.Focus()
        End If
    End Sub

    Private Sub btnSavJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavJPG.Click
        If txtPic.Text <> "" Then
            '-----------------------------------ذخیره تصویر
            Pic = IO.File.ReadAllBytes(Str_7)
            objCommand.Connection = objConnection
            objCommand.CommandText = "UPDATE Bas.PersPay SET PicCode = @PicCode, Pic = @Pic WHERE (pers_code = " & maskCode.Text & ")"
            objCommand.Parameters.Clear()
            objCommand.CommandType = CommandType.Text

            objCommand.Parameters.AddWithValue("@PicCode", txtPic.Text)
            objCommand.Parameters.AddWithValue("@Pic", Pic)
            objConnection.Open()
            objCommand.ExecuteNonQuery()
            objConnection.Close()
            MsgbOK.Label1.Text = " عملیات ثبت عکس با موفقیت انجام شد . "
            MsgbOK.ShowDialog()
        Else
            MsgbOK.Label1.Text = " محل الصاق تصویر خالی میباشد . "
            MsgbOK.ShowDialog()
            btnInsJPG.Focus()
        End If
    End Sub

    Private Sub ReadPicture()
        '-----------------------------------فراخوانی تصویر
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT PicCode FROM Bas.PersPay WHERE (pers_code = " & maskCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Bas.PersPay")
        objDataviewT = New DataView(objDataset.Tables("Bas.PersPay"))
        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)

        txtPic.Text = ""
        txtPic.DataBindings.Clear()
        txtPic.DataBindings.Add("Text", objDataviewT, "PicCode")

        If txtPic.Text <> "" Then
            objConnection.Open()
            objCommand.CommandText = "Select * From Bas.PersPay where(PicCode=" & txtPic.Text & ")"
            objCommand.Connection = objConnection

            Dim Reader As SqlDataReader = objCommand.ExecuteReader
            If Reader.HasRows = True Then
                Reader.Read()
                ' txt_show_path.Text = (reader.GetString(1))
                '========================================
                Me.PictureBox1.Image = Nothing
                Dim File() As Byte
                File = Reader("pic")
                Dim MS As New MemoryStream()
                MS.Write(File, 0, File.Length)
                PictureBox1.Image = Bitmap.FromStream(MS)
            Else
                MsgbOK.Label1.Text = " رکورد فوق فاقد عکس میباشد . "
                MsgbOK.ShowDialog()
            End If
            objConnection.Close()
        Else
            Me.PictureBox1.Image = Nothing
        End If
    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskCode.MaskInputRejected

    End Sub

    Private Sub maskCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.LostFocus
        If maskCode.Text <> "        " Then
            ReadPicture()
        End If
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillCboArea()
        FillCboSemat()
        FillCboVahed()
        FillDatasetAndDataview()
        filldatagridview()
        StatTahol()
    End Sub
End Class