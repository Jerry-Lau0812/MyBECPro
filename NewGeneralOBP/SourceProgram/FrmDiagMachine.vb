﻿Imports Bitron3DEQ.FVTRackBoardManager
Imports Bitron3DEQ.FVTRackBoardManager.RackSystem
Imports Bitron3DEQ.FVTTestAssistant
Imports Bitron3DEQ.FVTRackBoardManager.UI
Imports Bitron3DEQ.FVTRackBoardManager.Driver
Public Class FrmDiagMachine

#Region "System running code(No need modify)"

#Region "Variant Declare"
    Private InitialOK As Boolean = False

    Private PreUUTIndex As Integer = 0 'Current UUT selected for PIC
    Private PreUUT_Int As Integer = 0  'Current UUT selected for Interface

    Private PowerOnState As Boolean = False
    Private MovePowerFingerState As Boolean = False
    Private ComIntState As Boolean = False
    Private Risposta As String = ""

    Private RadioPIC_UUTs() As RadioButton
    Private Diag_ACOutPanels() As Diag_ACOutPanel
    Private Diag_DCRelayPanels() As Diag_DCRelayPanel
    Private Diag_SwitchPanels() As Diag_SwitchPanel
    Private RadioInt_UUTs() As RadioButton
    Private UseMotor As Boolean
    Private Enum DCBoardTypes
        LVRL16
        HVRL08
    End Enum
#End Region

