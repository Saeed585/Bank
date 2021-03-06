Public Class KatrijReq
    Dim ColumnSrch As Integer
    Dim Da As New SqlDataAdapter

    Public Sub FillRow()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT MAX(Row) AS Expr1 FROM Bnk.KatrijReq WHERE (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KatrijReq")
        objDataview = New DataView(objDataset.Tables("Bnk.KatrijReq"))
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
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bnk.View_KatrijReq WHERE (Shob = " & Shob & ") AND (Sal = " & Sal & ") ORDER BY Row DESC")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bnk.View_KatrijReq")
        objDataview = New DataView(objDataset.Tables("Bnk.View_KatrijReq"))
        objConnection.Close()
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).Visible = False ' CodeK
        Me.DataGridView1.Columns(3).HeaderText = "کاتریج"
        Me.DataGridView1.Columns(4).Visible = False ' Knd
        Me.DataGridView1.Columns(5).HeaderText = "نوع سرویس"
        Me.DataGridView1.Columns(6).HeaderText = "تعداد"
        Me.DataGridView1.Columns(7).HeaderText = "مبلغ"
        Me.DataGridView1.Columns(8).HeaderText = "شرح"
        Me.DataGridView1.Columns(9).Visible = False ' Shob
        Me.DataGridView1.Columns(10).Visible = False ' Sal

        DataGridView1.Columns(7).DefaultCellStyle.Format = "N0"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(5).Width = 80
        Me.DataGridView1.Columns(6).Width = 60
        Me.DataGridView1.Columns(7).Width = 120
        Me.DataGridView1.Columns(8).Width = 500

        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Cells(4).Value = 0 Then
                DataGridView1.Rows(X).Cells(5).Value = "شارژ"
            ElseIf DataGridView1.Rows(X).Cells(4).Value = 1 Then
                DataGridView1.Rows(X).Cells(5).Value = "تعمیر"
            End If
        Next
    End Sub

    Private Sub Clear()
        txtName.Text = ""
        txtCount.Text = 0
        txtMbl.Text = 0
        txtDscr.Text = ""
        txtName.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Single

        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(1).Value
        txtCodeK.Text = DataGridView1.Rows(X).Cells(2).Value
        txtName.Text = DataGridView1.Rows(X).Cells(3).Value
        cboReq.SelectedIndex = DataGridView1.Rows(X).Cells(4).Value
        txtCount.Text = DataGridView1.Rows(X).Cells(6).Value
        txtMbl.Text = DataGridView1.Rows(X).Cells(7).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(8).Value
    End Sub
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub KatrijReq_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub KatrijReq_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub KatrijReq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        maskDate.Text = ConvD.Class1.ConvDate
        cboReq.SelectedIndex = 0

        Me.KeyPreview = True
        txtCodeK.SendToBack()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub SaveLogKatrijReq()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].KatrijReqLog (Row, Dat, CodeK, Knd, [Count], Mbl, Dscr, Shob, Sal)" & _
           " SELECT Row, Dat, CodeK, Knd, [Count], Mbl, Dscr, Shob, Sal FROM Bnk.KatrijReq WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")"

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
            " UPDATE [Log].KatrijReqLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
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
            objCommand.CommandText = "Bnk.InsUpdKatrijReq"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Code", txtCodeK.Text)
            objCommand.Parameters.AddWithValue("@Knd", cboReq.SelectedIndex)
            objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
            objCommand.Parameters.AddWithValue("@Mbl", Str(txtMbl.Text))
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Sal", Sal)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            objConnection.Close()
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید ."
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtCodeK.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نوع کاتریج را وارد کنید ."
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.KatrijReq WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KatrijReq")
        objDataview = New DataView(objDataset.Tables("Bnk.KatrijReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " ردیف " & txtRow.Text & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()
            '--------LogKatrijReq
            logStat = 1
            SaveLogKatrijReq()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید ."
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtCodeK.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نوع کاتریج را وارد کنید ."
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.KatrijReq WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KatrijReq")
        objDataview = New DataView(objDataset.Tables("Bnk.KatrijReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & txtRow.Text & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()
            '--------LogKatrijReq
            logStat = 2
            SaveLogKatrijReq()

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

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید ."
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtCodeK.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نوع کاتریج را وارد کنید ."
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.KatrijReq WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KatrijReq")
        objDataview = New DataView(objDataset.Tables("Bnk.KatrijReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " کد " & txtRow.Text & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد  " & txtRow.Text & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtDscr.Focus()
                Exit Sub
            Else
                '--------LogKatrijReq
                logStat = 3
                SaveLogKatrijReq()
                '------------------
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
        If ColumnSrch <> 0 Or ColumnSrch <> 5 Or ColumnSrch <> 6 Then
            Label26.Text = Me.DataGridView1.Columns(e.ColumnIndex).HeaderText
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        Else
            Me.txtSrch.Text = ""
            Label26.Text = "-----"
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

    Private Sub KatrijReq_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        txtDscr.SelectionStart = 0
        txtDscr.SelectionLength = Len(txtDscr.Text)
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

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
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
                    .Range("C1").Value = "کاتریج"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نوع سرویس"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "تعداد"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "مبلغ"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "شرح"
                    .Range("G1").Font.Bold = True

                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("F" & i.ToString).NumberFormat = "#,##0"
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            i += 1
                        End If
                    Next X
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgbOK.Label1.Text = " جدول اطلاعات خالی میباشد . "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        If txtSrch.Text <> "" Then
            Select Case ColumnSrch
                Case "1"
                    objDataview.RowFilter = "Dat like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "3"
                    objDataview.RowFilter = "name like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "7"
                    txtSrch.Text = Format(Val(txtSrch.Text.Trim.Replace(",", "")), "#,0")
                    txtSrch.SelectionStart = txtSrch.TextLength
                    objDataview.RowFilter = "Mbl = '" & txtSrch.Text & "'"
                Case "8"
                    objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End Select
        Else
            txtSrch.Text = ""
            FillDatasetAndDataview()
        End If
        FillDataGridView()
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCodeK.Text = "" Then
                Code_Soft = 15
                FormName = "KatrijReq"
                FillKatrijReq()
                FillDataKatrij()
                Srch.lblName.Text = "لیست کاتریج"
                Srch.ShowDialog()
            Else
                cboReq.Focus()
            End If
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCodeK.Text = ""
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

    Private Sub cboReq_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboReq.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCount.Focus()
        End If
    End Sub

    Private Sub cboReq_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboReq.SelectedIndexChanged

    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub txtCount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount.GotFocus
        Me.txtCount.SelectionStart = 0
        Me.txtCount.SelectionLength = Len(Me.txtCount.Text)
    End Sub

    Private Sub txtCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMbl.Focus()
        End If
    End Sub

    Private Sub txtCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCount.TextChanged

    End Sub
End Class