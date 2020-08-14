Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports System.Timers
Imports System.Text

Public  Class ClassFEASA
    'classe per la gestione della Com 
    'Il pic dovra` avere sempre la con ID=0 
    Private TempBuf As String
    Private TempBufValido As String
    Private StartRx As Boolean = False
    Public WithEvents TimerMsg As New Timer
    Private EndRx As Boolean = False
    Private TempoTimeout As Integer
    Private DatoValido As New ArrayList
    Public Rs232 As New List(Of Serial)
    Private DefaultComIndex As Integer = 0
    Public ANSI As Encoding = Encoding.GetEncoding(1252) '拉丁字符集
    Private RecCmds(0) As Byte
    'Private TempBuf As String
    Private StartCHR As String
    Private EndCHR As String
    Private FailCHR As String
    Private NumCHR As Integer
    Private _IncludeEndFlag As Boolean
    Private _IncludeStartFlag As Boolean


    Public Enum EBrightLevel
        Auto = 0
        Level1 = 1
        Level2 = 2
        Level3 = 3
        Level4 = 4
        Level5 = 5
    End Enum


    Public Sub New()

    End Sub

    Public Enum OpenFile
        Append = True
        Write = False
    End Enum
    Public Enum Verso
        PcTrasmette = 0
        PcRiceve = 1
    End Enum