#Region "Initial"
    Private Sub FrmDiagMachine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitialOK = False
        For Each page As TabPage In TabCtrlMain.TabPages
            page.Parent = Nothing
        Next

        Call InitialPIC()
        Call InitialADAM()
        Call Initialks34972APanel()
        Call InitialMeter()
        Call InitialInterface()
        Call EnableAllGroup(False)
        Call InitialFlashRunner()

        InitialOK = True
    End Sub

    Private Sub InitialFlashRunner()
        TabPageFlashRunner.Parent = TabCtrlMain
    End Sub

    Private Sub InitialPIC()
        TabPagePIC.Parent = TabCtrlMain

        For Each page As TabPage In TabCtrlPICPanel.TabPages
            page.Parent = Nothing
        Next

        Call LoadPICCommand() 'Load all PIC command to command listbox
        Call IntialControl()
        Call Initial_CustomerControl()
        Call InitialACOutputPanel()
        Call InitialDCRelayPanel()
        Call InitialSwitchPanel()
        Call InitialMotorPanel()
    End Sub
    Private Sub IntialControl()
        RadioPIC_UUTs = New RadioButton() {RadioPIC_UUT1, RadioPIC_UUT2, RadioPIC_UUT3, RadioPIC_UUT4}

        For t As Integer = 0 To 3
            If t < UUTNum Then
                RadioPIC_UUTs(t).Enabled = True
            Else
                RadioPIC_UUTs(t).Enabled = False
            End If
        Next
    End Sub
    Private Sub InitialACOutputPanel()
        If ACOutputPanelCount <= 0 Then Exit Sub
        TabPageACOut.Parent = TabCtrlPICPanel

        Diag_ACOutPanels = New Diag_ACOutPanel() {Diag_ACOutPanel1, Diag_ACOutPanel2, Diag_ACOutPanel3, Diag_ACOutPanel4}
        For t As Integer = 0 To Diag_ACOutPanels.Count - 1
            If t < ACOutputPanelCount Then
                Diag_ACOutPanels(t).Visible = True
                Diag_ACOutPanels(t).SetoutDelegate = AddressOf GetACOutputs
            Else
                Diag_ACOutPanels(t).Visible = False
            End If
        Next

        Call InitialCustome_ACOutPanel()
    End Sub
    Private Sub InitialDCRelayPanel()
        If DCRelayPanelCount <= 0 Then Exit Sub
        TabPageDCRly.Parent = TabCtrlPICPanel
        Diag_DCRelayPanels = New Diag_DCRelayPanel() {Diag_DCRelayPanel1, Diag_DCRelayPanel2, Diag_DCRelayPanel3, Diag_DCRelayPanel4}

        For t As Integer = 0 To Diag_DCRelayPanels.Count - 1
            If t < DCRelayPanelCount Then
                Diag_DCRelayPanels(t).Visible = True
            Else
                Diag_DCRelayPanels(t).Visible = False
            End If
        Next
        Call InitialCustome_DCRelayPanel()
    End Sub
    Private Sub InitialSwitchPanel()
        If SwitchPanelCount <= 0 Then Exit Sub
        TabPageSwitch.Parent = TabCtrlPICPanel
        Diag_SwitchPanels = New Diag_SwitchPanel() {Diag_SwitchPanel1, Diag_SwitchPanel2}
        For t As Integer = 0 To Diag_SwitchPanels.Count - 1
            If t < SwitchPanelCount Then
                Diag_SwitchPanels(t).Visible = True
            Else
                Diag_SwitchPanels(t).Visible = False
            End If
        Next

        Call InitialCustome_SwitchPanel()
    End Sub
    Private Sub InitialMotorPanel()
        Dim HWList As New List(Of HardwareInformation)
        HWList = MyRack.GetHardwareInfos_External(ExtHardwareType.NewMOTOR)
        If HWList.Count <= 0 Then
            HWList = MyRack.GetHardwareInfos_External(ExtHardwareType.MOTOR)
            If HWList.Count <= 0 Then
                Exit Sub
            Else
                Me.PreMotorType = MotorTypes.OldMotor
            End If
        Else
            Me.PreMotorType = MotorTypes.NewMotor
        End If

        TabPageMotor.Parent = TabCtrlPICPanel
        For t As Integer = 1 To HWList.Count
            CombMotorCom.Items.Add("Port" & t)
        Next
        CombMotorCom.Text = CombMotorCom.Items(0)
        UseMotor = True
    End Sub
    Private Sub InitialADAM()
        Dim HWList As New List(Of HardwareInformation)
        HWList = MyRack.GetHardwareInfos_Internal(IntHardwareType.ADAM)
        If HWList.Count <= 0 Then
            HWList = MyRack.GetHardwareInfos_External(ExtHardwareType.ADAM4520)
            If HWList.Count <= 0 Then Exit Sub
        End If

        TabPageADAM.Parent = TabCtrlMain
        Call InitialCustome_ADAMPanel(HWList)
    End Sub
    Private Sub InitialMeter()
        Dim HWList As New List(Of HardwareInformation)
        For Each hardware As HardwareInformation In MyRack.GetHardwareInfos_External(ExtHardwareType.ACMETER8713)
            HWList.Add(hardware)
        Next
        For Each hardware As HardwareInformation In MyRack.GetHardwareInfos_External(ExtHardwareType.ACMETER8775)
            HWList.Add(hardware)
        Next
        For Each hardware As HardwareInformation In MyRack.GetHardwareInfos_External(ExtHardwareType.ACMETERZW16)
            HWList.Add(hardware)
        Next
        If HWList.Count <= 0 Then Exit Sub
        TabPageACMeter.Parent = TabCtrlMain

        With Diag_ACMeterPanel1

            .Initial(HWList)
        End With
    End Sub
    Private Sub InitialInterface()
        If InterfaceNum <= 0 Then Exit Sub
        RadioInt_UUTs = New RadioButton() {RadioInt_UUT1, RadioInt_UUT2, RadioInt_UUT3, RadioInt_UUT4}
        For t As Integer = 0 To 3
            If t < InterfaceNum Then
                RadioInt_UUTs(t).Enabled = True
            Else
                RadioInt_UUTs(t).Enabled = False
            End If
        Next
        TabPageInterface.Parent = TabCtrlMain
        PicOpen.Image = My.Resources.FVTResource.LED_Gray
        lblError.Text = "Error:"

        ' GroupBoxCmds.Enabled = False
        For Each ctrl In GroupBoxCmds.Controls
            If ctrl.GetType.Name = "Label" Then
                CType(ctrl, Label).Text = ""
                CType(ctrl, Label).BackColor = Color.Silver
            ElseIf ctrl.GetType.Name = "PictureBox" Then
                CType(ctrl, PictureBox).Image = My.Resources.FVTResource.LED_Gray
            End If
        Next

    End Sub

  
    Public Sub InitialKS34972APanel()
        Dim HWList As New List(Of HardwareInformation)
        HWList = MyRack.GetHardwareInfos_VISA(VISAHardwareType.KS34972A)
        If HWList.Count < 0 Then Exit Sub

        TabPage34972A.Parent = TabCtrlMain
        Call InitCustome_KS34972A(HWList)
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabCtrlMain.SelectedIndexChanged
        If InitialOK Then
            Select Case TabCtrlMain.SelectedTab.Name
                Case TabPageADAM.Name
                    Diag_ADAMPanel1.StartRead()
                Case TabPageACMeter.Name
                    Diag_ACMeterPanel1.StartRead()
                Case Else
                    Diag_ADAMPanel1.StopRead()
                    Diag_ACMeterPanel1.StopRead()
            End Select
        End If
    End Sub
#End Region

