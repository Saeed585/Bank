<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewResid
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewResid))
        Me.btnInsJPG = New System.Windows.Forms.Button
        Me.txtPic1 = New System.Windows.Forms.TextBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDelJPG = New System.Windows.Forms.Button
        Me.cboResid = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtResid = New System.Windows.Forms.TextBox
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.btnPrint = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnInsJPG
        '
        Me.btnInsJPG.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnInsJPG.BackColor = System.Drawing.SystemColors.Control
        Me.btnInsJPG.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInsJPG.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnInsJPG.Location = New System.Drawing.Point(184, 522)
        Me.btnInsJPG.Name = "btnInsJPG"
        Me.btnInsJPG.Size = New System.Drawing.Size(90, 28)
        Me.btnInsJPG.TabIndex = 213
        Me.btnInsJPG.Text = "ذخیره تصویر F2"
        Me.btnInsJPG.UseVisualStyleBackColor = False
        '
        'txtPic1
        '
        Me.txtPic1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPic1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtPic1.Location = New System.Drawing.Point(12, 453)
        Me.txtPic1.MaxLength = 20
        Me.txtPic1.Name = "txtPic1"
        Me.txtPic1.Size = New System.Drawing.Size(41, 21)
        Me.txtPic1.TabIndex = 474
        Me.txtPic1.Text = "0"
        Me.txtPic1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(572, 485)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 212
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Tag = ""
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(12, 522)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(70, 28)
        Me.btnExit.TabIndex = 475
        Me.btnExit.Text = "خروج Ext"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnDelJPG
        '
        Me.btnDelJPG.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelJPG.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelJPG.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelJPG.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelJPG.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDelJPG.Location = New System.Drawing.Point(88, 522)
        Me.btnDelJPG.Name = "btnDelJPG"
        Me.btnDelJPG.Size = New System.Drawing.Size(90, 28)
        Me.btnDelJPG.TabIndex = 476
        Me.btnDelJPG.Text = "حذف تصویر F4"
        Me.btnDelJPG.UseVisualStyleBackColor = False
        '
        'cboResid
        '
        Me.cboResid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboResid.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboResid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboResid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboResid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cboResid.FormattingEnabled = True
        Me.cboResid.Items.AddRange(New Object() {"شماره یک", "شماره دو", "شماره سه", "شماره چهار", "شماره پنج"})
        Me.cboResid.Location = New System.Drawing.Point(384, 527)
        Me.cboResid.Name = "cboResid"
        Me.cboResid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboResid.Size = New System.Drawing.Size(104, 21)
        Me.cboResid.TabIndex = 477
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label10.Location = New System.Drawing.Point(494, 530)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 13)
        Me.Label10.TabIndex = 478
        Me.Label10.Text = "رسید"
        '
        'txtResid
        '
        Me.txtResid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtResid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtResid.Location = New System.Drawing.Point(59, 453)
        Me.txtResid.MaxLength = 20
        Me.txtResid.Name = "txtResid"
        Me.txtResid.Size = New System.Drawing.Size(41, 21)
        Me.txtResid.TabIndex = 479
        Me.txtResid.Text = "0"
        Me.txtResid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TrackBar1
        '
        Me.TrackBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar1.AutoSize = False
        Me.TrackBar1.Location = New System.Drawing.Point(3, 489)
        Me.TrackBar1.Maximum = 50
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TrackBar1.Size = New System.Drawing.Size(573, 32)
        Me.TrackBar1.TabIndex = 480
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.SystemColors.Control
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.Location = New System.Drawing.Point(280, 522)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(98, 28)
        Me.btnPrint.TabIndex = 481
        Me.btnPrint.Text = "چاپ تصویر"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'PrintDocument1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.AllowSelection = True
        Me.PrintDialog1.AllowSomePages = True
        Me.PrintDialog1.UseEXDialog = True
        '
        'ViewResid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(578, 553)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.txtResid)
        Me.Controls.Add(Me.cboResid)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnDelJPG)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.txtPic1)
        Me.Controls.Add(Me.btnInsJPG)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ViewResid"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnInsJPG As System.Windows.Forms.Button
    Friend WithEvents txtPic1 As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnDelJPG As System.Windows.Forms.Button
    Friend WithEvents cboResid As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtResid As System.Windows.Forms.TextBox
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
End Class
