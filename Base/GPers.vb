
Public Class GPers
    Private Sub FillDatasetAndDataview()
        If Me.RButton1.Checked = True Then
            Text1.Text = 1
        ElseIf Me.RButton2.Checked = True Then
            Text1.Text = 2
        End If
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Grp_code, Gpers_code, Gpers_name FROM bas.GPers WHERE (Grp_code = " & Text1.Text & ") ORDER BY Gpers_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.GPers")
        objDataview = New DataView(objDataset.Tables("bas.GPers"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Gpers_code"
    End Sub
    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).Visible = False
        Me.DataGridView1.Columns(1).HeaderText = "کد"
        Me.DataGridView1.Columns(2).HeaderText = "نام گروه"
        
        Me.DataGridView1.Columns(1).Width = 50
        Me.DataGridView1.Columns(2).Width = 150
    End Sub
  
    Private Sub DGVMove()
        Dim X As Integer
        X = Me.DataGridView1.CurrentCell.RowIndex
        maskCode.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        txtName.Text = Me.DataGridView1.Rows(X).Cells(2).Value
    End Sub
    Private Sub Clear()
        maskCode.Text = "  "
        txtName.Text = ""
        maskCode.Focus()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub GPers_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Text1.Text = 1
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub GPers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Text1.SendToBack()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub
    Private Sub GPers_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 221
    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdGPers"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@grp_Code", Text1.Text)
            objCommand.Parameters.AddWithValue("@gpers_Code", maskCode.Text)
            objCommand.Parameters.AddWithValue("@gpers_Name", txtName.Text)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskCode.Focus()
            Exit Sub
        End If
        If Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام گروه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        IntPosition = objDataview.Find(maskCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد  " & Trim(maskCode.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        DGVMove()
        txtName.Focus()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub maskCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskCode.GotFocus
        maskCode.SelectionStart = 0
        maskCode.SelectionLength = Len(maskCode.Text)
    End Sub

    Private Sub maskCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            If maskCode.Text <> "  " Then
                IntPosition = objDataview.Find(maskCode.Text)
                If IntPosition = -1 Then
                    Me.txtName.Text = ""
                Else
                    objCurrencyManager.Position = IntPosition
                    Me.maskCode.DataBindings.Clear()
                    Me.txtName.DataBindings.Clear()
                    Me.maskCode.DataBindings.Add("text", objDataview, "Gpers_Code")
                    Me.txtName.DataBindings.Add("text", objDataview, "Gpers_Name")
                End If
                Me.txtName.Focus()
            End If
        End If
    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskCode.MaskInputRejected

    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.maskCode.Text = "  " Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            maskCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام گروه را وارد کنید . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillDatasetAndDataview()
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

        IntPosition = objDataview.Find(Me.maskCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد  " & Trim(Me.maskCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد  " & Trim(Me.maskCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.maskCode.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                FillDatasetAndDataview()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub RButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RButton1.CheckedChanged
        
    End Sub

    Private Sub RButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RButton2.CheckedChanged
        
    End Sub

    Private Sub RButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RButton1.Click
        Text1.Text = 1
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub

    Private Sub RButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RButton2.Click
        Text1.Text = 2
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
    End Sub
End Class