Public Class Srch
    Private Sub btnExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExt.Click
        Me.Dispose()
    End Sub

    Private Sub Srch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSrch.Text = ""
        txtSrch.Focus()
        ' FormName = "Srch"
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub txtSrch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.Click

    End Sub

    Private Sub txtSrch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSrch.GotFocus
        txtSrch.SelectionStart = 0
        txtSrch.SelectionLength = Len(txtSrch.Text)
    End Sub

    Private Sub txtSrch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSrch.KeyPress
        Dim X As String
        If e.KeyChar = ChrW(13) Then
            If IntPosition <> 0 Then
                If objDataview.Count <> 0 Then
                    X = DataGridView1.CurrentCell.RowIndex
                    If Code_Soft = 1 Then
                        If FormName = "PersinfoPay" Then
                            PersinfoPay.txtID.Text = DataGridView1.Rows(X).Cells(0).Value
                            PersinfoPay.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            PersinfoPay.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            PersinfoPay.txtPhnwrk.Focus()
                        ElseIf FormName = "PrintSerial" Then
                            PrintSerial.txtCodeP.Text = DataGridView1.Rows(X).Cells(0).Value
                            PrintSerial.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            PrintSerial.txtSerial.Focus()
                        ElseIf FormName = "KalaPriod" Then
                            KalaPriod.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            KalaPriod.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            KalaPriod.txtMah.Focus()
                        End If
                    ElseIf Code_Soft = 15 Then
                        If FormName = "Computer" Then
                            Computer.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            Computer.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            Computer.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            Computer.maskDate.Focus()
                        ElseIf FormName = "LapTop" Then
                            LapTop.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            LapTop.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            LapTop.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            LapTop.maskDate.Focus()
                        ElseIf FormName = "KalaOut" Then
                            If Z = 1 Then
                                KalaOut.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                                KalaOut.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                                KalaOut.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                                KalaOut.txtKala.Focus()
                            ElseIf Z = 2 Then
                                KalaOut.txtKcode.Text = DataGridView1.Rows(X).Cells(0).Value
                                KalaOut.txtKala.Text = DataGridView1.Rows(X).Cells(1).Value
                                KalaOut.txtCountOrg.Text = DataGridView1.Rows(X).Cells(2).Value
                                KalaOut.txtRowMoj.Text = DataGridView1.Rows(X).Cells(3).Value
                                KalaOut.cboArea.Focus()
                            End If
                        ElseIf FormName = "Mobil" Then
                            Mobil.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            Mobil.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            Mobil.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            Mobil.maskDate.Focus()
                        ElseIf FormName = "Pos" Then
                            Pos.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            Pos.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            Pos.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            Pos.txtFamily.Focus()
                        ElseIf FormName = "Work" Then
                            Work.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            Work.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            Work.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            Work.txtProblem.Focus()
                        ElseIf FormName = "PrintReq" Then
                            PrintReq.txtCodeP.Text = DataGridView1.Rows(X).Cells(0).Value
                            PrintReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            PrintReq.txtSerial.Text = DataGridView1.Rows(X).Cells(2).Value
                            PrintReq.txtSerial.Focus()
                        ElseIf FormName = "KatrijReq" Then
                            KatrijReq.txtCodeK.Text = DataGridView1.Rows(X).Cells(0).Value
                            KatrijReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            KatrijReq.cboReq.Focus()
                        ElseIf FormName = "Mojoodi" Then
                            Mojoodi.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            Mojoodi.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            Mojoodi.txtCount.Focus()
                        ElseIf FormName = "UsedKala" Then
                            UsedKala.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            UsedKala.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            UsedKala.txtCount.Text = DataGridView1.Rows(X).Cells(2).Value
                            UsedKala.txtRowMoj.Text = DataGridView1.Rows(X).Cells(3).Value
                            UsedKala.txtCountout.Focus()
                        ElseIf FormName = "TamirKala" Then
                            TamirKala.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            TamirKala.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            TamirKala.txtCount.Text = DataGridView1.Rows(X).Cells(2).Value
                            TamirKala.txtRowMoj.Text = DataGridView1.Rows(X).Cells(3).Value
                            TamirKala.txtCount1.Focus()
                        ElseIf FormName = "TahvilKala" Then
                            If Z = 1 Then
                                TahvilKala.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                                TahvilKala.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                                TahvilKala.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                                TahvilKala.txtKName.Focus()
                            ElseIf Z = 2 Then
                                TahvilKala.txtKCode.Text = DataGridView1.Rows(X).Cells(0).Value
                                TahvilKala.txtKName.Text = DataGridView1.Rows(X).Cells(1).Value
                                TahvilKala.txtCount.Focus()
                            End If
                            '----------------------------------------گزارشات
                        ElseIf FormName = "RComputer" Then
                            RepComputer.maskCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepComputer.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            RepComputer.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            RepComputer.BtnOK.Focus()
                        ElseIf FormName = "RLapTop" Then
                            RepLaptop.maskCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepLaptop.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            RepLaptop.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            RepLaptop.BtnOK.Focus()
                        ElseIf FormName = "RepPos" Then
                            RepPos.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepPos.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            RepPos.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            RepPos.BtnOK.Focus()
                        ElseIf FormName = "RMobil" Then
                            RepMobil.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepMobil.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            RepMobil.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            RepMobil.BtnOK.Focus()
                        ElseIf FormName = "RepWork" Then
                            RepWork.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepWork.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            RepWork.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            RepWork.btnOk.Focus()
                        ElseIf FormName = "RepPrintReq" Then
                            If Z = 1 Then
                                RepPrintReq.txtCodeP.Text = DataGridView1.Rows(X).Cells(0).Value
                                RepPrintReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                                RepPrintReq.BtnOK.Focus()
                            ElseIf Z = 2 Then
                                RepPrintReq.txtSerial.Text = DataGridView1.Rows(X).Cells(0).Value
                                RepPrintReq.BtnOK.Focus()
                            End If
                        ElseIf FormName = "RepKatrijReq" Then
                            RepKatrijReq.txtCodeK.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepKatrijReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            RepKatrijReq.BtnOK.Focus()
                        End If
                    End If
                    Me.Dispose()
                End If
            End If
        End If
    End Sub

    Private Sub txtSrch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrch.TextChanged
        If FormName = "PrintReq" Or FormName = "PrintSerial" Then
            objDataview.RowFilter = " prn_name like '" & "**" & txtSrch.Text & "**" & "'"
            FillDataPrint()
        ElseIf FormName = "RepPrintReq" Then
            If Z = 1 Then
                objDataview.RowFilter = " prn_name like '" & "**" & txtSrch.Text & "**" & "'"
                FillDataPrint()
            ElseIf Z = 2 Then
                objDataview.RowFilter = " Serial like '" & "**" & txtSrch.Text & "**" & "'"
                FillDataSerial()
            End If
        ElseIf FormName = "KatrijReq" Or FormName = "RepKatrijReq" Then
            objDataview.RowFilter = " name like '" & "**" & txtSrch.Text & "**" & "'"
            FillDataKatrij()
            'ElseIf FormName = "BardashtKala" Then
            '    objDataview.RowFilter = " Nam like '" & "**" & txtSrch.Text & "**" & "'"
            '    FillDataMojoodi()
        ElseIf FormName = "Mojoodi" Or FormName = "UsedKala" Or FormName = "TamirKala" Or FormName = "KalaOut" Or FormName = "KalaPriod" Or FormName = "TahvilKala" Then
            If FormName = "KalaOut" Or FormName = "TahvilKala" Then
                If Z = 1 Then
                    objDataview.RowFilter = " pers_family like '" & "**" & txtSrch.Text & "**" & "'"
                    FillDataPers()
                ElseIf Z = 2 Then
                    objDataview.RowFilter = " kala_Name like '" & "**" & txtSrch.Text & "**" & "'"
                    FillDataKala()
                End If
            Else
                objDataview.RowFilter = " kala_Name like '" & "**" & txtSrch.Text & "**" & "'"
                FillDataKala()
            End If
        Else
            objDataview.RowFilter = " pers_family like '" & "**" & txtSrch.Text & "**" & "'"
            FillDataPers()
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim X As String
        If IntPosition <> 0 Then
            If objDataview.Count <> 0 Then
                X = DataGridView1.CurrentCell.RowIndex
                If Code_Soft = 1 Then
                    If FormName = "PersinfoPay" Then
                        PersinfoPay.txtID.Text = DataGridView1.Rows(X).Cells(0).Value
                        PersinfoPay.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        PersinfoPay.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        PersinfoPay.txtPhnwrk.Focus()
                    ElseIf FormName = "PrintSerial" Then
                        PrintSerial.txtCodeP.Text = DataGridView1.Rows(X).Cells(0).Value
                        PrintSerial.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        PrintSerial.txtSerial.Focus()
                    ElseIf FormName = "KalaPriod" Then
                        KalaPriod.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        KalaPriod.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        KalaPriod.txtMah.Focus()
                    End If
                ElseIf Code_Soft = 15 Then
                    If FormName = "Computer" Then
                        Computer.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        Computer.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        Computer.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        Computer.maskDate.Focus()
                    ElseIf FormName = "LapTop" Then
                        LapTop.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        LapTop.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        LapTop.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        LapTop.maskDate.Focus()
                    ElseIf FormName = "KalaOut" Then
                        If Z = 1 Then
                            KalaOut.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            KalaOut.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            KalaOut.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            KalaOut.txtKala.Focus()
                        ElseIf Z = 2 Then
                            KalaOut.txtKcode.Text = DataGridView1.Rows(X).Cells(0).Value
                            KalaOut.txtKala.Text = DataGridView1.Rows(X).Cells(1).Value
                            KalaOut.txtCountOrg.Text = DataGridView1.Rows(X).Cells(2).Value
                            KalaOut.txtRowMoj.Text = DataGridView1.Rows(X).Cells(3).Value
                            KalaOut.cboArea.Focus()
                        End If
                    ElseIf FormName = "Mobil" Then
                        Mobil.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        Mobil.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        Mobil.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        Mobil.maskDate.Focus()
                    ElseIf FormName = "Pos" Then
                        Pos.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        Pos.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        Pos.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        Pos.txtFamily.Focus()
                    ElseIf FormName = "Work" Then
                        Work.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        Work.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        Work.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        Work.txtProblem.Focus()
                    ElseIf FormName = "PrintReq" Then
                        PrintReq.txtCodeP.Text = DataGridView1.Rows(X).Cells(0).Value
                        PrintReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        PrintReq.txtSerial.Text = DataGridView1.Rows(X).Cells(2).Value
                        PrintReq.txtSerial.Focus()
                    ElseIf FormName = "KatrijReq" Then
                        KatrijReq.txtCodeK.Text = DataGridView1.Rows(X).Cells(0).Value
                        KatrijReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        KatrijReq.cboReq.Focus()
                    ElseIf FormName = "Mojoodi" Then
                        Mojoodi.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        Mojoodi.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        Mojoodi.txtCount.Focus()
                    ElseIf FormName = "UsedKala" Then
                        UsedKala.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        UsedKala.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        UsedKala.txtCount.Text = DataGridView1.Rows(X).Cells(2).Value
                        UsedKala.txtRowMoj.Text = DataGridView1.Rows(X).Cells(3).Value
                        UsedKala.txtCountout.Focus()
                    ElseIf FormName = "TamirKala" Then
                        TamirKala.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        TamirKala.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        TamirKala.txtCount.Text = DataGridView1.Rows(X).Cells(2).Value
                        TamirKala.txtRowMoj.Text = DataGridView1.Rows(X).Cells(3).Value
                        TamirKala.txtCount1.Focus()
                    ElseIf FormName = "TahvilKala" Then
                        If Z = 1 Then
                            TahvilKala.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            TahvilKala.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            TahvilKala.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                            TahvilKala.txtKName.Focus()
                        ElseIf Z = 2 Then
                            TahvilKala.txtKCode.Text = DataGridView1.Rows(X).Cells(0).Value
                            TahvilKala.txtKName.Text = DataGridView1.Rows(X).Cells(1).Value
                            TahvilKala.txtCount.Focus()
                        End If
                        '----------------------------------------گزارشات
                    ElseIf FormName = "RComputer" Then
                        RepComputer.maskCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        RepComputer.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        RepComputer.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        RepComputer.BtnOK.Focus()
                    ElseIf FormName = "RLapTop" Then
                        RepLaptop.maskCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        RepLaptop.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        RepLaptop.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        RepLaptop.BtnOK.Focus()
                    ElseIf FormName = "RepPos" Then
                        RepPos.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        RepPos.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        RepPos.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        RepPos.BtnOK.Focus()
                    ElseIf FormName = "RMobil" Then
                        RepMobil.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        RepMobil.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        RepMobil.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        RepMobil.BtnOK.Focus()
                    ElseIf FormName = "RepWork" Then
                        RepWork.txtCode.Text = DataGridView1.Rows(X).Cells(0).Value
                        RepWork.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        RepWork.txtFamily.Text = DataGridView1.Rows(X).Cells(2).Value
                        RepWork.btnOk.Focus()
                    ElseIf FormName = "RepPrintReq" Then
                        If Z = 1 Then
                            RepPrintReq.txtCodeP.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepPrintReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                            RepPrintReq.BtnOK.Focus()
                        ElseIf Z = 2 Then
                            RepPrintReq.txtSerial.Text = DataGridView1.Rows(X).Cells(0).Value
                            RepPrintReq.BtnOK.Focus()
                        End If
                    ElseIf FormName = "RepKatrijReq" Then
                        RepKatrijReq.txtCodeK.Text = DataGridView1.Rows(X).Cells(0).Value
                        RepKatrijReq.txtName.Text = DataGridView1.Rows(X).Cells(1).Value
                        RepKatrijReq.BtnOK.Focus()
                    End If
                End If
                Me.Dispose()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class