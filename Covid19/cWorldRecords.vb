<DebuggerDisplay("Date:{RecordDate.ToShortDateString}, Value={RecordValue}")> Public Class cDailyValue
    Public RecordDate As New Date(2000, 1, 1)
    Public RecordValue As Double = 0
    Public Sub New(ByVal aDate As Date, ByVal aValue As Integer)
        Me.RecordDate = aDate
        Me.RecordValue = aValue
    End Sub
    Public Function Clone() As cDailyValue
        Return New cDailyValue(Me)
    End Function
    Public Sub New(ByVal anotherValue As cDailyValue)
        RecordDate = anotherValue.RecordDate
        RecordValue = anotherValue.RecordValue
    End Sub

End Class
<DebuggerDisplay("State:{Province_State}, Country:{Country_Region}, Cases={TotalCases}")> Public Class cCountryListboxItem
    Public Province_State As String = ""
    Public Country_Region As String = ""
    Public TotalCases As Integer = 0
    Public Overrides Function ToString() As String
        If Province_State.Length > 0 Then
            If Province_State <> Country_Region Then
                Return Province_State + ", " + Country_Region + " (" + CStr(TotalCases) + ")"
            Else
                Return Country_Region + " (" + CStr(TotalCases) + ")"
            End If
        Else
            Return Country_Region + " (" + CStr(TotalCases) + ")"
        End If
    End Function
    Public Sub New(ByVal Province As String, ByVal Country As String, ByVal Cases As Integer)
        Province_State = Province
        Country_Region = Country
        TotalCases = Cases
    End Sub
End Class
<DebuggerDisplay("State:{Province_State}, Country:{Country_Region}, Days covered:{DailyValues.Count}")> Public Class cCountryValues
    Public Province_State As String = "Unspecified"
    Public Country_Region As String = "Unspecified"
    Public Coordinates As New Tuple(Of Double, Double)(0, 0)
    Public DailyValues As New List(Of cDailyValue)
    Public Population As Double = 0
    Public Function IsRegionLike(ByVal listItem As cCountryListboxItem) As Boolean
        Return (listItem.Country_Region = Country_Region) AndAlso (listItem.Province_State = Province_State)
    End Function
    Public Sub New()

    End Sub
    Public Function Clone() As cCountryValues
        Return New cCountryValues(Me)
    End Function
    Public Sub New(ByVal anotherValue As cCountryValues)
        Province_State = anotherValue.Province_State
        Country_Region = anotherValue.Country_Region
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

Public Class cWorldRecords
    Public Deaths As New List(Of cCountryValues)
    Public Confirmed As New List(Of cCountryValues)
    Public Recovered As New List(Of cCountryValues)
    Public Sub SetDeaths(ByVal csvLines() As String)
        Deaths.Clear()
        AddValues(csvLines, Deaths)
        Deaths = Deaths.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        Deaths.Reverse()
    End Sub
    Public Sub SetConfirmed(ByVal csvLines() As String)
        Confirmed.Clear()
        AddValues(csvLines, Confirmed)
        Confirmed = Confirmed.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        Confirmed.Reverse()
    End Sub
    Public Sub SetRecovered(ByVal csvLines() As String)
        Recovered.Clear()
        AddValues(csvLines, Recovered)
        Recovered = Recovered.OrderBy(Function(x) x.DailyValues(x.DailyValues.Count - 1).RecordValue).ToList()
        Recovered.Reverse()
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
    Public Function GetRegionNames(ByVal dataType As cDisplayInfo.enWorldValueType) As Generic.List(Of cCountryListboxItem)
        Dim retVal As New Generic.List(Of cCountryListboxItem)
        If dataType >= 0 Then
            Dim targetList As List(Of cCountryValues) = Nothing
            Select Case dataType
                Case cDisplayInfo.enWorldValueType.Confirmed
                    targetList = Confirmed
                Case cDisplayInfo.enWorldValueType.Deaths
                    targetList = Deaths
                Case cDisplayInfo.enWorldValueType.Recovered
                    targetList = Recovered
            End Select
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
        End If
        Return retVal
    End Function
    Public Sub AddValues(ByVal csvLines() As String, ByVal Countries As List(Of cCountryValues))
        Try
            If (csvLines IsNot Nothing) AndAlso (csvLines.Count > 0) Then
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
                        thisCountryVals.Province_State = lineParts(5)
                        thisCountryVals.Country_Region = lineParts(6)
                        thisCountryVals.Coordinates = New Tuple(Of Double, Double)(CDbl(lineParts(8)), CDbl(lineParts(9)))
                        If USDeathsHeader Then
                            thisCountryVals.Population = CDbl(lineParts(11))
                        End If
                    Else
                        thisCountryVals.Province_State = lineParts(0)
                        thisCountryVals.Country_Region = lineParts(1)
                        thisCountryVals.Coordinates = New Tuple(Of Double, Double)(CDbl(lineParts(2)), CDbl(lineParts(3)))
                    End If
                    Dim AllValues As New System.Collections.Generic.List(Of Integer)

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
    Public Function GetDailyValues(ByVal valueType As cDisplayInfo.enWorldValueType, ByVal region As cCountryListboxItem) As List(Of cDailyValue)
        Dim retVal As New List(Of cDailyValue)
        Dim targetList As List(Of cCountryValues) = Nothing
        Select Case valueType
            Case cDisplayInfo.enWorldValueType.Confirmed
                targetList = Confirmed
            Case cDisplayInfo.enWorldValueType.Deaths
                targetList = Deaths
            Case cDisplayInfo.enWorldValueType.Recovered
                targetList = Recovered
        End Select

        Dim pop As Double = 1
        If NormalizeToPopulation Then
            pop = Population.GetWorldCountryPopulation(region.Country_Region) / 10000.0
            If pop = 0 Then
                pop = 1
            End If
        End If

        If targetList IsNot Nothing Then
            For iCounter As Integer = 0 To targetList.Count - 1
                If targetList(iCounter).IsRegionLike(region) Then
                    Dim collectionStarted As Boolean = False
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
    Public Sub ShiftDays(ByVal valueType As cDisplayInfo.enWorldValueType, ByVal region As cCountryListboxItem, ByVal days As Integer)
        Try
            Dim targetList As List(Of cCountryValues) = Nothing
            Select Case valueType
                Case cDisplayInfo.enWorldValueType.Confirmed
                    targetList = Confirmed
                Case cDisplayInfo.enWorldValueType.Deaths
                    targetList = Deaths
                Case cDisplayInfo.enWorldValueType.Recovered
                    targetList = Recovered
            End Select

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

