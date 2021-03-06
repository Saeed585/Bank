Public Class Story
    Public Sub FillRow()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bas.Story", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Story")
        objDataview = New DataView(objDataset.Tables("Bas.Story"))
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
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, Name, Dscr FROM Bas.Story ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Story")
        objDataview = New DataView(objDataset.Tables("Bas.Story"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "موضوع"
        Me.DataGridView1.Columns(3).HeaderText = "شرح"

        Me.DataGridView1.Columns(0).Width = 70
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 200
        Me.DataGridView1.Columns(3).Width = 600
    End Sub

    Private Sub Clear()
        Me.txtName.Text = ""
        Me.txtDscr.Text = ""
        Me.maskDate.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.maskDate.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(2).Value
        Me.txtDscr.Text = Me.DataGridView1.Rows(X).Cells(3).Value
    End Sub

    Private Sub txtDcsr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        Me.txtDscr.SelectionStart = 0
        Me.txtDscr.SelectionLength = Len(Me.txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub Del()
        If Me.txtRow.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtRow.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید کد " & Trim(Me.txtRow.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtRow.Focus()
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
        If Me.txtRow.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtRow.Focus()
            Exit Sub
        ElseIf Me.txtDscr.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtDscr.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
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
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdStory"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        If Me.txtRow.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtRow.Focus()
            Exit Sub
        ElseIf Me.txtDscr.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtDscr.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtRow.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtRow.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
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

    Private Sub Story_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub Story_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub Story_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.tsHeader.Cursor = Cursors.Hand
        Me.maskDate.Text = ConvD.Class1.ConvDate
        FillRow()

        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
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
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

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
End Class