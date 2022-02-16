Public Class KalaOut
    Dim ColumnSrch As Integer
    Public Sub FillPage()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Page) AS Expr1 FROM Bnk.KalaOut WHERE (Shob =" & Shob & ") AND (Sal =" & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KalaOut")
        objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            txtPage.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtPage.Text = 1
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub

    Public Sub FillRow()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.KalaOut WHERE (Page =" & txtPage.Text & ") AND (Shob =" & Shob & ") AND (Sal =" & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KalaOut")
        objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
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

    Private Sub FillCboArea()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Area_Code, Area_Name FROM bas.Area ORDER BY Area_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Area")
        Me.cboArea.DataSource = objDataset.Tables("bas.Area")
        Me.cboArea.DisplayMember = "Area_Name"
        Me.cboArea.ValueMember = "Area_Code"
    End Sub

    Private Sub FillcboKalaV()
        Dim objDataAdapter As New SqlDataAdapter _
            ("SELECT V_Code, V_Name FROM bas.V_Kala", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.V_Kala")
        Me.cboKalaV.DataSource = objDataset.Tables("bas.V_Kala")
        Me.cboKalaV.DisplayMember = "V_Name"
        Me.cboKalaV.ValueMember = "V_Code"
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim Da As New SqlDataAdapter
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If RB1.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_KalaOut WHERE (Page =" & txtPage.Text & ") AND (Shob =" & Shob & ") AND (Sal =" & Sal & ") ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_KalaOut")
            objDataview = New DataView(objDataset.Tables("Bnk.View_KalaOut"))
            objConnection.Close()
        ElseIf RB2.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_KalaOut WHERE (Shob =" & Shob & ") AND (Sal =" & Sal & ") ORDER BY Page DESC, Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_KalaOut")
            objDataview = New DataView(objDataset.Tables("Bnk.View_KalaOut"))
            objConnection.Close()
        End If

        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "شماره ردیف"
        Me.DataGridView1.Columns(2).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(3).HeaderText = "نام"
        Me.DataGridView1.Columns(4).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(5).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(6).HeaderText = "شرح کالا"
        Me.DataGridView1.Columns(7).Visible = False ' Area_code
        Me.DataGridView1.Columns(8).HeaderText = "محل ارسال"
        Me.DataGridView1.Columns(9).Visible = False ' V_code
        Me.DataGridView1.Columns(10).HeaderText = "واحد کالا"
        Me.DataGridView1.Columns(11).HeaderText = "تعداد"
        Me.DataGridView1.Columns(12).HeaderText = "توضیحات"
        Me.DataGridView1.Columns(13).Visible = False ' Ebtal
        Me.DataGridView1.Columns(14).HeaderText = "وضعیت"
        Me.DataGridView1.Columns(15).Visible = False ' Shob
        Me.DataGridView1.Columns(16).Visible = False ' Sal
        Me.DataGridView1.Columns(17).Visible = False ' ChkMoj
        Me.DataGridView1.Columns(18).Visible = False ' RowMoj
        Me.DataGridView1.Columns(19).Visible = False ' Kala_code
        Me.DataGridView1.Columns(20).HeaderText = "رسید"
        Me.DataGridView1.Columns(21).Visible = False ' RowUseK

        Me.DataGridView1.Columns(0).Width = 60
        Me.DataGridView1.Columns(1).Width = 60
        Me.DataGridView1.Columns(2).Width = 60
        Me.DataGridView1.Columns(3).Width = 80
        Me.DataGridView1.Columns(4).Width = 120
        Me.DataGridView1.Columns(5).Width = 70
        Me.DataGridView1.Columns(6).Width = 400
        Me.DataGridView1.Columns(7).Width = 150
        Me.DataGridView1.Columns(10).Width = 60
        Me.DataGridView1.Columns(11).Width = 50
        Me.DataGridView1.Columns(12).Width = 400

        For X = 0 To DataGridView1.RowCount - 1
            If Me.DataGridView1.Rows(X).Cells(13).Value = 1 Then
                Me.DataGridView1.Rows(X).Cells(14).Value = "ابطال شده"
            End If
            If Me.DataGridView1.Rows(X).Cells(20).Value >= 1 Then
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.GreenYellow
            End If
        Next
    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        txtPage.Text = DataGridView1.Rows(X).Cells(0).Value
        txtRow.Text = DataGridView1.Rows(X).Cells(1).Value
        txtCode.Text = DataGridView1.Rows(X).Cells(2).Value
        txtFamily.Text = DataGridView1.Rows(X).Cells(3).Value
        txtName.Text = DataGridView1.Rows(X).Cells(4).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(5).Value
        txtKala.Text = DataGridView1.Rows(X).Cells(6).Value
        cboArea.SelectedValue = DataGridView1.Rows(X).Cells(7).Value
        cboKalaV.SelectedValue = DataGridView1.Rows(X).Cells(9).Value
        txtCount.Text = DataGridView1.Rows(X).Cells(11).Value
        txtCount2.Text = txtCount.Text
        txtDscr.Text = DataGridView1.Rows(X).Cells(12).Value
        CheckBox1.CheckState = DataGridView1.Rows(X).Cells(17).Value
        txtRowMoj.Text = DataGridView1.Rows(X).Cells(18).Value
        txtKcode.Text = DataGridView1.Rows(X).Cells(19).Value
        txtRowUseK.Text = DataGridView1.Rows(X).Cells(21).Value

        Dim objdataadapter As New SqlDataAdapter _
           (" SELECT Bnk.ViewResid.PicCode1 FROM Bnk.KalaOut INNER JOIN Bnk.ViewResid ON Bnk.KalaOut.Page = Bnk.ViewResid.Page AND Bnk.KalaOut.Sal = Bnk.ViewResid.Sal AND Bnk.KalaOut.Shob = Bnk.ViewResid.Shob" &
            " WHERE (Bnk.ViewResid.Page = " & txtPage.Text & ") And (Bnk.ViewResid.Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Bnk.KalaOut")
        objDataviewT = New DataView(objDataset.Tables("Bnk.KalaOut"))

        txtCountR.Text = objDataviewT.Count

        Dim objDataAdapter1 As New SqlDataAdapter _
         (" Select CountOk FROM Bnk.Mojoodi WHERE (Kala_code = " & txtKcode.Text & ") And (Row = " & txtRowMoj.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        IntPosition = objDataview.Count

        txtCountOrg.Text = ""
        txtCountOrg.DataBindings.Clear()
        txtCountOrg.DataBindings.Add("text", objDataview, "CountOk")
        txt2.Text = DataGridView1.Rows(X).Cells(11).Value
    End Sub
    Private Sub Clear()
        txtFamily.Text = ""
        txtKala.Text = ""
        txtCount.Text = 0
        txtCount2.Text = ""
        txtDscr.Text = ""
        txtKcode.Text = ""
        txtRowMoj.Text = ""
        txtCountOrg.Text = ""
        txt1.Text = ""
        txt2.Text = ""
        CheckBox1.Checked = False
        txtCode.Focus()
    End Sub

    Private Sub SaveLogKalaOut()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].KalaOutLog (Page, Row, Dat, Pcode, Kala, Area_code, V_code, Count, Dscr, Shob, Sal)" & _
           " Select Page, Row, Dat, Pcode, Kala, Area_code, V_code, Count, Dscr, Shob, Sal FROM Bnk.KalaOut WHERE (Page = " & txtPage.Text & ") And (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ")"

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
            " UPDATE [Log].KalaOutLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
            " WHERE (Page = " & txtPage.Text & ") AND (Row = " & txtRow.Text & ") AND (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ") AND (tim_stat IS NULL)"
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

    Private Sub KalaOut_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim X As Integer
        FormChk()
        For X = 0 To DataGridView2.RowCount - 1
            txt1.Text = DataGridView2.Rows(X).Cells(1).Value
            If txt1.Text = "Area" Then
                btnArea.Enabled = True
            End If
        Next X
        txt1.Text = ""
        FillPage()
        FillRow()
        FillCboArea()
        FillcboKalaV()
        'FillDataSetAndDataView()
        'FillDataGridView()
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
        objCommand.CommandText = _
           " UPDATE Bnk.KalaOut SET Chk = @Chk WHERE (Page = " & Rw & ")"
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

    Private Sub KalaOut_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.DropTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 43)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub KalaOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            FillPage()
            FillRow()
            Clear()
        ElseIf e.KeyCode = Keys.F5 Then
            HighLight()
        ElseIf e.KeyCode = Keys.Space Then
            Spc()
        End If
    End Sub

    Private Sub Spc()
        If maskDate.Focused = True Then
            maskDate.Text = ConvD.Class1.ConvDate
        End If
    End Sub
    Private Sub KalaOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        maskDate.Text = ConvD.Class1.ConvDate
        txt1.SendToBack()
        txt2.SendToBack()
        txtKcode.SendToBack()
        txtRowMoj.SendToBack()
        txtCountOrg.SendToBack()
        txtRowUseK.SendToBack()
        txtCount2.SendToBack()
        DataGridView2.SendToBack()
        Me.KeyPreview = True
    End Sub

    Private Sub KalaOut_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 345
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If CheckBox1.Checked = False Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdkalaOut"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Page", txtPage.Text)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@RowMoj", 0)
                objCommand.Parameters.AddWithValue("@RowUseK", 0)
                objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
                objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
                objCommand.Parameters.AddWithValue("@KCode", 0)
                objCommand.Parameters.AddWithValue("@Kala", txtKala.Text)
                objCommand.Parameters.AddWithValue("@Area", cboArea.SelectedValue.ToString)
                objCommand.Parameters.AddWithValue("@Vcode", cboKalaV.SelectedValue.ToString)
                objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
                objCommand.Parameters.AddWithValue("@Chk", 0)
                objCommand.Parameters.AddWithValue("@Ebtal", 0)
                objCommand.Parameters.AddWithValue("@ChkMoj", CheckBox1.CheckState)
                objCommand.Parameters.AddWithValue("@shob", Shob)
                objCommand.Parameters.AddWithValue("@Sal", Sal)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf CheckBox1.Checked = True Then
            If Btn = 1 Then
                Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.UsedKala WHERE (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "Bnk.UsedKala")
                objDataview = New DataView(objDataset.Tables("Bnk.UsedKala"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Expr1"

                Label3.DataBindings.Clear()
                Label3.DataBindings.Add("Text", objDataview, "Expr1")
                If Label3.Text <> "" Then
                    txtRowUseK.Text = Val(Label3.Text) + 1
                ElseIf Label3.Text = "" Then
                    txtRowUseK.Text = Sal & "00001"
                End If
                Label3.DataBindings.Clear()
                Label3.Text = ""
            End If

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdkalaOut"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Page", txtPage.Text)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@RowMoj", txtRowMoj.Text)
                objCommand.Parameters.AddWithValue("@RowUseK", txtRowUseK.Text)
                objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
                objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
                objCommand.Parameters.AddWithValue("@KCode", txtKcode.Text)
                objCommand.Parameters.AddWithValue("@Kala", txtKala.Text)
                objCommand.Parameters.AddWithValue("@Area", cboArea.SelectedValue.ToString)
                objCommand.Parameters.AddWithValue("@Vcode", cboKalaV.SelectedValue.ToString)
                objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
                objCommand.Parameters.AddWithValue("@Chk", 0)
                objCommand.Parameters.AddWithValue("@Ebtal", 0)
                objCommand.Parameters.AddWithValue("@ChkMoj", CheckBox1.CheckState)
                objCommand.Parameters.AddWithValue("@shob", Shob)
                objCommand.Parameters.AddWithValue("@Sal", Sal)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdUsedKala"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRowUseK.Text)
                objCommand.Parameters.AddWithValue("@RowMoj", txtRowMoj.Text)
                objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
                objCommand.Parameters.AddWithValue("@Code", txtKcode.Text)
                objCommand.Parameters.AddWithValue("@Knd", 1)
                objCommand.Parameters.AddWithValue("@Area", cboArea.SelectedValue.ToString)
                objCommand.Parameters.AddWithValue("@CountOk", txt1.Text)
                objCommand.Parameters.AddWithValue("@Countout", txtCount.Text)
                objCommand.Parameters.AddWithValue("@Datout", maskDate.Text)
                objCommand.Parameters.AddWithValue("@Countin", 0)
                objCommand.Parameters.AddWithValue("@Datin", "1   /  /")
                objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
                objCommand.Parameters.AddWithValue("@Soorat", 0)
                objCommand.Parameters.AddWithValue("@DatSoorat", "1   /  /")
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
        End If
    End Sub

    Private Sub Sav()
        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        ElseIf txtKala.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            txtKala.Focus()
            Exit Sub
        ElseIf txtCount.Text = 0 Or txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCount.Focus()
            Exit Sub
        End If

        If CheckBox1.Checked = True Then
            If txtKcode.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام کالا را از لیست انتخاب کنید . "
                MsgbOK.ShowDialog()
                txtKala.Focus()
                Exit Sub
            ElseIf Val(txtCount.Text) > Val(txtCountOrg.Text) Then
                MsgbOK.Label1.Text = " تعداد خروجی بیشتر از تعداد موجودی است . "
                MsgbOK.ShowDialog()
                txtCount.Focus()
                Exit Sub
            End If
        End If

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        txt1.Text = Val(txtCountOrg.Text) - Val(txtCount.Text)

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, Shob FROM Bnk.KalaOut WHERE (Page = " & txtPage.Text & ") AND (Row = " & txtRow.Text & ") AND (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KalaOut")
        objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " شماره ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            ' FillPage()
            FillRow()
            Btn = 1
            InsertUpdate()
            '--------KalaOutLog
            logStat = 1
            SaveLogKalaOut()

            RB1.Checked = True
            FillRow()
            FillDataSetAndDataView()
            FillDataGridView()
            txtKala.Text = ""
            txtCount.Text = 0
            txtDscr.Text = ""
            CheckBox1.Checked = False
            txtKala.Focus()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        ElseIf txtKala.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            txtKala.Focus()
            Exit Sub
        ElseIf txtCount.Text = 0 Or txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCount.Focus()
            Exit Sub
        End If

        If CheckBox1.Checked = True Then
            If txtKcode.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام کالا را از لیست انتخاب کنید . "
                MsgbOK.ShowDialog()
                txtKala.Focus()
                Exit Sub
            ElseIf Val(txtCount2.Text) <> Val(txtCount.Text) Then
                If Val(txtCount.Text) > Val(txtCountOrg.Text) Then
                    MsgbOK.Label1.Text = " تعداد خروجی بیشتر از تعداد موجودی است . "
                    MsgbOK.ShowDialog()
                    txtCount.Focus()
                    Exit Sub
                End If
            End If
        End If

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        txt1.Text = Val(txtCountOrg.Text) + Val(txt2.Text)
        txt1.Text = Val(txt1.Text) - Val(txtCount.Text)

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, Shob FROM Bnk.KalaOut WHERE (Page = " & txtPage.Text & ") AND (Row = " & txtRow.Text & ") AND (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KalaOut")
        objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()
            '--------KalaOutLog
            logStat = 2
            SaveLogKalaOut()

            FillRow()
            FillDataSetAndDataView()
            FillDataGridView()
            txtKala.Text = ""
            txtCount.Text = 0
            txtDscr.Text = ""
            CheckBox1.Checked = False
            txtKala.Focus()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, Shob FROM Bnk.KalaOut WHERE (Page = " & txtPage.Text & ") AND (Row = " & txtRow.Text & ") AND (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KalaOut")
        objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                ' maskCode.Focus()
                Exit Sub
            Else
                txt1.Text = Val(txtCountOrg.Text) + Val(txtCount.Text)

                '--------KalaOutLog
                logStat = 3
                SaveLogKalaOut()
                '--------------------
                Btn = 3
                InsertUpdate()

                Dim objDataAdapter1 As New SqlDataAdapter _
                    (" SELECT Page, Dat, Shob FROM Bnk.KalaOut WHERE (Page = " & txtPage.Text & ") AND (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter1.Fill(objDataset, "Bnk.KalaOut")
                objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Page"

                If objDataview.Count = 0 Then
                    FillPage()
                End If

                FillRow()
                FillDataSetAndDataView()
                FillDataGridView()
                txtKala.Text = ""
                txtCount.Text = 0
                txtDscr.Text = ""
                CheckBox1.Checked = False
                txtKala.Focus()
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
        If ColumnSrch = 2 Or ColumnSrch = 3 Or ColumnSrch = 4 Or ColumnSrch = 5 Or ColumnSrch = 6 Or ColumnSrch = 8 Or ColumnSrch = 12 Then
            Label1.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        Else
            Me.txtSrch.Text = ""
            Label1.Text = "-----"
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

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKala.GotFocus
        txtKala.SelectionStart = 0
        txtKala.SelectionLength = Len(txtKala.Text)
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Z = 1
                Code_Soft = 15
                FormName = "KalaOut"
                FillPers()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                CheckBox1.Focus()
            End If
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtCode.Clear()
            txtName.Clear()
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

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillPage()
        FillRow()
        FillCboArea()
        FillcboKalaV()
        FillDataSetAndDataView()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDataSetAndDataView()
        If txtSrch.Text <> "" Then
            If ColumnSrch = 2 Then
                objDataview.RowFilter = "pcode = " & Me.txtSrch.Text & ""
            ElseIf ColumnSrch = 3 Then
                objDataview.RowFilter = "pers_name like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 4 Then
                objDataview.RowFilter = "pers_family like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 5 Then
                objDataview.RowFilter = "Dat like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 6 Then
                objDataview.RowFilter = "kala like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 8 Then
                objDataview.RowFilter = "Area_name like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 12 Then
                objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End If
        End If
        FillDataGridView()
    End Sub

    Private Sub cboArea_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboArea.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboKalaV.Focus()
        End If
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKala.KeyPress
        If e.KeyChar = ChrW(13) Then
            If CheckBox1.Checked = True Then
                If txtKcode.Text = "" Then
                    Z = 2
                    Code_Soft = 15
                    FormName = "KalaOut"
                    FillKalaMoj()
                    FillDataKala()
                    Srch.Width = 454
                    Srch.lblName.Text = "نام کالا"
                    Srch.ShowDialog()
                Else
                    cboArea.Focus()
                End If
            Else
                cboArea.Focus()
            End If
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

    Private Sub cboKalaV_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboKalaV.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCount.Focus()
        End If
    End Sub

    Private Sub txtCount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount.GotFocus
        txtCount.SelectionStart = 0
        txtCount.SelectionLength = Len(txtCount.Text)
    End Sub

    Private Sub txtCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCount.TextChanged
        If txtCount.Text = "" Then
            txtCount.Text = 0
        End If
    End Sub

    Private Sub txtDscr_GotFocus1(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        txtDscr.SelectionStart = 0
        txtDscr.SelectionLength = Len(txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
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
                    .Range("B1").Value = "شماره ردیف"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "کد پرسنلی"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نام"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "نام خانوادگی"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "تاریخ"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "شرح کالا"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "محل ارسال"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "واحد کالا"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "تعداد"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "توضیحات"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "وضعیت"
                    .Range("L1").Font.Bold = True

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
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                            .Range("L" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
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

    Private Sub btnRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRep.Click
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.CreateTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 43)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        If DataGridView1.RowCount > 0 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.KalaOut"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Page", txtPage.Text)
                objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)
                objCommand.Parameters.AddWithValue("@Repdate", ConvD.Class1.ConvDate)
                objCommand.Parameters.AddWithValue("@Company", Mainfrm.lblCompName.Text)
                objCommand.Parameters.AddWithValue("@Ucode", User_Code)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Page, Row, Dat, Pcode, Pname, Pfamily FROM Rep.RepKalaOut", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Rep.RepKalaOut")
            objDataview = New DataView(objDataset.Tables("Rep.RepKalaOut"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Row"

            If objDataview.Count > 0 Then
                Rptpath.FileName = My.Application.Info.DirectoryPath & "\Report\KalaOut.rpt"
                Rptpath.SetDatabaseLogon(Us, Pw)
                RepForm.CrystReport.ReportSource = Rptpath
                RepForm.Label1.Text = "برگه خروج کالا"
                RepForm.CrystReport.RefreshReport()
                RepForm.Show()
            End If
        Else
            MsgbOK.Label1.Text = " با اطلاعات فوق رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnRep.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub btnRecv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecv.Click
        If Panel1.Visible = False Then
            Panel1.Visible = True
            txtPageNo.Text = ""
            'FillPage()
            txtPageNo.Focus()
        Else
            Panel1.Visible = False
        End If
    End Sub

    Private Sub txtPageNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPageNo.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnOk.Focus()
        End If
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If txtPageNo.Text <> "" Then
            txtPage.Text = txtPageNo.Text
        End If
        FillDataSetAndDataView()
        FillDataGridView()
        FillRow()
        Clear()
        Panel1.Visible = False
    End Sub

    Private Sub txtKala_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKala.TextChanged
        If txtKala.Text = "" Then
            txtKcode.Text = ""
            txtRowMoj.Text = ""
            txtCountOrg.Text = ""
        End If
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            txtPageNo.Enabled = True
            txtPageNo.Focus()
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            txtPageNo.Text = ""
            txtPageNo.Enabled = False
        End If
    End Sub

    Private Sub btnResid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResid.Click
        If txtPage.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شماره برگه را وارد کنید . "
            MsgbOK.ShowDialog()
            txtPage.Focus()
            Exit Sub
        End If
        ViewResid.ShowDialog()
        ViewResid.Activate()
    End Sub

    Private Sub FormChk()
        Code_Soft = 1

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Bas.Form_Sec.U_Code, Bas.Form.Form_Name FROM Bas.Form_Sec INNER JOIN Bas.Form ON Bas.Form_Sec.S_Code = Bas.Form.S_Code AND Bas.Form_Sec.T_Code = Bas.Form.T_Code AND Bas.Form_Sec.F_Code = Bas.Form.Code" & _
             " WHERE (Bas.Form_Sec.U_Code = " & User_Code & ") AND (Bas.Form_Sec.S_Code = " & Code_Soft & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Soft_Sec")
        objDataviewT = New DataView(objDataset.Tables("bas.Soft_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)

        DataGridView2.ClearSelection()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataviewT

        Me.DataGridView2.Columns(0).Visible = False
        Me.DataGridView2.Columns(1).HeaderText = "منو"
    End Sub

    Private Sub FormDetailChk()
        Code_Form = 1110

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Sav, Edt, Del, Sayer FROM Bas.Form_Sec WHERE (U_Code = " & User_Code & ") AND (F_Code = " & Code_Form & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Form_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Form_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        DataGridView2.ClearSelection()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).HeaderText = "ذخیره"
        Me.DataGridView2.Columns(1).HeaderText = "اصلاح"
        Me.DataGridView2.Columns(2).HeaderText = "حذف"
        Me.DataGridView2.Columns(3).HeaderText = "سایر"
    End Sub

    Private Sub btnArea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArea.Click
        Code_Soft = 1
        Code_Form = 1110
        Name_Form = "Area"

        FormDetailChk()
        If Not IsDBNull(DataGridView2.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView2.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                Area.btnSave.Enabled = True
            Else
                Area.btnSave.Enabled = False
            End If
        Else
            Area.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView2.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView2.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                Area.btnEdit.Enabled = True
            Else
                Area.btnEdit.Enabled = False
            End If
        Else
            Area.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView2.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView2.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                Area.btnDelete.Enabled = True
            Else
                Area.btnDelete.Enabled = False
            End If
        Else
            Area.btnDelete.Enabled = False
        End If
        txt1.Text = ""

        Area.Show()
        Area.Activate()
    End Sub

    Private Sub lblNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNew.Click
        FillPage()
        FillRow()
        FillDataSetAndDataView()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub btnEbtal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEbtal.Click
        If Panel2.Visible = False Then
            Panel2.Visible = True
            btnOke.Focus()
        ElseIf Panel2.Visible = True Then
            Panel2.Visible = False
        End If
    End Sub

    Private Sub btnOke_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOke.Click
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, Shob FROM Bnk.KalaOut WHERE (Page = " & txtPage.Text & ") AND (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.KalaOut")
        objDataview = New DataView(objDataset.Tables("Bnk.KalaOut"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtPage.Text) & " در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        End If

        If RB3.Checked = True Then
            Knd = 1
        ElseIf RB4.Checked = True Then
            Knd = 0
        End If

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " UPDATE Bnk.KalaOut SET Ebtal = @Ebtal WHERE (Page = " & txtPage.Text & ") AND (Dat = N'" & maskDate.Text & "') AND (Shob = " & Shob & ")"
        objCommand.Parameters.Clear()
        objCommand.CommandType = CommandType.Text
        objCommand.Parameters.AddWithValue("@Ebtal", Knd)

        objConnection.Open()
        Try
            objCommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        FillDataSetAndDataView()
        FillDataGridView()
        Panel2.Visible = False
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        txtKala.Text = ""
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtKala.Focus()
        End If
    End Sub
End Class