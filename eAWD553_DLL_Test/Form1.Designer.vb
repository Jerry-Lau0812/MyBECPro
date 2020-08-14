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
        Me.Btn_Lin_Active = New System.Windows.Forms.Button()
        Me.Btn_LIN_Stop = New System.Windows.Forms.Button()
        Me.Btn_LIN_Sleep = New System.Windows.Forms.Button()
        Me.Btn_LIN_Wakeup = New System.Windows.Forms.Button()
        Me.Btn_Command_QVPM = New System.Windows.Forms.Button()
        Me.Btn_Get_FW = New System.Windows.Forms.Button()
        Me.Btn_ChangeSche = New System.Windows.Forms.Button()
        Me.Btn_GetFeedbackErr = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Btn_Lin_Active
        '
        Me.Btn_Lin_Active.Location = New System.Drawing.Point(12, 12)
        Me.Btn_Lin_Active.Name = "Btn_Lin_Active"
        Me.Btn_Lin_Active.Size = New System.Drawing.Size(97, 31)
        Me.Btn_Lin_Active.TabIndex = 0
        Me.Btn_Lin_Active.Text = "LIN_Active"
        Me.Btn_Lin_Active.UseVisualStyleBackColor = True
        '
        'Btn_LIN_Stop
        '
        Me.Btn_LIN_Stop.Location = New System.Drawing.Point(12, 49)
        Me.Btn_LIN_Stop.Name = "Btn_LIN_Stop"
        Me.Btn_LIN_Stop.Size = New System.Drawing.Size(97, 31)
        Me.Btn_LIN_Stop.TabIndex = 1
        Me.Btn_LIN_Stop.Text = "LIN_Deactive"
        Me.Btn_LIN_Stop.UseVisualStyleBackColor = True
        '
        'Btn_LIN_Sleep
        '
        Me.Btn_LIN_Sleep.Location = New System.Drawing.Point(115, 12)
        Me.Btn_LIN_Sleep.Name = "Btn_LIN_Sleep"
        Me.Btn_LIN_Sleep.Size = New System.Drawing.Size(97, 31)
        Me.Btn_LIN_Sleep.TabIndex = 2
        Me.Btn_LIN_Sleep.Text = "LIN_Sleep"
        Me.Btn_LIN_Sleep.UseVisualStyleBackColor = True
        '
        'Btn_LIN_Wakeup
        '
        Me.Btn_LIN_Wakeup.Location = New System.Drawing.Point(115, 49)
        Me.Btn_LIN_Wakeup.Name = "Btn_LIN_Wakeup"
        Me.Btn_LIN_Wakeup.Size = New System.Drawing.Size(97, 31)
        Me.Btn_LIN_Wakeup.TabIndex = 3
        Me.Btn_LIN_Wakeup.Text = "LIN_Wakeup"
        Me.Btn_LIN_Wakeup.UseVisualStyleBackColor = True
        '
        'Btn_Command_QVPM
        '
        Me.Btn_Command_QVPM.Location = New System.Drawing.Point(12, 143)
        Me.Btn_Command_QVPM.Name = "Btn_Command_QVPM"
        Me.Btn_Command_QVPM.Size = New System.Drawing.Size(97, 31)
        Me.Btn_Command_QVPM.TabIndex = 4
        Me.Btn_Command_QVPM.Text = "Command_QVPM"
        Me.Btn_Command_QVPM.UseVisualStyleBackColor = True
        '
        'Btn_Get_FW
        '
        Me.Btn_Get_FW.Location = New System.Drawing.Point(115, 143)
        Me.Btn_Get_FW.Name = "Btn_Get_FW"
        Me.Btn_Get_FW.Size = New System.Drawing.Size(97, 31)
        Me.Btn_Get_FW.TabIndex = 5
        Me.Btn_Get_FW.Text = "GetFW"
        Me.Btn_Get_FW.UseVisualStyleBackColor = True
        '
        'Btn_ChangeSche
        '
        Me.Btn_ChangeSche.Location = New System.Drawing.Point(12, 95)
        Me.Btn_ChangeSche.Name = "Btn_ChangeSche"
        Me.Btn_ChangeSche.Size = New System.Drawing.Size(200, 31)
        Me.Btn_ChangeSche.TabIndex = 6
        Me.Btn_ChangeSche.Text = "Change Schedule"
        Me.Btn_ChangeSche.UseVisualStyleBackColor = True
        '
        'Btn_GetFeedbackErr
        '
        Me.Btn_GetFeedbackErr.Location = New System.Drawing.Point(12, 190)
        Me.Btn_GetFeedbackErr.Name = "Btn_GetFeedbackErr"
        Me.Btn_GetFeedbackErr.Size = New System.Drawing.Size(200, 31)
        Me.Btn_GetFeedbackErr.TabIndex = 7
        Me.Btn_GetFeedbackErr.Text = "GetFeedbackError"
        Me.Btn_GetFeedbackErr.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 262)
        Me.Controls.Add(Me.Btn_GetFeedbackErr)
        Me.Controls.Add(Me.Btn_ChangeSche)
        Me.Controls.Add(Me.Btn_Get_FW)
        Me.Controls.Add(Me.Btn_Command_QVPM)
        Me.Controls.Add(Me.Btn_LIN_Wakeup)
        Me.Controls.Add(Me.Btn_LIN_Sleep)
        Me.Controls.Add(Me.Btn_LIN_Stop)
        Me.Controls.Add(Me.Btn_Lin_Active)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Btn_Lin_Active As System.Windows.Forms.Button
    Friend WithEvents Btn_LIN_Stop As System.Windows.Forms.Button
    Friend WithEvents Btn_LIN_Sleep As System.Windows.Forms.Button
    Friend WithEvents Btn_LIN_Wakeup As System.Windows.Forms.Button
    Friend WithEvents Btn_Command_QVPM As System.Windows.Forms.Button
    Friend WithEvents Btn_Get_FW As System.Windows.Forms.Button
    Friend WithEvents Btn_ChangeSche As System.Windows.Forms.Button
    Friend WithEvents Btn_GetFeedbackErr As System.Windows.Forms.Button

End Class
