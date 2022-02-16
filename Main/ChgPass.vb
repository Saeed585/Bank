Imports System.Data
Imports System.Data.SqlClient

Public Class ChgPass
    Inherits System.Windows.Forms.Form

    Private Sub FillDataSetAndDataView()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
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
           " INSERT INTO Bas.UserListTmp (U_Code, U_Pass)" & _
           " SELECT U_Code, U_Pass FROM Bas.UserList WHERE (U_Code = " & User_Code & ") AND (Shob = " & Shob & ")"
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

        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT U_Code, U_Pass FROM Bas.UserListTmp ORDER BY U_Code", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Bas.UserListTmp")
        objDataview = New DataView(objDataset.Tables("Bas.UserListTmp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "U_Code"
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub txtPassOld_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassOld.GotFocus
        Me.txtPassOld.SelectionStart = 0
        Me.txtPassOld.SelectionLength = Len(Me.txtPassOld.Text)
    End Sub

    Private Sub txtPassOld_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassOld.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.txtPassNew.Focus()
        End If
    End Sub

    Private Sub txtPassNew_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassNew.GotFocus
        Me.txtPassNew.SelectionStart = 0
        Me.txtPassNew.SelectionLength = Len(Me.txtPassNew.Text)
    End Sub

    Private Sub txtPassNew_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassNew.KeyPress
        If e.KeyChar = ChrW(13) Then
            Me.btnOk.Focus()
        End If
    End Sub

    Private Sub InsertUpdate()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        Try
            objConnection.Open()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "Bas.InsUpdUCode"
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@Btn", Btn)
            objCommand.Parameters.AddWithValue("@U_Code", User_Code)
            objCommand.Parameters.AddWithValue("@U_Pass", txtPassNew.Text)
            objCommand.Parameters.AddWithValue("@U_Name", "")
            objCommand.Parameters.AddWithValue("@U_Family", "")
            objCommand.Parameters.AddWithValue("@Pcode", 0)
            objCommand.Parameters.AddWithValue("@Adm", 0)
            objCommand.Parameters.AddWithValue("@Act", 0)
            objCommand.Parameters.AddWithValue("@Flg", 2)
            objCommand.Parameters.AddWithValue("@Shob", Shob)

            objCommand.ExecuteNonQuery()
        Catch Ex As Exception
            MessageBox.Show(Ex.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim objcommand As New SqlCommand

        If Me.txtPassOld.Text = "" Then
            MsgbOK.Label1.Text = " کلمه عبور قدیم را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtPassOld.Focus()
            Exit Sub
        ElseIf Me.txtPassNew.Text = "" Then
            MsgbOK.Label1.Text = " کلمه عبور جدید را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtPassOld.Focus()
            Exit Sub
        End If

        FillDataSetAndDataView()
        IntPosition = objDataview.Find(User_Code)
        objCurrencyManager.Position = IntPosition
        Me.Label3.DataBindings.Clear()
        Me.Label3.DataBindings.Add("text", objDataview, "U_Pass")
        If Label3.Text = txtPassOld.Text Then
            Btn = 2
            InsertUpdate()

            MsgbOK.Label1.Text = " کلمه عبور جدید ثبت گردید . "
            MsgbOK.ShowDialog()
            Label3.Text = ""
            Me.txtPassOld.Text = ""
            Me.txtPassNew.Text = ""
            Me.txtPassOld.Focus()
        Else
            MsgbOK.Label1.Text = " کلمه عبور قدیم را صحیح وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtPassOld.Text = ""
            Me.txtPassOld.Focus()
        End If
        objDataset.Reset()
    End Sub

    Private Sub ChgPass_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.txtPassOld.Focus()
    End Sub

    Private Sub ChgPass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.GroupBox1.Text = Mainfrm.lblU_name.Text + " " + Mainfrm.lblU_family.Text
    End Sub

    Private Sub txtPassOld_TextChanged(sender As Object, e As EventArgs) Handles txtPassOld.TextChanged

    End Sub
End Class