#Region "PIC Ready and set timeout"
    Private Sub ButReady_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButReady.Click
        PicReady.Image = My.Resources.FVTResource.LED_Yellow
        Application.DoEvents()

        If Board_PIC.VerComState Then
            PicReady.Image = My.Resources.FVTResource.LED_GreeN
            Call EnableAllGroup(True)

            Call Board_PIC.SetMachineUpTime()        'Set machine timeout
            ' Call Board_PIC.SetDebugMode(States.Set)  'Enable Remote Debug Mode
        Else
            PicReady.Image = My.Resources.FVTResource.LED_Red
            Call EnableAllGroup(False)
            Call Board_PIC.SetDebugMode(States.Reset) 'Disable Remote Debug Mode
        End If
    End Sub
    Private Sub EnableAllGroup(ByVal Enabled As Boolean)
        If Enabled = False Then
            PicReady.Image = My.Resources.FVTResource.LED_Gray
            PicPower.Image = My.Resources.FVTResource.LED_Gray
        End If
        For Each Ctrl As Control In TabCtrlPIC.TabPages(0).Controls
            If Ctrl.GetType.Name = "GroupBox" Then
                If Ctrl.Name <> GroupReady.Name Then
                    Ctrl.Enabled = Enabled
                End If
            ElseIf Ctrl.GetType.Name = "TabControl" Then
                Dim TmpTabCtrl As TabControl = CType(Ctrl, TabControl)
                For Each TAB As TabPage In TmpTabCtrl.TabPages
                    For Each Ctrl1 As Control In TAB.Controls
                        'If Ctrl1.GetType.Name = "GroupBox" Then
                        '    If Ctrl1.Name <> GroupReady.Name Then
                        Ctrl1.Enabled = Enabled
                        '   End If
                        'End If
                    Next
                Next
            End If
        Next
        GroupPowerON.Visible = UsePowerONPanel

        ButPIC_Up.Enabled = Enabled
        ButPIC_Down.Enabled = Enabled
    End Sub
    Private Sub ButDebug_Start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButPIC_Up.Click, ButPIC_Down.Click
        Dim Prebut As Button = CType(sender, Button)
        Dim SendCmd As String = ""
        Select Case Prebut.Name
            Case ButPIC_Up.Name
                Board_PIC.SetFixtureUP()
                ButPIC_Up.Enabled = False
                ButPIC_Down.Enabled = True
            Case ButPIC_Down.Name
                Board_PIC.SetFixtureDown()
                ButPIC_Up.Enabled = True
                ButPIC_Down.Enabled = False
        End Select
    End Sub

    Private Sub Radio_UUT1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioPIC_UUT1.Click, RadioPIC_UUT3.Click, RadioPIC_UUT4.Click, RadioPIC_UUT2.Click
        Dim preUUT As RadioButton = CType(sender, RadioButton)
        PreUUTIndex = CInt(preUUT.Tag)
    End Sub
#End Region

#Region "PIC Command debug panel"
    Private Sub LoadPICCommand()
        With ListPicCmd
            .Items.Add("READY")
            .Items.Add("MTOS")
            .Items.Add("END0000")
            .Items.Add("!~<")
            .Items.Add(">~!")



        End With
    End Sub
    Private Sub ButClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButClear.Click
        TxtCmd.Clear()
        TxtValue.Clear()
        ListRecieved.Items.Clear()
    End Sub
    Private Sub ButSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButSend.Click
        Dim SendCmd As String = TxtCmd.Text & TxtValue.Text
        Board_PIC.Send(SendCmd)
        Risposta = Board_PIC.Receive
        If Risposta <> "" Then
            ListRecieved.Items.Add(Risposta)
            LblInfo.Text = "OK"
            LblInfo.BackColor = Color.LimeGreen
        Else
            LblInfo.Text = "No answer!"
            LblInfo.BackColor = Color.Pink
        End If
    End Sub
    Private Sub ListPicCmd_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListPicCmd.MouseDoubleClick
        If ChKAppend.Checked Then
            TxtCmd.Text &= ListPicCmd.SelectedItem
        Else
            TxtCmd.Text = ListPicCmd.SelectedItem
        End If

    End Sub
#End Region

#Region "Interface"
    Private Sub RadioInt_UUT1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioInt_UUT1.Click, RadioInt_UUT4.Click, RadioInt_UUT3.Click, RadioInt_UUT2.Click
        Dim preUUT As RadioButton = CType(sender, RadioButton)
        PreUUT_Int = CInt(preUUT.Tag)
    End Sub
    Private Sub ButOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButOpen.Click
        ComIntState = Not ComIntState
        Dim SetResult As Boolean = False

        If ComIntState Then
            SetResult = MyInterface(PreUUT_Int).OpenCom()
            If SetResult Then
                PicOpen.Image = My.Resources.FVTResource.LED_GreeN
                GroupBoxCmds.Enabled = True
            Else
                PicOpen.Image = My.Resources.FVTResource.LED_Red
                GroupBoxCmds.Enabled = False
            End If
            Call IntialIntControl()
        Else
            SetResult = MyInterface(PreUUT_Int).CloseCom()
            PicOpen.Image = My.Resources.FVTResource.LED_Gray
            Call ResetIntControl()
        End If
    End Sub
    Private Sub ResetIntControl()
        For Each item As Control In GroupBoxCmds.Controls
            If item.GetType.Name = "TextBox" Then
                CType(item, TextBox).Clear()
            End If

            If item.GetType.Name = "PictureBox" Then
                CType(item, PictureBox).Image = My.Resources.FVTResource.LED_Gray
            End If
        Next
        GroupBoxCmds.Enabled = False
    End Sub

    Private Sub IntialIntControl()
        'CombInt_Modes.Items.Clear()
        'CombInt_Modes.Items.AddRange([Enum].GetNames(GetType(ClassInterface.eSubModes)))

    End Sub
#End Region

