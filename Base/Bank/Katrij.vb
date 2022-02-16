Public Class Katrij
    Public Sub FillRow()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT MAX(code) AS Expr1 FROM Bas.Katrij", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Katrij")
        objDataview = New DataView(objDataset.Tables("Bas.Katrij"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Expr1"

        Label3.DataBindings.Clear()
        Label3.DataBindings.Add("Text", objDataview, "Expr1")
        If Label3.Text <> "" Then
            txtCode.Text = Val(Label3.Text) + 1
        ElseIf Label3.Text = "" Then
            txtCode.Text = 1
        End If
        Label3.DataBindings.Clear()
        Label3.Text = ""
    End Sub
    Private Sub FillDataSetAndDataView()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT code, name FROM Bas.Katrij ORDER BY code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.Katrij")
        objDataview = New DataView(objDataset.Tables("Bas.Katrij"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 500
    End Sub

    Private Sub DGVMove()
        Dim X As Integer
        X = DataGridView1.CurrentCell.RowIndex
        txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
        txtName.Text = DataGridView1.Rows(X).Cells(1).Value
    End Sub
    Private Sub Clear()
        txtName.Text = ""
        txtName.Focus()
    End Sub

    Private Sub Katrij_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillRow()
        FillDataSetAndDataView()
        FillDataGridView()
    End Sub

    Private Sub Katrij_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        FormName = "Katrij"
    End Sub

    Private Sub Katrij_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dispose()
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Dispose()
    End Sub

    Private Sub InsertUpdate()
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdKatrij"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@code", txtCode.Text)
            objCommand.Parameters.AddWithValue("@name", txtName.Text)

            objCommand.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            objConnection.Close()
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition <> -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & txtCode.Text & "  قبلا در سیستم ثبت شده است . "
            MsgbOK.ShowDialog()
            Me.btnEdit.Focus()
            Exit Sub
        Else
            Btn = 1
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtName.Text = "" Then
            MsgbOK.Label1.Text = " لطفا کد را وارد کنید . "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره برگه " & txtCode.Text & "  قبلا در سیستم ثبت نشده است . "
            MsgbOK.ShowDialog()
            Me.btnSave.Focus()
            Exit Sub
        Else
            Btn = 2
            InsertUpdate()

            Ref.PerformClick()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        FillDataSetAndDataView()
        IntPosition = objDataview.Find(txtCode.Text)
        If IntPosition = -1 Then
            MsgbOK.Label1.Text = " شماره ردیف " & txtCode.Text & " در سیستم موجود نمی باشد. "
            MsgbOK.ShowDialog()
            txtCode.Focus()
            Exit Sub
        Else
            MsgbYN.Label1.Text = "  آیا می خواهید شماره ردیف  " & txtCode.Text & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtCode.Focus()
                Exit Sub
            Else
                Btn = 3
                InsertUpdate()

               Ref.PerformClick()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        txtName.SelectionStart = 0
        txtName.SelectionLength = Len(txtName.Text)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDataSetAndDataView()
        FillDataGridView()
        Clear()
    End Sub
End Class