
Public Class cITARegionsRecords
    'data,stato,codice_regione,denominazione_regione,lat,long
    Public ricoverati_con_sintomi As New cObservedDataCollection
    Public terapia_intensiva As New cObservedDataCollection
    Public totale_ospedalizzati As New cObservedDataCollection
    Public isolamento_domiciliare As New cObservedDataCollection
    Public totale_positivi As New cObservedDataCollection
    Public variazione_totale_positivi As New cObservedDataCollection
    Public nuovi_positivi As New cObservedDataCollection
    Public dimessi_guariti As New cObservedDataCollection
    Public deceduti As New cObservedDataCollection
    Public totale_casi As New cObservedDataCollection
    Public tamponi As New cObservedDataCollection
    Public ReadOnly Property StartingDate As Date
        Get
            If deceduti.Count > 0 Then
                Return deceduti(0).StartingDate
            Else
                Return New Date(2000, 1, 1)
            End If
        End Get
    End Property
    Public ReadOnly Property LastDate As Date
        Get
            Dim retVal As New Date(2000, 1, 1)
            For iCounter As Integer = 0 To deceduti.Count - 1
                If deceduti(iCounter).LatestDate > retVal Then
                    retVal = deceduti(iCounter).LatestDate
                End If
            Next
            Return retVal
        End Get
    End Property
    Public Function GetCountryValuesFromType(ByVal dataType As cDisplayInfo.enItalianValueType) As cObservedDataCollection
        Dim targetList As New cObservedDataCollection
        Select Case dataType
            Case cDisplayInfo.enItalianValueType.Hospitalized_with_Sypmtoms
                targetList = ricoverati_con_sintomi
            Case cDisplayInfo.enItalianValueType.Intensive_Care
                targetList = terapia_intensiva
            Case cDisplayInfo.enItalianValueType.Total_Hospitalized
                targetList = totale_ospedalizzati
            Case cDisplayInfo.enItalianValueType.Self_Isolating
                targetList = isolamento_domiciliare
            Case cDisplayInfo.enItalianValueType.Current_Positives
                targetList = totale_positivi
            Case cDisplayInfo.enItalianValueType.Current_Positives_Variation
                targetList = variazione_totale_positivi
            Case cDisplayInfo.enItalianValueType.New_Positives
                targetList = nuovi_positivi
            Case cDisplayInfo.enItalianValueType.Recovered
                targetList = dimessi_guariti
            Case cDisplayInfo.enItalianValueType.Deaths
                targetList = deceduti
            Case cDisplayInfo.enItalianValueType.Total_Cases
                targetList = totale_casi
            Case cDisplayInfo.enItalianValueType.Tests
                targetList = tamponi
        End Select
        Return targetList
    End Function
    Public Function GetListBoxItems(ByVal dataType As cDisplayInfo.enItalianValueType) As Generic.List(Of cCountryListboxItem)
        Dim retVal As New Generic.List(Of cCountryListboxItem)
        Dim targetList As cObservedDataCollection = GetCountryValuesFromType(dataType)
        If targetList.Count > 0 Then
            For dCounter As Integer = 0 To targetList.Count - 1
                Dim maxAbsValue As Double = 0
                Dim maxPercentValue As Double = 0
                If targetList(dCounter).DailyValues.Count > 0 Then
                    maxAbsValue = targetList(dCounter).DailyValues(targetList(dCounter).DailyValues.Count - 1).RecordAbsoluteValue
                    maxPercentValue = targetList(dCounter).DailyValues(targetList(dCounter).DailyValues.Count - 1).RecordPercentValue
                End If
                Dim newItem As New cCountryListboxItem(targetList(dCounter).ProvinceOrState, targetList(dCounter).CountryOrRegion, maxAbsValue, maxPercentValue)
                retVal.Add(newItem)
            Next
        End If
        If NormalizeToPopulation Then
            retVal = retVal.OrderBy(Function(x) x.TotalPercentCases).ToList()
            retVal.Reverse()
        End If

        Return retVal
    End Function
    Public Sub New()

    End Sub
    Public Sub New(ByVal csvLines() As String)
        If csvLines.Count <= 0 Then Return

        Dim allLines As New Generic.List(Of String)
        allLines.AddRange(csvLines)
        'First line is the header and must be discarded
        allLines.RemoveAt(0)

        'Extract all region names first
        Dim AllRegionNames As New List(Of String)
        Dim AllRegionCoords As New List(Of Tuple(Of Double, Double))
        For lCounter As Integer = 0 To allLines.Count - 1
            Dim lineParts() As String = allLines(lCounter).Split(",")
            Dim thisLineRegion As String = lineParts(3)
            Dim thisLineCoord As New Tuple(Of Double, Double)(CDbl(lineParts(4)), CDbl(lineParts(5)))
            If Not AllRegionNames.Contains(thisLineRegion) Then
                AllRegionNames.Add(thisLineRegion)
                AllRegionCoords.Add(thisLineCoord)
            End If
        Next

        'Prepare the lists for all the regions
        For rCounter As Integer = 0 To AllRegionNames.Count - 1
            Dim emptyCountryValues As New cCountryValues
            emptyCountryValues.ProvinceOrState = AllRegionNames(rCounter)
            emptyCountryValues.CountryOrRegion = "Italy"
            emptyCountryValues.Coordinates = AllRegionCoords(rCounter)
            emptyCountryValues.Population = Population.GetITARegionPopulation(emptyCountryValues.ProvinceOrState)

            ricoverati_con_sintomi.Add(emptyCountryValues.Clone)
            terapia_intensiva.Add(emptyCountryValues.Clone)
            totale_ospedalizzati.Add(emptyCountryValues.Clone)
            isolamento_domiciliare.Add(emptyCountryValues.Clone)
            totale_positivi.Add(emptyCountryValues.Clone)
            variazione_totale_positivi.Add(emptyCountryValues.Clone)
            nuovi_positivi.Add(emptyCountryValues.Clone)
            dimessi_guariti.Add(emptyCountryValues.Clone)
            deceduti.Add(emptyCountryValues.Clone)
            totale_casi.Add(emptyCountryValues.Clone)
            tamponi.Add(emptyCountryValues.Clone)
        Next

        'All lists are now ready to be filled with daily values
        For lCounter As Integer = 0 To allLines.Count - 1
            Dim lineParts() As String = allLines(lCounter).Split(",")

            Dim thisLineRegion As String = lineParts(3)
            Dim thisLinePopulationDivider As Double = Population.GetITARegionPopulation(thisLineRegion) / cPopulation.PerMillionDivider
            If thisLinePopulationDivider = 0 Then
                thisLinePopulationDivider = 1
            End If

            Dim thisLineDate As Date
            Date.TryParse(lineParts(0), thisLineDate)
            Dim ts As New TimeSpan(18, 0, 0)
            thisLineDate = thisLineDate.Date + ts

            Dim thisLine_ricoverati_con_sintomi As Integer = CInt(lineParts(6))
            Dim thisLine_terapia_intensiva As Integer = CInt(lineParts(7))
            Dim thisLine_totale_ospedalizzati As Integer = CInt(lineParts(8))
            Dim thisLine_isolamento_domiciliare As Integer = CInt(lineParts(9))
            Dim thisLine_totale_positivi As Integer = CInt(lineParts(10))
            Dim thisLine_variazione_totale_positivi As Integer = CInt(lineParts(11))
            Dim thisLine_nuovi_positivi As Integer = CInt(lineParts(12))
            Dim thisLine_dimessi_guariti As Integer = CInt(lineParts(13))
            Dim thisLine_deceduti As Integer = CInt(lineParts(14))
            Dim thisLine_totale_casi As Integer = CInt(lineParts(15))
            Dim thisLine_tamponi As Integer = CInt(lineParts(16))

            AddEntriesToTargetList(ricoverati_con_sintomi, thisLineRegion, thisLineDate, thisLine_ricoverati_con_sintomi, thisLine_ricoverati_con_sintomi / thisLinePopulationDivider)
            AddEntriesToTargetList(terapia_intensiva, thisLineRegion, thisLineDate, thisLine_terapia_intensiva, thisLine_terapia_intensiva / thisLinePopulationDivider)
            AddEntriesToTargetList(totale_ospedalizzati, thisLineRegion, thisLineDate, thisLine_totale_ospedalizzati, thisLine_totale_ospedalizzati / thisLinePopulationDivider)
            AddEntriesToTargetList(isolamento_domiciliare, thisLineRegion, thisLineDate, thisLine_isolamento_domiciliare, thisLine_isolamento_domiciliare / thisLinePopulationDivider)
            AddEntriesToTargetList(totale_positivi, thisLineRegion, thisLineDate, thisLine_totale_positivi, thisLine_totale_positivi / thisLinePopulationDivider)
            AddEntriesToTargetList(nuovi_positivi, thisLineRegion, thisLineDate, thisLine_nuovi_positivi, thisLine_nuovi_positivi / thisLinePopulationDivider)
            AddEntriesToTargetList(variazione_totale_positivi, thisLineRegion, thisLineDate, thisLine_variazione_totale_positivi, thisLine_variazione_totale_positivi / thisLinePopulationDivider)
            AddEntriesToTargetList(dimessi_guariti, thisLineRegion, thisLineDate, thisLine_dimessi_guariti, thisLine_dimessi_guariti / thisLinePopulationDivider)
            AddEntriesToTargetList(deceduti, thisLineRegion, thisLineDate, thisLine_deceduti, thisLine_deceduti / thisLinePopulationDivider)
            AddEntriesToTargetList(totale_casi, thisLineRegion, thisLineDate, thisLine_totale_casi, thisLine_totale_casi / thisLinePopulationDivider)
            AddEntriesToTargetList(tamponi, thisLineRegion, thisLineDate, thisLine_tamponi, thisLine_tamponi / thisLinePopulationDivider)
        Next

        ricoverati_con_sintomi = ricoverati_con_sintomi.OrderAscending(False)
        terapia_intensiva = terapia_intensiva.OrderAscending(False)
        totale_ospedalizzati = totale_ospedalizzati.OrderAscending(False)
        isolamento_domiciliare = isolamento_domiciliare.OrderAscending(False)
        totale_positivi = totale_positivi.OrderAscending(False)
        nuovi_positivi = nuovi_positivi.OrderAscending(False)
        dimessi_guariti = dimessi_guariti.OrderAscending(False)
        deceduti = deceduti.OrderAscending(False)
        totale_casi = totale_casi.OrderAscending(False)
        tamponi = tamponi.OrderAscending(False)

    End Sub
    Private Sub AddEntriesToTargetList(targetList As cObservedDataCollection, ByVal region As String, ByVal entryDate As Date, ByVal entry As Double, ByVal percentEntry As Double)
        For iCounter As Integer = 0 To targetList.Count - 1
            If targetList(iCounter).ProvinceOrState = region Then
                targetList(iCounter).DailyValues.Add(New cDailyValue(entryDate, entry, percentEntry))
                Exit For
            End If
        Next
    End Sub
    Private Sub AddValues(ByVal csvLines() As String, ByVal Countries As cObservedDataCollection)
        Try
            If (csvLines IsNot Nothing) AndAlso (csvLines.Count > 0) Then
                'First line is the header, which also contains all the dates covered
                'Every following line is a country/region, with the data for all the dates defined above

                'Let's start by saving all the dates
                Dim AllDates As New System.Collections.Generic.List(Of Date)
                Dim headerParts() As String = csvLines(0).Split(",")
                If headerParts.Count > 4 Then
                    'Parts from 4 up are dates
                    For pCounter As Integer = 4 To headerParts.Count - 1
                        Dim dateParts() As String = headerParts(pCounter).Split("/")
                        Dim thisDate As New Date(2000 + CInt(dateParts(2)), CInt(dateParts(0)), CInt(dateParts(1)))
                        AllDates.Add(thisDate)
                    Next
                End If

                'Following lines contains the data for every region at the specified dates
                For lineCounter As Integer = 1 To csvLines.Count - 1
                    Dim lineParts() As String = csvLines(lineCounter).Split(",")
                    Dim thisCountryVals As New cCountryValues
                    thisCountryVals.ProvinceOrState = lineParts(0)
                    thisCountryVals.CountryOrRegion = lineParts(1)
                    thisCountryVals.Coordinates = New Tuple(Of Double, Double)(CDbl(lineParts(2)), CDbl(lineParts(3)))
                    Dim AllValues As New System.Collections.Generic.List(Of Integer)
                    For partCounter As Integer = 4 To lineParts.Count - 1
                        If lineParts(partCounter).Length > 0 Then
                            AllValues.Add(CInt(lineParts(partCounter)))
                        Else
                            AllValues.Add(0)
                        End If
                    Next

                    Dim thisPopDivider As Double = Population.GetITARegionPopulation(thisCountryVals.ProvinceOrState) / cPopulation.PerMillionDivider
                    If thisPopDivider = 0 Then
                        thisPopDivider = 1
                    End If
                    For vCounter As Integer = 0 To AllValues.Count - 1
                        Dim thisDailyValue As New cDailyValue(AllDates(vCounter), AllValues(vCounter), AllValues(vCounter) / thisPopDivider)
                        thisCountryVals.DailyValues.Add(thisDailyValue)
                    Next
                    Countries.Add(thisCountryVals)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function GetDailyValues(ByVal valueType As cDisplayInfo.enItalianValueType, ByVal region As cCountryListboxItem) As cDailyValues
        Dim retVal As New cDailyValues
        Dim targetList As cObservedDataCollection = GetCountryValuesFromType(valueType)

        If targetList IsNot Nothing Then
            For iCounter As Integer = 0 To targetList.Count - 1
                If targetList(iCounter).IsAreaNameLike(region) Then
                    Dim collectionStarted As Boolean = False 'Ingnore initial zero values
                    For dCounter As Integer = 0 To targetList(iCounter).DailyValues.Count - 1
                        If (targetList(iCounter).DailyValues(dCounter).RecordAbsoluteValue > 0) Or collectionStarted Then
                            collectionStarted = True
                            Dim tmpVal As New cDailyValue(targetList(iCounter).DailyValues(dCounter))
                            tmpVal.RecordAbsoluteValue = tmpVal.RecordAbsoluteValue
                            retVal.Add(tmpVal)
                        End If
                    Next
                    Exit For
                End If
            Next
        End If

        Return retVal
    End Function
    Public Sub ShiftDays(ByVal valueType As cDisplayInfo.enItalianValueType, ByVal region As cCountryListboxItem, ByVal days As Integer)
        Try
            Dim targetList As cObservedDataCollection = GetCountryValuesFromType(valueType)
            If targetList IsNot Nothing Then
                For iCounter As Integer = 0 To targetList.Count - 1
                    If targetList(iCounter).IsAreaNameLike(region) Then
                        Dim collectionStarted As Boolean = False
                        For dCounter As Integer = 0 To targetList(iCounter).DailyValues.Count - 1
                            targetList(iCounter).DailyValues(dCounter).RecordDate = targetList(iCounter).DailyValues(dCounter).RecordDate.AddDays(days)
                        Next
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class

