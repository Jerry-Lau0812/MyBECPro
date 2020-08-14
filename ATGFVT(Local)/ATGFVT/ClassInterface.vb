Imports System.Runtime.InteropServices
Imports Bitron3DEQ.FVTRackBoardManager.RackSystem
Imports Bitron3DEQ.FVTRackBoardManager
Imports InterfaceUTL070
Imports System.Management
Imports System.IO.Ports
Imports System.Text

Public Class ClassInterface
    Implements IDisposable
    Public ComMode As ComModes = ComModes.Default
    Private ComType As ComTypes = ComTypes.SelfDefine
    Private Rs232 As Driver.Serial

    Public Enum Command
        Unknow = 0

        VERCOM = 1
        TRGCOM = 2
        OPENCOM = 3
        CLSCOM = 4


        UTL_STARTUTL070 = 5
        UTL_STOPUTL070 = 6
        UTL_SETSERIANNUMBER = 7
        UTL_READTESTRESULT = 8
        UTL_READTESTRESULTALL = 9
        UTL_WRITENFCRESULT = 10
        UTL_WRITENFC = 11
        UTL_CHECKNFC = 12
        UTL_OPENCOM = 13
        UTL_CLOSECOM = 14
        UTL_READIDTABLE = 15
        UTL_READSWVERSION = 16
        UTL_READDGTO = 17
        UTL_WRITEDGTO = 18
        UTL_WRITETDAINFO = 19
        UTL_READMAC = 20
        UTL_WRITETDARESULT = 21
        UTL_READSERIALNUMBER = 22
        UTL_GETUTL070RESULT = 23


        NORMAL_ENTERTESTMODE = 24
        NORMAL_ALLON = 25
        NORMAL_PART1ON = 26
        NORMAL_PART2ON = 27
        NORMAL_ALLOFF = 28
        NORMAL_GETBUTTONVALUE = 29
        NORMAL_OPENCOM = 30
        NORMAL_CLOSECOM = 31
        NORMAL_READCOMMAND = 32
        NORMAL_READCOMMAND_ATGHP = 33

        RESTART = 9999 ' restart server program
    End Enum

    Private Enum ComTypes
        DefaultDefine
        SelfDefine
    End Enum

    'Public Enum UTL070Commands
    '    START_UTL070 = 0
    '    QUIT_UTL070 = 1
    '    SET_SERIALNUMBER = 2
    '    READ_TESTRESULT = 3
    '    READ_TESTRESULTALL = 4
    '    WRITE_NFCRESULT = 5
    '    WRITE_NFC = 6
    '    CHECK_NFC = 7
    '    OPEN_UTL070COM = 8
    '    CLOSE_UTL070COM = 9
    '    READ_IDTABLE = 10
    '    READ_SWVERSION = 11
    '    READ_DGTO = 12
    '    WRITE_DGTO = 13
    '    WRITE_TDAINFO = 14
    '    READ_MAC = 15
    '    READ_SERIALNUMBER = 16
    '    GET_UTL070RESULT = 17
    'End Enum


#Region "Variant Declare"
    Private ComIndex As UInteger
    Private PortNum As Integer
    Private PortBaud As Long
    Private InterfaceType As Boolean

    Private StartCHR As String = ""
    Private EndCHR As String = ""
    Private StartCHRATG As String = "C3"

    Public Enum ComModes
        [Default]
        Client
        Server
    End Enum

    Public Enum DataDirs
        [In]
        [Out]
    End Enum

    Public Class DataInOutEventArgs
        Inherits EventArgs
        Public Message As String
        Public direction As DataDirs

        Public Sub New(ByVal MSG As String, ByVal direct As DataDirs)
            Me.Message = MSG
            Me.direction = direct
        End Sub
    End Class

    Public Event DataInOutEvent(ByVal e As DataInOutEventArgs)

    Private ParasIn As New List(Of String)

    Private ParasOut As New List(Of String)

#End Region

    Public Sub New(ByVal ComEnumID As Integer)
        Me.ComIndex = ComEnumID
        If Me.ComIndex = 0 Then
            If ComType = ComTypes.SelfDefine Then
                Rs232 = New Driver.Serial
            Else

            End If
        End If
    End Sub

