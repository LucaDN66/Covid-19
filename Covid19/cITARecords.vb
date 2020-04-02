Public Class cITARecord
    Public data As New Date(2000, 1, 1)
    Public stato As String = "ITA"
    Public ricoverati_con_sintomi As Integer = 0
    Public terapia_intensiva As Integer = 0
    Public totale_ospedalizzati As Integer = 0
    Public isolamento_domiciliare As Integer = 0
    Public totale_positivi As Integer = 0
    Public variazione_totale_positivi As Integer = 0
    Public nuovi_positivi As Integer = 0
    Public dimessi_guariti As Integer = 0
    Public deceduti As Integer = 0
    Public totale_casi As Integer = 0
    Public tamponi As Integer = 0
    Public Sub New(ByVal csvLine As String)
        If csvLine.Length > 0 Then
            Dim lineParts() As String = csvLine.Split(",")

            Date.TryParse(lineParts(0), data)
            Dim ts As New TimeSpan(18, 0, 0)
            data = data.Date + ts

            'ricoverati_con_sintomi,terapia_intensiva,totale_ospedalizzati,isolamento_domiciliare,totale_positivi,variazione_totale_positivi,nuovi_positivi,dimessi_guariti,deceduti,totale_casi,tamponi
            stato = lineParts(1)
            ricoverati_con_sintomi = CInt(lineParts(2))
            terapia_intensiva = CInt(lineParts(3))
            totale_ospedalizzati = CInt(lineParts(4))
            isolamento_domiciliare = CInt(lineParts(5))
            totale_positivi = CInt(lineParts(6))
            variazione_totale_positivi = CInt(lineParts(7))
            nuovi_positivi = CInt(lineParts(8))
            dimessi_guariti = CInt(lineParts(9))
            deceduti = CInt(lineParts(10))
            totale_casi = CInt(lineParts(11))
            tamponi = CInt(lineParts(12))

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

