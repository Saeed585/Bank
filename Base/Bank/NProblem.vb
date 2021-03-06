Public Class NProblem
    Private Sub FillCode()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Code) AS Expr1 FROM Bas.NProblem", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.NProblem")
        objDataview = New DataView(objDataset.Tables("Bas.NProblem"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")

        If Label6.Text <> "" Then
            txtCode.Text = Val(Label6.Text) + 1
        ElseIf Label6.Text = "" Then
            txtCode.Text = 1
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Public Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Code, Name FROM Bas.NProblem ORDER BY Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.NProblem")
        objDataview = New DataView(objDataset.Tables("Bas.NProblem"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "شرح"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 500
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(1).Value
    End Sub

    Private Sub Clear()
        Me.txtName.Clear()
        Me.txtName.Focus()
    End Sub
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub NProblem_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillCode()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub NProblem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub NProblem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        Me.KeyPreview = True
    End Sub

    Private Sub NProblem_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 225
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.SelectionStart = 0
        txtName.SelectionLength = Len(txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdNProblem"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Code", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)

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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillCode()
        Clear()
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

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
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

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT NCodeProb FROM Bnk.[Work] WHERE (NCodeProb = " & txtCode.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.[Work]")
        objDataview = New DataView(objDataset.Tables("Bnk.[Work]"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " کد " & txtCode.Text & " در فرم ثبت عملیات استفاده شده است . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT Prob_code FROM Bas.MergeProb WHERE (Prob_code = " & txtCode.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bas.MergeProb")
        objDataview = New DataView(objDataset.Tables("Bas.MergeProb"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " کد " & txtCode.Text & " در فرم تعیین گروه استفاده شده است . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub btnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRef.Click
        FillCode()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        txtCode.SelectionStart = 0
        txtCode.SelectionLength = Len(txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        objDataview.RowFilter = "Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView()
    End Sub
End Class