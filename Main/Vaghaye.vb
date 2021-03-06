
Public Class Vaghaye
    Private Sub FillRowMax()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bas.Vaghaye GROUP BY Sal, Shob_Code HAVING (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Vaghaye")
        objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")
        If objDataview.Count > 0 Then
            txtRow.Text = Label6.Text + 1
        Else
            txtRow.Text = 1
        End If
        Label6.Text = ""
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, Alarm1, Alarm2, Alarm3, Dscr, RD1, RD2, RD3 FROM Bas.Vaghaye WHERE (Sal = " & Sal & ") AND (u_code = " & User_Code & ") ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Vaghaye")
        objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"
    End Sub

    Private Sub FillDataGridView()
        If objDataview.Count > 0 Then
            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview
        Else
            Me.DataGridView1.AutoGenerateColumns = True
            objDataview.Dispose()
            Me.DataGridView1.DataSource = objDataview
        End If
        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "یادآور اول"
        Me.DataGridView1.Columns(3).HeaderText = "یادآور دوم"
        Me.DataGridView1.Columns(4).HeaderText = "یادآور سوم"
        Me.DataGridView1.Columns(5).HeaderText = "شرح"
        Me.DataGridView1.Columns(6).Visible = False ' RD1
        Me.DataGridView1.Columns(7).Visible = False ' RD2
        Me.DataGridView1.Columns(8).Visible = False ' RD3

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 70
        Me.DataGridView1.Columns(3).Width = 70
        Me.DataGridView1.Columns(4).Width = 70
        Me.DataGridView1.Columns(5).Width = 500
    End Sub

    Private Sub Clear()
        maskDate1.Clear()
        maskDate2.Clear()
        maskDate2.Clear()
        txtDscr.Clear()
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        maskDate.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Single

        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(1).Value
        maskDate1.Text = DataGridView1.Rows(X).Cells(2).Value
        maskDate2.Text = DataGridView1.Rows(X).Cells(3).Value
        maskDate3.Text = DataGridView1.Rows(X).Cells(4).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(5).Value
        CheckBox1.CheckState = DataGridView1.Rows(X).Cells(6).Value
        CheckBox2.CheckState = DataGridView1.Rows(X).Cells(7).Value
        CheckBox3.CheckState = DataGridView1.Rows(X).Cells(8).Value
    End Sub
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Vaghaye_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub Vaghaye_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.tsHeader.Cursor = Cursors.Hand
        maskDate.Text = ConvD.Class1.ConvDate
        FillRowMax()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRowMax()
        Clear()
    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdVaghaye"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Alarm1", maskDate1.Text)
            objCommand.Parameters.AddWithValue("@Alarm2", maskDate2.Text)
            objCommand.Parameters.AddWithValue("@Alarm3", maskDate3.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@RD1", CheckBox1.CheckState)
            objCommand.Parameters.AddWithValue("@RD2", CheckBox2.CheckState)
            objCommand.Parameters.AddWithValue("@RD3", CheckBox3.CheckState)
            objCommand.Parameters.AddWithValue("@Ucode", User_Code)
            objCommand.Parameters.AddWithValue("@Sal", Sal)
            objCommand.Parameters.AddWithValue("@Shob", Shob)


            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        FillDatasetAndDataview()
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " شماره ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillRowMax()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        FillDatasetAndDataview()
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillRowMax()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        FillDatasetAndDataview()
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره ردیف " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                maskDate.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                FillRowMax()
                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        End If
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
                maskDate1.Focus()
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
        'If e.KeyChar = ChrW(13) Then
        '    btnSave.Focus()
        'End If
    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub maskDate1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate1.GotFocus
        maskDate1.SelectionStart = 0
        maskDate1.SelectionLength = Len(maskDate1.Text)
    End Sub

    Private Sub maskDate2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate2.GotFocus
        maskDate2.SelectionStart = 0
        maskDate2.SelectionLength = Len(maskDate2.Text)
    End Sub

    Private Sub maskDate3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate3.GotFocus
        maskDate3.SelectionStart = 0
        maskDate3.SelectionLength = Len(maskDate3.Text)
    End Sub

    Private Sub maskDate1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate1.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate1.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate1.Focus()
                Exit Sub
            Else
                maskDate2.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate2.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate2.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate2.Focus()
                Exit Sub
            Else
                maskDate3.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate3.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate3.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate3.Focus()
                Exit Sub
            Else
                txtDscr.Focus()
            End If
        End If
    End Sub
End Class