#Region "Common function"
    Private Sub SetState(ByVal SendCmd As String, ByVal ImgName As PictureBox, ByVal State As Boolean)
        Call Board_PIC.Send(SendCmd)
        Risposta = Board_PIC.Receive(1)

        If Risposta = SendCmd Then
            If State Then
                ImgName.Image = My.Resources.FVTResource.LED_GreeN
            Else
                ImgName.Image = My.Resources.FVTResource.LED_Gray
            End If
        Else
            ImgName.Image = My.Resources.FVTResource.LED_Red
        End If
    End Sub
    Private Sub SetState(ByVal SendCmd As String, ByRef State As Boolean, ByVal ImgName As PictureBox)
        Call Board_PIC.Send(SendCmd)
        Risposta = Board_PIC.Receive(1)

        If Risposta = SendCmd Then
            If State Then
                ImgName.Image = My.Resources.FVTResource.LED_GreeN
            Else
                ImgName.Image = My.Resources.FVTResource.LED_Gray
            End If
        Else
            ImgName.Image = My.Resources.FVTResource.LED_Red
        End If
    End Sub
    Private Sub SetState(ByVal ImgName As PictureBox, ByVal State As Boolean, ByVal Result As Boolean)
        If Result Then
            If State Then
                ImgName.Image = My.Resources.FVTResource.LED_GreeN
            Else
                ImgName.Image = My.Resources.FVTResource.LED_Gray
            End If
        Else
            ImgName.Image = My.Resources.FVTResource.LED_Red
        End If
    End Sub
#End Region

