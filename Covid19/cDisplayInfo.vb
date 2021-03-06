﻿Public Class cDisplayInfo
    Public Enum enActiveArea As Integer
        ITA = 0
        ITA_Regions = 1
        ITA_Provinces = 2
        World = 3
        Europe = 4
    End Enum
    Public ActiveArea As enActiveArea = enActiveArea.World
    Public ReadOnly Property ShowITA As Boolean
        Get
            Return (ActiveArea = enActiveArea.ITA) Or (ActiveArea = enActiveArea.ITA_Provinces) Or (ActiveArea = enActiveArea.ITA_Regions)
        End Get
    End Property
    Public ReadOnly Property ShowEurope As Boolean
        Get
            Return ActiveArea = enActiveArea.Europe
        End Get
    End Property
    Public ReadOnly Property ShowWorld As Boolean
        Get
            Return ActiveArea = enActiveArea.World
        End Get
    End Property
    Public Enum enItalianValueType As Integer
        Hospitalized_with_Sypmtoms = 0
        Intensive_Care = 1
        Total_Hospitalized = 2
        Self_Isolating = 3
        Current_Positives = 4
        Current_Positives_Variation = 5
        New_Positives = 6
        Recovered = 7
        Deaths = 8
        Cases_FromSuspectDiagnostics = 9
        Cases_FromScreening = 10
        Total_Cases = 11
        Tests = 12
    End Enum
    Public Enum enWorldValueType
        Deaths = 0
        Confirmed = 1
        Recovered = 2
        FatalityRates = 3
    End Enum
    Public ActiveItalianData As enItalianValueType = enItalianValueType.Deaths
    Public ActiveWorldData As enWorldValueType = enWorldValueType.Deaths
    Public ActiveEUData As enWorldValueType = enWorldValueType.Deaths
    Public ActiveWorldRegions As New List(Of cCountryListboxItem)
    Public ActiveEURegions As New List(Of cCountryListboxItem)
    Public ActiveITARegions As New List(Of cCountryListboxItem)
    Public ActiveITAProvinces As New List(Of cCountryListboxItem)
    Public DailyIncrements As Boolean = False
    Public ShowEstimate As Boolean = False
    Public EstimatedFinalValue As Integer = 1000
    Public EstimatedSigma As Double = 1
    Public EstimatedCurPos100 As Integer = 50
End Class