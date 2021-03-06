
Public Class KalaPriod
    Dim ColumnSrch As Integer
    Private Sub fillcboSGrpKala()
        Dim objDataAdapter As New SqlDataAdapter _
             (" SELECT Bas.KalaSGrp.SCode, Bas.KalaSGrp.SName FROM Bas.KalaSGrp INNER JOIN Bas.SGKala_Sec ON Bas.KalaSGrp.SCode = Bas.SGKala_Sec.SarGrpK_Code" &
             " WHERE (Bas.SGKala_Sec.U_Code = 1) ORDER BY Bas.KalaSGrp.SCode", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.KalaSGrp")
        Me.cboSGrpKala.DataSource = objDataset.Tables("bas.KalaSGrp")
        Me.cboSGrpKala.DisplayMember = "SName"
        Me.cboSGrpKala.ValueMember = "SCode"
    End Sub

    Private Sub FillDataSetAndDataView()
        Dim SGcode As String
        SGcode = cboSGrpKala.SelectedValue.ToString
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.KalaPriod.Scode, Bas.KalaPriod.Code, Bas.Kala.kala_Name, Bas.KalaPriod.Priod, Bas.KalaPriod.Alarm" &
             " FROM Bas.KalaPriod INNER JOIN Bas.Kala ON Bas.Kala.Kala_Code = Bas.KalaPriod.Code WHERE (Bas.Kala.SCode = " & SGcode & ") ORDER BY Bas.KalaPriod.Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.KalaPriod")
        objDataview = New DataView(objDataset.Tables("Bas.KalaPriod"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).Visible = False ' Scode
        Me.DataGridView1.Columns(1).Visible = False ' Code
        Me.DataGridView1.Columns(2).HeaderText = "نام کالا"
        Me.DataGridView1.Columns(3).HeaderText = "مدت تعویض"
        Me.DataGridView1.Columns(4).HeaderText = "زمان اعلام"

        Me.DataGridView1.Columns(2).Width = 250
    End Sub
    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        cboSGrpKala.SelectedValue = DataGridView1.Rows(X).Cells(0).Value
        txtCode.Text = DataGridView1.Rows(X).Cells(1).Value
        txtName.Text = DataGridView1.Rows(X).Cells(2).Value
        txtMah.Text = DataGridView1.Rows(X).Cells(3).Value
        txtDay.Text = DataGridView1.Rows(X).Cells(4).Value
    End Sub
    Private Sub Clear()
        txtCode.Text = ""
        txtName.Text = ""
        txtMah.Text = ""
        txtDay.Text = ""
        txtName.Focus()
    End Sub
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub KalaPriod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillcboSGrpKala()
        FillDataSetAndDataView()
        FillDataGridView()

        txtCode.SendToBack()
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        If txtName.Text = "" Then
            txtCode.Text = ""
        End If
    End Sub

    Private Sub txtName_GotFocus(sender As Object, e As EventArgs) Handles txtName.GotFocus
        Me.txtName.SelectionStart = 0
        Me.txtName.SelectionLength = Len(Me.txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        Dim SGcode As String
        SGcode = cboSGrpKala.SelectedValue.ToString
        If e.KeyChar = ChrW(13) Then
            If txtCode.Text = "" Then
                Code_Soft = 1
                Pcd = SGcode
                FormName = "KalaPriod"
                FillKalaGaurd()
                FillDataKala()
                Srch.lblName.Text = "لیست کالا"
                Srch.ShowDialog()
            Else
                txtMah.Focus()
            End If
        End If
    End Sub

    Private Sub txtMah_TextChanged(sender As Object, e As EventArgs) Handles txtMah.TextChanged

    End Sub

    Private Sub txtMah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMah.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDay.Focus()
        End If
    End Sub

    Private Sub txtMah_GotFocus(sender As Object, e As EventArgs) Handles txtMah.GotFocus
        Me.txtMah.SelectionStart = 0
        Me.txtMah.SelectionLength = Len(Me.txtMah.Text)
    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdKalaPriod"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@Scode", cboSGrpKala.SelectedValue.ToString)
            objCommand.Parameters.AddWithValue("@Code", txtCode.Text)
            objCommand.Parameters.AddWithValue("@Priod", txtMah.Text)
            objCommand.Parameters.AddWithValue("@Alarm", txtDay.Text)

            objCommand.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            objConnection.Close()
        End Try
        objConnection.Close()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf txtMah.Text = "" Then
            MsgbOK.Label1.Text = " لطفا مدت تعویض را وارد کنید . "
            MsgbOK.ShowDialog()
            txtMah.Focus()
            Exit Sub
        ElseIf txtDay.Text = "" Then
            MsgbOK.Label1.Text = " لطفا زمان اعلام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtDay.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " کالای " & Trim(txtName.Text) & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            FillDataSetAndDataView()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        ElseIf txtMah.Text = "" Then
            MsgbOK.Label1.Text = " لطفا مدت تعویض را وارد کنید . "
            MsgbOK.ShowDialog()
            txtMah.Focus()
            Exit Sub
        ElseIf txtDay.Text = "" Then
            MsgbOK.Label1.Text = " لطفا زمان اعلام را وارد کنید . "
            MsgbOK.ShowDialog()
            txtDay.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کالای " & Trim(txtName.Text) & "  در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            FillDataSetAndDataView()
            FillDataGridView()
            Clear()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If Me.txtCode.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام کالا را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtName.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " کالای " & Trim(txtName.Text) & "  در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید کالای  " & Trim(Me.txtName.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.txtName.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

                FillDataSetAndDataView()
                FillDataGridView()
                Clear()
            End If
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub cboSGrpKala_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSGrpKala.SelectedIndexChanged

    End Sub

    Private Sub cboSGrpKala_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboSGrpKala.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtDay_TextChanged(sender As Object, e As EventArgs) Handles txtDay.TextChanged

    End Sub

    Private Sub txtDay_GotFocus(sender As Object, e As EventArgs) Handles txtDay.GotFocus
        Me.txtDay.SelectionStart = 0
        Me.txtDay.SelectionLength = Len(Me.txtDay.Text)
    End Sub

    Private Sub txtDay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDay.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtMah_LostFocus(sender As Object, e As EventArgs) Handles txtMah.LostFocus
        If txtMah.Text > 12 Then
            MsgbOK.Label1.Text = " ثبت مدت تعویض بیشتر از 12 ماه امکان پذیر نیست . "
            MsgbOK.ShowDialog()
            Me.txtMah.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtSrch_TextChanged(sender As Object, e As EventArgs) Handles txtSrch.TextChanged
        FillDataSetAndDataView()
        Select Case ColumnSrch
            Case "1"
                objDataview.RowFilter = "Kala_Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        End Select
        FillDataGridView()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ColumnSrch = e.ColumnIndex
        txtSrch.Text = ""
        txtSrch.Focus()
    End Sub
End Class