#Region "Com Function"

    Public Function OpenCom() As Boolean
        Dim Result As Boolean = QuitUTL070()
        Result = Device_Interface.ForceOpenCom(Me.ComIndex)
        Return Result
    End Function

    Public Function CloseCom() As Boolean
        Dim Result As Boolean = Device_Interface.ForceCloseCom(Me.ComIndex)
        Return Result
    End Function

    Public Function STARTUTL070Com(Optional ByVal BaudRate As String = "38400", Optional ByVal OBIDSN As String = "0", Optional ByVal IsUTL070Type As Boolean = True) As Boolean
        'Command from Master PC
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            If IsUTL070Type Then
                Result = Device_Interface.ForceCloseCom(Me.ComIndex)
                Result = StartUTL070(BaudRate, OBIDSN)
            Else
                Result = Device_Interface.ForceOpenCom(Me.ComIndex)
                'Result = Device_Interface.ForceCloseCom(Me.ComIndex)
            End If

        Else
            ParasIn.Clear()
            ParasIn.Add(BaudRate)
            ParasIn.Add(OBIDSN)
            ParasIn.Add(IsUTL070Type)
            Result = GetMsgFromServer(Command.UTL_STARTUTL070)
        End If
        Return Result
    End Function

    Public Function QUITUTL070Com() As Boolean
        'Command from Master PC
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = QuitUTL070()
        Else
            Result = GetMsgFromServer(Command.UTL_STOPUTL070)
        End If
        Return Result
    End Function

    Public Function SetUTL070SerialNumber(ByVal SN As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = SetSerialNumber(SN)
        Else
            ParasIn.Clear()
            ParasIn.Add(SN)
            Result = GetMsgFromServer(Command.UTL_SETSERIANNUMBER)
        End If
        Return Result
    End Function

    Public Function ReadUTL070TestResult(ByVal position As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = ReadTestResult(position)
        Else
            ParasIn.Clear()
            ParasIn.Add(position)
            Result = GetMsgFromServer(Command.UTL_READSERIALNUMBER)
        End If
        Return Result
    End Function

    Public Function ReadUTL070TestResultAll() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = ReadTestResult_All()
        Else
            Result = GetMsgFromServer(Command.UTL_READTESTRESULTALL)
        End If
        Return Result
    End Function

    Public Function WriteUTL070NFCResult(ByVal pos As String, ByVal value As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = WriteNFCResult(pos, value)
        Else
            ParasIn.Clear()
            ParasIn.Add(pos)
            ParasIn.Add(value)
            Result = GetMsgFromServer(Command.UTL_WRITENFCRESULT)
        End If
        Return Result
    End Function

    Public Function WriteUTL070NFC(ByVal nfcfile As String, ByVal HFG As String, ByVal HHW As String, ByVal HSW As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = WriteNFC(nfcfile, HFG, HHW, HSW)
        Else
            ParasIn.Clear()
            ParasIn.Add(nfcfile)
            ParasIn.Add(HFG)
            ParasIn.Add(HHW)
            Result = GetMsgFromServer(Command.UTL_WRITENFC)
        End If
        Return Result
    End Function

    Public Function CheckUTL070NFC(ByVal nfcfile As String, ByVal HFG As String, ByVal HHW As String, ByVal HSW As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = CheckNFC(nfcfile, HFG, HHW, HSW)
        Else
            ParasIn.Clear()
            ParasIn.Add(nfcfile)
            ParasIn.Add(HFG)
            ParasIn.Add(HHW)
            Result = GetMsgFromServer(Command.UTL_CHECKNFC)
        End If
        Return Result
    End Function

    Public Function OpenUTLCOM() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = uOpenCom()
        Else
            Result = GetMsgFromServer(Command.UTL_OPENCOM)
        End If
        Return Result
    End Function

    Public Function CloseUTLCOM() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = uCloseCom()
        Else
            Result = GetMsgFromServer(Command.UTL_CLOSECOM)
        End If
        Return Result
    End Function

    Public Function ReadUTLIDTable() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = ReadIDTable()
        Else
            Result = GetMsgFromServer(Command.UTL_READIDTABLE)
        End If
        Return Result
    End Function

    Public Function ReadUTLSWVersion() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = ReadSWVerision()
        Else
            Result = GetMsgFromServer(Command.UTL_READSWVERSION)
        End If
        Return Result
    End Function

    Public Function ReadUTLDGTO(ByVal address As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = ReadDGTO(address)
        Else
            ParasIn.Clear()
            ParasIn.Add(address)
            Result = GetMsgFromServer(Command.UTL_READDGTO)
        End If
        Return Result
    End Function

    Public Function WriteUTLDGTO(ByVal address As String, ByVal value As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = WriteDGTO(address, value)
        Else
            ParasIn.Clear()
            ParasIn.Add(address)
            ParasIn.Add(value)
            Result = GetMsgFromServer(Command.UTL_WRITEDGTO)
        End If
        Return Result
    End Function

    Public Function WriteUTLTDAInfo() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = WriteTDAInfo()
        Else
            Result = GetMsgFromServer(Command.UTL_WRITETDAINFO)
        End If
        Return Result
    End Function

    Public Function ReadUTLMAC() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = ReadMAC()
        Else
            Result = GetMsgFromServer(Command.UTL_READMAC)
        End If
        Return Result
    End Function

    Public Function WriteUTLTDAResult(Optional ByVal Res As String = "P") As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = WriteTDAResult(Res)
        Else
            ParasIn.Add(Res)
            Result = GetMsgFromServer(Command.UTL_WRITETDARESULT)
        End If
        Return Result
    End Function

    Public Function ReadUTLSerialNumber() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = ReadSerailNumber()
        Else
            Result = GetMsgFromServer(Command.UTL_READSERIALNUMBER)
        End If
        Return Result
    End Function

    Public Function GetUTLResult() As String
        Dim Res As String
        If Me.ComMode = ComModes.Default Then
            Res = GetUTL070Result()
        Else
            Res = GetMsgFromServer(Command.UTL_GETUTL070RESULT)
        End If
        Return Res
    End Function

    Public Function VerCom(ByVal e As Driver.PortEventArgs) As Boolean
        '*****************************************
        'Put the Interface communication verify code here
        '*****************************************
        PortNum = CInt(e.Com.Port)
        PortBaud = CLng(e.Com.Baud)
        ComIndex = e.Index

        If ComType = ComTypes.SelfDefine Then
            Call SetCom_SelfDefine(e)
        End If

        If Me.OpenCom Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function TrigCom(ByVal e As Driver.PortEventArgs) As Boolean
        If Me.OpenCom Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function STARTNORMALCom() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                'Rs232.BaudRate = Me.PortBaud
                'Rs232.PortNum = Me.PortNum
                If Rs232.IsOpen Then

                Else
                    Rs232.Open()
                End If

                Result = True
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_OPENCOM)
        End If
        Return Result
    End Function

    Public Function QUITNORMALCom() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                If Rs232.IsOpen Then
                    Rs232.Close()
                Else

                End If

                Result = True
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_CLOSECOM)
        End If
        Return Result
    End Function

    Public Function EnterTestMode() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = SendCommand(Command.NORMAL_ENTERTESTMODE)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_ENTERTESTMODE)
        End If
        Return Result
    End Function

    Public Function Part1ON() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = SendCommand(Command.NORMAL_PART1ON)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_PART1ON)
        End If
        Return Result
    End Function

    Public Function Part2ON() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = SendCommand(Command.NORMAL_PART2ON)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_PART2ON)
        End If
        Return Result
    End Function

    Public Function GetButtonValue() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = SendCommand(Command.NORMAL_GETBUTTONVALUE)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_GETBUTTONVALUE)
        End If
        Return Result
    End Function

    Public Function AllON() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = SendCommand(Command.NORMAL_ALLON)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_ALLON)
        End If
        Return Result
    End Function

    Public Function AllOFF() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = SendCommand(Command.NORMAL_ALLOFF)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_ALLOFF)
        End If
        Return Result
    End Function

    Public Function ReadCommandPara(ByRef FW As String, ByRef ButtonValue As String, ByRef P1Value As Integer, ByRef P2Value As Integer, ByRef AllValue As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = ReadCommand(FW, ButtonValue, P1Value, P2Value, AllValue)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_READCOMMAND)
            If Result = True Then
                FW = ParasOut(1)
                ButtonValue = ParasOut(2)
                P1Value = ParasOut(3)
                P2Value = ParasOut(4)
                AllValue = ParasOut(5)
            End If
        End If
        Return Result
    End Function

    Public Function ReadCommandPara_ATGHPHMI(ByRef FW As String, ByRef ButtonValue As String, ByRef P1Value As Integer, ByRef P2Value As Integer, ByRef AllValue As String) As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Try
                Result = ReadCommand_ATGHPHMI(FW, ButtonValue, P1Value, P2Value, AllValue)
            Catch
                Result = False
            End Try
        Else
            Result = GetMsgFromServer(Command.NORMAL_READCOMMAND_ATGHP)
            If Result = True Then
                FW = ParasOut(1)
                ButtonValue = ParasOut(2)
                P1Value = ParasOut(3)
                P2Value = ParasOut(4)
                AllValue = ParasOut(5)
            End If
        End If
        Return Result
    End Function


