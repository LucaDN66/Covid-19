﻿Public Class cPopulation
    Public Shared PopulationCSV As String = RootFolder() + "CountryPopulation.csv"
    Public Shared ITAPopulationCSV As String = RootFolder() + "ITAPopulation.csv"
    Public Shared ITAProvincesPopulationCSV As String = RootFolder() + "ITAProvincesPopulation.csv"
    Public Shared UKProvincesPopulationCSV As String = RootFolder() + "UKProvincesPopulation.csv"
    Public Shared USPopulationCSV As String = RootFolder() + "USPopulation.csv"
    Public Shared USCitiesPopulationCSV As String = RootFolder() + "USCitiesPopulation.csv"
    Public Shared EuropeanCountriesCSV As String = RootFolder() + "EuropeanCountries.csv"

    Private myITARegionsPopulation As New List(Of Tuple(Of String, Double))
    Private myITAProvincesPopulation As New List(Of Tuple(Of String, Double))
    Private myGlobalPopulation As New List(Of Tuple(Of String, Double))
    Private myUSPopulation As New List(Of Tuple(Of String, Double))
    Private myUSCitiesPopulation As New List(Of Tuple(Of String, Double))
    Private myUKProvincesPopulation As New List(Of Tuple(Of String, Double))
    Public Const ITATotalPopulation As Double = 60317000
    Public myEuropeanCountries As New List(Of String)
    Public Const PerMillionDivider As Double = 100000.0
    Public Const PerMillionString As String = "Per 100K people"
    Public Const CountryPopulationThreshold = 100000
    Public Sub New()
        Try
            If System.IO.File.Exists(USPopulationCSV) Then
                System.IO.File.Delete(USPopulationCSV)
            End If
            If System.IO.File.Exists(USCitiesPopulationCSV) Then
                System.IO.File.Delete(USCitiesPopulationCSV)
            End If
            If System.IO.File.Exists(EuropeanCountriesCSV) Then
                System.IO.File.Delete(EuropeanCountriesCSV)
            End If
            If System.IO.File.Exists(PopulationCSV) Then
                System.IO.File.Delete(PopulationCSV)
            End If
            If System.IO.File.Exists(ITAPopulationCSV) Then
                System.IO.File.Delete(ITAPopulationCSV)
            End If
            If System.IO.File.Exists(ITAProvincesPopulationCSV) Then
                System.IO.File.Delete(ITAProvincesPopulationCSV)
            End If
            If System.IO.File.Exists(UKProvincesPopulationCSV) Then
                System.IO.File.Delete(UKProvincesPopulationCSV)
            End If

            'Create files from resource 
            Dim thisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Static resourcesNames As String() = thisAssembly.GetManifestResourceNames()
            For Each name As String In resourcesNames
                If (name.EndsWith("ITAPopulation.csv")) OrElse (name.EndsWith("USPopulation.csv")) OrElse (name.EndsWith("EuropeanCountries.csv")) OrElse (name.EndsWith("USCitiesPopulation.csv")) OrElse (name.EndsWith("CountryPopulation.csv")) OrElse (name.EndsWith("ITAProvincesPopulation.csv")) OrElse (name.EndsWith("UKProvincesPopulation.csv")) Then
                    Dim resStream As System.IO.Stream = thisAssembly.GetManifestResourceStream(name)
                    Dim resBytes(resStream.Length - 1) As Byte
                    resStream.Read(resBytes, 0, resStream.Length)
                    Dim fileStream As System.IO.FileStream = Nothing
                    If (name.EndsWith("ITAPopulation.csv")) Then
                        fileStream = New System.IO.FileStream(ITAPopulationCSV, IO.FileMode.Create, IO.FileAccess.Write)
                    ElseIf (name.EndsWith("CountryPopulation.csv")) Then
                        fileStream = New System.IO.FileStream(PopulationCSV, IO.FileMode.Create, IO.FileAccess.Write)
                    ElseIf (name.EndsWith("USPopulation.csv")) Then
                        fileStream = New System.IO.FileStream(USPopulationCSV, IO.FileMode.Create, IO.FileAccess.Write)
                    ElseIf (name.EndsWith("USCitiesPopulation.csv")) Then
                        fileStream = New System.IO.FileStream(USCitiesPopulationCSV, IO.FileMode.Create, IO.FileAccess.Write)
                    ElseIf (name.EndsWith("EuropeanCountries.csv")) Then
                        fileStream = New System.IO.FileStream(EuropeanCountriesCSV, IO.FileMode.Create, IO.FileAccess.Write)
                    ElseIf (name.EndsWith("ITAProvincesPopulation.csv")) Then
                        fileStream = New System.IO.FileStream(ITAProvincesPopulationCSV, IO.FileMode.Create, IO.FileAccess.Write)
                    ElseIf (name.EndsWith("UKProvincesPopulation.csv")) Then
                        fileStream = New System.IO.FileStream(UKProvincesPopulationCSV, IO.FileMode.Create, IO.FileAccess.Write)
                    End If
                    fileStream.Write(resBytes, 0, resBytes.Length)
                    fileStream.Flush()
                    fileStream.Close()
                    fileStream.Dispose()
                End If
            Next

            'Load country population
            Dim population As New List(Of Tuple(Of String, Double))
            Dim popLines As New List(Of String)

            'Global
            popLines.AddRange(System.IO.File.ReadAllLines(PopulationCSV))
            ReplaceCommasInQuotations(popLines, False)
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myGlobalPopulation.Add(thisLineData)
            Next

            'US
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(USPopulationCSV))
            ReplaceCommasInQuotations(popLines, False)
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myUSPopulation.Add(thisLineData)
            Next

            'US Cities
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(USCitiesPopulationCSV))
            ReplaceCommasInQuotations(popLines, False)
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                If thisLineParts(0).Length > 0 Then
                    Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0) + "-" + thisLineParts(1), CInt(thisLineParts(2)))
                    myUSCitiesPopulation.Add(thisLineData)
                Else
                    Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(1), CInt(thisLineParts(2)))
                    myUSCitiesPopulation.Add(thisLineData)
                End If
            Next


            'Windows-1252 to get special characters for european languages

            'ITA regions
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(ITAPopulationCSV, System.Text.Encoding.GetEncoding("Windows-1252")))
            ReplaceCommasInQuotations(popLines, False)
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myITARegionsPopulation.Add(thisLineData)
            Next

            'ITA provinces
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(ITAProvincesPopulationCSV, System.Text.Encoding.GetEncoding("Windows-1252")))
            ReplaceCommasInQuotations(popLines, False)
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myITAProvincesPopulation.Add(thisLineData)
            Next

            'UK provinces
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(UKProvincesPopulationCSV, System.Text.Encoding.GetEncoding("Windows-1252")))
            ReplaceCommasInQuotations(popLines, False)
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(2)))
                myUKProvincesPopulation.Add(thisLineData)
            Next

            'Loads european countries
            myEuropeanCountries.Clear()
            myEuropeanCountries.AddRange(System.IO.File.ReadAllLines(EuropeanCountriesCSV, System.Text.Encoding.GetEncoding("Windows-1252")))


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public ReadOnly Property GetUSCityPopulation(ByVal City_State As String) As Double
        Get
            Dim nameCopy As String = MakeComparable(City_State)
            For pCounter As Integer = 0 To myUSCitiesPopulation.Count - 1
                Dim itemCopy As String = MakeComparable(myUSCitiesPopulation(pCounter).Item1)
                If itemCopy.StartsWith(nameCopy) Then
                    Return myUSCitiesPopulation(pCounter).Item2
                End If
            Next
            Return 0
        End Get
    End Property
    Public ReadOnly Property GetUSStatePopulation(ByVal state As String) As Double
        Get
            Dim stateCopy As String = MakeComparable(state)
            For pCounter As Integer = 0 To myUSPopulation.Count - 1
                Dim itemCopy As String = MakeComparable(myUSPopulation(pCounter).Item1)
                If stateCopy = itemCopy Then
                    Return myUSPopulation(pCounter).Item2
                End If
            Next
            Return 0
        End Get
    End Property
    Public ReadOnly Property GetUKProvincePopulation(ByVal province As String) As Double
        Get
            Dim provinceCopy As String = MakeComparable(province).ToUpper.Trim
            For pCounter As Integer = 0 To myUKProvincesPopulation.Count - 1
                Dim itemCopy As String = MakeComparable(myUKProvincesPopulation(pCounter).Item1)
                If itemCopy.StartsWith(provinceCopy) Then
                    Return myUKProvincesPopulation(pCounter).Item2
                End If
            Next
            Return 0
        End Get
    End Property
    Public ReadOnly Property GetITAProvincePopulation(ByVal province As String) As Double
        Get
            Dim provinceCopy As String = MakeComparable(province)
            For pCounter As Integer = 0 To myITAProvincesPopulation.Count - 1
                Dim itemCopy As String = MakeComparable(myITAProvincesPopulation(pCounter).Item1)
                If itemCopy.StartsWith(provinceCopy) Then
                    Return myITAProvincesPopulation(pCounter).Item2
                End If
            Next
            Return 0
        End Get
    End Property
    Public ReadOnly Property GetITARegionPopulation(ByVal region As String) As Double
        Get
            Dim regionCopy As String = MakeComparable(region)

            For pCounter As Integer = 0 To myITARegionsPopulation.Count - 1
                Dim itemCopy As String = MakeComparable(myITARegionsPopulation(pCounter).Item1)
                If regionCopy = itemCopy Then
                    Return myITARegionsPopulation(pCounter).Item2
                End If
            Next

            If region = "IT-23" Then
                Return GetITARegionPopulation("Valle dAosta")
            End If
            Return 0
        End Get
    End Property
    Public ReadOnly Property GetWorldCountryPopulation(ByVal country As String) As Double
        Get
            Dim regionCopy As String = MakeComparable(country)
            For pCounter As Integer = 0 To myGlobalPopulation.Count - 1
                Dim itemCopy As String = MakeComparable(myGlobalPopulation(pCounter).Item1)
                If itemCopy = regionCopy Then
                    Return myGlobalPopulation(pCounter).Item2
                Else
                    If regionCopy.Contains("-") Then
                        'Maybe it's scrambled up (e.g. South-Korea <> Korea-South)
                        Dim regionParts() As String = regionCopy.Split("-")
                        Dim allFound As Boolean = True
                        For iCounter As Integer = 0 To regionParts.Count - 1
                            If Not itemCopy.Contains(regionParts(iCounter)) Then
                                allFound = False
                            End If
                        Next
                        If allFound Then
                            Return myGlobalPopulation(pCounter).Item2
                        End If
                    End If
                End If
            Next

            Return 0
        End Get
    End Property
End Class
