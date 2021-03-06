﻿Module Main
    Public DataLoaded As Boolean = False
    Private myPopulation As cPopulation = Nothing
    Public Function Population() As cPopulation
        If myPopulation Is Nothing Then
            myPopulation = New cPopulation
        End If
        Return myPopulation
    End Function
    Public NormalizeToPopulation As Boolean = False
    Public UseMovingAverage As Boolean = True
    Public Function IniFileName() As String
        Return RootFolder() + "Covid19.ini"
    End Function

    Public ReadOnly Property MapHtml As String
        Get
            Return RootFolder() + "MapLoader.html"
        End Get
    End Property

    Public Function RootFolder() As String
        Try
            Dim retVal As String = Application.StartupPath + "\Covid19\"
            If System.IO.Directory.Exists(retVal) Then
                System.IO.Directory.CreateDirectory(retVal)
            End If
            Return retVal
        Catch ex As Exception
            Return Application.StartupPath + "\"
        End Try
    End Function
    Public Function Csv_Ita_Filename() As String
        Return RootFolder() + "Ita_full.csv"
    End Function
    Public Function Csv_ItaRegions_Filename() As String
        Return RootFolder() + "Ita_regions.csv"
    End Function
    Public Function Csv_ItaProvinces_Filename() As String
        Return RootFolder() + "Ita_provinces.csv"
    End Function
    Public Function Csv_World_Deaths_Filename() As String
        Return RootFolder() + "World_deaths.csv"
    End Function
    Public Function Csv_World_Confirmed_Filename() As String
        Return RootFolder() + "World_Confirmed.csv"
    End Function
    Public Function Csv_World_Recovered_Filename() As String
        Return RootFolder() + "World_Recovered.csv"
    End Function
    Public Function Csv_TmpPath() As String
        Return RootFolder() + "tmp.csv"
    End Function
    Public Sub ReplaceCommasInQuotations(ByVal csvLines As System.Collections.Generic.List(Of String), Optional ByVal RemoveSpaces As Boolean = True)
        Dim toArr() As String = csvLines.ToArray
        ReplaceCommasInQuotations(toArr, RemoveSpaces)
        csvLines.Clear()
        csvLines.AddRange(toArr)
    End Sub
    Public Function MakeComparable(ByVal aString As String) As String
        aString = aString.Replace(" ", "")
        aString = aString.Replace(",", "")
        aString = aString.Replace("-", "")
        Return aString.ToUpper.Trim
    End Function
    Public Sub ReplaceCommasInQuotations(ByVal csvLines() As String, Optional ByVal RemoveSpaces As Boolean = True)
        For lCounter As Integer = 0 To csvLines.Count - 1
            If csvLines(lCounter).Contains("""") Then
                Dim thisLine As String = csvLines(lCounter)
                Do While True
                    If thisLine.Contains("""") Then
                        Dim FirstQuote_Pos As Integer = thisLine.IndexOf("""")
                        Dim SecondQuotePos As Integer = thisLine.IndexOf("""", FirstQuote_Pos + 1)
                        If (FirstQuote_Pos >= 0) AndAlso (SecondQuotePos > FirstQuote_Pos) Then
                            Dim QuotedText As String = thisLine.Substring(FirstQuote_Pos + 1, SecondQuotePos - FirstQuote_Pos - 1)
                            If RemoveSpaces Then
                                QuotedText = QuotedText.Replace(" ", "")
                            End If
                            QuotedText = QuotedText.Replace(",", "-")
                            thisLine = thisLine.Substring(0, FirstQuote_Pos) + QuotedText + thisLine.Substring(SecondQuotePos + 1)
                        Else
                            Exit Do
                        End If
                    Else
                        Exit Do
                    End If
                Loop
                csvLines(lCounter) = thisLine
            End If
        Next
    End Sub

    Public ChartStartingDate As New Date

    Public Function GetPlotPointsItaProvinceFirst(ByVal ItaProvincesRecords As cITAProvincesRecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If displayInfo.ActiveITAProvinces.Count > 0 Then
            Dim dailyValsList As New List(Of cDailyValues)
            Dim prevValue As Double = 0
            Dim curValue As Double = 0
            Dim dailyVals As cDailyValues = ItaProvincesRecords.GetDailyValues(cDisplayInfo.enItalianValueType.Total_Cases, displayInfo.ActiveITAProvinces(0))
            For iCounter As Integer = 0 To dailyVals.Count - 1
                If NormalizeToPopulation Then
                    curValue = dailyVals(iCounter).RecordPercentValue
                Else
                    curValue = dailyVals(iCounter).RecordAbsoluteValue
                End If
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        retVal = CalcMovingAverage(retVal)
        Return retVal
    End Function
    Public Function GetPlotPointsItaRegionFirst(ByVal ItaRegionRecords As cITARegionsRecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If displayInfo.ActiveITARegions.Count > 0 Then
            Dim dailyValsList As New List(Of cDailyValues)
            Dim prevValue As Double = 0
            Dim curValue As Double = 0
            Dim dailyVals As cDailyValues = ItaRegionRecords.GetDailyValues(displayInfo.ActiveItalianData, displayInfo.ActiveITARegions(0))
            For iCounter As Integer = 0 To dailyVals.Count - 1
                If NormalizeToPopulation Then
                    curValue = dailyVals(iCounter).RecordPercentValue
                Else
                    curValue = dailyVals(iCounter).RecordAbsoluteValue
                End If
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        retVal = CalcMovingAverage(retVal)
        Return retVal
    End Function
    Public Function GetPlotPointsEURegionFirst(ByVal EURecords As cWorldRecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If displayInfo.ActiveEURegions.Count > 0 Then
            Dim dailyValsList As New List(Of cDailyValues)
            Dim prevValue As Double = 0
            Dim curValue As Double = 0
            Dim dailyVals As cDailyValues = EURecords.GetDailyValues(displayInfo.ActiveEUData, displayInfo.ActiveEURegions(0), False)
            For iCounter As Integer = 0 To dailyVals.Count - 1
                If NormalizeToPopulation Then
                    curValue = dailyVals(iCounter).RecordPercentValue
                Else
                    curValue = dailyVals(iCounter).RecordAbsoluteValue
                End If
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        retVal = CalcMovingAverage(retVal)
        Return retVal
    End Function
    Public Function GetPlotPointsWorldRegionFirst(ByVal worldRecords As cWorldRecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If displayInfo.ActiveWorldRegions.Count > 0 Then
            Dim dailyValsList As New List(Of cDailyValues)
            Dim prevValue As Double = 0
            Dim curValue As Double = 0
            Dim dailyVals As cDailyValues = worldRecords.GetDailyValues(displayInfo.ActiveWorldData, displayInfo.ActiveWorldRegions(0), False)
            For iCounter As Integer = 0 To dailyVals.Count - 1
                If NormalizeToPopulation Then
                    curValue = dailyVals(iCounter).RecordPercentValue
                Else
                    curValue = dailyVals(iCounter).RecordAbsoluteValue
                End If
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        retVal = CalcMovingAverage(retVal)
        Return retVal
    End Function
    Public Function GetPlotPointsIta(ByVal ItaRecords As cITARecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim prevValue As Double = 0
        Dim curValue As Double = 0
        Dim retVal As New List(Of Tuple(Of Date, Double))

        If NormalizeToPopulation Then
            For iCounter As Integer = 0 To ItaRecords.Count - 1
                Select Case displayInfo.ActiveItalianData
                    Case cDisplayInfo.enItalianValueType.Deaths
                        curValue = ItaRecords(iCounter).deceduti.Item2
                    Case cDisplayInfo.enItalianValueType.Cases_FromSuspectDiagnostics
                        curValue = ItaRecords(iCounter).casi_da_sospetto_diagnostico.Item2
                    Case cDisplayInfo.enItalianValueType.Cases_FromScreening
                        curValue = ItaRecords(iCounter).casi_da_screening.Item2
                    Case cDisplayInfo.enItalianValueType.Recovered
                        curValue = ItaRecords(iCounter).dimessi_guariti.Item2
                    Case cDisplayInfo.enItalianValueType.Self_Isolating
                        curValue = ItaRecords(iCounter).isolamento_domiciliare.Item2
                    Case cDisplayInfo.enItalianValueType.New_Positives
                        curValue = ItaRecords(iCounter).nuovi_positivi.Item2
                    Case cDisplayInfo.enItalianValueType.Hospitalized_with_Sypmtoms
                        curValue = ItaRecords(iCounter).ricoverati_con_sintomi.Item2
                    Case cDisplayInfo.enItalianValueType.Tests
                        curValue = ItaRecords(iCounter).tamponi.Item2
                    Case cDisplayInfo.enItalianValueType.Intensive_Care
                        curValue = ItaRecords(iCounter).terapia_intensiva.Item2
                    Case cDisplayInfo.enItalianValueType.Current_Positives
                        curValue = ItaRecords(iCounter).totale_positivi.Item2
                    Case cDisplayInfo.enItalianValueType.Current_Positives_Variation
                        curValue = ItaRecords(iCounter).variazione_totale_positivi.Item2
                    Case cDisplayInfo.enItalianValueType.Total_Cases
                        curValue = ItaRecords(iCounter).totale_casi.Item2
                    Case cDisplayInfo.enItalianValueType.Total_Hospitalized
                        curValue = ItaRecords(iCounter).totale_ospedalizzati.Item2
                End Select
                retVal.Add(New Tuple(Of Date, Double)(ItaRecords(iCounter).data, (curValue - prevValue)))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        Else
            For iCounter As Integer = 0 To ItaRecords.Count - 1
                Select Case displayInfo.ActiveItalianData
                    Case cDisplayInfo.enItalianValueType.Cases_FromSuspectDiagnostics
                        curValue = ItaRecords(iCounter).casi_da_sospetto_diagnostico.Item1
                    Case cDisplayInfo.enItalianValueType.Cases_FromScreening
                        curValue = ItaRecords(iCounter).casi_da_screening.Item1
                    Case cDisplayInfo.enItalianValueType.Deaths
                        curValue = ItaRecords(iCounter).deceduti.Item1
                    Case cDisplayInfo.enItalianValueType.Recovered
                        curValue = ItaRecords(iCounter).dimessi_guariti.Item1
                    Case cDisplayInfo.enItalianValueType.Self_Isolating
                        curValue = ItaRecords(iCounter).isolamento_domiciliare.Item1
                    Case cDisplayInfo.enItalianValueType.New_Positives
                        curValue = ItaRecords(iCounter).nuovi_positivi.Item1
                    Case cDisplayInfo.enItalianValueType.Hospitalized_with_Sypmtoms
                        curValue = ItaRecords(iCounter).ricoverati_con_sintomi.Item1
                    Case cDisplayInfo.enItalianValueType.Tests
                        curValue = ItaRecords(iCounter).tamponi.Item1
                    Case cDisplayInfo.enItalianValueType.Intensive_Care
                        curValue = ItaRecords(iCounter).terapia_intensiva.Item1
                    Case cDisplayInfo.enItalianValueType.Current_Positives
                        curValue = ItaRecords(iCounter).totale_positivi.Item1
                    Case cDisplayInfo.enItalianValueType.Current_Positives_Variation
                        curValue = ItaRecords(iCounter).variazione_totale_positivi.Item1
                    Case cDisplayInfo.enItalianValueType.Total_Cases
                        curValue = ItaRecords(iCounter).totale_casi.Item1
                    Case cDisplayInfo.enItalianValueType.Total_Hospitalized
                        curValue = ItaRecords(iCounter).totale_ospedalizzati.Item1
                End Select
                retVal.Add(New Tuple(Of Date, Double)(ItaRecords(iCounter).data, (curValue - prevValue)))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        retVal = CalcMovingAverage(retVal)
        Return retVal
    End Function
    Public Sub RefreshVisualization(ByVal aChart As DataVisualization.Charting.Chart, ByVal ItaRecords As cITARecords, ByVal italianRegionRecords As cITARegionsRecords, ByVal italianProvincesRecords As cITAProvincesRecords, ByVal worldRecords As cWorldRecords, ByVal USRecords As cWorldRecords, ByVal UKRecords As cWorldRecords, ByVal EURecords As cWorldRecords, ByVal displayInfo As cDisplayInfo)
        If Not DataLoaded Then Return

        Try
            Dim FillWithExtremeValues As Boolean = Not displayInfo.DailyIncrements
            Dim myChartType As DataVisualization.Charting.SeriesChartType = DataVisualization.Charting.SeriesChartType.Column
            Dim isolationStartIndex As Integer = -1

            aChart.Annotations.Clear()
            aChart.Series.Clear()
            aChart.ResetAutoValues()

            Dim pointsITA As New List(Of Tuple(Of Date, Double))
            If displayInfo.ShowITA Then
                pointsITA = GetPlotPointsIta(ItaRecords, displayInfo)
            End If

            Dim pointsITARegionsList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Regions Then
                Dim dailyValsList As New List(Of cDailyValues)
                For rCounter As Integer = 0 To displayInfo.ActiveITARegions.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsGlobal As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As cDailyValues = italianRegionRecords.GetDailyValues(displayInfo.ActiveItalianData, displayInfo.ActiveITARegions(rCounter))
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        If NormalizeToPopulation Then
                            curValue = dailyVals(iCounter).RecordPercentValue
                        Else
                            curValue = dailyVals(iCounter).RecordAbsoluteValue
                        End If
                        pointsGlobal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsITARegionsList.Add(pointsGlobal)
                Next
                pointsITARegionsList = CalcMovingAverage(pointsITARegionsList)

                'Series need to be aligned
                If pointsITARegionsList.Count > 0 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsITARegionsList.Count - 1
                        AlignSeries(pointsITARegionsList(0), pointsITARegionsList(pCounter), FillWithExtremeValues)
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsITARegionsList.Count - 1
                        AlignSeries(pointsITARegionsList(0), pointsITARegionsList(pCounter), FillWithExtremeValues)
                    Next

                    For pCounter As Integer = 0 To pointsITARegionsList.Count - 1
                        AlignSeries(pointsITARegionsList(pCounter), pointsITA, FillWithExtremeValues)
                    Next

                End If
            End If

            Dim pointsITAProvincesList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                Dim dailyValsList As New List(Of cDailyValues)
                For rCounter As Integer = 0 To displayInfo.ActiveITAProvinces.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsGlobal As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As cDailyValues = italianProvincesRecords.GetDailyValues(cDisplayInfo.enItalianValueType.Total_Cases, displayInfo.ActiveITAProvinces(rCounter))
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        If NormalizeToPopulation Then
                            curValue = dailyVals(iCounter).RecordPercentValue
                        Else
                            curValue = dailyVals(iCounter).RecordAbsoluteValue
                        End If
                        pointsGlobal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsITAProvincesList.Add(pointsGlobal)
                Next
                pointsITAProvincesList = CalcMovingAverage(pointsITAProvincesList)

                'Series need to be aligned
                If pointsITAProvincesList.Count > 0 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsITAProvincesList.Count - 1
                        AlignSeries(pointsITAProvincesList(0), pointsITAProvincesList(pCounter), FillWithExtremeValues)
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsITAProvincesList.Count - 1
                        AlignSeries(pointsITAProvincesList(0), pointsITAProvincesList(pCounter), FillWithExtremeValues)
                    Next

                    For pCounter As Integer = 0 To pointsITAProvincesList.Count - 1
                        AlignSeries(pointsITAProvincesList(pCounter), pointsITA, FillWithExtremeValues)
                    Next

                End If
            End If

            Dim pointsGlobalList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ShowWorld Then
                Dim dailyValsList As New List(Of cDailyValues)
                For rCounter As Integer = 0 To displayInfo.ActiveWorldRegions.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsGlobal As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As cDailyValues = worldRecords.GetDailyValues(displayInfo.ActiveWorldData, displayInfo.ActiveWorldRegions(rCounter), False)
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        If NormalizeToPopulation Then
                            curValue = dailyVals(iCounter).RecordPercentValue
                        Else
                            curValue = dailyVals(iCounter).RecordAbsoluteValue
                        End If
                        pointsGlobal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsGlobalList.Add(pointsGlobal)
                Next
                pointsGlobalList = CalcMovingAverage(pointsGlobalList)

                'Series need to be aligned
                If pointsGlobalList.Count > 1 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsGlobalList.Count - 1
                        AlignSeries(pointsGlobalList(0), pointsGlobalList(pCounter), FillWithExtremeValues)
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsGlobalList.Count - 1
                        AlignSeries(pointsGlobalList(0), pointsGlobalList(pCounter), FillWithExtremeValues)
                    Next
                End If
            End If

            Dim pointsEUList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ShowEurope Then
                Dim dailyValsList As New List(Of cDailyValues)
                For rCounter As Integer = 0 To displayInfo.ActiveEURegions.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsEU As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As cDailyValues = EURecords.GetDailyValues(displayInfo.ActiveEUData, displayInfo.ActiveEURegions(rCounter), False)
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        If NormalizeToPopulation Then
                            curValue = dailyVals(iCounter).RecordPercentValue
                        Else
                            curValue = dailyVals(iCounter).RecordAbsoluteValue
                        End If
                        pointsEU.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsEUList.Add(pointsEU)
                Next
                pointsEUList = CalcMovingAverage(pointsEUList)

                'Series need to be aligned
                If pointsEUList.Count > 1 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsEUList.Count - 1
                        AlignSeries(pointsEUList(0), pointsEUList(pCounter), FillWithExtremeValues)
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsEUList.Count - 1
                        AlignSeries(pointsEUList(0), pointsEUList(pCounter), FillWithExtremeValues)
                    Next
                End If
            End If

            Dim pointsNormal As New List(Of Tuple(Of Date, Double))
            If displayInfo.ShowEstimate Then
                Dim NormalDistribution As New cNormalDist
                NormalDistribution.ExpectedMax = displayInfo.EstimatedFinalValue
                NormalDistribution.Sigma = displayInfo.EstimatedSigma
                NormalDistribution.CurPos100 = displayInfo.EstimatedCurPos100
                Dim allDates As New System.Collections.Generic.List(Of Date)
                If displayInfo.ShowITA Then
                    For dCounter As Integer = 0 To pointsITA.Count - 1
                        allDates.Add(pointsITA(dCounter).Item1)
                    Next
                ElseIf displayInfo.ShowWorld Then
                    For dCounter As Integer = 0 To pointsGlobalList(0).Count - 1
                        allDates.Add(pointsGlobalList(0).Item(dCounter).Item1)
                    Next
                ElseIf displayInfo.ShowEurope Then
                    For dCounter As Integer = 0 To pointsEUList(0).Count - 1
                        allDates.Add(pointsEUList(0).Item(dCounter).Item1)
                    Next
                End If
                pointsNormal = NormalDistribution.GetPlotValues(allDates)
            End If

            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA Then
                Dim dataSeriesITA As New System.Windows.Forms.DataVisualization.Charting.Series
                dataSeriesITA.IsVisibleInLegend = True
                dataSeriesITA.IsXValueIndexed = True
                dataSeriesITA.ChartType = myChartType
                dataSeriesITA.BorderWidth = 4
                aChart.Series.Add(dataSeriesITA)
                Dim max As Double = 0
                If pointsITA.Count > 0 Then
                    ChartStartingDate = pointsITA(0).Item1
                End If

                For iCounter As Integer = 0 To pointsITA.Count - 1
                    If pointsITA(iCounter).Item2 > max Then
                        max = pointsITA(iCounter).Item2
                    End If
                    dataSeriesITA.Points.AddXY(pointsITA(iCounter).Item1, pointsITA(iCounter).Item2)

                    If pointsITA(iCounter).Item1 = cITARecords.RestrictionStartDate_ITA Then
                        If isolationStartIndex = -1 Then
                            isolationStartIndex = iCounter
                            AddIsolationAnnotation(aChart, dataSeriesITA, isolationStartIndex)
                        End If
                    End If
                Next
                dataSeriesITA.Name = strCutDecimals(max, 2) + " " + displayInfo.ActiveItalianData.ToString + vbCrLf + "(Italia-Protezione Civile)"
            End If

            If displayInfo.ShowWorld Then
                If pointsGlobalList.Count > 1 Then
                    myChartType = DataVisualization.Charting.SeriesChartType.Line
                End If
                Dim StartDateInitialized As Boolean = False
                For pCounter As Integer = 0 To pointsGlobalList.Count - 1
                    Dim dataSeriesGlobal As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesGlobal.Name = displayInfo.ActiveWorldData.ToString + vbCrLf + " (" + displayInfo.ActiveWorldRegions(pCounter).ToString + ")"
                    '                    dataSeriesGlobal.Color = Color.Green
                    dataSeriesGlobal.IsVisibleInLegend = True
                    dataSeriesGlobal.BorderWidth = 4
                    dataSeriesGlobal.IsXValueIndexed = True
                    dataSeriesGlobal.ChartType = myChartType
                    aChart.Series.Add(dataSeriesGlobal)
                    For iCounter As Integer = 0 To pointsGlobalList(pCounter).Count - 1
                        If Not StartDateInitialized Then
                            StartDateInitialized = True
                            ChartStartingDate = pointsGlobalList(pCounter).Item(0).Item1
                        Else
                            If pointsGlobalList(pCounter).Item(iCounter).Item1 < ChartStartingDate Then
                                ChartStartingDate = pointsGlobalList(pCounter).Item(iCounter).Item1
                            End If
                        End If
                        dataSeriesGlobal.Points.AddXY(pointsGlobalList(pCounter).Item(iCounter).Item1, pointsGlobalList(pCounter).Item(iCounter).Item2)
                    Next
                Next
            End If

            If displayInfo.ShowEurope Then
                If pointsEUList.Count > 1 Then
                    myChartType = DataVisualization.Charting.SeriesChartType.Line
                End If
                Dim StartDateInitialized As Boolean = False
                For pCounter As Integer = 0 To pointsEUList.Count - 1
                    Dim dataSeriesEU As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesEU.Name = displayInfo.ActiveEUData.ToString + vbCrLf + " (" + displayInfo.ActiveEURegions(pCounter).ToString + ")"
                    dataSeriesEU.IsVisibleInLegend = True
                    dataSeriesEU.BorderWidth = 4
                    dataSeriesEU.IsXValueIndexed = True
                    dataSeriesEU.ChartType = myChartType
                    aChart.Series.Add(dataSeriesEU)
                    For iCounter As Integer = 0 To pointsEUList(pCounter).Count - 1
                        If Not StartDateInitialized Then
                            StartDateInitialized = True
                            ChartStartingDate = pointsEUList(pCounter).Item(0).Item1
                        Else
                            If pointsEUList(pCounter).Item(iCounter).Item1 < ChartStartingDate Then
                                ChartStartingDate = pointsEUList(pCounter).Item(iCounter).Item1
                            End If
                        End If
                        dataSeriesEU.Points.AddXY(pointsEUList(pCounter).Item(iCounter).Item1, pointsEUList(pCounter).Item(iCounter).Item2)
                    Next
                Next
            End If

            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Regions Then
                Dim StartDateInitialized As Boolean = False
                If pointsITARegionsList.Count > 1 Then
                    myChartType = DataVisualization.Charting.SeriesChartType.Line
                End If
                For pCounter As Integer = 0 To pointsITARegionsList.Count - 1
                    Dim dataSeriesItaRegions As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesItaRegions.Name = displayInfo.ActiveItalianData.ToString + vbCrLf + displayInfo.ActiveITARegions(pCounter).ToString
                    dataSeriesItaRegions.IsVisibleInLegend = True
                    dataSeriesItaRegions.BorderWidth = 4
                    dataSeriesItaRegions.IsXValueIndexed = True
                    dataSeriesItaRegions.ChartType = myChartType
                    aChart.Series.Add(dataSeriesItaRegions)
                    For iCounter As Integer = 0 To pointsITARegionsList(pCounter).Count - 1
                        If Not StartDateInitialized Then
                            StartDateInitialized = True
                            ChartStartingDate = pointsITARegionsList(pCounter).Item(0).Item1
                        Else
                            If pointsITARegionsList(pCounter).Item(iCounter).Item1 < ChartStartingDate Then
                                ChartStartingDate = pointsITARegionsList(pCounter).Item(iCounter).Item1
                            End If
                        End If
                        dataSeriesItaRegions.Points.AddXY(pointsITARegionsList(pCounter).Item(iCounter).Item1, pointsITARegionsList(pCounter).Item(iCounter).Item2)
                        If pointsITARegionsList(pCounter).Item(iCounter).Item1 = cITARecords.RestrictionStartDate_ITA Then
                            If isolationStartIndex = -1 Then
                                isolationStartIndex = iCounter
                                AddIsolationAnnotation(aChart, dataSeriesItaRegions, isolationStartIndex)
                            End If
                        End If
                    Next
                Next
            End If

            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                Dim StartDateInitialized As Boolean = False
                If pointsITAProvincesList.Count > 1 Then
                    myChartType = DataVisualization.Charting.SeriesChartType.Line
                End If
                For pCounter As Integer = 0 To pointsITAProvincesList.Count - 1
                    Dim dataSeriesItaProvinces As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesItaProvinces.Name = cDisplayInfo.enItalianValueType.Total_Cases.ToString + vbCrLf + displayInfo.ActiveITAProvinces(pCounter).ToString
                    dataSeriesItaProvinces.IsVisibleInLegend = True
                    dataSeriesItaProvinces.BorderWidth = 4
                    dataSeriesItaProvinces.IsXValueIndexed = True
                    dataSeriesItaProvinces.ChartType = myChartType
                    aChart.Series.Add(dataSeriesItaProvinces)
                    For iCounter As Integer = 0 To pointsITAProvincesList(pCounter).Count - 1
                        If Not StartDateInitialized Then
                            StartDateInitialized = True
                            ChartStartingDate = pointsITAProvincesList(pCounter).Item(0).Item1
                        Else
                            If pointsITAProvincesList(pCounter).Item(iCounter).Item1 < ChartStartingDate Then
                                ChartStartingDate = pointsITAProvincesList(pCounter).Item(iCounter).Item1
                            End If
                        End If
                        dataSeriesItaProvinces.Points.AddXY(pointsITAProvincesList(pCounter).Item(iCounter).Item1, pointsITAProvincesList(pCounter).Item(iCounter).Item2)

                        If pointsITAProvincesList(pCounter).Item(iCounter).Item1 = cITARecords.RestrictionStartDate_ITA Then
                            If isolationStartIndex = -1 Then
                                isolationStartIndex = iCounter
                                AddIsolationAnnotation(aChart, dataSeriesItaProvinces, isolationStartIndex)
                            End If
                        End If
                    Next
                Next
            End If

            If displayInfo.ShowEstimate Then
                myChartType = DataVisualization.Charting.SeriesChartType.Line
                Dim dataSeriesNorm As New System.Windows.Forms.DataVisualization.Charting.Series
                dataSeriesNorm.Name = "Cumulative Normal"
                dataSeriesNorm.Color = Color.Red
                dataSeriesNorm.BorderWidth = 4
                dataSeriesNorm.IsVisibleInLegend = True
                dataSeriesNorm.IsXValueIndexed = True
                dataSeriesNorm.ChartType = DataVisualization.Charting.SeriesChartType.Line
                aChart.Series.Add(dataSeriesNorm)
                For iCounter As Integer = 0 To pointsNormal.Count - 1
                    dataSeriesNorm.Points.AddXY(pointsNormal(iCounter).Item1, pointsNormal(iCounter).Item2)
                Next
            End If

            aChart.Invalidate()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AddIsolationAnnotation(ByVal aChart As DataVisualization.Charting.Chart, ByVal aSeries As DataVisualization.Charting.Series, ByVal isolationStartIndex As Integer)
        Dim la As New DataVisualization.Charting.VerticalLineAnnotation
        la.LineColor = Color.Red
        la.LineWidth = 2
        la.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dash
        la.IsInfinitive = True
        la.AnchorDataPoint = aSeries.Points(isolationStartIndex)
        la.SmartLabelStyle.AllowOutsidePlotArea = False
        la.ToolTip = "11 March 2020, social distancing introduced"
        aChart.Annotations.Add(la)
    End Sub

    Private Function GetSeriesFirstValue(ByRef aSeries As List(Of Tuple(Of Date, Double))) As Double
        If aSeries.Count > 0 Then
            Return aSeries(0).Item2
        Else
            Return 0
        End If
    End Function

    Private Function GetSeriesLastValue(ByRef aSeries As List(Of Tuple(Of Date, Double))) As Double
        If aSeries.Count > 0 Then
            Return aSeries(aSeries.Count - 1).Item2
        Else
            Return 0
        End If
    End Function
    Private Sub AlignSeries(ByRef series1 As List(Of Tuple(Of Date, Double)), ByRef series2 As List(Of Tuple(Of Date, Double)), ByVal FillWithExtremeValues As Boolean)

        If series1.Count = 0 Then
            'Just copy the second one into the first
            For iCounter As Integer = 0 To series2.Count - 1
                If FillWithExtremeValues Then
                    series1.Add(New Tuple(Of Date, Double)(series2(iCounter).Item1, series2(iCounter).Item2))
                Else
                    series1.Add(New Tuple(Of Date, Double)(series2(iCounter).Item1, 0))
                End If
            Next
            Return
        End If

        If series2.Count = 0 Then
            'Just copy the first one into the second
            For iCounter As Integer = 0 To series1.Count - 1
                If FillWithExtremeValues Then
                    series2.Add(New Tuple(Of Date, Double)(series1(iCounter).Item1, series1(iCounter).Item2))
                Else
                    series2.Add(New Tuple(Of Date, Double)(series1(iCounter).Item1, 0))
                End If
            Next
            Return
        End If

        Dim first1 As Double = GetSeriesFirstValue(series1)
        Dim last1 As Double = GetSeriesLastValue(series1)
        Dim first2 As Double = GetSeriesFirstValue(series2)
        Dim last2 As Double = GetSeriesLastValue(series2)

        If Not FillWithExtremeValues Then
            first1 = 0
            last1 = 0
            first2 = 0
            last2 = 0
        End If

        Dim mergedDates As New List(Of Date)
        For iCounter As Integer = 0 To series1.Count - 1
            mergedDates.Add(series1(iCounter).Item1)
        Next
        For iCounter As Integer = 0 To series2.Count - 1
            If mergedDates.Contains(series2(iCounter).Item1) Then
                'Already included
            Else
                mergedDates.Add(series2(iCounter).Item1)
            End If
        Next

        mergedDates.Sort()

        Dim UseLastValue1 As Boolean = False 'Will turn to true after something has been found
        Dim UseLastValue2 As Boolean = False 'Will turn to true after something has been found
        For dCounter As Integer = 0 To mergedDates.Count - 1
            Dim thisDate As Date = mergedDates(dCounter)
            Dim dateFound As Boolean = False
            For counter1 As Integer = 0 To series1.Count - 1
                If series1(counter1).Item1 = thisDate Then
                    dateFound = True
                    Exit For
                End If
            Next

            If Not dateFound Then
                If UseLastValue1 Then
                    series1.Add(New Tuple(Of Date, Double)(thisDate, last1))
                Else
                    series1.Add(New Tuple(Of Date, Double)(thisDate, first1))
                End If
            Else
                UseLastValue1 = True
            End If

            dateFound = False
            For counter2 As Integer = 0 To series2.Count - 1
                If series2(counter2).Item1 = thisDate Then
                    dateFound = True
                    Exit For
                End If
            Next
            If Not dateFound Then
                If UseLastValue2 Then
                    series2.Add(New Tuple(Of Date, Double)(thisDate, last2))
                Else
                    series2.Add(New Tuple(Of Date, Double)(thisDate, first2))
                End If
            Else
                UseLastValue2 = True
            End If
        Next

        'Now both series contain all dates. The order is probably a mess so we reorder them
        series1 = series1.OrderBy(Function(x) x.Item1).ToList()
        series2 = series2.OrderBy(Function(x) x.Item1).ToList()


    End Sub
    Public Sub RestoreWindowPosition(ByVal frm As System.Windows.Forms.Form)
        '-------------------------------------------------------------------------
        'Autore   :    Luca De Nardi       24/06/96 17.53.51
        '-------------------------------------------------------------------------
        Try
            Dim strSection As String = frm.Name
            Dim strTrash As String = Space(255)
            Dim intTop, intLeft, intHeight, intWidth As Integer
            Dim WStatus As Short
            Dim SkipSizing As Boolean = True
            If (frm.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable) Or (frm.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow) Then
                SkipSizing = False
            End If

            'Location ...
            intLeft = cIniFileManager.ReadDouble(strSection, "Left", 0, IniFileName)
            intTop = cIniFileManager.ReadDouble(strSection, "Top", 0, IniFileName)


            If Screen.AllScreens.Count > 1 Then
                Dim relocationNeeded As Boolean = True
                For scCounter As Integer = 0 To Screen.AllScreens.Count - 1
                    Dim leftChangeNeeded As Boolean = True
                    Dim topChangeNeeded As Boolean = True
                    If intLeft >= ((Screen.AllScreens(scCounter).Bounds.Left + Screen.AllScreens(scCounter).Bounds.Width) * 0.97) Then
                        'Not inside
                    Else
                        leftChangeNeeded = False
                    End If
                    If intTop >= ((Screen.AllScreens(scCounter).Bounds.Top + Screen.AllScreens(scCounter).Bounds.Height) * 0.97) Then
                        'Not inside
                    Else
                        topChangeNeeded = False
                    End If
                    If topChangeNeeded Or leftChangeNeeded Then
                        'Nop wait for next
                    Else
                        'We are good here
                        relocationNeeded = False
                        Exit For
                    End If
                Next
                If relocationNeeded Then
                    intLeft = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) * 0.8
                    intTop = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) * 0.8
                End If
            Else
                'Just the primary one
                If intLeft >= ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) * 0.97) Then
                    intLeft = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) * 0.8
                End If
                If intTop >= ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) * 0.97) Then
                    intTop = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) * 0.8
                End If
            End If

            If intLeft < 0 Then intLeft = 0
            If intTop < 0 Then intTop = 0

            'Size
            If Not SkipSizing Then
                intWidth = cIniFileManager.ReadDouble(strSection, "Width", (frm.Width), IniFileName)
                intHeight = cIniFileManager.ReadDouble(strSection, "Height", (frm.Height), IniFileName)
                frm.SetBounds(intLeft, intTop, intWidth, intHeight)
                WStatus = cIniFileManager.ReadDouble(strSection, "Max", 0, IniFileName)
                If WStatus = 1 Then
                    frm.WindowState = System.Windows.Forms.FormWindowState.Maximized
                End If
            Else
                frm.Left = intLeft
                frm.Top = intTop
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SaveWindowPosition(ByVal frm As System.Windows.Forms.Form)
        '-------------------------------------------------------------------------
        ' Autore   :    Luca De Nardi       24/06/96 17.53.51
        '-------------------------------------------------------------------------
        Try
            Dim strSection As String = frm.Name
            If frm.WindowState = System.Windows.Forms.FormWindowState.Maximized Then
                cIniFileManager.WriteString(strSection, "Max", CStr(1), IniFileName)
            ElseIf frm.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
                'Non salvo nulla, altrimenti riparte minimizzato...
            Else
                cIniFileManager.WriteString(strSection, "Max", CStr(0), IniFileName)
                cIniFileManager.WriteString(strSection, "Left", CStr((frm.Left)), IniFileName)
                cIniFileManager.WriteString(strSection, "Top", CStr((frm.Top)), IniFileName)
                If (frm.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable) Or (frm.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow) Then
                    cIniFileManager.WriteString(strSection, "Width", CStr((frm.Width)), IniFileName)
                    cIniFileManager.WriteString(strSection, "Height", CStr((frm.Height)), IniFileName)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub BuildHeatMap(ByVal WorldRecords As cWorldRecords, ByVal USRecords As cWorldRecords, ByVal itaRegionsRecords As cITARegionsRecords, ByVal EURecords As cWorldRecords, ByVal displayInfo As cDisplayInfo)
        'Delete old files
        If System.IO.File.Exists(MapHtml) Then
            System.IO.File.Delete(MapHtml)
        End If

        'Create new ones from template
        Dim thisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Static resourcesNames As String() = thisAssembly.GetManifestResourceNames()
        For Each name As String In resourcesNames
            If (name.EndsWith("MapLoader.html")) Then
                Dim resStream As System.IO.Stream = thisAssembly.GetManifestResourceStream(name)
                Dim resBytes(resStream.Length - 1) As Byte
                resStream.Read(resBytes, 0, resStream.Length)
                Dim fileStream As System.IO.FileStream = Nothing
                If (name.EndsWith("MapLoader.html")) Then
                    fileStream = New System.IO.FileStream(MapHtml, IO.FileMode.Create, IO.FileAccess.Write)
                End If
                fileStream.Write(resBytes, 0, resBytes.Length)
                fileStream.Flush()
                fileStream.Close()
                fileStream.Dispose()
            End If
        Next

        'Create country lines
        Dim HtmlLines As New List(Of String)
        HtmlLines.AddRange(System.IO.File.ReadAllLines(MapHtml))

        Dim every1MText As String = ""
        If NormalizeToPopulation Then
            every1MText = " (" + cPopulation.PerMillionString + ")"
        End If
        Dim tooltipValueDescriptor As String = ""

        Dim insertPos As Integer = -1
        For lCounter As Integer = 0 To HtmlLines.Count - 1
            'If HtmlLines(lCounter).Contains("Country") AndAlso HtmlLines(lCounter).Contains("Deaths") Then
            If HtmlLines(lCounter).Contains("'Country', '#Value") Then
                If displayInfo.ShowWorld Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("Country", "Country")
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#Value", displayInfo.ActiveWorldData.ToString)
                    tooltipValueDescriptor = displayInfo.ActiveWorldData.ToString
                ElseIf displayInfo.ShowEurope Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("Country", "Country")
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#Value", displayInfo.ActiveEUData.ToString)
                    tooltipValueDescriptor = displayInfo.ActiveEUData.ToString
                Else
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("Country", "Province")
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#Value", displayInfo.ActiveItalianData.ToString)
                    tooltipValueDescriptor = displayInfo.ActiveItalianData.ToString
                End If
                insertPos = lCounter + 1
            ElseIf HtmlLines(lCounter).Contains("#HeaderParagraphTitle#") Then
                If displayInfo.ShowWorld Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphTitle#", displayInfo.ActiveWorldData.ToString + every1MText)
                ElseIf displayInfo.ShowEurope Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphTitle#", displayInfo.ActiveEUData.ToString + every1MText)
                Else
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphTitle#", displayInfo.ActiveItalianData.ToString + every1MText)
                End If
            ElseIf HtmlLines(lCounter).Contains("#HeaderParagraphText#") Then
                If displayInfo.ShowWorld Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphText#", "Countries with less than 100K people are not displayed")
                ElseIf displayInfo.ShowEurope Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphText#", "Countries with less than 100K people are not displayed")
                Else
                    HtmlLines.RemoveAt(lCounter)
                    lCounter = lCounter - 1
                End If
            ElseIf HtmlLines(lCounter).Contains("var options = {};") Then
                If displayInfo.ShowITA Then
                    HtmlLines.Insert(lCounter + 1, "		options['region'] = 'IT'")
                    HtmlLines.Insert(lCounter + 1, "		options['resolution'] = 'provinces'")
                ElseIf displayInfo.ShowEurope Then
                    HtmlLines.Insert(lCounter + 1, "		options['region'] = 150")
                End If
            End If
        Next
        If insertPos > 0 Then
            Dim countryItems As New cHeatMapItems
            Dim countryNames As New List(Of String)
            Dim targetList As cObservedDataCollection = Nothing
            If displayInfo.ShowWorld Then
                Select Case displayInfo.ActiveWorldData
                    Case cDisplayInfo.enWorldValueType.Confirmed
                        targetList = WorldRecords.Confirmed
                    Case cDisplayInfo.enWorldValueType.Deaths
                        targetList = WorldRecords.Deaths
                    Case cDisplayInfo.enWorldValueType.Recovered
                        targetList = WorldRecords.Recovered
                    Case cDisplayInfo.enWorldValueType.FatalityRates
                        targetList = WorldRecords.FatalityRates
                End Select
            ElseIf displayInfo.ShowEurope Then
                Select Case displayInfo.ActiveEUData
                    Case cDisplayInfo.enWorldValueType.Confirmed
                        targetList = EURecords.Confirmed
                    Case cDisplayInfo.enWorldValueType.Deaths
                        targetList = EURecords.Deaths
                    Case cDisplayInfo.enWorldValueType.Recovered
                        targetList = EURecords.Recovered
                    Case cDisplayInfo.enWorldValueType.FatalityRates
                        targetList = EURecords.FatalityRates
                End Select
            Else
                targetList = itaRegionsRecords.GetCountryValuesFromType(displayInfo.ActiveItalianData)
            End If

            For cCounter As Integer = 0 To targetList.Count - 1
                Dim regionName As String = targetList(cCounter).CountryOrRegion
                If displayInfo.ShowITA Then
                    regionName = targetList(cCounter).ProvinceOrState
                End If
                If regionName.Contains("'") Then
                    regionName = regionName.Replace("'", "")
                End If
                If displayInfo.ShowWorld Then
                    If regionName = "Korea-South" Then
                        regionName = "South Korea"
                    ElseIf regionName = "Korea-North" Then
                        regionName = "North Korea"
                    End If
                ElseIf displayInfo.ShowITA Then
                    If regionName.Contains("Romagna") Then
                        regionName = "Emilia-Romagna"
                    ElseIf regionName.Contains("Friuli") Then
                        regionName = "Friuli-Venezia Giulia"
                    ElseIf regionName.Contains("Aosta") Then
                        regionName = "IT-23"
                    End If
                    If regionName.Contains("Trento") OrElse regionName.Contains("Bolzano") Then
                        regionName = "Trentino-Alto Adige"
                    End If
                End If

                'Value for this region
                Dim maxAbsVal As Double = targetList(cCounter).DailyValues(targetList(cCounter).DailyValues.Count - 1).RecordAbsoluteValue
                Dim maxPercentVal As Double = targetList(cCounter).DailyValues(targetList(cCounter).DailyValues.Count - 1).RecordPercentValue
                Dim totalPopulation As Double = 0
                If displayInfo.ShowITA Then
                    If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                        totalPopulation = Population.GetITAProvincePopulation(regionName)
                    Else
                        totalPopulation = Population.GetITARegionPopulation(regionName)
                    End If
                ElseIf displayInfo.ShowWorld Then
                    totalPopulation = Population.GetWorldCountryPopulation(regionName)
                ElseIf displayInfo.ShowEurope Then
                    totalPopulation = Population.GetWorldCountryPopulation(regionName)
                End If

                If ((displayInfo.ShowWorld Or displayInfo.ShowEurope) AndAlso (totalPopulation < cPopulation.CountryPopulationThreshold)) Then
                    'Skip this one
                Else
                    If totalPopulation = 0 Then
                        maxAbsVal = 0
                        maxPercentVal = 0
                    End If
                    If countryNames.Contains(regionName) Then
                        'This has already been inserted
                        For iCounter As Integer = 0 To countryItems.Count - 1
                            If countryItems(iCounter).Region.ToUpper = (regionName.ToUpper) Then
                                'This is it
                                Dim prevVal As Double = countryItems(iCounter).AbsValue
                                maxAbsVal = maxAbsVal + prevVal
                                countryItems(iCounter).AbsValue = maxAbsVal
                            End If
                        Next
                    Else
                        'Not yet inserted
                        countryNames.Add(regionName)
                        Dim newItem As New cHeatMapItem
                        newItem.Region = regionName
                        newItem.AbsValue = maxAbsVal
                        countryItems.Add(newItem)
                    End If
                End If
            Next

            'Adjust the values according to the population and adds the lines
            Dim countryLines As New List(Of String)
            For iCounter As Integer = 0 To countryItems.Count - 1
                Dim thisCountry As String = countryItems(iCounter).Region
                Dim thisVal As Double = countryItems(iCounter).AbsValue
                Dim totalPopulation As Double = 0
                If displayInfo.ShowITA Then
                    If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                        totalPopulation = Population.GetITAProvincePopulation(thisCountry)
                    Else
                        totalPopulation = Population.GetITARegionPopulation(thisCountry)
                    End If
                ElseIf displayInfo.ShowWorld Then
                    totalPopulation = Population.GetWorldCountryPopulation(thisCountry)
                ElseIf displayInfo.ShowEurope Then
                    totalPopulation = Population.GetWorldCountryPopulation(thisCountry)
                End If

                If NormalizeToPopulation Then
                    thisVal = countryItems(iCounter).PercentValue(totalPopulation)
                Else
                    thisVal = countryItems(iCounter).AbsValue
                End If

                Dim thisTooltip As String = "'<b>Population:</b> " + ToKMB((totalPopulation)) + "<br>"
                thisTooltip = thisTooltip + tooltipValueDescriptor + ": " + strCutDecimals(countryItems(iCounter).AbsValue, 2)
                thisTooltip = thisTooltip + "<br>" + strCutDecimals(countryItems(iCounter).PercentValue(totalPopulation), 2) + " " + cPopulation.PerMillionString
                thisTooltip = thisTooltip + "'"

                ''Hello World<br>This is <b>Lombardia</b><br><img src="https://www.gstatic.com/onebox/sports/logos/flags/united_kingdom_icon_square.png"/>'


                Dim thisCountryLine As String = "          ['" + thisCountry + "'," + strCutDecimals(thisVal, 2, False) + "," + thisTooltip + "],"
                countryLines.Add(thisCountryLine)
            Next

            'Workaround for GeoChart not displaying Czechia
            For iCounter As Integer = 0 To countryLines.Count - 1
                If countryLines(iCounter).Contains("Czechia") Then
                    countryLines(iCounter) = countryLines(iCounter).Replace("Czechia", "Czech Republic")
                    Exit For
                End If
            Next

            HtmlLines.InsertRange(insertPos, countryLines)
            System.IO.File.Delete(MapHtml)
            System.IO.File.WriteAllLines(MapHtml, HtmlLines)

            Dim startInfo As New ProcessStartInfo
            startInfo.FileName = RootFolder() + "MapLoader.html"
            startInfo.UseShellExecute = True
            startInfo.WindowStyle = ProcessWindowStyle.Normal
            Process.Start(startInfo)
        End If

    End Sub

