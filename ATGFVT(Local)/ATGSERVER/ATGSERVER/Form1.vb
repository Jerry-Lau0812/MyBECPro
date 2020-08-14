Imports Bitron3DEQ.FVTRackBoardManager
Imports Bitron3DEQ.FVTRackBoardManager.RackSystem
Imports System.IO

Public Class Form1
    Private MyRack As New RackSystem
    Public MyInterfacePCB As ClassInterface
    Public MyInterfacePC As ClassInterface

    Private Delegate Sub DispMessageDelegate(ByVal e As ClassInterface.DataInOutEventArgs)
    Private MyDispDel As New DispMessageDelegate(AddressOf DispMsg)
    Private Risp As String, StrResult As String
    Private ShowAllInList As Boolean = False
    Private ShowIn As Boolean = True
    Private ShowOUT As Boolean = True
    Private ShowTime As Boolean = True


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyRack.XMLConfiguePath = Application.StartupPath & "\sysfile\HWConfigure.xml"
        Try
            'Call CopyDLLToSysdirectory()
        Catch ex As Exception
        End Try

        Call SetInterface()
        Call InitialMachine()
    End Sub

    Private Sub InitialMachine()
        If Myrack.InitialRackSystem() Then
            MyInterfacePCB.CloseCom()

            ButStart.Enabled = True
            Call StartServer()
        Else
            ButStart.Enabled = False
        End If

    End Sub


    Private Sub StartServer()
        BGW.RunWorkerAsync()

        ToolStripLblSpyStatus.Image = My.Resources.bullet_green
        ButInitial.Enabled = False
        ButStart.Enabled = False
        ButStop.Enabled = True
    End Sub

    Private Sub StopServer()
        BGW.CancelAsync()

        ToolStripLblSpyStatus.Image = My.Resources.bullet_black
        ButInitial.Enabled = True
        ButStart.Enabled = True
        ButStop.Enabled = False

        ToolStripLbl_DataIn.BackColor = Color.FromKnownColor(KnownColor.Control)
        ToolStripLbl_DataOut.BackColor = Color.FromKnownColor(KnownColor.Control)
        ToolStripLbl_DataIn.Text = ""
        ToolStripLbl_DataOut.Text = ""
    End Sub

    Private Sub DataInActived(ByVal e As ClassInterface.DataInOutEventArgs)
        Me.Invoke(MyDispDel, e)
    End Sub


    Private Sub BGW_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGW.DoWork
        While (Not BGW.CancellationPending)
            SpyData()
            Threading.Thread.Sleep(1)
        End While
    End Sub

    Private Sub DispMsg(ByVal e As ClassInterface.DataInOutEventArgs)
        If e.direction = ClassInterface.DataDirs.In Then
            ToolStripLbl_DataIn.Text = e.Message
            If e.Message = "" Or e.Message = "" Then
                ToolStripLbl_DataIn.BackColor = Color.Red
            Else
                ToolStripLbl_DataIn.BackColor = Color.LimeGreen
            End If
        Else
            ToolStripLbl_DataOut.Text = e.Message
            If e.Message = "" Or e.Message = "?" Then
                ToolStripLbl_DataOut.BackColor = Color.Red
            Else
                ToolStripLbl_DataOut.BackColor = Color.LimeGreen
            End If
        End If
        If ShowAllInList Then
            Dim Prefix As String = ""
            Dim Msg As String = ""
            If e.direction = ClassInterface.DataDirs.In Then
                If ShowIn Then Msg = "->" & e.Message
            Else
                If ShowOUT Then Msg = "->" & e.Message
            End If

            If Msg <> "" Then
                If ShowTime Then Msg = Date.Now.ToString("HH:mm:ss") & ":" & Date.Now.Millisecond & Msg
                'ListBox1.Items.Add(Msg)
            End If

        End If
    End Sub



#Region "Test relative function"
    Private Sub SetInterface()
        If MyInterfacePCB Is Nothing Then
            MyInterfacePCB = New ClassInterface(0)
            MyInterfacePCB.ComMode = ClassInterface.ComModes.Default
        Else
            MyInterfacePCB.CloseCom()
        End If

        If MyInterfacePC Is Nothing Then
            MyInterfacePC = New ClassInterface(1)
            MyInterfacePC.ComMode = ClassInterface.ComModes.Server
            AddHandler MyInterfacePC.DataInOutEvent, AddressOf DataInActived
        Else
            MyInterfacePC.CloseCom()
        End If

        With RackSystem.Device_Interface
            .DelegateOpenCom = AddressOf OpenComInterface
            .DelegateCloseCom = AddressOf CloseComInterface
            .DelegateVerCom = AddressOf VerComInterface
            .DelegateTrigCom = AddressOf TrigComInterface
        End With
    End Sub
