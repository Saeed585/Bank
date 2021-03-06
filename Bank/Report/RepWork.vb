Public Class RepWork
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
            (" SELECT Bas.NProblem.Code, Bas.NProblem.Name FROM Bas.NProblem INNER JOIN Bas.MergeProb ON Bas.NProblem.Code = Bas.MergeProb.Prob_code" & _
             " GROUP BY Bas.NProblem.Code, Bas.NProblem.Name ORDER BY Bas.NProblem.Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.MergeProb")
        Me.cboNProblem.DataSource = objDataset.Tables("Bas.MergeProb")
        Me.cboNProblem.DisplayMember = "Name"
        Me.cboNProblem.ValueMember = "Code"
    End Sub

    Private Sub FillRepTable()
        Dim GProb As String
        Dim NProb As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If CheckBox1.Checked = False Then
            Knd = 0
        Else
            Knd = 1
        End If

        If RB1.Checked = True Then
            Rb = 1
            txtCode.Text = 0
        ElseIf RB2.Checked = True Then
            Rb = 2
            If RB3.Checked = True Then
                Rbin = 3
            ElseIf RB4.Checked = True Then
                Rbin = 4
                txtCode.Text = 0
            ElseIf RB5.Checked = True Then
                Rbin = 5
                txtCode.Text = 0
            ElseIf RB6.Checked = True Then
                Rbin = 6
                txtCode.Text = 0
            End If
        End If

        GProb = cboGProblem.SelectedValue.ToString
        NProb = cboNProblem.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.CreateTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 26)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.Work"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Rb", Rb)
            If RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@Rbin", Rbin)
            Else
                objCommand.Parameters.AddWithValue("@Rbin", 0)
            End If
            objCommand.Parameters.AddWithValue("@Datin", maskDatein.Text)
            objCommand.Parameters.AddWithValue("@Datout", maskDateout.Text)
            objCommand.Parameters.AddWithValue("@GCodeProb", GProb)
            objCommand.Parameters.AddWithValue("@NCodeProb", NProb)
            objCommand.Parameters.AddWithValue("@pcode", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Knd", Knd)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim X As Integer

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Rep.RepWork.Row, Rep.RepWork.Dat, Bas.GProblem.Name AS GProblem, Bas.NProblem.Name AS NProblem, Rep.RepWork.Problem, Rep.RepWork.Chk, Rep.RepWork.DatEnd, Rep.RepWork.Dscr, Rep.RepWork.Pers_Code, Bas.PersIns.pers_name, Bas.PersIns.pers_family, Bas.UserList.U_Family, Rep.RepWork.Shob, Bas.Shob.Shob_Name" & _
             " FROM Bas.GProblem INNER JOIN Rep.RepWork ON Bas.GProblem.Code = Rep.RepWork.GProblem INNER JOIN Bas.NProblem ON Rep.RepWork.NProblem = Bas.NProblem.Code INNER JOIN Bas.PersIns ON Rep.RepWork.Pers_Code = Bas.PersIns.IDPcode INNER JOIN Bas.UserList ON Rep.RepWork.Ucode = Bas.UserList.U_Code INNER JOIN Bas.Shob ON Rep.RepWork.Shob = Bas.Shob.Shob_Code ORDER BY Rep.RepWork.Shob, Rep.RepWork.Row, Rep.RepWork.Dat", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.GProblem")
        objDataview = New DataView(objDataset.Tables("Bas.GProblem"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview
            lblRow.Text = objDataview.Count
        Else
            Me.DataGridView1.AutoGenerateColumns = True
            objDataview.Dispose()
            Me.DataGridView1.DataSource = objDataview
        End If

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "گروه اشکال/پروژه"
        Me.DataGridView1.Columns(3).HeaderText = "سرفصل اشکال"
        Me.DataGridView1.Columns(4).HeaderText = "اشکال/پروژه"
        Me.DataGridView1.Columns(5).Visible = False ' Chk
        Me.DataGridView1.Columns(6).HeaderText = "تاریخ اتمام"
        Me.DataGridView1.Columns(7).HeaderText = "شرح"
        Me.DataGridView1.Columns(8).Visible = False ' Pcode
        Me.DataGridView1.Columns(9).HeaderText = "نام کاربر"
        Me.DataGridView1.Columns(10).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(11).HeaderText = "کارشناس"
        Me.DataGridView1.Columns(12).Visible = False ' Shob_code
        If CheckBox1.Checked = True Then
            Me.DataGridView1.Columns(13).Visible = True
            Me.DataGridView1.Columns(13).HeaderText = "شعبه"
        Else
            Me.DataGridView1.Columns(13).Visible = False
        End If


        Me.DataGridView1.Columns(0).Width = 60
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 180
        Me.DataGridView1.Columns(3).Width = 180
        Me.DataGridView1.Columns(4).Width = 340
        Me.DataGridView1.Columns(6).Width = 70
        Me.DataGridView1.Columns(7).Width = 340
        Me.DataGridView1.Columns(9).Width = 80
        Me.DataGridView1.Columns(10).Width = 100
        Me.DataGridView1.Columns(11).Width = 100
        If CheckBox1.Checked = True Then
            Me.DataGridView1.Columns(11).Width = 100
        End If

        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Cells(5).Value = 0 Then
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
            Else
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub RepWrok_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillcboGProblem()
        FillcboNProblem()
    End Sub

    Private Sub RepWrok_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        maskDatein.Text = ConvD.Class1.ConvDate
        maskDateout.Text = ConvD.Class1.ConvDate

        FormName = "RepWork"
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub RepWrok_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 221
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            Grp3.Enabled = False
            txtFamily.Text = ""
            RB3.Checked = True
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            Grp3.Enabled = True
            txtFamily.Text = ""
            txtCode.Focus()
        End If
    End Sub

    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        txtFamily.SelectionStart = 0
        txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub

    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            Code_Soft = 15
            FormName = "RepWork"
            If txtCode.Text = "" Then
                FillPers()
                FillDataPers()
                Srch.lblName.Text = "نام خانوادگی"
                Srch.ShowDialog()
            Else
                btnOk.Focus()
            End If
        End If
    End Sub

    Private Sub txtFamily_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamily.TextChanged
        If txtFamily.Text = "" Then
            txtCode.Clear()
            txtName.Clear()
        End If
    End Sub

    Private Sub maskDatein_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDatein.GotFocus
        maskDatein.SelectionStart = 0
        maskDatein.SelectionLength = Len(maskDatein.Text)
    End Sub

    Private Sub maskDatein_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDatein.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDatein.Text
            Chkdate()
            If Datstat = 1 Then
                maskDatein.Focus()
                Exit Sub
            Else
                maskDateout.Focus()
            End If
        End If
    End Sub

    Private Sub maskDatein_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDatein.MaskInputRejected

    End Sub

    Private Sub maskDateout_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateout.GotFocus
        maskDateout.SelectionStart = 0
        maskDateout.SelectionLength = Len(maskDateout.Text)
    End Sub

    Private Sub maskDateout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateout.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateout.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateout.Focus()
                Exit Sub
            Else
                btnOk.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateout_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateout.MaskInputRejected

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If Me.maskDatein.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ شروع را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDatein.Focus()
            Exit Sub
        ElseIf Me.maskDateout.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ خاتمه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDateout.Focus()
            Exit Sub
        End If

        If RB2.Checked = True Then
            If RB3.Checked = True Then
                If txtName.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا نام خانوادگی را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtCode.Focus()
                    Exit Sub
                End If
            End If
        End If
        
        FillRepTable()
        FillDataSetAndDataView()
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        txtCode.SelectionStart = 0
        txtCode.SelectionLength = Len(txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim objCommand As New SqlCommand
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text = "" Then
                txtFamily.Text = ""
                txtName.Text = ""
                txtFamily.Focus()
            Else
                FormName = "RepWork"
                Pcd = txtCode.Text
                FillPers1()

                IntPosition = objDataview.Find(txtCode.Text)
                If IntPosition = -1 Then
                    MsgbOK.Label1.Text = " کد پرسنلی " & Trim(txtCode.Text) & " در سیستم تعریف نشده است . "
                    MsgbOK.ShowDialog()
                    txtCode.Focus()
                    Exit Sub
                Else
                    ' objCurrencyManager.Position = IntPosition
                    txtName.DataBindings.Clear()
                    txtFamily.DataBindings.Clear()
                    txtName.DataBindings.Add("Text", objDataview, "pers_name")
                    txtFamily.DataBindings.Add("Text", objDataview, "pers_family")
                End If
                txtFamily.Focus()
            End If
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

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
                    .Range("C1").Value = "گروه اشکال"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "سرفصل اشکال"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "شرح اشکال"
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
                    If CheckBox1.Checked = True Then
                        .Range("K1").Value = "شعبه"
                        .Range("K1").Font.Bold = True
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
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            If CheckBox1.Checked = True Then
                                .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(13).Value
                            End If
                            If DataGridView1.Rows(X).Cells(5).Value = 0 Then
                                .Range("A" & i.ToString).Font.Color = RGB(255, 0, 0)
                                .Range("B" & i.ToString).Font.Color = RGB(255, 0, 0)
                            End If
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

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        If RB3.Checked = True Then
            txtFamily.Text = ""
            txtCode.Enabled = True
            txtFamily.Enabled = True
            cboNProblem.Enabled = False
            cboGProblem.Enabled = False
            txtCode.Focus()
        End If
    End Sub

    Private Sub RB5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB5.CheckedChanged
        If RB5.Checked = True Then
            txtFamily.Text = ""
            txtCode.Enabled = False
            txtFamily.Enabled = False
            cboNProblem.Enabled = True
            cboGProblem.Enabled = False
            cboNProblem.Focus()
        End If
    End Sub

    Private Sub RB4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB4.CheckedChanged
        If RB4.Checked = True Then
            txtFamily.Text = ""
            txtCode.Enabled = False
            txtFamily.Enabled = False
            cboNProblem.Enabled = False
            cboGProblem.Enabled = True
            cboGProblem.Focus()
        End If
    End Sub

    Private Sub RB6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB6.CheckedChanged
        If RB6.Checked = True Then
            txtFamily.Text = ""
            txtCode.Enabled = False
            txtFamily.Enabled = False
            cboNProblem.Enabled = False
            cboGProblem.Enabled = False
        End If
    End Sub
End Class