#Region "Motor Panel"
    Private PreMotorPort As Integer
    Private PreMotorIndex As Integer
    Private Enum MotorTypes
        OldMotor
        NewMotor
    End Enum
    Private PreMotorType As MotorTypes = MotorTypes.NewMotor
    Private Sub CombMotorCom_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombMotorCom.SelectedValueChanged
        If CombMotorCom.Text <> "" Then
            PreMotorPort = CInt(CombMotorCom.Text.Substring(4)) - 1
            Dim PreMotorCount As Integer = 0
            If PreMotorType = MotorTypes.NewMotor Then
                PreMotorCount = Device_NewMotor.MotorCount(PreMotorPort)
            Else
                PreMotorCount = Device_Motor.MotorCount(PreMotorPort)
            End If


            'Load all motor within current motor port
            CombMotorIndex.Items.Clear()
            CombMotorIndex.Items.Add("ALL")
            For t As Integer = 1 To PreMotorCount
                CombMotorIndex.Items.Add("Motor" & t)
            Next
            CombMotorIndex.Text = "ALL"
        End If
    End Sub
    Private Sub CombMotorIndex_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombMotorIndex.SelectedValueChanged
        If CombMotorIndex.Text <> "" Then
            If CombMotorIndex.Text = "ALL" Then
                PreMotorIndex = -1
                ButMotor_SetAsZero.Enabled = False
            Else
                PreMotorIndex = CInt(CombMotorIndex.Text.Substring(5)) - 1
                ButMotor_SetAsZero.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Move motor to zero position
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_GoZero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_GoZero.Click
        If PreMotorIndex < 0 Then
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.MoveToZero_All(PreMotorPort)
            Else
                Device_Motor.MoveToZero_All(PreMotorPort)
            End If
        Else
            Dim Zero As String = ""
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.MoveToZero(PreMotorIndex, PreMotorPort, Zero)
            Else
                Device_Motor.MoveToZero(PreMotorIndex, PreMotorPort, Zero)
            End If

            LblMotor_Zero.Text = Zero
        End If
    End Sub
    ''' <summary>
    ''' Set current position as zero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_SetAsZero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_SetAsZero.Click
        If PreMotorIndex >= 0 Then
            Dim Zero_Org As Integer = 0
            Dim Zero_Now As Integer = NumMotor_Position.Value
            Dim New_Zero As Integer = 0
            If PreMotorType = MotorTypes.NewMotor Then
                Zero_Org = Device_NewMotor.GetZeroPos(PreMotorIndex, PreMotorPort)
                New_Zero = Zero_Org + Zero_Now
                Device_NewMotor.SetZero(New_Zero, PreMotorIndex, PreMotorPort)
            Else
                Zero_Org = Device_Motor.GetZeroPos(PreMotorIndex, PreMotorPort)
                New_Zero = Zero_Org + Zero_Now
                Device_Motor.SetZero(New_Zero, PreMotorIndex, PreMotorPort)
            End If

            LblMotor_Zero.Text = New_Zero
            NumMotor_Position.Value = 0
        End If
    End Sub
    ''' <summary>
    ''' Move motor to specified position
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_GotoPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_GotoPos.Click
        If PreMotorIndex < 0 Then
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.MoveToPos_All(NumMotor_Position.Value, PreMotorPort)
            Else
                Device_Motor.MoveToPos_All(NumMotor_Position.Value, PreMotorPort)
            End If
        Else
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.MoveToPos(NumMotor_Position.Value, PreMotorIndex, PreMotorPort)
            Else
                Device_Motor.MoveToPos(NumMotor_Position.Value, PreMotorIndex, PreMotorPort)
            End If

        End If
    End Sub
    ''' <summary>
    ''' move motor run 1 cycle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_Run1Loop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_Run1Loop.Click
        If PreMotorIndex < 0 Then
            Board_RS16.StartRotate(0, PreMotorPort)
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.MoveOneLoop_All(PreMotorPort)
            Else
                Device_Motor.MoveOneLoop_All(PreMotorPort)
            End If
        Else
            Board_RS16.StartRotate(PreMotorIndex + 1, PreMotorPort)
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.MoveOneLoop(PreMotorIndex, PreMotorPort)
            Else
                Device_Motor.MoveOneLoop(PreMotorIndex, PreMotorPort)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Read Switch count
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_ReadCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_ReadCount.Click
        If PreMotorIndex < 0 Then
            Dim Counts() As Integer
            If PreMotorType = MotorTypes.NewMotor Then
                Counts = Device_NewMotor.GetStepCount_All(PreMotorPort)
            Else
                Counts = Device_Motor.GetStepCount_All(PreMotorPort)
            End If

            Dim StrCount As String = ""
            For Each item In Counts
                StrCount &= "-" & item
            Next
            ListMotor.Items.Add(StrCount)
        Else
            Dim Count As Integer
            If PreMotorType = MotorTypes.NewMotor Then
                Count = Device_NewMotor.GetStepCount(PreMotorIndex, PreMotorPort)
            Else
                Count = Device_Motor.GetStepCount(PreMotorIndex, PreMotorPort)
            End If
            ListMotor.Items.Add(Count)
        End If
    End Sub

    ''' <summary>
    ''' Get Torque value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_ReadTorque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_ReadTorque.Click
        If PreMotorIndex < 0 Then
            Dim Counts() As Integer
            If PreMotorType = MotorTypes.NewMotor Then
                Counts = Device_NewMotor.GetTorqueValue_All(PreMotorPort)
            Else
                Counts = Device_Motor.GetTorqueValue_All(PreMotorPort)
            End If

            Dim StrCount As String = ""
            For Each item In Counts
                StrCount &= "-" & item
            Next
            ListMotor.Items.Add(StrCount)
        Else
            Dim Count As Integer
            If PreMotorType = MotorTypes.NewMotor Then
                Count = Device_NewMotor.GetTorqueValue(PreMotorIndex, PreMotorPort)
            Else
                Count = Device_Motor.GetTorqueValue(PreMotorIndex, PreMotorPort)
            End If
            ListMotor.Items.Add(Count)
        End If
    End Sub

    ''' <summary>
    ''' Use RS8 or RS16 board to get anolog step value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_ECHOCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_ECHOCount.Click
        If PreMotorIndex < 0 Then
            Dim StrCount As String = ""
            Dim Count As Integer = 0
            For t As Integer = 1 To CombMotorIndex.Items.Count - 1
                Count = Board_RS16.GetStepCount(t, PreMotorPort, StrCount)
                ListMotor.Items.Add(Count & "," & StrCount)
            Next
        Else
            Dim StrCount As String = ""
            Dim Count As Integer = 0

            Count = Board_RS16.GetStepCount(PreMotorIndex + 1, PreMotorPort, StrCount)
            ListMotor.Items.Add(Count & "," & StrCount)
        End If
    End Sub

    ''' <summary>
    ''' Unlock Motor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_Unlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_Unlock.Click
        If PreMotorIndex < 0 Then
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.UnlockMotor_All(PreMotorPort)
            Else
                Device_Motor.UnlockMotor_All(PreMotorPort)
            End If
        Else
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.UnlockMotor(PreMotorIndex, PreMotorPort)
            Else
                Device_Motor.UnlockMotor(PreMotorIndex, PreMotorPort)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Send command to Motor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButMotor_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_Send.Click
        If PreMotorIndex < 0 Then
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.SendALL(TxtMotorCommand.Text, PreMotorPort, "", "")
            Else
                Device_Motor.SendALL(TxtMotorCommand.Text, PreMotorPort, "", "")
            End If
        Else
            If PreMotorType = MotorTypes.NewMotor Then
                Device_NewMotor.Send(TxtMotorCommand.Text, PreMotorIndex, PreMotorPort)
            Else
                Device_Motor.Send(TxtMotorCommand.Text, PreMotorIndex, PreMotorPort)
            End If
        End If
        Dim Risp As String = Device_Motor.Receive()
        If Risp = "" Then Risp = "No Rispond!"
        ListMotor.Items.Add(Risp)
    End Sub

    Private Sub ButMotor_Clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButMotor_Clear.Click
        ListMotor.Items.Clear()
    End Sub
#End Region
#End Region

