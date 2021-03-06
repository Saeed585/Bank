Public Class RepMobil
    Private Sub FillCboArea()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Area_code, Area_name FROM Bas.Area ORDER BY Area_code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Area")
        Me.cboArea.DataSource = objDataset.Tables("Bas.Area")
        Me.cboArea.DisplayMember = "Area_name"
        Me.cboArea.ValueMember = "Area_code"
    End Sub

    Private Sub FillcboMark()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.Mark.Mark_code, Bas.Mark.Mark_name FROM Bas.Mark INNER JOIN Bas.Mark_Sec ON Bas.Mark.Mark_code = Bas.Mark_Sec.Mark_code" &
             " WHERE (Bas.Mark_Sec.U_code = " & User_Code & ") ORDER BY Bas.Mark.Mark_code", objConnection)
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
        objDataAdapter.Fill(objDataset, "Bas.MainBoard")
        Me.CboModel.DataSource = objDataset.Tables("Bas.MainBoard")
        Me.CboModel.DisplayMember = "Model_name"
        Me.CboModel.ValueMember = "Model_code"
    End Sub

    Private Sub FillRepTable()
        'Dim Da As New SqlDataAdapter
        Dim Comp As String
        Dim Mark As String
        Dim Model As String
        Dim Area As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Comp = Mainfrm.lblCompName.Text
        If RB1.Checked = True Then
            Rb = 1
            If CheckBox1.Checked = False Then
                Knd = 1
            Else
                Knd = 2
            End If
            If CheckBox2.Checked = False Then
                Knd1 = 1
            Else
                Knd1 = 2
            End If
        ElseIf RB2.Checked = True Then
            Knd = 0
            Knd1 = 0
            Rb = 2
            If RB3.Checked = True Then
                Rbin = 3
            ElseIf RB4.Checked = True Then
                Rbin = 4
            ElseIf RB5.Checked = True Then
                Rbin = 5
            End If
        End If

        Mark = cboMark.SelectedValue.ToString
        If CboModel.Text <> "" Then
            Model = CboModel.SelectedValue.ToString
        Else
            Model = 0
        End If
        Area = cboArea.SelectedValue.ToString
        Flag = 1

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.CreateTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 10)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.Mobil"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Flag", Flag)
            objCommand.Parameters.AddWithValue("@Rb", Rb)
            If RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@Rbin", Rbin)
            Else
                objCommand.Parameters.AddWithValue("@Rbin", 0)
            End If
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Mark", Mark)
            objCommand.Parameters.AddWithValue("@Model", Model)
            objCommand.Parameters.AddWithValue("@Area", Area)
            objCommand.Parameters.AddWithValue("@Knd", Knd)
            objCommand.Parameters.AddWithValue("@Knd1", Knd1)
            objCommand.Parameters.AddWithValue("@pcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Rowin", txtRowin.Text)
            objCommand.Parameters.AddWithValue("@Rowout", txtRowout.Text)
            objCommand.Parameters.AddWithValue("@Shob", Shob)
            objCommand.Parameters.AddWithValue("@Comp", Comp)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim Cnt As String
        Dim X As Integer
        Dim Chk As String

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, pers_code, pers_name, pers_family, Dat, Vahed_Name, Semat_Name, Mark_name, Model_name, SerialNo, Ram, Dscr, Chk, ChkOutComp, Tedad, Area_name, Sal, Shob_Code" & _
             " FROM Rep.RMobil ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Rep.RMobil")
        objDataview = New DataView(objDataset.Tables("Rep.RMobil"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        lblRow.Text = objDataview.Count

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(2).HeaderText = "نام"
        Me.DataGridView1.Columns(3).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(4).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(5).HeaderText = "واحد"
        Me.DataGridView1.Columns(6).HeaderText = "سمت"
        Me.DataGridView1.Columns(7).HeaderText = "مارک"
        Me.DataGridView1.Columns(8).HeaderText = "مدل"
        Me.DataGridView1.Columns(9).HeaderText = "سریال"
        Me.DataGridView1.Columns(10).HeaderText = "حافظه"
        Me.DataGridView1.Columns(11).HeaderText = "شرح"
        Me.DataGridView1.Columns(12).Visible = False ' Chk
        Me.DataGridView1.Columns(13).Visible = False ' ChkOutComp
        Me.DataGridView1.Columns(14).HeaderText = "تعداد"
        Me.DataGridView1.Columns(15).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(16).Visible = False ' Sal
        Me.DataGridView1.Columns(17).Visible = False ' Shob

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 50
        Me.DataGridView1.Columns(2).Width = 80
        Me.DataGridView1.Columns(3).Width = 100
        Me.DataGridView1.Columns(4).Width = 70
        Me.DataGridView1.Columns(5).Width = 100
        Me.DataGridView1.Columns(6).Width = 100
        Me.DataGridView1.Columns(7).Width = 150
        Me.DataGridView1.Columns(8).Width = 150
        Me.DataGridView1.Columns(9).Width = 100
        Me.DataGridView1.Columns(10).Width = 40
        Me.DataGridView1.Columns(11).Width = 150
        Me.DataGridView1.Columns(14).Width = 40
        Me.DataGridView1.Columns(15).Width = 100

        For X = 0 To DataGridView1.RowCount - 1
            If Not IsDBNull(DataGridView1.Rows(X).Cells(12).Value) Then
                Chk = DataGridView1.Rows(X).Cells(12).Value
                If Chk = 1 Then
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            Else
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
            End If
        Next

        txtRDate.Text = ConvD.Class1.ConvDate()
        txtCompName.Text = Mainfrm.lblCompName.Text
        Cnt = objDataview.Count - 1
        ProgressBar1.Maximum = objDataview.Count
        For X = 0 To Cnt
            ProgressBar1.Value = X + 1
        Next X

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " UPDATE Rep.RMobil SET Repdate = @RDat, Company = @Comp, U_code = @Ucode"
        objCommand.CommandType = CommandType.Text
        objCommand.Parameters.Clear()

        objCommand.Parameters.AddWithValue("@RDat", txtRDate.Text)
        objCommand.Parameters.AddWithValue("@Comp", txtCompName.Text)
        objCommand.Parameters.AddWithValue("@Ucode", User_Code)

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

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub RepMobil_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillcboMark()
        FillcboModel()
        FillCboArea()
    End Sub

    Private Sub RepMobil_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.DropTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 10)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub RepMobil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Me.maskDate.Text = ConvD.Class1.ConvDate()
        txtClm.SendToBack()
        txtRDate.SendToBack()
        txtCompName.SendToBack()
        FormName = "RMobil"
    End Sub

    Private Sub RepMobil_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 235
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

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
                    .Range("A1").Value = "شماره برگه"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "کد پرسنلی"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "نام"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نام خانوادگی"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "تاریخ"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "واحد"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "سمت"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "مارک"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "مدل"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "سریال"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "حافظه"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "شرح"
                    .Range("L1").Font.Bold = True
                    .Range("M1").Value = "تعداد"
                    .Range("M1").Font.Bold = True
                    .Range("N1").Value = "محل فعالیت"
                    .Range("N1").Font.Bold = True

                    ' X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            If DataGridView1.Rows(X).Cells(12).Value = 1 Then
                                .Range("A" & i.ToString).Font.ColorIndex = 9
                                .Range("B" & i.ToString).Font.ColorIndex = 9
                                .Range("C" & i.ToString).Font.ColorIndex = 9
                                .Range("D" & i.ToString).Font.ColorIndex = 9
                                .Range("E" & i.ToString).Font.ColorIndex = 9
                                .Range("F" & i.ToString).Font.ColorIndex = 9
                                .Range("G" & i.ToString).Font.ColorIndex = 9
                                .Range("H" & i.ToString).Font.ColorIndex = 9
                                .Range("I" & i.ToString).Font.ColorIndex = 9
                                .Range("J" & i.ToString).Font.ColorIndex = 9
                                .Range("K" & i.ToString).Font.ColorIndex = 9
                                .Range("L" & i.ToString).Font.ColorIndex = 9
                                .Range("M" & i.ToString).Font.ColorIndex = 9
                                .Range("N" & i.ToString).Font.ColorIndex = 9
                            End If
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
                            .Range("M" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
                            .Range("N" & i.ToString).Value = DataGridView1.Rows(X).Cells(15).Value

                            i += 1
                        End If
                    Next X
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgbOK.Label1.Text = " با اطلاعات فوق رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim objcommand As New SqlCommand

        If RB1.Checked = True Then
            If maskDate.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
                MsgbOK.ShowDialog()
                maskDate.Focus()
                Exit Sub
            End If
        ElseIf RB2.Checked = True Then
            If RB3.Checked = True Then
                If txtRowin.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا شروع شماره برگه را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtRowin.Focus()
                    Exit Sub
                ElseIf txtRowout.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا پایان شماره برگه را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtRowout.Focus()
                    Exit Sub
                End If
            ElseIf RB4.Checked = True Then
                If txtName.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtCode.Focus()
                    Exit Sub
                End If
            End If
        End If

        'objcommand.Connection = objConnection
        'objcommand.CommandText = _
        '    "DELETE FROM Rep.RPriceBim"
        'objConnection.Open()
        'Try
        '    objcommand.ExecuteNonQuery()
        'Catch SqlExceptionErr As SqlException
        '    MessageBox.Show(SqlExceptionErr.Message)
        '    objConnection.Close()
        '    Exit Sub
        'End Try
        'objConnection.Close()

        FillRepTable()
        FillDataSetAndDataView()
        If DataGridView1.RowCount = 0 Then
            ProgressBar1.Value = 0
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        txtClm.Text = e.ColumnIndex
        Label25.Text = DataGridView1.Columns(e.ColumnIndex).HeaderText
        txtSrch.Text = ""
        txtSrch.Focus()
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        If txtSrch.Text <> "" Then
            Select Case txtClm.Text
                Case "1"
                    objDataview.RowFilter = "Pers_code = " & txtSrch.Text & ""
                Case "2"
                    objDataview.RowFilter = "Pers_Name like '" & "**" & txtSrch.Text & "**" & "'"
                Case "3"
                    objDataview.RowFilter = "Pers_Family like '" & "**" & txtSrch.Text & "**" & "'"
            End Select
        Else
            FillDataSetAndDataView()
        End If
        lblRow.Text = objDataview.Count
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
                BtnOK.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub txtRowin_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRowin.GotFocus
        txtRowin.SelectionStart = 0
        txtRowin.SelectionLength = Len(txtRowin.Text)
    End Sub

    Private Sub txtRowin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRowin.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtRowout.Text = txtRowin.Text
            txtRowout.Focus()
        End If
    End Sub

    Private Sub txtRowin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRowin.TextChanged

    End Sub

    Private Sub txtRowout_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRowout.GotFocus
        txtRowout.SelectionStart = 0
        txtRowout.SelectionLength = Len(txtRowout.Text)
    End Sub

    Private Sub txtRowout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRowout.KeyPress
        If e.KeyChar = ChrW(13) Then
            BtnOK.Focus()
        End If
    End Sub

    Private Sub txtRowout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRowout.TextChanged

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

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Code_Soft = 15
                FormName = "RMobil"
                FillPers()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                BtnOK.Focus()
            End If
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtCode.Text = ""
            txtName.Text = ""
        End If
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            Grp3.Enabled = False
            RB3.Checked = True
            Label11.Enabled = True
            maskDate.Enabled = True
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            Label5.Enabled = True
            cboMark.Enabled = True
            Label2.Enabled = True
            CboModel.Enabled = True
            txtRowin.Text = ""
            txtRowout.Text = ""
            maskDate.Focus()
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            Grp3.Enabled = True
            RB3.Checked = True
            Label11.Enabled = False
            maskDate.Enabled = False
            CheckBox1.Checked = False
            CheckBox1.Enabled = False
            CheckBox2.Checked = False
            CheckBox2.Enabled = False
            Label5.Enabled = False
            cboMark.Enabled = False
            Label2.Enabled = False
            CboModel.Enabled = False
            txtRowin.Focus()
        End If
    End Sub

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        If RB3.Checked = True Then
            txtRowin.Text = ""
            txtRowout.Text = ""
            txtFamily.Text = ""

            txtRowin.Enabled = True
            Label3.Enabled = True
            txtRowout.Enabled = True
            txtFamily.Enabled = False
            txtCode.Enabled = False
            cboArea.Enabled = False
            txtRowin.Focus()
        End If
    End Sub

    Private Sub RB4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB4.CheckedChanged
        If RB4.Checked = True Then
            txtRowin.Text = ""
            txtRowout.Text = ""
            txtFamily.Text = ""

            txtRowin.Enabled = False
            Label3.Enabled = False
            txtRowout.Enabled = False
            txtFamily.Enabled = True
            txtCode.Enabled = True
            cboArea.Enabled = False
            txtCode.Focus()
        End If
    End Sub

    Private Sub btnRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRep.Click
        If DataGridView1.RowCount > 0 Then
            Rptpath.FileName = My.Application.Info.DirectoryPath & "\Report\Mobil.rpt"
            Rptpath.SetDatabaseLogon(Us, Pw)
            RepForm.CrystReport.ReportSource = Rptpath
            RepForm.Label1.Text = "لیست گوشی موبایل"
            RepForm.CrystReport.RefreshReport()
            RepForm.Show()
        Else
            MsgbOK.Label1.Text = " با اطلاعات فوق رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnRep.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub cboMark_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMark.SelectedIndexChanged

    End Sub

    Private Sub cboMark_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMark.SelectedValueChanged
        If Me.cboMark.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillcboModel()
        End If
    End Sub

    Private Sub RB5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB5.CheckedChanged
        If RB5.Checked = True Then
            txtRowin.Text = ""
            txtRowout.Text = ""
            txtFamily.Text = ""

            txtRowin.Enabled = False
            Label3.Enabled = False
            txtRowout.Enabled = False
            txtFamily.Enabled = False
            txtCode.Enabled = False
            cboArea.Enabled = True
            txtCode.Focus()
        End If
    End Sub
End Class