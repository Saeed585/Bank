Public Class Company
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim objcommand As New SqlCommand
        If Me.txtComp.Text = "" Then
            MsgbOK.Label1.Text = " لطفا نام موسسه را وارد کنید . "
            MsgbOK.ShowDialog()
            Me.txtComp.Focus()
            Exit Sub
        End If

        objcommand.Connection = objConnection
        objcommand.CommandText = _
           " UPDATE bas.Company SET CompName = @comp"
        objcommand.CommandType = CommandType.Text
        objcommand.Parameters.AddWithValue("@Comp", txtComp.Text)

        objConnection.Open()
        Try
            objcommand.ExecuteNonQuery()
        Catch SqlExceptionErr As SqlException
            MessageBox.Show(SqlExceptionErr.Message)
            objConnection.Close()
            Exit Sub
        End Try
        objConnection.Close()

        Mainfrm.lblCompName.Text = txtComp.Text
        Me.Dispose()
    End Sub

    Private Sub Company_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtComp.Text = Mainfrm.lblCompName.Text
    End Sub

    Private Sub txtComp_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComp.GotFocus
        txtComp.SelectionStart = 0
        txtComp.SelectionLength = Len(txtComp.Text)
    End Sub

    Private Sub txtComp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtComp.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnOk.Focus()
        End If
    End Sub

    Private Sub txtComp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComp.TextChanged

    End Sub
End Class