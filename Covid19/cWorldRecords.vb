<DebuggerDisplay("Date:{RecordDate.ToShortDateString}, Absolute={RecordAbsoluteValue}, Relative={RecordPercentValue}")> Public Class cDailyValue
    Public RecordDate As New Date(2000, 1, 1)
    Public RecordAbsoluteValue As Double = 0
    Public RecordPercentValue As Double = 0
    Public Sub New(ByVal aDate As Date, ByVal absoluteValue As Double, ByVal percentValue As Double)
        Me.RecordDate = aDate
        Me.RecordAbsoluteValue = absoluteValue
        Me.RecordPercentValue = percentValue
    End Sub
    Public Function Clone() As cDailyValue
        Return New cDailyValue(Me)
    End Function
    Public Sub New(ByVal anotherValue As cDailyValue)
        RecordDate = anotherValue.RecordDate
        RecordAbsoluteValue = anotherValue.RecordAbsoluteValue
        RecordPercentValue = anotherValue.RecordPercentValue
    End Sub
End Class

Public Class cDailyValues
    Inherits List(Of cDailyValue)
    Public Function GetIndexByDate(ByVal aDate As Date) As Integer
        For iCounter As Integer = 0 To Me.Count - 1
            If Me(iCounter).RecordDate = aDate Then
                Return iCounter
            End If
        Next
        Return -1
    End Function
End Class

<DebuggerDisplay("State:{ProvinceOrState}, Country:{CountryOrRegion}, Cases={TotalCases}")> Public Class cCountryListboxItem
    Public ProvinceOrState As String = ""
    Public CountryOrRegion As String = ""
    Public TotalCases As Double = 0
    Public TotalPercentCases As Double = 0
    Private ReadOnly Property TotalCasesReadable As String
        Get
            If NormalizeToPopulation Then
                Return strCutDecimals(TotalPercentCases, 2)
            Else
                Return strCutDecimals(TotalCases, 2)
            End If
        End Get
    End Property
    Public Overrides Function ToString() As String
        If ProvinceOrState.Length > 0 Then
            If ProvinceOrState <> CountryOrRegion Then
                Return ProvinceOrState + ", " + CountryOrRegion + " (" + TotalCasesReadable + ")"
            Else
                Return CountryOrRegion + " (" + TotalCasesReadable + ")"
            End If
        Else
            Return CountryOrRegion + " (" + TotalCasesReadable + ")"
        End If
    End Function
    Public Sub New(ByVal Province As String, ByVal Country As String, ByVal Cases As Double, ByVal PercentCases As Double)
        ProvinceOrState = Province
        CountryOrRegion = Country
        TotalCases = Cases
        TotalPercentCases = PercentCases
    End Sub
End Class

<DebuggerDisplay("State:{ProvinceOrState}, Country:{CountryOrRegion}, Days covered:{DailyValues.Count}, Population:{Population}")> Public Class cCountryValues
    Public ProvinceOrState As String = "Unspecified"
    Public CountryOrRegion As String = "Unspecified"
    Public Coordinates As New Tuple(Of Double, Double)(0, 0)
    Public DailyValues As New cDailyValues
    Public Population As Double = 0
    Public Function IsAreaNameLike(ByVal listItem As cCountryListboxItem) As Boolean
        Dim rCopy As String = CountryOrRegion.Trim.Replace(" ", "").ToUpper
        rCopy = rCopy.Replace("'", "")
        rCopy = rCopy.Replace("-", "")
        Dim sCopy As String = ProvinceOrState.Trim.Replace(" ", "").ToUpper
        sCopy = sCopy.Replace("'", "")
        sCopy = sCopy.Replace("-", "")

        Dim lstrCopy As String = listItem.CountryOrRegion.Trim.Replace(" ", "").ToUpper
        lstrCopy = lstrCopy.Replace("'", "")
        lstrCopy = lstrCopy.Replace("-", "")
        Dim lstsCopy As String = listItem.ProvinceOrState.Trim.Replace(" ", "").ToUpper
        lstsCopy = lstsCopy.Replace("'", "")
        lstsCopy = lstsCopy.Replace("-", "")

        Return (lstrCopy = rCopy) AndAlso (lstsCopy = sCopy)
    End Function
    Public Sub New()

    End Sub
    Public Function Clone() As cCountryValues
        Return New cCountryValues(Me)
    End Function
    Public Sub New(ByVal anotherValue As cCountryValues)
        ProvinceOrState = anotherValue.ProvinceOrState
        CountryOrRegion = anotherValue.CountryOrRegion
        Coordinates = anotherValue.Coordinates
        Population = anotherValue.Population
        DailyValues.Clear()
        For iCounter As Integer = 0 To anotherValue.DailyValues.Count - 1
            DailyValues.Add(anotherValue.DailyValues(iCounter).Clone)
        Next
    End Sub
    Public ReadOnly Property StartingDate As Date
        Get
            If Me.DailyValues.Count > 0 Then
                Return Me.DailyValues(0).RecordDate
            Else
                Return New Date(2000, 1, 1)
            End If
        End Get
    End Property
    Public Sub SumDailyAbsoluteValues(ByVal someValues As cDailyValues, ByVal population As Double)
        Dim destIndex As Integer = -1
        For Each dailyVal As cDailyValue In someValues
            destIndex = DailyValues.GetIndexByDate(dailyVal.RecordDate)
            If destIndex >= 0 Then
                DailyValues(destIndex).RecordAbsoluteValue += dailyVal.RecordAbsoluteValue
                DailyValues(destIndex).RecordPercentValue = DailyValues(destIndex).RecordAbsoluteValue / population * cPopulation.PerMillionDivider
            End If
        Next
    End Sub

    Public ReadOnly Property LatestDate As Date
        Get
            Dim retVal As New Date(2000, 1, 1)
            For icounter As Integer = 0 To Me.DailyValues.Count - 1
                If Me.DailyValues(icounter).RecordDate > retVal Then
                    retVal = Me.DailyValues(icounter).RecordDate
                End If
            Next
            Return retVal
        End Get
    End Property
