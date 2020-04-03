Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Net

Public Class frmMain
    Private italianRecords As New cITARecords
    Private worldRecords As New cWorldRecords
    Private USRecords As New cWorldRecords
    Private myDisplayInfo As New cDisplayInfo
    Private italianRegionRecords As New cITARegionsRecords
    Private italianProvincesRecords As New cITAProvincesRecords

    Private Function GetLatestInfo() As Boolean
        Try
            Dim globalDeathsUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_deaths_global.csv"
            globalDeathsUrl = globalDeathsUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim globalConfirmedUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv"
            globalConfirmedUrl = globalConfirmedUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim globalRecoveredUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_recovered_global.csv"
            globalRecoveredUrl = globalRecoveredUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim ITAFullDataUrl As String = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-andamento-nazionale/dpc-covid19-ita-andamento-nazionale.csv"
            ITAFullDataUrl = ITAFullDataUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim ITARegionsDataUrl As String = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-regioni/dpc-covid19-ita-regioni.csv"
            ITARegionsDataUrl = ITARegionsDataUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim ITAProvincesDataUrl As String = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-province/dpc-covid19-ita-province.csv"
            ITAProvincesDataUrl = ITAProvincesDataUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim USDeathsDataUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_deaths_US.csv"
            USDeathsDataUrl = USDeathsDataUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim USConfirmedDataUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_US.csv"
            USConfirmedDataUrl = USConfirmedDataUrl.Replace("/open?id=", "/uc?export=download&id=")

            Dim wc As New WebClient()

            'Italian data
            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (Italy full data), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(ITAFullDataUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_Ita_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading Italy Full Data")
            End Try

            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (Italy-Regions), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(ITARegionsDataUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_ItaRegions_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading Italy Regions Data")
            End Try

            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (Italy-Provinces), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(ITAProvincesDataUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_ItaProvinces_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading Italy Provinces Data")
            End Try

            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (World deaths), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(globalDeathsUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_World_Deaths_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading World Deaths Data")
            End Try

            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (World confirmed), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(globalConfirmedUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_World_Confirmed_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading World Confirmed Data")
            End Try

            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (World recovered), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(globalRecoveredUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_World_Recovered_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading World Recovered Data")
            End Try

            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (US confirmed), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(USConfirmedDataUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_US_Confirmed_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading US Confirmed Data")
            End Try

            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                SetInfoText("Checking for available updates (US deaths), please wait ...")
                Application.DoEvents()
                wc.DownloadFile(USDeathsDataUrl, Csv_TmpPath)
                SetInfoText(WindowTitle)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_US_Deaths_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading US Deaths Data")
            End Try

            Return LoadInfoFromLocalFiles()
        Catch ex As Exception
            Call MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function LoadInfoFromLocalFiles() As Boolean
        Try
            If System.IO.File.Exists(Csv_Ita_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_Ita_Filename)
                ReplaceCommasInQuotations(infoLines)
                italianRecords = New cITARecords(infoLines)
                labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianRecords.LastDate.ToShortDateString
            End If

            If System.IO.File.Exists(Csv_ItaRegions_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_ItaRegions_Filename)
                ReplaceCommasInQuotations(infoLines)
                italianRegionRecords = New cITARegionsRecords(infoLines)
            End If

            If System.IO.File.Exists(Csv_ItaProvinces_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_ItaProvinces_Filename)
                ReplaceCommasInQuotations(infoLines)
                italianProvincesRecords = New cITAProvincesRecords(infoLines)
            End If

            If System.IO.File.Exists(Csv_World_Confirmed_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_World_Confirmed_Filename)
                ReplaceCommasInQuotations(infoLines)
                worldRecords.SetConfirmed(infoLines)
            End If

            If System.IO.File.Exists(Csv_World_Deaths_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_World_Deaths_Filename)
                ReplaceCommasInQuotations(infoLines)
                worldRecords.SetDeaths(infoLines)
            End If

            If System.IO.File.Exists(Csv_World_Recovered_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_World_Recovered_Filename)
                ReplaceCommasInQuotations(infoLines)
                worldRecords.SetRecovered(infoLines)
            End If

            If System.IO.File.Exists(Csv_US_Confirmed_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_US_Confirmed_Filename)
                ReplaceCommasInQuotations(infoLines)
                USRecords.SetConfirmed(infoLines)
            End If

            If System.IO.File.Exists(Csv_US_Deaths_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_US_Deaths_Filename)
                ReplaceCommasInQuotations(infoLines)
                USRecords.SetDeaths(infoLines)
            End If


            lstRegionsUS.SuspendLayout()
            lstRegionsUS.Items.Clear()
            Dim allUSNames As System.Collections.Generic.List(Of cCountryListboxItem) = USRecords.GetRegionNames(myDisplayInfo.ActiveUSData)
            For iCounter As Integer = 0 To allUSNames.Count - 1
                lstRegionsUS.Items.Add(allUSNames(iCounter))
            Next
            lstRegionsUS.ResumeLayout(True)
            If lstRegionsUS.Items.Count > 0 Then
                lstRegionsUS.SelectedIndex = 0
            End If

            lstRegions.SuspendLayout()
            lstRegions.Items.Clear()
            Dim allNames As System.Collections.Generic.List(Of cCountryListboxItem) = worldRecords.GetRegionNames(myDisplayInfo.ActiveWorldData)
            For iCounter As Integer = 0 To allNames.Count - 1
                lstRegions.Items.Add(allNames(iCounter))
            Next
            lstRegions.ResumeLayout(True)
            If lstRegions.Items.Count > 0 Then
                lstRegions.SelectedIndex = 0
            End If

            lstItaRegions.SuspendLayout()
            lstItaRegions.Items.Clear()
            Dim allITARegionNames As System.Collections.Generic.List(Of cCountryListboxItem) = italianRegionRecords.GetRegionNames(myDisplayInfo.ActiveItalianData)
            For iCounter As Integer = 0 To allITARegionNames.Count - 1
                lstItaRegions.Items.Add(allITARegionNames(iCounter))
            Next
            lstItaRegions.ResumeLayout(True)
            If lstItaRegions.Items.Count > 0 Then
                lstItaRegions.SelectedIndex = 0
            End If

            lstItaProvinces.SuspendLayout()
            lstItaProvinces.Items.Clear()
            Dim allITAProvinceNames As System.Collections.Generic.List(Of cCountryListboxItem) = italianProvincesRecords.GetRegionNames(cDisplayInfo.enItalianValueType.totale_casi)
            For iCounter As Integer = 0 To allITAProvinceNames.Count - 1
                lstItaProvinces.Items.Add(allITAProvinceNames(iCounter))
            Next
            lstItaProvinces.ResumeLayout(True)
            If lstItaProvinces.Items.Count > 0 Then
                lstItaProvinces.SelectedIndex = 0
            End If
            DataLoaded = True
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Sub FillCombos()
        Try
            udSigma.Minimum = cNormalDist.SigmaMin
            udSigma.Maximum = cNormalDist.SigmaMax

            If cbChartItemITA.Items.Count > 0 Then Return
            cbChartItemITA.Items.Clear()
            Dim myITATypes() As cDisplayInfo.enItalianValueType = System.Enum.GetValues(GetType(cDisplayInfo.enItalianValueType))
            For iCounter As Integer = 0 To myITATypes.Length - 1
                cbChartItemITA.Items.Add(myITATypes(iCounter).ToString)
            Next
            Try
                cbChartItemITA.SelectedIndex = cDisplayInfo.enItalianValueType.deceduti
            Catch ex2 As Exception
            End Try

            If cbChartItemWorld.Items.Count > 0 Then Return
            cbChartItemWorld.Items.Clear()
            Dim myWorldTypes() As cDisplayInfo.enWorldValueType = System.Enum.GetValues(GetType(cDisplayInfo.enWorldValueType))
            For iCounter As Integer = 0 To myWorldTypes.Length - 1
                cbChartItemWorld.Items.Add(myWorldTypes(iCounter).ToString)
            Next
            Try
                cbChartItemWorld.SelectedIndex = cDisplayInfo.enWorldValueType.Deaths
            Catch ex2 As Exception
            End Try

            If cbChartItemUS.Items.Count > 0 Then Return
            cbChartItemUS.Items.Clear()
            Dim myUSTypes() As cDisplayInfo.enWorldValueType = System.Enum.GetValues(GetType(cDisplayInfo.enWorldValueType))
            For iCounter As Integer = 0 To myUSTypes.Length - 1
                cbChartItemUS.Items.Add(myUSTypes(iCounter).ToString)
            Next
            Try
                cbChartItemUS.SelectedIndex = cDisplayInfo.enWorldValueType.Deaths
            Catch ex2 As Exception
            End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private myFirstActivationDone As Boolean = False
    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Not myFirstActivationDone Then
            Me.Text = Application.ProductName + " V" + Application.ProductVersion
            myFirstActivationDone = True
            RestoreWindowPosition(Me)
            Me.Show()
            Application.DoEvents()
            Me.Refresh()
            FillCombos()
            Application.DoEvents()
            Me.Refresh()
            GetLatestInfo()
            UpdateAndRefresh(False)
        End If
    End Sub
    Private Sub btUpdate_ButtonClick(sender As Object, e As EventArgs) Handles btCheck4Updates.Click
        GetLatestInfo()
        UpdateAndRefresh(False)
    End Sub
    Private Sub chkDaily_CheckedChanged(sender As Object, e As EventArgs) Handles chkDaily.CheckedChanged
        UpdateAndRefresh(False)
    End Sub
    Private Sub EnableEstimateSection(ByVal newState As Boolean)
        udEstimatedMax.Enabled = newState
        udSigma.Enabled = newState
        udCurPosition.Enabled = newState
        btEstimate.Enabled = newState
    End Sub
    Private Sub UpdateDisplayInfo(ByVal showEstimate As Boolean)
        Try
            myDisplayInfo.ShowEstimate = showEstimate

            If myDisplayInfo.ShowITA Then
                pnlUS.Visible = False
                pnlWorld.Visible = False
                pnlIta.Visible = True
                If Not pnlLeft.Controls.Contains(pnlIta) Then
                    pnlLeft.Controls.Add(pnlIta)
                    pnlIta.Dock = DockStyle.Fill
                    pnlIta.Visible = True
                    pnlIta.BringToFront()
                End If
                Select Case myDisplayInfo.ActiveArea
                    Case cDisplayInfo.enActiveArea.ITA
                        labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianRecords.LastDate.ToShortDateString
                        mnMainItem.Text = mnITAFull.Text
                        mnMainItem.Image = mnITAFull.Image
                        lstItaProvinces.Visible = False
                        lstItaRegions.Visible = False
                        labSelectionHintITA.Visible = False
                    Case cDisplayInfo.enActiveArea.ITA_Provinces
                        labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianProvincesRecords.LastDate.ToShortDateString
                        mnMainItem.Text = mnITAProvinces.Text
                        mnMainItem.Image = mnITAProvinces.Image
                        lstItaRegions.Visible = False
                        lstItaProvinces.Visible = True
                        labSelectionHintITA.Visible = True
                    Case cDisplayInfo.enActiveArea.ITA_Regions
                        labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianRegionRecords.LastDate.ToShortDateString
                        mnMainItem.Text = mnITARegions.Text
                        mnMainItem.Image = mnITARegions.Image
                        lstItaProvinces.Visible = False
                        lstItaRegions.Visible = True
                        labSelectionHintITA.Visible = True
                End Select
            ElseIf myDisplayInfo.ShowWorld Then
                pnlIta.Visible = False
                pnlUS.Visible = False
                pnlWorld.Visible = True
                mnMainItem.Text = mnWorld.Text
                mnMainItem.Image = mnWorld.Image
                labLastUpdateInfo.Text = "Last update:" + vbCrLf + worldRecords.LastDate.ToShortDateString
                If Not pnlLeft.Controls.Contains(pnlWorld) Then
                    pnlLeft.Controls.Add(pnlWorld)
                    pnlWorld.Dock = DockStyle.Fill
                    pnlWorld.BringToFront()
                End If
            ElseIf myDisplayInfo.ShowUS Then
                pnlIta.Visible = False
                pnlWorld.Visible = False
                pnlUS.Visible = True
                mnMainItem.Text = mnUS.Text
                mnMainItem.Image = mnUS.Image
                labLastUpdateInfo.Text = "Last update:" + vbCrLf + USRecords.LastDate.ToShortDateString
                If Not pnlLeft.Controls.Contains(pnlUS) Then
                    pnlLeft.Controls.Add(pnlUS)
                    pnlUS.Dock = DockStyle.Fill
                    pnlUS.BringToFront()
                End If
            End If


            myDisplayInfo.DailyIncrements = chkDaily.Checked
            myDisplayInfo.ActiveItalianData = cbChartItemITA.SelectedIndex
            myDisplayInfo.ActiveWorldData = cbChartItemWorld.SelectedIndex

            myDisplayInfo.ActiveWorldRegions.Clear()
            If lstRegions.SelectedItems.Count > 0 Then
                For iCounter As Integer = 0 To lstRegions.SelectedItems.Count - 1
                    myDisplayInfo.ActiveWorldRegions.Add(lstRegions.SelectedItems(iCounter))
                Next
            End If

            myDisplayInfo.ActiveUSRegions.Clear()
            If lstRegionsUS.SelectedItems.Count > 0 Then
                For iCounter As Integer = 0 To lstRegionsUS.SelectedItems.Count - 1
                    myDisplayInfo.ActiveUSRegions.Add(lstRegionsUS.SelectedItems(iCounter))
                Next
            End If

            myDisplayInfo.ActiveITARegions.Clear()
            If lstItaRegions.SelectedItems.Count > 0 Then
                For iCounter As Integer = 0 To lstItaRegions.SelectedItems.Count - 1
                    myDisplayInfo.ActiveITARegions.Add(lstItaRegions.SelectedItems(iCounter))
                Next
            End If

            myDisplayInfo.ActiveITAProvinces.Clear()
            If lstItaProvinces.SelectedItems.Count > 0 Then
                For iCounter As Integer = 0 To lstItaProvinces.SelectedItems.Count - 1
                    myDisplayInfo.ActiveITAProvinces.Add(lstItaProvinces.SelectedItems(iCounter))
                Next
            End If

            If myDisplayInfo.ShowWorld Then
                btShowMap.Enabled = True
                If myDisplayInfo.ActiveWorldRegions.Count = 1 Then
                    btDateShiftLeft.Visible = False
                    btDateShiftRight.Visible = False
                Else
                    btDateShiftLeft.Visible = True
                    btDateShiftRight.Visible = True
                End If
            ElseIf myDisplayInfo.ShowUS Then
                btShowMap.Enabled = True
                If myDisplayInfo.ActiveUSRegions.Count = 1 Then
                    btDateShiftLeft.Visible = False
                    btDateShiftRight.Visible = False
                Else
                    btDateShiftLeft.Visible = True
                    btDateShiftRight.Visible = True
                End If
            ElseIf myDisplayInfo.ShowITA Then
                If myDisplayInfo.ActiveITARegions.Count = 1 Then
                    btDateShiftLeft.Visible = False
                    btDateShiftRight.Visible = False
                Else
                    btDateShiftLeft.Visible = True
                    btDateShiftRight.Visible = True
                End If
            End If

            myDisplayInfo.EstimatedFinalValue = udEstimatedMax.Value
            myDisplayInfo.EstimatedSigma = udSigma.Value
            myDisplayInfo.EstimatedCurPos100 = udCurPosition.Value
            Select Case myDisplayInfo.ActiveArea
                Case cDisplayInfo.enActiveArea.ITA
                    btShowMap.Enabled = True
                    cbChartItemITA.Enabled = True
                Case cDisplayInfo.enActiveArea.ITA_Provinces
                    btShowMap.Enabled = False
                    cbChartItemITA.Enabled = False
                    If cbChartItemITA.SelectedIndex <> cDisplayInfo.enItalianValueType.totale_casi Then
                        cbChartItemITA.SelectedIndex = cDisplayInfo.enItalianValueType.totale_casi
                    End If
                Case cDisplayInfo.enActiveArea.ITA_Regions
                    btShowMap.Enabled = True
                    cbChartItemITA.Enabled = True
            End Select

            If myDisplayInfo.ShowWorld Then
                chkNormalize.Enabled = True
                NormalizeToPopulation = chkNormalize.Checked
            ElseIf myDisplayInfo.ShowUS Then
                chkNormalize.Enabled = True
                NormalizeToPopulation = chkNormalize.Checked
            ElseIf myDisplayInfo.ShowITA Then
                Select Case myDisplayInfo.ActiveArea
                    Case cDisplayInfo.enActiveArea.ITA
                        chkNormalize.Enabled = True
                        NormalizeToPopulation = chkNormalize.Checked
                    Case cDisplayInfo.enActiveArea.ITA_Provinces
                        chkNormalize.Enabled = True
                        NormalizeToPopulation = chkNormalize.Checked
                    Case cDisplayInfo.enActiveArea.ITA_Regions
                        chkNormalize.Enabled = True
                        NormalizeToPopulation = chkNormalize.Checked
                End Select
            End If

            If myDisplayInfo.ShowWorld Then
                If myDisplayInfo.ActiveWorldRegions.Count = 1 Then
                    EnableEstimateSection((Not myDisplayInfo.DailyIncrements) AndAlso (Not NormalizeToPopulation))
                Else
                    EnableEstimateSection(False)
                End If
            ElseIf myDisplayInfo.ShowUS Then
                If myDisplayInfo.ActiveUSRegions.Count = 1 Then
                    EnableEstimateSection((Not myDisplayInfo.DailyIncrements) AndAlso (Not NormalizeToPopulation))
                Else
                    EnableEstimateSection(False)
                End If
            ElseIf myDisplayInfo.ShowITA Then
                If myDisplayInfo.ActiveITARegions.Count = 1 Then
                    EnableEstimateSection(Not myDisplayInfo.DailyIncrements AndAlso (Not NormalizeToPopulation))
                Else
                    EnableEstimateSection(False)
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UpdateAndRefresh(ByVal showEstimate As Boolean)
        Try
            UpdateDisplayInfo(showEstimate)
            RefreshVisualization(Chart1, italianRecords, italianRegionRecords, italianProvincesRecords, worldRecords, USRecords, myDisplayInfo)

            Dim distErr As Double = 0
            Dim NormalDistribution As New cNormalDist
            NormalDistribution.ExpectedMax = myDisplayInfo.EstimatedFinalValue
            NormalDistribution.Sigma = myDisplayInfo.EstimatedSigma
            NormalDistribution.CurPos100 = myDisplayInfo.EstimatedCurPos100

            If showEstimate Then
                If myDisplayInfo.ShowITA Then
                    Select Case myDisplayInfo.ActiveArea
                        Case cDisplayInfo.enActiveArea.ITA
                            distErr = NormalDistribution.EstimateDiffFromValues(GetPlotPointsIta(italianRecords, myDisplayInfo))
                        Case cDisplayInfo.enActiveArea.ITA_Provinces
                            distErr = NormalDistribution.EstimateDiffFromValues(GetPlotPointsItaProvinceFirst(italianProvincesRecords, myDisplayInfo))
                        Case cDisplayInfo.enActiveArea.ITA_Regions
                            distErr = NormalDistribution.EstimateDiffFromValues(GetPlotPointsItaRegionFirst(italianRegionRecords, myDisplayInfo))
                    End Select
                ElseIf myDisplayInfo.ShowWorld Then
                    distErr = NormalDistribution.EstimateDiffFromValues(GetPlotPointsWorldRegionFirst(worldRecords, myDisplayInfo))
                ElseIf myDisplayInfo.ShowUS Then
                    distErr = NormalDistribution.EstimateDiffFromValues(GetPlotPointsUSRegionFirst(USRecords, myDisplayInfo))
                End If
                labErrorValue.Text = CStr(CInt(distErr))
            Else
                labErrorValue.Text = "-"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub SetInfoText(ByVal newText As String)
        Try
            If MyBase.InvokeRequired Then
                Dim myD As New cNormalDist.SetInfoText_delegate(AddressOf SetInfoText_ex)
                MyBase.BeginInvoke(myD, newText)
            Else
                SetInfoText_ex(newText)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub SetInfoText_ex(ByVal newText As String)
        Try
            Me.Text = newText
            Me.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub cbChartItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbChartItemITA.SelectedIndexChanged, cbChartItemWorld.SelectedIndexChanged, cbChartItemUS.SelectedIndexChanged
        If sender Is cbChartItemWorld Then
            myDisplayInfo.ActiveWorldData = cbChartItemWorld.SelectedIndex
        ElseIf sender Is cbChartItemUS Then
            myDisplayInfo.ActiveUSData = cbChartItemUS.SelectedIndex
        ElseIf sender Is cbChartItemITA Then
            myDisplayInfo.ActiveItalianData = cbChartItemITA.SelectedIndex
        End If
        LoadInfoFromLocalFiles()
        UpdateAndRefresh(False)
    End Sub
    Private Sub lstRegions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRegions.SelectedIndexChanged
        UpdateAndRefresh(False)
    End Sub
    Private Sub labLastUpdateInfo_Click(sender As Object, e As EventArgs) Handles labLastUpdateInfo.Click
        Try
            Dim startInfo As New ProcessStartInfo
            Select Case myDisplayInfo.ActiveArea
                Case cDisplayInfo.enActiveArea.ITA, cDisplayInfo.enActiveArea.ITA_Provinces, cDisplayInfo.enActiveArea.ITA_Regions
                    startInfo.FileName = "https://opendatadpc.maps.arcgis.com/apps/opsdashboard/index.html#/b0c68bce2cce478eaac82fe38d4138b1"
                Case cDisplayInfo.enActiveArea.US, cDisplayInfo.enActiveArea.World
                    startInfo.FileName = "https://gisanddata.maps.arcgis.com/apps/opsdashboard/index.html#/bda7594740fd40299423467b48e9ecf6"
            End Select
            startInfo.UseShellExecute = True
            startInfo.WindowStyle = ProcessWindowStyle.Normal
            Process.Start(startInfo)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs)
        UpdateAndRefresh(False)
    End Sub
    Private Sub udEstimatedMax_ValueChanged(sender As Object, e As EventArgs) Handles udEstimatedMax.ValueChanged
        UpdateAndRefresh(True)
    End Sub
    Private Sub udSigma_ValueChanged(sender As Object, e As EventArgs) Handles udSigma.ValueChanged
        UpdateAndRefresh(True)
    End Sub
    Private Sub udCurPosition_ValueChanged(sender As Object, e As EventArgs) Handles udCurPosition.ValueChanged
        UpdateAndRefresh(True)
    End Sub

    Private Sub lstItaRegions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstItaRegions.SelectedIndexChanged, lstItaProvinces.SelectedIndexChanged
        UpdateAndRefresh(False)
    End Sub
    Private Sub Chart1_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart1.MouseMove
        Try
            Dim chArea As ChartArea = Chart1.ChartAreas(0)
            Dim xValue As Double = chArea.AxisX.PixelPositionToValue(e.X)
            Dim yValue As Double = chArea.AxisY.PixelPositionToValue(e.Y)

            Dim startDate As Date = ChartStartingDate
            startDate = startDate.AddDays(CInt(Math.Truncate(xValue - 0.5)))

            If (xValue <= chArea.AxisX.Maximum) AndAlso (xValue >= chArea.AxisX.Minimum) AndAlso (yValue >= chArea.AxisY.Minimum) AndAlso (yValue <= chArea.AxisY.Maximum) Then
                ToolTip1.SetToolTip(Chart1, startDate.ToShortDateString + ", " + CStr(CInt(yValue)))
            Else
                ToolTip1.SetToolTip(Chart1, "")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btEstimate_Click(sender As Object, e As EventArgs) Handles btEstimate.Click
        Try
            Dim NormalDistribution As New cNormalDist
            NormalDistribution.ExpectedMax = myDisplayInfo.EstimatedFinalValue
            NormalDistribution.Sigma = myDisplayInfo.EstimatedSigma
            NormalDistribution.CurPos100 = myDisplayInfo.EstimatedCurPos100
            Me.UseWaitCursor = True
            Me.Refresh()
            Application.DoEvents()
            If myDisplayInfo.ShowWorld Then
                NormalDistribution.FindBestEstimate(GetPlotPointsWorldRegionFirst(worldRecords, myDisplayInfo), AddressOf SetInfoText)
            ElseIf myDisplayInfo.ShowIta Then
                Select Case myDisplayInfo.ActiveArea
                    Case cDisplayInfo.enActiveArea.ITA
                        NormalDistribution.FindBestEstimate(GetPlotPointsIta(italianRecords, myDisplayInfo), AddressOf SetInfoText)
                    Case cDisplayInfo.enActiveArea.ITA_Provinces
                        NormalDistribution.FindBestEstimate(GetPlotPointsItaProvinceFirst(italianProvincesRecords, myDisplayInfo), AddressOf SetInfoText)
                    Case cDisplayInfo.enActiveArea.ITA_Regions
                        NormalDistribution.FindBestEstimate(GetPlotPointsItaRegionFirst(italianRegionRecords, myDisplayInfo), AddressOf SetInfoText)
                End Select
            End If
            udEstimatedMax.Value = NormalDistribution.ExpectedMax
            udSigma.Value = NormalDistribution.Sigma
            udCurPosition.Value = NormalDistribution.CurPos100
            UpdateAndRefresh(True)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SetInfoText(WindowTitle)
            Me.UseWaitCursor = False
            Me.Refresh()
            Application.DoEvents()
        End Try
    End Sub
    Private Function WindowTitle() As String
        Return Application.ProductName + " V" + Application.ProductVersion
    End Function
    Private Sub ShiftChartSeries(ByVal daysShift As Integer)
        Try
            If myDisplayInfo.DailyIncrements Then Return

            If myDisplayInfo.ShowWorld Then
                If lstRegions.SelectedIndices.Count > 1 Then
                    If daysShift <> 0 Then
                        'First one will not be affected
                        For lCounter As Integer = 1 To lstRegions.SelectedIndices.Count - 1
                            worldRecords.ShiftDays(myDisplayInfo.ActiveWorldData, lstRegions.Items(lstRegions.SelectedIndices(lCounter)), daysShift)
                        Next
                        UpdateAndRefresh(False)
                    End If
                End If
            ElseIf myDisplayInfo.Showus Then
                If lstRegionsUS.SelectedIndices.Count > 1 Then
                    If daysShift <> 0 Then
                        'First one will not be affected
                        For lCounter As Integer = 1 To lstRegionsUS.SelectedIndices.Count - 1
                            USRecords.ShiftDays(myDisplayInfo.ActiveUSData, lstRegionsUS.Items(lstRegionsUS.SelectedIndices(lCounter)), daysShift)
                        Next
                        UpdateAndRefresh(False)
                    End If
                End If

            Else
                Select Case myDisplayInfo.ActiveArea
                    Case cDisplayInfo.enActiveArea.ITA
                        'Nop 
                    Case cDisplayInfo.enActiveArea.ITA_Provinces
                        If lstItaProvinces.SelectedIndices.Count > 1 Then
                            If daysShift <> 0 Then
                                'First one will not be affected
                                For lCounter As Integer = 1 To lstItaProvinces.SelectedIndices.Count - 1
                                    italianProvincesRecords.ShiftDays(myDisplayInfo.ActiveItalianData, lstItaProvinces.Items(lstItaProvinces.SelectedIndices(lCounter)), daysShift)
                                Next
                                UpdateAndRefresh(False)
                            End If
                        End If
                    Case cDisplayInfo.enActiveArea.ITA_Regions
                        If lstItaRegions.SelectedIndices.Count > 1 Then
                            If daysShift <> 0 Then
                                'First one will not be affected
                                For lCounter As Integer = 1 To lstItaRegions.SelectedIndices.Count - 1
                                    italianRegionRecords.ShiftDays(myDisplayInfo.ActiveItalianData, lstItaRegions.Items(lstItaRegions.SelectedIndices(lCounter)), daysShift)
                                Next
                                UpdateAndRefresh(False)
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub picMap_Click(sender As Object, e As EventArgs) Handles btShowMap.Click
        Try
            BuildHeatMap(worldRecords, USRecords, italianRegionRecords, myDisplayInfo)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveWindowPosition(Me)
    End Sub

    Private Sub btDateShiftLeft_Click(sender As Object, e As EventArgs) Handles btDateShiftLeft.Click, btDateShiftRight.Click
        Try
            If sender Is btDateShiftLeft Then
                ShiftChartSeries(-1)
            ElseIf sender Is btDateShiftRight Then
                ShiftChartSeries(1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub chkNormalize_CheckedChanged(sender As Object, e As EventArgs) Handles chkNormalize.CheckedChanged
        UpdateAndRefresh(False)
    End Sub
    Private Sub cbITAResolution_SelectedIndexChanged(sender As Object, e As EventArgs)
        UpdateAndRefresh(False)
    End Sub
    Private Sub mnITAFull_Click(sender As Object, e As EventArgs) Handles mnITAFull.Click
        myDisplayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA
        UpdateAndRefresh(False)
    End Sub
    Private Sub mnITARegions_Click(sender As Object, e As EventArgs) Handles mnITARegions.Click
        myDisplayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Regions
        UpdateAndRefresh(False)
    End Sub
    Private Sub mnITAProvinces_Click(sender As Object, e As EventArgs) Handles mnITAProvinces.Click
        myDisplayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces
        UpdateAndRefresh(False)
    End Sub
    Private Sub mnWorld_Click(sender As Object, e As EventArgs) Handles mnWorld.Click
        myDisplayInfo.ActiveArea = cDisplayInfo.enActiveArea.World
        UpdateAndRefresh(False)
    End Sub
    Private Sub mnUS_Click(sender As Object, e As EventArgs) Handles mnUS.Click
        myDisplayInfo.ActiveArea = cDisplayInfo.enActiveArea.US
        UpdateAndRefresh(False)
    End Sub
    Private Sub lstRegionsUS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRegionsUS.SelectedIndexChanged
        UpdateAndRefresh(False)
    End Sub
End Class