#Region "Customer Code for all proudct"
    Private Const ACOutputPanelCount As Integer = 0 'AC optourcoulor Panel
    Private Const DCRelayPanelCount As Integer = 0  'DC relay panel
    Private Const SwitchPanelCount As Integer = 0   'Switch button Panel
    Private Const UsePowerONPanel As Boolean = True 'Power ON Panel

    Private Sub FrmDiagMachine_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If InterfaceNum > 0 Then
            'Call Board_PIC.Send(PICCmd.CINTF.ToString & "00")
            'Risposta = Board_PIC.Receive()
        End If

        Call MyTest.PowerOFF_FCT()
        Call MyTest.PowerOFF_PCB()
        Call MyTest.Discharge()

        If UseMotor Then
            'Update Motor Parameter to XML file
            Dim FoundModify As Boolean = False
            For t As Integer = 0 To CombMotorCom.Items.Count - 1
                If Device_NewMotor.ParameterChanged(t) Then
                    FoundModify = True
                    Exit For
                End If
            Next
            If FoundModify Then MyRack.SaveProject()
        End If

        MachineState = MachineStates.Ready
    End Sub

    Private Sub Initial_CustomerControl()

    End Sub

    Private Sub InitialCustome_ACOutPanel()
        With Diag_ACOutPanels(0)
            .OutNames(0) = "X"


            .Initial()
        End With
    End Sub
    Private Function GetACOutputs(ByVal Sender As Object) As String()
        Dim PreOutputPanel As Diag_ACOutPanel = CType(Sender, Diag_ACOutPanel)
        Dim PrePanelIndex As Integer = CInt(PreOutputPanel.Tag)

        Dim ACOUT(7) As String
        Risposta = Board_HVOC.ReadOutputs(PrePanelIndex, True)
        If Risposta.Length > 0 Then
            ACOUT = Risposta.Split("-")
        End If

        Return ACOUT
    End Function

    Private Sub InitialCustome_DCRelayPanel()
        With Diag_DCRelayPanels(0)
            .Relays(0).Name = "A"


        End With
    End Sub
    Private Sub InitialCustome_SwitchPanel()
        With Diag_SwitchPanels(0)
            .SwitchSettings(0).Name = "A"



            .Initial()
        End With

    End Sub
    Private Sub InitialCustome_ADAMPanel(ByVal HWList As List(Of HardwareInformation))
        With Diag_ADAMPanel1
            .ADAMInfos(0).CHs(0).Description = "-"





            .Initial(HWList)
        End With
    End Sub
    Public Sub InitCustome_KS34972A(ByVal HWList As List(Of HardwareInformation))
        With Diag_KS34972APanel1

            ''KS1 Module1===============================================
            .KSInfos(0).KSModulue1.ModuleType = Diag_KS34972APanel.ModuleTypes.KS34901A
            .KSInfos(0).KSModulue1.CHs(0).SetVoltageParameter("Vbat Pin1 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(1).SetVoltageParameter("HallPWR Pin07 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(2).SetVoltageParameter("HallInput Pin15 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(3).SetVoltageParameter("FR Pin09 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(4).SetVoltageParameter("LR Pin08 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(5).SetVoltageParameter("SW1 Pin12 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(6).SetVoltageParameter("SW2 Pin02 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(7).SetVoltageParameter("GNDSignal Pin17 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(8).SetVoltageParameter("GNDSignal Pin16 UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue1.CHs(9).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)

            .KSInfos(0).KSModulue1.CHs(10).SetVoltageParameter("Vbat Pin1 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(11).SetVoltageParameter("HallPWR Pin07 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(12).SetVoltageParameter("HallInput Pin15 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(13).SetVoltageParameter("FR Pin09 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(14).SetVoltageParameter("LR Pin08 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(15).SetVoltageParameter("SW1 Pin12 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(16).SetVoltageParameter("SW2 Pin02 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(17).SetVoltageParameter("GNDSignal Pin17 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(18).SetVoltageParameter("GNDSignal Pin16 UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.VoltageRange._100V, KS34972AInfo.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue1.CHs(19).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            ''Current Channel
            .KSInfos(0).KSModulue1.CHs(20).SetCurrentParameter("Current UUT1", KS34972AInfo.VoltageType.DC, KS34972AInfo.CurrentRange._10mA, KS34972AInfo.Resolution.FiveAndAHalf)
            .KSInfos(0).KSModulue1.CHs(21).SetCurrentParameter("Current UUT2", KS34972AInfo.VoltageType.DC, KS34972AInfo.CurrentRange._10mA, KS34972AInfo.Resolution.FiveAndAHalf)


            ' ''KS1 Module2===============================================
            '.KSInfos(0).KSModulue2.ModuleType = Diag_KS34972APanel.ModuleTypes.KS34903A
            '.KSInfos(0).KSModulue2.CHs(0).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(1).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(2).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(3).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(4).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(5).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(6).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(7).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(8).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(9).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(10).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(11).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(12).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(13).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(14).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(15).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(16).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(17).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(18).SetSwitchParameter("SW1")
            '.KSInfos(0).KSModulue2.CHs(19).SetSwitchParameter("SW1")



            ' ''KS1 Module3===============================================
            '.KSInfos(0).KSModulue3.ModuleType = Diag_KS34972APanel.ModuleTypes.KS34901A
            '.KSInfos(0).KSModulue3.CHs(0).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(1).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(2).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(3).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(4).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(5).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(6).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(7).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(8).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(9).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(10).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(11).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(12).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(13).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(14).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(15).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(16).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(17).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(18).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(19).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            ' ''Current Channel
            '.KSInfos(0).KSModulue3.CHs(20).SetCurrentParameter("Curr1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(0).KSModulue3.CHs(21).SetCurrentParameter("Curr2", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)



            ' ''KS2 Module1===============================================
            '.KSInfos(1).KSModulue1.ModuleType = Diag_KS34972APanel.ModuleTypes.KS34901A
            '.KSInfos(1).KSModulue1.CHs(0).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(1).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(2).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(3).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(4).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(5).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(6).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(7).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(8).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(9).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(10).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(11).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(12).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(13).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(14).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(15).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(16).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(17).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(18).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(19).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            ' ''Current Channel
            '.KSInfos(1).KSModulue1.CHs(20).SetCurrentParameter("Curr1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue1.CHs(21).SetCurrentParameter("Curr2", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)


            ' ''KS2 Module2===============================================
            '.KSInfos(1).KSModulue2.ModuleType = Diag_KS34972APanel.ModuleTypes.KS34901A
            '.KSInfos(1).KSModulue2.CHs(0).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(1).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(2).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(3).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(4).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(5).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(6).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(7).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(8).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(9).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(10).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(11).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(12).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(13).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(14).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(15).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(16).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(17).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(18).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(19).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            ' ''Current Channel
            '.KSInfos(1).KSModulue2.CHs(20).SetCurrentParameter("Curr1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue2.CHs(21).SetCurrentParameter("Curr2", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)



            ' ''KS2 Module3===============================================
            '.KSInfos(1).KSModulue3.ModuleType = Diag_KS34972APanel.ModuleTypes.KS34901A
            '.KSInfos(1).KSModulue3.CHs(0).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(1).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(2).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(3).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(4).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(5).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(6).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(7).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(8).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(9).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(10).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(11).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(12).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(13).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(14).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(15).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(16).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(17).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(18).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(19).SetVoltageParameter("Volt1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.VoltageRange._10V, Driver_KS34972A.Resolution.FiveAndAHalf)
            ' ''Current Channel
            '.KSInfos(1).KSModulue3.CHs(20).SetCurrentParameter("Curr1", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)
            '.KSInfos(1).KSModulue3.CHs(21).SetCurrentParameter("Curr2", Driver_KS34972A.VoltageType.DC, Driver_KS34972A.CurrentRange._1A, Driver_KS34972A.Resolution.FiveAndAHalf)


            .Initial(HWList)
        End With
    End Sub

    Private Sub Diag_DCRelayPanel1_RelayClick(ByVal Sender As System.Object, ByVal e As Bitron3DEQ.FVTRackBoardManager.UI.Diag_DCRelayPanel.RelayClickEventArgs) Handles Diag_DCRelayPanel1.RelayClick, Diag_DCRelayPanel2.RelayClick, Diag_DCRelayPanel4.RelayClick, Diag_DCRelayPanel3.RelayClick
        Dim RlyNo As Integer
        Dim BDIndex As Integer
        Dim BDType As DCBoardTypes
        Dim PrePanel As Diag_DCRelayPanel = CType(Sender, Diag_DCRelayPanel)
        Select Case PrePanel.Tag
            Case 0
                BDType = DCBoardTypes.LVRL16
                BDIndex = 0
                RlyNo = e.Index
            Case 1
                BDType = DCBoardTypes.LVRL16
                BDIndex = 0
                RlyNo = e.Index + 8
            Case 2
                BDType = DCBoardTypes.HVRL08
                BDIndex = 0
                RlyNo = e.Index
            Case 3
                BDType = DCBoardTypes.HVRL08
                BDIndex = 1
                RlyNo = e.Index
        End Select

        If BDType = DCBoardTypes.LVRL16 Then
            If e.States = States.Set Then
                Board_LVRL16.CloseRelay(RlyNo, BDIndex)
            Else
                Board_LVRL16.OpenRelay(RlyNo, BDIndex)
            End If
        Else
            If e.States = States.Set Then
                Board_HVRL08.CloseRelay(RlyNo, BDIndex)
            Else
                Board_HVRL08.OpenRelay(RlyNo, BDIndex)
            End If
        End If
    End Sub
    Private Sub Diag_DCRelayPanel1_RelayOpenAll(ByVal Sender As System.Object) Handles Diag_DCRelayPanel1.RelayOpenAll, Diag_DCRelayPanel2.RelayOpenAll, Diag_DCRelayPanel4.RelayOpenAll, Diag_DCRelayPanel3.RelayOpenAll
        Dim BDIndex As Integer
        Dim BDType As DCBoardTypes
        Dim PrePanel As Diag_DCRelayPanel = CType(Sender, Diag_DCRelayPanel)
        Select Case PrePanel.Tag
            Case 0
                BDType = DCBoardTypes.LVRL16
                BDIndex = 0
            Case 1
                BDType = DCBoardTypes.LVRL16
                BDIndex = 0
            Case 2
                BDType = DCBoardTypes.HVRL08
                BDIndex = 0
            Case 3
                BDType = DCBoardTypes.HVRL08
                BDIndex = 1
        End Select

        If BDType = DCBoardTypes.LVRL16 Then
            Board_LVRL16.OpenAllRelay(BDIndex)
        Else
            Board_HVRL08.OpenAllRelay(BDIndex)
        End If
    End Sub

    Private Sub Diag_SwitchPanel1_SwitchClick(ByVal Sender As System.Object, ByVal e As Bitron3DEQ.FVTRackBoardManager.UI.Diag_SwitchPanel.SwitchClickEventArgs) Handles Diag_SwitchPanel1.SwitchClick, Diag_SwitchPanel2.SwitchClick
        Dim PrePanel As Diag_SwitchPanel = CType(Sender, Diag_SwitchPanel)
        Board_LVRL16.CloseRelay(e.Index, PrePanel.Tag)

        If e.Mode = Diag_SwitchPanel.SwitchModes.Keep Then Exit Sub
        If e.Mode = Diag_SwitchPanel.SwitchModes.Normal Then
            Threading.Thread.Sleep(e.SleepTime)
        End If

        Board_LVRL16.OpenRelay(e.Index, PrePanel.Tag)
    End Sub
    Private Sub Diag_SwitchPanel1_SwitchClickAll(ByVal Sender As System.Object, ByVal e As Bitron3DEQ.FVTRackBoardManager.UI.Diag_SwitchPanel.SwitchClickEventArgs) Handles Diag_SwitchPanel1.SwitchClickAll, Diag_SwitchPanel2.SwitchClickAll
        Dim PrePanel As Diag_SwitchPanel = CType(Sender, Diag_SwitchPanel)

        If e.Mode <> Diag_SwitchPanel.SwitchModes.Release Then
            Board_LVRL16.SetRelay(e.Status, PrePanel.Tag)
        End If
        If e.Mode = Diag_SwitchPanel.SwitchModes.Keep Then Exit Sub
        If e.Mode = Diag_SwitchPanel.SwitchModes.Normal Then
            Threading.Thread.Sleep(e.SleepTime)
        End If

        Board_LVRL16.OpenAllRelay(PrePanel.Tag)
    End Sub


#End Region

#Region "Customer code for present product"
    Private Sub ButPower_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButPower.Click
        PowerOnState = Not PowerOnState
        PicPower.Image = My.Resources.FVTResource.LED_Yellow
        Application.DoEvents()

        'Power ON
        Dim Result As Boolean
        If PowerOnState Then
            Result = MyTest.PowerON_PCB
            Result = MyTest.PowerON_FCT
        Else
            Result = MyTest.PowerOFF_FCT
            Result = MyTest.PowerOFF_PCB
        End If

        Call SetState(PicPower, PowerOnState, Result)
        If Result = False Then PowerOnState = False
    End Sub
    Private Sub But_FingerPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_FingerPanel.Click
        MovePowerFingerState = Not MovePowerFingerState

        'Move Finger Panel
        Dim Result As Boolean
        If MovePowerFingerState Then
            'Result = MyTest.MoveupFingerPanel
        Else
            ' Result = MyTest.MovedownFingerPanel
        End If

        Call SetState(Pic_FingerUpDown, MovePowerFingerState, Result)
        If Result = False Then MovePowerFingerState = False
    End Sub


#End Region

#Region "Customer code for Interface panel"
    Private Sub ButInt_DLLVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButInt_DLLVersion.Click, ButInt_EnterTestMode.Click
        Dim PreBut As Button = CType(sender, Button)

        Select Case PreBut.Name
            Case ButInt_DLLVersion.Name
                ' TxtInt1.Text = MyInterface(PreUUT_Int).GetFwVersion


        End Select


        ' lblError .Text =
    End Sub
#End Region

    Dim FlashIntState As Boolean = False
    Private Sub Btn_ConnectFlashRunner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ConnectFlashRunner.Click
        FlashIntState = Not FlashIntState
        Pic_FlashInter.Image = My.Resources.FVTResource.LED_Yellow
        Application.DoEvents()

        'Power ON
        Dim Result As Boolean
        If FlashIntState Then
            Result = Fr2.OpenCommunicationA
        Else
            Result = Fr2.CloseCommunicationA
        End If

        Call SetState(Pic_FlashInter, FlashIntState, Result)
        If Result = False Then FlashIntState = False
    End Sub

    Private FlashResult As New List(Of String)
    Private Sub Btn_StartFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_StartFlash.Click
        Txt_FlahInfo.Text = ""

        FlashResult.Clear()
        Dim ProjectName As String = Txt_FW_Project.Text '工程名+通道　“TESLA_OHC20200303.prj:0001”代表只烧写CH1
        Dim FlashList As New List(Of String)
        FlashList.Add(ProjectName)

        Dim Res As Boolean = False
        Res = Fr2.FlashMultiProgram(FlashList)

        Btn_StartFlash.BackColor = Color.Yellow

        If Not Res Then
            Txt_FlahInfo.Text = "Start Flash Error"
            Return
        End If

        Res = Fr2.Wait_FlashProgram_FlashRunner2_0("0001", FlashResult) '0001代表 CH4，CH3,CH2，CH1中只检查CH1通道结果

        For Each Site As String In FlashResult
            Txt_FlahInfo.AppendText(Site & vbCrLf)
        Next

        'If Not Res Then
        '    Txt_FlahInfo.Text = "Get Result Error"
        'End If

        

        If Res Then
            Btn_StartFlash.BackColor = Color.Green
        Else
            Btn_StartFlash.BackColor = Color.Red
        End If
    End Sub

    Private Sub Btn_GetFVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_GetFVer.Click
        Dim Ver As String = Fr2.GetFr2Version
        Txt_FlashVer.Text = Ver
    End Sub

End Class