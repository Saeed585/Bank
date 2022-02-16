Public Class KeyBoard
    Private Sub FillCode()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Key_code) AS Expr1 FROM Bas.KeyBoard", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.KeyBoard")
        objDataview = New DataView(objDataset.Tables("Bas.KeyBoard"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            maskCode.Text = Label3.Text + 1
        Else
            maskCode.Text = 1
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim objDataAdapter As New SqlDataAdapter _
        (" SELECT Key_code, Key_name FROM Bas.KeyBoard ORDER BY Key_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.KeyBoard")
        objDataview = New DataView(objDataset.Tables("Bas.KeyBoard"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Key_code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 500
    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        maskCode.Text = DataGridView1.Rows(X).Cells(0).Value
        txtName.Text = DataGridView1.Rows(X).Cells(1).Value
    End Sub
    Private Sub Clear()
        txtName.Text = ""
        txtName.Focus()
    End Sub

    Private Sub KeyBoard_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillDataSetAndDataView()
        FillDataGridView()
        FillCode()
    End Sub

    Private Sub KeyBoard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
    End Sub

    Private Sub KeyBoard_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 225
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        maskCode.SelectionStart = 0
        maskCode.SelectionLength = Len(maskCode.Text)
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskCode.MaskInputRejected

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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.maskCode.Text = "   " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf txtName.Text = "      " Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(maskCode.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO Bas.KeyBoard (Key_code, Key_name) VALUES (@code ,@name)"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@code", maskCode.Text)
            objCommand.Parameters.AddWithValue("@name", txtName.Text)

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
            FillCode()
            Clear()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillCode()
        Clear()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.maskCode.Text = "   " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf txtName.Text = "      " Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(maskCode.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " Update Bas.KeyBoard SET Key_name='" & txtName.Text & "' WHERE(Key_code=" & maskCode.Text & ")"
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

            FillDataSetAndDataView()
            FillDataGridView()
            FillCode()
            Clear()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.maskCode.Text = "   " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد " & Trim(maskCode.Text) & " در سیستم موجود نمی باشد. "
            MsgbOK.ShowDialog()
            maskCode.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد  " & Trim(maskCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                maskCode.Focus()
                Exit Sub
            Else
                IntPosition = objCurrencyManager.Position
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM Bas.KeyBoard WHERE(Key_code = " & maskCode.Text & ")"

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
                FillCode()
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

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDataSetAndDataView()
        objDataview.RowFilter = "Key_name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView()
    End Sub
End Class