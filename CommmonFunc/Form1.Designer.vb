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
        Me.TxtStrRaw = New System.Windows.Forms.TextBox()
        Me.Btn_Hex2Asci = New System.Windows.Forms.Button()
        Me.TxtStrRes = New System.Windows.Forms.TextBox()
        Me.Btn_Compare = New System.Windows.Forms.Button()
        Me.TxtTarget = New System.Windows.Forms.TextBox()
        Me.TxtRaw = New System.Windows.Forms.TextBox()
        Me.TxtDiff = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtStrRaw
        '
        Me.TxtStrRaw.Location = New System.Drawing.Point(30, 38)
        Me.TxtStrRaw.Name = "TxtStrRaw"
        Me.TxtStrRaw.Size = New System.Drawing.Size(284, 21)
        Me.TxtStrRaw.TabIndex = 0
        '
        'Btn_Hex2Asci
        '
        Me.Btn_Hex2Asci.Location = New System.Drawing.Point(336, 36)
        Me.Btn_Hex2Asci.Name = "Btn_Hex2Asci"
        Me.Btn_Hex2Asci.Size = New System.Drawing.Size(75, 51)
        Me.Btn_Hex2Asci.TabIndex = 1
        Me.Btn_Hex2Asci.Text = "Hex2Ascii"
        Me.Btn_Hex2Asci.UseVisualStyleBackColor = True
        '
        'TxtStrRes
        '
        Me.TxtStrRes.Location = New System.Drawing.Point(30, 66)
        Me.TxtStrRes.Name = "TxtStrRes"
        Me.TxtStrRes.Size = New System.Drawing.Size(284, 21)
        Me.TxtStrRes.TabIndex = 2
        '
        'Btn_Compare
        '
        Me.Btn_Compare.Location = New System.Drawing.Point(336, 111)
        Me.Btn_Compare.Name = "Btn_Compare"
        Me.Btn_Compare.Size = New System.Drawing.Size(75, 49)
        Me.Btn_Compare.TabIndex = 3
        Me.Btn_Compare.Text = "CompareBit"
        Me.Btn_Compare.UseVisualStyleBackColor = True
        '
        'TxtTarget
        '
        Me.TxtTarget.Location = New System.Drawing.Point(40, 138)
        Me.TxtTarget.Name = "TxtTarget"
        Me.TxtTarget.Size = New System.Drawing.Size(284, 21)
        Me.TxtTarget.TabIndex = 4
        '
        'TxtRaw
        '
        Me.TxtRaw.Location = New System.Drawing.Point(40, 111)
        Me.TxtRaw.Name = "TxtRaw"
        Me.TxtRaw.Size = New System.Drawing.Size(284, 21)
        Me.TxtRaw.TabIndex = 5
        '
        'TxtDiff
        '
        Me.TxtDiff.Location = New System.Drawing.Point(148, 165)
        Me.TxtDiff.Name = "TxtDiff"
        Me.TxtDiff.Size = New System.Drawing.Size(166, 21)
        Me.TxtDiff.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(56, 221)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 537)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TxtDiff)
        Me.Controls.Add(Me.TxtRaw)
        Me.Controls.Add(Me.TxtTarget)
        Me.Controls.Add(Me.Btn_Compare)
        Me.Controls.Add(Me.TxtStrRes)
        Me.Controls.Add(Me.Btn_Hex2Asci)
        Me.Controls.Add(Me.TxtStrRaw)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtStrRaw As System.Windows.Forms.TextBox
    Friend WithEvents Btn_Hex2Asci As System.Windows.Forms.Button
    Friend WithEvents TxtStrRes As System.Windows.Forms.TextBox
    Friend WithEvents Btn_Compare As System.Windows.Forms.Button
    Friend WithEvents TxtTarget As System.Windows.Forms.TextBox
    Friend WithEvents TxtRaw As System.Windows.Forms.TextBox
    Friend WithEvents TxtDiff As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
