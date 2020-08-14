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
        Me.Btn_Open = New System.Windows.Forms.Button()
        Me.Btn_GetVer = New System.Windows.Forms.Button()
        Me.Txt_Ver = New System.Windows.Forms.TextBox()
        Me.Btn_TransPrj = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Lbl_Msg = New System.Windows.Forms.Label()
        Me.Btn_Delete_Pro = New System.Windows.Forms.Button()
        Me.Btn_Flash = New System.Windows.Forms.Button()
        Me.Btn_Analyse = New System.Windows.Forms.Button()
        Me.Txt_Result = New System.Windows.Forms.TextBox()
        Me.TextFlashConfig = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblPassCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Lbl_FailCount = New System.Windows.Forms.Label()
        Me.Btn_CheckCRCPrj = New System.Windows.Forms.Button()
        Me.Button1Btn_CheckCRCFrb = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Btn_Open
        '
        Me.Btn_Open.Location = New System.Drawing.Point(22, 28)
        Me.Btn_Open.Name = "Btn_Open"
        Me.Btn_Open.Size = New System.Drawing.Size(93, 40)
        Me.Btn_Open.TabIndex = 0
        Me.Btn_Open.Text = "Open Lan Port"
        Me.Btn_Open.UseVisualStyleBackColor = True
        '
        'Btn_GetVer
        '
        Me.Btn_GetVer.Location = New System.Drawing.Point(22, 74)
        Me.Btn_GetVer.Name = "Btn_GetVer"
        Me.Btn_GetVer.Size = New System.Drawing.Size(93, 40)
        Me.Btn_GetVer.TabIndex = 1
        Me.Btn_GetVer.Text = "Get Ver."
        Me.Btn_GetVer.UseVisualStyleBackColor = True
        '
        'Txt_Ver
        '
        Me.Txt_Ver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Ver.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Ver.Location = New System.Drawing.Point(130, 78)
        Me.Txt_Ver.Name = "Txt_Ver"
        Me.Txt_Ver.Size = New System.Drawing.Size(195, 29)
        Me.Txt_Ver.TabIndex = 3
        '
        'Btn_TransPrj
        '
        Me.Btn_TransPrj.Location = New System.Drawing.Point(22, 347)
        Me.Btn_TransPrj.Name = "Btn_TransPrj"
        Me.Btn_TransPrj.Size = New System.Drawing.Size(93, 40)
        Me.Btn_TransPrj.TabIndex = 4
        Me.Btn_TransPrj.Text = "Transfer File"
        Me.Btn_TransPrj.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(130, 347)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(208, 40)
        Me.ProgressBar1.TabIndex = 5
        '
        'Lbl_Msg
        '
        Me.Lbl_Msg.AutoSize = True
        Me.Lbl_Msg.Location = New System.Drawing.Point(45, 414)
        Me.Lbl_Msg.Name = "Lbl_Msg"
        Me.Lbl_Msg.Size = New System.Drawing.Size(41, 12)
        Me.Lbl_Msg.TabIndex = 6
        Me.Lbl_Msg.Text = "Label1"
        '
        'Btn_Delete_Pro
        '
        Me.Btn_Delete_Pro.Location = New System.Drawing.Point(22, 288)
        Me.Btn_Delete_Pro.Name = "Btn_Delete_Pro"
        Me.Btn_Delete_Pro.Size = New System.Drawing.Size(93, 40)
        Me.Btn_Delete_Pro.TabIndex = 7
        Me.Btn_Delete_Pro.Text = "Delete Project"
        Me.Btn_Delete_Pro.UseVisualStyleBackColor = True
        '
        'Btn_Flash
        '
        Me.Btn_Flash.Location = New System.Drawing.Point(377, 28)
        Me.Btn_Flash.Name = "Btn_Flash"
        Me.Btn_Flash.Size = New System.Drawing.Size(93, 40)
        Me.Btn_Flash.TabIndex = 8
        Me.Btn_Flash.Text = "StartFlash"
        Me.Btn_Flash.UseVisualStyleBackColor = True
        '
        'Btn_Analyse
        '
        Me.Btn_Analyse.Location = New System.Drawing.Point(130, 288)
        Me.Btn_Analyse.Name = "Btn_Analyse"
        Me.Btn_Analyse.Size = New System.Drawing.Size(95, 40)
        Me.Btn_Analyse.TabIndex = 9
        Me.Btn_Analyse.Text = "AnalyseResult"
        Me.Btn_Analyse.UseVisualStyleBackColor = True
        '
        'Txt_Result
        '
        Me.Txt_Result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Result.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Result.Location = New System.Drawing.Point(377, 78)
        Me.Txt_Result.Multiline = True
        Me.Txt_Result.Name = "Txt_Result"
        Me.Txt_Result.Size = New System.Drawing.Size(229, 108)
        Me.Txt_Result.TabIndex = 3
        '
        'TextFlashConfig
        '
        Me.TextFlashConfig.Location = New System.Drawing.Point(476, 39)
        Me.TextFlashConfig.Multiline = True
        Me.TextFlashConfig.Name = "TextFlashConfig"
        Me.TextFlashConfig.Size = New System.Drawing.Size(231, 26)
        Me.TextFlashConfig.TabIndex = 10
        Me.TextFlashConfig.Text = "MASTER_BS0025_20191011.prj:0011"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(386, 212)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Pass"
        '
        'LblPassCount
        '
        Me.LblPassCount.AutoSize = True
        Me.LblPassCount.Location = New System.Drawing.Point(421, 212)
        Me.LblPassCount.Name = "LblPassCount"
        Me.LblPassCount.Size = New System.Drawing.Size(41, 12)
        Me.LblPassCount.TabIndex = 12
        Me.LblPassCount.Text = "Label2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(388, 241)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Fail"
        '
        'Lbl_FailCount
        '
        Me.Lbl_FailCount.AutoSize = True
        Me.Lbl_FailCount.Location = New System.Drawing.Point(423, 241)
        Me.Lbl_FailCount.Name = "Lbl_FailCount"
        Me.Lbl_FailCount.Size = New System.Drawing.Size(41, 12)
        Me.Lbl_FailCount.TabIndex = 14
        Me.Lbl_FailCount.Text = "Label3"
        '
        'Btn_CheckCRCPrj
        '
        Me.Btn_CheckCRCPrj.Location = New System.Drawing.Point(243, 288)
        Me.Btn_CheckCRCPrj.Name = "Btn_CheckCRCPrj"
        Me.Btn_CheckCRCPrj.Size = New System.Drawing.Size(95, 40)
        Me.Btn_CheckCRCPrj.TabIndex = 15
        Me.Btn_CheckCRCPrj.Text = "CheckCrcPrj"
        Me.Btn_CheckCRCPrj.UseVisualStyleBackColor = True
        '
        'Button1Btn_CheckCRCFrb
        '
        Me.Button1Btn_CheckCRCFrb.Location = New System.Drawing.Point(353, 288)
        Me.Button1Btn_CheckCRCFrb.Name = "Button1Btn_CheckCRCFrb"
        Me.Button1Btn_CheckCRCFrb.Size = New System.Drawing.Size(95, 40)
        Me.Button1Btn_CheckCRCFrb.TabIndex = 15
        Me.Button1Btn_CheckCRCFrb.Text = "CheckCrcFrb"
        Me.Button1Btn_CheckCRCFrb.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 465)
        Me.Controls.Add(Me.Button1Btn_CheckCRCFrb)
        Me.Controls.Add(Me.Btn_CheckCRCPrj)
        Me.Controls.Add(Me.Lbl_FailCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblPassCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextFlashConfig)
        Me.Controls.Add(Me.Btn_Analyse)
        Me.Controls.Add(Me.Btn_Flash)
        Me.Controls.Add(Me.Btn_Delete_Pro)
        Me.Controls.Add(Me.Lbl_Msg)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Btn_TransPrj)
        Me.Controls.Add(Me.Txt_Result)
        Me.Controls.Add(Me.Txt_Ver)
        Me.Controls.Add(Me.Btn_GetVer)
        Me.Controls.Add(Me.Btn_Open)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_Open As System.Windows.Forms.Button
    Friend WithEvents Btn_GetVer As System.Windows.Forms.Button
    Friend WithEvents Txt_Ver As System.Windows.Forms.TextBox
    Friend WithEvents Btn_TransPrj As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Lbl_Msg As System.Windows.Forms.Label
    Friend WithEvents Btn_Delete_Pro As System.Windows.Forms.Button
    Friend WithEvents Btn_Flash As System.Windows.Forms.Button
    Friend WithEvents Btn_Analyse As System.Windows.Forms.Button
    Friend WithEvents Txt_Result As System.Windows.Forms.TextBox
    Friend WithEvents TextFlashConfig As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblPassCount As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Lbl_FailCount As System.Windows.Forms.Label
    Friend WithEvents Btn_CheckCRCPrj As System.Windows.Forms.Button
    Friend WithEvents Button1Btn_CheckCRCFrb As System.Windows.Forms.Button

End Class
