Imports System.IO
Public Class cIniFileManager
    Private Declare Auto Function GetPrivateProfileString Lib "kernel32.dll" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileString Lib "kernel32.dll" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Shared Function _Read(ByVal Filename As String, ByVal Section As String, ByVal Key As String, ByVal DefaultValue As String) As String
        Try
            If (Filename.Length = 0) OrElse (Section.Length = 0) OrElse (Key.Length = 0) Then
                Return ""
            End If
            Dim Result As String
            Dim RetVal As String = New String(" ", 255)
            Dim LenResult As Integer
            LenResult = GetPrivateProfileString(Section, Key, "", RetVal, RetVal.Length, Filename)
            If LenResult = 0 Then
                If Not (File.Exists(Filename)) Then
                    'Creo il file, se possibile, e aggiungo la chiave...
                    Try
                        _Write(Filename, Section, Key, DefaultValue)
                    Catch ex As Exception
                    End Try
                    Return DefaultValue
                Else
                    'Il file esiste, la chiave no (o forse il file non può essere letto, ma se guardi tutto...)
                    Try
                        _Write(Filename, Section, Key, DefaultValue)
                    Catch ex As Exception
                    End Try
                    Return DefaultValue
                End If
            Else
                'Chiave letta correttamente...
                Result = RetVal.Substring(0, LenResult)
                Return Result
            End If
        Catch ex As Exception
            Return DefaultValue
        End Try
    End Function
    Private Shared Sub _Write(ByVal Filename As String, ByVal Section As String, ByVal Key As String, ByVal Value As String)
        Try
            If (Filename.Length = 0) OrElse (Section.Length = 0) OrElse (Key.Length = 0) OrElse (Value.Length = 0) Then
                Return
            End If
            Dim LenResult As Integer
            If Not System.IO.File.Exists(Filename) Then
                Dim FileDir As String = System.IO.Path.GetDirectoryName(Filename)
                If Not System.IO.Directory.Exists(FileDir) Then
                    System.IO.Directory.CreateDirectory(FileDir)
                End If
                Dim NewStream As System.IO.Stream = System.IO.File.Create(Filename)
                If NewStream IsNot Nothing Then
                    NewStream.Flush()
                    NewStream.Close()
                End If
            End If
            LenResult = WritePrivateProfileString(Section, Key, Value, Filename)
        Catch ex As Exception
            Return
        End Try
    End Sub
    Public Shared Sub WriteString(ByVal SectionName As String, ByVal KeyName As String, ByVal StringValue As String, ByVal IniFileName As String)
        Try
            _Write(IniFileName, SectionName, KeyName, StringValue)
        Catch ex As Exception
            Return
        End Try
    End Sub
    Public Shared Sub WriteDouble(ByVal SectionName As String, ByVal KeyName As String, ByVal Value As Double, ByVal IniFileName As String)
        Try
            _Write(IniFileName, SectionName, KeyName, CStr(Value))
        Catch ex As Exception
            Return
        End Try
    End Sub
    Public Shared Sub WriteBoolean(ByVal SectionName As String, ByVal KeyName As String, ByVal Value As Boolean, ByVal IniFileName As String)
        Try
            If Value Then
                _Write(IniFileName, SectionName, KeyName, "1")
            Else
                _Write(IniFileName, SectionName, KeyName, "0")
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub
    Public Shared Function ReadDouble(ByVal SectionName As String, ByVal KeyName As String, ByVal DefaultValue As Double, ByVal IniFileName As String) As Double
        Try
            Dim retString As String = _Read(IniFileName, SectionName, KeyName, CStr(DefaultValue))
            Dim retVal As Double = DoubleFromString(retString)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Shared Function ReadString(ByVal SectionName As String, ByVal KeyName As String, ByVal strDefault As String, ByVal IniFileName As String) As String
        Try
            Return _Read(IniFileName, SectionName, KeyName, strDefault)
        Catch ex As Exception
            Return strDefault
        End Try
    End Function
    Public Shared Function ReadBoolean(ByVal SectionName As String, ByVal KeyName As String, ByVal DefaultValue As Boolean, ByVal IniFileName As String) As Boolean
        Try
            Dim retString As String = "0"
            If DefaultValue Then
                retString = _Read(IniFileName, SectionName, KeyName, "1")
            Else
                retString = _Read(IniFileName, SectionName, KeyName, "0")
            End If
            If retString = "1" Then
                Return True
            ElseIf retString = "0" Then
                Return False
            Else
                Try
                    Return CBool(retString)
                Catch ex As Exception
                    Return False
                End Try
            End If
        Catch ex As Exception
            Return DefaultValue
        End Try
    End Function
    Public Shared Function DoubleFromString(ByVal str As String) As Double
        Try
            If (str Is Nothing) OrElse (str.Length = 0) Then
                Return 0
            End If

            If str.ToUpper = "DOUBLE.MAXVALUE" Then
                Return Double.MaxValue
            End If
            If str.ToUpper = "DOUBLE.MINVALUE" Then
                Return Double.MinValue
            End If

            Dim myParts() As String = str.Split(";")
            If myParts.Length > 0 Then
                str = myParts(0)
            End If

            If System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator = "." Then
                str = str.Replace(",", ".")
            Else
                str = str.Replace(".", ",")
            End If
            Dim LastChar As Char
            Do While True
                If str.Length > 0 Then
                Else
                    Exit Do
                End If
                LastChar = str.Substring(str.Length - 1, 1)
                If Not IsNumeric(LastChar) Then
                    str = str.TrimEnd(LastChar)
                Else
                    Exit Do
                End If
            Loop
            If str.Length > 0 Then
                If str.ToUpper.Contains("E") Then
                    Dim expNotParts() As String = str.ToUpper.Split("E")
                    Dim retVal As Double = Double.Parse(expNotParts(0), System.Globalization.NumberFormatInfo.CurrentInfo)
                    retVal = retVal * 10 ^ (Double.Parse(expNotParts(1), System.Globalization.NumberFormatInfo.CurrentInfo))
                    Return retVal
                Else
                    Return Double.Parse(str, System.Globalization.NumberFormatInfo.CurrentInfo)
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        End Try
    End Function

End Class
