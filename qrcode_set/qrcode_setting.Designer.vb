<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class qrcode_setting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(qrcode_setting))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cb_code = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.b_h2 = New System.Windows.Forms.ComboBox()
        Me.e_m2 = New System.Windows.Forms.ComboBox()
        Me.b_m2 = New System.Windows.Forms.ComboBox()
        Me.b_m1 = New System.Windows.Forms.ComboBox()
        Me.e_h2 = New System.Windows.Forms.ComboBox()
        Me.e_m1 = New System.Windows.Forms.ComboBox()
        Me.b_h1 = New System.Windows.Forms.ComboBox()
        Me.e_h1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_IP = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.time_light = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.delay_time = New System.Windows.Forms.ComboBox()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_code)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 6)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(336, 57)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Code detail"
        '
        'cb_code
        '
        Me.cb_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_code.FormattingEnabled = True
        Me.cb_code.Items.AddRange(New Object() {"QRCODE", "DATA MATRIX"})
        Me.cb_code.Location = New System.Drawing.Point(14, 21)
        Me.cb_code.Margin = New System.Windows.Forms.Padding(2)
        Me.cb_code.Name = "cb_code"
        Me.cb_code.Size = New System.Drawing.Size(193, 21)
        Me.cb_code.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(247, 457)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(127, 34)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.b_h2)
        Me.GroupBox3.Controls.Add(Me.e_m2)
        Me.GroupBox3.Controls.Add(Me.b_m2)
        Me.GroupBox3.Controls.Add(Me.b_m1)
        Me.GroupBox3.Controls.Add(Me.e_h2)
        Me.GroupBox3.Controls.Add(Me.e_m1)
        Me.GroupBox3.Controls.Add(Me.b_h1)
        Me.GroupBox3.Controls.Add(Me.e_h1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(23, 352)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(336, 100)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "班別 Ca làm việc"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(188, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "-"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(188, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "-"
        '
        'b_h2
        '
        Me.b_h2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.b_h2.FormattingEnabled = True
        Me.b_h2.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.b_h2.Location = New System.Drawing.Point(66, 57)
        Me.b_h2.Name = "b_h2"
        Me.b_h2.Size = New System.Drawing.Size(55, 21)
        Me.b_h2.TabIndex = 1
        '
        'e_m2
        '
        Me.e_m2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.e_m2.FormattingEnabled = True
        Me.e_m2.Items.AddRange(New Object() {"00", "15", "30", "45"})
        Me.e_m2.Location = New System.Drawing.Point(270, 57)
        Me.e_m2.Name = "e_m2"
        Me.e_m2.Size = New System.Drawing.Size(55, 21)
        Me.e_m2.TabIndex = 1
        '
        'b_m2
        '
        Me.b_m2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.b_m2.FormattingEnabled = True
        Me.b_m2.Items.AddRange(New Object() {"00", "15", "30", "45"})
        Me.b_m2.Location = New System.Drawing.Point(127, 57)
        Me.b_m2.Name = "b_m2"
        Me.b_m2.Size = New System.Drawing.Size(55, 21)
        Me.b_m2.TabIndex = 1
        '
        'b_m1
        '
        Me.b_m1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.b_m1.FormattingEnabled = True
        Me.b_m1.Items.AddRange(New Object() {"00", "15", "30", "45"})
        Me.b_m1.Location = New System.Drawing.Point(127, 24)
        Me.b_m1.Name = "b_m1"
        Me.b_m1.Size = New System.Drawing.Size(55, 21)
        Me.b_m1.TabIndex = 1
        '
        'e_h2
        '
        Me.e_h2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.e_h2.FormattingEnabled = True
        Me.e_h2.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.e_h2.Location = New System.Drawing.Point(209, 57)
        Me.e_h2.Name = "e_h2"
        Me.e_h2.Size = New System.Drawing.Size(55, 21)
        Me.e_h2.TabIndex = 1
        '
        'e_m1
        '
        Me.e_m1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.e_m1.FormattingEnabled = True
        Me.e_m1.Items.AddRange(New Object() {"00", "15", "30", "45"})
        Me.e_m1.Location = New System.Drawing.Point(270, 24)
        Me.e_m1.Name = "e_m1"
        Me.e_m1.Size = New System.Drawing.Size(55, 21)
        Me.e_m1.TabIndex = 1
        '
        'b_h1
        '
        Me.b_h1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.b_h1.FormattingEnabled = True
        Me.b_h1.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.b_h1.Location = New System.Drawing.Point(66, 24)
        Me.b_h1.Name = "b_h1"
        Me.b_h1.Size = New System.Drawing.Size(55, 21)
        Me.b_h1.TabIndex = 1
        '
        'e_h1
        '
        Me.e_h1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.e_h1.FormattingEnabled = True
        Me.e_h1.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.e_h1.Location = New System.Drawing.Point(209, 24)
        Me.e_h1.Name = "e_h1"
        Me.e_h1.Size = New System.Drawing.Size(55, 21)
        Me.e_h1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "班別2 Ca 2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "班別1 Ca 1"
        '
        'txt_IP
        '
        Me.txt_IP.Location = New System.Drawing.Point(14, 19)
        Me.txt_IP.Name = "txt_IP"
        Me.txt_IP.Size = New System.Drawing.Size(193, 20)
        Me.txt_IP.TabIndex = 7
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txt_IP)
        Me.GroupBox4.Location = New System.Drawing.Point(23, 66)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(336, 59)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Scanner IP"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.time_light)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 152)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 59)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Time light "
        '
        'time_light
        '
        Me.time_light.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.time_light.FormattingEnabled = True
        Me.time_light.Items.AddRange(New Object() {"0.5", "1", "1.5", "2", "2.5", "3"})
        Me.time_light.Location = New System.Drawing.Point(14, 18)
        Me.time_light.Margin = New System.Windows.Forms.Padding(2)
        Me.time_light.Name = "time_light"
        Me.time_light.Size = New System.Drawing.Size(193, 21)
        Me.time_light.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.delay_time)
        Me.GroupBox5.Location = New System.Drawing.Point(23, 234)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(336, 59)
        Me.GroupBox5.TabIndex = 10
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Delay time "
        '
        'delay_time
        '
        Me.delay_time.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.delay_time.FormattingEnabled = True
        Me.delay_time.Items.AddRange(New Object() {"0", "0.5", "1"})
        Me.delay_time.Location = New System.Drawing.Point(14, 18)
        Me.delay_time.Margin = New System.Windows.Forms.Padding(2)
        Me.delay_time.Name = "delay_time"
        Me.delay_time.Size = New System.Drawing.Size(193, 21)
        Me.delay_time.TabIndex = 1
        '
        'qrcode_setting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(384, 497)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "qrcode_setting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cb_code As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents b_h2 As ComboBox
    Friend WithEvents e_m2 As ComboBox
    Friend WithEvents b_m2 As ComboBox
    Friend WithEvents b_m1 As ComboBox
    Friend WithEvents e_h2 As ComboBox
    Friend WithEvents e_m1 As ComboBox
    Friend WithEvents b_h1 As ComboBox
    Friend WithEvents e_h1 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_IP As TextBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents time_light As ComboBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents delay_time As ComboBox
End Class
