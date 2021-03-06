
Public Class ShobMove
    Public Sub FillCboMove()
        Dim a As String
        a = 0
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE (Shob_Code <> " & a & ") AND (Shob_Code <> " & Shob & ") ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Shob")
        objDataview = New DataView(objDataset.Tables("bas.Shob"))
        cboMove.DataSource = objDataset.Tables("bas.Shob")
        cboMove.DisplayMember = "Shob_Name"
        cboMove.ValueMember = "Shob_Code"
    End Sub

    Private Sub SaveLogComputer()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = cboMove.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].ComputerLog(Row, pers_code, Dat, Vahed_code, semat_code, mainboard, cpu_code, hdd_code, vga_code, modem_code, optic_code, monitor_code, keyboard_code, mouse_code, scan_code, a_code, prn_code, Ram, Dscr, Knd, DatMov, Sal, Shob_Code) " & _
           " SELECT Row, pers_code, Dat, Vahed_code, semat_code, mainboard, cpu_code, hdd_code, vga_code, modem_code, optic_code, monitor_code, keyboard_code, mouse_code, scan_code, a_code, prn_code, Ram, Dscr, Knd, DatMov, Sal, Shob_Code FROM Bnk.Computer " & _
           " WHERE (Shob_Code = " & Shob & ") AND (Row = " & Computer.txtRow.Text & ")"

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
        objcommand.CommandText = _
            " UPDATE [Log].ComputerLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
            " WHERE (Row = " & Computer.txtRow.Text & ") AND (Shob_Code = " & Shob & ") AND (tim_stat IS NULL)"
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

    Private Sub SaveLogLapTop()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = cboMove.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].LapTopLog(Row, pers_code, Dat, Vahed_code, semat_code, Mark, Model, cpu_code, hdd_code, vga_code, Lcd_code, mouse_code, scan_code, prn_code, Ram, Dscr, DatS, DscrS, Knd, DatMov, Sal, Shob_Code)" & _
           " SELECT Row, pers_code, Dat, Vahed_code, semat_code, Mark, Model, cpu_code, hdd_code, vga_code, LCD_code, mouse_code, scan_code, prn_code, Ram, Dscr, DatS, DscrS, Knd, DatMov, Sal, Shob_Code" & _
           " FROM Bnk.LapTop WHERE (Row = " & LapTop.txtRow.Text & ") AND (Shob_Code = " & Shob & ")"

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
        objcommand.CommandText = _
            " UPDATE [Log].LapTopLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
            " WHERE (Row = " & LapTop.txtRow.Text & ") AND (Shob_Code = " & Shob & ") AND (tim_stat IS NULL)"
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

    Private Sub SaveLogTerminal()
        Dim objcommand As New SqlCommand
        Dim PcName As String
        Dim Tim As String
        Dim Dat As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        IntPosition = objCurrencyManager.Position
        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " INSERT INTO [Log].TerminalLog(Row, pers_code, Dat, Vahed_code, semat_code, main_code, SerialNo, Ram, Mobil, Pencel, Dscr, Area_code, DatS, DscrS, InVahed, Tamir, Stock, Send, Sal, Shob_Code)" & _
           " SELECT Row, pers_code, Dat, Vahed_code, semat_code, main_code, SerialNo, Ram, Mobil, Pencel, Dscr, Area_code, DatS, DscrS, InVahed, Tamir, Stock, Send, Sal, Shob_Code FROM Bnk.Terminal" & _
           " WHERE (Row = " & KalaOut.txtPage.Text & ") AND (Shob_Code = " & Shob & ")"

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
        objcommand.CommandText = _
            " UPDATE [Log].TerminalLog SET U_Code = " & User_Code & ", status = " & logStat & ", tim_stat = '" & Tim & "', dat_stat = '" & Dat & "', PcName = '" & PcName & "'" & _
            " WHERE (Row = " & KalaOut.txtPage.Text & ") AND (Shob_Code = " & Shob & ") AND (tim_stat IS NULL)"
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

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub ShobMove_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillCboMove()
        cboMove1.SelectedIndex = 0
        cboMove2.SelectedIndex = 0
        If FormName = "Computer" Then
            RB3.Enabled = False
        ElseIf FormName = "LapTop" Then
            RB2.Enabled = False
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim objCommand As New SqlCommand
        Dim X As Integer
        Dim Rw As String
        Dim Shb As String
        Dim Dat As String

        Shb = cboMove.SelectedValue.ToString
        Dat = ConvD.Class1.ConvDate

        If FormName = "Computer" Then
            If Computer.RB1.Checked = True Then
                Knd = 1
            ElseIf Computer.RB2.Checked = True Then
                Knd = 2
            ElseIf Computer.RB3.Checked = True Then
                Knd = 3
            ElseIf Computer.RB4.Checked = True Then
                Knd = 4
            ElseIf Computer.RB5.Checked = True Then
                Knd = 5
            End If

            If RB1.Checked = True Then ' Shob - Computer
                MsgbYN.Label1.Text = "  آیا از انتقال به شعبه " & cboMove.Text & " اطمینان دارید ؟"
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnOk.Focus()
                    Exit Sub
                Else
                    For X = 0 To Computer.DataGridView1.RowCount - 1
                        If Computer.DataGridView1.Rows(X).Selected = True Then
                            If Computer.DataGridView1.Rows(X).Cells(25).Value <> Me.cboMove.SelectedValue Then
                                Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
                                Rw = Computer.DataGridView1.Rows(X).Cells(0).Value

                                '===========================================جستجوی آخرین شماره برگه شعبه مقابل
                                Dim objDataAdapter As New SqlDataAdapter _
                                    (" SELECT MAX(Row) AS Expr1 FROM Bnk.Computer where(Shob_code = " & Shb & ")", objConnection)
                                objDataset = New DataSet
                                objDataAdapter.Fill(objDataset, "Bnk.Computer")
                                objDataview = New DataView(objDataset.Tables("Bnk.Computer"))
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

                                objCommand.Connection = objConnection
                                objCommand.CommandText = _
                                   " UPDATE Bnk.Computer SET Shob_Code = @Shb, Row = @Row, DatMov = @Dat WHERE (Row = " & Rw & ") AND (Shob_Code = " & Shob & ")"
                                objCommand.CommandType = CommandType.Text
                                objCommand.Parameters.Clear()
                                objCommand.Parameters.AddWithValue("@Shb", Shb)
                                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                                objCommand.Parameters.AddWithValue("@Dat", Dat)

                                objConnection.Open()
                                Try
                                    objCommand.ExecuteNonQuery()
                                Catch SqlExceptionErr As SqlException
                                    MessageBox.Show(SqlExceptionErr.Message)
                                    objConnection.Close()
                                    Exit Sub
                                End Try
                                objConnection.Close()

                                '--------ComputerLog
                                logStat = 3
                                SaveLogComputer()
                            End If
                        End If
                    Next
                End If
            ElseIf RB2.Checked = True Then ' Knd - Computer
                MsgbYN.Label1.Text = "  آیا از انتقال به گروه " & cboMove1.Text & " اطمینان دارید ؟"
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnOk.Focus()
                    Exit Sub
                Else
                    For X = 0 To Computer.DataGridView1.RowCount - 1
                        If Computer.DataGridView1.Rows(X).Selected = True Then
                            If Computer.DataGridView1.Rows(X).Cells(28).Value <> Me.cboMove1.SelectedValue Then
                                Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
                                Knd = cboMove1.SelectedIndex + 1
                                Rw = Computer.DataGridView1.Rows(X).Cells(0).Value

                                If Knd <> 3 Then
                                    objCommand.Connection = objConnection
                                    objCommand.CommandText = _
                                       " UPDATE Bnk.Computer SET Knd = " & Knd & " WHERE (Row = " & Rw & ") AND (Shob_Code = " & Shob & ")"
                                    objCommand.CommandType = CommandType.Text
                                    objCommand.Parameters.Clear()
                                    objCommand.Parameters.AddWithValue("@Knd", Knd)

                                    objConnection.Open()
                                    Try
                                        objCommand.ExecuteNonQuery()
                                    Catch SqlExceptionErr As SqlException
                                        MessageBox.Show(SqlExceptionErr.Message)
                                        objConnection.Close()
                                        Exit Sub
                                    End Try
                                    objConnection.Close()

                                    '--------ComputerLog
                                    logStat = 3
                                    SaveLogComputer()
                                Else
                                    MsgbOK.Label1.Text = " لطفا جهت صورتجلسه کردن سیستم از کلید صورتجلسه واقع در فرم استفاده کنید . "
                                    MsgbOK.ShowDialog()
                                    btnOk.Focus()
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            Computer.Ref.PerformClick()
            '================================================================================
        ElseIf FormName = "LapTop" Then
            If LapTop.RB1.Checked = True Then
                Knd = 1
            ElseIf LapTop.RB2.Checked = True Then
                Knd = 2
            ElseIf LapTop.RB3.Checked = True Then
                Knd = 3
            ElseIf LapTop.RB4.Checked = True Then
                Knd = 4
            End If

            If RB1.Checked = True Then ' Shob - Laptop
                MsgbYN.Label1.Text = "  آیا از انتقال به شعبه " & cboMove.Text & " اطمینان دارید ؟"
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnOk.Focus()
                    Exit Sub
                Else
                    For X = 0 To LapTop.DataGridView1.RowCount - 1
                        If LapTop.DataGridView1.Rows(X).Selected = True Then
                            If LapTop.DataGridView1.Rows(X).Cells(24).Value <> Me.cboMove.SelectedValue Then
                                Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
                                Rw = LapTop.DataGridView1.Rows(X).Cells(0).Value

                                '===========================================جستجوی آخرین شماره برگه شعبه مقابل
                                Dim objDataAdapter As New SqlDataAdapter _
                                    (" SELECT MAX(Row) AS Expr1 FROM Bnk.LapTop where(Shob_code = " & Shb & ")", objConnection)
                                objDataset = New DataSet
                                objDataAdapter.Fill(objDataset, "Bnk.LapTop")
                                objDataview = New DataView(objDataset.Tables("Bnk.LapTop"))
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

                                objCommand.Connection = objConnection
                                objCommand.CommandText = _
                                   " UPDATE Bnk.LapTop SET Shob_Code = @Shb, Row = @Row, DatMov = @Dat WHERE (Row = " & Rw & ") AND (Shob_Code = " & Shob & ")"
                                objCommand.CommandType = CommandType.Text
                                objCommand.Parameters.Clear()
                                objCommand.Parameters.AddWithValue("@Shb", Shb)
                                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                                objCommand.Parameters.AddWithValue("@Dat", Dat)

                                objConnection.Open()
                                Try
                                    objCommand.ExecuteNonQuery()
                                Catch SqlExceptionErr As SqlException
                                    MessageBox.Show(SqlExceptionErr.Message)
                                    objConnection.Close()
                                    Exit Sub
                                End Try
                                objConnection.Close()

                                '--------LaptopLog
                                logStat = 3
                                SaveLogLapTop()
                            End If
                        End If
                    Next
                End If
            ElseIf RB3.Checked = True Then ' Knd - Laptop
                MsgbYN.Label1.Text = "  آیا از انتقال به گروه " & cboMove1.Text & " اطمینان دارید ؟"
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnOk.Focus()
                    Exit Sub
                Else
                    For X = 0 To LapTop.DataGridView1.RowCount - 1
                        If LapTop.DataGridView1.Rows(X).Selected = True Then
                            If LapTop.DataGridView1.Rows(X).Cells(30).Value <> Me.cboMove2.SelectedValue Then
                                Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
                                Knd = cboMove2.SelectedIndex + 1
                                Rw = LapTop.DataGridView1.Rows(X).Cells(0).Value

                                objCommand.Connection = objConnection
                                objCommand.CommandText = _
                                   " UPDATE Bnk.LapTop SET Knd = " & Knd & " WHERE (Row = " & Rw & ") AND (Shob_Code = " & Shob & ")"
                                objCommand.CommandType = CommandType.Text
                                objCommand.Parameters.Clear()
                                objCommand.Parameters.AddWithValue("@Knd", Knd)

                                objConnection.Open()
                                Try
                                    objCommand.ExecuteNonQuery()
                                Catch SqlExceptionErr As SqlException
                                    MessageBox.Show(SqlExceptionErr.Message)
                                    objConnection.Close()
                                    Exit Sub
                                End Try
                                objConnection.Close()

                                '--------LaptopLog
                                logStat = 3
                                SaveLogLapTop()
                            End If
                        End If
                    Next
                End If
            End If
            LapTop.Ref.PerformClick()
            '================================================================================
        ElseIf FormName = "Terminal" Then
            'If CheckBox1.Checked = True Then
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " UPDATE Bnk.Terminal SET pers_code = @pcode, Dat = @dat, InVahed = @InVahed, Tamir = @Tamir, Stock = @Stock, Send = @Send, Sal = @sal, Shob_Code = @shob" & _
               " WHERE (Row = " & KalaOut.txtPage.Text & ") AND (Shob_Code = " & Shob & ")"
            objCommand.CommandType = CommandType.Text
            'objCommand.Parameters.AddWithValue("@Pcode", txtCode.Text)
            'objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@Invahed", "False")
            objCommand.Parameters.AddWithValue("@Tamir", "False")
            objCommand.Parameters.AddWithValue("@Stock", "False")
            objCommand.Parameters.AddWithValue("@Send", "False")
            'Else
            '    objCommand.Connection = objConnection
            '    objCommand.CommandText = _
            '       " UPDATE Bnk.Terminal SET pers_code = @pcode, Dat = @dat, Sal = @sal, Shob_Code = @shob" & _
            '       " WHERE (Row = " & Terminal.txtRow.Text & ") AND (Shob_Code = " & Shob & ")"
            '    objCommand.CommandType = CommandType.Text
            '    objCommand.Parameters.AddWithValue("@Pcode", maskCode.Text)
            '    objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            'End If
            objCommand.Parameters.AddWithValue("@Sal", Mainfrm.cboSal_Mali.Text)
            objCommand.Parameters.AddWithValue("@Shob", cboMove.SelectedValue.ToString)

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            '--------TerminalLog
            logStat = 3
            SaveLogTerminal()
            '--------------------
        End If

        Me.Dispose()
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs)

    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        Label1.Enabled = False
        cboMove.Enabled = False
        Label2.Enabled = True
        cboMove1.Enabled = True
        Label4.Enabled = False
        cboMove2.Enabled = False
    End Sub

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        Label1.Enabled = False
        cboMove.Enabled = False
        Label2.Enabled = False
        cboMove1.Enabled = False
        Label4.Enabled = True
        cboMove2.Enabled = True
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB1.Click
        Label1.Enabled = True
        cboMove.Enabled = True
        Label2.Enabled = False
        cboMove1.Enabled = False
        Label4.Enabled = False
        cboMove2.Enabled = False
    End Sub
End Class