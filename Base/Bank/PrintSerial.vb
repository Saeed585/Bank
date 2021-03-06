
Public Class PrintSerial
    Public Sub FillRow()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bas.PrintSerial WHERE (Shob = " & cboShob.SelectedValue.ToString & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.PrintSerial")
        objDataview = New DataView(objDataset.Tables("Bas.PrintSerial"))
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

    Private Sub FillCboShob()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE(Shob_Code <> 0) ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Shob")
        Me.cboShob.DataSource = objDataset.Tables("Bas.Shob")
        Me.cboShob.DisplayMember = "Shob_Name"
        Me.cboShob.ValueMember = "Shob_Code"
    End Sub

    Private Sub FillCboMove()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE(Shob_Code <> 0) ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Shob")
        Me.cboMove.DataSource = objDataset.Tables("Bas.Shob")
        Me.cboMove.DisplayMember = "Shob_Name"
        Me.cboMove.ValueMember = "Shob_Code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bnk.View_PrintSerial WHERE (Shob = " & cboShob.SelectedValue.ToString & ") ORDER BY Row")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bnk.View_PrintSerial")
        objDataview = New DataView(objDataset.Tables("Bnk.View_PrintSerial"))
        objConnection.Close()
        txtCount.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).Visible = False
        Me.DataGridView1.Columns(2).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(3).Visible = False
        Me.DataGridView1.Columns(4).HeaderText = "واحد سازمانی"
        Me.DataGridView1.Columns(5).Visible = False ' Prn_Code
        Me.DataGridView1.Columns(6).HeaderText = "پرینتر"
        Me.DataGridView1.Columns(7).HeaderText = "سریال"
        Me.DataGridView1.Columns(8).HeaderText = "اموال"
        Me.DataGridView1.Columns(9).HeaderText = "IP"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(2).Width = 120
        Me.DataGridView1.Columns(4).Width = 120
        Me.DataGridView1.Columns(6).Width = 120
        Me.DataGridView1.Columns(7).Width = 120
        Me.DataGridView1.Columns(8).Width = 120
        Me.DataGridView1.Columns(9).Width = 120
    End Sub

    Private Sub Clear()
        txtName.Text = ""
        txtSerial.Text = ""
        txtAmval.Text = ""
        txtIP.Text = ""
        txtName.Focus()
    End Sub

    Private Sub SaveLogPrintSerial()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = cboShob.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].PrintSerialLog (Row, Shob, Vahed, prn_code, Serial, Amval, IP)" & _
           " SELECT Row, Shob, Vahed, prn_code, Serial, Amval, IP FROM Bas.PrintSerial WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")"

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
            " UPDATE [Log].PrintSerialLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
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

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.cboShob.SelectedValue = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.cboVahed.SelectedValue = Me.DataGridView1.Rows(X).Cells(3).Value
        Me.txtCodeP.Text = Me.DataGridView1.Rows(X).Cells(5).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(6).Value
        Me.txtSerial.Text = Me.DataGridView1.Rows(X).Cells(7).Value
        Me.txtAmval.Text = Me.DataGridView1.Rows(X).Cells(8).Value
        Me.txtIP.Text = Me.DataGridView1.Rows(X).Cells(9).Value
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub PrintSerial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Clear()
        End If
    End Sub

    Private Sub PrintSerial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillCboShob()
        FillCboMove()
        FillCboVahed()
        FillRow()
        txtCodeP.SendToBack()
        txtRowShMove.SendToBack()
        txtShName.SendToBack()
        FormName = "PrintSerial"
        KeyPreview = True
    End Sub

    Private Sub cboShob_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboShob.SelectedIndexChanged

    End Sub

    Private Sub cboShob_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboShob.SelectedValueChanged
        If Me.cboShob.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillDatasetAndDataview()
            FillDataGridView()
            FillRow()
            Clear()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCodeP.Text = "" Then
                FormName = "PrintSerial"
                FillPrint()
                FillDataPrint()
                Srch.lblName.Text = "لیست پرینتر"
                Srch.ShowDialog()
            Else
                txtSerial.Focus()
            End If
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCodeP.Text = ""
        End If
    End Sub

    Private Sub txtSerial_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSerial.GotFocus
        Me.txtSerial.SelectionStart = 0
        Me.txtSerial.SelectionLength = Len(Me.txtSerial.Text)
    End Sub

    Private Sub txtSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtAmval.Focus()
        End If
    End Sub

    Private Sub txtSerial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerial.TextChanged

    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdPrintSerial"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Shob", cboShob.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Vahed", cboVahed.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Printer", txtCodeP.Text)
            objCommand.Parameters.AddWithValue("@Serial", txtSerial.Text)
            objCommand.Parameters.AddWithValue("@Amval", txtAmval.Text)
            objCommand.Parameters.AddWithValue("@IP", txtIP.Text)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        Dim Shb As String
        Dim Vahed As String

        Shb = cboShob.SelectedValue.ToString
        Vahed = cboVahed.SelectedValue.ToString

        If Me.txtCodeP.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام پرینتر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf txtSerial.Text = "" Then
            MsgbOK.Label1.Text = " لطفا سریال را وارد کنید . "
            MsgbOK.ShowDialog()
            txtSerial.Focus()
            Exit Sub
        End If

        Dim objDataAdapter0 As New SqlDataAdapter _
            (" SELECT Bas.PrintSerial.Serial, Bas.Shob.Shob_Name FROM Bas.PrintSerial INNER JOIN Bas.Shob ON Bas.PrintSerial.Shob = Bas.Shob.Shob_Code" & _
             " WHERE (Bas.PrintSerial.Serial = N'" & txtSerial.Text & "')", objConnection)
        objDataset = New DataSet
        objDataAdapter0.Fill(objDataset, "Bas.PrintSerial")
        objDataview = New DataView(objDataset.Tables("Bas.PrintSerial"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        txtShName.Text = ""
        txtShName.DataBindings.Clear()
        txtShName.DataBindings.Add("Text", objDataview, "Shob_Name")

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " سریال فوق در شعبه " & txtShName.Text & " ثبت شده است . "
            MsgbOK.ShowDialog()
            txtSerial.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob FROM Bas.PrintSerial WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shb & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.PrintSerial")
        objDataview = New DataView(objDataset.Tables("Bas.PrintSerial"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " اطلاعات فوق در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()

            '--------PrintSerialLog
            logStat = 1
            SaveLogPrintSerial()
            '--------------------
            FillDatasetAndDataview()
            FillDataGridView()
            FillRow()
            Clear()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        Dim Shb As String
        Dim Vahed As String

        Shb = cboShob.SelectedValue.ToString
        Vahed = cboVahed.SelectedValue.ToString

        If Me.txtCodeP.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام پرینتر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf txtSerial.Text = "" Then
            MsgbOK.Label1.Text = " لطفا سریال را وارد کنید . "
            MsgbOK.ShowDialog()
            txtSerial.Focus()
            Exit Sub
        End If

        Dim objDataAdapter0 As New SqlDataAdapter _
            (" SELECT Bas.PrintSerial.Serial, Bas.Shob.Shob_Name FROM Bas.PrintSerial INNER JOIN Bas.Shob ON Bas.PrintSerial.Shob = Bas.Shob.Shob_Code" & _
             " WHERE (Bas.PrintSerial.Serial = N'" & txtSerial.Text & "') AND (prn_code <> " & txtCodeP.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter0.Fill(objDataset, "Bas.PrintSerial")
        objDataview = New DataView(objDataset.Tables("Bas.PrintSerial"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        txtShName.Text = ""
        txtShName.DataBindings.Clear()
        txtShName.DataBindings.Add("Text", objDataview, "Shob_Name")

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " سریال فوق در شعبه " & txtShName.Text & " ثبت شده است . "
            MsgbOK.ShowDialog()
            txtSerial.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob FROM Bas.PrintSerial WHERE (Row = " & txtRow.Text & ") AND (Shob = " & cboShob.SelectedValue.ToString & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.PrintSerial")
        objDataview = New DataView(objDataset.Tables("Bas.PrintSerial"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " اطلاعات فوق در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            '--------PrintSerialLog
            logStat = 2
            SaveLogPrintSerial()
            '--------------------
            FillDatasetAndDataview()
            FillDataGridView()
            FillRow()
            Clear()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Del()
        Dim Shb As String
        Dim Vahed As String

        Shb = cboShob.SelectedValue.ToString
        Vahed = cboVahed.SelectedValue.ToString

        If Me.txtCodeP.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام پرینتر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCodeP.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob FROM Bas.PrintSerial WHERE (Row = " & txtRow.Text & ") AND (Shob = " & cboShob.SelectedValue.ToString & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.PrintSerial")
        objDataview = New DataView(objDataset.Tables("Bas.PrintSerial"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " اطلاعات فوق در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید سریال  " & Trim(txtSerial.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtName.Focus()
                Exit Sub
            Else
                '--------PrintSerialLog
                logStat = 3
                SaveLogPrintSerial()
                '--------------------

                Btn = 3
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                FillRow()
                Clear()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim objCommand As New SqlCommand
        Dim Rw As String
        Dim ShMove As String
        Dim x As Integer
        Dim cnt As String

        Shob = cboShob.SelectedValue.ToString
        ShMove = cboMove.SelectedValue.ToString

        cnt = Me.DataGridView1.Rows.Count - 1
        If Shob <> ShMove Then
            MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboMove.Text & "  اطمینان دارید  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                btnMove.Focus()
                Exit Sub
            End If

            For x = 0 To cnt
                Rw = DataGridView1.Rows(x).Cells(0).Value
                If DataGridView1.Rows(x).Selected = True Then
                    FillRow()
                    '--------PrintSerialLog
                    logStat = 4
                    SaveLogPrintSerial()
                    '--------------------
                    Dim objDataAdapter As New SqlDataAdapter _
                        (" SELECT MAX(Row) AS Expr1 FROM Bas.PrintSerial WHERE (Shob = " & ShMove & ")", objConnection)
                    objDataset = New DataSet
                    objDataAdapter.Fill(objDataset, "Bas.PrintSerial")
                    objDataviewT = New DataView(objDataset.Tables("Bas.PrintSerial"))
                    objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                    objDataviewT.Sort = "Expr1"

                    Label6.DataBindings.Clear()
                    Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                    If Label6.Text <> "" Then
                        txtRowShMove.Text = Val(Label6.Text) + 1
                    ElseIf Label6.Text = "" Then
                        txtRowShMove.Text = 1
                    End If
                    Label6.DataBindings.Clear()
                    Label6.Text = ""

                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                       " UPDATE Bas.PrintSerial SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
                    objCommand.CommandType = CommandType.Text
                    objCommand.Parameters.Clear()

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
            Next x
        Else
            MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
            MsgbOK.ShowDialog()
            btnMove.Focus()
            Exit Sub
        End If
        
        FillDatasetAndDataview()
        FillDataGridView()
        FillRow()
        Clear()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            cboMove.Enabled = True
            btnMove.Enabled = True
        ElseIf CheckBox1.Checked = False Then
            cboMove.Enabled = False
            btnMove.Enabled = False
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
                    .Range("A1").Value = "ردیف"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "محل فعالیت"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "واحد سازمانی"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "پرینتر"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "سریال"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "اموال"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "IP"
                    .Range("G1").Font.Bold = True

                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        'If DataGridView1.Rows(X).Selected = True Then
                        .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                        .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                        .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                        .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                        .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                        .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                        .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                        i += 1
                        'End If
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

    Private Sub txtAmval_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmval.GotFocus
        Me.txtAmval.SelectionStart = 0
        Me.txtAmval.SelectionLength = Len(Me.txtAmval.Text)
    End Sub

    Private Sub txtAmval_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmval.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIP.Focus()
        End If
    End Sub

    Private Sub txtAmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmval.TextChanged

    End Sub

    Private Sub txtIP_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIP.GotFocus
        Me.txtIP.SelectionStart = 0
        Me.txtIP.SelectionLength = Len(Me.txtIP.Text)
    End Sub

    Private Sub txtIP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIP.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIP.TextChanged

    End Sub
End Class