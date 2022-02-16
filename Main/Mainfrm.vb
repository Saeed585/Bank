'Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class Mainfrm
    Dim Str_7 As String = ""
    Dim Pic As Byte()
    ' Inherits System.Windows.Forms.Form
    Public X As Integer
    Public Shared selItem As String
    Public Sub FillCboShob()
        Dim a As String
        a = 0
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.Shob_Sec.U_Code, Bas.Shob.Shob_Code, Bas.Shob.Shob_Name FROM Bas.Shob INNER JOIN" &
             " Bas.Shob_Sec ON Bas.Shob.Shob_Code = Bas.Shob_Sec.Shob_Code WHERE (Bas.Shob_Sec.U_Code = " & User_Code & ") AND (Bas.Shob.Shob_Code <> " & a & ") ORDER BY Bas.Shob.Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Shob")
        objDataview = New DataView(objDataset.Tables("bas.Shob"))
        cboShob.ComboBox.DataSource = objDataset.Tables("bas.Shob")
        cboShob.ComboBox.DisplayMember = "Shob_Name"
        cboShob.ComboBox.ValueMember = "Shob_Code"
        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " کد کاربری فوق به هیچ شعبه ای دسترسی ندارد . "
            MsgbOK.ShowDialog()
            objConnection.Close()
            End
        End If
    End Sub
    Private Sub FillCboSal()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.Sal.Code, Bas.Sal.Name FROM Bas.Sal INNER JOIN Bas.Sal_Sec ON Bas.Sal.Code = Bas.Sal_Sec.Code" &
             " WHERE(Bas.Sal_Sec.U_Code = " & User_Code & ") GROUP BY Bas.Sal.Code, Bas.Sal.Name ORDER BY Bas.Sal.Code DESC ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Sal")
        Me.cboSal_Mali.ComboBox.DataSource = objDataset.Tables("bas.Sal")
        Me.cboSal_Mali.ComboBox.DisplayMember = "Name"
        Me.cboSal_Mali.ComboBox.ValueMember = "Code"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " کد کاربری فوق به هیچ سال مالی دسترسی ندارد . "
            MsgbOK.ShowDialog()
            objConnection.Close()
            End
        End If
    End Sub

    Private Sub FillCompany()
        Dim objdataadapter As New SqlDataAdapter _
            ("SELECT CompCode, CompName FROM bas.Company", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Company")
        objDataview = New DataView(objDataset.Tables("bas.Company"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "CompCode"
    End Sub
    Private Sub TitleChk()
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Bas.Title_Sec.U_Code, Bas.Title.Title_Name FROM Bas.Title_Sec INNER JOIN" &
             " Bas.Title ON Bas.Title_Sec.T_Code = Bas.Title.Code AND Bas.Title_Sec.S_Code = Bas.Title.S_Code" &
             " WHERE (Bas.Title_Sec.U_Code = " & User_Code & ") AND (Bas.Title_Sec.S_Code = " & Code_Soft & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Soft_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Soft_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        DataGridView1.ClearSelection()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).Visible = False
        Me.DataGridView1.Columns(1).HeaderText = "منو"
    End Sub
    Private Sub FormChk()
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Bas.Form_Sec.U_Code, Bas.Form.Form_Name FROM Bas.Form_Sec INNER JOIN Bas.Form ON Bas.Form_Sec.S_Code = Bas.Form.S_Code AND Bas.Form_Sec.T_Code = Bas.Form.T_Code AND Bas.Form_Sec.F_Code = Bas.Form.Code" &
             " WHERE (Bas.Form_Sec.U_Code = " & User_Code & ") AND (Bas.Form_Sec.S_Code = " & Code_Soft & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Soft_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Soft_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        DataGridView1.ClearSelection()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).Visible = False
        Me.DataGridView1.Columns(1).HeaderText = "منو"
    End Sub

    Private Sub FormDetailChk()
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Sav, Edt, Del, Sayer FROM Bas.Form_Sec WHERE (U_Code = " & User_Code & ") AND (F_Code = " & Code_Form & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Form_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Form_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        DataGridView1.ClearSelection()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ذخیره"
        Me.DataGridView1.Columns(1).HeaderText = "اصلاح"
        Me.DataGridView1.Columns(2).HeaderText = "حذف"
        Me.DataGridView1.Columns(3).HeaderText = "سایر"
    End Sub

    Public Sub SavSoft()
        Dim objcommand As New SqlCommand
        Dim Soft As String
        Dim Dat As String
        Dim Tim As String
        Dim Pcname As String

        Soft = "ITBank"
        Dat = ConvD.Class1.ConvDate
        Tim = Mid(My.Computer.Clock.LocalTime, 13, 5)
        txtTim.Text = Tim
        Pcname = My.Computer.Name
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " INSERT INTO Bas.UserSoft (U_Code, Software, Dat, Tim, PcName)" &
           " VALUES (@user, @soft, @dat, @tim, @pc)"
        objcommand.Parameters.AddWithValue("@User", User_Code)
        objcommand.Parameters.AddWithValue("@soft", Soft)
        objcommand.Parameters.AddWithValue("@dat", Dat)
        objcommand.Parameters.AddWithValue("@tim", Tim)
        objcommand.Parameters.AddWithValue("@pc", Pcname)

        objConnection.Open()
        Try
            objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Public Sub DelSoft()
        Dim objcommand As New SqlCommand
        Dim Soft As String

        Soft = "ITBank"
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " DELETE FROM Bas.UserSoft WHERE (U_Code = " & User_Code & ") AND (Software = '" & Soft & "') AND (Tim = '" & txtTim.Text & "')"

        objConnection.Open()
        Try
            objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub FillAlarm()
        Dim Dat As String
        Dat = ConvD.Class1.ConvDate
        Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT Dat, u_code FROM Bas.Vaghaye WHERE (Alarm1 = '" & Dat & "') AND (u_code = " & User_Code & ") AND (Bas.Vaghaye.RD1 = 0)", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bas.Vaghaye")
        objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            Dim objDataAdapter2 As New SqlDataAdapter _
                (" SELECT Dat, u_code FROM Bas.Vaghaye WHERE (Alarm2 = '" & Dat & "') AND (u_code = " & User_Code & ") AND (Bas.Vaghaye.RD2 = 0)", objConnection)
            objDataset = New DataSet
            objDataAdapter2.Fill(objDataset, "Bas.Vaghaye")
            objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            If objDataview.Count = 0 Then
                Dim objDataAdapter3 As New SqlDataAdapter _
                    (" SELECT Dat, u_code FROM Bas.Vaghaye WHERE (Alarm3 = '" & Dat & "') AND (u_code = " & User_Code & ") AND (Bas.Vaghaye.RD3 = 0)", objConnection)
                objDataset = New DataSet
                objDataAdapter3.Fill(objDataset, "Bas.Vaghaye")
                objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            End If
        End If
        ' objDataview.Sort = "Row"
    End Sub

    Private Sub Mainfrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DelSoft()
        End
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.lblTime.Text = TimeOfDay
        ' If Mid(lblTime.Text, 1, 8) = "11:55:00" Then
        ' MsgBox("تماس با آقای یاری فراموش نشود")
        ' End If
    End Sub

    Private Sub Mainfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myCulture As New Globalization.CultureInfo("FA-IR")
        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(myCulture)
        Me.IsMdiContainer = True

        Me.Width = My.Computer.Screen.WorkingArea.Width + 2
        Me.Height = My.Computer.Screen.WorkingArea.Height + 2
        ' Me.Size = New Size(My.Computer.Screen.WorkingArea.Size)

        CuntM = ""
        SavSoft()
        FillCboShob()
        FillCboSal()

        lblCodShob.Text = cboShob.ComboBox.SelectedValue.ToString

        lblCodShob.SendToBack()
        txtCompCode.SendToBack()
        txtCompName.SendToBack()
        txtSepstat.SendToBack()
        txtTim.SendToBack()
        DataGridView1.SendToBack()
        txt1.SendToBack()
        txtDat.SendToBack()
        txtPic.SendToBack()

        FillCompany()
        lblSrv.Text = Pass.txtServer.Text
        lblDB.Text = Pass.txtDBase.Text
        txtCompCode.DataBindings.Clear()
        txtCompName.DataBindings.Clear()
        txtCompCode.DataBindings.Add("Text", objDataview, "CompCode")
        txtCompName.DataBindings.Add("Text", objDataview, "CompName")
        lblCompName.Text = txtCompName.Text
        lblDayName.Text = ConvD.Class1.Convdate1()
        txtDat.Text = ConvD.Class1.ConvDate
        FillAlarm()
        If objDataview.Count > 0 Then
            Alarm.ShowDialog()
        End If
        ReadPicture()
    End Sub

    Private Sub mnuChgUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  Pass.maskCode.Text = "            "
        '  Pass.txtPass.Text = ""
        '  Pass.Enabled = True
        '  Pass.ShowDialog()
        '  Pass.Activate()
        '  Pass.maskCode.Focus()
    End Sub

    Private Sub mnuChgPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            ' Me.BackgroundImage = OpenFileDialog1.SafeFileName.Replace()

        End If
    End Sub

    Private Sub mnuChgCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            'Me.BackgroundImage = Nothing
            Me.BackColor = Me.ColorDialog1.Color
        End If
    End Sub

    Private Sub cboShob_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.cboShob.ComboBox.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            lblCodShob.Text = cboShob.ComboBox.SelectedValue.ToString
        End If
    End Sub

    Private Sub ExitBase()
        Code_Soft = 1
        btnBase.ForeColor = Color.Black

        '-----------------------اطلاعات پایه
        VahedP.Dispose()
        SematP.Dispose()
        Area.Dispose()
        Ostan.Dispose()
        City.Dispose()
        GPers.Dispose()
        PersIns.Dispose()
        Bank.Dispose()
        '---------------------------------
        PersinfoPay.Dispose()
        MainBoard.Dispose()
        Cpu.Dispose()
        Hdd.Dispose()
        Vga.Dispose()
        Modem.Dispose()
        Optic.Dispose()
        Monitor.Dispose()
        KeyBoard.Dispose()
        Mouse.Dispose()
        Scan.Dispose()
        Access.Dispose()
        Print.Dispose()
        PrintSerial.Dispose()
        Mark.Dispose()
        LCD.Dispose()
        Model.Dispose()
        GProblem.Dispose()
        NProblem.Dispose()
        MergeProb.Dispose()
        Katrij.Dispose()

        mnuVahedP.Visible = False
        mnuSematP.Visible = False
        mnuArea.Visible = False
        mnuOstan.Visible = False
        mnuCity.Visible = False
        mnuGPer.Visible = False
        mnuPers.Visible = False
        mnuBank.Visible = False
        mnuSKala.Visible = False
        mnuGkala.Visible = False
        mnuVKala.Visible = False
        mnuInKala.Visible = False
        '---------------------------------
        mnuPersPay.Visible = False
        mnuMain.Visible = False
        mnuCpu.Visible = False
        mnuHdd.Visible = False
        mnuVga.Visible = False
        mnuModem.Visible = False
        mnuOptic.Visible = False
        mnuMonitor.Visible = False
        mnuKeyboard.Visible = False
        mnuMouse.Visible = False
        mnuScan.Visible = False
        mnuAccess.Visible = False
        mnuPrint.Visible = False
        mnuMark.Visible = False
        mnuLCD.Visible = False
        mnuModel.Visible = False
        mnuGProblem.Visible = False
        mnuNProblem.Visible = False
        mnuMergeProb.Visible = False
        mnuKatrij.Visible = False
    End Sub

    Private Sub ExitBank()
        Code_Soft = 15
        ' btnBank.ForeColor = Color.Black

        Computer.Dispose()
        LapTop.Dispose()
        Mobil.Dispose()
        KalaOut.Dispose()
        Mojoodi.Dispose()
        UsedKala.Dispose()
        TamirKala.Dispose()
        Work.Dispose()
        IPPass.Dispose()
        BankSrch.Dispose()
        PrintReq.Dispose()
        KatrijReq.Dispose()
        SabtEmail.Dispose()

        RepComputer.Dispose()
        RepLaptop.Dispose()
        RepPos.Dispose()
        RepMobil.Dispose()
        RepWork.Dispose()
        RepPrintReq.Dispose()
        RepKatrijReq.Dispose()

        OldComputer.Dispose()
        OldLapTop.Dispose()
        OldTerminal.Dispose()
        OldMobil.Dispose()

        mnuComputer.Visible = False
        mnuLaptop.Visible = False
        mnuMobil.Visible = False
        mnuKalaOut.Visible = False
        mnuMojoodi.Visible = False
        mnuUsedKala.Visible = False
        mnuTamirKala.Visible = False
        mnuPos.Visible = False
        mnuWork.Visible = False
        mnuIPPass.Visible = False
        mnuBankSrch.Visible = False
        mnuPrintReq.Visible = False
        mnuKatrijReq.Visible = False
        mnuEmail.Visible = False

        mnuListComputer.Visible = False
        mnuListLapTop.Visible = False
        mnuListPos.Visible = False
        mnuListMobil.Visible = False
        mnuListWork.Visible = False
        mnuListPrintReq.Visible = False
        mnuListKatrijReq.Visible = False

        mnuOldComputer.Visible = False
        mnuOldLapTop.Visible = False
        mnuOldTerminal.Visible = False
        mnuOldMobil.Visible = False
    End Sub

    Private Sub ToolStripUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripUser.Click
        UserList.ShowDialog()
    End Sub

    Private Sub ToolStripChgPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripChgPass.Click
        ChgPass.ShowDialog()
    End Sub

    Private Sub ToolStripAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripAbout.Click
        About.ShowDialog()
    End Sub

    Private Sub ExitOpt()
        Code_Soft = 99
        ' btnOpt.ForeColor = Color.Black

        Security.Dispose()
        Company.Dispose()
        Shobeh.Dispose()
        ChgPass.Dispose()
        Vaghaye.Dispose()
        Phone.Dispose()

        mnuSec.Visible = False
        mnuComp.Visible = False
        mnuShob.Visible = False
        mnuChgPass.Visible = False
        mnuVaghaye.Visible = False
        mnuPhone.Visible = False
    End Sub

    Private Sub mnuExitOpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ExitOpt()
    End Sub

    Private Sub btnInsJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsJPG.Click
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
            btnSavJPG.Enabled = True
        End If
    End Sub

    Private Sub btnSavJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavJPG.Click
        If txtPic.Text <> "" Then
            '-----------------------------------ذخیره تصویر
            Pic = IO.File.ReadAllBytes(Str_7)
            objCommand.Connection = objConnection
            objCommand.CommandText = "UPDATE Bas.UserList SET PicCode = @PicCode, Pic = @Pic WHERE (U_code = " & User_Code & ")"
            objCommand.Parameters.Clear()
            objCommand.CommandType = CommandType.Text

            objCommand.Parameters.AddWithValue("@PicCode", txtPic.Text)
            objCommand.Parameters.AddWithValue("@Pic", Pic)
            objConnection.Open()
            objCommand.ExecuteNonQuery()
            objConnection.Close()
            MsgbOK.Label1.Text = " عملیات ثبت عکس با موفقیت انجام شد . "
            MsgbOK.ShowDialog()
            btnSavJPG.Enabled = False
        Else
            MsgbOK.Label1.Text = " محل الصاق تصویر خالی میباشد . "
            MsgbOK.ShowDialog()
            btnInsJPG.Focus()
        End If
    End Sub

    Private Sub btnDelJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelJPG.Click
        If txtPic.Text <> "" Then
            '-----------------------------------حذف تصویر
            MsgbYN.Label1.Text = "  آیا از حذف عکس پروفایل اطمینان دارید ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.btnDelJPG.Focus()
                Exit Sub
            Else
                objCommand.Connection = objConnection
                objCommand.CommandText = "UPDATE Bas.UserList SET PicCode = @PicCode, Pic = @Pic WHERE (U_code = " & User_Code & ")"
                objCommand.Parameters.Clear()
                objCommand.CommandType = CommandType.Text

                objCommand.Parameters.AddWithValue("@PicCode", 0)
                objCommand.Parameters.AddWithValue("@Pic", 0)
                objConnection.Open()
                objCommand.ExecuteNonQuery()
                objConnection.Close()
                MsgbOK.Label1.Text = " حذف عکس پروفایل انجام شد . "
                MsgbOK.ShowDialog()
                Me.PictureBox1.Image = Nothing
                btnSavJPG.Enabled = False
                txtPic.Text = ""
            End If
        End If
    End Sub

    Private Sub ReadPicture()
        Dim Objcommand As New SqlCommand
        '-----------------------------------فراخوانی تصویر
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT PicCode FROM Bas.UserList WHERE (U_code = " & User_Code & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Bas.UserList")
        objDataviewT = New DataView(objDataset.Tables("Bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)

        txtPic.Text = ""
        txtPic.DataBindings.Clear()
        txtPic.DataBindings.Add("Text", objDataviewT, "PicCode")

        If txtPic.Text <> "" Then
            If txtPic.Text <> "0" Then
                objConnection.Open()
                Objcommand.CommandText = "Select * From Bas.UserList where(PicCode=" & txtPic.Text & ")"
                Objcommand.Connection = objConnection

                Dim Reader As SqlDataReader = Objcommand.ExecuteReader
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
        Else
            Me.PictureBox1.Image = Nothing
        End If
    End Sub

    Private Sub btnBase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBase.Click
        Code_Soft = 1
        Name_Soft = "mnuBase"

        '---------------------------------غیر فعال شدن فرمها
        mnuVahedP.Visible = False
        mnuSematP.Visible = False
        mnuArea.Visible = False
        mnuOstan.Visible = False
        mnuCity.Visible = False
        mnuGPer.Visible = False
        mnuPers.Visible = False
        mnuBank.Visible = False
        mnuSKala.Visible = False
        mnuGkala.Visible = False
        mnuVKala.Visible = False
        mnuInKala.Visible = False
        mnuKalaPriod.Visible = False
        '---------------------------------
        mnuPersPay.Visible = False
        mnuMain.Visible = False
        mnuCpu.Visible = False
        mnuHdd.Visible = False
        mnuVga.Visible = False
        mnuModem.Visible = False
        mnuOptic.Visible = False
        mnuMonitor.Visible = False
        mnuKeyboard.Visible = False
        mnuMouse.Visible = False
        mnuScan.Visible = False
        mnuAccess.Visible = False
        mnuPrint.Visible = False
        mnuMark.Visible = False
        mnuLCD.Visible = False
        mnuModel.Visible = False
        mnuGProblem.Visible = False
        mnuNProblem.Visible = False
        mnuMergeProb.Visible = False
        mnuKatrij.Visible = False
        '==================================================ٍEnd

        'btnBase.ForeColor = Color.Red
        'btnBank.ForeColor = Color.Black
        'btnOpt.ForeColor = Color.Black

        'If Me.mnuDBase.Visible = True Then
        '    Me.mnuDBase.BringToFront()
        '    Me.Label1.BringToFront()
        'Else
        '    Me.mnuDBase.Visible = True
        '    Me.mnuDBase.BringToFront()
        '    Me.Label1.BringToFront()
        'End If

        'mnuDBase.Items.Item(2).Visible = False
        'mnuDBase.Items.Item(3).Visible = False

        Dim X As Integer
        Dim Cnt As String
        TitleChk()
        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            txt1.Text = DataGridView1.Rows(X).Cells(1).Value
            If txt1.Text = "mnuGeneral" Then
                ' mnuDBase.Items.Item(2).Visible = True
                mnuGeneral.Visible = True
            ElseIf txt1.Text = "mnuKhas" Then
                ' mnuDBase.Items.Item(3).Visible = True
                mnuKhas.Visible = True
            End If
        Next X
        txt1.Text = ""

        FormChk()
        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            txt1.Text = DataGridView1.Rows(X).Cells(1).Value
            If txt1.Text = "VahedP" Then
                mnuVahedP.Visible = True
            ElseIf txt1.Text = "SematP" Then
                mnuSematP.Visible = True
            ElseIf txt1.Text = "Area" Then
                mnuArea.Visible = True
            ElseIf txt1.Text = "Ostan" Then
                mnuOstan.Visible = True
            ElseIf txt1.Text = "City" Then
                mnuCity.Visible = True
            ElseIf txt1.Text = "GPers" Then
                mnuGPer.Visible = True
            ElseIf txt1.Text = "PersIns" Then
                mnuPers.Visible = True
            ElseIf txt1.Text = "Bank" Then
                mnuBank.Visible = True
            ElseIf txt1.Text = "SKala" Then
                mnuSKala.Visible = True
            ElseIf txt1.Text = "GKala" Then
                mnuGkala.Visible = True
            ElseIf txt1.Text = "VKala" Then
                mnuVKala.Visible = True
            ElseIf txt1.Text = "INKala" Then
                mnuInKala.Visible = True
            ElseIf txt1.Text = "KalaPriod" Then
                mnuKalaPriod.Visible = True
                '------------------------------------
            ElseIf txt1.Text = "PersinfoPay" Then
                mnuPersPay.Visible = True
            ElseIf txt1.Text = "MainBoard" Then
                mnuMain.Visible = True
            ElseIf txt1.Text = "Cpu" Then
                mnuCpu.Visible = True
            ElseIf txt1.Text = "Hdd" Then
                mnuHdd.Visible = True
            ElseIf txt1.Text = "Vga" Then
                mnuVga.Visible = True
            ElseIf txt1.Text = "Modem" Then
                mnuModem.Visible = True
            ElseIf txt1.Text = "Optic" Then
                mnuOptic.Visible = True
            ElseIf txt1.Text = "Monitor" Then
                mnuMonitor.Visible = True
            ElseIf txt1.Text = "KeyBoard" Then
                mnuKeyboard.Visible = True
            ElseIf txt1.Text = "Mouse" Then
                mnuMouse.Visible = True
            ElseIf txt1.Text = "Scan" Then
                mnuScan.Visible = True
            ElseIf txt1.Text = "Access" Then
                mnuAccess.Visible = True
            ElseIf txt1.Text = "Print" Then
                mnuPrint.Visible = True
            ElseIf txt1.Text = "Mark" Then
                mnuMark.Visible = True
            ElseIf txt1.Text = "LCD" Then
                mnuLCD.Visible = True
            ElseIf txt1.Text = "Katrij" Then
                mnuKatrij.Visible = True
            ElseIf txt1.Text = "Model" Then
                mnuModel.Visible = True
            ElseIf txt1.Text = "GProblem" Then
                mnuGProblem.Visible = True
            ElseIf txt1.Text = "NProblem" Then
                mnuNProblem.Visible = True
            ElseIf txt1.Text = "MergeProb" Then
                mnuMergeProb.Visible = True
            End If
        Next X
        txt1.Text = ""
    End Sub

    Private Sub btnBank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBank.Click
        Code_Soft = 15
        Name_Soft = "mnuBank"

        '---------------------------------غیر فعال شدن فرمها
        mnuComputer.Visible = False
        mnuLaptop.Visible = False
        mnuMobil.Visible = False
        mnuKalaOut.Visible = False
        mnuMojoodi.Visible = False
        mnuUsedKala.Visible = False
        mnuTamirKala.Visible = False
        mnuPos.Visible = False
        mnuWork.Visible = False
        mnuIPPass.Visible = False
        mnuBankSrch.Visible = False
        mnuPrintReq.Visible = False
        mnuKatrijReq.Visible = False
        mnuEmail.Visible = False
        mnuTahvilKala.Visible = False
        '---------------------------------
        mnuListComputer.Visible = False
        mnuListLapTop.Visible = False
        mnuListPos.Visible = False
        mnuListMobil.Visible = False
        mnuListWork.Visible = False
        mnuListPrintReq.Visible = False
        mnuListKatrijReq.Visible = False
        '---------------------------------
        mnuOldComputer.Visible = False
        mnuOldLapTop.Visible = False
        mnuOldTerminal.Visible = False
        mnuOldMobil.Visible = False
        '==================================================ٍEnd

        'btnBase1.ForeColor = Color.Black
        'btnBank1.ForeColor = Color.Red
        'btnOpt1.ForeColor = Color.Black

        'If Me.mnuSBank.Visible = True Then
        '    Me.mnuSBank.BringToFront()
        '    Me.Label1.BringToFront()
        'Else
        '    Me.mnuSBank.Visible = True
        '    Me.mnuSBank.BringToFront()
        '    Me.Label1.BringToFront()
        'End If

        'mnuSBank.Items.Item(2).Visible = False
        'mnuSBank.Items.Item(3).Visible = False
        'mnuSBank.Items.Item(4).Visible = False

        Dim X As Integer
        Dim Cnt As String
        TitleChk()
        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            txt1.Text = DataGridView1.Rows(X).Cells(1).Value
            If txt1.Text = "mnuDIBank" Then
                'mnuSBank.Items.Item(2).Visible = True
                mnuDIBank.Visible = True
            ElseIf txt1.Text = "mnuRepBank" Then
                ' mnuSBank.Items.Item(3).Visible = True
                mnuRepBank.Visible = True
            ElseIf txt1.Text = "mnuOldRepBank" Then
                ' mnuSBank.Items.Item(4).Visible = True
                mnuOldRepBank.Visible = True
            End If
        Next X
        txt1.Text = ""

        FormChk()
        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            txt1.Text = DataGridView1.Rows(X).Cells(1).Value
            '--------------------ورود اطلاعات 
            If txt1.Text = "Computer" Then
                mnuComputer.Visible = True
            ElseIf txt1.Text = "LapTop" Then
                mnuLaptop.Visible = True
            ElseIf txt1.Text = "Mobil" Then
                mnuMobil.Visible = True
            ElseIf txt1.Text = "KalaOut" Then
                mnuKalaOut.Visible = True
            ElseIf txt1.Text = "Mojoodi" Then
                mnuMojoodi.Visible = True
            ElseIf txt1.Text = "UsedKala" Then
                mnuUsedKala.Visible = True
            ElseIf txt1.Text = "TamirKala" Then
                mnuTamirKala.Visible = True
            ElseIf txt1.Text = "Pos" Then
                mnuPos.Visible = True
            ElseIf txt1.Text = "Work" Then
                mnuWork.Visible = True
            ElseIf txt1.Text = "IPPass" Then
                mnuIPPass.Visible = True
            ElseIf txt1.Text = "BnkSrch" Then
                mnuBankSrch.Visible = True
            ElseIf txt1.Text = "PrintReq" Then
                mnuPrintReq.Visible = True
            ElseIf txt1.Text = "KatrijReq" Then
                mnuKatrijReq.Visible = True
            ElseIf txt1.Text = "Email" Then
                mnuEmail.Visible = True
            ElseIf txt1.Text = "TahvilKala" Then
                mnuTahvilKala.Visible = True
                '--------------------گزارشات
            ElseIf txt1.Text = "RepComputer" Then
                mnuListComputer.Visible = True
            ElseIf txt1.Text = "RepLapTop" Then
                mnuListLapTop.Visible = True
            ElseIf txt1.Text = "RepPos" Then
                mnuListPos.Visible = True
            ElseIf txt1.Text = "RepITSoft" Then
                mnuListITSoft.Visible = True
            ElseIf txt1.Text = "RepMobil" Then
                mnuListMobil.Visible = True
            ElseIf txt1.Text = "RepWork" Then
                mnuListWork.Visible = True
            ElseIf txt1.Text = "RepPrintReq" Then
                mnuListPrintReq.Visible = True
            ElseIf txt1.Text = "RepKatrijReq" Then
                mnuListKatrijReq.Visible = True
                '--------------------سوابق
            ElseIf txt1.Text = "OldComputer" Then
                mnuOldComputer.Visible = True
            ElseIf txt1.Text = "OldLapTop" Then
                mnuOldLapTop.Visible = True
            ElseIf txt1.Text = "OldTerminal" Then
                mnuOldTerminal.Visible = True
            ElseIf txt1.Text = "OldMobil" Then
                mnuOldMobil.Visible = True
            End If
        Next X
        txt1.Text = ""
        'If txtDat.Text >= "1392/01/13" Then
        '    mnuSBank.Visible = False
        'End If
    End Sub

    Private Sub btnOpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpt.Click
        Code_Soft = 99

        '---------------------------------غیر فعال شدن فرمها
        mnuSec.Visible = False
        mnuComp.Visible = False
        mnuShob.Visible = False
        mnuChgPass.Visible = False
        mnuVaghaye.Visible = False
        mnuPhone.Visible = False
        mnuChkPrint.Visible = False
        mnuStory.Visible = False
        '==================================================ٍEnd

        'btnBase1.ForeColor = Color.Black
        'btnBank1.ForeColor = Color.Black
        'btnOpt1.ForeColor = Color.Red

        'If Me.mnuSOpt.Visible = True Then
        '    Me.mnuSOpt.BringToFront()
        '    Me.Label1.BringToFront()
        'Else
        '    Me.mnuSOpt.Visible = True
        '    Me.mnuSOpt.BringToFront()
        '    Me.Label1.BringToFront()
        'End If

        Dim X As Integer
        Dim Cnt As String
        FormChk()
        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            txt1.Text = DataGridView1.Rows(X).Cells(1).Value
            If txt1.Text = "Security" Then
                mnuSec.Visible = True
            ElseIf txt1.Text = "Company" Then
                mnuComp.Visible = True
            ElseIf txt1.Text = "Shobeh" Then
                mnuShob.Visible = True
            ElseIf txt1.Text = "ChgPass" Then
                mnuChgPass.Visible = True
            ElseIf txt1.Text = "Vaghaye" Then
                mnuVaghaye.Visible = True
            ElseIf txt1.Text = "Phone" Then
                mnuPhone.Visible = True
            ElseIf txt1.Text = "ChkPrint" Then
                mnuChkPrint.Visible = True
            ElseIf txt1.Text = "Story" Then
                mnuStory.Visible = True
            End If
        Next X
        txt1.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        MsgbYN.Label1.Text = " آیا می خواهید از ITsystem  خارج شوید . "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        Else
            DelSoft()
            End
        End If
    End Sub

    Private Sub mnuVahedP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVahedP.Click
        Code_Soft = 1
        Code_Form = 111
        Name_Form = "VahedP"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                VahedP.btnSave.Enabled = True
            Else
                VahedP.btnSave.Enabled = False
            End If
        Else
            VahedP.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                VahedP.btnEdit.Enabled = True
            Else
                VahedP.btnEdit.Enabled = False
            End If
        Else
            VahedP.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                VahedP.btnDelete.Enabled = True
            Else
                VahedP.btnDelete.Enabled = False
            End If
        Else
            VahedP.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        VahedP.Show()
        VahedP.Activate()
    End Sub

    Private Sub mnuSematP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSematP.Click
        Code_Soft = 1
        Code_Form = 112
        Name_Form = "SematP"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                SematP.btnSave.Enabled = True
            Else
                SematP.btnSave.Enabled = False
            End If
        Else
            SematP.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                SematP.btnEdit.Enabled = True
            Else
                SematP.btnEdit.Enabled = False
            End If
        Else
            SematP.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                SematP.btnDelete.Enabled = True
            Else
                SematP.btnDelete.Enabled = False
            End If
        Else
            SematP.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        SematP.Show()
        SematP.Activate()
    End Sub

    Private Sub mnuArea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArea.Click
        Code_Soft = 1
        Code_Form = 1110
        Name_Form = "Area"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Area.btnSave.Enabled = True
            Else
                Area.btnSave.Enabled = False
            End If
        Else
            Area.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Area.btnEdit.Enabled = True
            Else
                Area.btnEdit.Enabled = False
            End If
        Else
            Area.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Area.btnDelete.Enabled = True
            Else
                Area.btnDelete.Enabled = False
            End If
        Else
            Area.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Area.Show()
        Area.Activate()
    End Sub

    Private Sub mnuBank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBank.Click
        Code_Soft = 1
        Code_Form = 1113
        Name_Form = "Bank"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Bank.btnSave.Enabled = True
            Else
                Bank.btnSave.Enabled = False
            End If
        Else
            Bank.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Bank.btnEdit.Enabled = True
            Else
                Bank.btnEdit.Enabled = False
            End If
        Else
            Bank.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Bank.btnDelete.Enabled = True
            Else
                Bank.btnDelete.Enabled = False
            End If
        Else
            Bank.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Bank.Show()
        Bank.Activate()
    End Sub

    Private Sub mnuOstan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOstan.Click
        Code_Soft = 1
        Code_Form = 1111
        Name_Form = "Ostan"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Ostan.btnSave.Enabled = True
            Else
                Ostan.btnSave.Enabled = False
            End If
        Else
            Ostan.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Ostan.btnEdit.Enabled = True
            Else
                Ostan.btnEdit.Enabled = False
            End If
        Else
            Ostan.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Ostan.btnDelete.Enabled = True
            Else
                Ostan.btnDelete.Enabled = False
            End If
        Else
            Ostan.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Ostan.Show()
        Ostan.Activate()
    End Sub

    Private Sub mnuCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCity.Click
        Code_Soft = 1
        Code_Form = 1112
        Name_Form = "City"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                City.btnSave.Enabled = True
            Else
                City.btnSave.Enabled = False
            End If
        Else
            City.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                City.btnEdit.Enabled = True
            Else
                City.btnEdit.Enabled = False
            End If
        Else
            City.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                City.btnDelete.Enabled = True
            Else
                City.btnDelete.Enabled = False
            End If
        Else
            City.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        City.Show()
        City.Activate()
    End Sub

    Private Sub mnuGPer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGPer.Click
        Code_Soft = 1
        Code_Form = 113
        Name_Form = "GPers"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                GPers.btnSave.Enabled = True
            Else
                GPers.btnSave.Enabled = False
            End If
        Else
            GPers.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                GPers.btnEdit.Enabled = True
            Else
                GPers.btnEdit.Enabled = False
            End If
        Else
            GPers.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                GPers.btnDelete.Enabled = True
            Else
                GPers.btnDelete.Enabled = False
            End If
        Else
            GPers.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        GPers.Show()
        GPers.Activate()
    End Sub

    Private Sub mnuPers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPers.Click
        Code_Soft = 1
        Code_Form = 114
        Name_Form = "PersIns"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                PersIns.btnSave.Enabled = True
            Else
                PersIns.btnSave.Enabled = False
            End If
        Else
            PersIns.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                PersIns.btnEdit.Enabled = True
            Else
                PersIns.btnEdit.Enabled = False
            End If
        Else
            PersIns.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                PersIns.btnDelete.Enabled = True
            Else
                PersIns.btnDelete.Enabled = False
            End If
        Else
            PersIns.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        PersIns.Show()
        PersIns.Activate()
    End Sub

    Private Sub mnuSKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSKala.Click
        Code_Soft = 1
        Code_Form = 1114
        Name_Form = "SKala"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Skala.btnSave.Enabled = True
            Else
                Skala.btnSave.Enabled = False
            End If
        Else
            Skala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Skala.btnEdit.Enabled = True
            Else
                Skala.btnEdit.Enabled = False
            End If
        Else
            Skala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Skala.btnDelete.Enabled = True
            Else
                Skala.btnDelete.Enabled = False
            End If
        Else
            Skala.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                Skala.SGAccount.Enabled = True
            Else
                Skala.SGAccount.Enabled = False
            End If
        Else
            Skala.SGAccount.Enabled = False
        End If
        txt1.Text = ""

        ' savCodeForm()
        Skala.Show()
        Skala.Activate()
    End Sub

    Private Sub mnuGkala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGkala.Click
        Code_Soft = 1
        Code_Form = 1115
        Name_Form = "GKala"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Gkala.btnSave.Enabled = True
            Else
                Gkala.btnSave.Enabled = False
            End If
        Else
            Gkala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Gkala.btnEdit.Enabled = True
            Else
                Gkala.btnEdit.Enabled = False
            End If
        Else
            Gkala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Gkala.btnDelete.Enabled = True
            Else
                Gkala.btnDelete.Enabled = False
            End If
        Else
            Gkala.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        Gkala.Show()
        Gkala.Activate()
    End Sub

    Private Sub mnuVKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVKala.Click
        Code_Soft = 1
        Code_Form = 1116
        Name_Form = "VKala"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                VKala.btnSave.Enabled = True
            Else
                VKala.btnSave.Enabled = False
            End If
        Else
            VKala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                VKala.btnEdit.Enabled = True
            Else
                VKala.btnEdit.Enabled = False
            End If
        Else
            VKala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                VKala.btnDelete.Enabled = True
            Else
                VKala.btnDelete.Enabled = False
            End If
        Else
            VKala.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        VKala.Show()
        VKala.Activate()
    End Sub

    Private Sub mnuInKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInKala.Click
        Code_Soft = 1
        Code_Form = 1117
        Name_Form = "INkala"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                INKala.btnSave.Enabled = True
            Else
                INKala.btnSave.Enabled = False
            End If
        Else
            INKala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                INKala.btnEdit.Enabled = True
            Else
                INKala.btnEdit.Enabled = False
            End If
        Else
            INKala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                INKala.btnDelete.Enabled = True
            Else
                INKala.btnDelete.Enabled = False
            End If
        Else
            INKala.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        INKala.Show()
        INKala.Activate()
    End Sub

    Private Sub mnuPersPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPersPay.Click
        Code_Soft = 1
        Code_Form = 121
        Name_Form = "PersinfoPay"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                PersinfoPay.btnSave.Enabled = True
            Else
                PersinfoPay.btnSave.Enabled = False
            End If
        Else
            PersinfoPay.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                PersinfoPay.btnEdit.Enabled = True
            Else
                PersinfoPay.btnEdit.Enabled = False
            End If
        Else
            PersinfoPay.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
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
    End Sub

    Private Sub mnuMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMain.Click
        Code_Soft = 1
        Code_Form = 1240
        Name_Form = "MainBoard"

    End Sub

    Private Sub mnuCpu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCpu.Click
        Code_Soft = 1
        Code_Form = 1241
        Name_Form = "Cpu"

    End Sub

    Private Sub mnuHdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHdd.Click
        Code_Soft = 1
        Code_Form = 1242
        Name_Form = "Hdd"

    End Sub

    Private Sub mnuVga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVga.Click
        Code_Soft = 1
        Code_Form = 1243
        Name_Form = "Vga"

    End Sub

    Private Sub mnuModem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModem.Click
        Code_Soft = 1
        Code_Form = 1244
        Name_Form = "Modem"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Modem.btnSave.Enabled = True
            Else
                Modem.btnSave.Enabled = False
            End If
        Else
            Modem.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Modem.btnEdit.Enabled = True
            Else
                Modem.btnEdit.Enabled = False
            End If
        Else
            Modem.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Modem.btnDelete.Enabled = True
            Else
                Modem.btnDelete.Enabled = False
            End If
        Else
            Modem.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Modem.Show()
        Modem.Activate()
    End Sub

    Private Sub mnuOptic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptic.Click
        Code_Soft = 1
        Code_Form = 1245
        Name_Form = "Optic"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Optic.btnSave.Enabled = True
            Else
                Optic.btnSave.Enabled = False
            End If
        Else
            Optic.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Optic.btnEdit.Enabled = True
            Else
                Optic.btnEdit.Enabled = False
            End If
        Else
            Optic.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Optic.btnDelete.Enabled = True
            Else
                Optic.btnDelete.Enabled = False
            End If
        Else
            Optic.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Optic.Show()
        Optic.Activate()
    End Sub

    Private Sub mnuMonitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonitor.Click
        Code_Soft = 1
        Code_Form = 1246
        Name_Form = "Monitor"

    End Sub

    Private Sub mnuKeyboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuKeyboard.Click
        Code_Soft = 1
        Code_Form = 1247
        Name_Form = "KeyBoard"

    End Sub

    Private Sub mnuMouse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMouse.Click
        Code_Soft = 1
        Code_Form = 1248
        Name_Form = "Mouse"

    End Sub

    Private Sub mnuScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScan.Click
        Code_Soft = 1
        Code_Form = 1249
        Name_Form = "Scan"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Scan.btnSave.Enabled = True
            Else
                Scan.btnSave.Enabled = False
            End If
        Else
            Scan.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Scan.btnEdit.Enabled = True
            Else
                Scan.btnEdit.Enabled = False
            End If
        Else
            Scan.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Scan.btnDelete.Enabled = True
            Else
                Scan.btnDelete.Enabled = False
            End If
        Else
            Scan.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Scan.Show()
        Scan.Activate()
    End Sub

    Private Sub mnuAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccess.Click
        Code_Soft = 1
        Code_Form = 1250
        Name_Form = "Access"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Access.btnSave.Enabled = True
            Else
                Access.btnSave.Enabled = False
            End If
        Else
            Access.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Access.btnEdit.Enabled = True
            Else
                Access.btnEdit.Enabled = False
            End If
        Else
            Access.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Access.btnDelete.Enabled = True
            Else
                Access.btnDelete.Enabled = False
            End If
        Else
            Access.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Access.Show()
        Access.Activate()
    End Sub

    Private Sub mnuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrint.Click
        Code_Soft = 1
        Code_Form = 1251
        Name_Form = "Print"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Print.btnSave.Enabled = True
            Else
                Print.btnSave.Enabled = False
            End If
        Else
            Print.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Print.btnEdit.Enabled = True
            Else
                Print.btnEdit.Enabled = False
            End If
        Else
            Print.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Print.btnDelete.Enabled = True
            Else
                Print.btnDelete.Enabled = False
            End If
        Else
            Print.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                PrintSerial.btnDelete.Enabled = True
            Else
                PrintSerial.btnDelete.Enabled = False
            End If
        Else
            PrintSerial.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Print.Show()
        Print.Activate()
    End Sub

    Private Sub mnuMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMark.Click
        Code_Soft = 1
        Code_Form = 1252
        Name_Form = "Mark"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Mark.btnSave.Enabled = True
            Else
                Mark.btnSave.Enabled = False
            End If
        Else
            Mark.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Mark.btnEdit.Enabled = True
            Else
                Mark.btnEdit.Enabled = False
            End If
        Else
            Mark.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Mark.btnDelete.Enabled = True
            Else
                Mark.btnDelete.Enabled = False
            End If
        Else
            Mark.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                Mark.btnMark.Enabled = True
            Else
                Mark.btnMark.Enabled = False
            End If
        Else
            Mark.btnMark.Enabled = False
        End If
        txt1.Text = ""

        Mark.Show()
        Mark.Activate()
    End Sub

    Private Sub mnuModel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModel.Click
        Code_Soft = 1
        Code_Form = 1270
        Name_Form = "Model"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Model.btnSave.Enabled = True
            Else
                Model.btnSave.Enabled = False
            End If
        Else
            Model.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Model.btnEdit.Enabled = True
            Else
                Model.btnEdit.Enabled = False
            End If
        Else
            Model.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Model.btnDelete.Enabled = True
            Else
                Model.btnDelete.Enabled = False
            End If
        Else
            Model.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Model.Show()
        Model.Activate()
    End Sub

    Private Sub mnuLCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLCD.Click
        Code_Soft = 1
        Code_Form = 1253
        Name_Form = "LCD"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                LCD.btnSave.Enabled = True
            Else
                LCD.btnSave.Enabled = False
            End If
        Else
            LCD.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                LCD.btnEdit.Enabled = True
            Else
                LCD.btnEdit.Enabled = False
            End If
        Else
            LCD.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                LCD.btnDelete.Enabled = True
            Else
                LCD.btnDelete.Enabled = False
            End If
        Else
            LCD.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        LCD.Show()
        LCD.Activate()
    End Sub

    Private Sub mnuKatrij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuKatrij.Click
        Code_Soft = 1
        Code_Form = 1222
        Name_Form = "Katrij"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Katrij.btnSave.Enabled = True
            Else
                Katrij.btnSave.Enabled = False
            End If
        Else
            Katrij.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Katrij.btnEdit.Enabled = True
            Else
                Katrij.btnEdit.Enabled = False
            End If
        Else
            Katrij.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Katrij.btnDelete.Enabled = True
            Else
                Katrij.btnDelete.Enabled = False
            End If
        Else
            Katrij.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Katrij.Show()
        Katrij.Activate()
    End Sub

    Private Sub mnuGProblem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGProblem.Click
        Code_Soft = 1
        Code_Form = 1273
        Name_Form = "GProblem"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                GProblem.btnSave.Enabled = True
            Else
                GProblem.btnSave.Enabled = False
            End If
        Else
            GProblem.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                GProblem.btnEdit.Enabled = True
            Else
                GProblem.btnEdit.Enabled = False
            End If
        Else
            GProblem.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                GProblem.btnDelete.Enabled = True
            Else
                GProblem.btnDelete.Enabled = False
            End If
        Else
            GProblem.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        GProblem.Show()
        GProblem.Activate()
    End Sub

    Private Sub mnuNProblem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNProblem.Click
        Code_Soft = 1
        Code_Form = 1274
        Name_Form = "NProblem"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                NProblem.btnSave.Enabled = True
            Else
                NProblem.btnSave.Enabled = False
            End If
        Else
            NProblem.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                NProblem.btnEdit.Enabled = True
            Else
                NProblem.btnEdit.Enabled = False
            End If
        Else
            NProblem.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                NProblem.btnDelete.Enabled = True
            Else
                NProblem.btnDelete.Enabled = False
            End If
        Else
            NProblem.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        NProblem.Show()
        NProblem.Activate()
    End Sub

    Private Sub mnuMergeProb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMergeProb.Click
        Code_Soft = 1
        Code_Form = 1275
        Name_Form = "MergeProb"

        MergeProb.Show()
        MergeProb.Activate()
    End Sub

    Private Sub mnuComputer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuComputer.Click
        Code_Soft = 15
        Code_Form = 1511
        FormName = "Computer"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Computer.btnSave.Enabled = True
            Else
                Computer.btnSave.Enabled = False
            End If
        Else
            Computer.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Computer.btnEdit.Enabled = True
            Else
                Computer.btnEdit.Enabled = False
            End If
        Else
            Computer.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Computer.btnDelete.Enabled = True
            Else
                Computer.btnDelete.Enabled = False
            End If
        Else
            Computer.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                Computer.btnMove.Enabled = True
                'Computer.btnOk.Enabled = True
                'Computer.btnNotOk.Enabled = True
            Else
                Computer.btnMove.Enabled = False
                'Computer.btnOk.Enabled = False
                'Computer.btnNotOk.Enabled = False
            End If
        Else
            Computer.btnMove.Enabled = False
            'Computer.btnOk.Enabled = False
            'Computer.btnNotOk.Enabled = False
        End If
        txt1.Text = ""

        Computer.Show()
        Computer.Activate()
    End Sub

    Private Sub mnuLaptop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLaptop.Click
        Code_Soft = 15
        Code_Form = 1512
        FormName = "LapTop"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                LapTop.btnSave.Enabled = True
            Else
                LapTop.btnSave.Enabled = False
            End If
        Else
            LapTop.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                LapTop.btnEdit.Enabled = True
            Else
                LapTop.btnEdit.Enabled = False
            End If
        Else
            LapTop.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                LapTop.btnDelete.Enabled = True
            Else
                LapTop.btnDelete.Enabled = False
            End If
        Else
            LapTop.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                LapTop.btnMove.Enabled = True
            Else
                LapTop.btnMove.Enabled = False
            End If
        Else
            LapTop.btnMove.Enabled = False
        End If
        txt1.Text = ""

        LapTop.Show()
        LapTop.Activate()
    End Sub

    Private Sub mnuMobil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMobil.Click
        Code_Soft = 15
        Code_Form = 1517
        FormName = "Mobil"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Mobil.btnSave.Enabled = True
            Else
                Mobil.btnSave.Enabled = False
            End If
        Else
            Mobil.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Mobil.btnEdit.Enabled = True
            Else
                Mobil.btnEdit.Enabled = False
            End If
        Else
            Mobil.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Mobil.btnDelete.Enabled = True
            Else
                Mobil.btnDelete.Enabled = False
            End If
        Else
            Mobil.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                Mobil.btnMoj.Enabled = True
                ' Mobil.lblList.Enabled = True
            Else
                Mobil.btnMoj.Enabled = False
                ' Mobil.lblList.Enabled = False
            End If
        Else
            Mobil.btnMoj.Enabled = False
            ' Mobil.lblList.Enabled = False
        End If
        txt1.Text = ""

        Mobil.Show()
        Mobil.Activate()
    End Sub

    Private Sub mnuKalaOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuKalaOut.Click
        Code_Soft = 15
        Code_Form = 1513
        FormName = "KalaOut"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                KalaOut.btnSave.Enabled = True
            Else
                KalaOut.btnSave.Enabled = False
            End If
        Else
            KalaOut.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                KalaOut.btnEdit.Enabled = True
            Else
                KalaOut.btnEdit.Enabled = False
            End If
        Else
            KalaOut.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                KalaOut.btnDelete.Enabled = True
            Else
                KalaOut.btnDelete.Enabled = False
            End If
        Else
            KalaOut.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        KalaOut.Show()
        KalaOut.Activate()
    End Sub

    Private Sub mnuMojoodi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMojoodi.Click
        Code_Soft = 15
        Code_Form = 1516
        FormName = "Mojoodi"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Mojoodi.btnSave.Enabled = True
            Else
                Mojoodi.btnSave.Enabled = False
            End If
        Else
            Mojoodi.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Mojoodi.btnEdit.Enabled = True
            Else
                Mojoodi.btnEdit.Enabled = False
            End If
        Else
            Mojoodi.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Mojoodi.btnDelete.Enabled = True
            Else
                Mojoodi.btnDelete.Enabled = False
            End If
        Else
            Mojoodi.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                Mojoodi.btnKala.Enabled = True
            Else
                Mojoodi.btnKala.Enabled = False
            End If
        Else
            Mojoodi.btnKala.Enabled = False
        End If
        txt1.Text = ""

        Mojoodi.Show()
        Mojoodi.Activate()
    End Sub

    Private Sub mnuUsedKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUsedKala.Click
        Code_Soft = 15
        Code_Form = 15114
        FormName = "UsedKala"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                UsedKala.btnSave.Enabled = True
            Else
                UsedKala.btnSave.Enabled = False
            End If
        Else
            UsedKala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                UsedKala.btnEdit.Enabled = True
            Else
                UsedKala.btnEdit.Enabled = False
            End If
        Else
            UsedKala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                UsedKala.btnDelete.Enabled = True
            Else
                UsedKala.btnDelete.Enabled = False
            End If
        Else
            UsedKala.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        UsedKala.Show()
        UsedKala.Activate()
    End Sub

    Private Sub mnuTamirKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTamirKala.Click
        Code_Soft = 15
        Code_Form = 15115
        FormName = "TamirKala"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                TamirKala.btnSave.Enabled = True
            Else
                TamirKala.btnSave.Enabled = False
            End If
        Else
            TamirKala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                TamirKala.btnEdit.Enabled = True
            Else
                TamirKala.btnEdit.Enabled = False
            End If
        Else
            TamirKala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                TamirKala.btnDelete.Enabled = True
            Else
                TamirKala.btnDelete.Enabled = False
            End If
        Else
            TamirKala.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        TamirKala.Show()
        TamirKala.Activate()
    End Sub

    Private Sub mnuPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPos.Click
        Code_Soft = 15
        Code_Form = 1514
        FormName = "Pos"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Pos.btnSave.Enabled = True
            Else
                Pos.btnSave.Enabled = False
            End If
        Else
            Pos.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Pos.btnEdit.Enabled = True
            Else
                Pos.btnEdit.Enabled = False
            End If
        Else
            Pos.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Pos.btnDelete.Enabled = True
            Else
                Pos.btnDelete.Enabled = False
            End If
        Else
            Pos.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                Pos.btnMoj.Enabled = True
                Pos.CheckBox4.Enabled = True
                Pos.CheckBox5.Enabled = True
            Else
                Pos.btnMoj.Enabled = False
                Pos.CheckBox4.Enabled = False
                Pos.CheckBox5.Enabled = False
            End If
        Else
            Pos.btnMoj.Enabled = False
            Pos.CheckBox4.Enabled = False
            Pos.CheckBox5.Enabled = False
        End If
        txt1.Text = ""

        Pos.Show()
        Pos.Activate()
    End Sub

    Private Sub mnuWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWork.Click
        Code_Soft = 15
        Code_Form = 1519
        FormName = "Work"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Work.btnSave.Enabled = True
            Else
                Work.btnSave.Enabled = False
            End If
        Else
            Work.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Work.btnEdit.Enabled = True
            Else
                Work.btnEdit.Enabled = False
            End If
        Else
            Work.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Work.btnDelete.Enabled = True
            Else
                Work.btnDelete.Enabled = False
            End If
        Else
            Work.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Work.Show()
        Work.Activate()
    End Sub

    Private Sub mnuIPPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIPPass.Click
        Code_Soft = 15
        Code_Form = 15110
        FormName = "IPPass"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                IPPass.btnSave.Enabled = True
            Else
                IPPass.btnSave.Enabled = False
            End If
        Else
            IPPass.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                IPPass.btnEdit.Enabled = True
            Else
                IPPass.btnEdit.Enabled = False
            End If
        Else
            IPPass.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                IPPass.btnDelete.Enabled = True
            Else
                IPPass.btnDelete.Enabled = False
            End If
        Else
            IPPass.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(3).Value
            If Trim(txt1.Text) = "True" Then
                IPPass.CheckBox8.Enabled = True
            Else
                IPPass.CheckBox8.Enabled = False
            End If
        Else
            IPPass.CheckBox8.Enabled = False
        End If
        txt1.Text = ""

        IPPass.Show()
        IPPass.Activate()
    End Sub

    Private Sub mnuBankSrch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBankSrch.Click
        Code_Soft = 15
        Code_Form = 1518
        FormName = "BnkSrch"

        BankSrch.Show()
        BankSrch.Activate()
    End Sub

    Private Sub mnuPrintReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintReq.Click
        Code_Soft = 15
        Code_Form = 15111
        FormName = "PrintReq"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                PrintReq.btnSave.Enabled = True
            Else
                PrintReq.btnSave.Enabled = False
            End If
        Else
            PrintReq.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                PrintReq.btnEdit.Enabled = True
            Else
                PrintReq.btnEdit.Enabled = False
            End If
        Else
            PrintReq.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                PrintReq.btnDelete.Enabled = True
            Else
                PrintReq.btnDelete.Enabled = False
            End If
        Else
            PrintReq.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        PrintReq.Show()
        PrintReq.Activate()
    End Sub

    Private Sub mnuKatrijReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuKatrijReq.Click
        Code_Soft = 15
        Code_Form = 1515
        FormName = "KatrijReq"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                KatrijReq.btnSave.Enabled = True
            Else
                KatrijReq.btnSave.Enabled = False
            End If
        Else
            KatrijReq.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                KatrijReq.btnEdit.Enabled = True
            Else
                KatrijReq.btnEdit.Enabled = False
            End If
        Else
            KatrijReq.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                KatrijReq.btnDelete.Enabled = True
            Else
                KatrijReq.btnDelete.Enabled = False
            End If
        Else
            KatrijReq.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        KatrijReq.Show()
        KatrijReq.Activate()
    End Sub

    Private Sub mnuEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmail.Click
        Code_Soft = 15
        Code_Form = 15113
        FormName = "Email"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                SabtEmail.btnSave.Enabled = True
            Else
                SabtEmail.btnSave.Enabled = False
            End If
        Else
            SabtEmail.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                SabtEmail.btnEdit.Enabled = True
            Else
                SabtEmail.btnEdit.Enabled = False
            End If
        Else
            SabtEmail.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                SabtEmail.btnDelete.Enabled = True
            Else
                SabtEmail.btnDelete.Enabled = False
            End If
        Else
            SabtEmail.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        SabtEmail.Show()
        SabtEmail.Activate()
    End Sub

    Private Sub mnuListComputer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListComputer.Click
        Code_Soft = 15
        Code_Form = 1521
        FormName = "RComputer"
        RepComputer.Show()
        RepComputer.Activate()
    End Sub

    Private Sub mnuListLapTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListLapTop.Click
        Code_Soft = 15
        Code_Form = 1522
        FormName = "RLapTop"
        RepLaptop.Show()
        RepLaptop.Activate()
    End Sub

    Private Sub mnuListMobil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListMobil.Click
        Code_Soft = 15
        Code_Form = 1525
        FormName = "RepMobil"
        RepMobil.Show()
        RepMobil.Activate()
    End Sub

    Private Sub mnuListPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListPos.Click
        Code_Soft = 15
        Code_Form = 1523
        FormName = "RepPos"
        RepPos.Show()
        RepPos.Activate()
    End Sub

    Private Sub mnuListITSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListITSoft.Click
        Code_Soft = 15
        Code_Form = 1524
        FormName = "RITSoft"
        RepKatrijReq.Show()
        RepKatrijReq.Activate()
    End Sub

    Private Sub mnuListWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListWork.Click
        Code_Soft = 15
        Code_Form = 1526
        FormName = "RepWork"
        RepWork.Show()
        RepWork.Activate()
    End Sub

    Private Sub mnuListPrintReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListPrintReq.Click
        Code_Soft = 15
        Code_Form = 1526
        FormName = "RepPrintReq"
        RepPrintReq.Show()
        RepPrintReq.Activate()
    End Sub

    Private Sub mnuListKatrijReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuListKatrijReq.Click
        Code_Soft = 15
        Code_Form = 1524
        FormName = "RepKatrijReq"
        RepKatrijReq.Show()
        RepKatrijReq.Activate()
    End Sub

    Private Sub mnuOldComputer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOldComputer.Click
        Code_Soft = 15
        Code_Form = 1531
        FormName = "OldComputer"
        OldComputer.Show()
        OldComputer.Activate()
    End Sub

    Private Sub mnuOldLapTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOldLapTop.Click
        Code_Soft = 15
        Code_Form = 1532
        FormName = "OldLapTop"
        OldLapTop.Show()
        OldLapTop.Activate()
    End Sub

    Private Sub mnuOldMobil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOldMobil.Click
        Code_Soft = 15
        Code_Form = 1534
        FormName = "OldMobil"
        OldMobil.Show()
        OldMobil.Activate()
    End Sub

    Private Sub mnuOldTerminal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOldTerminal.Click
        Code_Soft = 15
        Code_Form = 1533
        FormName = "OldTerminal"
        OldTerminal.Show()
        OldTerminal.Activate()
    End Sub

    Private Sub mnuSec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSec.Click
        Security.ShowDialog()
        Security.Activate()
    End Sub

    Private Sub mnuComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuComp.Click
        Company.ShowDialog()
        Company.Activate()
    End Sub

    Private Sub mnuShob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShob.Click
        Shobeh.ShowDialog()
        Shobeh.Activate()
    End Sub

    Private Sub mnuChgPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChgPass.Click
        ChgPass.ShowDialog()
        ChgPass.Activate()
    End Sub

    Private Sub mnuVaghaye_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVaghaye.Click
        Vaghaye.ShowDialog()
        Vaghaye.Activate()
    End Sub

    Private Sub mnuPhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPhone.Click
        Phone.Show()
        Phone.Activate()
    End Sub

    Private Sub mnuChkPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChkPrint.Click
        Code_Soft = 99
        Code_Form = 9920
        FormName = "ChkPrint"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                ChkPrint.btnSave.Enabled = True
            Else
                ChkPrint.btnSave.Enabled = False
            End If
        Else
            ChkPrint.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                ChkPrint.btnEdit.Enabled = True
            Else
                ChkPrint.btnEdit.Enabled = False
            End If
        Else
            ChkPrint.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                ChkPrint.btnDelete.Enabled = True
            Else
                ChkPrint.btnDelete.Enabled = False
            End If
        Else
            ChkPrint.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        'If Not IsDBNull(DataGridView1.Rows(0).Cells(3).Value) Then
        '    txt1.Text = DataGridView1.Rows(0).Cells(3).Value
        '    If Trim(txt1.Text) = "True" Then
        '        Computer.btnMove.Enabled = True
        '        'Computer.btnOk.Enabled = True
        '        'Computer.btnNotOk.Enabled = True
        '    Else
        '        Computer.btnMove.Enabled = False
        '        'Computer.btnOk.Enabled = False
        '        'Computer.btnNotOk.Enabled = False
        '    End If
        'Else
        '    Computer.btnMove.Enabled = False
        '    'Computer.btnOk.Enabled = False
        '    'Computer.btnNotOk.Enabled = False
        'End If
        'txt1.Text = ""

        ChkPrint.Show()
        ChkPrint.Activate()
    End Sub

    Private Sub mnuStory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStory.Click
        Code_Soft = 99
        Code_Form = 9921
        Name_Form = "Story"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Story.btnSave.Enabled = True
            Else
                Story.btnSave.Enabled = False
            End If
        Else
            Story.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Story.btnEdit.Enabled = True
            Else
                Story.btnEdit.Enabled = False
            End If
        Else
            Story.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Story.btnDelete.Enabled = True
            Else
                Story.btnDelete.Enabled = False
            End If
        Else
            Story.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Story.Show()
        Story.Activate()
    End Sub

    Private Sub mnuKalaPriod_Click(sender As Object, e As EventArgs) Handles mnuKalaPriod.Click
        Code_Soft = 1
        Code_Form = 1118
        Name_Form = "KalaPriod"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                KalaPriod.btnSave.Enabled = True
            Else
                KalaPriod.btnSave.Enabled = False
            End If
        Else
            KalaPriod.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                KalaPriod.btnEdit.Enabled = True
            Else
                KalaPriod.btnEdit.Enabled = False
            End If
        Else
            KalaPriod.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                KalaPriod.btnDelete.Enabled = True
            Else
                KalaPriod.btnDelete.Enabled = False
            End If
        Else
            KalaPriod.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        KalaPriod.ShowDialog()
        KalaPriod.Activate()
    End Sub

    Private Sub mnuTahvilKala_Click(sender As Object, e As EventArgs) Handles mnuTahvilKala.Click
        Code_Soft = 15
        Code_Form = 15112
        FormName = "TahvilKala"

        FormDetailChk()
        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                TahvilKala.btnSave.Enabled = True
            Else
                TahvilKala.btnSave.Enabled = False
            End If
        Else
            TahvilKala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                TahvilKala.btnEdit.Enabled = True
            Else
                TahvilKala.btnEdit.Enabled = False
            End If
        Else
            TahvilKala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                TahvilKala.btnDelete.Enabled = True
            Else
                TahvilKala.btnDelete.Enabled = False
            End If
        Else
            TahvilKala.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        TahvilKala.Show()
        TahvilKala.Activate()
    End Sub
End Class