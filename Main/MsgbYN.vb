
Public Class MsgbYN
    Private Sub MsgbYN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AutoSize = True
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 176
        Me.CenterToScreen()
        Me.btnNo.Focus()
        FormName = "MsgbYN"
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.AutoSize = False
        Me.Dispose()
    End Sub

    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Me.AutoSize = False
        Me.Dispose()
        Me.btnNo.Focus()
    End Sub
End Class