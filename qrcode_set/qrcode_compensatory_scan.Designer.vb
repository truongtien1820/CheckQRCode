<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class qrcode_compensatory_scan
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.status_lb = New System.Windows.Forms.Label()
        Me.dgv_productcompensatory = New System.Windows.Forms.DataGridView()
        Me.cl01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QRcode010 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ccl3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.dgv_productcompensatory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.72727!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.27273!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 450)
        Me.TableLayoutPanel1.TabIndex = 0
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
        Me.Label1.Size = New System.Drawing.Size(794, 102)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "COMPENSATORY PRODUCT"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.status_lb, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.dgv_productcompensatory, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 105)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(794, 342)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'status_lb
        '
        Me.status_lb.AutoSize = True
        Me.status_lb.BackColor = System.Drawing.Color.PeachPuff
        Me.TableLayoutPanel2.SetColumnSpan(Me.status_lb, 2)
        Me.status_lb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.status_lb.Font = New System.Drawing.Font("Times New Roman", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.status_lb.ForeColor = System.Drawing.Color.DodgerBlue
        Me.status_lb.Location = New System.Drawing.Point(3, 247)
        Me.status_lb.Name = "status_lb"
        Me.status_lb.Size = New System.Drawing.Size(788, 95)
        Me.status_lb.TabIndex = 5
        Me.status_lb.Text = "Status"
        Me.status_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgv_productcompensatory
        '
        Me.dgv_productcompensatory.AllowUserToAddRows = False
        Me.dgv_productcompensatory.AllowUserToDeleteRows = False
        Me.dgv_productcompensatory.BackgroundColor = System.Drawing.Color.Cornsilk
        Me.dgv_productcompensatory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_productcompensatory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cl01, Me.QRcode010, Me.ccl3})
        Me.dgv_productcompensatory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_productcompensatory.Location = New System.Drawing.Point(3, 3)
        Me.dgv_productcompensatory.Name = "dgv_productcompensatory"
        Me.dgv_productcompensatory.RowHeadersVisible = False
        Me.dgv_productcompensatory.Size = New System.Drawing.Size(788, 241)
        Me.dgv_productcompensatory.TabIndex = 4
        '
        'cl01
        '
        Me.cl01.HeaderText = "內容 Nội dung"
        Me.cl01.Name = "cl01"
        '
        'QRcode010
        '
        Me.QRcode010.HeaderText = "QRcode010"
        Me.QRcode010.Name = "QRcode010"
        '
        'ccl3
        '
        Me.ccl3.HeaderText = "返回時間 Thời gian quét bù"
        Me.ccl3.Name = "ccl3"
        '
        'qrcode_compensatory_scan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "qrcode_compensatory_scan"
        Me.Text = "qrcode_compensatory_scan"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.dgv_productcompensatory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents dgv_productcompensatory As DataGridView
    Friend WithEvents status_lb As Label
    Friend WithEvents cl01 As DataGridViewTextBoxColumn
    Friend WithEvents QRcode010 As DataGridViewTextBoxColumn
    Friend WithEvents ccl3 As DataGridViewTextBoxColumn
End Class
