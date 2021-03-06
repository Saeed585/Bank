Public Class MergeProb
    Private Sub FillcboGProblem()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Code, Name FROM Bas.GProblem ORDER BY Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.GProblem")
        Me.cboGProblem.DataSource = objDataset.Tables("Bas.GProblem")
        Me.cboGProblem.DisplayMember = "Name"
        Me.cboGProblem.ValueMember = "Code"
    End Sub

    Public Sub FillDatasetAndDataview()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Code, Name FROM Bas.NProblem ORDER BY Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.NProblem")
        objDataview = New DataView(objDataset.Tables("Bas.NProblem"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Code"
    End Sub

    Private Sub FillDataGridView()
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = objDataview

        Me.DataGridView1.Columns(0).HeaderText = "کد"
        Me.DataGridView1.Columns(1).HeaderText = "نام اشکال"

        Me.DataGridView1.Columns(0).Width = 50
        Me.DataGridView1.Columns(1).Width = 300
    End Sub

    Private Sub InsertMergeSrv()
        Dim X As Integer
        Dim GProbCode As String
        Dim ProbCode As String

        X = DataGridView1.CurrentCell.RowIndex
        ProbCode = DataGridView1.Rows(X).Cells(0).Value
        GProbCode = cboGProblem.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Prob_code, GProb_Code FROM Bas.MergeProb WHERE (Prob_code = " & ProbCode & ") AND (Gprob_Code = " & GProbCode & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bas.MergeProb")
        objDataview = New DataView(objDataset.Tables("Bas.MergeProb"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Prob_code"

        If objDataview.Count = 0 Then
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO Bas.MergeProb (Prob_code, Gprob_Code)" & _
               " VALUES (@SrvCode, @GSrvCode)"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()

            objCommand.Parameters.AddWithValue("@SrvCode", ProbCode)
            objCommand.Parameters.AddWithValue("@GSrvCode", GProbCode)

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
    End Sub

    Public Sub FillDatasetAndDataview2()
        Dim GProbCode As String

        GProbCode = cboGProblem.SelectedValue.ToString

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Bas.MergeProb.Prob_code, Bas.NProblem.Name FROM Bas.MergeProb INNER JOIN Bas.NProblem ON Bas.MergeProb.Prob_code = Bas.NProblem.Code" & _
             " WHERE (Bas.MergeProb.GProb_Code = " & GProbCode & ") ORDER BY Bas.MergeProb.Prob_code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.MergeProb")
        objDataview = New DataView(objDataset.Tables("bas.MergeProb"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "Prob_code"
    End Sub

    Private Sub FillDataGridView2()
        Me.DataGridView2.AutoGenerateColumns = True
        Me.DataGridView2.DataSource = objDataview

        Me.DataGridView2.Columns(0).HeaderText = "کد"
        Me.DataGridView2.Columns(1).HeaderText = "نام اشکال"
     
        Me.DataGridView2.Columns(0).Width = 50
        Me.DataGridView2.Columns(1).Width = 300
    End Sub

    Private Sub DelMergeSrv()
        Dim X As Integer
        Dim GProbCode As String
        Dim ProbCode As String

        X = DataGridView2.CurrentCell.RowIndex
        ProbCode = DataGridView2.Rows(X).Cells(0).Value
        GProbCode = cboGProblem.SelectedValue.ToString

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " DELETE FROM Bas.MergeProb" & _
           " WHERE (Prob_code = " & ProbCode & ") AND (GProb_Code = " & GProbCode & ")"
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

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub MergeProb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FillcboGProblem()
        FillDatasetAndDataview()
        FillDataGridView()
        FillDatasetAndDataview2()
        FillDataGridView2()
    End Sub

    Private Sub MergeProb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        FormName = "MergeProb"
    End Sub

    Private Sub MergeProb_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel2.Width = Me.tsHeader.Width - 225
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub cboGProblem_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGProblem.SelectedValueChanged
        If Me.cboGProblem.SelectedValue.ToString = "System.Data.DataRowView" Then
            Exit Sub
        Else
            FillDatasetAndDataview2()
            FillDataGridView2()
        End If
    End Sub

    Private Sub btnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRef.Click
        FillcboGProblem()
        FillDatasetAndDataview()
        FillDataGridView()
        FillDatasetAndDataview2()
        FillDataGridView2()
    End Sub

    Private Sub DataGridView1_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseDoubleClick
        InsertMergeSrv()
        FillDatasetAndDataview2()
        FillDataGridView2()
    End Sub

    Private Sub DataGridView2_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseDoubleClick
        DelMergeSrv()
        FillDatasetAndDataview2()
        FillDataGridView2()
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        FillDatasetAndDataview()
        objDataview.RowFilter = "Name like '" & "**" & Me.txtSrch.Text & "**" & "'"
        FillDataGridView()
    End Sub

    Private Sub cboGProblem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGProblem.SelectedIndexChanged

    End Sub
End Class