Public Class TamirKala
    Dim ColumnSrch As Integer

    Public Sub FillRow()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.TamirKala WHERE (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.TamirKala")
        objDataview = New DataView(objDataset.Tables("Bnk.TamirKala"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")
        If Label6.Text <> "" Then
            txtRow.Text = Val(Label6.Text) + 1
        ElseIf Label6.Text = "" Then
            txtRow.Text = Sal & "00001"
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bnk.View_TamirKala WHERE (Shob =" & Shob & ") ORDER BY Row")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bnk.View_TamirKala")
        objDataview = New DataView(objDataset.Tables("Bnk.View_TamirKala"))
        ' objDataview.Sort = "Row"
        objConnection.Close()

        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "کد کالا"
        Me.DataGridView1.Columns(3).HeaderText = "نام کالا"
        Me.DataGridView1.Columns(4).HeaderText = "تعداد ارسال"
        Me.DataGridView1.Columns(5).HeaderText = "تاریخ ارسال"
        Me.DataGridView1.Columns(6).HeaderText = "تعداد برگشت"
        Me.DataGridView1.Columns(7).HeaderText = "تاریخ برگشت"
        Me.DataGridView1.Columns(8).HeaderText = "مانده"
        Me.DataGridView1.Columns(9).HeaderText = "شرح"
        Me.DataGridView1.Columns(10).Visible = False ' Shob

        Me.DataGridView1.Columns(0).Width = 70
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 100
        Me.DataGridView1.Columns(3).Width = 250
        Me.DataGridView1.Columns(4).Width = 50
        Me.DataGridView1.Columns(5).Width = 70
        Me.DataGridView1.Columns(6).Width = 50
        Me.DataGridView1.Columns(7).Width = 70
        Me.DataGridView1.Columns(8).Width = 50
        Me.DataGridView1.Columns(9).Width = 400
    End Sub

    Private Sub Clear()
        Me.txtName.Clear()
        Me.txtCount1.Text = 0
        Me.maskDate1.Text = "1   /  /"
        Me.txtCount2.Text = 0
        Me.maskDate2.Text = "1   /  /"
        Me.txtMande.Text = 0
        Me.txtDscr.Text = ""
        Me.txtCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.maskDate.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(2).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(3).Value
        FillKalaMoj()
        Me.txtCount.Text = 0
        Me.txtCount.DataBindings.Clear()
        Me.txtCount.DataBindings.Add("Text", objDataview, "CountOk")
        Me.txtCount1.Text = Me.DataGridView1.Rows(X).Cells(4).Value
        Me.maskDate1.Text = Me.DataGridView1.Rows(X).Cells(5).Value
        Me.txtCount2.Text = Me.DataGridView1.Rows(X).Cells(6).Value
        Me.maskDate2.Text = Me.DataGridView1.Rows(X).Cells(7).Value
        Me.txtMande.Text = Me.DataGridView1.Rows(X).Cells(8).Value
        Me.txtDscr.Text = Me.DataGridView1.Rows(X).Cells(9).Value
    End Sub


    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text = "" Then
                Code_Soft = 15
                FormName = "TamirKala"
                FillKalaMoj()
                FillDataKala()
                Srch.lblName.Text = "نام کالا"
                Srch.ShowDialog()
            Else
                txtCount1.Focus()
            End If
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub Del()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه " & Trim(Me.txtRow.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtCode.Focus()
            Exit Sub
        Else
            '--------LogTamirKala
            logStat = 3
            SaveLogTamirKala()
            '------------------
            Btn = 3
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub Edt()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtCount1.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCount1.Focus()
            Exit Sub
        ElseIf txtCount1.Text > 0 Then
            If Me.maskDate1.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ ارسال را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.maskDate1.Focus()
                Exit Sub
            End If
        ElseIf txtCount2.Text > 0 Then
            If Me.maskDate2.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ برگشت را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.maskDate2.Focus()
                Exit Sub
            End If
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()
            '--------LogTamirKala
            logStat = 2
            SaveLogTamirKala()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub SaveLogTamirKala()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].TamirKalaLog (Row, Dat, Kala_code, OutCount, OutDat, InCount, InDat, Mande, Dscr, Shob, Sal)" & _
           " SELECT Row, Dat, Kala_code, OutCount, OutDat, InCount, InDat, Mande, Dscr, Shob, Sal FROM Bnk.Tamirkala WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")"

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
            " UPDATE [Log].TamirKalaLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
            " WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND (tim_stat IS NULL)"
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

    Private Sub InsertUpdate()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdTamirKala"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            'objCommand.Parameters.AddWithValue("@RowMoj", txtRowMoj.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Code", txtCode.Text)
            objCommand.Parameters.AddWithValue("@OutCount", txtCount1.Text)
            objCommand.Parameters.AddWithValue("@OutDat", maskDate1.Text)
            objCommand.Parameters.AddWithValue("@InCount", txtCount2.Text)
            objCommand.Parameters.AddWithValue("@InDat", maskDate2.Text)
            objCommand.Parameters.AddWithValue("@Mande", txtMande.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Shob", Shob)
            objCommand.Parameters.AddWithValue("@Sal", Sal)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtCount1.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCount1.Focus()
            Exit Sub
        ElseIf txtCount1.Text > 0 Then
            If Me.maskDate1.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ ارسال را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.maskDate1.Focus()
                Exit Sub
            End If
        ElseIf txtCount2.Text > 0 Then
            If Me.maskDate2.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ برگشت را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.maskDate2.Focus()
                Exit Sub
            End If
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(Me.txtRow.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()
            '--------LogTamirKala
            logStat = 1
            SaveLogTamirKala()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        Label7.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
        If ColumnSrch = 3 Or ColumnSrch = 8 Then
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        Else
            Me.txtSrch.Text = ""
            Label7.Text = "-----"
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub TamirKala_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        FormName = "TamirKala"
    End Sub

    Private Sub TamirKala_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.Space Then
            Spc()
        End If
    End Sub

    Private Sub Spc()
        If maskDate.Focused = True Then
            maskDate.Text = ConvD.Class1.ConvDate
        ElseIf maskDate1.Focused = True Then
            maskDate1.Text = ConvD.Class1.ConvDate
        ElseIf maskDate2.Focused = True Then
            maskDate2.Text = ConvD.Class1.ConvDate
        End If
    End Sub
    Private Sub TamirKala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        maskDate.Text = ConvD.Class1.ConvDate
        txtCode.SendToBack()
        txtRowMoj.SendToBack()

        Me.KeyPreview = True
        FormName = "TamirKala"
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub TamirKala_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        Me.txtCode.SelectionStart = 0
        Me.txtCode.SelectionLength = Len(Me.txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCount.Text = 0
            txtCode.Text = ""
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub maskDate1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate1.GotFocus
        maskDate1.SelectionStart = 0
        maskDate1.SelectionLength = Len(maskDate1.Text)
    End Sub

    Private Sub maskDate1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub maskDate1_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate1.MaskInputRejected

    End Sub

    Private Sub txtCount1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount1.GotFocus
        txtCount1.SelectionStart = 0
        txtCount1.SelectionLength = Len(txtCount1.Text)
    End Sub

    Private Sub txtCount1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount1.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDate1.Focus()
        End If
    End Sub

    Private Sub txtCount1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount1.LostFocus
        If Val(txtCount1.Text) > Val(txtCount.Text) Then
            MsgbOK.Label1.Text = " تعداد وارد شده بیشتر از موجودی است . "
            MsgbOK.ShowDialog()
            Me.txtCount1.Text = 0
            Me.maskDate1.Focus()
            Exit Sub
        End If
        txtMande.Text = Val(txtCount1.Text) - Val(txtCount2.Text)
    End Sub

    Private Sub txtCount1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCount1.TextChanged
        If txtCount1.Text = "" Then
            txtCount1.Text = 0
        End If
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
                txtName.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub txtCount2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount2.GotFocus
        txtCount2.SelectionStart = 0
        txtCount2.SelectionLength = Len(txtCount2.Text)
    End Sub

    Private Sub txtCount2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount2.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDate2.Focus()
        End If
    End Sub

    Private Sub txtCount2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount2.LostFocus
        If Val(txtCount2.Text) > Val(txtCount1.Text) Then
            MsgbOK.Label1.Text = " تعداد وارد شده بیشتر از ارسال است . "
            MsgbOK.ShowDialog()
            Me.txtCount2.Text = 0
            Me.maskDate2.Focus()
            Exit Sub
        End If
        txtMande.Text = Val(txtCount1.Text) - Val(txtCount2.Text)
    End Sub

    Private Sub txtCount2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCount2.TextChanged
        If txtCount2.Text = "" Then
            txtCount2.Text = 0
        End If
    End Sub

    Private Sub maskDate2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate2.GotFocus
        maskDate2.SelectionStart = 0
        maskDate2.SelectionLength = Len(maskDate2.Text)
    End Sub

    Private Sub maskDate2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate2.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub maskDate2_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate2.MaskInputRejected

    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
        FormName = "TamirKala"
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
                    .Range("B1").Value = "تاریخ"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "کد"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نام کالا"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "تعداد ارسال"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "تاریخ ارسال"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "تعداد برگشت"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "تاریخ برگشت"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "مانده"
                    .Range("I1").Font.Bold = True
                    .Range("j1").Value = "شرح"
                    .Range("j1").Font.Bold = True

                    ' .Range("A" & i.ToString).AutoFormat()
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
                            .Range("j" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
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

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        Select Case ColumnSrch
            Case "3"
                objDataview.RowFilter = "Kala_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
            Case "8"
                objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End Select
        FillDataGridView()
        lblRow.Text = objDataview.Count
    End Sub
End Class