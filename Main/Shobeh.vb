Public Class Shobeh
    Private Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
        (" SELECT Shob_Code, Shob_Name FROM bas.Shob ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Shob")
        objDataview = New DataView(objDataset.Tables("bas.Shob"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "shob_code"
    End Sub
    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام شعبه"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 180
    End Sub

    Private Sub Clear()
        maskCode.Text = "  "
        txtName.Text = ""
        Me.maskCode.Focus()
    End Sub
    Private Sub DGVMove()
        Dim X As String
        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.maskCode.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(1).Value
    End Sub
    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        Me.maskCode.SelectionStart = 0
        Me.maskCode.SelectionLength = Len(Me.maskCode.Text)
    End Sub
    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objCommand As New SqlCommand
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد شعبه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Shob_Code FROM bas.Shob WHERE (Shob_Code = " & maskCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.shob")
        objDataview = New DataView(objDataset.Tables("bas.shob"))
        objDataview.Sort = "Shob_Code"

        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد شعبه  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد  . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد شعبه  " & Trim(maskCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.maskCode.Focus()
                Exit Sub
            Else
                IntPosition = objCurrencyManager.Position
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Shob WHERE (Shob_Code = " & maskCode.Text & ")"

                objConnection.Open()
                Try
                    objCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim objCommand As New SqlCommand
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد شعبه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام شعبه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Shob_Code FROM bas.Shob WHERE (Shob_Code = " & maskCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.shob")
        objDataview = New DataView(objDataset.Tables("bas.shob"))
        objDataview.Sort = "Shob_Code"

        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد شعبه  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد  . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " UPDATE bas.Shob SET Shob_Name =@Shob_Name" & _
               " WHERE(Shob_Code = " & maskCode.Text & ")"
            objCommand.Parameters.Clear()
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.AddWithValue("@Shob_Name", Me.txtName.Text)

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objCommand As New SqlCommand
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد شعبه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام شعبه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Shob_Code FROM bas.Shob WHERE (Shob_Code = " & maskCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.shob")
        objDataview = New DataView(objDataset.Tables("bas.shob"))
        objDataview.Sort = "Shob_Code"

        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد شعبه  " & Trim(Me.maskCode.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO bas.Shob (Shob_Code, Shob_Name)" & _
               " VALUES (@Shob_Code,@Shob_Name)"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.AddWithValue("@Shob_Code", maskCode.Text)
            objCommand.Parameters.AddWithValue("@Shob_Name", txtName.Text)

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub Shobeh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillDatasetAndDataview()
        FillDataGridView()
        Me.tsHeader.Cursor = Cursors.Hand
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            If Me.maskCode.Text <> "   " Then
                FillDatasetAndDataview()
                IntPosition = objDataview.Find(Me.maskCode.Text)
                If IntPosition = -1 Then
                    Me.txtName.Text = ""
                    Me.txtName.Focus()
                    Exit Sub
                Else
                    objCurrencyManager.Position = IntPosition
                    Me.txtName.DataBindings.Clear()
                    Me.txtName.DataBindings.Add("text", objDataview, "Shob_Name")
                    Me.txtName.Focus()
                End If
            Else
                Clear()
            End If
        End If
        ' FillDatasetAndDataview()
        ' FillDataGridView()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub txtDBname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDBname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskCode.MaskInputRejected

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub
End Class