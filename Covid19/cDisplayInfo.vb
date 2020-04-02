Public Class cDisplayInfo
    Public Enum enActiveArea As Integer
        ITA = 0
        ITA_Regions = 1
        ITA_Provinces = 2
        World = 3
        US = 4
        UK = 5
    End Enum
    Public ActiveArea As enActiveArea = enActiveArea.ITA
    Public ReadOnly Property ShowITA As Boolean
        Get
            Return (ActiveArea = enActiveArea.ITA) Or (ActiveArea = enActiveArea.ITA_Provinces) Or (ActiveArea = enActiveArea.ITA_Regions)
        End Get
    End Property
    Public ReadOnly Property ShowUS As Boolean
        Get
            Return ActiveArea = enActiveArea.US
        End Get
    End Property
    Public ReadOnly Property ShowWorld As Boolean
        Get
            Return ActiveArea = enActiveArea.World
        End Get
    End Property
    Public Enum enItalianValueType As Integer
        ricoverati_con_sintomi = 0
        terapia_intensiva = 1
        totale_ospedalizzati = 2
        isolamento_domiciliare = 3
        totale_positivi = 4
        variazione_totale_positivi = 5
        nuovi_positivi = 6
        dimessi_guariti = 7
        deceduti = 8
        totale_casi = 9
        tamponi = 10
    End Enum
    Public Enum enWorldValueType
        Deaths = 0
        Confirmed = 1
        Recovered = 2
    End Enum
    Public ActiveItalianData As enItalianValueType = enItalianValueType.deceduti
    Public ActiveWorldData As enWorldValueType = enWorldValueType.Deaths
    Public ActiveUSData As enWorldValueType = enWorldValueType.Deaths
    Public ActiveWorldRegions As New List(Of cCountryListboxItem)
    Public ActiveUSRegions As New List(Of cCountryListboxItem)
    Public ActiveITARegions As New List(Of cCountryListboxItem)
    Public ActiveITAProvinces As New List(Of cCountryListboxItem)
    Public DailyIncrements As Boolean = False
    Public ShowEstimate As Boolean = False
    Public EstimatedFinalValue As Integer = 1000
    Public EstimatedSigma = 1
    Public EstimatedCurPos100 = 50
End Class