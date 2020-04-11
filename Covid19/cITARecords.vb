Public Class cITARecord
    Public data As New Date(2000, 1, 1)
    Public stato As String = "ITA"
    Public ricoverati_con_sintomi As New Tuple(Of Double, Double)(0, 0)
    Public terapia_intensiva As New Tuple(Of Double, Double)(0, 0)
    Public totale_ospedalizzati As New Tuple(Of Double, Double)(0, 0)
    Public isolamento_domiciliare As New Tuple(Of Double, Double)(0, 0)
    Public totale_positivi As New Tuple(Of Double, Double)(0, 0)
    Public variazione_totale_positivi As New Tuple(Of Double, Double)(0, 0)
    Public nuovi_positivi As New Tuple(Of Double, Double)(0, 0)
    Public dimessi_guariti As New Tuple(Of Double, Double)(0, 0)
    Public deceduti As New Tuple(Of Double, Double)(0, 0)
    Public totale_casi As New Tuple(Of Double, Double)(0, 0)
    Public tamponi As New Tuple(Of Double, Double)(0, 0)
    Public Sub New(ByVal csvLine As String)
        If csvLine.Length > 0 Then
            Dim lineParts() As String = csvLine.Split(",")

            Date.TryParse(lineParts(0), data)
            Dim ts As New TimeSpan(18, 0, 0)
            data = data.Date + ts

            'ricoverati_con_sintomi,terapia_intensiva,totale_ospedalizzati,isolamento_domiciliare,totale_positivi,variazione_totale_positivi,nuovi_positivi,dimessi_guariti,deceduti,totale_casi,tamponi
            stato = lineParts(1)
            ricoverati_con_sintomi = New Tuple(Of Double, Double)(CInt(lineParts(2)), CDbl(lineParts(2)) / cPopulation.ITATotalPopulation)
            terapia_intensiva = New Tuple(Of Double, Double)(CInt(lineParts(3)), CDbl(lineParts(3)) / cPopulation.ITATotalPopulation)
            totale_ospedalizzati = New Tuple(Of Double, Double)(CInt(lineParts(4)), CDbl(lineParts(4)) / cPopulation.ITATotalPopulation)
            isolamento_domiciliare = New Tuple(Of Double, Double)(CInt(lineParts(5)), CDbl(lineParts(5)) / cPopulation.ITATotalPopulation)
            totale_positivi = New Tuple(Of Double, Double)(CInt(lineParts(6)), CDbl(lineParts(6)) / cPopulation.ITATotalPopulation)
            variazione_totale_positivi = New Tuple(Of Double, Double)(CInt(lineParts(7)), CDbl(lineParts(7)) / cPopulation.ITATotalPopulation)
            nuovi_positivi = New Tuple(Of Double, Double)(CInt(lineParts(8)), CDbl(lineParts(8)) / cPopulation.ITATotalPopulation)
            dimessi_guariti = New Tuple(Of Double, Double)(CInt(lineParts(9)), CDbl(lineParts(9)) / cPopulation.ITATotalPopulation)
            deceduti = New Tuple(Of Double, Double)(CInt(lineParts(10)), CDbl(lineParts(10)) / cPopulation.ITATotalPopulation)
            totale_casi = New Tuple(Of Double, Double)(CInt(lineParts(11)), CDbl(lineParts(11)) / cPopulation.ITATotalPopulation)
            tamponi = New Tuple(Of Double, Double)(CInt(lineParts(12)), CDbl(lineParts(12)) / cPopulation.ITATotalPopulation)

        End If
    End Sub
End Class

Public Class cITARecords
    Inherits Generic.List(Of cITARecord)
    Public Sub New(ByVal csvLines() As String)
        For lCounter As Integer = 1 To csvLines.Count - 1 'First line is the header
            Dim newItem As New cITARecord(csvLines(lCounter))
            Me.Add(newItem)
        Next
    End Sub
    Public Sub New()

    End Sub
    Public ReadOnly Property LastDate As Date
        Get
            Dim retVal As New Date(2000, 1, 1)
            For icounter As Integer = 0 To Me.Count - 1
                If Me(icounter).data > retVal Then
                    retVal = Me(icounter).data
                End If
            Next
            Return retVal
        End Get
    End Property
    Public ReadOnly Property StartingDate As Date
        Get
            Dim retVal As New Date(2000, 1, 1)
            If Me.Count > 0 Then
                Return Me(0).data
            Else
                Return New Date(2000, 1, 1)
            End If
        End Get
    End Property
End Class

