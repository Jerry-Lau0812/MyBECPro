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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FileFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabHardware = New System.Windows.Forms.TabPage()
        Me.TabLEDs = New System.Windows.Forms.TabPage()
        Me.TabVersions = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 25)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.11723!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.88277!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(980, 563)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'FileFToolStripMenuItem
        '
        Me.FileFToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem1})
        Me.FileFToolStripMenuItem.Name = "FileFToolStripMenuItem"
        Me.FileFToolStripMenuItem.Size = New System.Drawing.Size(53, 21)
        Me.FileFToolStripMenuItem.Text = "File(&F)"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.SaveToolStripMenuItem.Text = "New(&N)"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.OpenToolStripMenuItem.Text = "Open(&O)"
        '
        'SaveToolStripMenuItem1
        '
        Me.SaveToolStripMenuItem1.Name = "SaveToolStripMenuItem1"
        Me.SaveToolStripMenuItem1.Size = New System.Drawing.Size(126, 22)
        Me.SaveToolStripMenuItem1.Text = "Save(&S)"
        '
        'EditEToolStripMenuItem
        '
        Me.EditEToolStripMenuItem.Name = "EditEToolStripMenuItem"
        Me.EditEToolStripMenuItem.Size = New System.Drawing.Size(57, 21)
        Me.EditEToolStripMenuItem.Text = "Edit(&E)"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileFToolStripMenuItem, Me.EditEToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(980, 25)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabHardware)
        Me.TabControl1.Controls.Add(Me.TabLEDs)
        Me.TabControl1.Controls.Add(Me.TabVersions)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(3, 105)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(974, 455)
        Me.TabControl1.TabIndex = 0
        '
        'TabHardware
        '
        Me.TabHardware.Location = New System.Drawing.Point(4, 22)
        Me.TabHardware.Name = "TabHardware"
        Me.TabHardware.Padding = New System.Windows.Forms.Padding(3)
        Me.TabHardware.Size = New System.Drawing.Size(966, 429)
        Me.TabHardware.TabIndex = 0
        Me.TabHardware.Text = "HARDWARES"
        Me.TabHardware.UseVisualStyleBackColor = True
        '
        'TabLEDs
        '
        Me.TabLEDs.Location = New System.Drawing.Point(4, 22)
        Me.TabLEDs.Name = "TabLEDs"
        Me.TabLEDs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabLEDs.Size = New System.Drawing.Size(966, 429)
        Me.TabLEDs.TabIndex = 1
        Me.TabLEDs.Text = "LEDs"
        Me.TabLEDs.UseVisualStyleBackColor = True
        '
        'TabVersions
        '
        Me.TabVersions.Location = New System.Drawing.Point(4, 22)
        Me.TabVersions.Name = "TabVersions"
        Me.TabVersions.Size = New System.Drawing.Size(966, 429)
        Me.TabVersions.TabIndex = 2
        Me.TabVersions.Text = "Versions"
        Me.TabVersions.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 588)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FileFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabHardware As System.Windows.Forms.TabPage
    Friend WithEvents TabLEDs As System.Windows.Forms.TabPage
    Friend WithEvents TabVersions As System.Windows.Forms.TabPage

End Class