End Class

Public Class cObservedDataCollection
    Inherits List(Of cCountryValues)
    Public Sub New(ByVal anotherCollection As cObservedDataCollection)
        Me.Clear()
        For iCounter As Integer = 0 To anotherCollection.Count - 1
            Me.Add(anotherCollection(iCounter).Clone)
        Next
    End Sub
    Public Sub New()

    End Sub
    Public Function FindValue(ByVal aRegion As String, ByVal aDate As Date) As Double
        Dim countryIndex As Integer = IndexOfCountry(aRegion)
        If countryIndex <> -1 Then
            Dim valIndex As Integer = Me(countryIndex).DailyValues.GetIndexByDate(aDate)
            If valIndex <> -1 Then
                Return Me(countryIndex).DailyValues(valIndex).RecordAbsoluteValue
            End If
        End If
        Return 0
    End Function
    Public Function IndexOfCountry(ByVal aCountryOrRegion As String) As Integer
        For iCounter As Integer = 0 To Me.Count - 1
            If Me(iCounter).CountryOrRegion = aCountryOrRegion Then
                Return iCounter
            End If
        Next
        Return -1
    End Function
    Public Function OrderAscending(ByVal UsePercentValues As Boolean) As cObservedDataCollection
        Dim orderedList As New List(Of cCountryValues)
        For iCounter As Integer = Me.Count - 1 To 0 Step -1
            If Me(iCounter).DailyValues.Count = 0 Then
                Me.RemoveAt(iCounter)
            End If
        Next
        If UsePercentValues Then
            orderedList = Me.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordPercentValue).ToList()
        Else
            orderedList = Me.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordAbsoluteValue).ToList()
        End If
        orderedList.Reverse()
        Dim newColl As New cObservedDataCollection
        newColl.AddRange(orderedList)
        Return newColl
    End Function
End Class

