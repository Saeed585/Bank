Public Class UsedKala
    Dim ColumnSrch As Integer
    Public Sub FillRow()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.UsedKala WHERE (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.UsedKala")
        objDataview = New DataView(objDataset.Tables("Bnk.UsedKala"))
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

    Private Sub FillCboArea()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Area_code, Area_name FROM Bas.Area ORDER BY Area_code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Area")
        Me.cboArea.DataSource = objDataset.Tables("Bas.Area")
        Me.cboArea.DisplayMember = "Area_name"
        Me.cboArea.ValueMember = "Area_code"
    End Sub

    Public Sub FillRowSoorat()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Soorat) AS Expr1 FROM Bnk.UsedKala", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.UsedKala")
        objDataview = New DataView(objDataset.Tables("Bnk.UsedKala"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")
        If Label6.Text <> "" Then
            txtSooratNo.Text = Val(Label6.Text) + 1
        ElseIf Label6.Text = "" Then
            txtSooratNo.Text = 1
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If RB1.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_UsedKala WHERE (Shob =" & Shob & ") AND (Knd = 1) AND (Soorat = 0) ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_UsedKala")
            objDataview = New DataView(objDataset.Tables("Bnk.View_UsedKala"))
            ' objDataview.Sort = "Row"
            objConnection.Close()
        ElseIf RB2.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_UsedKala WHERE (Shob =" & Shob & ") AND (Knd = 2) AND (Soorat = 0) ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_UsedKala")
            objDataview = New DataView(objDataset.Tables("Bnk.View_UsedKala"))
            ' objDataview.Sort = "Row"
            objConnection.Close()
        ElseIf RB3.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_UsedKala WHERE (Shob =" & Shob & ") AND (Soorat <> 0) ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_UsedKala")
            objDataview = New DataView(objDataset.Tables("Bnk.View_UsedKala"))
            ' objDataview.Sort = "Row"
            objConnection.Close()
        End If

        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "کد کالا"
        Me.DataGridView1.Columns(3).HeaderText = "نام کالا"
        Me.DataGridView1.Columns(4).HeaderText = " تعداد ارسال شده"
        Me.DataGridView1.Columns(5).HeaderText = "تاریخ ارسال شده"
        Me.DataGridView1.Columns(6).HeaderText = " تعداد برگشتی"
        Me.DataGridView1.Columns(7).HeaderText = "تاریخ برگشتی"
        Me.DataGridView1.Columns(8).HeaderText = "شرح"
        Me.DataGridView1.Columns(9).Visible = False ' Knd
        Me.DataGridView1.Columns(10).HeaderText = "وضعیت کالا"
        Me.DataGridView1.Columns(11).Visible = False ' Shob
        Me.DataGridView1.Columns(12).HeaderText = "شماره صورتجلسه"
        Me.DataGridView1.Columns(13).HeaderText = "تاریخ صورتجلسه"
        Me.DataGridView1.Columns(14).Visible = False ' Area
        Me.DataGridView1.Columns(15).HeaderText = "محل ارسال"

        Me.DataGridView1.Columns(0).Width = 70
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 100
        Me.DataGridView1.Columns(3).Width = 250
        Me.DataGridView1.Columns(4).Width = 70
        Me.DataGridView1.Columns(5).Width = 70
        Me.DataGridView1.Columns(6).Width = 70
        Me.DataGridView1.Columns(7).Width = 70
        Me.DataGridView1.Columns(8).Width = 400
        Me.DataGridView1.Columns(10).Width = 120
        Me.DataGridView1.Columns(15).Width = 120

        For X = 0 To DataGridView1.RowCount - 1
            If Me.DataGridView1.Rows(X).Cells(9).Value = 1 Then
                Me.DataGridView1.Rows(X).Cells(10).Value = "استفاده شده"
            ElseIf Me.DataGridView1.Rows(X).Cells(9).Value = 2 Then
                Me.DataGridView1.Rows(X).Cells(10).Value = "غیر قابل استفاده"
            End If

            If RB3.Checked = True Then
                Me.DataGridView1.Columns(12).Visible = True
                Me.DataGridView1.Columns(13).Visible = True
            Else
                Me.DataGridView1.Columns(12).Visible = False
                Me.DataGridView1.Columns(13).Visible = False
            End If
        Next
    End Sub

    Private Sub Clear()
        Me.txtName.Clear()
        Me.txtCountout.Text = 0
        Me.maskDateout.Text = "1   /  /"
        Me.txtCountin.Text = 0
        Me.maskDatein.Text = "1   /  /"
        Me.txtDscr.Text = ""
        Me.txtName.Focus()
    End Sub

    Public Sub SrchRowKalaMoj()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, CountOk FROM Bnk.Mojoodi WHERE (Shob = " & Shob & ") AND (Kala_code = " & txtCode.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bas.Mojoodi"))
        IntPosition = objDataview.Count
        objDataview.Sort = "Row"
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.maskDate.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(2).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(3).Value
        SrchRowKalaMoj()
        Me.txtRowMoj.Text = 0
        Me.txtRowMoj.DataBindings.Clear()
        Me.txtRowMoj.DataBindings.Add("Text", objDataview, "Row")
        Me.txtCount.Text = 0
        Me.txtCount.DataBindings.Clear()
        Me.txtCount.DataBindings.Add("Text", objDataview, "CountOk")
        Me.txtCountout.Text = Me.DataGridView1.Rows(X).Cells(4).Value
        Me.maskDateout.Text = Me.DataGridView1.Rows(X).Cells(5).Value
        Me.txtCountin.Text = Me.DataGridView1.Rows(X).Cells(6).Value
        Me.maskDatein.Text = Me.DataGridView1.Rows(X).Cells(7).Value
        Me.txtCountOrg.Text = Val(Me.txtCountout.Text) - Val(Me.txtCountin.Text)
        Me.txtDscr.Text = Me.DataGridView1.Rows(X).Cells(8).Value
        Me.cboStat.SelectedIndex = Me.DataGridView1.Rows(X).Cells(9).Value - 1
        Me.cboArea.SelectedValue = Me.DataGridView1.Rows(X).Cells(14).Value
    End Sub


    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text = "" Then
                Code_Soft = 15
                FormName = "UsedKala"
                FillKalaMoj()
                FillDataKala()
                Srch.lblName.Text = "نام کالا"
                Srch.ShowDialog()
            Else
                txtCountout.Focus()
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

        Z = Val(txtCountOrg.Text) + Val(txtCount.Text)

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(Me.txtRow.Text) & "  در سیستم ثبت نشده است  . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Tag FROM Bnk.UsedKala WHERE (Row = " & txtRow.Text & ") AND (Kala_code = " & txtCode.Text & ") AND (Shob = " & Shob & ") AND (Tag = 1)", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.UsedKala")
        objDataview = New DataView(objDataset.Tables("Bnk.UsedKala"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Tag"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " ردیف فوق جزو کالاهای انتقالی بوده و قابل حذف نمی باشد . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه " & Trim(Me.txtRow.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtName.Focus()
            Exit Sub
        Else
            '--------LogUsedKala
            logStat = 3
            SaveLogUsedKala()
            '-----------------
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
        ElseIf Me.txtCountout.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCountout.Focus()
            Exit Sub
        ElseIf Me.maskDate.Text = "1   /  /" Or Me.maskDateout.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            If Me.maskDate.Text = "1   /  /" Then
                Me.maskDate.Focus()
            ElseIf Me.maskDateout.Text = "1   /  /" Then
                Me.maskDateout.Focus()
            End If
            Exit Sub
        End If

        If Val(txtCountOrg.Text) = (Val(txtCountout.Text) - Val(txtCountin.Text)) Then
            Z = Val(txtCount.Text)
        ElseIf Val(txtCountOrg.Text) > (Val(txtCountout.Text) - Val(txtCountin.Text)) Then
            Z = Val(txtCount.Text) + (Val(txtCountOrg.Text) - (Val(txtCountout.Text) - Val(txtCountin.Text)))
        ElseIf Val(txtCountOrg.Text) < (Val(txtCountout.Text) - Val(txtCountin.Text)) Then
            Z = Val(txtCount.Text) - ((Val(txtCountout.Text) - Val(txtCountin.Text)) - Val(txtCountOrg.Text))
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "Row"
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(Me.txtRow.Text) & "  در سیستم ثبت نشده است  . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()
            '--------LogUsedKala
            logStat = 2
            SaveLogUsedKala()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub SaveLogUsedKala()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " INSERT INTO [Log].UsedKalaLog (Row, Dat, Kala_code, Knd, Area, Countout, Datout, Countin, Datin, Dscr, Soorat, DatSoorat, Shob, Sal)" &
           " SELECT Row, Dat, Kala_code, Knd, Area, Countout, Datout, Countin, Datin, Dscr, Soorat, DatSoorat, Shob, Sal FROM Bnk.UsedKala" &
           " WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")"

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
        objcommand.CommandText =
            " UPDATE [Log].UsedKalaLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" &
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
        Dim Area As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Area = cboArea.SelectedValue.ToString
        Knd = cboStat.SelectedIndex + 1

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdUsedKala"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@RowMoj", txtRowMoj.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Code", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Knd", Knd)
            objCommand.Parameters.AddWithValue("@Area", Area)
            objCommand.Parameters.AddWithValue("@CountOk", Z)
            objCommand.Parameters.AddWithValue("@Countout", txtCountout.Text)
            objCommand.Parameters.AddWithValue("@Datout", maskDateout.Text)
            objCommand.Parameters.AddWithValue("@Countin", txtCountin.Text)
            objCommand.Parameters.AddWithValue("@Datin", maskDatein.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Soorat", 0)
            objCommand.Parameters.AddWithValue("@DatSoorat", maskDateSoorat.Text)
            objCommand.Parameters.AddWithValue("@Tag", 0)
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
        ElseIf Me.txtCountout.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCountout.Focus()
            Exit Sub
        ElseIf Me.maskDate.Text = "1   /  /" Or Me.maskDateout.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            If Me.maskDate.Text = "1   /  /" Then
                Me.maskDate.Focus()
            ElseIf Me.maskDateout.Text = "1   /  /" Then
                Me.maskDateout.Focus()
            End If
            Exit Sub
        End If

        Z = Val(txtCount.Text) - (Val(txtCountout.Text) - Val(txtCountin.Text))

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
            '--------LogUsedKala
            logStat = 1
            SaveLogUsedKala()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        Label7.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
        If ColumnSrch = 3 Or ColumnSrch = 6 Then
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

    Private Sub UsedKala_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillCboArea()
        FillRowSoorat()
        FillDatasetAndDataview()
        FillDataGridView()
        FormName = "UsedKala"
    End Sub

    Private Sub UsedKala_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf maskDateout.Focused = True Then
            maskDateout.Text = ConvD.Class1.ConvDate
        ElseIf maskDatein.Focused = True Then
            maskDatein.Text = ConvD.Class1.ConvDate
        ElseIf maskDateSoorat.Focused = True Then
            maskDateSoorat.Text = ConvD.Class1.ConvDate
        End If
    End Sub
    Private Sub UsedKala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        cboStat.SelectedIndex = 0
        maskDate.Text = ConvD.Class1.ConvDate
        txtCode.SendToBack()
        txtCount2.SendToBack()
        txtCountOrg.SendToBack()
        txtRowMoj.SendToBack()
        txtSoorat.SendToBack()

        Me.KeyPreview = True
        FormName = "UsedKala"
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub UsedKala_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 315
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

    Private Sub txtCountout_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountout.GotFocus
        txtCountout.SelectionStart = 0
        txtCountout.SelectionLength = Len(txtCountout.Text)
    End Sub

    Private Sub txtCountout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCountout.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDateout.Focus()
        End If
    End Sub

    Private Sub txtCountout_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountout.LostFocus
        If Val(txtCountout.Text) > Val(txtCount.Text) Then
            MsgbOK.Label1.Text = " تعداد وارد شده بیشتر از موجودی است . "
            MsgbOK.ShowDialog()
            Me.txtCountout.Text = 0
            Me.maskDateout.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtCountout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountout.TextChanged
        If txtCountout.Text = "" Then
            txtCountout.Text = 0
        End If
    End Sub

    Private Sub maskDateout_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateout.GotFocus
        maskDateout.SelectionStart = 0
        maskDateout.SelectionLength = Len(maskDateout.Text)
    End Sub

    Private Sub maskDateout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub maskDateout_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateout.MaskInputRejected

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

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillCboArea()
        FillRowSoorat()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
        FormName = "UsedKala"
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
                    .Range("E1").Value = "تعداد"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "تاریخ وضعیت"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "شرح"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "وضعیت کالا"
                    .Range("H1").Font.Bold = True
                    If RB3.Checked = True Then
                        .Range("I1").Value = "شماره صورتجلسه"
                        .Range("I1").Font.Bold = True
                        .Range("J1").Value = "تاریخ صورتجلسه"
                        .Range("J1").Font.Bold = True
                    End If

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
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            If RB3.Checked = True Then
                                .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                                .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            End If
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
            Case "6"
                objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End Select
        FillDataGridView()
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
                txtName.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub btnSoorat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSoorat.Click
        Dim X As Integer
        Dim Rw As String
        Dim Dat As String

        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Selected = True Then
                Rw = DataGridView1.Rows(X).Cells(0).Value
                Dat = DataGridView1.Rows(X).Cells(1).Value

                Dim objDataAdapter As New SqlDataAdapter _
                    (" SELECT Soorat FROM Bnk.UsedKala WHERE (Row = " & Rw & ") AND (Dat = N'" & Dat & "')", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "Bnk.UsedKala")
                objDataview = New DataView(objDataset.Tables("Bnk.UsedKala"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

                txtSoorat.DataBindings.Clear()
                txtSoorat.DataBindings.Add("Text", objDataview, "Soorat")
                If txtSoorat.Text = "" Then
                    txtSoorat.Text = 0
                End If

                If txtSoorat.Text = 0 Then
                    objCommand.Connection = objConnection
                    objCommand.CommandText =
                       " UPDATE Bnk.UsedKala SET Soorat = " & txtSooratNo.Text & ", DatSoorat = N'" & maskDateSoorat.Text & "' WHERE (Row = " & Rw & ") AND (Dat = N'" & Dat & "')"
                    objCommand.Parameters.Clear()
                    objCommand.CommandType = CommandType.Text

                    objConnection.Open()
                    Try
                        objCommand.ExecuteNonQuery()
                    Catch SqlExceptionErr As SqlException
                        MessageBox.Show(SqlExceptionErr.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()
                End If
            End If
        Next

        Panel2.Visible = False
        Ref.PerformClick()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If Panel2.Visible = False Then
            Panel2.Visible = True
            maskDateSoorat.Text = ConvD.Class1.ConvDate
            maskDateSoorat.Focus()
        Else
            Panel2.Visible = False
            maskDateSoorat.Text = "1   /  /"
        End If
    End Sub

    Private Sub btnNotOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotOk.Click
        Dim X As Integer
        Dim Rw As String
        Dim Dat As String
        Dim Soorat As String

        MsgbYN.Label1.Text = "  آیا از برگشت صورتجلسه اطمینان دارید  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            txtName.Focus()
            Exit Sub
        Else
            For X = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(X).Selected = True Then
                    Rw = DataGridView1.Rows(X).Cells(0).Value
                    Dat = DataGridView1.Rows(X).Cells(1).Value
                    Soorat = DataGridView1.Rows(X).Cells(10).Value

                    objCommand.Connection = objConnection
                    objCommand.CommandText =
                       " UPDATE Bnk.UsedKala SET Soorat = 0, DatSoorat = N'1   /  /' WHERE (Row = " & Rw & ") AND (Dat = N'" & Dat & "') AND (Soorat = " & Soorat & ")"
                    objCommand.Parameters.Clear()
                    objCommand.CommandType = CommandType.Text

                    objConnection.Open()
                    Try
                        objCommand.ExecuteNonQuery()
                    Catch SqlExceptionErr As SqlException
                        MessageBox.Show(SqlExceptionErr.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()
                End If
            Next
        End If

        Ref.PerformClick()
    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim X As Integer
        Dim SNo As String

        If DataGridView1.RowCount > 0 Then
            X = DataGridView1.CurrentCell.RowIndex
            SNo = DataGridView1.Rows(X).Cells(10).Value

            Try
                Me.DirectoryEntry1.Path = My.Application.Info.DirectoryPath & "\BankDoc\SooratJalaseh\" + SNo ' جستجوی مسیر
                'System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path + "\" + FileFormat) ' اجرای فایل
                System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path) ' باز کردن مسیر
            Catch ex As Exception
                MsgbOK.Label1.Text = " مسیر  " & Me.DirectoryEntry1.Path & " پیدا نشد ."
                MsgbOK.ShowDialog()
            End Try
        End If
    End Sub

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        Ref.PerformClick()
        btnFile.Enabled = True
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB1.Click
        Ref.PerformClick()
        btnFile.Enabled = False
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        Ref.PerformClick()
        btnFile.Enabled = False
    End Sub

    Private Sub txtCountin_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountin.GotFocus
        txtCountout.SelectionStart = 0
        txtCountout.SelectionLength = Len(txtCountout.Text)
    End Sub

    Private Sub txtCountin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCountin.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDatein.Focus()
        End If
    End Sub

    Private Sub txtCountin_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountin.LostFocus
        If Val(txtCountin.Text) > Val(txtCount.Text) Then
            MsgbOK.Label1.Text = " تعداد وارد شده بیشتر از موجودی است . "
            MsgbOK.ShowDialog()
            Me.txtCountin.Text = 0
            Me.maskDatein.Focus()
            Exit Sub
        ElseIf Val(txtCountin.Text) > Val(txtCountout.Text) Then
            MsgbOK.Label1.Text = " تعداد وارد شده بیشتر از تعداد ارسال شده است . "
            MsgbOK.ShowDialog()
            Me.txtCountin.Text = 0
            Me.maskDatein.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtCountin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountin.TextChanged
        If txtCountin.Text = "" Then
            txtCountin.Text = 0
        End If
    End Sub

    Private Sub maskDatein_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDatein.GotFocus
        maskDatein.SelectionStart = 0
        maskDatein.SelectionLength = Len(maskDatein.Text)
    End Sub

    Private Sub maskDatein_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDatein.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub
End Class