#Region "Base Function"
    Public Sub AperturaCom(ByVal SetCom As List(Of String))
        Dim Risposta As String
        Dim NewCom As Serial
        Try
            NewCom = New Serial
            NewCom.Index = SetCom(0)
            NewCom.Address = SetCom(1)
            NewCom.PortName = SetCom(2)
            NewCom.BaudRate = SetCom(3)
            NewCom.DataBits = SetCom(4)
            NewCom.Parity = SetCom(5) 'Parity.None =0
            NewCom.StopBits = SetCom(6) 'StopBits.One 1
            NewCom.Encoding = System.Text.Encoding.GetEncoding(1252) 'per inviare caratteri Asci>128
            NewCom.ReadTimeout = 1000
            NewCom.DtrEnable = SetCom(7)
            NewCom.DtrEnable = SetCom(8)
            NewCom.ReceivedBytesThreshold = 1
            AddHandler NewCom.DataReceived, AddressOf Com_DataReceived
            Rs232.Add(NewCom)
            'NewCom.Open()
        Catch
            MsgBox("Cannot open com " & SetCom(2), MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Function Riceve_Standard_M(Optional ByVal Addr As Integer = 0) As String
        DefaultComIndex = Addr
        Dim ReadOK As Boolean = False
        Dim StrRead As String = ""
        Dim OKMessage As String = ""
        Dim RecCmds(0) As Byte
        Try
            StrRead = Rs232(Addr).ReadExisting
        Catch
            StrRead = ""
        End Try
        Return StrRead
    End Function

    Public Sub Com_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)

    End Sub


    Public Function OpenCom(Optional ByVal Addr As Integer = 0) As Boolean
        Dim Res As Boolean = False
        If Addr >= 0 Then DefaultComIndex = Addr
        Try
            Rs232(DefaultComIndex).Open()
            Res = True
        Catch ex As Exception
            Res = False
        End Try
        Return Res
    End Function

    Public Function CloseCom(Optional ByVal Addr As Integer = 0) As Boolean
        Dim Res As Boolean = False
        Try
            Rs232(DefaultComIndex).Close()
            Res = True
        Catch ex As Exception
            Res = False
        End Try
        Return Res
    End Function

    Public Sub Sleep(ByVal Ms As Integer)
        System.Threading.Thread.Sleep(Ms)
    End Sub

    Private Sub TimerMsg_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerMsg.Disposed

    End Sub

    Private Sub TimerMsg_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerMsg.Elapsed

    End Sub

    Public Function Send(ByVal strCmd As String, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Res As Boolean = False
        Dim CmdByte() As Byte
        Try
            CLog4Net.Logs4Net(CLog4Net.LogLevel._Debug, strCmd)
            CmdByte = ANSI.GetBytes(strCmd)
            Rs232(DefaultComIndex).DiscardInBuffer()
            Rs232(DefaultComIndex).DiscardOutBuffer()
            Call Rs232(DefaultComIndex).Write(CmdByte, 0, CmdByte.Length)
            Res = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        Return Res
    End Function

    Public Function Ricevi(Optional ByVal Tempo As Single = 1.0, Optional ByVal IncludeEndFlag As Boolean = False, Optional ByVal IncludeStartFlag As Boolean = False, Optional ByVal Addr As Integer = -1) As String
        Dim Dato As String = ""
        Tempo = Tempo * 1000 'ms
        If Addr >= 0 Then DefaultComIndex = Addr
        Rs232(DefaultComIndex).ReadTimeout = Tempo
        _IncludeEndFlag = IncludeEndFlag
        _IncludeStartFlag = IncludeStartFlag

        Dato = RiceviData()
        CLog4Net.Logs4Net(CLog4Net.LogLevel._Debug, Dato)

        Return Dato
    End Function

    Public Function Ricevi(ByVal Tempo As Single, ByVal Addr As Integer, Optional ByVal STX As String = "", Optional ByVal ETX As String = "", Optional ByVal FTX As String = "", Optional ByVal IncludeEndFlag As Boolean = False, Optional ByVal IncludeStartFlag As Boolean = False) As String
        Dim preComIndex As Integer = Me.DefaultComIndex
        Dim startCHR As String = Me.StartCHR
        Dim endCHR As String = Me.EndCHR

        Tempo = Tempo * 1000
        Me.Rs232.Item(Me.DefaultComIndex).ReadTimeout = CInt(Math.Round(CDbl(Tempo)))
        Me._IncludeEndFlag = IncludeEndFlag
        Me._IncludeStartFlag = IncludeStartFlag
        Me.StartCHR = STX
        Me.EndCHR = ETX
        Me.FailCHR = FTX
        Dim str4 As String = Me.RiceviData

        Me.DefaultComIndex = Addr
        Me.StartCHR = startCHR
        Me.EndCHR = endCHR
        Me.FailCHR = FailCHR
        Return str4
    End Function

    Private Function RiceviData() As String
        Dim Dato As String = ""
        'Wait data coming

        Call Riceve_Standard()

        If DatoValido.Count > 0 Then
            Dato = DatoValido.Item(0)
            DatoValido.Clear()
        End If
        Return Dato
    End Function

    Private Sub Riceve_Standard()
        Dim ReadOK As Boolean = False
        Dim StrRead As String = ""
        Dim OKMessage As String = ""
        Dim PreComIndex As Integer = DefaultComIndex
        Try
            If StartCHR <> "" Then
                While Not ReadOK
                    RecCmds(0) = Rs232(PreComIndex).ReadByte
                    StrRead &= ANSI.GetString(RecCmds)
                    If StrRead.Contains(StartCHR) Then ReadOK = True
                End While
                If ReadOK Then
                    StrRead = "" 'Èç¹û¿ªÊ¼±ê¼ÇÎª·Ç¿Õ£¬ÔòÈ¥³ý
                End If
            Else
                ReadOK = True
            End If

            If ReadOK Then
                ReadOK = False       'Reset Flag
                If EndCHR <> "" Then
                    While Not ReadOK
                        RecCmds(0) = Rs232(PreComIndex).ReadByte
                        StrRead &= ANSI.GetString(RecCmds)
                        If StrRead.Contains(EndCHR) Then ReadOK = True
                    End While
                Else
                    While Not ReadOK ''Èç¹ûÃ»ÓÐÖ¸¶¨½áÊø±ê¼ÇÎ»£¬ÔòÒ»Ö±¶Áµ½bufferÖÐ²»ÔÙÓÐÊý¾ÝÎªÖ¹¡£
                        RecCmds(0) = Rs232(PreComIndex).ReadByte
                        StrRead &= ANSI.GetString(RecCmds)
                        If Rs232(PreComIndex).BytesToRead <= 0 Then
                            Sleep(20)
                            If Rs232(PreComIndex).BytesToRead <= 0 Then
                                ReadOK = True
                            End If
                        End If
                    End While
                End If
            End If
        Catch ex As Exception
        End Try

        If ReadOK Or StrRead.Length > 0 Then
            OKMessage = StrRead
            Try
                OKMessage = StrRead.Substring(0, StrRead.Length - EndCHR.Length)
            Catch ex As Exception
            End Try
            DatoValido.Add(OKMessage)
        End If
    End Sub
#End Region

#Region "FEASA Commands"
    Public Function GetSerial(ByRef SerialName As String, Optional ByVal Addr As Integer = -1) As String
        Dim Risposta As String
        Dim Retry As Integer = 1
        Dim RetryTime As Integer = 10
        Dim TestOk As Boolean = False
        Dim Result As Integer = 0
        Dim Cmd As String = "getserial" + Chr(13)
        'TestOk = Me.Busfree()
        'Sleep(200)
        'If Addr >= 0 Then DefaultComIndex = Addr

        'If TestOk = False Then
        '    Return False
        'End If
        TestOk = False
        Try
            While Retry <= RetryTime And Not TestOk
                Me.Send(Cmd, DefaultComIndex)
                Risposta = Ricevi(2)
                If Risposta.Length < 5 Then
                    TestOk = False
                Else
                    SerialName = Risposta.Replace(" ", "")
                    TestOk = True
                End If
                If Not TestOk Then
                    Retry += 1
                End If
            End While

            If TestOk = False Then
                Result = 0
                MsgBox("Selection Feaasa comunication Errror")
            Else
                Result = 1
            End If
        Catch
            Result = 0
        End Try
        Return Result
    End Function

    Public Function BusgetFeasa(ByVal FeasaName As String, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Risposta As String
        Dim Retry As Integer = 1
        Dim RetryTime As Integer = 10
        Dim TestOk As Boolean = False
        Dim Result As Integer = 0
        Dim Cmd As String = "busget" + FeasaName + Chr(13)
        TestOk = Me.Busfree()

        If TestOk = False Then
            Return False
        End If
        TestOk = False
        Try
            While Retry <= RetryTime And Not TestOk
                Me.Send(Cmd, DefaultComIndex)
                Risposta = Ricevi(2)
                If Risposta.Substring(0, 2) <> "OK" Then
                    TestOk = False
                Else
                    TestOk = True
                End If
                If Not TestOk Then
                    Retry += 1
                End If
            End While

            If TestOk = False Then
                Result = 0
                MsgBox("Selection Feaasa comunication Errror")
            Else
                Result = 1
            End If
        Catch
            Result = 0
        End Try
        Return Result
    End Function

    Public Function Busfree(Optional ByVal Addr As Integer = 0) As Boolean
        Dim Risposta As String
        Dim Retry As Integer = 1
        Dim RetryTime As Integer = 10
        Dim TestOk As Boolean = False
        Dim Result As Integer = 0
        Dim Cmd As String = "busfree" + Chr(13)

        Try
            While Retry <= RetryTime And Not TestOk
                Me.Send(Cmd, DefaultComIndex)
                Risposta = Ricevi(2)
                If Risposta.Substring(0, 2) <> "OK" Then
                    TestOk = False
                Else
                    TestOk = True
                End If
                If Not TestOk Then
                    Retry += 1
                End If
            End While

            If TestOk = False Then
                Result = 0
                MsgBox("Bus Free Errror")
            Else
                Result = 1
            End If
        Catch
            Result = 0
        End Try
        Return Result
    End Function

    Public Function Capture(Optional ByVal brightlevel As EBrightLevel = EBrightLevel.Auto, Optional ByVal Addr As Integer = 0) As Boolean
        Dim IsSuccess As Boolean = True
        Dim cmd As String = ""
        Select Case brightlevel
            Case EBrightLevel.Level1
                cmd = "Capture1"
            Case EBrightLevel.Level2
                cmd = "Capture2"
            Case EBrightLevel.Level3
                cmd = "Capture3"
            Case EBrightLevel.Level4
                cmd = "Capture4"
            Case EBrightLevel.Level5
                cmd = "Capture5"
            Case Else
                cmd = "Capture"
        End Select
        cmd = cmd + vbCrLf

        Send(cmd, Addr)
        Dim res As String = Ricevi(2)

        If res.Contains("OK") Then
            IsSuccess = True
        Else
            IsSuccess = False
        End If
        Return IsSuccess
    End Function

    Public Function GetHSIAll(ByVal ChNum As Integer, ByRef HValue() As Double, ByRef SValue() As Integer, ByRef IValue() As Integer,
                                      Optional ByVal brightLevel As EBrightLevel = EBrightLevel.Auto, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Risposta As String
        Dim Retry As Integer = 1
        Dim RetryTime As Integer = 10
        Dim TestOk As Boolean = False
        Dim Result As Integer = 0
        Dim Cmd As String = ""
        'TestOk = Me.Busfree()

        ReDim HValue(ChNum - 1)
        ReDim SValue(ChNum - 1)
        ReDim IValue(ChNum - 1)

        TestOk = False
       Try
            Cmd = "GETHSIAll" & vbCrLf
            Call Send(Cmd, Addr)
            Threading.Thread.Sleep(100)
            Risposta = Ricevi(2)
            Dim RawData() As String = Risposta.Replace(vbCrLf, "-").Split("-")
            Dim ChPos As Integer = 0
            For i As Integer = 0 To ChNum - 1
                ChPos = RawData(i).Replace("-", "").IndexOf((i + 1).ToString("00"))
                HValue(i) = Convert.ToDouble(RawData(i).Substring(ChPos + 3, 6))
                SValue(i) = Convert.ToInt32(RawData(i).Substring(ChPos + 10, 3))
                IValue(i) = Convert.ToInt32(RawData(i).Substring(ChPos + 14, 5))
            Next
            TestOk = True
        Catch
            TestOk = False
        End Try
        Return TestOk
    End Function

    Public Function GetAbsoluIntensity(ByVal ChNum As Integer, ByRef AbsIntens() As Double,
                                      Optional ByVal brightLevel As EBrightLevel = EBrightLevel.Auto, Optional ByVal Addr As Integer = 0, Optional ByVal SITENo As Integer = 1) As Boolean
        Dim Risposta As String
        Dim Retry As Integer = 1
        Dim RetryTime As Integer = 10
        Dim TestOk As Boolean = False
        Dim Result As Integer = 0
        Dim Cmd As String = ""
        'TestOk = Me.Busfree()
        TestOk = True
        'If Not Capture(brightLevel) Then
        '    TestOk = False
        '    Return TestOk
        'End If

        'Dim wr As IO.StreamWriter
        'wr = New IO.StreamWriter("C:/" & SITENo & ".txt", True)
        'wr.Flush()

        ReDim AbsIntens(ChNum - 1)
        If TestOk = False Then
            Return False
        End If
        TestOk = False
        Try
            Cmd = "GETABSINTAll" & vbCrLf
            Call Send(Cmd, Addr)
            Threading.Thread.Sleep(100)
            Risposta = Ricevi(2)
            'Dim RawData() As String = Risposta.Replace(vbCrLf, "-").Split("-")
            Dim RawData() As String = Risposta.Split(vbCrLf)
            'wr.WriteLine(Risposta)

            Dim ChPos As Integer = 0
            For i As Integer = 0 To ChNum - 1
                Try
                    ChPos = RawData(i).Replace("-", "").IndexOf((i + 1).ToString("00"))
                    'wr.WriteLine(ChNum & ":" & RawData(i))
                    AbsIntens(i) = FormatNumber(CType(RawData(i).Substring(ChPos + 3, 10), Double), 2)

                Catch
                    AbsIntens(i) = "-1"
                    'MsgBox(RawData(i))
                End Try
                TestOk = True
            Next
            'wr.Close()
        Catch
            TestOk = False
        End Try
        Return TestOk
    End Function

    Public Function GetWavlength(ByVal ChNum As Integer, ByRef Wavlengths() As Integer,
                                      Optional ByVal brightLevel As EBrightLevel = EBrightLevel.Auto, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Risposta As String
        Dim Retry As Integer = 1
        Dim RetryTime As Integer = 10
        Dim TestOk As Boolean = False
        Dim Result As Integer = 0
        Dim Cmd As String = ""
        'TestOk = Me.Busfree()
        TestOk = True
        'If Not Capture(brightLevel) Then
        '    TestOk = False
        '    Return TestOk
        'End If

        ReDim Wavlengths(ChNum - 1)
        If TestOk = False Then
            Return False
        End If
        TestOk = False
        Try
            Cmd = "GETWAVELENGTHAll" & vbCrLf

            Call Send(Cmd, Addr)
            Threading.Thread.Sleep(100)
            Risposta = Ricevi(2)

            Dim RawData() As String = Risposta.Replace(vbCrLf, "-").Split("-")
            Dim ChPos As Integer = 0
            For i As Integer = 0 To ChNum - 1
                Try
                    ChPos = RawData(i).Replace("-", "").IndexOf((i + 1).ToString("00"))
                    ''AbsIntens(i) = Convert.ToDouble(RawData(i).Substring(ChPos + 3, 6))
                    Wavlengths(i) = CType(RawData(i).Substring(ChPos + 3, 3), Integer)
                Catch
                    Wavlengths(i) = "-1"
                End Try
                TestOk = True
            Next
        Catch
            TestOk = False
        End Try
        Return TestOk
    End Function


    Public Structure CxCy
        Dim Cx As Double
        Dim Cy As Double
    End Structure

    Public Function GetCxCy(ByVal ChNum As Integer, ByRef CxValue() As Double, ByRef CyValue() As Double,
                                      Optional ByVal brightLevel As EBrightLevel = EBrightLevel.Auto, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Risposta As String
        Dim Retry As Integer = 1
        Dim RetryTime As Integer = 10
        Dim TestOk As Boolean = False
        Dim Result As Integer = 0
        Dim Cmd As String = ""
        'TestOk = Me.Busfree()
        TestOk = True

        ReDim CxValue(ChNum - 1)
        ReDim CyValue(ChNum - 1)
        If TestOk = False Then
            Return False
        End If
        TestOk = False
        Try
            Cmd = "GETXYALL" & vbCrLf

            Call Send(Cmd, Addr)
            Threading.Thread.Sleep(100)
            Risposta = Ricevi(2)

            Dim RawData() As String = Risposta.Replace(vbCrLf, "-").Split("-")
            Dim ChPos As Integer = 0
            For i As Integer = 0 To ChNum - 1
                Try
                    ChPos = RawData(i).Replace("-", "").IndexOf((i + 1).ToString("00"))
                    CxValue(i) = Convert.ToDouble(RawData(i).Substring(ChPos + 3, 6))
                    CyValue(i) = Convert.ToDouble(RawData(i).Substring(ChPos + 10, 6))
                Catch
                    CxValue(i) = -1
                    CyValue(i) = -1
                End Try
            Next
            TestOk = True
        Catch
            TestOk = False
        End Try
        Return TestOk
    End Function

    Public Function CalibrationWavelength(ByVal CH As Integer, ByVal ValiWavSet As Double, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Cmd As String = ""
        Dim Risposta As String = ""
        Dim Res As Boolean = False
        Dim OffsetWav As Double
        Dim StrOffset As String

        Dim ValiWavRead As Integer = 0
        Dim PercDif As Double

        Cmd = "PUTWAVELENGTHOFFSET" & CH.ToString("00") & "+00" & Chr(13)

        Call Send(Cmd, Addr)
        Risposta = Ricevi(2)

        If Risposta.Trim <> "OK" Then
            MsgBox("Calibration" & CH.ToString("00") & " Wavelength Error")
            Return False
        End If

        Res = False

        Cmd = "GETWAVELENGTH" & CH.ToString("00") & Chr(13)

        Call Send(Cmd, Addr)
        Risposta = Ricevi(2)
        ValiWavRead = CType(Risposta.Substring(0, 3), Integer)

        OffsetWav = Int(ValiWavSet - ValiWavRead)

        If OffsetWav > 0 Then
            StrOffset = "+" & OffsetWav.ToString.Substring(0, OffsetWav.ToString.Length).PadLeft(2, "0")
        Else
            StrOffset = "-" & Math.Abs(OffsetWav).ToString.PadLeft(2, "0")
        End If

        If OffsetWav > 50 Then
            MsgBox("Cannot Calibration" & CH.ToString("00") & " Wavelenght verify if led is switch off ")
        Else
            Cmd = "PUTWAVELENGTHOFFSET" & CH.ToString("00") & StrOffset & Chr(13)
            Call Send(Cmd, Addr)
            Risposta = Ricevi(2)
        End If
        If Risposta.Trim <> "OK" Then
            MsgBox("Calibration" & CH.ToString("00") & " Wavelength Error!")
            Return False
        Else
            Res = True
        End If
        Return Res
    End Function

    Public Function CalibrationAbsIntensity(ByVal CH As Integer, ByVal ValiWave As Double, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Cmd As String = ""
        Dim Risposta As String = ""
        Dim Res As Boolean = False
        Dim OffsetIntensity As String = ""
        Dim ValiIntensityRead As Integer = 0
        Dim PercDif As Double

        Cmd = "PUTABSINTMULT" & CH.ToString("00") & "1.00000E+00" & Chr(13)
        Call Send(Cmd, Addr)
        Risposta = Ricevi(2)
        If Risposta.Trim <> "OK" Then
            MsgBox("Calibration" & CH.ToString("00") & "Intensity Error")
            Return False
        End If

        Res = False

        Cmd = "GETABSINT" & CH.ToString("00") & Chr(13)
        Call Send(Cmd, Addr)
        Risposta = Ricevi(2)
        If IsNumeric(Risposta) Then
            ValiIntensityRead = CType(Risposta, Double)
        Else
            MsgBox("Calibration AbsIntensity " & CH.ToString("00") & " UnderRange")
            Return False
        End If

        OffsetIntensity = Format(ValiWave / ValiIntensityRead, "0.00000E+00")

        Cmd = "PUTABSINTMULT" & CH.ToString("00") & OffsetIntensity & Chr(13)
        Call Send(Cmd, Addr)
        Risposta = Ricevi(2)

        If Risposta.Trim <> "OK" Then
            MsgBox("Calibration" & CH.ToString("00") & " Intensity Error")
            Return False
        End If

        Res = False

        Cmd = "GETABSINT" & CH.ToString("00") & Chr(13)
        Call Send(Cmd, Addr)

        Risposta = Ricevi(2)
        PercDif = Math.Abs((ValiWave - CType(Risposta, Integer)) * 100 / ValiWave)

        If PercDif > 10 Then
            MsgBox("Calibration" & CH.ToString("00") & " Intensity Error")
            Return False
        Else
            Res = True
            'MsgBox("Calibration" & CH.ToString("00") & " Intensity Success")
            'MsgPrintLog("Calibration" & CH.ToString("00") & "Intensity Success", 0)
        End If
        Return Res
    End Function

    Public Function CalibrationCxCy(ByVal Ch As Integer, ByVal Cx As Double, ByVal Cy As Double, Optional ByVal Addr As Integer = 0) As Boolean
        Dim Res As Boolean = False
        Dim cmd As String = ""
        Dim CxReadValue As Double = 0
        Dim CyReadValue As Double = 0
        Dim OffSetCx As Double = 0
        Dim OffSetCy As Double = 0
        Dim StrOffset As String = ""
        Dim Risposta As String

        cmd = "PUTXOFFSET" & Ch.ToString("00") & "+0.000" & Chr(13)
        Call Send(cmd, Addr)
        Risposta = Ricevi(2)
        If Risposta.Trim <> "OK" Then
            MsgBox("CALIBRATION" & Ch.ToString("00") & " CX-CY-1 Error")
            Return False
        End If
        Res = False

        cmd = "PUTYOFFSET" & Ch.ToString("00") & "+0.000" & Chr(13)
        Call Send(cmd, Addr)
        Risposta = Ricevi(2)
        If Risposta.Trim <> "OK" Then
            MsgBox("CALIBRATION" & Ch.ToString("00") & " CX-CY-2 Error")
            Return False
        End If
        Res = False

        cmd = "GETXY" & Ch.ToString("00") & Chr(13)
        Call Send(cmd, Addr)
        Risposta = Ricevi(2)

        CxReadValue = CType(Risposta.Substring(0, 6), Double)
        CyReadValue = CType(Risposta.Substring(7, 6), Double)

        OffSetCx = FormatNumber(Cx - CxReadValue, 3)
        OffSetCy = FormatNumber(Cy - CyReadValue, 3)

        If OffSetCx = 0 Then
            StrOffset = "+0.000"
        Else
            If OffSetCx > 0 Then
                StrOffset = "+" + OffSetCx.ToString.PadRight(5, "0")
            Else
                StrOffset = OffSetCx.ToString.PadRight(6, "0")
            End If
        End If

        cmd = "PUTXOFFSET" + Ch.ToString("00") + StrOffset + Chr(13)
        Call Send(cmd, Addr)

        Risposta = Ricevi(2)
        If Risposta.Trim <> "OK" Then
            MsgBox("CALIBRATION" & Ch.ToString("00") & " CX Error")
            Return False
        Else
            Res = True
        End If
        Res = False


        If OffSetCy = 0 Then
            StrOffset = "+0.000"
        Else
            If OffSetCy > 0 Then
                StrOffset = "+" + OffSetCy.ToString.PadRight(5, "0")
            Else
                StrOffset = OffSetCy.ToString.PadRight(6, "0")
            End If
        End If

        cmd = "PUTYOFFSET" + Ch.ToString("00") + StrOffset + Chr(13)
        Call Send(cmd, Addr)

        Risposta = Ricevi(2)
        If Risposta.Trim <> "OK" Then
            MsgBox("CALIBRATION" & Ch.ToString("00") & " CY Error")
            Return False
        Else
            Res = True
            'MsgPrintLog("Calibration" & Ch.ToString("00") & "Wavelength Success", 0)
            'MsgBox("CALIBRATION" & Ch.ToString("00") & " CY Success")
        End If
        Return Res
    End Function

#End Region


End Class
