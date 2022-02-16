Public Class OldMobil
    Private Sub FillDataPers()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If RB1.Checked = True Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row FROM [Log].MobilLog WHERE (Shob_Code = " & Shob & ") AND (dat_stat <= N'" & maskDate.Text & "') AND (pers_code = " & txtCode.Text & ") GROUP BY Row", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Log.MobilLog")
            objDataview = New DataView(objDataset.Tables("Log.MobilLog"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview

            Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
            Me.DataGridView1.Columns(0).Width = 100
        Else
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT [Log].MobilLog.pers_code, Bas.PersIns.pers_name, Bas.PersIns.pers_family" & _
                 " FROM [Log].MobilLog INNER JOIN Bas.PersIns ON [Log].MobilLog.pers_code = Bas.PersIns.IDPcode AND [Log].MobilLog.Shob_Code = Bas.PersIns.Shob" & _
                 " WHERE ([Log].MobilLog.Shob_Code = " & Shob & ") AND ([Log].MobilLog.Row = " & txtCode.Text & ") AND ([Log].MobilLog.dat_stat <= N'" & maskDate.Text & "') GROUP BY [Log].MobilLog.pers_code, Bas.PersIns.pers_name, Bas.PersIns.pers_family", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Log.MobilLog")
            objDataview = New DataView(objDataset.Tables("Log.MobilLog"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview

            Me.DataGridView1.Columns(0).HeaderText = "کد پرسنلی"
            Me.DataGridView1.Columns(1).HeaderText = "نام"
            Me.DataGridView1.Columns(2).HeaderText = "نام خانوادگی"

            Me.DataGridView1.Columns(0).Width = 60
            Me.DataGridView1.Columns(1).Width = 80
            Me.DataGridView1.Columns(2).Width = 120
        End If
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim Da As New SqlDataAdapter
        Dim X As Integer
        Dim Rw As String
        Dim Cnt As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        X = DataGridView1.CurrentCell.RowIndex
        Rw = DataGridView1.Rows(X).Cells(0).Value
        If RB1.Checked = True Then
            Rb = 1
        Else
            Rb = 2
        End If

        objConnection.Open()
        Da.SelectCommand = New SqlCommand
        Da.SelectCommand.Connection = objConnection
        Da.SelectCommand.CommandType = CommandType.StoredProcedure
        Da.SelectCommand.CommandText = "Rep.OldMobil"

        Da.SelectCommand.Parameters.Clear()
        Da.SelectCommand.Parameters.AddWithValue("@Rb", Rb)
        Da.SelectCommand.Parameters.AddWithValue("@Rw", Rw)
        Da.SelectCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
        Da.SelectCommand.Parameters.AddWithValue("@Shob", Shob)

        objDataset.Tables.Clear()
        Da.Fill(objDataset, "Log.MobilLog")
        objDataview = New DataView(objDataset.Tables("Log.MobilLog"))
        objDataview.Sort = "Dat"
        objConnection.Close()

        lblRow.Text = objDataview.Count
        If objDataview.Count > 0 Then
            Me.DataGridView2.AutoGenerateColumns = True
            Me.DataGridView2.DataSource = objDataview
        Else
            Me.DataGridView2.AutoGenerateColumns = True
            objDataview.Dispose()
            Me.DataGridView2.DataSource = objDataview
        End If

        Me.DataGridView2.Columns(0).Visible = False
        Me.DataGridView2.Columns(1).HeaderText = "وضعیت"
        Me.DataGridView2.Columns(2).HeaderText = "تاریخ تغییر"
        Me.DataGridView2.Columns(3).HeaderText = "زمان تغییر"
        Me.DataGridView2.Columns(4).HeaderText = "تاریخ"
        Me.DataGridView2.Columns(5).HeaderText = "واحد سازمانی"
        Me.DataGridView2.Columns(6).HeaderText = "پست سازمانی"
        Me.DataGridView2.Columns(7).Visible = False ' Mark
        Me.DataGridView2.Columns(8).HeaderText = "مدل"
        Me.DataGridView2.Columns(9).HeaderText = "شماره موبایل"
        Me.DataGridView2.Columns(10).Visible = False ' Ram
        Me.DataGridView2.Columns(11).HeaderText = "تعداد"
        Me.DataGridView2.Columns(12).HeaderText = "محل فعالیت"
        Me.DataGridView2.Columns(13).HeaderText = "توضیحات"

        Me.DataGridView2.Columns(0).Width = 50
        Me.DataGridView2.Columns(1).Width = 60
        Me.DataGridView2.Columns(2).Width = 70
        Me.DataGridView2.Columns(3).Width = 60
        Me.DataGridView2.Columns(4).Width = 70
        Me.DataGridView2.Columns(5).Width = 80
        Me.DataGridView2.Columns(6).Width = 80
        Me.DataGridView2.Columns(8).Width = 70
        Me.DataGridView2.Columns(9).Width = 120
        Me.DataGridView2.Columns(11).Width = 60
        Me.DataGridView2.Columns(12).Width = 120
        Me.DataGridView2.Columns(13).Width = 100

        Cnt = DataGridView2.RowCount - 1
        For X = 0 To Cnt
            If DataGridView2.Rows(X).Cells(0).Value = "1" Then
                DataGridView2.Rows(X).Cells(1).Value = "ذخیره"
            ElseIf DataGridView2.Rows(X).Cells(0).Value = "2" Then
                DataGridView2.Rows(X).Cells(1).Value = "اصلاح"
            ElseIf DataGridView2.Rows(X).Cells(0).Value = "3" Then
                DataGridView2.Rows(X).Cells(1).Value = "حذف"
            End If
        Next
    End Sub

    Private Sub OldMobil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Me.maskDate.Text = ConvD.Class1.ConvDate()
        DataGridView3.SendToBack()
        FormName = "OldMobil"
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub OldMobil_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 228
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        Me.txtCode.SelectionStart = 0
        Me.txtCode.SelectionLength = Len(Me.txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDate.Focus()
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
                If txtCode.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا کد پرسنلی و یا شماره برگه را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtCode.Focus()
                    Exit Sub
                End If
                FillDataPers()
            End If
        End If
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        Label1.Text = "کد پرسنلی"
        txtCode.Clear()
        txtCode.Focus()
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        Label1.Text = "شماره برگه"
        txtCode.Clear()
        txtCode.Focus()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            FillDataSetAndDataView()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        FillDataSetAndDataView()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If DataGridView2.RowCount > 0 Then
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
                Dim i1 As Integer = 4
                Dim X As Integer
                Dim XX As Integer
                Dim X3 As Integer
                Dim Pcode As String
                Dim Rw As String
                Dim Cnt As String
                With xlsheet
                    .DisplayRightToLeft = True
                    X = DataGridView1.CurrentCell.RowIndex

                    If RB1.Checked = True Then
                        Pcode = txtCode.Text
                        Rw = DataGridView1.Rows(X).Cells(0).Value
                    ElseIf RB2.Checked = True Then
                        Pcode = DataGridView1.Rows(X).Cells(0).Value
                        Rw = txtCode.Text
                    End If

                    Dim objDataAdapter As New SqlDataAdapter _
                        (" SELECT IDPcode, pers_name, pers_family FROM Bas.PersIns WHERE (IDPcode = " & Pcode & ")", objConnection)
                    objDataset = New DataSet
                    objDataAdapter.Fill(objDataset, "bas.PersIns")
                    objDataview = New DataView(objDataset.Tables("bas.PersIns"))
                    objDataview.Sort = "IDPcode"

                    Me.DataGridView3.AutoGenerateColumns = True
                    Me.DataGridView3.DataSource = objDataview

                    Me.DataGridView3.Columns(0).HeaderText = "کد پرسنلی"
                    Me.DataGridView3.Columns(1).HeaderText = "نام"
                    Me.DataGridView3.Columns(2).HeaderText = "نام خانوادگی"

                    .Range("A1").Value = "کد پرسنلی"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "نام"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "نام خانوادگی"
                    .Range("C1").Font.Bold = True

                    X3 = DataGridView3.CurrentCell.RowIndex
                    .Range("A" & i.ToString).Value = DataGridView3.Rows(X3).Cells(0).Value
                    .Range("B" & i.ToString).Value = DataGridView3.Rows(X3).Cells(1).Value
                    .Range("C" & i.ToString).Value = DataGridView3.Rows(X3).Cells(2).Value

                    Dim objDataAdapter1 As New SqlDataAdapter _
                        (" SELECT [Log].MobilLog.status, [Log].MobilLog.StatName, [Log].MobilLog.dat_stat, [Log].MobilLog.tim_stat, [Log].MobilLog.Dat, Bas.Vahed.Vahed_Name, Bas.Semat.Semat_Name, Bas.Mark.Mark_name, Bas.Model.Model_Name, [Log].MobilLog.SerialNo, [Log].MobilLog.Ram, [Log].MobilLog.Tedad, Bas.Area.Area_name, [Log].MobilLog.Dscr" & _
                         " FROM [Log].MobilLog INNER JOIN Bas.Mark ON [Log].MobilLog.Mark = Bas.Mark.Mark_code INNER JOIN Bas.Model ON Bas.Mark.Mark_code = Bas.Model.Mark_code AND [Log].MobilLog.Model = Bas.Model.Model_Code INNER JOIN Bas.Vahed ON [Log].MobilLog.Vahed_code = Bas.Vahed.Vahed_Code INNER JOIN Bas.Semat ON [Log].MobilLog.semat_code = Bas.Semat.Semat_Code INNER JOIN Bas.Area ON [Log].MobilLog.Area_Code = Bas.Area.Area_code" & _
                         " WHERE ([Log].MobilLog.Row = " & Rw & ") AND ([Log].MobilLog.pers_code = " & Pcode & ") ORDER BY [Log].MobilLog.dat_stat, [Log].MobilLog.tim_stat", objConnection)
                    objDataset = New DataSet
                    objDataAdapter1.Fill(objDataset, "Log.MobilLog")
                    objDataview = New DataView(objDataset.Tables("Log.MobilLog"))
                    objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                    lblRow.Text = objDataview.Count

                    If objDataview.Count > 0 Then
                        Me.DataGridView2.AutoGenerateColumns = True
                        Me.DataGridView2.DataSource = objDataview
                    Else
                        Me.DataGridView2.AutoGenerateColumns = True
                        objDataview.Dispose()
                        Me.DataGridView2.DataSource = objDataview
                    End If

                    Me.DataGridView2.Columns(0).Visible = False
                    Me.DataGridView2.Columns(1).HeaderText = "وضعیت"
                    Me.DataGridView2.Columns(2).HeaderText = "تاریخ تغییر"
                    Me.DataGridView2.Columns(3).HeaderText = "زمان تغییر"
                    Me.DataGridView2.Columns(4).HeaderText = "تاریخ"
                    Me.DataGridView2.Columns(5).HeaderText = "واحد سازمانی"
                    Me.DataGridView2.Columns(6).HeaderText = "پست سازمانی"
                    Me.DataGridView2.Columns(7).Visible = False ' Mark
                    Me.DataGridView2.Columns(8).HeaderText = "مدل"
                    Me.DataGridView2.Columns(9).HeaderText = "شماره موبایل"
                    Me.DataGridView2.Columns(10).Visible = False ' Ram
                    Me.DataGridView2.Columns(11).HeaderText = "تعداد"
                    Me.DataGridView2.Columns(12).HeaderText = "محل فعالیت"
                    Me.DataGridView2.Columns(13).HeaderText = "توضیحات"

                    Me.DataGridView2.Columns(0).Width = 50
                    Me.DataGridView2.Columns(1).Width = 60
                    Me.DataGridView2.Columns(2).Width = 70
                    Me.DataGridView2.Columns(3).Width = 60
                    Me.DataGridView2.Columns(4).Width = 70
                    Me.DataGridView2.Columns(5).Width = 80
                    Me.DataGridView2.Columns(6).Width = 80
                    Me.DataGridView2.Columns(8).Width = 70
                    Me.DataGridView2.Columns(9).Width = 120
                    Me.DataGridView2.Columns(11).Width = 60
                    Me.DataGridView2.Columns(12).Width = 120
                    Me.DataGridView2.Columns(13).Width = 100

                    Cnt = DataGridView2.RowCount - 1
                    For X = 0 To Cnt
                        If DataGridView2.Rows(X).Cells(0).Value = "1" Then
                            DataGridView2.Rows(X).Cells(1).Value = "ذخیره"
                        ElseIf DataGridView2.Rows(X).Cells(0).Value = "2" Then
                            DataGridView2.Rows(X).Cells(1).Value = "اصلاح"
                        ElseIf DataGridView2.Rows(X).Cells(0).Value = "3" Then
                            DataGridView2.Rows(X).Cells(1).Value = "حذف"
                        End If
                    Next

                    .Range("A3").Value = "وضعیت"
                    .Range("A3").Font.Bold = True
                    .Range("B3").Value = "تاریخ تغییر"
                    .Range("B3").Font.Bold = True
                    .Range("C3").Value = "زمان تغییر"
                    .Range("C3").Font.Bold = True
                    .Range("D3").Value = "تاریخ"
                    .Range("D3").Font.Bold = True
                    .Range("E3").Value = "واحد سازمانی"
                    .Range("E3").Font.Bold = True
                    .Range("F3").Value = "پست سازمانی"
                    .Range("F3").Font.Bold = True
                    .Range("G3").Value = "مدل"
                    .Range("G3").Font.Bold = True
                    .Range("H3").Value = "شماره موبایل"
                    .Range("H3").Font.Bold = True
                    .Range("I3").Value = "تعداد"
                    .Range("I3").Font.Bold = True
                    .Range("J3").Value = "محل فعالیت"
                    .Range("J3").Font.Bold = True
                    .Range("K3").Value = "توضیحات"
                    .Range("K3").Font.Bold = True

                    For XX = 0 To DataGridView2.RowCount - 1
                        ' If DataGridView2.Rows(XX).Selected = True Then
                        .Range("A" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(1).Value
                        .Range("B" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(2).Value
                        .Range("C" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(3).Value
                        .Range("D" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(4).Value
                        .Range("E" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(5).Value
                        .Range("F" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(6).Value
                        .Range("G" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(8).Value
                        .Range("H" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(9).Value
                        .Range("I" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(11).Value
                        .Range("J" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(12).Value
                        .Range("K" & i1.ToString).Value = DataGridView2.Rows(XX).Cells(13).Value
                        i1 += 1
                        ' End If
                    Next XX
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub
End Class