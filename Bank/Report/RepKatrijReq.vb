
Public Class RepKatrijReq
    Private Sub FillRepTable()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If RB1.Checked = True Then
            Rb = 1
            txtCodeK.Text = 0
        ElseIf RB2.Checked = True Then
            Rb = 2
            If RB3.Checked = True Then
                Rbin = 3
            End If
        End If

        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.CreateTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 42)

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
            objCommand.CommandText = "Rep.KatrijReq"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Rb", Rb)
            If RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@Rbin", Rbin)
            Else
                objCommand.Parameters.AddWithValue("@Rbin", 0)
            End If
            objCommand.Parameters.AddWithValue("@Datin", maskDatein.Text)
            objCommand.Parameters.AddWithValue("@Datout", maskDateout.Text)
            objCommand.Parameters.AddWithValue("@Code", txtCodeK.Text)
            objCommand.Parameters.AddWithValue("@Knd", cboReq.SelectedIndex)
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
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Rep.RepKatrijReq.Row, Rep.RepKatrijReq.Dat, Rep.RepKatrijReq.Knd, Rep.RepKatrijReq.KndName, Rep.RepKatrijReq.Code, Rep.RepKatrijReq.Name, Rep.RepKatrijReq.Count, Rep.RepKatrijReq.Mbl, Rep.RepKatrijReq.Dscr, Rep.RepKatrijReq.Shob, Bas.Shob.Shob_Name" & _
             " FROM Rep.RepKatrijReq INNER JOIN Bas.Shob ON Rep.RepKatrijReq.Shob = Bas.Shob.Shob_Code ORDER BY Rep.RepKatrijReq.Row", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Rep.RepKatrijReq")
        objDataview = New DataView(objDataset.Tables("Rep.RepKatrijReq"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"
        lblRow.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer
        If objDataview.Count > 0 Then
            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview
        Else
            Me.DataGridView1.AutoGenerateColumns = True
            objDataview.Dispose()
            Me.DataGridView1.DataSource = objDataview
        End If

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ"
        Me.DataGridView1.Columns(2).Visible = False ' Knd
        Me.DataGridView1.Columns(3).HeaderText = "نوع سرویس"
        Me.DataGridView1.Columns(4).Visible = False ' Code
        Me.DataGridView1.Columns(5).HeaderText = "نام"
        Me.DataGridView1.Columns(6).HeaderText = "تعداد"
        Me.DataGridView1.Columns(7).HeaderText = "مبلغ"
        Me.DataGridView1.Columns(8).HeaderText = "شرح"
        Me.DataGridView1.Columns(9).Visible = False ' Shob
        Me.DataGridView1.Columns(10).HeaderText = "شعبه"

        DataGridView1.Columns(7).DefaultCellStyle.Format = "N0"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(3).Width = 80
        Me.DataGridView1.Columns(5).Width = 100
        Me.DataGridView1.Columns(6).Width = 80
        Me.DataGridView1.Columns(7).Width = 120
        Me.DataGridView1.Columns(8).Width = 400
        Me.DataGridView1.Columns(10).Width = 120

        txtSumMbl.Text = 0
        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Cells(2).Value = 0 Then
                DataGridView1.Rows(X).Cells(3).Value = "شارژ"
            ElseIf DataGridView1.Rows(X).Cells(2).Value = 1 Then
                DataGridView1.Rows(X).Cells(3).Value = "تعمیر"
            End If
            txtSumMbl.Text = Val(Str(txtSumMbl.Text)) + Val(Str(DataGridView1.Rows(X).Cells(7).Value))
        Next
    End Sub
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub RepKatrijReq_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            objConnection.Open()
            objCommand = New SqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Rep.DropTbl"

            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@RepNo", 42)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub RepKatrijReq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm
        Me.maskDatein.Text = ConvD.Class1.ConvDate()
        Me.maskDateout.Text = ConvD.Class1.ConvDate()
        Me.cboReq.SelectedIndex = 2
        txtCodeK.SendToBack()
    End Sub

    Private Sub RepKatrijReq_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 235
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
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
                    .Range("C1").Value = "نوع سرویس"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "نام"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "تعداد"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "مبلغ"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "شرح"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "شعبه"
                    .Range("H1").Font.Bold = True

                    X = Me.DataGridView1.CurrentCell.RowIndex
                    For X = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(0).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("F" & i.ToString).NumberFormat = "#,##0"
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
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

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        FillRepTable()
        FillDataSetAndDataView()
        FillDataGridView()
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
                BtnOK.Focus()
            End If
        End If
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.SelectionStart = 0
        txtName.SelectionLength = Len(txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            If txtCodeK.Text = "" Then
                Code_Soft = 15
                FormName = "RepKatrijReq"
                FillKatrijReq()
                FillDataKatrij()
                Srch.lblName.Text = "لیست کاتریج"
                Srch.ShowDialog()
            Else
                BtnOK.Focus()
            End If
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCodeK.Text = ""
        End If
    End Sub

    Private Sub maskDateout_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateout.MaskInputRejected

    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            Grp1.Enabled = False
            RB3.Checked = True
            txtCodeK.Text = ""
            txtName.Text = ""
            maskDatein.Focus()
        End If
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            Grp1.Enabled = True
            txtCodeK.Text = ""
            txtName.Text = ""
            maskDatein.Focus()
        End If
    End Sub

    Private Sub txtSumMbl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSumMbl.TextChanged
        txtSumMbl.Text = Format(Val(txtSumMbl.Text.Trim.Replace(",", "")), "#,0")
        txtSumMbl.SelectionStart = txtSumMbl.TextLength
    End Sub
End Class