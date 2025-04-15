<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class qrcode_productreturn
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgv_productreturn = New System.Windows.Forms.DataGridView()
        Me.cl01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cl02 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ccl3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status_lb = New System.Windows.Forms.Label()
        Me.btn_commit = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgv_productreturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 2)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(634, 53)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "PRODUCT RETURN"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dgv_productreturn, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.status_lb, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_commit, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.77778!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.11111!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 450)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'dgv_productreturn
        '
        Me.dgv_productreturn.AllowUserToAddRows = False
        Me.dgv_productreturn.AllowUserToDeleteRows = False
        Me.dgv_productreturn.BackgroundColor = System.Drawing.Color.LightSalmon
        Me.dgv_productreturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_productreturn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cl01, Me.cl02, Me.ccl3})
        Me.dgv_productreturn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_productreturn.Location = New System.Drawing.Point(3, 56)
        Me.dgv_productreturn.Name = "dgv_productreturn"
        Me.dgv_productreturn.RowHeadersVisible = False
        Me.dgv_productreturn.Size = New System.Drawing.Size(634, 313)
        Me.dgv_productreturn.TabIndex = 2
        '
        'cl01
        '
        Me.cl01.HeaderText = "內容 Nội dung"
        Me.cl01.Name = "cl01"
        '
        'cl02
        '
        Me.cl02.HeaderText = "時間 Thời gian"
        Me.cl02.Name = "cl02"
        '
        'ccl3
        '
        Me.ccl3.HeaderText = "返回時間 Thời gian hoàn trả"
        Me.ccl3.Name = "ccl3"
        '
        'status_lb
        '
        Me.status_lb.AutoSize = True
        Me.status_lb.BackColor = System.Drawing.Color.Bisque
        Me.TableLayoutPanel1.SetColumnSpan(Me.status_lb, 2)
        Me.status_lb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.status_lb.Font = New System.Drawing.Font("Times New Roman", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.status_lb.ForeColor = System.Drawing.Color.Navy
        Me.status_lb.Location = New System.Drawing.Point(3, 372)
        Me.status_lb.Name = "status_lb"
        Me.status_lb.Size = New System.Drawing.Size(794, 78)
        Me.status_lb.TabIndex = 3
        Me.status_lb.Text = "Status"
        Me.status_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_commit
        '
        Me.btn_commit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn_commit.BackColor = System.Drawing.SystemColors.Info
        Me.btn_commit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.btn_commit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btn_commit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btn_commit.Location = New System.Drawing.Point(668, 106)
        Me.btn_commit.Name = "btn_commit"
        Me.btn_commit.Size = New System.Drawing.Size(129, 73)
        Me.btn_commit.TabIndex = 4
        Me.btn_commit.Text = "確認Xác nhận"
        Me.btn_commit.UseVisualStyleBackColor = False
        Me.btn_commit.Visible = False
        '
        'qrcode_productreturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "qrcode_productreturn"
        Me.Text = "Product Return"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.dgv_productreturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents dgv_productreturn As DataGridView
    Friend WithEvents status_lb As Label
    Friend WithEvents cl01 As DataGridViewTextBoxColumn
    Friend WithEvents cl02 As DataGridViewTextBoxColumn
    Friend WithEvents ccl3 As DataGridViewTextBoxColumn
    Friend WithEvents btn_commit As Button
End Class
