Public Class cPopulation
    Public Shared PopulationCSV As String = RootFolder() + "CountryPopulation.csv"
    Public Shared ITAPopulationCSV As String = RootFolder() + "ITAPopulation.csv"
    Public Shared ITAProvincesPopulationCSV As String = RootFolder() + "ITAProvincesPopulation.csv"
    Public Shared USPopulationCSV As String = RootFolder() + "USPopulation.csv"
    Private myITAPopulation As New List(Of Tuple(Of String, Double))
    Private myITAProvincesPopulation As New List(Of Tuple(Of String, Double))
    Private myGlobalPopulation As New List(Of Tuple(Of String, Double))
    Private myUSPopulation As New List(Of Tuple(Of String, Double))
    Public Const ITATotalPopulation As Double = 60317000
    Public Sub New()
        Try
            If System.IO.File.Exists(USPopulationCSV) Then
                System.IO.File.Delete(USPopulationCSV)
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

            'Create files from resource 
            Dim thisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Static resourcesNames As String() = thisAssembly.GetManifestResourceNames()
            For Each name As String In resourcesNames
                If (name.EndsWith("ITAPopulation.csv")) OrElse (name.EndsWith("USPopulation.csv")) OrElse (name.EndsWith("CountryPopulation.csv")) OrElse (name.EndsWith("ITAProvincesPopulation.csv")) Then
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
                    ElseIf (name.EndsWith("ITAProvincesPopulation.csv")) Then
                        fileStream = New System.IO.FileStream(ITAProvincesPopulationCSV, IO.FileMode.Create, IO.FileAccess.Write)
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
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myGlobalPopulation.Add(thisLineData)
            Next

            'US
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(USPopulationCSV))
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myUSPopulation.Add(thisLineData)
            Next

            'ITA regions
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(ITAPopulationCSV))
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myITAPopulation.Add(thisLineData)
            Next

            'ITA provinces
            popLines.Clear()
            popLines.AddRange(System.IO.File.ReadAllLines(ITAProvincesPopulationCSV))
            For lCounter As Integer = 0 To popLines.Count - 1
                Dim thisLineParts() As String = popLines(lCounter).Split(",")
                Dim thisLineData As New Tuple(Of String, Double)(thisLineParts(0), CInt(thisLineParts(1)))
                myITAProvincesPopulation.Add(thisLineData)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public ReadOnly Property GetUSPopulation(ByVal state As String) As Double
        Get
            Dim stateCopy As String = state.Replace(" ", "-").ToUpper.Trim
            For pCounter As Integer = 0 To myUSPopulation.Count - 1
                Dim itemCopy As String = myUSPopulation(pCounter).Item1.Replace(" ", "-").ToUpper.Trim
                If stateCopy = itemCopy Then
                    Return myUSPopulation(pCounter).Item2
                End If
            Next
            Return 0
        End Get
    End Property

    Public ReadOnly Property GetITAProvincePopulation(ByVal province As String) As Double
        Get
            Dim provinceCopy As String = province.Replace(" ", "-").ToUpper.Trim
            For pCounter As Integer = 0 To myITAProvincesPopulation.Count - 1
                Dim itemCopy As String = myITAProvincesPopulation(pCounter).Item1.Replace(" ", "-").ToUpper.Trim
                If itemCopy.StartsWith(provinceCopy) Then
                    Return myITAProvincesPopulation(pCounter).Item2
                End If
            Next
            Return 0
        End Get
    End Property
    Public ReadOnly Property GetITARegionPopulation(ByVal region As String) As Double
        Get
            Dim regionCopy As String = region.Replace(" ", "-").ToUpper.Trim

            For pCounter As Integer = 0 To myITAPopulation.Count - 1
                Dim itemCopy As String = myITAPopulation(pCounter).Item1.Replace(" ", "-").ToUpper.Trim
                If regionCopy = itemCopy Then
                    Return myITAPopulation(pCounter).Item2
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
            Dim regionCopy As String = country.Replace(" ", "-").ToUpper.Trim
            For pCounter As Integer = 0 To myGlobalPopulation.Count - 1
                Dim itemCopy As String = myGlobalPopulation(pCounter).Item1.Replace(" ", "-").ToUpper.Trim
                If itemCopy = regionCopy Then
                    Return myGlobalPopulation(pCounter).Item2
                End If
            Next
            Return 0
        End Get
    End Property
End Class
