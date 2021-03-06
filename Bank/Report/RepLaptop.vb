
Public Class RepLaptop
    Private Sub FillRepTable()
        Dim Da As New SqlDataAdapter
        Dim Comp As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Comp = Mainfrm.lblCompName.Text
        If RB1.Checked = True Then
            Rb = 1
            If RB5.Checked = True Then
                Knd = 1
            ElseIf RB6.Checked = True Then
                Knd = 2
            ElseIf RB7.Checked = True Then
                Knd = 3
            ElseIf RB8.Checked = True Then
                Knd = 4
            End If
        ElseIf RB2.Checked = True Then
            Rb = 2
            Knd = 0
            If RB3.Checked = True Then
                Rbin = 3
            ElseIf RB4.Checked = True Then
                Rbin = 4
            ElseIf RB9.Checked = True Then
                Rbin = 5
            End If
        End If

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.CreateTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 2)

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
            objCommand.CommandText = "Rep.LapTop"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Rb", Rb)
            If RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@Rbin", Rbin)
            Else
                objCommand.Parameters.AddWithValue("@Rbin", 0)
            End If
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Knd", Knd)
            objCommand.Parameters.AddWithValue("@pcode", maskCode.Text)
            objCommand.Parameters.AddWithValue("@Rowin", txtRowin.Text)
            objCommand.Parameters.AddWithValue("@Rowout", txtRowout.Text)
            objCommand.Parameters.AddWithValue("@Amval", txtAmval.Text)
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

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, pers_code, pers_name, pers_family, Dat, Vahed_Name, Semat_Name, Mark_name, Model_name, cpu_name, hdd_name, vga_name, LCD_name, mouse_name, " & _
            " scan_name, prn_name, Ram, Dscr, DatS, DscrS, LapName, LapIP, Area_Name, Amval, Knd, DatMov FROM Rep.RLapTop", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Rep.RLapTop")
        objDataview = New DataView(objDataset.Tables("Rep.RLapTop"))
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
        Me.DataGridView1.Columns(7).HeaderText = "Mark"
        Me.DataGridView1.Columns(8).HeaderText = "Model"
        Me.DataGridView1.Columns(9).HeaderText = "CPU"
        Me.DataGridView1.Columns(10).HeaderText = "HDD"
        Me.DataGridView1.Columns(11).HeaderText = "Graphic"
        Me.DataGridView1.Columns(12).HeaderText = "LCD"
        Me.DataGridView1.Columns(13).HeaderText = "Mouse"
        Me.DataGridView1.Columns(14).HeaderText = "Scanner"
        Me.DataGridView1.Columns(15).HeaderText = "Printer"
        Me.DataGridView1.Columns(16).HeaderText = "Ram"
        Me.DataGridView1.Columns(17).HeaderText = "توضیحات"
        Me.DataGridView1.Columns(18).HeaderText = "تاریخ ارسال"
        Me.DataGridView1.Columns(19).HeaderText = "شرح"
        Me.DataGridView1.Columns(20).Visible = False ' LapName
        Me.DataGridView1.Columns(21).Visible = False ' LapIP
        Me.DataGridView1.Columns(22).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(23).HeaderText = "اموال"
        Me.DataGridView1.Columns(24).Visible = False
        Me.DataGridView1.Columns(25).HeaderText = "تاریخ انتقال"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 50
        Me.DataGridView1.Columns(2).Width = 80
        Me.DataGridView1.Columns(3).Width = 100
        Me.DataGridView1.Columns(4).Width = 70
        Me.DataGridView1.Columns(5).Width = 100
        Me.DataGridView1.Columns(6).Width = 70
        Me.DataGridView1.Columns(7).Width = 150
        Me.DataGridView1.Columns(8).Width = 150
        Me.DataGridView1.Columns(9).Width = 150
        Me.DataGridView1.Columns(12).Width = 60
        Me.DataGridView1.Columns(13).Width = 80
        Me.DataGridView1.Columns(14).Width = 80
        Me.DataGridView1.Columns(15).Width = 80
        Me.DataGridView1.Columns(16).Width = 50
        Me.DataGridView1.Columns(17).Width = 120
        Me.DataGridView1.Columns(18).Width = 70
        Me.DataGridView1.Columns(19).Width = 80
        Me.DataGridView1.Columns(22).Width = 120

        txtRDate.Text = ConvD.Class1.ConvDate()
        txtCompName.Text = Mainfrm.lblCompName.Text
        Cnt = objDataview.Count - 1
        ProgressBar1.Maximum = objDataview.Count
        For X = 0 To Cnt
            ProgressBar1.Value = X + 1
        Next X
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub RepLaptop_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.DropTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 2)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub RepLaptop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Me.maskDate.Text = ConvD.Class1.ConvDate()
        txtRDate.SendToBack()
        txtCompName.SendToBack()
        txtPers.SendToBack()
        FormName = "RLapTop"
    End Sub
    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            Grp3.Enabled = False
            RB3.Checked = True
            Label11.Enabled = True
            maskDate.Enabled = True
            RB5.Checked = True
            Grp2.Enabled = True
            maskDate.Focus()
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            Grp3.Enabled = True
            RB3.Checked = True
            Label11.Enabled = False
            maskDate.Enabled = False
            Grp2.Enabled = False
            txtRowin.Focus()
        End If
    End Sub

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        If RB3.Checked = True Then
            txtRowin.Text = ""
            txtRowout.Text = ""
            txtFamily.Text = ""
            txtAmval.Text = ""

            txtRowin.Enabled = True
            Label3.Enabled = True
            txtRowout.Enabled = True
            txtFamily.Enabled = False
            maskCode.Enabled = False
            txtAmval.Enabled = False
            txtRowin.Focus()
        End If
    End Sub

    Private Sub RB4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB4.CheckedChanged
        If RB4.Checked = True Then
            txtRowin.Text = ""
            txtRowout.Text = ""
            txtFamily.Text = ""
            txtAmval.Text = ""

            txtRowin.Enabled = False
            Label3.Enabled = False
            txtRowout.Enabled = False
            txtFamily.Enabled = True
            maskCode.Enabled = True
            txtAmval.Enabled = False
            maskCode.Focus()
        End If
    End Sub

    Private Sub RepLaptop_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 195
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRep.Click
        If DataGridView1.RowCount > 0 Then
            Rptpath.FileName = My.Application.Info.DirectoryPath & "\Report\LapTop.rpt"
            Rptpath.SetDatabaseLogon(Us, Pw)
            RepForm.CrystReport.ReportSource = Rptpath
            RepForm.Label1.Text = "لیست لپ تاپ"
            RepForm.CrystReport.RefreshReport()
            RepForm.Show()
        Else
            MsgbOK.Label1.Text = " با اطلاعات فوق رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnRep.Focus()
            Exit Sub
        End If
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
                    .Range("H1").Value = "Mark"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "Model"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "CPU"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "HDD"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "Graphic"
                    .Range("L1").Font.Bold = True
                    .Range("M1").Value = "LCD"
                    .Range("M1").Font.Bold = True
                    .Range("N1").Value = "Mouse"
                    .Range("N1").Font.Bold = True
                    .Range("O1").Value = "Scanner"
                    .Range("O1").Font.Bold = True
                    .Range("P1").Value = "Printer"
                    .Range("P1").Font.Bold = True
                    .Range("Q1").Value = "Ram"
                    .Range("Q1").Font.Bold = True
                    .Range("R1").Value = "توضیحات"
                    .Range("R1").Font.Bold = True
                    .Range("S1").Value = "تاریخ"
                    .Range("S1").Font.Bold = True
                    .Range("T1").Value = "محل فعالیت"
                    .Range("T1").Font.Bold = True
                    .Range("U1").Value = "اموال"
                    .Range("U1").Font.Bold = True
                    .Range("V1").Value = "تاریخ انتقال"
                    .Range("V1").Font.Bold = True

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
                            .Range("R" & i.ToString).Value = DataGridView1.Rows(X).Cells(17).Value
                            .Range("S" & i.ToString).Value = DataGridView1.Rows(X).Cells(18).Value
                            .Range("T" & i.ToString).Value = DataGridView1.Rows(X).Cells(22).Value
                            .Range("U" & i.ToString).Value = DataGridView1.Rows(X).Cells(23).Value
                            .Range("V" & i.ToString).Value = DataGridView1.Rows(X).Cells(25).Value
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

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
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
                    MsgbOK.Label1.Text = " لطفا شماره برگه را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtRowin.Focus()
                    Exit Sub
                ElseIf txtRowout.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا شماره برگه را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtRowout.Focus()
                    Exit Sub
                End If
            ElseIf RB4.Checked = True Then
                If txtName.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا شماره پرسنلس را وارد کنید . "
                    MsgbOK.ShowDialog()
                    maskCode.Focus()
                    Exit Sub
                End If
            End If
        End If

        FillRepTable()
        FillDataSetAndDataView()
        If DataGridView1.RowCount = 0 Then
            ProgressBar1.Value = 0
        End If
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Code_Soft = 15
                FormName = "RLapTop"
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
            maskCode.Clear()
            txtName.Clear()
        End If
    End Sub

    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        maskCode.SelectionStart = 0
        maskCode.SelectionLength = Len(maskCode.Text)
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            If maskCode.Text <> "      " Then
                Pcd = maskCode.Text
                FillMaskCode()

                IntPosition = objDataview.Find(maskCode.Text)
                If IntPosition = -1 Then
                    MsgbOK.Label1.Text = "  کد پرسنلی فوق در سیستم موجود نمی باشد . "
                    MsgbOK.ShowDialog()
                    maskCode.Focus()
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

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskCode.MaskInputRejected

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

    Private Sub RB9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB9.CheckedChanged
        If RB9.Checked = True Then
            txtRowin.Text = ""
            txtRowout.Text = ""
            txtFamily.Text = ""
            txtAmval.Text = ""

            txtRowin.Enabled = False
            Label3.Enabled = False
            txtRowout.Enabled = False
            txtFamily.Enabled = False
            maskCode.Enabled = False
            txtAmval.Enabled = True
            txtAmval.Focus()
        End If
    End Sub

    Private Sub txtRowout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRowout.TextChanged

    End Sub

    Private Sub txtAmval_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmval.GotFocus
        txtAmval.SelectionStart = 0
        txtAmval.SelectionLength = Len(txtAmval.Text)
    End Sub

    Private Sub txtAmval_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmval.KeyPress
        If e.KeyChar = ChrW(13) Then
            BtnOK.Focus()
        End If
    End Sub

    Private Sub txtAmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmval.TextChanged

    End Sub
End Class