#End Region

#Region "Test Function"
    ''' <summary>
    '''Restart Server
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RestartServer() As Boolean
        Dim Result As Boolean = False
        If Me.ComMode = ComModes.Default Then
            Result = True
        Else
            Result = GetMsgFromServer(Command.RESTART)
        End If
        Return Result
    End Function

    Public Function SetupCom() As Boolean
        Dim Result As Boolean = False
        'If Me.ComMode = ComMode.Default Then

        'End If
    End Function


#End Region


#Region "UTL070 Operate Class"

    Private utl070Path = "C:\NFC\utl070.exe"
    Private utl As Classutl070 = New Classutl070(utl070Path)
    Private param_utl070 As List(Of String) = New List(Of String)

    Private Function StartUTL070(ByVal baudrate As String, Optional ByVal OBIDSN As String = "0") As Boolean
        Dim TestOk As Boolean = False
        Dim Retry As Integer = 0
        'Dim AMIPortName As String = ""
        'AMIPortName = GetAMIPortName()
        While (Not TestOk And Retry < 3)
            TestOk = utl.SetUARTProperty(baudrate, PortNum, OBIDSN)
            If Not TestOk Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOk
    End Function

    Private Function QuitUTL070() As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        While (Not TestOK And Retry < 3)
            TestOK = utl.Kill_utl070
            Sleep(250)
        End While
        Return TestOK
    End Function

    Private Function SetSerialNumber(ByVal sn As String) As Boolean
        Dim TestOk As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(sn)
        While (Not TestOk And Retry < 3)
            TestOk = utl.SendCommandtoUTL070(Classutl070.utl070CMD.SERIAL_NUMBER, param_utl070)
            If Not TestOk Then
                Retry += 1
            End If
        End While
        Return TestOk
    End Function

    Private Function ReadTestResult(ByVal position As String) As Boolean
        Dim TestOk As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(position)
        While (Not TestOk And Retry < 3)
            TestOk = utl.SendCommandtoUTL070(Classutl070.utl070CMD.NFC_READ_TEST_POS, param_utl070)
            If Not TestOk Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOk
    End Function

    Private Function ReadTestResult_All() As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.NFC_READ_TEST, param_utl070)
            If Not TestOK Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function WriteNFCResult(ByVal pos As String, ByVal value As String) As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(pos)
        param_utl070.Add(value)
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.NFC_WRITE_TEST_POS, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function WriteNFC(ByVal nfcfile As String, ByVal HFG As String, ByVal HHW As String, ByVal HSW As String) As Boolean
        Dim TestOk As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(nfcfile)
        param_utl070.Add(HFG)
        param_utl070.Add(HHW)
        param_utl070.Add(HSW)
        While (Not TestOk And Retry < 3)
            TestOk = utl.SendCommandtoUTL070(Classutl070.utl070CMD.NFC_WRITE_INIT, param_utl070)
            If (Not TestOk) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOk
    End Function

    Private Function CheckNFC(ByVal nfcfile As String, ByVal HFG As String, ByVal HHW As String, ByVal HSW As String)
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(nfcfile)
        param_utl070.Add(HFG)
        param_utl070.Add(HHW)
        param_utl070.Add(HSW)
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.NFC_CHECK_INIT, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function uOpenCom() As Boolean
        Dim TestOk As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOk And Retry < 3)
            TestOk = utl.SendCommandtoUTL070(Classutl070.utl070CMD.OPEN_COMM, param_utl070)
            If Not TestOk Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOk
    End Function

    Private Function uCloseCom() As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.CLOSE_COMM, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function ReadIDTable()
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.READ_ID_TABLE, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function ReadSWVerision() As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.READ_SW_VER, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function ReadDGTO(ByVal address As String) As Boolean
        Dim TestOk As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(utl.GetDTGOHex(address))
        While (Not TestOk And Retry < 3)
            TestOk = utl.SendCommandtoUTL070(Classutl070.utl070CMD.READ_DGTO, param_utl070)
            If (Not TestOk) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOk
    End Function

    Private Function WriteDGTO(ByVal address As String, ByVal value As String)
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(utl.GetDTGOHex(address))
        param_utl070.Add(value)
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.WRITE_DGTO, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function WriteTDAInfo() As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.TDA_WRITE_INFO, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function ReadMAC() As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.READ_MAC_ADDRESS, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function WriteTDAResult(Optional ByVal result As String = "P") As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        param_utl070.Add(result)
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.TDA_REGISTER_TEST, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function ReadSerailNumber() As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        param_utl070.Clear()
        While (Not TestOK And Retry < 3)
            TestOK = utl.SendCommandtoUTL070(Classutl070.utl070CMD.READ_SERIAL_NUMBER, param_utl070)
            If (Not TestOK) Then
                Retry += 1
                Sleep(250)
            End If
        End While
        Return TestOK
    End Function

    Private Function GetUTL070Result() As String
        Dim ansInfo As New Classutl070.utl070ansInfo
        ansInfo = utl.GetUTL070answer
        Return ansInfo.ToString()
    End Function





