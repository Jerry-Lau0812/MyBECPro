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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Btn_Initial = New System.Windows.Forms.Button()
        Me.ButInt_CloseCom = New System.Windows.Forms.Button()
        Me.ButInt_OpenCom = New System.Windows.Forms.Button()
        Me.RadButUUT2 = New System.Windows.Forms.RadioButton()
        Me.RadButUUT1 = New System.Windows.Forms.RadioButton()
        Me.Btn_CMD_Com_Open = New System.Windows.Forms.Button()
        Me.Btn_CMD_Com_Close = New System.Windows.Forms.Button()
        Me.Btn_CMD_Set_SerialNum = New System.Windows.Forms.Button()
        Me.Btn_CMD_ReadResult = New System.Windows.Forms.Button()
        Me.Btn_Normal_COM_Open = New System.Windows.Forms.Button()
        Me.Btn_Normal_COM_Close = New System.Windows.Forms.Button()
        Me.Btn_Enter_TestMode = New System.Windows.Forms.Button()
        Me.CmdCmds = New System.Windows.Forms.ComboBox()
        Me.Btn_SendCmd = New System.Windows.Forms.Button()
        Me.Txt_Status = New System.Windows.Forms.TextBox()
        Me.Btn_ReadInfo = New System.Windows.Forms.Button()
        Me.Btn_ReadIDTable = New System.Windows.Forms.Button()
        Me.TextSN = New System.Windows.Forms.TextBox()
        Me.Btn_SWVer = New System.Windows.Forms.Button()
        Me.Txt_Write_DGTO = New System.Windows.Forms.TextBox()
        Me.Btn_WriteDGTO = New System.Windows.Forms.Button()
        Me.Txt_DGTO_Value = New System.Windows.Forms.TextBox()
        Me.Btn_Write_TDA = New System.Windows.Forms.Button()
        Me.Btn_Read_DGTO = New System.Windows.Forms.Button()
        Me.TxtReadDGTO = New System.Windows.Forms.TextBox()
        Me.Btn_WriteTDARegister = New System.Windows.Forms.Button()
        Me.Btn_Read_SN = New System.Windows.Forms.Button()
        Me.Btn_ReadMac = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Btn_Initial)
        Me.GroupBox1.Controls.Add(Me.ButInt_CloseCom)
        Me.GroupBox1.Controls.Add(Me.ButInt_OpenCom)
        Me.GroupBox1.Controls.Add(Me.RadButUUT2)
        Me.GroupBox1.Controls.Add(Me.RadButUUT1)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 18)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(340, 75)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'Btn_Initial
        '
        Me.Btn_Initial.Location = New System.Drawing.Point(22, 20)
        Me.Btn_Initial.Name = "Btn_Initial"
        Me.Btn_Initial.Size = New System.Drawing.Size(127, 44)
        Me.Btn_Initial.TabIndex = 1
        Me.Btn_Initial.Text = "Initial"
        Me.Btn_Initial.UseVisualStyleBackColor = True
        '
        'ButInt_CloseCom
        '
        Me.ButInt_CloseCom.Location = New System.Drawing.Point(227, 45)
        Me.ButInt_CloseCom.Name = "ButInt_CloseCom"
        Me.ButInt_CloseCom.Size = New System.Drawing.Size(83, 23)
        Me.ButInt_CloseCom.TabIndex = 1
        Me.ButInt_CloseCom.Text = "QuitUTL070"
        Me.ButInt_CloseCom.UseVisualStyleBackColor = True
        '
        'ButInt_OpenCom
        '
        Me.ButInt_OpenCom.Location = New System.Drawing.Point(227, 16)
        Me.ButInt_OpenCom.Name = "ButInt_OpenCom"
        Me.ButInt_OpenCom.Size = New System.Drawing.Size(83, 23)
        Me.ButInt_OpenCom.TabIndex = 1
        Me.ButInt_OpenCom.Text = "StartUTL070"
        Me.ButInt_OpenCom.UseVisualStyleBackColor = True
        '
        'RadButUUT2
        '
        Me.RadButUUT2.AutoSize = True
        Me.RadButUUT2.Location = New System.Drawing.Point(179, 48)
        Me.RadButUUT2.Name = "RadButUUT2"
        Me.RadButUUT2.Size = New System.Drawing.Size(29, 16)
        Me.RadButUUT2.TabIndex = 0
        Me.RadButUUT2.TabStop = True
        Me.RadButUUT2.Text = "2"
        Me.RadButUUT2.UseVisualStyleBackColor = True
        '
        'RadButUUT1
        '
        Me.RadButUUT1.AutoSize = True
        Me.RadButUUT1.Location = New System.Drawing.Point(179, 20)
        Me.RadButUUT1.Name = "RadButUUT1"
        Me.RadButUUT1.Size = New System.Drawing.Size(29, 16)
        Me.RadButUUT1.TabIndex = 0
        Me.RadButUUT1.TabStop = True
        Me.RadButUUT1.Text = "1"
        Me.RadButUUT1.UseVisualStyleBackColor = True
        '
        'Btn_CMD_Com_Open
        '
        Me.Btn_CMD_Com_Open.Location = New System.Drawing.Point(22, 128)
        Me.Btn_CMD_Com_Open.Name = "Btn_CMD_Com_Open"
        Me.Btn_CMD_Com_Open.Size = New System.Drawing.Size(96, 23)
        Me.Btn_CMD_Com_Open.TabIndex = 1
        Me.Btn_CMD_Com_Open.Text = "CMD_Com_Open"
        Me.Btn_CMD_Com_Open.UseVisualStyleBackColor = True
        '
        'Btn_CMD_Com_Close
        '
        Me.Btn_CMD_Com_Close.Location = New System.Drawing.Point(22, 157)
        Me.Btn_CMD_Com_Close.Name = "Btn_CMD_Com_Close"
        Me.Btn_CMD_Com_Close.Size = New System.Drawing.Size(96, 23)
        Me.Btn_CMD_Com_Close.TabIndex = 2
        Me.Btn_CMD_Com_Close.Text = "CMD_Com_Close"
        Me.Btn_CMD_Com_Close.UseVisualStyleBackColor = True
        '
        'Btn_CMD_Set_SerialNum
        '
        Me.Btn_CMD_Set_SerialNum.Location = New System.Drawing.Point(152, 99)
        Me.Btn_CMD_Set_SerialNum.Name = "Btn_CMD_Set_SerialNum"
        Me.Btn_CMD_Set_SerialNum.Size = New System.Drawing.Size(96, 23)
        Me.Btn_CMD_Set_SerialNum.TabIndex = 3
        Me.Btn_CMD_Set_SerialNum.Text = "CMD_Set_SN"
        Me.Btn_CMD_Set_SerialNum.UseVisualStyleBackColor = True
        '
        'Btn_CMD_ReadResult
        '
        Me.Btn_CMD_ReadResult.Location = New System.Drawing.Point(268, 244)
        Me.Btn_CMD_ReadResult.Name = "Btn_CMD_ReadResult"
        Me.Btn_CMD_ReadResult.Size = New System.Drawing.Size(115, 50)
        Me.Btn_CMD_ReadResult.TabIndex = 4
        Me.Btn_CMD_ReadResult.Text = "CMD_Read_Result"
        Me.Btn_CMD_ReadResult.UseVisualStyleBackColor = True
        '
        'Btn_Normal_COM_Open
        '
        Me.Btn_Normal_COM_Open.Location = New System.Drawing.Point(468, 35)
        Me.Btn_Normal_COM_Open.Name = "Btn_Normal_COM_Open"
        Me.Btn_Normal_COM_Open.Size = New System.Drawing.Size(98, 23)
        Me.Btn_Normal_COM_Open.TabIndex = 5
        Me.Btn_Normal_COM_Open.Text = "StartNormalCom"
        Me.Btn_Normal_COM_Open.UseVisualStyleBackColor = True
        '
        'Btn_Normal_COM_Close
        '
        Me.Btn_Normal_COM_Close.Location = New System.Drawing.Point(468, 64)
        Me.Btn_Normal_COM_Close.Name = "Btn_Normal_COM_Close"
        Me.Btn_Normal_COM_Close.Size = New System.Drawing.Size(98, 23)
        Me.Btn_Normal_COM_Close.TabIndex = 5
        Me.Btn_Normal_COM_Close.Text = "QuitNormalCom"
        Me.Btn_Normal_COM_Close.UseVisualStyleBackColor = True
        '
        'Btn_Enter_TestMode
        '
        Me.Btn_Enter_TestMode.Location = New System.Drawing.Point(592, 35)
        Me.Btn_Enter_TestMode.Name = "Btn_Enter_TestMode"
        Me.Btn_Enter_TestMode.Size = New System.Drawing.Size(98, 23)
        Me.Btn_Enter_TestMode.TabIndex = 5
        Me.Btn_Enter_TestMode.Text = "EnterTestMode"
        Me.Btn_Enter_TestMode.UseVisualStyleBackColor = True
        '
        'CmdCmds
        '
        Me.CmdCmds.FormattingEnabled = True
        Me.CmdCmds.Location = New System.Drawing.Point(465, 115)
        Me.CmdCmds.Name = "CmdCmds"
        Me.CmdCmds.Size = New System.Drawing.Size(121, 20)
        Me.CmdCmds.TabIndex = 6
        '
        'Btn_SendCmd
        '
        Me.Btn_SendCmd.Location = New System.Drawing.Point(592, 112)
        Me.Btn_SendCmd.Name = "Btn_SendCmd"
        Me.Btn_SendCmd.Size = New System.Drawing.Size(98, 23)
        Me.Btn_SendCmd.TabIndex = 7
        Me.Btn_SendCmd.Text = "SendCmd"
        Me.Btn_SendCmd.UseVisualStyleBackColor = True
        '
        'Txt_Status
        '
        Me.Txt_Status.Location = New System.Drawing.Point(468, 171)
        Me.Txt_Status.Multiline = True
        Me.Txt_Status.Name = "Txt_Status"
        Me.Txt_Status.Size = New System.Drawing.Size(222, 267)
        Me.Txt_Status.TabIndex = 8
        '
        'Btn_ReadInfo
        '
        Me.Btn_ReadInfo.Location = New System.Drawing.Point(592, 142)
        Me.Btn_ReadInfo.Name = "Btn_ReadInfo"
        Me.Btn_ReadInfo.Size = New System.Drawing.Size(98, 23)
        Me.Btn_ReadInfo.TabIndex = 9
        Me.Btn_ReadInfo.Text = "ReadInfo"
        Me.Btn_ReadInfo.UseVisualStyleBackColor = True
        '
        'Btn_ReadIDTable
        '
        Me.Btn_ReadIDTable.Location = New System.Drawing.Point(22, 186)
        Me.Btn_ReadIDTable.Name = "Btn_ReadIDTable"
        Me.Btn_ReadIDTable.Size = New System.Drawing.Size(96, 23)
        Me.Btn_ReadIDTable.TabIndex = 10
        Me.Btn_ReadIDTable.Text = "ReadIDTable"
        Me.Btn_ReadIDTable.UseVisualStyleBackColor = True
        '
        'TextSN
        '
        Me.TextSN.Location = New System.Drawing.Point(22, 99)
        Me.TextSN.Name = "TextSN"
        Me.TextSN.Size = New System.Drawing.Size(124, 21)
        Me.TextSN.TabIndex = 11
        Me.TextSN.Text = "041921VC0001"
        '
        'Btn_SWVer
        '
        Me.Btn_SWVer.Location = New System.Drawing.Point(22, 216)
        Me.Btn_SWVer.Name = "Btn_SWVer"
        Me.Btn_SWVer.Size = New System.Drawing.Size(96, 23)
        Me.Btn_SWVer.TabIndex = 12
        Me.Btn_SWVer.Text = "Read SW VER"
        Me.Btn_SWVer.UseVisualStyleBackColor = True
        '
        'Txt_Write_DGTO
        '
        Me.Txt_Write_DGTO.Location = New System.Drawing.Point(22, 313)
        Me.Txt_Write_DGTO.Name = "Txt_Write_DGTO"
        Me.Txt_Write_DGTO.Size = New System.Drawing.Size(124, 21)
        Me.Txt_Write_DGTO.TabIndex = 13
        '
        'Btn_WriteDGTO
        '
        Me.Btn_WriteDGTO.Location = New System.Drawing.Point(152, 313)
        Me.Btn_WriteDGTO.Name = "Btn_WriteDGTO"
        Me.Btn_WriteDGTO.Size = New System.Drawing.Size(96, 49)
        Me.Btn_WriteDGTO.TabIndex = 14
        Me.Btn_WriteDGTO.Text = "Write DGTO"
        Me.Btn_WriteDGTO.UseVisualStyleBackColor = True
        '
        'Txt_DGTO_Value
        '
        Me.Txt_DGTO_Value.Location = New System.Drawing.Point(22, 341)
        Me.Txt_DGTO_Value.Name = "Txt_DGTO_Value"
        Me.Txt_DGTO_Value.Size = New System.Drawing.Size(124, 21)
        Me.Txt_DGTO_Value.TabIndex = 15
        '
        'Btn_Write_TDA
        '
        Me.Btn_Write_TDA.Location = New System.Drawing.Point(22, 245)
        Me.Btn_Write_TDA.Name = "Btn_Write_TDA"
        Me.Btn_Write_TDA.Size = New System.Drawing.Size(96, 23)
        Me.Btn_Write_TDA.TabIndex = 16
        Me.Btn_Write_TDA.Text = "Write_TDA"
        Me.Btn_Write_TDA.UseVisualStyleBackColor = True
        '
        'Btn_Read_DGTO
        '
        Me.Btn_Read_DGTO.Location = New System.Drawing.Point(152, 374)
        Me.Btn_Read_DGTO.Name = "Btn_Read_DGTO"
        Me.Btn_Read_DGTO.Size = New System.Drawing.Size(96, 49)
        Me.Btn_Read_DGTO.TabIndex = 17
        Me.Btn_Read_DGTO.Text = "Read DGTO"
        Me.Btn_Read_DGTO.UseVisualStyleBackColor = True
        '
        'TxtReadDGTO
        '
        Me.TxtReadDGTO.Location = New System.Drawing.Point(16, 389)
        Me.TxtReadDGTO.Name = "TxtReadDGTO"
        Me.TxtReadDGTO.Size = New System.Drawing.Size(130, 21)
        Me.TxtReadDGTO.TabIndex = 18
        '
        'Btn_WriteTDARegister
        '
        Me.Btn_WriteTDARegister.Location = New System.Drawing.Point(22, 274)
        Me.Btn_WriteTDARegister.Name = "Btn_WriteTDARegister"
        Me.Btn_WriteTDARegister.Size = New System.Drawing.Size(96, 23)
        Me.Btn_WriteTDARegister.TabIndex = 19
        Me.Btn_WriteTDARegister.Text = "Write_TDA_Result"
        Me.Btn_WriteTDARegister.UseVisualStyleBackColor = True
        '
        'Btn_Read_SN
        '
        Me.Btn_Read_SN.Location = New System.Drawing.Point(152, 128)
        Me.Btn_Read_SN.Name = "Btn_Read_SN"
        Me.Btn_Read_SN.Size = New System.Drawing.Size(96, 23)
        Me.Btn_Read_SN.TabIndex = 20
        Me.Btn_Read_SN.Text = "ReadSN"
        Me.Btn_Read_SN.UseVisualStyleBackColor = True
        '
        'Btn_ReadMac
        '
        Me.Btn_ReadMac.Location = New System.Drawing.Point(152, 157)
        Me.Btn_ReadMac.Name = "Btn_ReadMac"
        Me.Btn_ReadMac.Size = New System.Drawing.Size(96, 23)
        Me.Btn_ReadMac.TabIndex = 21
        Me.Btn_ReadMac.Text = "ReadMAC"
        Me.Btn_ReadMac.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(743, 450)
        Me.Controls.Add(Me.Btn_ReadMac)
        Me.Controls.Add(Me.Btn_Read_SN)
        Me.Controls.Add(Me.Btn_WriteTDARegister)
        Me.Controls.Add(Me.TxtReadDGTO)
        Me.Controls.Add(Me.Btn_Read_DGTO)
        Me.Controls.Add(Me.Btn_Write_TDA)
        Me.Controls.Add(Me.Txt_DGTO_Value)
        Me.Controls.Add(Me.Btn_WriteDGTO)
        Me.Controls.Add(Me.Txt_Write_DGTO)
        Me.Controls.Add(Me.Btn_SWVer)
        Me.Controls.Add(Me.TextSN)
        Me.Controls.Add(Me.Btn_ReadIDTable)
        Me.Controls.Add(Me.Btn_ReadInfo)
        Me.Controls.Add(Me.Txt_Status)
        Me.Controls.Add(Me.Btn_SendCmd)
        Me.Controls.Add(Me.CmdCmds)
        Me.Controls.Add(Me.Btn_Normal_COM_Close)
        Me.Controls.Add(Me.Btn_Enter_TestMode)
        Me.Controls.Add(Me.Btn_Normal_COM_Open)
        Me.Controls.Add(Me.Btn_CMD_ReadResult)
        Me.Controls.Add(Me.Btn_CMD_Set_SerialNum)
        Me.Controls.Add(Me.Btn_CMD_Com_Close)
        Me.Controls.Add(Me.Btn_CMD_Com_Open)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadButUUT2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadButUUT1 As System.Windows.Forms.RadioButton
    Friend WithEvents ButInt_CloseCom As System.Windows.Forms.Button
    Friend WithEvents ButInt_OpenCom As System.Windows.Forms.Button
    Friend WithEvents Btn_Initial As System.Windows.Forms.Button
    Friend WithEvents Btn_CMD_Com_Open As System.Windows.Forms.Button
    Friend WithEvents Btn_CMD_Com_Close As System.Windows.Forms.Button
    Friend WithEvents Btn_CMD_Set_SerialNum As System.Windows.Forms.Button
    Friend WithEvents Btn_CMD_ReadResult As System.Windows.Forms.Button
    Friend WithEvents Btn_Normal_COM_Close As System.Windows.Forms.Button
    Friend WithEvents Btn_Normal_COM_Open As System.Windows.Forms.Button
    Friend WithEvents Btn_Enter_TestMode As System.Windows.Forms.Button
    Friend WithEvents CmdCmds As System.Windows.Forms.ComboBox
    Friend WithEvents Btn_SendCmd As System.Windows.Forms.Button
    Friend WithEvents Txt_Status As System.Windows.Forms.TextBox
    Friend WithEvents Btn_ReadInfo As System.Windows.Forms.Button
    Friend WithEvents Btn_ReadIDTable As System.Windows.Forms.Button
    Friend WithEvents TextSN As System.Windows.Forms.TextBox
    Friend WithEvents Btn_SWVer As System.Windows.Forms.Button
    Friend WithEvents Txt_Write_DGTO As System.Windows.Forms.TextBox
    Friend WithEvents Btn_WriteDGTO As System.Windows.Forms.Button
    Friend WithEvents Txt_DGTO_Value As System.Windows.Forms.TextBox
    Friend WithEvents Btn_Write_TDA As System.Windows.Forms.Button
    Friend WithEvents Btn_Read_DGTO As System.Windows.Forms.Button
    Friend WithEvents TxtReadDGTO As System.Windows.Forms.TextBox
    Friend WithEvents Btn_WriteTDARegister As System.Windows.Forms.Button
    Friend WithEvents Btn_Read_SN As System.Windows.Forms.Button
    Friend WithEvents Btn_ReadMac As System.Windows.Forms.Button

End Class
