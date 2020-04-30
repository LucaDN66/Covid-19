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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.mnMain = New System.Windows.Forms.MenuStrip()
        Me.mnMainItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnWorld = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnEurope = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnUS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnUK = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnITAFull = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnITARegions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnITAProvinces = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CheckForUpdatedDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.labLastUpdateInfo = New System.Windows.Forms.Label()
        Me.pnlLineSpacer = New System.Windows.Forms.Panel()
        Me.btShowMap = New System.Windows.Forms.Button()
        Me.PanelEstimate = New System.Windows.Forms.Panel()
        Me.chkNormalize = New System.Windows.Forms.CheckBox()
        Me.btDateShiftLeft = New System.Windows.Forms.Button()
        Me.btDateShiftRight = New System.Windows.Forms.Button()
        Me.pnlInvisible = New System.Windows.Forms.Panel()
        Me.pnlUK = New System.Windows.Forms.Panel()
        Me.lstRegionsUK = New System.Windows.Forms.ListBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.cbChartItemUK = New System.Windows.Forms.ComboBox()
        Me.labSelectionHintUK = New System.Windows.Forms.Label()
        Me.pnlEurope = New System.Windows.Forms.Panel()
        Me.lstRegionsEurope = New System.Windows.Forms.ListBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cbChartItemEurope = New System.Windows.Forms.ComboBox()
        Me.labSelectionHintEurope = New System.Windows.Forms.Label()
        Me.pnlUS = New System.Windows.Forms.Panel()
        Me.lstRegionsUS = New System.Windows.Forms.ListBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cbChartItemUS = New System.Windows.Forms.ComboBox()
        Me.labSelectionHintUS = New System.Windows.Forms.Label()
        Me.pnlWorld = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.labSelectionHintWorld = New System.Windows.Forms.Label()
        Me.labSnapshot = New System.Windows.Forms.Label()
        Me.chkMA = New System.Windows.Forms.CheckBox()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udCurPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udSigma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udEstimatedMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLeft.SuspendLayout()
        Me.pnlIta.SuspendLayout()
        Me.mnMain.SuspendLayout()
        Me.PanelEstimate.SuspendLayout()
        Me.pnlInvisible.SuspendLayout()
        Me.pnlUK.SuspendLayout()
        Me.pnlEurope.SuspendLayout()
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
        Me.Chart1.Location = New System.Drawing.Point(225, 0)
        Me.Chart1.Margin = New System.Windows.Forms.Padding(0)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(914, 710)
        Me.Chart1.TabIndex = 9
        Me.Chart1.Text = "Chart1"
        '
        'cbChartItemITA
        '
        Me.cbChartItemITA.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemITA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemITA.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemITA.FormattingEnabled = True
        Me.cbChartItemITA.Location = New System.Drawing.Point(4, 2)
        Me.cbChartItemITA.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.cbChartItemITA.Name = "cbChartItemITA"
        Me.cbChartItemITA.Size = New System.Drawing.Size(215, 28)
        Me.cbChartItemITA.TabIndex = 15
        '
        'lstItaRegions
        '
        Me.lstItaRegions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstItaRegions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstItaRegions.FormattingEnabled = True
        Me.lstItaRegions.ItemHeight = 17
        Me.lstItaRegions.Location = New System.Drawing.Point(4, 33)
        Me.lstItaRegions.Name = "lstItaRegions"
        Me.lstItaRegions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstItaRegions.Size = New System.Drawing.Size(215, 549)
        Me.lstItaRegions.TabIndex = 30
        Me.lstItaRegions.Visible = False
        '
        'lstItaProvinces
        '
        Me.lstItaProvinces.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstItaProvinces.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstItaProvinces.FormattingEnabled = True
        Me.lstItaProvinces.ItemHeight = 17
        Me.lstItaProvinces.Location = New System.Drawing.Point(4, 30)
        Me.lstItaProvinces.Name = "lstItaProvinces"
        Me.lstItaProvinces.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstItaProvinces.Size = New System.Drawing.Size(215, 592)
        Me.lstItaProvinces.TabIndex = 34
        Me.lstItaProvinces.Visible = False
        '
        'lstRegions
        '
        Me.lstRegions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRegions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRegions.FormattingEnabled = True
        Me.lstRegions.ItemHeight = 17
        Me.lstRegions.Location = New System.Drawing.Point(4, 33)
        Me.lstRegions.Name = "lstRegions"
        Me.lstRegions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRegions.Size = New System.Drawing.Size(221, 400)
        Me.lstRegions.TabIndex = 20
        '
        'cbChartItemWorld
        '
        Me.cbChartItemWorld.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemWorld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemWorld.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemWorld.FormattingEnabled = True
        Me.cbChartItemWorld.Location = New System.Drawing.Point(4, 2)
        Me.cbChartItemWorld.Name = "cbChartItemWorld"
        Me.cbChartItemWorld.Size = New System.Drawing.Size(221, 28)
        Me.cbChartItemWorld.TabIndex = 19
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "PC48.png")
        Me.ImageList1.Images.SetKeyName(1, "WHO42.png")
        '
        'chkDaily
        '
        Me.chkDaily.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkDaily.BackColor = System.Drawing.Color.Transparent
        Me.chkDaily.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkDaily.Location = New System.Drawing.Point(436, 2)
        Me.chkDaily.Margin = New System.Windows.Forms.Padding(0)
        Me.chkDaily.Name = "chkDaily"
        Me.chkDaily.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.chkDaily.Size = New System.Drawing.Size(102, 22)
        Me.chkDaily.TabIndex = 12
        Me.chkDaily.Text = "Day-to-day"
        Me.chkDaily.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkDaily.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkDaily.UseVisualStyleBackColor = False
        '
        'labErrorValue
        '
        Me.labErrorValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labErrorValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.labErrorValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labErrorValue.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labErrorValue.Location = New System.Drawing.Point(5, 202)
        Me.labErrorValue.Name = "labErrorValue"
        Me.labErrorValue.Size = New System.Drawing.Size(66, 25)
        Me.labErrorValue.TabIndex = 35
        Me.labErrorValue.Text = "1000"
        Me.labErrorValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labError
        '
        Me.labError.Cursor = System.Windows.Forms.Cursors.Default
        Me.labError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labError.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labError.Location = New System.Drawing.Point(5, 184)
        Me.labError.Name = "labError"
        Me.labError.Size = New System.Drawing.Size(66, 19)
        Me.labError.TabIndex = 34
        Me.labError.Text = "Err"
        Me.labError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'udCurPosition
        '
        Me.udCurPosition.Location = New System.Drawing.Point(5, 158)
        Me.udCurPosition.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.udCurPosition.Name = "udCurPosition"
        Me.udCurPosition.Size = New System.Drawing.Size(66, 25)
        Me.udCurPosition.TabIndex = 28
        Me.udCurPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.udCurPosition.Value = New Decimal(New Integer() {48, 0, 0, 0})
        '
        'udSigma
        '
        Me.udSigma.DecimalPlaces = 2
        Me.udSigma.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.udSigma.Location = New System.Drawing.Point(5, 113)
        Me.udSigma.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.udSigma.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.udSigma.Name = "udSigma"
        Me.udSigma.Size = New System.Drawing.Size(66, 25)
        Me.udSigma.TabIndex = 26
        Me.udSigma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.udSigma.Value = New Decimal(New Integer() {15, 0, 0, 65536})
        '
        'udEstimatedMax
        '
        Me.udEstimatedMax.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.udEstimatedMax.Location = New System.Drawing.Point(5, 70)
        Me.udEstimatedMax.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.udEstimatedMax.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.udEstimatedMax.Name = "udEstimatedMax"
        Me.udEstimatedMax.Size = New System.Drawing.Size(66, 25)
        Me.udEstimatedMax.TabIndex = 24
        Me.udEstimatedMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.udEstimatedMax.Value = New Decimal(New Integer() {10000, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label2.Location = New System.Drawing.Point(5, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 20)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "%"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label1.Location = New System.Drawing.Point(5, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 20)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "σ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labMaxEstDeaths
        '
        Me.labMaxEstDeaths.Cursor = System.Windows.Forms.Cursors.Default
        Me.labMaxEstDeaths.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMaxEstDeaths.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labMaxEstDeaths.Location = New System.Drawing.Point(5, 51)
        Me.labMaxEstDeaths.Name = "labMaxEstDeaths"
        Me.labMaxEstDeaths.Size = New System.Drawing.Size(66, 20)
        Me.labMaxEstDeaths.TabIndex = 25
        Me.labMaxEstDeaths.Text = "Final"
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
        Me.pnlLeft.Controls.Add(Me.mnMain)
        Me.pnlLeft.Controls.Add(Me.labLastUpdateInfo)
        Me.pnlLeft.Controls.Add(Me.pnlLineSpacer)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(225, 710)
        Me.pnlLeft.TabIndex = 33
        '
        'pnlLine
        '
        Me.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLine.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlLine.ForeColor = System.Drawing.SystemColors.Window
        Me.pnlLine.Location = New System.Drawing.Point(220, 39)
        Me.pnlLine.Margin = New System.Windows.Forms.Padding(6)
        Me.pnlLine.Name = "pnlLine"
        Me.pnlLine.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pnlLine.Size = New System.Drawing.Size(1, 624)
        Me.pnlLine.TabIndex = 42
        '
        'pnlIta
        '
        Me.pnlIta.Controls.Add(Me.lstItaRegions)
        Me.pnlIta.Controls.Add(Me.labSelectionHintITA)
        Me.pnlIta.Controls.Add(Me.Panel2)
        Me.pnlIta.Controls.Add(Me.lstItaProvinces)
        Me.pnlIta.Controls.Add(Me.cbChartItemITA)
        Me.pnlIta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlIta.Location = New System.Drawing.Point(0, 39)
        Me.pnlIta.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlIta.Name = "pnlIta"
        Me.pnlIta.Padding = New System.Windows.Forms.Padding(4, 2, 2, 2)
        Me.pnlIta.Size = New System.Drawing.Size(221, 624)
        Me.pnlIta.TabIndex = 12
        '
        'labSelectionHintITA
        '
        Me.labSelectionHintITA.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintITA.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.labSelectionHintITA.Location = New System.Drawing.Point(4, 582)
        Me.labSelectionHintITA.Name = "labSelectionHintITA"
        Me.labSelectionHintITA.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintITA.Size = New System.Drawing.Size(215, 40)
        Me.labSelectionHintITA.TabIndex = 35
        Me.labSelectionHintITA.Text = "Hold <Ctrl> or <Shift> for multiple selection"
        Me.labSelectionHintITA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(4, 30)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(215, 3)
        Me.Panel2.TabIndex = 37
        '
        'mnMain
        '
        Me.mnMain.BackgroundImage = CType(resources.GetObject("mnMain.BackgroundImage"), System.Drawing.Image)
        Me.mnMain.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnMain.ImageScalingSize = New System.Drawing.Size(34, 34)
        Me.mnMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnMainItem})
        Me.mnMain.Location = New System.Drawing.Point(0, 0)
        Me.mnMain.Name = "mnMain"
        Me.mnMain.Padding = New System.Windows.Forms.Padding(4, 2, 0, 1)
        Me.mnMain.Size = New System.Drawing.Size(221, 39)
        Me.mnMain.TabIndex = 41
        Me.mnMain.Text = "mnMain"
        '
        'mnMainItem
        '
        Me.mnMainItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnWorld, Me.mnEurope, Me.ToolStripMenuItem1, Me.mnUS, Me.ToolStripMenuItem2, Me.mnUK, Me.ToolStripMenuItem3, Me.mnITAFull, Me.mnITARegions, Me.mnITAProvinces, Me.ToolStripMenuItem4, Me.CheckForUpdatedDataToolStripMenuItem})
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
        Me.mnWorld.Size = New System.Drawing.Size(319, 40)
        Me.mnWorld.Text = "World"
        '
        'mnEurope
        '
        Me.mnEurope.Image = CType(resources.GetObject("mnEurope.Image"), System.Drawing.Image)
        Me.mnEurope.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnEurope.Name = "mnEurope"
        Me.mnEurope.Size = New System.Drawing.Size(319, 40)
        Me.mnEurope.Text = "Europe"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(316, 6)
        '
        'mnUS
        '
        Me.mnUS.Image = CType(resources.GetObject("mnUS.Image"), System.Drawing.Image)
        Me.mnUS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnUS.Name = "mnUS"
        Me.mnUS.Size = New System.Drawing.Size(319, 40)
        Me.mnUS.Text = "US"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(316, 6)
        '
        'mnUK
        '
        Me.mnUK.Image = CType(resources.GetObject("mnUK.Image"), System.Drawing.Image)
        Me.mnUK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnUK.Name = "mnUK"
        Me.mnUK.Size = New System.Drawing.Size(319, 40)
        Me.mnUK.Text = "UK"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(316, 6)
        '
        'mnITAFull
        '
        Me.mnITAFull.Image = CType(resources.GetObject("mnITAFull.Image"), System.Drawing.Image)
        Me.mnITAFull.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnITAFull.Name = "mnITAFull"
        Me.mnITAFull.Size = New System.Drawing.Size(319, 40)
        Me.mnITAFull.Text = "Italy"
        '
        'mnITARegions
        '
        Me.mnITARegions.Image = CType(resources.GetObject("mnITARegions.Image"), System.Drawing.Image)
        Me.mnITARegions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnITARegions.Name = "mnITARegions"
        Me.mnITARegions.Size = New System.Drawing.Size(319, 40)
        Me.mnITARegions.Text = "Italy - Regions"
        '
        'mnITAProvinces
        '
        Me.mnITAProvinces.Image = CType(resources.GetObject("mnITAProvinces.Image"), System.Drawing.Image)
        Me.mnITAProvinces.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnITAProvinces.Name = "mnITAProvinces"
        Me.mnITAProvinces.Size = New System.Drawing.Size(319, 40)
        Me.mnITAProvinces.Text = "Italy - Provinces"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(316, 6)
        '
        'CheckForUpdatedDataToolStripMenuItem
        '
        Me.CheckForUpdatedDataToolStripMenuItem.Image = CType(resources.GetObject("CheckForUpdatedDataToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CheckForUpdatedDataToolStripMenuItem.Name = "CheckForUpdatedDataToolStripMenuItem"
        Me.CheckForUpdatedDataToolStripMenuItem.Size = New System.Drawing.Size(319, 40)
        Me.CheckForUpdatedDataToolStripMenuItem.Text = "Check for updated data"
        '
        'labLastUpdateInfo
        '
        Me.labLastUpdateInfo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.labLastUpdateInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labLastUpdateInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labLastUpdateInfo.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.labLastUpdateInfo.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.labLastUpdateInfo.Location = New System.Drawing.Point(0, 663)
        Me.labLastUpdateInfo.Name = "labLastUpdateInfo"
        Me.labLastUpdateInfo.Size = New System.Drawing.Size(221, 47)
        Me.labLastUpdateInfo.TabIndex = 46
        Me.labLastUpdateInfo.Text = "Last updated:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " - - - - - -"
        Me.labLastUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlLineSpacer
        '
        Me.pnlLineSpacer.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlLineSpacer.Location = New System.Drawing.Point(221, 0)
        Me.pnlLineSpacer.Margin = New System.Windows.Forms.Padding(6)
        Me.pnlLineSpacer.Name = "pnlLineSpacer"
        Me.pnlLineSpacer.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pnlLineSpacer.Size = New System.Drawing.Size(4, 710)
        Me.pnlLineSpacer.TabIndex = 43
        '
        'btShowMap
        '
        Me.btShowMap.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btShowMap.Image = CType(resources.GetObject("btShowMap.Image"), System.Drawing.Image)
        Me.btShowMap.Location = New System.Drawing.Point(1051, 653)
        Me.btShowMap.Name = "btShowMap"
        Me.btShowMap.Size = New System.Drawing.Size(66, 45)
        Me.btShowMap.TabIndex = 38
        Me.btShowMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btShowMap.UseVisualStyleBackColor = True
        '
        'PanelEstimate
        '
        Me.PanelEstimate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelEstimate.Controls.Add(Me.btEstimate)
        Me.PanelEstimate.Controls.Add(Me.udEstimatedMax)
        Me.PanelEstimate.Controls.Add(Me.labErrorValue)
        Me.PanelEstimate.Controls.Add(Me.udCurPosition)
        Me.PanelEstimate.Controls.Add(Me.udSigma)
        Me.PanelEstimate.Controls.Add(Me.labMaxEstDeaths)
        Me.PanelEstimate.Controls.Add(Me.Label1)
        Me.PanelEstimate.Controls.Add(Me.Label2)
        Me.PanelEstimate.Controls.Add(Me.labError)
        Me.PanelEstimate.Location = New System.Drawing.Point(1046, 415)
        Me.PanelEstimate.Name = "PanelEstimate"
        Me.PanelEstimate.Size = New System.Drawing.Size(76, 231)
        Me.PanelEstimate.TabIndex = 37
        Me.PanelEstimate.Visible = False
        '
        'chkNormalize
        '
        Me.chkNormalize.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkNormalize.BackColor = System.Drawing.Color.Transparent
        Me.chkNormalize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkNormalize.Location = New System.Drawing.Point(551, 2)
        Me.chkNormalize.Margin = New System.Windows.Forms.Padding(0)
        Me.chkNormalize.Name = "chkNormalize"
        Me.chkNormalize.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.chkNormalize.Size = New System.Drawing.Size(139, 22)
        Me.chkNormalize.TabIndex = 40
        Me.chkNormalize.Text = "Per 100K people"
        Me.chkNormalize.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkNormalize.UseVisualStyleBackColor = False
        '
        'btDateShiftLeft
        '
        Me.btDateShiftLeft.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btDateShiftLeft.Image = CType(resources.GetObject("btDateShiftLeft.Image"), System.Drawing.Image)
        Me.btDateShiftLeft.Location = New System.Drawing.Point(542, 681)
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
        Me.btDateShiftRight.Location = New System.Drawing.Point(651, 681)
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
        Me.pnlInvisible.BackColor = System.Drawing.Color.White
        Me.pnlInvisible.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInvisible.Controls.Add(Me.pnlUK)
        Me.pnlInvisible.Controls.Add(Me.pnlEurope)
        Me.pnlInvisible.Controls.Add(Me.pnlUS)
        Me.pnlInvisible.Controls.Add(Me.pnlWorld)
        Me.pnlInvisible.Location = New System.Drawing.Point(324, 96)
        Me.pnlInvisible.Name = "pnlInvisible"
        Me.pnlInvisible.Size = New System.Drawing.Size(763, 501)
        Me.pnlInvisible.TabIndex = 43
        Me.pnlInvisible.Visible = False
        '
        'pnlUK
        '
        Me.pnlUK.Controls.Add(Me.lstRegionsUK)
        Me.pnlUK.Controls.Add(Me.Panel6)
        Me.pnlUK.Controls.Add(Me.cbChartItemUK)
        Me.pnlUK.Controls.Add(Me.labSelectionHintUK)
        Me.pnlUK.Location = New System.Drawing.Point(493, 20)
        Me.pnlUK.Name = "pnlUK"
        Me.pnlUK.Padding = New System.Windows.Forms.Padding(4, 2, 2, 2)
        Me.pnlUK.Size = New System.Drawing.Size(227, 475)
        Me.pnlUK.TabIndex = 16
        '
        'lstRegionsUK
        '
        Me.lstRegionsUK.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRegionsUK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRegionsUK.FormattingEnabled = True
        Me.lstRegionsUK.ItemHeight = 17
        Me.lstRegionsUK.Location = New System.Drawing.Point(4, 33)
        Me.lstRegionsUK.Name = "lstRegionsUK"
        Me.lstRegionsUK.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRegionsUK.Size = New System.Drawing.Size(221, 400)
        Me.lstRegionsUK.TabIndex = 20
        '
        'Panel6
        '
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(4, 30)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(221, 3)
        Me.Panel6.TabIndex = 38
        '
        'cbChartItemUK
        '
        Me.cbChartItemUK.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemUK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemUK.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemUK.FormattingEnabled = True
        Me.cbChartItemUK.Location = New System.Drawing.Point(4, 2)
        Me.cbChartItemUK.Name = "cbChartItemUK"
        Me.cbChartItemUK.Size = New System.Drawing.Size(221, 28)
        Me.cbChartItemUK.TabIndex = 19
        '
        'labSelectionHintUK
        '
        Me.labSelectionHintUK.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintUK.ForeColor = System.Drawing.SystemColors.Highlight
        Me.labSelectionHintUK.Location = New System.Drawing.Point(4, 433)
        Me.labSelectionHintUK.Name = "labSelectionHintUK"
        Me.labSelectionHintUK.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintUK.Size = New System.Drawing.Size(221, 40)
        Me.labSelectionHintUK.TabIndex = 36
        Me.labSelectionHintUK.Text = "Hold <Ctrl> or <Shift> for multiple selection"
        Me.labSelectionHintUK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlEurope
        '
        Me.pnlEurope.Controls.Add(Me.lstRegionsEurope)
        Me.pnlEurope.Controls.Add(Me.Panel5)
        Me.pnlEurope.Controls.Add(Me.cbChartItemEurope)
        Me.pnlEurope.Controls.Add(Me.labSelectionHintEurope)
        Me.pnlEurope.Location = New System.Drawing.Point(247, 124)
        Me.pnlEurope.Name = "pnlEurope"
        Me.pnlEurope.Padding = New System.Windows.Forms.Padding(4, 2, 2, 2)
        Me.pnlEurope.Size = New System.Drawing.Size(227, 475)
        Me.pnlEurope.TabIndex = 15
        '
        'lstRegionsEurope
        '
        Me.lstRegionsEurope.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRegionsEurope.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRegionsEurope.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstRegionsEurope.FormattingEnabled = True
        Me.lstRegionsEurope.ItemHeight = 17
        Me.lstRegionsEurope.Location = New System.Drawing.Point(4, 33)
        Me.lstRegionsEurope.Name = "lstRegionsEurope"
        Me.lstRegionsEurope.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRegionsEurope.Size = New System.Drawing.Size(221, 400)
        Me.lstRegionsEurope.TabIndex = 20
        '
        'Panel5
        '
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(4, 30)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(221, 3)
        Me.Panel5.TabIndex = 38
        '
        'cbChartItemEurope
        '
        Me.cbChartItemEurope.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemEurope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemEurope.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemEurope.FormattingEnabled = True
        Me.cbChartItemEurope.Location = New System.Drawing.Point(4, 2)
        Me.cbChartItemEurope.Name = "cbChartItemEurope"
        Me.cbChartItemEurope.Size = New System.Drawing.Size(221, 28)
        Me.cbChartItemEurope.TabIndex = 19
        '
        'labSelectionHintEurope
        '
        Me.labSelectionHintEurope.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintEurope.ForeColor = System.Drawing.SystemColors.Highlight
        Me.labSelectionHintEurope.Location = New System.Drawing.Point(4, 433)
        Me.labSelectionHintEurope.Name = "labSelectionHintEurope"
        Me.labSelectionHintEurope.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintEurope.Size = New System.Drawing.Size(221, 40)
        Me.labSelectionHintEurope.TabIndex = 36
        Me.labSelectionHintEurope.Text = "Hold <Ctrl> or <Shift> for multiple selection"
        Me.labSelectionHintEurope.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlUS
        '
        Me.pnlUS.Controls.Add(Me.lstRegionsUS)
        Me.pnlUS.Controls.Add(Me.Panel4)
        Me.pnlUS.Controls.Add(Me.cbChartItemUS)
        Me.pnlUS.Controls.Add(Me.labSelectionHintUS)
        Me.pnlUS.Location = New System.Drawing.Point(111, 62)
        Me.pnlUS.Name = "pnlUS"
        Me.pnlUS.Padding = New System.Windows.Forms.Padding(4, 2, 2, 2)
        Me.pnlUS.Size = New System.Drawing.Size(227, 475)
        Me.pnlUS.TabIndex = 14
        '
        'lstRegionsUS
        '
        Me.lstRegionsUS.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRegionsUS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRegionsUS.FormattingEnabled = True
        Me.lstRegionsUS.ItemHeight = 17
        Me.lstRegionsUS.Location = New System.Drawing.Point(4, 33)
        Me.lstRegionsUS.Name = "lstRegionsUS"
        Me.lstRegionsUS.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRegionsUS.Size = New System.Drawing.Size(221, 400)
        Me.lstRegionsUS.TabIndex = 20
        '
        'Panel4
        '
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(4, 30)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(221, 3)
        Me.Panel4.TabIndex = 38
        '
        'cbChartItemUS
        '
        Me.cbChartItemUS.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbChartItemUS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartItemUS.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbChartItemUS.FormattingEnabled = True
        Me.cbChartItemUS.Location = New System.Drawing.Point(4, 2)
        Me.cbChartItemUS.Name = "cbChartItemUS"
        Me.cbChartItemUS.Size = New System.Drawing.Size(221, 28)
        Me.cbChartItemUS.TabIndex = 19
        '
        'labSelectionHintUS
        '
        Me.labSelectionHintUS.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintUS.ForeColor = System.Drawing.SystemColors.Highlight
        Me.labSelectionHintUS.Location = New System.Drawing.Point(4, 433)
        Me.labSelectionHintUS.Name = "labSelectionHintUS"
        Me.labSelectionHintUS.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintUS.Size = New System.Drawing.Size(221, 40)
        Me.labSelectionHintUS.TabIndex = 36
        Me.labSelectionHintUS.Text = "Hold <Ctrl> or <Shift> for multiple selection"
        Me.labSelectionHintUS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlWorld
        '
        Me.pnlWorld.Controls.Add(Me.lstRegions)
        Me.pnlWorld.Controls.Add(Me.Panel3)
        Me.pnlWorld.Controls.Add(Me.cbChartItemWorld)
        Me.pnlWorld.Controls.Add(Me.labSelectionHintWorld)
        Me.pnlWorld.Location = New System.Drawing.Point(14, 13)
        Me.pnlWorld.Name = "pnlWorld"
        Me.pnlWorld.Padding = New System.Windows.Forms.Padding(4, 2, 2, 2)
        Me.pnlWorld.Size = New System.Drawing.Size(227, 475)
        Me.pnlWorld.TabIndex = 13
        '
        'Panel3
        '
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 30)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(221, 3)
        Me.Panel3.TabIndex = 38
        '
        'labSelectionHintWorld
        '
        Me.labSelectionHintWorld.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labSelectionHintWorld.ForeColor = System.Drawing.SystemColors.Highlight
        Me.labSelectionHintWorld.Location = New System.Drawing.Point(4, 433)
        Me.labSelectionHintWorld.Name = "labSelectionHintWorld"
        Me.labSelectionHintWorld.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labSelectionHintWorld.Size = New System.Drawing.Size(221, 40)
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
        Me.labSnapshot.Size = New System.Drawing.Size(1139, 710)
        Me.labSnapshot.TabIndex = 44
        Me.labSnapshot.Text = "Application starting, please wait ..."
        Me.labSnapshot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkMA
        '
        Me.chkMA.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkMA.BackColor = System.Drawing.Color.Transparent
        Me.chkMA.Checked = True
        Me.chkMA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMA.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkMA.Location = New System.Drawing.Point(690, 2)
        Me.chkMA.Margin = New System.Windows.Forms.Padding(0)
        Me.chkMA.Name = "chkMA"
        Me.chkMA.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.chkMA.Size = New System.Drawing.Size(142, 22)
        Me.chkMA.TabIndex = 45
        Me.chkMA.Text = "Moving average"
        Me.chkMA.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkMA.UseVisualStyleBackColor = False
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1139, 710)
        Me.Controls.Add(Me.labSnapshot)
        Me.Controls.Add(Me.chkMA)
        Me.Controls.Add(Me.pnlInvisible)
        Me.Controls.Add(Me.PanelEstimate)
        Me.Controls.Add(Me.btDateShiftLeft)
        Me.Controls.Add(Me.btShowMap)
        Me.Controls.Add(Me.chkDaily)
        Me.Controls.Add(Me.chkNormalize)
        Me.Controls.Add(Me.btDateShiftRight)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.pnlLeft)
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
        Me.mnMain.ResumeLayout(False)
        Me.mnMain.PerformLayout()
        Me.PanelEstimate.ResumeLayout(False)
        Me.pnlInvisible.ResumeLayout(False)
        Me.pnlUK.ResumeLayout(False)
        Me.pnlEurope.ResumeLayout(False)
        Me.pnlUS.ResumeLayout(False)
        Me.pnlWorld.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents chkDaily As CheckBox
    Friend WithEvents cbChartItemITA As ComboBox
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
    Friend WithEvents btShowMap As Button
    Friend WithEvents PanelEstimate As Panel
    Friend WithEvents chkNormalize As CheckBox
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
    Friend WithEvents pnlLineSpacer As Panel
    Friend WithEvents mnEurope As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
    Friend WithEvents pnlEurope As Panel
    Friend WithEvents lstRegionsEurope As ListBox
    Friend WithEvents cbChartItemEurope As ComboBox
    Friend WithEvents labSelectionHintEurope As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ToolStripMenuItem4 As ToolStripSeparator
    Friend WithEvents CheckForUpdatedDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents labLastUpdateInfo As Label
    Friend WithEvents chkMA As CheckBox
    Friend WithEvents pnlUK As Panel
    Friend WithEvents lstRegionsUK As ListBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents cbChartItemUK As ComboBox
    Friend WithEvents labSelectionHintUK As Label
End Class
