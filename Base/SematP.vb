Public Class SematP
    Private Sub FillCode()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Semat_code) AS Expr1 FROM Bas.Semat", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Semat")
        objDataview = New DataView(objDataset.Tables("Bas.Semat"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("text", objDataview, "Expr1")
        If Label6.Text <> "" Then
            maskCode.Text = Label6.Text + 1
        Else
            maskCode.Text = 1
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Scode As String
        Scode = 0
        Dim objDataAdapter As New SqlDataAdapter _
        ("SELECT Semat_Code, Semat_Name FROM Bas.Semat WHERE (Semat_Code <> " & Scode & ") ORDER BY Semat_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Semat")
        objDataview = New DataView(objDataset.Tables("bas.Semat"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Semat_Code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام سمت"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 500
    End Sub

    Private Sub Clear()
        Me.txtName.Clear()
        Me.maskCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim x As Integer

        x = Me.DataGridView1.CurrentCell.RowIndex
        Me.maskCode.Text = Me.DataGridView1.Rows(x).Cells(0).Value
        Me.txtName.Text = Me.DataGridView1.Rows(x).Cells(1).Value
    End Sub
    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        Me.maskCode.SelectionStart = 0
        Me.maskCode.SelectionLength = Len(Me.maskCode.Text)
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            If Me.maskCode.Text <> "  " Then
                ' FillDatasetAndDataview()
                IntPosition = objDataview.Find(Me.maskCode.Text)
                If IntPosition = -1 Then
                    Me.txtName.Text = ""
                    Me.txtName.Focus()
                    Exit Sub
                Else
                    objCurrencyManager.Position = IntPosition
                    Me.txtName.DataBindings.Clear()
                    Me.txtName.DataBindings.Add("text", objDataview, "Semat_Name")
                    Me.txtName.Focus()
                End If
            End If
        End If

    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillCode()
        Clear()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد سمت را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد سمت  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
               ("SELECT semat_code FROM bas.Perspay WHERE (semat_code ='" & Trim(Me.maskCode.Text) & "') ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Perspay")
        objDataview = New DataView(objDataset.Tables("bas.Perspay"))

        Dim x As Integer
        x = objDataview.Count
        If x > 0 Then
            MsgbOK.Label1.Text = " کد سمت  " & Trim(Me.maskCode.Text) & "  در فرم اطلاعات پرسنلی استفاده شده است . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد سمت  " & Trim(Me.maskCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.maskCode.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                FillCode()
                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد سمت را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام سمترا وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد سمت  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد . "
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

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdSemat"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Code", maskCode.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد سمت را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام سمت را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد سمت  " & Trim(Me.maskCode.Text) & "  قبلا در سیستم ثبت شده است  . "
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

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub SematP_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillCode()
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub SematP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'ITsystemDataSet.Semat' table. You can move, or remove it, as needed.
        ' Me.SematTableAdapter.Fill(Me.ITsystemDataSet.Semat)
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub SematP_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 221
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub
End Class