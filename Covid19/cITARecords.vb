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
    Public casi_da_sospetto_diagnostico As New Tuple(Of Double, Double)(0, 0)
    Public casi_da_screening As New Tuple(Of Double, Double)(0, 0)
    Public totale_casi As New Tuple(Of Double, Double)(0, 0)
    Public tamponi As New Tuple(Of Double, Double)(0, 0)
    Public Sub New()

    End Sub
    Public Sub New(ByVal csvLine As String, ByVal startDateRecord As cITARecord)
        If csvLine.Length > 0 Then
            If startDateRecord Is Nothing Then
                startDateRecord = New cITARecord
            End If

            Dim lineParts() As String = csvLine.Split(",")

            Date.TryParse(lineParts(0), data)
            Dim ts As New TimeSpan(18, 0, 0)
            data = data.Date + ts

            'ricoverati_con_sintomi,terapia_intensiva,totale_ospedalizzati,isolamento_domiciliare,totale_positivi,variazione_totale_positivi,nuovi_positivi,dimessi_guariti,deceduti,totale_casi,tamponi
            stato = lineParts(1)
            ricoverati_con_sintomi = New Tuple(Of Double, Double)(CInt(lineParts(2)) - startDateRecord.ricoverati_con_sintomi.Item1, (CdblEx(lineParts(2) - startDateRecord.ricoverati_con_sintomi.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            terapia_intensiva = New Tuple(Of Double, Double)(CInt(lineParts(3)) - startDateRecord.terapia_intensiva.Item1, (CdblEx(lineParts(3) - startDateRecord.terapia_intensiva.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            totale_ospedalizzati = New Tuple(Of Double, Double)(CInt(lineParts(4)) - startDateRecord.totale_ospedalizzati.Item1, (CdblEx(lineParts(4) - startDateRecord.totale_ospedalizzati.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            isolamento_domiciliare = New Tuple(Of Double, Double)(CInt(lineParts(5)) - startDateRecord.isolamento_domiciliare.Item1, (CdblEx(lineParts(5) - startDateRecord.isolamento_domiciliare.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            totale_positivi = New Tuple(Of Double, Double)(CInt(lineParts(6)) - startDateRecord.totale_positivi.Item1, (CdblEx(lineParts(6) - startDateRecord.totale_positivi.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            variazione_totale_positivi = New Tuple(Of Double, Double)(CInt(lineParts(7)) - startDateRecord.variazione_totale_positivi.Item1, (CdblEx(lineParts(7) - startDateRecord.variazione_totale_positivi.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            nuovi_positivi = New Tuple(Of Double, Double)(CInt(lineParts(8)) - startDateRecord.nuovi_positivi.Item1, (CdblEx(lineParts(8) - startDateRecord.nuovi_positivi.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            dimessi_guariti = New Tuple(Of Double, Double)(CInt(lineParts(9)) - startDateRecord.dimessi_guariti.Item1, (CdblEx(lineParts(9) - startDateRecord.dimessi_guariti.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            deceduti = New Tuple(Of Double, Double)(CInt(lineParts(10)) - startDateRecord.deceduti.Item1, (CdblEx(lineParts(10) - startDateRecord.deceduti.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            If lineParts(11).Length = 0 Then lineParts(11) = "0"
            If lineParts(12).Length = 0 Then lineParts(12) = "0"

            casi_da_sospetto_diagnostico = New Tuple(Of Double, Double)(CInt(lineParts(11)) - startDateRecord.casi_da_sospetto_diagnostico.Item1, (CdblEx(lineParts(11) - startDateRecord.casi_da_sospetto_diagnostico.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            casi_da_screening = New Tuple(Of Double, Double)(CInt(lineParts(12)) - startDateRecord.casi_da_screening.Item1, (CdblEx(lineParts(12) - startDateRecord.casi_da_screening.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            totale_casi = New Tuple(Of Double, Double)(CInt(lineParts(13)) - startDateRecord.totale_casi.Item1, (CdblEx(lineParts(13) - startDateRecord.totale_casi.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            tamponi = New Tuple(Of Double, Double)(CInt(lineParts(14)) - startDateRecord.tamponi.Item1, (CdblEx(lineParts(14) - startDateRecord.tamponi.Item1) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))

        End If
    End Sub
    Public Sub Subtract(ByVal recordToSubtract As cITARecord)

    End Sub
End Class

Public Class cITARecords
    Inherits Generic.List(Of cITARecord)
    Public Sub New(ByVal csvLines() As String, ByVal startDate As Date)
        Dim firstRecord As cITARecord = Nothing
        For lCounter As Integer = 1 To csvLines.Count - 1 'First line is the header
            If csvLines(lCounter).Trim.Length > 0 Then
                Dim newItem As New cITARecord(csvLines(lCounter), Nothing)
                If newItem.data >= startDate Then
                    'Ok, store this one, with values relative to the first one 
                    newItem = New cITARecord(csvLines(lCounter), firstRecord)
                    Me.Add(newItem)
                Else
                    firstRecord = newItem
                End If
            End If
        Next
    End Sub
    Public Sub New()

    End Sub
    Public Shared ReadOnly Property RestrictionStartDate_ITA As Date
        Get
            Dim restrictionStartDate As New Date(2020, 3, 11)
            Dim ts As New TimeSpan(18, 0, 0)
            restrictionStartDate = restrictionStartDate.Date + ts
            Return restrictionStartDate
        End Get
    End Property

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

