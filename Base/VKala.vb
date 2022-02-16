Public Class VKala
    Private Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
        ("SELECT V_Code, V_Name FROM bas.V_Kala ORDER BY V_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.V_Kala")
        objDataview = New DataView(objDataset.Tables("bas.V_Kala"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "V_Code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام واحد"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 125
    End Sub

    Private Sub Clear()
        Me.txtCode.Text = ""
        Me.txtName.Text = ""
        Me.txtCode.Focus()
    End Sub

    Private Sub DGVMove()
        Me.txtCode.Text = Me.DataGridView1.Rows(Me.DataGridView1.CurrentCell.RowIndex).Cells(0).Value
        Me.txtName.Text = Me.DataGridView1.Rows(Me.DataGridView1.CurrentCell.RowIndex).Cells(1).Value
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
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد واحد کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد واحد  " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Dim objDataAdapter As New SqlDataAdapter _
                  ("SELECT V_Code FROM bas.Kala WHERE(V_Code = '" & Trim(Me.txtCode.Text) & "') ", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Kala")
            objDataview = New DataView(objDataset.Tables("bas.Kala"))

            Dim x As Integer
            x = objDataview.Count
            If x > 0 Then
                MsgbOK.Label1.Text = " کد واحد کالا  " & Trim(Me.txtCode.Text) & "  در فرم تعریف کالاها استفاده شده است . "
                MsgbOK.ShowDialog()
                Me.txtCode.Focus()
                Exit Sub
            Else
                MsgbYN.Label1.Text = "  آیا می خواهید کد واحد  " & Trim(txtCode.Text) & "  از سیستم حذف شود  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    Me.txtCode.Focus()
                    Exit Sub
                Else
                    Btn = 3
                    InsertUpdate()

                    FillDatasetAndDataview()
                    FillDataGridView()
                    Clear()
                End If
            End If
        End If

    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد واحد کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام واحد کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد واحد  " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد . "
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

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdVKala"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@V_Code", Me.txtCode.Text)
            objCommand.Parameters.AddWithValue("@V_Name", Me.txtName.Text)

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
            MsgbOK.Label1.Text = " لطفا کد واحد کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام واحد کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد واحد کالا  " & Trim(Me.txtCode.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

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

    Private Sub Vkala_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub Vkala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Vkala_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 221
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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
End Class