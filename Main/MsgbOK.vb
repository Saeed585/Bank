
Public Class MsgbOK

    Private Sub MsgbOK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AutoSize = True
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 176
        Me.CenterToScreen()
        FormName = "MsgbOK"
    End Sub

    Private Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
        Me.AutoSize = False
        Me.Dispose()
    End Sub
End Class