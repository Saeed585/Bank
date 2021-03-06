Public Class Skala
    Private Sub FillCboUser()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT U_Code, U_Name, U_Family FROM bas.UserList WHERE (Act = 1) ORDER BY U_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.UserList")
        cboUser.DataSource = objDataset.Tables("bas.UserList")
        cboUser.DisplayMember = "u_family"
        cboUser.ValueMember = "u_Code"
    End Sub

    Private Sub CheckValue()
        Dim ucode As String
        ucode = Me.cboUser.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
              (" SELECT SarGrpK_Code FROM bas.SGKala_Sec  WHERE(U_Code = '" & ucode & "') ORDER BY SarGrpK_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.SGKala_Sec")
        objDataview = New DataView(objDataset.Tables("bas.SGKala_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "SarGrpK_Code"

        Dim X As Integer
        Dim rw As String
        rw = Me.DataGridView2.Rows.Count - 1
        For X = 0 To rw
            IntPosition = (objDataview.Find(Me.DataGridView2.Rows(X).Cells(1).Value))
            If IntPosition <> -1 Then
                Me.DataGridView2.Rows(X).Cells(0).Value = True
            End If
        Next X
    End Sub
    Private Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT SCode, SName FROM bas.KalaSGrp ORDER BY SCode ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.KalaSGrp")
        objDataview = New DataView(objDataset.Tables("bas.KalaSGrp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "SCode"
    End Sub

    Private Sub FillDataGridView1()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام سر گروه"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 225
    End Sub

    Private Sub FillDataGridView2()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(1).Visible = False
        Me.DataGridView2.Columns(2).HeaderText = "نام سر گروه"

        Me.DataGridView2.Columns(2).Width = 220
    End Sub

    Private Sub Clear()
        Me.txtCode.Text = ""
        Me.txtName.Text = ""
        Me.txtCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As String
        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(1).Value
    End Sub

    Private Sub Skala_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillDatasetAndDataview()
        FillDataGridView1()
    End Sub

    Private Sub Skala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
    End Sub

    Private Sub Skala_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 290
    End Sub
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد سر گروه کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد سر گروه  " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Dim objDataAdapter As New SqlDataAdapter _
                  ("SELECT SCode FROM bas.Kalagrp WHERE(SCode = '" & Trim(Me.txtCode.Text) & "') ", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Kalagrp")
            objDataview = New DataView(objDataset.Tables("bas.Kalagrp"))
            Dim x As Integer = objDataview.Count
            If x > 0 Then
                MsgbOK.Label1.Text = " کد سرگروه  " & Trim(Me.txtCode.Text) & "  در فرم گروه کالاها استفاده شده است . "
                MsgbOK.ShowDialog()
                Me.txtCode.Focus()
                Exit Sub
            Else
                MsgbYN.Label1.Text = "  آیا می خواهید کد سر گروه  " & Trim(Me.txtCode.Text) & "  از سیستم حذف شود  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    Me.txtCode.Focus()
                    Exit Sub
                Else
                    Btn = 3
                    InsertUpdate()

                    FillDatasetAndDataview()
                    FillDataGridView1()
                    Clear()
                End If
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد سر گروه کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام سر گروه کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "SCode"
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد واحد  " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView1()
            Clear()
        End If

    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdSKala"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@SCode", Me.txtCode.Text)
            objCommand.Parameters.AddWithValue("@SName", Me.txtName.Text)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد سر گروه کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام سر گروه کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "SCode"
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد سر گروه  " & Trim(Me.txtCode.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView1()
            Clear()
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
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

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim objCommand As New SqlCommand
        Dim Y As String
        Dim rw As String
        Dim usr As String
        If e.ColumnIndex = 0 Then
            Y = Me.DataGridView2.CurrentCell.RowIndex
            rw = Me.DataGridView2.Rows(Y).Cells(1).Value.ToString
            If Me.DataGridView2.Rows(Y).Cells(0).Value = Nothing Or Me.DataGridView2.Rows(Y).Cells(0).Value = False Then
                Me.DataGridView2.Rows(Y).Cells(0).Value = True
                objCommand.Connection = objConnection
                objCommand.CommandText =
                   " INSERT INTO bas.SGKala_Sec (U_Code, SarGrpK_Code) VALUES(@U_Code,@SarGrpK_Code )"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@U_Code", Me.cboUser.SelectedValue.ToString)
                objCommand.Parameters.AddWithValue("@SarGrpK_Code", Me.DataGridView2.Rows(Y).Cells(1).Value)
            Else
                usr = Me.cboUser.SelectedValue.ToString
                Me.DataGridView2.Rows(Y).Cells(0).Value = False
                objCommand.Connection = objConnection
                objCommand.CommandText =
                   " DELETE FROM bas.SGKala_Sec Where (U_Code=" & usr & ") and (SarGrpK_Code=" & rw & ")"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@U_Code", usr)
                objCommand.Parameters.AddWithValue("@SarGrpK_Code", Me.DataGridView2.Rows(Y).Cells(1).Value.ToString)
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

    Private Sub SGAccount_Click(sender As Object, e As EventArgs) Handles SGAccount.Click
        If Me.cboUser.Visible = True Then
            Me.cboUser.Visible = False
            Me.CheckBox1.Visible = False
            Me.Label1.Visible = False
            Me.txtNam.Visible = False
            Me.DataGridView2.Visible = False

            Me.btnSave.Enabled = True
            Me.btnEdit.Enabled = True
            Me.btnDelete.Enabled = True
            Me.txtCode.Focus()
        Else
            Me.cboUser.Visible = True
            Me.CheckBox1.Visible = True
            Me.Label1.Visible = True
            Me.txtNam.Visible = True
            Me.DataGridView2.Visible = True

            Me.btnSave.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnDelete.Enabled = False

            FillCboUser()
            FillDatasetAndDataview()
            FillDataGridView2()
            CheckValue()
            Me.cboUser.Focus()
        End If
    End Sub

    Private Sub cboUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUser.SelectedIndexChanged

    End Sub

    Private Sub cboUser_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboUser.SelectedValueChanged
        If Me.cboUser.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        End If

        CheckBox1.Checked = False
        Dim Usr As String
        Usr = Me.cboUser.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT U_Code, U_Name FROM bas.UserList WHERE(U_Code = '" & Usr & "') ORDER BY U_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.UserList")
        objDataview = New DataView(objDataset.Tables("bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        Me.txtNam.DataBindings.Clear()
        Me.txtNam.DataBindings.Add("Text", objDataview, "U_Name")

        FillDatasetAndDataview()
        FillDataGridView2()
        CheckValue()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        Dim X As Object
        Dim Ucode As Object
        Dim SGcode As Integer
        Ucode = cboUser.SelectedValue.ToString
        For X = 0 To DataGridView2.RowCount - 1
            SGcode = Me.DataGridView2.Rows(X).Cells(1).Value
            If CheckBox1.Checked = True Then
                If Me.DataGridView2.Rows(X).Cells(0).Value = Nothing Or Me.DataGridView2.Rows(X).Cells(0).Value = False Then
                    Me.DataGridView2.Rows(X).Cells(0).Value = True

                    objCommand.Connection = objConnection
                    objCommand.CommandText =
                        " INSERT INTO bas.SGKala_Sec (U_Code, SarGrpK_Code) VALUES (@U_Code, @SarGrpK_Code )"
                    objCommand.Parameters.Clear()
                    objCommand.CommandType = CommandType.Text
                    objCommand.Parameters.AddWithValue("@U_Code", Ucode)
                    objCommand.Parameters.AddWithValue("@SarGrpK_Code", SGcode)
                End If
            Else
                Me.DataGridView2.Rows(X).Cells(0).Value = False

                objCommand.Connection = objConnection
                objCommand.CommandText =
                   " DELETE FROM bas.SGKala_Sec Where (U_Code=" & Ucode & ") and (SarGrpK_Code=" & SGcode & ")"
                objCommand.Parameters.Clear()
                objCommand.CommandType = CommandType.Text
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

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub
End Class