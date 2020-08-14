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
        Me.Btn_OpenCom = New System.Windows.Forms.Button()
        Me.Radio1 = New System.Windows.Forms.RadioButton()
        Me.Radio2 = New System.Windows.Forms.RadioButton()
        Me.Radio3 = New System.Windows.Forms.RadioButton()
        Me.Radio4 = New System.Windows.Forms.RadioButton()
        Me.PicComOpen = New System.Windows.Forms.PictureBox()
        Me.BtnGetSerialNo = New System.Windows.Forms.Button()
        Me.TextFeasaNo = New System.Windows.Forms.TextBox()
        Me.Btn_GetAllValues = New System.Windows.Forms.Button()
        Me.Btn_Capture = New System.Windows.Forms.Button()
        Me.ComboBrightLevel = New System.Windows.Forms.ComboBox()
        Me.DGVResult = New System.Windows.Forms.DataGridView()
        Me.D_Co1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Col8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Btn_ChangeFeasa = New System.Windows.Forms.Button()
        Me.TextFeasaChange = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Radio6 = New System.Windows.Forms.RadioButton()
        Me.Radio5 = New System.Windows.Forms.RadioButton()
        Me.ComboBoxFeasa = New System.Windows.Forms.ComboBox()
        Me.Btn_Validate = New System.Windows.Forms.Button()
        Me.Btn_StartCalibration = New System.Windows.Forms.Button()
        Me.TextBoxFile = New System.Windows.Forms.TextBox()
        CType(Me.PicComOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_OpenCom
        '
        Me.Btn_OpenCom.Location = New System.Drawing.Point(213, 44)
        Me.Btn_OpenCom.Name = "Btn_OpenCom"
        Me.Btn_OpenCom.Size = New System.Drawing.Size(101, 38)
        Me.Btn_OpenCom.TabIndex = 1
        Me.Btn_OpenCom.Text = "Open"
        Me.Btn_OpenCom.UseVisualStyleBackColor = True
        '
        'Radio1
        '
        Me.Radio1.AutoSize = True
        Me.Radio1.Location = New System.Drawing.Point(28, 34)
        Me.Radio1.Name = "Radio1"
        Me.Radio1.Size = New System.Drawing.Size(49, 17)
        Me.Radio1.TabIndex = 2
        Me.Radio1.TabStop = True
        Me.Radio1.Text = "Site1"
        Me.Radio1.UseVisualStyleBackColor = True
        '
        'Radio2
        '
        Me.Radio2.AutoSize = True
        Me.Radio2.Location = New System.Drawing.Point(83, 35)
        Me.Radio2.Name = "Radio2"
        Me.Radio2.Size = New System.Drawing.Size(49, 17)
        Me.Radio2.TabIndex = 2
        Me.Radio2.TabStop = True
        Me.Radio2.Text = "Site2"
        Me.Radio2.UseVisualStyleBackColor = True
        '
        'Radio3
        '
        Me.Radio3.AutoSize = True
        Me.Radio3.Location = New System.Drawing.Point(138, 35)
        Me.Radio3.Name = "Radio3"
        Me.Radio3.Size = New System.Drawing.Size(49, 17)
        Me.Radio3.TabIndex = 2
        Me.Radio3.TabStop = True
        Me.Radio3.Text = "Site3"
        Me.Radio3.UseVisualStyleBackColor = True
        '
        'Radio4
        '
        Me.Radio4.AutoSize = True
        Me.Radio4.Location = New System.Drawing.Point(29, 76)
        Me.Radio4.Name = "Radio4"
        Me.Radio4.Size = New System.Drawing.Size(49, 17)
        Me.Radio4.TabIndex = 2
        Me.Radio4.TabStop = True
        Me.Radio4.Text = "Site4"
        Me.Radio4.UseVisualStyleBackColor = True
        '
        'PicComOpen
        '
        Me.PicComOpen.InitialImage = Global.MyBECPro.My.Resources.Resources.LEDGray
        Me.PicComOpen.Location = New System.Drawing.Point(321, 44)
        Me.PicComOpen.Name = "PicComOpen"
        Me.PicComOpen.Size = New System.Drawing.Size(37, 38)
        Me.PicComOpen.TabIndex = 3
        Me.PicComOpen.TabStop = False
        '
        'BtnGetSerialNo
        '
        Me.BtnGetSerialNo.Location = New System.Drawing.Point(12, 164)
        Me.BtnGetSerialNo.Name = "BtnGetSerialNo"
        Me.BtnGetSerialNo.Size = New System.Drawing.Size(140, 29)
        Me.BtnGetSerialNo.TabIndex = 4
        Me.BtnGetSerialNo.Text = "GetFeasaNo"
        Me.BtnGetSerialNo.UseVisualStyleBackColor = True
        '
        'TextFeasaNo
        '
        Me.TextFeasaNo.Location = New System.Drawing.Point(161, 168)
        Me.TextFeasaNo.Name = "TextFeasaNo"
        Me.TextFeasaNo.Size = New System.Drawing.Size(140, 20)
        Me.TextFeasaNo.TabIndex = 5
        '
        'Btn_GetAllValues
        '
        Me.Btn_GetAllValues.Location = New System.Drawing.Point(11, 328)
        Me.Btn_GetAllValues.Name = "Btn_GetAllValues"
        Me.Btn_GetAllValues.Size = New System.Drawing.Size(289, 34)
        Me.Btn_GetAllValues.TabIndex = 6
        Me.Btn_GetAllValues.Text = "GetAllValue"
        Me.Btn_GetAllValues.UseVisualStyleBackColor = True
        '
        'Btn_Capture
        '
        Me.Btn_Capture.Location = New System.Drawing.Point(160, 282)
        Me.Btn_Capture.Name = "Btn_Capture"
        Me.Btn_Capture.Size = New System.Drawing.Size(140, 27)
        Me.Btn_Capture.TabIndex = 7
        Me.Btn_Capture.Text = "Capture"
        Me.Btn_Capture.UseVisualStyleBackColor = True
        '
        'ComboBrightLevel
        '
        Me.ComboBrightLevel.FormattingEnabled = True
        Me.ComboBrightLevel.Location = New System.Drawing.Point(11, 286)
        Me.ComboBrightLevel.Name = "ComboBrightLevel"
        Me.ComboBrightLevel.Size = New System.Drawing.Size(140, 21)
        Me.ComboBrightLevel.TabIndex = 8
        '
        'DGVResult
        '
        Me.DGVResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVResult.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.D_Co1, Me.D_Col2, Me.D_Col3, Me.D_Col4, Me.D_Col5, Me.D_Col6, Me.D_Col7, Me.D_Col8})
        Me.DGVResult.Location = New System.Drawing.Point(2, 13)
        Me.DGVResult.Name = "DGVResult"
        Me.DGVResult.RowTemplate.Height = 23
        Me.DGVResult.Size = New System.Drawing.Size(635, 609)
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
        'Btn_ChangeFeasa
        '
        Me.Btn_ChangeFeasa.Location = New System.Drawing.Point(159, 220)
        Me.Btn_ChangeFeasa.Name = "Btn_ChangeFeasa"
        Me.Btn_ChangeFeasa.Size = New System.Drawing.Size(140, 51)
        Me.Btn_ChangeFeasa.TabIndex = 12
        Me.Btn_ChangeFeasa.Text = "ChangeFeasa"
        Me.Btn_ChangeFeasa.UseVisualStyleBackColor = True
        '
        'TextFeasaChange
        '
        Me.TextFeasaChange.Location = New System.Drawing.Point(11, 247)
        Me.TextFeasaChange.Name = "TextFeasaChange"
        Me.TextFeasaChange.Size = New System.Drawing.Size(140, 20)
        Me.TextFeasaChange.TabIndex = 13
        '
        'GroupBox1
        '
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
        Me.GroupBox1.Location = New System.Drawing.Point(650, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(377, 378)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Base Operate"
        '
        'Radio6
        '
        Me.Radio6.AutoSize = True
        Me.Radio6.Location = New System.Drawing.Point(139, 76)
        Me.Radio6.Name = "Radio6"
        Me.Radio6.Size = New System.Drawing.Size(49, 17)
        Me.Radio6.TabIndex = 16
        Me.Radio6.TabStop = True
        Me.Radio6.Text = "Site6"
        Me.Radio6.UseVisualStyleBackColor = True
        '
        'Radio5
        '
        Me.Radio5.AutoSize = True
        Me.Radio5.Location = New System.Drawing.Point(84, 76)
        Me.Radio5.Name = "Radio5"
        Me.Radio5.Size = New System.Drawing.Size(49, 17)
        Me.Radio5.TabIndex = 15
        Me.Radio5.TabStop = True
        Me.Radio5.Text = "Site5"
        Me.Radio5.UseVisualStyleBackColor = True
        '
        'ComboBoxFeasa
        '
        Me.ComboBoxFeasa.FormattingEnabled = True
        Me.ComboBoxFeasa.Location = New System.Drawing.Point(12, 220)
        Me.ComboBoxFeasa.Name = "ComboBoxFeasa"
        Me.ComboBoxFeasa.Size = New System.Drawing.Size(139, 21)
        Me.ComboBoxFeasa.TabIndex = 14
        '
        'Btn_Validate
        '
        Me.Btn_Validate.Location = New System.Drawing.Point(733, 452)
        Me.Btn_Validate.Name = "Btn_Validate"
        Me.Btn_Validate.Size = New System.Drawing.Size(188, 25)
        Me.Btn_Validate.TabIndex = 15
        Me.Btn_Validate.Text = "Validation"
        Me.Btn_Validate.UseVisualStyleBackColor = True
        '
        'Btn_StartCalibration
        '
        Me.Btn_StartCalibration.Location = New System.Drawing.Point(733, 489)
        Me.Btn_StartCalibration.Name = "Btn_StartCalibration"
        Me.Btn_StartCalibration.Size = New System.Drawing.Size(188, 25)
        Me.Btn_StartCalibration.TabIndex = 16
        Me.Btn_StartCalibration.Text = "Calibration"
        Me.Btn_StartCalibration.UseVisualStyleBackColor = True
        '
        'TextBoxFile
        '
        Me.TextBoxFile.Location = New System.Drawing.Point(733, 419)
        Me.TextBoxFile.Name = "TextBoxFile"
        Me.TextBoxFile.Size = New System.Drawing.Size(188, 20)
        Me.TextBoxFile.TabIndex = 17
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1043, 625)
        Me.Controls.Add(Me.TextBoxFile)
        Me.Controls.Add(Me.Btn_StartCalibration)
        Me.Controls.Add(Me.Btn_Validate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DGVResult)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PicComOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_OpenCom As System.Windows.Forms.Button
    Friend WithEvents Radio1 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio2 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio3 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio4 As System.Windows.Forms.RadioButton
    Friend WithEvents PicComOpen As System.Windows.Forms.PictureBox
    Friend WithEvents BtnGetSerialNo As System.Windows.Forms.Button
    Friend WithEvents TextFeasaNo As System.Windows.Forms.TextBox
    Friend WithEvents Btn_GetAllValues As System.Windows.Forms.Button
    Friend WithEvents Btn_Capture As System.Windows.Forms.Button
    Friend WithEvents ComboBrightLevel As System.Windows.Forms.ComboBox
    Friend WithEvents DGVResult As System.Windows.Forms.DataGridView
    Friend WithEvents Btn_ChangeFeasa As System.Windows.Forms.Button
    Friend WithEvents TextFeasaChange As System.Windows.Forms.TextBox
    Friend WithEvents D_Co1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Col8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Btn_Validate As System.Windows.Forms.Button
    Friend WithEvents Btn_StartCalibration As System.Windows.Forms.Button
    Friend WithEvents TextBoxFile As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxFeasa As System.Windows.Forms.ComboBox
    Friend WithEvents Radio6 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio5 As System.Windows.Forms.RadioButton

End Class