Public Class cWorldRecords
    Public Enum enRecordsVariant As Integer
        World = 0
        USCities = 1
    End Enum
    Private myRecordsVariant As enRecordsVariant = enRecordsVariant.World
    Public Deaths As New cObservedDataCollection
    Public Confirmed As New cObservedDataCollection
    Public Recovered As New cObservedDataCollection
    Public FatalityRates As New cObservedDataCollection
    Public Sub New()

    End Sub
    Public Sub New(ByVal anotherCollection As cWorldRecords)
        Deaths = New cObservedDataCollection(anotherCollection.Deaths)
        Confirmed = New cObservedDataCollection(anotherCollection.Confirmed)
        Recovered = New cObservedDataCollection(anotherCollection.Recovered)
        FatalityRates = New cObservedDataCollection(anotherCollection.FatalityRates)
    End Sub
    Private Sub RemoveNonEUEntriesFromList(ByVal aList As cObservedDataCollection)
        For iCounter As Integer = aList.Count - 1 To 0 Step -1
            If aList(iCounter).ProvinceOrState.Length > 0 Then
                'We remove every item that is not a Country, but just a province/state of the main country
                aList.RemoveAt(iCounter)
            Else
                If Not Population.myEuropeanCountries.Contains(aList(iCounter).CountryOrRegion) Then
                    aList.RemoveAt(iCounter)
                End If
            End If
        Next
    End Sub
    Public Sub RemoveNonEuropeanEntries()
        RemoveNonEUEntriesFromList(Deaths)
        RemoveNonEUEntriesFromList(Confirmed)
        RemoveNonEUEntriesFromList(Recovered)
        RemoveNonEUEntriesFromList(FatalityRates)
    End Sub
    Public Sub SetDeaths(ByVal csvLines() As String, ByVal recVariant As enRecordsVariant)
        Deaths.Clear()
        AddValues(csvLines, Deaths, recVariant)
        Deaths = Deaths.OrderAscending(False)
    End Sub
    Public Sub SetConfirmed(ByVal csvLines() As String, ByVal recVariant As enRecordsVariant)
        Confirmed.Clear()
        AddValues(csvLines, Confirmed, recVariant)
        Confirmed = Confirmed.OrderAscending(False)
    End Sub
    Public Sub SetRecovered(ByVal csvLines() As String, ByVal recVariant As enRecordsVariant)
        Recovered.Clear()
        AddValues(csvLines, Recovered, recVariant)
        Recovered = Recovered.OrderAscending(False)
    End Sub
    Public Sub SetFatalityRates()
        FatalityRates = New cObservedDataCollection(Deaths)
        For dCounter As Integer = 0 To FatalityRates.Count - 1
            For vCounter As Integer = 0 To FatalityRates(dCounter).DailyValues.Count - 1
                Dim correspondingConfirmed As Double = Confirmed.FindValue(FatalityRates(dCounter).CountryOrRegion, FatalityRates(dCounter).DailyValues(vCounter).RecordDate)
                If correspondingConfirmed > 1000 Then
                    FatalityRates(dCounter).DailyValues(vCounter).RecordAbsoluteValue = FatalityRates(dCounter).DailyValues(vCounter).RecordAbsoluteValue / correspondingConfirmed * 100
                Else
                    FatalityRates(dCounter).DailyValues(vCounter).RecordAbsoluteValue = 0
                End If
                FatalityRates(dCounter).DailyValues(vCounter).RecordPercentValue = FatalityRates(dCounter).DailyValues(vCounter).RecordAbsoluteValue
            Next
        Next

        For iCounter As Integer = FatalityRates.Count - 1 To 0 Step -1
            If FatalityRates(iCounter).DailyValues.Count > 0 Then
                If FatalityRates(iCounter).DailyValues(FatalityRates(iCounter).DailyValues.Count - 1).RecordAbsoluteValue = 0 Then
                    FatalityRates.RemoveAt(iCounter)
                End If
            End If
        Next



        FatalityRates = FatalityRates.OrderAscending(False)
    End Sub
    Public ReadOnly Property LastDate As Date
        Get
            Dim retVal As New Date(2000, 1, 1)
            For iCounter As Integer = 0 To Deaths.Count - 1
                If Deaths(iCounter).LatestDate > retVal Then
                    retVal = Deaths(iCounter).LatestDate
                End If
            Next
            Return retVal
        End Get
    End Property
    Public ReadOnly Property StartingDate As Date
        Get
            If Deaths.Count > 0 Then
                Return Deaths(0).StartingDate
            Else
                Return New Date(2000, 1, 1)
            End If
        End Get
    End Property
    Public Function GetListBoxItems(ByVal dataType As cDisplayInfo.enWorldValueType, ByVal IgnoreSmallCountries As Boolean) As Generic.List(Of cCountryListboxItem)
        Dim retVal As New Generic.List(Of cCountryListboxItem)
        If dataType >= 0 Then
            Dim targetList As cObservedDataCollection = Nothing
            Select Case dataType
                Case cDisplayInfo.enWorldValueType.Confirmed
                    targetList = Confirmed
                Case cDisplayInfo.enWorldValueType.Deaths
                    targetList = Deaths
                Case cDisplayInfo.enWorldValueType.Recovered
                    targetList = Recovered
                Case cDisplayInfo.enWorldValueType.FatalityRates
                    targetList = FatalityRates
            End Select
            If targetList.Count > 0 Then
                For dCounter As Integer = 0 To targetList.Count - 1
                    Dim maxAbsValue As Double = 0
                    Dim maxPercentValue As Double = 0
                    If (targetList(dCounter).DailyValues.Count > 0) Then
                        maxAbsValue = targetList(dCounter).DailyValues(targetList(dCounter).DailyValues.Count - 1).RecordAbsoluteValue
                        maxPercentValue = targetList(dCounter).DailyValues(targetList(dCounter).DailyValues.Count - 1).RecordPercentValue
                    End If
                    If IgnoreSmallCountries AndAlso (targetList(dCounter).Population < 100000) Then
                        'Skip this one
                    Else
                        Dim newItem As New cCountryListboxItem(targetList(dCounter).ProvinceOrState, targetList(dCounter).CountryOrRegion, maxAbsValue, maxPercentValue)
                        retVal.Add(newItem)
                    End If
                Next
            End If
        End If
        If NormalizeToPopulation Then
            retVal = retVal.OrderBy(Function(x) x.TotalPercentCases).ToList()
            retVal.Reverse()
        End If
        Return retVal
    End Function
    Public Sub AddValues(ByVal csvLines() As String, ByVal Countries As cObservedDataCollection, ByVal RecVariant As enRecordsVariant)
        Try
            If (csvLines IsNot Nothing) AndAlso (csvLines.Count > 0) Then
                myRecordsVariant = RecVariant
                'First line is the header, which also contains all the dates covered
                'Every following line is a country/region, with the data for all the dates defined above

                Dim USRecordMode As Boolean = False
                Dim USDeathsHeader As Boolean = False
                If csvLines(0).ToUpper.StartsWith("UID,iso2,iso3,code3,FIPS,Admin2".ToUpper) Then
                    USRecordMode = True
                    If csvLines(0).ToUpper.Contains("POPULATION") Then
                        USDeathsHeader = True
                    End If
                Else
                    'World records
                End If


                'Let's start by saving all the dates
                Dim AllDates As New System.Collections.Generic.List(Of Date)
                Dim headerParts() As String = csvLines(0).Split(",")
                Dim AdditionalShift As Integer = 0
                If USRecordMode Then
                    If USDeathsHeader Then
                        AdditionalShift = 1
                    End If
                    'Parts from 12 up are dates
                    If headerParts.Count > 11 Then
                        For pCounter As Integer = (11 + AdditionalShift) To headerParts.Count - 1
                            Dim dateParts() As String = headerParts(pCounter).Split("/")
                            Dim thisDate As New Date(2000 + CInt(dateParts(2)), CInt(dateParts(0)), CInt(dateParts(1)))
                            AllDates.Add(thisDate)
                        Next
                    End If
                Else
                    If headerParts.Count > 4 Then
                        'Parts from 4 up are dates
                        For pCounter As Integer = 4 To headerParts.Count - 1
                            Dim dateParts() As String = headerParts(pCounter).Split("/")
                            Dim thisDate As New Date(2000 + CInt(dateParts(2)), CInt(dateParts(0)), CInt(dateParts(1)))
                            AllDates.Add(thisDate)
                        Next
                    End If
                End If

                'Following lines contains the data for every region at the specified dates
                For lineCounter As Integer = 1 To csvLines.Count - 1
                    Dim lineParts() As String = csvLines(lineCounter).Split(",")
                    Dim thisCountryVals As New cCountryValues
                    If USRecordMode Then
                        thisCountryVals.ProvinceOrState = lineParts(5)
                        thisCountryVals.CountryOrRegion = lineParts(6)
                        thisCountryVals.Coordinates = New Tuple(Of Double, Double)(CDbl(lineParts(8)), CDbl(lineParts(9)))
                        If USDeathsHeader Then
                            'These records also contain the population
                            thisCountryVals.Population = CDbl(lineParts(11))
                        Else
                            'No population on these lines, we need to add it
                            thisCountryVals.Population = Population.GetUSStatePopulation(thisCountryVals.CountryOrRegion)
                        End If
                    Else
                        'No population on these lines, we need to add it
                        thisCountryVals.ProvinceOrState = lineParts(0)
                        thisCountryVals.CountryOrRegion = lineParts(1)
                        thisCountryVals.Coordinates = New Tuple(Of Double, Double)(CDbl(lineParts(2)), CDbl(lineParts(3)))
                        thisCountryVals.Population = Population.GetWorldCountryPopulation(thisCountryVals.CountryOrRegion)
                    End If
                    Dim AllValues As New System.Collections.Generic.List(Of Double)

                    Dim startIndex As Integer = 4
                    If USRecordMode Then
                        startIndex = 11 + AdditionalShift
                    End If
                    For partCounter As Integer = startIndex To lineParts.Count - 1
                        If lineParts(partCounter).Length > 0 Then
                            AllValues.Add(CInt(lineParts(partCounter)))
                        Else
                            AllValues.Add(0)
                        End If
                    Next

                    Dim divider As Double = thisCountryVals.Population / cPopulation.PerMillionDivider
                    If divider = 0 Then
                        'Skip this one, no population no way of comparing it to others
                        divider = 1
                    Else
                        For vCounter As Integer = 0 To AllValues.Count - 1
                            Dim thisDailyValue As New cDailyValue(AllDates(vCounter), AllValues(vCounter), AllValues(vCounter) / divider)
                            thisCountryVals.DailyValues.Add(thisDailyValue)
                        Next

                        If USRecordMode Then
                            Countries.Add(thisCountryVals)
                        Else
                            'We are doing world countries here, and if data are given for different regions of a country, we merge them
                            'If we need to analyze the situation inside a country, we will do that somewhere else
                            Dim preExistingIndex As Integer = Countries.IndexOfCountry(thisCountryVals.CountryOrRegion)
                            If preExistingIndex >= 0 Then
                                Countries(preExistingIndex).ProvinceOrState = "" 'Get rid of this, we are taking all provinces together
                                Countries(preExistingIndex).SumDailyAbsoluteValues(thisCountryVals.DailyValues, thisCountryVals.Population)
                            Else
                                Countries.Add(thisCountryVals)
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function GetDailyValues(ByVal valueType As cDisplayInfo.enWorldValueType, ByVal region As cCountryListboxItem, ByVal isUSCity As Boolean) As cDailyValues
        Dim retVal As New cDailyValues
        Dim targetList As cObservedDataCollection = Nothing
        Select Case valueType
            Case cDisplayInfo.enWorldValueType.Confirmed
                targetList = Confirmed
            Case cDisplayInfo.enWorldValueType.Deaths
                targetList = Deaths
            Case cDisplayInfo.enWorldValueType.Recovered
                targetList = Recovered
            Case cDisplayInfo.enWorldValueType.FatalityRates
                targetList = FatalityRates
        End Select

        If targetList IsNot Nothing Then
            For iCounter As Integer = 0 To targetList.Count - 1
                If targetList(iCounter).IsAreaNameLike(region) Then
                    Dim collectionStarted As Boolean = False
                    For dCounter As Integer = 0 To targetList(iCounter).DailyValues.Count - 1
                        If (targetList(iCounter).DailyValues(dCounter).RecordAbsoluteValue > 0) Or collectionStarted Then
                            collectionStarted = True
                            Dim tmpVal As New cDailyValue(targetList(iCounter).DailyValues(dCounter))
                            retVal.Add(tmpVal)
                        End If
                    Next
                    Exit For
                End If
            Next
        End If

        Return retVal
    End Function
    Public Sub ShiftDays(ByVal valueType As cDisplayInfo.enWorldValueType, ByVal region As cCountryListboxItem, ByVal days As Integer)
        Try
            Dim targetList As cObservedDataCollection = Nothing
            Select Case valueType
                Case cDisplayInfo.enWorldValueType.Confirmed
                    targetList = Confirmed
                Case cDisplayInfo.enWorldValueType.Deaths
                    targetList = Deaths
                Case cDisplayInfo.enWorldValueType.Recovered
                    targetList = Recovered
                Case cDisplayInfo.enWorldValueType.FatalityRates
                    targetList = FatalityRates
            End Select

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

Public Class cHeatMapItem
    Public Region As String
    Public AbsValue As Double
    Public ReadOnly Property PercentValue(ByVal population As Double) As Double
        Get
            If population <> 0 Then
                Return AbsValue / population * cPopulation.PerMillionDivider
            Else
                Return 0
            End If
        End Get
    End Property
    Public ItemTooltip As String = ""
End Class
Public Class cHeatMapItems
    Inherits List(Of cHeatMapItem)

End Class
