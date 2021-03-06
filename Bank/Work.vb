Public Class Work
    Dim ColumnSrch As Integer
    Public Sub FillRow()
        Dim Sl As String
        Dim Sal As String

        Sl = Mid(Mainfrm.cboSal_Mali.Text, 3, 2)
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.Work WHERE (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Work")
        objDataview = New DataView(objDataset.Tables("Bnk.Work"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")
        If Label6.Text <> "" Then
            txtRow.Text = Val(Label6.Text) + 1
        ElseIf Label6.Text = "" Then
            txtRow.Text = Sl & "00001"
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Private Sub FillcboGProblem()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Code, Name FROM Bas.GProblem ORDER BY Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.GProblem")
        Me.cboGProblem.DataSource = objDataset.Tables("Bas.GProblem")
        Me.cboGProblem.DisplayMember = "Name"
        Me.cboGProblem.ValueMember = "Code"
    End Sub

    Private Sub FillcboNProblem()
        Dim GProb As String

        GProb = cboGProblem.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.MergeProb.Prob_code, Bas.NProblem.Name FROM Bas.MergeProb INNER JOIN Bas.NProblem ON Bas.MergeProb.Prob_code = Bas.NProblem.Code" & _
             " WHERE(Bas.MergeProb.GProb_Code = " & GProb & ") ORDER BY Bas.MergeProb.Prob_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.MergeProb")
        Me.cboNProblem.DataSource = objDataset.Tables("Bas.MergeProb")
        Me.cboNProblem.DisplayMember = "Name"
        Me.cboNProblem.ValueMember = "Prob_code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If Pass.txtCode.Text <> 1 Then
            If CheckBox2.Checked = False Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_WorkOpen WHERE(Shob = " & Shob & ") AND (Sal = " & Sal & ") AND (UCode = " & Pass.txtCode.Text & ") ORDER BY Dat DESC, Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_WorkOpen")
                objDataview = New DataView(objDataset.Tables("Bnk.View_WorkOpen"))
                objConnection.Close()
            Else
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_WorkClose WHERE(Shob = " & Shob & ") AND (Sal = " & Sal & ") AND (Chk = 0) AND (UCode = " & Pass.txtCode.Text & ") ORDER BY Dat DESC, Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_WorkClose")
                objDataview = New DataView(objDataset.Tables("Bnk.View_WorkClose"))
                objConnection.Close()
            End If
        Else
            If CheckBox2.Checked = False Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_WorkEnd WHERE(Shob = " & Shob & ") AND (Sal = " & Sal & ") ORDER BY Dat DESC, Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_WorkEnd")
                objDataview = New DataView(objDataset.Tables("Bnk.View_WorkEnd"))
                objConnection.Close()
            Else
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_WorkNotEnd WHERE(Shob = " & Shob & ") AND (Sal = " & Sal & ") AND (Chk = 0) ORDER BY Dat DESC, Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_WorkNotEnd")
                objDataview = New DataView(objDataset.Tables("Bnk.View_WorkNotEnd"))
                objConnection.Close()
            End If
        End If

        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).Visible = False ' GProblem
        Me.DataGridView1.Columns(3).HeaderText = "گروه عملیات/پروژه"
        Me.DataGridView1.Columns(4).Visible = False ' NProblem
        Me.DataGridView1.Columns(5).HeaderText = "سرفصل عملیات"
        Me.DataGridView1.Columns(6).HeaderText = "عملیات/پروژه"
        Me.DataGridView1.Columns(7).Visible = False ' Chk
        Me.DataGridView1.Columns(8).HeaderText = "تاریخ اتمام"
        Me.DataGridView1.Columns(9).HeaderText = "شرح"
        Me.DataGridView1.Columns(10).Visible = False ' Pcode
        Me.DataGridView1.Columns(11).HeaderText = "نام کاربر"
        Me.DataGridView1.Columns(12).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(13).Visible = False ' Ucode
        Me.DataGridView1.Columns(14).Visible = False ' Uname
        Me.DataGridView1.Columns(15).HeaderText = "کارشناس"

        Me.DataGridView1.Columns(0).Width = 60
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(3).Width = 180
        Me.DataGridView1.Columns(5).Width = 180
        Me.DataGridView1.Columns(6).Width = 340
        Me.DataGridView1.Columns(8).Width = 70
        Me.DataGridView1.Columns(9).Width = 340
        Me.DataGridView1.Columns(11).Width = 80
        Me.DataGridView1.Columns(12).Width = 100
        Me.DataGridView1.Columns(15).Width = 100

        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Cells(7).Value = 0 Then
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
            Else
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
            End If
        Next

        'X = 0
        'While X < DataGridView1.RowCount - 1
        '    If DataGridView1.Rows(X).Cells(7).Value = 0 Then
        '        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
        '    Else
        '        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
        '    End If
        '    X = X + 1
        'End While
    End Sub

    Private Sub Clear()
        Me.txtFamily.Clear()
        Me.txtProblem.Clear()
        Me.CheckBox1.Checked = False
        maskDate.Text = ConvD.Class1.ConvDate
        Me.maskDate.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.maskDate.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.cboGProblem.SelectedValue = Me.DataGridView1.Rows(X).Cells(2).Value
        Me.cboNProblem.SelectedValue = Me.DataGridView1.Rows(X).Cells(4).Value
        Me.txtProblem.Text = Me.DataGridView1.Rows(X).Cells(6).Value
        Me.CheckBox1.CheckState = Me.DataGridView1.Rows(X).Cells(7).Value
        Me.maskDateEnd.Text = Me.DataGridView1.Rows(X).Cells(8).Value
        Me.txtDscr.Text = Me.DataGridView1.Rows(X).Cells(9).Value
        Me.txtCode.Text = Me.DataGridView1.Rows(X).Cells(10).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(11).Value
        Me.txtFamily.Text = Me.DataGridView1.Rows(X).Cells(12).Value
    End Sub


    Private Sub txtProbleme_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProblem.GotFocus
        Me.txtProblem.SelectionStart = 0
        Me.txtProblem.SelectionLength = Len(Me.txtProblem.Text)
    End Sub

    Private Sub txtProblem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProblem.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnSave.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub Del()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.Work WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Work")
        objDataview = New DataView(objDataset.Tables("Bnk.Work"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید ردیف " & Trim(Me.txtRow.Text) & "  از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.txtRow.Focus()
            Exit Sub
        Else
            Btn = 3
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub Edt()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Me.maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDate.Focus()
            Exit Sub
        ElseIf Me.txtProblem.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح عملیات را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtProblem.Focus()
            Exit Sub
        ElseIf Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کاربر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtFamily.Focus()
            Exit Sub
        ElseIf cboGProblem.SelectedValue = Nothing Then
            MsgbOK.Label1.Text = " گروه عملیات خالی میباشد . "
            MsgbOK.ShowDialog()
            Me.cboGProblem.Focus()
            Exit Sub
        ElseIf cboNProblem.SelectedValue = Nothing Then
            MsgbOK.Label1.Text = " سرفصل عملیات خالی میباشد . "
            MsgbOK.ShowDialog()
            Me.cboNProblem.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.Work WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Work")
        objDataview = New DataView(objDataset.Tables("Bnk.Work"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(Me.txtRow.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdWork"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@GCodeProb", cboGProblem.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@NCodeProb", cboNProblem.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Problem", txtProblem.Text)
            objCommand.Parameters.AddWithValue("@Chk", CheckBox1.CheckState)
            objCommand.Parameters.AddWithValue("@Datend", maskDateEnd.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@UCode", User_Code)
            objCommand.Parameters.AddWithValue("@Shob", Shob)
            objCommand.Parameters.AddWithValue("@Sal", Sal)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Sav()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Me.maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDate.Focus()
            Exit Sub
        ElseIf Me.txtProblem.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح عملیات را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtProblem.Focus()
            Exit Sub
        ElseIf Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کاربر را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtFamily.Focus()
            Exit Sub
        ElseIf cboGProblem.SelectedValue = Nothing Then
            MsgbOK.Label1.Text = " گروه عملیات خالی میباشد . "
            MsgbOK.ShowDialog()
            Me.cboGProblem.Focus()
            Exit Sub
        ElseIf cboNProblem.SelectedValue = Nothing Then
            MsgbOK.Label1.Text = " سرفصل عملیات خالی میباشد . "
            MsgbOK.ShowDialog()
            Me.cboNProblem.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row FROM Bnk.Work WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND  (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Work")
        objDataview = New DataView(objDataset.Tables("Bnk.Work"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " ردیف " & Trim(Me.txtRow.Text) & "  قبلا در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        If ColumnSrch = 6 Or ColumnSrch = 9 Then
            Label9.Text = Me.DataGridView1.Columns(e.ColumnIndex).HeaderText
        Else
            Label9.Text = "----------"
        End If
        txtSrch.Text = ""
        txtSrch.Focus()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub Work_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillcboGProblem()
        FillcboNProblem()
        FillDatasetAndDataview()
        FillDataGridView()
        FormName = "Work"
    End Sub

    Private Sub Work_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.Space Then
            Spc()
        End If
    End Sub

    Private Sub Spc()
        If maskDate.Focused = True Then
            maskDate.Text = ConvD.Class1.ConvDate
        End If
    End Sub
    Private Sub Work_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        maskDate.Text = ConvD.Class1.ConvDate

        txtCode.SendToBack()

        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Work_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub txtRow_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRow.GotFocus
        Me.txtRow.SelectionStart = 0
        Me.txtRow.SelectionLength = Len(Me.txtRow.Text)
    End Sub

    Private Sub txtRow_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRow.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtProblem.Focus()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtProblem_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProblem.TextChanged

    End Sub

    Private Sub cboGProblem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboGProblem.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.cboNProblem.Focus()
        End If
    End Sub

    Private Sub cboGProblem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGProblem.SelectedIndexChanged

    End Sub

    Private Sub cboGProblem_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGProblem.SelectedValueChanged
        If Me.cboGProblem.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillcboNProblem()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            maskDateEnd.Text = "1   /  /"
            txtDscr.Text = ""
            maskDateEnd.ReadOnly = True
            txtDscr.ReadOnly = True
        Else
            maskDateEnd.Text = ConvD.Class1.ConvDate
            maskDateEnd.ReadOnly = False
            txtDscr.ReadOnly = False
            maskDateEnd.Focus()
        End If
    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.GotFocus
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.cboGProblem.Focus()
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub cboNProblem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboNProblem.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtFamily.Focus()
        End If
    End Sub

    Private Sub cboNProblem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNProblem.SelectedIndexChanged

    End Sub

    Private Sub maskDateEnd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateEnd.GotFocus
        maskDateEnd.SelectionStart = 0
        maskDateEnd.SelectionLength = Len(maskDateEnd.Text)
    End Sub

    Private Sub maskDateEnd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateEnd.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtDscr.Focus()
        End If
    End Sub

    Private Sub maskDateEnd_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateEnd.MaskInputRejected

    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        Me.txtDscr.SelectionStart = 0
        Me.txtDscr.SelectionLength = Len(Me.txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnEdit.Focus()
        End If
    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillcboGProblem()
        FillcboNProblem()
        FillDatasetAndDataview()
        FillDataGridView()
        Clear()
        Label9.Text = "----------"
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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
                    X = DataGridView1.CurrentCell.RowIndex
                    .Range("A1").Value = "ردیف"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "تاریخ"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "گروه عملیات"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "سرفصل عملایات"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "شرح عملیات"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "تاریخ اتمام"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "شرح"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "نام کاربر"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "نام خانوادگی"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "کارشناس"
                    .Range("J1").Font.Bold = True

                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(15).Value
                            i += 1
                        End If
                    Next X
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgbOK.Label1.Text = " با اطلاعات فوق رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        Select Case ColumnSrch
            Case "6"
                objDataview.RowFilter = "Problem like '" & "**" & Me.txtSrch.Text & "**" & "'"
            Case "9"
                objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End Select
        FillDataGridView()
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        Me.txtFamily.SelectionStart = 0
        Me.txtFamily.SelectionLength = Len(Me.txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Code_Soft = 15
                FormName = "Work"
                FillPers()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                txtProblem.Focus()
            End If
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtCode.Clear()
            txtName.Clear()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub
End Class