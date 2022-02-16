Public Class LapTop
    Dim ColumnSrch As Integer
    Public Sub FillRow()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.LapTop where(Shob_code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.LapTop")
        objDataview = New DataView(objDataset.Tables("Bnk.LapTop"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            txtRow.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtRow.Text = 1
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
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

    Private Sub FillCboSemat()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Semat_Code, Semat_Name FROM bas.Semat ORDER BY Semat_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Semat")
        Me.cboSemat.DataSource = objDataset.Tables("bas.Semat")
        Me.cboSemat.DisplayMember = "Semat_Name"
        Me.cboSemat.ValueMember = "Semat_Code"
    End Sub

    Private Sub FillcboMark()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Mark_code, Mark_name FROM Bas.Mark WHERE (SarGroupNo = 1) ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        Me.cboMark.DataSource = objDataset.Tables("Bas.Mark")
        Me.cboMark.DisplayMember = "Mark_name"
        Me.cboMark.ValueMember = "Mark_code"
    End Sub

    Private Sub FillcboModel()
        Dim Mark As String

        Mark = cboMark.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Model_Code, Model_Name FROM Bas.Model WHERE(Mark_code = " & Mark & ") ORDER BY Model_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Model")
        Me.CboModel.DataSource = objDataset.Tables("Bas.Model")
        Me.CboModel.DisplayMember = "Model_name"
        Me.CboModel.ValueMember = "Model_code"
    End Sub
    Private Sub FillcboMarkCpu()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Mark_code, Mark_name FROM Bas.Mark WHERE (SarGroupNo = 4) ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        Me.cboMarkCpu.DataSource = objDataset.Tables("Bas.Mark")
        Me.cboMarkCpu.DisplayMember = "Mark_name"
        Me.cboMarkCpu.ValueMember = "Mark_code"
    End Sub
    Private Sub FillcboCpu()
        Dim Mark As String

        Mark = cboMarkCpu.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Model_Code, Model_Name FROM Bas.Model WHERE(Mark_code = " & Mark & ") ORDER BY Model_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Model")
        Me.cboCpu.DataSource = objDataset.Tables("Bas.Model")
        Me.cboCpu.DisplayMember = "Model_name"
        Me.cboCpu.ValueMember = "Model_code"
    End Sub

    Private Sub FillcboMarkHdd()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Mark_code, Mark_name FROM Bas.Mark WHERE (SarGroupNo = 6) ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        Me.cboMarkHdd.DataSource = objDataset.Tables("Bas.Mark")
        Me.cboMarkHdd.DisplayMember = "Mark_name"
        Me.cboMarkHdd.ValueMember = "Mark_code"
    End Sub
    Private Sub FillcboHdd()
        Dim Mark As String

        Mark = cboMarkHdd.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Model_Code, Model_Name FROM Bas.Model WHERE(Mark_code = " & Mark & ") ORDER BY Model_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Model")
        Me.cboHdd.DataSource = objDataset.Tables("Bas.Model")
        Me.cboHdd.DisplayMember = "Model_name"
        Me.cboHdd.ValueMember = "Model_code"
    End Sub

    Private Sub FillcboMarkVGA()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Mark_code, Mark_name FROM Bas.Mark WHERE (SarGroupNo = 5) ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        Me.cboMarkVGA.DataSource = objDataset.Tables("Bas.Mark")
        Me.cboMarkVGA.DisplayMember = "Mark_name"
        Me.cboMarkVGA.ValueMember = "Mark_code"
    End Sub
    Private Sub FillcboVga()
        Dim Mark As String

        Mark = cboMarkVGA.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Model_Code, Model_Name FROM Bas.Model WHERE(Mark_code = " & Mark & ") ORDER BY Model_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Model")
        Me.cboVga.DataSource = objDataset.Tables("Bas.Model")
        Me.cboVga.DisplayMember = "Model_name"
        Me.cboVga.ValueMember = "Model_code"
    End Sub

    Private Sub FillcboMarkMouse()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Mark_code, Mark_name FROM Bas.Mark WHERE (SarGroupNo = 9) ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        Me.cboMarkMouse.DataSource = objDataset.Tables("Bas.Mark")
        Me.cboMarkMouse.DisplayMember = "Mark_name"
        Me.cboMarkMouse.ValueMember = "Mark_code"
    End Sub
    Private Sub FillcboMouse()
        Dim Mark As String

        Mark = cboMarkMouse.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Model_Code, Model_Name FROM Bas.Model WHERE(Mark_code = " & Mark & ") ORDER BY Model_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Model")
        Me.cboMouse.DataSource = objDataset.Tables("Bas.Model")
        Me.cboMouse.DisplayMember = "Model_name"
        Me.cboMouse.ValueMember = "Model_code"
    End Sub

    Private Sub FillcboLCD()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT LCD_code, LCD_name FROM Bas.LCD ORDER BY LCD_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.LCD")
        Me.cboLCD.DataSource = objDataset.Tables("Bas.LCD")
        Me.cboLCD.DisplayMember = "LCD_name"
        Me.cboLCD.ValueMember = "LCD_code"
    End Sub
    Private Sub FillcboPrint()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT prn_code, prn_name FROM Bas.Printer ORDER BY prn_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Printer")
        Me.cboPrint.DataSource = objDataset.Tables("Bas.Printer")
        Me.cboPrint.DisplayMember = "prn_name"
        Me.cboPrint.ValueMember = "prn_code"
    End Sub

    Private Sub FillcboScan()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Scan_code, Scan_name FROM Bas.Scan ORDER BY Scan_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Scan")
        Me.cboScan.DataSource = objDataset.Tables("Bas.Scan")
        Me.cboScan.DisplayMember = "Scan_name"
        Me.cboScan.ValueMember = "Scan_code"
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

    Private Sub FillDataSetAndDataView()
        Dim Da As New SqlDataAdapter
        If RB1.Checked = True Then
            Knd = 1
        ElseIf RB2.Checked = True Then
            Knd = 2
        ElseIf RB3.Checked = True Then
            Knd = 3
        ElseIf RB4.Checked = True Then
            Knd = 4
        End If
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bnk.View_Laptop WHERE (Shob_Code = " & Shob & ") AND (Knd = " & Knd & ") ORDER BY Row")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bnk.View_Laptop")
        objDataview = New DataView(objDataset.Tables("Bnk.View_Laptop"))
        ' objDataview.Sort = "Row"
        objConnection.Close()
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer
        Dim Chk As String

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(2).HeaderText = "نام"
        Me.DataGridView1.Columns(3).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(4).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(5).Visible = False ' Vahed
        Me.DataGridView1.Columns(6).Visible = False ' Semat
        Me.DataGridView1.Columns(7).Visible = False ' mark
        Me.DataGridView1.Columns(8).HeaderText = "مارک"
        Me.DataGridView1.Columns(9).Visible = False ' Model
        Me.DataGridView1.Columns(10).HeaderText = "مدل"
        Me.DataGridView1.Columns(11).Visible = False ' Cpu
        Me.DataGridView1.Columns(12).HeaderText = "CPU"
        Me.DataGridView1.Columns(13).Visible = False ' Hdd
        Me.DataGridView1.Columns(14).Visible = False ' Vga
        Me.DataGridView1.Columns(15).Visible = False ' Lcd
        Me.DataGridView1.Columns(16).Visible = False ' Mouse
        Me.DataGridView1.Columns(17).Visible = False ' Scan
        Me.DataGridView1.Columns(18).Visible = False ' Printer
        Me.DataGridView1.Columns(19).Visible = False ' Ram
        Me.DataGridView1.Columns(20).Visible = False ' Dscr
        Me.DataGridView1.Columns(21).Visible = False ' DatS
        Me.DataGridView1.Columns(22).Visible = False ' DscrS
        Me.DataGridView1.Columns(23).Visible = False ' Chk
        Me.DataGridView1.Columns(24).Visible = False ' Shob
        Me.DataGridView1.Columns(25).HeaderText = "نام کامپیوتر"
        Me.DataGridView1.Columns(26).HeaderText = "IP"
        Me.DataGridView1.Columns(27).Visible = False ' Area_Code
        Me.DataGridView1.Columns(28).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(29).HeaderText = "اموال"
        Me.DataGridView1.Columns(30).Visible = False ' Knd
        Me.DataGridView1.Columns(31).Visible = False ' markCpu
        Me.DataGridView1.Columns(32).Visible = False ' markHdd
        Me.DataGridView1.Columns(33).Visible = False ' markVga
        Me.DataGridView1.Columns(34).Visible = False ' markMouse

        Me.DataGridView1.Columns(0).Width = 70
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 80
        Me.DataGridView1.Columns(3).Width = 130
        Me.DataGridView1.Columns(4).Width = 70
        Me.DataGridView1.Columns(8).Width = 100
        Me.DataGridView1.Columns(10).Width = 150
        Me.DataGridView1.Columns(12).Width = 150
        Me.DataGridView1.Columns(28).Width = 120
        Me.DataGridView1.Columns(29).Width = 120

        For X = 0 To DataGridView1.RowCount - 1
            Chk = 0
            If Not IsDBNull(DataGridView1.Rows(X).Cells(23).Value) Then
                Chk = DataGridView1.Rows(X).Cells(23).Value
                If Chk = 1 Then
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            Else
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        txtCode.Text = DataGridView1.Rows(X).Cells(1).Value
        txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
        txtName.Text = DataGridView1.Rows(X).Cells(3).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(4).Value
        cboVahed.SelectedValue = DataGridView1.Rows(X).Cells(5).Value
        cboSemat.SelectedValue = DataGridView1.Rows(X).Cells(6).Value
        cboMark.SelectedValue = DataGridView1.Rows(X).Cells(7).Value
        CboModel.SelectedValue = DataGridView1.Rows(X).Cells(9).Value
        cboMarkCpu.SelectedValue = DataGridView1.Rows(X).Cells(31).Value
        cboCpu.SelectedValue = DataGridView1.Rows(X).Cells(11).Value
        cboMarkHdd.SelectedValue = DataGridView1.Rows(X).Cells(32).Value
        cboHdd.SelectedValue = DataGridView1.Rows(X).Cells(13).Value
        cboMarkVGA.SelectedValue = DataGridView1.Rows(X).Cells(33).Value
        cboVga.SelectedValue = DataGridView1.Rows(X).Cells(14).Value
        cboLCD.SelectedValue = DataGridView1.Rows(X).Cells(15).Value
        cboMarkMouse.SelectedValue = DataGridView1.Rows(X).Cells(34).Value
        cboMouse.SelectedValue = DataGridView1.Rows(X).Cells(16).Value
        cboScan.SelectedValue = DataGridView1.Rows(X).Cells(17).Value
        cboPrint.SelectedValue = DataGridView1.Rows(X).Cells(18).Value
        txtRam.Text = DataGridView1.Rows(X).Cells(19).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(20).Value
        txtCompName.Text = DataGridView1.Rows(X).Cells(25).Value
        txtIP.Text = DataGridView1.Rows(X).Cells(26).Value
        cboArea.SelectedValue = DataGridView1.Rows(X).Cells(27).Value
        txtAmval.Text = DataGridView1.Rows(X).Cells(29).Value
        If DataGridView1.Rows(X).Cells(30).Value = 1 Then
            RB1.Checked = True
        ElseIf DataGridView1.Rows(X).Cells(30).Value = 2 Then
            RB2.Checked = True
        ElseIf DataGridView1.Rows(X).Cells(30).Value = 3 Then
            RB3.Checked = True
        ElseIf DataGridView1.Rows(X).Cells(30).Value = 4 Then
            RB4.Checked = True
            Panel1.Visible = True
            maskDateS.Text = DataGridView1.Rows(X).Cells(21).Value
            txtDscrS.Text = DataGridView1.Rows(X).Cells(22).Value
        End If
    End Sub
    Private Sub Clear()
        txtFamily.Text = ""
        txtAmval.Text = ""
        txtRam.Text = ""
        txtDscr.Text = ""
        maskDateS.Text = "1   /  /"
        txtDscrS.Text = ""
        txtCode.Focus()
    End Sub

    Private Sub HighLight()
        Dim X As Integer
        Dim Chk As String
        Dim Rw As String

        X = DataGridView1.CurrentCell.RowIndex
        Rw = DataGridView1.Rows(X).Cells(0).Value

        If DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow Then
            Chk = 0
            DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
        Else
            Chk = 1
            DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
        End If

        objCommand.Connection = objConnection
        objCommand.CommandText =
           " UPDATE Bnk.LapTop SET Chk = @Chk WHERE (Row = " & Rw & ")"
        objCommand.CommandType = CommandType.Text
        objCommand.Parameters.Clear()

        objCommand.Parameters.AddWithValue("@Chk", Chk)
        objConnection.Open()
        Try
            objCommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub
    Private Sub SaveLogLapTop()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " INSERT INTO [Log].LapTopLog(Row, pers_code, Dat, Vahed_code, semat_code, Mark, Model, MarkCpu, cpu_code, MarkHdd, hdd_code, MarkVga, vga_code, Lcd_code, MarkMouse, mouse_code, scan_code, prn_code, Ram, Dscr, Knd, DatS, DscrS, LapName, LapIP, Amval, Sal, Shob_Code)" &
           " SELECT Row, pers_code, Dat, Vahed_code, semat_code, Mark, Model, MarkCpu, cpu_code, MarkHdd, hdd_code, MarkVga, vga_code, LCD_code, MarkMouse, mouse_code, scan_code, prn_code, Ram, Dscr, Knd, DatS, DscrS, LapName, LapIP, Amval, Sal, Shob_Code" &
           " FROM Bnk.LapTop WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")"

        objConnection.Open()
        Try
            objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        Tim = Mid(Mainfrm.lblTime.Text, 1, 8)
        Dat = ConvD.Class1.ConvDate()
        PcName = My.Computer.Name
        objcommand.Connection = objConnection
        objcommand.CommandText = _
            " UPDATE [Log].LapTopLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
            " WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ") AND (tim_stat IS NULL)"
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

    Private Sub LapTop_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        Clear()
        FillCboVahed()
        FillCboSemat()
        FillcboMark()
        ' FillcboModel()
        FillcboMarkCpu()
        FillcboMarkHdd()
        FillcboMarkVGA()
        FillcboMarkMouse()
        FillcboLCD()
        FillcboPrint()
        FillcboScan()
        FillCboArea()
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub LapTop_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.F5 Then
            HighLight()
        ElseIf e.KeyCode = Keys.Insert Then
            FillRow()
            Clear()
        ElseIf e.KeyCode = Keys.Space Then
            Spc()
        End If
    End Sub

    Private Sub Spc()
        If maskDate.Focused = True Then
            maskDate.Text = ConvD.Class1.ConvDate
        ElseIf maskDateS.Focused = True Then
            maskDateS.Text = ConvD.Class1.ConvDate
        End If
    End Sub
    Private Sub LapTop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        maskDate.Text = ConvD.Class1.ConvDate
        ColumnSrch = 1

        txt1.SendToBack()

        Me.KeyPreview = True
        FormName = "LapTop"
        Flag = 1
    End Sub

    Private Sub LapTop_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Edt()
        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        ElseIf maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtRam.Text = "" Then
            MsgbOK.Label1.Text = " لطفا مموری را وارد کنید . "
            MsgbOK.ShowDialog()
            txtRam.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            '--------LapTopLog
            logStat = 2
            SaveLogLapTop()
            '--------------------
            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtCode.Focus()
                Exit Sub
            Else
                '--------LapTopLog
                logStat = 4
                SaveLogLapTop()
                '--------------------
                Btn = 3
                InsertUpdate()

                Ref.PerformClick()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        If RB1.Checked = True Or RB2.Checked = True Then
            If ColumnSrch = 1 Or ColumnSrch = 3 Or ColumnSrch = 28 Or ColumnSrch = 29 Then
                Label32.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
                Me.txtSrch.Text = ""
                Me.txtSrch.Focus()
            Else
                Me.txtSrch.Text = ""
            End If
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        txtDscr.SelectionStart = 0
        txtDscr.SelectionLength = Len(txtDscr.Text)
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Code_Soft = 15
                FormName = "LapTop"
                FillPers()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                Dim TH As String = ""
                If Mainfrm.cboShob.ComboBox.SelectedValue.ToString = 1 Then
                    TH = "THSS"
                ElseIf Mainfrm.cboShob.ComboBox.SelectedValue.ToString = 2 Then
                    TH = "THES"
                ElseIf Mainfrm.cboShob.ComboBox.SelectedValue.ToString = 5 Then
                    TH = "THWO"
                ElseIf Mainfrm.cboShob.ComboBox.SelectedValue.ToString = 6 Then
                    TH = "THWS"
                ElseIf Mainfrm.cboShob.ComboBox.SelectedValue.ToString = 11 Then
                    TH = "THNO"
                End If
                txtCompName.Text = TH & "-" & Trim(txtCode.Text) & "-L"
                cboVahed.Focus()
            End If
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtCode.Clear()
            txtName.Clear()
            txtCompName.Clear()
            txtIP.Clear()
        End If
    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.GotFocus
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate.Focus()
                Exit Sub
            Else
                txtCode.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub cboVahed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboVahed.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboSemat.Focus()
        End If
    End Sub

    Private Sub cboVahed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVahed.SelectedIndexChanged

    End Sub

    Private Sub cboSemat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboSemat.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCompName.Focus()
        End If
    End Sub

    Private Sub cboSemat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSemat.SelectedIndexChanged

    End Sub

    Private Sub CboMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CboModel.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboCpu.Focus()
        End If
    End Sub

    Private Sub CboMain_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboModel.SelectedIndexChanged

    End Sub

    Private Sub cboOptical_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboMark.KeyPress
        If e.KeyChar = ChrW(13) Then
            CboModel.Focus()
        End If
    End Sub

    Private Sub cboOptical_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMark.SelectedIndexChanged

    End Sub

    Private Sub cboCpu_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboCpu.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboHdd.Focus()
        End If
    End Sub

    Private Sub cboCpu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCpu.SelectedIndexChanged

    End Sub

    Private Sub cboMonitor_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            cboHdd.Focus()
        End If
    End Sub

    Private Sub cboMonitor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cboHdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboHdd.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboVga.Focus()
        End If
    End Sub

    Private Sub cboHdd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboHdd.SelectedIndexChanged

    End Sub

    Private Sub cboKeyBoard_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            cboVga.Focus()
        End If
    End Sub

    Private Sub cboKeyBoard_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cboVga_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboVga.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboLCD.Focus()
        End If
    End Sub

    Private Sub cboVga_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVga.SelectedIndexChanged

    End Sub

    Private Sub cboMouse_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboMouse.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboPrint.Focus()
        End If
    End Sub

    Private Sub cboMouse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMouse.SelectedIndexChanged

    End Sub

    Private Sub cboModem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            cboScan.Focus()
        End If
    End Sub

    Private Sub cboModem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cboScan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboScan.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtRam.Focus()
        End If
    End Sub

    Private Sub cboScan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboScan.SelectedIndexChanged

    End Sub

    Private Sub cboAccess_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            cboPrint.Focus()
        End If
    End Sub

    Private Sub cboAccess_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtRam_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRam.GotFocus
        txtRam.SelectionStart = 0
        txtRam.SelectionLength = Len(txtRam.Text)
    End Sub

    Private Sub txtRam_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRam.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboArea.Focus()
        End If
    End Sub

    Private Sub txtRam_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRam.TextChanged

    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        FormName = "LapTop"
        ShobMove.ShowDialog()
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDataSetAndDataView()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB1.Click
        Clear()
        Ref.PerformClick()
        Panel1.Visible = False
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FillRow()
        Clear()
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDataSetAndDataView()
        If txtSrch.Text <> "" Then
            If ColumnSrch = 1 Then
                objDataview.RowFilter = "pers_code = '" & Me.txtSrch.Text & "'"
            ElseIf ColumnSrch = 3 Then
                objDataview.RowFilter = "pers_family like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 28 Then
                objDataview.RowFilter = "LapName like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 29 Then
                objDataview.RowFilter = "LapIP like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End If
        End If
        FillDataGridView()
    End Sub

    Private Sub cboPrint_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboPrint.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboScan.Focus()
        End If
    End Sub

    Private Sub cboPrint_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPrint.SelectedIndexChanged

    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String

        If RB1.Checked = True Then
            Knd = 1
        ElseIf RB2.Checked = True Then
            Knd = 2
        ElseIf RB3.Checked = True Then
            Knd = 3
        ElseIf RB4.Checked = True Then
            Knd = 4
        End If

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdLapTop"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@vahed", cboVahed.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@semat", cboSemat.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@mark", cboMark.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@model", CboModel.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Markcpu", cboMarkCpu.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@cpu", cboCpu.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Markhdd", cboMarkHdd.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@hdd", cboHdd.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Markvga", cboMarkVGA.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@vga", cboVga.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Markmouse", cboMarkMouse.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@mouse", cboMouse.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@lcd", cboLCD.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@print", cboPrint.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@scan", cboScan.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Ram", txtRam.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Knd", Knd)
            objCommand.Parameters.AddWithValue("@DatS", maskDateS.Text)
            objCommand.Parameters.AddWithValue("@DscrS", txtDscrS.Text)
            objCommand.Parameters.AddWithValue("@Chk", 0)
            objCommand.Parameters.AddWithValue("@Sal", Sal)
            objCommand.Parameters.AddWithValue("@shob", Shob)
            objCommand.Parameters.AddWithValue("@LapName", txtCompName.Text)
            objCommand.Parameters.AddWithValue("@IP", txtIP.Text)
            objCommand.Parameters.AddWithValue("@Area", cboArea.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Amval", txtAmval.Text)


            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        ElseIf maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtRam.Text = "" Then
            MsgbOK.Label1.Text = " لطفا مموری را وارد کنید . "
            MsgbOK.ShowDialog()
            txtRam.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()

            '--------LapTopLog
            logStat = 1
            SaveLogLapTop()
            '--------------------
            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub maskDateS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateS.GotFocus
        maskDateS.SelectionStart = 0
        maskDateS.SelectionLength = Len(maskDateS.Text)
    End Sub

    Private Sub maskDateS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateS.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateS.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateS.Focus()
                Exit Sub
            Else
                txtDscrS.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateS_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateS.MaskInputRejected

    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub txtDscrS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscrS.GotFocus
        txtDscrS.SelectionStart = 0
        txtDscrS.SelectionLength = Len(txtDscrS.Text)
    End Sub

    Private Sub txtDscrS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrS.TextChanged

    End Sub

    Private Sub cboMark_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMark.SelectedValueChanged
        If Me.cboMark.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillcboModel()
        End If
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        txtCode.SelectionStart = 0
        txtCode.SelectionLength = Len(txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text <> "" Then
                Pcd = txtCode.Text
                FillMaskCode()

                IntPosition = objDataview.Find(txtCode.Text)
                If IntPosition = -1 Then
                    MsgbOK.Label1.Text = "  کد پرسنلی فوق در سیستم موجود نمی باشد . "
                    MsgbOK.ShowDialog()
                    txtCode.Focus()
                    Exit Sub
                Else
                    txtName.DataBindings.Clear()
                    txtName.DataBindings.Add("Text", objDataview, "Pers_Name")
                    txtFamily.DataBindings.Clear()
                    txtFamily.DataBindings.Add("Text", objDataview, "Pers_Family")
                End If
            Else
                txtFamily.Clear()
                txtName.Clear()
            End If
            txtFamily.Focus()
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub cboLCD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboLCD.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboMouse.Focus()
        End If
    End Sub

    Private Sub cboLCD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLCD.SelectedIndexChanged

    End Sub

    Private Sub txtCompName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompName.GotFocus
        txtCompName.SelectionStart = 0
        txtCompName.SelectionLength = Len(txtCompName.Text)
    End Sub

    Private Sub txtCompName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCompName.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIP.Focus()
        End If
    End Sub

    Private Sub txtIP_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIP.GotFocus
        txtIP.SelectionStart = 0
        txtIP.SelectionLength = Len(txtIP.Text)
    End Sub

    Private Sub txtIP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIP.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAmval.Focus()
        End If
    End Sub

    Private Sub txtIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIP.TextChanged

    End Sub

    Private Sub txtAmval_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmval.GotFocus
        txtAmval.SelectionStart = 0
        txtAmval.SelectionLength = Len(txtAmval.Text)
    End Sub

    Private Sub txtAmval_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmval.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboMark.Focus()
        End If
    End Sub

    Private Sub txtAmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmval.TextChanged

    End Sub

    Private Sub cboArea_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboArea.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        Clear()
        Ref.PerformClick()
        Panel1.Visible = False
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        Clear()
        Ref.PerformClick()
        Panel1.Visible = False
    End Sub

    Private Sub RB4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB4.CheckedChanged
        Clear()
        Ref.PerformClick()
    End Sub

    Private Sub cboMarkCpu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMarkCpu.SelectedIndexChanged

    End Sub

    Private Sub cboMarkCpu_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMarkCpu.SelectedValueChanged
        If Me.cboMarkCpu.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillcboCpu()
        End If
    End Sub

    Private Sub cboMarkHdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMarkHdd.SelectedIndexChanged

    End Sub

    Private Sub cboMarkHdd_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMarkHdd.SelectedValueChanged
        If Me.cboMarkHdd.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillcboHdd()
        End If
    End Sub

    Private Sub cboMarkVGA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMarkVGA.SelectedIndexChanged

    End Sub

    Private Sub cboMarkVGA_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMarkVGA.SelectedValueChanged
        If Me.cboMarkVGA.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillcboVga()
        End If
    End Sub

    Private Sub cboMarkMouse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMarkMouse.SelectedIndexChanged

    End Sub

    Private Sub cboMarkMouse_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMarkMouse.SelectedValueChanged
        If Me.cboMarkMouse.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillcboMouse()
        End If
    End Sub
End Class