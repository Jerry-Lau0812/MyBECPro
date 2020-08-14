<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.DGVResult = New System.Windows.Forms.DataGridView()
        Me.D_Co1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Btn_Validate = New System.Windows.Forms.Button()
        Me.Btn_StartCalibration = New System.Windows.Forms.Button()
        Me.TextBoxFile = New System.Windows.Forms.TextBox()
        Me.Btn_LoadPara = New System.Windows.Forms.Button()
        Me.Btn_FujiTrax = New System.Windows.Forms.Button()
        Me.Btn_GetCur = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxFuji = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxXMLTool = New System.Windows.Forms.TextBox()
        Me.BtnConfig = New System.Windows.Forms.Button()
        Me.Radio8 = New System.Windows.Forms.RadioButton()
        Me.Radio7 = New System.Windows.Forms.RadioButton()
        Me.Radio6 = New System.Windows.Forms.RadioButton()
        Me.Radio5 = New System.Windows.Forms.RadioButton()
        Me.ComboBoxFeasa = New System.Windows.Forms.ComboBox()
        Me.TextFeasaChange = New System.Windows.Forms.TextBox()
        Me.Btn_ChangeFeasa = New System.Windows.Forms.Button()
        Me.ComboBrightLevel = New System.Windows.Forms.ComboBox()
        Me.Btn_Capture = New System.Windows.Forms.Button()
        Me.Btn_GetAllValues = New System.Windows.Forms.Button()
        Me.TextFeasaNo = New System.Windows.Forms.TextBox()
        Me.BtnGetSerialNo = New System.Windows.Forms.Button()
        Me.PicComOpen = New System.Windows.Forms.PictureBox()
        Me.Radio4 = New System.Windows.Forms.RadioButton()
        Me.Radio3 = New System.Windows.Forms.RadioButton()
        Me.Radio2 = New System.Windows.Forms.RadioButton()
        Me.Radio1 = New System.Windows.Forms.RadioButton()
        Me.Btn_OpenCom = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TxtFuji = New System.Windows.Forms.TextBox()
        CType(Me.DGVResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicComOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGVResult
        '
        Me.DGVResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVResult.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.D_Co1, Me.D_Col2, Me.D_Col3, Me.D_Col4, Me.D_Col5, Me.D_Col6, Me.D_Col7, Me.D_Col8})
        Me.DGVResult.Location = New System.Drawing.Point(2, 12)
        Me.DGVResult.Name = "DGVResult"
        Me.DGVResult.RowTemplate.Height = 23
        Me.DGVResult.Size = New System.Drawing.Size(625, 562)
        Me.DGVResult.TabIndex = 11
        '
        'D_Co1
        '
        Me.D_Co1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Co1.FillWeight = 50.0!
        Me.D_Co1.HeaderText = "No."
        Me.D_Co1.Name = "D_Co1"
        '
        'D_Col2
        '
        Me.D_Col2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Col2.FillWeight = 50.0!
        Me.D_Col2.HeaderText = "H."
        Me.D_Col2.Name = "D_Col2"
        '
        'D_Col3
        '
        Me.D_Col3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Col3.FillWeight = 50.0!
        Me.D_Col3.HeaderText = "S."
        Me.D_Col3.Name = "D_Col3"
        '
        'D_Col4
        '
        Me.D_Col4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Col4.FillWeight = 50.0!
        Me.D_Col4.HeaderText = "I."
        Me.D_Col4.Name = "D_Col4"
        '
        'D_Col5
        '
        Me.D_Col5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Col5.FillWeight = 50.0!
        Me.D_Col5.HeaderText = "AbsI."
        Me.D_Col5.Name = "D_Col5"
        '
        'D_Col6
        '
        Me.D_Col6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Col6.FillWeight = 50.0!
        Me.D_Col6.HeaderText = "WaveLength"
        Me.D_Col6.Name = "D_Col6"
        '
        'D_Col7
        '
        Me.D_Col7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Col7.FillWeight = 50.0!
        Me.D_Col7.HeaderText = "Cx."
        Me.D_Col7.Name = "D_Col7"
        '
        'D_Col8
        '
        Me.D_Col8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.D_Col8.FillWeight = 50.0!
        Me.D_Col8.HeaderText = "Cy."
        Me.D_Col8.Name = "D_Col8"
        '
        'Btn_Validate
        '
        Me.Btn_Validate.Location = New System.Drawing.Point(88, 58)
        Me.Btn_Validate.Name = "Btn_Validate"
        Me.Btn_Validate.Size = New System.Drawing.Size(150, 23)
        Me.Btn_Validate.TabIndex = 15
        Me.Btn_Validate.Text = "Validation"
        Me.Btn_Validate.UseVisualStyleBackColor = True
        '
        'Btn_StartCalibration
        '
        Me.Btn_StartCalibration.Location = New System.Drawing.Point(244, 12)
        Me.Btn_StartCalibration.Name = "Btn_StartCalibration"
        Me.Btn_StartCalibration.Size = New System.Drawing.Size(127, 23)
        Me.Btn_StartCalibration.TabIndex = 16
        Me.Btn_StartCalibration.Text = "Calibration"
        Me.Btn_StartCalibration.UseVisualStyleBackColor = True
        '
        'TextBoxFile
        '
        Me.TextBoxFile.Location = New System.Drawing.Point(88, 14)
        Me.TextBoxFile.Name = "TextBoxFile"
        Me.TextBoxFile.Size = New System.Drawing.Size(150, 21)
        Me.TextBoxFile.TabIndex = 17
        '
        'Btn_LoadPara
        '
        Me.Btn_LoadPara.Location = New System.Drawing.Point(220, 308)
        Me.Btn_LoadPara.Name = "Btn_LoadPara"
        Me.Btn_LoadPara.Size = New System.Drawing.Size(110, 23)
        Me.Btn_LoadPara.TabIndex = 18
        Me.Btn_LoadPara.Text = "CheckResPara"
        Me.Btn_LoadPara.UseVisualStyleBackColor = True
        '
        'Btn_FujiTrax
        '
        Me.Btn_FujiTrax.Location = New System.Drawing.Point(220, 276)
        Me.Btn_FujiTrax.Name = "Btn_FujiTrax"
        Me.Btn_FujiTrax.Size = New System.Drawing.Size(110, 23)
        Me.Btn_FujiTrax.TabIndex = 19
        Me.Btn_FujiTrax.Text = "GetFujiTrax"
        Me.Btn_FujiTrax.UseVisualStyleBackColor = True
        '
        'Btn_GetCur
        '
        Me.Btn_GetCur.Location = New System.Drawing.Point(220, 341)
        Me.Btn_GetCur.Name = "Btn_GetCur"
        Me.Btn_GetCur.Size = New System.Drawing.Size(110, 23)
        Me.Btn_GetCur.TabIndex = 20
        Me.Btn_GetCur.Text = "CheckCurPara"
        Me.Btn_GetCur.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "FileCali:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "StartVali:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 287)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "FujiTrax:"
        '
        'TextBoxFuji
        '
        Me.TextBoxFuji.Location = New System.Drawing.Point(22, 308)
        Me.TextBoxFuji.Name = "TextBoxFuji"
        Me.TextBoxFuji.Size = New System.Drawing.Size(187, 21)
        Me.TextBoxFuji.TabIndex = 24
        Me.TextBoxFuji.Text = "34619299003c20102860"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(629, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(402, 562)
        Me.TabControl1.TabIndex = 25
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(394, 536)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "BaseOperation"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TextBoxXMLTool)
        Me.GroupBox1.Controls.Add(Me.BtnConfig)
        Me.GroupBox1.Controls.Add(Me.Radio8)
        Me.GroupBox1.Controls.Add(Me.Radio7)
        Me.GroupBox1.Controls.Add(Me.Radio6)
        Me.GroupBox1.Controls.Add(Me.Radio5)
        Me.GroupBox1.Controls.Add(Me.ComboBoxFeasa)
        Me.GroupBox1.Controls.Add(Me.TextFeasaChange)
        Me.GroupBox1.Controls.Add(Me.Btn_ChangeFeasa)
        Me.GroupBox1.Controls.Add(Me.ComboBrightLevel)
        Me.GroupBox1.Controls.Add(Me.Btn_Capture)
        Me.GroupBox1.Controls.Add(Me.Btn_GetAllValues)
        Me.GroupBox1.Controls.Add(Me.TextFeasaNo)
        Me.GroupBox1.Controls.Add(Me.BtnGetSerialNo)
        Me.GroupBox1.Controls.Add(Me.PicComOpen)
        Me.GroupBox1.Controls.Add(Me.Radio4)
        Me.GroupBox1.Controls.Add(Me.Radio3)
        Me.GroupBox1.Controls.Add(Me.Radio2)
        Me.GroupBox1.Controls.Add(Me.Radio1)
        Me.GroupBox1.Controls.Add(Me.Btn_OpenCom)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(377, 524)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Base Operate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 321)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(155, 12)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Please Select XMLPad Path"
        '
        'TextBoxXMLTool
        '
        Me.TextBoxXMLTool.Location = New System.Drawing.Point(18, 342)
        Me.TextBoxXMLTool.Name = "TextBoxXMLTool"
        Me.TextBoxXMLTool.Size = New System.Drawing.Size(340, 21)
        Me.TextBoxXMLTool.TabIndex = 18
        '
        'BtnConfig
        '
        Me.BtnConfig.Location = New System.Drawing.Point(18, 373)
        Me.BtnConfig.Name = "BtnConfig"
        Me.BtnConfig.Size = New System.Drawing.Size(161, 23)
        Me.BtnConfig.TabIndex = 17
        Me.BtnConfig.Text = "Modify Funzconf"
        Me.BtnConfig.UseVisualStyleBackColor = True
        '
        'Radio8
        '
        Me.Radio8.AutoSize = True
        Me.Radio8.Location = New System.Drawing.Point(197, 70)
        Me.Radio8.Name = "Radio8"
        Me.Radio8.Size = New System.Drawing.Size(53, 16)
        Me.Radio8.TabIndex = 16
        Me.Radio8.TabStop = True
        Me.Radio8.Text = "Site8"
        Me.Radio8.UseVisualStyleBackColor = True
        '
        'Radio7
        '
        Me.Radio7.AutoSize = True
        Me.Radio7.Location = New System.Drawing.Point(138, 70)
        Me.Radio7.Name = "Radio7"
        Me.Radio7.Size = New System.Drawing.Size(53, 16)
        Me.Radio7.TabIndex = 16
        Me.Radio7.TabStop = True
        Me.Radio7.Text = "Site7"
        Me.Radio7.UseVisualStyleBackColor = True
        '
        'Radio6
        '
        Me.Radio6.AutoSize = True
        Me.Radio6.Location = New System.Drawing.Point(83, 70)
        Me.Radio6.Name = "Radio6"
        Me.Radio6.Size = New System.Drawing.Size(53, 16)
        Me.Radio6.TabIndex = 16
        Me.Radio6.TabStop = True
        Me.Radio6.Text = "Site6"
        Me.Radio6.UseVisualStyleBackColor = True
        '
        'Radio5
        '
        Me.Radio5.AutoSize = True
        Me.Radio5.Location = New System.Drawing.Point(31, 70)
        Me.Radio5.Name = "Radio5"
        Me.Radio5.Size = New System.Drawing.Size(53, 16)
        Me.Radio5.TabIndex = 15
        Me.Radio5.TabStop = True
        Me.Radio5.Text = "Site5"
        Me.Radio5.UseVisualStyleBackColor = True
        '
        'ComboBoxFeasa
        '
        Me.ComboBoxFeasa.FormattingEnabled = True
        Me.ComboBoxFeasa.Location = New System.Drawing.Point(31, 153)
        Me.ComboBoxFeasa.Name = "ComboBoxFeasa"
        Me.ComboBoxFeasa.Size = New System.Drawing.Size(139, 20)
        Me.ComboBoxFeasa.TabIndex = 14
        '
        'TextFeasaChange
        '
        Me.TextFeasaChange.Location = New System.Drawing.Point(30, 178)
        Me.TextFeasaChange.Name = "TextFeasaChange"
        Me.TextFeasaChange.Size = New System.Drawing.Size(140, 21)
        Me.TextFeasaChange.TabIndex = 13
        '
        'Btn_ChangeFeasa
        '
        Me.Btn_ChangeFeasa.Location = New System.Drawing.Point(178, 153)
        Me.Btn_ChangeFeasa.Name = "Btn_ChangeFeasa"
        Me.Btn_ChangeFeasa.Size = New System.Drawing.Size(140, 47)
        Me.Btn_ChangeFeasa.TabIndex = 12
        Me.Btn_ChangeFeasa.Text = "ChangeFeasa"
        Me.Btn_ChangeFeasa.UseVisualStyleBackColor = True
        '
        'ComboBrightLevel
        '
        Me.ComboBrightLevel.FormattingEnabled = True
        Me.ComboBrightLevel.Location = New System.Drawing.Point(30, 214)
        Me.ComboBrightLevel.Name = "ComboBrightLevel"
        Me.ComboBrightLevel.Size = New System.Drawing.Size(140, 20)
        Me.ComboBrightLevel.TabIndex = 8
        '
        'Btn_Capture
        '
        Me.Btn_Capture.Location = New System.Drawing.Point(179, 210)
        Me.Btn_Capture.Name = "Btn_Capture"
        Me.Btn_Capture.Size = New System.Drawing.Size(140, 25)
        Me.Btn_Capture.TabIndex = 7
        Me.Btn_Capture.Text = "Capture"
        Me.Btn_Capture.UseVisualStyleBackColor = True
        '
        'Btn_GetAllValues
        '
        Me.Btn_GetAllValues.Location = New System.Drawing.Point(30, 253)
        Me.Btn_GetAllValues.Name = "Btn_GetAllValues"
        Me.Btn_GetAllValues.Size = New System.Drawing.Size(289, 31)
        Me.Btn_GetAllValues.TabIndex = 6
        Me.Btn_GetAllValues.Text = "GetAllValue"
        Me.Btn_GetAllValues.UseVisualStyleBackColor = True
        '
        'TextFeasaNo
        '
        Me.TextFeasaNo.Location = New System.Drawing.Point(180, 105)
        Me.TextFeasaNo.Name = "TextFeasaNo"
        Me.TextFeasaNo.Size = New System.Drawing.Size(140, 21)
        Me.TextFeasaNo.TabIndex = 5
        '
        'BtnGetSerialNo
        '
        Me.BtnGetSerialNo.Location = New System.Drawing.Point(31, 101)
        Me.BtnGetSerialNo.Name = "BtnGetSerialNo"
        Me.BtnGetSerialNo.Size = New System.Drawing.Size(140, 27)
        Me.BtnGetSerialNo.TabIndex = 4
        Me.BtnGetSerialNo.Text = "GetFeasaNo"
        Me.BtnGetSerialNo.UseVisualStyleBackColor = True
        '
        'PicComOpen
        '
        Me.PicComOpen.Image = Global.MyBECPro.My.Resources.Resources.LEDGray
        Me.PicComOpen.InitialImage = Global.MyBECPro.My.Resources.Resources.LEDGray
        Me.PicComOpen.Location = New System.Drawing.Point(311, 60)
        Me.PicComOpen.Name = "PicComOpen"
        Me.PicComOpen.Size = New System.Drawing.Size(31, 39)
        Me.PicComOpen.TabIndex = 3
        Me.PicComOpen.TabStop = False
        '
        'Radio4
        '
        Me.Radio4.AutoSize = True
        Me.Radio4.Location = New System.Drawing.Point(197, 31)
        Me.Radio4.Name = "Radio4"
        Me.Radio4.Size = New System.Drawing.Size(53, 16)
        Me.Radio4.TabIndex = 2
        Me.Radio4.TabStop = True
        Me.Radio4.Text = "Site4"
        Me.Radio4.UseVisualStyleBackColor = True
        '
        'Radio3
        '
        Me.Radio3.AutoSize = True
        Me.Radio3.Location = New System.Drawing.Point(138, 32)
        Me.Radio3.Name = "Radio3"
        Me.Radio3.Size = New System.Drawing.Size(53, 16)
        Me.Radio3.TabIndex = 2
        Me.Radio3.TabStop = True
        Me.Radio3.Text = "Site3"
        Me.Radio3.UseVisualStyleBackColor = True
        '
        'Radio2
        '
        Me.Radio2.AutoSize = True
        Me.Radio2.Location = New System.Drawing.Point(83, 32)
        Me.Radio2.Name = "Radio2"
        Me.Radio2.Size = New System.Drawing.Size(53, 16)
        Me.Radio2.TabIndex = 2
        Me.Radio2.TabStop = True
        Me.Radio2.Text = "Site2"
        Me.Radio2.UseVisualStyleBackColor = True
        '
        'Radio1
        '
        Me.Radio1.AutoSize = True
        Me.Radio1.Location = New System.Drawing.Point(28, 31)
        Me.Radio1.Name = "Radio1"
        Me.Radio1.Size = New System.Drawing.Size(53, 16)
        Me.Radio1.TabIndex = 2
        Me.Radio1.TabStop = True
        Me.Radio1.Text = "Site1"
        Me.Radio1.UseVisualStyleBackColor = True
        '
        'Btn_OpenCom
        '
        Me.Btn_OpenCom.Location = New System.Drawing.Point(270, 20)
        Me.Btn_OpenCom.Name = "Btn_OpenCom"
        Me.Btn_OpenCom.Size = New System.Drawing.Size(101, 35)
        Me.Btn_OpenCom.TabIndex = 1
        Me.Btn_OpenCom.Text = "Open"
        Me.Btn_OpenCom.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TxtFuji)
        Me.TabPage2.Controls.Add(Me.TextBoxFile)
        Me.TabPage2.Controls.Add(Me.TextBoxFuji)
        Me.TabPage2.Controls.Add(Me.Btn_Validate)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Btn_StartCalibration)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Btn_LoadPara)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.Btn_FujiTrax)
        Me.TabPage2.Controls.Add(Me.Btn_GetCur)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(394, 536)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Cali&Res&Fuji"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TxtFuji
        '
        Me.TxtFuji.Location = New System.Drawing.Point(31, 377)
        Me.TxtFuji.Multiline = True
        Me.TxtFuji.Name = "TxtFuji"
        Me.TxtFuji.Size = New System.Drawing.Size(298, 143)
        Me.TxtFuji.TabIndex = 25
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1043, 577)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.DGVResult)
        Me.Name = "Form1"
        Me.Text = "FeasaDebug"
        CType(Me.DGVResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicComOpen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGVResult As System.Windows.Forms.DataGridView
    Friend WithEvents D_Co1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Btn_Validate As System.Windows.Forms.Button
    Friend WithEvents Btn_StartCalibration As System.Windows.Forms.Button
    Friend WithEvents TextBoxFile As System.Windows.Forms.TextBox
    Friend WithEvents Btn_LoadPara As System.Windows.Forms.Button
    Friend WithEvents Btn_FujiTrax As System.Windows.Forms.Button
    Friend WithEvents Btn_GetCur As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFuji As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Radio6 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio5 As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBoxFeasa As System.Windows.Forms.ComboBox
    Friend WithEvents TextFeasaChange As System.Windows.Forms.TextBox
    Friend WithEvents Btn_ChangeFeasa As System.Windows.Forms.Button
    Friend WithEvents ComboBrightLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Btn_Capture As System.Windows.Forms.Button
    Friend WithEvents Btn_GetAllValues As System.Windows.Forms.Button
    Friend WithEvents TextFeasaNo As System.Windows.Forms.TextBox
    Friend WithEvents BtnGetSerialNo As System.Windows.Forms.Button
    Friend WithEvents PicComOpen As System.Windows.Forms.PictureBox
    Friend WithEvents Radio4 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio3 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio2 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio1 As System.Windows.Forms.RadioButton
    Friend WithEvents Btn_OpenCom As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents BtnConfig As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxXMLTool As System.Windows.Forms.TextBox
    Friend WithEvents Radio8 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio7 As System.Windows.Forms.RadioButton
    Friend WithEvents TxtFuji As System.Windows.Forms.TextBox

End Class
