Public Class Mobil
    Dim ColumnSrch As Integer
    Public Sub FillRow()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.Mobil WHERE (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
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
             " WHERE (Bas.Mark_Sec.U_code = " & User_Code & ") AND (Bas.Mark.SarGroupNo = 10) OR (Bas.Mark.SarGroupNo = 11) GROUP BY Bas.Mark.Mark_code, Bas.Mark.Mark_name ORDER BY Bas.Mark.Mark_code", objConnection)
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

    Private Sub FillDataSetAndDataView()
        Dim Knd As String
        Dim Mark As String
        Dim Model As String
        Dim Stat As String
        Dim Da As New SqlDataAdapter

        Mark = cboMark.SelectedValue.ToString
        If CboModel.Text = "" Then
            Model = 0
        Else
            Model = CboModel.SelectedValue.ToString
        End If
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If RB1.Checked = True Then
            Knd = 1
        ElseIf RB2.Checked = True Then
            Knd = 2
        End If

        If RB3.Checked = True Then
            Stat = 1
        ElseIf RB4.Checked = True Then
            Stat = 2
        End If

        If CheckBox1.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_Mobil WHERE (Shob_code =" & Shob & ") AND (Knd = " & Knd & ") ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_Mobil")
            objDataview = New DataView(objDataset.Tables("Bnk.View_Mobil"))
            objDataview.Sort = "Row"
            objConnection.Close()
        Else
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_Mobil WHERE (Shob_code =" & Shob & ") AND (Knd = " & Knd & ")  AND (Mark = " & Mark & ") AND (Model = " & Model & ") AND (Stat = " & Stat & ") ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_Mobil")
            objDataview = New DataView(objDataset.Tables("Bnk.View_Mobil"))
            objDataview.Sort = "Row"
            objConnection.Close()
        End If
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer
        Dim Chk As String

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(2).HeaderText = "نام"
        Me.DataGridView1.Columns(3).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(4).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(5).Visible = False
        Me.DataGridView1.Columns(6).HeaderText = "واحد سازمانی"
        Me.DataGridView1.Columns(7).Visible = False
        Me.DataGridView1.Columns(8).HeaderText = "پست سازمانی"
        Me.DataGridView1.Columns(9).Visible = False
        Me.DataGridView1.Columns(10).Visible = False
        Me.DataGridView1.Columns(11).HeaderText = "شماره سریال"
        Me.DataGridView1.Columns(12).HeaderText = "MVPN"
        Me.DataGridView1.Columns(13).HeaderText = "رم"
        Me.DataGridView1.Columns(14).HeaderText = "شرح"
        Me.DataGridView1.Columns(15).Visible = False
        Me.DataGridView1.Columns(16).Visible = False
        Me.DataGridView1.Columns(17).HeaderText = "تعداد"
        Me.DataGridView1.Columns(18).Visible = False
        Me.DataGridView1.Columns(19).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(20).Visible = False ' Odat
        Me.DataGridView1.Columns(21).HeaderText = "تاریخ عودت"
        Me.DataGridView1.Columns(22).Visible = False ' Mafgod
        Me.DataGridView1.Columns(23).HeaderText = "تاریخ مفقود"
        Me.DataGridView1.Columns(24).HeaderText = "تعداد قسط"
        Me.DataGridView1.Columns(25).HeaderText = "مبلغ قسط"
        Me.DataGridView1.Columns(26).Visible = False ' Knd
        Me.DataGridView1.Columns(27).Visible = False ' Shob_code
        If CheckBox1.Checked = True Then
            Me.DataGridView1.Columns(28).Visible = True ' Mark
            Me.DataGridView1.Columns(29).Visible = True ' Model
            Me.DataGridView1.Columns(30).Visible = False ' Stat
            Me.DataGridView1.Columns(31).Visible = True ' StatDscr
            Me.DataGridView1.Columns(28).HeaderText = "مارک"
            Me.DataGridView1.Columns(29).HeaderText = "مدل"
            Me.DataGridView1.Columns(31).HeaderText = "وضعیت"
        Else
            Me.DataGridView1.Columns(28).Visible = False ' Mark
            Me.DataGridView1.Columns(29).Visible = False ' Model
            Me.DataGridView1.Columns(30).Visible = False ' Stat
            Me.DataGridView1.Columns(31).Visible = False ' StatDscr
        End If
        Me.DataGridView1.Columns(32).Visible = False ' ITMoj

        DataGridView1.Columns(25).DefaultCellStyle.Format = "N0"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 60
        Me.DataGridView1.Columns(2).Width = 80
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(4).Width = 70
        Me.DataGridView1.Columns(6).Width = 100
        Me.DataGridView1.Columns(8).Width = 100
        Me.DataGridView1.Columns(11).Width = 120
        Me.DataGridView1.Columns(12).Width = 60
        Me.DataGridView1.Columns(13).Width = 60
        Me.DataGridView1.Columns(14).Width = 330
        Me.DataGridView1.Columns(17).Width = 60
        Me.DataGridView1.Columns(19).Width = 100
        Me.DataGridView1.Columns(21).Width = 70
        Me.DataGridView1.Columns(23).Width = 70
        Me.DataGridView1.Columns(24).Width = 60
        Me.DataGridView1.Columns(25).Width = 120
        If CheckBox1.Checked = True Then
            Me.DataGridView1.Columns(28).Width = 70
            Me.DataGridView1.Columns(29).Width = 70
            Me.DataGridView1.Columns(31).Width = 70
        End If

        RowColorHighLight()
        lblRow.Text = DataGridView1.RowCount
    End Sub
    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
        txtCode.Text = DataGridView1.Rows(X).Cells(1).Value
        txtName.Text = DataGridView1.Rows(X).Cells(2).Value
        txtFamily.Text = DataGridView1.Rows(X).Cells(3).Value
        maskDate.Text = DataGridView1.Rows(X).Cells(4).Value
        cboVahed.SelectedValue = DataGridView1.Rows(X).Cells(5).Value
        cboSemat.SelectedValue = DataGridView1.Rows(X).Cells(7).Value
        cboMark.SelectedValue = DataGridView1.Rows(X).Cells(9).Value
        CboModel.SelectedValue = DataGridView1.Rows(X).Cells(10).Value
        txtSerial.Text = DataGridView1.Rows(X).Cells(11).Value
        txtMVPN.Text = DataGridView1.Rows(X).Cells(12).Value
        txtRam.Text = DataGridView1.Rows(X).Cells(13).Value
        txtDscr.Text = DataGridView1.Rows(X).Cells(14).Value
        If DataGridView1.Rows(X).Cells(15).Value = 1 Then
            ChkOutComp.Checked = True
            txtTedad.Text = DataGridView1.Rows(X).Cells(17).Value
        Else
            ChkOutComp.Checked = False
        End If
        cboArea.SelectedValue = DataGridView1.Rows(X).Cells(18).Value
        CheckBox2.CheckState = DataGridView1.Rows(X).Cells(20).Value
        maskDateOdat.Text = DataGridView1.Rows(X).Cells(21).Value
        CheckBox3.CheckState = DataGridView1.Rows(X).Cells(22).Value
        maskDateMafgod.Text = DataGridView1.Rows(X).Cells(23).Value
        txtTghest.Text = DataGridView1.Rows(X).Cells(24).Value
        txtGhest.Text = DataGridView1.Rows(X).Cells(25).Value
        If DataGridView1.Rows(X).Cells(30).Value = 1 Then
            RB3.Checked = True
        ElseIf DataGridView1.Rows(X).Cells(30).Value = 2 Then
            RB4.Checked = True
        End If
        CheckBox4.CheckState = DataGridView1.Rows(X).Cells(32).Value
    End Sub

    Private Sub RowColorHighLight()
        Dim X As Integer
        Dim Chk As String

        lblTozi.Text = 0
        For X = 0 To DataGridView1.RowCount - 1
            Chk = 0
            If Not IsDBNull(DataGridView1.Rows(X).Cells(15).Value) Then
                Chk = DataGridView1.Rows(X).Cells(15).Value
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
                If Not IsDBNull(DataGridView1.Rows(X).Cells(20).Value) Then
                    Chk = DataGridView1.Rows(X).Cells(20).Value
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
                If Not IsDBNull(DataGridView1.Rows(X).Cells(22).Value) Then
                    Chk = DataGridView1.Rows(X).Cells(22).Value
                    If Chk = 1 Then
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Pink
                    Else
                        DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                    End If
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            End If
            lblTozi.Text = Val(lblTozi.Text) + Val(DataGridView1.Rows(X).Cells(17).Value)

            If DataGridView1.Rows(X).Cells(30).Value = 1 Then
                DataGridView1.Rows(X).Cells(31).Value = "فعال"
            ElseIf DataGridView1.Rows(X).Cells(30).Value = 2 Then
                DataGridView1.Rows(X).Cells(31).Value = "غیر فعال"
            End If
        Next
    End Sub
    Private Sub Clear()
        txtFamily.Text = ""
        txtSerial.Text = ""
        txtMVPN.Text = 0
        txtRam.Text = ""
        txtDscr.Text = ""
        txtRowRep.Text = ""
        ChkOutComp.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        maskDate.Focus()
    End Sub

    Private Sub FillDataSetAndDataView2()
        Dim Mark As String
        Dim Model As String
        Dim Stat As String

        Mark = cboMark.SelectedValue.ToString
        If CboModel.Text = "" Then
            Model = 0
        Else
            Model = CboModel.SelectedValue.ToString
        End If
        If RB3.Checked = True Then
            Stat = 1
        ElseIf RB4.Checked = True Then
            Stat = 2
        End If
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Dat, Tedad, Dscr" &
             " FROM Bnk.MobilMoj WHERE (Mark_Code = " & Mark & ") AND (Model_Code = " & Model & ") AND (Stat = " & Stat & ") AND (Shob_Code = " & Shob & ") ORDER BY Dat", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.MobilMoj")
        objDataview = New DataView(objDataset.Tables("Bnk.MobilMoj"))
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
            (" SELECT SUM(Tedad) AS Expr1 FROM Bnk.Mobil WHERE (ITMoj = 1) AND (Mark = " & Mark & ") AND (Model = " & Model & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        txtSumTedad.Text = 0
        txtSumTedad.DataBindings.Clear()
        txtSumTedad.DataBindings.Add("text", objDataview, "Expr1")
        lblMande.Text = Val(lblMande.Text) + Val(txtSumTedad.Text)
    End Sub

    Private Sub Mobil_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        Clear()
        FillCboVahed()
        FillCboSemat()
        FillCboArea()
        FillcboMark()
        FillcboModel()
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
           " UPDATE Bnk.Mobil SET Chk = @Chk WHERE (Row = " & Rw & ")"
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

    Private Sub Mobil_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
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

    Private Sub Mobil_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub Mobil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        maskDate.Text = ConvD.Class1.ConvDate

        txtSumTedad.SendToBack()
        KeyPreview = True
        FormName = "Mobil"
    End Sub

    Private Sub Mobil_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub SaveLogMobil()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText =
           " INSERT INTO [Log].MobilLog (Row, pers_code, Dat, Vahed_code, semat_code, Mark, Model, SerialNo, MVPN, Ram, Dscr, Knd, Sal, Shob_Code, Chk, ChkOutComp, Tedad, Tghest, Mbl, Area_Code, Odat, DatOdat, Mafgod, DatMafgod)" &
           " SELECT Row, pers_code, Dat, Vahed_code, semat_code, Mark, Model, SerialNo, MVPN, Ram, Dscr, Knd, Sal, Shob_Code, Chk, ChkOutComp, Tedad, Tghest, Mbl, Area_Code, Odat, DatOdat, Mafgod, DatMafgod" &
           " FROM Bnk.Mobil AS Mobil_1 WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")"

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
            " UPDATE [Log].MobilLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" &
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
        Dim Stat As String
        'Dim ChkOut As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdMobil"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            If RB1.Checked = True Then
                Knd = 1
            ElseIf RB2.Checked = True Then
                Knd = 2
            End If
            objCommand.Parameters.AddWithValue("@Knd", Knd)
            objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@vahed", cboVahed.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@semat", cboSemat.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@mark", cboMark.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@model", CboModel.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Serial", txtSerial.Text)
            objCommand.Parameters.AddWithValue("@MVPN", txtMVPN.Text)
            objCommand.Parameters.AddWithValue("@Ram", txtRam.Text)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@Chk", 0)
            objCommand.Parameters.AddWithValue("@Sal", Sal)
            objCommand.Parameters.AddWithValue("@shob", Shob)
            'If ChkOutComp.Checked = True Then
            '    ChkOut = 1
            'Else
            '    ChkOut = 0
            'End If
            objCommand.Parameters.AddWithValue("@ChkOut", ChkOutComp.CheckState)
            objCommand.Parameters.AddWithValue("@Tedad", txtTedad.Text)
            objCommand.Parameters.AddWithValue("@Area_code", cboArea.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Odat", CheckBox2.CheckState)
            objCommand.Parameters.AddWithValue("@DatOdat", maskDateOdat.Text)
            objCommand.Parameters.AddWithValue("@Mafgod", CheckBox3.CheckState)
            objCommand.Parameters.AddWithValue("@DatMafgod", maskDateMafgod.Text)
            objCommand.Parameters.AddWithValue("@Tghest", txtTghest.Text)
            objCommand.Parameters.AddWithValue("@Ghest", Str(txtGhest.Text))
            objCommand.Parameters.AddWithValue("@ITMoj", CheckBox4.CheckState)

            If RB3.Checked = True Then
                Stat = 1
            ElseIf RB4.Checked = True Then
                Stat = 2
            End If

            objCommand.Parameters.AddWithValue("@Stat", Stat)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        'If Btn = 2 Then
        '    If RB4.Checked = True Then
        '        'If RB3.Checked = True Then
        '        '    Stat = 1
        '        'ElseIf RB4.Checked = True Then
        '        '    Stat = 2
        '        'End If
        '        MobilMoj.FillRow()

        '        Try
        '            objConnection.Open()
        '            objCommand.Connection = objConnection
        '            objCommand.CommandType = CommandType.StoredProcedure
        '            objCommand.CommandText = "Bnk.InsUpdMobilMoj"
        '            objCommand.Parameters.Clear()
        '            objCommand.Parameters.AddWithValue("@Btn", 1)
        '            objCommand.Parameters.AddWithValue("@Row", MobilMoj.txtRow.Text)
        '            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
        '            objCommand.Parameters.AddWithValue("@Tedad", 1)
        '            objCommand.Parameters.AddWithValue("@Dscr", "صدور مجدد شماره " & txtSerial.Text & "")
        '            objCommand.Parameters.AddWithValue("@MblKh", 0)
        '            objCommand.Parameters.AddWithValue("@CMark", cboMark.SelectedValue.ToString)
        '            objCommand.Parameters.AddWithValue("@CModel", CboModel.SelectedValue.ToString)
        '            objCommand.Parameters.AddWithValue("@Stat", 1)
        '            objCommand.Parameters.AddWithValue("@Shob", Shob)
        '            objCommand.Parameters.AddWithValue("@Sal", Sal)

        '            objCommand.ExecuteNonQuery()
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            objConnection.Close()
        '        End Try
        '        objConnection.Close()
        '    End If
        'End If
    End Sub

    Private Sub Sav()
        Dim objCommand As New SqlCommand
        Dim Sal As String
        Dim Mark As String

        Mark = cboMark.SelectedValue.ToString
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If ChkOutComp.Checked = True Then
            If txtTedad.Text = 0 Then
                MsgbOK.Label1.Text = " لطفا تعداد ارسال را وارد کنید . "
                MsgbOK.ShowDialog()
                txtTedad.Focus()
                Exit Sub
            End If
        End If

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
            If ChkOutComp.Checked = False Then
                MsgbOK.Label1.Text = " لطفا شماره سریال را وارد کنید . "
                MsgbOK.ShowDialog()
                txtSerial.Focus()
                Exit Sub
            End If
        ElseIf txtRam.Text = "" Then
            MsgbOK.Label1.Text = " لطفا مقدار حافظه را وارد کنید . "
            MsgbOK.ShowDialog()
            txtRam.Focus()
            Exit Sub
        End If

        Dim objDataAdapter0 As New SqlDataAdapter _
            (" SELECT SerialNo FROM Bnk.Mobil WHERE (Mark = " & Mark & ") AND (SerialNo = N'" & txtSerial.Text & "') AND (SerialNo <> N'0') AND (Stat = 1)", objConnection)
        objDataset = New DataSet
        objDataAdapter0.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " سریال فوق " & Trim(txtSerial.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.txtSerial.Focus()
            Exit Sub
        End If

        FillRow()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Bnk.Mobil WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
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

            '--------LogMobil
            logStat = 1
            SaveLogMobil()
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

        If ChkOutComp.Checked = True Then
            If txtTedad.Text = 0 Then
                MsgbOK.Label1.Text = " لطفا تعداد ارسال را وارد کنید . "
                MsgbOK.ShowDialog()
                txtTedad.Focus()
                Exit Sub
            End If
        End If

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
            If ChkOutComp.Checked = False Then
                MsgbOK.Label1.Text = " لطفا شماره سریال را وارد کنید . "
                MsgbOK.ShowDialog()
                txtSerial.Focus()
                Exit Sub
            End If
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
             (" SELECT SerialNo FROM Bnk.Mobil WHERE (Row <> " & txtRow.Text & ") AND (Mark = " & Mark & ") AND (SerialNo = N'" & txtSerial.Text & "') AND (SerialNo <> N'0') AND (Stat = 1)", objConnection)
        objDataset = New DataSet
        objDataAdapter0.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " سریال فوق " & Trim(txtSerial.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.txtSerial.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Shob_Code FROM Bnk.Mobil WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(txtRow.Text) & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            '--------LogMobil
            logStat = 2
            SaveLogMobil()
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
            (" SELECT Row, Shob_Code FROM Bnk.Mobil WHERE (Row = " & txtRow.Text & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
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
                '--------LogMobil
                logStat = 3
                SaveLogMobil()
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
        If ColumnSrch = 0 Or ColumnSrch = 1 Or ColumnSrch = 3 Or ColumnSrch = 11 Or ColumnSrch = 12 Or ColumnSrch = 13 Then
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
                FormName = "Mobil"
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

    Private Sub txtRam_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRam.GotFocus
        txtRam.SelectionStart = 0
        txtRam.SelectionLength = Len(txtRam.Text)
    End Sub

    Private Sub txtRam_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRam.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscr.Focus()
        End If
    End Sub

    Private Sub txtRam_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRam.TextChanged

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

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged

    End Sub

    Private Sub RB2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB2.Click
        FillDataSetAndDataView()
        FillDataGridView()
        FillDataSetAndDataView2()
        FillDataGridView2()
    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB1.Click
        FillDataSetAndDataView()
        FillDataGridView()
        FillDataSetAndDataView2()
        FillDataGridView2()
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
            ElseIf ColumnSrch = 1 Then
                objDataview.RowFilter = "pers_code = " & Me.txtSrch.Text & ""
            ElseIf ColumnSrch = 3 Then
                objDataview.RowFilter = "pers_family like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 11 Then
                objDataview.RowFilter = "SerialNo like '" & "**" & Me.txtSrch.Text & "**" & "'"
            ElseIf ColumnSrch = 12 Then
                objDataview.RowFilter = "MVPN =" & Me.txtSrch.Text & ""
            ElseIf ColumnSrch = 13 Then
                objDataview.RowFilter = "Ram like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End If
        End If
        FillDataGridView()
        RowColorHighLight()
    End Sub

    Private Sub maskCode_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs)

    End Sub

    Private Sub txtSerial_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSerial.GotFocus
        txtRam.SelectionStart = 0
        txtRam.SelectionLength = Len(txtRam.Text)
    End Sub

    Private Sub txtSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtMVPN.Focus()
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
                    .Range("B1").Value = "کد پرسنلی"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "نام"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نام خانوادگی"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "تاریخ"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "واحد سازمانی"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "پست سازمانی"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "شماره سریال"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "MVPN"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "رم"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "شرح"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "تعداد"
                    .Range("L1").Font.Bold = True
                    .Range("M1").Value = "محل فعالیت"
                    .Range("M1").Font.Bold = True
                    .Range("N1").Value = "تاریخ عودت"
                    .Range("N1").Font.Bold = True
                    .Range("O1").Value = "تاریخ مفقود"
                    .Range("O1").Font.Bold = True
                    .Range("P1").Value = "تعداد قسط"
                    .Range("P1").Font.Bold = True
                    .Range("Q1").Value = "مبلغ قسط"
                    .Range("Q1").Font.Bold = True
                    If CheckBox1.Checked = True Then
                        .Range("R1").Value = "مارک"
                        .Range("R1").Font.Bold = True
                        .Range("S1").Value = "مدل"
                        .Range("S1").Font.Bold = True
                        .Range("T1").Value = "وضعیت"
                        .Range("T1").Font.Bold = True
                    End If

                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(13).Value
                            .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
                            .Range("L" & i.ToString).Value = DataGridView1.Rows(X).Cells(17).Value
                            .Range("M" & i.ToString).Value = DataGridView1.Rows(X).Cells(19).Value
                            .Range("N" & i.ToString).Value = DataGridView1.Rows(X).Cells(21).Value
                            .Range("O" & i.ToString).Value = DataGridView1.Rows(X).Cells(23).Value
                            .Range("P" & i.ToString).Value = DataGridView1.Rows(X).Cells(24).Value
                            .Range("Q" & i.ToString).Value = DataGridView1.Rows(X).Cells(25).Value
                            .Range("Q" & i.ToString).NumberFormat = "#,##0"
                            If CheckBox1.Checked = True Then
                                .Range("R" & i.ToString).Value = DataGridView1.Rows(X).Cells(28).Value
                                .Range("S" & i.ToString).Value = DataGridView1.Rows(X).Cells(29).Value
                                .Range("T" & i.ToString).Value = DataGridView1.Rows(X).Cells(31).Value
                            End If
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

    Private Sub ChkOutComp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkOutComp.CheckedChanged
        If ChkOutComp.Checked = True Then
            Label7.Enabled = True
            txtTedad.Enabled = True
            txtTedad.Text = 0
            ' txtTedad.Focus()
        Else
            Label7.Enabled = False
            txtTedad.Enabled = False
            txtTedad.Text = 1
        End If
    End Sub

    Private Sub txtTedad_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTedad.GotFocus
        txtTedad.SelectionStart = 0
        txtTedad.SelectionLength = Len(txtTedad.Text)
    End Sub

    Private Sub txtTedad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTedad.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDate.Focus()
        End If
    End Sub

    Private Sub txtTedad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTedad.TextChanged

    End Sub

    Private Sub cboArea_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboArea.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtSerial.Focus()
        End If
    End Sub

    Private Sub cboArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArea.SelectedIndexChanged

    End Sub

    Private Sub btnMoj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoj.Click
        FormName = "Mobil"
        MobilMoj.Show()
        MobilMoj.Activate()
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

    'Private Sub LblName()
    '    Dim Mark As String
    '    Dim Model As String
    '    Mark = cboMark.SelectedValue.ToString
    '    Model = CboModel.SelectedValue.ToString
    '    Dim objDataAdapter As New SqlDataAdapter _
    '        (" SELECT Model_NameFa FROM Bas.Model WHERE (Mark_code = " & Mark & ") AND (Model_Code = " & Model & ")", objConnection)
    '    objDataset = New DataSet
    '    objDataAdapter.Fill(objDataset, "Bas.Model")
    '    objDataview = New DataView(objDataset.Tables("Bas.Model"))
    '    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

    '    txtLabelName.Text = ""
    '    txtLabelName.DataBindings.Clear()
    '    txtLabelName.DataBindings.Add("text", objDataview, "Model_NameFa")
    'End Sub

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
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
        Else
            FillDataSetAndDataView()
            FillDataGridView()
            FillDataSetAndDataView2()
            FillDataGridView2()
            cboMark.Enabled = True
            CboModel.Enabled = True
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnDelete.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox4.Checked = True
            Label12.Visible = True
            maskDateOdat.Visible = True
            maskDateOdat.Focus()
        Else
            CheckBox4.Checked = False
            Label12.Visible = False
            maskDateOdat.Text = "1   /  /"
            maskDateOdat.Visible = False
        End If
    End Sub

    Private Sub txtGhest_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGhest.GotFocus
        txtGhest.SelectionStart = 0
        txtGhest.SelectionLength = Len(txtGhest.Text)
    End Sub

    Private Sub txtGhest_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGhest.TextChanged
        txtGhest.Text = Format(Val(txtGhest.Text.Trim.Replace(",", "")), "#,0")
        txtGhest.SelectionStart = txtGhest.TextLength
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub txtTghest_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTghest.GotFocus
        txtTghest.SelectionStart = 0
        txtTghest.SelectionLength = Len(txtTghest.Text)
    End Sub

    Private Sub txtTghest_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTghest.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtGhest.Focus()
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
            Flag = 2

            For X = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(X).Selected = True Then
                    Rw = DataGridView1.Rows(X).Cells(0).Value
                    Dat = DataGridView1.Rows(X).Cells(4).Value
                    Area = DataGridView1.Rows(X).Cells(15).Value
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
                Rptpath.FileName = My.Application.Info.DirectoryPath & "\Report\Mobil.rpt"
                Rptpath.SetDatabaseLogon(Us, Pw)
                RepForm.CrystReport.ReportSource = Rptpath
                RepForm.Label1.Text = "لیست گوشی موبایل"
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
            maskDatemafgod.Visible = True
            maskDateMafgod.Focus()
        Else
            Label35.Visible = False
            maskDateMafgod.Text = "1   /  /"
            maskDateMafgod.Visible = False
        End If
    End Sub

    Private Sub txtTghest_TextChanged(sender As Object, e As EventArgs) Handles txtTghest.TextChanged
        txtTghest.Text = Format(Val(txtTghest.Text.Trim.Replace(",", "")), "0")
        txtTghest.SelectionStart = txtTghest.TextLength
    End Sub

    Private Sub txtMVPN_TextChanged(sender As Object, e As EventArgs) Handles txtMVPN.TextChanged
        txtMVPN.Text = Format(Val(txtMVPN.Text.Trim.Replace(",", "")), "0")
        txtMVPN.SelectionStart = txtMVPN.TextLength
    End Sub

    Private Sub txtMVPN_GotFocus(sender As Object, e As EventArgs) Handles txtMVPN.GotFocus
        txtMVPN.SelectionStart = 0
        txtMVPN.SelectionLength = Len(txtMVPN.Text)
    End Sub

    Private Sub txtMVPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMVPN.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtRam.Focus()
        End If
    End Sub

    Private Sub txtSerial_LostFocus(sender As Object, e As EventArgs) Handles txtSerial.LostFocus
        If cboMark.SelectedValue.ToString = 10 Then
            If Len(txtSerial.Text) >= 11 Then
                txtMVPN.Text = Mid(txtSerial.Text, 8, 4)
            End If
        End If
    End Sub

    Private Sub RB3_CheckedChanged(sender As Object, e As EventArgs) Handles RB3.CheckedChanged

    End Sub

    Private Sub RB3_Click(sender As Object, e As EventArgs) Handles RB3.Click
        FillDataSetAndDataView()
        FillDataGridView()
        FillDataSetAndDataView2()
        FillDataGridView2()
    End Sub

    Private Sub RB4_CheckedChanged(sender As Object, e As EventArgs) Handles RB4.CheckedChanged

    End Sub

    Private Sub RB4_Click(sender As Object, e As EventArgs) Handles RB4.Click
        FillDataSetAndDataView()
        FillDataGridView()
        FillDataSetAndDataView2()
        FillDataGridView2()
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Mafgod, Stat FROM Bnk.Mobil WHERE (Mafgod = 1) AND (Stat = 1) ", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Mobil")
        objDataview = New DataView(objDataset.Tables("Bnk.Mobil"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        txtSumTedad.Text = objDataview.Count

        MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای مسدود شده به گروه غیر فعال اطمینان دارید ؟  "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            maskDate.Focus()
            Exit Sub
        Else
            objCommand.Connection = objConnection
            objCommand.CommandText =
               " UPDATE Bnk.Mobil SET Stat = 2 WHERE (Stat = 1) AND (Shob_Code = " & Shob & ") AND (Mafgod = 1)"
            objCommand.Parameters.Clear()
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

            MsgbOK.Label1.Text = " تعداد " & txtSumTedad.Text & " رکورد مسدود به گروه غیر فعال انتقال پیدا کرد. "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub

            Ref.PerformClick()
        End If
    End Sub
End Class