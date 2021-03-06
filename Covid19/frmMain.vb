﻿Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Net

Public Class frmMain
    Private italianRecords As New cITARecords
    Private worldRecords As New cWorldRecords
    Private europeanRecords As New cWorldRecords 'Identical to world records, with records of non european countries removed
    Private USRecords As New cWorldRecords
    Private UKRecords As New cWorldRecords
    Private myDisplayInfo As New cDisplayInfo
    Private italianRegionRecords As New cITARegionsRecords
    Private italianProvincesRecords As New cITAProvincesRecords
    Private Sub AddInfoText(ByVal message As String)
        labSnapshot.Text = labSnapshot.Text + vbCrLf + message
        labSnapshot.Refresh()
        Application.DoEvents()
    End Sub
    Private Function GetLatestInfo() As Boolean
        Try
            EnableControls(False)

            Dim globalDeathsUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_deaths_global.csv"
            Dim globalConfirmedUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv"
            Dim globalRecoveredUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_recovered_global.csv"
            Dim ITAFullDataUrl As String = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-andamento-nazionale/dpc-covid19-ita-andamento-nazionale.csv"
            Dim ITARegionsDataUrl As String = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-regioni/dpc-covid19-ita-regioni.csv"
            Dim ITAProvincesDataUrl As String = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-province/dpc-covid19-ita-province.csv"
            Dim USDeathsDataUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_deaths_US.csv"
            Dim USConfirmedDataUrl As String = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_US.csv"
            Dim UKDeathsDataUrl As String = "https://c19downloads.azureedge.net/downloads/csv/coronavirus-deaths_latest.csv" 'https://coronavirus.data.gov.uk/downloads/csv/coronavirus-deaths_latest.csv
            Dim UKConfirmedDataUrl As String = "https://c19downloads.azureedge.net/downloads/csv/coronavirus-cases_latest.csv" '"https://coronavirus.data.gov.uk/downloads/csv/coronavirus-cases_latest.csv"

            Dim tmpWebClient As New WebClient()

            AddInfoText("Checking for available updates, please wait ..." + vbCrLf)

            'Italian data
            Try
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Delete(Csv_TmpPath)
                End If
                AddInfoText("Italy (full data)")
                Application.DoEvents()
                tmpWebClient.DownloadFile(ITAFullDataUrl, Csv_TmpPath)
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
                AddInfoText("Italy (Regions)")
                Application.DoEvents()
                tmpWebClient.DownloadFile(ITARegionsDataUrl, Csv_TmpPath)
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
                AddInfoText("Italy (Provinces)")
                Application.DoEvents()
                tmpWebClient.DownloadFile(ITAProvincesDataUrl, Csv_TmpPath)
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
                AddInfoText("World Deaths")
                Application.DoEvents()
                tmpWebClient.DownloadFile(globalDeathsUrl, Csv_TmpPath)
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
                AddInfoText("World Confirmed")
                Application.DoEvents()
                tmpWebClient.DownloadFile(globalConfirmedUrl, Csv_TmpPath)
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
                AddInfoText("World Recovered")
                Application.DoEvents()
                tmpWebClient.DownloadFile(globalRecoveredUrl, Csv_TmpPath)
                Application.DoEvents()
                If System.IO.File.Exists(Csv_TmpPath) Then
                    System.IO.File.Copy(Csv_TmpPath, Csv_World_Recovered_Filename, True)
                End If
            Catch ex As Exception
                MsgBox("An error occurred downloading World Recovered Data")
            End Try

            Return LoadInfoFromLocalFiles()
        Catch ex As Exception
            Call MsgBox(ex.Message)
            Return False
        Finally
            EnableControls(True)
        End Try
    End Function
    Private ReadOnly Property AreControlsEnabled As Boolean
        Get
            Return Not labSnapshot.Visible
        End Get
    End Property
    Private Sub EnableControls(ByVal enable As Boolean)
        Dim disable As Boolean = Not enable
        If disable Then
            If AreControlsEnabled Then
                labSnapshot.Text = "" 'Clear text on startup
            End If
            labSnapshot.Visible = False
            Me.Refresh()
            Application.DoEvents()
            labSnapshot.Image = GetWindowImageGrayed(Me)
            labSnapshot.Visible = True
            labSnapshot.BringToFront()
            Me.Refresh()
            Application.DoEvents()
        Else
            labSnapshot.Visible = False
            Me.Refresh()
            Application.DoEvents()
        End If
    End Sub
    Private Function LoadInfoFromLocalFiles() As Boolean
        If SkipDataRefresh Then Return False
        Dim reEnableWhenDone As Boolean = False
        Try

            If AreControlsEnabled Then
                reEnableWhenDone = True
                EnableControls(False)
            End If
            AddInfoText("Loading data")

            Dim startDate As Date = dtpStartDate.Value

            If System.IO.File.Exists(Csv_Ita_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_Ita_Filename)
                ReplaceCommasInQuotations(infoLines)
                italianRecords = New cITARecords(infoLines, startDate)
                labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianRecords.LastDate.ToLongDateString
            End If

            If System.IO.File.Exists(Csv_ItaRegions_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_ItaRegions_Filename)
                ReplaceCommasInQuotations(infoLines)
                italianRegionRecords = New cITARegionsRecords(infoLines, startDate)
            End If

            If System.IO.File.Exists(Csv_ItaProvinces_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_ItaProvinces_Filename)
                ReplaceCommasInQuotations(infoLines)
                italianProvincesRecords = New cITAProvincesRecords(infoLines)
            End If

            If System.IO.File.Exists(Csv_World_Confirmed_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_World_Confirmed_Filename)
                ReplaceCommasInQuotations(infoLines)
                worldRecords.SetConfirmed(infoLines, startDate)
            End If
            If System.IO.File.Exists(Csv_World_Deaths_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_World_Deaths_Filename)
                ReplaceCommasInQuotations(infoLines)
                worldRecords.SetDeaths(infoLines, startDate)
            End If
            If System.IO.File.Exists(Csv_World_Recovered_Filename) Then
                Dim infoLines() As String = System.IO.File.ReadAllLines(Csv_World_Recovered_Filename)
                ReplaceCommasInQuotations(infoLines)
                worldRecords.SetRecovered(infoLines, startDate)
            End If
            worldRecords.SetFatalityRates()

            europeanRecords = New cWorldRecords(worldRecords)
            europeanRecords.RemoveNonEuropeanEntries()
            'Already normalized if needed, as they've been cloned after the normalization

            FillListBoxes()

            DataLoaded = True
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            If reEnableWhenDone Then
                EnableControls(True)
            End If
        End Try
    End Function
    Private Sub FillListBoxes()

        lstRegions.SuspendLayout()
        lstRegions.Items.Clear()
        Dim allNames As System.Collections.Generic.List(Of cCountryListboxItem) = worldRecords.GetListBoxItems(myDisplayInfo.ActiveWorldData, True)
        lstRegions.Items.AddRange(allNames.ToArray)
        lstRegions.ResumeLayout(True)
        If lstRegions.Items.Count > 0 Then
            lstRegions.SelectedIndex = 0
        End If

        lstRegionsEurope.SuspendLayout()
        lstRegionsEurope.Items.Clear()
        Dim allEUNames As System.Collections.Generic.List(Of cCountryListboxItem) = europeanRecords.GetListBoxItems(myDisplayInfo.ActiveEUData, True)
        lstRegionsEurope.Items.AddRange(allEUNames.ToArray)
        lstRegionsEurope.ResumeLayout(True)
        If lstRegionsEurope.Items.Count > 0 Then
            lstRegionsEurope.SelectedIndex = 0
        End If

        lstItaRegions.SuspendLayout()
        lstItaRegions.Items.Clear()
        Dim allITARegionNames As System.Collections.Generic.List(Of cCountryListboxItem) = italianRegionRecords.GetListBoxItems(myDisplayInfo.ActiveItalianData)
        lstItaRegions.Items.AddRange(allITARegionNames.ToArray)
        lstItaRegions.ResumeLayout(True)
        If lstItaRegions.Items.Count > 0 Then
            lstItaRegions.SelectedIndex = 0
        End If

        lstItaProvinces.SuspendLayout()
        lstItaProvinces.Items.Clear()
        Dim allITAProvinceNames As System.Collections.Generic.List(Of cCountryListboxItem) = italianProvincesRecords.GetListBoxItems(cDisplayInfo.enItalianValueType.Total_Cases)
        lstItaProvinces.Items.AddRange(allITAProvinceNames.ToArray)
        lstItaProvinces.ResumeLayout(True)
        If lstItaProvinces.Items.Count > 0 Then
            lstItaProvinces.SelectedIndex = 0
        End If
    End Sub
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
                cbChartItemITA.SelectedIndex = cDisplayInfo.enItalianValueType.Deaths
            Catch ex2 As Exception
            End Try


            cbChartItemWorld.Items.Clear()
            Dim myWorldTypes() As cDisplayInfo.enWorldValueType = System.Enum.GetValues(GetType(cDisplayInfo.enWorldValueType))
            For iCounter As Integer = 0 To myWorldTypes.Length - 1
                cbChartItemWorld.Items.Add(myWorldTypes(iCounter).ToString)
            Next
            Try
                cbChartItemWorld.SelectedIndex = cDisplayInfo.enWorldValueType.Deaths
            Catch ex2 As Exception
            End Try

            cbChartItemEurope.Items.Clear()
            Dim myEUTypes() As cDisplayInfo.enWorldValueType = System.Enum.GetValues(GetType(cDisplayInfo.enWorldValueType))
            For iCounter As Integer = 0 To myEUTypes.Length - 1
                cbChartItemEurope.Items.Add(myEUTypes(iCounter).ToString)
            Next
            Try
                cbChartItemEurope.SelectedIndex = cDisplayInfo.enWorldValueType.Deaths
            Catch ex2 As Exception
            End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private myFirstActivationDone As Boolean = False
    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            If Not myFirstActivationDone Then
                dtpStartDate.MaxDate = Now
                Dim oldestDate As Date = Now
                oldestDate = oldestDate.AddDays(-31)
                dtpStartDate.Value = oldestDate

                chkNormalize.Text = cPopulation.PerMillionString
                Me.Text = Application.ProductName + " V" + Application.ProductVersion
                RestoreWindowPosition(Me)
                Me.Show()
                Application.DoEvents()
                Me.Refresh()
                FillCombos()
                Application.DoEvents()
                Me.Refresh()
                labSnapshot.Text = ""
                GetLatestInfo()
                SkipDataRefresh = False
                LoadInfoFromLocalFiles()
                UpdateAndRefresh(False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            myFirstActivationDone = True
        End Try
    End Sub
    Private Sub chkDaily_CheckedChanged(sender As Object, e As EventArgs) Handles chkDaily.CheckedChanged
        If SkipDataRefresh Then Return
        myDisplayInfo.DailyIncrements = chkDaily.Checked
        FillListBoxes()
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
            NormalizeToPopulation = chkNormalize.Checked
            UseMovingAverage = chkMA.Checked

            If myDisplayInfo.ShowITA Then
                pnlWorld.Visible = False
                pnlEurope.Visible = False
                pnlIta.Visible = True
                If Not pnlLeft.Controls.Contains(pnlIta) Then
                    pnlLeft.Controls.Add(pnlIta)
                    pnlIta.Dock = DockStyle.Fill
                    pnlIta.Visible = True
                    pnlIta.BringToFront()
                End If
                Select Case myDisplayInfo.ActiveArea
                    Case cDisplayInfo.enActiveArea.ITA
                        dtpStartDate.Enabled = True
                        labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianRecords.LastDate.ToLongDateString
                        mnMainItem.Text = mnITAFull.Text
                        mnMainItem.Image = mnITAFull.Image
                        lstItaProvinces.Visible = False
                        lstItaRegions.Visible = False
                        labSelectionHintITA.Visible = False
                    Case cDisplayInfo.enActiveArea.ITA_Provinces
                        dtpStartDate.Enabled = False
                        labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianProvincesRecords.LastDate.ToLongDateString
                        mnMainItem.Text = mnITAProvinces.Text
                        mnMainItem.Image = mnITAProvinces.Image
                        lstItaRegions.Visible = False
                        lstItaProvinces.Visible = True
                        lstItaProvinces.BringToFront()
                        labSelectionHintITA.Visible = True
                    Case cDisplayInfo.enActiveArea.ITA_Regions
                        dtpStartDate.Enabled = True
                        labLastUpdateInfo.Text = "Last update:" + vbCrLf + italianRegionRecords.LastDate.ToLongDateString
                        mnMainItem.Text = mnITARegions.Text
                        mnMainItem.Image = mnITARegions.Image
                        lstItaProvinces.Visible = False
                        lstItaRegions.Visible = True
                        lstItaRegions.BringToFront()
                        labSelectionHintITA.Visible = True
                End Select
            ElseIf myDisplayInfo.ShowWorld Then
                dtpStartDate.Enabled = True
                pnlIta.Visible = False
                pnlEurope.Visible = False
                pnlWorld.Visible = True
                mnMainItem.Text = mnWorld.Text
                mnMainItem.Image = mnWorld.Image
                labLastUpdateInfo.Text = "Last update:" + vbCrLf + worldRecords.LastDate.ToLongDateString
                If Not pnlLeft.Controls.Contains(pnlWorld) Then
                    pnlLeft.Controls.Add(pnlWorld)
                    pnlWorld.Dock = DockStyle.Fill
                    pnlWorld.BringToFront()
                End If
                lstRegions.BringToFront()
            ElseIf myDisplayInfo.ShowEurope Then
                dtpStartDate.Enabled = True
                pnlIta.Visible = False
                pnlWorld.Visible = False
                pnlEurope.Visible = True
                mnMainItem.Text = mnEurope.Text
                mnMainItem.Image = mnEurope.Image
                labLastUpdateInfo.Text = "Last update:" + vbCrLf + europeanRecords.LastDate.ToLongDateString
                If Not pnlLeft.Controls.Contains(pnlEurope) Then
                    pnlLeft.Controls.Add(pnlEurope)
                    pnlEurope.Dock = DockStyle.Fill
                    pnlEurope.BringToFront()
                End If
                lstRegionsEurope.BringToFront()
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

            myDisplayInfo.ActiveEURegions.Clear()
            If lstRegionsEurope.SelectedItems.Count > 0 Then
                For iCounter As Integer = 0 To lstRegionsEurope.SelectedItems.Count - 1
                    myDisplayInfo.ActiveEURegions.Add(lstRegionsEurope.SelectedItems(iCounter))
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
            ElseIf myDisplayInfo.ShowEurope Then
                btShowMap.Enabled = True
                If myDisplayInfo.ActiveEURegions.Count = 1 Then
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
                    If cbChartItemITA.SelectedIndex <> cDisplayInfo.enItalianValueType.Total_Cases Then
                        cbChartItemITA.SelectedIndex = cDisplayInfo.enItalianValueType.Total_Cases
                    End If
                Case cDisplayInfo.enActiveArea.ITA_Regions
                    btShowMap.Enabled = True
                    cbChartItemITA.Enabled = True
            End Select


            If myDisplayInfo.ShowWorld Then
                If myDisplayInfo.ActiveWorldRegions.Count = 1 Then
                    EnableEstimateSection((Not myDisplayInfo.DailyIncrements) AndAlso (Not NormalizeToPopulation))
                Else
                    EnableEstimateSection(False)
                End If
            ElseIf myDisplayInfo.ShowEurope Then
                If myDisplayInfo.ActiveEURegions.Count = 1 Then
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
    Private SkipDataRefresh As Boolean = True
    Private Sub UpdateAndRefresh(ByVal showEstimate As Boolean)
        Try
            UpdateDisplayInfo(showEstimate)
            If SkipDataRefresh Then Return
            RefreshVisualization(Chart1, italianRecords, italianRegionRecords, italianProvincesRecords, worldRecords, USRecords, UKRecords, europeanRecords, myDisplayInfo)

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
                ElseIf myDisplayInfo.ShowEurope Then
                    distErr = NormalDistribution.EstimateDiffFromValues(GetPlotPointsEURegionFirst(europeanRecords, myDisplayInfo))
                End If
                labErrorValue.Text = CStr(CInt(distErr))
            Else
                labErrorValue.Text = "-"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub cbChartItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbChartItemITA.SelectedIndexChanged, cbChartItemWorld.SelectedIndexChanged, cbChartItemEurope.SelectedIndexChanged
        If sender Is cbChartItemWorld Then
            myDisplayInfo.ActiveWorldData = cbChartItemWorld.SelectedIndex
        ElseIf sender Is cbChartItemEurope Then
            myDisplayInfo.ActiveEUData = cbChartItemEurope.SelectedIndex
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
                Case cDisplayInfo.enActiveArea.World
                    startInfo.FileName = "https://gisanddata.maps.arcgis.com/apps/opsdashboard/index.html#/bda7594740fd40299423467b48e9ecf6"
            End Select
            startInfo.UseShellExecute = True
            startInfo.WindowStyle = ProcessWindowStyle.Normal
            Process.Start(startInfo)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
                ToolTip1.SetToolTip(Chart1, startDate.ToLongDateString + ", " + CStr(CInt(yValue)))
            Else
                ToolTip1.SetToolTip(Chart1, "")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btEstimate_Click(sender As Object, e As EventArgs) Handles btEstimate.Click
        Try
            EnableControls(False)

            Dim NormalDistribution As New cNormalDist
            NormalDistribution.ExpectedMax = myDisplayInfo.EstimatedFinalValue
            NormalDistribution.Sigma = myDisplayInfo.EstimatedSigma
            NormalDistribution.CurPos100 = myDisplayInfo.EstimatedCurPos100
            Me.UseWaitCursor = True
            Me.Refresh()
            Application.DoEvents()
            If myDisplayInfo.ShowWorld Then
                NormalDistribution.FindBestEstimate(GetPlotPointsWorldRegionFirst(worldRecords, myDisplayInfo), AddressOf AddInfoText)
            ElseIf myDisplayInfo.ShowEurope Then
                NormalDistribution.FindBestEstimate(GetPlotPointsEURegionFirst(europeanRecords, myDisplayInfo), AddressOf AddInfoText)
            ElseIf myDisplayInfo.ShowITA Then
                Select Case myDisplayInfo.ActiveArea
                    Case cDisplayInfo.enActiveArea.ITA
                        NormalDistribution.FindBestEstimate(GetPlotPointsIta(italianRecords, myDisplayInfo), AddressOf AddInfoText)
                    Case cDisplayInfo.enActiveArea.ITA_Provinces
                        NormalDistribution.FindBestEstimate(GetPlotPointsItaProvinceFirst(italianProvincesRecords, myDisplayInfo), AddressOf AddInfoText)
                    Case cDisplayInfo.enActiveArea.ITA_Regions
                        NormalDistribution.FindBestEstimate(GetPlotPointsItaRegionFirst(italianRegionRecords, myDisplayInfo), AddressOf AddInfoText)
                End Select
            End If
            udEstimatedMax.Value = NormalDistribution.ExpectedMax
            udSigma.Value = NormalDistribution.Sigma
            udCurPosition.Value = NormalDistribution.CurPos100
            UpdateAndRefresh(True)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.UseWaitCursor = False
            EnableControls(True)
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
            ElseIf myDisplayInfo.ShowEurope Then
                If lstRegionsEurope.SelectedIndices.Count > 1 Then
                    If daysShift <> 0 Then
                        'First one will not be affected
                        For lCounter As Integer = 1 To lstRegionsEurope.SelectedIndices.Count - 1
                            europeanRecords.ShiftDays(myDisplayInfo.ActiveEUData, lstRegionsEurope.Items(lstRegionsEurope.SelectedIndices(lCounter)), daysShift)
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
            BuildHeatMap(worldRecords, USRecords, italianRegionRecords, europeanRecords, myDisplayInfo)
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
        If SkipDataRefresh Then Return
        NormalizeToPopulation = chkNormalize.Checked
        FillListBoxes()
        UpdateAndRefresh(False)
    End Sub
    Private Sub chkMA_CheckedChanged(sender As Object, e As EventArgs) Handles chkMA.CheckedChanged
        If SkipDataRefresh Then Return
        UseMovingAverage = chkMA.Checked
        UpdateAndRefresh(False)
    End Sub

    Private Sub mnEurope_Click(sender As Object, e As EventArgs) Handles mnEurope.Click
        myDisplayInfo.ActiveArea = cDisplayInfo.enActiveArea.Europe
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
    Private Sub lstRegionsUS_SelectedIndexChanged(sender As Object, e As EventArgs)
        UpdateAndRefresh(False)
    End Sub
    Private Sub lstRegionsEurope_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRegionsEurope.SelectedIndexChanged
        UpdateAndRefresh(False)
    End Sub
    Private Sub CheckForUpdatedDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatedDataToolStripMenuItem.Click
        GetLatestInfo()
        UpdateAndRefresh(False)
    End Sub
    Private Sub lstRegionsUK_SelectedIndexChanged(sender As Object, e As EventArgs)
        UpdateAndRefresh(False)
    End Sub
    Private Sub dtpStartDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpStartDate.ValueChanged
        If Not myFirstActivationDone Then
            Return
        Else
            LoadInfoFromLocalFiles()
        End If
    End Sub
End Class
