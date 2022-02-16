Public Class INKala
    Private Sub KalaMax()
        Dim objDataAdapter As New SqlDataAdapter _
            ("SELECT Max (kala_code) FROM bas.kala ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.kala")
        objDataview = New DataView(objDataset.Tables("bas.kala"))

        Label6.DataBindings.Add("Text", objDataview, "column1")
        If Label6.Text = Nothing Then
            txtCode.Text = 1
        Else
            txtCode.Text = Trim(Label6.Text + 1)
        End If
        Label6.Text = ""
        Label6.DataBindings.Clear()
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

    Private Sub FillcboKalaGrp()
        Dim sgcode As String
        sgcode = Me.cboSGrpKala.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT GCode, GName FROM Bas.Kalagrp WHERE(SCode = " & sgcode & ") ORDER BY GCode", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Kalagrp")
        objDataview = New DataView(objDataset.Tables("bas.Kalagrp"))
        Me.cboKalaGrp.DataSource = objDataset.Tables("bas.Kalagrp")
        Me.cboKalaGrp.DisplayMember = "GName"
        Me.cboKalaGrp.ValueMember = "GCode"
    End Sub

    Private Sub fillcboKalaV()
        Dim objDataAdapter As New SqlDataAdapter _
            ("SELECT V_Code, V_Name FROM bas.V_Kala", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.V_Kala")
        Me.cboKalaV.DataSource = objDataset.Tables("bas.V_Kala")
        Me.cboKalaV.DisplayMember = "V_Name"
        Me.cboKalaV.ValueMember = "V_Code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter
        Dim X As Integer
        Dim Y As Integer
        X = Me.cboSGrpKala.SelectedValue
        Y = Me.cboKalaGrp.SelectedValue
     
        If CheckBox1.Checked = False Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bas.View_Kala WHERE (SCode = " & X & ") AND (GCode = " & Y & ") ORDER BY Kala_Code")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bas.View_Kala")
            objDataview = New DataView(objDataset.Tables("Bas.View_Kala"))
            ' objDataview.Sort = "Row"
            objConnection.Close()
        Else
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bas.View_Kala ORDER BY SCode, GCode, Kala_Code")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bas.View_Kala")
            objDataview = New DataView(objDataset.Tables("Bas.View_Kala"))
            ' objDataview.Sort = "Row"
            objConnection.Close()
        End If
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        ' Me.DataGridView1.DataSource = Nothing
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        If CheckBox1.Checked = False Then
            Me.DataGridView1.Columns(0).Visible = False ' Scode
            Me.DataGridView1.Columns(1).Visible = False ' Sname
            Me.DataGridView1.Columns(2).Visible = False ' Gcode
            Me.DataGridView1.Columns(3).Visible = False ' Gname
            Me.DataGridView1.Columns(4).HeaderText = "کد کالا"
            Me.DataGridView1.Columns(5).HeaderText = "نام کالا"
            Me.DataGridView1.Columns(6).Visible = False ' vcode
            Me.DataGridView1.Columns(7).HeaderText = "واحد"
        Else
            Me.DataGridView1.Columns(0).Visible = False ' Scode
            Me.DataGridView1.Columns(1).HeaderText = "سرگروه"
            Me.DataGridView1.Columns(2).Visible = False ' Gcode
            Me.DataGridView1.Columns(3).HeaderText = "گروه"
            Me.DataGridView1.Columns(4).HeaderText = "کد کالا"
            Me.DataGridView1.Columns(5).HeaderText = "نام کالا"
            Me.DataGridView1.Columns(6).Visible = False ' vcode
            Me.DataGridView1.Columns(7).HeaderText = "واحد"
        End If

        If CheckBox1.Checked = False Then
            Me.DataGridView1.Columns(4).Width = 80
            Me.DataGridView1.Columns(5).Width = 250
            Me.DataGridView1.Columns(7).Width = 70
        Else
            Me.DataGridView1.Columns(1).Width = 150
            Me.DataGridView1.Columns(3).Width = 150
            Me.DataGridView1.Columns(4).Width = 80
            Me.DataGridView1.Columns(5).Width = 250
            Me.DataGridView1.Columns(7).Width = 70
        End If

    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.cboSGrpKala.SelectedValue = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.cboKalaGrp.SelectedValue = Me.DataGridView1.Rows(X).Cells(2).Value
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(4).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(5).Value
        Me.cboKalaV.SelectedValue = Me.DataGridView1.Rows(X).Cells(6).Value
    End Sub

    Private Sub fillcboMove()
        Dim sgcode As String
        sgcode = Me.cboSGrpKala.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT GCode, GName FROM Bas.Kalagrp WHERE(SCode = " & sgcode & ") ORDER BY GCode", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Kalagrp")
        objDataview = New DataView(objDataset.Tables("bas.Kalagrp"))
        Me.cboMove.DataSource = objDataset.Tables("bas.Kalagrp")
        Me.cboMove.DisplayMember = "GName"
        Me.cboMove.ValueMember = "GCode"
    End Sub

    Private Sub ChkCode()
        Dim X As Integer
        Dim Y As Integer
        X = Me.cboSGrpKala.SelectedValue
        Y = Me.cboKalaGrp.SelectedValue
        Dim objDataAdapter As New SqlDataAdapter _
        (" SELECT SCode, GCode, Kala_Code FROM bas.Kala WHERE (GCode <> '" & Y & "') OR (SCode <> '" & X & "')ORDER BY SCode ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Kala")
        objDataview = New DataView(objDataset.Tables("bas.Kala"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Kala_Code"
    End Sub

    Private Sub Clear()
        Me.txtName.Clear()
        Me.txtName.Focus()
    End Sub

    Private Sub INKala_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Then
            If btnSave.Enabled = True Then
                Sav()
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            If btnEdit.Enabled = True Then
                Edt()
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            If btnDelete.Enabled = True Then
                Del()
            End If
        ElseIf e.KeyCode = Keys.Insert Then
            Clear()
            KalaMax()
        End If
    End Sub

    Private Sub INkala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Me.cboMove.Enabled = False
        Me.btnMove.Enabled = False
        KeyPreview = True
    End Sub

    Private Sub cboSGrpKala_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSGrpKala.SelectedValueChanged
        If cboSGrpKala.Text <> Nothing Then
            If Me.cboSGrpKala.SelectedValue.ToString = "System.Data.DataRowView" Then
                Exit Sub
            Else
                FillcboKalaGrp()
                If Me.cboKalaGrp.Text <> Nothing Then
                    txtName.ReadOnly = False
                Else
                    txtName.ReadOnly = True
                End If
                fillcboMove()
                txtCode.Clear()
                txtName.Clear()
                KalaMax()
            End If
        End If
    End Sub

    Private Sub cboKalaGrp_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboKalaGrp.SelectedValueChanged
        If cboKalaGrp.Text <> Nothing Then
            If Me.cboKalaGrp.SelectedValue.ToString = "System.Data.DataRowView" Then
                Exit Sub
            Else
                If Me.CheckBox1.Checked = False Then
                    FillDatasetAndDataview()
                    FillDataGridView()
                    txtCode.Clear()
                    txtName.Clear()
                    KalaMax()
                End If
            End If
        End If
    End Sub

    Private Sub txtSrch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSrch.KeyPress

    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        objDataview.RowFilter = "kala_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView()
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub CheckBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = ChrW(13) Then
            If Me.CheckBox2.Checked = True Then
                Me.CheckBox1.Enabled = False
                Me.cboMove.Enabled = True
                Me.btnMove.Enabled = True
                Clear()
                Me.cboMove.Focus()
                Me.btnSave.Enabled = False
                Me.btnEdit.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnClear.Enabled = False
            Else
                Me.CheckBox1.Enabled = True
                Me.cboMove.Enabled = False
                Me.btnMove.Enabled = False
                Me.btnSave.Enabled = True
                Me.btnEdit.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnClear.Enabled = True
                Me.txtCode.Focus()
            End If
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            If Me.CheckBox2.Checked = False Then
                DGVMove()
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Me.DataGridView1.DataSource = Nothing
        If Me.CheckBox1.Checked = True Then
            Me.cboKalaGrp.Enabled = False
            Me.cboSGrpKala.Enabled = False
            Me.Label7.Visible = True
            Me.Label8.Visible = True
            Me.txtSrch.Visible = True
            Me.CheckBox2.Visible = False
            Me.cboMove.Visible = False
            Me.btnMove.Visible = False
            Me.btnSave.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnClear.Enabled = False

            FillDatasetAndDataview()
            FillDataGridView()
            Me.txtSrch.Focus()
        Else
            Me.cboKalaGrp.Enabled = True
            Me.cboSGrpKala.Enabled = True
            Me.Label7.Visible = False
            Me.Label8.Visible = False
            Me.txtSrch.Visible = False
            Me.CheckBox2.Visible = True
            Me.cboMove.Visible = True
            Me.btnMove.Visible = True
            Me.btnSave.Enabled = True
            Me.btnEdit.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnClear.Enabled = True
            Me.txtSrch.Clear()

            FillDatasetAndDataview()
            FillDataGridView()
            Me.cboSGrpKala.Focus()
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If Me.CheckBox2.Checked = True Then
            Me.CheckBox1.Enabled = False
            Me.cboMove.Enabled = True
            Me.btnMove.Enabled = True
            Clear()
            Me.cboMove.Focus()
            Me.btnSave.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnClear.Enabled = False
        Else
            Me.CheckBox1.Enabled = True
            Me.cboMove.Enabled = False
            Me.btnMove.Enabled = False
            Me.btnSave.Enabled = True
            Me.btnEdit.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnClear.Enabled = True
            Me.txtCode.Focus()
        End If

    End Sub

    Private Sub INkala_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 221
    End Sub

    Private Sub cboKalaGrp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboKalaGrp.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtCode.Focus()
        End If
    End Sub

    Private Sub INkala_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim x As Integer

        If CheckBox1.Checked = False Then
            If Me.cboSGrpKala.SelectedValue <> Nothing Then
                x = Me.cboSGrpKala.SelectedValue.ToString
                fillcboSGrpKala()
                Me.cboSGrpKala.SelectedValue = x
            Else
                fillcboSGrpKala()
            End If
            fillcboMove()
            fillcboKalaV()
            FillDatasetAndDataview()
            FillDataGridView()
            KalaMax()
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub


    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.cboKalaV.Focus()
        End If

    End Sub

    Private Sub cboSGrpKala_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboSGrpKala.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.cboKalaGrp.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
        KalaMax()
    End Sub

    Private Sub txtSrch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.GotFocus
        Me.txtSrch.SelectionStart = 0
        Me.txtSrch.SelectionLength = Len(Me.txtSrch.Text)
    End Sub

    Private Sub CheckBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = ChrW(13) Then
            If Me.CheckBox1.Checked = True Then
                Me.cboKalaGrp.Enabled = False
                Me.cboSGrpKala.Enabled = False
                Me.Label7.Visible = True
                Me.Label8.Visible = True
                Me.txtSrch.Visible = True
                Me.CheckBox2.Visible = False
                Me.cboMove.Visible = False
                Me.btnMove.Visible = False
                Me.btnSave.Enabled = False
                Me.btnEdit.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnClear.Enabled = False

                FillDatasetAndDataview()
                FillDataGridView()
                Me.txtSrch.Focus()
            Else
                Me.cboKalaGrp.Enabled = True
                Me.cboSGrpKala.Enabled = True
                Me.Label7.Visible = False
                Me.Label8.Visible = False
                Me.txtSrch.Visible = False
                Me.CheckBox2.Visible = True
                Me.cboMove.Visible = True
                Me.btnMove.Visible = True
                Me.btnSave.Enabled = True
                Me.btnEdit.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnClear.Enabled = True
                Me.txtSrch.Clear()

                FillDatasetAndDataview()
                FillDataGridView()
                Me.cboSGrpKala.Focus()
            End If
        End If
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim objCommand As New SqlCommand
        Dim sgcode As String
        Dim gcode As String
        Dim x As Integer
        Dim cnt As String
        sgcode = Me.cboSGrpKala.SelectedValue
        gcode = Me.cboKalaGrp.SelectedValue
        cnt = Me.DataGridView1.Rows.Count - 1

        If Me.cboMove.Text <> Nothing Then
            For x = 0 To cnt
                If DataGridView1.Rows(x).Selected = True Then
                    If Me.DataGridView1.Rows(x).Cells(2).Value <> Me.cboMove.SelectedValue.ToString Then
                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE bas.Kala SET  GCode = " & Me.cboMove.SelectedValue.ToString & "" & _
                           " WHERE (SCode = " & sgcode & ") AND (GCode =  " & gcode & ") AND (Kala_Code = " & Me.DataGridView1.Rows(x).Cells(4).Value & ")"
                        objCommand.CommandType = CommandType.Text

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
                End If
            Next x

            FillDatasetAndDataview()
            FillDataGridView()
        End If
    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdKala"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@SCode", Me.cboSGrpKala.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@GCode", Me.cboKalaGrp.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Kala_Code", Trim(Me.txtCode.Text))
            objCommand.Parameters.AddWithValue("@kala_Name", Me.txtName.Text)
            objCommand.Parameters.AddWithValue("@V_Code", Me.cboKalaV.SelectedValue.ToString)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا کد کالا را وارد کنید  . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا نام کالا را وارد کنید  . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        ChkCode()
        objDataview.Sort = "kala_code"
        IntPosition = objDataview.Find(Trim(Me.txtCode.Text))
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = "  کد کالای  " & Trim(Me.txtCode.Text) & "  قبلا در گروه کالای دیگری ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.txtName.Clear()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "kala_code"
        IntPosition = objDataview.Find(Trim(Me.txtCode.Text))
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = "  کد کالای  " & Trim(Me.txtCode.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            KalaMax()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا کد کالا را وارد کنید  . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا نام کالا را وارد کنید  . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        ChkCode()
        objDataview.Sort = "kala_code"
        IntPosition = objDataview.Find(Trim(Me.txtCode.Text))
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = "  کد کالای  " & Trim(Me.txtCode.Text) & "  قبلا در گروه کالای دیگری ثبت شده است و در گروه خود میتوانید اصلاح نمائید ."
            MsgbOK.ShowDialog()
            Me.txtName.Clear()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "kala_code"
        IntPosition = objDataview.Find(Trim(Me.txtCode.Text))
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = "  کد کالای  " & Trim(Me.txtCode.Text) & "  قبلا در سیستم به ثبت نرسیده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            KalaMax()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = "  لطفا کد کالا را وارد کنید  . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        End If

        ChkCode()
        objDataview.Sort = "kala_code"
        IntPosition = objDataview.Find(Trim(Me.txtCode.Text))
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = "  کد کالای  " & Trim(Me.txtCode.Text) & "  قبلا در گروه کالای دیگری ثبت شده است  و در گروه خود میتوانید حذف نمائید ."
            MsgbOK.ShowDialog()
            Me.txtName.Clear()
            Me.txtCode.Focus()
            Exit Sub
        End If

        FillDatasetAndDataview()
        objDataview.Sort = "kala_code"
        IntPosition = objDataview.Find(Trim(Me.txtCode.Text))
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = "  کد کالای  " & Trim(Me.txtCode.Text) & "  قبلا در سیستم به ثبت نرسیده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد کالای  " & Trim(Me.txtCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.txtCode.Focus()
                Exit Sub
            End If
            Btn = 3
            InsertUpdate()

            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            KalaMax()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub cboKalaV_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboKalaV.KeyPress
        If e.KeyChar = ChrW(13) Then
            If Me.btnSave.Enabled = False Then
                Me.txtCode.Focus()
            Else
                Me.btnSave.Focus()
            End If
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

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub cboSGrpKala_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSGrpKala.SelectedIndexChanged

    End Sub

    Private Sub cboKalaGrp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboKalaGrp.SelectedIndexChanged

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If DataGridView1.RowCount > 0 Then
            Try
                Dim xlapp As New Excel.Application()
                Dim xlbook As Excel.Workbook
                Dim xlsheet As Excel.Worksheet

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

                xlapp = CType(CreateObject("excel.application"), Excel.Application)
                xlbook = CType(xlapp.Workbooks.Add, Excel.Workbook)
                xlsheet = CType(xlbook.Worksheets(1), Excel.Worksheet)
                xlsheet.Application.Visible = True
                Dim i As Integer = 2
                Dim X As Integer

                With xlsheet
                    .DisplayRightToLeft = True
                    .Range("A1").Value = "سرگروه کالا"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "گروه کالا"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "کد کالا"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نام کالا"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "واحد"
                    .Range("E1").Font.Bold = True
                   
                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            i += 1
                        End If
                    Next X
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgbOK.Label1.Text = " جدول اطلاعات خالی میباشد . "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub
        End If
    End Sub
End Class