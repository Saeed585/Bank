Public Class Security
    Dim ColumnSrch As Integer

    Public Sub FillUcode()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(U_Code) AS Expr1 FROM Bas.UserList", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.UserList")
        objDataview = New DataView(objDataset.Tables("Bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label5.DataBindings.Clear()
        Label5.DataBindings.Add("Text", objDataview, "Expr1")
        If Label5.Text <> "" Then
            txtUCode.Text = Val(Label5.Text) + 1
        ElseIf Label5.Text = "" Then
            txtUCode.Text = 1
        End If
        Label5.DataBindings.Clear()
        Label5.Text = ""
    End Sub

    Public Sub FillCboShob()
        Dim a As String
        a = 0
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE(Shob_Code <> 0) ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Shob")
        objDataview = New DataView(objDataset.Tables("bas.Shob"))

        cboShob.DataSource = objDataset.Tables("bas.Shob")
        cboShob.DisplayMember = "Shob_Name"
        cboShob.ValueMember = "Shob_Code"
    End Sub

    Public Sub FillCboMove()
        Dim a As String
        a = 0
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE(Shob_Code <> 0) ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Shob")
        objDataview = New DataView(objDataset.Tables("bas.Shob"))

        cboMove.DataSource = objDataset.Tables("bas.Shob")
        cboMove.DisplayMember = "Shob_Name"
        cboMove.ValueMember = "Shob_Code"
    End Sub

    Private Sub FillUserList()
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

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " INSERT INTO Bas.UserListTmp (U_Code, U_Pass, U_Name, U_Family, pers_code, Adm, Act, Shob)" & _
           " SELECT U_Code, U_Pass, U_Name, U_Family, pers_code, Adm, Act, Shob FROM Bas.UserList"
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

        Shob = Me.cboShob.SelectedValue.ToString
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT U_Code, U_Pass, pers_code, U_Name, U_Family, Act, ActDscr, Adm, Shob FROM Bas.UserListTmp WHERE (Shob = " & Shob & ") ORDER BY U_Code", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Bas.UserListTmp")
        objDataview = New DataView(objDataset.Tables("Bas.UserListTmp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        lblCount.Text = objDataview.Count
    End Sub
    Private Sub FillUserView()
        Dim X As Integer

        Me.DataGridView6.AutoGenerateColumns = True
        Me.DataGridView6.DataSource = objDataview

        Me.DataGridView6.Columns(0).HeaderText = "کد کاربر"
        Me.DataGridView6.Columns(1).Visible = False ' Pass
        Me.DataGridView6.Columns(2).Visible = False ' Pcode
        Me.DataGridView6.Columns(3).Visible = False ' Name
        Me.DataGridView6.Columns(4).HeaderText = "نام کاربر"
        Me.DataGridView6.Columns(5).Visible = False ' Act
        Me.DataGridView6.Columns(6).HeaderText = "فعال / غیرفعال"
        Me.DataGridView6.Columns(7).Visible = False ' Adm
        Me.DataGridView6.Columns(8).Visible = False ' Shob

        Me.DataGridView6.Columns(0).Width = 40
        Me.DataGridView6.Columns(4).Width = 120
        Me.DataGridView6.Columns(6).Width = 50

        For X = 0 To DataGridView6.RowCount - 1
            If DataGridView6.Rows(X).Cells(5).Value = 0 Then
                DataGridView6.Rows(X).Cells(6).Value = "غیر فعال"
            End If
        Next
    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        Dim X1 As Integer
        Dim X2 As Integer
        X = DataGridView6.CurrentCell.RowIndex
        txtUCode.Text = DataGridView6.Rows(X).Cells(0).Value
        txtPass.Text = DataGridView6.Rows(X).Cells(1).Value
        txtCode.Text = DataGridView6.Rows(X).Cells(2).Value
        txtName.Text = DataGridView6.Rows(X).Cells(3).Value
        txtFamily.Text = DataGridView6.Rows(X).Cells(4).Value
        If DataGridView6.Rows(X).Cells(5).Value = 1 Then
            RB1.Checked = True
        Else
            RB2.Checked = True
        End If
        If DataGridView6.Rows(X).Cells(7).Value = True Then
            CheckBox5.Checked = True
        Else
            CheckBox5.Checked = False
        End If

        Label6.Enabled = True
        Label7.Enabled = True
        Label8.Enabled = True
        Label9.Enabled = True
        Label13.Enabled = True
        CheckBox1.Visible = True
        CheckBox2.Visible = True
        CheckBox3.Visible = True
        CheckBox4.Visible = True
        CheckBox6.Visible = True
        DataGridView1.Visible = True
        DataGridView2.Visible = True
        DataGridView3.Visible = True
        DataGridView4.Visible = True
        DataGridView5.Visible = True
        DataGridView7.Visible = True

        If RB1.Checked = True Then
            DataGridView1.Visible = True
            DataGridView2.Enabled = True
            DataGridView3.Enabled = True
            DataGridView4.Enabled = True
            DataGridView5.Enabled = True
            DataGridView7.Visible = True
        Else
            Label6.Enabled = False
            Label7.Enabled = False
            Label8.Enabled = False
            Label9.Enabled = False
            Label13.Enabled = False
            CheckBox1.Visible = False
            CheckBox2.Visible = False
            CheckBox3.Visible = False
            CheckBox4.Visible = False
            CheckBox6.Visible = False
            DataGridView1.Visible = False
            DataGridView2.Visible = False
            DataGridView3.Visible = False
            DataGridView4.Visible = False
            DataGridView5.Visible = False
            DataGridView7.Visible = False
        End If

        FillDataShob()
        FillDGVShob()
        ChkValueShob()
        FillDataSal()
        FillDGVSal()
        ChkValueSal()
        SoftWare()
        FillDGVSoft()
        ChkValueSoft()
        Title()
        FillDGVTitle()
        ChkValueTitle()
        Form()
        FillDGVForm()
        ChkValueForm()
        SED()
        FillDGVSED()
        ChkValueSED()
        ' FillDataUser()

        X1 = Me.DataGridView2.CurrentCell.RowIndex
        If DataGridView2.Rows(X1).Cells(0).Value = True Then
            DataGridView3.Enabled = True
        Else
            DataGridView3.Enabled = False
        End If

        X2 = Me.DataGridView3.CurrentCell.RowIndex
        If DataGridView3.Rows(X2).Cells(0).Value = True Then
            DataGridView4.Enabled = True
        Else
            DataGridView4.Enabled = False
        End If
    End Sub

    Private Sub FillDataUser()
        Dim objdataadapter As New SqlDataAdapter _
             (" SELECT U_Code, U_Pass, U_Name, U_Family, pers_code, ADM, Act FROM bas.UserListTmp WHERE (U_Code = " & txtUCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.UserListTmp")
        objDataview = New DataView(objDataset.Tables("bas.UserListTmp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
    End Sub

    Private Sub FillDataShob()
        Dim Shb As String
        Shb = 0
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM bas.Shob where(shob_code <>" & Shb & ") ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.shob")
        objDataview = New DataView(objDataset.Tables("bas.shob"))
        objDataview.Sort = "shob_code"
    End Sub

    Private Sub FillDGVShob()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(1).Visible = False
        Me.DataGridView1.Columns(2).HeaderText = "نام شعبه"
        Me.DataGridView1.Columns(2).Width = 100
    End Sub

    Private Sub ChkValueShob()
        Dim Cnt As Integer
        Dim x As Integer
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT U_Code, Shob_Code FROM bas.Shob_Sec WHERE (U_Code = " & txtUCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Shob_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Shob_Sec"))
        objDataview.Sort = "shob_code"
        Cnt = DataGridView1.Rows.Count - 1
        For x = 0 To Cnt
            IntPosition = objDataview.Find(DataGridView1.Rows(x).Cells(1).Value)
            If IntPosition <> -1 Then
                Me.DataGridView1.Rows(x).Cells(0).Value = True
            Else
                Me.DataGridView1.Rows(x).Cells(0).Value = False
            End If
        Next x
    End Sub

    Private Sub FillDataSal()
       
        Dim objDataAdapter As New SqlDataAdapter _
        (" SELECT Code, Name FROM Bas.Sal ORDER BY Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Sal")
        objDataview = New DataView(objDataset.Tables("Bas.Sal"))
        objDataview.Sort = "code"
    End Sub

    Private Sub FillDGVSal()
        Me.DataGridView7.AutoGenerateColumns = True
        Me.DataGridView7.DataSource = objDataview

        Me.DataGridView7.Columns(1).Visible = False
        Me.DataGridView7.Columns(2).HeaderText = "سال"
        Me.DataGridView7.Columns(2).Width = 50
    End Sub

    Private Sub ChkValueSal()
        Dim Cnt As Integer
        Dim x As Integer
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT U_Code, Code FROM Bas.Sal_Sec WHERE (U_Code = " & txtUCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Bas.Sal_Sec")
        objDataview = New DataView(objDataset.Tables("Bas.Sal_Sec"))
        objDataview.Sort = "code"
        Cnt = DataGridView7.Rows.Count - 1
        For x = 0 To Cnt
            IntPosition = objDataview.Find(DataGridView7.Rows(x).Cells(1).Value)
            If IntPosition <> -1 Then
                Me.DataGridView7.Rows(x).Cells(0).Value = True
            Else
                Me.DataGridView7.Rows(x).Cells(0).Value = False
            End If
        Next x
    End Sub

    Private Sub SoftWare()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Code, Name FROM Bas.Software ORDER BY Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Software")
        objDataview = New DataView(objDataset.Tables("bas.Software"))
        objDataview.Sort = "Code"
    End Sub
    Private Sub FillDGVSoft()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(1).Visible = False
        Me.DataGridView2.Columns(2).HeaderText = "نام سیستم"
        Me.DataGridView2.Columns(2).Width = 100
    End Sub
    Private Sub ChkValueSoft()
        Dim Cnt As Integer
        Dim x As Integer
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT U_Code, S_Code FROM bas.Soft_Sec WHERE (U_Code = " & txtUCode.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "bas.Soft_Sec")
        objDataview = New DataView(objDataset.Tables("bas.Soft_Sec"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "s_code"
        Cnt = DataGridView2.Rows.Count - 1
        For x = 0 To Cnt
            IntPosition = objDataview.Find(DataGridView2.Rows(x).Cells(1).Value)
            If IntPosition <> -1 Then
                Me.DataGridView2.Rows(x).Cells(0).Value = True
                ' Title()
                ' FillDGVTitle()
                ' ChkValueTitle()
            Else
                Me.DataGridView2.Rows(x).Cells(0).Value = False
                'If User_Code <> 1 Then
                '    Me.DataGridView2.Rows(x).Height = 0
                'End If
            End If
        Next x
    End Sub
    Private Sub Title()
        Dim X As Integer
        Dim SCode As String
        If DataGridView2.RowCount > 0 Then
            X = Me.DataGridView2.CurrentCell.RowIndex
            ' If DataGridView2.Rows(X).Cells(0).Value = True Then
            SCode = DataGridView2.Rows(X).Cells(1).Value
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Code, Name FROM Bas.Title WHERE(S_Code = " & SCode & ") ORDER BY Code", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Title")
            objDataview = New DataView(objDataset.Tables("bas.Title"))
            objDataview.Sort = "Code"
            ' Else
            '     objDataview.Dispose()
            ' End If
        End If
    End Sub
    Private Sub FillDGVTitle()
        Me.DataGridView3.AutoGenerateColumns = True
        Me.DataGridView3.DataSource = objDataview

        Me.DataGridView3.Columns(1).Visible = False
        Me.DataGridView3.Columns(2).HeaderText = "نام منو"
        Me.DataGridView3.Columns(2).Width = 80
    End Sub
    Private Sub ChkValueTitle()
        Dim Cnt As Integer
        Dim x As Integer
        Dim SCode As String
        If DataGridView3.RowCount > 0 Then
            x = Me.DataGridView2.CurrentCell.RowIndex
            SCode = DataGridView2.Rows(x).Cells(1).Value
            Dim objdataadapter As New SqlDataAdapter _
                (" SELECT U_Code, S_Code, T_Code FROM Bas.Title_Sec WHERE (U_Code = " & txtUCode.Text & ") AND (S_Code = " & SCode & ")", objConnection)
            objDataset = New DataSet
            objdataadapter.Fill(objDataset, "bas.Title_Sec")
            objDataview = New DataView(objDataset.Tables("bas.Title_Sec"))
            objDataview.Sort = "t_code"
            Cnt = DataGridView3.Rows.Count - 1
            For x = 0 To Cnt
                IntPosition = objDataview.Find(DataGridView3.Rows(x).Cells(1).Value)
                If IntPosition <> -1 Then
                    Me.DataGridView3.Rows(x).Cells(0).Value = True
                Else
                    Me.DataGridView3.Rows(x).Cells(0).Value = False
                End If
            Next x
        End If
    End Sub
    Private Sub Form()
        Dim X As Integer
        Dim X1 As Integer
        Dim SCode As String
        Dim TCode As String
        If DataGridView3.RowCount > 0 Then
            X = Me.DataGridView2.CurrentCell.RowIndex
            SCode = DataGridView2.Rows(X).Cells(1).Value
            X1 = Me.DataGridView3.CurrentCell.RowIndex
            TCode = DataGridView3.Rows(X1).Cells(1).Value
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Code, Name FROM Bas.Form WHERE (T_Code = " & TCode & ") AND (S_Code = " & SCode & ") AND (Form_Name IS NOT NULL) ORDER BY Code", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "bas.Form")
            objDataview = New DataView(objDataset.Tables("bas.Form"))
            objDataview.Sort = "Code"
        End If
    End Sub
    Private Sub FillDGVForm()
        DataGridView4.ClearSelection()
        Me.DataGridView4.AutoGenerateColumns = True
        Me.DataGridView4.DataSource = objDataview

        Me.DataGridView4.Columns(1).Visible = False
        Me.DataGridView4.Columns(2).HeaderText = "نام فرم"
        Me.DataGridView4.Columns(2).Width = 120
    End Sub
    Private Sub ChkValueForm()
        Dim Cnt As Integer
        Dim X As Integer
        Dim X1 As Integer
        Dim X2 As Integer
        Dim SCode As String
        Dim TCode As String

        If DataGridView3.RowCount > 0 Then
            X1 = DataGridView2.CurrentCell.RowIndex
            SCode = DataGridView2.Rows(X1).Cells(1).Value
            X2 = DataGridView3.CurrentCell.RowIndex
            TCode = DataGridView3.Rows(X2).Cells(1).Value
            Dim objdataadapter As New SqlDataAdapter _
                (" SELECT U_Code, F_Code FROM Bas.Form_Sec WHERE (U_Code = " & txtUCode.Text & ") AND (S_Code = " & SCode & ") AND (T_Code = " & TCode & ")", objConnection)
            objDataset = New DataSet
            objdataadapter.Fill(objDataset, "bas.Form_Sec")
            objDataview = New DataView(objDataset.Tables("bas.Form_Sec"))
            objDataview.Sort = "f_code"
            Cnt = DataGridView4.Rows.Count - 1
            For X = 0 To Cnt
                IntPosition = objDataview.Find(DataGridView4.Rows(X).Cells(1).Value)
                If IntPosition <> -1 Then
                    Me.DataGridView4.Rows(X).Cells(0).Value = True
                Else
                    Me.DataGridView4.Rows(X).Cells(0).Value = False
                End If
            Next X
        End If
    End Sub
    Private Sub SED()
        Dim X1 As Integer
        Dim X2 As Integer
        Dim X3 As Integer
        Dim SCode As String
        Dim TCode As String
        Dim FCode As String

        If DataGridView3.RowCount > 0 And DataGridView4.RowCount > 0 Then
            X1 = Me.DataGridView2.CurrentCell.RowIndex
            SCode = DataGridView2.Rows(X1).Cells(1).Value
            X2 = Me.DataGridView3.CurrentCell.RowIndex
            TCode = DataGridView3.Rows(X2).Cells(1).Value
            X3 = Me.DataGridView4.CurrentCell.RowIndex
            FCode = DataGridView4.Rows(X3).Cells(1).Value

            If DataGridView4.Rows(X3).Cells(0).Value = True Then
                Dim objDataAdapter As New SqlDataAdapter _
                    (" SELECT F_Code FROM Bas.Form_Sec WHERE (U_Code = " & txtUCode.Text & ") AND (F_Code = " & FCode & ") AND (T_Code = " & TCode & ") AND (S_Code = " & SCode & ")", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "Bas.Form_Sec")
                objDataview = New DataView(objDataset.Tables("Bas.Form_Sec"))
            End If
        End If
    End Sub
    Private Sub FillDGVSED()
        Me.DataGridView5.AutoGenerateColumns = True
        Me.DataGridView5.DataSource = objDataview

        Me.DataGridView5.Columns(4).Visible = False
    End Sub
    Private Sub ChkValueSED()
        Dim X1 As Integer
        Dim X2 As Integer
        Dim X3 As Integer
        Dim SCode As String
        Dim TCode As String
        Dim FCode As String

        If DataGridView4.RowCount > 0 And DataGridView5.RowCount > 0 Then
            X1 = Me.DataGridView2.CurrentCell.RowIndex
            SCode = DataGridView2.Rows(X1).Cells(1).Value
            X2 = Me.DataGridView3.CurrentCell.RowIndex
            TCode = DataGridView3.Rows(X2).Cells(1).Value
            X3 = Me.DataGridView4.CurrentCell.RowIndex
            FCode = DataGridView4.Rows(X3).Cells(1).Value
            Dim objdataadapter As New SqlDataAdapter _
                (" SELECT U_Code, F_Code, Sayer, Sav, Edt, Del FROM Bas.Form_Sec WHERE (T_Code = " & TCode & ") AND (S_Code = " & SCode & ") AND (U_Code = " & txtUCode.Text & ") AND (F_Code = " & FCode & ")", objConnection)
            objDataset = New DataSet
            objdataadapter.Fill(objDataset, "bas.Form_Sec")
            objDataview = New DataView(objDataset.Tables("bas.Form_Sec"))
            objDataview.Sort = "F_Code"

            If Me.DataGridView4.Rows(X3).Cells(0).Value = True Then
                txt1.DataBindings.Clear()
                txt1.DataBindings.Add("Text", objDataview, "sayer")
                If Trim(txt1.Text) = "True" Then
                    Me.DataGridView5.Rows(0).Cells(3).Value = True
                Else
                    Me.DataGridView5.Rows(0).Cells(3).Value = False
                End If
                txt1.Text = ""

                txt1.DataBindings.Clear()
                txt1.DataBindings.Add("Text", objDataview, "sav")
                If Trim(txt1.Text) = "True" Then
                    Me.DataGridView5.Rows(0).Cells(2).Value = True
                Else
                    Me.DataGridView5.Rows(0).Cells(2).Value = False
                End If
                txt1.Text = ""

                txt1.DataBindings.Clear()
                txt1.DataBindings.Add("Text", objDataview, "edt")
                If Trim(txt1.Text) = "True" Then
                    Me.DataGridView5.Rows(0).Cells(1).Value = True
                Else
                    Me.DataGridView5.Rows(0).Cells(1).Value = False
                End If
                txt1.Text = ""

                txt1.DataBindings.Clear()
                txt1.DataBindings.Add("Text", objDataview, "del")
                If Trim(txt1.Text) = "True" Then
                    Me.DataGridView5.Rows(0).Cells(0).Value = True
                Else
                    Me.DataGridView5.Rows(0).Cells(0).Value = False
                End If
                txt1.Text = ""
            End If
        End If
    End Sub
    Private Sub Clear()
        Me.txtPass.DataBindings.Clear()
        Me.txtName.DataBindings.Clear()
        Me.txtFamily.DataBindings.Clear()
        CheckBox5.Checked = False
        Me.txtName.Text = ""
        Me.txtFamily.Text = ""
        Me.txtPass.Text = ""
        Me.txtCode.Text = ""
        Label6.Enabled = False
        Label7.Enabled = False
        Label8.Enabled = False
        Label9.Enabled = False
        Label13.Enabled = False
        CheckBox1.Visible = False
        CheckBox2.Visible = False
        CheckBox3.Visible = False
        CheckBox4.Visible = False
        CheckBox6.Visible = False
        DataGridView1.Visible = False
        DataGridView2.Visible = False
        DataGridView3.Visible = False
        DataGridView4.Visible = False
        DataGridView5.Visible = False
        DataGridView7.Visible = False
        Me.txtPass.Focus()
    End Sub
    Private Sub txtPass_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass.GotFocus
        Me.txtPass.SelectionStart = 0
        Me.txtPass.SelectionLength = Len(txtPass.Text)
    End Sub
    Private Sub txtPass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPass.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtCode.Focus()
        End If
    End Sub
    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(txtName.Text)
    End Sub
    Private Sub txtFamily_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFamily.GotFocus
        Me.txtFamily.SelectionStart = 0
        Me.txtFamily.SelectionLength = Len(txtFamily.Text)
    End Sub
    Private Sub txtFamily_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamily.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub InsertUpdate()
        Shob = Me.cboShob.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdUCode"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@U_Code", Me.txtUCode.Text)
            objCommand.Parameters.AddWithValue("@U_Pass", txtPass.Text)
            objCommand.Parameters.AddWithValue("@U_Name", Me.txtName.Text)
            objCommand.Parameters.AddWithValue("@U_Family", Me.txtFamily.Text)
            objCommand.Parameters.AddWithValue("@Pcode", Me.txtCode.Text)
            objCommand.Parameters.AddWithValue("@Adm", CheckBox5.CheckState)
            If RB1.Checked = True Then
                objCommand.Parameters.AddWithValue("@Act", 1)
            ElseIf RB2.Checked = True Then
                objCommand.Parameters.AddWithValue("@Act", 0)
            End If
            objCommand.Parameters.AddWithValue("@Flg", 1)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objcommand As New SqlCommand
        If Me.txtUCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد کاربری را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtUCode.Focus()
            Exit Sub
        ElseIf Me.txtPass.Text = "" Then
            MsgbOK.Label1.Text = " لطفا رمز عبور را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtPass.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کاربری را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtFamily.Text = "" Then
            MsgbOK.Label1.Text = " للطفا نام خانوادگی کاربری را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtFamily.Focus()
            Exit Sub
        ElseIf Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد نمائید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        End If

        FillDataUser()
        objDataview.Sort = "U_Code"
        IntPosition = objDataview.Find(txtUCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کد کاربری  " & Trim(Me.txtUCode.Text) & "  در سیستم موجود می باشد . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillUcode()
            Clear()
            FillUserList()
            FillUserView()
        End If
    End Sub

    Private Sub Security_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
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

    Private Sub Security_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tsHeader.Cursor = Cursors.Hand
        FillCboShob()
        FillCboMove()

        txt1.SendToBack()
        FormName = "Security"
    End Sub
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub
    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtFamily.Focus()
        End If
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim objcommand As New SqlCommand
        If Me.txtUCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد کاربری را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtUCode.Focus()
            Exit Sub
        ElseIf Me.txtPass.Text = "" Then
            MsgbOK.Label1.Text = " لطفا رمز عبور را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtPass.Focus()
            Exit Sub
        ElseIf Me.txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کاربری را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf Me.txtFamily.Text = "" Then
            MsgbOK.Label1.Text = " للطفا نام خانوادگی کاربری را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtFamily.Focus()
            Exit Sub
        ElseIf Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد پرسنلی را وارد نمائید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        End If

        FillDataUser()
        objDataview.Sort = "U_Code"
        IntPosition = objDataview.Find(Me.txtUCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد کاربری  " & Trim(Me.txtUCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillUcode()
            Clear()
            FillUserList()
            FillUserView()
        End If
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillUcode()
        Clear()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objcommand As New SqlCommand
        If Me.txtUCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد کاربری را وارد نمائید . "
            MsgbOK.ShowDialog()
            Me.txtUCode.Focus()
            Exit Sub
        ElseIf txtUCode.Text = User_Code Then
            MsgbOK.Label1.Text = " ! کد کاربری  " & Trim(Me.txtUCode.Text) & "   کاربر جاری بوده و  نمی توانید از سیستم حذف کنید . "
            MsgbOK.ShowDialog()
            Me.txtUCode.Focus()
            Exit Sub
        End If

        FillDataUser()
        objDataview.Sort = "U_Code"
        IntPosition = objDataview.Find(Me.txtUCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کد کاربری  " & Trim(Me.txtUCode.Text) & "  در سیستم موجود نمی باشد . "
            MsgbOK.ShowDialog()
            Me.txtUCode.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کد کاربری  " & Trim(Me.txtUCode.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtUCode.Focus()
                Exit Sub
            End If
            Btn = 3
            InsertUpdate()

            FillUcode()
            Clear()
            FillUserList()
            FillUserView()
        End If
    End Sub
   
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Security_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 221
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim objCommand As New SqlCommand
        If e.ColumnIndex = 0 Then
            Dim X As Object
            Dim y As Object
            Dim Rw As Integer
            X = Me.DataGridView1.CurrentCell.RowIndex
            y = Me.DataGridView1.Rows(X).Cells(2).Value
            If Me.DataGridView1.Rows(X).Cells(0).Value = Nothing Or Me.DataGridView1.Rows(X).Cells(0).Value = False Then
                Me.DataGridView1.Rows(X).Cells(0).Value = True

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " INSERT INTO bas.Shob_Sec (U_Code, Shob_Code)VALUES (@u_code, @shob_code)"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@u_code", txtUCode.Text)
                objCommand.Parameters.AddWithValue("@shob_code", DataGridView1.Rows(X).Cells(1).Value)
            Else
                Rw = Me.DataGridView1.Rows(X).Cells(1).Value
                Me.DataGridView1.Rows(X).Cells(0).Value = False

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Shob_Sec WHERE(u_code = " & txtUCode.Text & ") AND (Shob_code = " & Rw & ")"
            End If

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            'Me.FillDataShob()
            'Me.FillDGVShob()
            'ChkValueShob()
        End If
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim objCommand As New SqlCommand
        If e.ColumnIndex = 0 Then
            Dim X As Object
            Dim X1 As Object
            Dim Scode As String
            Dim Tcode As String

            X = Me.DataGridView2.CurrentCell.RowIndex
            Scode = Me.DataGridView2.Rows(X).Cells(1).Value
            If User_Code <> 1 Then
                If Scode = 2 Or Scode = 3 Or Scode = 7 Or Scode = 10 Or Scode = 12 Or Scode = 15 Or Scode = 16 Then
                    MsgbOK.Label1.Text = " این سیستم مربوط به واحد کامپیوتر میباشد لطفا با مسئول سیستم هماهنگ کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtUCode.Focus()
                    Exit Sub
                End If
            End If
            X1 = Me.DataGridView3.CurrentCell.RowIndex
            Tcode = Me.DataGridView3.Rows(X1).Cells(1).Value

            If Me.DataGridView2.Rows(X).Cells(0).Value = Nothing Or Me.DataGridView2.Rows(X).Cells(0).Value = False Then
                Me.DataGridView2.Rows(X).Cells(0).Value = True
                DataGridView3.Enabled = True
                DataGridView4.Enabled = True

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " INSERT INTO bas.Soft_Sec (U_Code, S_Code)VALUES (@u_code, @s_code)"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@u_code", txtUCode.Text)
                objCommand.Parameters.AddWithValue("@s_code", Scode)

                objConnection.Open()
                Try
                    objCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()
            Else
                Me.DataGridView2.Rows(X).Cells(0).Value = False
                DataGridView3.Enabled = False
                DataGridView4.Enabled = False

                MsgbYN.Label1.Text = "  با حذف این برنامه دسترسی به منوها و فرمهای زیر مجموعه نیز حذف می شود "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    Me.DataGridView2.Rows(X).Cells(0).Value = True
                    txtUCode.Focus()
                    Exit Sub
                End If

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Soft_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ")"

                objConnection.Open()
                Try
                    objCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Title_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ")"

                objConnection.Open()
                Try
                    objCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Form_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ")"

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
            'SoftWare()
            'FillDGVSoft()
            'ChkValueSoft()
            Title()
            FillDGVTitle()
            ChkValueTitle()
            Form()
            FillDGVForm()
            ChkValueForm()
            SED()
            FillDGVSED()
            ChkValueSED()
        End If
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView3_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Dim objCommand As New SqlCommand
        If e.ColumnIndex = 0 Then
            Dim X As Object
            Dim X1 As Object
            Dim Scode As Object
            Dim Tcode As String

            X = Me.DataGridView3.CurrentCell.RowIndex
            Tcode = Me.DataGridView3.Rows(X).Cells(1).Value
            X1 = Me.DataGridView2.CurrentCell.RowIndex
            Scode = Me.DataGridView2.Rows(X1).Cells(1).Value

            If User_Code <> 1 Then
                If Scode = 2 Or Scode = 3 Or Scode = 7 Or Scode = 10 Or Scode = 12 Or Scode = 15 Or Scode = 16 Then
                    MsgbOK.Label1.Text = " این سیستم مربوط به واحد کامپیوتر میباشد لطفا با مسئول سیستم هماهنگ کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtUCode.Focus()
                    Exit Sub
                End If
            End If
            If Me.DataGridView3.Rows(X).Cells(0).Value = Nothing Or Me.DataGridView3.Rows(X).Cells(0).Value = False Then
                Me.DataGridView3.Rows(X).Cells(0).Value = True
                DataGridView4.Enabled = True

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " INSERT INTO bas.Title_Sec (U_Code, S_Code, T_Code)VALUES (@u_code, @s_code, @t_code)"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@u_code", txtUCode.Text)
                objCommand.Parameters.AddWithValue("@s_code", Scode)
                objCommand.Parameters.AddWithValue("@t_code", Tcode)

                objConnection.Open()
                Try
                    objCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()
            Else
                Me.DataGridView3.Rows(X).Cells(0).Value = False
                DataGridView4.Enabled = False

                MsgbYN.Label1.Text = "  با حذف این منو دسترسی به فرمهای زیر مجموعه نیز حذف می شود "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    Me.DataGridView3.Rows(X).Cells(0).Value = True
                    txtUCode.Focus()
                    Exit Sub
                End If

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Title_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ") AND (T_code = " & Tcode & ")"

                objConnection.Open()
                Try
                    objCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Form_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ") AND (T_code = " & Tcode & ")"

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

            'Title()
            'FillDGVTitle()
            'ChkValueTitle()
            Form()
            FillDGVForm()
            ChkValueForm()
            SED()
            FillDGVSED()
            ChkValueSED()
        End If
    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub DataGridView2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView2.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            Dim X As Integer
            X = Me.DataGridView2.CurrentCell.RowIndex
            If Me.DataGridView2.Rows(X).Cells(0).Value = True Then
                DataGridView3.Enabled = True
                DataGridView4.Enabled = True
            Else
                DataGridView3.Enabled = False
                DataGridView4.Enabled = False
            End If
            Title()
            FillDGVTitle()
            ChkValueTitle()
            Form()
            FillDGVForm()
            ChkValueForm()
            SED()
            FillDGVSED()
            ChkValueSED()
        End If
    End Sub

    Private Sub DataGridView2_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseClick
        Dim X As Integer
        X = Me.DataGridView2.CurrentCell.RowIndex
        If Me.DataGridView2.Rows(X).Cells(0).Value = True Then
            DataGridView3.Enabled = True
            DataGridView4.Enabled = True
        Else
            DataGridView3.Enabled = False
            DataGridView4.Enabled = False
        End If
        Title()
        FillDGVTitle()
        ChkValueTitle()
        Form()
        FillDGVForm()
        ChkValueForm()
        SED()
        FillDGVSED()
        ChkValueSED()
    End Sub

    Private Sub DataGridView3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView3.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            Dim X As Integer
            X = Me.DataGridView3.CurrentCell.RowIndex
            If Me.DataGridView3.Rows(X).Cells(0).Value = True Then
                DataGridView4.Enabled = True
            Else
                DataGridView4.Enabled = False
            End If
            Form()
            FillDGVForm()
            ChkValueForm()
            SED()
            FillDGVSED()
            ChkValueSED()
        End If
    End Sub

    Private Sub DataGridView3_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.RowHeaderMouseClick
        Dim X As Integer
        X = Me.DataGridView3.CurrentCell.RowIndex
        If Me.DataGridView3.Rows(X).Cells(0).Value = True Then
            DataGridView4.Enabled = True
        Else
            DataGridView4.Enabled = False
        End If
        Form()
        FillDGVForm()
        ChkValueForm()
        SED()
        FillDGVSED()
        ChkValueSED()
    End Sub

    Private Sub DataGridView4_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView4.CellClick
        Dim objCommand As New SqlCommand
        If e.ColumnIndex = 0 Then
            Dim X As Object
            Dim X1 As Object
            Dim X2 As Object
            Dim Scode As String
            Dim Tcode As String
            Dim Fcode As Object

            X = Me.DataGridView2.CurrentCell.RowIndex
            Scode = Me.DataGridView2.Rows(X).Cells(1).Value
            X1 = Me.DataGridView3.CurrentCell.RowIndex
            Tcode = Me.DataGridView3.Rows(X1).Cells(1).Value
            X2 = Me.DataGridView4.CurrentCell.RowIndex
            Fcode = Me.DataGridView4.Rows(X2).Cells(1).Value

            If User_Code <> 1 Then
                If Scode = 2 Or Scode = 3 Or Scode = 7 Or Scode = 10 Or Scode = 12 Or Scode = 15 Or Scode = 16 Then
                    MsgbOK.Label1.Text = " این سیستم مربوط به واحد کامپیوتر میباشد لطفا با مسئول سیستم هماهنگ کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtUCode.Focus()
                    Exit Sub
                End If
            End If
            If Me.DataGridView4.Rows(X2).Cells(0).Value = Nothing Or Me.DataGridView4.Rows(X2).Cells(0).Value = False Then
                Me.DataGridView4.Rows(X2).Cells(0).Value = True

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " INSERT INTO bas.Form_Sec (U_Code, S_Code, T_Code, F_Code)VALUES (@u_code, @s_code, @t_code, @f_code)"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@u_code", txtUCode.Text)
                objCommand.Parameters.AddWithValue("@s_code", Scode)
                objCommand.Parameters.AddWithValue("@t_code", Tcode)
                objCommand.Parameters.AddWithValue("@f_code", Fcode)
            Else
                Me.DataGridView4.Rows(X2).Cells(0).Value = False

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Form_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ") AND (T_code = " & Tcode & ") AND (f_code = " & Fcode & ")"
            End If

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            'Form()
            'FillDGVForm()
            'ChkValueForm()
            SED()
            FillDGVSED()
            ChkValueSED()
        End If
    End Sub

    Private Sub DataGridView4_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick

    End Sub

    Private Sub DataGridView5_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub DataGridView4_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView4.KeyUp
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            SED()
            FillDGVSED()
            ChkValueSED()
        End If
    End Sub

    Private Sub DataGridView4_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView4.RowHeaderMouseClick
        SED()
        FillDGVSED()
        ChkValueSED()
    End Sub

    Private Sub DataGridView5_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView5.CellClick
        Dim objCommand As New SqlCommand

        Dim X As Object
        Dim X1 As Object
        Dim X2 As Object
        Dim Scode As Object
        Dim Tcode As Object
        Dim Fcode As Object

        If DataGridView3.RowCount > 0 Then
            If DataGridView4.RowCount > 0 Then
                X = Me.DataGridView4.CurrentCell.RowIndex
                X1 = Me.DataGridView3.CurrentCell.RowIndex
                X2 = Me.DataGridView2.CurrentCell.RowIndex
                Fcode = Me.DataGridView4.Rows(X).Cells(1).Value
                Tcode = Me.DataGridView3.Rows(X1).Cells(1).Value
                Scode = Me.DataGridView2.Rows(X2).Cells(1).Value

                If User_Code <> 1 Then
                    If Scode = 15 Then
                        MsgbOK.Label1.Text = " این سیستم مربوط به واحد کامپیوتر میباشد لطفا با مسئول سیستم هماهنگ کنید . "
                        MsgbOK.ShowDialog()
                        Me.txtUCode.Focus()
                        Exit Sub
                    End If
                End If

                If e.ColumnIndex = 3 Then
                    If Me.DataGridView5.Rows(0).Cells(3).Value = Nothing Or Me.DataGridView5.Rows(0).Cells(3).Value = False Then
                        Me.DataGridView5.Rows(0).Cells(3).Value = True
                    Else
                        Me.DataGridView5.Rows(0).Cells(3).Value = False
                    End If
                    txt1.Text = DataGridView5.Rows(0).Cells(3).Value

                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                        "UPDATE Bas.Form_Sec SET Sayer = '" & txt1.Text & "' WHERE (U_Code = " & txtUCode.Text & ") AND (S_Code = " & Scode & ") AND (t_Code = " & Tcode & ") AND (F_Code = " & Fcode & ")"
                    objCommand.CommandType = CommandType.Text
                    txt1.Text = ""

                    objConnection.Open()
                    Try
                        objCommand.ExecuteNonQuery()
                    Catch SqlExceptionErr As SqlException
                        MessageBox.Show(SqlExceptionErr.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    SED()
                    FillDGVSED()
                    ChkValueSED()
                ElseIf e.ColumnIndex = 2 Then
                    If Me.DataGridView5.Rows(0).Cells(2).Value = Nothing Or Me.DataGridView5.Rows(0).Cells(2).Value = False Then
                        Me.DataGridView5.Rows(0).Cells(2).Value = True
                    Else
                        Me.DataGridView5.Rows(0).Cells(2).Value = False
                    End If
                    txt1.Text = DataGridView5.Rows(0).Cells(2).Value

                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                        "UPDATE Bas.Form_Sec SET Sav = '" & txt1.Text & "' WHERE (U_Code = " & txtUCode.Text & ") AND (S_Code = " & Scode & ") AND (t_Code = " & Tcode & ") AND (F_Code = " & Fcode & ")"
                    objCommand.CommandType = CommandType.Text
                    txt1.Text = ""

                    objConnection.Open()
                    Try
                        objCommand.ExecuteNonQuery()
                    Catch SqlExceptionErr As SqlException
                        MessageBox.Show(SqlExceptionErr.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    SED()
                    FillDGVSED()
                    ChkValueSED()
                ElseIf e.ColumnIndex = 1 Then
                    If Me.DataGridView5.Rows(0).Cells(1).Value = Nothing Or Me.DataGridView5.Rows(0).Cells(1).Value = False Then
                        Me.DataGridView5.Rows(0).Cells(1).Value = True
                    Else
                        Me.DataGridView5.Rows(0).Cells(1).Value = False
                    End If
                    txt1.Text = DataGridView5.Rows(0).Cells(1).Value

                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                        "UPDATE Bas.Form_Sec SET Edt = '" & txt1.Text & "' WHERE (U_Code = " & txtUCode.Text & ") AND (S_Code = " & Scode & ") AND (t_Code = " & Tcode & ") AND (F_Code = " & Fcode & ")"
                    objCommand.CommandType = CommandType.Text
                    txt1.Text = ""

                    objConnection.Open()
                    Try
                        objCommand.ExecuteNonQuery()
                    Catch SqlExceptionErr As SqlException
                        MessageBox.Show(SqlExceptionErr.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    SED()
                    FillDGVSED()
                    ChkValueSED()
                ElseIf e.ColumnIndex = 0 Then
                    If Me.DataGridView5.Rows(0).Cells(0).Value = Nothing Or Me.DataGridView5.Rows(0).Cells(0).Value = False Then
                        Me.DataGridView5.Rows(0).Cells(0).Value = True
                    Else
                        Me.DataGridView5.Rows(0).Cells(0).Value = False
                    End If
                    txt1.Text = DataGridView5.Rows(0).Cells(0).Value

                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                        "UPDATE Bas.Form_Sec SET Del = '" & txt1.Text & "' WHERE (U_Code = " & txtUCode.Text & ") AND (S_Code = " & Scode & ") AND (t_Code = " & Tcode & ") AND (F_Code = " & Fcode & ")"
                    objCommand.CommandType = CommandType.Text
                    txt1.Text = ""

                    objConnection.Open()
                    Try
                        objCommand.ExecuteNonQuery()
                    Catch SqlExceptionErr As SqlException
                        MessageBox.Show(SqlExceptionErr.Message)
                        objConnection.Close()
                        Exit Sub
                    End Try
                    objConnection.Close()

                    SED()
                    FillDGVSED()
                    ChkValueSED()
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView5_CellContentClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView5.CellContentClick

    End Sub

    Private Sub txtPass_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass.LostFocus
        Dim myCulture As New Globalization.CultureInfo("FA-IR")
        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(myCulture)
    End Sub

    Private Sub txtPass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPass.TextChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.Click
        Dim ObjCommand As New SqlCommand
        Dim Cnt As String
        Dim X As Integer
        Dim Shcode As String
        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            If CheckBox1.Checked = False Then
                Shcode = Me.DataGridView1.Rows(X).Cells(1).Value
                Me.DataGridView1.Rows(X).Cells(0).Value = True

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                     " INSERT INTO bas.Shob_Sec (U_Code, Shob_Code)VALUES (" & txtUCode.Text & ", " & Shcode & ")"
            ElseIf CheckBox1.Checked = True Then
                Shcode = Me.DataGridView1.Rows(X).Cells(1).Value
                Me.DataGridView1.Rows(X).Cells(0).Value = False

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                   " DELETE FROM bas.Shob_Sec WHERE(u_code = " & txtUCode.Text & ") AND (Shob_code = " & Shcode & ")"
            End If

            objConnection.Open()
            Try
                ObjCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        Next X
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub CheckBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox2.Click
        Dim ObjCommand As New SqlCommand
        Dim Cnt As String
        Dim X As Integer
        Dim Scode As String
        Cnt = DataGridView2.RowCount - 1
        For X = 0 To Cnt
            If CheckBox2.Checked = False Then
                Scode = Me.DataGridView2.Rows(X).Cells(1).Value
                Me.DataGridView2.Rows(X).Cells(0).Value = True
                DataGridView3.Enabled = True

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                     " INSERT INTO bas.Soft_Sec (U_Code, S_Code)VALUES (" & txtUCode.Text & ", " & Scode & ")"

                objConnection.Open()
                Try
                    ObjCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()
            ElseIf CheckBox2.Checked = True Then
                Scode = Me.DataGridView2.Rows(X).Cells(1).Value
                Me.DataGridView2.Rows(X).Cells(0).Value = False
                DataGridView3.Enabled = False
                DataGridView4.Enabled = False
                DataGridView5.Enabled = False

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                   " DELETE FROM bas.Soft_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ")"

                objConnection.Open()
                Try
                    ObjCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                   " DELETE FROM bas.Title_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ")"

                objConnection.Open()
                Try
                    ObjCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                   " DELETE FROM bas.Form_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ")"

                objConnection.Open()
                Try
                    ObjCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()
            End If
        Next X

        Title()
        FillDGVTitle()
        ChkValueTitle()
        Form()
        FillDGVForm()
        ChkValueForm()
        SED()
        FillDGVSED()
        ChkValueSED()
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged

    End Sub

    Private Sub CheckBox3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox3.Click
        Dim ObjCommand As New SqlCommand
        Dim Cnt As String
        Dim X As Integer
        Dim X1 As Integer
        Dim Tcode As String
        Dim Scode As String
        Cnt = DataGridView3.RowCount - 1
        For X = 0 To Cnt
            If CheckBox3.Checked = False Then
                X1 = Me.DataGridView2.CurrentCell.RowIndex
                Scode = Me.DataGridView2.Rows(X1).Cells(1).Value
                Tcode = Me.DataGridView3.Rows(X).Cells(1).Value
                Me.DataGridView3.Rows(X).Cells(0).Value = True
                DataGridView4.Enabled = True

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                     " INSERT INTO bas.Title_Sec (U_Code, S_Code, T_Code)VALUES (" & txtUCode.Text & ", " & Scode & ", " & Tcode & ")"

                objConnection.Open()
                Try
                    ObjCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()
            ElseIf CheckBox3.Checked = True Then
                X1 = Me.DataGridView2.CurrentCell.RowIndex
                Scode = Me.DataGridView2.Rows(X1).Cells(1).Value
                Tcode = Me.DataGridView3.Rows(X).Cells(1).Value
                Me.DataGridView3.Rows(X).Cells(0).Value = False
                DataGridView4.Enabled = False
                DataGridView5.Enabled = False

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                   " DELETE FROM bas.Title_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ") AND (T_code = " & Tcode & ")"

                objConnection.Open()
                Try
                    ObjCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                   " DELETE FROM bas.Form_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ")"

                objConnection.Open()
                Try
                    ObjCommand.ExecuteNonQuery()
                Catch SqlExceptionErr As SqlException
                    MessageBox.Show(SqlExceptionErr.Message)
                    objConnection.Close()
                    Exit Sub
                End Try
                objConnection.Close()
            End If
        Next X

        Form()
        FillDGVForm()
        ChkValueForm()
        SED()
        FillDGVSED()
        ChkValueSED()
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged

    End Sub

    Private Sub CheckBox4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox4.Click
        Dim objCommand As New SqlCommand
        Dim X As Integer
        Dim X1 As Integer
        Dim X2 As Integer
        Dim Scode As String
        Dim Tcode As String
        Dim Fcode As String
        Dim Cnt As String

        Cnt = DataGridView4.RowCount - 1
        For X = 0 To Cnt
            If CheckBox4.Checked = False Then
                X1 = Me.DataGridView2.CurrentCell.RowIndex
                Scode = Me.DataGridView2.Rows(X1).Cells(1).Value
                X2 = Me.DataGridView3.CurrentCell.RowIndex
                Tcode = Me.DataGridView3.Rows(X2).Cells(1).Value
                Fcode = Me.DataGridView4.Rows(X).Cells(1).Value
                Me.DataGridView4.Rows(X).Cells(0).Value = True
                DataGridView5.Enabled = True

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                     " INSERT INTO bas.Form_Sec (U_Code, S_Code, T_Code, F_Code, Sayer, Sav, Edt, Del)VALUES (" & txtUCode.Text & ", " & Scode & ", " & Tcode & ", " & Fcode & ", '" & True & "', '" & True & "', '" & True & "', '" & True & "')"
            ElseIf CheckBox4.Checked = True Then
                X1 = Me.DataGridView2.CurrentCell.RowIndex
                Scode = Me.DataGridView2.Rows(X1).Cells(1).Value
                X2 = Me.DataGridView3.CurrentCell.RowIndex
                Tcode = Me.DataGridView3.Rows(X2).Cells(1).Value
                Fcode = Me.DataGridView4.Rows(X).Cells(1).Value
                Me.DataGridView4.Rows(X).Cells(0).Value = False
                DataGridView5.Enabled = False

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Form_Sec WHERE(u_code = " & txtUCode.Text & ") AND (S_code = " & Scode & ") AND (T_code = " & Tcode & ") AND (f_code = " & Fcode & ")"
            End If

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        Next X

        SED()
        FillDGVSED()
        ChkValueSED()
    End Sub

    Private Sub txtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.GotFocus
        txtCode.SelectionStart = 0
        txtCode.SelectionLength = Len(txtCode.Text)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.LostFocus
        If txtCode.Text <> "" Then
            Pcd = txtCode.Text
            FillPers1()

            IntPosition = objDataview.Count
            If IntPosition = 0 Then
                MsgbOK.Label1.Text = "کد فوق در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                txtCode.Clear()
                txtName.Clear()
                txtFamily.Clear()
                txtCode.Focus()
                Exit Sub
            Else
                txtName.DataBindings.Clear()
                txtName.DataBindings.Add("Text", objDataview, "Pers_Name")
                txtFamily.DataBindings.Clear()
                txtFamily.DataBindings.Add("Text", objDataview, "Pers_Family")
            End If
        End If
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillUserList()
        If txtSrch.Text <> "" Then
            Select Case ColumnSrch
                Case "4"
                    objDataview.RowFilter = "U_Family like '" & "**" & Me.txtSrch.Text & "**" & "'"
            End Select
            FillUserView()
        Else
            FillUserList()
            FillUserView()
        End If
    End Sub

    Private Sub DataGridView6_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView6.CellContentClick

    End Sub

    Private Sub DataGridView6_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView6.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        If ColumnSrch <> 0 Then
            Me.Label11.Text = Me.DataGridView6.Columns(e.ColumnIndex).HeaderText
            Me.txtSrch.Text = ""
            Me.txtSrch.Focus()
        End If
    End Sub

    Private Sub DataGridView7_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView7.CellClick
        Dim objCommand As New SqlCommand
        If e.ColumnIndex = 0 Then
            Dim X As Object
            Dim Cod As Integer
            X = Me.DataGridView7.CurrentCell.RowIndex
            If Me.DataGridView7.Rows(X).Cells(0).Value = Nothing Or Me.DataGridView7.Rows(X).Cells(0).Value = False Then
                Me.DataGridView7.Rows(X).Cells(0).Value = True

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " INSERT INTO Bas.Sal_Sec (U_Code, Code)VALUES (@u_code, @code)"
                objCommand.CommandType = CommandType.Text
                objCommand.Parameters.AddWithValue("@u_code", txtUCode.Text)
                objCommand.Parameters.AddWithValue("@code", DataGridView7.Rows(X).Cells(1).Value)
            Else
                Cod = Me.DataGridView7.Rows(X).Cells(1).Value
                Me.DataGridView7.Rows(X).Cells(0).Value = False

                objCommand.Connection = objConnection
                objCommand.CommandText = _
                   " DELETE FROM bas.Sal_Sec WHERE(u_code = " & txtUCode.Text & ") AND (code = " & Cod & ")"
            End If

            objConnection.Open()
            Try
                objCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            'FillDataSal()
            'FillDGVSal()
            'ChkValueSal()
        End If
    End Sub

    Private Sub DataGridView7_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView7.CellContentClick

    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged

    End Sub

    Private Sub CheckBox6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox6.Click
        Dim ObjCommand As New SqlCommand
        Dim Cnt As String
        Dim X As Integer
        Dim Code As String
        Cnt = DataGridView7.RowCount - 1
        For X = 0 To Cnt
            If CheckBox1.Checked = False Then
                Code = Me.DataGridView7.Rows(X).Cells(1).Value
                Me.DataGridView7.Rows(X).Cells(0).Value = True

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                     " INSERT INTO bas.Sal_Sec (U_Code, Code)VALUES (" & txtUCode.Text & ", " & Code & ")"
            ElseIf CheckBox1.Checked = True Then
                Code = Me.DataGridView7.Rows(X).Cells(1).Value
                Me.DataGridView7.Rows(X).Cells(0).Value = False

                ObjCommand.Connection = objConnection
                ObjCommand.CommandText = _
                   " DELETE FROM bas.Sal_Sec WHERE(u_code = " & txtUCode.Text & ") AND (code = " & Code & ")"
            End If

            objConnection.Open()
            Try
                ObjCommand.ExecuteNonQuery()
            Catch SqlExceptionErr As SqlException
                MessageBox.Show(SqlExceptionErr.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        Next X
    End Sub

    Private Sub DataGridView6_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView6.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView6_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView6.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub

    Private Sub cboShob_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboShob.SelectedIndexChanged

    End Sub

    Private Sub cboShob_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboShob.SelectedValueChanged
        If Me.cboShob.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillUcode()
            FillUserList()
            FillUserView()
            Clear()
        End If
    End Sub

    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged

    End Sub

    Private Sub RB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB2.CheckedChanged

    End Sub

    Private Sub txtUCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUCode.GotFocus
        txtUCode.SelectionStart = 0
        txtUCode.SelectionLength = Len(txtUCode.Text)
    End Sub

    Private Sub txtUCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUCode.KeyPress
        Dim objcommand As New SqlCommand
        Dim X As Integer
        Dim X1 As Integer
        If e.KeyChar = ChrW(13) Then
            If Me.txtUCode.Text <> "" Then
                FillDataUser()
                objDataview.Sort = "U_Code"
                IntPosition = objDataview.Find(txtUCode.Text)
                If IntPosition = -1 Then
                    Me.txtPass.Text = ""
                    Me.txtName.Text = ""
                    Me.txtFamily.Text = ""
                    Me.txtCode.Text = ""
                    CheckBox5.Checked = False
                    Me.txtPass.Focus()
                    Exit Sub
                Else
                    objCurrencyManager.Position = IntPosition
                    Me.txtPass.DataBindings.Clear()
                    Me.txtPass.DataBindings.Add("text", objDataview, "U_Pass")
                    Me.txtName.DataBindings.Clear()
                    Me.txtName.DataBindings.Add("text", objDataview, "U_Name")
                    Me.txtFamily.DataBindings.Clear()
                    Me.txtFamily.DataBindings.Add("text", objDataview, "U_Family")
                    Me.txtCode.DataBindings.Clear()
                    Me.txtCode.DataBindings.Add("text", objDataview, "pers_code")
                    txt1.DataBindings.Clear()
                    Me.txt1.DataBindings.Add("text", objDataview, "Adm")
                    If IsDBNull(txt1.Text) Or txt1.Text = "" Then
                        txt1.Text = "False"
                    End If
                    CheckBox5.Checked = txt1.Text
                    txt1.DataBindings.Clear()
                    Me.txt1.DataBindings.Add("text", objDataview, "Act")
                    If txt1.Text = 1 Then
                        RB1.Checked = True
                    Else
                        RB2.Checked = True
                    End If
                    txt1.Clear()
                    txtPass.Focus()

                    Label6.Enabled = True
                    Label7.Enabled = True
                    Label8.Enabled = True
                    Label9.Enabled = True
                    Label13.Enabled = True
                    CheckBox1.Visible = True
                    CheckBox2.Visible = True
                    CheckBox3.Visible = True
                    CheckBox4.Visible = True
                    CheckBox6.Visible = True
                    DataGridView1.Visible = True
                    DataGridView2.Visible = True
                    DataGridView3.Visible = True
                    DataGridView4.Visible = True
                    DataGridView5.Visible = True
                    DataGridView7.Visible = True

                    If RB1.Checked = True Then
                        DataGridView1.Visible = True
                        DataGridView2.Enabled = True
                        DataGridView3.Enabled = True
                        DataGridView4.Enabled = True
                        DataGridView5.Enabled = True
                        DataGridView7.Visible = True
                    Else
                        Label6.Enabled = False
                        Label7.Enabled = False
                        Label8.Enabled = False
                        Label9.Enabled = False
                        Label13.Enabled = False
                        CheckBox1.Visible = False
                        CheckBox2.Visible = False
                        CheckBox3.Visible = False
                        CheckBox4.Visible = False
                        CheckBox6.Visible = False
                        DataGridView1.Visible = False
                        DataGridView2.Visible = False
                        DataGridView3.Visible = False
                        DataGridView4.Visible = False
                        DataGridView5.Visible = False
                        DataGridView7.Visible = False
                    End If

                    FillDataShob()
                    FillDGVShob()
                    ChkValueShob()
                    FillDataSal()
                    FillDGVSal()
                    ChkValueSal()
                    SoftWare()
                    FillDGVSoft()
                    ChkValueSoft()
                    Title()
                    FillDGVTitle()
                    ChkValueTitle()
                    Form()
                    FillDGVForm()
                    ChkValueForm()
                    SED()
                    FillDGVSED()
                    ChkValueSED()
                    ' FillDataUser()

                    X = Me.DataGridView2.CurrentCell.RowIndex
                    If DataGridView2.Rows(X).Cells(0).Value = True Then
                        DataGridView3.Enabled = True
                    Else
                        DataGridView3.Enabled = False
                    End If

                    X1 = Me.DataGridView3.CurrentCell.RowIndex
                    If DataGridView3.Rows(X).Cells(0).Value = True Then
                        DataGridView4.Enabled = True
                    Else
                        DataGridView4.Enabled = False
                    End If
                End If
            Else
                Clear()
            End If
        End If
    End Sub

    Private Sub txtUCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUCode.LostFocus
        Dim myCulture As New Globalization.CultureInfo("EN-US")
        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(myCulture)
    End Sub

    Private Sub txtUCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUCode.TextChanged

    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = False Then
            cboMove.Visible = False
            btnMove.Visible = False
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnDelete.Enabled = True
        Else
            cboMove.Visible = True
            btnMove.Visible = True
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim objCommand As New SqlCommand
        Dim Ucode As String
        Dim Shobcode As String
        Dim x As Integer
        Dim cnt As String

        cnt = Me.DataGridView6.Rows.Count - 1
        For x = 0 To cnt
            If DataGridView6.Rows(x).Selected = True Then
                If Me.DataGridView6.Rows(x).Cells(8).Value <> Me.cboMove.SelectedValue Then
                    Shobcode = cboMove.SelectedValue
                    Ucode = DataGridView6.Rows(x).Cells(0).Value

                    IntPosition = objCurrencyManager.Position
                    objCommand.Connection = objConnection
                    objCommand.CommandText = _
                       " UPDATE Bas.UserList SET Shob = " & Shobcode & " WHERE (U_Code = " & Ucode & ")"
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
        FillUserList()
        FillUserView()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Security_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        FillUcode()
    End Sub
End Class