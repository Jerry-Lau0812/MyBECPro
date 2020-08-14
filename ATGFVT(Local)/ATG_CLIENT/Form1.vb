Imports System.IO
Imports System.Threading.Thread
Imports Bitron3DEQ.FVTRackBoardManager
Imports Bitron3DEQ.FVTRackBoardManager.RackSystem

Public Class Form1

    Public Const InterfaceNum As Integer = 2
    Public MyInterface(InterfaceNum) As ClassInterface
    Public MyRack As New RackSystem
    Private Risposta As String
    Private PreUUT As Integer = 0
    Private _FirstLoadOK As Boolean = False

    Public Enum CMDS
        EnterTestMode
        AllON
        Part1ON
        Part2ON
        AllOFF
        GetButtonValue
    End Enum


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If InterfaceNum > 0 Then
            For t As Integer = 0 To InterfaceNum - 1
                MyInterface(t) = New ClassInterface(t)
            Next
            MyInterface(0).ComMode = Bitron3DEQ.ClassInterface.ComModes.Default
            MyInterface(1).ComMode = Bitron3DEQ.ClassInterface.ComModes.Client


            With RackSystem.Device_Interface
                .DelegateOpenCom = AddressOf OpenComInterface
                .DelegateCloseCom = AddressOf CloseComInterface
                .DelegateVerCom = AddressOf VerComInterface
                .DelegateTrigCom = AddressOf TrigComInterface
            End With
        End If

        InitialCmds()

    End Sub


    Private Sub InitialCmds()
        CmdCmds.Items.Clear()
        CmdCmds.Items.AddRange(System.Enum.GetNames(GetType(CMDS)))

        CmdCmds.Text = CmdCmds.Items(1)
    End Sub



#Region "Used for control by FVTRacksystem.dll"
    Public Function OpenComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        Dim Result As Boolean = MyInterface(e.Index).OpenCom()
        Return Result
    End Function
    Public Function CloseComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        If e.Index < InterfaceNum Then
            Dim Result As Boolean = MyInterface(e.Index).CloseCom()
            Return Result
        End If
    End Function
    Public Function VerComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        Dim Result As Boolean = MyInterface(e.Index).VerCom(e)
        Return Result
    End Function
    Public Function TrigComInterface(ByVal e As Driver.PortEventArgs) As Boolean
        Dim Result As Boolean = MyInterface(e.Index).TrigCom(e)
        Return Result
    End Function
