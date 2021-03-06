Public Class PrintReq
    Dim ColumnSrch As Integer
    Dim Da As New SqlDataAdapter

    Public Sub FillRow()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.PrintReq WHERE (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PrintReq")
        objDataview = New DataView(objDataset.Tables("Bnk.PrintReq"))
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

    Private Sub FillCboVahed()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Vahed_Code, Vahed_Name FROM bas.Vahed ORDER BY Vahed_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Vahed")
        Me.cboVahed.DataSource = objDataset.Tables("bas.Vahed")
        Me.cboVahed.DisplayMember = "Vahed_Name"
        Me.cboVahed.ValueMember = "Vahed_Code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bnk.View_PrintReq WHERE (Sal = " & Sal & ") AND (Shob = " & Shob & ") ORDER BY Row DESC")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bnk.View_PrintReq")
        objDataview = New DataView(objDataset.Tables("Bnk.View_PrintReq"))
        objConnection.Close()
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).Visible = False ' ShobCode
        Me.DataGridView1.Columns(3).HeaderText = "شعبه"
        Me.DataGridView1.Columns(4).Visible = False ' VCode
        Me.DataGridView1.Columns(5).HeaderText = "واحد سازمانی"
        Me.DataGridView1.Columns(6).Visible = False ' PrintCode
        Me.DataGridView1.Columns(7).HeaderText = "پرینتر"
        Me.DataGridView1.Columns(8).HeaderText = "سریال"
        Me.DataGridView1.Columns(9).HeaderText = "مبلغ"
        Me.DataGridView1.Columns(10).HeaderText = "شرح"
        Me.DataGridView1.Columns(11).HeaderText = "شرح فاکتور"
        Me.DataGridView1.Columns(12).Visible = False ' Sal

        DataGridView1.Columns(9).DefaultCellStyle.Format = "N0"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(5).Width = 100
        Me.DataGridView1.Columns(7).Width = 120
        Me.DataGridView1.Columns(8).Width = 120
        Me.DataGridView1.Columns(9).Width = 120
        Me.DataGridView1.Columns(10).Width = 400
        Me.DataGridView1.Columns(11).Width = 400
    End Sub

    Private Sub Clear()
        Me.txtName.Clear()
        Me.txtSerial.Text = 0
        Me.txtMbl.Text = 0
        Me.txtDscr.Clear()
        Me.txtDscrFact.Clear()
        Me.maskDate.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.maskDate.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.cboVahed.SelectedValue = Me.DataGridView1.Rows(X).Cells(4).Value
        Me.txtCodeP.Text = Me.DataGridView1.Rows(X).Cells(6).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(7).Value
        Me.txtSerial.Text = Me.DataGridView1.Rows(X).Cells(8).Value
        Me.txtMbl.Text = Me.DataGridView1.Rows(X).Cells(9).Value
        Me.txtDscr.Text = Me.DataGridView1.Rows(X).Cells(10).Value
        Me.txtDscrFact.Text = Me.DataGridView1.Rows(X).Cells(11).Value
    End Sub


    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCodeP.Text = "" Then
                Code_Soft = 15
                FormName = "PrintReq"
                FillPrintReq()
                FillDataPrint()
                Srch.Width = 700
                Srch.ToolStripLabel2.Width = 480
                Srch.lblName.Text = "لیست پرینتر"
                Srch.ShowDialog()
            Else
                txtSerial.Focus()
            End If
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub Del()
        If Me.maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDate.Focus()
            Exit Sub
        End If

        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.PrintReq WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PrintReq")
        objDataview = New DataView(objDataset.Tables("Bnk.PrintReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید ردیف " & Trim(Me.txtRow.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtRow.Focus()
            Exit Sub
        Else
            '--------LogPrintReq
            logStat = 3
            SaveLogPrintReq()
            '-------------------
            Btn = 3
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub Edt()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If Me.maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDate.Focus()
            Exit Sub
        ElseIf Me.txtCodeP.Text = "" Then
            MsgbOK.Label1.Text = " لطفا پرینتر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtSerial.Text = "" Then
            MsgbOK.Label1.Text = " لطفا سریال را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtSerial.Focus()
            Exit Sub
        End If

        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.PrintReq WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PrintReq")
        objDataview = New DataView(objDataset.Tables("Bnk.PrintReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()
            '--------LogPrintReq
            logStat = 2
            SaveLogPrintReq()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub SaveLogPrintReq()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].PrintReqLog (Row, Dat, Vahed, Printer, Serial, Mbl, Dscr, DscrFact, Shob, Sal)" & _
           " SELECT Row, Dat, Vahed, Printer, Serial, Mbl, Dscr, DscrFact, Shob, Sal FROM Bnk.PrintReq WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")"

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
            " UPDATE [Log].PrintReqLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
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
            objCommand.CommandText = "Bnk.InsUpdPrintReq"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Vahed", cboVahed.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Printer", txtCodeP.Text)
            objCommand.Parameters.AddWithValue("@Serial", txtSerial.Text)
            objCommand.Parameters.AddWithValue("@Shob", Shob)
            objCommand.Parameters.AddWithValue("@Mbl", Str(txtMbl.Text))
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@DscrFact", txtDscrFact.Text)
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
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If Me.maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDate.Focus()
            Exit Sub
        ElseIf Me.txtCodeP.Text = "" Then
            MsgbOK.Label1.Text = " لطفا پرینتر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtSerial.Text = "" Then
            MsgbOK.Label1.Text = " لطفا سریال را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtSerial.Focus()
            Exit Sub
        End If

        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.PrintReq WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PrintReq")
        objDataview = New DataView(objDataset.Tables("Bnk.PrintReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(Me.txtRow.Text) & "  در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()
            '--------LogPrintReq
            logStat = 1
            SaveLogPrintReq()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        If ColumnSrch = 1 Or ColumnSrch > 6 Then
            Label26.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        Else
            Me.txtSrch.Text = ""
            Label26.Text = "-----"
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub PrintReq_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillCboVahed()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub PrintReq_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        End If
    End Sub
    Private Sub PrintReq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        maskDate.Text = ConvD.Class1.ConvDate
        txtCodeP.SendToBack()

        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub PrintReq_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.GotFocus
        Me.maskDate.SelectionStart = 0
        Me.maskDate.SelectionLength = Len(Me.maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate.Focus()
                Exit Sub
            Else
                cboVahed.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub cboVahed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboVahed.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub cboVahed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVahed.SelectedIndexChanged

    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCodeP.Text = ""
            txtSerial.Text = 0
        End If
    End Sub

    Private Sub txtSerial_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSerial.GotFocus
        Me.txtSerial.SelectionStart = 0
        Me.txtSerial.SelectionLength = Len(Me.txtSerial.Text)
    End Sub

    Private Sub txtSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMbl.Focus()
        End If
    End Sub

    Private Sub txtSerial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerial.TextChanged

    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        Me.txtDscr.SelectionStart = 0
        Me.txtDscr.SelectionLength = Len(Me.txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtDscrFact.Focus()
        End If
    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        If txtSrch.Text <> "" Then
            Select Case ColumnSrch
                Case "1"
                    objDataview.RowFilter = "Dat like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "7"
                    objDataview.RowFilter = "prn_name like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "8"
                    objDataview.RowFilter = "Serial like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "9"
                    txtSrch.Text = Format(Val(txtSrch.Text.Trim.Replace(",", "")), "#,0")
                    txtSrch.SelectionStart = txtSrch.TextLength
                    objDataview.RowFilter = "Mbl = '" & txtSrch.Text & "'"
                Case "10"
                    objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End Select
        Else
            txtSrch.Text = ""
            FillDatasetAndDataview()
        End If
        FillDataGridView()
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub cboShob_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            cboVahed.Focus()
        End If
    End Sub

    Private Sub cboShob_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

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
                    .Range("A1").Value = "ردیف"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "تاریخ"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "محل فعالیت"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "واحد سازمانی"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "پرینتر"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "سریال"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "مبلغ"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "شرح"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "شرح فاکتور"
                    .Range("I1").Font.Bold = True

                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                            .Range("G" & i.ToString).NumberFormat = "#,##0"
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            i += 1
                        End If
                    Next X
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgbOK.Label1.Text = "رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtMbl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMbl.GotFocus
        Me.txtMbl.SelectionStart = 0
        Me.txtMbl.SelectionLength = Len(Me.txtMbl.Text)
    End Sub

    Private Sub txtMbl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMbl.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtMbl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMbl.TextChanged
        txtMbl.Text = Format(Val(txtMbl.Text.Trim.Replace(",", "")), "#,0")
        txtMbl.SelectionStart = txtMbl.TextLength
    End Sub

    Private Sub txtDscrFact_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscrFact.GotFocus
        Me.txtDscrFact.SelectionStart = 0
        Me.txtDscrFact.SelectionLength = Len(Me.txtDscrFact.Text)
    End Sub

    Private Sub txtDscrFact_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrFact.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrFact_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrFact.TextChanged

    End Sub
End Class