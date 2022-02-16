<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Bank
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Bank))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.tsHeader = New System.Windows.Forms.ToolStrip
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel
        Me.btnExt = New System.Windows.Forms.ToolStripButton
        Me.btnRes = New System.Windows.Forms.ToolStripButton
        Me.btnMin = New System.Windows.Forms.ToolStripButton
        Me.btnRef = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripLabel
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtShob = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.txtShobCode = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cboBank = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.RB2 = New System.Windows.Forms.RadioButton
        Me.RB1 = New System.Windows.Forms.RadioButton
        Me.GRP1 = New System.Windows.Forms.GroupBox
        Me.txtDscr = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtSite = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPhon2 = New System.Windows.Forms.TextBox
        Me.txtKndcode = New System.Windows.Forms.TextBox
        Me.GRP2 = New System.Windows.Forms.GroupBox
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtAdr = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtPhon1 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtSrch = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.txtJar = New System.Windows.Forms.TextBox
        Me.txtAct = New System.Windows.Forms.TextBox
        Me.txtMabna = New System.Windows.Forms.TextBox
        Me.txtJr = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox2.SuspendLayout()
        Me.tsHeader.SuspendLayout()
        Me.GRP1.SuspendLayout()
        Me.GRP2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(303, 16)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 28)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "ذخیره F2"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.SystemColors.Control
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClear.Location = New System.Drawing.Point(84, 16)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(67, 28)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "اضافه Ins"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEdit.Location = New System.Drawing.Point(230, 16)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(67, 28)
        Me.btnEdit.TabIndex = 5
        Me.btnEdit.Text = "ویرایش F3"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(970, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "کد"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDelete.Location = New System.Drawing.Point(157, 16)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(67, 28)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "حذف F4"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Controls.Add(Me.btnEdit)
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 516)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1000, 52)
        Me.GroupBox2.TabIndex = 37
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(11, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(67, 28)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "خروج Esc"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(879, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "نام بانک"
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Location = New System.Drawing.Point(1, 505)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1017, 80)
        Me.Label3.TabIndex = 39
        '
        'txtName
        '
        Me.txtName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Location = New System.Drawing.Point(747, 15)
        Me.txtName.MaxLength = 30
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtName.Size = New System.Drawing.Size(126, 21)
        Me.txtName.TabIndex = 1
        '
        'tsHeader
        '
        Me.tsHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tsHeader.AutoSize = False
        Me.tsHeader.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tsHeader.Dock = System.Windows.Forms.DockStyle.None
        Me.tsHeader.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripLabel2, Me.btnExt, Me.btnRes, Me.btnMin, Me.btnRef, Me.ToolStripButton1})
        Me.tsHeader.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tsHeader.Location = New System.Drawing.Point(9, 3)
        Me.tsHeader.Name = "tsHeader"
        Me.tsHeader.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tsHeader.Size = New System.Drawing.Size(1000, 21)
        Me.tsHeader.TabIndex = 57
        Me.tsHeader.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.AutoSize = False
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ToolStripLabel1.LinkColor = System.Drawing.SystemColors.ControlText
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(150, 18)
        Me.ToolStripLabel1.Text = "تعریف بانک"
        Me.ToolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.AutoSize = False
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(20, 15)
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
        'btnRef
        '
        Me.btnRef.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRef.Image = CType(resources.GetObject("btnRef.Image"), System.Drawing.Image)
        Me.btnRef.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRef.Name = "btnRef"
        Me.btnRef.Size = New System.Drawing.Size(23, 20)
        Me.btnRef.Text = "Refresh"
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
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(804, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "نام شعبه"
        '
        'txtShob
        '
        Me.txtShob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShob.Location = New System.Drawing.Point(641, 46)
        Me.txtShob.MaxLength = 30
        Me.txtShob.Name = "txtShob"
        Me.txtShob.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtShob.Size = New System.Drawing.Size(157, 21)
        Me.txtShob.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Location = New System.Drawing.Point(928, 15)
        Me.txtCode.MaxLength = 2
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCode.Size = New System.Drawing.Size(36, 21)
        Me.txtCode.TabIndex = 40
        '
        'txtShobCode
        '
        Me.txtShobCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShobCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShobCode.Location = New System.Drawing.Point(860, 46)
        Me.txtShobCode.MaxLength = 10
        Me.txtShobCode.Name = "txtShobCode"
        Me.txtShobCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShobCode.Size = New System.Drawing.Size(77, 21)
        Me.txtShobCode.TabIndex = 169
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(943, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(48, 13)
        Me.Label8.TabIndex = 168
        Me.Label8.Text = "کد شعبه"
        '
        'cboBank
        '
        Me.cboBank.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboBank.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboBank.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cboBank.FormattingEnabled = True
        Me.cboBank.Location = New System.Drawing.Point(797, 19)
        Me.cboBank.Name = "cboBank"
        Me.cboBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboBank.Size = New System.Drawing.Size(157, 21)
        Me.cboBank.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label12.Location = New System.Drawing.Point(960, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(27, 13)
        Me.Label12.TabIndex = 166
        Me.Label12.Text = "بانک"
        '
        'RB2
        '
        Me.RB2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB2.AutoSize = True
        Me.RB2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RB2.Location = New System.Drawing.Point(3, 3)
        Me.RB2.Name = "RB2"
        Me.RB2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RB2.Size = New System.Drawing.Size(52, 17)
        Me.RB2.TabIndex = 1
        Me.RB2.Text = "شعبه"
        Me.RB2.UseVisualStyleBackColor = True
        '
        'RB1
        '
        Me.RB1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB1.AutoSize = True
        Me.RB1.Checked = True
        Me.RB1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RB1.Location = New System.Drawing.Point(61, 3)
        Me.RB1.Name = "RB1"
        Me.RB1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RB1.Size = New System.Drawing.Size(45, 17)
        Me.RB1.TabIndex = 0
        Me.RB1.TabStop = True
        Me.RB1.Text = "بانک"
        Me.RB1.UseVisualStyleBackColor = True
        '
        'GRP1
        '
        Me.GRP1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GRP1.Controls.Add(Me.txtDscr)
        Me.GRP1.Controls.Add(Me.Label32)
        Me.GRP1.Controls.Add(Me.Label1)
        Me.GRP1.Controls.Add(Me.txtSite)
        Me.GRP1.Controls.Add(Me.txtName)
        Me.GRP1.Controls.Add(Me.Label11)
        Me.GRP1.Controls.Add(Me.Label2)
        Me.GRP1.Controls.Add(Me.Label13)
        Me.GRP1.Controls.Add(Me.txtCode)
        Me.GRP1.Controls.Add(Me.txtPhon2)
        Me.GRP1.Location = New System.Drawing.Point(9, 54)
        Me.GRP1.Name = "GRP1"
        Me.GRP1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GRP1.Size = New System.Drawing.Size(1000, 105)
        Me.GRP1.TabIndex = 168
        Me.GRP1.TabStop = False
        '
        'txtDscr
        '
        Me.txtDscr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDscr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDscr.Location = New System.Drawing.Point(6, 43)
        Me.txtDscr.MaxLength = 100
        Me.txtDscr.Name = "txtDscr"
        Me.txtDscr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDscr.Size = New System.Drawing.Size(934, 21)
        Me.txtDscr.TabIndex = 243
        Me.txtDscr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label32.AutoSize = True
        Me.Label32.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label32.Location = New System.Drawing.Point(941, 48)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(47, 13)
        Me.Label32.TabIndex = 242
        Me.Label32.Text = "توضیحات"
        '
        'txtSite
        '
        Me.txtSite.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSite.Location = New System.Drawing.Point(129, 15)
        Me.txtSite.MaxLength = 100
        Me.txtSite.Name = "txtSite"
        Me.txtSite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSite.Size = New System.Drawing.Size(350, 21)
        Me.txtSite.TabIndex = 175
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(690, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(51, 13)
        Me.Label11.TabIndex = 172
        Me.Label11.Text = "تلفن بانک"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(483, 17)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(62, 13)
        Me.Label13.TabIndex = 174
        Me.Label13.Text = "آدرس سایت"
        '
        'txtPhon2
        '
        Me.txtPhon2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPhon2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhon2.Location = New System.Drawing.Point(551, 15)
        Me.txtPhon2.MaxLength = 15
        Me.txtPhon2.Name = "txtPhon2"
        Me.txtPhon2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhon2.Size = New System.Drawing.Size(126, 21)
        Me.txtPhon2.TabIndex = 173
        '
        'txtKndcode
        '
        Me.txtKndcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKndcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKndcode.Enabled = False
        Me.txtKndcode.Location = New System.Drawing.Point(73, 468)
        Me.txtKndcode.MaxLength = 2
        Me.txtKndcode.Name = "txtKndcode"
        Me.txtKndcode.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtKndcode.Size = New System.Drawing.Size(36, 21)
        Me.txtKndcode.TabIndex = 175
        Me.txtKndcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GRP2
        '
        Me.GRP2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GRP2.Controls.Add(Me.txtFax)
        Me.GRP2.Controls.Add(Me.Label31)
        Me.GRP2.Controls.Add(Me.txtAdr)
        Me.GRP2.Controls.Add(Me.Label14)
        Me.GRP2.Controls.Add(Me.txtPhon1)
        Me.GRP2.Controls.Add(Me.Label10)
        Me.GRP2.Controls.Add(Me.txtShobCode)
        Me.GRP2.Controls.Add(Me.cboBank)
        Me.GRP2.Controls.Add(Me.Label8)
        Me.GRP2.Controls.Add(Me.Label4)
        Me.GRP2.Controls.Add(Me.txtShob)
        Me.GRP2.Controls.Add(Me.Label12)
        Me.GRP2.Location = New System.Drawing.Point(12, 54)
        Me.GRP2.Name = "GRP2"
        Me.GRP2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GRP2.Size = New System.Drawing.Size(997, 105)
        Me.GRP2.TabIndex = 169
        Me.GRP2.TabStop = False
        Me.GRP2.Visible = False
        '
        'txtFax
        '
        Me.txtFax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Location = New System.Drawing.Point(351, 46)
        Me.txtFax.MaxLength = 15
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(102, 21)
        Me.txtFax.TabIndex = 179
        '
        'Label31
        '
        Me.Label31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label31.AutoSize = True
        Me.Label31.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label31.Location = New System.Drawing.Point(459, 48)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(34, 13)
        Me.Label31.TabIndex = 178
        Me.Label31.Text = "فاکس"
        '
        'txtAdr
        '
        Me.txtAdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAdr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdr.Location = New System.Drawing.Point(6, 73)
        Me.txtAdr.MaxLength = 200
        Me.txtAdr.Name = "txtAdr"
        Me.txtAdr.Size = New System.Drawing.Size(931, 21)
        Me.txtAdr.TabIndex = 177
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(959, 75)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(32, 13)
        Me.Label14.TabIndex = 176
        Me.Label14.Text = "آدرس"
        '
        'txtPhon1
        '
        Me.txtPhon1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPhon1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhon1.Location = New System.Drawing.Point(499, 46)
        Me.txtPhon1.MaxLength = 15
        Me.txtPhon1.Name = "txtPhon1"
        Me.txtPhon1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhon1.Size = New System.Drawing.Size(102, 21)
        Me.txtPhon1.TabIndex = 171
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(607, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(28, 13)
        Me.Label10.TabIndex = 170
        Me.Label10.Text = "تلفن"
        '
        'txtSrch
        '
        Me.txtSrch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSrch.BackColor = System.Drawing.Color.Beige
        Me.txtSrch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSrch.Location = New System.Drawing.Point(809, 165)
        Me.txtSrch.MaxLength = 30
        Me.txtSrch.Name = "txtSrch"
        Me.txtSrch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSrch.Size = New System.Drawing.Size(157, 21)
        Me.txtSrch.TabIndex = 172
        Me.txtSrch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(967, 167)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 173
        Me.Label9.Text = "جستجو"
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
        Me.DataGridView1.Location = New System.Drawing.Point(1, 192)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
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
        Me.DataGridView1.Size = New System.Drawing.Size(1008, 310)
        Me.DataGridView1.TabIndex = 174
        Me.DataGridView1.TabStop = False
        '
        'txtJar
        '
        Me.txtJar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtJar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJar.Location = New System.Drawing.Point(20, 468)
        Me.txtJar.MaxLength = 30
        Me.txtJar.Name = "txtJar"
        Me.txtJar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJar.Size = New System.Drawing.Size(47, 21)
        Me.txtJar.TabIndex = 175
        '
        'txtAct
        '
        Me.txtAct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAct.Enabled = False
        Me.txtAct.Location = New System.Drawing.Point(115, 468)
        Me.txtAct.MaxLength = 2
        Me.txtAct.Name = "txtAct"
        Me.txtAct.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAct.Size = New System.Drawing.Size(36, 21)
        Me.txtAct.TabIndex = 180
        Me.txtAct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMabna
        '
        Me.txtMabna.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMabna.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMabna.Enabled = False
        Me.txtMabna.Location = New System.Drawing.Point(157, 468)
        Me.txtMabna.MaxLength = 2
        Me.txtMabna.Name = "txtMabna"
        Me.txtMabna.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtMabna.Size = New System.Drawing.Size(36, 21)
        Me.txtMabna.TabIndex = 293
        Me.txtMabna.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtJr
        '
        Me.txtJr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJr.Enabled = False
        Me.txtJr.Location = New System.Drawing.Point(199, 468)
        Me.txtJr.MaxLength = 2
        Me.txtJr.Name = "txtJr"
        Me.txtJr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtJr.Size = New System.Drawing.Size(36, 21)
        Me.txtJr.TabIndex = 294
        Me.txtJr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Beige
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.RB2)
        Me.Panel1.Controls.Add(Me.RB1)
        Me.Panel1.Location = New System.Drawing.Point(899, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(110, 24)
        Me.Panel1.TabIndex = 295
        '
        'Bank
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(1018, 585)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtJr)
        Me.Controls.Add(Me.txtMabna)
        Me.Controls.Add(Me.GRP1)
        Me.Controls.Add(Me.txtAct)
        Me.Controls.Add(Me.txtJar)
        Me.Controls.Add(Me.txtKndcode)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtSrch)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.tsHeader)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GRP2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Bank"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.GroupBox2.ResumeLayout(False)
        Me.tsHeader.ResumeLayout(False)
        Me.tsHeader.PerformLayout()
        Me.GRP1.ResumeLayout(False)
        Me.GRP1.PerformLayout()
        Me.GRP2.ResumeLayout(False)
        Me.GRP2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents tsHeader As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnExt As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRes As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnMin As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtShob As System.Windows.Forms.TextBox
    Friend WithEvents cboBank As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtShobCode As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents RB2 As System.Windows.Forms.RadioButton
    Friend WithEvents RB1 As System.Windows.Forms.RadioButton
    Friend WithEvents GRP1 As System.Windows.Forms.GroupBox
    Friend WithEvents GRP2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSrch As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPhon2 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPhon1 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAdr As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSite As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtKndcode As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtJar As System.Windows.Forms.TextBox
    Friend WithEvents txtAct As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtDscr As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtMabna As System.Windows.Forms.TextBox
    Friend WithEvents txtJr As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnRef As System.Windows.Forms.ToolStripButton
End Class