#Region "Window capture"
    <System.Runtime.InteropServices.DllImport("gdi32.dll")> Private Function BitBlt(ByVal hdc As IntPtr,
                                       ByVal nXDest As Integer,
                                       ByVal nYDest As Integer,
                                       ByVal nWidth As Integer,
                                       ByVal nHeight As Integer,
                                       ByVal hdcSrc As IntPtr,
                                       ByVal nXSrc As Integer,
                                       ByVal nYSrc As Integer,
                                       ByVal dwRop As CopyPixelOperation) As Boolean
    End Function
    Public Function GetWindowImageGrayed(ByVal aForm As System.Windows.Forms.Form) As Bitmap
        Try
            Dim b As New Bitmap(aForm.ClientSize.Width, aForm.ClientSize.Height, Imaging.PixelFormat.Format24bppRgb)
            Using img As Graphics = Graphics.FromImage(b)
                Dim ImageHDC As IntPtr = img.GetHdc
                Using window As Graphics = Graphics.FromHwnd(aForm.Handle)
                    Dim WindowHDC As IntPtr = window.GetHdc
                    BitBlt(ImageHDC, 0, 0, aForm.ClientSize.Width, aForm.ClientSize.Height, WindowHDC, 0, 0, CopyPixelOperation.SourceCopy)
                    window.ReleaseHdc()
                End Using
                img.ReleaseHdc()
            End Using

            Return System.Windows.Forms.ToolStripRenderer.CreateDisabledImage(b)
        Catch ex As Exception
            Return New Bitmap(3, 3)
        End Try
    End Function
