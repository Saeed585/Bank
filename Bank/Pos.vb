Public Class Pos
    Dim ColumnSrch As Integer
    Public Sub FillRow()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.Pos WHERE (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
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

    Private Sub FillCboVahed()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Vahed_Code, Vahed_Name FROM bas.Vahed ORDER BY Vahed_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Vahed")
        Me.cboVahed.DataSource = objDataset.Tables("bas.Vahed")
        Me.cboVahed.DisplayMember = "Vahed_Name"
        Me.cboVahed.ValueMember = "Vahed_Code"
    End Sub

    Private Sub FillCboSemat()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Semat_Code, Semat_Name FROM bas.Semat ORDER BY Semat_Code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Semat")
        Me.cboSemat.DataSource = objDataset.Tables("bas.Semat")
        Me.cboSemat.DisplayMember = "Semat_Name"
        Me.cboSemat.ValueMember = "Semat_Code"
    End Sub

    Private Sub FillCboArea()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Area_code, Area_name FROM Bas.Area ORDER BY Area_code ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Area")
        Me.cboArea.DataSource = objDataset.Tables("Bas.Area")
        Me.cboArea.DisplayMember = "Area_name"
        Me.cboArea.ValueMember = "Area_code"
    End Sub

    Private Sub FillcboMark()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.Mark.Mark_code, Bas.Mark.Mark_name FROM Bas.Mark INNER JOIN Bas.Mark_Sec ON Bas.Mark.Mark_code = Bas.Mark_Sec.Mark_code" &
             " WHERE (Bas.Mark_Sec.U_code = " & User_Code & ") AND (Bas.Mark.SarGroupNo = 2) ORDER BY Bas.Mark.Mark_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Mark")
        Me.cboMark.DataSource = objDataset.Tables("Bas.Mark")
        Me.cboMark.DisplayMember = "Mark_name"
        Me.cboMark.ValueMember = "Mark_code"
    End Sub

    Private Sub FillcboModel()
        Dim Mark As String

        Mark = cboMark.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Model_Code, Model_Name FROM Bas.Model WHERE(Mark_code = " & Mark & ") ORDER BY Model_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.MainBoard")
        Me.CboModel.DataSource = objDataset.Tables("Bas.MainBoard")
        Me.CboModel.DisplayMember = "Model_name"
        Me.CboModel.ValueMember = "Model_code"
    End Sub

    Private Sub FillCboMove()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter = New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE (Shob_Code <> 0) AND (Shob_Code <> " & Shob & ") ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Shob")
        cboMove.DataSource = objDataset.Tables("Bas.Shob")
        cboMove.DisplayMember = "Shob_Name"
        cboMove.ValueMember = "Shob_Code"
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim Mark As String
        Dim Model As String
        Dim Da As New SqlDataAdapter

        Mark = cboMark.SelectedValue.ToString
        If CboModel.Text = "" Then
            Model = 0
        Else
            Model = CboModel.SelectedValue.ToString
        End If
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If CheckBox1.Checked = True Then
            If CheckBox4.Checked = True Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_Pos ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_Pos")
                objDataview = New DataView(objDataset.Tables("Bnk.View_Pos"))
                objDataview.Sort = "Row"
                objConnection.Close()
            Else
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_Pos WHERE (Shob_code =" & Shob & ") ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_Pos")
                objDataview = New DataView(objDataset.Tables("Bnk.View_Pos"))
                objDataview.Sort = "Row"
                objConnection.Close()
            End If
        Else
            If CheckBox4.Checked = True Then
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_Pos WHERE (Mark = " & Mark & ") AND (Model = " & Model & ") ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_Pos")
                objDataview = New DataView(objDataset.Tables("Bnk.View_Pos"))
                objDataview.Sort = "Row"
                objConnection.Close()
            Else
                objConnection.Open()
                Da.SelectCommand = New SqlCommand
                Da.SelectCommand.Connection = objConnection
                Da.SelectCommand.CommandText = ("Select * From Bnk.View_Pos WHERE (Shob_code =" & Shob & ") AND (Mark = " & Mark & ") AND (Model = " & Model & ") ORDER BY Row")

                objDataset.Tables.Clear()
                Da.Fill(objDataset, "Bnk.View_Pos")
                objDataview = New DataView(objDataset.Tables("Bnk.View_Pos"))
                objDataview.Sort = "Row"
                objConnection.Close()
            End If
        End If
    End Sub

    Private Sub FillDataGridView()
        'Dim Mark As String
        'Dim Model As String

        'Mark = cboMark.SelectedValue.ToString
        'If CboModel.Text = "" Then
        '    Model = 0
        'Else
        '    Model = CboModel.SelectedValue.ToString
        'End If
        'Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).Visible = False ' Mark
        Me.DataGridView1.Columns(3).HeaderText = "مارک"
        Me.DataGridView1.Columns(4).Visible = False ' Model
        Me.DataGridView1.Columns(5).HeaderText = "مدل"
        Me.DataGridView1.Columns(6).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(7).HeaderText = "نام"
        Me.DataGridView1.Columns(8).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(9).Visible = False
        Me.DataGridView1.Columns(10).HeaderText = "واحد سازمانی"
        Me.DataGridView1.Columns(11).Visible = False
        Me.DataGridView1.Columns(12).HeaderText = "پست سازمانی"
        Me.DataGridView1.Columns(13).Visible = False
        Me.DataGridView1.Columns(14).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(15).HeaderText = "شماره سریال"
        Me.DataGridView1.Columns(16).HeaderText = "پایانه / پذیرنده"
        Me.DataGridView1.Columns(17).HeaderText = "شرح"
        Me.DataGridView1.Columns(18).Visible = False ' Odat
        Me.DataGridView1.Columns(19).HeaderText = "تاریخ عودت"
        Me.DataGridView1.Columns(20).Visible = False ' Mafgod
        Me.DataGridView1.Columns(21).HeaderText = "تاریخ مفقود"
        Me.DataGridView1.Columns(22).Visible = False ' Chk
        Me.DataGridView1.Columns(23).Visible = False ' OSadad
        Me.DataGridView1.Columns(24).HeaderText = "تاریخ عودت به سداد"
        Me.DataGridView1.Columns(25).Visible = False ' Shob_code

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(3).Width = 100
        Me.DataGridView1.Columns(5).Width = 100
        Me.DataGridView1.Columns(6).Width = 70
        Me.DataGridView1.Columns(7).Width = 100
        Me.DataGridView1.Columns(8).Width = 120
        Me.DataGridView1.Columns(10).Width = 100
        Me.DataGridView1.Columns(12).Width = 100
        Me.DataGridView1.Columns(14).Width = 100
        Me.DataGridView1.Columns(15).Width = 150
        Me.DataGridView1.Columns(16).Width = 100
        Me.DataGridView1.Columns(17).Width = 200
        Me.DataGridView1.Columns(19).Width = 70
        Me.DataGridView1.Columns(21).Width = 70
        Me.DataGridView1.Columns(24).Width = 70

        lblRow.Text = DataGridView1.RowCount
        lblTozi.Text = lblRow.Text
        RowColorHighLight()

        'Dim objDataAdapter As New SqlDataAdapter _
        '    (" SELECT COUNT(Row) AS Expr1 FROM Bnk.Pos WHERE (Shob_Code = " & Shob & ") AND (Mafgod = 0) AND (Mark = " & Mark & ") AND (Model = " & Model & ")", objConnection)
        'objDataset = New DataSet
        'objDataAdapter.Fill(objDataset, "Bnk.Pos")
        'objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        'objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        'objDataview.Sort = "Expr1"

        'lblTozi.DataBindings.Clear()
        'lblTozi.DataBindings.Add("Text", objDataview, "Expr1")
    End Sub
    Private Sub DGVMove()
        Dim X As Integer

        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(1).Value
        cboMark.SelectedValue = DataGridView1.Rows(X).Cells(2).Value
        CboModel.SelectedValue = DataGridView1.Rows(X).Cells(4).Value
        txtCode.Text = DataGridView1.Rows(X).Cells(6).Value
        txtName.Text = DataGridView1.Rows(X).Cells(7).Value
        txtFamily.Text = DataGridView1.Rows(X).Cells(8).Value
        cboVahed.SelectedValue = DataGridView1.Rows(X).Cells(9).Value
        cboSemat.SelectedValue = DataGridView1.Rows(X).Cells(11).Value
        cboArea.SelectedValue = DataGridView1.Rows(X).Cells(13).Value
        txtSerial.Text = DataGridView1.Rows(X).Cells(15).Value
        txtPayane.Text = DataGridView1.Rows(X).Cells(16).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(17).Value
        CheckBox2.CheckState = DataGridView1.Rows(X).Cells(18).Value
        maskDateOdat.Text = DataGridView1.Rows(X).Cells(19).Value
        CheckBox3.CheckState = DataGridView1.Rows(X).Cells(20).Value
        maskDateMafgod.Text = DataGridView1.Rows(X).Cells(21).Value
        CheckBox6.CheckState = DataGridView1.Rows(X).Cells(23).Value
        maskDateSadad.Text = DataGridView1.Rows(X).Cells(24).Value
    End Sub

    Private Sub RowColorHighLight()
        Dim X As Integer
        Dim Chk As String

        For X = 0 To DataGridView1.RowCount - 1
            Chk = 0
            If Not IsDBNull(DataGridView1.Rows(X).Cells(22).Value) Then
                Chk = DataGridView1.Rows(X).Cells(22).Value
                If Chk = 1 Then
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            Else
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
            End If

            If DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White Then
                Chk = 0
                If Not IsDBNull(DataGridView1.Rows(X).Cells(18).Value) Then
                    Chk = DataGridView1.Rows(X).Cells(18).Value
                    If Chk = 1 Then
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.GreenYellow
                    Else
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                    End If
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            End If

            If DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White Then
                Chk = 0
                If Not IsDBNull(DataGridView1.Rows(X).Cells(20).Value) Then
                    Chk = DataGridView1.Rows(X).Cells(20).Value
                    If Chk = 1 Then
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Pink
                    Else
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                    End If
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            End If

            If DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White Then
                Chk = 0
                If Not IsDBNull(DataGridView1.Rows(X).Cells(23).Value) Then
                    Chk = DataGridView1.Rows(X).Cells(23).Value
                    If Chk = 1 Then
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Thistle
                    Else
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                    End If
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            End If
        Next
    End Sub
    Private Sub Clear()
        txtFamily.Text = ""
        txtSerial.Text = ""
        txtPayane.Text = ""
        txtDscr.Text = ""
        txtRowRep.Text = ""
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox6.Checked = False
        maskDate.Focus()
    End Sub

    Private Sub FillDataSetAndDataView2()
        Dim Mark As String
        Dim Model As String

        Mark = cboMark.SelectedValue.ToString
        If CboModel.Text = "" Then
            Model = 0
        Else
            Model = CboModel.SelectedValue.ToString
        End If
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Dat, Tedad, Dscr" &
             " FROM Bnk.PosMoj WHERE (Mark_Code = " & Mark & ") AND (Model_Code = " & Model & ") AND (Shob_Code = " & Shob & ") ORDER BY Dat", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.PosMoj")
        objDataview = New DataView(objDataset.Tables("Bnk.PosMoj"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
    End Sub

    Private Sub FillDataGridView2()
        Dim X As Integer
        Dim Mark As String
        Dim Model As String

        Mark = cboMark.SelectedValue.ToString
        If CboModel.Text = "" Then
            Model = 0
        Else
            Model = CboModel.SelectedValue.ToString
        End If
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).HeaderText = "تاریخ"
        Me.DataGridView2.Columns(1).HeaderText = "تعداد"
        Me.DataGridView2.Columns(2).HeaderText = "شرح"

        Me.DataGridView2.Columns(0).Width = 70
        Me.DataGridView2.Columns(1).Width = 40
        Me.DataGridView2.Columns(2).Width = 250

        lblTedad.Text = 0
        lblMande.Text = 0
        For X = 0 To DataGridView2.RowCount - 1
            lblTedad.Text = Val(lblTedad.Text) + Val(DataGridView2.Rows(X).Cells(1).Value)
        Next
        lblMande.Text = Val(lblTedad.Text) - Val(lblTozi.Text)

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT COUNT(Row) AS Expr1 FROM Bnk.Pos WHERE (Mark = " & Mark & ") AND (Model = " & Model & ") AND (Shob_Code = " & Shob & ") AND (Odat = 1)", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        txtSumTedad.Text = 0
        txtSumTedad.DataBindings.Clear()
        txtSumTedad.DataBindings.Add("text", objDataview, "Expr1")
        lblMande.Text = Val(lblMande.Text) + Val(txtSumTedad.Text)
    End Sub

    Private Sub Pos_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        Clear()
        FillCboVahed()
        FillCboSemat()
        FillCboArea()
        FillCboMove()
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
           " UPDATE Bnk.Pos SET Chk = @Chk WHERE (Row = " & Rw & ")"
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

    Private Sub Pos_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.DropTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 10)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub Pos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            FillRow()
            Clear()
        ElseIf e.KeyCode = Keys.Space Then
            Spc()
        ElseIf e.KeyCode = Keys.F5 Then
            HighLight()
        End If
    End Sub

    Private Sub Spc()
        If maskDate.Focused = True Then
            maskDate.Text = ConvD.Class1.ConvDate
        ElseIf maskDateOdat.Focused = True Then
            maskDateOdat.Text = ConvD.Class1.ConvDate
        ElseIf maskDateMafgod.Focused = True Then
            maskDateMafgod.Text = ConvD.Class1.ConvDate
        End If
    End Sub

    Private Sub Pos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        maskDate.Text = ConvD.Class1.ConvDate

        FillcboMark()
        FillcboModel()
        txtSumTedad.SendToBack()
        KeyPreview = True
        FormName = "Pos"
    End Sub

    Private Sub Pos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub SaveLogPos()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " INSERT INTO [Log].PosLog (Row, Dat, Mark, Model, Odat, DatOdat, OSadad, DatSadad, Mafgod, DatMafgod, pers_code, Vahed_code, semat_code, Area_Code, SerialNo, Payane, Dscr, Chk, Shob_Code, Sal)" &
           " SELECT Row, Dat, Mark, Model, Odat, DatOdat, OSadad, DatSadad, Mafgod, DatMafgod, pers_code, Vahed_code, semat_code, Area_Code, SerialNo, Payane, Dscr, Chk, Shob_Code, Sal" &
           " FROM Bnk.Pos AS Pos_1 WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")"

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
            " UPDATE [Log].PosLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" &
            " WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ") AND (tim_stat IS NULL)"
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
        Dim Knd As String
        'Dim ChkOut As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdPos"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@mark", cboMark.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@model", CboModel.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Odat", CheckBox2.CheckState)
            objCommand.Parameters.AddWithValue("@DatOdat", maskDateOdat.Text)
            objCommand.Parameters.AddWithValue("@OSadad", CheckBox6.CheckState)
            objCommand.Parameters.AddWithValue("@DatSadad", maskDateSadad.Text)
            objCommand.Parameters.AddWithValue("@Mafgod", CheckBox3.CheckState)
            objCommand.Parameters.AddWithValue("@DatMafgod", maskDateMafgod.Text)
            objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@vahed", cboVahed.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@semat", cboSemat.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Area_code", cboArea.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Serial", txtSerial.Text)
            objCommand.Parameters.AddWithValue("@Payane", txtPayane.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Chk", 0)
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
        Dim objCommand As New SqlCommand
        Dim Sal As String
        Dim Mark As String

        Mark = cboMark.SelectedValue.ToString
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        ElseIf maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtSerial.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شماره سریال را وارد کنید . "
            MsgbOK.ShowDialog()
            txtSerial.Focus()
            Exit Sub
        ElseIf txtPayane.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شماره پایانه را وارد کنید . "
            MsgbOK.ShowDialog()
            txtPayane.Focus()
            Exit Sub
        End If

        Dim objDataAdapter0 As New SqlDataAdapter _
            (" SELECT SerialNo FROM Bnk.Pos WHERE (Mark = " & Mark & ") AND (SerialNo = N'" & txtSerial.Text & "') AND (SerialNo <> N'0') AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter0.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " سریال فوق " & Trim(txtSerial.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.txtSerial.Focus()
            Exit Sub
        End If

        Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT SerialNo FROM Bnk.Pos WHERE (Mark = " & Mark & ") AND (Payane = N'" & txtPayane.Text & "') AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " پایانه فوق " & Trim(txtPayane.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.txtPayane.Focus()
            Exit Sub
        End If

        FillRow()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Bnk.Pos WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            FillRow()
            Btn = 1
            InsertUpdate()

            '--------LogPos
            logStat = 1
            SaveLogPos()
            '--------------------
            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub Edt()
        Dim objCommand As New SqlCommand
        Dim Sal As String
        Dim Mark As String

        Mark = cboMark.SelectedValue.ToString
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        ElseIf maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
            MsgbOK.ShowDialog()
            maskDate.Focus()
            Exit Sub
        ElseIf txtSerial.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شماره سریال را وارد کنید . "
            MsgbOK.ShowDialog()
            txtSerial.Focus()
            Exit Sub
        End If

        If CheckBox2.Checked = True Then
            If maskDateOdat.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ عودت را وارد کنید . "
                MsgbOK.ShowDialog()
                maskDateOdat.Focus()
                Exit Sub
            End If
        End If

        If CheckBox3.Checked = True Then
            If maskDateMafgod.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ عودت را وارد کنید . "
                MsgbOK.ShowDialog()
                maskDateMafgod.Focus()
                Exit Sub
            End If
        End If

        Dim objDataAdapter0 As New SqlDataAdapter _
             (" SELECT SerialNo FROM Bnk.Pos WHERE (Row <> " & txtRow.Text & ") AND (Mark = " & Mark & ") AND (SerialNo = N'" & txtSerial.Text & "') AND (SerialNo <> N'0')  AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter0.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " سریال فوق " & Trim(txtSerial.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.txtSerial.Focus()
            Exit Sub
        End If

        Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT SerialNo FROM Bnk.Pos WHERE (Row <> " & txtRow.Text & ") AND (Mark = " & Mark & ") AND (Payane = N'" & txtPayane.Text & "')  AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " پایانه فوق " & Trim(txtPayane.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.txtPayane.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Bnk.Pos WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            '--------LogPos
            logStat = 2
            SaveLogPos()
            '--------------------
            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Dim objCommand As New SqlCommand
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Bnk.Pos WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Pos")
        objDataview = New DataView(objDataset.Tables("Bnk.Pos"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtCode.Focus()
                Exit Sub
            Else
                '--------LogPosl
                logStat = 3
                SaveLogPos()
                '--------------------
                Btn = 3
                InsertUpdate()

                Ref.PerformClick()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        If ColumnSrch = 0 Or ColumnSrch = 6 Or ColumnSrch = 8 Or ColumnSrch = 15 Or ColumnSrch = 16 Then
            Label28.Text = Me.DataGridView1.Columns(ColumnSrch).HeaderText
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        Else
            Me.txtSrch.Text = ""
            Label28.Text = "----------"
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

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        txtDscr.SelectionStart = 0
        txtDscr.SelectionLength = Len(txtDscr.Text)
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtFamily.Text = "" Then
                Code_Soft = 15
                FormName = "Pos"
                FillPers()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                cboVahed.Focus()
            End If
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtCode.Clear()
            txtName.Clear()
        End If
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
                cboMark.Focus()
            End If
        End If
    End Sub

    Private Sub cboVahed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboVahed.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboSemat.Focus()
        End If
    End Sub

    Private Sub cboVahed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVahed.SelectedIndexChanged

    End Sub

    Private Sub cboSemat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboSemat.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboArea.Focus()
        End If
    End Sub

    Private Sub cboSemat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSemat.SelectedIndexChanged

    End Sub

    Private Sub txtPayane_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayane.GotFocus
        txtPayane.SelectionStart = 0
        txtPayane.SelectionLength = Len(txtPayane.Text)
    End Sub

    Private Sub txtPayane_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPayane.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillCboVahed()
        FillCboSemat()
        FillCboArea()
        FillDataSetAndDataView()
        FillDataGridView()
        FillDataSetAndDataView2()
        FillDataGridView2()
        Clear()
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RB2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FillRow()
        Clear()
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDataSetAndDataView()
        If txtSrch.Text <> "" Then
            If ColumnSrch = 0 Then
                objDataview.RowFilter = "Row = " & Me.txtSrch.Text & ""
            ElseIf ColumnSrch = 6 Then
                objDataview.RowFilter = "pers_code = " & Me.txtSrch.Text & ""
            ElseIf ColumnSrch = 8 Then
                objDataview.RowFilter = "pers_family like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 15 Then
                objDataview.RowFilter = "SerialNo like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 16 Then
                objDataview.RowFilter = "Payane like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End If
        End If
        FillDataGridView()
        RowColorHighLight()
    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs)

    End Sub

    Private Sub txtSerial_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSerial.GotFocus
        txtPayane.SelectionStart = 0
        txtPayane.SelectionLength = Len(txtPayane.Text)
    End Sub

    Private Sub txtSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPayane.Focus()
        End If
    End Sub

    Private Sub txtSerial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerial.TextChanged

    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        txtCode.SelectionStart = 0
        txtCode.SelectionLength = Len(txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text <> "" Then
                Pcd = txtCode.Text
                FillMaskCode()

                IntPosition = objDataview.Find(txtCode.Text)
                If IntPosition = -1 Then
                    MsgbOK.Label1.Text = "  کد پرسنلی فوق در سیستم موجود نمی باشد . "
                    MsgbOK.ShowDialog()
                    txtCode.Focus()
                    Exit Sub
                Else
                    txtName.DataBindings.Clear()
                    txtName.DataBindings.Add("Text", objDataview, "Pers_Name")
                    txtFamily.DataBindings.Clear()
                    txtFamily.DataBindings.Add("Text", objDataview, "Pers_Family")
                End If
            Else
                txtFamily.Clear()
                txtName.Clear()
            End If
            txtFamily.Focus()
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
                    .Range("C1").Value = "مارک"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "مدل"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "کد پرسنلی"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "نام"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "نام خانوادگی"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "واحد سازمانی"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "پست سازمانی"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "محل فعالیت"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "شماره سریال"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "پایانه / پذیرنده"
                    .Range("L1").Font.Bold = True
                    .Range("M1").Value = "شرح"
                    .Range("M1").Font.Bold = True
                    .Range("N1").Value = "تاریخ عودت"
                    .Range("N1").Font.Bold = True
                    .Range("O1").Value = "تاریخ مفقود"
                    .Range("O1").Font.Bold = True

                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
                            .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(15).Value
                            .Range("L" & i.ToString).Value = DataGridView1.Rows(X).Cells(16).Value
                            .Range("M" & i.ToString).Value = DataGridView1.Rows(X).Cells(17).Value
                            .Range("N" & i.ToString).Value = DataGridView1.Rows(X).Cells(19).Value
                            .Range("O" & i.ToString).Value = DataGridView1.Rows(X).Cells(21).Value
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

    Private Sub cboMark_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboMark.KeyPress
        If e.KeyChar = ChrW(13) Then
            CboModel.Focus()
        End If
    End Sub

    Private Sub cboMark_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMark.SelectedIndexChanged

    End Sub

    Private Sub CboModel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CboModel.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCode.Focus()
        End If
    End Sub
    Private Sub cboArea_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboArea.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtSerial.Focus()
        End If
    End Sub

    Private Sub cboArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArea.SelectedIndexChanged

    End Sub

    Private Sub btnMoj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoj.Click
        FormName = "Pos"
        PosMoj.Show()
        PosMoj.Activate()
    End Sub

    Private Sub cboMark_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMark.SelectedValueChanged
        If Me.cboMark.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            If CheckBox1.Checked = False Then
                FillcboModel()
                'FillDataSetAndDataView()
                'FillDataGridView()
                'FillDataSetAndDataView2()
                'FillDataGridView2()
            End If
        End If
    End Sub

    Private Sub CboModel_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboModel.SelectedValueChanged
        If Me.CboModel.Text <> "" Then
            If Me.CboModel.SelectedValue.ToString = "System.Data.DataRowView" Then
                Exit Sub
            Else
                If CheckBox1.Checked = False Then
                    FillDataSetAndDataView()
                    FillDataGridView()
                    FillDataSetAndDataView2()
                    FillDataGridView2()
                End If
                ' LblName()
            End If
        End If
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            FillDataSetAndDataView()
            FillDataGridView()
            cboMark.Enabled = False
            CboModel.Enabled = False
            'btnSave.Enabled = False
            'btnEdit.Enabled = False
            'btnDelete.Enabled = False
        Else
            FillDataSetAndDataView()
            FillDataGridView()
            'FillDataSetAndDataView2()
            'FillDataGridView2()
            cboMark.Enabled = True
            CboModel.Enabled = True
            'If CheckBox4.Checked = True Then
            '    btnSave.Enabled = False
            '    btnEdit.Enabled = False
            '    btnDelete.Enabled = False
            'Else
            '    btnSave.Enabled = True
            '    btnEdit.Enabled = True
            '    btnDelete.Enabled = True
            'End If
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Label12.Visible = True
            maskDateOdat.Visible = True
            maskDateOdat.Focus()
        Else
            Label12.Visible = False
            maskDateOdat.Text = "1   /  /"
            maskDateOdat.Visible = False
        End If
    End Sub
    Private Sub btnRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRep.Click
        Dim Comp As String
        Dim Dat As String
        Dim Mark As String
        Dim Model As String
        Dim Area As String
        Dim X As Integer
        Dim Rw As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Comp = Mainfrm.lblCompName.Text

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.CreateTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 10)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        If DataGridView1.RowCount > 0 Then
            Mark = cboMark.SelectedValue.ToString
            If CboModel.Text <> "" Then
                Model = CboModel.SelectedValue.ToString
            Else
                Model = 0
            End If
            Flag = 3

            For X = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(X).Selected = True Then
                    Rw = DataGridView1.Rows(X).Cells(0).Value
                    Dat = DataGridView1.Rows(X).Cells(1).Value
                    Area = DataGridView1.Rows(X).Cells(13).Value
                    Try
                        objConnection.Open()
                        objCommand = New SqlCommand
                        objCommand.Connection = objConnection
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Rep.Mobil"

                        objCommand.Parameters.Clear()
                        objCommand.Parameters.AddWithValue("@Flag", Flag)
                        objCommand.Parameters.AddWithValue("@Rb", 0)
                        objCommand.Parameters.AddWithValue("@Rbin", 0)
                        objCommand.Parameters.AddWithValue("@Dat", Dat)
                        objCommand.Parameters.AddWithValue("@Mark", Mark)
                        objCommand.Parameters.AddWithValue("@Model", Model)
                        objCommand.Parameters.AddWithValue("@Area", Area)
                        objCommand.Parameters.AddWithValue("@Knd", 0)
                        objCommand.Parameters.AddWithValue("@Knd1", 0)
                        objCommand.Parameters.AddWithValue("@pcode", txtCode.Text)
                        objCommand.Parameters.AddWithValue("@Rowin", Rw)
                        objCommand.Parameters.AddWithValue("@Rowout", 0)
                        'objCommand.Parameters.AddWithValue("@LabelName", txtLabelName)
                        objCommand.Parameters.AddWithValue("@Shob", Shob)
                        objCommand.Parameters.AddWithValue("@Comp", Comp)

                        objCommand.ExecuteNonQuery()
                    Catch Ex As Exception
                        MessageBox.Show(Ex.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()
                End If
            Next

            Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, pers_code, pers_name, pers_family, Dat, Vahed_Name, Semat_Name, Mark_name, Model_name, SerialNo, Ram, Dscr, Chk, ChkOutComp, Tedad, Area_name, Sal, Shob_Code" &
             " FROM Rep.RMobil ORDER BY Row", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Rep.RMobil")
            objDataview = New DataView(objDataset.Tables("Rep.RMobil"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Row"

            'txtRowRep.Text = objDataview.Count

            Me.DataGridView3.AutoGenerateColumns = True
            Me.DataGridView3.DataSource = objDataview

            If DataGridView3.RowCount > 0 Then
                Rptpath.FileName = My.Application.Info.DirectoryPath & "\Report\Pos.rpt"
                Rptpath.SetDatabaseLogon(Us, Pw)
                RepForm.CrystReport.ReportSource = Rptpath
                RepForm.Label1.Text = "لیست کارتخوان / پوز"
                RepForm.CrystReport.RefreshReport()
                RepForm.Show()
                ' RepForm.CrystReport.PrintReport()
            End If
        Else
            MsgbOK.Label1.Text = " با اطلاعات فوق رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnRep.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        Dim X As Integer
        txtRowRep.Text = 0
        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Selected = True Then
                txtRowRep.Text = txtRowRep.Text + 1
            End If
        Next
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblList.Click
        If Me.DataGridView2.Visible = False Then
            Me.DataGridView2.Visible = True
            lblList.Text = "عدم نمایش لیست خرید"
        Else
            Me.DataGridView2.Visible = False
            lblList.Text = "نمایش لیست خرید"
        End If
    End Sub

    Private Sub CboModel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboModel.SelectedIndexChanged

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            Label35.Visible = True
            maskDateMafgod.Visible = True
            maskDateMafgod.Focus()
        Else
            Label35.Visible = False
            maskDateMafgod.Text = "1   /  /"
            maskDateMafgod.Visible = False
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub txtPayane_TextChanged(sender As Object, e As EventArgs) Handles txtPayane.TextChanged

    End Sub

    Private Sub txtCode_TextChanged(sender As Object, e As EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        Dim objCommand As New SqlCommand
        Dim ShobMove As String
        Dim Rw As String
        Dim x As Integer
        Dim cnt As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        cnt = Me.DataGridView1.Rows.Count - 1
        For x = 0 To cnt
            If DataGridView1.Rows(x).Selected = True Then
                If Me.DataGridView1.Rows(x).Cells(23).Value <> Me.cboMove.SelectedValue Then
                    ShobMove = cboMove.SelectedValue
                    Rw = DataGridView1.Rows(x).Cells(0).Value

                    IntPosition = objCurrencyManager.Position
                    objCommand.Connection = objConnection
                    objCommand.CommandText =
                        " UPDATE Bnk.Pos SET Shob_Code = " & ShobMove & " WHERE (Row = " & Rw & ") AND (Shob_Code = " & Shob & ")"
                    objCommand.CommandType = CommandType.Text
                    objCommand.Parameters.Clear()

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
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            FillDataSetAndDataView()
            FillDataGridView()
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
        Else
            FillDataSetAndDataView()
            FillDataGridView()
            'FillDataSetAndDataView2()
            'FillDataGridView2()
            If CheckBox1.Checked = True Then
                btnSave.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False
            Else
                btnSave.Enabled = True
                btnEdit.Enabled = True
                btnDelete.Enabled = True
            End If
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            cboMove.Enabled = True
            btnMove.Enabled = True
        Else
            cboMove.Enabled = False
            btnMove.Enabled = False
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            Label7.Visible = True
            maskDateSadad.Visible = True
            maskDateSadad.Focus()
        Else
            Label7.Visible = False
            maskDateSadad.Text = "1   /  /"
            maskDateSadad.Visible = False
        End If
    End Sub
End Class