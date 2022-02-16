'Imports System.Data
'Imports System.Data.SqlClient

Module Module1

    'کد ماژول را بر میدارد
    Public Code_Soft As String
    'نام ماژول را بر میدارد
    Public Name_Soft As String
    'کد فرم را بر میدارد
    Public Code_Form As String
    Public Name_Form As String
    ' برای محاسبه تعداد ماژول را  بر میدارد
    Public CuntM As String
    'کد کاربر جاری را بر می دارد
    Public User_Code As String
    ' وضعیت درست یا نادرست بودن تاریخ ثبت میشود
    Public Datstat As String
    ' تاریخ وارد شده در فرم را می گیرد
    Public Datin As String
    ' وضعیت درست یا نادرست بودن ساعت ثبت میشود
    Public Timstat As String
    ' ساعت وارد شده در فرم را می گیرد
    Public Timein As String
    Public Pcd As String
    Public Z As String
    Public FormNam As String
    Public FormName As String
    Public Knd As String
    Public Knd1 As String
    Public Shob As String
    Public logStat As String
    Public Flag As String
    Public Rb As String
    Public Rbin As String
    Public Btn As String
    Public Invahed As String
    Public Stock As String
    Public Send As String
    Public Tamir As String
    Public Gscode As String
    Public FileFormat As String

    Public IntPosition As Integer
    Public Rptpath As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
    Public Us As String = "itsys"
    Public Pw As String = "@4708"
    ' Public JpgPath As Imaging.BitmapData

    Public objDataset As DataSet
    Public objDataview As DataView
    Public objDataviewT As DataView
    Public objCommand As New SqlCommand
    Public objCurrencyManager As CurrencyManager
   
    Public objConnection As New SqlConnection _
        ("server='" & Pass.txtServer.Text & "';database='" & Pass.txtDBase.Text & "';user id=" & Us & ";password=" & Pw & "")

    Public Sub Chkdate()
        Dim dat As String
        EdtDT.Class1.DT = Datin
        EdtDT.Class1.EdtD()
        If Datin <> EdtDT.Class1.DT Then
            Datstat = 1
            MsgbOK.Label1.Text = "  تاریخ اشتباه وارد شده است . "
            MsgbOK.ShowDialog()
            Exit Sub
        Else
            Datstat = 0
        End If

        dat = Mid(ConvD.Class1.ConvDate, 2, 3)
        If Mid(Datin, 2, 3) <> dat Then
            Datstat = 1
            MsgbOK.Label1.Text = "  سال مالی فوق مطابق با سال مالی سیستم نمی باشد . "
            MsgbOK.ShowDialog()
            Exit Sub
        Else
            Datstat = 0
        End If
    End Sub

    Public Sub Chkdate1()
        EdtDT.Class1.DT = Datin
        EdtDT.Class1.EdtD()
        If Datin <> EdtDT.Class1.DT Then
            Datstat = 1
            MsgbOK.Label1.Text = "  تاریخ اشتباه وارد شده است . "
            MsgbOK.ShowDialog()
            Exit Sub
        Else
            Datstat = 0
        End If
    End Sub

    Public Sub ChkTime()
        EdtDT.Class1.TM = Timein
        EdtDT.Class1.EdtT()
        Timein = EdtDT.Class1.TM
        If Mid(Timein, 1, 2) > 24 Then
            MsgbOK.Label1.Text = "  ساعت اشتباه وارد شده است . "
            MsgbOK.ShowDialog()
            Timstat = 1
            Exit Sub
        ElseIf Mid(Timein, 4, 2) > 59 Then
            MsgbOK.Label1.Text = "  دقیقه اشتباه وارد شده است . "
            MsgbOK.ShowDialog()
            Timstat = 1
            Exit Sub
        Else
            Timstat = 0
        End If
    End Sub

    Public Function Pic_Patch() As String
        Pic_Patch = Pass.lblPatch_Pic.Text
    End Function
    Public Function Col_Patch() As String
        Col_Patch = Pass.lblPatch_Col.Text
    End Function

    Public Sub FillPrint()
        Dim objDataAdapter As New SqlDataAdapter _
         (" SELECT prn_code, prn_name FROM Bas.Printer WHERE(prn_code > 0) ORDER BY prn_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Printer")
        objDataview = New DataView(objDataset.Tables("bas.Printer"))
        IntPosition = objDataview.Count
        objDataview.Sort = "prn_code"
    End Sub

    Public Sub FillPrintReq()
        Dim Sh As String
        Sh = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
          (" SELECT Bas.Printer.prn_code, Bas.Printer.prn_name, Bas.PrintSerial.Serial, Bas.PrintSerial.Amval, Bas.PrintSerial.IP, Bas.Vahed.Vahed_Name" & _
           " FROM Bas.Printer INNER JOIN Bas.PrintSerial ON Bas.Printer.prn_code = Bas.PrintSerial.prn_code INNER JOIN Bas.Vahed ON Bas.PrintSerial.Vahed = Bas.Vahed.Vahed_Code WHERE (Bas.Printer.prn_code > 0) AND (Bas.PrintSerial.Shob = " & Sh & ") ORDER BY Bas.Printer.prn_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Printer")
        objDataview = New DataView(objDataset.Tables("bas.Printer"))
        IntPosition = objDataview.Count
        objDataview.Sort = "prn_code"
    End Sub

    Public Sub FillPrintRep()
        Dim objDataAdapter As New SqlDataAdapter _
         (" SELECT Bas.Printer.prn_code, Bas.Printer.prn_name, Bas.PrintSerial.Serial, Bas.PrintSerial.Amval, Bas.PrintSerial.IP, Bas.Vahed.Vahed_Name" & _
         " FROM Bas.Printer INNER JOIN Bas.PrintSerial ON Bas.Printer.prn_code = Bas.PrintSerial.prn_code INNER JOIN Bas.Vahed ON Bas.PrintSerial.Vahed = Bas.Vahed.Vahed_Code WHERE(Bas.Printer.prn_code > 0) ORDER BY Bas.Printer.prn_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Printer")
        objDataview = New DataView(objDataset.Tables("bas.Printer"))
        IntPosition = objDataview.Count
        objDataview.Sort = "prn_code"
    End Sub

    Public Sub FillDataPrint()
        Srch.DataGridView1.AutoGenerateColumns = True
        Srch.DataGridView1.DataSource = objDataview

        Srch.DataGridView1.Columns(0).Visible = False
        Srch.DataGridView1.Columns(1).HeaderText = "پرینتر"
        If FormName = "PrintReq" Then
            Srch.DataGridView1.Columns(2).HeaderText = "سریال"
            Srch.DataGridView1.Columns(3).HeaderText = "اموال"
            Srch.DataGridView1.Columns(4).HeaderText = "IP"
            Srch.DataGridView1.Columns(5).HeaderText = "واحد"

            Srch.DataGridView1.Columns(5).Width = 120
        End If
        Srch.DataGridView1.Columns(1).Width = 200
    End Sub

    Public Sub FillKatrijReq()
        Dim objDataAdapter As New SqlDataAdapter _
          (" SELECT code, name FROM Bas.Katrij ORDER BY code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Katrij")
        objDataview = New DataView(objDataset.Tables("bas.Katrij"))
        IntPosition = objDataview.Count
        objDataview.Sort = "code"
    End Sub

    Public Sub FillDataKatrij()
        Srch.DataGridView1.AutoGenerateColumns = True
        Srch.DataGridView1.DataSource = objDataview

        Srch.DataGridView1.Columns(0).Visible = False
        Srch.DataGridView1.Columns(1).HeaderText = "کاتریج"

        Srch.DataGridView1.Columns(1).Width = 200
    End Sub

    Public Sub FillKala()
        Dim objDataAdapter As New SqlDataAdapter _
         (" SELECT Kala_Code, kala_Name FROM Bas.Kala ORDER BY Kala_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Kala")
        objDataview = New DataView(objDataset.Tables("Bas.Kala"))
        IntPosition = objDataview.Count
        objDataview.Sort = "Kala_Code"
    End Sub

    Public Sub FillKalaMoj()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
         (" SELECT Bnk.Mojoodi.Kala_code, Bas.Kala.kala_Name, Bnk.Mojoodi.CountOk, Bnk.Mojoodi.Row FROM Bas.Kala INNER JOIN Bnk.Mojoodi ON Bas.Kala.Kala_Code = Bnk.Mojoodi.Kala_code" & _
          " WHERE(Bnk.Mojoodi.Shob = " & Shob & ") ORDER BY Bnk.Mojoodi.Kala_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Kala")
        objDataview = New DataView(objDataset.Tables("Bas.Kala"))
        IntPosition = objDataview.Count
        objDataview.Sort = "Kala_Code"
    End Sub

    Public Sub FillKalaGaurd()
        Dim objDataAdapter As New SqlDataAdapter _
         (" SELECT Kala_Code, kala_Name FROM Bas.Kala WHERE (SCode = " & Pcd & ") ORDER BY Kala_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Kala")
        objDataview = New DataView(objDataset.Tables("Bas.Kala"))
        IntPosition = objDataview.Count
        objDataview.Sort = "Kala_Code"
    End Sub

    Public Sub FillDataKala()
        Srch.DataGridView1.AutoGenerateColumns = True
        Srch.DataGridView1.DataSource = objDataview

        Srch.DataGridView1.Columns(0).Visible = False
        Srch.DataGridView1.Columns(1).HeaderText = "نام کالا"
        If FormName = "UsedKala" Or FormName = "TamirKala" Then
            Srch.DataGridView1.Columns(2).Visible = False
            Srch.DataGridView1.Columns(3).Visible = False
        ElseIf FormName = "KalaOut" Then
            Srch.DataGridView1.Columns(2).Visible = True
            Srch.DataGridView1.Columns(3).Visible = False
        End If

        Srch.DataGridView1.Columns(1).Width = 290
    End Sub

    Public Sub FillSerial()
        Dim objDataAdapter As New SqlDataAdapter _
          (" SELECT Bnk.PrintReq.Serial, Bas.Printer.prn_name, Bas.Vahed.Vahed_Name, Bas.PrintSerial.Amval, Bas.PrintSerial.IP" &
           " FROM Bnk.PrintReq INNER JOIN Bas.PrintSerial ON Bnk.PrintReq.Printer = Bas.PrintSerial.prn_code AND Bnk.PrintReq.Serial = Bas.PrintSerial.Serial INNER JOIN Bas.Vahed ON Bas.PrintSerial.Vahed = Bas.Vahed.Vahed_Code INNER JOIN Bas.Printer ON Bas.PrintSerial.prn_code = Bas.Printer.prn_code" &
           " GROUP BY Bnk.PrintReq.Serial, Bas.Printer.prn_name, Bas.Vahed.Vahed_Name, Bas.PrintSerial.Amval, Bas.PrintSerial.IP HAVING (Bnk.PrintReq.Serial <> N'0') ORDER BY Bnk.PrintReq.Serial", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PrintReq")
        objDataview = New DataView(objDataset.Tables("Bnk.PrintReq"))
        IntPosition = objDataview.Count
        objDataview.Sort = "Serial"
    End Sub

    Public Sub FillDataSerial()
        Srch.DataGridView1.AutoGenerateColumns = True
        Srch.DataGridView1.DataSource = objDataview

        Srch.DataGridView1.Columns(0).HeaderText = "سریال پرینتر"
        Srch.DataGridView1.Columns(1).HeaderText = "پرینتر"
        Srch.DataGridView1.Columns(2).HeaderText = "واحد"

        Srch.DataGridView1.Columns(0).Width = 200
        Srch.DataGridView1.Columns(1).Width = 120
        Srch.DataGridView1.Columns(2).Width = 120
    End Sub

    Public Sub FillPers()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
          (" SELECT IDPcode, pers_name, pers_family FROM Bas.PersIns Where (Shob = " & Shob & ") ORDER BY IDPcode", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.PersIns")
        objDataview = New DataView(objDataset.Tables("bas.PersIns"))
        IntPosition = objDataview.Count
        objDataview.Sort = "IDPcode"
    End Sub

    Public Sub FillPers1()
        If FormName = "Security" Then
            Shob = Security.cboShob.SelectedValue.ToString
        Else
            Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        End If

        If FormName = "PersinfoPay" Then
            Dim Pcode As String
            Dim Pstat As String
            Pstat = "False"
            Pcode = 0
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT ID, pers_name, pers_family FROM bas.PersIns WHERE (pers_code = " & Pcode & ") and (Pers_stat= '" & Pstat & "') AND (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "PersIns")
            objDataview = New DataView(objDataset.Tables("PersIns"))
            IntPosition = objDataview.Count
            objDataview.Sort = "ID"
        Else
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT IDPcode, pers_name, pers_family FROM bas.PersIns WHERE (IDPcode = " & Pcd & ") AND (Shob = " & Shob & ")", objConnection)
            ' AND (Shob = " & Shob & ")
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "PersIns")
            objDataview = New DataView(objDataset.Tables("PersIns"))
            IntPosition = objDataview.Count
            objDataview.Sort = "IDPcode"
        End If
    End Sub

    Public Sub FillMaskCode()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT IDPcode, pers_name, pers_family FROM Bas.PersIns WHERE(IDPcode = " & Pcd & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.PersIns")
        objDataview = New DataView(objDataset.Tables("bas.PersIns"))
        objDataview.Sort = "IDPcode"
    End Sub
    Public Sub FillDataPers()
        Srch.DataGridView1.AutoGenerateColumns = True
        Srch.DataGridView1.DataSource = objDataview

        Srch.DataGridView1.Columns(0).HeaderText = "کد"
        Srch.DataGridView1.Columns(1).HeaderText = "نام"
        Srch.DataGridView1.Columns(2).HeaderText = "نام خانوادگی"

        Srch.DataGridView1.Columns(0).Width = 40
        Srch.DataGridView1.Columns(1).Width = 100
        Srch.DataGridView1.Columns(2).Width = 150
    End Sub
End Module


