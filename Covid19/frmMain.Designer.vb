<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.cbChartItemITA = New System.Windows.Forms.ComboBox()
        Me.lstItaRegions = New System.Windows.Forms.ListBox()
        Me.lstItaProvinces = New System.Windows.Forms.ListBox()
        Me.lstRegions = New System.Windows.Forms.ListBox()
        Me.cbChartItemWorld = New System.Windows.Forms.ComboBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.labLastUpdateInfo = New System.Windows.Forms.Label()
        Me.chkDaily = New System.Windows.Forms.CheckBox()
        Me.labErrorValue = New System.Windows.Forms.Label()
        Me.labError = New System.Windows.Forms.Label()
        Me.udCurPosition = New System.Windows.Forms.NumericUpDown()
        Me.udSigma = New System.Windows.Forms.NumericUpDown()
        Me.udEstimatedMax = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labMaxEstDeaths = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btEstimate = New System.Windows.Forms.Button()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLine = New System.Windows.Forms.Panel()
        Me.pnlIta = New System.Windows.Forms.Panel()
        Me.labSelectionHintITA = New System.Windows.Forms.Label()
        Me.pnlUpdate = New System.Windows.Forms.Panel()
        Me.btCheck4Updates = New System.Windows.Forms.Button()
        Me.mnMain = New System.Windows.Forms.MenuStrip()
        Me.mnMainItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnWorld = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnITAFull = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnITARegions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnITAProvinces = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnUS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnUK = New System.Windows.Forms.ToolStripMenuItem()
        Me.btShowMap = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkNormalize = New System.Windows.Forms.CheckBox()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btDateShiftLeft = New System.Windows.Forms.Button()
        Me.btDateShiftRight = New System.Windows.Forms.Button()
        Me.pnlInvisible = New System.Windows.Forms.Panel()
        Me.pnlUS = New System.Windows.Forms.Panel()
        Me.lstRegionsUS = New System.Windows.Forms.ListBox()
        Me.cbChartItemUS = New System.Windows.Forms.ComboBox()
        Me.labSelectionHintUS = New System.Windows.Forms.Label()
        Me.pnlWorld = New System.Windows.Forms.Panel()
        Me.labSelectionHintWorld = New System.Windows.Forms.Label()
        Me.labSnapshot = New System.Windows.Forms.Label()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udCurPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udSigma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udEstimatedMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLeft.SuspendLayout()
        Me.pnlIta.SuspendLayout()
        Me.pnlUpdate.SuspendLayout()
        Me.mnMain.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlInvisible.SuspendLayout()
        Me.pnlUS.SuspendLayout()
        Me.pnlWorld.SuspendLayout()
        Me.SuspendLayout()
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(225, 54)
        Me.Chart1.Margin = New System.Windows.Forms.Padding(0)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(949, 658)
        Me.Chart1.TabIndex = 9
        Me.Chart1.Text = "Chart1"
        '
        'cbChartItemITA
        '
        Me.cbChartItemITA.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemITA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemITA.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemITA.FormattingEnabled = True
        Me.cbChartItemITA.Location = New System.Drawing.Point(2, 2)
        Me.cbChartItemITA.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.cbChartItemITA.Name = "cbChartItemITA"
        Me.cbChartItemITA.Size = New System.Drawing.Size(221, 28)
        Me.cbChartItemITA.TabIndex = 15
        '
        'lstItaRegions
        '
        Me.lstItaRegions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstItaRegions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstItaRegions.FormattingEnabled = True
        Me.lstItaRegions.ItemHeight = 17
        Me.lstItaRegions.Location = New System.Drawing.Point(2, 30)
        Me.lstItaRegions.Name = "lstItaRegions"
        Me.lstItaRegions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstItaRegions.Size = New System.Drawing.Size(221, 477)
        Me.lstItaRegions.TabIndex = 30
        Me.lstItaRegions.Visible = False
        '
        'lstItaProvinces
        '
        Me.lstItaProvinces.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstItaProvinces.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstItaProvinces.FormattingEnabled = True
        Me.lstItaProvinces.ItemHeight = 17
        Me.lstItaProvinces.Location = New System.Drawing.Point(2, 30)
        Me.lstItaProvinces.Name = "lstItaProvinces"
        Me.lstItaProvinces.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstItaProvinces.Size = New System.Drawing.Size(221, 477)
        Me.lstItaProvinces.TabIndex = 34
        Me.lstItaProvinces.Visible = False
        '
        'lstRegions
        '
        Me.lstRegions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRegions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRegions.FormattingEnabled = True
        Me.lstRegions.ItemHeight = 17
        Me.lstRegions.Location = New System.Drawing.Point(2, 30)
        Me.lstRegions.Name = "lstRegions"
        Me.lstRegions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRegions.Size = New System.Drawing.Size(223, 403)
        Me.lstRegions.TabIndex = 20
        '
        'cbChartItemWorld
        '
        Me.cbChartItemWorld.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemWorld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemWorld.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemWorld.FormattingEnabled = True
        Me.cbChartItemWorld.Location = New System.Drawing.Point(2, 2)
        Me.cbChartItemWorld.Name = "cbChartItemWorld"
        Me.cbChartItemWorld.Size = New System.Drawing.Size(223, 28)
        Me.cbChartItemWorld.TabIndex = 19
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "PC48.png")
        Me.ImageList1.Images.SetKeyName(1, "WHO42.png")
        '
        'labLastUpdateInfo
        '
        Me.labLastUpdateInfo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.labLastUpdateInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labLastUpdateInfo.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.labLastUpdateInfo.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labLastUpdateInfo.Location = New System.Drawing.Point(18, 66)
        Me.labLastUpdateInfo.Name = "labLastUpdateInfo"
        Me.labLastUpdateInfo.Size = New System.Drawing.Size(169, 47)
        Me.labLastUpdateInfo.TabIndex = 16
        Me.labLastUpdateInfo.Text = "Last updated:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " - - - - - -"
        Me.labLastUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkDaily
        '
        Me.chkDaily.BackColor = System.Drawing.Color.Transparent
        Me.chkDaily.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkDaily.Location = New System.Drawing.Point(61, 4)
        Me.chkDaily.Margin = New System.Windows.Forms.Padding(0)
        Me.chkDaily.Name = "chkDaily"
        Me.chkDaily.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.chkDaily.Size = New System.Drawing.Size(205, 23)
        Me.chkDaily.TabIndex = 12
        Me.chkDaily.Text = "Day-to-day changes"
        Me.chkDaily.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkDaily.UseVisualStyleBackColor = False
        '
        'labErrorValue
        '
        Me.labErrorValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labErrorValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.labErrorValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labErrorValue.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labErrorValue.Location = New System.Drawing.Point(266, 24)
        Me.labErrorValue.Name = "labErrorValue"
        Me.labErrorValue.Size = New System.Drawing.Size(59, 25)
        Me.labErrorValue.TabIndex = 35
        Me.labErrorValue.Text = "1000"
        Me.labErrorValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labError
        '
        Me.labError.Cursor = System.Windows.Forms.Cursors.Default
        Me.labError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labError.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labError.Location = New System.Drawing.Point(266, 4)
        Me.labError.Name = "labError"
        Me.labError.Size = New System.Drawing.Size(59, 19)
        Me.labError.TabIndex = 34
        Me.labError.Text = "Error"
        Me.labError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'udCurPosition
        '
        Me.udCurPosition.Location = New System.Drawing.Point(212, 24)
        Me.udCurPosition.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.udCurPosition.Name = "udCurPosition"
        Me.udCurPosition.Size = New System.Drawing.Size(48, 25)
        Me.udCurPosition.TabIndex = 28
        Me.udCurPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.udCurPosition.Value = New Decimal(New Integer() {48, 0, 0, 0})
        '
        'udSigma
        '
        Me.udSigma.DecimalPlaces = 2
        Me.udSigma.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.udSigma.Location = New System.Drawing.Point(158, 24)
        Me.udSigma.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.udSigma.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.udSigma.Name = "udSigma"
        Me.udSigma.Size = New System.Drawing.Size(48, 25)
        Me.udSigma.TabIndex = 26
        Me.udSigma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.udSigma.Value = New Decimal(New Integer() {15, 0, 0, 65536})
        '
        'udEstimatedMax
        '
        Me.udEstimatedMax.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.udEstimatedMax.Location = New System.Drawing.Point(76, 24)
        Me.udEstimatedMax.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.udEstimatedMax.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.udEstimatedMax.Name = "udEstimatedMax"
        Me.udEstimatedMax.Size = New System.Drawing.Size(76, 25)
        Me.udEstimatedMax.TabIndex = 24
        Me.udEstimatedMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.udEstimatedMax.Value = New Decimal(New Integer() {10000, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label2.Location = New System.Drawing.Point(212, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 20)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "%"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label1.Location = New System.Drawing.Point(158, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 20)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "σ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labMaxEstDeaths
        '
        Me.labMaxEstDeaths.Cursor = System.Windows.Forms.Cursors.Default
        Me.labMaxEstDeaths.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMaxEstDeaths.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labMaxEstDeaths.Location = New System.Drawing.Point(76, 3)
        Me.labMaxEstDeaths.Name = "labMaxEstDeaths"
        Me.labMaxEstDeaths.Size = New System.Drawing.Size(76, 20)
        Me.labMaxEstDeaths.TabIndex = 25
        Me.labMaxEstDeaths.Text = "Final value"
        Me.labMaxEstDeaths.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btEstimate
        '
        Me.btEstimate.Image = CType(resources.GetObject("btEstimate.Image"), System.Drawing.Image)
        Me.btEstimate.Location = New System.Drawing.Point(5, 5)
        Me.btEstimate.Name = "btEstimate"
        Me.btEstimate.Size = New System.Drawing.Size(66, 45)
        Me.btEstimate.TabIndex = 36
        Me.btEstimate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btEstimate, "Estimate progression")
        Me.btEstimate.UseVisualStyleBackColor = True
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.SystemColors.Window
        Me.pnlLeft.Controls.Add(Me.pnlLine)
        Me.pnlLeft.Controls.Add(Me.pnlIta)
        Me.pnlLeft.Controls.Add(Me.pnlUpdate)
        Me.pnlLeft.Controls.Add(Me.mnMain)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(225, 712)
        Me.pnlLeft.TabIndex = 33
        '
        'pnlLine
        '
        Me.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLine.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlLine.Location = New System.Drawing.Point(224, 48)
        Me.pnlLine.Margin = New System.Windows.Forms.Padding(6)
        Me.pnlLine.Name = "pnlLine"
        Me.pnlLine.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pnlLine.Size = New System.Drawing.Size(1, 549)
        Me.pnlLine.TabIndex = 42
        '
        'pnlIta
        '
        Me.pnlIta.Controls.Add(Me.lstItaRegions)
        Me.pnlIta.Controls.Add(Me.lstItaProvinces)
        Me.pnlIta.Controls.Add(Me.cbChartItemITA)
        Me.pnlIta.Controls.Add(Me.labSelectionHintITA)
        Me.pnlIta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlIta.Location = New System.Drawing.Point(0, 48)
        Me.pnlIta.Name = "pnlIta"
        Me.pnlIta.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlIta.Size = New System.Drawing.Size(225, 549)
        Me.pnlIta.TabIndex = 12
        '
        'labSelectionHintITA
        '
        Me.labSelectionHintITA.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintITA.ForeColor = System.Drawing.SystemColors.Highlight
        Me.labSelectionHintITA.Location = New System.Drawing.Point(2, 507)
        Me.labSelectionHintITA.Name = "labSelectionHintITA"
        Me.labSelectionHintITA.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintITA.Size = New System.Drawing.Size(221, 40)
        Me.labSelectionHintITA.TabIndex = 35
        Me.labSelectionHintITA.Text = "Hold <Ctrl> or <Shift> for multiple selection"
        Me.labSelectionHintITA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlUpdate
        '
        Me.pnlUpdate.Controls.Add(Me.btCheck4Updates)
        Me.pnlUpdate.Controls.Add(Me.labLastUpdateInfo)
        Me.pnlUpdate.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlUpdate.Location = New System.Drawing.Point(0, 597)
        Me.pnlUpdate.Name = "pnlUpdate"
        Me.pnlUpdate.Size = New System.Drawing.Size(225, 115)
        Me.pnlUpdate.TabIndex = 40
        '
        'btCheck4Updates
        '
        Me.btCheck4Updates.Image = CType(resources.GetObject("btCheck4Updates.Image"), System.Drawing.Image)
        Me.btCheck4Updates.Location = New System.Drawing.Point(19, 6)
        Me.btCheck4Updates.Name = "btCheck4Updates"
        Me.btCheck4Updates.Size = New System.Drawing.Size(168, 61)
        Me.btCheck4Updates.TabIndex = 39
        Me.btCheck4Updates.Text = "Check for data updates"
        Me.btCheck4Updates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btCheck4Updates.UseVisualStyleBackColor = True
        '
        'mnMain
        '
        Me.mnMain.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnMain.ImageScalingSize = New System.Drawing.Size(34, 34)
        Me.mnMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnMainItem})
        Me.mnMain.Location = New System.Drawing.Point(0, 0)
        Me.mnMain.Margin = New System.Windows.Forms.Padding(3)
        Me.mnMain.Name = "mnMain"
        Me.mnMain.Padding = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.mnMain.Size = New System.Drawing.Size(225, 48)
        Me.mnMain.TabIndex = 41
        Me.mnMain.Text = "mnMain"
        '
        'mnMainItem
        '
        Me.mnMainItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnWorld, Me.ToolStripMenuItem1, Me.mnITAFull, Me.mnITARegions, Me.mnITAProvinces, Me.ToolStripMenuItem2, Me.mnUS, Me.mnUK})
        Me.mnMainItem.Image = CType(resources.GetObject("mnMainItem.Image"), System.Drawing.Image)
        Me.mnMainItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.mnMainItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnMainItem.Name = "mnMainItem"
        Me.mnMainItem.Size = New System.Drawing.Size(129, 36)
        Me.mnMainItem.Text = "World"
        '
        'mnWorld
        '
        Me.mnWorld.Image = CType(resources.GetObject("mnWorld.Image"), System.Drawing.Image)
        Me.mnWorld.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnWorld.Name = "mnWorld"
        Me.mnWorld.Size = New System.Drawing.Size(256, 38)
        Me.mnWorld.Text = "World"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(253, 6)
        '
        'mnITAFull
        '
        Me.mnITAFull.Image = CType(resources.GetObject("mnITAFull.Image"), System.Drawing.Image)
        Me.mnITAFull.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnITAFull.Name = "mnITAFull"
        Me.mnITAFull.Size = New System.Drawing.Size(256, 38)
        Me.mnITAFull.Text = "Italy"
        '
        'mnITARegions
        '
        Me.mnITARegions.Image = CType(resources.GetObject("mnITARegions.Image"), System.Drawing.Image)
        Me.mnITARegions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnITARegions.Name = "mnITARegions"
        Me.mnITARegions.Size = New System.Drawing.Size(256, 38)
        Me.mnITARegions.Text = "Italy - Regions"
        '
        'mnITAProvinces
        '
        Me.mnITAProvinces.Image = CType(resources.GetObject("mnITAProvinces.Image"), System.Drawing.Image)
        Me.mnITAProvinces.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnITAProvinces.Name = "mnITAProvinces"
        Me.mnITAProvinces.Size = New System.Drawing.Size(256, 38)
        Me.mnITAProvinces.Text = "Italy - Provinces"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(253, 6)
        '
        'mnUS
        '
        Me.mnUS.Image = CType(resources.GetObject("mnUS.Image"), System.Drawing.Image)
        Me.mnUS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnUS.Name = "mnUS"
        Me.mnUS.Size = New System.Drawing.Size(256, 38)
        Me.mnUS.Text = "US"
        '
        'mnUK
        '
        Me.mnUK.Enabled = False
        Me.mnUK.Image = CType(resources.GetObject("mnUK.Image"), System.Drawing.Image)
        Me.mnUK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnUK.Name = "mnUK"
        Me.mnUK.Size = New System.Drawing.Size(256, 38)
        Me.mnUK.Text = "UK"
        Me.mnUK.Visible = False
        '
        'btShowMap
        '
        Me.btShowMap.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btShowMap.Image = CType(resources.GetObject("btShowMap.Image"), System.Drawing.Image)
        Me.btShowMap.Location = New System.Drawing.Point(1071, 631)
        Me.btShowMap.Name = "btShowMap"
        Me.btShowMap.Size = New System.Drawing.Size(98, 77)
        Me.btShowMap.TabIndex = 38
        Me.btShowMap.Text = "Show on map"
        Me.btShowMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btShowMap.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btEstimate)
        Me.Panel1.Controls.Add(Me.udEstimatedMax)
        Me.Panel1.Controls.Add(Me.labErrorValue)
        Me.Panel1.Controls.Add(Me.udCurPosition)
        Me.Panel1.Controls.Add(Me.udSigma)
        Me.Panel1.Controls.Add(Me.labError)
        Me.Panel1.Controls.Add(Me.labMaxEstDeaths)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(616, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(333, 54)
        Me.Panel1.TabIndex = 37
        Me.Panel1.Visible = False
        '
        'chkNormalize
        '
        Me.chkNormalize.BackColor = System.Drawing.Color.Transparent
        Me.chkNormalize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkNormalize.Location = New System.Drawing.Point(61, 27)
        Me.chkNormalize.Margin = New System.Windows.Forms.Padding(0)
        Me.chkNormalize.Name = "chkNormalize"
        Me.chkNormalize.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.chkNormalize.Size = New System.Drawing.Size(203, 26)
        Me.chkNormalize.TabIndex = 40
        Me.chkNormalize.Text = "Values per 10 000 people"
        Me.chkNormalize.UseVisualStyleBackColor = False
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.chkNormalize)
        Me.pnlTop.Controls.Add(Me.chkDaily)
        Me.pnlTop.Controls.Add(Me.Panel1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(225, 0)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(949, 54)
        Me.pnlTop.TabIndex = 36
        '
        'btDateShiftLeft
        '
        Me.btDateShiftLeft.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btDateShiftLeft.Image = CType(resources.GetObject("btDateShiftLeft.Image"), System.Drawing.Image)
        Me.btDateShiftLeft.Location = New System.Drawing.Point(580, 683)
        Me.btDateShiftLeft.Name = "btDateShiftLeft"
        Me.btDateShiftLeft.Size = New System.Drawing.Size(103, 25)
        Me.btDateShiftLeft.TabIndex = 41
        Me.btDateShiftLeft.Text = "Shift dates"
        Me.btDateShiftLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btDateShiftLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btDateShiftLeft.UseVisualStyleBackColor = True
        '
        'btDateShiftRight
        '
        Me.btDateShiftRight.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btDateShiftRight.Image = CType(resources.GetObject("btDateShiftRight.Image"), System.Drawing.Image)
        Me.btDateShiftRight.Location = New System.Drawing.Point(689, 683)
        Me.btDateShiftRight.Name = "btDateShiftRight"
        Me.btDateShiftRight.Size = New System.Drawing.Size(103, 25)
        Me.btDateShiftRight.TabIndex = 42
        Me.btDateShiftRight.Text = "Shift dates"
        Me.btDateShiftRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btDateShiftRight.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btDateShiftRight.UseVisualStyleBackColor = True
        '
        'pnlInvisible
        '
        Me.pnlInvisible.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlInvisible.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInvisible.Controls.Add(Me.pnlUS)
        Me.pnlInvisible.Controls.Add(Me.pnlWorld)
        Me.pnlInvisible.Location = New System.Drawing.Point(319, 110)
        Me.pnlInvisible.Name = "pnlInvisible"
        Me.pnlInvisible.Size = New System.Drawing.Size(685, 534)
        Me.pnlInvisible.TabIndex = 43
        Me.pnlInvisible.Visible = False
        '
        'pnlUS
        '
        Me.pnlUS.Controls.Add(Me.lstRegionsUS)
        Me.pnlUS.Controls.Add(Me.cbChartItemUS)
        Me.pnlUS.Controls.Add(Me.labSelectionHintUS)
        Me.pnlUS.Location = New System.Drawing.Point(482, 14)
        Me.pnlUS.Name = "pnlUS"
        Me.pnlUS.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlUS.Size = New System.Drawing.Size(227, 475)
        Me.pnlUS.TabIndex = 14
        '
        'lstRegionsUS
        '
        Me.lstRegionsUS.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRegionsUS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRegionsUS.FormattingEnabled = True
        Me.lstRegionsUS.ItemHeight = 17
        Me.lstRegionsUS.Location = New System.Drawing.Point(2, 30)
        Me.lstRegionsUS.Name = "lstRegionsUS"
        Me.lstRegionsUS.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRegionsUS.Size = New System.Drawing.Size(223, 403)
        Me.lstRegionsUS.TabIndex = 20
        '
        'cbChartItemUS
        '
        Me.cbChartItemUS.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemUS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemUS.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemUS.FormattingEnabled = True
        Me.cbChartItemUS.Location = New System.Drawing.Point(2, 2)
        Me.cbChartItemUS.Name = "cbChartItemUS"
        Me.cbChartItemUS.Size = New System.Drawing.Size(223, 28)
        Me.cbChartItemUS.TabIndex = 19
        '
        'labSelectionHintUS
        '
        Me.labSelectionHintUS.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintUS.ForeColor = System.Drawing.SystemColors.Highlight
        Me.labSelectionHintUS.Location = New System.Drawing.Point(2, 433)
        Me.labSelectionHintUS.Name = "labSelectionHintUS"
        Me.labSelectionHintUS.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintUS.Size = New System.Drawing.Size(223, 40)
        Me.labSelectionHintUS.TabIndex = 36
        Me.labSelectionHintUS.Text = "Hold <Ctrl> or <Shift> for multiple selection"
        Me.labSelectionHintUS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlWorld
        '
        Me.pnlWorld.Controls.Add(Me.lstRegions)
        Me.pnlWorld.Controls.Add(Me.cbChartItemWorld)
        Me.pnlWorld.Controls.Add(Me.labSelectionHintWorld)
        Me.pnlWorld.Location = New System.Drawing.Point(14, 13)
        Me.pnlWorld.Name = "pnlWorld"
        Me.pnlWorld.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlWorld.Size = New System.Drawing.Size(227, 475)
        Me.pnlWorld.TabIndex = 13
        '
        'labSelectionHintWorld
        '
        Me.labSelectionHintWorld.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintWorld.ForeColor = System.Drawing.SystemColors.Highlight
        Me.labSelectionHintWorld.Location = New System.Drawing.Point(2, 433)
        Me.labSelectionHintWorld.Name = "labSelectionHintWorld"
        Me.labSelectionHintWorld.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintWorld.Size = New System.Drawing.Size(223, 40)
        Me.labSelectionHintWorld.TabIndex = 36
        Me.labSelectionHintWorld.Text = "Hold <Ctrl> or <Shift> for multiple selection"
        Me.labSelectionHintWorld.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labSnapshot
        '
        Me.labSnapshot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labSnapshot.BackColor = System.Drawing.SystemColors.ControlLight
        Me.labSnapshot.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSnapshot.Location = New System.Drawing.Point(0, 0)
        Me.labSnapshot.Name = "labSnapshot"
        Me.labSnapshot.Size = New System.Drawing.Size(1174, 712)
        Me.labSnapshot.TabIndex = 44
        Me.labSnapshot.Text = "Application starting, please wait ..."
        Me.labSnapshot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1174, 712)
        Me.Controls.Add(Me.pnlInvisible)
        Me.Controls.Add(Me.btDateShiftRight)
        Me.Controls.Add(Me.btDateShiftLeft)
        Me.Controls.Add(Me.btShowMap)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.labSnapshot)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(1020, 600)
        Me.Name = "frmMain"
        Me.Text = "COVID-19"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udCurPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udSigma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udEstimatedMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeft.PerformLayout()
        Me.pnlIta.ResumeLayout(False)
        Me.pnlUpdate.ResumeLayout(False)
        Me.mnMain.ResumeLayout(False)
        Me.mnMain.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlInvisible.ResumeLayout(False)
        Me.pnlUS.ResumeLayout(False)
        Me.pnlWorld.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents chkDaily As CheckBox
    Friend WithEvents cbChartItemITA As ComboBox
    Friend WithEvents labLastUpdateInfo As Label
    Friend WithEvents cbChartItemWorld As ComboBox
    Friend WithEvents lstRegions As ListBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents labMaxEstDeaths As Label
    Friend WithEvents udEstimatedMax As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents udCurPosition As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents udSigma As NumericUpDown
    Friend WithEvents lstItaRegions As ListBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents labErrorValue As Label
    Friend WithEvents labError As Label
    Friend WithEvents pnlLeft As Panel
    Friend WithEvents btEstimate As Button
    Friend WithEvents btCheck4Updates As Button
    Friend WithEvents btShowMap As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chkNormalize As CheckBox
    Friend WithEvents pnlTop As Panel
    Friend WithEvents pnlUpdate As Panel
    Friend WithEvents btDateShiftLeft As Button
    Friend WithEvents btDateShiftRight As Button
    Friend WithEvents lstItaProvinces As ListBox
    Friend WithEvents mnMain As MenuStrip
    Friend WithEvents mnMainItem As ToolStripMenuItem
    Friend WithEvents mnITAFull As ToolStripMenuItem
    Friend WithEvents mnITARegions As ToolStripMenuItem
    Friend WithEvents mnITAProvinces As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents mnWorld As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents mnUS As ToolStripMenuItem
    Friend WithEvents mnUK As ToolStripMenuItem
    Friend WithEvents pnlInvisible As Panel
    Friend WithEvents pnlIta As Panel
    Friend WithEvents pnlWorld As Panel
    Friend WithEvents labSelectionHintITA As Label
    Friend WithEvents labSelectionHintWorld As Label
    Friend WithEvents pnlLine As Panel
    Friend WithEvents pnlUS As Panel
    Friend WithEvents lstRegionsUS As ListBox
    Friend WithEvents cbChartItemUS As ComboBox
    Friend WithEvents labSelectionHintUS As Label
    Friend WithEvents labSnapshot As Label
End Class
