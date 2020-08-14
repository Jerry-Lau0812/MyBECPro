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
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.CmdPhaseID = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Btn_StartCollect = New System.Windows.Forms.Button()
        Me.Btn_StopCollect = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.StripStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TxtMsg = New System.Windows.Forms.TextBox()
        Me.TxtBitronCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TxtOldSerail = New System.Windows.Forms.TextBox()
        Me.ButGetDBDate = New System.Windows.Forms.Button()
        Me.ButCheckPrePhase = New System.Windows.Forms.Button()
        Me.TxtQueryTestRecord = New System.Windows.Forms.TextBox()
        Me.Txt_Src_FileParh = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Des_FileParh = New System.Windows.Forms.TextBox()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(14, 82)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(163, 19)
        Me.Button4.TabIndex = 107
        Me.Button4.Text = "Link Serial"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'CmdPhaseID
        '
        Me.CmdPhaseID.FormattingEnabled = True
        Me.CmdPhaseID.Location = New System.Drawing.Point(26, 68)
        Me.CmdPhaseID.Name = "CmdPhaseID"
        Me.CmdPhaseID.Size = New System.Drawing.Size(129, 20)
        Me.CmdPhaseID.TabIndex = 106
        Me.CmdPhaseID.Text = "65000036"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(170, 68)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(149, 19)
        Me.Button2.TabIndex = 105
        Me.Button2.Text = "ChangePhaseSetup"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(248, 76)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(130, 19)
        Me.Button3.TabIndex = 104
        Me.Button3.Text = "Save Pass Result"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Btn_StartCollect
        '
        Me.Btn_StartCollect.Location = New System.Drawing.Point(633, 230)
        Me.Btn_StartCollect.Name = "Btn_StartCollect"
        Me.Btn_StartCollect.Size = New System.Drawing.Size(89, 23)
        Me.Btn_StartCollect.TabIndex = 109
        Me.Btn_StartCollect.Text = "StartCollect"
        Me.Btn_StartCollect.UseVisualStyleBackColor = True
        Me.Btn_StartCollect.Visible = False
        '
        'Btn_StopCollect
        '
        Me.Btn_StopCollect.Location = New System.Drawing.Point(633, 273)
        Me.Btn_StopCollect.Name = "Btn_StopCollect"
        Me.Btn_StopCollect.Size = New System.Drawing.Size(89, 23)
        Me.Btn_StopCollect.TabIndex = 110
        Me.Btn_StopCollect.Text = "StopCollect"
        Me.Btn_StopCollect.UseVisualStyleBackColor = True
        Me.Btn_StopCollect.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StripStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 390)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(744, 22)
        Me.StatusStrip1.TabIndex = 111
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StripStatus
        '
        Me.StripStatus.Name = "StripStatus"
        Me.StripStatus.Size = New System.Drawing.Size(44, 17)
        Me.StripStatus.Text = "Ready"
        '
        'TxtMsg
        '
        Me.TxtMsg.Location = New System.Drawing.Point(12, 194)
        Me.TxtMsg.Multiline = True
        Me.TxtMsg.Name = "TxtMsg"
        Me.TxtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtMsg.Size = New System.Drawing.Size(615, 177)
        Me.TxtMsg.TabIndex = 112
        '
        'TxtBitronCode
        '
        Me.TxtBitronCode.Location = New System.Drawing.Point(170, 32)
        Me.TxtBitronCode.Name = "TxtBitronCode"
        Me.TxtBitronCode.Size = New System.Drawing.Size(149, 21)
        Me.TxtBitronCode.TabIndex = 113
        Me.TxtBitronCode.Text = "30415850"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 12)
        Me.Label1.TabIndex = 114
        Me.Label1.Text = "BitronCode:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_Des_FileParh)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Src_FileParh)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TxtBitronCode)
        Me.GroupBox1.Controls.Add(Me.CmdPhaseID)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Enabled = False
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 176)
        Me.GroupBox1.TabIndex = 115
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Eclipse Phase Setup"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TxtOldSerail)
        Me.GroupBox2.Controls.Add(Me.ButGetDBDate)
        Me.GroupBox2.Controls.Add(Me.ButCheckPrePhase)
        Me.GroupBox2.Controls.Add(Me.TxtQueryTestRecord)
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Location = New System.Drawing.Point(354, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(378, 176)
        Me.GroupBox2.TabIndex = 116
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Debug"
        '
        'TxtOldSerail
        '
        Me.TxtOldSerail.Location = New System.Drawing.Point(14, 48)
        Me.TxtOldSerail.Multiline = True
        Me.TxtOldSerail.Name = "TxtOldSerail"
        Me.TxtOldSerail.Size = New System.Drawing.Size(259, 22)
        Me.TxtOldSerail.TabIndex = 112
        Me.TxtOldSerail.Text = "24718001602120281558"
        '
        'ButGetDBDate
        '
        Me.ButGetDBDate.Location = New System.Drawing.Point(279, 20)
        Me.ButGetDBDate.Name = "ButGetDBDate"
        Me.ButGetDBDate.Size = New System.Drawing.Size(99, 19)
        Me.ButGetDBDate.TabIndex = 111
        Me.ButGetDBDate.Text = "Get DBDatetime"
        Me.ButGetDBDate.UseVisualStyleBackColor = True
        '
        'ButCheckPrePhase
        '
        Me.ButCheckPrePhase.Location = New System.Drawing.Point(279, 45)
        Me.ButCheckPrePhase.Name = "ButCheckPrePhase"
        Me.ButCheckPrePhase.Size = New System.Drawing.Size(99, 19)
        Me.ButCheckPrePhase.TabIndex = 110
        Me.ButCheckPrePhase.Text = "Check Prephase"
        Me.ButCheckPrePhase.UseVisualStyleBackColor = True
        '
        'TxtQueryTestRecord
        '
        Me.TxtQueryTestRecord.Location = New System.Drawing.Point(14, 20)
        Me.TxtQueryTestRecord.Multiline = True
        Me.TxtQueryTestRecord.Name = "TxtQueryTestRecord"
        Me.TxtQueryTestRecord.Size = New System.Drawing.Size(259, 22)
        Me.TxtQueryTestRecord.TabIndex = 109
        Me.TxtQueryTestRecord.Text = "MR146570-83701700010001170920180000000437"
        '
        'Txt_Src_FileParh
        '
        Me.Txt_Src_FileParh.Location = New System.Drawing.Point(131, 105)
        Me.Txt_Src_FileParh.Name = "Txt_Src_FileParh"
        Me.Txt_Src_FileParh.Size = New System.Drawing.Size(188, 21)
        Me.Txt_Src_FileParh.TabIndex = 115
        Me.Txt_Src_FileParh.Text = "E:\Report\TestReport"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 12)
        Me.Label2.TabIndex = 116
        Me.Label2.Text = "Src File Path"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 12)
        Me.Label3.TabIndex = 117
        Me.Label3.Text = "Des File Path"
        '
        'Txt_Des_FileParh
        '
        Me.Txt_Des_FileParh.Location = New System.Drawing.Point(131, 138)
        Me.Txt_Des_FileParh.Name = "Txt_Des_FileParh"
        Me.Txt_Des_FileParh.Size = New System.Drawing.Size(188, 21)
        Me.Txt_Des_FileParh.TabIndex = 118
        Me.Txt_Des_FileParh.Text = "E:\Report\TestClean"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 412)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtMsg)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Btn_StopCollect)
        Me.Controls.Add(Me.Btn_StartCollect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EclipseCollector"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents CmdPhaseID As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Btn_StartCollect As System.Windows.Forms.Button
    Friend WithEvents Btn_StopCollect As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StripStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TxtMsg As System.Windows.Forms.TextBox
    Friend WithEvents TxtBitronCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtOldSerail As System.Windows.Forms.TextBox
    Friend WithEvents ButGetDBDate As System.Windows.Forms.Button
    Friend WithEvents ButCheckPrePhase As System.Windows.Forms.Button
    Friend WithEvents TxtQueryTestRecord As System.Windows.Forms.TextBox
    Friend WithEvents Txt_Des_FileParh As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Txt_Src_FileParh As System.Windows.Forms.TextBox

End Class
