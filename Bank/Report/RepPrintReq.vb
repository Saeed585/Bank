Public Class RepPrintReq

    Private Sub FillCboShob()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Shob")
        Me.cboShob.DataSource = objDataset.Tables("Bas.Shob")
        Me.cboShob.DisplayMember = "Shob_Name"
        Me.cboShob.ValueMember = "Shob_Code"
    End Sub

    Private Sub FillRepTable()
        If RB1.Checked = True Then
            Rb = 1
            txtCodeP.Text = 0
            txtSerial.Text = 0
        ElseIf RB2.Checked = True Then
            Rb = 2
            If RB3.Checked = True Then
                Rbin = 3
                txtSerial.Text = 0
            ElseIf RB4.Checked = True Then
                Rbin = 4
                txtCodeP.Text = 0
            End If
        End If

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.CreateTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 29)

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
            objCommand.CommandText = "Rep.PrintReq"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Rb", Rb)
            If RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@Rbin", Rbin)
            Else
                objCommand.Parameters.AddWithValue("@Rbin", 0)
            End If
            objCommand.Parameters.AddWithValue("@Datin", maskDatein.Text)
            objCommand.Parameters.AddWithValue("@Datout", maskDateout.Text)
            objCommand.Parameters.AddWithValue("@ShobF", cboShob.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Printer", txtCodeP.Text)
            objCommand.Parameters.AddWithValue("@Serial", txtSerial.Text)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Dat, VahedName, PrinterName, Serial, ShobName, Mbl, Dscr, DscrFact FROM Rep.RepPrintReq ORDER BY Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Rep.RepPrintReq")
        objDataview = New DataView(objDataset.Tables("Rep.RepPrintReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).HeaderText = "واحد سازمانی"
        Me.DataGridView1.Columns(3).HeaderText = "پرینتر"
        Me.DataGridView1.Columns(4).HeaderText = "سریال"
        Me.DataGridView1.Columns(5).HeaderText = "محل فعالیت"
        Me.DataGridView1.Columns(6).HeaderText = "مبلغ"
        Me.DataGridView1.Columns(7).HeaderText = "شرح"
        Me.DataGridView1.Columns(8).HeaderText = "شرح فاکتور"

        DataGridView1.Columns(6).DefaultCellStyle.Format = "N0"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 100
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(4).Width = 120
        Me.DataGridView1.Columns(5).Width = 120
        Me.DataGridView1.Columns(6).Width = 120
        Me.DataGridView1.Columns(7).Width = 300
        Me.DataGridView1.Columns(8).Width = 300

        txtSumMbl.Text = 0
        For X = 0 To DataGridView1.RowCount - 1
            txtSumMbl.Text = Val(Str(txtSumMbl.Text)) + Val(Str(DataGridView1.Rows(X).Cells(6).Value))
        Next
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub RepPrintReq_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.DropTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 29)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub RepPrintReq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        FillCboShob()
        maskDatein.Text = ConvD.Class1.ConvDate
        maskDateout.Text = ConvD.Class1.ConvDate

        txtCodeP.SendToBack()
        FormName = "RepPrintReq"
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub RepPrintReq_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 221
    End Sub

    Private Sub maskDatein_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDatein.GotFocus
        Me.maskDatein.SelectionStart = 0
        Me.maskDatein.SelectionLength = Len(Me.maskDatein.Text)
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
        Me.maskDateout.SelectionStart = 0
        Me.maskDateout.SelectionLength = Len(Me.maskDateout.Text)
    End Sub

    Private Sub maskDateout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateout.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateout.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateout.Focus()
                Exit Sub
            Else
                If RB1.Checked = True Then
                    BtnOK.Focus()
                Else
                    If RB3.Checked = True Then
                        txtName.Focus()
                    ElseIf RB4.Checked = True Then
                        txtSerial.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub maskDateout_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateout.MaskInputRejected

    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCodeP.Text = "0" Or txtCodeP.Text = "" Then
                Z = 1
                Code_Soft = 15
                FormName = "RepPrintReq"
                FillPrintRep()
                FillDataPrint()
                Srch.Width = 700
                Srch.ToolStripLabel2.Width = 480
                Srch.lblName.Text = "لیست پرینتر"
                Srch.ShowDialog()
            Else
                BtnOK.Focus()
            End If
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCodeP.Text = ""
        End If
    End Sub

    Private Sub txtSerial_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSerial.GotFocus
        Me.txtSerial.SelectionStart = 0
        Me.txtSerial.SelectionLength = Len(Me.txtSerial.Text)
    End Sub

    Private Sub txtSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtSerial.Text = "0" Or txtSerial.Text = "" Then
                Z = 2
                Code_Soft = 15
                FormName = "RepPrintReq"
                FillSerial()
                FillDataSerial()
                Srch.Width = 700
                Srch.ToolStripLabel2.Width = 480
                Srch.lblName.Text = "لیست سریال پرینتر"
                Srch.ShowDialog()
            Else
                BtnOK.Focus()
            End If
        End If
    End Sub

    Private Sub txtSerial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerial.TextChanged

    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            Grp1.Enabled = False
            RB3.Checked = True
            txtCodeP.Text = ""
            txtName.Text = ""
            txtSerial.Text = ""
            maskDatein.Focus()
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            Grp1.Enabled = True
            txtCodeP.Text = ""
            txtName.Text = ""
            txtSerial.Text = ""
            maskDatein.Focus()
        End If
    End Sub

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        If RB3.Checked = True Then
            txtName.Enabled = True
            txtSerial.Enabled = False
            txtSerial.Text = ""
            txtName.Focus()
        End If
    End Sub

    Private Sub RB4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB4.CheckedChanged
        If RB4.Checked = True Then
            txtName.Enabled = False
            txtSerial.Enabled = True
            txtCodeP.Text = ""
            txtName.Text = ""
            txtSerial.Focus()
        End If
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        If RB1.Checked = True Then
            If maskDatein.Text = "1   /  /" Then
                MsgbOK.Label1.Text = " لطفا تاریخ را وارد کنید . "
                MsgbOK.ShowDialog()
                maskDatein.Focus()
                Exit Sub
            End If
        ElseIf RB2.Checked = True Then
            If RB3.Checked = True Then
                If txtCodeP.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا شروع شماره برگه را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtName.Focus()
                    Exit Sub
                End If
            ElseIf RB4.Checked = True Then
                If txtSerial.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد کنید . "
                    MsgbOK.ShowDialog()
                    txtSerial.Focus()
                    Exit Sub
                End If
            End If
        End If

        FillRepTable()
        FillDatasetAndDataview()
        FillDataGridView()
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
                    .Range("A1").Value = "ردیف"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "تاریخ"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "واحد سازمانی"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "پرینتر"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "سریال"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "محل فعالیت"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "مبلغ"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "شرح"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "شرح فاکتور"
                    .Range("I1").Font.Bold = True

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
                            .Range("G" & i.ToString).NumberFormat = "#,##0"
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            i += 1
                        End If
                    Next X
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgbOK.Label1.Text = "رکوردی یافت نشد . "
            MsgbOK.ShowDialog()
            btnExcel.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtSumMbl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSumMbl.TextChanged
        txtSumMbl.Text = Format(Val(txtSumMbl.Text.Trim.Replace(",", "")), "#,0")
        txtSumMbl.SelectionStart = txtSumMbl.TextLength
    End Sub
End Class