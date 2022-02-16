Public Class UserList
    Private Sub FillDataSetAndDataView()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.UserSoft.U_Code, Bas.UserList.U_Name, Bas.UserList.U_Family, Bas.UserSoft.Dat, Bas.UserSoft.PcName, Bas.UserSoft.Software, " & _
            " COUNT(Bas.UserSoft.Tim) AS Expr1 FROM Bas.UserSoft INNER JOIN Bas.UserList ON Bas.UserSoft.U_Code = Bas.UserList.U_Code" & _
            " GROUP BY Bas.UserSoft.U_Code, Bas.UserSoft.Dat, Bas.UserSoft.PcName, Bas.UserList.U_Name, Bas.UserList.U_Family, Bas.UserSoft.Software ORDER BY Bas.UserSoft.Dat DESC", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.UserList")
        objDataview = New DataView(objDataset.Tables("Bas.UserList"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        ' objDataview.Sort = "u_code"
        txtCount.Text = objDataview.Count
    End Sub
    Private Sub FillDataGridView()
        Dim Cnt As String
        Dim X As Integer

        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).Visible = False
        Me.DataGridView1.Columns(1).HeaderText = "نام"
        Me.DataGridView1.Columns(2).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(3).HeaderText = "تاریخ ورود به سیستم"
        Me.DataGridView1.Columns(4).HeaderText = "نام کامپیوتر"
        Me.DataGridView1.Columns(5).HeaderText = "نام سیستم"
        Me.DataGridView1.Columns(6).HeaderText = "تعداد اجرا سیستم"

        ' Me.DataGridView1.Columns(0).Width = 40
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 120
        Me.DataGridView1.Columns(3).Width = 80
        Me.DataGridView1.Columns(4).Width = 100
        Me.DataGridView1.Columns(5).Width = 100
        Me.DataGridView1.Columns(6).Width = 60

        Cnt = DataGridView1.RowCount - 1
        For X = 0 To Cnt
            txtSoftWare.Text = DataGridView1.Rows(X).Cells(5).Value
            If txtSoftWare.Text = "ITmaintenance" Then
                DataGridView1.Rows(X).Cells(5).Value = "تدارکات و حراست"
            ElseIf txtSoftWare.Text = "ITloan" Then
                DataGridView1.Rows(X).Cells(5).Value = "وام"
            ElseIf txtSoftWare.Text = "ITAcc" Then
                DataGridView1.Rows(X).Cells(5).Value = "حسابداری"
            ElseIf txtSoftWare.Text = "ITPursuit" Then
                DataGridView1.Rows(X).Cells(5).Value = "درخواست تعمیر"
            ElseIf txtSoftWare.Text = "ITDana" Then
                DataGridView1.Rows(X).Cells(5).Value = "بیمه دانا"
            ElseIf txtSoftWare.Text = "ITBank" Then
                DataGridView1.Rows(X).Cells(5).Value = "بانک اطلاعاتی IT"
            ElseIf txtSoftWare.Text = "ITBarnameh" Then
                DataGridView1.Rows(X).Cells(5).Value = "بارنامه"
            ElseIf txtSoftWare.Text = "ITFact" Then
                DataGridView1.Rows(X).Cells(5).Value = "فروش"
            End If
            txtSoftWare.Text = ""
        Next X
    End Sub

    Private Sub UserList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Opacity = 1
        'Dim i As Integer
        'For i = 0 To 20
        '    Me.Opacity -= 0.01
        '    ' Application.DoEvents
        'Next
        FillDataSetAndDataView()
        FillDataGridView()
        txtCount.BorderStyle = BorderStyle.None
        txtSoftWare.SendToBack()
        FormName = "UserList"
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub
End Class