#End Region

    Private Sub RadButUUT1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButUUT1.Click
        If RadButUUT1.Checked Then PreUUT = 0
    End Sub

    Private Sub RadButUUT2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButUUT2.Click
        If RadButUUT2.Checked Then PreUUT = 1
    End Sub

    Private Sub ButInt_OpenCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButInt_OpenCom.Click
        Dim Res As Boolean = MyInterface(PreUUT).STARTUTL070Com("38400")
    End Sub

    Private Sub ButInt_CloseCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButInt_CloseCom.Click
        Dim Res As Boolean = MyInterface(PreUUT).QUITUTL070Com()
    End Sub

    Private Sub Btn_Initial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Initial.Click
        MyRack.XMLConfiguePath = Application.StartupPath & "\SysFile\HWConfigure.xml"
        If _FirstLoadOK Then
            For t As Integer = 0 To InterfaceNum - 1
                MyInterface(t).CloseCom()
            Next
        End If

        If MyRack.InitialRackSystem() Then
            Dim LoadAddr As String = ""
            MyInterface(0).CloseCom() 'Ami Interface
            'For t As Integer = 0 To InterfaceNum - 1
            '    LoadAddr = MyInterface(t).OpenCom
            'Next
            _FirstLoadOK = True

        Else
        End If
    End Sub

    Private Sub Btn_Set_SerialNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_CMD_Set_SerialNum.Click
        Dim Res As Boolean = MyInterface(PreUUT).SetUTL070SerialNumber(TextSN.Text)
    End Sub


    Private Sub Btn_CMD_ReadResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_CMD_ReadResult.Click
        Dim Res As String = MyInterface(PreUUT).GetUTLResult()
        Txt_Status.Text = Res
    End Sub

    Private Sub Btn_CMD_Com_Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_CMD_Com_Open.Click
        Dim Res As String = MyInterface(PreUUT).OpenUTLCOM()
    End Sub

    Private Sub Btn_CMD_Com_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_CMD_Com_Close.Click
        Dim Res As String = MyInterface(PreUUT).CloseUTLCOM()
    End Sub

    Private Sub Btn_Normal_COM_Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Normal_COM_Open.Click
        Dim Res As Boolean = MyInterface(PreUUT).STARTNORMALCom()
    End Sub

    Private Sub Btn_Normal_COM_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Normal_COM_Close.Click
        Dim Res As Boolean = MyInterface(PreUUT).QUITNORMALCom()
    End Sub

    Private Sub Btn_Enter_TestMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Enter_TestMode.Click
        Dim Res As Boolean = MyInterface(PreUUT).EnterTestMode()
    End Sub

    Private Sub Btn_SendCmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SendCmd.Click
        Dim Result As Boolean = False
        Dim CMD As CMDS = System.Enum.Parse(GetType(CMDS), CmdCmds.Text)
        Select Case CMD
            Case CMDS.EnterTestMode
                Result = MyInterface(PreUUT).EnterTestMode()
            Case CMDS.AllON
                Result = MyInterface(PreUUT).AllON()
            Case CMDS.AllOFF
                Result = MyInterface(PreUUT).AllOFF
            Case CMDS.Part1ON
                Result = MyInterface(PreUUT).Part1ON
            Case CMDS.Part2ON
                Result = MyInterface(PreUUT).Part2ON
        End Select
        Txt_Status.Text = Result.ToString()
    End Sub

    Private Sub Btn_ReadInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ReadInfo.Click
        Dim FW As String = ""
        Dim ButtonValue As String = ""
        Dim CW As Integer
        Dim CCW As Integer
        Dim AllValue As String
        Dim Result As String = MyInterface(PreUUT).ReadCommandPara(FW, ButtonValue, CW, CCW, AllValue)

        Txt_Status.Text = Result & " " & FW & " " & ButtonValue & " " & CW & " " & CCW & " " & AllValue
    End Sub

    Private Sub Btn_ReadIDTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ReadIDTable.Click
        Dim Res As Boolean = MyInterface(PreUUT).ReadUTLIDTable()
    End Sub

    Private Sub Btn_SWVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SWVer.Click
        Dim Res As Boolean = False
        Res = MyInterface(PreUUT).ReadUTLSWVersion
        Txt_Status.Text = Res
    End Sub

    Private Sub Btn_WriteDGTO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_WriteDGTO.Click
        Dim Res As Boolean = False
        Res = MyInterface(PreUUT).WriteUTLDGTO(Txt_Write_DGTO.Text, Txt_DGTO_Value.Text)
        Txt_Status.Text = Res
    End Sub

    Private Sub Btn_Write_TDA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Write_TDA.Click
        Dim Res As Boolean = False
        Res = MyInterface(PreUUT).WriteUTLTDAInfo()
        Txt_Status.Text = Res
    End Sub

    Private Sub Btn_Read_DGTO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Read_DGTO.Click
        Dim Res As Boolean = False
        Res = MyInterface(PreUUT).ReadUTLDGTO(TxtReadDGTO.Text)
        Txt_Status.Text = Res
    End Sub

    Private Sub Btn_WriteTDARegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_WriteTDARegister.Click
        Dim Res As Boolean = False
        Res = MyInterface(PreUUT).WriteUTLTDAResult("P")
        Txt_Status.Text = Res
    End Sub

    Private Sub Btn_Read_SN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Read_SN.Click
        Dim Res As Boolean = False
        Res = MyInterface(PreUUT).ReadUTLSerialNumber()
        Txt_Status.Text = Res
    End Sub

    Private Sub Btn_ReadMac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ReadMac.Click
        Dim Res As Boolean = False
        Res = MyInterface(PreUUT).ReadUTLMAC()
        Txt_Status.Text = Res
    End Sub
End Class
