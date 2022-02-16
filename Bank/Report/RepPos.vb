
Public Class RepPos
    Private Sub FillRepTable()
        Dim Da As New SqlDataAdapter

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If CheckBox1.Checked = True Then
            Knd = 1
        Else
            Knd = 0
        End If

        If RB1.Checked = True Then
            If Knd = 0 Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Rep.View_ListPos WHERE (Dat >=N'" & maskDatein.Text & "') AND (Dat <=N'" & maskDateout.Text & "') AND (Shob_code =" & Shob & ") ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Rep.View_ListPos")
                objDataview = New DataView(objDataset.Tables("Rep.View_ListPos"))
                objConnection.Close()
            ElseIf Knd = 1 Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Rep.View_ListPos WHERE (Dat >=N'" & maskDatein.Text & "') AND (Dat <=N'" & maskDateout.Text & "') ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Rep.View_ListPos")
                objDataview = New DataView(objDataset.Tables("Rep.View_ListPos"))
                objConnection.Close()
            End If
        ElseIf RB2.Checked = True Then
            If RB3.Checked = True Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Rep.View_ListPos WHERE (Dat >=N'" & maskDatein.Text & "') AND (Dat <=N'" & maskDateout.Text & "') AND (Pers_code =" & txtCode.Text & ") ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Rep.View_ListPos")
                objDataview = New DataView(objDataset.Tables("Rep.View_ListPos"))
                objConnection.Close()
            ElseIf RB4.Checked = True Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Rep.View_ListPos WHERE (Dat >=N'" & maskDatein.Text & "') AND (Dat <=N'" & maskDateout.Text & "') AND (Payane =" & txtSerial.Text & ") ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Rep.View_ListPos")
                objDataview = New DataView(objDataset.Tables("Rep.View_ListPos"))
                objConnection.Close()
            End If
        End If
    End Sub

    Private Sub FillDataSetAndDataView()
        lblRow.Text = objDataview.Count
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(3).HeaderText = "نام"
        Me.DataGridView1.Columns(4).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(5).HeaderText = "واحد"
        Me.DataGridView1.Columns(6).HeaderText = "سمت"
        Me.DataGridView1.Columns(7).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(8).HeaderText = "مارک"
        Me.DataGridView1.Columns(9).HeaderText = "مدل"
        Me.DataGridView1.Columns(10).HeaderText = "شماره سریال"
        Me.DataGridView1.Columns(11).HeaderText = "شماره پذیرنده"
        Me.DataGridView1.Columns(12).HeaderText = "تاریخ عودت"
        Me.DataGridView1.Columns(13).HeaderText = "تاریخ مفقود"
        Me.DataGridView1.Columns(14).HeaderText = "توضیحات"
        Me.DataGridView1.Columns(15).Visible = False ' Shob_code
        If CheckBox1.Checked = True Then
            Me.DataGridView1.Columns(16).Visible = True
            Me.DataGridView1.Columns(16).HeaderText = "شعبه"
        Else
            Me.DataGridView1.Columns(16).Visible = False
        End If

        Me.DataGridView1.Columns(16).Width = 200
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub RepPos_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.DropTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 3)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub RepPos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Me.maskDatein.Text = ConvD.Class1.ConvDate()
        Me.maskDateout.Text = ConvD.Class1.ConvDate()
        txtRDate.SendToBack()
        txtCompName.SendToBack()
        txtPers.SendToBack()
        txtPencel.SendToBack()
        FormName = "RepPos"
    End Sub
    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            Grp3.Enabled = False
            RB3.Checked = True
            txtFamily.Text = ""
            txtSerial.Text = ""
            maskDatein.Focus()
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            Grp3.Enabled = True
            RB3.Checked = True
            txtFamily.Text = ""
            txtSerial.Text = ""
            maskDatein.Focus()
        End If
    End Sub

    Private Sub RepPos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 195
    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDatein.GotFocus
        maskDatein.SelectionStart = 0
        maskDatein.SelectionLength = Len(maskDatein.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDatein.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDatein.Text
            Chkdate()
            If Datstat = 1 Then
                maskDatein.Focus()
                Exit Sub
            Else
                maskDateout.Focus()
            End If
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
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
                    .Range("H1").Value = "مدل"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "شماره سریال"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "RAM"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "موبایل"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "قلم"
                    .Range("L1").Font.Bold = True
                    .Range("M1").Value = "توضیحات"
                    .Range("M1").Font.Bold = True
                    'If RB6.Checked = False And RB8.Checked = False Then
                    '    .Range("N1").Value = "محل فعالیت"
                    '    .Range("N1").Font.Bold = True
                    '    '.Range("O1").Value = "شعبه"
                    '    '.Range("O1").Font.Bold = True
                    'Else
                    '    .Range("N1").Value = "تاریخ ارسال"
                    '    .Range("N1").Font.Bold = True
                    '    .Range("O1").Value = "شرح"
                    '    .Range("O1").Font.Bold = True
                    '    .Range("P1").Value = "محل فعالیت"
                    '    .Range("P1").Font.Bold = True
                    '    '.Range("Q1").Value = "شعبه"
                    '    '.Range("Q1").Font.Bold = True
                    'End If

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
                            'If RB6.Checked = False And RB8.Checked = False Then
                            '    .Range("N" & i.ToString).Value = DataGridView1.Rows(X).Cells(15).Value
                            '    '  .Range("O" & i.ToString).Value = DataGridView1.Rows(X).Cells(16).Value
                            'Else
                            '    .Range("N" & i.ToString).Value = DataGridView1.Rows(X).Cells(13).Value
                            '    .Range("O" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
                            '    .Range("P" & i.ToString).Value = DataGridView1.Rows(X).Cells(15).Value
                            '    ' .Range("Q" & i.ToString).Value = DataGridView1.Rows(X).Cells(16).Value
                            'End If
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
        If maskDatein.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ شروع را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDatein.Focus()
            Exit Sub
        ElseIf maskDateout.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ خاتمه را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDateout.Focus()
            Exit Sub
        End If
        If RB2.Checked = True Then
            If RB3.Checked = True Then
                If txtName.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا کد و یا نام پرسنل را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtCode.Focus()
                    Exit Sub
                End If
            ElseIf RB4.Checked = True Then
                If txtSerial.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا شماره پذیرنده را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtSerial.Focus()
                    Exit Sub
                End If
            End If
        End If

        FillRepTable()
        FillDataSetAndDataView()
        ' FillUpdate()
        'If DataGridView1.RowCount = 0 Then
        '    ProgressBar1.Value = 0
        'End If
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Code_Soft = 15
                FormName = "RepPos"
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
            txtCode.Clear()
            txtName.Clear()
        End If
    End Sub
    Private Sub txtSerial_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSerial.GotFocus
        txtSerial.SelectionStart = 0
        txtSerial.SelectionLength = Len(txtSerial.Text)
    End Sub

    Private Sub txtSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = ChrW(13) Then
            BtnOK.Focus()
        End If
    End Sub

    Private Sub txtCode_GotFocus(sender As Object, e As EventArgs) Handles txtCode.GotFocus
        txtCode.SelectionStart = 0
        txtCode.SelectionLength = Len(txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCode.KeyPress
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

    Private Sub maskDatein_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles maskDatein.MaskInputRejected

    End Sub

    Private Sub maskDateout_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles maskDateout.MaskInputRejected

    End Sub

    Private Sub maskDateout_KeyPress(sender As Object, e As KeyPressEventArgs) Handles maskDateout.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateout.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateout.Focus()
                Exit Sub
            Else
                BtnOK.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateout_GotFocus(sender As Object, e As EventArgs) Handles maskDateout.GotFocus
        maskDateout.SelectionStart = 0
        maskDateout.SelectionLength = Len(maskDateout.Text)
    End Sub

    Private Sub RB3_CheckedChanged(sender As Object, e As EventArgs) Handles RB3.CheckedChanged
        If RB3.Checked = True Then
            txtSerial.Text = ""
            txtCode.Focus()
        End If
    End Sub

    Private Sub RB4_CheckedChanged(sender As Object, e As EventArgs) Handles RB4.CheckedChanged
        If RB4.Checked = True Then
            txtFamily.Text = ""
            txtSerial.Focus()
        End If
    End Sub

    Private Sub txtSerial_TextChanged(sender As Object, e As EventArgs) Handles txtSerial.TextChanged

    End Sub
End Class