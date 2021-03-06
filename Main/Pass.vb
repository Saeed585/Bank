
Public Class Pass
    Private Sub FillDataSetAndDataView()
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT UserList.U_Code, UserList.U_Name, UserList.U_Family" & _
             " FROM bas.Shob_Sec INNER JOIN bas.UserList ON Shob_Sec.U_Code = UserList.U_Code WHERE (UserList.U_Code = " & txtCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.UserList")
        objDataview = New DataView(objDataset.Tables("bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Me.lblName.DataBindings.Clear()
        Me.lblFamily.DataBindings.Clear()
        Me.lblPatch_Pic.DataBindings.Clear()
        Me.lblPatch_Col.DataBindings.Clear()
        ' Me.txtDbase.DataBindings.Clear()
        ' Me.lblDb_Name.DataBindings.Clear()

        Me.lblName.DataBindings.Add("text", objDataview, "U_Name")
        Me.lblFamily.DataBindings.Add("text", objDataview, "U_Family")
        ' Me.txtDbase.DataBindings.Add("text", objDataview, "Def_Shob")
        ' Me.lblDb_Name.DataBindings.Add("text", objDataview, "Db_Name")

        Mainfrm.lblU_name.Text = Me.lblName.Text
        Mainfrm.lblU_family.Text = Me.lblFamily.Text
        ' Mainfrm.lblCompName.Text = Me.lblComp_Name.Text
    End Sub
    Private Sub ChkSoft()
        Dim X As Integer
        Dim Cnt As String

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT Bas.Soft_Sec.U_Code, Bas.Soft_Sec.S_Code, Bas.Software.Soft_Name" & _
             " FROM Bas.Soft_Sec INNER JOIN Bas.Software ON Bas.Soft_Sec.S_Code = Bas.Software.Code WHERE(Bas.Soft_Sec.U_Code = " & User_Code & ") ORDER BY Bas.Soft_Sec.S_Code", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Soft_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Soft_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "S_Code"

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(1).Visible = False
        Me.DataGridView1.Columns(2).HeaderText = "نرم افزار"

        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            txt1.Text = DataGridView1.Rows(X).Cells(2).Value
            If txt1.Text = "mnuBase" Then
                Mainfrm.btnBase.Visible = True
            ElseIf txt1.Text = "mnuBank" Then
                Mainfrm.btnBank.Visible = True
            ElseIf txt1.Text = "mnuOpt" Then
                Mainfrm.btnOpt.Visible = True
            End If
        Next X
        If Mainfrm.btnBank.Enabled = False Then
            MsgbOK.Label1.Text = " کد کاربری فوق به این سیستم دسترسی ندارد . "
            MsgbOK.ShowDialog()
            Mainfrm.Dispose()
            Me.Show()
            txtCode.Focus()
            Exit Sub
        Else
            txt1.Text = ""
        End If
    End Sub
    Private Sub DefShob()
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT U_Code, Srv, Def_Shob FROM UserList WHERE (U_Code = " & txtCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.UserList")
        objDataview = New DataView(objDataset.Tables("bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        ' Height = 235

        If txtServer.Text = "Server" Or txtServer.Text = Nothing Then
            Me.txtServer.DataBindings.Clear()
            Me.txtServer.DataBindings.Add("text", objDataview, "Srv")
        End If

        If txtDBase.Text = "DataBase" Or txtDBase.Text = Nothing Then
            Me.txtDBase.DataBindings.Clear()
            Me.txtDBase.DataBindings.Add("text", objDataview, "Def_Shob")
        End If
    End Sub

    Private Sub FillStory()
        Dim Dat As String
        Dat = ConvD.Class1.ConvDate
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, Name, Dscr FROM Bas.Story WHERE (Dat = N'" & Dat & "')", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Story")
        objDataview = New DataView(objDataset.Tables("Bas.Story"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        If objDataview.Count > 0 Then
            'Me.Height = 400
            Panel1.Visible = True
            txtRow.DataBindings.Clear()
            txtRow.DataBindings.Add("Text", objDataview, "Row")
            lblTitle.DataBindings.Clear()
            lblTitle.DataBindings.Add("Text", objDataview, "Name")
            lblDscr.DataBindings.Clear()
            lblDscr.DataBindings.Add("Text", objDataview, "Dscr")

            Dim objDataAdapter1 As New SqlDataAdapter _
                (" SELECT COUNT(Ucode) AS Expr1 FROM Bas.Seen WHERE (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter1.Fill(objDataset, "Bas.Seen")
            objDataview = New DataView(objDataset.Tables("Bas.Seen"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            lblSeen.DataBindings.Clear()
            lblSeen.DataBindings.Add("Text", objDataview, "Expr1")

            Dim objDataAdapter2 As New SqlDataAdapter _
                (" SELECT Ucode, Row FROM Bas.Seen WHERE (Ucode = " & User_Code & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter2.Fill(objDataset, "Bas.Seen")
            objDataview = New DataView(objDataset.Tables("Bas.Seen"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " INSERT INTO Bas.Seen(Ucode, Row)" & _
                   " VALUES (@Ucode, @Row)"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.Clear()

                objCommand.Parameters.AddWithValue("@Ucode", User_Code)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)

                objConnection.Open()
                Try
                    objCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                Dim objDataAdapter3 As New SqlDataAdapter _
                    (" SELECT COUNT(Ucode) AS Expr1 FROM Bas.Seen WHERE (Row = " & txtRow.Text & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter3.Fill(objDataset, "Bas.Seen")
                objDataview = New DataView(objDataset.Tables("Bas.Seen"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

                lblSeen.DataBindings.Clear()
                lblSeen.DataBindings.Add("Text", objDataview, "Expr1")
            End If
        Else
            ' Me.Height = 208
            lblTitle.Text = ""
            lblTitle.DataBindings.Clear()
            Panel1.Visible = False

            FillDataSetAndDataView()
            Me.Hide()
            Mainfrm.Show()
            ChkSoft() 'دسترسی نرم افزار
        End If
    End Sub

    Private Sub Pass_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " DELETE FROM Bas.UserListTmp"
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
    End Sub

    Private Sub Pass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 176
        txtServer.Text = GetSetting("WWW", "LogBankNew", "ServerName", "NOServer")
        txtDBase.Text = GetSetting("ITsystem", "LogBankNew", "DataBaseName", "NODataBase")
        GroupBox3.SendToBack()
        txtRow.SendToBack()
        txtCode.Focus()

        '--------------------------------بدست آوردن IP کامپیوتر
        'Dim strHostName As String = System.Net.Dns.GetHostName()
        'Dim strHostName1 As String = System.Net.Dns.GetHostName()
        'Dim ipHostInfo As System.Net.IPHostEntry = System.Net.Dns.Resolve(System.Net.Dns.GetHostName())
        'Dim ipAddress As System.Net.IPAddress = ipHostInfo.AddressList(0)
        'MsgBox(ipAddress.ToString)
        '--------------------------------پایان

        ' Me.Opacity = 20 'حالتهای اجرای فرم
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim objcommand As New SqlCommand

        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " کد کاربری را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtCode.Focus()
            Exit Sub
        ElseIf Me.txtPass.Text = "" Then
            MsgbOK.Label1.Text = " رمز عبور را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtPass.Focus()
            Exit Sub
        ElseIf txtServer.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام سرور را وارد کنید . "
            MsgbOK.ShowDialog()
            Height = 235
            txtServer.Focus()
            Exit Sub
        ElseIf txtDBase.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام دیتابیس را وارد کنید . "
            MsgbOK.ShowDialog()
            Height = 235
            txtDBase.Focus()
            Exit Sub
        End If

        Try
            objConnection.Close()
            objConnection.ConnectionString = "server='" & txtServer.Text & "';database='" & txtDBase.Text & "';user id=" & Us & ";password=" & Pw & ""
            objConnection.Open()
        Catch SqlExceptionErr As SqlException
            MsgbOK.Label1.Text = "ارتباط با سرور '" & txtServer.Text & "' و دیتابیس '" & txtDBase.Text & "' امکان پذیر نمی باشد . "
            MsgbOK.ShowDialog()
            txtServer.Focus()
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        txtSal.Text = Mid(ConvD.Class1.ConvDate(), 1, 4)

        Dim objdataadapter1 As New SqlDataAdapter _
            ("SELECT Code, Name FROM Bas.Sal WHERE(Name = " & txtSal.Text & ") ORDER BY Code", objConnection)
        objDataset = New DataSet
        objdataadapter1.Fill(objDataset, "bas.Sal")
        objDataview = New DataView(objDataset.Tables("bas.Sal"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Code"

        If objDataview.Count = 0 Then
            MsgbYN.Label1.Text = "  سال  '" & Mid(txtSal.Text, 1, 4) & "'  در سیستم ایجاد نشده است آیا مایل به ایجاد آن هستید  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                End
            Else
                Dim objdataadapter2 As New SqlDataAdapter _
                    (" SELECT MAX(Code) AS Expr1 FROM Bas.Sal", objConnection)
                objDataset = New DataSet
                objdataadapter2.Fill(objDataset, "bas.Sal")
                objDataview = New DataView(objDataset.Tables("bas.Sal"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Expr1"

                txtCod.DataBindings.Clear()
                txtCod.DataBindings.Add("Text", objDataview.Item(0), "Expr1")

                objcommand.Connection = objConnection
                objcommand.CommandText = _
                    " INSERT INTO Bas.Sal (Code, Name) VALUES(@code, @sal)"
                objcommand.CommandType = CommandType.Text
                objcommand.Parameters.AddWithValue("@Code", txtCod.Text + 1)
                objcommand.Parameters.AddWithValue("@Sal", txtSal.Text)

                objConnection.Open()
                Try
                    objcommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                MsgbOK.Label1.Text = "  سال  '" & Mid(txtSal.Text, 1, 4) & "'  در سیستم ایجاد شد لطفا برنامه را دوباره اجرا کنید "
                MsgbOK.ShowDialog()
                End
            End If
        Else
            objcommand.Connection = objConnection
            objcommand.CommandText = _
               " DELETE FROM Bas.UserListTmp"
            objcommand.CommandType = CommandType.Text
            objcommand.Parameters.Clear()

            objConnection.Open()
            Try
                objcommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objcommand.Connection = objConnection
            objcommand.CommandText = _
               " INSERT INTO Bas.UserListTmp (U_Code, U_Pass, Act)" & _
               " SELECT U_Code, U_Pass, Act FROM Bas.UserList WHERE (U_Code = " & txtCode.Text & ")"
            objcommand.CommandType = CommandType.Text
            objcommand.Parameters.Clear()

            objConnection.Open()
            Try
                objcommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Dim objdataadapter As New SqlDataAdapter _
                (" SELECT U_Code, U_Pass, Act FROM Bas.UserListTmp ORDER BY U_Code ", objConnection)
            objDataset = New DataSet
            objdataadapter.Fill(objDataset, "Bas.UserListTmp")
            objDataview = New DataView(objDataset.Tables("Bas.UserListTmp"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "U_Code"

            txt1.DataBindings.Clear()
            txt1.DataBindings.Add("Text", objDataview, "Act")
            txt2.DataBindings.Clear()
            txt2.DataBindings.Add("Text", objDataview, "U_Pass")
            If txt1.Text = "" Then
                txt1.Text = 0
            End If

            'کد کاربری را چک میکند
            IntPosition = objDataview.Find(txtCode.Text)
            If IntPosition = -1 Then
                MsgbOK.Label1.Text = " کد کاربری فوق در سیستم تعریف نشده است . "
                MsgbOK.ShowDialog()
                Me.txtCode.Focus()
                Exit Sub
            Else
                objDataview.Sort = "U_Pass"
                If Trim(txtPass.Text) <> Trim(txt2.Text) Then
                    MsgbOK.Label1.Text = " رمز عبور اشتباه است . "
                    MsgbOK.ShowDialog()
                    Me.txtPass.Focus()
                    Exit Sub
                Else
                    If txt1.Text = 1 Then
                        User_Code = txtCode.Text
                        Try
                            objConnection.Close()
                            objConnection.ConnectionString = "server='" & txtServer.Text & "';database='" & txtDBase.Text & "';user id=" & Us & ";password=" & Pw & ""
                            objConnection.Open()
                        Catch SqlExceptionErr As SqlException
                            MsgbOK.Label1.Text = "ارتباط با سرور '" & txtServer.Text & "' و دیتابیس '" & txtDBase.Text & "' امکان پذیر نمی باشد . "
                            MsgbOK.ShowDialog()
                            txtServer.Focus()
                            objConnection.Close()
                            Exit Sub
                        End Try
                        objConnection.Close()
                    Else
                        MsgbOK.Label1.Text = "کد کاربری وارد شده غیر فعال میباشد لطفا با مسئول سیستم هماهنگ کنید . "
                        MsgbOK.ShowDialog()
                        Me.txtCode.Focus()
                        Exit Sub
                    End If
                    txt1.Text = ""
                    txt2.Text = ""
                End If

                SaveSetting("WWW", "LogBankNew", "ServerName", txtServer.Text)
                SaveSetting("ITsystem", "LogBankNew", "DataBaseName", txtDBase.Text)

                FillStory()
                'FillDataSetAndDataView()
                'Me.Hide()
                'Mainfrm.Show()

                'ChkSoft() 'دسترسی نرم افزار
            End If
        End If
    End Sub

    Private Sub txtPass_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass.GotFocus
        Me.txtPass.SelectionStart = 0
        Me.txtPass.SelectionLength = Len(txtPass.Text)
    End Sub

    Private Sub txtPass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPass.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnOk.Focus()
        End If
    End Sub

    Private Sub txtPass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPass.TextChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        txtCode.SelectionStart = 0
        txtCode.SelectionLength = Len(txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtPass.Focus()
            ' DefShob()
        End If
    End Sub

    Private Sub txtServer_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServer.GotFocus
        txtServer.SelectionStart = 0
        txtServer.SelectionLength = Len(txtServer.Text)
    End Sub

    Private Sub txtServer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtServer.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtDBase.Focus()
        End If
    End Sub

    Private Sub txtDBase_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDBase.GotFocus
        txtDBase.SelectionStart = 0
        txtDBase.SelectionLength = Len(txtDBase.Text)
    End Sub

    Private Sub txtDBase_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDBase.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnOk.Focus()
        End If
    End Sub

    Private Sub txt3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Panel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel2.Click
        Panel1.Visible = False

        FillDataSetAndDataView()
        Me.Hide()
        Mainfrm.Show()
        ChkSoft() 'دسترسی نرم افزار
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub lblView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblView.Click
        Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT Bas.Seen.Ucode, Bas.UserList.U_Name, Bas.UserList.U_Family FROM Bas.UserList INNER JOIN Bas.Seen ON Bas.UserList.U_Code = Bas.Seen.Ucode" & _
             " WHERE(Bas.Seen.Row = " & txtRow.Text & ") ORDER BY Bas.Seen.Ucode", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bas.UserList")
        objDataview = New DataView(objDataset.Tables("Bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).Visible = False ' Ucode
        Me.DataGridView2.Columns(1).HeaderText = "نام"
        Me.DataGridView2.Columns(2).HeaderText = "نام خانوادگی"

        Me.DataGridView2.Columns(1).Width = 80
        Me.DataGridView2.Columns(2).Width = 120
        If Panel3.Visible = False Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub
End Class