#Region "Used for control by FVTRacksystem.dll"
    Public Function OpenComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        Dim Result As Boolean = False
        If e.Index = 0 Then
            Result = MyInterfacePCB.OpenCom()
        Else
            Result = MyInterfacePC.OpenCom()
        End If
        Return Result
    End Function
    Public Function CloseComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        Dim Result As Boolean = False
        If e.Index = 0 Then
            Result = MyInterfacePCB.CloseCom()
        Else
            Result = MyInterfacePC.CloseCom()
        End If
        Return Result
    End Function
    Public Function VerComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        Dim Result As Boolean = False
        If e.Index = 0 Then
            Result = MyInterfacePCB.VerCom(e)
        Else
            Result = MyInterfacePC.VerCom(e)
        End If
        Return Result
    End Function
    Public Function TrigComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        Dim Result As Boolean = False
        If e.Index = 0 Then
            Result = MyInterfacePCB.TrigCom(e)
        Else
            Result = MyInterfacePC.TrigCom(e)
        End If
        Return Result
    End Function
#End Region

    Public Sub SpyData()
        If MyInterfacePC.HasDataComing Then
            Try
                'Receive Cmd
                Risp = MyInterfacePC.ReceiveCommand()

                'Get Respond resultD:\Tools\MyBECPro\ATGFVT\ATGFVT\ATGSERVER\ATGSERVER\Form1.vb
                StrResult = AssortCommand(Risp)

                'Send Back the result
                Call MyInterfacePC.SendCommand(StrResult)

                'if command is restart server if restart program'2018-01-08 by ivey
                If StrResult.StartsWith(ClassInterface.Command.RESTART.ToString()) Then
                    Call MyInterfacePC.CloseCom()
                    System.Threading.Thread.Sleep(100)
                    Dim ExePath As String = Application.ExecutablePath
                    Process.Start(ExePath)
                    End
                End If

            Catch ex As Exception
            End Try
        End If
    End Sub
    Public Function AssortCommand(ByVal StrCmd As String) As String
        Dim StrResult As String = "?"

        If StrCmd.Contains(";") Then
            Dim Risps() As String = StrCmd.Split(";")
            Dim Paras(10) As String
            If Risps(1).Contains("~") Then
                Paras = Risps(1).Split("~")
            Else
                Paras(0) = Risps(1)
            End If

            Dim cmd As ClassInterface.Command = MyInterfacePC.Analyzcmd(Risps(0))
            Select Case cmd

                Case Bitron3DEQ.ClassInterface.Command.OPENCOM
                    StrResult = MyInterfacePCB.OpenCom()

                Case Bitron3DEQ.ClassInterface.Command.CLSCOM
                    StrResult = MyInterfacePCB.CloseCom()

                Case Bitron3DEQ.ClassInterface.Command.UTL_STARTUTL070
                    StrResult = MyInterfacePCB.STARTUTL070Com(Paras(0), Paras(1))

                Case Bitron3DEQ.ClassInterface.Command.UTL_STOPUTL070
                    StrResult = MyInterfacePCB.QUITUTL070Com()

                Case Bitron3DEQ.ClassInterface.Command.UTL_SETSERIANNUMBER
                    StrResult = MyInterfacePCB.SetUTL070SerialNumber(Paras(0))

                Case Bitron3DEQ.ClassInterface.Command.UTL_READTESTRESULT
                    StrResult = MyInterfacePCB.ReadUTL070TestResult(Paras(0))

                Case Bitron3DEQ.ClassInterface.Command.UTL_READTESTRESULTALL
                    StrResult = MyInterfacePCB.ReadUTL070TestResultAll

                Case Bitron3DEQ.ClassInterface.Command.UTL_WRITENFCRESULT
                    StrResult = MyInterfacePCB.WriteUTL070NFCResult(Paras(0), Paras(1))

                Case Bitron3DEQ.ClassInterface.Command.UTL_WRITENFC
                    StrResult = MyInterfacePC.WriteUTL070NFC(Paras(0), Paras(1), Paras(2), Paras(3))

                Case Bitron3DEQ.ClassInterface.Command.UTL_CHECKNFC
                    StrResult = MyInterfacePCB.CheckUTL070NFC(Paras(0), Paras(1), Paras(2), Paras(3))

                Case Bitron3DEQ.ClassInterface.Command.UTL_OPENCOM
                    StrResult = MyInterfacePCB.OpenUTLCOM

                Case Bitron3DEQ.ClassInterface.Command.UTL_CLOSECOM
                    StrResult = MyInterfacePCB.CloseUTLCOM

                Case Bitron3DEQ.ClassInterface.Command.UTL_READIDTABLE
                    StrResult = MyInterfacePCB.ReadUTLIDTable

                Case Bitron3DEQ.ClassInterface.Command.UTL_READSWVERSION
                    StrResult = MyInterfacePCB.ReadUTLSWVersion

                Case Bitron3DEQ.ClassInterface.Command.UTL_READDGTO
                    StrResult = MyInterfacePCB.ReadUTLDGTO(Paras(0))

                Case Bitron3DEQ.ClassInterface.Command.UTL_WRITEDGTO
                    StrResult = MyInterfacePCB.WriteUTLDGTO(Paras(0), Paras(1))

                Case Bitron3DEQ.ClassInterface.Command.UTL_READMAC
                    StrResult = MyInterfacePCB.ReadUTLMAC

                Case Bitron3DEQ.ClassInterface.Command.UTL_WRITETDAINFO
                    StrResult = MyInterfacePCB.WriteUTLTDAInfo

                Case Bitron3DEQ.ClassInterface.Command.UTL_WRITETDARESULT
                    StrResult = MyInterfacePCB.WriteUTLTDAResult(Paras(0))

                Case Bitron3DEQ.ClassInterface.Command.UTL_READSERIALNUMBER
                    StrResult = MyInterfacePCB.ReadUTLSerialNumber()

                Case Bitron3DEQ.ClassInterface.Command.UTL_GETUTL070RESULT
                    StrResult = MyInterfacePCB.GetUTLResult()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_OPENCOM
                    StrResult = MyInterfacePCB.STARTNORMALCom()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_CLOSECOM
                    StrResult = MyInterfacePCB.QUITNORMALCom()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_ALLON
                    StrResult = MyInterfacePCB.AllON()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_ALLOFF
                    StrResult = MyInterfacePCB.AllOFF()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_PART1ON
                    StrResult = MyInterfacePCB.Part1ON()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_PART2ON
                    StrResult = MyInterfacePCB.Part2ON()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_ENTERTESTMODE
                    StrResult = MyInterfacePCB.EnterTestMode()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_GETBUTTONVALUE
                    StrResult = MyInterfacePCB.GetButtonValue()

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_READCOMMAND
                    Dim FW As String
                    Dim ButtonValue As String
                    Dim CWValue As Integer
                    Dim CCWValue As Integer
                    Dim AllValue As String
                    StrResult = MyInterfacePCB.ReadCommandPara(FW, ButtonValue, CWValue, CCWValue, AllValue)

                    StrResult = StrResult & "~" & FW & "~" & ButtonValue & "~" & CWValue _
                        & "~" & CCWValue & "~" & AllValue & "~"

                Case Bitron3DEQ.ClassInterface.Command.NORMAL_READCOMMAND_ATGHP
                    Dim FW As String
                    Dim ButtonValue As String
                    Dim CWValue As Integer
                    Dim CCWValue As Integer
                    Dim AllValue As String
                    StrResult = MyInterfacePCB.ReadCommandPara_ATGHPHMI(FW, ButtonValue, CWValue, CCWValue, AllValue)

                    StrResult = StrResult & "~" & FW & "~" & ButtonValue & "~" & CWValue _
                        & "~" & CCWValue & "~" & AllValue & "~"

                    'Case Bitron3DEQ.ClassInterface.Command.SETLDU
                    '    StrResult = MyInterfacePCB.SetLDUType(Paras(0))
                    'Case ClassInterface.Command.CLSCOM
                    '    StrResult = MyInterfacePCB.CloseAMICom
                    'Case ClassInterface.Command.OPNCOM, ClassInterface.Command.VERCOM, ClassInterface.Command.TRGCOM
                    '    StrResult = MyInterfacePCB.OpenAMICom
                    'Case ClassInterface.Command.LDADDR
                    '    StrResult = MyInterfacePCB.LoadCommandAddress(Paras(0), Paras(1))

                    'Case ClassInterface.Command.DLLVERS
                    '    StrResult = MyInterfacePCB.GetDLLVersion
                    'Case ClassInterface.Command.RESET
                    '    StrResult = MyInterfacePCB.Reset
                    'Case ClassInterface.Command.ENTMODE
                    '    StrResult = MyInterfacePCB.EnterTestmode(Paras(0))
                    'Case ClassInterface.Command.GETFW
                    '    StrResult = MyInterfacePCB.GetFirmWare

                    'Case ClassInterface.Command.RDTEMP
                    '    StrResult = MyInterfacePCB.ReadTempSink()
                    'Case ClassInterface.Command.RDBATF
                    '    StrResult = MyInterfacePCB.ReadBattFilt()
                    'Case ClassInterface.Command.RDCRC
                    '    StrResult = MyInterfacePCB.ReadCRC()

                    'Case ClassInterface.Command.INJECTA
                    '    StrResult = MyInterfacePCB.InjectCurrent(CInt(Paras(0)), CInt(Paras(1)))
                    'Case ClassInterface.Command.RDITP
                    '    StrResult = MyInterfacePCB.ReadIThreePhase(CInt(Paras(0)))
                    'Case ClassInterface.Command.STPDRV
                    '    StrResult = MyInterfacePCB.StopDriving
                    'Case Bitron3DEQ.ClassInterface.Command.RDOFFSET
                    '    StrResult = MyInterfacePCB.ReadCurentOffset(CInt(Paras(0)))
                    'Case Bitron3DEQ.ClassInterface.Command.RDATRIP
                    '    StrResult = MyInterfacePCB.ReadCurentTrip(CInt(Paras(0)), CInt(Paras(1)))

                    'Case Bitron3DEQ.ClassInterface.Command.RDALARM
                    '    StrResult = MyInterfacePCB.ReadAlramMask
                    'Case ClassInterface.Command.SETPWM
                    '    StrResult = MyInterfacePCB.SetPWMDutyAt50


                    '    'Write Control Test report
                    'Case ClassInterface.Command.WRRPT
                    '    StrResult = MyInterfacePCB.WriteReport
                    '    'Write Finalize report
                    'Case ClassInterface.Command.WRFNLZ
                    '    StrResult = MyInterfacePCB.WriteFinalize(Paras(0), Paras(1), Paras(2))
                    '    'Read report
                    'Case ClassInterface.Command.RDRPT
                    '    StrResult = MyInterfacePCB.ReadReport

                    '    'Get write report result during Control FVT
                    'Case ClassInterface.Command.RDWFLAG
                    '    Dim Msg As String = "", RetList As New List(Of String)
                    '    StrResult = MyInterfacePCB.GetWriteReportResult(Msg)
                    '    StrResult = StrResult & "~" & Msg & "~"
                    '    For Each item In RetList
                    '        StrResult &= item & "~"
                    '    Next
                    '    'Read Control FVT test result flag during Complete FVT
                    'Case ClassInterface.Command.RDRFLAG
                    '    Dim Msg As String = "", RetList As New List(Of String)
                    '    StrResult = MyInterfacePCB.GetReadReportResult(Msg, RetList)
                    '    StrResult = StrResult & "~" & Msg & "~"
                    '    For Each item In RetList
                    '        StrResult &= item & "~"
                    '    Next

                    'Case ClassInterface.Command.RDWFINARST
                    '    Dim Msg As String = "", RetList As New List(Of String)
                    '    StrResult = MyInterfacePCB.GetWriteFinalizeResult(Msg, RetList)
                    '    StrResult = StrResult & "~" & Msg & "~"
                    '    For Each item In RetList
                    '        StrResult &= item & "~"
                    '    Next
                    'Case ClassInterface.Command.RDRFINARST
                    '    Dim Msg As String = "", RetList As New List(Of String)
                    '    StrResult = MyInterfacePCB.GetReadFinalizeResult(Msg, RetList)
                    '    StrResult = StrResult & "~" & Msg & "~"
                    '    For Each item In RetList
                    '        StrResult &= item & "~"
                    '    Next


                    '    'Boot protect
                    'Case ClassInterface.Command.BOOTPROT
                    '    StrResult = MyInterfacePCB.BootProtect


                    'Case ClassInterface.Command.RESTART
                    '    StrResult = MyInterfacePCB.CloseAMICom()
                    '    StrResult = True


            End Select
            If StrResult <> "?" Then StrResult = cmd.ToString & ";" & StrResult
        End If

        Return StrResult
    End Function

#End Region



    Private Sub ButStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButStop.Click
        Call StopServer()
    End Sub

    Private Sub ButStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButStart.Click
        Call StartServer()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub ButInitial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButInitial.Click
        Call InitialMachine()
    End Sub
End Class
