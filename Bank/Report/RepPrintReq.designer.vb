<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RepPrintReq
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RepPrintReq))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblRow = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnExcel = New System.Windows.Forms.Button
        Me.BtnOK = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.tsHeader = New System.Windows.Forms.ToolStrip
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel
        Me.btnExt = New System.Windows.Forms.ToolStripButton
        Me.btnRes = New System.Windows.Forms.ToolStripButton
        Me.btnMin = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripLabel
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Grp1 = New System.Windows.Forms.GroupBox
        Me.txtSerial = New System.Windows.Forms.TextBox
        Me.RB4 = New System.Windows.Forms.RadioButton
        Me.RB3 = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RB2 = New System.Windows.Forms.RadioButton
        Me.RB1 = New System.Windows.Forms.RadioButton
        Me.maskDatein = New System.Windows.Forms.MaskedTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.maskDateout = New System.Windows.Forms.MaskedTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCodeP = New System.Windows.Forms.TextBox
        Me.txtSumMbl = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.cboShob = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.tsHeader.SuspendLayout()
        Me.Grp1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeight = 35
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.InactiveCaption
        Me.DataGridView1.Location = New System.Drawing.Point(1, 27)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.RowHeadersWidth = 23
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.Window
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.DataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.DataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.DataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.WindowText
        Me.DataGridView1.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowTemplate.Height = 25
        Me.DataGridView1.RowTemplate.ReadOnly = True
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.Size = New System.Drawing.Size(780, 479)
        Me.DataGridView1.TabIndex = 78
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.GroupBox2.Controls.Add(Me.lblRow)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.btnExcel)
        Me.GroupBox2.Controls.Add(Me.BtnOK)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 522)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1000, 46)
        Me.GroupBox2.TabIndex = 80
        Me.GroupBox2.TabStop = False
        '
        'lblRow
        '
        Me.lblRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRow.BackColor = System.Drawing.Color.SlateGray
        Me.lblRow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRow.Location = New System.Drawing.Point(880, 16)
        Me.lblRow.Name = "lblRow"
        Me.lblRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRow.Size = New System.Drawing.Size(47, 20)
        Me.lblRow.TabIndex = 242
        Me.lblRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(933, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 241
        Me.Label10.Text = "تعداد رکورد"
        '
        'btnExcel
        '
        Me.btnExcel.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExcel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.Location = New System.Drawing.Point(140, 13)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnExcel.Size = New System.Drawing.Size(70, 28)
        Me.btnExcel.TabIndex = 6
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcel.UseVisualStyleBackColor = False
        '
        'BtnOK
        '
        Me.BtnOK.BackColor = System.Drawing.SystemColors.Control
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(74, 13)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(60, 28)
        Me.BtnOK.TabIndex = 5
        Me.BtnOK.Text = "تائیـــد"
        Me.BtnOK.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnExit.Location = New System.Drawing.Point(8, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(60, 28)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Location = New System.Drawing.Point(1, 505)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1017, 80)
        Me.Label6.TabIndex = 79
        '
        'tsHeader
        '
        Me.tsHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tsHeader.AutoSize = False
        Me.tsHeader.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.tsHeader.Dock = System.Windows.Forms.DockStyle.None
        Me.tsHeader.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripLabel4, Me.btnExt, Me.btnRes, Me.btnMin, Me.ToolStripButton1})
        Me.tsHeader.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tsHeader.Location = New System.Drawing.Point(9, 3)
        Me.tsHeader.Name = "tsHeader"
        Me.tsHeader.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tsHeader.Size = New System.Drawing.Size(1000, 21)
        Me.tsHeader.TabIndex = 81
        Me.tsHeader.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.AutoSize = False
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ToolStripLabel1.LinkColor = System.Drawing.SystemColors.ControlText
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(150, 15)
        Me.ToolStripLabel1.Text = "کنترل گزارش سرویس پرینتر"
        Me.ToolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.AutoSize = False
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(20, 15)
        '
        'btnExt
        '
        Me.btnExt.AutoSize = False
        Me.btnExt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnExt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExt.Image = CType(resources.GetObject("btnExt.Image"), System.Drawing.Image)
        Me.btnExt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExt.Name = "btnExt"
        Me.btnExt.Size = New System.Drawing.Size(15, 18)
        Me.btnExt.Text = "x"
        Me.btnExt.ToolTipText = "Exit"
        '
        'btnRes
        '
        Me.btnRes.AutoSize = False
        Me.btnRes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnRes.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnRes.Image = CType(resources.GetObject("btnRes.Image"), System.Drawing.Image)
        Me.btnRes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRes.Name = "btnRes"
        Me.btnRes.Size = New System.Drawing.Size(15, 18)
        Me.btnRes.Text = "o"
        Me.btnRes.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRes.ToolTipText = "Resize"
        '
        'btnMin
        '
        Me.btnMin.AutoSize = False
        Me.btnMin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnMin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMin.Image = CType(resources.GetObject("btnMin.Image"), System.Drawing.Image)
        Me.btnMin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnMin.Name = "btnMin"
        Me.btnMin.Size = New System.Drawing.Size(15, 18)
        Me.btnMin.Text = "_"
        Me.btnMin.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMin.ToolTipText = "Minimize"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.AutoSize = False
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(25, 20)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'txtName
        '
        Me.txtName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtName.Location = New System.Drawing.Point(6, 15)
        Me.txtName.MaxLength = 100
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(155, 21)
        Me.txtName.TabIndex = 1
        Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Grp1
        '
        Me.Grp1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grp1.Controls.Add(Me.txtSerial)
        Me.Grp1.Controls.Add(Me.RB4)
        Me.Grp1.Controls.Add(Me.RB3)
        Me.Grp1.Controls.Add(Me.txtName)
        Me.Grp1.Enabled = False
        Me.Grp1.Location = New System.Drawing.Point(787, 119)
        Me.Grp1.Name = "Grp1"
        Me.Grp1.Size = New System.Drawing.Size(222, 71)
        Me.Grp1.TabIndex = 457
        Me.Grp1.TabStop = False
        '
        'txtSerial
        '
        Me.txtSerial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerial.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtSerial.Location = New System.Drawing.Point(6, 41)
        Me.txtSerial.MaxLength = 100
        Me.txtSerial.Name = "txtSerial"
        Me.txtSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSerial.Size = New System.Drawing.Size(155, 21)
        Me.txtSerial.TabIndex = 8
        Me.txtSerial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RB4
        '
        Me.RB4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB4.AutoSize = True
        Me.RB4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RB4.Location = New System.Drawing.Point(162, 41)
        Me.RB4.Name = "RB4"
        Me.RB4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RB4.Size = New System.Drawing.Size(54, 17)
        Me.RB4.TabIndex = 7
        Me.RB4.Text = "سریال"
        Me.RB4.UseVisualStyleBackColor = True
        '
        'RB3
        '
        Me.RB3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB3.AutoSize = True
        Me.RB3.Checked = True
        Me.RB3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RB3.Location = New System.Drawing.Point(167, 15)
        Me.RB3.Name = "RB3"
        Me.RB3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RB3.Size = New System.Drawing.Size(49, 17)
        Me.RB3.TabIndex = 4
        Me.RB3.TabStop = True
        Me.RB3.Text = "پرینتر"
        Me.RB3.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RB2)
        Me.GroupBox1.Controls.Add(Me.RB1)
        Me.GroupBox1.Location = New System.Drawing.Point(895, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(114, 41)
        Me.GroupBox1.TabIndex = 458
        Me.GroupBox1.TabStop = False
        '
        'RB2
        '
        Me.RB2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB2.AutoSize = True
        Me.RB2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RB2.Location = New System.Drawing.Point(2, 15)
        Me.RB2.Name = "RB2"
        Me.RB2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RB2.Size = New System.Drawing.Size(55, 17)
        Me.RB2.TabIndex = 1
        Me.RB2.Text = "ادواری"
        Me.RB2.UseVisualStyleBackColor = True
        '
        'RB1
        '
        Me.RB1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB1.AutoSize = True
        Me.RB1.Checked = True
        Me.RB1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RB1.Location = New System.Drawing.Point(63, 15)
        Me.RB1.Name = "RB1"
        Me.RB1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RB1.Size = New System.Drawing.Size(45, 17)
        Me.RB1.TabIndex = 0
        Me.RB1.TabStop = True
        Me.RB1.Text = "کلی"
        Me.RB1.UseVisualStyleBackColor = True
        '
        'maskDatein
        '
        Me.maskDatein.AllowPromptAsInput = False
        Me.maskDatein.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.maskDatein.BackColor = System.Drawing.SystemColors.Window
        Me.maskDatein.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.maskDatein.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt
        Me.maskDatein.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.maskDatein.Location = New System.Drawing.Point(902, 74)
        Me.maskDatein.Mask = "1000/00/00"
        Me.maskDatein.Name = "maskDatein"
        Me.maskDatein.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.maskDatein.ResetOnSpace = False
        Me.maskDatein.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.maskDatein.Size = New System.Drawing.Size(66, 21)
        Me.maskDatein.TabIndex = 459
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label11.Location = New System.Drawing.Point(974, 76)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 13)
        Me.Label11.TabIndex = 460
        Me.Label11.Text = "از تاریخ"
        '
        'maskDateout
        '
        Me.maskDateout.AllowPromptAsInput = False
        Me.maskDateout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.maskDateout.BackColor = System.Drawing.SystemColors.Window
        Me.maskDateout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.maskDateout.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt
        Me.maskDateout.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.maskDateout.Location = New System.Drawing.Point(787, 73)
        Me.maskDateout.Mask = "1000/00/00"
        Me.maskDateout.Name = "maskDateout"
        Me.maskDateout.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.maskDateout.ResetOnSpace = False
        Me.maskDateout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.maskDateout.Size = New System.Drawing.Size(66, 21)
        Me.maskDateout.TabIndex = 461
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(859, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 462
        Me.Label4.Text = "تا تاریخ"
        '
        'txtCodeP
        '
        Me.txtCodeP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCodeP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCodeP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtCodeP.Location = New System.Drawing.Point(9, 471)
        Me.txtCodeP.MaxLength = 30
        Me.txtCodeP.Name = "txtCodeP"
        Me.txtCodeP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodeP.Size = New System.Drawing.Size(31, 21)
        Me.txtCodeP.TabIndex = 463
        '
        'txtSumMbl
        '
        Me.txtSumMbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSumMbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSumMbl.Location = New System.Drawing.Point(836, 477)
        Me.txtSumMbl.MaxLength = 15
        Me.txtSumMbl.Name = "txtSumMbl"
        Me.txtSumMbl.ReadOnly = True
        Me.txtSumMbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSumMbl.Size = New System.Drawing.Size(120, 21)
        Me.txtSumMbl.TabIndex = 464
        Me.txtSumMbl.Text = "0"
        Me.txtSumMbl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Window
        Me.Label12.Location = New System.Drawing.Point(962, 479)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 13)
        Me.Label12.TabIndex = 465
        Me.Label12.Text = "جمع مبلغ"
        '
        'cboShob
        '
        Me.cboShob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboShob.BackColor = System.Drawing.Color.LightGray
        Me.cboShob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShob.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboShob.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cboShob.FormattingEnabled = True
        Me.cboShob.Location = New System.Drawing.Point(787, 100)
        Me.cboShob.Name = "cboShob"
        Me.cboShob.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboShob.Size = New System.Drawing.Size(150, 21)
        Me.cboShob.TabIndex = 466
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(947, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(62, 13)
        Me.Label8.TabIndex = 467
        Me.Label8.Text = "محل فعالیت"
        '
        'RepPrintReq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(1018, 585)
        Me.ControlBox = False
        Me.Controls.Add(Me.cboShob)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSumMbl)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtCodeP)
        Me.Controls.Add(Me.maskDateout)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.maskDatein)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Grp1)
        Me.Controls.Add(Me.tsHeader)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DataGridView1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RepPrintReq"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.tsHeader.ResumeLayout(False)
        Me.tsHeader.PerformLayout()
        Me.Grp1.ResumeLayout(False)
        Me.Grp1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tsHeader As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnExt As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRes As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnMin As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripLabel4 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents SematCodeDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SematNameDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Grp1 As System.Windows.Forms.GroupBox
    Friend WithEvents RB4 As System.Windows.Forms.RadioButton
    Friend WithEvents RB3 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RB2 As System.Windows.Forms.RadioButton
    Friend WithEvents RB1 As System.Windows.Forms.RadioButton
    Friend WithEvents maskDatein As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents maskDateout As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSerial As System.Windows.Forms.TextBox
    Friend WithEvents txtCodeP As System.Windows.Forms.TextBox
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents lblRow As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSumMbl As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboShob As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
