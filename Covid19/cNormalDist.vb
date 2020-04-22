Public Class cNormalDist
    <DebuggerDisplay("Error:{Cint(EstimateError)}, Sigma={Sigma}, Max={ExpectedMax}, %={Pos100}")> Private Class cEstimateStep
        Public Sigma As Double = SigmaMin
        Public Pos100 As Double = 50
        Public ExpectedMax As Double = 100
        Public EstimateError As Double = 0
    End Class

    Public Delegate Sub SetInfoText_delegate(ByVal newText As String)
    Public Const SigmaMin As Double = 1
    Public Const SigmaMax As Double = 5
    Private myMean As Double = 0
    Private mySigma As Double = 1.5
    Private myInverse2Pi_root As Double = 0
    Private myExpectedMax As Double = 18000
    Private myCurPos100 As Double = 50
    Private myPreCalculatedTable As New List(Of Tuple(Of Double, Double))
    Private Const PrecalculatedTableRange As Double = 20  'From -10 to 10, this needs to be improved as dataseries are likely covering just a part of the curve
    Private myTableStep As Double = 0.01

    Private Property TableStep As Double
        Get
            Return myTableStep
        End Get
        Set(value As Double)
            If value < 0.01 Then
                value = 0.01
            End If
            If value > 1 Then
                value = 1
            End If
            If myTableStep <> value Then
                myTableStep = value
                RecalculateTable()
            End If
        End Set
    End Property
    Private ReadOnly Property PreCalculatedTable As List(Of Tuple(Of Double, Double))
        Get
            If myPreCalculatedTable.Count = 0 Then
                RecalculateTable()
            End If
            Return myPreCalculatedTable
        End Get
    End Property
    Private Sub RecalculateTable()
        myPreCalculatedTable.Clear()
        '1K values
        Dim startPos As Double = -PrecalculatedTableRange / 2
        Dim endPos As Double = PrecalculatedTableRange / 2

        Dim curPos As Double = startPos
        Do While True
            myPreCalculatedTable.Add(New Tuple(Of Double, Double)(curPos, InstantlyCalculatedValue(curPos)))
            curPos = curPos + TableStep
            If curPos >= (endPos + TableStep) Then
                Exit Do
            End If
        Loop
    End Sub
    Public Property Mean As Double
        Get
            Return myMean
        End Get
        Set(value As Double)
            myMean = value
            RecalculateTable()
        End Set
    End Property
    Public Property CurPos100 As Double
        Get
            Return myCurPos100
        End Get
        Set(value As Double)
            myCurPos100 = value
        End Set
    End Property
    Public Property ExpectedMax As Double
        Get
            Return myExpectedMax
        End Get
        Set(value As Double)
            myExpectedMax = value
        End Set
    End Property
    Public Property Sigma As Double
        Get
            Return mySigma
        End Get
        Set(value As Double)
            If value > 1 Then
                mySigma = value
                RecalculateTable()
            End If
        End Set
    End Property
    Private ReadOnly Property Inverse2Pi_Root As Double
        Get
            If myInverse2Pi_root = 0 Then
                myInverse2Pi_root = 1 / Math.Sqrt(2 * Math.PI)
            End If
            Return myInverse2Pi_root
        End Get
    End Property
    Private ReadOnly Property InstantlyCalculatedValue(ByVal x As Double) As Double
        Get
            Dim exp As Double = (x - Mean) / Sigma
            exp = -0.5 * exp * exp
            Dim retVal As Double = (Inverse2Pi_Root / Sigma) * Math.Exp(exp)
            Return retVal
        End Get
    End Property
    Private ReadOnly Property CumulativeValue(ByVal x As Double) As Double
        Get
            Dim retVal As Double = 0
            For iCounter As Integer = 0 To PreCalculatedTable.Count - 1
                If PreCalculatedTable(iCounter).Item1 <= x Then
                    retVal = retVal + PreCalculatedTable(iCounter).Item2 * TableStep
                Else
                    Exit For
                End If
            Next
            Return retVal
        End Get
    End Property
    Public Function GetPlotValues(ByVal dates As System.Collections.Generic.List(Of Date)) As List(Of Tuple(Of Date, Double))
        Dim retVal As New List(Of Tuple(Of Date, Double))
        If dates.Count < 10 Then
            'Not enough values for an estimate
            For dCounter As Integer = 0 To dates.Count - 1
                retVal.Add(New Tuple(Of Date, Double)(dates(dCounter), 0))
            Next
        Else
            Dim deltaStep As Double = PrecalculatedTableRange / (dates.Count - 1)
            deltaStep = deltaStep * CurPos100 / 100
            Dim curPos As Double = -PrecalculatedTableRange / 2
            Dim posCount As Integer = 0
            Do While True
                If posCount > dates.Count - 1 Then
                    Exit Do
                Else
                    curPos = -PrecalculatedTableRange / 2 + posCount * deltaStep
                    ' Debug.Print(CStr(curPos) + "    " + CStr(myExpectedMax * CumulativeValue(curPos)))
                    retVal.Add(New Tuple(Of Date, Double)(dates(posCount), myExpectedMax * CumulativeValue(curPos)))
                End If
                posCount = posCount + 1
            Loop
        End If
        Return retVal
    End Function
    Public Function EstimateDiffFromValues(ByVal dataSeries As List(Of Tuple(Of Date, Double))) As Double
        Dim dates As New System.Collections.Generic.List(Of Date)
        For dCounter As Integer = 0 To dataSeries.Count - 1
            dates.Add(dataSeries(dCounter).Item1)
        Next
        Dim estValues As List(Of Tuple(Of Date, Double)) = GetPlotValues(dates)

        Dim TotalDiff As Double = 0
        For dCounter As Integer = 0 To dataSeries.Count - 1
            If estValues(dCounter).Item2 <> 0 Then
                TotalDiff = TotalDiff + Math.Abs(dataSeries(dCounter).Item2 - estValues(dCounter).Item2)
            End If

        Next
        Return TotalDiff
    End Function
    Public Sub FindBestEstimate(ByVal dataSeries As List(Of Tuple(Of Date, Double)), ByVal callBack As SetInfoText_delegate)
        Try
            If dataSeries.Count < 5 Then
                Return
            End If

            Dim maxLoops As Integer = 100
            Dim loopCounter As Integer = 0
            Dim nullError As Double = 10
            Dim currentDistance As Double = Double.MaxValue
            Dim prevDistance As Double = Double.MaxValue

            'Step 1: coarse analysis 
            Dim sigmaList As New List(Of Double)
            Dim maxList As New List(Of Double)
            Dim pos100List As New List(Of Double)

            Dim tmpSigma As Double = SigmaMin
            Do While True
                sigmaList.Add(tmpSigma)
                tmpSigma = tmpSigma + 0.5
                If tmpSigma > SigmaMax Then
                    Exit Do
                End If
            Loop

            Dim tmpPos100 As Double = 10
            Do While True
                pos100List.Add(tmpPos100)
                tmpPos100 = tmpPos100 + 5
                If tmpPos100 > 100 Then
                    Exit Do
                End If
            Loop

            Dim achievedMax As Double = dataSeries(dataSeries.Count - 1).Item2
            Dim maxEstimateLimit As Double = achievedMax * 5
            Dim maxStep As Double = achievedMax / 5
            Dim tmpMax As Double = achievedMax
            Do While True
                maxList.Add(tmpMax)
                tmpMax = tmpMax + maxStep
                If tmpMax > maxEstimateLimit Then
                    Exit Do
                End If
            Loop

            TableStep = 0.5
            Dim estimateSteps As New List(Of cEstimateStep)
            For sigCounter As Integer = 0 To sigmaList.Count - 1
                For p100Counter As Integer = 0 To pos100List.Count - 1
                    For maxCounter As Integer = 0 To maxList.Count - 1
                        Dim thisEstimate As New cEstimateStep
                        thisEstimate.Sigma = sigmaList(sigCounter)
                        thisEstimate.Pos100 = pos100List(p100Counter)
                        thisEstimate.ExpectedMax = maxList(maxCounter)


                        Me.Sigma = thisEstimate.Sigma
                        Me.CurPos100 = thisEstimate.Pos100
                        Me.ExpectedMax = thisEstimate.ExpectedMax
                        RecalculateTable()
                        thisEstimate.EstimateError = Me.EstimateDiffFromValues(dataSeries)
                        estimateSteps.Add(thisEstimate)
                    Next
                Next
            Next

            If estimateSteps.Count > 0 Then
                Dim minIndex As Integer = 0
                Dim minError As Double = estimateSteps(0).EstimateError
                For sCounter As Integer = 0 To estimateSteps.Count - 1
                    If estimateSteps(sCounter).EstimateError < minError Then
                        minError = estimateSteps(sCounter).EstimateError
                        minIndex = sCounter
                    End If
                Next

                Me.Sigma = estimateSteps(minIndex).Sigma
                Me.CurPos100 = estimateSteps(minIndex).Pos100
                Me.ExpectedMax = estimateSteps(minIndex).ExpectedMax
            End If




            'Step2: fine tuning 
            TableStep = 0.01
            Do While True
                If loopCounter > maxLoops Then
                    Exit Do
                End If
                prevDistance = currentDistance
                callBack("Loop #" + CStr(loopCounter) + ", " + "optimizing Sigma, please wait ...")
                currentDistance = FindBestSigma(dataSeries)
                callBack("Loop #" + CStr(loopCounter) + ", " + "optimizing Expected Max, please wait ...")
                currentDistance = FindBestMax(dataSeries)
                callBack("Loop #" + CStr(loopCounter) + ", " + "optimizing present time, please wait ...")
                currentDistance = FindBestPos100(dataSeries)

                If Math.Abs(currentDistance - prevDistance) < nullError Then
                    'Done, no more improvement
                    Exit Do
                End If
                loopCounter += 1
            Loop


        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Optimize sigma and return the distance from the data series
    ''' </summary>
    ''' <param name="dataSeries"></param>
    ''' <returns></returns>
    Private Function FindBestSigma(ByVal dataSeries As List(Of Tuple(Of Date, Double))) As Double
        Try
            Dim maxLoops As Integer = 1000
            Dim loopCounter As Integer = 0
            Dim nullError As Double = 10
            Dim sigmaStep As Double = 0.2

            Dim ErrPlus As Double = 0
            Dim ErrMinus As Double = 0
            Dim ErrCur As Double = 0

            Do While True
                ErrCur = EstimateDiffFromValues(dataSeries)

                If Sigma > (SigmaMin + sigmaStep) Then
                    Sigma = Sigma - sigmaStep
                    ErrMinus = EstimateDiffFromValues(dataSeries)
                    Sigma = Sigma + sigmaStep
                Else
                    ErrMinus = ErrCur
                End If

                If Sigma < (SigmaMax - sigmaStep) Then
                    Sigma = Sigma + sigmaStep
                    ErrPlus = EstimateDiffFromValues(dataSeries)
                    Sigma = Sigma - sigmaStep
                Else
                    ErrPlus = ErrCur
                End If

                If (ErrCur < nullError) OrElse (loopCounter > maxLoops) Then
                    'Done
                    Return ErrCur
                End If

                If ErrMinus < ErrCur Then
                    'It's good to decrease sigma
                    Sigma = Sigma - sigmaStep
                ElseIf ErrPlus < ErrCur Then
                    'It's good to increase sigma
                    Sigma = Sigma + sigmaStep
                Else
                    'No one smaller
                    Return ErrCur
                End If
                loopCounter += 1
            Loop

            Return EstimateDiffFromValues(dataSeries)
        Catch ex As Exception
            Throw (ex)
            Return 0
        End Try
    End Function
    Private Function FindBestPos100(ByVal dataSeries As List(Of Tuple(Of Date, Double))) As Double
        Try
            Dim maxLoops As Integer = 1000
            Dim loopCounter As Integer = 0
            Dim nullError As Double = 10

            Dim ErrPlus As Double = 0
            Dim ErrMinus As Double = 0
            Dim ErrCur As Double = 0

            Do While True
                ErrCur = EstimateDiffFromValues(dataSeries)

                If CurPos100 > 1 Then
                    CurPos100 = CurPos100 - 1
                    ErrMinus = EstimateDiffFromValues(dataSeries)
                    CurPos100 = CurPos100 + 1
                Else
                    ErrMinus = ErrCur
                End If

                If CurPos100 < 100 Then
                    CurPos100 = CurPos100 + 1
                    ErrPlus = EstimateDiffFromValues(dataSeries)
                    CurPos100 = CurPos100 - 1
                Else
                    ErrPlus = ErrCur
                End If

                If (ErrCur < nullError) OrElse (loopCounter > maxLoops) Then
                    'Done
                    Return ErrCur
                End If

                If ErrMinus < ErrCur Then
                    'It's good to decrease sigma
                    CurPos100 = CurPos100 - 1
                ElseIf ErrPlus < ErrCur Then
                    'It's good to increase sigma
                    CurPos100 = CurPos100 + 1
                Else
                    'No one smaller
                    Return ErrCur
                End If
                loopCounter += 1
            Loop

            Return EstimateDiffFromValues(dataSeries)
        Catch ex As Exception
            Throw (ex)
            Return 0
        End Try
    End Function
    Private Function FindBestMax(ByVal dataSeries As List(Of Tuple(Of Date, Double))) As Double
        Try
            Dim maxLoops As Integer = 1000
            Dim loopCounter As Integer = 0
            Dim nullError As Double = 10
            Dim MaxStep As Double = 10

            Dim expectedAbsoluteMin As Double = dataSeries(dataSeries.Count - 1).Item2 'Already achieved
            Dim expectedAbsoluteMax As Double = dataSeries(dataSeries.Count - 1).Item2 * 20

            Dim ErrPlus As Double = 0
            Dim ErrMinus As Double = 0
            Dim ErrCur As Double = 0

            Do While True
                ErrCur = EstimateDiffFromValues(dataSeries)

                If myExpectedMax > (expectedAbsoluteMin + MaxStep) Then
                    myExpectedMax = myExpectedMax - MaxStep
                    ErrMinus = EstimateDiffFromValues(dataSeries)
                    myExpectedMax = myExpectedMax + MaxStep
                Else
                    ErrMinus = ErrCur
                End If

                If myExpectedMax < (expectedAbsoluteMax - MaxStep) Then
                    myExpectedMax = myExpectedMax + MaxStep
                    ErrPlus = EstimateDiffFromValues(dataSeries)
                    myExpectedMax = myExpectedMax - MaxStep
                Else
                    ErrPlus = ErrCur
                End If

                If (ErrCur < nullError) OrElse (loopCounter > maxLoops) Then
                    'Done
                    Return ErrCur
                End If

                If ErrMinus < ErrCur Then
                    'It's good to decrease sigma
                    myExpectedMax = myExpectedMax - MaxStep
                ElseIf ErrPlus < ErrCur Then
                    'It's good to increase sigma
                    myExpectedMax = myExpectedMax + MaxStep
                Else
                    'No one smaller
                    Return ErrCur
                End If
                loopCounter += 1
            Loop

            Return EstimateDiffFromValues(dataSeries)
        Catch ex As Exception
            Throw (ex)
            Return 0
        End Try
    End Function
End Class
