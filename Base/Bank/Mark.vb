Public Class Mark
    Private Sub FillCode()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Mark_code) AS Expr1 FROM Bas.Mark", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        objDataview = New DataView(objDataset.Tables("Bas.Mark"))
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
        (" SELECT Mark_code, Mark_name, SarGroupNo FROM Bas.Mark ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        objDataview = New DataView(objDataset.Tables("Bas.Mark"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Mark_code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام"
        Me.DataGridView1.Columns(2).HeaderText = "شماره سرگروه"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 500
        Me.DataGridView1.Columns(2).Width = 100
    End Sub

    Private Sub FillDataGridView2()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(1).Visible = False
        Me.DataGridView2.Columns(2).HeaderText = "نام"

        Me.DataGridView2.Columns(2).Width = 175
    End Sub
    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        maskCode.Text = DataGridView1.Rows(X).Cells(0).Value
        txtName.Text = DataGridView1.Rows(X).Cells(1).Value
        txtSarG.Text = DataGridView1.Rows(X).Cells(2).Value
    End Sub
    Private Sub Clear()
        txtName.Text = ""
        txtSarG.Text = ""
        maskCode.Focus()
    End Sub

    Private Sub FillCboUser()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT U_Code, U_Name, U_Family FROM bas.UserList WHERE (Act = 1) ORDER BY U_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.UserList")
        cboUser.DataSource = objDataset.Tables("bas.UserList")
        cboUser.DisplayMember = "u_Family"
        cboUser.ValueMember = "u_Code"
    End Sub

    Private Sub FillDataUser()
        Dim ucode As String
        ucode = cboUser.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT U_Code, U_Name, U_Family FROM bas.UserList WHERE (U_Code = " & ucode & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.UserList")
        objDataview = New DataView(objDataset.Tables("bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "u_code"
    End Sub
    Private Sub ChkValueMark()
        Dim Cnt As Integer
        Dim x As Integer
        Dim Ucode
        Ucode = cboUser.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT U_code, Mark_code FROM Bas.Mark_Sec WHERE (U_code = " & Ucode & ") ORDER BY Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark_Sec")
        objDataview = New DataView(objDataset.Tables("Bas.Mark_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Mark_code"

        Cnt = DataGridView2.Rows.Count - 1
        For x = 0 To Cnt
            IntPosition = objDataview.Find(DataGridView2.Rows(x).Cells(1).Value)
            If IntPosition <> -1 Then
                Me.DataGridView2.Rows(x).Cells(0).Value = True
            Else
                Me.DataGridView2.Rows(x).Cells(0).Value = False
            End If
        Next x
    End Sub
    Private Sub Model_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillCode()
        FillDataSetAndDataView()
        FillDataGridView()
        FillCboUser()
    End Sub

    Private Sub Model_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
    End Sub

    Private Sub Model_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 335
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
            txtSarG.Focus()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub
    Private Sub InsertUpdate()

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdMark"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Code", maskCode.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)
            objCommand.Parameters.AddWithValue("@SarG", txtSarG.Text)

            objCommand.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            objConnection.Close()
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.maskCode.Text = "  " Then
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
            Btn = 1
            InsertUpdate()

            FillCode()
            FillDataSetAndDataView()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillCode()
        Clear()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.maskCode.Text = "  " Then
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
            Btn = 2
            InsertUpdate()

            FillCode()
            FillDataSetAndDataView()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.maskCode.Text = "  " Then
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
                Btn = 3
                InsertUpdate()

                FillCode()
                FillDataSetAndDataView()
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

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDataSetAndDataView()
        objDataview.RowFilter = "Mark_name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView()
    End Sub

    Private Sub btnMark_Click(sender As Object, e As EventArgs) Handles btnMark.Click
        If GrpMark.Visible = False Then
            CheckBox1.Visible = True
            GrpMark.Visible = True
            FillDataSetAndDataView()
            FillDataGridView2()
            ChkValueMark()
            txtName.ReadOnly = True
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
            cboUser.Focus()
        ElseIf GrpMark.Visible = True Then
            CheckBox1.Visible = False
            txtName.ReadOnly = False
            GrpMark.Visible = False
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnDelete.Enabled = True
            txtName.Focus()
        End If
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim objCommand As New SqlCommand
        If e.ColumnIndex = 0 Then
            Dim X As Object
            Dim Ucode As Object
            Dim Mark As Integer
            Ucode = cboUser.SelectedValue.ToString
            X = Me.DataGridView2.CurrentCell.RowIndex
            If Me.DataGridView2.Rows(X).Cells(0).Value = Nothing Or Me.DataGridView2.Rows(X).Cells(0).Value = False Then
                Me.DataGridView2.Rows(X).Cells(0).Value = True

                objCommand.Connection = objConnection
                objCommand.CommandText =
                    " INSERT INTO Bas.Mark_Sec (U_code, Mark_code) VALUES (@U_code, @Mark_code)"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@U_code", Ucode)
                objCommand.Parameters.AddWithValue("@Mark_Code", DataGridView2.Rows(X).Cells(1).Value)
            Else
                Mark = Me.DataGridView2.Rows(X).Cells(1).Value
                Me.DataGridView2.Rows(X).Cells(0).Value = False

                objCommand.Connection = objConnection
                objCommand.CommandText =
                   " DELETE FROM Bas.Mark_Sec WHERE (U_code = " & Ucode & ") AND (Mark_Code = " & Mark & ")"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.Clear()
            End If

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
    End Sub

    Private Sub cboUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUser.SelectedIndexChanged

    End Sub

    Private Sub cboUser_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboUser.SelectedValueChanged
        Dim ucode As String
        If Me.cboUser.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            ucode = cboUser.SelectedValue.ToString
            FillDataUser()
            IntPosition = objDataview.Find(ucode)
            If IntPosition <> -1 Then
                txtUName.DataBindings.Clear()
                txtUName.DataBindings.Add("text", objDataview, "u_name")
            End If
            ChkValueMark()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Dim X As Object
        Dim Ucode As Object
        Dim Mark As Integer
        Ucode = cboUser.SelectedValue.ToString
        For X = 0 To DataGridView2.RowCount - 1
            Mark = Me.DataGridView2.Rows(X).Cells(1).Value
            If CheckBox1.Checked = True Then
                If Me.DataGridView2.Rows(X).Cells(0).Value = Nothing Or Me.DataGridView2.Rows(X).Cells(0).Value = False Then
                    Me.DataGridView2.Rows(X).Cells(0).Value = True

                    objCommand.Connection = objConnection
                    objCommand.CommandText =
                        " INSERT INTO Bas.Mark_Sec (U_code, Mark_code) VALUES (@U_code, @Mark_code)"
                    objCommand.CommandType = CommandType.Text
                    objCommand.Parameters.Clear()
                    objCommand.Parameters.AddWithValue("@U_code", Ucode)
                    objCommand.Parameters.AddWithValue("@Mark_Code", Mark)
                End If
            Else
                Me.DataGridView2.Rows(X).Cells(0).Value = False

                objCommand.Connection = objConnection
                objCommand.CommandText =
                   " DELETE FROM Bas.Mark_Sec WHERE (U_code = " & Ucode & ") AND (Mark_code = " & Mark & ")"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.Clear()
            End If

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        Next
    End Sub

    Private Sub txtSarG_TextChanged(sender As Object, e As EventArgs) Handles txtSarG.TextChanged

    End Sub

    Private Sub txtSarG_GotFocus(sender As Object, e As EventArgs) Handles txtSarG.GotFocus
        txtSarG.SelectionStart = 0
        txtSarG.SelectionLength = Len(txtSarG.Text)
    End Sub

    Private Sub txtSarG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSarG.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub
End Class