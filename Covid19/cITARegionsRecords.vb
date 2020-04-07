
Public Class cITARegionsRecords
    'data,stato,codice_regione,denominazione_regione,lat,long
    Public ricoverati_con_sintomi As New List(Of cCountryValues)
    Public terapia_intensiva As New List(Of cCountryValues)
    Public totale_ospedalizzati As New List(Of cCountryValues)
    Public isolamento_domiciliare As New List(Of cCountryValues)
    Public totale_positivi As New List(Of cCountryValues)
    Public variazione_totale_positivi As New List(Of cCountryValues)
    Public nuovi_positivi As New List(Of cCountryValues)
    Public dimessi_guariti As New List(Of cCountryValues)
    Public deceduti As New List(Of cCountryValues)
    Public totale_casi As New List(Of cCountryValues)
    Public tamponi As New List(Of cCountryValues)
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
    Public Function GetCountryValuesFromType(ByVal dataType As cDisplayInfo.enItalianValueType) As List(Of cCountryValues)
        Dim targetList As New List(Of cCountryValues)
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
    Public Function GetRegionNames(ByVal dataType As cDisplayInfo.enItalianValueType) As Generic.List(Of cCountryListboxItem)
        Dim retVal As New Generic.List(Of cCountryListboxItem)
        Dim targetList As List(Of cCountryValues) = GetCountryValuesFromType(dataType)
        If targetList.Count > 0 Then
            For dCounter As Integer = 0 To targetList.Count - 1
                Dim maxValue As Integer = 0
                If targetList(dCounter).DailyValues.Count > 0 Then
                    maxValue = targetList(dCounter).DailyValues(targetList(dCounter).DailyValues.Count - 1).RecordValue
                End If
                Dim newItem As New cCountryListboxItem(targetList(dCounter).Province_State, targetList(dCounter).Country_Region, maxValue)
                retVal.Add(newItem)
            Next
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
            emptyCountryValues.Province_State = AllRegionNames(rCounter)
            emptyCountryValues.Country_Region = "Italy"
            emptyCountryValues.Coordinates = AllRegionCoords(rCounter)

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

            AddEntriesToTargetList(ricoverati_con_sintomi, thisLineRegion, thisLineDate, thisLine_ricoverati_con_sintomi)
            AddEntriesToTargetList(terapia_intensiva, thisLineRegion, thisLineDate, thisLine_terapia_intensiva)
            AddEntriesToTargetList(totale_ospedalizzati, thisLineRegion, thisLineDate, thisLine_totale_ospedalizzati)
            AddEntriesToTargetList(isolamento_domiciliare, thisLineRegion, thisLineDate, thisLine_isolamento_domiciliare)
            AddEntriesToTargetList(totale_positivi, thisLineRegion, thisLineDate, thisLine_totale_positivi)
            AddEntriesToTargetList(nuovi_positivi, thisLineRegion, thisLineDate, thisLine_nuovi_positivi)
            AddEntriesToTargetList(variazione_totale_positivi, thisLineRegion, thisLineDate, thisLine_variazione_totale_positivi)
            AddEntriesToTargetList(dimessi_guariti, thisLineRegion, thisLineDate, thisLine_dimessi_guariti)
            AddEntriesToTargetList(deceduti, thisLineRegion, thisLineDate, thisLine_deceduti)
            AddEntriesToTargetList(totale_casi, thisLineRegion, thisLineDate, thisLine_totale_casi)
            AddEntriesToTargetList(tamponi, thisLineRegion, thisLineDate, thisLine_tamponi)
        Next

        ricoverati_con_sintomi = ricoverati_con_sintomi.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        ricoverati_con_sintomi.Reverse()
        terapia_intensiva = terapia_intensiva.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        terapia_intensiva.Reverse()
        totale_ospedalizzati = totale_ospedalizzati.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        totale_ospedalizzati.Reverse()
        isolamento_domiciliare = isolamento_domiciliare.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        isolamento_domiciliare.Reverse()
        totale_positivi = totale_positivi.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        totale_positivi.Reverse()
        nuovi_positivi = nuovi_positivi.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        nuovi_positivi.Reverse()
        dimessi_guariti = dimessi_guariti.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        dimessi_guariti.Reverse()
        deceduti = deceduti.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        deceduti.Reverse()
        totale_casi = totale_casi.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        totale_casi.Reverse()
        tamponi = tamponi.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        tamponi.Reverse()

    End Sub
    Private Sub AddEntriesToTargetList(targetList As List(Of cCountryValues), ByVal region As String, ByVal entryDate As Date, ByVal entry As Integer)
        For iCounter As Integer = 0 To targetList.Count - 1
            If targetList(iCounter).Province_State = region Then
                targetList(iCounter).DailyValues.Add(New cDailyValue(entryDate, entry))
                Exit For
            End If
        Next
    End Sub
    Private Sub AddValues(ByVal csvLines() As String, ByVal Countries As List(Of cCountryValues))
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
                    thisCountryVals.Province_State = lineParts(0)
                    thisCountryVals.Country_Region = lineParts(1)
                    thisCountryVals.Coordinates = New Tuple(Of Double, Double)(CDbl(lineParts(2)), CDbl(lineParts(3)))
                    Dim AllValues As New System.Collections.Generic.List(Of Integer)
                    For partCounter As Integer = 4 To lineParts.Count - 1
                        If lineParts(partCounter).Length > 0 Then
                            AllValues.Add(CInt(lineParts(partCounter)))
                        Else
                            AllValues.Add(0)
                        End If
                    Next

                    For vCounter As Integer = 0 To AllValues.Count - 1
                        Dim thisDailyValue As New cDailyValue(AllDates(vCounter), AllValues(vCounter))
                        thisCountryVals.DailyValues.Add(thisDailyValue)
                    Next
                    Countries.Add(thisCountryVals)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function GetDailyValues(ByVal valueType As cDisplayInfo.enItalianValueType, ByVal region As cCountryListboxItem) As List(Of cDailyValue)
        Dim retVal As New List(Of cDailyValue)
        Dim targetList As List(Of cCountryValues) = GetCountryValuesFromType(valueType)

        Dim pop As Double = 1
        If NormalizeToPopulation Then
            pop = Population.GetITARegionPopulation(region.Province_State) / 10000.0
            If pop = 0 Then
                pop = 1
            End If
        End If

        If targetList IsNot Nothing Then
            For iCounter As Integer = 0 To targetList.Count - 1
                If targetList(iCounter).IsRegionLike(region) Then
                    Dim collectionStarted As Boolean = False 'Ingnore initial zero values
                    For dCounter As Integer = 0 To targetList(iCounter).DailyValues.Count - 1
                        If (targetList(iCounter).DailyValues(dCounter).RecordValue > 0) Or collectionStarted Then
                            collectionStarted = True
                            Dim tmpVal As New cDailyValue(targetList(iCounter).DailyValues(dCounter))
                            tmpVal.RecordValue = tmpVal.RecordValue / pop
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
            Dim targetList As List(Of cCountryValues) = GetCountryValuesFromType(valueType)
            If targetList IsNot Nothing Then
                For iCounter As Integer = 0 To targetList.Count - 1
                    If targetList(iCounter).IsRegionLike(region) Then
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

