Public Class TahvilKala
    Dim ColumnSrch As Integer
    Public Sub FillRow()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.TahvilKala WHERE (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.TahvilKala")
        objDataview = New DataView(objDataset.Tables("Bnk.TahvilKala"))
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

    Private Sub fillcboSGrpKala()
        Dim objDataAdapter As New SqlDataAdapter _
             (" SELECT Bas.KalaSGrp.SCode, Bas.KalaSGrp.SName FROM Bas.KalaSGrp INNER JOIN Bas.SGKala_Sec ON Bas.KalaSGrp.SCode = Bas.SGKala_Sec.SarGrpK_Code" &
             " WHERE (Bas.SGKala_Sec.U_Code = 1) ORDER BY Bas.KalaSGrp.SCode", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.KalaSGrp")
        Me.cboSGrpKala.DataSource = objDataset.Tables("bas.KalaSGrp")
        Me.cboSGrpKala.DisplayMember = "SName"
        Me.cboSGrpKala.ValueMember = "SCode"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bnk.View_TahvilKala WHERE (Shob =" & Shob & ") ORDER BY Row")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bnk.View_TahvilKala")
        objDataview = New DataView(objDataset.Tables("Bnk.View_TahvilKala"))
        ' objDataview.Sort = "Row"
        objConnection.Close()

        'lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).Visible = False ' Scode
        Me.DataGridView1.Columns(2).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(3).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(4).HeaderText = "نام"
        Me.DataGridView1.Columns(5).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(6).Visible = False ' Kcode
        Me.DataGridView1.Columns(7).HeaderText = "نام کالا"
        Me.DataGridView1.Columns(8).HeaderText = "تعداد"
        Me.DataGridView1.Columns(9).HeaderText = "تاریخ تعویض"
        Me.DataGridView1.Columns(10).HeaderText = "شرح"
        Me.DataGridView1.Columns(11).Visible = False ' KB
        Me.DataGridView1.Columns(12).HeaderText = "وضعیت"
        Me.DataGridView1.Columns(13).Visible = False ' Shob
        Me.DataGridView1.Columns(14).Visible = False ' Sal

        Me.DataGridView1.Columns(0).Width = 60
        Me.DataGridView1.Columns(2).Width = 70
        Me.DataGridView1.Columns(3).Width = 60
        Me.DataGridView1.Columns(4).Width = 80
        Me.DataGridView1.Columns(5).Width = 120
        Me.DataGridView1.Columns(7).Width = 250
        Me.DataGridView1.Columns(8).Width = 60
        Me.DataGridView1.Columns(9).Width = 70
        Me.DataGridView1.Columns(10).Width = 500
        Me.DataGridView1.Columns(12).Width = 80

        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Cells(11).Value = 1 Then
                DataGridView1.Rows(X).Cells(12).Value = "بسته شده"
            End If
        Next
    End Sub

    Private Sub Clear()
        Me.maskDate.Text = ConvD.Class1.ConvDate
        Me.txtFamily.Text = ""
        Me.txtKName.Text = ""
        Me.txtCount.Text = ""
        Me.maskDateTav.Text = "1   /  /"
        Me.txtDscr.Text = ""
        CheckBox1.Checked = False
        Me.txtCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        cboSGrpKala.SelectedValue = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.maskDate.Text = Me.DataGridView1.Rows(X).Cells(2).Value
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(3).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(4).Value
        Me.txtFamily.Text = Me.DataGridView1.Rows(X).Cells(5).Value
        Me.txtKCode.Text = Me.DataGridView1.Rows(X).Cells(6).Value
        Me.txtKName.Text = Me.DataGridView1.Rows(X).Cells(7).Value
        Me.txtCount.Text = Me.DataGridView1.Rows(X).Cells(8).Value
        Me.maskDateTav.Text = Me.DataGridView1.Rows(X).Cells(9).Value
        Me.txtDscr.Text = Me.DataGridView1.Rows(X).Cells(10).Value
        Me.CheckBox1.CheckState = Me.DataGridView1.Rows(X).Cells(11).Value
    End Sub

    Private Sub txtKName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKName.GotFocus
        Me.txtKName.SelectionStart = 0
        Me.txtKName.SelectionLength = Len(Me.txtKName.Text)
    End Sub

    Private Sub txtKName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKName.KeyPress
        Dim SGcode As String
        SGcode = cboSGrpKala.SelectedValue.ToString
        If e.KeyChar = ChrW(13) Then
            If txtKCode.Text = "" Then
                Z = 2
                Code_Soft = 15
                Pcd = SGcode
                FormName = "TahvilKala"
                FillKalaGaurd()
                FillDataKala()
                Srch.lblName.Text = "لیست کالا"
                Srch.ShowDialog()
            Else
                txtCount.Focus()
            End If
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub Del()
        If Me.txtKCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtKCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره ردیف " & txtRow.Text & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید کالای " & Trim(Me.txtKName.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtKName.Focus()
            Exit Sub
        Else
            Btn = 3
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub Edt()
        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtKCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtKName.Focus()
            Exit Sub
        ElseIf Me.txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCount.Focus()
            Exit Sub
        ElseIf maskDateTav.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ تعویض را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDateTav.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره ردیف " & txtRow.Text & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdTahvilKala"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Scode", cboSGrpKala.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Kcode", txtKCode.Text)
            objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
            objCommand.Parameters.AddWithValue("@DatTaviz", maskDateTav.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@KB", CheckBox1.CheckState)
            objCommand.Parameters.AddWithValue("@shob", Shob)
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

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtKCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtKName.Focus()
            Exit Sub
        ElseIf Me.txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCount.Focus()
            Exit Sub
        ElseIf maskDateTav.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ تعویض را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDateTav.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " شماره ردیف " & txtRow.Text & "  در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Pers_Code, Kala_Code, KB, Shob FROM Bnk.TahvilKala" &
             " WHERE (Pers_Code = " & txtCode.Text & ") AND (Kala_Code = " & txtKCode.Text & ") AND (KB = 0) AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.TahvilKala")
        objDataview = New DataView(objDataset.Tables("Bnk.TahvilKala"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Pers_Code"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " کالای " & Trim(Me.txtKName.Text) & "  برای " & txtName.Text + " " + txtFamily.Text & " در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
            'End If

            'FillDatasetAndDataview()
            'objDataview.Sort = "Row"
            'IntPosition = objDataview.Find(Me.txtRow.Text)
            'If IntPosition <> -1 Then
            '    MsgbOK.Label1.Text = " شماره ردیف " & txtRow.Text & "  در سیستم ثبت شده است  . "
            '    MsgbOK.ShowDialog()
            '    Me.btnEdit.Focus()
            '    Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub TahvilKala_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        fillcboSGrpKala()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub TahvilKala_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub TahvilKala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        maskDate.Text = ConvD.Class1.ConvDate

        txtKCode.SendToBack()
        txtDat.SendToBack()
        txtAlarm.SendToBack()
        txtPriod.SendToBack()
        txtDatOk.SendToBack()
        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub TahvilKala_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 345
    End Sub

    Private Sub txtKName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKName.TextChanged
        If txtKName.Text = "" Then
            txtKCode.Text = ""
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub Ref_Click(sender As Object, e As EventArgs) Handles Ref.Click
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub maskDate_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub maskDate_GotFocus(sender As Object, e As EventArgs) Handles maskDate.GotFocus
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles maskDate.KeyPress
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

    Private Sub txtDscr_TextChanged(sender As Object, e As EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub txtDscr_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub txtCount_TextChanged(sender As Object, e As EventArgs) Handles txtCount.TextChanged

    End Sub

    Private Sub txtCount_GotFocus(sender As Object, e As EventArgs) Handles txtCount.GotFocus
        Me.txtCount.SelectionStart = 0
        Me.txtCount.SelectionLength = Len(Me.txtCount.Text)
    End Sub

    Private Sub txtCount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCount.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.maskDateTav.Focus()
        End If
    End Sub

    Private Sub maskDateTav_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles maskDateTav.MaskInputRejected

    End Sub

    Private Sub maskDateTav_GotFocus(sender As Object, e As EventArgs) Handles maskDateTav.GotFocus
        maskDateTav.SelectionStart = 0
        maskDateTav.SelectionLength = Len(maskDateTav.Text)
    End Sub

    Private Sub maskDateTav_KeyPress(sender As Object, e As KeyPressEventArgs) Handles maskDateTav.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtDscr_GotFocus(sender As Object, e As EventArgs) Handles txtDscr.GotFocus
        Me.txtDscr.SelectionStart = 0
        Me.txtDscr.SelectionLength = Len(Me.txtDscr.Text)
    End Sub

    Private Sub cboSGrpKala_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSGrpKala.SelectedIndexChanged

    End Sub

    Private Sub cboSGrpKala_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboSGrpKala.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDate.Focus()
        End If
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        Label5.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
        If ColumnSrch = 2 Or ColumnSrch = 3 Or ColumnSrch = 4 Or ColumnSrch = 5 Or ColumnSrch = 7 Or ColumnSrch = 8 Or ColumnSrch = 9 Or ColumnSrch = 10 Then
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        Else
            Me.txtSrch.Text = ""
            Label5.Text = "-----"
        End If
    End Sub

    Private Sub txtSrch_TextChanged(sender As Object, e As EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        If txtSrch.Text <> "" Then
            Select Case ColumnSrch
                Case "2"
                    objDataview.RowFilter = "Dat like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "3"
                    objDataview.RowFilter = "Pers_code = '" & Me.txtSrch.Text & "'"
                Case "4"
                    objDataview.RowFilter = "Pers_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "5"
                    objDataview.RowFilter = "Pers_Family like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "7"
                    objDataview.RowFilter = "Kala_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "8"
                    objDataview.RowFilter = "Count = '" & Me.txtSrch.Text & "'"
                Case "9"
                    objDataview.RowFilter = "DatTaviz like '" & "**" & Me.txtSrch.Text & "**" & "'"
                Case "10"
                    objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End Select
        End If
        FillDataGridView()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
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

                If DataGridView2.Visible = False Then
                    With xlsheet
                        .DisplayRightToLeft = True
                        .Range("A1").Value = "ردیف"
                        .Range("A1").Font.Bold = True
                        .Range("B1").Value = "تاریخ"
                        .Range("B1").Font.Bold = True
                        .Range("C1").Value = "کد پرسنلی"
                        .Range("C1").Font.Bold = True
                        .Range("D1").Value = "نام"
                        .Range("D1").Font.Bold = True
                        .Range("E1").Value = "نام خانوادگی"
                        .Range("E1").Font.Bold = True
                        .Range("F1").Value = "نام کالا"
                        .Range("F1").Font.Bold = True
                        .Range("G1").Value = "تعداد"
                        .Range("G1").Font.Bold = True
                        .Range("H1").Value = "تاریخ تعویض"
                        .Range("H1").Font.Bold = True
                        .Range("I1").Value = "شرح"
                        .Range("I1").Font.Bold = True
                        .Range("J1").Value = "وضعیت"
                        .Range("J1").Font.Bold = True

                        ' .Range("A" & i.ToString).AutoFormat()
                        X = Me.DataGridView1.CurrentCell.RowIndex
                        For X = 0 To DataGridView1.RowCount - 1
                            If DataGridView1.Rows(X).Selected = True Then
                                .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                                .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                                .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                                .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                                .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                                .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                                .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                                .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                                .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                                .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                                i += 1
                            End If
                        Next X
                    End With
                ElseIf DataGridView2.Visible = True Then
                    With xlsheet
                        .DisplayRightToLeft = True
                        .Range("A1").Value = "ردیف"
                        .Range("A1").Font.Bold = True
                        .Range("B1").Value = "تاریخ تعویض"
                        .Range("B1").Font.Bold = True
                        .Range("C1").Value = "کد پرسنلی"
                        .Range("C1").Font.Bold = True
                        .Range("D1").Value = "نام"
                        .Range("D1").Font.Bold = True
                        .Range("E1").Value = "نام خانوادگی"
                        .Range("E1").Font.Bold = True
                        .Range("F1").Value = "نام کالا"
                        .Range("F1").Font.Bold = True
                        .Range("G1").Value = "تعداد"
                        .Range("G1").Font.Bold = True

                        ' .Range("A" & i.ToString).AutoFormat()
                        X = Me.DataGridView2.CurrentCell.RowIndex
                        For X = 0 To DataGridView2.RowCount - 1
                            If DataGridView2.Rows(X).Selected = True Then
                                .Range("A" & i.ToString).Value = DataGridView2.Rows(X).Cells(0).Value
                                .Range("B" & i.ToString).Value = DataGridView2.Rows(X).Cells(1).Value
                                .Range("C" & i.ToString).Value = DataGridView2.Rows(X).Cells(2).Value
                                .Range("D" & i.ToString).Value = DataGridView2.Rows(X).Cells(3).Value
                                .Range("E" & i.ToString).Value = DataGridView2.Rows(X).Cells(4).Value
                                .Range("F" & i.ToString).Value = DataGridView2.Rows(X).Cells(6).Value
                                .Range("G" & i.ToString).Value = DataGridView2.Rows(X).Cells(7).Value
                                i += 1
                            End If
                        Next X
                    End With
                End If

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

    Private Sub txtCode_GotFocus(sender As Object, e As EventArgs) Handles txtCode.GotFocus
        Me.txtCode.SelectionStart = 0
        Me.txtCode.SelectionLength = Len(Me.txtCode.Text)
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
                txtKName.Clear()
            End If
            txtFamily.Focus()
        End If
    End Sub

    Private Sub txtFamily_TextChanged(sender As Object, e As EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtCode.Clear()
            txtName.Clear()
        End If
    End Sub

    Private Sub txtFamily_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Z = 1
                Code_Soft = 15
                FormName = "TahvilKala"
                FillPers()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                txtKName.Focus()
            End If
        End If
    End Sub

    Private Sub txtFamily_GotFocus(sender As Object, e As EventArgs) Handles txtFamily.GotFocus
        Me.txtFamily.SelectionStart = 0
        Me.txtFamily.SelectionLength = Len(Me.txtFamily.Text)
    End Sub

    Private Sub InsertDrop()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdTahvilElam"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", Z)
            objCommand.Parameters.AddWithValue("@Pcode", Pcd)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSarResid_Click(sender As Object, e As EventArgs) Handles btnSarResid.Click
        Dim X As Integer
        Dim Row As String
        Dim Pcode As String
        Dim Kcode As String
        Dim Y As String
        Dim M As String
        Dim D As String
        Dim Dat As String
        Dim Y1 As String
        Dim M1 As String
        Dim DatT As String
        Dim Y2 As String
        Dim M2 As String
        Dim D2 As String
        Dim Y3 As String
        Dim D3 As String
        Dim A As String

        txtDat.Text = ConvD.Class1.ConvDate

        For X = 0 To DataGridView1.RowCount - 1
            Row = DataGridView1.Rows(X).Cells(0).Value
            Z = Row
            Pcode = DataGridView1.Rows(X).Cells(3).Value
            Pcd = Pcode
            Kcode = DataGridView1.Rows(X).Cells(6).Value

            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Priod, Alarm FROM Bas.KalaPriod WHERE (Code = " & Kcode & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bas.KalaPriod")
            objDataviewT = New DataView(objDataset.Tables("Bas.KalaPriod"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)

            txtPriod.Text = ""
            txtPriod.DataBindings.Clear()
            txtPriod.DataBindings.Add("Text", objDataviewT, "Priod")
            txtAlarm.Text = ""
            txtAlarm.DataBindings.Clear()
            txtAlarm.DataBindings.Add("Text", objDataviewT, "Alarm")

            Dat = DataGridView1.Rows(X).Cells(2).Value
            Y1 = Mid(Dat, 1, 4)
            M1 = Mid(Dat, 6, 2)
            DatT = DataGridView1.Rows(X).Cells(9).Value
            Y2 = Mid(DatT, 1, 4)
            M2 = Mid(DatT, 6, 2)
            D2 = Mid(DatT, 9, 2)

            Y3 = Val(Y2) - Val(Y1)
            If Y3 = 0 Then
                Y = Y2
                M = Val(M2) - Val(M1)

                If M = Val(txtPriod.Text) Then
                    If Val(D2) <= Val(txtAlarm.Text) Then
                        M = Val(M2) - 1

                        If Val(M) >= 1 And Val(M) <= 6 Then
                            If Val(D2) = Val(txtAlarm.Text) Then
                                D = 31
                            ElseIf Val(D2) < Val(txtAlarm.Text) Then
                                D3 = Val(txtAlarm.Text) - Val(D2)
                                D = 31 - D3
                            End If
                        ElseIf Val(M) >= 7 And Val(M) <= 11 Then
                            If Val(D2) = Val(txtAlarm.Text) Then
                                D = 30
                            ElseIf Val(D2) < Val(txtAlarm.Text) Then
                                D3 = Val(txtAlarm.Text) - Val(D2)
                                D = 30 - D3
                            End If
                        ElseIf Val(M) = 12 Then
                            If Val(D2) = Val(txtAlarm.Text) Then
                                D = 29
                            ElseIf Val(D2) < Val(txtAlarm.Text) Then
                                D3 = Val(txtAlarm.Text) - Val(D2)
                                D = 29 - D3
                            End If
                        End If

                        If Len(M) = 1 Then
                            M = "0" + M
                        End If
                        If Len(D) = 1 Then
                            D = "0" + D
                        End If
                        txtDatOk.Text = Y + "/" + M + "/" + D
                    ElseIf Val(D2) > Val(txtAlarm.Text) Then
                        M = M2
                        D = Val(D2) - Val(txtAlarm.Text)

                        If Len(M) = 1 Then
                            M = "0" + M
                        End If
                        If Len(D) = 1 Then
                            D = "0" + D
                        End If
                        txtDatOk.Text = Y + "/" + M + "/" + D
                    End If
                ElseIf M < Val(txtPriod.Text) Then
                    txtDatOk.Text = ""
                ElseIf M > Val(txtPriod.Text) Then
                    txtDatOk.Text = 0
                End If
            Else
                Y = Y2
                A = 12 - Val(M1)
                M = Val(M2) + Val(A)

                If M = Val(txtPriod.Text) Then
                    If Val(D2) <= Val(txtAlarm.Text) Then
                        M = Val(M2) - 1
                        If M = 0 Then
                            Y = Val(Y) - 1
                            M = 12
                        End If
                        If Val(M) >= 1 And Val(M) <= 6 Then
                            If Val(D2) = Val(txtAlarm.Text) Then
                                D = 31
                            ElseIf Val(D2) < Val(txtAlarm.Text) Then
                                D3 = Val(txtAlarm.Text) - Val(D2)
                                D = 31 - D3
                            End If
                        ElseIf Val(M) >= 7 And Val(M) <= 11 Then
                            If Val(D2) = Val(txtAlarm.Text) Then
                                D = 30
                            ElseIf Val(D2) < Val(txtAlarm.Text) Then
                                D3 = Val(txtAlarm.Text) - Val(D2)
                                D = 30 - D3
                            End If
                        ElseIf Val(M) = 12 Then
                            If Val(D2) = Val(txtAlarm.Text) Then
                                D = 29
                            ElseIf Val(D2) < Val(txtAlarm.Text) Then
                                D3 = Val(txtAlarm.Text) - Val(D2)
                                D = 29 - D3
                            End If
                        End If

                        If Len(M) = 1 Then
                            M = "0" + M
                        End If
                        If Len(D) = 1 Then
                            D = "0" + D
                        End If
                        txtDatOk.Text = Y + "/" + M + "/" + D
                    ElseIf Val(D2) > Val(txtAlarm.Text) Then
                        M = M2
                        D = Val(D2) - Val(txtAlarm.Text)

                        If Len(M) = 1 Then
                            M = "0" + M
                        End If
                        If Len(D) = 1 Then
                            D = "0" + D
                        End If
                        txtDatOk.Text = Y + "/" + M + "/" + D
                    End If
                ElseIf M < Val(txtPriod.Text) Then
                    txtDatOk.Text = txtDat.Text
                ElseIf M > Val(txtPriod.Text) Then
                    txtDatOk.Text = ""
                End If
            End If

            If txtDatOk.Text <> "" Then
                If txtDatOk.Text <= txtDat.Text Then
                    Btn = 1
                    InsertDrop()
                End If
            End If
        Next

        Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT Row, Dat, Pers_Code, pers_name, pers_family, Kala_Code, kala_Name, Count FROM Bnk.TahvilElam ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bnk.TahvilElam")
        objDataview = New DataView(objDataset.Tables("Bnk.TahvilElam"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).HeaderText = "ردیف"
        Me.DataGridView2.Columns(1).HeaderText = "تاریخ تعویض"
        Me.DataGridView2.Columns(2).HeaderText = "کد پرسنلی"
        Me.DataGridView2.Columns(3).HeaderText = "نام"
        Me.DataGridView2.Columns(4).HeaderText = "نام خانوادگی"
        Me.DataGridView2.Columns(5).Visible = False ' Kcode
        Me.DataGridView2.Columns(6).HeaderText = "نام کالا"
        Me.DataGridView2.Columns(7).HeaderText = "تعداد"

        Me.DataGridView2.Columns(0).Width = 60
        Me.DataGridView2.Columns(1).Width = 70
        Me.DataGridView2.Columns(2).Width = 60
        Me.DataGridView2.Columns(3).Width = 80
        Me.DataGridView2.Columns(4).Width = 120
        Me.DataGridView2.Columns(6).Width = 250
        Me.DataGridView2.Columns(7).Width = 60

        If DataGridView2.RowCount > 0 Then
            If DataGridView2.Visible = False Then
                DataGridView2.Visible = True
            Else
                DataGridView2.Visible = False
                Btn = 2
                InsertDrop()
            End If
        Else
            Btn = 2
            InsertDrop()
            MsgbOK.Label1.Text = " سررسید تحویل کالا برای هیچ یک از پرسنل فرا نرسیده است . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        End If
    End Sub
End Class