#End Region

    Public Function strCutDecimals(ByVal Value As Double, ByVal DesiredDecDigits As Integer, Optional ByVal useKMBFormat As Boolean = True) As String
        Try
            If (Value = Double.NegativeInfinity OrElse Value = Double.PositiveInfinity OrElse Value = Double.MaxValue OrElse Value = Double.MinValue) Then
                Return CStr(Value)
            End If
            DesiredDecDigits = Math.Abs(DesiredDecDigits)
            If DesiredDecDigits > 8 Then
                DesiredDecDigits = 8
            End If
            If useKMBFormat Then
                Return ToKMB(Math.Round(Value, DesiredDecDigits))
            Else
                Return CStr(Math.Round(Value, DesiredDecDigits))
            End If
        Catch ex As Exception
            Return ToKMB(Value)
        End Try
    End Function
    Public Function ToKMB(ByVal num As Double) As String
        If (num > 999999999 OrElse num < -999999999) Then
            Return num.ToString("0,,,.###B")
        ElseIf (num > 999999 OrElse num < -999999) Then
            Return num.ToString("0,,.##M")
        ElseIf (num > 999 OrElse num < -999) Then
            Return num.ToString("0,.#K")
        Else
            Return num.ToString
        End If
    End Function

    Public Function CdblEx(ByVal str As String) As Double
        Try
            If (str Is Nothing) OrElse (str.Length = 0) Then
                Return 0
            End If

            If str.ToUpper = "DOUBLE.MAXVALUE" Then
                Return Double.MaxValue
            End If
            If str.ToUpper = "DOUBLE.MINVALUE" Then
                Return Double.MinValue
            End If

            Dim myParts() As String = str.Split(";")
            If myParts.Length > 0 Then
                str = myParts(0)
            End If

            If System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator = "." Then
                str = str.Replace(",", ".")
            Else
                str = str.Replace(".", ",")
            End If
            Dim LastChar As Char
            Do While True
                If str.Length > 0 Then
                Else
                    Exit Do
                End If
                LastChar = str.Substring(str.Length - 1, 1)
                If Not IsNumeric(LastChar) Then
                    str = str.TrimEnd(LastChar)
                Else
                    Exit Do
                End If
            Loop
            If str.Length > 0 Then
                If str.ToUpper.Contains("E") Then
                    Dim expNotParts() As String = str.ToUpper.Split("E")
                    Dim retVal As Double = Double.Parse(expNotParts(0), System.Globalization.NumberFormatInfo.CurrentInfo)
                    retVal = retVal * 10 ^ (Double.Parse(expNotParts(1), System.Globalization.NumberFormatInfo.CurrentInfo))
                    Return retVal
                Else
                    Return Double.Parse(str, System.Globalization.NumberFormatInfo.CurrentInfo)
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox("DoubleFromString: cannot process string '" + str + "'" + vbCrLf + ex.Message)
            Return 0
        End Try
    End Function
    Private Function CalcMovingAverage_ex(ByVal values As List(Of Double)) As List(Of Double)
        Dim sideItemsCount As Integer = 3 '7 values will be used, i.e. a week.
        Dim retVal As New List(Of Double)
        Dim maItems As New List(Of Double)
        For iCounter As Integer = 0 To values.Count - 1
            maItems.Clear()
            maItems.Add(values(iCounter))
            For shifter As Integer = 1 To sideItemsCount
                If ((iCounter - shifter) >= 0) AndAlso ((iCounter + shifter) <= (values.Count - 1)) Then  'Must be symmetrical
                    maItems.Add(values(iCounter - shifter))
                    maItems.Add(values(iCounter + shifter))
                End If
            Next
            Dim maValue As Double = 0
            If maItems.Count > 0 Then
                For maCounter As Integer = 0 To maItems.Count - 1
                    maValue += maItems(maCounter)
                Next
                maValue = maValue / maItems.Count
            End If
            retVal.Add(maValue)
        Next
        Return retVal
    End Function
    Public Function CalcMovingAverage(ByVal values As List(Of Tuple(Of Date, Double))) As List(Of Tuple(Of Date, Double))
        If Not UseMovingAverage Then Return values

        Dim retVal As New List(Of Tuple(Of Date, Double))

        Dim vals As New List(Of Double)
        For iCounter As Integer = 0 To values.Count - 1
            vals.Add(values(iCounter).Item2)
        Next
        vals = CalcMovingAverage_ex(vals)
        For iCounter As Integer = 0 To values.Count - 1
            retVal.Add(New Tuple(Of Date, Double)(values(iCounter).Item1, vals(iCounter)))
        Next
        Return retVal

    End Function
    Public Function CalcMovingAverage(ByVal values As List(Of List(Of Tuple(Of Date, Double)))) As List(Of List(Of Tuple(Of Date, Double)))
        If Not UseMovingAverage Then Return values

        Dim retval As New List(Of List(Of Tuple(Of Date, Double)))
        For lCounter As Integer = 0 To values.Count - 1
            retval.Add(CalcMovingAverage(values(lCounter)))
        Next
        Return retval
    End Function


End Module
