﻿Imports Bitron3DEQ.Sherlock
Imports Bitron3DEQ.FVTTestAssistant
Imports Bitron3DEQ.FVTRackBoardManager
Imports Bitron3DEQ.FVTRackBoardManager.RackSystem
Imports Bitron3DEQ.FVTRackBoardManager.UI
Imports Bitron3DEQ.FVTRackBoardManager.Driver
Imports Bitron3DEQ.BitronDefinedClass
Imports System.IO

Public Class Frmpannello
#Region "No need modified code"

#Region "Variant Declare"
    Public Shared TimeSleep As Integer = 0  'Time used
    Private YMaxScale As Integer = 0
    Private XMaxScale As Integer = 0
#End Region

#Region "Start Application"
    Private Sub Frmpannello_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'MyTAT.SetScanLabelFocus()
        MyTAT.SetScanMatrixFocus()
    End Sub
    Private Sub Frmpannello_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = MyTAT.Sysinfo.ApplicationName

        Call ReloadAndDispInfos()
    End Sub
    Private Sub Frmpannello_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Space Then
            MyTest.SpaceKeyPressed = True
        End If
    End Sub
    ''' <summary>
    ''' 重新加载设置-当用户选择其他产品，切换操作模式时发生
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReloadAndDispInfos()
        Call DispTestMode()         'Set all Menu,button etc. state accroding current Testmode
        If MachineState = MachineStates.Fault Then
            ToolButUnload.Enabled = False
            Exit Sub
        End If

        'UUTNum = CType(DatiProd.Value(ProdOtherInfos.UUTNum), Integer)
        UUTNum = 2
        EndUUT = UUTNum - 1
        MyTAT.Sysinfo.System.Project.UUTNum = UUTNum
        MyTAT.SysSettings.Sysparas.Project.UUTNum = UUTNum

        Try
            Call MyTest.GetFlashInfosFromDB()
            Call AutoSetUUT()       'Auto set UUT number
            Call LoadOperators()    'Load all Operator name to listbox
            Call DispProdVersionInfo()  'Display Product version in Datagridview
            Call DispLabelColor()       'Set Label color
            Call DispPassfail()         'Display Pass Fail Qty
            Call DispOperatorName()     'Display Operator name
            Call DispCurrentTestMode()  'Display Test Mode: product,Retest,or Debug  
            Call DispApplicationInfo()  'Display Application Name,Version...
            Call MyBench.SetDataToDatagridview(DGVTestItem)
            Call MyTest.FillTestStepInfos() 'Fill all Test Step information

            Call TranslateTestItems()
            Call CollapseAllRow_DGVResult() 'Collap All row
            Call ClearTestResult()      'Clear Display result area

            Call CheckProgramCanUseOrNot(DatiProd.Value(ProdInfos.CodeBitron))
            Call MyTest.InitFlashRunner()

            Call MyTest.CheckFlashNameFromCyclone() 'compare flash name from cyclone
            Call SetImageInitialState() 'Set image to initial state
            Call SetConciseInitialState() 'set concise mode to initial state
            Call DispResetMachinePanel() 'Reset Fixture  and camera motor to Zero

            Call MyTest.DispFailTube()
            Call DispScanFiches()
            Call DispManaulScanPanel()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            MachineState = MachineStates.Fault
        End Try

        Call DispSplitMain()
    End Sub

    ''' <summary>
    ''' 显示主面板及设置相关的按钮状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DispSplitMain()
        Dim Enabled As Boolean
        If MachineState = MachineStates.Ready Then
            Enabled = True
            ToolStripStatusLblMachineState.Text = "Ready"
            ToolStripStatusLblMachineState.BackColor = Color.Cyan
            Me.ToolStripStatusLblTestMode.Text = TestMode.ToString

            Select Case MyTAT.Sysinfo.DispMode
                Case DisplayModes.Table : TabCtrlDisplay.SelectedTab = TabPageDispTable
                Case DisplayModes.Picture : TabCtrlDisplay.SelectedTab = TabPageDispPicture
                Case DisplayModes.Concise : TabCtrlDisplay.SelectedTab = TabPageDispConcise
            End Select
        Else
            Enabled = False
            ToolStripStatusLblMachineState.Text = "Not Ready"
            ToolStripStatusLblMachineState.BackColor = Color.Red
            Me.ToolStripStatusLblTestMode.Text = ""
        End If

        SplitMain.Visible = Enabled

        'Menu Item state
        MenuLoadProgram.Enabled = Not Enabled
        MenuUnloadProgram.Enabled = Enabled

        'ToolStripButton State
        ToolButLoad.Enabled = Not Enabled
        ToolButUnload.Enabled = Enabled

        Call DispMachineState()
        Call SetOtherProdControl()
    End Sub

    ''' <summary>
    ''' 生产模式及调试模式间切换时的处理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispTestMode()
        Dim Enabled As Boolean
        Dim MenuColor As Color
        Dim ForeColor As Color

        If TestMode = TestModes.Debug Then
            Enabled = True
            ToolStripStatusLblTestMode.Text = "Debug Mode"
            If Not SkipCheckCom Then MenuDignostic.Visible = True
            MenuColor = Color.DarkSalmon 'Menu控件颜色
            ForeColor = Color.Red        '选择行时的字体颜色
            'TabPageMachine.Parent = tabLeftBottom 'Machine debug tools

            ToolStripSelStep.Enabled = False
            ToolStripDebug.Enabled = False
            ToolButPause.Enabled = False
            ToolButStop.Enabled = False
            ToolButReExecute.Enabled = False
            Call ExpandAllRow() '展开所有行
        Else
            Enabled = False
            ToolStripStatusLblTestMode.Text = "Product Mode"
            MenuDignostic.Visible = False
            MenuColor = Color.SteelBlue
            ForeColor = Color.Green

            'TabPageMachine.Parent = Nothing
        End If

        'change menu and Status BackColor
        MenuStrip.BackColor = MenuColor
        StatusStrip.BackColor = MenuColor

        'debug & product button
        ToolButDebugMode.Enabled = Not Enabled
        ToolButProdMode.Enabled = Enabled
        MenuDebug.Enabled = Not Enabled
        MenuProduct.Enabled = Enabled
        MenuConfiguration.Enabled = Enabled
        MenuTranslator.Enabled = Enabled

        'datagridview DGVTestItem
        DGVTestItem.Columns("ColBreak").Visible = Enabled
        DGVTestItem.Columns("ColTest").ReadOnly = Not Enabled
        DGVTestItem.DefaultCellStyle.SelectionForeColor = ForeColor
        DGVTestItem.MultiSelect = Enabled

        'BreakPoint,Debug and Select tools
        ToolStripSelStep.Visible = Enabled
        ToolStripDebug.Visible = Enabled
        ToolStripSelect.Visible = Enabled
        ToolStrip_RemoteDebug.Visible = Enabled

        'Stop timer
        Me.TimeTimeUsed.Enabled = False
    End Sub

    ''' <summary>
    ''' Check program can be used or not(2018.1.10)
    ''' </summary>
    ''' <param name="BitronCode"></param>
    ''' <remarks></remarks>
    Private Sub CheckProgramCanUseOrNot(ByVal BitronCode As String)
        Dim Msg As String = ""
        Dim BitronCodeState As CheckProgramLock.ClassCheckProgramLock.BitroncodeState = CheckProgramLock.ClassCheckProgramLock.BitroncodeState.NoUsable
        Dim PogogramLock As CheckProgramLock.ClassCheckProgramLock = New CheckProgramLock.ClassCheckProgramLock(MyTAT.SysSettings.Sysparas.Supervisual.SQLServer)
        Dim Result As Boolean = PogogramLock.GetProgramUsableByBitronCode(BitronCode, BitronCodeState, Msg)
        If Result Then
            If BitronCodeState = CheckProgramLock.ClassCheckProgramLock.BitroncodeState.NoUsable Then
                Call SelectProduct()

                If BitronCode = DatiProd.Value(ProdInfos.CodeBitron) Then
                    MyTAT.HideScanMatrixForm()
                    MyTAT.ScanFiches.HideScanner()

                    Call MyTAT.SysSettings.ProdHistory.UpdateBitronCode(BitronCode)
                    MachineState = MachineStates.Ready
                    Call StartFVT()
                    Call ReloadAndDispInfos()
                    Application.DoEvents()
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' 设置Debug工具条各按钮状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDebugToolState()
        If RunState = RunStates.Pause Then
            Me.TimeTimeUsed.Stop()
            Me.ToolStripSelStep.Enabled = True

            'Debug tool button
            Me.ToolButRun.Enabled = True
            Me.ToolButPause.Enabled = False
            Me.ToolButStop.Enabled = True
            Me.ToolButReExecute.Enabled = True
            Me.ToolStripDebug.Enabled = True
            Me.ToolStrip_RemoteDebug.Enabled = False

        ElseIf RunState = RunStates.Run Or RunState = RunStates.ExecCurrent Then
            Me.TimeTimeUsed.Start()
            Me.ToolStripSelStep.Enabled = False

            'Debug tool button
            Me.ToolButRun.Enabled = False
            Me.ToolButPause.Enabled = True
            Me.ToolButStop.Enabled = False
            Me.ToolButReExecute.Enabled = False
            Me.ToolStripDebug.Enabled = True

            If TestStep.NextStep.Master > TestStep.MasterNums Then
                Me.ToolStrip_RemoteDebug.Enabled = True
            Else
                Me.ToolStrip_RemoteDebug.Enabled = False
            End If
        Else
            Me.TimeTimeUsed.Stop()
            Me.ToolStripSelStep.Enabled = False
            Me.ToolStripDebug.Enabled = False
            Me.ToolStrip_RemoteDebug.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' 设置其他按钮状态
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOtherProdControl()
        If MachineState <> MachineStates.Ready Then Exit Sub

        If MyTAT.Sysinfo.System.ManualScanLabel = "1" Then
            ButReadBarcode.Enabled = True
        Else
            ButReadBarcode.Enabled = False
        End If

        If MyTAT.Sysinfo.Sherlock.CameraNum > 0 Then
            ButSherlock.Enabled = True
        Else
            ButSherlock.Enabled = False
        End If

        If MyTAT.ScanFiches.ScanEnabled Then
            ButScanFiches.Enabled = True
            ButEditBarcodes.Enabled = True
        Else
            ButScanFiches.Enabled = False
            ButEditBarcodes.Enabled = False
        End If


    End Sub

    ''' <summary>
    ''' 自动设置UUT
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AutoSetUUT()
        If DatiProd.Value(ProdOtherInfos.UUTPosition) <> "" Then
            Dim UUTPos As Integer '多块不同的板共用一套测试设备
            UUTPos = CInt(DatiProd.Value(ProdOtherInfos.UUTPosition))
            IniUUT = UUTPos
            EndUUT = IniUUT
        End If

        Call AutoSetResultLabel()
        Call AutoSetResultDGV()
        'Call AutoSetConciseUI()
        Call DispMachineStateInit()
    End Sub

    ''' <summary>
    ''' 加载所有操作者姓名
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadOperators()
        '*****************************************
        'Load operators
        '*****************************************
        Try
            Dim f As New FileInfo(MyTAT.Sysinfo.System.OperatorPath)
            Dim DR As StreamReader = f.OpenText
            CombOperator.Items.Clear()
            Dim StrRead As String
            Dim OprValues() As String
            Dim foundOld As Boolean = False

            'Read all operators
            While Not DR.EndOfStream
                StrRead = DR.ReadLine
                If StrRead.IndexOf(",") > 0 Then
                    OprValues = StrRead.Split(",")
                    CombOperator.Items.Add(OprValues(1))
                    If OprValues(1) = MyTAT.Sysinfo.System.OperatorName Then
                        foundOld = True
                    End If
                End If
            End While
            DR.Close()

            'speical an operator displayed in combbox
            If Not foundOld Then
                MyTAT.Sysinfo.System.OperatorName = CombOperator.Items(0).ToString
            End If
        Catch ex As Exception
            Throw New Exception("Load Operator Fail! Please check the path or file format is correct!" & vbNewLine & ex.Message & ":" & MyTAT.Sysinfo.System.OperatorPath)
        End Try
    End Sub

    ''' <summary>
    ''' 设置当前工程名称等信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispApplicationInfo()
        '******************************************
        'display Application Name,Version etc. 
        'information in ToolStripStatus
        '******************************************
        ToolStripStatusLblProduct.Text = MyTAT.Sysinfo.ApplicationName
        Me.Text = MyTAT.Sysinfo.ApplicationName
        ToolStripStatusLblVersion.Text = MyTAT.Sysinfo.ProdVersion
        ToolStripStatusLblTestMode.Text = ""
        LblProductName.Text = MyTAT.Sysinfo.ApplicationName
    End Sub

    ''' <summary>
    ''' 显示当前产品版本信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispProdVersionInfo()
        '*********************************************
        'Display product version relative information
        'on Frmpannello
        '*********************************************
        DGVProdVersion.Rows.Clear()
        For Each Item As ProdInfos In [Enum].GetValues(GetType(ProdInfos))
            If DatiProd.GetDispedtemState(Item) Then
                If Item = ProdInfos.ProdDesc Then
                    Call AddProdVersionInfo(Item.ToString, DatiProd.Value(Item), 2)
                Else
                    Call AddProdVersionInfo(Item.ToString, DatiProd.Value(Item))
                End If
            End If
        Next

        For Each Item As ProdOtherInfos In [Enum].GetValues(GetType(ProdOtherInfos))
            If DatiProd.GetDispedtemState(Item) Then
                Call AddProdVersionInfo(Item.ToString, DatiProd.Value(Item))
            End If
        Next

    End Sub
    Private Sub AddProdVersionInfo(ByVal Item As String, ByVal Value As String, Optional ByVal RowHeight As Integer = 1)
        '**********************************************
        'Add new rows to DataGridView: DGVProdVersion
        '**********************************************
        With DGVProdVersion
            Dim NewRow As New DataGridViewRow
            For Each c As DataGridViewColumn In .Columns
                NewRow.Cells.Add(c.CellTemplate.Clone)  'Add cells for new row
            Next

            NewRow.Cells(0).Value = Item   'Set cell's value
            NewRow.Cells(1).Value = Value
            NewRow.Height = .RowTemplate.Height * RowHeight 'Set Row height
            .Rows.Add(NewRow)
        End With

    End Sub

    ''' <summary>
    ''' 显示标签颜色
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispLabelColor()
        '**************************************************
        'Display label color that used for present product
        '**************************************************
        PanelLabelColor.BackColor = MyTAT.GetColor(DatiProd.Value(ProdInfos.LabelColor))
    End Sub

    ''' <summary>
    ''' 显示Pass及Fail数量并绘制饼图
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispPassfail()
        '*********************************************************
        'Dispaly pass fail number 
        'Calculate fail percent
        'draw fail percent pie graphic
        '*********************************************************
        If AbortTest Then
            Call SetDispStep(DispSteps.TestAbort)
            Exit Sub
        End If

        Dim Rate As Single
        lblPassNum.Text = MyTAT.Sysinfo.PassNum.ToString
        lblFailNum.Text = MyTAT.Sysinfo.FailNum.ToString
        Dim SumNum As Long = MyTAT.Sysinfo.PassNum + MyTAT.Sysinfo.FailNum
        If SumNum <> 0 Then
            Rate = MyTAT.Sysinfo.FailNum / SumNum
        Else
            Rate = 0
        End If
        lblPercentNum.Text = Microsoft.VisualBasic.Format(Rate, "0.00%")
        LblAmount.Text = MyTAT.Sysinfo.Amount

        Call DrawDial(Rate * 100)
    End Sub
    Private Sub DrawDial(ByVal ValFail As Integer)
        With PicDial
            Dim UperLeft As New Point(.Width / 2 - .Height / 2, 0)
            Dim Size As New Size(.Height - 2, .Height - 2)
            If ValFail > 100 Then ValFail = 100
            Dim GradiRosso As Single = CType(Int((360 * ValFail) / 100), Single)

            Dim Img As New Bitmap(.Width, .Height)
            Dim f As Graphics = Graphics.FromImage(Img)

            f.FillPie(Brushes.Red, New Rectangle(UperLeft, Size), 0, -GradiRosso)
            f.DrawPie(Pens.Black, New Rectangle(UperLeft, Size), 0, -GradiRosso)

            f.FillPie(Brushes.Green, New Rectangle(UperLeft, Size), -GradiRosso, GradiRosso - 360)
            f.DrawPie(Pens.Black, New Rectangle(UperLeft, Size), -GradiRosso, GradiRosso - 360)

            .BackgroundImage = Img
        End With
    End Sub

    ''' <summary>
    ''' 显示操作者姓名
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispOperatorName()
        CombOperator.Text = MyTAT.Sysinfo.System.OperatorName
    End Sub

    ''' <summary>
    ''' 显示当前操作模式
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DispCurrentTestMode()
        '********************************
        'Set test mode
        '********************************
        If TestMode = TestModes.Debug Then
            radioButProduct.Enabled = False
            radioButRetest.Enabled = False
            radioButDebug.Enabled = True
            radioButNPI.Enabled = False
        Else
            radioButProduct.Enabled = True
            radioButRetest.Enabled = True
            radioButDebug.Enabled = False
            radioButNPI.Enabled = True
        End If

        Select Case MyTAT.Sysinfo.ProdMode
            Case TestModes.Product
                radioButProduct.Checked = True
            Case TestModes.Retest
                radioButRetest.Checked = True
            Case TestModes.Debug
                radioButDebug.Checked = True
            Case TestModes.NPI
                radioButNPI.Checked = True
            Case Else
                radioButProduct.Checked = True
        End Select

        Call SetProdMode(MyTAT.Sysinfo.ProdMode)
    End Sub

    ''' <summary>
    ''' 显示手动扫描窗体
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DispManaulScanPanel()
        Dim BarcodeLen As String = DatiProd.Value(ProdOtherInfos.BarcodeLength)
        Dim BarcodeLen1 As String = DatiProd.Value(ProdOtherInfos.BarcodeLength1)
        Dim BarcodeLen2 As String = DatiProd.Value(ProdOtherInfos.BarcodeLength2)
        If BarcodeLen.Trim() = String.Empty Then BarcodeLen = "0"
        If BarcodeLen1.Trim() = String.Empty Then BarcodeLen1 = "0"
        If BarcodeLen2.Trim() = String.Empty Then BarcodeLen2 = "0"

        Call MyTAT.ShowScanMatrixForm(BarcodeLen, BarcodeLen1, BarcodeLen2)

        'If MyTAT.Sysinfo.System.ManualScanLabelCount = 2 Then
        '    MyTAT.ShowScanLabelForm(DatiProd.Value(ProdOtherInfos.BarcodeLength), DatiProd.Value(ProdInfos.LabelColor), DatiProd.Value(ProdOtherInfos.BarcodeLength2))
        'Else
        '    MyTAT.ShowScanLabelForm(DatiProd.Value(ProdOtherInfos.BarcodeLength), DatiProd.Value(ProdInfos.LabelColor))
        'End If
    End Sub
    ''' <summary>
    ''' 显示Fiches扫描框
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DispScanFiches()
        MyTAT.ScanFiches.StartScan()
    End Sub

    ''' <summary>
    ''' 进度增加1
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub IncProgBar()
        '*****************************************
        'Increase the test progress bar
        '*****************************************
        If ProBarTest.Value < ProBarTest.Maximum Then
            ProBarTest.Increment(1)
        Else
            ProBarTest.Value = ProBarTest.Maximum
        End If
    End Sub
    ''' <summary>
    ''' 显示初始化Fixture马达窗体
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispResetMachinePanel()
        If MyTAT.SysSettings.Sysparas.FixMoveType = FixtureMoveTypes.DCMotor Then
            If Device_NewMotor.ComPorts.Count <= 0 Then Exit Sub
            If Device_NewMotor.NeedReset Then
                Dim f As New FrmResetMachine
                f.ResetFixture = True
                f.FixtureMotortype = FrmResetMachine.FixtureMotor.SmartMotor
                f.CameraComPort = 0 'Change portnum as need
                f.FixtureComPort = 0 'Change portnum as need
                If MyTAT.SysSettings.Sherlockparas.CameraNum > 0 Then
                    f.ResetCamera = True
                    f.IniCameraPosition = DatiProd.Value(ProdOtherInfos.IniCameraPosition)
                End If
                f.ShowDialog()
                f.Close()
            Else
                If MyTAT.SysSettings.Sherlockparas.CameraNum > 0 Then
                    MyTest.MoveCameraToPosition(DatiProd.Value(ProdOtherInfos.IniCameraPosition))
                End If
            End If
        End If
    End Sub



#End Region

#Region "End Application"
    Private Sub Frmpannello_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If UseSql Then MyMemSQL.CloseDB()
            CloseSherlock()
            For t As Integer = 0 To InterfaceNum - 1
                MyInterface(t).CloseCom()
                MyInterface(t).Dispose()
            Next

            MyRack.Dispose()
            MyTAT.Dispose()
        Catch ex As Exception
        End Try

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "Common function"
    Private Sub CollapseAllRow_DGVResult()
        For Each row As DataGridViewRow In DGVTestItem.Rows
            If Not row Is Nothing Then
                If IsFatherNode(row.Index) Then
                    Call CollapseOrExpandDGVResult(row.Index + 1, False)
                End If
                row.Selected = False
            End If
        Next
        On Error Resume Next
        DGVTestItem.Rows(0).Selected = True
    End Sub
    Private Sub CollapseAllRow()
        For Each row As DataGridViewRow In DGVTestItem.Rows
            If Not row Is Nothing Then
                If IsFatherNode(row.Index) Then
                    CollapseRow(row.Index)
                    Call CollapseOrExpandDGVResult(row.Index + 1, False)
                End If
                row.Selected = False
            End If
        Next
        On Error Resume Next
        DGVTestItem.Rows(0).Selected = True
    End Sub
    Public Sub CollapseRow(ByVal RowIndex As Integer)
        If IsFatherNode(RowIndex) Then
            CType(DGVTestItem.Rows(RowIndex).Cells(DGVTestItem.Columns("ColFold").Index), DataGridViewFoldCell).Collapse()
        End If
    End Sub
    Private Sub ExpandAllRow()
        For Each row As DataGridViewRow In DGVTestItem.Rows
            If Not row Is Nothing Then
                If IsFatherNode(row.Index) Then
                    ExpandRow(row.Index)
                End If
                row.Selected = False
            End If
        Next
        For Each row As DataGridViewRow In DGVTestResult.Rows
            row.Visible = True
        Next

        On Error Resume Next
        DGVTestItem.Rows(0).Selected = True
    End Sub
    Public Sub ExpandRow(ByVal RowIndex As Integer)
        If IsFatherNode(RowIndex) Then
            CType(DGVTestItem.Rows(RowIndex).Cells(DGVTestItem.Columns("ColFold").Index), DataGridViewFoldCell).Expand()
            Call CollapseOrExpandDGVResult(RowIndex + 1, True)
        End If
    End Sub
    Public Function IsFatherNode(ByVal RowIndex As Integer) As Boolean
        With DGVTestItem
            If RowIndex < 0 Then Exit Function
            Dim PreCell As DataGridViewCell = .Rows(RowIndex).Cells(.Columns("ColNo").Index)
            If InStr(PreCell.Value, "-") > 0 Then
                Return False
            Else
                Return True
            End If
        End With
    End Function

    ''' <summary>
    ''' 取消对所有行的选择，用于每次测试开始时的初始状态设置
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CancelSelectAllRow()
        For Each row As DataGridViewRow In DGVTestItem.SelectedRows
            row.Selected = False
        Next

        For Each row As DataGridViewRow In DGVTestResult.SelectedRows
            row.Selected = False
        Next
    End Sub
#End Region

#Region "MenuStrip Program"
    Private Sub MenuUnloadProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuUnloadProgram.Click
        MachineState = MachineStates.Fault
        Call DispSplitMain()
    End Sub
    Private Sub MenuQuitApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuQuitApplication.Click
        Application.Exit()
    End Sub

    ''' <summary>
    ''' 重新加载
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuLoadProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuLoadProgram.Click
        Call StartFVT()
        Call ReloadAndDispInfos()
        If MachineState = MachineStates.Ready Then DispScanFiches()
    End Sub
    Private Sub MenuBrowseFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuBrowseFiles.Click
        MyRack.ShowLogfileBroswer()
    End Sub
    Private Sub MenuProductStatistic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuProductStatistic.Click
        Dim MyDic As New Dictionary(Of String, String)
        MyDic = MyBench.Products
        MyTAT.ShowStatisticForm(MyDic)
    End Sub

    Private Sub TestItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuTestItem.Click
        MachineState = MachineStates.Diagnostic
        MyBench.ShowEditBench()
        MachineState = MachineStates.Ready
    End Sub
    Private Sub SystemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuSystem.Click
        MyTAT.SysSettings.ShowSettingsPanel()
    End Sub
    Private Sub ToolStripExportReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExportReport.Click
        'Dim myexport As New FrmExportReport
        'Try
        '    myexport.ShowDialog()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub
    Private Sub SelectProductToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuSelectProduct.Click
        Call SelectProduct()
    End Sub
#End Region

#Region "MenuStrip Product"
    Private Sub MenuProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuProduct.Click
        Call ResetAndUpdatePassFail()

        Call StartFVT()

        TestMode = TestModes.Product
        MyTAT.Sysinfo.ProdMode = TestModes.Product
        radioButProduct.Checked = True

        Call ReloadAndDispInfos()
        If MachineState = MachineStates.Ready Then DispScanFiches()
    End Sub
    Private Sub MenuDebug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDebug.Click
        Call ResetAndUpdatePassFail()

        If ModuleMain.DebugProgam Then
            TestMode = TestModes.Debug
            SherlockProject.TestMode = SherlockProject.TestModes.Debug
        Else
            If MyTAT.ShowForm_Login Then
                TestMode = TestModes.Debug
            End If
        End If

        MyTAT.Sysinfo.ProdMode = TestModes.Debug
        Call DispCurrentTestMode()
        Call DispTestMode()
    End Sub
    Private Sub CloseSherlock()
        '********************************************
        'Close Sherlock first when switch test mode
        '********************************************
        If MyTAT.Sysinfo.Sherlock.CameraNum > 0 Then
            MySherLock.CloseCamera()
            SherlockProject.TestMode = SherlockProject.TestModes.Test
        End If
    End Sub
    Private Sub ResetAndUpdatePassFail()
        If MachineState <> MachineStates.Fault Then
            Call ResetPassFail()
            Call DispPassfail()
            Call CloseSherlock()
        End If
    End Sub
#End Region

#Region "MenuStrip Diagnostic"
    Private Sub DiagMachineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDiagMachine.Click
        MyRack.StartSpyPIC = False  'Stop SpyPIC
        MachineState = MachineStates.Diagnostic
        FrmDiagMachine.Show()
        MyRack.StartSpyPIC = True
    End Sub
    Private Sub DiagFlashToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDiagFlash.Click
        'Auto find out flash device count
        'Auto find out flash device count
        Dim MyFlashDevice As New List(Of HardwareInformation)
        Dim FlashType As FrmDiagFlashProg.FlashDevices = FrmDiagFlashProg.FlashDevices.WriteNow

        MyFlashDevice = MyRack.GetHardwareInfos_External(ExtHardwareType.WRITENOW)
        If MyFlashDevice.Count <= 0 Then
            MyFlashDevice = MyRack.GetHardwareInfos_External(ExtHardwareType.FLASHRUNNER)
            FlashType = FrmDiagFlashProg.FlashDevices.FlashRunner
        End If
        If MyFlashDevice.Count <= 0 Then
            MyFlashDevice = MyRack.GetHardwareInfos_External(ExtHardwareType.Cyclone)
            FlashType = FrmDiagFlashProg.FlashDevices.Cyclone
        End If
        If MyFlashDevice.Count <= 0 Then
            MsgBox("No Flash Device is aviable!", MsgBoxStyle.Information)
            Exit Sub
        End If

        MachineState = MachineStates.Diagnostic
        Dim frmFlash As New FrmDiagFlashProg
        With frmFlash.FlashSeting
            .FlashCount = MyFlashDevice.Count
            .FlashDevice = FlashType
            .FlashTimeOut = 20

            .Scripts.Add(New FrmDiagFlashProg.ClassScript(DatiProd.Value(ProdOtherInfos.FlashScript), DatiProd.Value(ProdOtherInfos.RelFW1)))
        End With
        AddHandler frmFlash.DisconnectPowerEvent, AddressOf MyTest.DisconnectPowerWithFlash
        AddHandler frmFlash.ConnectFlashEvent, AddressOf MyTest.ConnectFlashWithPCB
        AddHandler frmFlash.DisconnectFlashEvent, AddressOf MyTest.DisConnectFlashWithPCB
        frmFlash.InitialFlash()
        frmFlash.ShowDialog()

        MachineState = MachineStates.Ready
    End Sub

    ''' <summary>
    ''' 远程调试按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripBut_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBut_Up.Click, ToolStripBut_StartTest.Click, ToolStripBut_Down.Click, ToolStripBut_UnBlock.Click
        Dim PreBut As ToolStripButton = CType(sender, ToolStripButton)

        Select Case PreBut.Name
            Case ToolStripBut_StartTest.Name
                Board_PIC.StartTestOnDebugMode()
            Case ToolStripBut_Up.Name
                Board_PIC.SetFixtureUP()
            Case ToolStripBut_Down.Name
                Board_PIC.SetFixtureDown()
            Case ToolStripBut_UnBlock.Name
                Board_PIC.SendUnblock()
        End Select
    End Sub
    ''' <summary>
    ''' 显示信号及相机马达调整面板
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButShowSignal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButShowSignal.Click
        MyRack.StartSpyPIC = False  'Stop SpyPIC
        MachineState = MachineStates.Diagnostic

        Dim MyFrmSignal As New FrmFVTSignalPanel
        With MyFrmSignal
            If MyTAT.Sysinfo.Sherlock.CameraNum > 0 Then
                .ShowMotorpanel = True
                .MotorAddress = 1
                .IniPos = DatiProd.Value(ProdOtherInfos.IniCameraPosition)
            Else
                .ShowMotorpanel = False
            End If
        End With
        MyFrmSignal.Show()
        MachineState = MachineStates.Free
        MyRack.StartSpyPIC = True
    End Sub
    Private Sub ToolButAutoEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButAutoEnd.Click
        AutoEndTest = Not AutoEndTest
        If AutoEndTest Then
            ToolButAutoEnd.Image = My.Resources.FVTResource.lightning
        Else
            ToolButAutoEnd.Image = My.Resources.FVTResource.lightning_delete
        End If
    End Sub
#End Region

#Region "MenuStrip help"
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuAbout.Click
        MyTAT.ShowForm_About(MyTAT.Sysinfo.ApplicationName)
    End Sub
#End Region

#Region "主窗体左侧功能区按钮"
    ''' <summary>
    ''' 产品选择
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButSelProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButSelProd.Click
        Call SelectProduct()
    End Sub
    Private Sub SelectProduct()
        Dim BitronCode As String = MyBench.ShowSelectProduct(DatiProd.Value(ProdInfos.CodeBitron))
        If BitronCode <> DatiProd.Value(ProdInfos.CodeBitron) Then
            MyTAT.HideScanMatrixForm()
            MyTAT.ScanFiches.HideScanner()

            Call MyTAT.SysSettings.ProdHistory.UpdateBitronCode(BitronCode)
            MachineState = MachineStates.Ready
            Call StartFVT()
            Call ReloadAndDispInfos()

            'Call DispScanFiches()
            'MyTAT.ShowScanLabelForm(DatiProd.Value(ProdOtherInfos.BarcodeLength))
            Application.DoEvents()
        End If
    End Sub

    ''' <summary>
    ''' 重绘饼图
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PicDial_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicDial.Resize
        If Me.Visible Then
            Call DispPassfail()
        End If
    End Sub

    ''' <summary>
    ''' 数量清零
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub butReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butReset.Click
        Call ResetPassFail()
        Call DispPassfail()
    End Sub

    ''' <summary>
    ''' 测试模式改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radioButProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButProduct.Click, radioButRetest.Click, radioButDebug.Click, radioButNPI.Click
        Dim PreRadioBut As RadioButton = CType(sender, RadioButton)
        Call ResetPassFail()
        Call DispPassfail()

        If PreRadioBut.Checked Then
            Select Case PreRadioBut.Name
                Case radioButProduct.Name
                    MyTAT.Sysinfo.ProdMode = TestModes.Product
                Case radioButRetest.Name
                    MyTAT.Sysinfo.ProdMode = TestModes.Retest
                Case radioButDebug.Name
                    MyTAT.Sysinfo.ProdMode = TestModes.Debug
                Case radioButNPI.Name
                    MyTAT.Sysinfo.ProdMode = TestModes.NPI
            End Select
        End If

        Call SetProdMode(MyTAT.Sysinfo.ProdMode)
        Call ResetPassFail()
    End Sub
    Private Sub SetProdMode(ByVal Mode As TestModes)
        '**********************************
        'Display Mode string and set backcolor
        '**********************************
        Select Case Mode
            Case TestModes.Product
                PanelTestMode.BackColor = Color.Green
                LblMode.Text = radioButProduct.Text
            Case TestModes.Retest
                PanelTestMode.BackColor = Color.Red
                LblMode.Text = radioButRetest.Text
            Case TestModes.Debug
                PanelTestMode.BackColor = Color.DarkOrange
                LblMode.Text = radioButDebug.Text
            Case TestModes.NPI
                PanelTestMode.BackColor = Color.DeepSkyBlue
                LblMode.Text = radioButNPI.Text
        End Select
    End Sub

    ''' <summary>
    ''' 操作者姓名改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CombOperator_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CombOperator.Text <> MyTAT.Sysinfo.System.OperatorName Then
            MyTAT.Sysinfo.System.OperatorName = CombOperator.Text
            Call MyTAT.SysSettings.ProdHistory.UpdateOperator(MyTAT.Sysinfo.System.OperatorName)
        End If
    End Sub

    ''' <summary>
    ''' 退出系统
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButQuit.Click
        Application.Exit()
    End Sub

#End Region

#Region "附加功能按钮"
    ''' <summary>
    ''' 启动读条码窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButReadBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButReadBarcode.Click
        Call DispManaulScanPanel()
    End Sub

    ''' <summary>
    ''' 启动Sherlock窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButSherlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButSherlock.Click
        tabLeftBottom.SelectedTab = tabPageTimer
        Application.DoEvents()
        MySherLock.InitialCamera()
    End Sub

    ''' <summary>
    ''' 启动Fiches扫描窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButScanFiches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButScanFiches.Click
        MyTAT.ScanFiches.StartScan()
    End Sub

    ''' <summary>
    ''' 编辑已有条码
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButEditBarcodes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButEditBarcodes.Click
        MyTAT.ScanFiches.ShowBarcodeEditor()
    End Sub

    ''' <summary>
    ''' 测试中止
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure abort test Now?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
            'MyTest.AbortTest = True
        End If
    End Sub
#End Region

#Region "Datagridview DGVTestItem and DGVResult Synchronization effect"
    Private Sub CollapseOrExpandDGVResult(ByVal StartRowIndex As Integer, ByVal Value As Boolean)
        '**************************************
        'Collapse or Expand Fold Cell
        '**************************************
        Try
            Dim TmpStepNo As String
            If MachineState <> MachineStates.Fault Then
                For t As Integer = StartRowIndex To DGVTestItem.Rows.Count - 1
                    TmpStepNo = DGVTestItem.Rows(t).Cells(DGVTestItem.Columns("ColNo").Index).Value
                    If InStr(TmpStepNo, "-") > 0 Then
                        DGVTestResult.Rows(t).Visible = Value
                    Else
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGVTestItem_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGVTestItem.CellMouseDown
        If e.ColumnIndex = DGVTestItem.Columns("ColFold").Index Then
            If IsFatherNode(e.RowIndex) Then
                Dim FoldCell As DataGridViewFoldCell = CType(DGVTestItem.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewFoldCell)
                If FoldCell.Collapsed Then
                    CollapseOrExpandDGVResult(e.RowIndex + 1, False)
                Else
                    CollapseOrExpandDGVResult(e.RowIndex + 1, True)
                End If
            End If
        Else
            Try
                For Each row As DataGridViewRow In DGVTestResult.SelectedRows
                    row.Selected = False
                Next
                DGVTestResult.Rows(e.RowIndex).Selected = True
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub DGVTestItem_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVTestItem.CellValueChanged
        '******************************************
        'Chose all or not when press father node
        '******************************************
        If TestMode = TestModes.Debug Then
            If DGVTestItem.Columns(e.ColumnIndex).Name = "ColTest" Then
                Dim PreRow As DataGridViewRow = DGVTestItem.Rows(e.RowIndex)
                Dim PreTestFlags As String = PreRow.Cells(DGVTestItem.Columns("ColNo").Index).Value
                If InStr(PreTestFlags, "-") <= 0 Then 'if is father node
                    Dim PreTestState As Boolean = Convert.ToBoolean(PreRow.Cells(DGVTestItem.Columns("ColTest").Index).Value)
                    For t As Integer = e.RowIndex + 1 To DGVTestItem.Rows.Count - 1
                        Dim ChildFlag As String = DGVTestItem.Rows(t).Cells(DGVTestItem.Columns("ColNo").Index).Value
                        If InStr(ChildFlag, PreTestFlags & "-") > 0 Then
                            DGVTestItem.Rows(t).Cells(DGVTestItem.Columns("ColTest").Index).Value = PreTestState
                        Else
                            Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub
    Private Sub DGVTestItem_CurrentCellDirtyStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVTestItem.CurrentCellDirtyStateChanged
        If DGVTestItem.IsCurrentCellDirty Then
            DGVTestItem.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Public Function GetCheckState(ByVal RowIndex As Integer) As Boolean
        '***********************************************
        'Get the check or skip flag of special row 
        '***********************************************
        Dim BoolResult As Boolean
        BoolResult = Convert.ToBoolean(DGVTestItem.Rows(RowIndex).Cells(DGVTestItem.Columns("ColTest").Index).Value)
        Return BoolResult
    End Function
    Public Function GetBreakState(ByVal RowIndex As String)
        '***********************************************
        'Get the BreakPoint flag of special row 
        '***********************************************

        Dim BoolResult As Boolean
        Dim BreakCell As DataGridViewBreakPointCell
        BreakCell = CType(DGVTestItem.Rows(RowIndex).Cells(DGVTestItem.Columns("ColBreak").Index), DataGridViewBreakPointCell)
        BoolResult = Convert.ToBoolean(BreakCell.IsBreakPoint)
        Return BoolResult
    End Function

    Public Sub SetBreakPoint(ByVal RowIndex As Integer)
        '***********************************************
        'Set the special row with Break Point
        '***********************************************
        With DGVTestItem
            If RowIndex >= 0 And RowIndex < .RowCount Then
                Dim BreakCell As DataGridViewBreakPointCell
                BreakCell = CType(.Rows(RowIndex).Cells(.Columns("ColBreak").Index), DataGridViewBreakPointCell)
                BreakCell.SetBreakPoint(True)
            End If
        End With
    End Sub

    Private Sub DGVTestItem_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DGVTestItem.Scroll
        DGVTestResult.FirstDisplayedScrollingRowIndex = DGVTestItem.FirstDisplayedScrollingRowIndex
    End Sub
    Private Sub DGVTestItem_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVTestItem.SelectionChanged
        If MachineState = MachineStates.Run Then
            If TestMode = TestModes.Product Then
                On Error Resume Next
                DGVTestItem.FirstDisplayedScrollingRowIndex = DGVTestItem.SelectedRows(0).Index
            Else
                If RunState <> RunStates.Pause Then
                    On Error Resume Next
                    DGVTestItem.FirstDisplayedScrollingRowIndex = DGVTestItem.SelectedRows(0).Index - 10
                End If
            End If
        End If
    End Sub
#End Region

#Region "Datagridview DGVTestResult"

    ''' <summary>
    ''' 清除上一次的测试结果
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearTestResult()
        For Each row As DataGridViewRow In DGVTestResult.Rows
            For Each col As DataGridViewColumn In DGVTestResult.Columns
                row.Cells(col.Index).Value = ""
                row.Cells(col.Index).Style.BackColor = Color.White
            Next
        Next
    End Sub

    Private Sub AutoSetResultDGV()
        '*********************************************
        'Auto Set Test Result DGV Column
        '*********************************************
        With DGVTestResult
            For t As Integer = 2 To UUTNum
                Dim Col As New DataGridViewColumn
                Col = DGVTestResult.Columns(0).Clone
                DGVTestResult.Columns.Add(Col)
            Next
            For Each col As DataGridViewColumn In .Columns
                col.Width = CInt(.Width / UUTNum) - 1
                col.HeaderText = "UUT" & col.Index + 1
            Next

            .RowCount = TestStep.TotalStepNum
        End With
    End Sub
    Private Sub AutoSetResultLabel()
        '********************************
        'Auto Add new LblUUT and LblState
        '********************************
        Me.LayoutUUTInfo.Controls.Clear()
        Me.LayoutUUTInfo.ColumnCount = UUTNum
        Me.LayoutUUTInfo.ColumnStyles.Clear()
        Dim fontsize As Single = 30 - (UUTNum - 1) * 4
        'Dim fontsize As Single = 9
        For t As Integer = 1 To UUTNum
            Dim NewLblUUT As Label = New Label
            NewLblUUT.Name = "LblUUT" & t
            NewLblUUT.Text = t
            NewLblUUT.Font = New System.Drawing.Font("Arial", fontsize - 5, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            NewLblUUT.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            'NewLblUUT.Dock = DockStyle.Fill

            NewLblUUT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
     Or System.Windows.Forms.AnchorStyles.Left) _
     Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

            NewLblUUT.TextAlign = ContentAlignment.MiddleCenter
            NewLblUUT.Margin = New System.Windows.Forms.Padding(1)
            NewLblUUT.BorderStyle = BorderStyle.Fixed3D

            Dim NewLblState As Label = New Label
            NewLblState.Name = "LblState" & t
            NewLblState.Text = "Ready"
            NewLblState.BackColor = Color.Cyan
            NewLblState.BorderStyle = BorderStyle.Fixed3D
            'NewLblState.Dock = DockStyle.Fill
            NewLblState.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
Or System.Windows.Forms.AnchorStyles.Left) _
Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

            NewLblState.Font = New System.Drawing.Font("Arial", fontsize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            NewLblState.TextAlign = ContentAlignment.MiddleCenter
            NewLblState.Margin = New System.Windows.Forms.Padding(1)

            Me.LayoutUUTInfo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / UUTNum))
            Me.LayoutUUTInfo.Controls.Add(NewLblState, t - 1, 0)
            Me.LayoutUUTInfo.Controls.Add(NewLblUUT, t - 1, 1)
        Next
    End Sub
    Private Sub AutoSetConciseUI()
        Call CtrlConcise.Initial(UUTNum, IniUUT, EndUUT)
    End Sub

    Private Sub DGVTestResult_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVTestResult.Resize
        For Each col As DataGridViewColumn In DGVTestResult.Columns
            col.Width = DGVTestResult.Width / UUTNum - 1
        Next
    End Sub
    Private Sub DGVTestResult_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DGVTestResult.Scroll
        On Error Resume Next
        DGVTestItem.FirstDisplayedScrollingRowIndex = DGVTestResult.FirstDisplayedScrollingRowIndex
    End Sub

#End Region

#Region "ToolStrip Select Test steps"
    Private Sub ToolButSelect_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButTest.ButtonClick
        '*******************************
        'Select Test Steps
        '*******************************
        If TestMode = TestModes.Debug Then

            For Each row As DataGridViewRow In DGVTestItem.SelectedRows
                row.Cells(DGVTestItem.Columns("ColTest").Index).Value = Not Convert.ToBoolean(row.Cells(DGVTestItem.Columns("ColTest").Index).Value)
            Next

            If DGVTestItem.IsCurrentCellDirty Then 'Submit change state
                DGVTestItem.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            End If
        End If
    End Sub
    Private Sub ToolButSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButTestAll.Click
        '*******************************
        'Select All Test Steps
        '*******************************
        If TestMode = TestModes.Debug Then
            For Each row As DataGridViewRow In DGVTestItem.Rows
                row.Cells(DGVTestItem.Columns("ColTest").Index).Value = True
            Next
            If DGVTestItem.IsCurrentCellDirty Then 'Submit change state
                DGVTestItem.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            End If
        End If
    End Sub
    Private Sub ToolButCancelAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolButSkipAll.Click
        '*******************************
        'Cancel All Select Test Steps
        '*******************************
        If TestMode = TestModes.Debug Then
            For Each row As DataGridViewRow In DGVTestItem.Rows
                row.Cells(DGVTestItem.Columns("ColTest").Index).Value = False
            Next
            If DGVTestItem.IsCurrentCellDirty Then 'Submit change state
                DGVTestItem.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            End If
        End If
    End Sub
#End Region

#Region "ToolStrip Set Break point"
    Private Sub ToolButSetBreak_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButSetBreak.ButtonClick
        '*******************************
        'Set Break Point of selected
        '*******************************
        If TestMode = TestModes.Debug Then
            Dim BreakCell As DataGridViewBreakPointCell

            For Each row As DataGridViewRow In DGVTestItem.SelectedRows
                BreakCell = CType(row.Cells(DGVTestItem.Columns("ColBreak").Index), DataGridViewBreakPointCell)
                If BreakCell.IsBreakPoint Then
                    CType(row.Cells(DGVTestItem.Columns("ColBreak").Index), DataGridViewBreakPointCell).SetBreakPoint(False)
                Else
                    CType(row.Cells(DGVTestItem.Columns("ColBreak").Index), DataGridViewBreakPointCell).SetBreakPoint(True)
                End If
            Next

            If DGVTestItem.IsCurrentCellDirty Then 'Submit change state
                DGVTestItem.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            End If
        End If
    End Sub
    Private Sub ToolButSetAllBreak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButSetAllBreak.Click
        '*******************************
        'Set Break Point of selected
        '*******************************
        If TestMode = TestModes.Debug Then
            For Each row As DataGridViewRow In DGVTestItem.Rows
                CType(row.Cells(DGVTestItem.Columns("ColBreak").Index), DataGridViewBreakPointCell).SetBreakPoint(True)
            Next

            If DGVTestItem.IsCurrentCellDirty Then 'Submit change state
                DGVTestItem.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            End If
        End If
    End Sub
    Private Sub ToolButClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButClearAll.Click
        '***********************************
        'Clear All Break Point of selected
        '***********************************
        If TestMode = TestModes.Debug Then
            For Each row As DataGridViewRow In DGVTestItem.Rows
                CType(row.Cells(DGVTestItem.Columns("ColBreak").Index), DataGridViewBreakPointCell).SetBreakPoint(False)
            Next

            If DGVTestItem.IsCurrentCellDirty Then 'Submit change state
                DGVTestItem.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            End If
        End If
    End Sub
#End Region

#Region "ToolStrip Change Current Step"
    Private Sub ToolButHead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButHead.Click, ToolButEnd.Click, ToolButNext.Click, ToolButPrevious.Click, ToolButHere.Click
        Dim PreBut As ToolStripButton = CType(sender, ToolStripButton)

        Select Case PreBut.Name
            Case ToolButHead.Name
                TestStep.PNextStep = 0

            Case ToolButPrevious.Name
                TestStep.DecStep()

            Case ToolButNext.Name
                If TestStep.PNextStep < TestStep.TotalStepNum - 1 Then
                    TestStep.IncStep()
                Else
                    TestStep.PNextStep = TestStep.TotalStepNum - 1
                End If

            Case ToolButEnd.Name
                TestStep.PNextStep = TestStep.TotalStepNum - 1

            Case ToolButHere.Name
                TestStep.PNextStep = DGVTestItem.CurrentRow.Index

        End Select

        For t As Integer = 0 To UUTNum - 1
            Esito(t) = True
        Next
        Call DispCurrentSelect()
    End Sub

    Private Sub DispCurrentSelect()
        Dim SelectRowIndex As Integer = TestStep.PNextStep
        Call CancelSelectAllRow()
        On Error Resume Next
        DGVTestItem.Rows(SelectRowIndex).Selected = True
        DGVTestItem.FirstDisplayedScrollingRowIndex = SelectRowIndex
    End Sub
    Public Sub DispCurrentStep()
        DGVTestItem.Rows(TestStep.PNextStep).Selected = True
    End Sub
    Public Sub DispCurrentStep(ByVal RowIndex As Integer)
        On Error Resume Next
        Call CancelSelectAllRow()
        DGVTestItem.Rows(RowIndex).Selected = True
        DGVTestResult.Rows(RowIndex).Selected = True
    End Sub

#End Region

#Region "ToolStrip Run State"
    Private Sub ToolButRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButRun.Click
        RunState = RunStates.Run
    End Sub
    Private Sub ToolButPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButPause.Click
        RunState = RunStates.Pause
    End Sub
    Private Sub ToolButStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButStop.Click
        RunState = RunStates.Stoped
        TestStep.PNextStep = TestStep.TotalStepNum
    End Sub
    Private Sub ToolButCurrent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButReExecute.Click
        RunState = RunStates.ExecCurrent
    End Sub
#End Region

#Region "ToolStrip Program"
    Private Sub ToolButLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButLoad.Click
        Call MenuLoadProgram_Click(sender, e)
    End Sub
    Private Sub ToolButUnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButUnload.Click
        Call MenuUnloadProgram_Click(sender, e)
    End Sub
    Private Sub ToolButConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButConfig.Click
        Call ShowTrigger()
    End Sub
    Private Sub ToolButProdMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButProdMode.Click
        Call MenuProduct_Click(sender, e)
    End Sub
    Private Sub ToolButDebugMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolButDebugMode.Click
        Call MenuDebug_Click(sender, e)
    End Sub
    Private Sub ShowTrigger()
        MachineState = MachineStates.Diagnostic
        MyRack.ShowComTrigger()
        MachineState = MachineStates.Ready
    End Sub
#End Region

#Region "Test time"
    Private Sub TimeTimeUsed_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimeTimeUsed.Tick
        Dim Pretime As Long = 0
        Try
            Pretime = System.Environment.TickCount - TimeSleep
        Catch ex As OverflowException
            TimeSleep = 0
        End Try

        DispTime = Pretime / 1000
        LabTimeUsed.Text = DispTime.ToString("0.00")
        Application.DoEvents()
    End Sub
#End Region

#Region "Splitter"
    Private Sub SplitDisplay_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitDisplay.SplitterMoved
        SplitTitle.SplitterDistance = SplitDisplay.SplitterDistance
    End Sub
#End Region

#End Region

#Region "Code maybe need modified"
    ''' <summary>
    ''' 每次测试开始时，重置各UUT状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitialForm()
        '******************************* ******
        'Initial each control when start test
        '*************************************
        MachineState = MachineStates.Run
        Call DispMachineState(MachineStates.Ready)

        If TestMode = TestModes.Debug Then
            RunState = RunStates.Run
            Call SetDebugToolState()
        Else
            Call CollapseAllRow()
        End If
        Call CancelSelectAllRow()
        Call ClearTestResult()

        tabLeftTop.SelectTab("TabProduct")
        tabLeftBottom.SelectTab("tabPageTimer")

        MyTAT.ScanFiches.HideScanner()
        MyTAT.HideScanMatrixForm()
        Call ResetDispImage(10, 9)


        Me.ProBarTest.Value = 0
        Me.LabTimeUsed.Text = ""

        Application.DoEvents()
        TimeSleep = System.Environment.TickCount
        TimeTimeUsed.Enabled = True
    End Sub

    ''' <summary>
    ''' 每次测试结束后，设置各状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FinishTest()
        'Cancel select to display the fail row
        For t As Integer = 0 To UUTNum - 1
            If Esito(t) = False Then
                For Each row As DataGridViewRow In DGVTestResult.SelectedRows
                    row.Selected = False
                Next
                Exit For
            End If
        Next

        Call SetLastResult()
        Call SetLastResultConcise()

        Call DispPassfail()
        tabLeftTop.SelectTab("TabPassFail")
        Application.DoEvents()
        TimeTimeUsed.Enabled = False
    End Sub

#Region "图片显示的支持"
    Public Sub ResetDispImage(ByVal XMax As Integer, ByVal YMax As Integer)
        If MyTAT.Sysinfo.DispMode = DisplayModes.Table Then Exit Sub

        DGVU1.Rows.Clear()
        DGVU2.Rows.Clear()

        'YMaxScale = YMax
        'XMaxScale = XMax

        'DrawFigure_Scale(Pic1, XMax, YMax)
        'DrawFigure_Scale(Pic2, XMax, YMax)

    End Sub
    Private Sub DrawFigure_Scale(ByVal Pic As PictureBox, ByVal XMax As Integer, ByVal YMax As Integer)
        Dim bmp As Bitmap = New Bitmap(Pic.Width, Pic.Height)
        Pic.BackgroundImage = bmp
        Pic.BackgroundImageLayout = ImageLayout.Stretch

        Dim G As Graphics
        G = Graphics.FromImage(bmp)
        G.Clear(Color.Ivory)

        Dim rect As Rectangle
        rect = New Rectangle(4, 4, Pic.Width - 8, Pic.Height - 8)

        Dim P0 As New PointF(rect.Left + 5, rect.Bottom - 5)
        Dim PX As New PointF(rect.Right - 5, rect.Bottom - 5)
        Dim PY As New PointF(rect.Left + 5, rect.Top + 5)

        Dim myCapPen As Pen = New Pen(Color.Black, 1)
        With myCapPen
            .SetLineCap(Drawing2D.LineCap.Square, Drawing2D.LineCap.Triangle, Drawing2D.DashCap.Triangle)
        End With
        G.DrawLine(myCapPen, P0, PX)
        G.DrawLine(myCapPen, P0, PY)

        Dim Width As Long = PX.X - P0.X
        Dim Height As Long = P0.Y - PY.Y

        Dim HorzFont As Font = New Font("Verdana", 5, FontStyle.Bold)
        Dim VertFont As Font = New Font("Verdana", 5, FontStyle.Bold)
        Dim VertStrFormat As StringFormat = New StringFormat
        VertStrFormat.FormatFlags = StringFormatFlags.DirectionVertical

        'Draw X scale
        Dim Sep As Single
        For t As Integer = 1 To XMax
            Sep = t * (Width / XMax)
            G.DrawString(t, HorzFont, Brushes.Black, P0.X + Sep - 20, P0.Y)
            G.DrawString("-", HorzFont, Brushes.Black, P0.X + Sep - 20, P0.Y - 3, VertStrFormat)
        Next

        'Draw Y scale
        For t As Integer = 5 To YMax
            Sep = (YMax - 4 - (t - 4)) * (Height / (YMax - 4))
            G.DrawString(t, VertFont, Brushes.Black, P0.X - 7, PY.Y + Sep)
            G.DrawLine(Pens.Gray, P0.X, PY.Y + Sep + 3, PX.X, PY.Y + Sep + 3)
        Next

        Dim HW As Integer = Width / 2
        G.DrawString("---- TR12", VertFont, Brushes.Blue, P0.X + HW, P0.Y - 10)
        G.DrawString("---- TR11", VertFont, Brushes.Red, P0.X + HW, P0.Y - 20)

        G.Dispose()
    End Sub
    Private Function Getgraphic(ByVal PBox As PictureBox) As Graphics
        Dim bmp As Bitmap = New Bitmap(PBox.Width, PBox.Height)
        PBox.Image = bmp
        PBox.SizeMode = ImageLayout.Stretch

        Dim G As Graphics
        G = Graphics.FromImage(bmp)
        Return G
    End Function
    Public Sub DGVMesAddNewRow(ByVal UUTNo As Integer, ByVal ID As Integer, ByVal Value1 As String, ByVal Value2 As String)
        Dim DGV As DataGridView
        If UUTNo = 0 Then
            DGV = DGVU1
        Else
            DGV = DGVU2
        End If
        DGV.Rows.Add()

        With DGV.Rows(DGV.Rows.Count - 1)
            .Cells(0).Value = ID
            .Cells(1).Value = Value1
            .Cells(2).Value = Value2
        End With

        Call DrawNewPoint(UUTNo, ID, Value1, Value2)
    End Sub
    Private Sub DrawNewPoint(ByVal UUTNo As Integer, ByVal ID As Integer, ByVal Value1 As Single, ByVal Value2 As Single)
        Static LastPoint(3) As PointF

        Dim Pic As PictureBox
        If UUTNo = 0 Then
            Pic = Pic1
        Else
            Pic = Pic2
        End If

        Dim bmp As Bitmap = Pic.BackgroundImage
        Pic.Image = bmp
        Pic.SizeMode = ImageLayout.Stretch
        Dim G As Graphics
        G = Graphics.FromImage(bmp)


        Dim rect As Rectangle
        rect = New Rectangle(4, 4, Pic.Width - 8, Pic.Height - 8)

        Dim P0 As New PointF(rect.Left + 5, rect.Bottom - 5)
        Dim PX As New PointF(rect.Right - 5, rect.Bottom - 5)
        Dim PY As New PointF(rect.Left + 5, rect.Top + 5)

        Dim Width As Long = PX.X - P0.X
        Dim Height As Long = P0.Y - PY.Y

        If ID <= 1 Then
            For t As Integer = 0 To 3
                LastPoint(t) = P0
            Next
        End If

        Dim tmpYDiv As Integer = YMaxScale - 4
        Dim XDiv As Single = Width / 100
        Dim YDiv1 As Single = (tmpYDiv - (Value1 - 4)) * (Height / tmpYDiv)
        Dim YDiv2 As Single = (tmpYDiv - (Value2 - 4)) * (Height / tmpYDiv)

        Dim PreP1 As PointF = New PointF(P0.X + XDiv * ID, PY.Y + YDiv1 + 4)
        Dim PreP2 As PointF = New PointF(P0.X + XDiv * ID, PY.Y + YDiv2 + 4)

        If UUTNo = 0 Then
            G.DrawLine(Pens.Red, LastPoint(0), PreP1)
            G.DrawLine(Pens.Blue, LastPoint(1), PreP2)
            LastPoint(0) = PreP1
            LastPoint(1) = PreP2
        Else
            G.DrawLine(Pens.Red, LastPoint(2), PreP1)
            G.DrawLine(Pens.Blue, LastPoint(3), PreP2)

            LastPoint(2) = PreP1
            LastPoint(3) = PreP2
        End If

        G.Dispose()
    End Sub
#End Region


#Region "图形显示模式的支持"
    Public Enum DispSteps
        ScanBarcode
        PutPCB

        TestPass
        TestFailed
        TestAbort
    End Enum


    ''' <summary>
    ''' 设置显示于图片上方的文字提示信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispMsg(ByVal msg As String, Optional ByVal Append As Boolean = False)
        If Append Then
            LblTip.Text = LblTip.Text & msg
        Else
            LblTip.Text = msg
        End If
    End Sub
    Public Sub SetDispMsg(ByVal msg As String, ByVal BKColor As Color, Optional ByVal Append As Boolean = False)
        Call SetDispMsg(msg, Append)
        LblTip.BackColor = BKColor
    End Sub
    Public Sub SetDispMsg(ByVal msg As String, ByVal BKColor As Color, ByVal ForeColor As Color, Optional ByVal Append As Boolean = False)
        Call SetDispMsg(msg, Append)
        LblTip.BackColor = BKColor
        LblTip.ForeColor = ForeColor
    End Sub


    ''' <summary>
    ''' 设置图片内容
    ''' </summary>
    ''' <param name="img"></param>
    ''' <remarks></remarks>
    ''' 
    Public Sub SetDispImage(ByVal img As Image)
        Pic2.Image = img
    End Sub
    Public Sub SetDispImage(ByVal ImageName As String)
        Pic2.Image = GetImgFromResource(ImageName)
    End Sub
    Private Function GetImgFromResource(ByVal ImageName As String) As Image
        Dim Value As Image = Nothing
        If ImageName <> "" Then
            Try
                Value = CType(My.Resources.ResourceManager.GetObject(ImageName), Image)
            Catch ex As Exception
            End Try
        End If
        Return Value
    End Function

    Public Sub SetImageInitialState()
        If MyTAT.Sysinfo.DispMode = DisplayModes.Table Then Exit Sub

        Call SetDispStep(DispSteps.ScanBarcode)
    End Sub
    Private Sub SetLastResult()
        If MyTAT.Sysinfo.DispMode = DisplayModes.Table Then Exit Sub
        If AbortTest Then Exit Sub

        For t As Integer = 0 To UUTNum - 1
            If Esito(t) Then
                SetDispStep(DispSteps.TestPass)
            Else
                SetDispStep(DispSteps.TestFailed)
            End If
        Next
    End Sub
    Public Sub SetDispStep(ByVal DispStep As DispSteps)
        If MyTAT.Sysinfo.DispMode = DisplayModes.Table Then Exit Sub

        Select Case DispStep
            Case DispSteps.TestPass
                SetDispMsg(My.Resources.FVTResource.MSG_Pass, Color.LimeGreen)
            Case DispSteps.TestFailed
                SetDispMsg(vbNewLine & My.Resources.FVTResource.MSG_Failed, Color.Red, Append:=True)
            Case DispSteps.TestAbort
                SetDispMsg(My.Resources.FVTResource.MSG_Abort, Color.Pink)

            Case DispSteps.ScanBarcode
                DGVU2.Rows.Clear()
                SetDispMsg(My.Resources.FVTResource.MSG_ScanBarcode)
                SetDispImage("IMG_HeatSink")

            Case DispSteps.PutPCB
                SetDispMsg(My.Resources.FVTResource.MSG_PutPCB)
                SetDispImage("IMG_PutPCB")



        End Select
    End Sub

    Public Sub SetConciseInitialState()
        'If MyTAT.Sysinfo.DispMode = DisplayModes.Table Then Exit Sub

        Call SetDispStepConcise(DispSteps.ScanBarcode)
    End Sub
    Public Sub SetDispStepConcise(ByVal DispStep As DispSteps)
        'If MyTAT.Sysinfo.DispMode = DisplayModes.Table Then Exit Sub

        Select Case DispStep
            Case DispSteps.TestPass
                SetDispMsgConcise(My.Resources.FVTResource.MSG_Pass, Color.LimeGreen)
            Case DispSteps.TestFailed
                SetDispMsgConcise(vbNewLine & My.Resources.FVTResource.MSG_Failed, Color.Red, Append:=True)
            Case DispSteps.TestAbort
                SetDispMsgConcise(My.Resources.FVTResource.MSG_Abort, Color.Pink)
            Case DispSteps.ScanBarcode
                SetDispMsgConcise(My.Resources.FVTResource.MSG_ScanBarcode)
            Case DispSteps.PutPCB
                SetDispMsgConcise(My.Resources.FVTResource.MSG_PutPCB)




        End Select
    End Sub
    Private Sub SetLastResultConcise()
        If AbortTest Then Exit Sub
        For t As Integer = 0 To UUTNum - 1
            If Esito(t) Then
                SetDispStepConcise(DispSteps.TestPass)
            Else
                SetDispStepConcise(DispSteps.TestFailed)
            End If
        Next
    End Sub
    Public Sub SetDispMsgConcise(ByVal msg As String, Optional ByVal Append As Boolean = False)
        If Append Then
            CtrlConcise.SetTipText(LblTip.Text & msg)
        Else
            CtrlConcise.SetTipText(LblTip.Text & msg)
        End If
    End Sub
    Public Sub SetDispMsgConcise(ByVal msg As String, ByVal BKColor As Color, Optional ByVal Append As Boolean = False)
        Call SetDispMsgConcise(msg, Append)
        Call CtrlConcise.SetTipBKColor(BKColor)
    End Sub
    Public Sub SetDispMsgConcise(ByVal msg As String, ByVal BKColor As Color, ByVal ForeColor As Color, Optional ByVal Append As Boolean = False)
        Call SetDispMsgConcise(msg, Append)
        Call CtrlConcise.SetTipBKColor(BKColor)
        Call CtrlConcise.SetTipFCColor(ForeColor)
    End Sub

#End Region

#End Region

#Region "Language"
    Private Sub EnglishToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuEnglish.Click
        Call SwitchLanguage(Languages.English)
    End Sub
    Private Sub MenuChinese_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuChinese.Click
        Call SwitchLanguage(Languages.Chinese)
    End Sub
    Private Sub MenuItalian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItalian.Click
        Call SwitchLanguage(Languages.Italian)
    End Sub
    Private Sub MenuPolish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuPolish.Click
        Call SwitchLanguage(Languages.Polish)
    End Sub
    Private Sub TurkishToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuTurkish.Click
        Call SwitchLanguage(Languages.Turkish)
    End Sub

    Private Sub SwitchLanguage(ByVal Lan As Languages)
        MyTAT.SysSettings.UpdateLanguage(Lan)
        MyTAT.HideScanMatrixForm()
        MyResManager.SelectLanguage(Lan)
        MsgBox(MyResManager.GetValue("Info_ChangeLanguage"), MsgBoxStyle.Information)
    End Sub

    Private Sub MenuTranslator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuTranslator.Click
        MyResManager.ShowEditor()
    End Sub

#End Region


End Class