Imports JntNum2Text
Public Class ChkPrint
    'Private cheang As New JntNum2Text.Num2Text ' کد اصلی اینترنت
    Dim ColumnSrch As Integer
    Dim Da As New SqlDataAdapter
    Public Sub FillRow()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bas.ChkPrint WHERE (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.ChkPrint")
        objDataview = New DataView(objDataset.Tables("Bas.ChkPrint"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")
        If Label6.Text <> "" Then
            txtRow.Text = Val(Label6.Text) + 1
        ElseIf Label6.Text = "" Then
            txtRow.Text = 1
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Shob As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bas.View_ChkPrint WHERE (Shob = " & Shob & ")ORDER BY Date DESC")
      
        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bas.View_ChkPrint")
        objDataview = New DataView(objDataset.Tables("Bas.View_ChkPrint"))
        objConnection.Close()
        ' objDataview.Sort = "Row"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview
      
        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "تاریخ به حروف"
        Me.DataGridView1.Columns(3).HeaderText = "مبلغ"
        Me.DataGridView1.Columns(4).HeaderText = "مبلغ به حروف"
        Me.DataGridView1.Columns(5).HeaderText = "دروجه"
        Me.DataGridView1.Columns(6).Visible = False
        Me.DataGridView1.Columns(7).HeaderText = "وصول شده"
        Me.DataGridView1.Columns(8).Visible = False

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 250
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(4).Width = 400
        Me.DataGridView1.Columns(5).Width = 550
        Me.DataGridView1.Columns(7).Width = 80

        DataGridView1.Columns(3).DefaultCellStyle.Format = "N0"
    End Sub

    Private Sub Clear()
        maskDate.Text = "1   /  /"
        txtDate.Text = ""
        txtMblH.Text = ""
        txtVajh.Text = ""
        txtMbl.Text = 0

        CheckBox1.Checked = False
        maskDate.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Single

        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(1).Value
        txtDate.Text = DataGridView1.Rows(X).Cells(2).Value
        txtMbl.Text = DataGridView1.Rows(X).Cells(3).Value
        txtMblH.Text = DataGridView1.Rows(X).Cells(4).Value
        txtVajh.Text = DataGridView1.Rows(X).Cells(5).Value
        CheckBox1.CheckState = DataGridView1.Rows(X).Cells(6).Value
        maskDateV.Text = DataGridView1.Rows(X).Cells(7).Value
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub ChkPrint_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub ChkPrint_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            FillRow()
            Clear()
        End If
    End Sub

    Private Sub ChkPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        maskDate.Text = ConvD.Class1.ConvDate
        lblCutMbl.SendToBack()
        KeyPreview = True
    End Sub

    Private Sub txtMbl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMbl.GotFocus
        txtMbl.SelectionStart = 0
        txtMbl.SelectionLength = Len(txtMbl.Text)
    End Sub

    Private Sub txtMbl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMbl.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtMbl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMbl.TextChanged
        txtMbl.Text = Format(Val(txtMbl.Text.Trim.Replace(",", "")), "#,0")
        txtMbl.SelectionStart = txtMbl.TextLength

        If txtMbl.Text <> "" Then
            If txtMbl.Text <> 0 Then
                txtMblH.Text = Num2Text.ToFarsi(txtMbl.Text) + " " + "ریال"
                lblCutMbl.Text = txtMbl.Text + " " + "/---------"
            Else
                txtMblH.Text = ""
                lblCutMbl.Text = ""
            End If
        Else
            txtMblH.Text = ""
            lblCutMbl.Text = ""
        End If
    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.GotFocus
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate.Text
            Chkdate1()
            If Datstat = 1 Then
                maskDate.Focus()
                Exit Sub
            Else
                txtDate.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub txtDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.GotFocus
        txtDate.SelectionStart = 0
        txtDate.SelectionLength = Len(txtDate.Text)
    End Sub

    Private Sub txtDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMblH.Focus()
        End If
    End Sub

    Private Sub txtMblH_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMblH.GotFocus
        txtMblH.SelectionStart = 0
        txtMblH.SelectionLength = Len(txtMblH.Text)
    End Sub

    Private Sub txtMblH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMblH.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtVajh.Focus()
        End If
    End Sub

    Private Sub txtMblH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMblH.TextChanged

    End Sub

    Private Sub txtVajh_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVajh.GotFocus
        txtVajh.SelectionStart = 0
        txtVajh.SelectionLength = Len(txtVajh.Text)
    End Sub

    Private Sub txtVajh_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVajh.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMbl.Focus()
        End If
    End Sub

    Private Sub txtVajh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVajh.TextChanged

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRep.Click
        Dim Objcommand As New SqlCommand
        Dim Ln As String
        Dim Ln1 As String
        Dim Mbl As String
        Dim Mbl1 As String
        Dim Mbl2 As String
        Dim Mbl3 As String
        Dim Mbl4 As String
        Dim Mbl5 As String
        Dim Mbl6 As String
        Dim Mbl7 As String
        Dim Mbl8 As String
        Dim Mbl9 As String
        Dim Mbl10 As String
        Dim Mbl11 As String
        Dim Mbl12 As String
        Dim Mbl13 As String
        Dim Mbl14 As String
        Dim Mbl15 As String

        Objcommand.Connection = objConnection
        Objcommand.CommandText = _
            "DELETE FROM Bas.ChkPrintTmp"
        objConnection.Open()
        Try
            Objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        Mbl = Val(Str(txtMbl.Text))
        Ln = Len(Mbl)

        Ln1 = (Ln - 0)
        If Ln1 > 0 Then
            Mbl15 = Mid(Mbl, (Ln - 0), 1)
        Else
            Mbl15 = "-"
        End If
        Ln1 = (Ln - 1)
        If Ln1 > 0 Then
            Mbl14 = Mid(Mbl, (Ln - 1), 1)
        Else
            Mbl14 = "-"
        End If
        Ln1 = (Ln - 2)
        If Ln1 > 0 Then
            Mbl13 = Mid(Mbl, (Ln - 2), 1)
        Else
            Mbl13 = "-"
        End If
        Ln1 = (Ln - 3)
        If Ln1 > 0 Then
            Mbl12 = Mid(Mbl, (Ln - 3), 1)
        Else
            Mbl12 = "-"
        End If
        Ln1 = (Ln - 4)
        If Ln1 > 0 Then
            Mbl11 = Mid(Mbl, (Ln - 4), 1)
        Else
            Mbl11 = "-"
        End If
        Ln1 = (Ln - 5)
        If Ln1 > 0 Then
            Mbl10 = Mid(Mbl, (Ln - 5), 1)
        Else
            Mbl10 = "-"
        End If
        Ln1 = (Ln - 6)
        If Ln1 > 0 Then
            Mbl9 = Mid(Mbl, (Ln - 6), 1)
        Else
            Mbl9 = "-"
        End If
        Ln1 = (Ln - 7)
        If Ln1 > 0 Then
            Mbl8 = Mid(Mbl, (Ln - 7), 1)
        Else
            Mbl8 = "-"
        End If
        Ln1 = (Ln - 8)
        If Ln1 > 0 Then
            Mbl7 = Mid(Mbl, (Ln - 8), 1)
        Else
            Mbl7 = "-"
        End If
        Ln1 = (Ln - 9)
        If Ln1 > 0 Then
            Mbl6 = Mid(Mbl, (Ln - 9), 1)
        Else
            Mbl6 = "-"
        End If
        Ln1 = (Ln - 10)
        If Ln1 > 0 Then
            Mbl5 = Mid(Mbl, (Ln - 10), 1)
        Else
            Mbl5 = "-"
        End If
        Ln1 = (Ln - 11)
        If Ln1 > 0 Then
            Mbl4 = Mid(Mbl, (Ln - 11), 1)
        Else
            Mbl4 = "-"
        End If
        Ln1 = (Ln - 12)
        If Ln1 > 0 Then
            Mbl3 = Mid(Mbl, (Ln - 12), 1)
        Else
            Mbl3 = "-"
        End If
        Ln1 = (Ln - 13)
        If Ln1 > 0 Then
            Mbl2 = Mid(Mbl, (Ln - 13), 1)
        Else
            Mbl2 = "-"
        End If
        Ln1 = (Ln - 14)
        If Ln1 > 0 Then
            Mbl1 = Mid(Mbl, (Ln - 14), 1)
        Else
            Mbl1 = "-"
        End If

        Objcommand.Connection = objConnection
        Objcommand.CommandText = _
           " INSERT INTO Bas.ChkPrintTmp (Y1, Y2, Y3, Y4, M1, M2, D1, D2, Date, DatH, Mbl, Mbl1, Mbl2, Mbl3, Mbl4, Mbl5, Mbl6 ,Mbl7 ,Mbl8, Mbl9, Mbl10, Mbl11, Mbl12, Mbl13, Mbl14, Mbl15, MblH, Vajh, CutMbl)" & _
           " VALUES (@Y1, @Y2, @Y3, @Y4, @M1, @M2, @D1, @D2, @Date, @DatH, @Mbl, @Mbl1, @Mbl2, @Mbl3, @Mbl4, @Mbl5, @Mbl6, @Mbl7, @Mbl8, @Mbl9, @Mbl10, @Mbl11, @Mbl12, @Mbl13, @Mbl14, @Mbl15, @MblH, @Vajh, @CutMbl)"
        Objcommand.CommandType = CommandType.Text
        Objcommand.Parameters.Clear()
        Objcommand.Parameters.AddWithValue("@Y1", Mid(maskDate.Text, 1, 1))
        Objcommand.Parameters.AddWithValue("@Y2", Mid(maskDate.Text, 2, 1))
        Objcommand.Parameters.AddWithValue("@Y3", Mid(maskDate.Text, 3, 1))
        Objcommand.Parameters.AddWithValue("@Y4", Mid(maskDate.Text, 4, 1))
        Objcommand.Parameters.AddWithValue("@M1", Mid(maskDate.Text, 6, 1))
        Objcommand.Parameters.AddWithValue("@M2", Mid(maskDate.Text, 7, 1))
        Objcommand.Parameters.AddWithValue("@D1", Mid(maskDate.Text, 9, 1))
        Objcommand.Parameters.AddWithValue("@D2", Mid(maskDate.Text, 10, 1))
        Objcommand.Parameters.AddWithValue("@Date", maskDate.Text)
        Objcommand.Parameters.AddWithValue("@DatH", txtDate.Text)
        Objcommand.Parameters.AddWithValue("@Mbl", Str(txtMbl.Text))
        Objcommand.Parameters.AddWithValue("@Mbl1", Mbl1)
        Objcommand.Parameters.AddWithValue("@Mbl2", Mbl2)
        Objcommand.Parameters.AddWithValue("@Mbl3", Mbl3)
        Objcommand.Parameters.AddWithValue("@Mbl4", Mbl4)
        Objcommand.Parameters.AddWithValue("@Mbl5", Mbl5)
        Objcommand.Parameters.AddWithValue("@Mbl6", Mbl6)
        Objcommand.Parameters.AddWithValue("@Mbl7", Mbl7)
        Objcommand.Parameters.AddWithValue("@Mbl8", Mbl8)
        Objcommand.Parameters.AddWithValue("@Mbl9", Mbl9)
        Objcommand.Parameters.AddWithValue("@Mbl10", Mbl10)
        Objcommand.Parameters.AddWithValue("@Mbl11", Mbl11)
        Objcommand.Parameters.AddWithValue("@Mbl12", Mbl12)
        Objcommand.Parameters.AddWithValue("@Mbl13", Mbl13)
        Objcommand.Parameters.AddWithValue("@Mbl14", Mbl14)
        Objcommand.Parameters.AddWithValue("@Mbl15", Mbl15)
        Objcommand.Parameters.AddWithValue("@MblH", txtMblH.Text)
        Objcommand.Parameters.AddWithValue("@Vajh", txtVajh.Text)
        Objcommand.Parameters.AddWithValue("@CutMbl", lblCutMbl.Text)

        objConnection.Open()
        Try
            Objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Date FROM Bas.ChkPrintTmp", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.ChkPrintTmp")
        objDataview = New DataView(objDataset.Tables("Bas.ChkPrintTmp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Date"

        If objDataview.Count > 0 Then
            Rptpath.FileName = My.Application.Info.DirectoryPath & "\Report\Meli-Pak.rpt"
            Rptpath.SetDatabaseLogon(Us, Pw)
            RepForm.CrystReport.ReportSource = Rptpath
            RepForm.Label1.Text = "پرینت چک"
            RepForm.CrystReport.RefreshReport()
            RepForm.Show()
        Else
            MsgbOK.Label1.Text = " لطفا چک مورد نظر را انتخاب نمائید . "
            MsgbOK.ShowDialog()
            btnRep.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub maskDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.TextChanged
        If Mid(maskDate.Text, 9, 2) <> "" Then
            txtDate.Text = Num2Text.ToFarsi(Mid(maskDate.Text, 9, 2))

            If Mid(maskDate.Text, 6, 2) = "01" Then
                txtDate.Text = txtDate.Text + " " + "فروردین"
            ElseIf Mid(maskDate.Text, 6, 2) = "02" Then
                txtDate.Text = txtDate.Text + " " + "اردیبهشت"
            ElseIf Mid(maskDate.Text, 6, 2) = "03" Then
                txtDate.Text = txtDate.Text + " " + "خرداد"
            ElseIf Mid(maskDate.Text, 6, 2) = "04" Then
                txtDate.Text = txtDate.Text + " " + "تیر"
            ElseIf Mid(maskDate.Text, 6, 2) = "05" Then
                txtDate.Text = txtDate.Text + " " + "مرداد"
            ElseIf Mid(maskDate.Text, 6, 2) = "06" Then
                txtDate.Text = txtDate.Text + " " + "شهریور"
            ElseIf Mid(maskDate.Text, 6, 2) = "07" Then
                txtDate.Text = txtDate.Text + " " + "مهر"
            ElseIf Mid(maskDate.Text, 6, 2) = "08" Then
                txtDate.Text = txtDate.Text + " " + "آبان"
            ElseIf Mid(maskDate.Text, 6, 2) = "09" Then
                txtDate.Text = txtDate.Text + " " + "آذر"
            ElseIf Mid(maskDate.Text, 6, 2) = "10" Then
                txtDate.Text = txtDate.Text + " " + "دی"
            ElseIf Mid(maskDate.Text, 6, 2) = "11" Then
                txtDate.Text = txtDate.Text + " " + "بهمن"
            ElseIf Mid(maskDate.Text, 6, 2) = "12" Then
                txtDate.Text = txtDate.Text + " " + "اسفند"
            End If

            ' lblDateH.Text = lblDateH.Text + "" + TextPersian(ConvNumberToPersian.NumToFarsi(lblSal.Text))
            txtDate.Text = txtDate.Text + " " + Num2Text.ToFarsi(Mid(maskDate.Text, 1, 4))
        End If
    End Sub

    Private Sub txtDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDate.TextChanged

    End Sub

    Private Sub InsertUpdate()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdChkPrint"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Y", Mid(maskDate.Text, 3, 2))
            objCommand.Parameters.AddWithValue("@M", Mid(maskDate.Text, 6, 2))
            objCommand.Parameters.AddWithValue("@D", Mid(maskDate.Text, 9, 2))
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@DatH", txtDate.Text)
            objCommand.Parameters.AddWithValue("@Mbl", Str(txtMbl.Text))
            objCommand.Parameters.AddWithValue("@CutMbl", lblCutMbl.Text)
            objCommand.Parameters.AddWithValue("@MblH", txtMblH.Text)
            objCommand.Parameters.AddWithValue("@Vajh", txtVajh.Text)
            ' objCommand.Parameters.AddWithValue("@Knd", Rb)
            objCommand.Parameters.AddWithValue("@Chk", CheckBox1.CheckState)
            objCommand.Parameters.AddWithValue("@DatV", maskDateV.Text)
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

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ چک را وارد کنید ."
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf Me.txtDate.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تاریخ به حروف را وارد کنید ."
            MsgbOK.ShowDialog()
            txtDate.Focus()
            Exit Sub
        ElseIf Me.txtMblH.Text = "" Then
            MsgbOK.Label1.Text = " لطفا مبلغ به حروف را وارد کنید ."
            MsgbOK.ShowDialog()
            txtMblH.Focus()
            Exit Sub
        ElseIf Me.txtVajh.Text = "" Then
            MsgbOK.Label1.Text = " لطفا در وجه را وارد کنید ."
            MsgbOK.ShowDialog()
            txtVajh.Focus()
            Exit Sub
        ElseIf CheckBox1.Checked = True Then
            If maskDateV.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ وصول را وارد کنید ."
                MsgbOK.ShowDialog()
                maskDateV.Focus()
                Exit Sub
            End If
            'ElseIf Rb = 4 Then
            '    MsgbOK.Label1.Text = " لطفا نوع بانک را مشخص نمائید ."
            '    MsgbOK.ShowDialog()
            '    maskDate.Focus()
            '    Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob FROM Bas.ChkPrint WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.ChkPrint")
        objDataview = New DataView(objDataset.Tables("Bas.ChkPrint"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            'FillRow()
            FillDatasetAndDataview()
            FillDataGridView()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub btnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRef.Click
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub Edt()
        Dim objCommand As New SqlCommand
        Dim Shob As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ چک را وارد کنید ."
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf Me.txtDate.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تاریخ به حروف را وارد کنید ."
            MsgbOK.ShowDialog()
            txtDate.Focus()
            Exit Sub
        ElseIf Me.txtMblH.Text = "" Then
            MsgbOK.Label1.Text = " لطفا مبلغ به حروف را وارد کنید ."
            MsgbOK.ShowDialog()
            txtMblH.Focus()
            Exit Sub
        ElseIf Me.txtVajh.Text = "" Then
            MsgbOK.Label1.Text = " لطفا در وجه را وارد کنید ."
            MsgbOK.ShowDialog()
            txtVajh.Focus()
            Exit Sub
        ElseIf CheckBox1.Checked = True Then
            If maskDateV.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ وصول را وارد کنید ."
                MsgbOK.ShowDialog()
                maskDateV.Focus()
                Exit Sub
            End If
            'ElseIf Rb = 4 Then
            '    MsgbOK.Label1.Text = " لطفا نوع بانک را مشخص نمائید ."
            '    MsgbOK.ShowDialog()
            '    maskDate.Focus()
            '    Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob FROM Bas.ChkPrint WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.ChkPrint")
        objDataview = New DataView(objDataset.Tables("Bas.ChkPrint"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillRow()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Dim objCommand As New SqlCommand
        Dim Shob As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob FROM Bas.ChkPrint WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.ChkPrint")
        objDataview = New DataView(objDataset.Tables("Bas.ChkPrint"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                maskDate.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                FillRow()
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
        ' If Label18.Text <> "" Then
        If txtSrch.Text <> "" Then
            Select Case ColumnSrch
                Case "1"
                    objDataview.RowFilter = "Date like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "3"
                    txtSrch.Text = Format(Val(txtSrch.Text.Trim.Replace(",", "")), "#,0")
                    txtSrch.SelectionStart = txtSrch.TextLength
                    objDataview.RowFilter = "Mbl = '" & txtSrch.Text & "'"
                Case "5"
                    objDataview.RowFilter = "Vajh like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End Select
        Else
            '    txtSrch.Text = ""
            FillDatasetAndDataview()
        End If
        FillDataGridView()
        'Else
        'MsgbOK.Label1.Text = " لطفا ستون مورد نظر جهت جستجو را انتخاب کنید ."
        'MsgbOK.ShowDialog()
        'txtSrch.Focus()
        'Exit Sub
        'End If
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
                .Range("A1").Value = "ردیف"
                .Range("A1").Font.Bold = True
                .Range("B1").Value = "تاریخ"
                .Range("B1").Font.Bold = True
                .Range("C1").Value = "تاریخ به حروف"
                .Range("C1").Font.Bold = True
                .Range("D1").Value = "مبلغ"
                .Range("D1").Font.Bold = True
                .Range("E1").Value = "مبلغ به حروف"
                .Range("E1").Font.Bold = True
                .Range("F1").Value = "دروجه"
                .Range("F1").Font.Bold = True
                .Range("G1").Value = "وصول شده"
                .Range("G1").Font.Bold = True

                For X = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Rows(X).Selected = True Then
                        .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                        .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                        .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                        .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                        .Range("D" & i.ToString).NumberFormat = "#,##0"
                        .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                        .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                        .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                        i += 1
                    End If
                Next X
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            maskDateV.Visible = True
            maskDateV.Focus()
        Else
            maskDateV.Text = "1   /  /"
            maskDateV.Visible = False
        End If
    End Sub

    Private Sub ChkPrint_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 215
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        If ColumnSrch = 1 Or ColumnSrch = 3 Or ColumnSrch = 5 Then
            Label18.Text = Me.DataGridView1.Columns(e.ColumnIndex).HeaderText
        Else
            Label18.Text = ""
        End If
        Me.txtSrch.Text = ""
        Me.txtSrch.Focus()
    End Sub

    Private Sub DataGridView1_CellContentClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub
End Class