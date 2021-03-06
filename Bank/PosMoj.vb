
Public Class PosMoj
    Public Sub FillRow()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT MAX(Row) AS Expr1 FROM Bnk.PosMoj WHERE (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PosMoj")
        objDataview = New DataView(objDataset.Tables("Bnk.PosMoj"))
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

    Private Sub FillDataSetAndDataView()
        Dim Mark As String
        Dim Model As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Mark = Pos.cboMark.SelectedValue.ToString
        If Pos.CboModel.Text = "" Then
            Model = 0
        Else
            Model = Pos.CboModel.SelectedValue.ToString
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, Tedad, Dscr, MblKh, Shob_Code, Sal" &
             " FROM Bnk.PosMoj WHERE (Mark_Code = " & Mark & ") AND (Model_Code = " & Model & ") AND (Shob_Code = " & Shob & ") ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PosMoj")
        objDataview = New DataView(objDataset.Tables("Bnk.PosMoj"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "تعداد"
        Me.DataGridView1.Columns(3).HeaderText = "شرح"
        Me.DataGridView1.Columns(4).HeaderText = "مبلغ خرید"
        Me.DataGridView1.Columns(5).Visible = False
        Me.DataGridView1.Columns(6).Visible = False

        DataGridView1.Columns(4).DefaultCellStyle.Format = "N0"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 50
        Me.DataGridView1.Columns(3).Width = 250
        Me.DataGridView1.Columns(4).Width = 120
    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(1).Value
        txtTedad.Text = DataGridView1.Rows(X).Cells(2).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(3).Value
        txtMblKh.Text = DataGridView1.Rows(X).Cells(4).Value
    End Sub

    Private Sub Clear()
        maskDate.Text = ConvD.Class1.ConvDate
        txtTedad.Text = ""
        txtDscr.Text = ""
        txtMblKh.Text = 0
        maskDate.Focus()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub txtTedad_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTedad.GotFocus
        txtTedad.SelectionStart = 0
        txtTedad.SelectionLength = Len(txtTedad.Text)
    End Sub

    Private Sub txtTedad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTedad.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMblKh.Focus()
        End If
    End Sub

    Private Sub txtTedad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTedad.TextChanged

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
                txtTedad.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

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

    Private Sub PosMoj_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'FillDataSetAndDataView()
        'FillDataGridView()
    End Sub

    Private Sub PosMoj_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub PosMoj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        maskDate.Text = ConvD.Class1.ConvDate
        FillRow()
        FillDataSetAndDataView()
        FillDataGridView()

        KeyPreview = True
    End Sub

    Private Sub SaveLogPosMoj()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " INSERT INTO [Log].PosMojLog (Row, Dat, Tedad, Dscr, MblKh, Mark_Code, Model_Code, Shob_Code, Sal)" &
           " SELECT Row, Dat, Tedad, Dscr, MblKh, Mark_Code, Model_Code, Shob_Code, Sal FROM Bnk.PosMoj WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")"

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
            " UPDATE [Log].PosMojLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" &
            " WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ") AND (tim_stat IS NULL)"
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
        Dim Mark As String
        Dim Model As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Mark = Pos.cboMark.SelectedValue.ToString
        If Pos.CboModel.Text = "" Then
            Model = 0
        Else
            Model = Pos.CboModel.SelectedValue.ToString
        End If

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdPosMoj"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Tedad", txtTedad.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@MblKh", Str(txtMblKh.Text))
            objCommand.Parameters.AddWithValue("@CMark", Mark)
            objCommand.Parameters.AddWithValue("@CModel", Model)
            objCommand.Parameters.AddWithValue("@Shob", Shob)
            objCommand.Parameters.AddWithValue("@Sal", Sal)

            objCommand.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            objConnection.Close()
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        Dim objCommand As New SqlCommand
        Dim Sal As String
        Dim Mark As String
        Dim Model As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtTedad.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtTedad.Focus()
            Exit Sub
        ElseIf txtDscr.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            txtDscr.Focus()
            Exit Sub
        End If

        Mark = Pos.cboMark.SelectedValue.ToString
        If Pos.CboModel.Text = "" Then
            Model = 0
        Else
            Model = Pos.CboModel.SelectedValue.ToString
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Bnk.PosMoj WHERE (Row = " & txtRow.Text & ") AND (Mark_Code = " & Mark & ") AND (Model_Code = " & Model & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PosMoj")
        objDataview = New DataView(objDataset.Tables("Bnk.PosMoj"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()
            '--------LogPosMoj
            logStat = 1
            SaveLogPosMoj()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        Dim objCommand As New SqlCommand
        Dim Sal As String
        Dim Mark As String
        Dim Model As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtTedad.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtTedad.Focus()
            Exit Sub
        ElseIf txtDscr.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            txtDscr.Focus()
            Exit Sub
        End If

        Mark = Pos.cboMark.SelectedValue.ToString
        If Pos.CboModel.Text = "" Then
            Model = 0
        Else
            Model = Pos.CboModel.SelectedValue.ToString
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Bnk.PosMoj WHERE (Row = " & txtRow.Text & ") AND (Mark_Code = " & Mark & ") AND (Model_Code = " & Model & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PosMoj")
        objDataview = New DataView(objDataset.Tables("Bnk.PosMoj"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()
            '--------LogPosMoj
            logStat = 2
            SaveLogPosMoj()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Dim objCommand As New SqlCommand
        Dim Sal As String
        Dim Mark As String
        Dim Model As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        End If

        Mark = Pos.cboMark.SelectedValue.ToString
        If Pos.CboModel.Text = "" Then
            Model = 0
        Else
            Model = Pos.CboModel.SelectedValue.ToString
        End If

        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT Row, Shob_Code FROM Bnk.PosMoj WHERE (Row = " & txtRow.Text & ") AND (Mark_Code = " & Mark & ") AND (Model_Code = " & Model & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PosMoj")
        objDataview = New DataView(objDataset.Tables("Bnk.PosMoj"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                maskDate.Focus()
                Exit Sub
            Else
                '--------LogPosMoj
                logStat = 3
                SaveLogPosMoj()
                '-------------------
                Btn = 3
                InsertUpdate()

                Ref.PerformClick()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FillRow()
        Clear()
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDataSetAndDataView()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub txtMblKh_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMblKh.GotFocus
        txtMblKh.SelectionStart = 0
        txtMblKh.SelectionLength = Len(txtMblKh.Text)
    End Sub

    Private Sub txtMblKh_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMblKh.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtMblKh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMblKh.TextChanged
        txtMblKh.Text = Format(Val(txtMblKh.Text.Trim.Replace(",", "")), "#,0")
        txtMblKh.SelectionStart = txtMblKh.TextLength
    End Sub
End Class