#Region "GET AMI PORT NAME"
    Public Function GetAMIPortName() As String

        Dim AMIPort As String = ""
        Dim Portlist() As String = GetHarewareInfor(HardwareEnum.Win32_PnPEntity, "Name")
        For Each portDesc In Portlist
            If portDesc.Contains("Appliance Mini Interface (COM") Then
                AMIPort = portDesc.Substring(portDesc.IndexOf("COM"), 5)
                If AMIPort.Substring(AMIPort.Length - 1, 1) = ")" Then
                    AMIPort = AMIPort.Substring(0, 4)
                End If
                Exit For
            End If
        Next

        Return AMIPort
    End Function

    Public Enum HardwareEnum
        Win32_Processor
        Win32_PhysicalMemory
        Win32_Keyboard
        Win32_PointingDevice
        Win32_FloppyDrive
        Win32_DiskDrive
        Win32_CDROMDrive
        Win32_BaseBoard
        Win32_BIOS
        Win32_ParallelPort
        Win32_SerialPort
        Win32_SerialPortConfiguration
        Win32_SoundDevice
        Win32_SystemSlot
        Win32_USBController
        Win32_NetworkAdapter
        Win32_NetworkAdapterConfiguration
        Win32_Printer
        Win32_PrinterConfiguration
        Win32_PrintJob
        Win32_TCPIPPrinterPort
        Win32_POTSModem
        Win32_POTSModemToSerialPort
        Win32_DesktopMonitor
        Win32_DisplayConfiguration
        Win32_DisplayControllerConfiguration
        Win32_VideoController
        Win32_VideoSettings


        Win32_TimeZone
        Win32_SystemDriver
        Win32_DiskPartition
        Win32_LogicalDisk
        Win32_LogicalDiskToPartition
        Win32_LogicalMemoryConfiguration
        Win32_PageFile
        Win32_PageFileSetting
        Win32_BootConfiguration
        Win32_ComputerSystem
        Win32_OperatingSystem
        Win32_StartupCommand
        Win32_Service
        Win32_Group
        Win32_GroupUser
        Win32_UserAccount
        Win32_Process
        Win32_Thread
        Win32_Share
        Win32_NetworkClient
        Win32_NetworkProtocol
        Win32_PnPEntity
    End Enum

    Private Shared Function GetHarewareInfor(ByVal hardType As HardwareEnum, ByRef propKey As String)

        Dim strs As List(Of String) = New List(Of String)

        Try
            Using search As New ManagementObjectSearcher("select * from " + hardType.ToString)
                Dim hardInfos = search.Get()
                For Each hardInfo In hardInfos
                    If hardInfo.Properties(propKey).Value <> "" Then
                        Dim str As String = hardInfo.Properties(propKey).Value.ToString()
                        strs.Add(str)
                    End If
                Next
            End Using
            Return strs.ToArray()
        Catch
            Return ""
        End Try

    End Function


