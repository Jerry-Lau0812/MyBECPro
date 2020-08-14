<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSetup
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.GridDispFeasa = New System.Windows.Forms.DataGridView()
        Me.Ch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Color = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SITE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HUE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saturation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Intensity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dominant_Wavelength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Intensity_Abosolute = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbRange = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CmbLed = New System.Windows.Forms.ComboBox()
        Me.TxtDispfeasa = New System.Windows.Forms.TextBox()
        Me.Button59 = New System.Windows.Forms.Button()
        Me.Button58 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.LblSite = New System.Windows.Forms.Label()
        Me.LblFeasa = New System.Windows.Forms.Label()
        Me.TabPage7.SuspendLayout()
        CType(Me.GridDispFeasa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label13
        '
        Me.Label13.AllowDrop = True
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.Font = New System.Drawing.Font("Arial", 26.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Navy
        Me.Label13.Location = New System.Drawing.Point(23, 9)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(514, 42)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "Remote control"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.GridDispFeasa)
        Me.TabPage7.Controls.Add(Me.Button7)
        Me.TabPage7.Controls.Add(Me.Button4)
        Me.TabPage7.Controls.Add(Me.Label1)
        Me.TabPage7.Controls.Add(Me.CmbRange)
        Me.TabPage7.Controls.Add(Me.GroupBox1)
        Me.TabPage7.Controls.Add(Me.TxtDispfeasa)
        Me.TabPage7.Controls.Add(Me.Button59)
        Me.TabPage7.Controls.Add(Me.Button58)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(1077, 629)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "FEASA-UDA"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'GridDispFeasa
        '
        Me.GridDispFeasa.AllowUserToAddRows = False
        Me.GridDispFeasa.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GridDispFeasa.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridDispFeasa.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridDispFeasa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridDispFeasa.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ch, Me.Name, Me.Color, Me.SITE, Me.HUE, Me.Saturation, Me.Intensity, Me.Cx, Me.Cy, Me.Dominant_Wavelength, Me.Intensity_Abosolute})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridDispFeasa.DefaultCellStyle = DataGridViewCellStyle4
        Me.GridDispFeasa.Location = New System.Drawing.Point(10, 14)
        Me.GridDispFeasa.Margin = New System.Windows.Forms.Padding(4)
        Me.GridDispFeasa.Name = "GridDispFeasa"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridDispFeasa.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.GridDispFeasa.RowHeadersVisible = False
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.GridDispFeasa.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GridDispFeasa.Size = New System.Drawing.Size(1044, 380)
        Me.GridDispFeasa.TabIndex = 46
        '
        'Ch
        '
        Me.Ch.HeaderText = "CH"
        Me.Ch.Name = "Ch"
        Me.Ch.Width = 50
        '
        'Name
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.Name.DefaultCellStyle = DataGridViewCellStyle2
        Me.Name.FillWeight = 30.0!
        Me.Name.HeaderText = "Led Name"
        Me.Name.Name = "Name"
        Me.Name.ReadOnly = True
        '
        'Color
        '
        Me.Color.HeaderText = "Color"
        Me.Color.Name = "Color"
        '
        'SITE
        '
        Me.SITE.HeaderText = "SITE"
        Me.SITE.Name = "SITE"
        '
        'HUE
        '
        Me.HUE.HeaderText = "HUE"
        Me.HUE.Name = "HUE"
        Me.HUE.Width = 50
        '
        'Saturation
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.Saturation.DefaultCellStyle = DataGridViewCellStyle3
        Me.Saturation.HeaderText = "Saturation"
        Me.Saturation.Name = "Saturation"
        Me.Saturation.ReadOnly = True
        '
        'Intensity
        '
        Me.Intensity.HeaderText = "Intensity Relative"
        Me.Intensity.Name = "Intensity"
        '
        'Cx
        '
        Me.Cx.HeaderText = "Cx"
        Me.Cx.Name = "Cx"
        '
        'Cy
        '
        Me.Cy.HeaderText = "Cy"
        Me.Cy.Name = "Cy"
        '
        'Dominant_Wavelength
        '
        Me.Dominant_Wavelength.HeaderText = "Dominant Wavelength"
        Me.Dominant_Wavelength.Name = "Dominant_Wavelength"
        '
        'Intensity_Abosolute
        '
        Me.Intensity_Abosolute.HeaderText = "Intensity Abosolute"
        Me.Intensity_Abosolute.Name = "Intensity_Abosolute"
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(789, 401)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(265, 21)
        Me.Button7.TabIndex = 45
        Me.Button7.Text = "Calibration ALL"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(18, 401)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(128, 19)
        Me.Button4.TabIndex = 35
        Me.Button4.Text = "Get Fease  HW"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(158, 430)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Feasa Range"
        '
        'CmbRange
        '
        Me.CmbRange.FormattingEnabled = True
        Me.CmbRange.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.CmbRange.Location = New System.Drawing.Point(237, 427)
        Me.CmbRange.Name = "CmbRange"
        Me.CmbRange.Size = New System.Drawing.Size(52, 21)
        Me.CmbRange.TabIndex = 32
        Me.CmbRange.Text = "1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.CmbLed)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 454)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(361, 97)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Power Led Using Driver"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(224, 51)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(131, 20)
        Me.Button2.TabIndex = 37
        Me.Button2.Text = "OFF LED"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Led Name"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(68, 51)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(131, 20)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "ON LED"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CmbLed
        '
        Me.CmbLed.FormattingEnabled = True
        Me.CmbLed.Location = New System.Drawing.Point(68, 24)
        Me.CmbLed.Name = "CmbLed"
        Me.CmbLed.Size = New System.Drawing.Size(95, 21)
        Me.CmbLed.TabIndex = 30
        '
        'TxtDispfeasa
        '
        Me.TxtDispfeasa.Location = New System.Drawing.Point(10, 557)
        Me.TxtDispfeasa.Multiline = True
        Me.TxtDispfeasa.Name = "TxtDispfeasa"
        Me.TxtDispfeasa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtDispfeasa.Size = New System.Drawing.Size(869, 55)
        Me.TxtDispfeasa.TabIndex = 27
        '
        'Button59
        '
        Me.Button59.Location = New System.Drawing.Point(266, 401)
        Me.Button59.Name = "Button59"
        Me.Button59.Size = New System.Drawing.Size(128, 19)
        Me.Button59.TabIndex = 26
        Me.Button59.Text = "Get Feasa Serial"
        Me.Button59.UseVisualStyleBackColor = True
        '
        'Button58
        '
        Me.Button58.Location = New System.Drawing.Point(18, 426)
        Me.Button58.Name = "Button58"
        Me.Button58.Size = New System.Drawing.Size(128, 20)
        Me.Button58.TabIndex = 25
        Me.Button58.Text = "Feasa Acquire ALL CH"
        Me.Button58.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Location = New System.Drawing.Point(23, 54)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1085, 655)
        Me.TabControl1.TabIndex = 23
        '
        'LblSite
        '
        Me.LblSite.AllowDrop = True
        Me.LblSite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblSite.Font = New System.Drawing.Font("Arial", 26.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite.ForeColor = System.Drawing.Color.Navy
        Me.LblSite.Location = New System.Drawing.Point(954, 9)
        Me.LblSite.Name = "LblSite"
        Me.LblSite.Size = New System.Drawing.Size(147, 42)
        Me.LblSite.TabIndex = 24
        Me.LblSite.Text = "Site:"
        Me.LblSite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblFeasa
        '
        Me.LblFeasa.AllowDrop = True
        Me.LblFeasa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblFeasa.Font = New System.Drawing.Font("Arial", 26.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFeasa.ForeColor = System.Drawing.Color.Red
        Me.LblFeasa.Location = New System.Drawing.Point(21, 9)
        Me.LblFeasa.Name = "LblFeasa"
        Me.LblFeasa.Size = New System.Drawing.Size(914, 42)
        Me.LblFeasa.TabIndex = 26
        Me.LblFeasa.Text = "WAIT END FEASA CALIBRATION"
        Me.LblFeasa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblFeasa.Visible = False
        '
        'FrmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1135, 721)
        Me.Controls.Add(Me.LblFeasa)
        Me.Controls.Add(Me.LblSite)
        Me.Controls.Add(Me.TabControl1)

        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Diagnos Automation"
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        CType(Me.GridDispFeasa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TxtDispfeasa As System.Windows.Forms.TextBox
    Friend WithEvents Button59 As System.Windows.Forms.Button
    Friend WithEvents Button58 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents CmbLed As System.Windows.Forms.ComboBox
    Friend WithEvents CmbRange As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblSite As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents LblFeasa As System.Windows.Forms.Label
    Friend WithEvents GridDispFeasa As System.Windows.Forms.DataGridView
    Friend WithEvents Ch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SITE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HUE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saturation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Intensity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cx As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dominant_Wavelength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Intensity_Abosolute As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
