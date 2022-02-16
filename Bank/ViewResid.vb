Imports System.Data.SqlClient
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing.Printing

Public Class ViewResid
    Dim Str_7 As String = ""
    Dim Pic As Byte()

    Private Sub ReadPicture()
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        '-----------------------------------فراخوانی تصویر
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT PicCode1, Pic1 FROM Bnk.ViewResid WHERE (Page = " & KalaOut.txtPage.Text & ") AND (Row = " & txtResid.Text & ") AND (Shob = " & Shob & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Bnk.ViewResid")
        objDataviewT = New DataView(objDataset.Tables("Bnk.ViewResid"))
        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)

        txtPic1.Text = 0
        txtPic1.DataBindings.Clear()
        txtPic1.DataBindings.Add("Text", objDataviewT, "PicCode1")
        If txtPic1.Text = "" Then
            txtPic1.Text = 0
        End If

        If txtPic1.Text <> 0 Then
            objConnection.Open()
            objCommand.CommandText = "Select * From Bnk.ViewResid where(PicCode1=" & txtPic1.Text & ") AND (Page = " & KalaOut.txtPage.Text & ") AND (Shob = " & Shob & ")"
            objCommand.Connection = objConnection

            Dim Reader1 As SqlDataReader = objCommand.ExecuteReader
            If Reader1.HasRows = True Then
                Reader1.Read()
                ' txt_show_path.Text = (reader.GetString(1))
                '========================================
                Me.PictureBox1.Image = Nothing
                Dim File1() As Byte
                File1 = Reader1("pic1")
                Dim MS1 As New MemoryStream()
                MS1.Write(File1, 0, File1.Length)
                If MS1.Length <> 4 Then
                    PictureBox1.Image = Bitmap.FromStream(MS1)
                End If
            Else
                MsgbOK.Label1.Text = " رکورد فوق فاقد عکس میباشد . "
                MsgbOK.ShowDialog()
            End If
            objConnection.Close()
        Else
            Me.PictureBox1.Image = Nothing
        End If
    End Sub

    Private Sub btnInsJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsJPG.Click
        Dim Dat As String
        Dim Sal As String
        Dim Page As String

        Sal = Mainfrm.cboSal_Mali.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString
        '----------------------------------درج تصویر
        Dim open As New OpenFileDialog
        open.Filter = "Jpge|*.jpg|Gif|*.gif|Bitmap|*.bmp|PNG|*.png|All Images|*.jpg;*.gif;*.bmp;*.png"
        open.Title = "الصاق تصویر"
        open.Multiselect = False
        open.RestoreDirectory = True

        If open.ShowDialog = Windows.Forms.DialogResult.OK Then
            Str_7 = open.FileName
            PictureBox1.Image = Image.FromFile(Str_7)
            Randomize()
            txtPic1.Text = Int((1 + 500) * Rnd())
            Pic = IO.File.ReadAllBytes(Str_7)
            '----------------------------------ذخیره تصویر
            Page = KalaOut.txtPage.Text
            Dat = KalaOut.maskDate.Text

            Dim objDataAdapter As New SqlDataAdapter _
                (" SELECT Row FROM Bnk.ViewResid WHERE (Page = " & Page & ") AND (Row = " & txtResid.Text & ") AND (Shob = " & Shob & ")", objConnection)
            objDataset = New DataSet
            objDataAdapter.Fill(objDataset, "Bnk.ViewResid")
            objDataview = New DataView(objDataset.Tables("Bnk.ViewResid"))
            objCurrencyManager = CType(Me.BindingContext(objDataview), CurrencyManager)
            objDataview.Sort = "Row"

            If objDataview.Count = 0 Then
                objCommand.Connection = objConnection
                objCommand.CommandText = _
                    " INSERT INTO Bnk.ViewResid (Page, Row, Dat, PicCode1, Pic1, Shob, Sal)" & _
                    " VALUES (@Page, @Row, @Dat, @PicCode1, @Pic, @Shob, @Sal)"
                objCommand.Parameters.Clear()
                objCommand.CommandType = CommandType.Text

                objCommand.Parameters.AddWithValue("@Page", Page)
                objCommand.Parameters.AddWithValue("@Row", txtResid.Text)
                objCommand.Parameters.AddWithValue("@Dat", Dat)
                objCommand.Parameters.AddWithValue("@PicCode1", txtPic1.Text)
                objCommand.Parameters.AddWithValue("@Pic", Pic)
                objCommand.Parameters.AddWithValue("@Shob", Shob)
                objCommand.Parameters.AddWithValue("@Sal", Sal)

                objConnection.Open()
                objCommand.ExecuteNonQuery()
                objConnection.Close()
            Else
                MsgbOK.Label1.Text = " رسید شماره " & txtResid.Text & " قبلا ثبت شده است . "
                MsgbOK.ShowDialog()
                btnDelJPG.Focus()
                Exit Sub
            End If

        End If
    End Sub

    Private Sub ViewResid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboResid.SelectedIndex = 0
        txtResid.Text = cboResid.SelectedIndex + 1
        ReadPicture()
        txtPic1.SendToBack()
        txtResid.SendToBack()
    End Sub

    Private Sub ViewResid_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If e.Delta = 120 Then
            If Me.Height <= 800 Then
                PictureBox1.Width = PictureBox1.Width + 8
                PictureBox1.Height = PictureBox1.Height + 5
                Me.Width += 8
                Me.Height += 5
            End If
        ElseIf e.Delta = -120 Then
            If Me.Height >= 560 Then
                PictureBox1.Width = PictureBox1.Width - 8
                PictureBox1.Height = PictureBox1.Height - 5
                Me.Width -= 8
                Me.Height -= 5
            End If
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnDelJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelJPG.Click
        Dim objCommand As New SqlCommand
        Dim Page As String

        Page = KalaOut.txtPage.Text
        Shob = Mainfrm.cboShob.ComboBox.SelectedValue.ToString

        If KalaOut.txtPage.Text = "" Then
            MsgbOK.Label1.Text = " لطفا شماره برگه را وارد کنید . "
            MsgbOK.ShowDialog()
            btnDelJPG.Focus()
            Exit Sub
        End If
        MsgbYN.Label1.Text = "  آیا از حذف رسید " & cboResid.Text & " اطمینان دارید ؟ "
        If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Me.btnDelJPG.Focus()
            Exit Sub
        Else
            objCommand.Connection = objConnection
            objCommand.CommandText = _
                "DELETE FROM Bnk.ViewResid WHERE (Page = " & Page & ") AND (Row = " & txtResid.Text & ") AND (Shob = " & Shob & ")"
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

            Me.PictureBox1.Image = Nothing
            txtPic1.Text = 0
        End If
    End Sub

    Private Sub cboFact_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboResid.SelectedIndexChanged

    End Sub

    Private Sub cboFact_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboResid.SelectedValueChanged
        Me.PictureBox1.Image = Nothing
        txtPic1.Text = 0
        txtResid.Text = cboResid.SelectedIndex + 1
        ReadPicture()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        btnInsJPG.Focus()
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        PictureBox1.Width = TrackBar1.Value + 572
        PictureBox1.Height = TrackBar1.Value + 484
        Me.Width = PictureBox1.Width + 11
        Me.Height = PictureBox1.Height + 97
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim newMargins As System.Drawing.Printing.Margins
        newMargins = New System.Drawing.Printing.Margins(0, 0, 0, 0)
        PrintDocument1.DefaultPageSettings.Margins = newMargins
        e.Graphics.DrawImage(PictureBox1.Image, 0, 0)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub
End Class