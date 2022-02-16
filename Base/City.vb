Public Class City
    Private Sub FillDatasetAndDataview()
        Dim cod As String
        cod = Me.cboState.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT CityState.CityState_Code, City.City_Code, City.City_Name FROM bas.City INNER JOIN bas.CityState ON City.CityState_Code = CityState.CityState_Code " & _
             " WHERE(CityState.CityState_Code = " & cod & ") ORDER BY CityState.CityState_Code, City.City_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.City")
        objDataview = New DataView(objDataset.Tables("bas.City"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "City_Code"
    End Sub

    Private Sub fillcboState()
        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT CityState_Code, CityState_Name FROM bas.CityState ORDER BY CityState_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.CityState")
        Me.cboState.DataSource = objDataset.Tables("bas.CityState")
        Me.cboState.DisplayMember = "CityState_Name"
        Me.cboState.ValueMember = "CityState_Code"
    End Sub

    Private Sub FillDataGridView1()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).Visible = False
        Me.DataGridView1.Columns(1).HeaderText = "کد شهر"
        Me.DataGridView1.Columns(2).HeaderText = "نام شهر"

        Me.DataGridView1.Columns(1).Width = 50
        Me.DataGridView1.Columns(2).Width = 300

    End Sub

    Private Sub SrchCity()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT CityState_Code, City_Code, City_Name FROM Bas.City ORDER BY City_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.City")
        objDataview = New DataView(objDataset.Tables("bas.City"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "City_Code"
    End Sub

    Private Sub FillDSAll()
        Dim objDataAdapter As New SqlDataAdapter _
               (" SELECT CityState_Code, City_Code, City_Name FROM bas.City ORDER BY CityState_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.City")
        objDataview = New DataView(objDataset.Tables("bas.City"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "City_Code"
    End Sub
   
    Private Sub CityMax()
        Dim objDataAdapter As New SqlDataAdapter _
            ("SELECT MAX (City_Code) FROM bas.City", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.City")
        objDataview = New DataView(objDataset.Tables("bas.City"))

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "column1")
        If Label3.Text = "" Then
            maskCode.Text = 1
        Else
            maskCode.Text = Trim(Label3.Text + 1)
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub
    Private Sub Clear()
        Me.maskCode.Text = "   "
        Me.txtName.Text = ""
        Me.maskCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.cboState.SelectedValue = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.maskCode.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(2).Value
    End Sub
    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        Me.maskCode.SelectionStart = 0
        Me.maskCode.SelectionLength = Len(Me.maskCode.Text)
    End Sub

    Public Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        Dim objCommand As New SqlCommand
        If e.KeyChar = ChrW(13) Then
            If Me.maskCode.Text <> "   " Then
                FillDSAll()
                IntPosition = objDataview.Find(Me.maskCode.Text)
                If IntPosition = -1 Then
                    Me.txtName.Text = ""
                    Me.txtName.Focus()
                    Exit Sub
                Else
                    objCurrencyManager.Position = IntPosition
                    Me.txtName.DataBindings.Clear()
                    Me.maskCode.DataBindings.Clear()
                    Me.Label3.DataBindings.Clear()
                    Me.Label3.DataBindings.Add("Text", objDataview, "CityState_Code")
                    Me.cboState.SelectedValue = Me.Label3.Text
                    Me.Label3.Text = ""
                    Me.maskCode.DataBindings.Add("text", objDataview, "City_Code")
                    Me.txtName.DataBindings.Add("text", objDataview, "City_Name")
                    Me.txtName.Focus()
                End If
            End If
        End If
        FillDatasetAndDataview()
        FillDataGridView1()
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
        Clear()
        CityMax()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objCommand As New SqlCommand
        If Me.maskCode.Text = "   " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        FillDSAll()
        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        objCurrencyManager.Position = IntPosition
        Me.Label3.DataBindings.Clear()
        Me.Label3.DataBindings.Add("Text", objDataview, "CityState_Code")
        Me.cboState.SelectedValue = Me.Label3.Text
        Me.Label3.Text = ""

        Dim objDataAdapter As New SqlDataAdapter _
             ("SELECT City_Code FROM bas.City_Sec WHERE(City_Code = '" & Trim(Me.maskCode.Text) & "') ORDER BY City_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.City_Sec")
        objDataview = New DataView(objDataset.Tables("bas.City_Sec"))
        Dim x As Integer
        x = objDataview.Count
        If x > 0 Then
            MsgbOK.Label1.Text = " کد شهر  " & Trim(Me.maskCode.Text) & "  در دسترسی به محل استقرار استفاده شده است . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد  " & Trim(Me.maskCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.maskCode.Focus()
                Exit Sub
            Else
                IntPosition = objCurrencyManager.Position
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   "DELETE FROM bas.City WHERE (City_Code = '" & Me.maskCode.Text & "')"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@City_Code", Me.maskCode.Text)

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
                FillDataGridView1()
                Clear()
                CityMax()
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim objCommand As New SqlCommand
        Dim Stcode As String
        If Me.maskCode.Text = "   " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام شهر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If

        FillDSAll()
        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Stcode = Me.cboState.SelectedValue.ToString
            objCurrencyManager.Position = IntPosition
            Me.Label3.DataBindings.Clear()
            Me.Label3.DataBindings.Add("Text", objDataview, "CityState_Code")
            Me.cboState.SelectedValue = Me.Label3.Text
            Me.Label3.Text = ""

            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " UPDATE bas.City SET City_Name = @City_Name WHERE (CityState_Code = " & Stcode & ") AND (City_Code = " & Me.maskCode.Text & ")"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.AddWithValue("@City_Name", Me.txtName.Text)

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
            FillDataGridView1()
            Clear()
            CityMax()
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objCommand As New SqlCommand
        Dim Stcode As String
        If Me.maskCode.Text = "   " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام شهر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDSAll()
        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد  " & Trim(Me.maskCode.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Stcode = Me.cboState.SelectedValue.ToString
            IntPosition = objCurrencyManager.Position
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO bas.City(CityState_Code, City_Code, City_Name)" & _
               " VALUES (@CityState_Code ,@City_Code ,@City_Name )"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.AddWithValue("@CityState_Code", Stcode)
            objCommand.Parameters.AddWithValue("@City_Code", Me.maskCode.Text)
            objCommand.Parameters.AddWithValue("@City_Name", Me.txtName.Text)

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
            FillDataGridView1()
            Clear()
            CityMax()
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        DGVMove()
        Me.maskCode.Focus()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub
    Private Sub City_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        fillcboState()
        FillDatasetAndDataview()
        FillDataGridView1()
        CityMax()
    End Sub

    Private Sub City_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub City_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 225
    End Sub

    Private Sub cboState_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboState.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.maskCode.Focus()
        End If
    End Sub

    Private Sub cboState_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboState.SelectedValueChanged
        If Me.cboState.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        End If
        FillDatasetAndDataview()
        FillDataGridView1()
        maskCode.Clear()
        txtName.Clear()
        CityMax()
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        If txtSrch.Text <> "" Then
            SrchCity()
        Else
            FillDatasetAndDataview()
        End If
        objDataview.RowFilter = "City_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView1()
    End Sub
End Class