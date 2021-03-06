Public Class Model
    Private Sub FillCode()
        Dim Mark As String
        Mark = cboMark.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Model_Code) AS Expr1 FROM Bas.Model WHERE (Mark_code = " & Mark & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Model")
        objDataview = New DataView(objDataset.Tables("Bas.Model"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("text", objDataview, "Expr1")
        If Label6.Text <> "" Then
            txtCode.Text = Label6.Text + 1
        Else
            txtCode.Text = 1
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Private Sub FillcboMark()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Mark_code, Mark_name FROM Bas.Mark ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        Me.cboMark.DataSource = objDataset.Tables("Bas.Mark")
        Me.cboMark.DisplayMember = "Mark_name"
        Me.cboMark.ValueMember = "Mark_code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Mark As String

        Mark = cboMark.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.Model.Model_Code, Bas.Model.Model_Name, Bas.Model.Model_NameFa" & _
             " FROM Bas.Model INNER JOIN Bas.Mark ON Bas.Model.Mark_code = Bas.Mark.Mark_code WHERE(Bas.Model.Mark_code = " & Mark & ") ORDER BY Bas.Model.Model_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Model")
        objDataview = New DataView(objDataset.Tables("Bas.Model"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Model_Code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام"
        Me.DataGridView1.Columns(2).HeaderText = "شرح فارسی"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 500
        Me.DataGridView1.Columns(2).Width = 200
    End Sub

    Private Sub Clear()
        Me.txtName.Clear()
        Me.txtNameFa.Clear()
        Me.txtCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtNameFa.Text = Me.DataGridView1.Rows(X).Cells(2).Value
    End Sub


    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtNameFa.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillCode()
        Clear()
    End Sub

    Private Sub Del()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید کد " & Trim(Me.txtCode.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtCode.Focus()
            Exit Sub
        Else
            Btn = 3
            InsertUpdate()

            FillCode()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub Edt()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtNameFa.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح فارسی را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtNameFa.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillCode()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
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
            objCommand.CommandText = "Bas.InsUpdModel"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@CMark", cboMark.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@CModel", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)
            objCommand.Parameters.AddWithValue("@NameFa", txtNameFa.Text)

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
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtNameFa.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح فارسی را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtNameFa.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(Me.txtCode.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillCode()
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
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

    Private Sub Model_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillcboMark()
        FillCode()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub Model_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub Model_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Model_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 221
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

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub cboMark_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMark.SelectedIndexChanged

    End Sub

    Private Sub cboMark_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMark.SelectedValueChanged
        If Me.cboMark.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillCode()
            FillDatasetAndDataview()
            FillDataGridView()
        End If
    End Sub

    Private Sub txtNameFa_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNameFa.GotFocus
        Me.txtNameFa.SelectionStart = 0
        Me.txtNameFa.SelectionLength = Len(Me.txtNameFa.Text)
    End Sub

    Private Sub txtNameFa_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameFa.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub txtNameFa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameFa.TextChanged

    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        objDataview.RowFilter = "Model_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView()
    End Sub
End Class