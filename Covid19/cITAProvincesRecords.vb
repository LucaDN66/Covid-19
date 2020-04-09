
Public Class cITAProvincesRecords
    'data,stato,codice_regione,denominazione_regione,lat,long
    Public totale_casi As New List(Of cCountryValues)
    Public ReadOnly Property StartingDate As Date
        Get
            If totale_casi.Count > 0 Then
                Return totale_casi(0).StartingDate
            Else
                Return New Date(2000, 1, 1)
            End If
        End Get
    End Property
    Public ReadOnly Property LastDate As Date
        Get
            Dim retVal As New Date(2000, 1, 1)
            For iCounter As Integer = 0 To totale_casi.Count - 1
                If totale_casi(iCounter).LatestDate > retVal Then
                    retVal = totale_casi(iCounter).LatestDate
                End If
            Next
            Return retVal
        End Get
    End Property
    Public Function GetCountryValuesFromType(ByVal dataType As cDisplayInfo.enItalianValueType) As List(Of cCountryValues)
        Dim targetList As New List(Of cCountryValues)
        Select Case dataType
            Case cDisplayInfo.enItalianValueType.Total_Cases
                targetList = totale_casi
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
        Dim AllProvincesNames As New List(Of String)
        Dim AllRegionsAndProvinces As New List(Of Tuple(Of String, String))
        Dim AllProvincesCoords As New List(Of Tuple(Of Double, Double))
        For lCounter As Integer = 0 To allLines.Count - 1
            Dim lineParts() As String = allLines(lCounter).Split(",")
            Dim thisLinePCode As Double = CDbl(lineParts(4))
            If thisLinePCode < 900 Then
                Dim thisLineRegion As String = lineParts(3)
                Dim thisLineProvince As String = lineParts(5)
                Dim thisLineCoord As New Tuple(Of Double, Double)(CDbl(lineParts(7)), CDbl(lineParts(8)))
                If Not AllProvincesNames.Contains(thisLineProvince) Then
                    AllProvincesNames.Add(thisLineProvince)
                    AllProvincesCoords.Add(thisLineCoord)
                    AllRegionsAndProvinces.Add(New Tuple(Of String, String)(thisLineRegion, thisLineProvince))
                End If
            End If
        Next

        'Prepare the lists for all the regions
        For rCounter As Integer = 0 To AllRegionsAndProvinces.Count - 1
            Dim emptyCountryValues As New cCountryValues
            emptyCountryValues.Province_State = AllRegionsAndProvinces(rCounter).Item2
            emptyCountryValues.Country_Region = AllRegionsAndProvinces(rCounter).Item1
            emptyCountryValues.Coordinates = AllProvincesCoords(rCounter)
            totale_casi.Add(emptyCountryValues.Clone)
        Next

        'All lists are now ready to be filled with daily values
        For lCounter As Integer = 0 To allLines.Count - 1
            Dim lineParts() As String = allLines(lCounter).Split(",")
            Dim thisLineRegion As String = lineParts(3)
            Dim thisLineProvince As String = lineParts(5)
            Dim thisLineDate As Date
            Date.TryParse(lineParts(0), thisLineDate)
            Dim ts As New TimeSpan(18, 0, 0)
            thisLineDate = thisLineDate.Date + ts
            Dim thisLine_totale_casi As Integer = CInt(lineParts(9))
            AddEntriesToTargetList(totale_casi, thisLineProvince, thisLineDate, thisLine_totale_casi)
        Next

        totale_casi = totale_casi.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        totale_casi.Reverse()

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

        If targetList IsNot Nothing Then
            For iCounter As Integer = 0 To targetList.Count - 1
                If targetList(iCounter).IsRegionLike(region) Then
                    Dim collectionStarted As Boolean = False 'Ingnore initial zero values
                    For dCounter As Integer = 0 To targetList(iCounter).DailyValues.Count - 1
                        If (targetList(iCounter).DailyValues(dCounter).RecordValue > 0) Or collectionStarted Then
                            collectionStarted = True
                            Dim tmpVal As New cDailyValue(targetList(iCounter).DailyValues(dCounter))
                            tmpVal.RecordValue = tmpVal.RecordValue
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
    Public Sub SetValuesAsPopulationPercentage()
        'If population is not available, the record is removed
        For iCounter As Integer = totale_casi.Count - 1 To 0 Step -1
            Dim pop As Double = 1
            pop = Population.GetITAProvincePopulation(totale_casi(iCounter).Province_State) / 10000.0
            If pop = 0 Then
                Me.totale_casi.RemoveAt(iCounter)
            Else
                For dCounter As Integer = 0 To Me.totale_casi(iCounter).DailyValues.Count - 1
                    Me.totale_casi(iCounter).DailyValues(dCounter).RecordValue = Me.totale_casi(iCounter).DailyValues(dCounter).RecordValue / pop
                Next
            End If
        Next
        'Reorder according to the last recorded value of each series
        totale_casi = totale_casi.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        totale_casi.Reverse()
    End Sub

End Class

