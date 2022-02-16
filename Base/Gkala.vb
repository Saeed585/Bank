Public Class Gkala
    Private Sub FillDatasetAndDataview()
        Dim sgcode As String
        sgcode = Me.cboSGrpKala.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
             (" SELECT KalaSGrp.SCode, Kalagrp.GCode, Kalagrp.GName FROM bas.Kalagrp INNER JOIN bas.KalaSGrp ON Kalagrp.SCode = KalaSGrp.SCode WHERE(KalaSGrp.SCode = " & sgcode & ") ORDER BY Kalagrp.GCode ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Kalagrp")
        objDataview = New DataView(objDataset.Tables("bas.Kalagrp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "GCode"
    End Sub
    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).Visible = False
        Me.DataGridView1.Columns(1).HeaderText = "کد"
        Me.DataGridView1.Columns(2).HeaderText = "نام گروه"

        Me.DataGridView1.Columns(1).Width = 50
        Me.DataGridView1.Columns(2).Width = 225
    End Sub

    Private Sub fillcboSGrpKala()
        Dim objDataAdapter As New SqlDataAdapter _
             (" SELECT Bas.KalaSGrp.SCode, Bas.KalaSGrp.SName FROM Bas.KalaSGrp INNER JOIN Bas.SGKala_Sec ON Bas.KalaSGrp.SCode = Bas.SGKala_Sec.SarGrpK_Code" &
             " WHERE (Bas.SGKala_Sec.U_Code = 1) ORDER BY Bas.KalaSGrp.SCode", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.KalaSGrp")
        Me.cboSGrpKala.DataSource = objDataset.Tables("bas.KalaSGrp")
        Me.cboSGrpKala.DisplayMember = "SName"
        Me.cboSGrpKala.ValueMember = "SCode"
    End Sub
    Private Sub Clear()
        Me.txtCode.Text = ""
        Me.txtName.Text = ""
        Me.txtCode.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(2).Value
    End Sub

    Private Sub Gkala_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        fillcboSGrpKala()
        If Me.cboSGrpKala.SelectedValue <> Nothing Then
            FillDatasetAndDataview()
            FillDataGridView()
        End If
    End Sub

    Private Sub Gkala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Me.cboSGrpKala.Focus()
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(txtName.Text)
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
        If txtCode.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا کد گروه کالا را وارد کنید  . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = "  کد گروه  " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد   . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Dim objDataAdapter As New SqlDataAdapter _
                ("SELECT SCode, GCode FROM bas.Kala WHERE (SCode = '" & Me.cboSGrpKala.SelectedValue.ToString & "') AND (GCode = '" & Trim(Me.txtCode.Text) & "')ORDER BY SCode ", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Kala")
            objDataview = New DataView(objDataset.Tables("bas.Kala"))

            Dim x As Integer
            x = objDataview.Count
            If x > 0 Then
                MsgbOK.Label1.Text = " کد گروه  " & Trim(Me.txtCode.Text) & "  در فرم کالاها استفاده شده است . "
                MsgbOK.ShowDialog()
                Me.txtCode.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید کد گروه  " & Trim(Me.txtCode.Text) & "  از سیستم حذف شود  ؟ "
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
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا کد گروه کالا را وارد کنید   . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا نام گروه کالا را وارد کنید   . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = "  کد گروه  " & Trim(Me.txtCode.Text) & "  در سیستم موجود نمی باشد   . "
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
            objCommand.CommandText = "Bas.InsUpdGKala"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@SCode", Me.cboSGrpKala.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@GCode", Me.txtCode.Text)
            objCommand.Parameters.AddWithValue("@GName", Me.txtName.Text)
          
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
            MsgbOK.Label1.Text = "  لطفا کد گروه کالا را وارد کنید   . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا نام گروه کالا را وارد کنید   . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        IntPosition = objDataview.Find(Me.txtCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = "  کد گروه  " & Trim(Me.txtCode.Text) & "  قبلا در سیستم ثبت شده است   . "
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

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Gkala_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 221
    End Sub

    Private Sub cboSGrpKala_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboSGrpKala.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtCode.Focus()
        End If
    End Sub

    Private Sub cboSGrpKala_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSGrpKala.SelectedValueChanged
        If Me.cboSGrpKala.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            If Me.cboSGrpKala.SelectedValue = Nothing Then
                Exit Sub
            End If
        End If
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
        cboSGrpKala.Focus()
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

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub
End Class