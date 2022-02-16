Public Class Mojoodi
    Dim ColumnSrch As Integer
    Public Sub FillRow()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.Mojoodi WHERE (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            txtRow.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtRow.Text = Sal & "00001"
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub

    Public Sub FillRowMove()
        Dim ShMove As String
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        ShMove = cboShobMove.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.Mojoodi WHERE (Sal = " & Sal & ") AND (Shob = " & ShMove & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            txtRow.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtRow.Text = Sal & "00001"
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub

    Public Sub FillRowUsedkala()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.UsedKala WHERE (Sal = " & Sal & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.UsedKala")
        objDataview = New DataView(objDataset.Tables("Bnk.UsedKala"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            txtRowUsedKala.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtRowUsedKala.Text = Sal & "00001"
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub

    Public Sub FillCboShobMove()
        Dim a As String
        a = 0
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE(Shob_Code <> 0) ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Shob")
        objDataview = New DataView(objDataset.Tables("bas.Shob"))

        cboShobMove.DataSource = objDataset.Tables("bas.Shob")
        cboShobMove.DisplayMember = "Shob_Name"
        cboShobMove.ValueMember = "Shob_Code"
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim Da As New SqlDataAdapter

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandText = ("Select * From Bnk.View_Mojoodi WHERE (Shob =" & Shob & ") ORDER BY Row")

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Bnk.View_Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.View_Mojoodi"))
        ' objDataview.Sort = "Row"
        objConnection.Close()

        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "کد"
        Me.DataGridView1.Columns(3).HeaderText = "نام کالا"
        Me.DataGridView1.Columns(4).HeaderText = "تعداد اولیه"
        Me.DataGridView1.Columns(5).HeaderText = "تعداد موجودی"
        Me.DataGridView1.Columns(6).HeaderText = "شماره قفسه"
        Me.DataGridView1.Columns(7).HeaderText = "شرح"
        Me.DataGridView1.Columns(8).Visible = False ' Chk
        Me.DataGridView1.Columns(9).Visible = False ' Shob
        Me.DataGridView1.Columns(10).Visible = False ' Mande TamirKala
        Me.DataGridView1.Columns(11).HeaderText = "شماره برگه تعمیر"

        Me.DataGridView1.Columns(0).Width = 80
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 100
        Me.DataGridView1.Columns(3).Width = 300
        Me.DataGridView1.Columns(4).Width = 50
        Me.DataGridView1.Columns(5).Width = 50
        Me.DataGridView1.Columns(6).Width = 50
        Me.DataGridView1.Columns(7).Width = 500

        RowColorHighLight()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(1).Value
        txtCode.Text = DataGridView1.Rows(X).Cells(2).Value
        txtName.Text = DataGridView1.Rows(X).Cells(3).Value
        txtCount.Text = DataGridView1.Rows(X).Cells(4).Value
        txtCountOld.Text = DataGridView1.Rows(X).Cells(4).Value
        txtCountOk.Text = DataGridView1.Rows(X).Cells(5).Value
        txtCountOkOld.Text = DataGridView1.Rows(X).Cells(5).Value
        txtBox.Text = DataGridView1.Rows(X).Cells(6).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(7).Value
        lblKala.Text = txtName.Text
    End Sub

    Private Sub RowColorHighLight()
        Dim X As Integer
        Dim Cnt As String
        Dim Chk As String

        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            Chk = 0
            If Not IsDBNull(DataGridView1.Rows(X).Cells(8).Value) Then 'Highlight
                Chk = DataGridView1.Rows(X).Cells(8).Value
                If Chk = 0 Then
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
                End If
            Else
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
            End If

            If DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White Then
                Chk = 0
                If Not IsDBNull(DataGridView1.Rows(X).Cells(10).Value) Then 'TamirKala
                    Chk = DataGridView1.Rows(X).Cells(10).Value
                    If Chk = 0 Then
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                    Else
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Aqua
                    End If
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            End If

            If DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White Then
                Chk = 0
                If Not IsDBNull(DataGridView1.Rows(X).Cells(5).Value) Then 'تعداد موجودی
                    Chk = DataGridView1.Rows(X).Cells(5).Value
                    If Chk > 0 Then
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                    Else
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Pink
                    End If
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            End If
        Next
    End Sub

    Private Sub Clear()
        txtName.Text = ""
        txtCount.Text = 0
        txtCountOk.Text = 0
        txtBox.Text = 0
        txtDscr.Text = ""
        txtName.Focus()
    End Sub

    Private Sub Mojoodi_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillCboShobMove()
        FillDataSetAndDataView()
        FillDataGridView()
        Clear()
        FormName = "Mojoodi"
    End Sub

    Private Sub HighLight()
        Dim X As Integer
        Dim Chk As String
        Dim Rw As String

        X = DataGridView1.CurrentCell.RowIndex
        Rw = DataGridView1.Rows(X).Cells(0).Value

        If DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow Then
            Chk = 0
            DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
        Else
            Chk = 1
            DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
        End If

        objCommand.Connection = objConnection
        objCommand.CommandText =
           " UPDATE Bnk.Mojoodi SET Chk = @Chk WHERE (Row = " & Rw & ")"
        objCommand.CommandType = CommandType.Text
        objCommand.Parameters.Clear()
        objCommand.Parameters.AddWithValue("@Chk", Chk)

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

    Private Sub Mojoodi_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.F5 Then
            HighLight()
        ElseIf e.KeyCode = Keys.Insert Then
            FillRow()
            Clear()
        ElseIf e.KeyCode = Keys.Space Then
            Spc()
        End If
    End Sub

    Private Sub Spc()
        If maskDate.Focused = True Then
            maskDate.Text = ConvD.Class1.ConvDate
        ElseIf maskDates.Focused = True Then
            maskDateS.Text = ConvD.Class1.ConvDate
        ElseIf maskDateMove.Focused = True Then
            maskDateMove.Text = ConvD.Class1.ConvDate
        End If
    End Sub
    Private Sub Mojoodi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        maskDate.Text = ConvD.Class1.ConvDate
        txtCountOld.SendToBack()
        txtCountOldS.SendToBack()
        txtCountOkOld.SendToBack()
        txtRowUsedKala.SendToBack()
        txtChk.SendToBack()
        txt1.SendToBack()
        Panel4.SendToBack()

        Me.KeyPreview = True
        FormName = "Mojoodi"
    End Sub

    Private Sub Mojoodi_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 305
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub SaveLogMojoodi()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " INSERT INTO [Log].MojoodiLog (Row, Dat, Kala_code, Count, CountOk, Box, Dscr, Chk, Shob, Sal)" &
           " SELECT Row, Dat, kala_code, Count, CountOk, Box, Dscr, Chk, Shob, Sal FROM Bnk.Mojoodi WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")"

        objConnection.Open()
        Try
            objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        Tim = Mid(Mainfrm.lblTime.Text, 1, 8)
        Dat = ConvD.Class1.ConvDate()
        PcName = My.Computer.Name

        objcommand.Connection = objConnection
        objcommand.CommandText =
            " UPDATE [Log].MojoodiLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" &
            " WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ") AND (tim_stat IS NULL)"
        objConnection.Open()
        Try
            objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub InsertUpdate()
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdMojoodi"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Kcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
            objCommand.Parameters.AddWithValue("@CountOk", txtCountOk.Text)
            objCommand.Parameters.AddWithValue("@Box", txtBox.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Chk", 0)
            objCommand.Parameters.AddWithValue("@Tag", 0)
            objCommand.Parameters.AddWithValue("@shob", Shob)
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
        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        MojoodiCount()

        Dim objDataAdapter0 As New SqlDataAdapter _
            (" SELECT Row, Shob FROM Bnk.Mojoodi WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter0.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Kala_code, Shob FROM Bnk.Mojoodi WHERE (Kala_code = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Kala_code"

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " کد کالای " & Trim(txtCode.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()
            '--------LogMojoodi
            logStat = 1
            SaveLogMojoodi()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Kala_code, Shob FROM Bnk.Mojoodi WHERE (Kala_code = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Kala_code"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()
            '--------LogMojoodi
            logStat = 2
            SaveLogMojoodi()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        If maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtName.Focus()
            Exit Sub
        End If

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Kala_code, Shob FROM Bnk.Mojoodi WHERE (Kala_code = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Kala_code"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT Tag FROM Bnk.Mojoodi WHERE (Row = " & txtRow.Text & ") AND (Kala_code = " & txtCode.Text & ") AND (Shob = " & Shob & ") AND (Tag = 1)", objConnection)
            objDataset = New DataSet
            objDataAdapter1.Fill(objDataset, "Bnk.Mojoodi")
            objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Tag"

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق جزو کالاهای انتقالی بوده و قابل حذف نمی باشد . "
                MsgbOK.ShowDialog()
                Me.txtName.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtName.Focus()
                Exit Sub
            Else
                '--------LogMojoodi
                logStat = 3
                SaveLogMojoodi()
                '-----------------
                Btn = 3
                InsertUpdate()

                Ref.PerformClick()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub
    Private Sub ReadSabtMohoodi()
        Dim X As Integer
        Dim Code As String
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        X = DataGridView1.CurrentCell.RowIndex
        Code = DataGridView1.Rows(X).Cells(2).Value

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Dat, Count, Dscr, Serial FROM Bnk.SabtMojoodi WHERE (Kala_code = " & Code & ") AND (Shob = " & Shob & ") ORDER BY Dat", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.SabtMojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.SabtMojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Dat"

        Me.DataGridView2.AutoGenerateColumns = True
        ' Me.DataGridView2.DataSource = ""
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).HeaderText = "تاریخ"
        Me.DataGridView2.Columns(1).HeaderText = "تعداد"
        Me.DataGridView2.Columns(2).HeaderText = "شرح"
        Me.DataGridView2.Columns(3).HeaderText = "شماره سریال"

        Me.DataGridView2.Columns(0).Width = 70
        Me.DataGridView2.Columns(1).Width = 60
        Me.DataGridView2.Columns(2).Width = 350
        Me.DataGridView2.Columns(3).Width = 150
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.RowCount > 0 Then
            If e.ColumnIndex = 2 Then
                ColumnSrch = e.ColumnIndex
                Panel1.Visible = True
                ReadSabtMohoodi()
            End If
        End If
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        Label7.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
        If ColumnSrch = 3 Or ColumnSrch = 7 Then
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        Else
            Me.txtSrch.Text = ""
            Label7.Text = "-----"
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.SelectionStart = 0
        txtName.SelectionLength = Len(txtName.Text)
    End Sub

    Private Sub maskDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate.Focus()
                Exit Sub
            Else
                txtName.Focus()
            End If
        End If
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillCboShobMove()
        FillDataSetAndDataView()
        FillDataGridView()
        RowColorHighLight()
        Clear()
        FormName = "Mojoodi"
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FillRow()
        Clear()
        lblKala.Text = ""
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDataSetAndDataView()
        Select Case ColumnSrch
            Case "3"
                objDataview.RowFilter = "Kala_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
            Case "7"
                objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End Select
        FillDataGridView()
        RowColorHighLight()
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub maskDate_GotFocus1(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDate.GotFocus
        maskDate.SelectionStart = 0
        maskDate.SelectionLength = Len(maskDate.Text)
    End Sub

    Private Sub maskDate_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDate.Text
            Chkdate()
            If Datstat = 1 Then
                maskDate.Focus()
                Exit Sub
            Else
                txtName.Focus()
            End If
        End If
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text = "" Then
                Code_Soft = 15
                FormName = "Mojoodi"
                FillKala()
                FillDataKala()
                Srch.lblName.Text = "نام کالا"
                Srch.ShowDialog()
            Else
                txtCount.Focus()
            End If
        End If
    End Sub

    Private Sub txtCount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount.GotFocus
        txtCount.SelectionStart = 0
        txtCount.SelectionLength = Len(txtCount.Text)
    End Sub

    Private Sub txtCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtBox.Focus()
        End If
    End Sub

    Private Sub txtDscr_GotFocus1(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        txtDscr.SelectionStart = 0
        txtDscr.SelectionLength = Len(txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
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
                    .Range("A1").Value = "شماره برگه"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "تاریخ"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "کد"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نام کالا"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "تعداد اولیه"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "تعداد موجودی"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "شماره قفسه"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "شرح"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "شماره برگه تعمیر"
                    .Range("I1").Font.Bold = True

                    ' .Range("A" & i.ToString).AutoFormat()
                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
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

    Private Sub txtBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBox.GotFocus
        txtBox.SelectionStart = 0
        txtBox.SelectionLength = Len(txtBox.Text)
    End Sub

    Private Sub txtBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBox.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBox.TextChanged
        If txtBox.Text = "" Then
            txtBox.Text = 0
        End If
    End Sub

    Private Sub MojoodiCount()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtCode.Text <> "" Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Kala_code, Shob FROM Bnk.Mojoodi WHERE (Kala_code = " & txtCode.Text & ") AND (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
            objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Kala_code"

            If objDataview.Count = 0 Then
                txtCountOk.Text = txtCount.Text
            ElseIf objDataview.Count > 0 Then
                txtCountOk.Text = Val(txtCountOkOld.Text) + (Val(txtCount.Text) - Val(txtCountOld.Text))
            End If
        End If
    End Sub

    Private Sub txtCount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCount.LostFocus
        MojoodiCount()
    End Sub

    Private Sub txtCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCount.TextChanged
        If txtCount.Text = "" Then
            txtCount.Text = 0
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCode.Text = ""
        End If
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        If Panel3.Visible = False Then
            Panel3.Visible = True
            maskDateMove.Text = ConvD.Class1.ConvDate
            txtCountMove.Text = 0
            cboShobMove.Focus()
        Else
            Panel3.Visible = False
            maskDateMove.Text = "1   /  /"
        End If
    End Sub

    Private Sub cboShobMove_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboShobMove.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCountMove.Focus()
        End If
    End Sub

    Private Sub cboShobMove_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboShobMove.SelectedIndexChanged

    End Sub

    Private Sub txtCountMove_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountMove.GotFocus
        txtCountMove.SelectionStart = 0
        txtCountMove.SelectionLength = Len(txtCountMove.Text)
    End Sub

    Private Sub txtCountMove_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCountMove.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDateMove.Focus()
        End If
    End Sub

    Private Sub txtCountMove_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountMove.TextChanged

    End Sub

    Private Sub maskDateMove_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateMove.GotFocus
        maskDateMove.SelectionStart = 0
        maskDateMove.SelectionLength = Len(maskDateMove.Text)
    End Sub

    Private Sub maskDateMove_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateMove.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSabtMove.Focus()
        End If
    End Sub

    Private Sub btnSabtMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSabtMove.Click
        Dim X As Integer
        Dim X1 As Integer
        Dim Rw As String
        Dim Rw1 As String
        Dim Code As String
        Dim Count As String
        Dim CountOk As String
        Dim ShMove As String
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        ShMove = cboShobMove.SelectedValue.ToString

        If ShMove <> Shob Then
            ' For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.RowCount > 0 Then
                Rw = DataGridView1.Rows(X).Cells(0).Value
                Code = DataGridView1.Rows(X).Cells(2).Value
                Count = DataGridView1.Rows(X).Cells(4).Value
                CountOk = DataGridView1.Rows(X).Cells(5).Value

                If txtCountMove.Text = 0 Then
                    MsgbOK.Label1.Text = " تعداد کالای انتقالی را مشخص کنید . "
                    MsgbOK.ShowDialog()
                    txtCountMove.Focus()
                    Exit Sub
                ElseIf Val(txtCountMove.Text) > Val(CountOk) Then
                    MsgbOK.Label1.Text = " تعداد کالای انتقالی بزرگتر از تعداد موجودی میباشد . "
                    MsgbOK.ShowDialog()
                    txtCountMove.Focus()
                    Exit Sub
                End If

                Dim objDataAdapter As New SqlDataAdapter _
                    (" SELECT Kala_code, Shob FROM Bnk.Mojoodi WHERE (Kala_code = " & Code & ") AND (Shob = " & ShMove & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
                objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Kala_code"
                If objDataview.Count = 0 Then
                    '------------------------------ثبت در فرم موجودی شعبه مقصد
                    FillRowMove()
                    Try
                        objConnection.Open()
                        objCommand.Connection = objConnection
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Bnk.InsUpdMojoodi"
                        objCommand.Parameters.Clear()
                        objCommand.Parameters.AddWithValue("@Btn", 1)
                        objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                        objCommand.Parameters.AddWithValue("@Dat", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Kcode", Code)
                        objCommand.Parameters.AddWithValue("@Count", txtCountMove.Text)
                        objCommand.Parameters.AddWithValue("@CountOk", txtCountMove.Text)
                        objCommand.Parameters.AddWithValue("@Box", 0)
                        objCommand.Parameters.AddWithValue("@Dscr", "انتقال از " & Mainfrm.cboShob.Text & "")
                        objCommand.Parameters.AddWithValue("@Chk", 0)
                        objCommand.Parameters.AddWithValue("@Tag", 1)
                        objCommand.Parameters.AddWithValue("@Shob", ShMove)
                        objCommand.Parameters.AddWithValue("@Sal", Sal)

                        objCommand.ExecuteNonQuery()
                    Catch Ex As Exception
                        MessageBox.Show(Ex.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    '------------------------------ثبت به عنوان استفاده شده در شعبه مبداً
                    FillRowUsedkala()
                    Try
                        objConnection.Open()
                        objCommand.Connection = objConnection
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Bnk.InsUpdUsedKala"
                        objCommand.Parameters.Clear()
                        objCommand.Parameters.AddWithValue("@Btn", 1)
                        objCommand.Parameters.AddWithValue("@Row", txtRowUsedKala.Text)
                        objCommand.Parameters.AddWithValue("@RowMoj", Rw)
                        objCommand.Parameters.AddWithValue("@Dat", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Code", Code)
                        objCommand.Parameters.AddWithValue("@Knd", 1)
                        objCommand.Parameters.AddWithValue("@Area", 0)
                        objCommand.Parameters.AddWithValue("@Countout", txtCountMove.Text)
                        objCommand.Parameters.AddWithValue("@CountOk", Val(CountOk) - Val(txtCountMove.Text))
                        objCommand.Parameters.AddWithValue("@Datout", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Countin", 0)
                        objCommand.Parameters.AddWithValue("@Datin", "1   /  /")
                        objCommand.Parameters.AddWithValue("@Dscr", "انتقال به " & cboShobMove.Text & "")
                        objCommand.Parameters.AddWithValue("@Soorat", 0)
                        objCommand.Parameters.AddWithValue("@DatSoorat", "1   /  /")
                        objCommand.Parameters.AddWithValue("@Tag", 1)
                        objCommand.Parameters.AddWithValue("@Shob", Shob)
                        objCommand.Parameters.AddWithValue("@Sal", Sal)

                        objCommand.ExecuteNonQuery()
                    Catch Ex As Exception
                        MessageBox.Show(Ex.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    '------------------------------ثبت شرح
                    Try
                        objConnection.Open()
                        objCommand.Connection = objConnection
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Bnk.InsUpdMojoodiDscr"
                        objCommand.Parameters.Clear()
                        objCommand.Parameters.AddWithValue("@Btn", 1)
                        objCommand.Parameters.AddWithValue("@Row", Rw)
                        objCommand.Parameters.AddWithValue("@Dat", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Kcode", Code)
                        objCommand.Parameters.AddWithValue("@Count", txtCountMove.Text)
                        objCommand.Parameters.AddWithValue("@Dscr", "انتقال از " & Mainfrm.cboShob.Text & " به " & cboShobMove.Text & "")
                        objCommand.Parameters.AddWithValue("@Ucode", User_Code)
                        objCommand.Parameters.AddWithValue("@Shob", Shob)
                        objCommand.Parameters.AddWithValue("@Sal", Sal)

                        objCommand.ExecuteNonQuery()
                    Catch Ex As Exception
                        MessageBox.Show(Ex.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    '--------LogMojoodi
                    'logStat = 1
                    'SaveLogMojoodi()

                    Ref.PerformClick()
                ElseIf objDataview.Count > 0 Then
                    '------------------------------اصلاح در فرم موجودی شعبه مقصد
                    Dim objDataAdapter1 As New SqlDataAdapter _
                        (" SELECT Row, Count, CountOk, Box, Chk, Dscr FROM Bnk.Mojoodi WHERE (Kala_code = " & Code & ") AND (Shob = " & ShMove & ")", objConnection)
                    objDataset = New DataSet
                    objDataAdapter1.Fill(objDataset, "Bnk.Mojoodi")
                    objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
                    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                    objDataview.Sort = "Row"

                    txtRow.Text = 0
                    txtRow.DataBindings.Clear()
                    txtRow.DataBindings.Add("Text", objDataview, "Row")
                    txtCount.Text = 0
                    txtCount.DataBindings.Clear()
                    txtCount.DataBindings.Add("Text", objDataview, "Count")
                    txtCountOk.Text = 0
                    txtCountOk.DataBindings.Clear()
                    txtCountOk.DataBindings.Add("Text", objDataview, "CountOk")
                    txtBox.Text = 0
                    txtBox.DataBindings.Clear()
                    txtBox.DataBindings.Add("Text", objDataview, "Box")
                    txtChk.Text = 0
                    txtChk.DataBindings.Clear()
                    txtChk.DataBindings.Add("Text", objDataview, "Chk")
                    txtDscr.Text = 0
                    txtDscr.DataBindings.Clear()
                    txtDscr.DataBindings.Add("Text", objDataview, "Dscr")

                    Try
                        objConnection.Open()
                        objCommand.Connection = objConnection
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Bnk.InsUpdMojoodi"
                        objCommand.Parameters.Clear()
                        objCommand.Parameters.AddWithValue("@Btn", 2)
                        objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                        objCommand.Parameters.AddWithValue("@Dat", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Kcode", Code)
                        objCommand.Parameters.AddWithValue("@Count", Val(txtCount.Text))
                        objCommand.Parameters.AddWithValue("@CountOk", Val(txtCountOk.Text) + Val(txtCountMove.Text))
                        objCommand.Parameters.AddWithValue("@Box", txtBox.Text)
                        objCommand.Parameters.AddWithValue("@Dscr", "انتقال از " & Mainfrm.cboShob.Text & "")
                        objCommand.Parameters.AddWithValue("@Chk", txtChk.Text)
                        objCommand.Parameters.AddWithValue("@Tag", 1)
                        objCommand.Parameters.AddWithValue("@shob", ShMove)
                        objCommand.Parameters.AddWithValue("@Sal", Sal)

                        objCommand.ExecuteNonQuery()
                    Catch Ex As Exception
                        MessageBox.Show(Ex.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    '------------------------------ثبت به عنوان استفاده شده در شعبه مبداً
                    FillRowUsedkala()
                    Try
                        objConnection.Open()
                        objCommand.Connection = objConnection
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Bnk.InsUpdUsedKala"
                        objCommand.Parameters.Clear()
                        objCommand.Parameters.AddWithValue("@Btn", 1)
                        objCommand.Parameters.AddWithValue("@Row", txtRowUsedKala.Text)
                        objCommand.Parameters.AddWithValue("@RowMoj", Rw)
                        objCommand.Parameters.AddWithValue("@Dat", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Code", Code)
                        objCommand.Parameters.AddWithValue("@Knd", 1)
                        objCommand.Parameters.AddWithValue("@Area", 0)
                        objCommand.Parameters.AddWithValue("@Countout", txtCountMove.Text)
                        objCommand.Parameters.AddWithValue("@CountOk", Val(CountOk) - Val(txtCountMove.Text))
                        objCommand.Parameters.AddWithValue("@Datout", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Countin", 0)
                        objCommand.Parameters.AddWithValue("@Datin", "1   /  /")
                        objCommand.Parameters.AddWithValue("@Dscr", "انتقال به " & cboShobMove.Text & "")
                        objCommand.Parameters.AddWithValue("@Soorat", 0)
                        objCommand.Parameters.AddWithValue("@DatSoorat", "1   /  /")
                        objCommand.Parameters.AddWithValue("@Tag", 1)
                        objCommand.Parameters.AddWithValue("@Shob", Shob)
                        objCommand.Parameters.AddWithValue("@Sal", Sal)

                        objCommand.ExecuteNonQuery()
                    Catch Ex As Exception
                        MessageBox.Show(Ex.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    '------------------------------ثبت شرح
                    X1 = DataGridView1.CurrentCell.RowIndex
                    Rw1 = DataGridView1.Rows(X1).Cells(0).Value
                    Try
                        objConnection.Open()
                        objCommand.Connection = objConnection
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Bnk.InsUpdMojoodiDscr"
                        objCommand.Parameters.Clear()
                        objCommand.Parameters.AddWithValue("@Btn", 1)
                        objCommand.Parameters.AddWithValue("@Row", Rw1)
                        objCommand.Parameters.AddWithValue("@Dat", maskDateMove.Text)
                        objCommand.Parameters.AddWithValue("@Kcode", Code)
                        objCommand.Parameters.AddWithValue("@Count", txtCountMove.Text)
                        objCommand.Parameters.AddWithValue("@Dscr", "انتقال از " & Mainfrm.cboShob.Text & " به " & cboShobMove.Text & "")
                        objCommand.Parameters.AddWithValue("@Ucode", User_Code)
                        objCommand.Parameters.AddWithValue("@Shob", Shob)
                        objCommand.Parameters.AddWithValue("@Sal", Sal)

                        objCommand.ExecuteNonQuery()
                    Catch Ex As Exception
                        MessageBox.Show(Ex.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    '--------LogMojoodi
                    'logStat = 2
                    'SaveLogMojoodi()
                End If
            End If
            ' Next

            Panel3.Visible = False
            Ref.PerformClick()
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub btnKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKala.Click
        Code_Soft = 1
        Code_Form = 1117
        Name_Form = "INkala"

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Sav, Edt, Del, Sayer FROM Bas.Form_Sec WHERE (U_Code = " & User_Code & ") AND (F_Code = " & Code_Form & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Form_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Form_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        DataGridView1.ClearSelection()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ذخیره"
        Me.DataGridView1.Columns(1).HeaderText = "اصلاح"
        Me.DataGridView1.Columns(2).HeaderText = "حذف"
        Me.DataGridView1.Columns(3).HeaderText = "سایر"

        If Not IsDBNull(DataGridView1.Rows(0).Cells(0).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(0).Value
            If Trim(txt1.Text) = "True" Then
                INKala.btnSave.Enabled = True
            Else
                INKala.btnSave.Enabled = False
            End If
        Else
            INKala.btnSave.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(1).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(1).Value
            If Trim(txt1.Text) = "True" Then
                INKala.btnEdit.Enabled = True
            Else
                INKala.btnEdit.Enabled = False
            End If
        Else
            INKala.btnEdit.Enabled = False
        End If
        txt1.Text = ""

        If Not IsDBNull(DataGridView1.Rows(0).Cells(2).Value) Then
            txt1.Text = DataGridView1.Rows(0).Cells(2).Value
            If Trim(txt1.Text) = "True" Then
                INKala.btnDelete.Enabled = True
            Else
                INKala.btnDelete.Enabled = False
            End If
        Else
            INKala.btnDelete.Enabled = False
        End If
        txt1.Text = ""
        ' savCodeForm()
        INKala.Show()
        INKala.Activate()
    End Sub

    Private Sub btnExitP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitP.Click
        objDataview.Dispose()
        Panel1.Visible = False
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub btnMoj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoj.Click
        Panel2.Visible = True
        txtDscrS.Text = ""
        txtCountS.Text = 0
        txtCountOldS.Text = 0
        maskDateS.Text = ConvD.Class1.ConvDate
        lblKala.Text = txtName.Text

        FillDataSetAndDataViewSabt()
        FillDataGridViewSabt()
        txtDscrS.Focus()
    End Sub

    Private Sub btnExitS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitS.Click
        Panel2.Visible = False
    End Sub

    Private Sub maskDateS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateS.GotFocus
        maskDateS.SelectionStart = 0
        maskDateS.SelectionLength = Len(maskDateS.Text)
    End Sub

    Private Sub maskDateS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateS.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateS.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateS.Focus()
                Exit Sub
            Else
                txtDscrS.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateS_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateS.MaskInputRejected

    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub txtDscrS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscrS.GotFocus
        txtDscrS.SelectionStart = 0
        txtDscrS.SelectionLength = Len(txtDscrS.Text)
    End Sub

    Private Sub txtDscrS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrS.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCountS.Focus()
        End If
    End Sub

    Private Sub txtDscrS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrS.TextChanged

    End Sub

    Private Sub txtCountS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountS.GotFocus
        txtCountS.SelectionStart = 0
        txtCountS.SelectionLength = Len(txtCountS.Text)
    End Sub

    Private Sub txtCountS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCountS.KeyPress
        If e.KeyChar = ChrW(13) Then
            CheckBox1.Focus()
        End If
    End Sub

    Private Sub txtCountS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountS.TextChanged
        If txtCountS.Text = "" Then
            txtCountS.Text = 0
        End If
    End Sub

    Private Sub FillDataSetAndDataViewSabt()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bnk.SabtMojoodi.Dat, Bnk.SabtMojoodi.Kala_code, Bas.Kala.kala_Name, Bnk.SabtMojoodi.Count, Bnk.SabtMojoodi.Dscr, Bnk.SabtMojoodi.ChkSerial, Bnk.SabtMojoodi.Serial" &
            " FROM Bnk.SabtMojoodi INNER JOIN Bas.Kala ON Bnk.SabtMojoodi.Kala_code = Bas.Kala.Kala_Code WHERE (Bnk.SabtMojoodi.Shob = " & Shob & ") ORDER BY Bnk.SabtMojoodi.Dat", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.SabtMojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.SabtMojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Dat"
    End Sub

    Private Sub FillDataGridViewSabt()
        Me.DataGridView3.AutoGenerateColumns = True
        Me.DataGridView3.DataSource = objDataview

        Me.DataGridView3.Columns(0).HeaderText = "تاریخ"
        Me.DataGridView3.Columns(1).Visible = False 'kcode
        Me.DataGridView3.Columns(2).HeaderText = "نام کالا"
        Me.DataGridView3.Columns(3).HeaderText = "تعداد"
        Me.DataGridView3.Columns(4).HeaderText = "شرح"
        Me.DataGridView3.Columns(5).Visible = False 'ChkSerial
        Me.DataGridView3.Columns(6).HeaderText = "شماره سریال"

        Me.DataGridView3.Columns(0).Width = 70
        Me.DataGridView3.Columns(2).Width = 200
        Me.DataGridView3.Columns(3).Width = 60
        Me.DataGridView3.Columns(4).Width = 200
        Me.DataGridView3.Columns(6).Width = 150
    End Sub

    Private Sub MohasebeCountOk()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT CountOk FROM Bnk.Mojoodi WHERE (Row = " & txtRow.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mojoodi")
        objDataview = New DataView(objDataset.Tables("Bnk.Mojoodi"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "CountOk"

        txtCountOk.DataBindings.Clear()
        txtCountOk.DataBindings.Add("Text", objDataview, "CountOk")

        RB1.Checked = True
        txtDscrS.Text = ""
        txtCountS.Text = 0
        txtCountOldS.Text = 0
        FillDataSetAndDataViewSabt()
        FillDataGridViewSabt()
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub btnSabt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSabt.Click
        Dim Sal As String
        Dim Bt As String
        Dim Cnt As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If maskDateS.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDateS.Focus()
            Exit Sub
        ElseIf txtDscrS.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            txtDscrS.Focus()
            Exit Sub
        ElseIf txtCountS.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCountS.Focus()
            Exit Sub
        ElseIf txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا رکورد مورد نظر را از لیست موجودی کالا انتخاب کنید . "
            MsgbOK.ShowDialog()
            maskDateS.Focus()
            Exit Sub
        End If

        If RB1.Checked = True Then
            Bt = 1
        ElseIf RB2.Checked = True Then
            Bt = 2
        End If
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdSabtMojoodi"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Bt", Bt)
            objCommand.Parameters.AddWithValue("@Dat", maskDateS.Text)
            objCommand.Parameters.AddWithValue("@Kcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Count", txtCountS.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscrS.Text)
            objCommand.Parameters.AddWithValue("@ChkSerial", CheckBox1.CheckState)
            objCommand.Parameters.AddWithValue("@Serial", txtSerial.Text)
            objCommand.Parameters.AddWithValue("@shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        If Val(txtCountOldS.Text) > Val(txtCountS.Text) Then
            Cnt = Val(txtCountOldS.Text) - Val(txtCountS.Text)
        Else
            Cnt = Val(txtCountS.Text) - Val(txtCountOldS.Text)
        End If

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdMojoodi"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", 2)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Kcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
            If RB1.Checked = True Then
                objCommand.Parameters.AddWithValue("@CountOk", Val(txtCountOk.Text) + Val(txtCountS.Text))
            ElseIf RB2.Checked = True Then
                If Val(txtCountOldS.Text) > Val(txtCountS.Text) Then
                    objCommand.Parameters.AddWithValue("@CountOk", Val(txtCountOk.Text) - Val(Cnt))
                Else
                    objCommand.Parameters.AddWithValue("@CountOk", Val(txtCountOk.Text) + Val(Cnt))
                End If
            End If
            objCommand.Parameters.AddWithValue("@Box", txtBox.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Chk", 0)
            objCommand.Parameters.AddWithValue("@Tag", 0)
            objCommand.Parameters.AddWithValue("@shob", Shob)
            objCommand.Parameters.AddWithValue("@Sal", Sal)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        MohasebeCountOk()
    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub DataGridView3_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.RowHeaderMouseClick
        Dim X As Integer

        X = DataGridView3.CurrentCell.RowIndex
        maskDateS.Text = DataGridView3.Rows(X).Cells(0).Value
        txtCountS.Text = DataGridView3.Rows(X).Cells(3).Value
        txtCountOldS.Text = txtCountS.Text
        txtDscrS.Text = DataGridView3.Rows(X).Cells(4).Value
        If DataGridView3.Rows(X).Cells(5).Value = 0 Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
        txtSerial.Text = DataGridView3.Rows(X).Cells(6).Value
        RB2.Checked = True
    End Sub

    Private Sub btnNewS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewS.Click
        txtDscrS.Text = ""
        txtCountS.Text = 0
        txtCountOldS.Text = 0
        CheckBox1.Checked = False
        RB1.Checked = True
        txtDscrS.Focus()
    End Sub

    Private Sub btnDeleteS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteS.Click
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If maskDateS.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDateS.Focus()
            Exit Sub
        ElseIf txtDscrS.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            txtDscrS.Focus()
            Exit Sub
        ElseIf txtCountS.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا تعداد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCountS.Focus()
            Exit Sub
        ElseIf txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا رکورد مورد نظر را از لیست موجودی کالا انتخاب کنید . "
            MsgbOK.ShowDialog()
            maskDateS.Focus()
            Exit Sub
        End If

        MsgbYN.Label1.Text = "  آیا می خواهید رکورد فوق از سیستم حذف شود  ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            txtDscrS.Focus()
            Exit Sub
        Else
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdSabtMojoodi"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Bt", 3)
                objCommand.Parameters.AddWithValue("@Dat", maskDateS.Text)
                objCommand.Parameters.AddWithValue("@Kcode", txtCode.Text)
                objCommand.Parameters.AddWithValue("@Count", txtCountS.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrS.Text)
                objCommand.Parameters.AddWithValue("@ChkSerial", CheckBox1.CheckState)
                objCommand.Parameters.AddWithValue("@Serial", txtSerial.Text)
                objCommand.Parameters.AddWithValue("@shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdMojoodi"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", 2)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
                objCommand.Parameters.AddWithValue("@Kcode", txtCode.Text)
                objCommand.Parameters.AddWithValue("@Count", txtCount.Text)
                objCommand.Parameters.AddWithValue("@CountOk", Val(txtCountOk.Text) - Val(txtCountS.Text))
                objCommand.Parameters.AddWithValue("@Box", txtBox.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
                objCommand.Parameters.AddWithValue("@Chk", 0)
                objCommand.Parameters.AddWithValue("@Tag", 0)
                objCommand.Parameters.AddWithValue("@shob", Shob)
                objCommand.Parameters.AddWithValue("@Sal", Sal)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            MohasebeCountOk()
        End If
    End Sub

    Private Sub txtSrchS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrchS.TextChanged
        ColumnSrch = "4"
        FillDataSetAndDataViewSabt()
        Select Case ColumnSrch
            Case "4"
                objDataview.RowFilter = "Dscr like '" & "**" & Me.txtSrchS.Text & "**" & "'"
        End Select
        FillDataGridViewSabt()
    End Sub

    Private Sub RB4_CheckedChanged(sender As Object, e As EventArgs) Handles RB4.CheckedChanged
        Dim X As Integer
        Dim Code As String
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        X = DataGridView1.CurrentCell.RowIndex
        Code = DataGridView1.Rows(X).Cells(2).Value
        If ColumnSrch = 2 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Dat, Count, Dscr FROM Bnk.MojoodiDscr WHERE (Kala_code = " & Code & ") AND (Shob = " & Shob & ") ORDER BY Dat", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.MojoodiDscr")
            objDataview = New DataView(objDataset.Tables("Bnk.MojoodiDscr"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Dat"

            Me.DataGridView2.AutoGenerateColumns = True
            Me.DataGridView2.DataSource = objDataview

            Me.DataGridView2.Columns(0).HeaderText = "تاریخ"
            Me.DataGridView2.Columns(1).HeaderText = "تعداد"
            Me.DataGridView2.Columns(2).HeaderText = "شرح"

            Me.DataGridView2.Columns(0).Width = 70
            Me.DataGridView2.Columns(1).Width = 60
            Me.DataGridView2.Columns(2).Width = 350
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub RB3_CheckedChanged(sender As Object, e As EventArgs) Handles RB3.CheckedChanged
        If DataGridView1.RowCount > 0 Then
            If ColumnSrch = 2 Then
                ReadSabtMohoodi()
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtSerial.ReadOnly = False
            txtSerial.Focus()
        Else
            txtSerial.ReadOnly = True
            txtSerial.Text = ""
        End If
    End Sub

    Private Sub txtSerial_TextChanged(sender As Object, e As EventArgs) Handles txtSerial.TextChanged

    End Sub

    Private Sub txtSerial_GotFocus(sender As Object, e As EventArgs) Handles txtSerial.GotFocus
        txtSerial.SelectionStart = 0
        txtSerial.SelectionLength = Len(txtSerial.Text)
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSabt.Focus()
        End If
    End Sub

    Private Sub txtSerial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSabt.Focus()
        End If
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class