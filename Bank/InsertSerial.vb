
Public Class InsertSerial
    Private Sub RowSerial()
        Dim X As Integer
        Dim Rw As String
        Dim Sal As String

        Sal = Mainfrm.cboSal_Mali.Text
        For X = 0 To Ersal.txtCount.Text - 1
            Rw = X + 1
            If objDataview.Count <= 0 Then
                DataGridView1.Rows(X).Cells(1).Value = Rw
            Else
                DataGridView2.Rows(X).Cells(1).Value = Rw
            End If
        Next
    End Sub
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub InsertSerial_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Dim Sal As String
        Sal = Mainfrm.cboSal_Mali.Text

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT RowSer FROM Bnk.SerialErsalTmp WHERE (Row = " & Ersal.txtRow.Text & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.SerialErsalTmp")
        objDataview = New DataView(objDataset.Tables("Bnk.SerialErsalTmp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "RowSer"

        If objDataview.Count = 0 Then
            MsgbOK.Label1.Text = " لطفا سریال شماره برگه " & Ersal.txtRow.Text & " را وارد نمائید . "
            MsgbOK.ShowDialog()
            Exit Sub
        End If

        If objDataview.Count < Ersal.txtCount.Text Then
            MsgbOK.Label1.Text = " سریالهای وارد شده با تعداد سریال مغایرت دارد . "
            MsgbOK.ShowDialog()
            Exit Sub
        End If
    End Sub

    Private Sub InsertSerial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If objDataview.Count <= 0 Then
            DataGridView1.Visible = True
            DataGridView2.Visible = False

            ' DataGridView1.RowCount = Z
            DataGridView1.RowCount = Val(Ersal.txtCount.Text)
            DataGridView1.Columns(1).ReadOnly = True
        Else
            DataGridView1.Visible = False
            DataGridView2.Visible = True
            DataGridView2.Columns(1).ReadOnly = True
        End If
        RowSerial()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        Dim X As Integer
        Dim Sal As String
        Dim Ser As String
        Dim Rws As String

        X = DataGridView2.CurrentCell.RowIndex
        Ser = Me.DataGridView2.Rows(X).Cells(0).Value
        Rws = Me.DataGridView2.Rows(X).Cells(1).Value
        Sal = Me.DataGridView2.Rows(X).Cells(2).Value
        Shob = Me.DataGridView2.Rows(X).Cells(3).Value

        objCommand.Connection = objConnection
        objCommand.CommandText = _
           " UPDATE Bnk.SerialErsalTmp SET SerialNo = '" & Ser & "'" & _
           " WHERE (Row = " & Ersal.txtRow.Text & ") AND (RowSer = " & Rws & ") AND (Sal = " & Sal & ") AND (Shob_Code = " & Shob & ")"
        objCommand.CommandType = CommandType.Text
        'objCommand.Parameters.Clear()

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

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim Sal As String
        Dim X As Integer
        Dim Rws As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        X = DataGridView1.CurrentCell.RowIndex
        Rws = DataGridView1.Rows(X).Cells(1).Value

        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Row, RowSer FROM Bnk.SerialErsalTmp WHERE (Row = " & Ersal.txtRow.Text & ") AND (RowSer = " & Rws & ")", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "Bnk.SerialErsalTmp")
        objDataview = New DataView(objDataset.Tables("Bnk.SerialErsalTmp"))
        objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
        objDataview.Sort = "RowSer"

        If objDataview.Count = 0 Then
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " INSERT INTO Bnk.SerialErsalTmp (Row, RowSer, SerialNo, Sal, Shob_Code)" & _
               " VALUES (@rw, @Rws, @ser, @sal, @shob)"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@ser", DataGridView1.Rows(X).Cells(0).Value)
            objCommand.Parameters.AddWithValue("@rw", Ersal.txtRow.Text)
            objCommand.Parameters.AddWithValue("@Rws", DataGridView1.Rows(X).Cells(1).Value)
            objCommand.Parameters.AddWithValue("@sal", Sal)
            objCommand.Parameters.AddWithValue("@shob", Shob)
        Else
            objCommand.Connection = objConnection
            objCommand.CommandText = _
               " UPDATE Bnk.SerialErsalTmp SET SerialNo = @ser WHERE (Row = " & Ersal.txtRow.Text & ") AND (RowSer = " & Rws & ")"
            objCommand.CommandType = CommandType.Text
            objCommand.Parameters.Clear()
            objCommand.Parameters.AddWithValue("@ser", DataGridView1.Rows(X).Cells(0).Value)
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
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class