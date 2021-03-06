Public Class IPPass
    Public Sub FillRow()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.ServerInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.ServerInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.ServerInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        ElseIf Flag = 2 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.RadioInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RadioInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.RadioInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        ElseIf Flag = 3 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.FirewallInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.FirewallInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.FirewallInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        ElseIf Flag = 4 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.RouterInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RouterInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.RouterInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        ElseIf Flag = 5 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.SwitchInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SwitchInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.SwitchInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        ElseIf Flag = 6 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.WirelessInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WirelessInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.WirelessInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        ElseIf Flag = 7 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.SoftWareInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SoftWareInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.SoftWareInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        ElseIf Flag = 8 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT MAX(Row) AS Expr1 FROM Bnk.WebServiceInf WHERE (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WebServiceInf")
            objDataviewT = New DataView(objDataset.Tables("Bnk.WebServiceInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
            objDataviewT.Sort = "Expr1"
        End If

        Label6.DataBindings.Clear()
        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
        If Label6.Text <> "" Then
            txtRow.Text = Val(Label6.Text) + 1
        ElseIf Label6.Text = "" Then
            txtRow.Text = 1
        End If
        Label6.DataBindings.Clear()
        Label6.Text = ""
    End Sub

    Public Sub FillCboShob()
        Dim objDataAdapter As New SqlDataAdapter _
            (" SELECT Shob_Code, Shob_Name FROM Bas.Shob WHERE(Shob_Code <> 0) ORDER BY Shob_Code", objConnection)
        objDataset = New DataSet
        objDataAdapter.Fill(objDataset, "bas.Shob")
        objDataview = New DataView(objDataset.Tables("bas.Shob"))
        cboShob.DataSource = objDataset.Tables("bas.Shob")
        cboShob.DisplayMember = "Shob_Name"
        cboShob.ValueMember = "Shob_Code"
    End Sub

    Private Sub FillDatasetAndDataview()
        Dim ChkAmn As String
        Dim Da As New SqlDataAdapter

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 34)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckServer.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_ServerInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_ServerInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_ServerInfTmp"))
            ' objDataview.Sort = "Row"
            objConnection.Close()
        ElseIf Flag = 2 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 35)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckRadio.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_RadioInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_RadioInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_RadioInfTmp"))
            objConnection.Close()
        ElseIf Flag = 3 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 36)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckFirewall.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_FirewallInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_FirewallInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_FirewallInfTmp"))
            objConnection.Close()
        ElseIf Flag = 4 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 37)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckRouter.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_RouterInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_RouterInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_RouterInfTmp"))
            objConnection.Close()
        ElseIf Flag = 5 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 38)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckSwitch.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_SwitchInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_SwitchInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_SwitchInfTmp"))
            objConnection.Close()
        ElseIf Flag = 6 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 39)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckWireless.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_WirelessInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_WirelessInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_WirelessInfTmp"))
            objConnection.Close()
        ElseIf Flag = 7 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 40)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckSoftware.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_SoftwareInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_SoftwareInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_SoftwareInfTmp"))
            objConnection.Close()
        ElseIf Flag = 8 Then
            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Rep.CreateTbl"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@RepNo", 41)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            If CheckWebService.Checked = True Then
                ChkAmn = 0
            Else
                ChkAmn = 1
            End If

            Try
                objConnection.Open()
                objCommand = New SqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsIPass"

                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@ChkAmn", ChkAmn)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            objConnection.Open()
            Da.SelectCommand = New SqlCommand
            Da.SelectCommand.Connection = objConnection
            Da.SelectCommand.CommandText = ("Select * From Bnk.View_WebServiceInfTmp ORDER BY Row")

            objDataset.Tables.Clear()
            Da.Fill(objDataset, "Bnk.View_WebServiceInfTmp")
            objDataview = New DataView(objDataset.Tables("Bnk.View_WebServiceInfTmp"))
            objConnection.Close()
        End If
    End Sub

    Private Sub FillDataGridView()
        Dim X As Integer

        If Flag = 1 Then ' Server
            Me.DataGridView1.AutoGenerateColumns = True
            Me.DataGridView1.DataSource = objDataview

            Me.DataGridView1.Columns(0).HeaderText = "Row"
            Me.DataGridView1.Columns(1).HeaderText = "Brand/Model"
            Me.DataGridView1.Columns(2).HeaderText = "Server Name"
            Me.DataGridView1.Columns(3).HeaderText = "IP Number1"
            Me.DataGridView1.Columns(4).HeaderText = "IP Number2"
            Me.DataGridView1.Columns(5).Visible = False ' SoftCode
            Me.DataGridView1.Columns(6).HeaderText = "Software"
            Me.DataGridView1.Columns(7).HeaderText = "SoftPass"
            Me.DataGridView1.Columns(8).HeaderText = "LocalPass"
            Me.DataGridView1.Columns(9).HeaderText = "User"
            Me.DataGridView1.Columns(10).HeaderText = "Password"
            Me.DataGridView1.Columns(11).HeaderText = "Dscr"
            Me.DataGridView1.Columns(12).Visible = False ' ChkAmn

            Me.DataGridView1.Columns(0).Width = 50
            Me.DataGridView1.Columns(2).Width = 120
            Me.DataGridView1.Columns(3).Width = 150
            Me.DataGridView1.Columns(4).Width = 150
            Me.DataGridView1.Columns(11).Width = 400

            For X = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(X).Cells(5).Value = 0 Then
                    DataGridView1.Rows(X).Cells(6).Value = ""
                ElseIf DataGridView1.Rows(X).Cells(5).Value = 1 Then
                    DataGridView1.Rows(X).Cells(6).Value = "Sql Server"
                ElseIf DataGridView1.Rows(X).Cells(5).Value = 2 Then
                    DataGridView1.Rows(X).Cells(6).Value = "AntiVirus"
                ElseIf DataGridView1.Rows(X).Cells(5).Value = 3 Then
                    DataGridView1.Rows(X).Cells(6).Value = "Luxriot"
                End If

                'Dim i As Integer = DataGridView1.Rows(X).Cells(10).Value
                'Dim s As String = Convert.ToString(i, 2).PadLeft(32, "0"c) '32 bits
                'DataGridView1.Rows(X).Cells(10).Value = s
            Next
        ElseIf Flag = 2 Then ' Radio
            Me.DataGridView2.AutoGenerateColumns = True
            Me.DataGridView2.DataSource = objDataview

            Me.DataGridView2.Columns(0).HeaderText = "Row"
            Me.DataGridView2.Columns(1).HeaderText = "Brand"
            Me.DataGridView2.Columns(2).HeaderText = "IP"
            Me.DataGridView2.Columns(3).HeaderText = "User"
            Me.DataGridView2.Columns(4).HeaderText = "Password"
            Me.DataGridView2.Columns(5).HeaderText = "Dscr"
            Me.DataGridView2.Columns(6).Visible = False ' ChkAmn

            Me.DataGridView2.Columns(0).Width = 50
            Me.DataGridView2.Columns(1).Width = 150
            Me.DataGridView2.Columns(2).Width = 130
            Me.DataGridView2.Columns(4).Width = 130
            Me.DataGridView2.Columns(5).Width = 800
        ElseIf Flag = 3 Then ' FireWall
            Me.DataGridView3.AutoGenerateColumns = True
            Me.DataGridView3.DataSource = objDataview

            Me.DataGridView3.Columns(0).HeaderText = "Row"
            Me.DataGridView3.Columns(1).Visible = False ' ChkBrand
            Me.DataGridView3.Columns(2).HeaderText = "Brand/Model"
            Me.DataGridView3.Columns(3).HeaderText = "Number Of Port"
            Me.DataGridView3.Columns(4).HeaderText = "IP Number"
            Me.DataGridView3.Columns(5).HeaderText = "User Admin"
            Me.DataGridView3.Columns(6).HeaderText = "Pass Admin"
            Me.DataGridView3.Columns(7).HeaderText = "User"
            Me.DataGridView3.Columns(8).HeaderText = "Password"
            Me.DataGridView3.Columns(9).Visible = False ' ChkPort
            Me.DataGridView3.Columns(10).Visible = False ' PortCode
            Me.DataGridView3.Columns(11).HeaderText = "PortNo"
            Me.DataGridView3.Columns(12).HeaderText = "Radio1 IP"
            Me.DataGridView3.Columns(13).HeaderText = "Radio2 IP"
            Me.DataGridView3.Columns(14).HeaderText = "Dscr"
            Me.DataGridView3.Columns(15).Visible = False ' ChkAmn

            Me.DataGridView3.Columns(0).Width = 50
            Me.DataGridView3.Columns(6).Width = 130
            Me.DataGridView3.Columns(8).Width = 130
            Me.DataGridView3.Columns(14).Width = 400

            For X = 0 To DataGridView3.RowCount - 1
                If DataGridView3.Rows(X).Cells(9).Value = 1 Then
                    If DataGridView3.Rows(X).Cells(10).Value = 0 Then
                        DataGridView3.Rows(X).Cells(11).Value = ""
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 1 Then
                        DataGridView3.Rows(X).Cells(11).Value = "Port1"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 2 Then
                        DataGridView3.Rows(X).Cells(11).Value = "Port2"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 3 Then
                        DataGridView3.Rows(X).Cells(11).Value = "Port3"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 4 Then
                        DataGridView3.Rows(X).Cells(11).Value = "Port4"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 5 Then
                        DataGridView3.Rows(X).Cells(11).Value = "Port5"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 6 Then
                        DataGridView3.Rows(X).Cells(11).Value = "Port6"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 7 Then
                        DataGridView3.Rows(X).Cells(11).Value = "Port7"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 8 Then
                        DataGridView3.Rows(X).Cells(11).Value = "DMZ"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 9 Then
                        DataGridView3.Rows(X).Cells(11).Value = "WAN1"
                    ElseIf DataGridView3.Rows(X).Cells(10).Value = 10 Then
                        DataGridView3.Rows(X).Cells(11).Value = "WAN2"
                    End If
                End If
            Next
        ElseIf Flag = 4 Then ' Router
            Me.DataGridView4.AutoGenerateColumns = True
            Me.DataGridView4.DataSource = objDataview

            Me.DataGridView4.Columns(0).HeaderText = "Row"
            Me.DataGridView4.Columns(1).Visible = False ' ChkBrand
            Me.DataGridView4.Columns(2).HeaderText = "Brand/Model"
            Me.DataGridView4.Columns(3).HeaderText = "Number Of Port"
            Me.DataGridView4.Columns(4).HeaderText = "IP Number"
            Me.DataGridView4.Columns(5).HeaderText = "User"
            Me.DataGridView4.Columns(6).HeaderText = "Password"
            Me.DataGridView4.Columns(7).Visible = False ' ChkPort
            Me.DataGridView4.Columns(8).Visible = False ' PortCode
            Me.DataGridView4.Columns(9).HeaderText = "PortNo"
            Me.DataGridView4.Columns(10).HeaderText = "Port IP"
            Me.DataGridView4.Columns(11).HeaderText = "Dscr"
            Me.DataGridView4.Columns(12).Visible = False ' ChkAmn

            Me.DataGridView4.Columns(0).Width = 50
            Me.DataGridView4.Columns(6).Width = 130
            Me.DataGridView4.Columns(11).Width = 700

            For X = 0 To DataGridView4.RowCount - 1
                If DataGridView4.Rows(X).Cells(7).Value = 1 Then
                    If DataGridView4.Rows(X).Cells(8).Value = 0 Then
                        DataGridView4.Rows(X).Cells(9).Value = ""
                    ElseIf DataGridView4.Rows(X).Cells(8).Value = 1 Then
                        DataGridView4.Rows(X).Cells(9).Value = "Port1"
                    ElseIf DataGridView4.Rows(X).Cells(8).Value = 2 Then
                        DataGridView4.Rows(X).Cells(9).Value = "Port2"
                    ElseIf DataGridView4.Rows(X).Cells(8).Value = 3 Then
                        DataGridView4.Rows(X).Cells(9).Value = "Port3"
                    ElseIf DataGridView4.Rows(X).Cells(8).Value = 4 Then
                        DataGridView4.Rows(X).Cells(9).Value = "Port4"
                    ElseIf DataGridView4.Rows(X).Cells(8).Value = 5 Then
                        DataGridView4.Rows(X).Cells(9).Value = "Port5"
                    End If
                End If
            Next
        ElseIf Flag = 5 Then ' Switch
            Me.DataGridView5.AutoGenerateColumns = True
            Me.DataGridView5.DataSource = objDataview

            Me.DataGridView5.Columns(0).HeaderText = "Row"
            Me.DataGridView5.Columns(1).HeaderText = "Brand/Model"
            Me.DataGridView5.Columns(2).HeaderText = "Number Of Port"
            Me.DataGridView5.Columns(3).HeaderText = "IP"
            Me.DataGridView5.Columns(4).HeaderText = "User"
            Me.DataGridView5.Columns(5).HeaderText = "Password"
            Me.DataGridView5.Columns(6).HeaderText = "Host Name"
            Me.DataGridView5.Columns(7).HeaderText = "Dscr"
            Me.DataGridView5.Columns(8).Visible = False ' ChkAmn

            Me.DataGridView5.Columns(0).Width = 50
            Me.DataGridView5.Columns(3).Width = 130
            Me.DataGridView5.Columns(5).Width = 130
            Me.DataGridView5.Columns(6).Width = 120
            Me.DataGridView5.Columns(7).Width = 800

        ElseIf Flag = 6 Then ' Wireless
            Me.DataGridView6.AutoGenerateColumns = True
            Me.DataGridView6.DataSource = objDataview

            Me.DataGridView6.Columns(0).HeaderText = "Row"
            Me.DataGridView6.Columns(1).HeaderText = "Brand/Model"
            Me.DataGridView6.Columns(2).HeaderText = "IP LAN"
            Me.DataGridView6.Columns(3).HeaderText = "IP WAN"
            Me.DataGridView6.Columns(4).HeaderText = "User"
            Me.DataGridView6.Columns(5).HeaderText = "Password"
            Me.DataGridView6.Columns(6).HeaderText = "SSID"
            Me.DataGridView6.Columns(7).HeaderText = "SSID Pass"
            Me.DataGridView6.Columns(8).HeaderText = "Dscr"
            Me.DataGridView6.Columns(9).Visible = False ' ChkAmn

            Me.DataGridView6.Columns(0).Width = 50
            Me.DataGridView6.Columns(1).Width = 120
            Me.DataGridView6.Columns(7).Width = 150
            Me.DataGridView6.Columns(8).Width = 690
        ElseIf Flag = 7 Or Flag = 8 Then ' SoftWare & WebService
            Me.DataGridView7.AutoGenerateColumns = True
            Me.DataGridView7.DataSource = objDataview

            Me.DataGridView7.Columns(0).HeaderText = "Row"
            Me.DataGridView7.Columns(1).HeaderText = "Name"
            Me.DataGridView7.Columns(2).HeaderText = "IP"
            Me.DataGridView7.Columns(3).HeaderText = "User"
            Me.DataGridView7.Columns(4).HeaderText = "Password"
            Me.DataGridView7.Columns(5).HeaderText = "Dscr"
            Me.DataGridView7.Columns(6).Visible = False ' ChkAmn

            Me.DataGridView7.Columns(0).Width = 50
            Me.DataGridView7.Columns(1).Width = 150
            Me.DataGridView7.Columns(2).Width = 130
            Me.DataGridView7.Columns(4).Width = 130
            Me.DataGridView7.Columns(5).Width = 800
        End If
    End Sub

    Private Sub DGVMove()
        Dim X As Integer

        If Flag = 1 Then
            X = DataGridView1.CurrentCell.RowIndex
            txtRow.Text = DataGridView1.Rows(X).Cells(0).Value
            txtBrandSrv.Text = DataGridView1.Rows(X).Cells(1).Value
            txtSrvName.Text = DataGridView1.Rows(X).Cells(2).Value
            txtIP1.Text = DataGridView1.Rows(X).Cells(3).Value
            txtIP2.Text = DataGridView1.Rows(X).Cells(4).Value
            cboSoft.SelectedIndex = DataGridView1.Rows(X).Cells(5).Value
            txtSoftPass.Text = DataGridView1.Rows(X).Cells(7).Value
            txtLocalPass.Text = DataGridView1.Rows(X).Cells(8).Value
            txtUserSrv.Text = DataGridView1.Rows(X).Cells(9).Value
            txtPassSrv.Text = DataGridView1.Rows(X).Cells(10).Value
            txtDscrSrv.Text = DataGridView1.Rows(X).Cells(11).Value
            CheckBox6.CheckState = DataGridView1.Rows(X).Cells(12).Value
        ElseIf flag = 2 Then
            X = DataGridView2.CurrentCell.RowIndex
            txtRow.Text = DataGridView2.Rows(X).Cells(0).Value
            txtBrandRad.Text = DataGridView2.Rows(X).Cells(1).Value
            txtIPRad.Text = DataGridView2.Rows(X).Cells(2).Value
            txtUserRad.Text = DataGridView2.Rows(X).Cells(3).Value
            txtPassRad.Text = DataGridView2.Rows(X).Cells(4).Value
            txtDscrRad.Text = DataGridView2.Rows(X).Cells(5).Value
            CheckBox6.CheckState = DataGridView2.Rows(X).Cells(6).Value
        ElseIf flag = 3 Then
            X = DataGridView3.CurrentCell.RowIndex
            txtRow.Text = DataGridView3.Rows(X).Cells(0).Value
            CheckBox1.CheckState = DataGridView3.Rows(X).Cells(1).Value
            txtBrandFire.Text = DataGridView3.Rows(X).Cells(2).Value
            txtPortNo.Text = DataGridView3.Rows(X).Cells(3).Value
            txtIPFire.Text = DataGridView3.Rows(X).Cells(4).Value
            txtUser1.Text = DataGridView3.Rows(X).Cells(5).Value
            txtPass1.Text = DataGridView3.Rows(X).Cells(6).Value
            txtUser2.Text = DataGridView3.Rows(X).Cells(7).Value
            txtPass2.Text = DataGridView3.Rows(X).Cells(8).Value
            CheckBox2.CheckState = DataGridView3.Rows(X).Cells(9).Value
            cboPortFire.SelectedIndex = DataGridView3.Rows(X).Cells(10).Value
            txtIPRadio1.Text = DataGridView3.Rows(X).Cells(12).Value
            txtIPRadio2.Text = DataGridView3.Rows(X).Cells(13).Value
            txtDscrFire.Text = DataGridView3.Rows(X).Cells(14).Value
            CheckBox6.CheckState = DataGridView3.Rows(X).Cells(15).Value
        ElseIf flag = 4 Then
            X = DataGridView4.CurrentCell.RowIndex
            txtRow.Text = DataGridView4.Rows(X).Cells(0).Value
            CheckBox3.CheckState = DataGridView4.Rows(X).Cells(1).Value
            txtBrandRout.Text = DataGridView4.Rows(X).Cells(2).Value
            txtPortNoRout.Text = DataGridView4.Rows(X).Cells(3).Value
            txtIPRout.Text = DataGridView4.Rows(X).Cells(4).Value
            txtUserRout.Text = DataGridView4.Rows(X).Cells(5).Value
            txtPassRout.Text = DataGridView4.Rows(X).Cells(6).Value
            CheckBox4.CheckState = DataGridView4.Rows(X).Cells(7).Value
            cboPortRout.SelectedIndex = DataGridView4.Rows(X).Cells(8).Value
            txtIPPortRout.Text = DataGridView4.Rows(X).Cells(10).Value
            txtDscrRout.Text = DataGridView4.Rows(X).Cells(11).Value
            CheckBox6.CheckState = DataGridView4.Rows(X).Cells(12).Value
        ElseIf flag = 5 Then
            X = DataGridView5.CurrentCell.RowIndex
            txtRow.Text = DataGridView5.Rows(X).Cells(0).Value
            txtBrandSw.Text = DataGridView5.Rows(X).Cells(1).Value
            txtPortNoSw.Text = DataGridView5.Rows(X).Cells(2).Value
            txtIPSw.Text = DataGridView5.Rows(X).Cells(3).Value
            txtUserSw.Text = DataGridView5.Rows(X).Cells(4).Value
            txtPassSw.Text = DataGridView5.Rows(X).Cells(5).Value
            txtHostName.Text = DataGridView5.Rows(X).Cells(6).Value
            txtDscrSw.Text = DataGridView5.Rows(X).Cells(7).Value
            CheckBox6.CheckState = DataGridView5.Rows(X).Cells(8).Value
        ElseIf flag = 6 Then
            X = DataGridView6.CurrentCell.RowIndex
            txtRow.Text = DataGridView6.Rows(X).Cells(0).Value
            txtBrandWire.Text = DataGridView6.Rows(X).Cells(1).Value
            txtIPLan.Text = DataGridView6.Rows(X).Cells(2).Value
            txtIPWan.Text = DataGridView6.Rows(X).Cells(3).Value
            txtUserWire.Text = DataGridView6.Rows(X).Cells(4).Value
            txtPassWire.Text = DataGridView6.Rows(X).Cells(5).Value
            txtSSID.Text = DataGridView6.Rows(X).Cells(6).Value
            txtPassSSID.Text = DataGridView6.Rows(X).Cells(7).Value
            txtDscrWire.Text = DataGridView6.Rows(X).Cells(8).Value
            CheckBox6.CheckState = DataGridView6.Rows(X).Cells(9).Value
        ElseIf flag = 7 Then
            X = DataGridView7.CurrentCell.RowIndex
            txtRow.Text = DataGridView7.Rows(X).Cells(0).Value
            txtNameSoft.Text = DataGridView7.Rows(X).Cells(1).Value
            txtIPSoft.Text = DataGridView7.Rows(X).Cells(2).Value
            txtUserSoft.Text = DataGridView7.Rows(X).Cells(3).Value
            txtPassSoft.Text = DataGridView7.Rows(X).Cells(4).Value
            txtDscrSoft.Text = DataGridView7.Rows(X).Cells(5).Value
            CheckBox6.CheckState = DataGridView7.Rows(X).Cells(6).Value
        ElseIf flag = 8 Then
            X = DataGridView7.CurrentCell.RowIndex
            txtRow.Text = DataGridView7.Rows(X).Cells(0).Value
            txtNameWeb.Text = DataGridView7.Rows(X).Cells(1).Value
            txtIPWeb.Text = DataGridView7.Rows(X).Cells(2).Value
            txtUserWeb.Text = DataGridView7.Rows(X).Cells(3).Value
            txtPassWeb.Text = DataGridView7.Rows(X).Cells(4).Value
            txtDscrWeb.Text = DataGridView7.Rows(X).Cells(5).Value
            CheckBox6.CheckState = DataGridView7.Rows(X).Cells(6).Value
        End If
    End Sub

    Private Sub Clear()
        If Flag = 1 Then
            txtBrandSrv.Text = ""
            txtSrvName.Text = ""
            txtIP1.Text = ""
            txtIP2.Text = ""
            cboSoft.SelectedIndex = 0
            txtLocalPass.Text = ""
            txtUserSrv.Text = ""
            txtPassSrv.Text = ""
            txtDscrSrv.Text = ""
            txtBrandSrv.Focus()
        ElseIf flag = 2 Then
            txtBrandRad.Text = ""
            txtIPRad.Text = ""
            txtUserRad.Text = ""
            txtPassRad.Text = ""
            txtDscrRad.Text = ""
            txtBrandRad.Focus()
        ElseIf flag = 3 Then
            CheckBox1.Checked = False
            txtBrandFire.Text = ""
            txtPortNo.Text = 0
            txtIPFire.Text = ""
            txtUser1.Text = ""
            txtPass1.Text = ""
            txtUser2.Text = ""
            txtPass2.Text = ""
            CheckBox2.Checked = False
            cboPortFire.SelectedIndex = 0
            txtIPRadio1.Text = ""
            txtIPRadio2.Text = ""
            txtDscrFire.Text = ""
            txtBrandFire.Focus()
        ElseIf flag = 4 Then
            CheckBox3.Checked = False
            txtBrandRout.Text = ""
            txtPortNoRout.Text = 0
            txtIPRout.Text = ""
            txtUserRout.Text = ""
            txtPassRout.Text = ""
            CheckBox4.Checked = False
            cboPortRout.SelectedIndex = 0
            txtIPPortRout.Text = ""
            txtDscrRout.Text = ""
            txtBrandRout.Focus()
        ElseIf flag = 5 Then
            txtBrandSw.Text = ""
            txtPortNoSw.Text = 0
            txtIPSw.Text = ""
            txtUserSw.Text = ""
            txtPassSw.Text = ""
            txtHostName.Text = ""
            txtDscrSw.Text = ""
            txtBrandSw.Focus()
        ElseIf flag = 6 Then
            txtBrandWire.Text = ""
            txtIPLan.Text = ""
            txtIPWan.Text = ""
            txtUserWire.Text = ""
            txtPassWire.Text = ""
            txtSSID.Text = ""
            txtPassSSID.Text = ""
            txtDscrWire.Text = ""
            txtBrandWire.Focus()
        ElseIf flag = 7 Then
            txtNameSoft.Text = ""
            txtIPSoft.Text = ""
            txtUserSoft.Text = ""
            txtPassSoft.Text = ""
            txtDscrSoft.Text = ""
            txtNameSoft.Focus()
        ElseIf flag = 8 Then
            txtNameWeb.Text = ""
            txtIPWeb.Text = ""
            txtUserWeb.Text = ""
            txtPassWeb.Text = ""
            txtDscrWeb.Text = ""
            txtNameWeb.Focus()
        End If
        CheckBox6.Checked = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub InsertUpdate()
        'Dim A As Integer ' Convert String to Ascii
        'A = Asc(txtCode.Text)
        'MsgBox(A)

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdServerInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@Brand", txtBrandSrv.Text)
                objCommand.Parameters.AddWithValue("@SrvName", txtSrvName.Text)
                objCommand.Parameters.AddWithValue("@IP1", txtIP1.Text)
                objCommand.Parameters.AddWithValue("@IP2", txtIP2.Text)
                objCommand.Parameters.AddWithValue("@SoftCode", cboSoft.SelectedIndex)
                objCommand.Parameters.AddWithValue("@SoftPass", 0)
                objCommand.Parameters.AddWithValue("@LocalPass", 0)
                objCommand.Parameters.AddWithValue("@Usr", txtUserSrv.Text)
                objCommand.Parameters.AddWithValue("@Pass", 0)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrSrv.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", txtSoftPass.Text)
                objCommand.Parameters.AddWithValue("@LocalPass", txtLocalPass.Text)
                objCommand.Parameters.AddWithValue("@PassSrv", txtPassSrv.Text)
                objCommand.Parameters.AddWithValue("@Pass", "")
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf Flag = 2 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdRadioInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@Brand", txtBrandRad.Text)
                objCommand.Parameters.AddWithValue("@IP", txtIPRad.Text)
                objCommand.Parameters.AddWithValue("@Usr", txtUserRad.Text)
                objCommand.Parameters.AddWithValue("@Pass", 0)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrRad.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", "")
                objCommand.Parameters.AddWithValue("@LocalPass", "")
                objCommand.Parameters.AddWithValue("@PassSrv", "")
                objCommand.Parameters.AddWithValue("@Pass", txtPassRad.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf Flag = 3 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdFirewallInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@ChkBrand", CheckBox1.CheckState)
                objCommand.Parameters.AddWithValue("@Brand", txtBrandFire.Text)
                objCommand.Parameters.AddWithValue("@NumberP", txtPortNo.Text)
                objCommand.Parameters.AddWithValue("@IP", txtIPFire.Text)
                objCommand.Parameters.AddWithValue("@User1", txtUser1.Text)
                objCommand.Parameters.AddWithValue("@Pass1", 0)
                objCommand.Parameters.AddWithValue("@User2", txtUser2.Text)
                objCommand.Parameters.AddWithValue("@Pass2", 0)
                objCommand.Parameters.AddWithValue("@ChkPort", CheckBox2.CheckState)
                objCommand.Parameters.AddWithValue("@PortCode", cboPortFire.SelectedIndex)
                objCommand.Parameters.AddWithValue("@Radio1IP", txtIPRadio1.Text)
                objCommand.Parameters.AddWithValue("@Radio2IP", txtIPRadio2.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrFire.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", txtPass1.Text)
                objCommand.Parameters.AddWithValue("@LocalPass", "")
                objCommand.Parameters.AddWithValue("@PassSrv", "")
                objCommand.Parameters.AddWithValue("@Pass", txtPass2.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf Flag = 4 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdRouterInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@ChkBrand", CheckBox3.CheckState)
                objCommand.Parameters.AddWithValue("@Brand", txtBrandRout.Text)
                objCommand.Parameters.AddWithValue("@NumberP", txtPortNoRout.Text)
                objCommand.Parameters.AddWithValue("@IP", txtIPRout.Text)
                objCommand.Parameters.AddWithValue("@Usr", txtUserRout.Text)
                objCommand.Parameters.AddWithValue("@Pass", 0)
                objCommand.Parameters.AddWithValue("@ChkPort", CheckBox4.CheckState)
                objCommand.Parameters.AddWithValue("@PortCode", cboPortRout.SelectedIndex)
                objCommand.Parameters.AddWithValue("@PortIP", txtIPPortRout.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrRout.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", "")
                objCommand.Parameters.AddWithValue("@LocalPass", "")
                objCommand.Parameters.AddWithValue("@PassSrv", "")
                objCommand.Parameters.AddWithValue("@Pass", txtPassRout.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf Flag = 5 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdSwitchInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@Brand", txtBrandSw.Text)
                objCommand.Parameters.AddWithValue("@NumberP", txtPortNoSw.Text)
                objCommand.Parameters.AddWithValue("@IP", txtIPSw.Text)
                objCommand.Parameters.AddWithValue("@Usr", txtUserSw.Text)
                objCommand.Parameters.AddWithValue("@Pass", 0)
                objCommand.Parameters.AddWithValue("@Hostname", txtHostName.Text)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrSw.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", "")
                objCommand.Parameters.AddWithValue("@LocalPass", "")
                objCommand.Parameters.AddWithValue("@PassSrv", "")
                objCommand.Parameters.AddWithValue("@Pass", txtPassSw.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf Flag = 6 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdWirelessInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@Brand", txtBrandWire.Text)
                objCommand.Parameters.AddWithValue("@LanIP", txtIPLan.Text)
                objCommand.Parameters.AddWithValue("@WanIP", txtIPWan.Text)
                objCommand.Parameters.AddWithValue("@Usr", txtUserWire.Text)
                objCommand.Parameters.AddWithValue("@Pass", 0)
                objCommand.Parameters.AddWithValue("@SSID", txtSSID.Text)
                objCommand.Parameters.AddWithValue("@SSIDPass", 0)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrWire.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", txtPassWire.Text)
                objCommand.Parameters.AddWithValue("@LocalPass", "")
                objCommand.Parameters.AddWithValue("@PassSrv", "")
                objCommand.Parameters.AddWithValue("@Pass", txtPassSSID.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf Flag = 7 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdSoftWareInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@Name", txtNameSoft.Text)
                objCommand.Parameters.AddWithValue("@IP", txtIPSoft.Text)
                objCommand.Parameters.AddWithValue("@Usr", txtUserSoft.Text)
                objCommand.Parameters.AddWithValue("@Pass", 0)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrSoft.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", "")
                objCommand.Parameters.AddWithValue("@LocalPass", "")
                objCommand.Parameters.AddWithValue("@PassSrv", "")
                objCommand.Parameters.AddWithValue("@Pass", txtPassSoft.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        ElseIf Flag = 8 Then
            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.InsUpdWebServiceInf"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Btn", Btn)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@Name", txtNameWeb.Text)
                objCommand.Parameters.AddWithValue("@IP", txtIPWeb.Text)
                objCommand.Parameters.AddWithValue("@Usr", txtUserWeb.Text)
                objCommand.Parameters.AddWithValue("@Pass", 0)
                objCommand.Parameters.AddWithValue("@Dscr", txtDscrWeb.Text)
                objCommand.Parameters.AddWithValue("@ChkAmn", CheckBox6.CheckState)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()

            Try
                objConnection.Open()
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "Bnk.SavePass"
                objCommand.Parameters.Clear()
                objCommand.Parameters.AddWithValue("@Flg", Flag)
                objCommand.Parameters.AddWithValue("@Row", txtRow.Text)
                objCommand.Parameters.AddWithValue("@SoftPass", "")
                objCommand.Parameters.AddWithValue("@LocalPass", "")
                objCommand.Parameters.AddWithValue("@PassSrv", "")
                objCommand.Parameters.AddWithValue("@Pass", txtPassWeb.Text)
                objCommand.Parameters.AddWithValue("@Shob", Shob)

                objCommand.ExecuteNonQuery()
            Catch Ex As Exception
                MessageBox.Show(Ex.Message)
                objConnection.Close()
                Exit Sub
            End Try
            objConnection.Close()
        End If
    End Sub

    Private Sub Sav()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            If Me.txtBrandSrv.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند سرور را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandSrv.Focus()
                Exit Sub
            End If
        ElseIf Flag = 2 Then
            If Me.txtBrandRad.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند رادیو را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandRad.Focus()
                Exit Sub
            End If
        ElseIf Flag = 3 Then
            If Me.CheckBox1.Checked = True Then
                If Me.txtBrandFire.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا برند فایروال را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtBrandFire.Focus()
                    Exit Sub
                End If
            ElseIf Me.CheckBox2.Checked = True Then
                If Me.txtIPRadio1.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا آی پی پورت را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtIPRadio1.Focus()
                    Exit Sub
                End If
            End If
        ElseIf Flag = 4 Then
            If Me.CheckBox3.Checked = True Then
                If Me.txtBrandRout.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا برند روتر را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtBrandRout.Focus()
                    Exit Sub
                End If
            ElseIf Me.CheckBox4.Checked = True Then
                If Me.txtIPPortRout.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا آی پی پورت را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtIPPortRout.Focus()
                    Exit Sub
                End If
            End If
        ElseIf Flag = 5 Then
            If Me.txtBrandSw.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند سوئیچ را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandSw.Focus()
                Exit Sub
            End If
        ElseIf Flag = 6 Then
            If Me.txtBrandWire.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند وایرلس را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandWire.Focus()
                Exit Sub
            End If
        ElseIf Flag = 7 Then
            If Me.txtNameSoft.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام نرم افزار را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtNameSoft.Focus()
                Exit Sub
            End If
        ElseIf Flag = 8 Then
            If Me.txtNameWeb.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام وب سرویس را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtNameWeb.Focus()
                Exit Sub
            End If
        End If

        If Flag = 1 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.ServerInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.ServerInf")
            objDataview = New DataView(objDataset.Tables("Bnk.ServerInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        ElseIf Flag = 2 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.RadioInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RadioInf")
            objDataview = New DataView(objDataset.Tables("Bnk.RadioInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        ElseIf Flag = 3 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.FirewallInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.FirewallInf")
            objDataview = New DataView(objDataset.Tables("Bnk.FirewallInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        ElseIf Flag = 4 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.RouterInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RouterInf")
            objDataview = New DataView(objDataset.Tables("Bnk.RouterInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        ElseIf Flag = 5 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.SwitchInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SwitchInf")
            objDataview = New DataView(objDataset.Tables("Bnk.SwitchInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        ElseIf Flag = 6 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.WirelessInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WirelessInf")
            objDataview = New DataView(objDataset.Tables("Bnk.WirelessInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        ElseIf Flag = 7 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.SoftWareInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SoftWareInf")
            objDataview = New DataView(objDataset.Tables("Bnk.SoftWareInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        ElseIf Flag = 8 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.WebServiceInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WebServiceInf")
            objDataview = New DataView(objDataset.Tables("Bnk.WebServiceInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count > 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت شده است . "
                MsgbOK.ShowDialog()
                btnEdit.Focus()
                Exit Sub
            End If
        End If

        FillRow()
        Btn = 1
        InsertUpdate()

        Ref.PerformClick()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Sav()
    End Sub

    Private Sub IPPass_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Flag = 1
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        FillCboShob()
        DataGridBring()
        Clear()
        Panel1.BringToFront()
        CheckServer.Checked = True
        lblName.Text = "Server Panel"
    End Sub

    Private Sub IPPass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Then
            If btnSave.Enabled = True Then
                Sav()
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            If btnEdit.Enabled = True Then
                Edt()
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            If btnDelete.Enabled = True Then
                Del()
            End If
        ElseIf e.KeyCode = Keys.Insert Then
            FillRow()
            Clear()
        End If
    End Sub

    Private Sub DataGridBring()
        If Flag = 1 Then
            DataGridView1.BringToFront()
        ElseIf Flag = 2 Then
            DataGridView2.BringToFront()
        ElseIf Flag = 3 Then
            DataGridView3.BringToFront()
        ElseIf Flag = 4 Then
            DataGridView4.BringToFront()
        ElseIf Flag = 5 Then
            DataGridView5.BringToFront()
        ElseIf Flag = 6 Then
            DataGridView6.BringToFront()
        ElseIf Flag = 7 Or Flag = 8 Then
            DataGridView7.BringToFront()
        End If
    End Sub

    Private Sub IPPass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = My.Computer.Screen.WorkingArea.Width - 20
        Me.Height = My.Computer.Screen.WorkingArea.Height - 120
        Me.tsHeader.Cursor = Cursors.Hand
        Me.MdiParent = Mainfrm

        cboSoft.SelectedIndex = 0
        cboPortFire.SelectedIndex = 0
        Panel1.BringToFront()
        DataGridView1.BringToFront()

        Me.KeyPreview = True
    End Sub

    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub IPPass_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ToolStripLabel4.Width = Me.tsHeader.Width - 245
    End Sub

    Private Sub cboSoft_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSoft.SelectedIndexChanged
        txtSoftPass.Text = "0"
        If cboSoft.SelectedIndex = 0 Then
            txtSoftPass.ReadOnly = True
        Else
            txtSoftPass.ReadOnly = False
            txtSoftPass.Focus()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            txtBrandFire.Focus()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
            cboPortFire.Focus()
        End If
    End Sub

    Private Sub txtBrandSrv_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBrandSrv.GotFocus

    End Sub

    Private Sub txtBrandSrv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrandSrv.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtSrvName.Focus()
        End If
    End Sub

    Private Sub txtBrandSrv_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandSrv.TextChanged

    End Sub

    Private Sub txtSrvName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSrvName.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIP1.Focus()
        End If
    End Sub

    Private Sub txtSrvName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrvName.TextChanged

    End Sub

    Private Sub txtIP1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIP1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIP2.Focus()
        End If
    End Sub

    Private Sub txtIP1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIP1.TextChanged

    End Sub

    Private Sub txtIP2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIP2.KeyPress
        If e.KeyChar = ChrW(13) Then
            cboSoft.Focus()
        End If
    End Sub

    Private Sub txtIP2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIP2.TextChanged

    End Sub

    Private Sub txtSoftPass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSoftPass.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtLocalPass.Focus()
        End If
    End Sub

    Private Sub txtSoftPass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSoftPass.TextChanged

    End Sub

    Private Sub txtLocalPass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLocalPass.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUserSrv.Focus()
        End If
    End Sub

    Private Sub txtLocalPass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocalPass.TextChanged

    End Sub

    Private Sub txtUserSrv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserSrv.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassSrv.Focus()
        End If
    End Sub

    Private Sub txtUserSrv_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserSrv.TextChanged

    End Sub

    Private Sub txtPassSrv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassSrv.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrSrv.Focus()
        End If
    End Sub

    Private Sub txtPassSrv_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassSrv.TextChanged

    End Sub

    Private Sub txtDscrSrv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrSrv.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrSrv_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrSrv.TextChanged

    End Sub

    Private Sub txtBrandFire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrandFire.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPortNo.Focus()
        End If
    End Sub

    Private Sub txtBrandFire_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandFire.TextChanged

    End Sub

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNo.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPFire.Focus()
        End If
    End Sub

    Private Sub txtPortNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPortNo.TextChanged
        If txtPortNo.Text = "" Then
            txtPortNo.Text = 0
        End If
    End Sub

    Private Sub txtIPFire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPFire.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUser1.Focus()
        End If
    End Sub

    Private Sub txtIPFire_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPFire.TextChanged

    End Sub

    Private Sub txtUser1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUser1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPass1.Focus()
        End If
    End Sub

    Private Sub txtUser1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUser1.TextChanged

    End Sub

    Private Sub txtPass1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPass1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUser2.Focus()
        End If
    End Sub

    Private Sub txtPass1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPass1.TextChanged

    End Sub

    Private Sub txtUser2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUser2.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPass2.Focus()
        End If
    End Sub

    Private Sub txtUser2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUser2.TextChanged

    End Sub

    Private Sub txtPass2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPass2.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrFire.Focus()
        End If
    End Sub

    Private Sub txtPass2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPass2.TextChanged

    End Sub

    Private Sub txtDscrFire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrFire.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrFire_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrFire.TextChanged

    End Sub

    Private Sub cboPortFire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboPortFire.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPRadio1.Focus()
        End If
    End Sub

    Private Sub cboPortFire_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPortFire.SelectedIndexChanged

    End Sub

    Private Sub txtIPRadio1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPRadio1.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPRadio2.Focus()
        End If
    End Sub

    Private Sub txtIPRadio1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPRadio1.TextChanged

    End Sub

    Private Sub txtIPRadio2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPRadio2.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrFire.Focus()
        End If
    End Sub

    Private Sub txtIPRadio2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPRadio2.TextChanged

    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox4.Checked = False
            txtBrandRout.Focus()
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            CheckBox3.Checked = False
            cboPortRout.Focus()
        End If
    End Sub

    Private Sub txtBrandRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrandRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPortNoRout.Focus()
        End If
    End Sub

    Private Sub txtBrandRout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandRout.TextChanged

    End Sub

    Private Sub txtPortNoRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNoRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPRout.Focus()
        End If
    End Sub

    Private Sub txtPortNoRout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPortNoRout.TextChanged
        If txtPortNoRout.Text = "" Then
            txtPortNoRout.Text = 0
        End If
    End Sub

    Private Sub txtIPRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUserRout.Focus()
        End If
    End Sub

    Private Sub txtIPRout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPRout.TextChanged

    End Sub

    Private Sub txtUserRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassRout.Focus()
        End If
    End Sub

    Private Sub txtUserRout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserRout.TextChanged

    End Sub

    Private Sub txtPassRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrRout.Focus()
        End If
    End Sub

    Private Sub txtPassRout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassRout.TextChanged

    End Sub

    Private Sub txtDscrRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrRout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrRout.TextChanged

    End Sub

    Private Sub cboPortRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboPortRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPPortRout.Focus()
        End If
    End Sub

    Private Sub cboPortRout_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPortRout.SelectedIndexChanged

    End Sub

    Private Sub txtIPPortRout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPPortRout.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrRout.Focus()
        End If
    End Sub

    Private Sub txtIPPortRout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPPortRout.TextChanged

    End Sub

    Private Sub txtBrandSw_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrandSw.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPortNoSw.Focus()
        End If
    End Sub

    Private Sub txtBrandSw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandSw.TextChanged

    End Sub

    Private Sub txtPortNoSw_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNoSw.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPSw.Focus()
        End If
    End Sub

    Private Sub txtPortNoSw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPortNoSw.TextChanged
        If txtPortNoSw.Text = "" Then
            txtPortNoSw.Text = 0
        End If
    End Sub

    Private Sub txtIPSw_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPSw.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUserSw.Focus()
        End If
    End Sub

    Private Sub txtIPSw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPSw.TextChanged

    End Sub

    Private Sub txtUserSw_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserSw.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassSw.Focus()
        End If
    End Sub

    Private Sub txtUserSw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserSw.TextChanged

    End Sub

    Private Sub txtPassSw_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassSw.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtHostName.Focus()
        End If
    End Sub

    Private Sub txtPassSw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassSw.TextChanged

    End Sub

    Private Sub txtDscrSw_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrSw.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrSw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrSw.TextChanged

    End Sub

    Private Sub txtBrandWire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrandWire.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPLan.Focus()
        End If
    End Sub

    Private Sub txtBrandWire_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandWire.TextChanged

    End Sub

    Private Sub txtIPLan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPLan.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPWan.Focus()
        End If
    End Sub

    Private Sub txtIPLan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPLan.TextChanged

    End Sub

    Private Sub txtIPWan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPWan.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUserWire.Focus()
        End If
    End Sub

    Private Sub txtIPWan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPWan.TextChanged

    End Sub

    Private Sub txtUserWire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserWire.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassWire.Focus()
        End If
    End Sub

    Private Sub txtUserWire_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserWire.TextChanged

    End Sub

    Private Sub txtPassWire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassWire.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtSSID.Focus()
        End If
    End Sub

    Private Sub txtPassWire_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassWire.TextChanged

    End Sub

    Private Sub txtSSID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSSID.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassSSID.Focus()
        End If
    End Sub

    Private Sub txtSSID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSSID.TextChanged

    End Sub

    Private Sub txtPassSSID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassSSID.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrWire.Focus()
        End If
    End Sub

    Private Sub txtPassSSID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassSSID.TextChanged

    End Sub

    Private Sub txtDscrWire_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrWire.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrWire_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrWire.TextChanged

    End Sub

    Private Sub txtNameSoft_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameSoft.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPSoft.Focus()
        End If
    End Sub

    Private Sub txtNameSoft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameSoft.TextChanged

    End Sub

    Private Sub txtIPSoft_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPSoft.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUserSoft.Focus()
        End If
    End Sub

    Private Sub txtIPSoft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPSoft.TextChanged

    End Sub

    Private Sub txtUserSoft_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserSoft.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassSoft.Focus()
        End If
    End Sub

    Private Sub txtUserSoft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserSoft.TextChanged

    End Sub

    Private Sub txtPassSoft_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassSoft.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrSoft.Focus()
        End If
    End Sub

    Private Sub txtPassSoft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassSoft.TextChanged

    End Sub

    Private Sub txtNameWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameWeb.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPWeb.Focus()
        End If
    End Sub

    Private Sub txtNameWeb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameWeb.TextChanged

    End Sub

    Private Sub txtIPWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPWeb.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUserWeb.Focus()
        End If
    End Sub

    Private Sub txtIPWeb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPWeb.TextChanged

    End Sub

    Private Sub txtUserWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserWeb.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassWeb.Focus()
        End If
    End Sub

    Private Sub txtUserWeb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserWeb.TextChanged

    End Sub

    Private Sub txtPassWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassWeb.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrWeb.Focus()
        End If
    End Sub

    Private Sub txtPassWeb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassWeb.TextChanged

    End Sub

    Private Sub txtDscrWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrWeb.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrWeb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrWeb.TextChanged

    End Sub

    Private Sub Edt()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            If Me.txtBrandSrv.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند سرور را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandSrv.Focus()
                Exit Sub
            End If
        ElseIf Flag = 2 Then
            If Me.txtBrandRad.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند رادیو را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandRad.Focus()
                Exit Sub
            End If
        ElseIf Flag = 3 Then
            If Me.CheckBox1.Checked = True Then
                If Me.txtBrandFire.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا برند فایروال را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtBrandFire.Focus()
                    Exit Sub
                End If
            ElseIf Me.CheckBox2.Checked = True Then
                If Me.txtIPRadio1.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا آی پی پورت را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtIPRadio1.Focus()
                    Exit Sub
                End If
            End If
        ElseIf Flag = 4 Then
            If Me.CheckBox3.Checked = True Then
                If Me.txtBrandRout.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا برند روتر را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtBrandRout.Focus()
                    Exit Sub
                End If
            ElseIf Me.CheckBox4.Checked = True Then
                If Me.txtIPPortRout.Text = "" Then
                    MsgbOK.Label1.Text = " لطفا آی پی پورت را وارد کنید . "
                    MsgbOK.ShowDialog()
                    Me.txtIPPortRout.Focus()
                    Exit Sub
                End If
            End If
        ElseIf Flag = 5 Then
            If Me.txtBrandSw.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند سوئیچ را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandSw.Focus()
                Exit Sub
            End If
        ElseIf Flag = 6 Then
            If Me.txtBrandWire.Text = "" Then
                MsgbOK.Label1.Text = " لطفا برند وایرلس را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtBrandWire.Focus()
                Exit Sub
            End If
        ElseIf Flag = 7 Then
            If Me.txtNameSoft.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام نرم افزار را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtNameSoft.Focus()
                Exit Sub
            End If
        ElseIf Flag = 8 Then
            If Me.txtNameWeb.Text = "" Then
                MsgbOK.Label1.Text = " لطفا نام وب سرویس را وارد کنید . "
                MsgbOK.ShowDialog()
                Me.txtNameWeb.Focus()
                Exit Sub
            End If
        End If

        If Flag = 1 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.ServerInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.ServerInf")
            objDataview = New DataView(objDataset.Tables("Bnk.ServerInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        ElseIf Flag = 2 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.RadioInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RadioInf")
            objDataview = New DataView(objDataset.Tables("Bnk.RadioInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        ElseIf Flag = 3 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.FirewallInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.FirewallInf")
            objDataview = New DataView(objDataset.Tables("Bnk.FirewallInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        ElseIf Flag = 4 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.RouterInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RouterInf")
            objDataview = New DataView(objDataset.Tables("Bnk.RouterInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        ElseIf Flag = 5 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.SwitchInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SwitchInf")
            objDataview = New DataView(objDataset.Tables("Bnk.SwitchInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        ElseIf Flag = 6 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.WirelessInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WirelessInf")
            objDataview = New DataView(objDataset.Tables("Bnk.WirelessInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        ElseIf Flag = 7 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.SoftWareInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SoftWareInf")
            objDataview = New DataView(objDataset.Tables("Bnk.SoftWareInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        ElseIf Flag = 8 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.WebServiceInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WebServiceInf")
            objDataview = New DataView(objDataset.Tables("Bnk.WebServiceInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If
        End If

        Btn = 2
        InsertUpdate()

        Ref.PerformClick()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Edt()
    End Sub

    Private Sub Del()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        If Flag = 1 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row AS Expr1, Shob FROM Bnk.ServerInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.ServerInf")
            objDataview = New DataView(objDataset.Tables("Bnk.ServerInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtBrandSrv.Focus()
                Exit Sub
            End If
        ElseIf Flag = 2 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.RadioInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RadioInf")
            objDataview = New DataView(objDataset.Tables("Bnk.RadioInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtBrandFire.Focus()
                Exit Sub
            End If
        ElseIf Flag = 3 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.FirewallInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.FirewallInf")
            objDataview = New DataView(objDataset.Tables("Bnk.FirewallInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtBrandFire.Focus()
                Exit Sub
            End If
        ElseIf Flag = 4 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.RouterInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.RouterInf")
            objDataview = New DataView(objDataset.Tables("Bnk.RouterInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtBrandRout.Focus()
                Exit Sub
            End If
        ElseIf Flag = 5 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.SwitchInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SwitchInf")
            objDataview = New DataView(objDataset.Tables("Bnk.SwitchInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtBrandSw.Focus()
                Exit Sub
            End If
        ElseIf Flag = 6 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.WirelessInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WirelessInf")
            objDataview = New DataView(objDataset.Tables("Bnk.WirelessInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtBrandWire.Focus()
                Exit Sub
            End If
        ElseIf Flag = 7 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.SoftWareInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.SoftWareInf")
            objDataview = New DataView(objDataset.Tables("Bnk.SoftWareInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtNameSoft.Focus()
                Exit Sub
            End If
        ElseIf Flag = 8 Then
            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row, Shob FROM Bnk.WebServiceInf WHERE (Shob = " & Shob & ") AND (Row = " & txtRow.Text & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.WebServiceInf")
            objDataview = New DataView(objDataset.Tables("Bnk.WebServiceInf"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)

            If objDataview.Count = 0 Then
                MsgbOK.Label1.Text = " ردیف فوق قبلا در سیستم ثبت نشده است . "
                MsgbOK.ShowDialog()
                btnSave.Focus()
                Exit Sub
            End If

            MsgbYN.Label1.Text = "  آیا می خواهید ردیف  " & Trim(txtRow.Text) & "  از سیستم حذف شود  ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                txtNameWeb.Focus()
                Exit Sub
            End If
        End If
        Btn = 3
        InsertUpdate()

        Ref.PerformClick()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Del()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillRow()
        Clear()
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

    Private Sub Ref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ref.Click
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
    End Sub

    Private Sub DataGridView2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView2.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView2_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub txtDscrSoft_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrSoft.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtDscrSoft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDscrSoft.TextChanged

    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub DataGridView3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView3.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView3_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub DataGridView5_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView5.CellContentClick

    End Sub

    Private Sub DataGridView5_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView5.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView5_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView5.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub DataGridView6_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView6.CellContentClick

    End Sub

    Private Sub DataGridView6_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView6.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView6_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView6.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub DataGridView4_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick

    End Sub

    Private Sub DataGridView4_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView4.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView4_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView4.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub Xls()
        Dim X As Integer
        Dim Shb As String

        Dim xlapp As New Excel.Application()
        Dim xlbook As Excel.Workbook
        Dim xlsheet As Excel.Worksheet

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        xlapp = CType(CreateObject("excel.application"), Excel.Application)
        xlbook = CType(xlapp.Workbooks.Add, Excel.Workbook)  ' xlsheet = CType(xlbook.Worksheets(1), Excel.Worksheet)
        xlsheet = CType(xlbook.Worksheets(1), Excel.Worksheet)
        xlsheet.Application.Visible = True

        Shb = Mainfrm.cboShob.Text
        xlsheet.Name = Shb

        With xlsheet
            Dim i As Integer = 2
            ' .DisplayRightToLeft = True

            Flag = 1
            Panel1.BringToFront()
            FillRow()
            CheckServer.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView1.BringToFront()
            lblName.Text = "Server Panel"

            .Range("A1").Value = "Servers"
            .Range("A1").Font.Bold = True
            .Range("A1").Font.Size = 20
            .Range("A1").Font.ColorIndex = 9
            .Range("A1").Borders.LineStyle = True

            .Range("A" & i.ToString).Value = "Brand/Model"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "Server Name"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "Host Name"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "IP Number1"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "IP Number2"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5
            .Range("F" & i.ToString).Value = "Software"
            .Range("F" & i.ToString).Font.Bold = True
            .Range("F" & i.ToString).Font.ColorIndex = 5
            .Range("G" & i.ToString).Value = "SoftPass"
            .Range("G" & i.ToString).Font.Bold = True
            .Range("G" & i.ToString).Font.ColorIndex = 5
            .Range("H" & i.ToString).Value = "LocalPass"
            .Range("H" & i.ToString).Font.Bold = True
            .Range("H" & i.ToString).Font.ColorIndex = 5
            .Range("I" & i.ToString).Value = "User"
            .Range("I" & i.ToString).Font.Bold = True
            .Range("I" & i.ToString).Font.ColorIndex = 5
            .Range("J" & i.ToString).Value = "Password"
            .Range("J" & i.ToString).Font.Bold = True
            .Range("J" & i.ToString).Font.ColorIndex = 5
            .Range("K" & i.ToString).Value = "Dscr"
            .Range("K" & i.ToString).Font.Bold = True
            .Range("K" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView1.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(5).Value
                .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                .Range("K" & i.ToString).Value = DataGridView1.Rows(X).Cells(12).Value
                i += 1
                ' End If
            Next X

            Flag = 2
            Panel2.BringToFront()
            FillRow()
            CheckRadio.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView2.BringToFront()
            lblName.Text = "Radio Panel"

            .Range("A" & i.ToString).Value = "Radios"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.Size = 20
            .Range("A" & i.ToString).Font.ColorIndex = 9
            .Range("A" & i.ToString).Borders.LineStyle = True

            i += 1
            .Range("A" & i.ToString).Value = "Brand"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "IP"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "User"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "Password"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "Dscr"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView2.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView2.Rows(X).Cells(1).Value
                .Range("B" & i.ToString).Value = DataGridView2.Rows(X).Cells(2).Value
                .Range("C" & i.ToString).Value = DataGridView2.Rows(X).Cells(3).Value
                .Range("D" & i.ToString).Value = DataGridView2.Rows(X).Cells(4).Value
                .Range("E" & i.ToString).Value = DataGridView2.Rows(X).Cells(5).Value
                i += 1
                ' End If
            Next X

            Flag = 3
            Panel3.BringToFront()
            FillRow()
            CheckFirewall.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView3.BringToFront()
            lblName.Text = "Firewall Panel"

            .Range("A" & i.ToString).Value = "Firewalls"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.Size = 20
            .Range("A" & i.ToString).Font.ColorIndex = 9
            .Range("A" & i.ToString).Borders.LineStyle = True

            i += 1
            .Range("A" & i.ToString).Value = "Brand/Model"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "Number Of Port"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "IP Number"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "User Admin"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "Pass Admin"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5
            .Range("F" & i.ToString).Value = "User"
            .Range("F" & i.ToString).Font.Bold = True
            .Range("F" & i.ToString).Font.ColorIndex = 5
            .Range("G" & i.ToString).Value = "Password"
            .Range("G" & i.ToString).Font.Bold = True
            .Range("G" & i.ToString).Font.ColorIndex = 5
            .Range("H" & i.ToString).Value = "PortNo"
            .Range("H" & i.ToString).Font.Bold = True
            .Range("H" & i.ToString).Font.ColorIndex = 5
            .Range("I" & i.ToString).Value = "Radio1 IP"
            .Range("I" & i.ToString).Font.Bold = True
            .Range("I" & i.ToString).Font.ColorIndex = 5
            .Range("J" & i.ToString).Value = "Radio2 IP"
            .Range("J" & i.ToString).Font.Bold = True
            .Range("J" & i.ToString).Font.ColorIndex = 5
            .Range("K" & i.ToString).Value = "Dscr"
            .Range("K" & i.ToString).Font.Bold = True
            .Range("K" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView3.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView3.Rows(X).Cells(2).Value
                .Range("B" & i.ToString).Value = DataGridView3.Rows(X).Cells(3).Value
                .Range("C" & i.ToString).Value = DataGridView3.Rows(X).Cells(4).Value
                .Range("D" & i.ToString).Value = DataGridView3.Rows(X).Cells(5).Value
                .Range("E" & i.ToString).Value = DataGridView3.Rows(X).Cells(6).Value
                .Range("F" & i.ToString).Value = DataGridView3.Rows(X).Cells(7).Value
                .Range("G" & i.ToString).Value = DataGridView3.Rows(X).Cells(8).Value
                .Range("H" & i.ToString).Value = DataGridView3.Rows(X).Cells(11).Value
                .Range("I" & i.ToString).Value = DataGridView3.Rows(X).Cells(12).Value
                .Range("J" & i.ToString).Value = DataGridView3.Rows(X).Cells(13).Value
                .Range("K" & i.ToString).Value = DataGridView3.Rows(X).Cells(14).Value
                i += 1
                ' End If
            Next X

            Flag = 4
            Panel4.BringToFront()
            FillRow()
            CheckRouter.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView4.BringToFront()
            lblName.Text = "Router Panel"

            .Range("A" & i.ToString).Value = "Routers"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.Size = 20
            .Range("A" & i.ToString).Font.ColorIndex = 9
            .Range("A" & i.ToString).Borders.LineStyle = True

            i += 1
            .Range("A" & i.ToString).Value = "Brand/Model"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "Number Of Port"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "IP Number"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "User"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "Password"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5
            .Range("F" & i.ToString).Value = "PortNo"
            .Range("F" & i.ToString).Font.Bold = True
            .Range("F" & i.ToString).Font.ColorIndex = 5
            .Range("G" & i.ToString).Value = "PortIP"
            .Range("G" & i.ToString).Font.Bold = True
            .Range("G" & i.ToString).Font.ColorIndex = 5
            .Range("H" & i.ToString).Value = "Dscr"
            .Range("H" & i.ToString).Font.Bold = True
            .Range("H" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView4.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView4.Rows(X).Cells(2).Value
                .Range("B" & i.ToString).Value = DataGridView4.Rows(X).Cells(3).Value
                .Range("C" & i.ToString).Value = DataGridView4.Rows(X).Cells(4).Value
                .Range("D" & i.ToString).Value = DataGridView4.Rows(X).Cells(5).Value
                .Range("E" & i.ToString).Value = DataGridView4.Rows(X).Cells(6).Value
                .Range("F" & i.ToString).Value = DataGridView4.Rows(X).Cells(9).Value
                .Range("G" & i.ToString).Value = DataGridView4.Rows(X).Cells(10).Value
                .Range("H" & i.ToString).Value = DataGridView4.Rows(X).Cells(11).Value
                i += 1
                ' End If
            Next X

            Flag = 5
            Panel5.BringToFront()
            FillRow()
            CheckSwitch.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView5.BringToFront()
            lblName.Text = "Switch Panel"

            .Range("A" & i.ToString).Value = "Switches"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.Size = 20
            .Range("A" & i.ToString).Font.ColorIndex = 9
            .Range("A" & i.ToString).Borders.LineStyle = True

            i += 1
            .Range("A" & i.ToString).Value = "Brand/Model"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "Number Of Port"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "IP"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "User"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "Password"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5
            .Range("F" & i.ToString).Value = "Host Name"
            .Range("F" & i.ToString).Font.Bold = True
            .Range("F" & i.ToString).Font.ColorIndex = 5
            .Range("G" & i.ToString).Value = "Dscr"
            .Range("G" & i.ToString).Font.Bold = True
            .Range("G" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView5.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView5.Rows(X).Cells(1).Value
                .Range("B" & i.ToString).Value = DataGridView5.Rows(X).Cells(2).Value
                .Range("C" & i.ToString).Value = DataGridView5.Rows(X).Cells(3).Value
                .Range("D" & i.ToString).Value = DataGridView5.Rows(X).Cells(4).Value
                .Range("E" & i.ToString).Value = DataGridView5.Rows(X).Cells(5).Value
                .Range("F" & i.ToString).Value = DataGridView5.Rows(X).Cells(6).Value
                .Range("G" & i.ToString).Value = DataGridView5.Rows(X).Cells(7).Value
                i += 1
                ' End If
            Next X

            Flag = 6
            Panel6.BringToFront()
            FillRow()
            CheckWireless.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView6.BringToFront()
            lblName.Text = "Wireless Panel"

            .Range("A" & i.ToString).Value = "Wireless"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.Size = 20
            .Range("A" & i.ToString).Font.ColorIndex = 9
            .Range("A" & i.ToString).Borders.LineStyle = True

            i += 1
            .Range("A" & i.ToString).Value = "Brand/Model"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "IP LAN"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "IP WAN"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "User"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "Password"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5
            .Range("F" & i.ToString).Value = "SSID"
            .Range("F" & i.ToString).Font.Bold = True
            .Range("F" & i.ToString).Font.ColorIndex = 5
            .Range("G" & i.ToString).Value = "SSIDPass"
            .Range("G" & i.ToString).Font.Bold = True
            .Range("G" & i.ToString).Font.ColorIndex = 5
            .Range("H" & i.ToString).Value = "DScr"
            .Range("H" & i.ToString).Font.Bold = True
            .Range("H" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView6.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView6.Rows(X).Cells(1).Value
                .Range("B" & i.ToString).Value = DataGridView6.Rows(X).Cells(2).Value
                .Range("C" & i.ToString).Value = DataGridView6.Rows(X).Cells(3).Value
                .Range("D" & i.ToString).Value = DataGridView6.Rows(X).Cells(4).Value
                .Range("E" & i.ToString).Value = DataGridView6.Rows(X).Cells(5).Value
                .Range("F" & i.ToString).Value = DataGridView6.Rows(X).Cells(6).Value
                .Range("G" & i.ToString).Value = DataGridView6.Rows(X).Cells(7).Value
                .Range("H" & i.ToString).Value = DataGridView6.Rows(X).Cells(8).Value
                i += 1
                ' End If
            Next X

            Flag = 7
            Panel7.BringToFront()
            FillRow()
            CheckSoftware.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView7.BringToFront()
            lblName.Text = "Software Panel"

            .Range("A" & i.ToString).Value = "Software"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.Size = 20
            .Range("A" & i.ToString).Font.ColorIndex = 9
            .Range("A" & i.ToString).Borders.LineStyle = True

            i += 1
            .Range("A" & i.ToString).Value = "Name"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "IP"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "User"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "Password"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "Dscr"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView7.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView7.Rows(X).Cells(1).Value
                .Range("B" & i.ToString).Value = DataGridView7.Rows(X).Cells(2).Value
                .Range("C" & i.ToString).Value = DataGridView7.Rows(X).Cells(3).Value
                .Range("D" & i.ToString).Value = DataGridView7.Rows(X).Cells(4).Value
                .Range("E" & i.ToString).Value = DataGridView7.Rows(X).Cells(5).Value
                i += 1
                ' End If
            Next X

            Flag = 8
            Panel8.BringToFront()
            FillRow()
            CheckWebService.Checked = True
            FillDatasetAndDataview()
            FillDataGridView()
            Clear()
            DataGridView7.BringToFront()
            lblName.Text = "WebService Panel"

            .Range("A" & i.ToString).Value = "WebService"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.Size = 20
            .Range("A" & i.ToString).Font.ColorIndex = 9
            .Range("A" & i.ToString).Borders.LineStyle = True

            i += 1
            .Range("A" & i.ToString).Value = "Name"
            .Range("A" & i.ToString).Font.Bold = True
            .Range("A" & i.ToString).Font.ColorIndex = 5
            .Range("B" & i.ToString).Value = "IP"
            .Range("B" & i.ToString).Font.Bold = True
            .Range("B" & i.ToString).Font.ColorIndex = 5
            .Range("C" & i.ToString).Value = "User"
            .Range("C" & i.ToString).Font.Bold = True
            .Range("C" & i.ToString).Font.ColorIndex = 5
            .Range("D" & i.ToString).Value = "Password"
            .Range("D" & i.ToString).Font.Bold = True
            .Range("D" & i.ToString).Font.ColorIndex = 5
            .Range("E" & i.ToString).Value = "Dscr"
            .Range("E" & i.ToString).Font.Bold = True
            .Range("E" & i.ToString).Font.ColorIndex = 5

            i += 1
            For X = 0 To DataGridView7.RowCount - 1
                ' If DataGridView1.Rows(X).Selected = True Then
                .Range("A" & i.ToString).Value = DataGridView7.Rows(X).Cells(1).Value
                .Range("B" & i.ToString).Value = DataGridView7.Rows(X).Cells(2).Value
                .Range("C" & i.ToString).Value = DataGridView7.Rows(X).Cells(3).Value
                .Range("D" & i.ToString).Value = DataGridView7.Rows(X).Cells(4).Value
                .Range("E" & i.ToString).Value = DataGridView7.Rows(X).Cells(5).Value
                i += 1
                ' End If
            Next X
        End With
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim X As Integer
        Dim X1 As Integer
        Dim Sh As Integer
        Dim Shb As String

        Try
            If CheckBox5.Checked = True Then
                Dim xlapp As New Excel.Application()
                Dim xlbook As Excel.Workbook
                Dim xlsheet As Excel.Worksheet

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
                xlapp = CType(CreateObject("excel.application"), Excel.Application)
                xlbook = CType(xlapp.Workbooks.Add, Excel.Workbook)  ' xlsheet = CType(xlbook.Worksheets(1), Excel.Worksheet)

                Dim objDataAdapter As New SqlDataAdapter _
                    (" SELECT Bnk.ServerInf.Shob, Bas.Shob.Shob_Name FROM Bnk.ServerInf INNER JOIN Bas.Shob ON Bnk.ServerInf.Shob = Bas.Shob.Shob_Code" & _
                    " GROUP BY Bnk.ServerInf.Shob, Bas.Shob.Shob_Name ORDER BY Bnk.ServerInf.Shob", objConnection)
                objDataset = New DataSet
                objDataAdapter.Fill(objDataset, "Bnk.ServerInf")
                objDataviewT = New DataView(objDataset.Tables("Bnk.ServerInf"))
                objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)

                Me.DataGridView8.AutoGenerateColumns = True
                Me.DataGridView8.DataSource = objDataviewT

                For X1 = 0 To DataGridView8.RowCount
                    ' If X1 > 3 Then ' زمانی که آفیس 2007/2010 نصب باشد
                    If X1 > 1 Then ' زمانی که آفیس 2016 نصب باشد
                        xlsheet = CType(xlbook.Worksheets.Add, Excel.Worksheet)
                    End If
                Next

                For X1 = 0 To DataGridView8.RowCount - 1
                    Mainfrm.cboShob.ComboBox.SelectedValue = DataGridView8.Rows(X1).Cells(0).Value
                    If CheckBox5.Checked = True Then
                        Shb = DataGridView8.Rows(X1).Cells(1).Value

                        If X1 = 0 Then
                            Sh = 1
                            xlsheet = CType(xlbook.Worksheets(Sh), Excel.Worksheet)
                            xlsheet.Application.Visible = True
                        Else
                            Sh = Sh + 1
                            xlsheet = CType(xlbook.Worksheets(Sh), Excel.Worksheet)
                        End If
                    Else
                        Shb = Mainfrm.cboShob.Text
                    End If
                    xlsheet.Name = Shb

                    With xlsheet
                        Dim i As Integer = 2
                        ' .DisplayRightToLeft = True

                        Flag = 1
                        Panel1.BringToFront()
                        FillRow()
                        CheckServer.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView1.BringToFront()
                        lblName.Text = "Server Panel"

                        .Range("A1").Value = "Servers"
                        .Range("A1").Font.Bold = True
                        .Range("A1").Font.Size = 20
                        .Range("A1").Font.ColorIndex = 9
                        .Range("A1").Borders.LineStyle = True

                        .Range("A" & i.ToString).Value = "Brand/Model"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "Server Name"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "IP Number1"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "IP Number2"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Software"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5
                        .Range("F" & i.ToString).Value = "SoftPass"
                        .Range("F" & i.ToString).Font.Bold = True
                        .Range("F" & i.ToString).Font.ColorIndex = 5
                        .Range("G" & i.ToString).Value = "LocalPass"
                        .Range("G" & i.ToString).Font.Bold = True
                        .Range("G" & i.ToString).Font.ColorIndex = 5
                        .Range("H" & i.ToString).Value = "User"
                        .Range("H" & i.ToString).Font.Bold = True
                        .Range("H" & i.ToString).Font.ColorIndex = 5
                        .Range("I" & i.ToString).Value = "Password"
                        .Range("I" & i.ToString).Font.Bold = True
                        .Range("I" & i.ToString).Font.ColorIndex = 5
                        .Range("J" & i.ToString).Value = "Dscr"
                        .Range("J" & i.ToString).Font.Bold = True
                        .Range("J" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView1.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView1.Rows(X).Cells(1).Value
                            .Range("B" & i.ToString).Value = DataGridView1.Rows(X).Cells(2).Value
                            .Range("C" & i.ToString).Value = DataGridView1.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView1.Rows(X).Cells(4).Value
                            .Range("E" & i.ToString).Value = DataGridView1.Rows(X).Cells(6).Value
                            .Range("F" & i.ToString).Value = DataGridView1.Rows(X).Cells(7).Value
                            .Range("G" & i.ToString).Value = DataGridView1.Rows(X).Cells(8).Value
                            .Range("H" & i.ToString).Value = DataGridView1.Rows(X).Cells(9).Value
                            .Range("I" & i.ToString).Value = DataGridView1.Rows(X).Cells(10).Value
                            .Range("J" & i.ToString).Value = DataGridView1.Rows(X).Cells(11).Value
                            i += 1
                            ' End If
                        Next X

                        Flag = 2
                        Panel2.BringToFront()
                        FillRow()
                        CheckRadio.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView2.BringToFront()
                        lblName.Text = "Radio Panel"

                        .Range("A" & i.ToString).Value = "Radios"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.Size = 20
                        .Range("A" & i.ToString).Font.ColorIndex = 9
                        .Range("A" & i.ToString).Borders.LineStyle = True

                        i += 1
                        .Range("A" & i.ToString).Value = "Brand"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "IP"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "User"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "Password"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Dscr"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView2.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView2.Rows(X).Cells(1).Value
                            .Range("B" & i.ToString).Value = DataGridView2.Rows(X).Cells(2).Value
                            .Range("C" & i.ToString).Value = DataGridView2.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView2.Rows(X).Cells(4).Value
                            .Range("E" & i.ToString).Value = DataGridView2.Rows(X).Cells(5).Value
                            i += 1
                            ' End If
                        Next X

                        Flag = 3
                        Panel3.BringToFront()
                        FillRow()
                        CheckFirewall.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView3.BringToFront()
                        lblName.Text = "Firewall Panel"

                        .Range("A" & i.ToString).Value = "Firewalls"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.Size = 20
                        .Range("A" & i.ToString).Font.ColorIndex = 9
                        .Range("A" & i.ToString).Borders.LineStyle = True

                        i += 1
                        .Range("A" & i.ToString).Value = "Brand/Model"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "Number Of Port"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "IP Number"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "User Admin"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Pass Admin"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5
                        .Range("F" & i.ToString).Value = "User"
                        .Range("F" & i.ToString).Font.Bold = True
                        .Range("F" & i.ToString).Font.ColorIndex = 5
                        .Range("G" & i.ToString).Value = "Password"
                        .Range("G" & i.ToString).Font.Bold = True
                        .Range("G" & i.ToString).Font.ColorIndex = 5
                        .Range("H" & i.ToString).Value = "PortNo"
                        .Range("H" & i.ToString).Font.Bold = True
                        .Range("H" & i.ToString).Font.ColorIndex = 5
                        .Range("I" & i.ToString).Value = "Radio1 IP"
                        .Range("I" & i.ToString).Font.Bold = True
                        .Range("I" & i.ToString).Font.ColorIndex = 5
                        .Range("J" & i.ToString).Value = "Radio2 IP"
                        .Range("J" & i.ToString).Font.Bold = True
                        .Range("J" & i.ToString).Font.ColorIndex = 5
                        .Range("K" & i.ToString).Value = "Dscr"
                        .Range("K" & i.ToString).Font.Bold = True
                        .Range("K" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView3.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView3.Rows(X).Cells(2).Value
                            .Range("B" & i.ToString).Value = DataGridView3.Rows(X).Cells(3).Value
                            .Range("C" & i.ToString).Value = DataGridView3.Rows(X).Cells(4).Value
                            .Range("D" & i.ToString).Value = DataGridView3.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView3.Rows(X).Cells(6).Value
                            .Range("F" & i.ToString).Value = DataGridView3.Rows(X).Cells(7).Value
                            .Range("G" & i.ToString).Value = DataGridView3.Rows(X).Cells(8).Value
                            .Range("H" & i.ToString).Value = DataGridView3.Rows(X).Cells(11).Value
                            .Range("I" & i.ToString).Value = DataGridView3.Rows(X).Cells(12).Value
                            .Range("J" & i.ToString).Value = DataGridView3.Rows(X).Cells(13).Value
                            .Range("K" & i.ToString).Value = DataGridView3.Rows(X).Cells(14).Value
                            i += 1
                            ' End If
                        Next X

                        Flag = 4
                        Panel4.BringToFront()
                        FillRow()
                        CheckRouter.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView4.BringToFront()
                        lblName.Text = "Router Panel"

                        .Range("A" & i.ToString).Value = "Routers"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.Size = 20
                        .Range("A" & i.ToString).Font.ColorIndex = 9
                        .Range("A" & i.ToString).Borders.LineStyle = True

                        i += 1
                        .Range("A" & i.ToString).Value = "Brand/Model"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "Number Of Port"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "IP Number"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "User"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Password"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5
                        .Range("F" & i.ToString).Value = "PortNo"
                        .Range("F" & i.ToString).Font.Bold = True
                        .Range("F" & i.ToString).Font.ColorIndex = 5
                        .Range("G" & i.ToString).Value = "PortIP"
                        .Range("G" & i.ToString).Font.Bold = True
                        .Range("G" & i.ToString).Font.ColorIndex = 5
                        .Range("H" & i.ToString).Value = "Dscr"
                        .Range("H" & i.ToString).Font.Bold = True
                        .Range("H" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView4.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView4.Rows(X).Cells(2).Value
                            .Range("B" & i.ToString).Value = DataGridView4.Rows(X).Cells(3).Value
                            .Range("C" & i.ToString).Value = DataGridView4.Rows(X).Cells(4).Value
                            .Range("D" & i.ToString).Value = DataGridView4.Rows(X).Cells(5).Value
                            .Range("E" & i.ToString).Value = DataGridView4.Rows(X).Cells(6).Value
                            .Range("F" & i.ToString).Value = DataGridView4.Rows(X).Cells(9).Value
                            .Range("G" & i.ToString).Value = DataGridView4.Rows(X).Cells(10).Value
                            .Range("H" & i.ToString).Value = DataGridView4.Rows(X).Cells(11).Value
                            i += 1
                            ' End If
                        Next X

                        Flag = 5
                        Panel5.BringToFront()
                        FillRow()
                        CheckSwitch.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView5.BringToFront()
                        lblName.Text = "Switch Panel"

                        .Range("A" & i.ToString).Value = "Switches"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.Size = 20
                        .Range("A" & i.ToString).Font.ColorIndex = 9
                        .Range("A" & i.ToString).Borders.LineStyle = True

                        i += 1
                        .Range("A" & i.ToString).Value = "Brand/Model"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "Number Of Port"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "IP"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "User"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Password"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5
                        .Range("F" & i.ToString).Value = "Host Name"
                        .Range("F" & i.ToString).Font.Bold = True
                        .Range("F" & i.ToString).Font.ColorIndex = 5
                        .Range("G" & i.ToString).Value = "Dscr"
                        .Range("G" & i.ToString).Font.Bold = True
                        .Range("G" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView5.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView5.Rows(X).Cells(1).Value
                            .Range("B" & i.ToString).Value = DataGridView5.Rows(X).Cells(2).Value
                            .Range("C" & i.ToString).Value = DataGridView5.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView5.Rows(X).Cells(4).Value
                            .Range("E" & i.ToString).Value = DataGridView5.Rows(X).Cells(5).Value
                            .Range("F" & i.ToString).Value = DataGridView5.Rows(X).Cells(6).Value
                            .Range("G" & i.ToString).Value = DataGridView5.Rows(X).Cells(7).Value
                            i += 1
                            ' End If
                        Next X

                        Flag = 6
                        Panel6.BringToFront()
                        FillRow()
                        CheckWireless.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView6.BringToFront()
                        lblName.Text = "Wireless Panel"

                        .Range("A" & i.ToString).Value = "Wireless"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.Size = 20
                        .Range("A" & i.ToString).Font.ColorIndex = 9
                        .Range("A" & i.ToString).Borders.LineStyle = True

                        i += 1
                        .Range("A" & i.ToString).Value = "Brand/Model"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "IP LAN"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "IP WAN"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "User"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Password"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5
                        .Range("F" & i.ToString).Value = "SSID"
                        .Range("F" & i.ToString).Font.Bold = True
                        .Range("F" & i.ToString).Font.ColorIndex = 5
                        .Range("G" & i.ToString).Value = "SSIDPass"
                        .Range("G" & i.ToString).Font.Bold = True
                        .Range("G" & i.ToString).Font.ColorIndex = 5
                        .Range("H" & i.ToString).Value = "DScr"
                        .Range("H" & i.ToString).Font.Bold = True
                        .Range("H" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView6.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView6.Rows(X).Cells(1).Value
                            .Range("B" & i.ToString).Value = DataGridView6.Rows(X).Cells(2).Value
                            .Range("C" & i.ToString).Value = DataGridView6.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView6.Rows(X).Cells(4).Value
                            .Range("E" & i.ToString).Value = DataGridView6.Rows(X).Cells(5).Value
                            .Range("F" & i.ToString).Value = DataGridView6.Rows(X).Cells(6).Value
                            .Range("G" & i.ToString).Value = DataGridView6.Rows(X).Cells(7).Value
                            .Range("H" & i.ToString).Value = DataGridView6.Rows(X).Cells(8).Value
                            i += 1
                            ' End If
                        Next X

                        Flag = 7
                        Panel7.BringToFront()
                        FillRow()
                        CheckSoftware.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView7.BringToFront()
                        lblName.Text = "Software Panel"

                        .Range("A" & i.ToString).Value = "Software"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.Size = 20
                        .Range("A" & i.ToString).Font.ColorIndex = 9
                        .Range("A" & i.ToString).Borders.LineStyle = True

                        i += 1
                        .Range("A" & i.ToString).Value = "Name"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "IP"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "User"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "Password"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Dscr"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView7.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView7.Rows(X).Cells(1).Value
                            .Range("B" & i.ToString).Value = DataGridView7.Rows(X).Cells(2).Value
                            .Range("C" & i.ToString).Value = DataGridView7.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView7.Rows(X).Cells(4).Value
                            .Range("E" & i.ToString).Value = DataGridView7.Rows(X).Cells(5).Value
                            i += 1
                            ' End If
                        Next X

                        Flag = 8
                        Panel8.BringToFront()
                        FillRow()
                        CheckWebService.Checked = True
                        FillDatasetAndDataview()
                        FillDataGridView()
                        Clear()
                        DataGridView7.BringToFront()
                        lblName.Text = "WebService Panel"

                        .Range("A" & i.ToString).Value = "WebService"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.Size = 20
                        .Range("A" & i.ToString).Font.ColorIndex = 9
                        .Range("A" & i.ToString).Borders.LineStyle = True

                        i += 1
                        .Range("A" & i.ToString).Value = "Name"
                        .Range("A" & i.ToString).Font.Bold = True
                        .Range("A" & i.ToString).Font.ColorIndex = 5
                        .Range("B" & i.ToString).Value = "IP"
                        .Range("B" & i.ToString).Font.Bold = True
                        .Range("B" & i.ToString).Font.ColorIndex = 5
                        .Range("C" & i.ToString).Value = "User"
                        .Range("C" & i.ToString).Font.Bold = True
                        .Range("C" & i.ToString).Font.ColorIndex = 5
                        .Range("D" & i.ToString).Value = "Password"
                        .Range("D" & i.ToString).Font.Bold = True
                        .Range("D" & i.ToString).Font.ColorIndex = 5
                        .Range("E" & i.ToString).Value = "Dscr"
                        .Range("E" & i.ToString).Font.Bold = True
                        .Range("E" & i.ToString).Font.ColorIndex = 5

                        i += 1
                        For X = 0 To DataGridView7.RowCount - 1
                            ' If DataGridView1.Rows(X).Selected = True Then
                            .Range("A" & i.ToString).Value = DataGridView7.Rows(X).Cells(1).Value
                            .Range("B" & i.ToString).Value = DataGridView7.Rows(X).Cells(2).Value
                            .Range("C" & i.ToString).Value = DataGridView7.Rows(X).Cells(3).Value
                            .Range("D" & i.ToString).Value = DataGridView7.Rows(X).Cells(4).Value
                            .Range("E" & i.ToString).Value = DataGridView7.Rows(X).Cells(5).Value
                            i += 1
                            ' End If
                        Next X
                    End With
                Next
            Else
                Xls()
            End If
            '  xlsheet.Application.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtBrandRad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrandRad.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtIPRad.Focus()
        End If
    End Sub

    Private Sub txtBrandRad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandRad.TextChanged

    End Sub

    Private Sub txtIPRad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPRad.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtUserRad.Focus()
        End If
    End Sub

    Private Sub txtIPRad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPRad.TextChanged

    End Sub

    Private Sub txtUserRad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserRad.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPassRad.Focus()
        End If
    End Sub

    Private Sub txtUserRad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserRad.TextChanged

    End Sub

    Private Sub txtPassRad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassRad.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrRad.Focus()
        End If
    End Sub

    Private Sub txtPassRad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassRad.TextChanged

    End Sub

    Private Sub txtDscrRad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDscrRad.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub CheckRadio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckRadio.CheckedChanged
        Flag = 2
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel2.BringToFront()
        If CheckRadio.Checked = True Then
            lblName.Text = "Radio Panel"
        Else
            lblName.Text = "Radio Panel - Amani"
        End If
    End Sub

    Private Sub CheckFirewall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckFirewall.CheckedChanged
        Flag = 3
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel3.BringToFront()
        If CheckFirewall.Checked = True Then
            lblName.Text = "Firewall Panel"
        Else
            lblName.Text = "Firewall Panel - Amani"
        End If
    End Sub

    Private Sub CheckRouter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckRouter.CheckedChanged
        Flag = 4
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel4.BringToFront()
        If CheckRouter.Checked = True Then
            lblName.Text = "Router Panel"
        Else
            lblName.Text = "Router Panel - Amani"
        End If
    End Sub

    Private Sub CheckSwitch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckSwitch.CheckedChanged
        Flag = 5
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel5.BringToFront()
        If CheckSwitch.Checked = True Then
            lblName.Text = "Switch Panel"
        Else
            lblName.Text = "Switch Panel - Amani"
        End If
    End Sub

    Private Sub CheckWireless_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckWireless.CheckedChanged
        Flag = 6
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel6.BringToFront()
        If CheckWireless.Checked = True Then
            lblName.Text = "Wireless Panel"
        Else
            lblName.Text = "Wireless Panel - Amani"
        End If
    End Sub

    Private Sub CheckSoftware_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckSoftware.CheckedChanged
        Flag = 7
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel7.BringToFront()
        If CheckSoftware.Checked = True Then
            lblName.Text = "Software Panel"
        Else
            lblName.Text = "Software Panel - Amani"
        End If
    End Sub

    Private Sub CheckWebService_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckWebService.CheckedChanged
        Flag = 8
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel8.BringToFront()
        If CheckWebService.Checked = True Then
            lblName.Text = "WebService Panel"
        Else
            lblName.Text = "WebService Panel - Amani"
        End If
    End Sub

    Private Sub CheckServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckServer.CheckedChanged
        Flag = 1
        FillRow()
        FillDatasetAndDataview()
        FillDataGridView()
        DataGridBring()
        Clear()
        Panel1.BringToFront()
        If CheckServer.Checked = True Then
            lblName.Text = "Server Panel"
        Else
            lblName.Text = "Server Panel - Amani"
        End If
    End Sub

    Private Sub DataGridView7_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView7.CellContentClick

    End Sub

    Private Sub DataGridView7_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView7.KeyUp
        If e.KeyCode = 38 Or e.KeyCode = 40 Then
            DGVMove()
        End If
    End Sub

    Private Sub DataGridView7_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView7.RowHeaderMouseClick
        DGVMove()
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = False Then
            CheckBox7.Text = "خروجی اکسل لیست اموال"
            CheckServer.Checked = True
            CheckRadio.Checked = True
            CheckFirewall.Checked = True
            CheckRouter.Checked = True
            CheckSwitch.Checked = True
            CheckWireless.Checked = True
            CheckSoftware.Checked = True
            CheckWebService.Checked = True
        Else
            CheckBox7.Text = "خروجی اکسل لیست امانی"
            CheckServer.Checked = False
            CheckRadio.Checked = False
            CheckFirewall.Checked = False
            CheckRouter.Checked = False
            CheckSwitch.Checked = False
            CheckWireless.Checked = False
            CheckSoftware.Checked = False
            CheckWebService.Checked = False
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then
            cboShob.Enabled = True
            btnMove.Enabled = True
        ElseIf CheckBox8.Checked = False Then
            cboShob.Enabled = False
            btnMove.Enabled = False
        End If
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim objCommand As New SqlCommand
        Dim ShMove As String
        Dim Rw As String
        Dim X As Integer
        Dim cnt As String

        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        ShMove = cboShob.SelectedValue.ToString

        If Flag = 1 Then
            cnt = Me.DataGridView1.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView1.Rows(X).Cells(0).Value
                    If DataGridView1.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.ServerInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.ServerInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.ServerInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.ServerInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        ElseIf Flag = 2 Then
            cnt = Me.DataGridView2.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView2.Rows(X).Cells(0).Value
                    If DataGridView2.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.RadioInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.RadioInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.RadioInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.RadioInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        ElseIf Flag = 3 Then
            cnt = Me.DataGridView3.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView3.Rows(X).Cells(0).Value
                    If DataGridView3.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.FirewallInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.FirewallInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.FirewallInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.FirewallInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        ElseIf Flag = 4 Then
            cnt = Me.DataGridView4.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView4.Rows(X).Cells(0).Value
                    If DataGridView4.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.RouterInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.RouterInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.RouterInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.RouterInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        ElseIf Flag = 5 Then
            cnt = Me.DataGridView5.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView5.Rows(X).Cells(0).Value
                    If DataGridView5.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.SwitchInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.SwitchInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.SwitchInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.SwitchInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        ElseIf Flag = 6 Then
            cnt = Me.DataGridView6.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView6.Rows(X).Cells(0).Value
                    If DataGridView6.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.WirelessInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.WirelessInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.WirelessInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.WirelessInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        ElseIf Flag = 7 Then
            cnt = Me.DataGridView7.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView7.Rows(X).Cells(0).Value
                    If DataGridView7.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.SoftwareInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.SoftwareInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.SoftwareInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.SoftwareInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        ElseIf Flag = 8 Then
            cnt = Me.DataGridView7.RowCount - 1
            If Shob <> ShMove Then
                MsgbYN.Label1.Text = "  آیا از انتقال رکوردهای انتخاب شده به شعبه  " & cboShob.Text & "  اطمینان دارید  ؟ "
                If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    btnMove.Focus()
                    Exit Sub
                End If
                For X = 0 To cnt
                    Rw = DataGridView7.Rows(X).Cells(0).Value
                    If DataGridView7.Rows(X).Selected = True Then
                        Dim objDataAdapter As New SqlDataAdapter _
                            (" SELECT MAX(Row) AS Expr1 FROM Bnk.WebServiceInf WHERE (Shob = " & ShMove & ")", objConnection)
                        objDataset = New DataSet
                        objDataAdapter.Fill(objDataset, "Bnk.WebServiceInf")
                        objDataviewT = New DataView(objDataset.Tables("Bnk.WebServiceInf"))
                        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)
                        objDataviewT.Sort = "Expr1"

                        Label6.DataBindings.Clear()
                        Label6.DataBindings.Add("Text", objDataviewT, "Expr1")
                        If Label6.Text <> "" Then
                            txtRowShMove.Text = Val(Label6.Text) + 1
                        ElseIf Label6.Text = "" Then
                            txtRowShMove.Text = 1
                        End If
                        Label6.DataBindings.Clear()
                        Label6.Text = ""

                        objCommand.Connection = objConnection
                        objCommand.CommandText = _
                           " UPDATE Bnk.WebServiceInf SET Row = " & txtRowShMove.Text & ", Shob = " & ShMove & " WHERE (Row = " & Rw & ") AND (Shob = " & Shob & ")"
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
                    End If
                Next
            Else
                MsgbOK.Label1.Text = " انتخاب شعبه مقصد صحیح نمی باشد . "
                MsgbOK.ShowDialog()
                btnMove.Focus()
                Exit Sub
            End If
        End If
        FillDatasetAndDataview()
        FillDataGridView()
    End Sub

    Private Sub txtHostName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHostName.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtDscrSw.Focus()
        End If
    End Sub

    Private Sub txtHostName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHostName.TextChanged

    End Sub
End Class