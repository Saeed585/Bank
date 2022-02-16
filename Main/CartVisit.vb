Imports System.Data.SqlClient
Imports System.IO
Public Class Cartvisit
    Dim Str_7 As String = ""
    Dim Pic As Byte()

    Private Sub ReadPicture()
        '-----------------------------------فراخوانی تصویر
        Dim objdataadapter As New SqlDataAdapter _
            (" SELECT PicCode1, Pic1, PicCode2, Pic2 FROM Phn.Phone WHERE (Row = " & Phone.txtRow.Text & ")", objConnection)
        objDataset = New DataSet
        objdataadapter.Fill(objDataset, "Phn.Phone")
        objDataviewT = New DataView(objDataset.Tables("Phn.Phone"))
        objCurrencyManager = CType(Me.BindingContext(objDataviewT), CurrencyManager)

        txtPic1.Text = 0
        txtPic1.DataBindings.Clear()
        txtPic1.DataBindings.Add("Text", objDataviewT, "PicCode1")
        If txtPic1.Text = "" Then
            txtPic1.Text = 0
        End If

        txtPic2.Text = 0
        txtPic2.DataBindings.Clear()
        txtPic2.DataBindings.Add("Text", objDataviewT, "PicCode2")
        If txtPic2.Text = "" Then
            txtPic2.Text = 0
        End If

        If txtPic1.Text <> 0 Then
            objConnection.Open()
            objCommand.CommandText = "Select * From Phn.Phone where(PicCode1=" & txtPic1.Text & ")"
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

        If txtPic2.Text <> 0 Then
            objConnection.Open()
            objCommand.CommandText = "Select * From Phn.Phone where(PicCode2=" & txtPic2.Text & ")"
            objCommand.Connection = objConnection

            Dim Reader2 As SqlDataReader = objCommand.ExecuteReader
            If Reader2.HasRows = True Then
                Reader2.Read()
                ' txt_show_path.Text = (reader.GetString(1))
                '========================================
                Me.PictureBox2.Image = Nothing
                Dim File2() As Byte
                File2 = Reader2("pic2")
                Dim MS2 As New MemoryStream()
                MS2.Write(File2, 0, File2.Length)
                If MS2.Length <> 4 Then
                    PictureBox2.Image = Bitmap.FromStream(MS2)
                End If
            Else
                MsgbOK.Label1.Text = " رکورد فوق فاقد عکس میباشد . "
                MsgbOK.ShowDialog()
            End If
            objConnection.Close()
        Else
            Me.PictureBox2.Image = Nothing
        End If
    End Sub

    Private Sub Cartvisit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Then
            Sav()
        ElseIf e.KeyCode = Keys.F4 Then
            Del()
        End If
    End Sub

    Private Sub CartVisit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReadPicture()
        txtPic1.SendToBack()
        txtPic2.SendToBack()

        Me.KeyPreview = True
    End Sub

    Private Sub Sav()
        '----------------------------------درج تصویر
        Dim open As New OpenFileDialog
        open.Filter = "Jpge|*.jpg|Gif|*.gif|Bitmap|*.bmp|PNG|*.png|All Images|*.bmp;*.gif;*.jpg;*.png"
        open.Title = "الصاق تصویر"
        open.Multiselect = False
        open.RestoreDirectory = True

        If open.ShowDialog = Windows.Forms.DialogResult.OK Then
            Str_7 = open.FileName
            If RB1.Checked = True Then
                PictureBox1.Image = Image.FromFile(Str_7)
                Randomize()
                txtPic1.Text = Int((1 + 500) * Rnd())
                Pic = IO.File.ReadAllBytes(Str_7)
                '----------------------------------ذخیره تصویر
                objCommand.Connection = objConnection
                objCommand.CommandText = "UPDATE Phn.Phone SET PicCode1 = @PicCode1, Pic1 = @Pic WHERE (Row = " & Phone.txtRow.Text & ")"
                objCommand.Parameters.Clear()
                objCommand.CommandType = CommandType.Text

                objCommand.Parameters.AddWithValue("@PicCode1", txtPic1.Text)
                objCommand.Parameters.AddWithValue("@Pic", Pic)
                objConnection.Open()
                objCommand.ExecuteNonQuery()
                objConnection.Close()
            ElseIf RB2.Checked = True Then
                PictureBox2.Image = Image.FromFile(Str_7)
                Randomize()
                txtPic2.Text = Int((1 + 500) * Rnd())
                Pic = IO.File.ReadAllBytes(Str_7)
                '----------------------------------ذخیره تصویر
                objCommand.Connection = objConnection
                objCommand.CommandText = "UPDATE Phn.Phone SET PicCode2 = @PicCode2, Pic2 = @Pic2 WHERE (Row = " & Phone.txtRow.Text & ")"
                objCommand.Parameters.Clear()
                objCommand.CommandType = CommandType.Text

                objCommand.Parameters.AddWithValue("@PicCode2", txtPic2.Text)
                objCommand.Parameters.AddWithValue("@Pic2", Pic)
                objConnection.Open()
                objCommand.ExecuteNonQuery()
                objConnection.Close()
            End If
        End If
    End Sub
    Private Sub btnInsJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsJPG.Click
        Sav()
    End Sub

    Private Sub Del()
        If RB1.Checked = True Then
            MsgbYN.Label1.Text = "  آیا از حذف تصویر اول اطمینان دارید ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.btnDelJPG.Focus()
                Exit Sub
            Else
                objCommand.Connection = objConnection
                objCommand.CommandText = "UPDATE Phn.Phone SET PicCode1 = @PicCode1, Pic1 = @Pic1 WHERE (Row = " & Phone.txtRow.Text & ")"
                objCommand.Parameters.Clear()
                objCommand.CommandType = CommandType.Text

                objCommand.Parameters.AddWithValue("@PicCode1", 0)
                objCommand.Parameters.AddWithValue("@Pic1", 0)
                objConnection.Open()
                objCommand.ExecuteNonQuery()
                objConnection.Close()

                Me.PictureBox1.Image = Nothing
                txtPic1.Text = 0
            End If
        ElseIf RB2.Checked = True Then
            MsgbYN.Label1.Text = "  آیا از حذف تصویر دوم اطمینان دارید ؟ "
            If MsgbYN.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Me.btnDelJPG.Focus()
                Exit Sub
            Else
                objCommand.Connection = objConnection
                objCommand.CommandText = "UPDATE Phn.Phone SET PicCode2 = @PicCode2, Pic2 = @Pic2 WHERE (Row = " & Phone.txtRow.Text & ")"
                objCommand.Parameters.Clear()
                objCommand.CommandType = CommandType.Text

                objCommand.Parameters.AddWithValue("@PicCode2", 0)
                objCommand.Parameters.AddWithValue("@Pic2", 0)
                objConnection.Open()
                objCommand.ExecuteNonQuery()
                objConnection.Close()

                Me.PictureBox2.Image = Nothing
                txtPic2.Text = 0
            End If
        End If
    End Sub

    Private Sub btnDelJPG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelJPG.Click
        Del()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub Cartvisit_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        'If e.Delta = 120 Then
        '    PictureBox1.Width = PictureBox1.Width + 8
        '    PictureBox1.Height = PictureBox1.Height + 5
        '    Me.Width += 8
        '    Me.Height += 5
        'ElseIf e.Delta = -120 Then
        '    PictureBox1.Width = PictureBox1.Width - 8
        '    PictureBox1.Height = PictureBox1.Height - 5
        '    Me.Width -= 8
        '    Me.Height -= 5
        'End If
    End Sub
End Class