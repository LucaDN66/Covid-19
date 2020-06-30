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
    Public Sub New(ByVal csvLine As String)
        If csvLine.Length > 0 Then
            Dim lineParts() As String = csvLine.Split(",")

            Date.TryParse(lineParts(0), data)
            Dim ts As New TimeSpan(18, 0, 0)
            data = data.Date + ts

            'ricoverati_con_sintomi,terapia_intensiva,totale_ospedalizzati,isolamento_domiciliare,totale_positivi,variazione_totale_positivi,nuovi_positivi,dimessi_guariti,deceduti,totale_casi,tamponi
            stato = lineParts(1)
            ricoverati_con_sintomi = New Tuple(Of Double, Double)(CInt(lineParts(2)), CdblEx(lineParts(2) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            terapia_intensiva = New Tuple(Of Double, Double)(CInt(lineParts(3)), CdblEx(lineParts(3) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            totale_ospedalizzati = New Tuple(Of Double, Double)(CInt(lineParts(4)), CdblEx(lineParts(4) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            isolamento_domiciliare = New Tuple(Of Double, Double)(CInt(lineParts(5)), CdblEx(lineParts(5) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            totale_positivi = New Tuple(Of Double, Double)(CInt(lineParts(6)), CdblEx(lineParts(6) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            variazione_totale_positivi = New Tuple(Of Double, Double)(CInt(lineParts(7)), CdblEx(lineParts(7) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            nuovi_positivi = New Tuple(Of Double, Double)(CInt(lineParts(8)), CdblEx(lineParts(8) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            dimessi_guariti = New Tuple(Of Double, Double)(CInt(lineParts(9)), CdblEx(lineParts(9) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            deceduti = New Tuple(Of Double, Double)(CInt(lineParts(10)), CdblEx(lineParts(10) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            If lineParts(11).Length = 0 Then lineParts(11) = "0"
            If lineParts(12).Length = 0 Then lineParts(12) = "0"

            casi_da_sospetto_diagnostico = New Tuple(Of Double, Double)(CInt(lineParts(11)), CdblEx(lineParts(11) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            casi_da_screening = New Tuple(Of Double, Double)(CInt(lineParts(12)), CdblEx(lineParts(12) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            totale_casi = New Tuple(Of Double, Double)(CInt(lineParts(13)), CdblEx(lineParts(13) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))
            tamponi = New Tuple(Of Double, Double)(CInt(lineParts(14)), CdblEx(lineParts(14) / cPopulation.ITATotalPopulation * cPopulation.PerMillionDivider))

        End If
    End Sub
End Class

Public Class cITARecords
    Inherits Generic.List(Of cITARecord)
    Public Sub New(ByVal csvLines() As String)
        For lCounter As Integer = 1 To csvLines.Count - 1 'First line is the header
            If csvLines(lCounter).Trim.Length > 0 Then
                Dim newItem As New cITARecord(csvLines(lCounter))
                Me.Add(newItem)
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

