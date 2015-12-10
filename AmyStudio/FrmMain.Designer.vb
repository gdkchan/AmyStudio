<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: O procedimento a seguir é exigido pelo Windows Form Designer
    'Ele pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.TilesScroll = New System.Windows.Forms.VScrollBar()
        Me.TilesPanel = New System.Windows.Forms.Panel()
        Me.PicTiles = New System.Windows.Forms.PictureBox()
        Me.BtnOpen = New System.Windows.Forms.Label()
        Me.MapScrollY = New System.Windows.Forms.VScrollBar()
        Me.MapScrollX = New System.Windows.Forms.HScrollBar()
        Me.PicMap = New System.Windows.Forms.PictureBox()
        Me.CmbZone = New System.Windows.Forms.ComboBox()
        Me.CmbAct = New System.Windows.Forms.ComboBox()
        Me.BtnClose = New System.Windows.Forms.Label()
        Me.BtnMinimize = New System.Windows.Forms.Label()
        Me.TitleGrab = New System.Windows.Forms.Label()
        Me.TilesPanel.SuspendLayout()
        CType(Me.PicTiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TilesScroll
        '
        Me.TilesScroll.Enabled = False
        Me.TilesScroll.LargeChange = 128
        Me.TilesScroll.Location = New System.Drawing.Point(775, 60)
        Me.TilesScroll.Maximum = 128
        Me.TilesScroll.Name = "TilesScroll"
        Me.TilesScroll.Size = New System.Drawing.Size(16, 512)
        Me.TilesScroll.SmallChange = 16
        Me.TilesScroll.TabIndex = 1
        '
        'TilesPanel
        '
        Me.TilesPanel.BackColor = System.Drawing.Color.Transparent
        Me.TilesPanel.Controls.Add(Me.PicTiles)
        Me.TilesPanel.Location = New System.Drawing.Point(647, 60)
        Me.TilesPanel.Name = "TilesPanel"
        Me.TilesPanel.Size = New System.Drawing.Size(128, 512)
        Me.TilesPanel.TabIndex = 2
        '
        'PicTiles
        '
        Me.PicTiles.BackColor = System.Drawing.Color.Transparent
        Me.PicTiles.Location = New System.Drawing.Point(0, 0)
        Me.PicTiles.Name = "PicTiles"
        Me.PicTiles.Size = New System.Drawing.Size(128, 512)
        Me.PicTiles.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PicTiles.TabIndex = 2
        Me.PicTiles.TabStop = False
        '
        'BtnOpen
        '
        Me.BtnOpen.BackColor = System.Drawing.Color.Transparent
        Me.BtnOpen.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOpen.Location = New System.Drawing.Point(12, 0)
        Me.BtnOpen.Name = "BtnOpen"
        Me.BtnOpen.Size = New System.Drawing.Size(64, 24)
        Me.BtnOpen.TabIndex = 3
        Me.BtnOpen.Text = "Abrir"
        Me.BtnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MapScrollY
        '
        Me.MapScrollY.Enabled = False
        Me.MapScrollY.LargeChange = 16
        Me.MapScrollY.Location = New System.Drawing.Point(631, 60)
        Me.MapScrollY.Maximum = 16
        Me.MapScrollY.Name = "MapScrollY"
        Me.MapScrollY.Size = New System.Drawing.Size(16, 512)
        Me.MapScrollY.TabIndex = 5
        '
        'MapScrollX
        '
        Me.MapScrollX.Enabled = False
        Me.MapScrollX.LargeChange = 16
        Me.MapScrollX.Location = New System.Drawing.Point(12, 572)
        Me.MapScrollX.Maximum = 16
        Me.MapScrollX.Name = "MapScrollX"
        Me.MapScrollX.Size = New System.Drawing.Size(619, 16)
        Me.MapScrollX.TabIndex = 6
        '
        'PicMap
        '
        Me.PicMap.BackColor = System.Drawing.Color.Transparent
        Me.PicMap.Location = New System.Drawing.Point(12, 60)
        Me.PicMap.Name = "PicMap"
        Me.PicMap.Size = New System.Drawing.Size(619, 512)
        Me.PicMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PicMap.TabIndex = 7
        Me.PicMap.TabStop = False
        '
        'CmbZone
        '
        Me.CmbZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmbZone.FormattingEnabled = True
        Me.CmbZone.Items.AddRange(New Object() {"Emerald Hill Zone", "Chemical Plant Zone", "Mystic Cave Zone"})
        Me.CmbZone.Location = New System.Drawing.Point(660, 33)
        Me.CmbZone.Name = "CmbZone"
        Me.CmbZone.Size = New System.Drawing.Size(128, 21)
        Me.CmbZone.TabIndex = 8
        '
        'CmbAct
        '
        Me.CmbAct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmbAct.FormattingEnabled = True
        Me.CmbAct.Items.AddRange(New Object() {"Act 1", "Act 2"})
        Me.CmbAct.Location = New System.Drawing.Point(590, 33)
        Me.CmbAct.Name = "CmbAct"
        Me.CmbAct.Size = New System.Drawing.Size(64, 21)
        Me.CmbAct.TabIndex = 9
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.Transparent
        Me.BtnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(756, 0)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(32, 24)
        Me.BtnClose.TabIndex = 10
        Me.BtnClose.Text = "X"
        Me.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnMinimize
        '
        Me.BtnMinimize.BackColor = System.Drawing.Color.Transparent
        Me.BtnMinimize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMinimize.Location = New System.Drawing.Point(724, 0)
        Me.BtnMinimize.Name = "BtnMinimize"
        Me.BtnMinimize.Size = New System.Drawing.Size(32, 24)
        Me.BtnMinimize.TabIndex = 11
        Me.BtnMinimize.Text = "_"
        Me.BtnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TitleGrab
        '
        Me.TitleGrab.BackColor = System.Drawing.Color.Transparent
        Me.TitleGrab.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleGrab.Location = New System.Drawing.Point(272, 0)
        Me.TitleGrab.Name = "TitleGrab"
        Me.TitleGrab.Size = New System.Drawing.Size(256, 24)
        Me.TitleGrab.TabIndex = 12
        Me.TitleGrab.Text = " Amy Studio"
        Me.TitleGrab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.TitleGrab)
        Me.Controls.Add(Me.BtnMinimize)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.CmbAct)
        Me.Controls.Add(Me.CmbZone)
        Me.Controls.Add(Me.PicMap)
        Me.Controls.Add(Me.MapScrollX)
        Me.Controls.Add(Me.MapScrollY)
        Me.Controls.Add(Me.BtnOpen)
        Me.Controls.Add(Me.TilesPanel)
        Me.Controls.Add(Me.TilesScroll)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AmyStudio"
        Me.TilesPanel.ResumeLayout(False)
        Me.TilesPanel.PerformLayout()
        CType(Me.PicTiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TilesScroll As System.Windows.Forms.VScrollBar
    Friend WithEvents TilesPanel As System.Windows.Forms.Panel
    Friend WithEvents PicTiles As System.Windows.Forms.PictureBox
    Friend WithEvents BtnOpen As System.Windows.Forms.Label
    Friend WithEvents MapScrollY As System.Windows.Forms.VScrollBar
    Friend WithEvents MapScrollX As System.Windows.Forms.HScrollBar
    Friend WithEvents PicMap As System.Windows.Forms.PictureBox
    Friend WithEvents CmbZone As System.Windows.Forms.ComboBox
    Friend WithEvents CmbAct As System.Windows.Forms.ComboBox
    Friend WithEvents BtnClose As System.Windows.Forms.Label
    Friend WithEvents BtnMinimize As System.Windows.Forms.Label
    Friend WithEvents TitleGrab As System.Windows.Forms.Label

End Class
