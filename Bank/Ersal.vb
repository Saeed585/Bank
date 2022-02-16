Public Class Ersal
    Public Sub FillRow()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.ErsalTerminal", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.ErsalTerminal")
        objDataview = New DataView(objDataset.Tables("Bnk.ErsalTerminal"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            txtRow.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtRow.Text = 1
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub

    Private Sub FillCboArea()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Area_Code, Area_Name FROM bas.Area ORDER BY Area_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Area")
        Me.cboArea.DataSource = objDataset.Tables("bas.Area")
        Me.cboArea.DisplayMember = "Area_Name"
        Me.cboArea.ValueMember = "Area_Code"
    End Sub

    Private Sub FillcboModel()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Main_code, Main_name FROM Bas.MainBoard ORDER BY Main_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.MainBoard")
        Me.CboModel.DataSource = objDataset.Tables("Bas.MainBoard")
        Me.CboModel.DisplayMember = "Main_name"
        Me.CboModel.ValueMember = "Main_code"
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bnk.ErsalTerminal.Row, Bnk.ErsalTerminal.Dat, Bnk.ErsalTerminal.Area_code, Bas.Area.Area_name, Bnk.ErsalTerminal.main_code, Bas.MainBoard.Main_name, " & _
             " Bnk.ErsalTerminal.Count, Bnk.ErsalTerminal.Dscr FROM Bnk.ErsalTerminal INNER JOIN Bas.Area ON Bnk.ErsalTerminal.Area_code = Bas.Area.Area_code INNER JOIN" & _
             " Bas.MainBoard ON Bnk.ErsalTerminal.main_code = Bas.MainBoard.Main_code WHERE (Bnk.ErsalTerminal.Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.ErsalTerminal")
        objDataview = New DataView(objDataset.Tables("Bnk.ErsalTerminal"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).Visible = False
        Me.DataGridView1.Columns(3).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(4).Visible = False
        Me.DataGridView1.Columns(5).HeaderText = "مدل"
        Me.DataGridView1.Columns(6).HeaderText = "تعداد"
        Me.DataGridView1.Columns(7).HeaderText = "شرح"

        Me.DataGridView1.Columns(0).Width = 70
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(5).Width = 120
        Me.DataGridView1.Columns(6).Width = 50
        Me.DataGridView1.Columns(7).Width = 650
    End Sub

    Private Sub FillDataSetAndDataView2()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, RowSer, SerialNo, Sal, Shob_Code FROM Bnk.SerialErsal WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.SerialErsal")
        objDataview = New DataView(objDataset.Tables("Bnk.SerialErsal"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "RowSer"

        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).Visible = False
        Me.DataGridView2.Columns(1).Visible = False
        Me.DataGridView2.Columns(2).HeaderText = "سریال"
        Me.DataGridView2.Columns(3).Visible = False
        Me.DataGridView2.Columns(4).Visible = False

        Me.DataGridView2.Columns(2).Width = 120
    End Sub

    Private Sub FillDataGridViewSrch()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, RowSer, SerialNo, Sal, Shob_Code FROM Bnk.SerialErsal WHERE (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.SerialErsal")
        objDataview = New DataView(objDataset.Tables("Bnk.SerialErsal"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).Visible = True
        Me.DataGridView2.Columns(0).HeaderText = "برگه"
        Me.DataGridView2.Columns(1).Visible = False
        Me.DataGridView2.Columns(2).HeaderText = "سریال"
        Me.DataGridView2.Columns(3).Visible = False
        Me.DataGridView2.Columns(4).Visible = False

        Me.DataGridView2.Columns(0).Width = 30
        Me.DataGridView2.Columns(2).Width = 90
    End Sub
    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(1).Value
        cboArea.SelectedValue = DataGridView1.Rows(X).Cells(2).Value
        CboModel.SelectedValue = DataGridView1.Rows(X).Cells(4).Value
        txtCount.Text = DataGridView1.Rows(X).Cells(6).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(7).Value
        If txtCount.Text <> "" Then
            txtCount.ReadOnly = True
        End If
        InsertSerTmp()
    End Sub
    Private Sub Clear()
        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " DELETE FROM Bnk.SerialErsalTmp"
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

        FillRow()
        txtCount.Text = ""
        txtCount.ReadOnly = False
        txtDscr.Text = ""
        FillDataSetAndDataView2()
        txtCount.Focus()
    End Sub

    Private Sub InsertSerTmp()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " DELETE FROM Bnk.SerialErsalTmp"
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

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " INSERT INTO Bnk.SerialErsalTmp (Row, RowSer, SerialNo, Sal, Shob_Code)" & _
           " SELECT Row, RowSer, SerialNo, Sal, Shob_Code FROM Bnk.SerialErsal WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"
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
    End Sub
    Private Sub Ersal_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillCboArea()
        FillcboModel()
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub Ersal_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " DELETE FROM Bnk.SerialErsalTmp"
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
    End Sub

    Private Sub Ersal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        maskDate.Text = ConvD.Class1.ConvDate
    End Sub

    Private Sub Ersal_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
            FillDataSetAndDataView2()
            txtSrch.Text = ""
            CheckBox1.Checked = True
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
        FillDataSetAndDataView2()
        txtSrch.Text = ""
        CheckBox1.Checked = True
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        Clear()
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.GotFocus
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate.Focus()
                Exit Sub
            Else
                cboArea.Focus()
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub cboArea_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboArea.KeyPress
        If e.KeyChar = ChrW(13) Then
            CboModel.Focus()
        End If
    End Sub

    Private Sub cboArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArea.SelectedIndexChanged

    End Sub

    Private Sub CboModel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CboModel.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCount.Focus()
        End If
    End Sub

    Private Sub CboModel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboModel.SelectedIndexChanged

    End Sub

    Private Sub txtCount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount.GotFocus
        txtCount.SelectionStart = 0
        txtCount.SelectionLength = Len(txtCount.Text)
    End Sub

    Private Sub txtCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSerial.Focus()
        End If
    End Sub

    Private Sub txtCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCount.TextChanged

    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        txtDscr.SelectionStart = 0
        txtDscr.SelectionLength = Len(txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub btnSerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerial.Click
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCount.Focus()
            Exit Sub
        End If

        'Dim objDataAdapter0 As New SqlDataAdapter _
        '    (" SELECT RowSer, SerialNo, Sal, Shob_code FROM Bnk.SerialErsalTmp WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")", objConnection)
        'objDataset = New DataSet
        'objDataAdapter0.Fill(objDataset, "Bnk.SerialErsalTmp")
        'objDataview = New DataView(objDataset.Tables("Bnk.SerialErsalTmp"))
        'objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        'objDataview.Sort = "RowSer"

        'If objDataview.Count >= txtCount.Text Then
        '    MsgbYN.Label1.Text = "  جدول سریال پر میباشدآیا از حذف سریالهای وارد شده اطمینان دارید؟ "
        '    If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
        '        btnSerial.Focus()
        '        Exit Sub
        '    Else
        '        'objCommand.Connection = objConnection
        '        'objCommand.CommandText = _
        '        '   " DELETE FROM Bnk.SerialErsalTmp"
        '        'objCommand.CommandType = CommandType.Text

        '        'objConnection.Open()
        '        'Try
        '        '    objCommand.ExecuteNonQuery()
        '        'Catch SqlExceptionErr As SqlException
        '        '    MessageBox.Show(SqlExceptionErr.Message)
        '        '    objConnection.Close()
        '        '    Exit Sub
        '        'End Try
        '        'objConnection.Close()

        '        InsertSerTmp()
        '    End If
        'End If

        InsertSerTmp()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT SerialNo, RowSer, Sal, Shob_code FROM Bnk.SerialErsalTmp WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.SerialErsalTmp")
        objDataview = New DataView(objDataset.Tables("Bnk.SerialErsalTmp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "RowSer"

        If objDataview.Count = txtCount.Text Then
            InsertSerial.DataGridView2.AutoGenerateColumns = True
            InsertSerial.DataGridView2.DataSource = objDataview

            InsertSerial.DataGridView2.Columns(0).HeaderText = "سریال"
            InsertSerial.DataGridView2.Columns(1).HeaderText = "ردیف"
            InsertSerial.DataGridView2.Columns(2).Visible = False
            InsertSerial.DataGridView2.Columns(3).Visible = False

            InsertSerial.DataGridView2.Columns(0).Width = 150
            InsertSerial.DataGridView2.Columns(1).Width = 50

            ' InsertSerTmp()
            'Else
            '    If txtCount.Text > objDataview.Count Then
            '        Z = txtCount.Text - objDataview.Count

            '        InsertSerial.DataGridView2.AutoGenerateColumns = True
            '        InsertSerial.DataGridView2.DataSource = objDataview

            '        InsertSerial.DataGridView2.Columns(0).HeaderText = "سریال"
            '        InsertSerial.DataGridView2.Columns(1).HeaderText = "ردیف"
            '        InsertSerial.DataGridView2.Columns(2).Visible = False
            '        InsertSerial.DataGridView2.Columns(3).Visible = False

            '        Z = DataGridView2.RowCount + Z
            '        InsertSerial.DataGridView2.Columns(0).Width = 150
            '        InsertSerial.DataGridView2.Columns(1).Width = 50
            '    Else
            '        Z = objDataview.Count - txtCount.Text
            '    End If

            'MsgbYN.Label1.Text = "  تعداد ردیف سریالهای ثبت شده مربوط به شماره برگه فوق " & objDataview.Count & " میباشد و تعداد وارد شده آیا اطلاعات جدول حذف شود؟ " & txtCount.Text & " ؟ "
            'If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            '    btnSerial.Focus()
            '    Exit Sub
            'Else
            '    objCommand.Connection = objConnection
            '    objCommand.CommandText = _
            '       " DELETE FROM Bnk.SerialErsalTmp"
            '    objCommand.CommandType = CommandType.Text

            '    objConnection.Open()
            '    Try
            '        objCommand.ExecuteNonQuery()
            '    Catch SqlExceptionErr As SqlException
            '        MessageBox.Show(SqlExceptionErr.Message)
            '        objConnection.Close()
            '        Exit Sub
            '    End Try
            '    objConnection.Close()

            '    InsertSerTmp()
            'End If

            'objCommand.Connection = objConnection
            'objCommand.CommandText = _
            '   " DELETE FROM Bnk.SerialErsalTmp"
            'objCommand.CommandType = CommandType.Text

            'objConnection.Open()
            'Try
            '    objCommand.ExecuteNonQuery()
            'Catch SqlExceptionErr As SqlException
            '    MessageBox.Show(SqlExceptionErr.Message)
            '    objConnection.Close()
            '    Exit Sub
            'End Try
            'objConnection.Close()

            'objCommand.Connection = objConnection
            'objCommand.CommandText = _
            '   " DELETE FROM Bnk.SerialErsal WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"

            'objConnection.Open()
            'Try
            '    objCommand.ExecuteNonQuery()
            'Catch SqlExceptionErr As SqlException
            '    MessageBox.Show(SqlExceptionErr.Message)
            '    objConnection.Close()
            '    Exit Sub
            'End Try
            'objConnection.Close()

            'Dim objDataAdapter1 As New SqlDataAdapter _
            '    (" SELECT SerialNo, RowSer, Sal, Shob_code FROM Bnk.SerialErsal WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")", objConnection)
            'objDataset = New DataSet
            'objDataAdapter1.Fill(objDataset, "Bnk.SerialErsal")
            'objDataview = New DataView(objDataset.Tables("Bnk.SerialErsal"))
            'objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            'objDataview.Sort = "SerialNo"
        End If
        FormName = "Ersal"
        InsertSerial.Show()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCount.Focus()
            Exit Sub
        ElseIf maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtCount.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                '--------------------------------------حذف سریال از جدول SerialErsal
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM Bnk.SerialErsal" & _
                   " WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"
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

                FillDataSetAndDataView()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdErsal"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Acode", cboArea.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@main", CboModel.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Sal", Sal)
            objCommand.Parameters.AddWithValue("@shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCount.Focus()
            Exit Sub
        ElseIf maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            ' FillRow()
            Btn = 1
            InsertUpdate()

            '--------------------------------------حذف سریال از جدول SerialErsal
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " DELETE FROM Bnk.SerialErsal" & _
               " WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"
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
            '--------------------------------------ثبت سریال در جدول SerialErsal
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO Bnk.SerialErsal(Row, RowSer, SerialNo, Sal, Shob_Code)" & _
               " SELECT Row, RowSer, SerialNo, Sal, Shob_Code FROM Bnk.SerialErsalTmp"
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

            FillDataSetAndDataView()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If txtCount.Text = "" Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCount.Focus()
            Exit Sub
        ElseIf maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtRow.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            ' FillRow()
            Btn = 2
            InsertUpdate()

            '--------------------------------------حذف سریال از جدول SerialErsal
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " DELETE FROM Bnk.SerialErsal" & _
               " WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"
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
            '--------------------------------------ثبت سریال در جدول SerialErsal
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO Bnk.SerialErsal(Row, RowSer, SerialNo, Sal, Shob_Code)" & _
               " SELECT Row, RowSer, SerialNo, Sal, Shob_Code FROM Bnk.SerialErsalTmp"
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

            FillDataSetAndDataView()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Clear()
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
                    .Range("A1").Value = "تاریخ"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "محل فعالیت"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "مدل"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "تعداد"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "شرح"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "سریال"
                    .Range("F1").Font.Bold = True

                    .Range("A" & i.ToString).AutoFormat()
                    X = Me.DataGridView1.CurrentCell.RowIndex
                    .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                    .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                    .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                    .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                    .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value

                    X = Me.DataGridView2.CurrentCell.RowIndex
                    i += 1
                    For X = 0 To DataGridView2.RowCount - 1
                        .Range("F" & i.ToString).Value = DataGridView2.Rows(X).Cells(0).Value
                        i += 1
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

    Private Sub txtSrch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.Click
        FillDataGridViewSrch()
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDataGridViewSrch()
        objDataview.RowFilter = "serialno like '" & "**" & Me.txtSrch.Text & "**" & "'"
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseDoubleClick
        Dim X As Integer
        Dim Sal As String
        Dim Ser As String
        Dim Rws As String
        Dim Cnt As String

        X = DataGridView2.CurrentCell.RowIndex
        Rws = Me.DataGridView2.Rows(X).Cells(1).Value
        Ser = Me.DataGridView2.Rows(X).Cells(2).Value
        Sal = Me.DataGridView2.Rows(X).Cells(3).Value
        Shob = Me.DataGridView2.Rows(X).Cells(4).Value

        If Me.DataGridView2.Columns(0).Visible = False Then
            If CheckBox1.Checked = True Then
                MsgbYN.Label1.Text = "  آیا می خواهید شماره سریال  " & Ser & "  از سیستم حذف شود  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    DataGridView2.Focus()
                    Exit Sub
                Else
                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                       " DELETE FROM Bnk.SerialErsal WHERE (Row = " & txtRow.Text & ") AND (RowSer = " & Rws & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"

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
            Else
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM Bnk.SerialErsal WHERE (Row = " & txtRow.Text & ") AND (RowSer = " & Rws & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"

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

            FillDataSetAndDataView2()
            Cnt = DataGridView2.RowCount - 1
            'Dim Count As String
            'Count = DataGridView2.RowCount
            If Cnt > 0 Then
                For X = 0 To Cnt
                    Rws = X + 1

                    Ser = Me.DataGridView2.Rows(X).Cells(2).Value

                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                       " UPDATE Bnk.SerialErsal SET RowSer = " & Rws & " WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ") AND (SerialNo = N'" & Ser & "')"
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

                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                       " UPDATE Bnk.ErsalTerminal SET Count = " & Cnt + 1 & " WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"
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
                    txtCount.Text = DataGridView2.RowCount
                Next
            Else
                Cnt = DataGridView2.RowCount
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " UPDATE Bnk.ErsalTerminal SET Count = " & Cnt & " WHERE (Row = " & txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"
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
                txtCount.Text = DataGridView2.RowCount
            End If

            InsertSerTmp()
            FillDataSetAndDataView()
            FillDataGridView()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class