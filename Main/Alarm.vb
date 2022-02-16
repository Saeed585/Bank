
Public Class Alarm
    Private Sub FillDatasetAndDataview()
        Dim Dat As String
        Dat = ConvD.Class1.ConvDate
        ' Shob = Mainfrm.cboShob.SelectedValue.ToString
        Dim objDataAdapter1 As New SqlDataAdapter _
            (" SELECT Bas.Vaghaye.Row, Bas.Vaghaye.u_code, Bas.UserList.U_Name, Bas.UserList.U_Family, Bas.Vaghaye.Dat" & _
            " FROM Bas.Vaghaye INNER JOIN Bas.UserList ON Bas.Vaghaye.u_code = Bas.UserList.U_Code WHERE (Bas.Vaghaye.Alarm1 = '" & Dat & "') AND (Bas.Vaghaye.u_code = " & User_Code & ") AND (Bas.Vaghaye.RD1 = 0) ORDER BY Bas.Vaghaye.Row", objConnection)
        objDataset = New DataSet
        objDataAdapter1.Fill(objDataset, "Bas.Vaghaye")
        objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Row"

        If objDataview.Count = 0 Then
            Dim objDataAdapter2 As New SqlDataAdapter _
                (" SELECT Bas.Vaghaye.Row, Bas.Vaghaye.u_code, Bas.UserList.U_Name, Bas.UserList.U_Family, Bas.Vaghaye.Dat" & _
                " FROM Bas.Vaghaye INNER JOIN Bas.UserList ON Bas.Vaghaye.u_code = Bas.UserList.U_Code WHERE (Bas.Vaghaye.Alarm2 = '" & Dat & "') AND (Bas.Vaghaye.u_code = " & User_Code & ") AND (Bas.Vaghaye.RD2 = 0) ORDER BY Bas.Vaghaye.Row", objConnection)
            objDataset = New DataSet
            objDataAdapter2.Fill(objDataset, "Bas.Vaghaye")
            objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Row"
            If objDataview.Count = 0 Then
                Dim objDataAdapter3 As New SqlDataAdapter _
                    (" SELECT Bas.Vaghaye.Row, Bas.Vaghaye.u_code, Bas.UserList.U_Name, Bas.UserList.U_Family, Bas.Vaghaye.Dat" & _
                    " FROM Bas.Vaghaye INNER JOIN Bas.UserList ON Bas.Vaghaye.u_code = Bas.UserList.U_Code WHERE (Bas.Vaghaye.Alarm3 = '" & Dat & "') AND (Bas.Vaghaye.u_code = " & User_Code & ") AND (Bas.Vaghaye.RD3 = 0) ORDER BY Bas.Vaghaye.Row", objConnection)
                objDataset = New DataSet
                objDataAdapter3.Fill(objDataset, "Bas.Vaghaye")
                objDataview = New DataView(objDataset.Tables("Bas.Vaghaye"))
                objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
                objDataview.Sort = "Row"
            End If
        End If
    End Sub

    Private Sub FillDataGridView()
        If objDataview.Count > 0 Then
            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview
        Else
            Me.DataGridView1.AutoGenerateColumns = True
            objDataview.Dispose()
            Me.DataGridView1.DataSource = objDataview
        End If
        Me.DataGridView1.Columns(0).HeaderText = "ردیف"
        Me.DataGridView1.Columns(1).HeaderText = "کد پرسنلی"
        Me.DataGridView1.Columns(2).HeaderText = "نام"
        Me.DataGridView1.Columns(3).HeaderText = "نام خانوادگی"
        Me.DataGridView1.Columns(4).HeaderText = "تاریخ"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 70
        Me.DataGridView1.Columns(2).Width = 80
        Me.DataGridView1.Columns(3).Width = 120
        Me.DataGridView1.Columns(4).Width = 70
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Alarm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Vaghaye.ShowDialog()
        Vaghaye.Activate()
        Me.Dispose()
    End Sub
End Class