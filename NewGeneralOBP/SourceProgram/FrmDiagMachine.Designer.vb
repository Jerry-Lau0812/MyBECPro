<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDiagMachine
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabPageInterface = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioInt_UUT4 = New System.Windows.Forms.RadioButton()
        Me.RadioInt_UUT3 = New System.Windows.Forms.RadioButton()
        Me.RadioInt_UUT2 = New System.Windows.Forms.RadioButton()
        Me.RadioInt_UUT1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PicOpen = New System.Windows.Forms.PictureBox()
        Me.ButOpen = New System.Windows.Forms.Button()
        Me.GroupBoxErrRemark = New System.Windows.Forms.GroupBox()
        Me.lblError = New System.Windows.Forms.Label()
        Me.GroupBoxCmds = New System.Windows.Forms.GroupBox()
        Me.TxtInt_EnterTestMode = New System.Windows.Forms.TextBox()
        Me.ButInt_EnterTestMode = New System.Windows.Forms.Button()
        Me.TxtInt1 = New System.Windows.Forms.TextBox()
        Me.ButInt_DLLVersion = New System.Windows.Forms.Button()
        Me.TabPagePIC = New System.Windows.Forms.TabPage()
        Me.TabCtrlPIC = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.RadioPIC_UUT4 = New System.Windows.Forms.RadioButton()
        Me.RadioPIC_UUT3 = New System.Windows.Forms.RadioButton()
        Me.RadioPIC_UUT2 = New System.Windows.Forms.RadioButton()
        Me.RadioPIC_UUT1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.But_FingerPanel = New System.Windows.Forms.Button()
        Me.Pic_FingerUpDown = New System.Windows.Forms.PictureBox()
        Me.GroupPowerON = New System.Windows.Forms.GroupBox()
        Me.ButPower = New System.Windows.Forms.Button()
        Me.PicPower = New System.Windows.Forms.PictureBox()
        Me.TabCtrlPICPanel = New System.Windows.Forms.TabControl()
        Me.TabPageDCRly = New System.Windows.Forms.TabPage()
        Me.Diag_DCRelayPanel4 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel()
        Me.Diag_DCRelayPanel3 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel()
        Me.Diag_DCRelayPanel2 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel()
        Me.Diag_DCRelayPanel1 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel()
        Me.TabPageSwitch = New System.Windows.Forms.TabPage()
        Me.Diag_SwitchPanel2 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_SwitchPanel()
        Me.Diag_SwitchPanel1 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_SwitchPanel()
        Me.TabPageACOut = New System.Windows.Forms.TabPage()
        Me.Diag_ACOutPanel4 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel()
        Me.Diag_ACOutPanel3 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel()
        Me.Diag_ACOutPanel2 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel()
        Me.Diag_ACOutPanel1 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel()
        Me.TabPageMotor = New System.Windows.Forms.TabPage()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.TxtMotorCommand = New System.Windows.Forms.TextBox()
        Me.ButMotor_Clear = New System.Windows.Forms.Button()
        Me.ButMotor_Send = New System.Windows.Forms.Button()
        Me.ListMotor = New System.Windows.Forms.ListBox()
        Me.GroupMotorTools = New System.Windows.Forms.GroupBox()
        Me.ButMotor_ECHOCount = New System.Windows.Forms.Button()
        Me.ButMotor_ReadTorque = New System.Windows.Forms.Button()
        Me.LblMotor_Zero = New System.Windows.Forms.Label()
        Me.ButMotor_Unlock = New System.Windows.Forms.Button()
        Me.ButMotor_Run1Loop = New System.Windows.Forms.Button()
        Me.ButMotor_GoZero = New System.Windows.Forms.Button()
        Me.ButMotor_ReadCount = New System.Windows.Forms.Button()
        Me.ButMotor_SetAsZero = New System.Windows.Forms.Button()
        Me.ButMotor_GotoPos = New System.Windows.Forms.Button()
        Me.NumMotor_Position = New System.Windows.Forms.NumericUpDown()
        Me.GroupBoxMotorSelect = New System.Windows.Forms.GroupBox()
        Me.CombMotorCom = New System.Windows.Forms.ComboBox()
        Me.CombMotorIndex = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupReady = New System.Windows.Forms.GroupBox()
        Me.ButPIC_Down = New System.Windows.Forms.Button()
        Me.PicReady = New System.Windows.Forms.PictureBox()
        Me.ButPIC_Up = New System.Windows.Forms.Button()
        Me.ButReady = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ChKAppend = New System.Windows.Forms.CheckBox()
        Me.LblInfo = New System.Windows.Forms.Label()
        Me.ListPicCmd = New System.Windows.Forms.ListBox()
        Me.ListRecieved = New System.Windows.Forms.ListBox()
        Me.ButClear = New System.Windows.Forms.Button()
        Me.TxtValue = New System.Windows.Forms.TextBox()
        Me.ButSend = New System.Windows.Forms.Button()
        Me.TxtCmd = New System.Windows.Forms.TextBox()
        Me.TabCtrlMain = New System.Windows.Forms.TabControl()
        Me.TabPageADAM = New System.Windows.Forms.TabPage()
        Me.Diag_ADAMPanel1 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_ADAMPanel()
        Me.TabPageACMeter = New System.Windows.Forms.TabPage()
        Me.Diag_ACMeterPanel1 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACMeterPanel()
        Me.TabPage34972A = New System.Windows.Forms.TabPage()
        Me.Diag_KS34972APanel1 = New Bitron3DEQ.FVTRackBoardManager.UI.Diag_KS34972APanel()
        Me.TabPageFlashRunner = New System.Windows.Forms.TabPage()
        Me.Txt_FlahInfo = New System.Windows.Forms.TextBox()
        Me.Txt_FW_Project = New System.Windows.Forms.TextBox()
        Me.Btn_StartFlash = New System.Windows.Forms.Button()
        Me.Txt_FlashVer = New System.Windows.Forms.TextBox()
        Me.Btn_GetFVer = New System.Windows.Forms.Button()
        Me.Pic_FlashInter = New System.Windows.Forms.PictureBox()
        Me.Btn_ConnectFlashRunner = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPageInterface.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PicOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxErrRemark.SuspendLayout()
        Me.GroupBoxCmds.SuspendLayout()
        Me.TabPagePIC.SuspendLayout()
        Me.TabCtrlPIC.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.Pic_FingerUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPowerON.SuspendLayout()
        CType(Me.PicPower, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCtrlPICPanel.SuspendLayout()
        Me.TabPageDCRly.SuspendLayout()
        Me.TabPageSwitch.SuspendLayout()
        Me.TabPageACOut.SuspendLayout()
        Me.TabPageMotor.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupMotorTools.SuspendLayout()
        CType(Me.NumMotor_Position, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxMotorSelect.SuspendLayout()
        Me.GroupReady.SuspendLayout()
        CType(Me.PicReady, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabCtrlMain.SuspendLayout()
        Me.TabPageADAM.SuspendLayout()
        Me.TabPageACMeter.SuspendLayout()
        Me.TabPage34972A.SuspendLayout()
        Me.TabPageFlashRunner.SuspendLayout()
        CType(Me.Pic_FlashInter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabPageInterface
        '
        Me.TabPageInterface.Controls.Add(Me.GroupBox3)
        Me.TabPageInterface.Controls.Add(Me.GroupBox2)
        Me.TabPageInterface.Controls.Add(Me.GroupBoxErrRemark)
        Me.TabPageInterface.Controls.Add(Me.GroupBoxCmds)
        Me.TabPageInterface.Location = New System.Drawing.Point(4, 22)
        Me.TabPageInterface.Name = "TabPageInterface"
        Me.TabPageInterface.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageInterface.Size = New System.Drawing.Size(730, 494)
        Me.TabPageInterface.TabIndex = 4
        Me.TabPageInterface.Text = "Interface"
        Me.TabPageInterface.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.RadioInt_UUT4)
        Me.GroupBox3.Controls.Add(Me.RadioInt_UUT3)
        Me.GroupBox3.Controls.Add(Me.RadioInt_UUT2)
        Me.GroupBox3.Controls.Add(Me.RadioInt_UUT1)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(123, 55)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "UUT Selector"
        '
        'RadioInt_UUT4
        '
        Me.RadioInt_UUT4.AutoSize = True
        Me.RadioInt_UUT4.Location = New System.Drawing.Point(65, 35)
        Me.RadioInt_UUT4.Name = "RadioInt_UUT4"
        Me.RadioInt_UUT4.Size = New System.Drawing.Size(47, 16)
        Me.RadioInt_UUT4.TabIndex = 24
        Me.RadioInt_UUT4.Tag = "3"
        Me.RadioInt_UUT4.Text = "UUT4"
        Me.RadioInt_UUT4.UseVisualStyleBackColor = True
        '
        'RadioInt_UUT3
        '
        Me.RadioInt_UUT3.AutoSize = True
        Me.RadioInt_UUT3.Location = New System.Drawing.Point(9, 35)
        Me.RadioInt_UUT3.Name = "RadioInt_UUT3"
        Me.RadioInt_UUT3.Size = New System.Drawing.Size(47, 16)
        Me.RadioInt_UUT3.TabIndex = 23
        Me.RadioInt_UUT3.Tag = "2"
        Me.RadioInt_UUT3.Text = "UUT3"
        Me.RadioInt_UUT3.UseVisualStyleBackColor = True
        '
        'RadioInt_UUT2
        '
        Me.RadioInt_UUT2.AutoSize = True
        Me.RadioInt_UUT2.Location = New System.Drawing.Point(65, 18)
        Me.RadioInt_UUT2.Name = "RadioInt_UUT2"
        Me.RadioInt_UUT2.Size = New System.Drawing.Size(47, 16)
        Me.RadioInt_UUT2.TabIndex = 22
        Me.RadioInt_UUT2.Tag = "1"
        Me.RadioInt_UUT2.Text = "UUT2"
        Me.RadioInt_UUT2.UseVisualStyleBackColor = True
        '
        'RadioInt_UUT1
        '
        Me.RadioInt_UUT1.AutoSize = True
        Me.RadioInt_UUT1.Checked = True
        Me.RadioInt_UUT1.Location = New System.Drawing.Point(9, 18)
        Me.RadioInt_UUT1.Name = "RadioInt_UUT1"
        Me.RadioInt_UUT1.Size = New System.Drawing.Size(47, 16)
        Me.RadioInt_UUT1.TabIndex = 22
        Me.RadioInt_UUT1.TabStop = True
        Me.RadioInt_UUT1.Tag = "0"
        Me.RadioInt_UUT1.Text = "UUT1"
        Me.RadioInt_UUT1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.PicOpen)
        Me.GroupBox2.Controls.Add(Me.ButOpen)
        Me.GroupBox2.Location = New System.Drawing.Point(126, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(147, 55)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Open/Close Commnication"
        '
        'PicOpen
        '
        Me.PicOpen.Image = Global.GeneralPro_OBP.My.Resources.FVTResource.LED_Gray
        Me.PicOpen.Location = New System.Drawing.Point(110, 24)
        Me.PicOpen.Name = "PicOpen"
        Me.PicOpen.Size = New System.Drawing.Size(31, 25)
        Me.PicOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicOpen.TabIndex = 5
        Me.PicOpen.TabStop = False
        '
        'ButOpen
        '
        Me.ButOpen.Location = New System.Drawing.Point(6, 22)
        Me.ButOpen.Name = "ButOpen"
        Me.ButOpen.Size = New System.Drawing.Size(98, 27)
        Me.ButOpen.TabIndex = 2
        Me.ButOpen.Text = "Connect"
        Me.ButOpen.UseVisualStyleBackColor = True
        '
        'GroupBoxErrRemark
        '
        Me.GroupBoxErrRemark.Controls.Add(Me.lblError)
        Me.GroupBoxErrRemark.Location = New System.Drawing.Point(3, 434)
        Me.GroupBoxErrRemark.Name = "GroupBoxErrRemark"
        Me.GroupBoxErrRemark.Size = New System.Drawing.Size(719, 54)
        Me.GroupBoxErrRemark.TabIndex = 4
        Me.GroupBoxErrRemark.TabStop = False
        Me.GroupBoxErrRemark.Text = "Error Description"
        '
        'lblError
        '
        Me.lblError.BackColor = System.Drawing.Color.White
        Me.lblError.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblError.Location = New System.Drawing.Point(3, 17)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(713, 34)
        Me.lblError.TabIndex = 0
        Me.lblError.Text = "Error:"
        '
        'GroupBoxCmds
        '
        Me.GroupBoxCmds.Controls.Add(Me.TxtInt_EnterTestMode)
        Me.GroupBoxCmds.Controls.Add(Me.ButInt_EnterTestMode)
        Me.GroupBoxCmds.Controls.Add(Me.TxtInt1)
        Me.GroupBoxCmds.Controls.Add(Me.ButInt_DLLVersion)
        Me.GroupBoxCmds.Enabled = False
        Me.GroupBoxCmds.Location = New System.Drawing.Point(3, 55)
        Me.GroupBoxCmds.Name = "GroupBoxCmds"
        Me.GroupBoxCmds.Size = New System.Drawing.Size(719, 373)
        Me.GroupBoxCmds.TabIndex = 8
        Me.GroupBoxCmds.TabStop = False
        Me.GroupBoxCmds.Text = " "
        '
        'TxtInt_EnterTestMode
        '
        Me.TxtInt_EnterTestMode.Location = New System.Drawing.Point(123, 22)
        Me.TxtInt_EnterTestMode.Name = "TxtInt_EnterTestMode"
        Me.TxtInt_EnterTestMode.Size = New System.Drawing.Size(141, 21)
        Me.TxtInt_EnterTestMode.TabIndex = 18
        '
        'ButInt_EnterTestMode
        '
        Me.ButInt_EnterTestMode.Location = New System.Drawing.Point(9, 18)
        Me.ButInt_EnterTestMode.Name = "ButInt_EnterTestMode"
        Me.ButInt_EnterTestMode.Size = New System.Drawing.Size(104, 27)
        Me.ButInt_EnterTestMode.TabIndex = 17
        Me.ButInt_EnterTestMode.Text = "Enter Test Mode"
        Me.ButInt_EnterTestMode.UseVisualStyleBackColor = True
        '
        'TxtInt1
        '
        Me.TxtInt1.Location = New System.Drawing.Point(123, 54)
        Me.TxtInt1.Name = "TxtInt1"
        Me.TxtInt1.Size = New System.Drawing.Size(141, 21)
        Me.TxtInt1.TabIndex = 16
        '
        'ButInt_DLLVersion
        '
        Me.ButInt_DLLVersion.Location = New System.Drawing.Point(9, 50)
        Me.ButInt_DLLVersion.Name = "ButInt_DLLVersion"
        Me.ButInt_DLLVersion.Size = New System.Drawing.Size(104, 27)
        Me.ButInt_DLLVersion.TabIndex = 14
        Me.ButInt_DLLVersion.Text = "GetFwVersion"
        Me.ButInt_DLLVersion.UseVisualStyleBackColor = True
        '
        'TabPagePIC
        '
        Me.TabPagePIC.Controls.Add(Me.TabCtrlPIC)
        Me.TabPagePIC.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePIC.Name = "TabPagePIC"
        Me.TabPagePIC.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePIC.Size = New System.Drawing.Size(730, 494)
        Me.TabPagePIC.TabIndex = 0
        Me.TabPagePIC.Text = "PIC"
        Me.TabPagePIC.UseVisualStyleBackColor = True
        '
        'TabCtrlPIC
        '
        Me.TabCtrlPIC.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TabCtrlPIC.Controls.Add(Me.TabPage1)
        Me.TabCtrlPIC.Controls.Add(Me.TabPage2)
        Me.TabCtrlPIC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabCtrlPIC.Location = New System.Drawing.Point(3, 3)
        Me.TabCtrlPIC.Multiline = True
        Me.TabCtrlPIC.Name = "TabCtrlPIC"
        Me.TabCtrlPIC.SelectedIndex = 0
        Me.TabCtrlPIC.Size = New System.Drawing.Size(724, 488)
        Me.TabCtrlPIC.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.GroupPowerON)
        Me.TabPage1.Controls.Add(Me.TabCtrlPICPanel)
        Me.TabPage1.Controls.Add(Me.GroupReady)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(22, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(698, 480)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RadioPIC_UUT4)
        Me.GroupBox4.Controls.Add(Me.RadioPIC_UUT3)
        Me.GroupBox4.Controls.Add(Me.RadioPIC_UUT2)
        Me.GroupBox4.Controls.Add(Me.RadioPIC_UUT1)
        Me.GroupBox4.Location = New System.Drawing.Point(230, 1)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(142, 46)
        Me.GroupBox4.TabIndex = 9
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "UUT"
        '
        'RadioPIC_UUT4
        '
        Me.RadioPIC_UUT4.AutoSize = True
        Me.RadioPIC_UUT4.Location = New System.Drawing.Point(110, 19)
        Me.RadioPIC_UUT4.Name = "RadioPIC_UUT4"
        Me.RadioPIC_UUT4.Size = New System.Drawing.Size(29, 16)
        Me.RadioPIC_UUT4.TabIndex = 0
        Me.RadioPIC_UUT4.Tag = "3"
        Me.RadioPIC_UUT4.Text = "4"
        Me.RadioPIC_UUT4.UseVisualStyleBackColor = True
        '
        'RadioPIC_UUT3
        '
        Me.RadioPIC_UUT3.AutoSize = True
        Me.RadioPIC_UUT3.Location = New System.Drawing.Point(73, 19)
        Me.RadioPIC_UUT3.Name = "RadioPIC_UUT3"
        Me.RadioPIC_UUT3.Size = New System.Drawing.Size(29, 16)
        Me.RadioPIC_UUT3.TabIndex = 0
        Me.RadioPIC_UUT3.Tag = "2"
        Me.RadioPIC_UUT3.Text = "3"
        Me.RadioPIC_UUT3.UseVisualStyleBackColor = True
        '
        'RadioPIC_UUT2
        '
        Me.RadioPIC_UUT2.AutoSize = True
        Me.RadioPIC_UUT2.Location = New System.Drawing.Point(39, 19)
        Me.RadioPIC_UUT2.Name = "RadioPIC_UUT2"
        Me.RadioPIC_UUT2.Size = New System.Drawing.Size(29, 16)
        Me.RadioPIC_UUT2.TabIndex = 0
        Me.RadioPIC_UUT2.Tag = "1"
        Me.RadioPIC_UUT2.Text = "2"
        Me.RadioPIC_UUT2.UseVisualStyleBackColor = True
        '
        'RadioPIC_UUT1
        '
        Me.RadioPIC_UUT1.AutoSize = True
        Me.RadioPIC_UUT1.Checked = True
        Me.RadioPIC_UUT1.Location = New System.Drawing.Point(6, 19)
        Me.RadioPIC_UUT1.Name = "RadioPIC_UUT1"
        Me.RadioPIC_UUT1.Size = New System.Drawing.Size(29, 16)
        Me.RadioPIC_UUT1.TabIndex = 0
        Me.RadioPIC_UUT1.TabStop = True
        Me.RadioPIC_UUT1.Tag = "0"
        Me.RadioPIC_UUT1.Text = "1"
        Me.RadioPIC_UUT1.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.But_FingerPanel)
        Me.GroupBox6.Controls.Add(Me.Pic_FingerUpDown)
        Me.GroupBox6.Enabled = False
        Me.GroupBox6.Location = New System.Drawing.Point(406, 2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(141, 45)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Move Finger Panel"
        '
        'But_FingerPanel
        '
        Me.But_FingerPanel.Location = New System.Drawing.Point(6, 15)
        Me.But_FingerPanel.Name = "But_FingerPanel"
        Me.But_FingerPanel.Size = New System.Drawing.Size(92, 26)
        Me.But_FingerPanel.TabIndex = 10
        Me.But_FingerPanel.Text = "Move Finger"
        Me.But_FingerPanel.UseVisualStyleBackColor = True
        '
        'Pic_FingerUpDown
        '
        Me.Pic_FingerUpDown.Image = Global.GeneralPro_OBP.My.Resources.FVTResource.LED_Gray
        Me.Pic_FingerUpDown.Location = New System.Drawing.Point(104, 17)
        Me.Pic_FingerUpDown.Name = "Pic_FingerUpDown"
        Me.Pic_FingerUpDown.Size = New System.Drawing.Size(27, 23)
        Me.Pic_FingerUpDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pic_FingerUpDown.TabIndex = 1
        Me.Pic_FingerUpDown.TabStop = False
        '
        'GroupPowerON
        '
        Me.GroupPowerON.Controls.Add(Me.ButPower)
        Me.GroupPowerON.Controls.Add(Me.PicPower)
        Me.GroupPowerON.Enabled = False
        Me.GroupPowerON.Location = New System.Drawing.Point(553, 2)
        Me.GroupPowerON.Name = "GroupPowerON"
        Me.GroupPowerON.Size = New System.Drawing.Size(138, 45)
        Me.GroupPowerON.TabIndex = 4
        Me.GroupPowerON.TabStop = False
        Me.GroupPowerON.Text = "Power On PCB"
        '
        'ButPower
        '
        Me.ButPower.Location = New System.Drawing.Point(6, 14)
        Me.ButPower.Name = "ButPower"
        Me.ButPower.Size = New System.Drawing.Size(91, 27)
        Me.ButPower.TabIndex = 0
        Me.ButPower.Text = "Power ON"
        Me.ButPower.UseVisualStyleBackColor = True
        '
        'PicPower
        '
        Me.PicPower.Image = Global.GeneralPro_OBP.My.Resources.FVTResource.LED_Gray
        Me.PicPower.Location = New System.Drawing.Point(103, 17)
        Me.PicPower.Name = "PicPower"
        Me.PicPower.Size = New System.Drawing.Size(27, 23)
        Me.PicPower.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicPower.TabIndex = 1
        Me.PicPower.TabStop = False
        '
        'TabCtrlPICPanel
        '
        Me.TabCtrlPICPanel.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabCtrlPICPanel.Controls.Add(Me.TabPageDCRly)
        Me.TabCtrlPICPanel.Controls.Add(Me.TabPageSwitch)
        Me.TabCtrlPICPanel.Controls.Add(Me.TabPageACOut)
        Me.TabCtrlPICPanel.Controls.Add(Me.TabPageMotor)
        Me.TabCtrlPICPanel.Location = New System.Drawing.Point(6, 185)
        Me.TabCtrlPICPanel.Multiline = True
        Me.TabCtrlPICPanel.Name = "TabCtrlPICPanel"
        Me.TabCtrlPICPanel.SelectedIndex = 0
        Me.TabCtrlPICPanel.Size = New System.Drawing.Size(685, 289)
        Me.TabCtrlPICPanel.TabIndex = 8
        '
        'TabPageDCRly
        '
        Me.TabPageDCRly.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageDCRly.Controls.Add(Me.Diag_DCRelayPanel4)
        Me.TabPageDCRly.Controls.Add(Me.Diag_DCRelayPanel3)
        Me.TabPageDCRly.Controls.Add(Me.Diag_DCRelayPanel2)
        Me.TabPageDCRly.Controls.Add(Me.Diag_DCRelayPanel1)
        Me.TabPageDCRly.Location = New System.Drawing.Point(4, 4)
        Me.TabPageDCRly.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPageDCRly.Name = "TabPageDCRly"
        Me.TabPageDCRly.Size = New System.Drawing.Size(677, 263)
        Me.TabPageDCRly.TabIndex = 0
        Me.TabPageDCRly.Text = "Relay"
        Me.TabPageDCRly.UseVisualStyleBackColor = True
        '
        'Diag_DCRelayPanel4
        '
        Me.Diag_DCRelayPanel4.BackColor = System.Drawing.Color.Silver
        Me.Diag_DCRelayPanel4.Location = New System.Drawing.Point(2, 199)
        Me.Diag_DCRelayPanel4.Name = "Diag_DCRelayPanel4"
        Me.Diag_DCRelayPanel4.Size = New System.Drawing.Size(670, 61)
        Me.Diag_DCRelayPanel4.TabIndex = 3
        Me.Diag_DCRelayPanel4.Tag = "3"
        '
        'Diag_DCRelayPanel3
        '
        Me.Diag_DCRelayPanel3.BackColor = System.Drawing.Color.Silver
        Me.Diag_DCRelayPanel3.Location = New System.Drawing.Point(3, 133)
        Me.Diag_DCRelayPanel3.Name = "Diag_DCRelayPanel3"
        Me.Diag_DCRelayPanel3.Size = New System.Drawing.Size(670, 61)
        Me.Diag_DCRelayPanel3.TabIndex = 2
        Me.Diag_DCRelayPanel3.Tag = "2"
        '
        'Diag_DCRelayPanel2
        '
        Me.Diag_DCRelayPanel2.BackColor = System.Drawing.Color.Silver
        Me.Diag_DCRelayPanel2.Location = New System.Drawing.Point(3, 68)
        Me.Diag_DCRelayPanel2.Name = "Diag_DCRelayPanel2"
        Me.Diag_DCRelayPanel2.Size = New System.Drawing.Size(670, 61)
        Me.Diag_DCRelayPanel2.TabIndex = 1
        Me.Diag_DCRelayPanel2.Tag = "1"
        '
        'Diag_DCRelayPanel1
        '
        Me.Diag_DCRelayPanel1.BackColor = System.Drawing.Color.Silver
        Me.Diag_DCRelayPanel1.Location = New System.Drawing.Point(4, 4)
        Me.Diag_DCRelayPanel1.Name = "Diag_DCRelayPanel1"
        Me.Diag_DCRelayPanel1.Size = New System.Drawing.Size(668, 61)
        Me.Diag_DCRelayPanel1.TabIndex = 0
        Me.Diag_DCRelayPanel1.Tag = "0"
        '
        'TabPageSwitch
        '
        Me.TabPageSwitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageSwitch.Controls.Add(Me.Diag_SwitchPanel2)
        Me.TabPageSwitch.Controls.Add(Me.Diag_SwitchPanel1)
        Me.TabPageSwitch.Location = New System.Drawing.Point(4, 4)
        Me.TabPageSwitch.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPageSwitch.Name = "TabPageSwitch"
        Me.TabPageSwitch.Size = New System.Drawing.Size(677, 263)
        Me.TabPageSwitch.TabIndex = 3
        Me.TabPageSwitch.Text = "Switch"
        Me.TabPageSwitch.UseVisualStyleBackColor = True
        '
        'Diag_SwitchPanel2
        '
        Me.Diag_SwitchPanel2.BackColor = System.Drawing.Color.Silver
        Me.Diag_SwitchPanel2.Location = New System.Drawing.Point(6, 83)
        Me.Diag_SwitchPanel2.Name = "Diag_SwitchPanel2"
        Me.Diag_SwitchPanel2.Size = New System.Drawing.Size(668, 75)
        Me.Diag_SwitchPanel2.TabIndex = 3
        Me.Diag_SwitchPanel2.Tag = "1"
        '
        'Diag_SwitchPanel1
        '
        Me.Diag_SwitchPanel1.BackColor = System.Drawing.Color.Silver
        Me.Diag_SwitchPanel1.Location = New System.Drawing.Point(6, 3)
        Me.Diag_SwitchPanel1.Name = "Diag_SwitchPanel1"
        Me.Diag_SwitchPanel1.Size = New System.Drawing.Size(668, 75)
        Me.Diag_SwitchPanel1.TabIndex = 2
        Me.Diag_SwitchPanel1.Tag = "0"
        '
        'TabPageACOut
        '
        Me.TabPageACOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageACOut.Controls.Add(Me.Diag_ACOutPanel4)
        Me.TabPageACOut.Controls.Add(Me.Diag_ACOutPanel3)
        Me.TabPageACOut.Controls.Add(Me.Diag_ACOutPanel2)
        Me.TabPageACOut.Controls.Add(Me.Diag_ACOutPanel1)
        Me.TabPageACOut.Location = New System.Drawing.Point(4, 4)
        Me.TabPageACOut.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPageACOut.Name = "TabPageACOut"
        Me.TabPageACOut.Size = New System.Drawing.Size(677, 263)
        Me.TabPageACOut.TabIndex = 2
        Me.TabPageACOut.Text = "ACOutput"
        Me.TabPageACOut.UseVisualStyleBackColor = True
        '
        'Diag_ACOutPanel4
        '
        Me.Diag_ACOutPanel4.CycleReadTime = 250
        Me.Diag_ACOutPanel4.Location = New System.Drawing.Point(6, 155)
        Me.Diag_ACOutPanel4.Name = "Diag_ACOutPanel4"
        Me.Diag_ACOutPanel4.Size = New System.Drawing.Size(665, 45)
        Me.Diag_ACOutPanel4.TabIndex = 13
        Me.Diag_ACOutPanel4.Tag = "3"
        '
        'Diag_ACOutPanel3
        '
        Me.Diag_ACOutPanel3.CycleReadTime = 250
        Me.Diag_ACOutPanel3.Location = New System.Drawing.Point(6, 105)
        Me.Diag_ACOutPanel3.Name = "Diag_ACOutPanel3"
        Me.Diag_ACOutPanel3.Size = New System.Drawing.Size(665, 45)
        Me.Diag_ACOutPanel3.TabIndex = 12
        Me.Diag_ACOutPanel3.Tag = "2"
        '
        'Diag_ACOutPanel2
        '
        Me.Diag_ACOutPanel2.CycleReadTime = 250
        Me.Diag_ACOutPanel2.Location = New System.Drawing.Point(6, 54)
        Me.Diag_ACOutPanel2.Name = "Diag_ACOutPanel2"
        Me.Diag_ACOutPanel2.Size = New System.Drawing.Size(665, 45)
        Me.Diag_ACOutPanel2.TabIndex = 11
        Me.Diag_ACOutPanel2.Tag = "1"
        '
        'Diag_ACOutPanel1
        '
        Me.Diag_ACOutPanel1.CycleReadTime = 250
        Me.Diag_ACOutPanel1.Location = New System.Drawing.Point(6, 5)
        Me.Diag_ACOutPanel1.Name = "Diag_ACOutPanel1"
        Me.Diag_ACOutPanel1.Size = New System.Drawing.Size(665, 45)
        Me.Diag_ACOutPanel1.TabIndex = 10
        Me.Diag_ACOutPanel1.Tag = "0"
        '
        'TabPageMotor
        '
        Me.TabPageMotor.Controls.Add(Me.GroupBox7)
        Me.TabPageMotor.Controls.Add(Me.GroupMotorTools)
        Me.TabPageMotor.Controls.Add(Me.GroupBoxMotorSelect)
        Me.TabPageMotor.Location = New System.Drawing.Point(4, 4)
        Me.TabPageMotor.Name = "TabPageMotor"
        Me.TabPageMotor.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMotor.Size = New System.Drawing.Size(677, 263)
        Me.TabPageMotor.TabIndex = 4
        Me.TabPageMotor.Text = "Motor"
        Me.TabPageMotor.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.TxtMotorCommand)
        Me.GroupBox7.Controls.Add(Me.ButMotor_Clear)
        Me.GroupBox7.Controls.Add(Me.ButMotor_Send)
        Me.GroupBox7.Controls.Add(Me.ListMotor)
        Me.GroupBox7.Location = New System.Drawing.Point(254, 3)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(419, 257)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Terminal"
        '
        'TxtMotorCommand
        '
        Me.TxtMotorCommand.BackColor = System.Drawing.SystemColors.Info
        Me.TxtMotorCommand.Location = New System.Drawing.Point(6, 18)
        Me.TxtMotorCommand.Name = "TxtMotorCommand"
        Me.TxtMotorCommand.Size = New System.Drawing.Size(286, 21)
        Me.TxtMotorCommand.TabIndex = 2
        '
        'ButMotor_Clear
        '
        Me.ButMotor_Clear.Location = New System.Drawing.Point(371, 16)
        Me.ButMotor_Clear.Name = "ButMotor_Clear"
        Me.ButMotor_Clear.Size = New System.Drawing.Size(42, 21)
        Me.ButMotor_Clear.TabIndex = 3
        Me.ButMotor_Clear.Text = "Clear"
        Me.ButMotor_Clear.UseVisualStyleBackColor = True
        '
        'ButMotor_Send
        '
        Me.ButMotor_Send.Location = New System.Drawing.Point(298, 16)
        Me.ButMotor_Send.Name = "ButMotor_Send"
        Me.ButMotor_Send.Size = New System.Drawing.Size(67, 21)
        Me.ButMotor_Send.TabIndex = 3
        Me.ButMotor_Send.Text = "Send"
        Me.ButMotor_Send.UseVisualStyleBackColor = True
        '
        'ListMotor
        '
        Me.ListMotor.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ListMotor.ForeColor = System.Drawing.Color.Teal
        Me.ListMotor.FormattingEnabled = True
        Me.ListMotor.ItemHeight = 12
        Me.ListMotor.Location = New System.Drawing.Point(6, 43)
        Me.ListMotor.Name = "ListMotor"
        Me.ListMotor.Size = New System.Drawing.Size(407, 208)
        Me.ListMotor.TabIndex = 0
        '
        'GroupMotorTools
        '
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_ECHOCount)
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_ReadTorque)
        Me.GroupMotorTools.Controls.Add(Me.LblMotor_Zero)
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_Unlock)
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_Run1Loop)
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_GoZero)
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_ReadCount)
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_SetAsZero)
        Me.GroupMotorTools.Controls.Add(Me.ButMotor_GotoPos)
        Me.GroupMotorTools.Controls.Add(Me.NumMotor_Position)
        Me.GroupMotorTools.Location = New System.Drawing.Point(6, 49)
        Me.GroupMotorTools.Name = "GroupMotorTools"
        Me.GroupMotorTools.Size = New System.Drawing.Size(242, 210)
        Me.GroupMotorTools.TabIndex = 2
        Me.GroupMotorTools.TabStop = False
        Me.GroupMotorTools.Text = "Position Position"
        '
        'ButMotor_ECHOCount
        '
        Me.ButMotor_ECHOCount.Location = New System.Drawing.Point(6, 133)
        Me.ButMotor_ECHOCount.Name = "ButMotor_ECHOCount"
        Me.ButMotor_ECHOCount.Size = New System.Drawing.Size(227, 21)
        Me.ButMotor_ECHOCount.TabIndex = 4
        Me.ButMotor_ECHOCount.Text = "Echo RS Count"
        Me.ButMotor_ECHOCount.UseVisualStyleBackColor = True
        '
        'ButMotor_ReadTorque
        '
        Me.ButMotor_ReadTorque.Location = New System.Drawing.Point(126, 106)
        Me.ButMotor_ReadTorque.Name = "ButMotor_ReadTorque"
        Me.ButMotor_ReadTorque.Size = New System.Drawing.Size(107, 21)
        Me.ButMotor_ReadTorque.TabIndex = 3
        Me.ButMotor_ReadTorque.Text = "Read Torque"
        Me.ButMotor_ReadTorque.UseVisualStyleBackColor = True
        '
        'LblMotor_Zero
        '
        Me.LblMotor_Zero.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LblMotor_Zero.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblMotor_Zero.Location = New System.Drawing.Point(87, 19)
        Me.LblMotor_Zero.Name = "LblMotor_Zero"
        Me.LblMotor_Zero.Size = New System.Drawing.Size(64, 17)
        Me.LblMotor_Zero.TabIndex = 1
        '
        'ButMotor_Unlock
        '
        Me.ButMotor_Unlock.Location = New System.Drawing.Point(9, 184)
        Me.ButMotor_Unlock.Name = "ButMotor_Unlock"
        Me.ButMotor_Unlock.Size = New System.Drawing.Size(227, 21)
        Me.ButMotor_Unlock.TabIndex = 3
        Me.ButMotor_Unlock.Text = "Unlock Motor"
        Me.ButMotor_Unlock.UseVisualStyleBackColor = True
        '
        'ButMotor_Run1Loop
        '
        Me.ButMotor_Run1Loop.Location = New System.Drawing.Point(6, 79)
        Me.ButMotor_Run1Loop.Name = "ButMotor_Run1Loop"
        Me.ButMotor_Run1Loop.Size = New System.Drawing.Size(227, 21)
        Me.ButMotor_Run1Loop.TabIndex = 3
        Me.ButMotor_Run1Loop.Text = "Run 1 Loop"
        Me.ButMotor_Run1Loop.UseVisualStyleBackColor = True
        '
        'ButMotor_GoZero
        '
        Me.ButMotor_GoZero.Location = New System.Drawing.Point(6, 16)
        Me.ButMotor_GoZero.Name = "ButMotor_GoZero"
        Me.ButMotor_GoZero.Size = New System.Drawing.Size(75, 21)
        Me.ButMotor_GoZero.TabIndex = 3
        Me.ButMotor_GoZero.Text = "Goto Zero"
        Me.ButMotor_GoZero.UseVisualStyleBackColor = True
        '
        'ButMotor_ReadCount
        '
        Me.ButMotor_ReadCount.Location = New System.Drawing.Point(6, 106)
        Me.ButMotor_ReadCount.Name = "ButMotor_ReadCount"
        Me.ButMotor_ReadCount.Size = New System.Drawing.Size(114, 21)
        Me.ButMotor_ReadCount.TabIndex = 3
        Me.ButMotor_ReadCount.Text = "Read Count"
        Me.ButMotor_ReadCount.UseVisualStyleBackColor = True
        '
        'ButMotor_SetAsZero
        '
        Me.ButMotor_SetAsZero.Location = New System.Drawing.Point(157, 16)
        Me.ButMotor_SetAsZero.Name = "ButMotor_SetAsZero"
        Me.ButMotor_SetAsZero.Size = New System.Drawing.Size(76, 21)
        Me.ButMotor_SetAsZero.TabIndex = 3
        Me.ButMotor_SetAsZero.Text = "Set As Zero"
        Me.ButMotor_SetAsZero.UseVisualStyleBackColor = True
        '
        'ButMotor_GotoPos
        '
        Me.ButMotor_GotoPos.Location = New System.Drawing.Point(6, 42)
        Me.ButMotor_GotoPos.Name = "ButMotor_GotoPos"
        Me.ButMotor_GotoPos.Size = New System.Drawing.Size(145, 21)
        Me.ButMotor_GotoPos.TabIndex = 3
        Me.ButMotor_GotoPos.Text = "Goto Position:"
        Me.ButMotor_GotoPos.UseVisualStyleBackColor = True
        '
        'NumMotor_Position
        '
        Me.NumMotor_Position.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NumMotor_Position.Location = New System.Drawing.Point(157, 42)
        Me.NumMotor_Position.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.NumMotor_Position.Minimum = New Decimal(New Integer() {4000, 0, 0, -2147483648})
        Me.NumMotor_Position.Name = "NumMotor_Position"
        Me.NumMotor_Position.Size = New System.Drawing.Size(76, 21)
        Me.NumMotor_Position.TabIndex = 2
        '
        'GroupBoxMotorSelect
        '
        Me.GroupBoxMotorSelect.Controls.Add(Me.CombMotorCom)
        Me.GroupBoxMotorSelect.Controls.Add(Me.CombMotorIndex)
        Me.GroupBoxMotorSelect.Controls.Add(Me.Label2)
        Me.GroupBoxMotorSelect.Controls.Add(Me.Label1)
        Me.GroupBoxMotorSelect.Location = New System.Drawing.Point(6, 3)
        Me.GroupBoxMotorSelect.Name = "GroupBoxMotorSelect"
        Me.GroupBoxMotorSelect.Size = New System.Drawing.Size(242, 43)
        Me.GroupBoxMotorSelect.TabIndex = 2
        Me.GroupBoxMotorSelect.TabStop = False
        Me.GroupBoxMotorSelect.Text = "Motor Select"
        '
        'CombMotorCom
        '
        Me.CombMotorCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombMotorCom.FormattingEnabled = True
        Me.CombMotorCom.Location = New System.Drawing.Point(48, 17)
        Me.CombMotorCom.Name = "CombMotorCom"
        Me.CombMotorCom.Size = New System.Drawing.Size(56, 20)
        Me.CombMotorCom.TabIndex = 0
        '
        'CombMotorIndex
        '
        Me.CombMotorIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombMotorIndex.FormattingEnabled = True
        Me.CombMotorIndex.Location = New System.Drawing.Point(160, 17)
        Me.CombMotorIndex.Name = "CombMotorIndex"
        Me.CombMotorIndex.Size = New System.Drawing.Size(73, 20)
        Me.CombMotorIndex.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Location = New System.Drawing.Point(115, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Motor:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "COM:"
        '
        'GroupReady
        '
        Me.GroupReady.Controls.Add(Me.ButPIC_Down)
        Me.GroupReady.Controls.Add(Me.PicReady)
        Me.GroupReady.Controls.Add(Me.ButPIC_Up)
        Me.GroupReady.Controls.Add(Me.ButReady)
        Me.GroupReady.Location = New System.Drawing.Point(6, 1)
        Me.GroupReady.Name = "GroupReady"
        Me.GroupReady.Size = New System.Drawing.Size(219, 45)
        Me.GroupReady.TabIndex = 1
        Me.GroupReady.TabStop = False
        Me.GroupReady.Text = "Machine Control"
        '
        'ButPIC_Down
        '
        Me.ButPIC_Down.Enabled = False
        Me.ButPIC_Down.Font = New System.Drawing.Font("Wingdings 3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButPIC_Down.Location = New System.Drawing.Point(167, 13)
        Me.ButPIC_Down.Name = "ButPIC_Down"
        Me.ButPIC_Down.Size = New System.Drawing.Size(46, 28)
        Me.ButPIC_Down.TabIndex = 0
        Me.ButPIC_Down.Text = "q"
        Me.ButPIC_Down.UseVisualStyleBackColor = True
        '
        'PicReady
        '
        Me.PicReady.Image = Global.GeneralPro_OBP.My.Resources.FVTResource.LED_Gray
        Me.PicReady.Location = New System.Drawing.Point(80, 15)
        Me.PicReady.Name = "PicReady"
        Me.PicReady.Size = New System.Drawing.Size(31, 24)
        Me.PicReady.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicReady.TabIndex = 2
        Me.PicReady.TabStop = False
        '
        'ButPIC_Up
        '
        Me.ButPIC_Up.Enabled = False
        Me.ButPIC_Up.Font = New System.Drawing.Font("Wingdings 3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButPIC_Up.Location = New System.Drawing.Point(119, 13)
        Me.ButPIC_Up.Name = "ButPIC_Up"
        Me.ButPIC_Up.Size = New System.Drawing.Size(46, 28)
        Me.ButPIC_Up.TabIndex = 0
        Me.ButPIC_Up.Text = "p"
        Me.ButPIC_Up.UseVisualStyleBackColor = True
        '
        'ButReady
        '
        Me.ButReady.Location = New System.Drawing.Point(4, 13)
        Me.ButReady.Name = "ButReady"
        Me.ButReady.Size = New System.Drawing.Size(74, 28)
        Me.ButReady.TabIndex = 0
        Me.ButReady.Text = "Ready"
        Me.ButReady.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Enabled = False
        Me.GroupBox1.Location = New System.Drawing.Point(6, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(685, 126)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ChKAppend)
        Me.TabPage2.Controls.Add(Me.LblInfo)
        Me.TabPage2.Controls.Add(Me.ListPicCmd)
        Me.TabPage2.Controls.Add(Me.ListRecieved)
        Me.TabPage2.Controls.Add(Me.ButClear)
        Me.TabPage2.Controls.Add(Me.TxtValue)
        Me.TabPage2.Controls.Add(Me.ButSend)
        Me.TabPage2.Controls.Add(Me.TxtCmd)
        Me.TabPage2.Location = New System.Drawing.Point(22, 4)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(698, 480)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ChKAppend
        '
        Me.ChKAppend.AutoSize = True
        Me.ChKAppend.Location = New System.Drawing.Point(6, 37)
        Me.ChKAppend.Name = "ChKAppend"
        Me.ChKAppend.Size = New System.Drawing.Size(108, 16)
        Me.ChKAppend.TabIndex = 12
        Me.ChKAppend.Text = "Append Command"
        Me.ChKAppend.UseVisualStyleBackColor = True
        '
        'LblInfo
        '
        Me.LblInfo.BackColor = System.Drawing.Color.White
        Me.LblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInfo.Location = New System.Drawing.Point(3, 450)
        Me.LblInfo.Name = "LblInfo"
        Me.LblInfo.Size = New System.Drawing.Size(688, 31)
        Me.LblInfo.TabIndex = 11
        '
        'ListPicCmd
        '
        Me.ListPicCmd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ListPicCmd.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListPicCmd.FormattingEnabled = True
        Me.ListPicCmd.ItemHeight = 18
        Me.ListPicCmd.Location = New System.Drawing.Point(6, 61)
        Me.ListPicCmd.Name = "ListPicCmd"
        Me.ListPicCmd.Size = New System.Drawing.Size(134, 382)
        Me.ListPicCmd.Sorted = True
        Me.ListPicCmd.TabIndex = 7
        '
        'ListRecieved
        '
        Me.ListRecieved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListRecieved.BackColor = System.Drawing.Color.LightBlue
        Me.ListRecieved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListRecieved.ForeColor = System.Drawing.Color.Blue
        Me.ListRecieved.FormattingEnabled = True
        Me.ListRecieved.HorizontalScrollbar = True
        Me.ListRecieved.ItemHeight = 16
        Me.ListRecieved.Location = New System.Drawing.Point(149, 44)
        Me.ListRecieved.Name = "ListRecieved"
        Me.ListRecieved.Size = New System.Drawing.Size(546, 372)
        Me.ListRecieved.TabIndex = 8
        '
        'ButClear
        '
        Me.ButClear.Location = New System.Drawing.Point(623, 8)
        Me.ButClear.Name = "ButClear"
        Me.ButClear.Size = New System.Drawing.Size(68, 23)
        Me.ButClear.TabIndex = 4
        Me.ButClear.Text = "Clear"
        Me.ButClear.UseVisualStyleBackColor = True
        '
        'TxtValue
        '
        Me.TxtValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtValue.ForeColor = System.Drawing.Color.Blue
        Me.TxtValue.Location = New System.Drawing.Point(324, 9)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New System.Drawing.Size(125, 22)
        Me.TxtValue.TabIndex = 10
        '
        'ButSend
        '
        Me.ButSend.Location = New System.Drawing.Point(455, 8)
        Me.ButSend.Name = "ButSend"
        Me.ButSend.Size = New System.Drawing.Size(162, 23)
        Me.ButSend.TabIndex = 6
        Me.ButSend.Text = "Send"
        Me.ButSend.UseVisualStyleBackColor = True
        '
        'TxtCmd
        '
        Me.TxtCmd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCmd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCmd.ForeColor = System.Drawing.Color.Blue
        Me.TxtCmd.Location = New System.Drawing.Point(6, 9)
        Me.TxtCmd.Name = "TxtCmd"
        Me.TxtCmd.Size = New System.Drawing.Size(312, 22)
        Me.TxtCmd.TabIndex = 9
        '
        'TabCtrlMain
        '
        Me.TabCtrlMain.Controls.Add(Me.TabPagePIC)
        Me.TabCtrlMain.Controls.Add(Me.TabPageADAM)
        Me.TabCtrlMain.Controls.Add(Me.TabPageACMeter)
        Me.TabCtrlMain.Controls.Add(Me.TabPage34972A)
        Me.TabCtrlMain.Controls.Add(Me.TabPageInterface)
        Me.TabCtrlMain.Controls.Add(Me.TabPageFlashRunner)
        Me.TabCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabCtrlMain.Location = New System.Drawing.Point(0, 0)
        Me.TabCtrlMain.Name = "TabCtrlMain"
        Me.TabCtrlMain.SelectedIndex = 0
        Me.TabCtrlMain.Size = New System.Drawing.Size(738, 520)
        Me.TabCtrlMain.TabIndex = 0
        '
        'TabPageADAM
        '
        Me.TabPageADAM.Controls.Add(Me.Diag_ADAMPanel1)
        Me.TabPageADAM.Location = New System.Drawing.Point(4, 22)
        Me.TabPageADAM.Name = "TabPageADAM"
        Me.TabPageADAM.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageADAM.Size = New System.Drawing.Size(730, 494)
        Me.TabPageADAM.TabIndex = 5
        Me.TabPageADAM.Text = "ADAM"
        Me.TabPageADAM.UseVisualStyleBackColor = True
        '
        'Diag_ADAMPanel1
        '
        Me.Diag_ADAMPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Diag_ADAMPanel1.Location = New System.Drawing.Point(3, 3)
        Me.Diag_ADAMPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Diag_ADAMPanel1.Name = "Diag_ADAMPanel1"
        Me.Diag_ADAMPanel1.Size = New System.Drawing.Size(724, 488)
        Me.Diag_ADAMPanel1.TabIndex = 0
        '
        'TabPageACMeter
        '
        Me.TabPageACMeter.Controls.Add(Me.Diag_ACMeterPanel1)
        Me.TabPageACMeter.Location = New System.Drawing.Point(4, 22)
        Me.TabPageACMeter.Name = "TabPageACMeter"
        Me.TabPageACMeter.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageACMeter.Size = New System.Drawing.Size(730, 494)
        Me.TabPageACMeter.TabIndex = 6
        Me.TabPageACMeter.Text = "AC Meter"
        Me.TabPageACMeter.UseVisualStyleBackColor = True
        '
        'Diag_ACMeterPanel1
        '
        Me.Diag_ACMeterPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Diag_ACMeterPanel1.Location = New System.Drawing.Point(3, 3)
        Me.Diag_ACMeterPanel1.Name = "Diag_ACMeterPanel1"
        Me.Diag_ACMeterPanel1.Size = New System.Drawing.Size(724, 488)
        Me.Diag_ACMeterPanel1.TabIndex = 0
        '
        'TabPage34972A
        '
        Me.TabPage34972A.Controls.Add(Me.Diag_KS34972APanel1)
        Me.TabPage34972A.Location = New System.Drawing.Point(4, 22)
        Me.TabPage34972A.Name = "TabPage34972A"
        Me.TabPage34972A.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage34972A.Size = New System.Drawing.Size(730, 494)
        Me.TabPage34972A.TabIndex = 7
        Me.TabPage34972A.Text = "34972A"
        Me.TabPage34972A.UseVisualStyleBackColor = True
        '
        'Diag_KS34972APanel1
        '
        Me.Diag_KS34972APanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Diag_KS34972APanel1.InterfaceType = Bitron3DEQ.FVTRackBoardManager.UI.Diag_KS34972APanel.InterfaceTypes.Visa
        Me.Diag_KS34972APanel1.Location = New System.Drawing.Point(3, 3)
        Me.Diag_KS34972APanel1.Name = "Diag_KS34972APanel1"
        Me.Diag_KS34972APanel1.Size = New System.Drawing.Size(724, 488)
        Me.Diag_KS34972APanel1.TabIndex = 0
        '
        'TabPageFlashRunner
        '
        Me.TabPageFlashRunner.Controls.Add(Me.Txt_FlahInfo)
        Me.TabPageFlashRunner.Controls.Add(Me.Txt_FW_Project)
        Me.TabPageFlashRunner.Controls.Add(Me.Btn_StartFlash)
        Me.TabPageFlashRunner.Controls.Add(Me.Txt_FlashVer)
        Me.TabPageFlashRunner.Controls.Add(Me.Btn_GetFVer)
        Me.TabPageFlashRunner.Controls.Add(Me.Pic_FlashInter)
        Me.TabPageFlashRunner.Controls.Add(Me.Btn_ConnectFlashRunner)
        Me.TabPageFlashRunner.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFlashRunner.Name = "TabPageFlashRunner"
        Me.TabPageFlashRunner.Size = New System.Drawing.Size(730, 494)
        Me.TabPageFlashRunner.TabIndex = 8
        Me.TabPageFlashRunner.Text = "FlashRunner"
        Me.TabPageFlashRunner.UseVisualStyleBackColor = True
        '
        'Txt_FlahInfo
        '
        Me.Txt_FlahInfo.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Txt_FlahInfo.Location = New System.Drawing.Point(371, 20)
        Me.Txt_FlahInfo.Multiline = True
        Me.Txt_FlahInfo.Name = "Txt_FlahInfo"
        Me.Txt_FlahInfo.Size = New System.Drawing.Size(333, 435)
        Me.Txt_FlahInfo.TabIndex = 9
        '
        'Txt_FW_Project
        '
        Me.Txt_FW_Project.Location = New System.Drawing.Point(161, 172)
        Me.Txt_FW_Project.Name = "Txt_FW_Project"
        Me.Txt_FW_Project.Size = New System.Drawing.Size(208, 21)
        Me.Txt_FW_Project.TabIndex = 8
        Me.Txt_FW_Project.Text = "TESLA_OHC_20200303.prj:0001"
        '
        'Btn_StartFlash
        '
        Me.Btn_StartFlash.Location = New System.Drawing.Point(15, 164)
        Me.Btn_StartFlash.Name = "Btn_StartFlash"
        Me.Btn_StartFlash.Size = New System.Drawing.Size(136, 34)
        Me.Btn_StartFlash.TabIndex = 7
        Me.Btn_StartFlash.Text = "StartFlash"
        Me.Btn_StartFlash.UseVisualStyleBackColor = True
        '
        'Txt_FlashVer
        '
        Me.Txt_FlashVer.Location = New System.Drawing.Point(161, 99)
        Me.Txt_FlashVer.Name = "Txt_FlashVer"
        Me.Txt_FlashVer.Size = New System.Drawing.Size(208, 21)
        Me.Txt_FlashVer.TabIndex = 6
        '
        'Btn_GetFVer
        '
        Me.Btn_GetFVer.Location = New System.Drawing.Point(15, 91)
        Me.Btn_GetFVer.Name = "Btn_GetFVer"
        Me.Btn_GetFVer.Size = New System.Drawing.Size(136, 34)
        Me.Btn_GetFVer.TabIndex = 5
        Me.Btn_GetFVer.Text = "Get Ver."
        Me.Btn_GetFVer.UseVisualStyleBackColor = True
        '
        'Pic_FlashInter
        '
        Me.Pic_FlashInter.Image = Global.GeneralPro_OBP.My.Resources.FVTResource.LED_Gray
        Me.Pic_FlashInter.Location = New System.Drawing.Point(161, 27)
        Me.Pic_FlashInter.Name = "Pic_FlashInter"
        Me.Pic_FlashInter.Size = New System.Drawing.Size(31, 24)
        Me.Pic_FlashInter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pic_FlashInter.TabIndex = 4
        Me.Pic_FlashInter.TabStop = False
        '
        'Btn_ConnectFlashRunner
        '
        Me.Btn_ConnectFlashRunner.Location = New System.Drawing.Point(15, 20)
        Me.Btn_ConnectFlashRunner.Name = "Btn_ConnectFlashRunner"
        Me.Btn_ConnectFlashRunner.Size = New System.Drawing.Size(136, 34)
        Me.Btn_ConnectFlashRunner.TabIndex = 3
        Me.Btn_ConnectFlashRunner.Text = "ConnectFlashRunner"
        Me.Btn_ConnectFlashRunner.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn1.HeaderText = "Channel"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn3.HeaderText = "Parameter"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn4.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn5.HeaderText = "Parameter"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn7.HeaderText = "Parameter"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Parameter"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewTextBoxColumn10.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn11.HeaderText = "Parameter"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn12.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "Parameter"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn14.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "Parameter"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn16.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn17
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn17.DefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridViewTextBoxColumn17.HeaderText = "Channel"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn18.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn19
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn19.DefaultCellStyle = DataGridViewCellStyle16
        Me.DataGridViewTextBoxColumn19.HeaderText = "Channel"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn20.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column7
        '
        Me.Column7.HeaderText = "TP Name"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "TP Name"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        '
        'DataGridViewTextBoxColumn36
        '
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        '
        'FrmDiagMachine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 520)
        Me.Controls.Add(Me.TabCtrlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmDiagMachine"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Diagnostic Tools for Machine"
        Me.TabPageInterface.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.PicOpen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxErrRemark.ResumeLayout(False)
        Me.GroupBoxCmds.ResumeLayout(False)
        Me.GroupBoxCmds.PerformLayout()
        Me.TabPagePIC.ResumeLayout(False)
        Me.TabCtrlPIC.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.Pic_FingerUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPowerON.ResumeLayout(False)
        CType(Me.PicPower, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCtrlPICPanel.ResumeLayout(False)
        Me.TabPageDCRly.ResumeLayout(False)
        Me.TabPageSwitch.ResumeLayout(False)
        Me.TabPageACOut.ResumeLayout(False)
        Me.TabPageMotor.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupMotorTools.ResumeLayout(False)
        CType(Me.NumMotor_Position, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxMotorSelect.ResumeLayout(False)
        Me.GroupReady.ResumeLayout(False)
        CType(Me.PicReady, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabCtrlMain.ResumeLayout(False)
        Me.TabPageADAM.ResumeLayout(False)
        Me.TabPageACMeter.ResumeLayout(False)
        Me.TabPage34972A.ResumeLayout(False)
        Me.TabPageFlashRunner.ResumeLayout(False)
        Me.TabPageFlashRunner.PerformLayout()
        CType(Me.Pic_FlashInter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPageInterface As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents PicOpen As System.Windows.Forms.PictureBox
    Friend WithEvents ButOpen As System.Windows.Forms.Button
    Friend WithEvents GroupBoxErrRemark As System.Windows.Forms.GroupBox
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents GroupBoxCmds As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPagePIC As System.Windows.Forms.TabPage
    Friend WithEvents TabCtrlPIC As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupPowerON As System.Windows.Forms.GroupBox
    Friend WithEvents ButPower As System.Windows.Forms.Button
    Friend WithEvents PicPower As System.Windows.Forms.PictureBox
    Friend WithEvents GroupReady As System.Windows.Forms.GroupBox
    Friend WithEvents PicReady As System.Windows.Forms.PictureBox
    Friend WithEvents ButReady As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents LblInfo As System.Windows.Forms.Label
    Friend WithEvents ListPicCmd As System.Windows.Forms.ListBox
    Friend WithEvents ListRecieved As System.Windows.Forms.ListBox
    Friend WithEvents ButClear As System.Windows.Forms.Button
    Friend WithEvents TxtValue As System.Windows.Forms.TextBox
    Friend WithEvents ButSend As System.Windows.Forms.Button
    Friend WithEvents TxtCmd As System.Windows.Forms.TextBox
    Friend WithEvents TabCtrlMain As System.Windows.Forms.TabControl
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioInt_UUT4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioInt_UUT3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioInt_UUT2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioInt_UUT1 As System.Windows.Forms.RadioButton
    Friend WithEvents ButPIC_Up As System.Windows.Forms.Button
    Friend WithEvents ButPIC_Down As System.Windows.Forms.Button
    Friend WithEvents TabCtrlPICPanel As System.Windows.Forms.TabControl
    Friend WithEvents TabPageDCRly As System.Windows.Forms.TabPage
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TxtInt1 As System.Windows.Forms.TextBox
    Friend WithEvents ButInt_DLLVersion As System.Windows.Forms.Button
    Friend WithEvents TabPageACOut As System.Windows.Forms.TabPage
    Friend WithEvents TabPageADAM As System.Windows.Forms.TabPage
    Friend WithEvents TabPageACMeter As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Diag_ACOutPanel1 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioPIC_UUT4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioPIC_UUT3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioPIC_UUT2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioPIC_UUT1 As System.Windows.Forms.RadioButton
    Friend WithEvents Diag_ADAMPanel1 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_ADAMPanel
    Friend WithEvents Diag_ACMeterPanel1 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACMeterPanel
    Friend WithEvents Diag_ACOutPanel2 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel
    Friend WithEvents Diag_ACOutPanel4 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel
    Friend WithEvents Diag_ACOutPanel3 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_ACOutPanel
    Friend WithEvents Diag_SwitchPanel1 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_SwitchPanel
    Friend WithEvents Diag_DCRelayPanel2 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel
    Friend WithEvents Diag_DCRelayPanel1 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel
    Friend WithEvents TabPageSwitch As System.Windows.Forms.TabPage
    Friend WithEvents Diag_SwitchPanel2 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_SwitchPanel
    Friend WithEvents ChKAppend As System.Windows.Forms.CheckBox
    Friend WithEvents Diag_DCRelayPanel4 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel
    Friend WithEvents Diag_DCRelayPanel3 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel
    Friend WithEvents But_FingerPanel As System.Windows.Forms.Button
    Friend WithEvents TxtInt_EnterTestMode As System.Windows.Forms.TextBox
    Friend WithEvents ButInt_EnterTestMode As System.Windows.Forms.Button
    Friend WithEvents TabPageMotor As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CombMotorCom As System.Windows.Forms.ComboBox
    Friend WithEvents CombMotorIndex As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBoxMotorSelect As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Pic_FingerUpDown As System.Windows.Forms.PictureBox
    Friend WithEvents ButMotor_SetAsZero As System.Windows.Forms.Button
    Friend WithEvents GroupMotorTools As System.Windows.Forms.GroupBox
    Friend WithEvents ButMotor_GotoPos As System.Windows.Forms.Button
    Friend WithEvents NumMotor_Position As System.Windows.Forms.NumericUpDown
    Friend WithEvents ButMotor_GoZero As System.Windows.Forms.Button
    Friend WithEvents ButMotor_Run1Loop As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtMotorCommand As System.Windows.Forms.TextBox
    Friend WithEvents ButMotor_Send As System.Windows.Forms.Button
    Friend WithEvents ListMotor As System.Windows.Forms.ListBox
    Friend WithEvents ButMotor_ReadTorque As System.Windows.Forms.Button
    Friend WithEvents ButMotor_ReadCount As System.Windows.Forms.Button
    Friend WithEvents LblMotor_Zero As System.Windows.Forms.Label
    Friend WithEvents ButMotor_Clear As System.Windows.Forms.Button
    Friend WithEvents ButMotor_Unlock As System.Windows.Forms.Button
    Friend WithEvents ButMotor_ECHOCount As System.Windows.Forms.Button
    Friend WithEvents TabPage34972A As System.Windows.Forms.TabPage
    Friend WithEvents Diag_KS34972APanel1 As Bitron3DEQ.FVTRackBoardManager.UI.Diag_KS34972APanel
    Friend WithEvents TabPageFlashRunner As System.Windows.Forms.TabPage
    Friend WithEvents Pic_FlashInter As System.Windows.Forms.PictureBox
    Friend WithEvents Btn_ConnectFlashRunner As System.Windows.Forms.Button
    Friend WithEvents Txt_FW_Project As System.Windows.Forms.TextBox
    Friend WithEvents Btn_StartFlash As System.Windows.Forms.Button
    Friend WithEvents Txt_FlashVer As System.Windows.Forms.TextBox
    Friend WithEvents Btn_GetFVer As System.Windows.Forms.Button
    Friend WithEvents Txt_FlahInfo As System.Windows.Forms.TextBox
End Class