#End Region

#End Region


#Region "Normal Com Operate Class"
    Dim ReadList As List(Of Byte) = New List(Of Byte)

    Private Sub Com_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
    End Sub
    Private Sub SetCom_SelfDefine(ByVal e As Driver.PortEventArgs)
        Try
            With Rs232
                .Index = Me.ComIndex
                .PortNum = e.Com.Port                         'Port number
                .PortName = "COM" & e.Com.Port                'PortName
                .BaudRate = e.Com.Baud                        'Baundrate

                .Parity = 0 'e.Com.Parity      'Parity.None =0
                .DataBits = e.Com.Data                           'DataBits
                .StopBits = 1 ' e.Com.Stopbit  'StopBits.One 1
                .TypeID = ExtHardwareType.INTERF
                .HWLocation = HardWareLocation.Exteranl

                .Address = e.Index                           'Board address
                .Name = ExtHardwareType.INTERF.ToString & e.Index + 1 'Name we give

                .Encoding = Encoding.GetEncoding(1252)     'per Sendre caratteri Asci>128
                .ReadTimeout = 2000
                .ReceivedBytesThreshold = 1

                '.RtsEnable = True  '非常关键！必须设为可用，否则接收不到数据！

                AddHandler .DataReceived, AddressOf Com_DataReceived

                'Rs232.Open()
            End With
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function Riceve_Standardxx(Optional ByVal Timeout As Double = 1) As List(Of String)
        Dim ReadOK As Boolean = False
        Dim StrRead As String = ""
        Dim TmpRead As String = ""
        Dim RecCmds(0) As Byte

        Dim SW As Stopwatch = New Stopwatch()
        SW.Restart()

        Try
            Dim ByteRead As Integer = 0
            ReadOK = False       'Reset Flag
            While Not ReadOK
                ByteRead = Rs232.BytesToRead
                Dim Buffer(ByteRead - 1) As Byte

                If ByteRead > 0 Then
                    Call Rs232.Read(Buffer, 0, ByteRead)
                    If Not (Buffer Is Nothing) Then
                        ReadList.AddRange(Buffer)
                    End If
                End If

                If SW.ElapsedMilliseconds / 1000 > Timeout Then
                    ReadOK = True
                End If

                Sleep(10)
            End While

        Catch ex As Exception
        End Try

        ''
        For Each s In ReadList
            StrRead &= "-" & s.ToString("X2")
        Next

        Dim StrData As List(Of String) = StrRead.Split(New String() {StartCHRATG}, StringSplitOptions.RemoveEmptyEntries).ToList()
        'Dim RetrunData As List(Of String) = From c In StrData Select StartCHR & c

        Dim RetrunData As List(Of String) = New List(Of String)
        For Each ss In StrData
            RetrunData.Add(StartCHRATG & ss)
        Next

        Return RetrunData

    End Function


    Public Function HexStringToByte(ByVal Hex As String) As Byte()

        Hex = Hex.Replace("-", "").Trim()

        Dim CurrStr As String = ""
        Dim CurrByte As Byte
        Dim Len As Integer = (Hex.Length / 2)
        Dim RecBytes As List(Of Byte) = New List(Of Byte)



        For i = 0 To Len - 1
            CurrStr = Hex.Substring(i * 2, 2)
            CurrByte = Convert.ToByte(CurrStr, 16)
            RecBytes.Add(CurrByte)
        Next

        Return RecBytes.ToArray()
    End Function
    Public Function ListToByteList(ByVal DataList As List(Of String)) As List(Of Byte())
        Dim ByteArray As List(Of Byte()) = New List(Of Byte())
        For Each rec In DataList
            Dim Arr() As Byte = HexStringToByte(rec)
            If Arr IsNot Nothing Then
                ByteArray.Add(Arr)
            End If
        Next
        Return ByteArray
    End Function
    Public Function GetCmd(ByVal Cmd As String) As List(Of Byte)
        ''Convert to byte array
        Dim CmdArray() As String = Cmd.Split(New String() {"-"}, StringSplitOptions.RemoveEmptyEntries)
        Dim ByteCmds As List(Of Byte) = New List(Of Byte)
        For Each d In CmdArray
            ByteCmds.Add(Convert.ToByte(d, 16))
        Next

        Dim ByteCRC As Byte = GetCRC(ByteCmds.ToArray())

        ByteCmds.Add(ByteCRC)

        Return ByteCmds
    End Function
    Public Function GetCRC(ByVal datas As Byte())
        ''Get CRC
        'Dim Datas() As Byte = ByteCmds.ToArray()
        Dim Sum As Integer = 0
        For Each d In datas
            Sum += d
        Next
        Dim Str As String = Sum.ToString("X4")
        Dim StrCRC As String = Str.Substring(Str.Length - 2)
        Dim ByteCRC As Byte = Convert.ToByte(StrCRC, 16)
        Return ByteCRC
    End Function

    ''' <summary>
    '''  cmd format "C3-FF-52-14"
    ''' </summary>
    ''' <param name="Cmd"></param>
    Public Sub Send(ByVal Cmd As String)
        Dim ByteCmd As List(Of Byte) = GetCmd(Cmd)
        ByteCmd.Insert(0, Convert.ToByte("3C", 16))
        If Rs232.IsOpen Then
            ReadList.Clear()
            Rs232.DiscardInBuffer()
            Rs232.Write(ByteCmd.ToArray(), 0, ByteCmd.Count)
        End If
    End Sub

    Public Function Read(Optional ByVal ReturnLen As Integer = 39, Optional ByVal Timeout As Double = 1) As Byte()

        Dim StrReceive As List(Of String) = Riceve_Standardxx(Timeout)
        Dim ByteArray As List(Of Byte()) = ListToByteList(StrReceive)
        'Dim RecByte As Byte() = (From c In ByteArray Where c(0).ToString("X2") = StartCHR And (c.Length = ReturnLen Or c.Length = ReturnLen - 1) Select c).ToList().LastOrDefault()
        Dim RecByte As Byte()
        Dim RecByteList = (From c In ByteArray Where c(0).ToString("X2") = StartCHRATG And (c.Length = ReturnLen Or c.Length = ReturnLen - 1) Select c).ToList()
        Dim RecByteListCount As Integer = RecByteList.Count
        If RecByteListCount > 0 Then
            RecByte = RecByteList(RecByteListCount - 1)
        End If


        ''如果CRC是C3，则数据会截断，造成数据少一位，丢失CRC，如果丢失则添加
        If Not RecByte Is Nothing Then
            If RecByte.Length = ReturnLen - 1 Then
                Dim CRC As Byte = GetCRC(RecByte)
                Dim ListBytes As List(Of Byte) = (RecByte.ToList())
                ListBytes.Add(CRC)
                RecByte = ListBytes.ToArray()
            End If
        End If

        Return RecByte

    End Function

    Public Function SendCommand(ByVal CmdType As Command) As Boolean
        Dim Byte1 As Byte
        Dim Byte10 As Byte
        Dim Cmd As String = ""
        Select Case CmdType
            Case Command.NORMAL_ENTERTESTMODE
                Byte1 = &HFF
                Byte10 = &H0

            Case Command.NORMAL_ALLON
                Byte1 = &HFF
                Byte10 = &H1

            Case Command.NORMAL_PART1ON
                Byte1 = &H9
                Byte10 = &H2

            Case Command.NORMAL_PART2ON
                Byte1 = &H5
                Byte10 = &H3

            Case Command.NORMAL_ALLOFF
                Byte1 = &H0
        End Select

        'Dim Cmd As String = String.Format("C3-41-52-22-03-FF-00-{0}-00-00-00-00-00-00-00-00-{1}-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00", Byte1.ToString("X2"), Byte10.ToString("X2"))
        'Dim Cmd As String = String.Format("C3-41-52-02-02-{0}-00-00-00-00-00-00-00-00-{1}-00-00-00-00-00-00-00-00-00", Byte1.ToString("X2"), Byte10.ToString("X2"))
        If CmdType = Command.NORMAL_GETBUTTONVALUE Then
            Cmd = "C3-41-52-02-02-FF"
        Else
            Cmd = String.Format("C3-FF-52-14-00-{0}-00-00-00-00-00-00-00-00-{1}-00-00-00-00-00-00-00-00-00", Byte1.ToString("X2"), Byte10.ToString("X2"))
        End If

        Call Send(Cmd)

        Return True
    End Function

    Public Function ReadCommand(ByRef FW As String, ByRef ButtonValue As String, Optional ByRef P1Value As Integer = 0, Optional ByRef P2Value As Integer = 0, Optional ByRef AllValue As String = "") As Boolean
        ReadList.Clear() ''don't delete
        FW = "xx.xx.xx"
        ButtonValue = ""
        P1Value = 999999999
        P2Value = 999999999

        Dim IsOK As Boolean = False
        Dim Retry As Integer = 0
        Dim Rec() As Byte
        While Not IsOK And Retry < 10
            Rec = Read(39, 0.2)
            If Not (Rec Is Nothing) Then
                If (Rec.Length >= 39 And Rec(0).ToString("X2") = StartCHRATG) Then
                    'For ATG Clas HMI
                    FW = Rec(35).ToString("X2") & "." & Rec(36).ToString("X2") & "." & Rec(37).ToString("X2")
                    ButtonValue = Rec(7).ToString("X2") & " " & Rec(8).ToString("X2")

                    P1Value = Rec(10)
                    P2Value = Rec(11)
                    For i As Integer = 0 To Rec.Length - 1
                        AllValue += " " & Rec(i).ToString("X2")
                    Next
                    IsOK = True
                End If
            End If
            If Not IsOK Then
                Retry += 1
                SendCommand(Command.NORMAL_GETBUTTONVALUE)
                ButtonValue = ""
                Sleep(100)
            End If
            'Application.DoEvents()
        End While
        Return IsOK
    End Function

    Public Function ReadCommand_ATGHPHMI(ByRef FW As String, ByRef ButtonValue As String, Optional ByRef P1Value As Integer = 0, Optional ByRef P2Value As Integer = 0, Optional ByRef AllValue As String = "") As Boolean
        ReadList.Clear() ''don't delete
        FW = "xx.xx.xx"
        ButtonValue = ""
        P1Value = 999999999
        P2Value = 999999999

        Dim IsOK As Boolean = False
        Dim Retry As Integer = 0
        Dim Rec() As Byte
        While Not IsOK And Retry < 10
            Rec = Read(39, 0.2)
            If Not (Rec Is Nothing) Then
                If (Rec.Length >= 39 And Rec(0).ToString("X2") = StartCHRATG) Then
                    'For ATG HP
                    FW = Rec(8).ToString("X2") & "." & Rec(9).ToString("X2") & "." & Rec(10).ToString("X2")
                    ButtonValue = Rec(12).ToString("X2")

                    P1Value = Rec(10)
                    P2Value = Rec(11)
                    For i As Integer = 0 To Rec.Length - 1
                        AllValue += " " & Rec(i).ToString("X2")
                    Next
                    IsOK = True
                End If
            End If
            If Not IsOK Then
                Retry += 1
                SendCommand(Command.NORMAL_GETBUTTONVALUE)
                ButtonValue = ""
                Sleep(100)
            End If
            'Application.DoEvents()
        End While
        Return IsOK
    End Function



