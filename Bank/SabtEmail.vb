Public Class SabtEmail
    Dim ColumnSrch As Integer

    Public Sub FillRow()
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(Row) AS Expr1 FROM Bnk.Email WHERE (Shob = " & Shob & ") AND (Sal = " & Sal & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Email")
        objDataview = New DataView(objDataset.Tables("Bnk.Email"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataview, "Expr1")
        If Label6.Text <> "" Then
            txtRow.Text = Val(Label6.Text) + 1
        ElseIf Label6.Text = "" Then
            txtRow.Text = Sal & "00001"
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim Da As New SqlDataAdapter
        Dim Sal As String

        If RB4.Checked = True Then
            Knd = 1
        ElseIf RB5.Checked = True Then
            Knd = 2
        End If
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If RB1.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("SELECT Row, Dat, Name, DatKh, DatMali, DstRecv, DatInOut, Stat, StatDscr, Chk, ChkDscr, Dscr, LetterNo, DarNo, DarKhNo, Knd, ChkH, Shob, Sal FROM Bnk.View_Email" & _
                                            " WHERE (Knd = " & Knd & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ") AND (Stat < 4) OR (Knd = " & Knd & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ") AND (Stat = 6) ORDER BY Dat DESC")
            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_Email")
            objDataview = New DataView(objDataset.Tables("Bnk.View_Email"))
            ' objDataview.Sort = "Row"
            objConnection.Close()
        ElseIf RB2.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_Email WHERE (Knd = " & Knd & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ") AND (Stat = 4) ORDER BY Dat DESC")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_Email")
            objDataview = New DataView(objDataset.Tables("Bnk.View_Email"))
            objConnection.Close()
        ElseIf RB3.Checked = True Then
            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_Email WHERE (Knd = " & Knd & ") AND (Shob = " & Shob & ") AND (Sal = " & Sal & ") AND (Stat = 5) ORDER BY Dat DESC")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_Email")
            objDataview = New DataView(objDataset.Tables("Bnk.View_Email"))
            objConnection.Close()
        End If
        txtCount.Text = objDataview.Count
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "شماره برگه"
        Me.DataGridView1.Columns(1).HeaderText = "تاریخ ثبت"
        Me.DataGridView1.Columns(2).HeaderText = "شرح"
        Me.DataGridView1.Columns(3).HeaderText = "ثبت درخواست خرید"
        Me.DataGridView1.Columns(4).HeaderText = "تائید مالی"
        Me.DataGridView1.Columns(5).HeaderText = "دریافت کالا"
        Me.DataGridView1.Columns(6).HeaderText = "تحویل/ارسال"
        Me.DataGridView1.Columns(7).Visible = False ' Stat
        Me.DataGridView1.Columns(8).HeaderText = "وضعیت"
        Me.DataGridView1.Columns(9).Visible = False ' Chk
        Me.DataGridView1.Columns(10).HeaderText = "محل خرید"
        Me.DataGridView1.Columns(11).HeaderText = "توضیحات"
        Me.DataGridView1.Columns(12).HeaderText = "شماره نامه"
        Me.DataGridView1.Columns(13).HeaderText = "شماره درخواست"
        Me.DataGridView1.Columns(14).HeaderText = "شماره درخواست خرید"
        Me.DataGridView1.Columns(15).Visible = False ' Knd
        Me.DataGridView1.Columns(16).Visible = False ' ChkH
        Me.DataGridView1.Columns(17).Visible = False ' Shob
        Me.DataGridView1.Columns(18).Visible = False ' Sal

        Me.DataGridView1.Columns(0).Width = 70
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 480
        Me.DataGridView1.Columns(3).Width = 80
        Me.DataGridView1.Columns(4).Width = 70
        Me.DataGridView1.Columns(5).Width = 70
        Me.DataGridView1.Columns(6).Width = 70
        Me.DataGridView1.Columns(10).Width = 80
        Me.DataGridView1.Columns(11).Width = 240
        Me.DataGridView1.Columns(12).Width = 120
        Me.DataGridView1.Columns(13).Width = 60
        Me.DataGridView1.Columns(14).Width = 90

        EditDataGridView()
    End Sub

    Private Sub EditDataGridView()
        Dim X As Integer
        Dim Chk As String

        txtCountNow.Text = 0
        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(X).Cells(7).Value = 0 Then
                DataGridView1.Rows(X).Cells(8).Value = "در انتظار ثبت خرید"
                DataGridView1.Rows(X).Cells(8).Style.ForeColor = Color.Red
            ElseIf DataGridView1.Rows(X).Cells(7).Value = 1 Then
                DataGridView1.Rows(X).Cells(8).Value = "ثبت درخواست خرید"
                DataGridView1.Rows(X).Cells(8).Style.ForeColor = Color.Brown
            ElseIf DataGridView1.Rows(X).Cells(7).Value = 2 Then
                DataGridView1.Rows(X).Cells(8).Value = "درحال اجرا"
                DataGridView1.Rows(X).Cells(8).Style.ForeColor = Color.Blue
                txtCountNow.Text = Val(txtCountNow.Text) + 1
            ElseIf DataGridView1.Rows(X).Cells(7).Value = 3 Then
                DataGridView1.Rows(X).Cells(8).Value = "درانتظار صدور چک"
                DataGridView1.Rows(X).Cells(8).Style.ForeColor = Color.Purple
            ElseIf DataGridView1.Rows(X).Cells(7).Value = 4 Then
                DataGridView1.Rows(X).Cells(8).Value = "انجام شده"
                'DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Wheat
            ElseIf DataGridView1.Rows(X).Cells(7).Value = 5 Then
                DataGridView1.Rows(X).Cells(8).Value = "کنسل شده"
                DataGridView1.Rows(X).Cells(8).Style.ForeColor = Color.Black
            ElseIf DataGridView1.Rows(X).Cells(7).Value = 6 Then
                DataGridView1.Rows(X).Cells(8).Value = "جهت پیگیری"
                DataGridView1.Rows(X).Cells(8).Style.ForeColor = Color.Black
            End If

            If DataGridView1.Rows(X).Cells(9).Value = 1 Then
                DataGridView1.Rows(X).Cells(10).Value = "انبار ساوه"
            ElseIf DataGridView1.Rows(X).Cells(9).Value = 2 Then
                DataGridView1.Rows(X).Cells(10).Value = "خوشگوار تهران"
            End If

            If Not IsDBNull(DataGridView1.Rows(X).Cells(16).Value) Then
                Chk = DataGridView1.Rows(X).Cells(16).Value
                If Chk = 1 Then
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
                Else
                    DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
                End If
            Else
                DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub HighLight()
        Dim X As Integer
        Dim Chk As String
        Dim Rw As String

        X = DataGridView1.CurrentCell.RowIndex
        Rw = DataGridView1.Rows(X).Cells(0).Value

        If DataGridView1.Rows(X).Cells(16).Value = 1 Then
            Chk = 0
            DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.White
        Else
            Chk = 1
            DataGridView1.Rows(X).DefaultCellStyle.BackColor = Color.Yellow
        End If

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " UPDATE Bnk.Email SET ChkH = @ChkH WHERE (Row = " & Rw & ")"
        objCommand.CommandType = CommandType.Text
        objCommand.Parameters.Clear()
        objCommand.Parameters.AddWithValue("@ChkH", Chk)

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

    Private Sub Clear()
        Me.maskDate.Text = ConvD.Class1.ConvDate
        Me.maskDateMali.Text = ConvD.Class1.ConvDate
        Me.txtLetterNo.Text = 0
        Me.txtDarNo.Text = 0
        Me.txtDarKhNo.Text = 0
        Me.txtName.Text = ""
        Me.maskDateKh.Text = "1   /  /"
        Me.maskDateMali.Text = "1   /  /"
        Me.maskDateRecv.Text = "1   /  /"
        Me.maskDateInOut.Text = "1   /  /"
        Me.txtDscr.Text = ""
        Me.cboKharid.SelectedIndex = 0
        Me.maskDate.Focus()
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        X = Me.DataGridView1.CurrentCell.RowIndex
        Me.txtRow.Text = Me.DataGridView1.Rows(X).Cells(0).Value
        Me.maskDate.Text = Me.DataGridView1.Rows(X).Cells(1).Value
        Me.txtName.Text = Me.DataGridView1.Rows(X).Cells(2).Value
        Me.maskDateKh.Text = Me.DataGridView1.Rows(X).Cells(3).Value
        Me.maskDateMali.Text = Me.DataGridView1.Rows(X).Cells(4).Value
        Me.maskDateRecv.Text = Me.DataGridView1.Rows(X).Cells(5).Value
        Me.maskDateInOut.Text = Me.DataGridView1.Rows(X).Cells(6).Value
        Me.cboStat.SelectedIndex = Me.DataGridView1.Rows(X).Cells(7).Value
        Me.cboKharid.SelectedIndex = Me.DataGridView1.Rows(X).Cells(9).Value
        Me.txtDscr.Text = Me.DataGridView1.Rows(X).Cells(11).Value
        Me.txtLetterNo.Text = Me.DataGridView1.Rows(X).Cells(12).Value
        Me.txtDarNo.Text = Me.DataGridView1.Rows(X).Cells(13).Value
        Me.txtDarKhNo.Text = Me.DataGridView1.Rows(X).Cells(14).Value
    End Sub


    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            maskDateKh.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub Del()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT Row FROM Bnk.Email WHERE (Shob = " & Shob & ") AND (Dat = N'" & maskDate.Text & "') AND (Row = " & txtRow.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Email")
        objDataview = New DataView(objDataset.Tables("Bnk.Email"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(Me.txtRow.Text) & "  در سیستم ثبت نشده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره برگه  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                maskDate.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                Ref.PerformClick()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub Edt()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If Me.maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ ثبت را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDate.Focus()
            Exit Sub
        ElseIf Me.txtDarNo.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا شماره درخواست را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtDarNo.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT Row FROM Bnk.Email WHERE (Shob = " & Shob & ") AND (Dat = N'" & maskDate.Text & "') AND (Row = " & txtRow.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Email")
        objDataview = New DataView(objDataset.Tables("Bnk.Email"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(Me.txtRow.Text) & "  در سیستم ثبت نشده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
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

        If RB4.Checked = True Then
            Knd = 1
        ElseIf RB5.Checked = True Then
            Knd = 2
        End If
        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bnk.InsUpdEmail"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
            objCommand.Parameters.AddWithValue("@Knd", Knd)
            objCommand.Parameters.AddWithValue("@Dat", maskDate.Text)
            objCommand.Parameters.AddWithValue("@LetterNo", txtLetterNo.Text)
            objCommand.Parameters.AddWithValue("@DarNo", txtDarNo.Text)
            objCommand.Parameters.AddWithValue("@DarKhNo", txtDarKhNo.Text)
            objCommand.Parameters.AddWithValue("@Name", txtName.Text)
            objCommand.Parameters.AddWithValue("@DatKh", maskDateKh.Text)
            objCommand.Parameters.AddWithValue("@DatMali", maskDateMali.Text)
            objCommand.Parameters.AddWithValue("@DatRecv", maskDateRecv.Text)
            objCommand.Parameters.AddWithValue("@DatInOut", maskDateInOut.Text)
            objCommand.Parameters.AddWithValue("@Stat", cboStat.SelectedIndex)
            objCommand.Parameters.AddWithValue("@Chk", cboKharid.SelectedIndex)
            objCommand.Parameters.AddWithValue("@Dscr", txtDscr.Text)
            objCommand.Parameters.AddWithValue("@ChkH", 0)
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
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If Me.maskDate.Text = "1   /  /" Then
            MsgbOK.Label1.Text = " لطفا تاریخ ثبت را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.maskDate.Focus()
            Exit Sub
        ElseIf Me.txtDarNo.Text = 0 Then
            MsgbOK.Label1.Text = " لطفا شماره درخواست را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtDarNo.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شرح را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        Dim objDataAdapter As New SqlDataAdapter _
           (" SELECT Row FROM Bnk.Email WHERE (Shob = " & Shob & ") AND (Dat = N'" & maskDate.Text & "') AND (Row = " & txtRow.Text & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.Email")
        objDataview = New DataView(objDataset.Tables("Bnk.Email"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            MsgbOK.Label1.Text = " شماره برگه " & Trim(Me.txtRow.Text) & "  در سیستم ثبت شده است  . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        EditDataGridView()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub SabtEmail_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Ref.PerformClick()
    End Sub

    Private Sub SabtEmail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Ref.PerformClick()
        ElseIf e.KeyCode = Keys.Insert Then
            FillRow()
            Clear()
        End If
    End Sub

    Private Sub SabtEmail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        Me.maskDate.Text = ConvD.Class1.ConvDate
        cboStat.SelectedIndex = 0
        cboKharid.SelectedIndex = 0
        txtFile.SendToBack()

        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub SabtEmail_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
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
                If RB4.Checked = True Then
                    txtLetterNo.Focus()
                ElseIf RB5.Checked = True Then
                    txtDarNo.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub maskDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDate.MaskInputRejected

    End Sub

    Private Sub maskDateMali_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateMali.GotFocus
        maskDateMali.SelectionStart = 0
        maskDateMali.SelectionLength = Len(maskDateMali.Text)
    End Sub

    Private Sub maskDateMali_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateMali.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateMali.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateMali.Focus()
                Exit Sub
            Else
                maskDateRecv.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateKh_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateKh.GotFocus
        maskDateKh.SelectionStart = 0
        maskDateKh.SelectionLength = Len(maskDateKh.Text)
    End Sub

    Private Sub maskDateKh_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateKh.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateKh.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateKh.Focus()
                Exit Sub
            Else
                maskDateMali.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateKh_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateKh.MaskInputRejected
       
    End Sub

    Private Sub maskDateRecv_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateRecv.GotFocus
        maskDateRecv.SelectionStart = 0
        maskDateRecv.SelectionLength = Len(maskDateRecv.Text)
    End Sub

    Private Sub maskDateRecv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateRecv.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateRecv.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateRecv.Focus()
                Exit Sub
            Else
                maskDateInOut.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateRecv_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateRecv.MaskInputRejected

    End Sub

    Private Sub maskDateInOut_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles maskDateInOut.GotFocus
        maskDateInOut.SelectionStart = 0
        maskDateInOut.SelectionLength = Len(maskDateInOut.Text)
    End Sub

    Private Sub maskDateInOut_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles maskDateInOut.KeyPress
        If e.KeyChar = ChrW(13) Then
            Datin = maskDateInOut.Text
            Chkdate()
            If Datstat = 1 Then
                maskDateInOut.Focus()
                Exit Sub
            Else
                txtDscr.Focus()
            End If
        End If
    End Sub

    Private Sub maskDateInOut_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateInOut.MaskInputRejected

    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
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
                    .Range("A1").Value = "شماره برگه"
                    .Range("A1").Font.Bold = True
                    .Range("B1").Value = "تاریخ ثبت"
                    .Range("B1").Font.Bold = True
                    .Range("C1").Value = "شرح"
                    .Range("C1").Font.Bold = True
                    .Range("D1").Value = "تاریخ ثبت درخواست خرید"
                    .Range("D1").Font.Bold = True
                    .Range("E1").Value = "تاریخ تائید مالی"
                    .Range("E1").Font.Bold = True
                    .Range("F1").Value = "تاریخ دریافت کالا"
                    .Range("F1").Font.Bold = True
                    .Range("G1").Value = "تاریخ تحویل / ارسال"
                    .Range("G1").Font.Bold = True
                    .Range("H1").Value = "وضعیت"
                    .Range("H1").Font.Bold = True
                    .Range("I1").Value = "محل خرید"
                    .Range("I1").Font.Bold = True
                    .Range("J1").Value = "توضیحات"
                    .Range("J1").Font.Bold = True
                    .Range("K1").Value = "شماره نامه"
                    .Range("K1").Font.Bold = True
                    .Range("L1").Value = "شماره درخواست"
                    .Range("L1").Font.Bold = True
                    .Range("M1").Value = "شماره درخواست خرید"
                    .Range("M1").Font.Bold = True

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
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                            .Range("L" & i.ToString).Value = DataGridView1.Rows(X).Cells(13).Value
                            .Range("M" & i.ToString).Value = DataGridView1.Rows(X).Cells(14).Value
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

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim X As Integer
        Dim Rw As String

        X = DataGridView1.CurrentCell.RowIndex
        Rw = DataGridView1.Rows(X).Cells(0).Value
        Try
            'If txtFile.Text = 0 Then
            '    FileFormat = Trim(Rw) + "." + "xlsx"
            'ElseIf txtFile.Text = 1 Then
            '    FileFormat = Trim(Rw) + "." + "docx"
            'ElseIf txtFile.Text = 2 Then
            '    FileFormat = Trim(Rw) + "." + "pdf"
            'ElseIf txtFile.Text = 3 Then
            '    FileFormat = Trim(Rw) + "." + "jpg"
            'End If

            Me.DirectoryEntry1.Path = My.Application.Info.DirectoryPath & "\Email\" + Rw ' جستجوی مسیر
            ' System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path + "\" + FileFormat) ' اجرای فایل
            System.Diagnostics.Process.Start(Me.DirectoryEntry1.Path) ' باز کردن مسیر
        Catch ex As Exception
            MsgbOK.Label1.Text = " مسیر  " & Me.DirectoryEntry1.Path & " پیدا نشد ."
            MsgbOK.ShowDialog()
        End Try
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        If txtSrch.Text <> "" Then
            objDataview.RowFilter = "Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End If
        FillDataGridView()
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub txtDscr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDscr.GotFocus
        Me.txtDscr.SelectionStart = 0
        Me.txtDscr.SelectionLength = Len(Me.txtDscr.Text)
    End Sub

    Private Sub txtDscr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscr.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscr.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB1.Click
        Ref.PerformClick()
    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged
        Ref.PerformClick()
    End Sub

    Private Sub RB3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3.CheckedChanged
        Ref.PerformClick()
    End Sub

    Private Sub RB4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB4.CheckedChanged

    End Sub

    Private Sub RB4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB4.Click
        Label12.Enabled = True
        txtLetterNo.Enabled = True
        Ref.PerformClick()
    End Sub

    Private Sub RB5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB5.CheckedChanged
        Label12.Enabled = False
        txtLetterNo.Enabled = False
        Ref.PerformClick()
    End Sub

    Private Sub txtLetterNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLetterNo.GotFocus
        Me.txtLetterNo.SelectionStart = 0
        Me.txtLetterNo.SelectionLength = Len(Me.txtLetterNo.Text)
    End Sub

    Private Sub txtLetterNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLetterNo.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDarNo.Focus()
        End If
    End Sub

    Private Sub txtDarNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDarNo.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDarKhNo.Focus()
        End If
    End Sub

    Private Sub txtDarKhNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDarKhNo.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtDarNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDarNo.TextChanged
        If txtDarNo.Text = "" Then
            txtDarNo.Text = 0
        End If
    End Sub

    Private Sub txtDarKhNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDarKhNo.TextChanged
        If txtDarKhNo.Text = "" Then
            txtDarKhNo.Text = 0
        End If
    End Sub

    Private Sub maskDateMali_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles maskDateMali.MaskInputRejected

    End Sub
End Class