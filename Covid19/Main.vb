Module Main
    Public DataLoaded As Boolean = False
    Private myPopulation As cPopulation = Nothing
    Public Function Population() As cPopulation
        If myPopulation Is Nothing Then
            myPopulation = New cPopulation
        End If
        Return myPopulation
    End Function
    Public NormalizeToPopulation As Boolean = False

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
    Public Function Csv_US_Deaths_Filename() As String
        Return RootFolder() + "US_deaths.csv"
    End Function
    Public Function Csv_US_Confirmed_Filename() As String
        Return RootFolder() + "US_Confirmed.csv"
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
    Public Sub ReplaceCommasInQuotations(ByVal csvLines() As String)
        For lCounter As Integer = 0 To csvLines.Count - 1
            If csvLines(lCounter).Contains("""") Then
                Dim thisLine As String = csvLines(lCounter)
                Do While True
                    If thisLine.Contains("""") Then
                        Dim FirstQuote_Pos As Integer = thisLine.IndexOf("""")
                        Dim SecondQuotePos As Integer = thisLine.IndexOf("""", FirstQuote_Pos + 1)
                        If (FirstQuote_Pos >= 0) AndAlso (SecondQuotePos > FirstQuote_Pos) Then
                            Dim QuotedText As String = thisLine.Substring(FirstQuote_Pos + 1, SecondQuotePos - FirstQuote_Pos - 1)
                            QuotedText = QuotedText.Replace(" ", "")
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
            Dim dailyValsList As New List(Of List(Of cDailyValue))
            Dim prevValue As Integer = 0
            Dim curValue As Integer = 0
            Dim dailyVals As List(Of cDailyValue) = ItaProvincesRecords.GetDailyValues(cDisplayInfo.enItalianValueType.Total_Cases, displayInfo.ActiveITAProvinces(0))
            For iCounter As Integer = 0 To dailyVals.Count - 1
                curValue = dailyVals(iCounter).RecordValue
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        Return retVal
    End Function
    Public Function GetPlotPointsItaRegionFirst(ByVal ItaRegionRecords As cITARegionsRecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If displayInfo.ActiveITARegions.Count > 0 Then
            Dim dailyValsList As New List(Of List(Of cDailyValue))
            Dim prevValue As Integer = 0
            Dim curValue As Integer = 0
            Dim dailyVals As List(Of cDailyValue) = ItaRegionRecords.GetDailyValues(displayInfo.ActiveItalianData, displayInfo.ActiveITARegions(0))
            For iCounter As Integer = 0 To dailyVals.Count - 1
                curValue = dailyVals(iCounter).RecordValue
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        Return retVal
    End Function
    Public Function GetPlotPointsWorldRegionFirst(ByVal worldRecords As cWorldRecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If displayInfo.ActiveWorldRegions.Count > 0 Then
            Dim dailyValsList As New List(Of List(Of cDailyValue))
            Dim prevValue As Integer = 0
            Dim curValue As Integer = 0
            Dim dailyVals As List(Of cDailyValue) = worldRecords.GetDailyValues(displayInfo.ActiveWorldData, displayInfo.ActiveWorldRegions(0))
            For iCounter As Integer = 0 To dailyVals.Count - 1
                curValue = dailyVals(iCounter).RecordValue
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        Return retVal
    End Function
    Public Function GetPlotPointsUSRegionFirst(ByVal usRecords As cWorldRecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If displayInfo.ActiveUSRegions.Count > 0 Then
            Dim dailyValsList As New List(Of List(Of cDailyValue))
            Dim prevValue As Integer = 0
            Dim curValue As Integer = 0
            Dim dailyVals As List(Of cDailyValue) = usRecords.GetDailyValues(displayInfo.ActiveUSData, displayInfo.ActiveUSRegions(0))
            For iCounter As Integer = 0 To dailyVals.Count - 1
                curValue = dailyVals(iCounter).RecordValue
                retVal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                If displayInfo.DailyIncrements Then
                    prevValue = curValue
                End If
            Next
        End If
        Return retVal
    End Function
    Public Function GetPlotPointsIta(ByVal ItaRecords As cITARecords, ByVal displayInfo As cDisplayInfo) As List(Of Tuple(Of Date, Double))
        Dim prevValue As Integer = 0
        Dim curValue As Integer = 0
        Dim retVal As New List(Of Tuple(Of Date, Double))

        Dim normalizer As Double = 1
        If NormalizeToPopulation Then
            normalizer = 10000.0 / cPopulation.ITATotalPopulation
        End If

        For iCounter As Integer = 0 To ItaRecords.Count - 1
            Select Case displayInfo.ActiveItalianData
                Case cDisplayInfo.enItalianValueType.Deaths
                    curValue = ItaRecords(iCounter).deceduti
                Case cDisplayInfo.enItalianValueType.Recovered
                    curValue = ItaRecords(iCounter).dimessi_guariti
                Case cDisplayInfo.enItalianValueType.Self_Isolating
                    curValue = ItaRecords(iCounter).isolamento_domiciliare
                Case cDisplayInfo.enItalianValueType.New_Positives
                    curValue = ItaRecords(iCounter).nuovi_positivi
                Case cDisplayInfo.enItalianValueType.Hospitalized_with_Sypmtoms
                    curValue = ItaRecords(iCounter).ricoverati_con_sintomi
                Case cDisplayInfo.enItalianValueType.Tests
                    curValue = ItaRecords(iCounter).tamponi
                Case cDisplayInfo.enItalianValueType.Intensive_Care
                    curValue = ItaRecords(iCounter).terapia_intensiva
                Case cDisplayInfo.enItalianValueType.Current_Positives
                    curValue = ItaRecords(iCounter).totale_positivi
                Case cDisplayInfo.enItalianValueType.Current_Positives_Variation
                    curValue = ItaRecords(iCounter).variazione_totale_positivi
                Case cDisplayInfo.enItalianValueType.Total_Cases
                    curValue = ItaRecords(iCounter).totale_casi
                Case cDisplayInfo.enItalianValueType.Total_Hospitalized
                    curValue = ItaRecords(iCounter).totale_ospedalizzati
            End Select
            retVal.Add(New Tuple(Of Date, Double)(ItaRecords(iCounter).data, (curValue - prevValue) * normalizer))
            If displayInfo.DailyIncrements Then
                prevValue = curValue
            End If
        Next
        Return retVal
    End Function
    Public Sub RefreshVisualization(ByVal aChart As DataVisualization.Charting.Chart, ByVal ItaRecords As cITARecords, ByVal italianRegionRecords As cITARegionsRecords, ByVal italianProvincesRecords As cITAProvincesRecords, ByVal worldRecords As cWorldRecords, ByVal USRecords As cWorldRecords, ByVal displayInfo As cDisplayInfo)
        If Not DataLoaded Then Return
        Try
            aChart.Series.Clear()
            aChart.ResetAutoValues()

            Dim pointsITA As New List(Of Tuple(Of Date, Double))
            If displayInfo.ShowITA Then
                pointsITA = GetPlotPointsIta(ItaRecords, displayInfo)
            End If

            Dim pointsITARegionsList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Regions Then
                Dim dailyValsList As New List(Of List(Of cDailyValue))
                For rCounter As Integer = 0 To displayInfo.ActiveITARegions.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsGlobal As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As List(Of cDailyValue) = italianRegionRecords.GetDailyValues(displayInfo.ActiveItalianData, displayInfo.ActiveITARegions(rCounter))
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        curValue = dailyVals(iCounter).RecordValue
                        pointsGlobal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsITARegionsList.Add(pointsGlobal)
                Next

                'Series need to be aligned
                If pointsITARegionsList.Count > 0 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsITARegionsList.Count - 1
                        AlignSeries(pointsITARegionsList(0), pointsITARegionsList(pCounter))
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsITARegionsList.Count - 1
                        AlignSeries(pointsITARegionsList(0), pointsITARegionsList(pCounter))
                    Next

                    For pCounter As Integer = 0 To pointsITARegionsList.Count - 1
                        AlignSeries(pointsITARegionsList(pCounter), pointsITA)
                    Next

                End If
            End If

            Dim pointsITAProvincesList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                Dim dailyValsList As New List(Of List(Of cDailyValue))
                For rCounter As Integer = 0 To displayInfo.ActiveITAProvinces.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsGlobal As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As List(Of cDailyValue) = italianProvincesRecords.GetDailyValues(cDisplayInfo.enItalianValueType.Total_Cases, displayInfo.ActiveITAProvinces(rCounter))
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        curValue = dailyVals(iCounter).RecordValue
                        pointsGlobal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsITAProvincesList.Add(pointsGlobal)
                Next

                'Series need to be aligned
                If pointsITAProvincesList.Count > 0 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsITAProvincesList.Count - 1
                        AlignSeries(pointsITAProvincesList(0), pointsITAProvincesList(pCounter))
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsITAProvincesList.Count - 1
                        AlignSeries(pointsITAProvincesList(0), pointsITAProvincesList(pCounter))
                    Next

                    For pCounter As Integer = 0 To pointsITAProvincesList.Count - 1
                        AlignSeries(pointsITAProvincesList(pCounter), pointsITA)
                    Next

                End If
            End If

            Dim pointsGlobalList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ShowWorld Then
                Dim dailyValsList As New List(Of List(Of cDailyValue))
                For rCounter As Integer = 0 To displayInfo.ActiveWorldRegions.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsGlobal As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As List(Of cDailyValue) = worldRecords.GetDailyValues(displayInfo.ActiveWorldData, displayInfo.ActiveWorldRegions(rCounter))
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        curValue = dailyVals(iCounter).RecordValue
                        pointsGlobal.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsGlobalList.Add(pointsGlobal)
                Next

                'Series need to be aligned
                If pointsGlobalList.Count > 1 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsGlobalList.Count - 1
                        AlignSeries(pointsGlobalList(0), pointsGlobalList(pCounter))
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsGlobalList.Count - 1
                        AlignSeries(pointsGlobalList(0), pointsGlobalList(pCounter))
                    Next
                End If
            End If

            Dim pointsUSList As New List(Of List(Of Tuple(Of Date, Double)))
            If displayInfo.ShowUS Then
                Dim dailyValsList As New List(Of List(Of cDailyValue))
                For rCounter As Integer = 0 To displayInfo.ActiveUSRegions.Count - 1
                    Dim prevValue As Double = 0
                    Dim curValue As Double = 0
                    Dim pointsUS As New List(Of Tuple(Of Date, Double))
                    Dim dailyVals As List(Of cDailyValue) = USRecords.GetDailyValues(displayInfo.ActiveUSData, displayInfo.ActiveUSRegions(rCounter))
                    For iCounter As Integer = 0 To dailyVals.Count - 1
                        curValue = dailyVals(iCounter).RecordValue
                        pointsUS.Add(New Tuple(Of Date, Double)(dailyVals(iCounter).RecordDate, curValue - prevValue))
                        If displayInfo.DailyIncrements Then
                            prevValue = curValue
                        End If
                    Next
                    pointsUSList.Add(pointsUS)
                Next

                'Series need to be aligned
                If pointsUSList.Count > 1 Then
                    'Align first to all others
                    For pCounter As Integer = 1 To pointsUSList.Count - 1
                        AlignSeries(pointsUSList(0), pointsUSList(pCounter))
                    Next
                    'And all others to first
                    For pCounter As Integer = 1 To pointsUSList.Count - 1
                        AlignSeries(pointsUSList(0), pointsUSList(pCounter))
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
                ElseIf displayInfo.ShowUS Then
                    For dCounter As Integer = 0 To pointsUSList(0).Count - 1
                        allDates.Add(pointsUSList(0).Item(dCounter).Item1)
                    Next
                End If
                pointsNormal = NormalDistribution.GetPlotValues(allDates)
            End If

            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA Then
                Dim dataSeriesITA As New System.Windows.Forms.DataVisualization.Charting.Series
                dataSeriesITA.Color = Color.DarkRed
                dataSeriesITA.IsVisibleInLegend = True
                dataSeriesITA.IsXValueIndexed = True
                dataSeriesITA.ChartType = DataVisualization.Charting.SeriesChartType.Column
                aChart.Series.Add(dataSeriesITA)
                Dim max As Integer = 0
                If pointsITA.Count > 0 Then
                    ChartStartingDate = pointsITA(0).Item1
                End If
                For iCounter As Integer = 0 To pointsITA.Count - 1

                    If pointsITA(iCounter).Item2 > max Then
                        max = pointsITA(iCounter).Item2
                    End If
                    dataSeriesITA.Points.AddXY(pointsITA(iCounter).Item1, pointsITA(iCounter).Item2)
                Next
                dataSeriesITA.Name = CStr(max) + " " + displayInfo.ActiveItalianData.ToString + vbCrLf + "(Italia-Protezione Civile)"
            End If

            If displayInfo.ShowWorld Then
                Dim StartDateInitialized As Boolean = False
                For pCounter As Integer = 0 To pointsGlobalList.Count - 1
                    Dim dataSeriesGlobal As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesGlobal.Name = displayInfo.ActiveWorldData.ToString + vbCrLf + " (" + displayInfo.ActiveWorldRegions(pCounter).ToString + ")"
                    '                    dataSeriesGlobal.Color = Color.Green
                    dataSeriesGlobal.IsVisibleInLegend = True
                    dataSeriesGlobal.BorderWidth = 3
                    dataSeriesGlobal.IsXValueIndexed = True
                    dataSeriesGlobal.ChartType = DataVisualization.Charting.SeriesChartType.Column
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

            If displayInfo.ShowUS Then
                Dim StartDateInitialized As Boolean = False
                For pCounter As Integer = 0 To pointsUSList.Count - 1
                    Dim dataSeriesUS As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesUS.Name = displayInfo.ActiveUSData.ToString + vbCrLf + " (" + displayInfo.ActiveUSRegions(pCounter).ToString + ")"
                    '                    dataSeriesGlobal.Color = Color.Green
                    dataSeriesUS.IsVisibleInLegend = True
                    dataSeriesUS.BorderWidth = 3
                    dataSeriesUS.IsXValueIndexed = True
                    dataSeriesUS.ChartType = DataVisualization.Charting.SeriesChartType.Column
                    aChart.Series.Add(dataSeriesUS)
                    For iCounter As Integer = 0 To pointsUSList(pCounter).Count - 1
                        If Not StartDateInitialized Then
                            StartDateInitialized = True
                            ChartStartingDate = pointsUSList(pCounter).Item(0).Item1
                        Else
                            If pointsUSList(pCounter).Item(iCounter).Item1 < ChartStartingDate Then
                                ChartStartingDate = pointsUSList(pCounter).Item(iCounter).Item1
                            End If
                        End If
                        dataSeriesUS.Points.AddXY(pointsUSList(pCounter).Item(iCounter).Item1, pointsUSList(pCounter).Item(iCounter).Item2)
                    Next
                Next
            End If


            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Regions Then
                Dim StartDateInitialized As Boolean = False
                For pCounter As Integer = 0 To pointsITARegionsList.Count - 1
                    Dim dataSeriesItaRegions As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesItaRegions.Name = displayInfo.ActiveItalianData.ToString + vbCrLf + displayInfo.ActiveITARegions(pCounter).ToString
                    dataSeriesItaRegions.IsVisibleInLegend = True
                    dataSeriesItaRegions.BorderWidth = 3
                    dataSeriesItaRegions.IsXValueIndexed = True
                    dataSeriesItaRegions.ChartType = DataVisualization.Charting.SeriesChartType.Column
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
                    Next
                Next
            End If

            If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                Dim StartDateInitialized As Boolean = False
                For pCounter As Integer = 0 To pointsITAProvincesList.Count - 1
                    Dim dataSeriesItaProvinces As New System.Windows.Forms.DataVisualization.Charting.Series
                    dataSeriesItaProvinces.Name = cDisplayInfo.enItalianValueType.Total_Cases.ToString + vbCrLf + displayInfo.ActiveITAProvinces(pCounter).ToString
                    dataSeriesItaProvinces.IsVisibleInLegend = True
                    dataSeriesItaProvinces.BorderWidth = 3
                    dataSeriesItaProvinces.IsXValueIndexed = True
                    dataSeriesItaProvinces.ChartType = DataVisualization.Charting.SeriesChartType.Column
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
                    Next
                Next
            End If


            If displayInfo.ShowEstimate Then
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
    Private Sub AlignSeries(ByRef series1 As List(Of Tuple(Of Date, Double)), ByRef series2 As List(Of Tuple(Of Date, Double)))
        If series1.Count = 0 Then
            'Just copy the second one into the first
            For iCounter As Integer = 0 To series2.Count - 1
                series1.Add(New Tuple(Of Date, Double)(series2(iCounter).Item1, 0))
            Next
            Return
        End If

        If series2.Count = 0 Then
            'Just copy the first one into the second
            For iCounter As Integer = 0 To series1.Count - 1
                series2.Add(New Tuple(Of Date, Double)(series1(iCounter).Item1, 0))
            Next
            Return
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
                series1.Add(New Tuple(Of Date, Double)(thisDate, 0))
            End If

            dateFound = False
            For counter2 As Integer = 0 To series2.Count - 1
                If series2(counter2).Item1 = thisDate Then
                    dateFound = True
                    Exit For
                End If
            Next
            If Not dateFound Then
                series2.Add(New Tuple(Of Date, Double)(thisDate, 0))
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

    Public Sub BuildHeatMap(ByVal WorldRecords As cWorldRecords, ByVal USRecords As cWorldRecords, ByVal itaRegionsRecords As cITARegionsRecords, ByVal displayInfo As cDisplayInfo)

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

        Dim every10KText As String = ""
        If NormalizeToPopulation Then
            every10KText = " (Every 10,000)"
        End If

        Dim insertPos As Integer = -1
        For lCounter As Integer = 0 To HtmlLines.Count - 1
            'If HtmlLines(lCounter).Contains("Country") AndAlso HtmlLines(lCounter).Contains("Deaths") Then
            If HtmlLines(lCounter).Contains("'Country', '#Value") Then
                If displayInfo.ShowWorld Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("Country", "Country")
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#Value", displayInfo.ActiveWorldData.ToString)
                ElseIf displayInfo.ShowUS Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("Country", "Country")
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#Value", displayInfo.ActiveUSData.ToString)
                Else
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("Country", "Province")
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#Value", displayInfo.ActiveItalianData.ToString)
                End If
                insertPos = lCounter + 1
            ElseIf HtmlLines(lCounter).Contains("#HeaderParagraphTitle#") Then
                If displayInfo.ShowWorld Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphTitle#", displayInfo.ActiveWorldData.ToString + every10KText)
                ElseIf displayInfo.ShowUS Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphTitle#", displayInfo.ActiveUSData.ToString + every10KText)
                Else
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphTitle#", displayInfo.ActiveItalianData.ToString + every10KText)
                End If
            ElseIf HtmlLines(lCounter).Contains("#HeaderParagraphText#") Then
                If displayInfo.ShowWorld Then
                    HtmlLines(lCounter) = HtmlLines(lCounter).Replace("#HeaderParagraphText#", "Countries with less than 100,000 people are not displayed")
                ElseIf displayInfo.ShowUS Then
                    HtmlLines.RemoveAt(lCounter)
                Else
                    HtmlLines.RemoveAt(lCounter)
                End If
            ElseIf HtmlLines(lCounter).Contains("var options = {};") Then
                If displayInfo.ShowITA Then
                    HtmlLines.Insert(lCounter + 1, "		options['region'] = 'IT'")
                    HtmlLines.Insert(lCounter + 1, "		options['resolution'] = 'provinces'")
                ElseIf displayInfo.ShowUS Then
                    HtmlLines.Insert(lCounter + 1, "		options['region'] = 'US'")
                    HtmlLines.Insert(lCounter + 1, "		options['resolution'] = 'provinces'")
                End If
            End If
        Next
        If insertPos > 0 Then
            Dim countryTuples As New List(Of Tuple(Of String, Double))
            Dim countryNames As New List(Of String)
            Dim targetList As List(Of cCountryValues) = Nothing
            If displayInfo.ShowWorld Then
                Select Case displayInfo.ActiveWorldData
                    Case cDisplayInfo.enWorldValueType.Confirmed
                        targetList = WorldRecords.Confirmed
                    Case cDisplayInfo.enWorldValueType.Deaths
                        targetList = WorldRecords.Deaths
                    Case cDisplayInfo.enWorldValueType.Recovered
                        targetList = WorldRecords.Recovered
                End Select
            ElseIf displayInfo.ShowUS Then
                Select Case displayInfo.ActiveUSData
                    Case cDisplayInfo.enWorldValueType.Confirmed
                        targetList = USRecords.Confirmed
                    Case cDisplayInfo.enWorldValueType.Deaths
                        targetList = USRecords.Deaths
                    Case cDisplayInfo.enWorldValueType.Recovered
                        targetList = USRecords.Recovered
                End Select
            Else
                targetList = itaRegionsRecords.GetCountryValuesFromType(displayInfo.ActiveItalianData)
            End If

            Dim SumTrentoBolzano As Double = -1
            For cCounter As Integer = 0 To targetList.Count - 1
                Dim regionName As String = targetList(cCounter).Country_Region
                If displayInfo.ShowITA Then
                    regionName = targetList(cCounter).Province_State
                ElseIf displayInfo.ShowUS Then
                    regionName = targetList(cCounter).Country_Region
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
                Dim maxVal As Double = targetList(cCounter).DailyValues(targetList(cCounter).DailyValues.Count - 1).RecordValue
                Dim totalPopulation As Double = 0
                If displayInfo.ShowITA Then
                    If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                        totalPopulation = Population.GetITAProvincePopulation(regionName)
                    Else
                        totalPopulation = Population.GetITARegionPopulation(regionName)
                    End If
                ElseIf displayInfo.ShowUS Then
                    totalPopulation = Population.GetUSPopulation(regionName)
                ElseIf displayInfo.ShowWorld Then
                    totalPopulation = Population.GetWorldCountryPopulation(regionName)
                End If

                If (displayInfo.ShowWorld AndAlso (totalPopulation < 100000)) Then
                    'Skip this one
                Else
                    If totalPopulation = 0 Then
                        maxVal = 0
                    End If
                    If countryNames.Contains(regionName) Then
                        'This has already been inserted
                        For iCounter As Integer = 0 To countryTuples.Count - 1
                            If countryTuples(iCounter).Item1.ToUpper = (regionName.ToUpper) Then
                                'This is it
                                Dim prevVal As Double = countryTuples(iCounter).Item2
                                maxVal = maxVal + prevVal
                                countryTuples(iCounter) = New Tuple(Of String, Double)(regionName, maxVal)
                            End If
                        Next
                    Else
                        'Not yet inserted
                        countryNames.Add(regionName)
                        countryTuples.Add(New Tuple(Of String, Double)(regionName, maxVal))
                    End If
                End If
            Next

            'Adjust the values according to the population and adds the lines
            Dim countryLines As New List(Of String)
            For iCounter As Integer = 0 To countryTuples.Count - 1
                Dim thisCountry As String = countryTuples(iCounter).Item1
                Dim thisVal As Double = countryTuples(iCounter).Item2
                Dim totalPopulation As Double = 0
                If displayInfo.ShowITA Then
                    If displayInfo.ActiveArea = cDisplayInfo.enActiveArea.ITA_Provinces Then
                        totalPopulation = Population.GetITAProvincePopulation(thisCountry)
                    Else
                        totalPopulation = Population.GetITARegionPopulation(thisCountry)
                    End If
                ElseIf displayInfo.ShowUS Then
                    totalPopulation = Population.GetUSPopulation(thisCountry)
                ElseIf displayInfo.ShowWorld Then
                    totalPopulation = Population.GetWorldCountryPopulation(thisCountry)
                End If

                If NormalizeToPopulation Then
                    If (totalPopulation > 0) Then
                        thisVal = (thisVal * 10000.0) / totalPopulation
                    Else
                        thisVal = 0
                    End If
                End If

                Dim thisCountryLine As String = "          ['" + thisCountry + "'," + CStr((thisVal)) + "," + CStr((totalPopulation)) + "],"
                countryLines.Add(thisCountryLine)
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



End Module
