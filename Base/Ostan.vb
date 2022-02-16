Public Class Ostan
    Public Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
             ("SELECT CityState_Code, CityState_Name FROM bas.CityState ORDER BY CityState_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.CityState")
        objDataview = New DataView(objDataset.Tables("bas.CityState"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "CityState_Code"
    End Sub
    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد استان"
        Me.DataGridView1.Columns(1).HeaderText = "نام استان"

        Me.DataGridView1.Columns(0).Width = 60
        Me.DataGridView1.Columns(1).Width = 300
    End Sub
  
    Private Sub Clear()
        Me.maskCode.Text = "  "
        Me.txtName.Text = ""
        Me.maskCode.Focus()
    End Sub
    Private Sub DGVMove()
        Dim X As Integer
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
        Dim x As Integer
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد استان را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد استان   " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد  . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Dim objDataAdapter As New SqlDataAdapter _
                  ("SELECT CityState_Code FROM bas.City_Sec WHERE(City_Code = '" & Trim(Me.maskCode.Text) & "') ORDER BY City_Code ", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.City_Sec")
            objDataview = New DataView(objDataset.Tables("bas.City_Sec"))
            x = objDataview.Count
            If x > 0 Then
                MsgbOK.Label1.Text = " کد استان  " & Trim(Me.maskCode.Text) & "  در فرم محل استقرار (دسترسی) استفاده شده است . "
                MsgbOK.ShowDialog()
                Me.maskCode.Focus()
                Exit Sub
            End If

            Dim objDataAdapter1 As New SqlDataAdapter _
                  ("SELECT CityState_Code FROM bas.City WHERE(CityState_Code = '" & Trim(Me.maskCode.Text) & "') ORDER BY CityState_Code ", objConnection)
            objDataset = New DataSet
            objDataAdapter1.Fill(objDataset, "bas.City")
            objDataview = New DataView(objDataset.Tables("bas.City"))
            x = objDataview.Count
            If x > 0 Then
                MsgbOK.Label1.Text = " کد استان  " & Trim(Me.maskCode.Text) & "  در فرم محل استقرار استفاده شده است . "
                MsgbOK.ShowDialog()
                Me.maskCode.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید کد استان   " & Trim(maskCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.maskCode.Focus()
                Exit Sub
            End If
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " DELETE FROM bas.CityState Where CityState_Code=" & maskCode.Text & ""

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

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim objCommand As New SqlCommand
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد استان را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام استان را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد استان  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد  . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " UPDATE bas.CityState SET CityState_Name =@CityState_Name Where CityState_Code =@CityState_Code "
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.AddWithValue("@CityState_Name", Me.txtName.Text)
            objCommand.Parameters.AddWithValue("@CityState_Code", Me.maskCode.Text)

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
            MsgbOK.Label1.Text = " لطفا کد استان را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام استان را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد استان   " & Trim(Me.maskCode.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO bas.CityState (CityState_Code, CityState_Name) VALUES(@CityState_Code,@CityState_Name )"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.AddWithValue("@CityState_Code", Me.maskCode.Text)
            objCommand.Parameters.AddWithValue("@CityState_Name", Me.txtName.Text)

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

    Private Sub Ostan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Code_Soft = 8
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub Ostan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        Dim objCommand As New SqlCommand
        If e.KeyChar = ChrW(13) Then
            If Me.maskCode.Text <> "  " Then
                FillDatasetAndDataview()
                IntPosition = objDataview.Find(Me.maskCode.Text)
                If IntPosition = -1 Then
                    Me.txtName.Text = ""
                    Me.txtName.Focus()
                    Exit Sub
                Else
                    objCurrencyManager.Position = IntPosition
                    Me.txtName.DataBindings.Clear()
                    Me.txtName.DataBindings.Add("text", objDataview, "CityState_Name")
                    Me.txtName.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtNameShop_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub txtSrch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.GotFocus
        Me.txtSrch.SelectionStart = 0
        Me.txtSrch.SelectionLength = Len(Me.txtSrch.Text)
    End Sub

    Private Sub maskSrch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskSrch.GotFocus
        Me.maskSrch.SelectionStart = 0
        Me.maskSrch.SelectionLength = Len(Me.maskSrch.Text)
    End Sub

    Private Sub maskSrch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskSrch.KeyPress
        If e.KeyChar = ChrW(13) Then
            FillDatasetAndDataview()
            If Me.maskSrch.Text <> "   " Then
                objDataview.RowFilter = "CityState_Code = '" & Me.maskSrch.Text & "'"
            End If
            FillDataGridView()
        End If
    End Sub
    Private Sub txtSrch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        objDataview.RowFilter = "CityState_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        Me.Label2.Text = Me.DataGridView1.Columns(e.ColumnIndex).HeaderText
        If e.ColumnIndex = 0 Then
            Me.txtSrch.Visible = False
            Me.maskSrch.Visible = True
            Me.maskSrch.Focus()
        Else
            Me.txtSrch.Visible = True
            Me.maskSrch.Visible = False
            Me.txtSrch.Focus()
        End If
        Me.txtSrch.Text = ""
        Me.maskSrch.Text = "    "
    End Sub
    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Ostan_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 195
    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskCode.MaskInputRejected

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
        Me.maskCode.Focus()
    End Sub
End Class