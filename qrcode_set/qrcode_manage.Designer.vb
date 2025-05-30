<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class qrcode_manage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(qrcode_manage))
        Me.qrcode_history = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cb_type = New System.Windows.Forms.ComboBox()
        Me.btn_excel = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtp_edate = New System.Windows.Forms.DateTimePicker()
        Me.dtp_bdate = New System.Windows.Forms.DateTimePicker()
        Me.count = New System.Windows.Forms.TextBox()
        Me.code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QRcode010 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.qrcode_history, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'qrcode_history
        '
        Me.qrcode_history.AllowUserToAddRows = False
        Me.qrcode_history.AllowUserToDeleteRows = False
        Me.qrcode_history.BackgroundColor = System.Drawing.Color.FloralWhite
        Me.qrcode_history.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.qrcode_history.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.code, Me.time, Me.QRcode010})
        Me.qrcode_history.Dock = System.Windows.Forms.DockStyle.Fill
        Me.qrcode_history.Location = New System.Drawing.Point(3, 104)
        Me.qrcode_history.Name = "qrcode_history"
        Me.qrcode_history.ReadOnly = True
        Me.qrcode_history.Size = New System.Drawing.Size(900, 296)
        Me.qrcode_history.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.qrcode_history, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.count, 0, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.76238!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.23763!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 302.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(906, 431)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Green
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(900, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "抬頭 Tra tìm"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cb_type)
        Me.Panel1.Controls.Add(Me.btn_excel)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtp_edate)
        Me.Panel1.Controls.Add(Me.dtp_bdate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(900, 71)
        Me.Panel1.TabIndex = 2
        '
        'cb_type
        '
        Me.cb_type.FormattingEnabled = True
        Me.cb_type.Location = New System.Drawing.Point(453, 18)
        Me.cb_type.Name = "cb_type"
        Me.cb_type.Size = New System.Drawing.Size(121, 21)
        Me.cb_type.TabIndex = 5
        '
        'btn_excel
        '
        Me.btn_excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_excel.BackColor = System.Drawing.Color.CadetBlue
        Me.btn_excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_excel.ForeColor = System.Drawing.Color.White
        Me.btn_excel.Image = CType(resources.GetObject("btn_excel.Image"), System.Drawing.Image)
        Me.btn_excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_excel.Location = New System.Drawing.Point(762, 3)
        Me.btn_excel.Name = "btn_excel"
        Me.btn_excel.Size = New System.Drawing.Size(122, 57)
        Me.btn_excel.TabIndex = 4
        Me.btn_excel.Text = "導出" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Xuất excel"
        Me.btn_excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_excel.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.DarkCyan
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(634, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(122, 57)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "履行 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tìm kiếm"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(236, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "結束日期 Đến ngày"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(60, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "開始日 Ngày bắt đầu"
        '
        'dtp_edate
        '
        Me.dtp_edate.CustomFormat = "yyyy/MM/dd HH:mm:ss"
        Me.dtp_edate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_edate.Location = New System.Drawing.Point(239, 19)
        Me.dtp_edate.Name = "dtp_edate"
        Me.dtp_edate.Size = New System.Drawing.Size(168, 20)
        Me.dtp_edate.TabIndex = 1
        '
        'dtp_bdate
        '
        Me.dtp_bdate.CustomFormat = "yyyy/MM/dd HH:mm:ss"
        Me.dtp_bdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_bdate.Location = New System.Drawing.Point(63, 19)
        Me.dtp_bdate.Name = "dtp_bdate"
        Me.dtp_bdate.Size = New System.Drawing.Size(161, 20)
        Me.dtp_bdate.TabIndex = 1
        '
        'count
        '
        Me.count.Enabled = False
        Me.count.Location = New System.Drawing.Point(3, 406)
        Me.count.Name = "count"
        Me.count.Size = New System.Drawing.Size(100, 20)
        Me.count.TabIndex = 3
        Me.count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'code
        '
        Me.code.DataPropertyName = "code"
        Me.code.HeaderText = "內容 Nội dung"
        Me.code.Name = "code"
        Me.code.ReadOnly = True
        Me.code.Width = 600
        '
        'time
        '
        Me.time.DataPropertyName = "time"
        Me.time.HeaderText = "時間 Thời gian"
        Me.time.Name = "time"
        Me.time.ReadOnly = True
        Me.time.Width = 150
        '
        'QRcode010
        '
        Me.QRcode010.HeaderText = "QRcode010"
        Me.QRcode010.Name = "QRcode010"
        Me.QRcode010.ReadOnly = True
        '
        'qrcode_manage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 435)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "qrcode_manage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "History"
        CType(Me.qrcode_history, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents qrcode_history As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents count As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cb_type As ComboBox
    Friend WithEvents btn_excel As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dtp_edate As DateTimePicker
    Friend WithEvents dtp_bdate As DateTimePicker
    Friend WithEvents code As DataGridViewTextBoxColumn
    Friend WithEvents time As DataGridViewTextBoxColumn
    Friend WithEvents QRcode010 As DataGridViewTextBoxColumn
End Class