#End Region

#Region "Other function support for client and Server"
    Public Sub Sleep(ByVal ms As Integer)
        Threading.Thread.Sleep(ms)
    End Sub

    Public Function HasDataComing() As Boolean
        Return Device_Interface.HasDataComing(Me.ComIndex)
    End Function
    Public Function ReceiveCommand(Optional ByVal Timeout As Single = 5) As String
        Dim RecCmd As String = Device_Interface.RecieveData(Me.ComIndex, StartCHR, EndCHR, timeout:=Timeout)
        RaiseEvent DataInOutEvent(New DataInOutEventArgs(RecCmd, DataDirs.In))
        Return RecCmd
    End Function
    Public Sub SendCommand(ByVal Cmd As String)
        RaiseEvent DataInOutEvent(New DataInOutEventArgs(Cmd, DataDirs.Out))
        Cmd = StartCHR & Cmd & EndCHR
        Try
            Device_Interface.Send(Cmd, StartCHR, EndCHR, CInt(Me.ComIndex))
        Catch ex As Exception
            Device_Interface.ForceOpenCom(Me.ComIndex)
            Device_Interface.Send(Cmd, StartCHR, EndCHR, CInt(Me.ComIndex))
        End Try

    End Sub
    Public Function Analyzcmd(ByVal StrCmd As String) As Command
        Dim RetCmd As Command
        Try
            RetCmd = [Enum].Parse(GetType(Command), StrCmd)
        Catch ex As Exception
            RetCmd = Command.Unknow
        End Try
        Return RetCmd
    End Function

    ''' <summary>
    ''' 此函数用于正常生产时调用Server端的数据
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsgFromServer(ByVal cmd As Command) As String
        'Send Command to Server
        Dim StrSendCmd As String = cmd.ToString & ";"
        If ParasIn.Count > 0 Then
            For Each para In ParasIn
                StrSendCmd &= para & "~"
            Next
            StrSendCmd = StrSendCmd.Substring(0, StrSendCmd.Length - 1)
        End If
        Me.SendCommand(StrSendCmd) 'Send command
        If ParasIn.Count > 0 Then ParasIn.Clear() 'Get ready for next send 


        'Wait and received from Server
        ParasOut.Clear()
        Dim StrResult As String = ""
        Select Case cmd
            'Case Command.RDRPT, Command.WRRPT, Command.WRFNLZ
            Case Command.Unknow
                StrResult = Me.ReceiveCommand(30)
            Case Else
                StrResult = Me.ReceiveCommand(10)
        End Select
        If StrResult <> "" Then
            If StrResult.Contains(";") Then
                Dim StrValues() As String = StrResult.Split(";")
                If StrValues(0) = cmd.ToString Then
                    If StrValues(1) <> "" Then
                        If StrValues(1).Contains("~") Then
                            Dim Paras() As String = StrValues(1).Split("~")
                            For Each item In Paras
                                If item <> "" Then
                                    ParasOut.Add(item)
                                End If
                            Next
                        Else
                            ParasOut.Add(StrValues(1))
                        End If
                    End If
                Else
                    StrResult = False
                End If
            Else
                StrResult = False
            End If
        Else
            StrResult = False
        End If

        If ParasOut.Count > 0 Then
            StrResult = ParasOut(0)
        Else
            StrResult = False
        End If
        Return StrResult
    End Function

    ''' <summary>
    ''' Load all command address from address.txt
    ''' </summary>
    ''' <param name="AddressName"></param>
    ''' <param name="BoardType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
