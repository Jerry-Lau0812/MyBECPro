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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ButStart = New System.Windows.Forms.Button()
        Me.ButStop = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.BGW = New System.ComponentModel.BackgroundWorker()
        Me.ButInitial = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripLblSpyStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLbl_DataIn = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLbl_DataOut = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButStart
        '
        Me.ButStart.Image = CType(resources.GetObject("ButStart.Image"), System.Drawing.Image)
        Me.ButStart.Location = New System.Drawing.Point(69, 22)
        Me.ButStart.Name = "ButStart"
        Me.ButStart.Size = New System.Drawing.Size(115, 45)
        Me.ButStart.TabIndex = 0
        Me.ButStart.UseVisualStyleBackColor = True
        '
        'ButStop
        '
        Me.ButStop.Image = CType(resources.GetObject("ButStop.Image"), System.Drawing.Image)
        Me.ButStop.Location = New System.Drawing.Point(69, 85)
        Me.ButStop.Name = "ButStop"
        Me.ButStop.Size = New System.Drawing.Size(115, 45)
        Me.ButStop.TabIndex = 1
        Me.ButStop.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.Location = New System.Drawing.Point(200, 85)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 43)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Quit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'BGW
        '
        '
        'ButInitial
        '
        Me.ButInitial.Location = New System.Drawing.Point(200, 22)
        Me.ButInitial.Name = "ButInitial"
        Me.ButInitial.Size = New System.Drawing.Size(115, 45)
        Me.ButInitial.TabIndex = 4
        Me.ButInitial.Text = "Initial"
        Me.ButInitial.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLblSpyStatus, Me.ToolStripLbl_DataIn, Me.ToolStripLbl_DataOut})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 156)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(427, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripLblSpyStatus
        '
        Me.ToolStripLblSpyStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLblSpyStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLblSpyStatus.Image = Global.ATGSERVER.My.Resources.Resources.bullet_black
        Me.ToolStripLblSpyStatus.Name = "ToolStripLblSpyStatus"
        Me.ToolStripLblSpyStatus.Size = New System.Drawing.Size(20, 17)
        '
        'ToolStripLbl_DataIn
        '
        Me.ToolStripLbl_DataIn.AutoSize = False
        Me.ToolStripLbl_DataIn.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLbl_DataIn.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLbl_DataIn.Name = "ToolStripLbl_DataIn"
        Me.ToolStripLbl_DataIn.Size = New System.Drawing.Size(150, 17)
        Me.ToolStripLbl_DataIn.Text = " "
        '
        'ToolStripLbl_DataOut
        '
        Me.ToolStripLbl_DataOut.AutoSize = False
        Me.ToolStripLbl_DataOut.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLbl_DataOut.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLbl_DataOut.Name = "ToolStripLbl_DataOut"
        Me.ToolStripLbl_DataOut.Size = New System.Drawing.Size(150, 17)
        Me.ToolStripLbl_DataOut.Text = "    "
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 178)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ButInitial)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ButStop)
        Me.Controls.Add(Me.ButStart)
        Me.Name = "Form1"
        Me.Text = "ATG_Server"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButStart As System.Windows.Forms.Button
    Friend WithEvents ButStop As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents BGW As System.ComponentModel.BackgroundWorker
    Friend WithEvents ButInitial As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripLblSpyStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLbl_DataIn As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLbl_DataOut As System.Windows.Forms.ToolStripStatusLabel

End Class
