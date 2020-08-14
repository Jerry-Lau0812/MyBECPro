Imports System.Text
Imports System.IO
Imports Bitron3DEQ.FVTRackBoardManager.RackSystem
Imports Bitron3DEQ.FVTTestAssistant
Imports Testbench

Public Class ClassTest
#Region "Don't need modified code here"

#Region "Common Variant Declare"
    Private StrMachineStates As String = ""
    Private MesResult As MeasureResults
    Private LimitValue As LimitValues
    Public FailStepInfo(UUTNum - 1) As FailStepInformation

    Private Retry As Integer
    Private TestOK As Boolean
    Private Risposta As String = ""

    Private ContaTest As Integer
    Public WithEvents TimerMsg As New Timer
    Private DatoValido As New ArrayList
    Private Barcodes(UUTNum - 1) As String
    Private StepTimeStopWatch As Stopwatch = New Stopwatch()
    Public SpaceKeyPressed As Boolean

    Public Enum States
        SetOFF = 0
        SetON = 1
    End Enum
    Public Sub New()
        TimerMsg.Interval = 250
        TimerMsg.Enabled = True
    End Sub
#End Region

#Region "program running contorl"
    Public Sub EseguiTest()
        Call InitialTest()

        Dim TmpEsito As Boolean = True
        Dim IniUUT_Begain As Integer = IniUUT
        Dim EndUUT_Begain As Integer = EndUUT
        If TestMode = TestModes.Debug Then
            TestStep.PNextStep = 0
            If MyTAT.Sysinfo.System.TestSequence = TestSequences.Parallel Then
                Call DispMachineState()
                While (ContaTest <= TestStep.MasterNums)
                    Call TestFVT(ContaTest)

                    If TestStep.PNextStep < TestStep.TotalStepNum Then
                        ContaTest = TestStep.NextStep.Master
                    Else
                        If RunState <> RunStates.Stoped Then
                            ContaTest = WaitingForNext()
                            If ContaTest = 1 Then
                                For s As Integer = IniUUT To EndUUT
                                    Esito(s) = True
                                Next
                                Frmpannello.InitialForm()
                                Call ReleaseResource()
                            End If
                        Else
                            ContaTest = TestStep.MasterNums + 1
                        End If
                    End If
                    If AbortTest Then
                        ContaTest = TestStep.MasterNums + 1
                        RunState = RunStates.Stoped
                    End If
                End While
            Else
                For t As Integer = IniUUT_Begain To EndUUT_Begain
                    IniUUT = t
                    EndUUT = t
                    ContaTest = 1
                    TestStep.PNextStep = 0
                    Call DispMachineState()
                    While (ContaTest <= TestStep.MasterNums)
                        Call TestFVT(ContaTest)

                        If TestStep.PNextStep < TestStep.TotalStepNum Then
                            ContaTest = TestStep.NextStep.Master
                        Else
                            If t < UUTNum - 1 Then 'if not the last UUT then continue test next UUT
                                Exit While
                            Else
                                If RunState <> RunStates.Stoped Then
                                    ContaTest = WaitingForNext()
                                    If ContaTest = 1 Then 'Get ready to test from begainning
                                        For s As Integer = 0 To UUTNum - 1
                                            Esito(s) = True
                                        Next
                                        IniUUT = IniUUT_Begain
                                        EndUUT = EndUUT_Begain
                                        Frmpannello.InitialForm()
                                        t = -1
                                        Call ReleaseResource()
                                        Exit While
                                    End If
                                Else
                                    ContaTest = TestStep.MasterNums + 1
                                End If
                            End If
                        End If
                        If AbortTest Then
                            ContaTest = TestStep.MasterNums + 1
                            RunState = RunStates.Stoped
                            Exit For
                        End If
                    End While
                Next
                IniUUT = IniUUT_Begain
                EndUUT = EndUUT_Begain
            End If
        Else
            If MyTAT.Sysinfo.System.TestSequence = TestSequences.Parallel Then
                Call DispMachineState()
                While (TmpEsito And ContaTest <= TestStep.MasterNums)
                    Call TestFVT(ContaTest)

                    ContaTest += 1
                    TmpEsito = GetEsitoState()
                End While
            Else
                For t As Integer = IniUUT_Begain To EndUUT_Begain
                    IniUUT = t
                    EndUUT = t
                    ContaTest = 1
                    TmpEsito = True
                    Call DispMachineState()
                    While (TmpEsito And ContaTest <= TestStep.MasterNums)
                        Call TestFVT(ContaTest)

                        ContaTest += 1
                        TmpEsito = GetEsitoState()
                    End While
                Next
                IniUUT = IniUUT_Begain
                EndUUT = EndUUT_Begain
            End If
        End If

        Call GetCurrentTestStepInfo(True)
        Call ReleaseResource()
        Call EndTest()
    End Sub
    Private Function GetEsitoState() As Boolean
        Dim TmpEsito As Boolean = False
        For t As Integer = IniUUT To EndUUT
            If Esito(t) = True Then
                TmpEsito = True
                Exit For
            End If
        Next
        If AbortTest Then TmpEsito = False

        Return TmpEsito
    End Function

    Private Sub GetLimitValue(ByVal RowIndex As Integer)
        '***********************************************
        'Get Limit Value from Datagridview DGVTestItems
        '***********************************************
        LimitValue.MinValue = Frmpannello.DGVTestItem.Rows(RowIndex).Cells(Frmpannello.DGVTestItem.Columns("ColMin").Index).Value
        LimitValue.NormalValue = Frmpannello.DGVTestItem.Rows(RowIndex).Cells(Frmpannello.DGVTestItem.Columns("ColNormal").Index).Value
        LimitValue.MaxValue = Frmpannello.DGVTestItem.Rows(RowIndex).Cells(Frmpannello.DGVTestItem.Columns("ColMax").Index).Value
        LimitValue.Unit = Frmpannello.DGVTestItem.Rows(RowIndex).Cells(Frmpannello.DGVTestItem.Columns("ColUnit").Index).Value
        LimitValue.Description = Frmpannello.DGVTestItem.Rows(RowIndex).Cells(Frmpannello.DGVTestItem.Columns("ColDesc").Index).Value
        LimitValue.IsTest = Frmpannello.DGVTestItem.Rows(RowIndex).Cells(Frmpannello.DGVTestItem.Columns("ColTest").Index).Value

    End Sub
    Private Function GetStdValue(ByVal ValueType As StandardValues) As String
        '***********************************************
        'Get Limit Value from Datagridview DGVTestItems
        '***********************************************
        Dim RowIndex As Integer = TestStep.GetStepIndex(CurrentStepNo)
        Dim PreRow As DataGridViewRow = Frmpannello.DGVTestItem.Rows(RowIndex)

        Select Case ValueType
            Case StandardValues.Min
                Return PreRow.Cells(5).Value
            Case StandardValues.Normal
                Return PreRow.Cells(6).Value
            Case StandardValues.Max
                Return PreRow.Cells(7).Value
            Case StandardValues.Unit
                Return PreRow.Cells(8).Value
            Case Else
                Return ""
        End Select
    End Function
    Private Function GetMesValue(Optional ByVal UUT As Integer = 0, Optional ByVal Offset As Integer = -1) As String
        Dim RowIndex As Integer = TestStep.GetStepIndex(CurrentStepNo)
        If Offset <> 0 Then
            RowIndex += Offset
            If RowIndex > Frmpannello.DGVTestResult.RowCount Then RowIndex = Frmpannello.DGVTestResult.RowCount - 1
            If RowIndex < 0 Then RowIndex = 0
        End If

        Dim PreRow As DataGridViewRow = Frmpannello.DGVTestResult.Rows(RowIndex)
        Dim Value As String = PreRow.Cells(UUT).Value

        Return Value
    End Function
    Private Sub AppendDispValue(ByVal Value As String, Optional ByVal UUT As Integer = 0, Optional ByVal Replace As Boolean = False, Optional ByVal Offset As Integer = 0)
        Dim RowIndex As Integer = TestStep.GetStepIndex(CurrentStepNo)
        If Offset <> 0 Then
            RowIndex += Offset
            If RowIndex > Frmpannello.DGVTestResult.RowCount Then RowIndex = Frmpannello.DGVTestResult.RowCount - 1
            If RowIndex < 0 Then RowIndex = 0
        End If

        Dim PreRow As DataGridViewRow = Frmpannello.DGVTestResult.Rows(RowIndex)
        If Replace Then
            PreRow.Cells(UUT).Value = Value
        Else
            PreRow.Cells(UUT).Value &= " " & Value
        End If
        Application.DoEvents()
    End Sub

    Private Sub CheckResult(Optional ByVal CheckMode As CheckModes = CheckModes.CheckEqual)
        '*********************************************
        'Verify the measure result with standard value
        '2 Mode :Equal or Range
        '*********************************************
        MesResult.Result = False

        If CheckMode = CheckModes.CheckEqual Then
            If MesResult.MesValue.ToUpper = "TRUE" Then MesResult.MesValue = "OK"
            If MesResult.MesValue = LimitValue.NormalValue Then MesResult.Result = True
        Else
            If MesResult.MesValue = "" Then MesResult.MesValue = "0"
            Dim MesValue As Single = Convert.ToSingle(MesResult.MesValue)
            Dim MinValue As Single = Convert.ToSingle(LimitValue.MinValue)
            Dim MaxValue As Single = Convert.ToSingle(LimitValue.MaxValue)
            Select Case CheckMode
                Case CheckModes.CheckRange
                    If MesValue >= MinValue And MesValue <= MaxValue Then MesResult.Result = True
                Case CheckModes.CheckRangeNoLimit
                    If MesValue > MinValue And MesValue < MaxValue Then MesResult.Result = True
                Case CheckModes.CheckOutRange
                    If MesValue <= MinValue Or MesValue >= MaxValue Then MesResult.Result = True
                Case CheckModes.CheckOutRangeNoLimit
                    If MesValue < MinValue Or MesValue > MaxValue Then MesResult.Result = True
            End Select
        End If
    End Sub
    Private Sub DispResult(Optional ByVal UUTNo As Integer = 0)
        '****************************************************
        'Display the measure result in DatagridView DGVResult
        'If pass the relative cell's backcolor will be set to
        'Green else Red
        '****************************************************

        MesResult.UUTNo = UUTNo
        Dim Cell As DataGridViewCell
        Cell = Frmpannello.DGVTestResult.Rows(MesResult.TestNo).Cells(UUTNo)
        If TestMode = TestModes.Debug Then
            If RunState = RunStates.ExecCurrent Then
                Cell.Value = ""
            End If
        End If
        If MesResult.AppendInfos <> "" Then
            Cell.Value = MesResult.MesValue & "," & MesResult.AppendInfos  'Display the measure value
        Else
            Cell.Value = MesResult.MesValue 'Display the measure value
        End If

        If MesResult.Result = True Then 'Set Cell's Backcolor
            Cell.Style.BackColor = Color.LightGreen
            Frmpannello.CtrlConcise.SetStauts(MachineStates.Run, UUTNo)
        Else
            Cell.Style.BackColor = Color.Red
            Frmpannello.CtrlConcise.SetStauts(MesResult.StepCode, Color.Red, UUTNo)
        End If

        Call GetCurrentTestStepResult(UUTNo, MesResult.TestNo, MesResult.Result)

        Application.DoEvents()
    End Sub
    Private Sub SetStdValue(Optional ByVal MinValue As String = "-", Optional ByVal NormalValue As String = "-", Optional ByVal MaxValue As String = "-")
        Dim RowIndex As Integer = TestStep.GetStepIndex(CurrentStepNo)
        Dim PreRow As DataGridViewRow = Frmpannello.DGVTestItem.Rows(RowIndex)
        If MinValue <> "-" Then PreRow.Cells(5).Value = MinValue
        If NormalValue <> "-" Then PreRow.Cells(6).Value = NormalValue
        If MaxValue <> "-" Then PreRow.Cells(7).Value = MaxValue

        Call GetLimitValue(RowIndex)
    End Sub

    Private Sub CheckAndDispResult(Optional ByVal CheckMode As CheckModes = CheckModes.CheckEqual, Optional ByVal UUTNo As Integer = 0)
        Call CheckResult(CheckMode)
        Call DispResult(UUTNo)
        If MesResult.Result = False Then Call GetFailInfo(UUTNo)
    End Sub
    Private Sub CheckAndDispResults(Optional ByVal CheckMode As CheckModes = CheckModes.CheckEqual)
        For t As Integer = IniUUT To EndUUT
            If Esito(t) Then
                Call CheckResult(CheckMode)
                Call DispResult(t)
                If MesResult.Result = False Then Call GetFailInfo(t)
            End If
        Next
    End Sub
    Private Sub GetFailInfo(Optional ByVal UUTNo As Integer = 0)
        '***********************************
        'Get Fail information of current UUT
        'when error occurs
        '***********************************
        Esito(UUTNo) = False
        With FailStepInfo(UUTNo)
            If .Matrix = "" Then .Matrix = "-"
            .StepNo = MesResult.TestNo
            .StepCode = MesResult.StepCode
            .MeasValue = MesResult.MesValue
            .DateTime = Date.Now
            .MinValue = LimitValue.MinValue
            .MaxValue = LimitValue.MaxValue
            .NormalValue = LimitValue.NormalValue
            .Unit = LimitValue.Unit

        End With
    End Sub

    Public Sub MemEsito()
        '******************************************
        'Save all testResult to local document
        '******************************************
        If TestMode = TestModes.Debug Or AbortTest Then Exit Sub
        Try
            'Check if file is exist
            Dim DesFilePath As String = MyTAT.Sysinfo.System.ReportPath & DatiProd.Value(ProdInfos.CodeBitron) & "\Dati\Rpt" & Today.DayOfYear & "_" & Today.Year & ".dat"
            If File.Exists(DesFilePath) = False Then
                Dim FileHeadString As String = " ProdDate ;ProdTime;BitronCode;UUT;Result;Mode;FailNo;TestTime;"
                For Each row As DataGridViewRow In Frmpannello.DGVTestItem.Rows
                    FileHeadString &= row.Cells("ColDesc").Value & ";"
                Next
                My.Computer.FileSystem.WriteAllText(DesFilePath, FileHeadString & vbNewLine, True)
            End If

            '*************************************************************************************************************************
            '测试结果保存到xml
            If MyTAT.Sysinfo.System.SaveBenchReport = "1" Then
                Dim resultPath As String = MyTAT.SysSettings.Sysparas.BenchReportPath
                Dim failStepNo(UUTNum - 1) As String
                For i As Integer = IniUUT To EndUUT
                    failStepNo(i) = FailStepInfo(i).StepNo
                Next
                MyBench.SaveResults(UUTNum, Barcodes, Frmpannello.DGVTestItem, Frmpannello.DGVTestResult, resultPath, Esito, failStepNo)
            End If
            '*************************************************************************************************************************

            'Store all meaure value
            Dim StrReport As New StringBuilder
            For UUT As Integer = IniUUT To EndUUT
                If StrReport.Length > 0 Then StrReport.Remove(0, StrReport.Length)

                StrReport.Append(Date.Now.ToString("yyyy-MM-dd") & ";")                      'ProductDate
                StrReport.Append(Date.Now.ToString("HH:mm:ss") & ";")                        'ProductTime
                StrReport.Append(" " & DatiProd.Value(ProdInfos.CodeBitron) & " ;")          'BitronCode
                StrReport.Append(" " & UUT.ToString & " ;")                                  'UUT No
                StrReport.Append("  " & IIf(Esito(UUT), "OK", "KO") & "  ;")                 'Test Result
                StrReport.Append("  " & MyTAT.Sysinfo.ProdMode & " ;")                       'Test Mode
                StrReport.Append("   " & FailStepInfo(UUT).StepCode & "  ;")                 'Test Fail Step No
                StrReport.Append(DispTime & " ;")                                            'Test time
                For t As Integer = 0 To Frmpannello.DGVTestResult.RowCount - 1               'All Meausre value
                    StrReport.Append(Frmpannello.DGVTestResult.Rows(t).Cells(UUT).Value & ";")
                Next
                StrReport.Append(vbNewLine)
                My.Computer.FileSystem.WriteAllText(DesFilePath, StrReport.ToString, True)
            Next
            StrReport = Nothing

        Catch ex As Exception
            MsgBox("Save Test result Fai!" & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    'Public Sub MemEsitoCSV()
    '    '******************************************
    '    'Save all testResult to local document
    '    '******************************************
    '    If TestMode = TestModes.Debug Or AbortTest Then Exit Sub
    '    Try

    '        'Store all meaure value

    '        Dim StrReport As New StringBuilder
    '        Dim StartIndex As Integer = TestStep.TotalStepNum

    '        For UUT As Integer = IniUUT To EndUUT

    '            If StrReport.Length > 0 Then StrReport.Remove(0, StrReport.Length)
    '            Dim FileName As String = Barcodes(UUT) & "_" & DateTime.Now.ToString("yyyyMMdd") & "_" & DateTime.Now.ToString("HHmmss") & "_" & IIf(Esito(UUT) = True, "Good", "Fail") & ".csv"

    '            Dim DesFileDirectory As String = MyTAT.Sysinfo.System.ReportPath & DatiProd.Value(ProdInfos.CodeBitron) & "\DatiCSV\" & DateTime.Now.ToString("yyyyMMdd")
    '            Dim DesFilePath As String = Path.Combine(DesFileDirectory, FileName)
    '            If Directory.Exists(DesFileDirectory) = False Then
    '                Directory.CreateDirectory(DesFileDirectory)
    '            End If

    '            StrReport.Append("StationID" & ";" & MyTAT.SysSettings.Sysparas.Supervisual.StationID & vbNewLine)                     'StationID
    '            StrReport.Append("Operator" & ";" & Frmpannello.CombOperator.Text & vbNewLine)            'Operator
    '            StrReport.Append("UUT Serial Number" & ";" & Barcodes(UUT) & vbNewLine)       'Serial Number         
    '            StrReport.Append("Batch Serial Number" & ";" & MyTAT.SysSettings.Sysparas.Supervisual.BenchSN & vbNewLine)   'Batch Serial Number
    '            StrReport.Append("Test Socket Index" & ";" & (UUT + 1).ToString() & vbNewLine)             'Test Socket Index
    '            StrReport.Append("Version_SW" & ";" & DatiProd.Value(ProdOtherInfos.VerSW) & vbNewLine)   'SW Version
    '            StrReport.Append("Version_HW" & ";" & DatiProd.Value(ProdOtherInfos.VerHW) & vbNewLine)   'HW Version
    '            StrReport.Append(vbNewLine)

    '            StrReport.Append("Sequence;Step Name;Status;Date;Time;Value;Units;LimitLow;LimitHigh;Normal" & vbNewLine)  'Head
    '            For t As Integer = 0 To Frmpannello.DGVTestResult.RowCount - 1               'All Meausre value
    '                Dim StepNo As String = Frmpannello.DGVTestItem.Rows(t).Cells("ColNo").Value.ToString()
    '                Dim StepName As String = Frmpannello.DGVTestItem.Rows(t).Cells("ColDesc").Value.ToString()

    '                Dim Status As String = "Passed"
    '                If Esito(UUT) = False Then
    '                    Dim FailStepNo As Integer = FailStepInfo(UUT).StepNo
    '                    If t >= FailStepNo Then
    '                        Status = "Failed"
    '                    End If
    '                End If

    '                Dim StrDate As String = DateTime.Now.ToString("yyyy-MM-dd")
    '                Dim StrTime As String = DateTime.Now.ToString("HH:mm:ss")
    '                Dim Value As String = ""
    '                Try
    '                    Value = Frmpannello.DGVTestResult.Rows(t).Cells(UUT).Value.ToString()
    '                Catch ex As Exception
    '                End Try

    '                Dim Unit As String = Frmpannello.DGVTestItem.Rows(t).Cells("ColUnit").Value.ToString()
    '                Dim LimitLow As String = Frmpannello.DGVTestItem.Rows(t).Cells("ColMin").Value.ToString()
    '                Dim LimitHigh As String = Frmpannello.DGVTestItem.Rows(t).Cells("ColMax").Value.ToString()
    '                Dim LimitNormal As String = Frmpannello.DGVTestItem.Rows(t).Cells("ColNormal").Value.ToString()

    '                StrReport.Append(String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}", StepNo, StepName, Status, StrDate, StrTime, Value, Unit, LimitLow, LimitHigh, LimitNormal) & vbNewLine)
    '            Next


    '            StrReport.Append(vbNewLine)
    '            StrReport.Append("UUT Serial Number" & ";" & Barcodes(UUT) & vbNewLine) 'UUT Serial Number
    '            StrReport.Append("UUT Status" & ";" & IIf(Esito(UUT), "Pass", "Fail") & vbNewLine) 'UUT Result

    '            My.Computer.FileSystem.WriteAllText(DesFilePath, StrReport.ToString, False)

    '        Next
    '        StrReport = Nothing

    '    Catch ex As Exception
    '        MsgBox("Save .CSV Test result Fai!" & ex.Message, MsgBoxStyle.Critical)
    '    End Try
    'End Sub

    ''' <summary>
    ''' 保存CSV格式的数据2019.09.21
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MemEsitoCSV()
        '******************************************
        'Save all testResult to local document
        '******************************************
        If TestMode = TestModes.Debug Or AbortTest Then Exit Sub
        Try

            'Store all meaure value
            Dim StrReport As New StringBuilder
            For UUT As Integer = IniUUT To EndUUT

                If StrReport.Length > 0 Then StrReport.Remove(0, StrReport.Length)
                Dim FileName As String = Barcodes(UUT) & "_" & DateTime.Now.ToString("yyyyMMdd") & "_" & DateTime.Now.ToString("HHmmss") & "_" & IIf(Esito(UUT) = True, "Good", "Fail") & ".csv"

                Dim DesFileDirectory As String = MyTAT.Sysinfo.System.ReportPath & DatiProd.Value(ProdInfos.CodeBitron) & "\DatiCSV\" & DateTime.Now.ToString("yyyyMMdd")
                Dim DesFilePath As String = Path.Combine(DesFileDirectory, FileName)
                If Directory.Exists(DesFileDirectory) = False Then
                    Directory.CreateDirectory(DesFileDirectory)
                End If

                StrReport.Append("StationID" & ";" & MyTAT.SysSettings.Sysparas.Supervisual.StationID & vbNewLine)                     'StationID
                StrReport.Append("Operator" & ";" & Frmpannello.CombOperator.Text & vbNewLine)            'Operator
                StrReport.Append("UUT Serial Number" & ";" & Barcodes(UUT) & vbNewLine)       'Serial Number         
                StrReport.Append("Batch Serial Number" & ";" & MyTAT.SysSettings.Sysparas.Supervisual.BenchSN & vbNewLine)   'Batch Serial Number
                StrReport.Append("Test Socket Index" & ";" & (UUT + 1).ToString() & vbNewLine)             'Test Socket Index
                StrReport.Append("Version_SW" & ";" & DatiProd.Value(ProdOtherInfos.VerSW) & vbNewLine)   'SW Version
                StrReport.Append("Version_HW" & ";" & DatiProd.Value(ProdOtherInfos.VerHW) & vbNewLine)   'HW Version
                StrReport.Append(vbNewLine)

                StrReport.Append("Sequence;Step Name;Status;Date;Time;Elapsed[ms];Value;Units;LimitLow;LimitHigh;Normal" & vbNewLine)  'Head
                Dim TSI As TestStepInformation
                For t As Integer = 0 To TestStepInfos(UUT).Length - 1
                    TSI = TestStepInfos(UUT)(t)
                    StrReport.Append(String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                                                   TSI.StepCode,
                                                   TSI.Remark,
                                                   TSI.TestResult.ToString(),
                                                   TSI.DateTime.ToString("yyyy-MM-dd"),
                                                   TSI.DateTime.ToString("HH:mm:ss"),
                                                   TSI.Elapsed.ToString("0.000"),
                                                   TSI.MeasValue,
                                                   TSI.Unit,
                                                   TSI.MinValue,
                                                   TSI.MaxValue,
                                                   TSI.NormalValue) & vbNewLine)
                Next

                StrReport.Append(vbNewLine)
                StrReport.Append("UUT Serial Number" & ";" & Barcodes(UUT) & vbNewLine) 'UUT Serial Number
                StrReport.Append("UUT Status" & ";" & IIf(Esito(UUT), "Pass", "Fail") & vbNewLine) 'UUT Result

                My.Computer.FileSystem.WriteAllText(DesFilePath, StrReport.ToString, False)

            Next
            StrReport = Nothing

        Catch ex As Exception
            MsgBox("Save .CSV Test result Fai!" & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub MemFailRecord()
        '***************************************
        'Insert all fail records into Database
        '***************************************
        If TestMode = TestModes.Debug Or AbortTest Then Exit Sub

        Dim PassNum As Integer, FailNum As Integer
        For UUT As Integer = IniUUT To EndUUT
            If Esito(UUT) = False Then
                If IncFailCount(UUT) Then
                    Dim FailStep As New Bitron.DataTracer.TestStepDescriptor()
                    With FailStep
                        .UUTNo = UUT
                        .SerialNo = FailStepInfo(UUT).Matrix
                        .StepIndex = FailStepInfo(UUT).StepNo
                        .StepNo = FailStepInfo(UUT).StepCode
                        .Description = Frmpannello.DGVTestItem.Rows(.StepIndex).Cells("ColDesc").Value
                        .MinValue = FailStepInfo(UUT).MinValue
                        .StdValue = FailStepInfo(UUT).NormalValue
                        .MaxValue = FailStepInfo(UUT).MaxValue
                        .Unit = FailStepInfo(UUT).Unit
                        .MesValue = FailStepInfo(UUT).MeasValue
                    End With
                    Call MyDataTracer.UpdateFailInfo(FailStep)
                    FailNum += 1
                End If
            Else
                PassNum += 1
            End If
        Next
        Call MyDataTracer.UpdatePassFailQty(PassNum, FailNum)
        Call MyDataTracer.UpdateTraceResult(Barcodes, Esito)
    End Sub

#Region "Function used for debug"

    Public Sub FillTestStepInfos()

        ReDim TestStepInfos(UUTNum - 1)(MyBench.TestSteps.Count - 1)
        For t As Integer = 0 To UUTNum - 1

            Dim ListTestSteps(MyBench.TestSteps.Count - 1) As TestStepInformation
            Dim TestStepInfo As TestStepInformation
            For i As Integer = 0 To MyBench.TestSteps.Count - 1
                TestStepInfo = New TestStepInformation()
                With TestStepInfo
                    .Initial()
                    .StepIndex = i + 1
                    .StepCode = MyBench.TestSteps(i).TestID
                    .Remark = MyBench.TestSteps(i).Remark
                    .MinValue = MyBench.TestSteps(i).MinValue
                    .MaxValue = MyBench.TestSteps(i).MaxValue
                    .NormalValue = MyBench.TestSteps(i).Normal
                    .Unit = MyBench.TestSteps(i).Unit
                    .IsTest = MyBench.TestSteps(i).Test
                    .TestResult = TestResults.NotExecuted
                End With
                ListTestSteps(i) = TestStepInfo
            Next
            TestStepInfos(t) = ListTestSteps
        Next
    End Sub
    Public Sub InitialTestStepInfos()
        For t As Integer = 0 To TestStepInfos.Length - 1
            For i As Integer = 0 To TestStepInfos(t).Count - 1
                TestStepInfos(t)(i).InitialValue()
            Next
        Next
    End Sub
    Public Sub GetCurrentTestStepInfo(Optional StopTimer As Boolean = False)
        Dim Elapsed As Double = StepTimeStopWatch.ElapsedMilliseconds
        Dim CurrIndex As Integer = TestStep.GetStepIndex(CurrentStepNo, True)
        If CurrIndex >= 0 Then
            For t As Integer = IniUUT To EndUUT
                TestStepInfos(t)(CurrIndex).DateTime = DateTime.Now
                TestStepInfos(t)(CurrIndex).Elapsed = Elapsed
                TestStepInfos(t)(CurrIndex).StepIndex = CurrIndex
                TestStepInfos(t)(CurrIndex).StepCode = CurrentStepNo.ToString()
                TestStepInfos(t)(CurrIndex).MeasValue = GetMesValue(t)
                TestStepInfos(t)(CurrIndex).MinValue = LimitValue.MinValue
                TestStepInfos(t)(CurrIndex).MaxValue = LimitValue.MaxValue
                TestStepInfos(t)(CurrIndex).NormalValue = LimitValue.NormalValue
                TestStepInfos(t)(CurrIndex).Unit = LimitValue.Unit
                TestStepInfos(t)(CurrIndex).Remark = LimitValue.Description
                TestStepInfos(t)(CurrIndex).IsTest = LimitValue.IsTest
                If Not LimitValue.IsTest Then
                    TestStepInfos(t)(CurrIndex).TestResult = TestResults.Skipped
                End If
            Next
        End If
        Call StepTimeStopWatch.Restart()
        If StopTimer Then Call StepTimeStopWatch.Stop()
    End Sub
    Public Sub GetCurrentTestStepResult(UUT As Integer, StepIndex As Integer, Result As Boolean)
        TestStepInfos(UUT)(StepIndex).TestResult = IIf(Result, TestResults.Passed, TestResults.Failed)
    End Sub
    Private Function WaitingForNext() As Integer
        Dim IntReturn As Integer = 0

        RunState = RunStates.Pause
        Call Frmpannello.SetDebugToolState()

        Do While (True)
            Application.DoEvents()
            Select Case RunState
                Case RunStates.Run, RunStates.ExecCurrent
                    IntReturn = TestStep.NextStep.Master
                    Exit Do

                Case RunStates.Stoped
                    IntReturn = TestStep.MasterNums + 1
                    Exit Do
                Case Else
                    If TestStep.NextStep.Master > TestStep.MasterNums Then
                        If AutoEndTest Then
                            IntReturn = TestStep.MasterNums + 1
                            RunState = RunStates.Stoped
                            Exit Do
                        End If
                    End If
            End Select
        Loop

        Call Frmpannello.SetDebugToolState()
        Return IntReturn
    End Function
    Private Function NeedCheck(ByVal StepNo As String, Optional ByVal AllowSkipped As Boolean = True) As Boolean

        '**************************************************
        'StepNo:the test step No,e.g:1,2,2-1,2-2....
        'AllowSkipped: Decide skip current step or not
        '**************************************************

        Call GetCurrentTestStepInfo()

        CurrentStepNo.Master = ContaTest
        CurrentStepNo.Slave = StepNo

        Dim RowIndex As Integer = TestStep.GetStepIndex(CurrentStepNo)
        If RowIndex = -1 Then
            Return False 'Not found this Test items in DGVTestItem list,so skipped
        ElseIf RowIndex = -2 Then
            If AllowSkipped = True Then Return False 'if not allow skip this step
            RowIndex = TestStep.GetStepIndex(CurrentStepNo, True)
        End If

        If TestMode = TestModes.Debug Then
            If Frmpannello.GetBreakState(RowIndex) = True Then '(如果遇到了断点，则等待)if found breakPoint then wait
                Call Frmpannello.DispCurrentStep(RowIndex)
                Me.WaitingForNext()
            End If
            If CurrentStepNo = TestStep.NextStep OrElse Not AllowSkipped Then
                If RunState = RunStates.Stoped Then Return False
                If RunState <> RunStates.ExecCurrent Then '(如果不是重新执行当前，则加1)'if is not Execute current step again
                    TestStep.IncStep()
                    If RunState = RunStates.Pause Then 'if Press Pause Button
                        Call Frmpannello.SetBreakPoint(TestStep.PNextStep)
                    End If
                End If
            Else
                Return False
            End If
        End If

        'Expand Cell
        If StepNo = 0 Then Call Frmpannello.ExpandRow(RowIndex) 'if is FoldRow
        'if Need test
        Dim TestState As Boolean = Frmpannello.GetCheckState(RowIndex)

        'If TestState = True Then
        'TestState = True 'if not allowskipped 
        Call Frmpannello.DispCurrentStep(RowIndex)
        Call MesResult.Initial()
        MesResult.TestNo = RowIndex
        MesResult.StepCode = CurrentStepNo.ToString
        Call GetLimitValue(RowIndex)
        'End If

        Frmpannello.CtrlConcise.SetTipText(CurrentStepNo.Master & "-" & CurrentStepNo.Slave & "  " & LimitValue.Description)
        'Application.DoEvents()

        If RunState <> RunStates.ExecCurrent Then
            Call Frmpannello.IncProgBar()
        End If

        Return TestState
    End Function
    Private Function ConvertStates(ByVal State As String) As String
        Dim Value As String = ""
        For t As Integer = 0 To State.Length - 1
            Value &= IIf(State.Substring(t, 1) = "1", "0", "1")
        Next
        Return Value
    End Function
    Private Sub PreparRetest()
        For t As Integer = IniUUT To EndUUT
            If RetryTest(t) = 0 Then
                BeforePass(t) = Esito(t)
                NowPass(t) = False
            End If

        Next
    End Sub
    Private Function CheckIfRetest(Optional ByVal RetryCheckNum As Integer = 3) As Boolean
        Dim NeedRetest As Boolean = False
        For t As Integer = IniUUT To EndUUT
            If RetryTest(t) < 0 Then 'if don't need retest again
                'Don't need do anything 
            ElseIf RetryTest(t) = 0 Then 'the first test
                If BeforePass(t) Then
                    If Esito(t) Then 'if the first tiem is Passed
                        NowPass(t) = True
                        Esito(t) = False 'skip test if other UUT need retest
                        RetryTest(t) = -1
                    Else
                        RetryTest(t) += 1 'Ready for test again
                        NeedRetest = True
                        Esito(t) = True
                    End If
                Else
                    'Before is already failed ,so don't need test again
                End If
            ElseIf RetryTest(t) > 0 And RetryTest(t) < RetryCheckNum Then 'Retest
                If BeforePass(t) Then
                    If Esito(t) Then 'if retest n time and passed
                        NowPass(t) = True
                        Esito(t) = False 'skip test if other UUT need retest 
                        RetryTest(t) = -1
                    Else
                        RetryTest(t) += 1
                        NeedRetest = True
                        Esito(t) = True
                    End If
                Else
                    'Before is already failed ,so don't need test again
                End If
            Else 'if retest n times but still NG
                If Esito(t) Then 'if retest >=n time and passed
                    NowPass(t) = True
                    RetryTest(t) = -1
                Else
                    NowPass(t) = False
                End If
            End If
        Next

        If NeedRetest Then
            If TestMode = TestModes.Debug Then
                Do While (TestStep.NextStep.Master > ContaTest Or TestStep.NextStep.Slave > 0)
                    TestStep.DecStep()
                Loop
            Else
                ContaTest -= 1
            End If
            For t As Integer = IniUUT To EndUUT
                If BeforePass(t) Then
                    FailStepInfo(t).StepNo = "-"
                End If
            Next
        Else
            For t As Integer = IniUUT To EndUUT
                Esito(t) = NowPass(t)
                RetryTest(t) = 0
            Next
        End If

        Return NeedRetest
    End Function

#End Region

#End Region

#End Region

#Region "Put All Custom Variant Declare here"
    Private Const RetryNum As Integer = 5
    Private UUTBarcode(UUTNum - 1) As String 'store the barcode when we use the thread to read barcode
    Private Enum Switchs
        SW1

    End Enum
    Public Enum Volts
        _230V
        _115V
    End Enum
    Private Enum Loads
        None
        Power = 1

    End Enum
    Private Enum LEDColors
        Red
        Green
        Brown
        RedBlink
    End Enum

    Public Enum FileTypes
        PRJ
        FRB
    End Enum

#End Region

#Region "Start and End Control Function here"
    Private Sub TimerMsg_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerMsg.Tick
        '*************************************
        'Polling Pic Com
        '*************************************
        Select Case MyTAT.Sysinfo.RunStartMode
            Case RunStartModes.Default
                StrMachineStates = MyRack.GetMachineStates()
                If StrMachineStates <> "" Then
                    If StrMachineStates.Contains("STARTEST") Then
                        StrMachineStates = "STARTEST"
                    End If
                End If
            Case RunStartModes.Keyboard
                If SpaceKeyPressed Then
                    SpaceKeyPressed = False
                    MyRack.SetMachineStates("STARTEST")
                    StrMachineStates = MyRack.GetMachineStates()
                Else
                    StrMachineStates = ""
                End If
            Case RunStartModes.Other
                If FindStartSignal() Then
                    MyRack.SetMachineStates("STARTEST")
                    StrMachineStates = MyRack.GetMachineStates()
                Else
                    StrMachineStates = ""
                End If
        End Select

        Select Case StrMachineStates
            Case "SBLOCK"
                MyTAT.ShowFailForm("Board FAIL")
            Case "ENDTST"
                MyTAT.HideFailForm()
                Call StartFailTube()
                MyTAT.ScanFiches.SaveBarcode(Barcodes, Esito, MyTAT.Sysinfo.ProdMode)
                Call Frmpannello.DispManaulScanPanel()
            Case "NOARIA"
                MyTAT.ShowFailForm("Board not in up position")
            Case "STARTEST"
                MyRack.ClearLogFile()
                If MachineState <> MachineStates.Fault And MachineState <> MachineStates.Diagnostic And MyTAT.ScanFiches.CanStartTest And MyTAT.ScanOKMatrix Then
                    MyRack.StartSpyPIC = False
                    MyTest.EseguiTest()
                    MyRack.StartSpyPIC = True
                Else
                    If MachineState <> MachineStates.Diagnostic Then
                        If MachineState = MachineStates.Fault Or MyTAT.ScanOKMatrix = False Or MyTAT.ScanFiches.CanStartTest = False Then
                            Board_PIC.EndTest("0000")
                            MachineState = MachineStates.Fault
                            Call DispMachineState()
                            MachineState = MachineStates.Free
                            MyTAT.ShowFailForm("Can't start Test Now!")
                        End If
                    End If
                End If
                MyRack.SaveLogFile()
        End Select

    End Sub
    Private Function FindStartSignal() As Boolean
        Return False
    End Function
    Public Sub InitialTest()
        '**********************************
        'Test is Start
        '**********************************
        ContaTest = 1
        AbortTest = False
        For t As Integer = 0 To UUTNum - 1
            Esito(t) = True
            FailStepInfo(t).Initial()
            IncFailCount(t) = True
        Next

        Call InitialTestStepInfos()
        Call ResetFailLED()
        Call Frmpannello.InitialForm()
        Board_PIC.SetMachineUpTime()
    End Sub
    Public Sub EndTest()
        '***************************************
        'Test is End
        '***************************************
        MachineState = MachineStates.Free

        'Display test result and Stop machine 
        Dim TestResult As String = IncPassFail()
        Call SetFailLED_Stamp(TestResult)

        Call Frmpannello.FinishTest()
        Call MemEsito()                 'Mem test value
        Call MemEsitoCSV()              'Mem CSV format data
        Call MemFailRecord()            'Mem Fail Step information to local database
        Call MemResultToSQL()           'Mem reuslt to SQL server

        'Scan Fiches code
        Dim FoundError As Boolean = False
        For t As Integer = IniUUT To EndUUT
            If Esito(t) = False Then FoundError = True
        Next
        If Not FoundError Then
            MyTAT.ScanFiches.SaveBarcode(Barcodes, Esito, TestMode)
            Call Frmpannello.DispManaulScanPanel()
        End If

        'Start Timer receive Message from Com
        TimerMsg.Enabled = True
        AbortTest = False
    End Sub

    Private Sub MemResultToSQL()
        If AbortTest Or Not UseSql Then Exit Sub

        If DatiProd.Value(ProdOtherInfos.SaveResultToSQL) = "1" Then
            For UUTNo As Integer = IniUUT To EndUUT
                If Barcodes(UUTNo) <> "" Then
                    If DatiProd.Value(ProdOtherInfos.BarcodeType).ToString <> "" And DatiProd.Value(ProdOtherInfos.BarcodeType).ToUpper = "PlasticBarcode" Then   '20190612 
                        MyTestRecords.SaveOBPResult(Barcodes(UUTNo), TestRecords.RecordsSQL.BarcodeTypes.PlasticBarocde, Esito(UUTNo))
                    Else
                        MyTestRecords.SaveOBPResult(Barcodes(UUTNo), TestRecords.RecordsSQL.BarcodeTypes.PCBDatamatrix, Esito(UUTNo))
                    End If

                End If
            Next
        End If
    End Sub



    Public Sub ResetFailLED()
        If MyTAT.Sysinfo.System.FixMoveType = FixtureMoveTypes.DCMotor Then
            Board_IO49WAY.SetOutput("00XX1", 10)
        End If
    End Sub
    Public Sub SetFailLED_Stamp(ByVal Value As String)
        If MyTAT.Sysinfo.System.FixMoveType = FixtureMoveTypes.DCMotor Then
            If Value = "0" Or Value = "00" Then 'Fail
                Board_IO49WAY.SetOutput("11XX0", 10)
                Board_IO49WAY.SetOutput("00", 15)
            ElseIf Value = "1" Or Value = "11" Then       'pass
                Board_IO49WAY.SetOutput("00XX1", 10)   'Turn off the fail led
                Board_IO49WAY.SetOutput("11", 15)         'stamp move
                Sleep(250)
                Board_IO49WAY.SetOutput("00", 15)         'stamp back
            Else                                          'One of UUT was failed
                Board_IO49WAY.SetOutput(ConvertStates(Value) & "XX0", 10)

                Board_IO49WAY.SetOutput(Value, 15)        'stamp move
                Sleep(250)
                Board_IO49WAY.SetOutput("00", 15)         'stamp back
            End If
        End If

        Board_PIC.EndTest(Value)
    End Sub
    Public Function VerifyFichesFormat(ByVal sender As Object, ByVal e As ClassScanFiches.FichesCheckEventArgs) As Boolean
        '*************************************
        'Verify Fiches format and Pakage format 
        'when need scan fiches after test
        '*************************************
        If e.MessageType = ClassScanFiches.FichesCheckEventArgs.MessageTypes.PackageSN Then
            '1.Check PackageSN
            If Mid(e.Message, 1, 1) = "S" Then
                Return True
            Else
                Return False
            End If
        ElseIf e.MessageType = ClassScanFiches.FichesCheckEventArgs.MessageTypes.FichesCode Then
            '2.Check FichesCode
            If e.Message.Substring(1) = DatiProd.Value(ProdInfos.CodeClient) Then
                Return True
            Else
                Return False
            End If
            'ElseIf e.MessageType = ClassScanFiches.FichesCheckEventArgs.MessageTypes.LayoutCode Then
            '    '3.Check Tray Code
            '    If e.Message.Length = DatiProd.Value(ProdOtherInfos.TrayCodeLength) Then
            '        Return True
            '    Else
            '        Return False
            '    End If

            'ElseIf e.MessageType = ClassScanFiches.FichesCheckEventArgs.MessageTypes.ExtLabel Then
            '    '4.Check ExtLabelLength 
            '    If e.Message.Length = DatiProd.Value(ProdOtherInfos.ExtLabelLength) Then
            '        Return True
            '    Else
            '        Return False
            '    End If

        Else
            Return False
        End If
    End Function


#End Region

#Region "Put all test codes here"
    Public Sub TestFVT(ByVal TestNo As Integer)
        Select Case TestNo
            Case 1
                Ver_Barcode()
            Case 2

        End Select

    End Sub
    Private Sub ReleaseResource()
        ''*************************************
        ''Release source when test fail
        ''*************************************
        Call PowerOFF_FCT()
        Call PowerOFF_PCB()
        Call Discharge()
    End Sub

#Region "Present product subroutine"
    Private Sub Ver_Barcode()
        '******************************************
        'Verify Barcode
        '******************************************
        If NeedCheck(0) Then
            If MyTAT.SysSettings.Sysparas.ManualScanLabel Then 'Check barcode pasted on Input Board
                Call CheckBarcode_Manual()
            Else
                Call CheckBarcode_Auto()
            End If
        End If
    End Sub
    Private Sub Ver_Flash()
        If NeedCheck(0) Then
            For t As Integer = IniUUT To EndUUT
                If Esito(t) Then
                    If DisconnectPowerWithFlash(0, t) Then
                        MesResult.MesValue = ConnectFlashWithPCB(0, t)
                        Call CheckAndDispResult(CheckModes.CheckEqual, t)
                    Else
                        MesResult.MesValue = "AC Power Not OFF"
                        Call CheckAndDispResult(CheckModes.CheckEqual, t)
                    End If
                End If
            Next
        End If

        If NeedCheck(1) Then
            For t As Integer = IniUUT To EndUUT
                If Esito(t) Then
                    MesResult.MesValue = FlashPrgram_WriteNow()
                    Call CheckAndDispResult(CheckModes.CheckEqual, t)
                End If
            Next
        End If

        If NeedCheck(2) Then
            Dim NeedWait As Boolean = False
            For t As Integer = IniUUT To EndUUT
                If Esito(t) Then
                    MesResult.MesValue = DisConnectFlashWithPCB(0, t)
                    Call CheckAndDispResult(CheckModes.CheckEqual, t)
                    NeedWait = True
                End If
            Next
            If NeedWait Then Sleep(250) 'Don't Modify or remove
        End If
    End Sub

    Public Sub Discharge()

    End Sub
    Public Function PowerON_FCT() As Boolean
        Board_PIC.Send("ACON")
        Risposta = Board_PIC.Receive

        If Risposta = "+" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function PowerOFF_FCT(Optional ByVal UUT As Integer = 0) As Boolean
        Board_PIC.Send("ACOFF")
        Risposta = Board_PIC.Receive

        If Risposta = "+" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function PowerON_PCB(Optional ByVal UUT As Integer = 0) As Boolean
        Dim Result As Boolean = False
        Result = Board_IO49WAY.SetOutput("1", UUT + 1)
        Return Result
    End Function
    Public Function PowerOFF_PCB(Optional ByVal UUT As Integer = 0) As Boolean
        Dim Result As Boolean = False
        Result = Board_IO49WAY.SetOutput("0", UUT + 1)
        Return Result
    End Function
    Private Function ConnectPCBToFlash(ByVal State As States) As Boolean
        Dim Result As Boolean
        If State = States.SetON Then
            Result = Board_IO49Output.SetOutput("1", 21)
        Else
            Result = Board_IO49Output.SetOutput("0", 21)
        End If
        Return Result
    End Function

#End Region

#Region "Common function defined here"
#Region "Flash Diag Panel"
    Public Function DisconnectPowerWithFlash(ByVal FlashIndex As Integer, Optional ByVal UUTIndex As Integer = 0) As Boolean
        Return PowerOFF_PCB()
    End Function
    Public Function ConnectFlashWithPCB(ByVal FlashIndex As Integer, Optional ByVal UUTIndex As Integer = 0) As Boolean
        Return ConnectPCBToFlash(States.SetON)
    End Function
    Public Function DisConnectFlashWithPCB(ByVal FlashIndex As Integer, Optional ByVal UUTIndex As Integer = 0) As Boolean
        Return ConnectPCBToFlash(States.SetOFF)
    End Function
#End Region

#Region "FailTube"
    ''' <summary>
    ''' 显示Fail Tube扫描
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DispFailTube()
        MyTAT.StartFailTube()
    End Sub
    Private Sub StartFailTube()
        Dim FailCodes As New List(Of String)
        For t As Integer = IniUUT To EndUUT
            If Esito(t) = False Then
                If Barcodes(t) <> "" Then
                    FailCodes.Add(Barcodes(t))
                End If
            End If
        Next
        If FailCodes.Count > 0 Then
            MachineState = MachineStates.Diagnostic
            MyTAT.InsertFailBarcode(FailCodes) 'Insert and start Tube to get failed PCB
            MachineState = MachineStates.Fail
        End If
    End Sub
    Public Function LockTube() As Boolean
        'Board_PIC.Send("FAILBDIN")
        'Risposta = Board_PIC.Receive
        'If Risposta = "+" Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function
    Public Function UnLockTube() As Boolean
        'Board_PIC.Send("FAILBDOUT")
        'Risposta = Board_PIC.Receive
        'If Risposta = "+" Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function
    Public Function ReadFailBarCode() As String
        Dim Barcode As String = ""
        Try
            Barcode = Device_DataMatrix.ReadMatrix(2)
        Catch ex As Exception
        End Try
        Return Barcode
    End Function
    Public Function WaitPCBArrived() As Boolean
        Board_PIC.Send("FAILBD")
        Dim Risp As String = Board_PIC.Receive(5)
        If Risp = "GETFAILBOARD" Then
            Return True
        Else
            Return False
        End If
    End Function


#End Region

    Private Function FlashPrgram_WriteNow()
        '******************************************
        'Flash up with WriteNow
        '******************************************
        'Start Flash Program
        Dim Retry As Integer
        Dim FlashOK As Boolean
        While Not FlashOK And Retry <= 2
            FlashOK = Device_WriteNow.FlashProgram(DatiProd.Value(ProdOtherInfos.FlashScript), 0, 20)
            If Not FlashOK Then
                Sleep(100)
                Retry += 1
            End If
            Application.DoEvents()
        End While

        Return FlashOK
    End Function
    Private Function FlashPrgram_FlashRunner()
        '******************************************
        'Flash up with WriteNow
        '******************************************
        'Start Flash Program
        Dim Retry As Integer
        Dim FlashOK As Boolean
        While Not FlashOK And Retry <= 2
            FlashOK = Device_Flashrunner.FlashProgram(DatiProd.Value(ProdOtherInfos.FlashScript), 0, 10)
            If Not FlashOK Then
                Sleep(100)
                Retry += 1
            End If
            Application.DoEvents()
        End While

        Return FlashOK
    End Function
    Private Function FlashPrgram_Cyclone()
        '******************************************
        'Flash up with WriteNow
        '******************************************
        'Start Flash Program
        Dim Retry As Integer
        Dim FlashOK As Boolean
        While Not FlashOK And Retry <= 2
            FlashOK = Device_Cyclone.FlashProgram(DatiProd.Value(ProdOtherInfos.FlashScript), DatiProd.Value(ProdOtherInfos.RelFW1), 0, 20)
            If Not FlashOK Then
                Sleep(100)
                Retry += 1
            End If
            Application.DoEvents()
        End While

        Return FlashOK
    End Function
    Public Sub CheckFlashNameFromCyclone()
        Dim HWList As New List(Of Bitron3DEQ.FVTRackBoardManager.HardwareInformation)
        HWList = MyRack.GetHardwareInfos_Internal(Bitron3DEQ.FVTRackBoardManager.ExtHardwareType.Cyclone)
        If HWList.Count <= 0 Then Exit Sub

        For t As Integer = IniUUT To EndUUT
            Dim Strname As String = Device_Cyclone.GetImageDescription(DatiProd.Value(ProdOtherInfos.FlashScript), t)
            If Strname.StartsWith(DatiProd.Value(ProdOtherInfos.FlashDesc)) Then
                Throw New Exception("Error:Flash inside cyclone=" & Strname & " is not same with setting parameter=" & DatiProd.Value(ProdOtherInfos.FlashDesc))
            End If
        Next
    End Sub

    Public Sub C2000GangInitial()
        Dim FW As String = Application.StartupPath & "\SysFile\FW\" + DatiProd.Value(ProdOtherInfos.FlashScript)
        Dim C200GangDeviceType As String = DatiProd.Value(ProdOtherInfos.C2000GangDeviceType)
        Dim C200GangTargetSite As Byte = Byte.Parse(DatiProd.Value(ProdOtherInfos.C2000GangTargetSite))

        With Device_TexasGANG.FlashParameter
            .ScriptPath = FW
            .ImageName = "DABInvert"
            .DeviceType = C200GangDeviceType
            .TargetSite = C200GangTargetSite
        End With

        If Device_TexasGANG.LoadConfig(True) = False Then
            Throw New Exception("Texas C2000 Gang programmer initial Failed!")
        End If
    End Sub
    Public Sub FlashProgram_C2000Gang()
        'Start Flash Program
        Dim Result() As Boolean = Device_TexasGANG.FlashProgram(1000, True)
        For t As Integer = IniUUT To EndUUT
            If Esito(t) Then
                MesResult.MesValue = Result(t + 1)
                Call CheckAndDispResult(CheckModes.CheckEqual, t)

                If (Not Result(t + 1)) Then
                    Call AppendDispValue("," + Device_TexasGANG.ErrMsg)
                End If
            End If
        Next
    End Sub

    Public Function MoveCameraToPosition(ByVal Postion As String)
        Dim XPos As Integer = 0
        Dim YPos As Integer = 0
        Dim TempPos() As String = Postion.Split(",")
        Try
            XPos = CInt(TempPos(0))
            YPos = CInt(TempPos(1))
            Return MyTest.MoveCamera(XPos, YPos)
        Catch ex As Exception
        End Try
        Return False
    End Function
    Public Function MoveCamera(ByVal XPos As Integer, ByVal YPos As Integer) As Boolean
        Dim Result As Boolean = False
        Result = Device_NewMotor.MoveCameraToPos_XY(XPos, YPos, 0)
        Return Result
    End Function


    Private Sub SyncReadBarcode(ByVal UUTNo)
        Dim GetBarcode As Boolean = False
        Dim RetryGetNum As Integer = 0
        While Not GetBarcode And RetryGetNum < 3
            UUTBarcode(UUTNo) = Device_DataMatrix.ReadMatrix(UUTNo)
            If UUTBarcode(UUTNo) <> "" Then
                If UUTBarcode(UUTNo).Length = DatiProd.Value(ProdOtherInfos.BarcodeLength) + 2 Then
                    GetBarcode = True
                End If
            End If
            If Not GetBarcode Then
                RetryGetNum += 1
                Sleep(200)
            End If
        End While
    End Sub
    Private Sub CheckLabelPos(ByRef OutBarcode() As String)
        Dim ReaderPos As String = ""  'readerpos = 1 if we read 1-xxxxxx
        Dim BarcodeIndex As Integer = -1  'if the readerpos = 1 then the barcodeindex = 1 -1 = 0
        Dim GetBarcode As Boolean = False
        Dim RetryGetNum As Integer = 0

        'Sync read barcode
        Dim THsReadBarcode(UUTNum - 1) As Threading.Thread
        For i As Integer = 0 To UUTNum - 1
            UUTBarcode(i) = ""
            OutBarcode(i) = "NoRead"
            THsReadBarcode(i) = New Threading.Thread(AddressOf SyncReadBarcode)
            THsReadBarcode(i).Start(i)
            Sleep(300)
        Next

        For i As Integer = 0 To UUTNum - 1
            THsReadBarcode(i).Join()
            If Not THsReadBarcode(i).IsAlive Then THsReadBarcode(i).Abort()
        Next


        For ReaderNo As Integer = 0 To UUTNum - 1
            If UUTBarcode(ReaderNo) <> "" Then
                Try
                    ReaderPos = UUTBarcode(ReaderNo).Substring(0, 1)
                    BarcodeIndex = CInt(ReaderPos) - 1
                    OutBarcode(BarcodeIndex) = UUTBarcode(ReaderNo).Substring(2)
                Catch ex As Exception
                End Try
            End If
        Next
    End Sub
    Private Sub CheckBarcode_Auto()
        Dim GetBarcode(UUTNum - 1) As String
        Call CheckLabelPos(GetBarcode)  'Check the relations between Scanner and Label
        For t As Integer = IniUUT To EndUUT
            If Esito(t) Then
                Retry = 0
                TestOK = False
                While Retry <= 0 And Not TestOK
                    MesResult.MesValue = GetBarcode(t) 'Device_DataMatrix.ReadMatrix(t)  'Read barcode
                    MesResult.Result = CheckMatrix(t)                     'Verify barcode format
                    Call DispResult(t)                                    'Display verify result

                    If MesResult.Result = True Then
                        TestOK = True
                        FailStepInfo(t).Matrix = MesResult.MesValue
                    Else
                        Retry += 1
                    End If
                End While

                If MesResult.Result = False Then
                    IncFailCount(t) = False
                    Call GetFailInfo(t)
                End If
            End If
        Next
    End Sub
    Private Sub CheckBarcode_Manual()
        Dim Barcode As String = ""
        For t As Integer = IniUUT To EndUUT
            If Esito(t) Then
                Barcode = MyTAT.GetReadMatrix(t)
                MesResult.MesValue = Barcode
                MesResult.MesValue = CheckMatrix(t).ToString()
                Call CheckAndDispResult(CheckModes.CheckEqual, t)
                Call AppendDispValue(Barcode, t, True)
                FailStepInfo(t).Matrix = Barcode
            End If
        Next
    End Sub
    Private Function CheckMatrix(Optional ByVal UUT As Integer = 0) As Boolean
        Dim Barcode As String = MesResult.MesValue

        If Barcode.Length <> DatiProd.Value(ProdOtherInfos.BarcodeLength) Then Return False
        If DatiProd.Value(ProdOtherInfos.BarcodeCase) <> "" Then
            If Barcode.Substring(6, 2) <> DatiProd.Value(ProdOtherInfos.BarcodeCase) Then Return False
        Else
            If Barcode.Substring(0, 8) <> DatiProd.Value(ProdInfos.CodeBitron) Then Return False
        End If

        Barcodes(UUT) = Barcode
        Return True
    End Function

    Private Sub CheckBuzzer(Optional ByVal UUTNo As Integer = 0)
        Retry = 0
        TestOK = False
        While Retry <= 10 And Not TestOK
            MesResult.MesValue = Board_LEDMIC.ReadMICInputByBit(UUTNo)
            Call CheckResult(CheckModes.CheckRange)
            Call DispResult(UUTNo)
            TestOK = MesResult.Result
            If Not TestOK Then
                Retry += 1
                Sleep(20)
            End If
        End While
        If Not TestOK Then Call GetFailInfo(UUTNo)
    End Sub

    Private Sub CheckACVoltage(ByVal UUTNo As Integer)
        Retry = 1
        TestOK = False
        While Retry <= 50 And Not TestOK
            MesResult.MesValue = Device_ACMeter8775.ReadACVolt
            Call CheckResult(CheckModes.CheckRange)
            Call DispResult(UUTNo)

            TestOK = MesResult.Result
            If Not TestOK Then
                Retry += 1
                Sleep(50)
            End If
        End While
        If Not TestOK Then Call GetFailInfo(UUTNo)
    End Sub
    Private Sub CheckACPower(ByVal UUTNo As Integer, ByRef PowerValue As Single)
        Dim Value As Single = 0.0
        Retry = 1
        TestOK = False
        While Retry <= 50 And Not TestOK
            Value = Device_ACMeter8775.ReadACPower
            MesResult.MesValue = Value.ToString("0.00")
            Call CheckResult(CheckModes.CheckRange)
            Call DispResult(UUTNo)

            TestOK = MesResult.Result
            If Not TestOK Then
                Retry += 1
                Sleep(50)
            End If
        End While
        If Not TestOK Then Call GetFailInfo(UUTNo)

        PowerValue = Value
    End Sub
    Private Sub CheckACOutput(ByVal UUTNo As Integer, ByVal Load As Loads)
        Retry = 1
        TestOK = False
        While Retry <= 10 And Not TestOK
            Try
                MesResult.MesValue = Board_HVOC.ReadOutput(Load)
                Call CheckResult()
                Call DispResult(UUTNo)
            Catch ex As Exception
                MesResult.Result = False
            End Try

            TestOK = MesResult.Result
            If Not TestOK Then
                Retry += 1
                Sleep(50)
            End If
        End While
        If Not TestOK Then Call GetFailInfo(UUTNo)
    End Sub

    Private Sub CheckDCVolt(ByVal Channel As Integer, Optional ByVal UUTNo As Integer = 0, Optional ByVal ADAMNo As Integer = 0, Optional ByVal MultiRate As Single = 1.0)
        '***********************************
        'TP1: ADAM#2,Ch0-Ch3 (VCC)
        'TP5: ADAM#2,Ch4-Ch7
        'TP3: ADAM#1,Ch0-Ch3 (Output)
        'TP6: ADAM#1,Ch4-Ch7
        '***********************************
        If Esito(UUTNo) Then
            Retry = 0
            TestOK = False
            While Retry <= 12 And Not TestOK
                MesResult.MesValue = Board_ADAM.ReadV(Channel, ADAMNo)
                If MultiRate <> 1.0 Then
                    If MesResult.MesValue <> "" Then
                        Try
                            Dim MultiVolt As Single = CSng(MesResult.MesValue) * MultiRate
                            MesResult.MesValue = MultiVolt.ToString
                        Catch ex As Exception
                        End Try
                    End If
                End If
                Call CheckResult(CheckModes.CheckRange)
                Call DispResult(UUTNo)

                TestOK = MesResult.Result
                If Not TestOK Then
                    Retry += 1
                    Sleep(20)
                End If
            End While
            If Not TestOK Then Call GetFailInfo(UUTNo)
        End If
    End Sub
    Private Sub CheckDCCurrent(ByVal Channel As Integer, Optional ByVal UUTNo As Integer = 0, Optional ByVal ADAMNo As Integer = 0, Optional ByVal MultiRate As Single = 1.0, Optional ByVal Resistor As Integer = 100)
        '***********************************
        'TP1: ADAM#2,Ch0-Ch3 (VCC)
        'TP5: ADAM#2,Ch4-Ch7
        'TP3: ADAM#1,Ch0-Ch3 (Output)
        'TP6: ADAM#1,Ch4-Ch7
        '***********************************
        If Esito(UUTNo) Then
            Dim MesVolt As Single
            Retry = 0
            TestOK = False
            While Retry <= 12 And Not TestOK
                MesVolt = CSng(Board_ADAM.ReadV(Channel, ADAMNo))
                If MultiRate <> 1.0 Then
                    If MesResult.MesValue <> "" Then
                        Try
                            MesVolt = MesVolt * MultiRate
                        Catch ex As Exception
                        End Try
                    End If
                End If
                Dim MesCurrent As Single = (MesVolt / Resistor) * 1000
                MesResult.MesValue = MesCurrent.ToString

                Call CheckResult(CheckModes.CheckRange)
                Call DispResult(UUTNo)

                TestOK = MesResult.Result
                If Not TestOK Then
                    Retry += 1
                    Sleep(20)
                End If
            End While
            If Not TestOK Then Call GetFailInfo(UUTNo)
        End If
    End Sub
    Private Sub CheckDCPower(ByVal Channel As Integer, ByVal UUTNo As Integer, ByVal Resistor As Single, Optional ByVal ADAMNo As Integer = 0, Optional ByRef PowerValue As Single = 0.0)
        '***********************************
        'TP1: ADAM#2,Ch0-Ch3 (VCC)
        'TP5: ADAM#2,Ch4-Ch7
        'TP3: ADAM#1,Ch0-Ch3 (Output)
        'TP6: ADAM#1,Ch4-Ch7
        '***********************************
        If Esito(UUTNo) Then
            Dim Volt As Single = 0.0
            Dim Power As Single = 0.0
            Retry = 0
            TestOK = False
            While Retry <= 12 And Not TestOK
                Volt = Board_ADAM.ReadV(Channel, ADAMNo)
                Power = (Volt * Volt) / Resistor
                MesResult.MesValue = Power.ToString("0.00")
                Call CheckResult(CheckModes.CheckRange)
                Call DispResult(UUTNo)

                TestOK = MesResult.Result
                If Not TestOK Then
                    Retry += 1
                    Sleep(20)
                End If
            End While
            If Not TestOK Then Call GetFailInfo(UUTNo)

            PowerValue = Power
        End If
    End Sub
#End Region

#Region "FlashRunner"
    Public ErrMSg As String = ""


    Private GetFlashInfoFromDBOK As Boolean = False
    Public Function GetFlashInfosFromDB() As Boolean
        Try
            FlashBinFile = ""
            FlashScriptFile = ""
            FlashBinFileCRC = ""
            FlashScriptFileCRC = ""
            MyMemSQL.GetFlashInfos(DatiProd.Value(ProdInfos.CodeBitron), FlashBinFile, FlashScriptFile, FlashBinFileCRC, FlashScriptFileCRC, FlashUsable)
            GetFlashInfoFromDBOK = True
            If FlashUsable = False Then Throw New Exception("This Model Locked by Admin:" & MyTAT.Sysinfo.System.BitronCode)
        Catch ex As Exception
            GetFlashInfoFromDBOK = False
            Throw New Exception("FlashRuuner Script Info Error:" & ex.Message)
        End Try
    End Function

    Public Sub InitFlashRunner()
        '--------------------------
        'More than 4 UUT
        Dim MyEndUUT As Integer = EndUUT
        If EndUUT > 3 Then
            MyEndUUT = 3
        End If
        If Not GetFlashInfoFromDBOK Then Exit Sub
        Try
            ErrMSg = ""
            Dim Result As Boolean = False
            Dim Msg As String = ""
            'Dim FlashInfoDirectory As String = Path.Combine(MyTAT.Sysinfo.System.BenchXMLPath.Substring(0, 46), "SW\" & DatiProd.Value(ProdInfos.CodeBitron))

            Dim FlashInfoDirectory As String = Path.Combine(MyTAT.Sysinfo.System.BenchXMLPath.Substring(0, 45), "SW\" & DatiProd.Value(ProdInfos.CodeBitron))

            Dim BinaryFileNames() As String = FlashBinFile.Split({";"}, StringSplitOptions.RemoveEmptyEntries)
            Dim ProjectFileNames() As String = FlashScriptFile.Split({";"}, StringSplitOptions.RemoveEmptyEntries)
            Dim BinFileCRCs() As String = FlashBinFileCRC.Split({";"}, StringSplitOptions.RemoveEmptyEntries)
            Dim ProjectFileCRCs() As String = FlashScriptFileCRC.Split({";"}, StringSplitOptions.RemoveEmptyEntries)

            Dim Infos As List(Of String) = New List(Of String)
            Infos.Add("-1Resets The Master Controller")
            Infos.Add("-1Open Flashrunner Communication")

            For i As Integer = 0 To BinaryFileNames.Length - 1
                Infos.Add("Check Server Binary File:" & BinaryFileNames(i))
            Next

            For i As Integer = 0 To ProjectFileNames.Length - 1
                Infos.Add("Check Server Script File:" & ProjectFileNames(i))
            Next

            ''检查CRC 不再使用离线工具检查CRC，新版本在文件传送时自动校验CRC，如文件不完整会传送失败
            'For i As Integer = 0 To BinFileCRCs.Length - 1
            '    Infos.Add("FlashRunner Binary File CRC:" & "-")
            'Next

            'For i As Integer = 0 To ProjectFileCRCs.Length - 1
            '    Infos.Add("Check FlashRunner Script File CRC:" & ProjectFileCRCs(i))
            'Next
            ''--

            For i As Integer = 0 To BinaryFileNames.Length - 1
                Infos.Add("Delete FlashRunner SD Card Binary File:" & BinaryFileNames(i))
            Next

            For i As Integer = 0 To BinaryFileNames.Length - 1
                Infos.Add("Delete FlashRunner SD Card Script File:" & ProjectFileNames(i))
            Next

            For i As Integer = 0 To BinaryFileNames.Length - 1
                Infos.Add("Copy Binary File To FlashRunner SD Card:" & BinaryFileNames(i))
            Next

            For i As Integer = 0 To ProjectFileNames.Length - 1
                Infos.Add("Copy Script File To FlashRunner SD Card:" & ProjectFileNames(i))
            Next

            For i As Integer = 0 To BinaryFileNames.Length - 1
                Infos.Add("Check FlashRunner Binary File:" & BinaryFileNames(i))
            Next

            For i As Integer = 0 To ProjectFileNames.Length - 1
                Infos.Add("Check FlashRunner Script File:" & ProjectFileNames(i))
            Next

            For i As Integer = 0 To BinFileCRCs.Length - 1
                Infos.Add("FlashRunner Binary File CRC:" & "-")
            Next

            For i As Integer = 0 To ProjectFileCRCs.Length - 1
                Infos.Add("Check FlashRunner Script File CRC:" & ProjectFileCRCs(i))
            Next

            Dim z As Integer = 0
            Call ShowMgsForm(Infos)
            Fr2.CurrentProgress = New FlashInterface.TransferStatus(AddressOf ShowProgressStatus)

            ''1.Resets the master controller
            Result = ResetFlashRunner(-1, z)
            Result = True
            If Not Result Then GoTo Err

            z = 1
            ''2.Open flashrunner
            Result = OpenFlashRunner(-1, z)
            If Not Result Then GoTo Err

            z = 2
            ''3.检查服务器上的文件和DataBase中的文件是否一致
            For Each BinFile In BinaryFileNames
                Dim FlashBinaryName As String = Path.Combine(FlashInfoDirectory, "BINARIES\" & BinFile)
                Result = CheckSeverFile(FlashBinaryName, -1, z)
                If Not Result Then GoTo Err
                z += 1
            Next

            For Each ScriptFile In ProjectFileNames
                Dim FlashScriptName As String = Path.Combine(FlashInfoDirectory, "SCRIPTS\" & ScriptFile)
                Result = CheckSeverFile(FlashScriptName, -1, z)
                    If Not Result Then GoTo Err
                z += 1
            Next

            ' ''7.检查FlushRunner中文件的CRC
            'For i As Integer = 0 To BinaryFileNames.Length - 1
            '    Result = CheckSDCardFileCRC(FileTypes.FRB, "FRB " & BinaryFileNames(i), -1, z, BinFileCRCs(i), True)
            '    z += 1
            'Next

            'For i As Integer = 0 To ProjectFileNames.Length - 1
            '    Result = CheckSDCardFileCRC(FileTypes.PRJ, "PRJ " & ProjectFileNames(i), -1, z, ProjectFileCRCs(i), True)
            '    z += 1
            'Next

            ''4. 删除原有FRB文件
            For i As Integer = 0 To BinaryFileNames.Length - 1
                Result = DeleteSDCardFile("FRB", BinaryFileNames(i), z)
                If Not Result Then GoTo Err
                z += 1
            Next

            ''5. 删除原有PRJ
            For i As Integer = 0 To ProjectFileNames.Length - 1
                Result = DeleteSDCardFile("PRJ", ProjectFileNames(i), z)
                If Not Result Then GoTo Err
                z += 1
            Next


            ''5.复制服务中的文件到FlushRunner中
            For Each BinFile In BinaryFileNames
                Dim FlashBinaryName As String = Path.Combine(FlashInfoDirectory, "BINARIES\" & BinFile)
                'Result = CopyFileToSDCard(FlashBinaryName, "\BINARIES", t, z)
                Result = CopyFileToSDCard(FlashBinaryName, "FRB", -1, z)
                If Not Result Then GoTo Err
                z += 1
            Next


            For Each ScriptFile In ProjectFileNames
                Dim FlashScriptName As String = Path.Combine(FlashInfoDirectory, "SCRIPTS\" & ScriptFile)
                Result = CopyFileToSDCard(FlashScriptName, "PRJ", -1, z)
                    If Not Result Then GoTo Err
                z += 1
            Next


            ''6.检查FlushRunner中的文件是否导入
            For Each BinFile In BinaryFileNames
                Result = CheckSDCardFileExists(BinFile, "FRB", -1, z)
                If Not Result Then GoTo Err
                z += 1
            Next

            For Each ScriptFile In ProjectFileNames
                Result = CheckSDCardFileExists(ScriptFile, "PRJ", -1, z)
                If Not Result Then GoTo Err
                z += 1
            Next

            ''7.检查FlushRunner中文件的CRC

            For i As Integer = 0 To BinaryFileNames.Length - 1
                Result = CheckSDCardFileCRC(FileTypes.FRB, "FRB " & BinaryFileNames(i), 0, z, BinFileCRCs(i))
                If Not Result Then GoTo Err
                z += 1
            Next


            For i As Integer = 0 To ProjectFileNames.Length - 1
                Result = CheckSDCardFileCRC(FileTypes.PRJ, "PRJ " & ProjectFileNames(i), 0, z, ProjectFileCRCs(i))
                If Not Result Then GoTo Err
                z += 1
            Next

Err:


            Call EndMsgForm(Result)

            If Result = False Then Throw New Exception("Init FlashRunner Error" & Environment.NewLine & ErrMSg)
            'If Result = False Then MessageBox.Show("Init FlashRunner Error" & Environment.NewLine & ErrMSg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            'Throw New Exception("Init FlashRunner Error:" & ex.Message)
            Throw New Exception("Init FlashRunner Error:" & ex.Message)
            'MessageBox.Show("Init FlashRunner Error:" & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Public Function OpenFlashRunner(ByVal UUTNo As Integer, ByVal InfoIndex As Integer) As Boolean

        Call SetDGVMsgForm(TipStatus.Loading, UUTNo, InfoIndex)

        'Dim Result As Boolean = MyInterface(0).OpenCom()
        Dim Result As Boolean = Fr2.OpenCommunicationA()

        Call SetDGVMsgForm(IIf(Result, TipStatus.PASS, TipStatus.FAIL), UUTNo, InfoIndex)

        If Result = False Then ErrMSg = "Open flashrunner error"

        Return Result

    End Function
    Public Function ResetFlashRunner(ByVal UUTNo As Integer, ByVal InfoIndex As Integer) As Boolean

        Call SetDGVMsgForm(TipStatus.Loading, UUTNo, InfoIndex)

        'Dim Result As Boolean = Fr2.SendRec_DLL("MCRESET MC")
        Dim Result As Boolean = True
        Sleep(1000)

        Call SetDGVMsgForm(IIf(Result, TipStatus.PASS, TipStatus.FAIL), UUTNo, InfoIndex)

        If Result = False Then ErrMSg = "Resets the master controller error"

        Return Result

    End Function
    Public Function CheckSeverFile(ByVal FilePath As String, ByVal UUTNo As Integer, ByVal InfoIndex As Integer) As Boolean

        Call SetDGVMsgForm(TipStatus.Loading, UUTNo, InfoIndex)

        Dim Result As Boolean = File.Exists(FilePath)

        Call SetDGVMsgForm(IIf(Result, TipStatus.PASS, TipStatus.FAIL), UUTNo, InfoIndex)

        If Result = False Then ErrMSg = "Can't Find File:" & FilePath

        Return Result

    End Function
    Public Function DeleteSDCardFile(ByVal FileType As String, ByVal FileName As String, ByVal InfoIndex As Integer) As Boolean
        Call SetDGVMsgForm(TipStatus.Loading, -1, InfoIndex)

        Dim Result As Boolean = Fr2.DelectFileFormSD(FileType, FileName)

        Call SetDGVMsgForm(IIf(Result, TipStatus.PASS, TipStatus.FAIL), -1, InfoIndex)
        If Result = False Then ErrMSg = "Delete SD Card File Error:" & Dir() & Environment.NewLine & Fr2.ErrMsg

        Return Result
    End Function
    Public Function CopyFileToSDCard(ByVal SoureceFile As String, ByVal dir As String, ByVal UUTNo As Integer, ByVal InfoIndex As Integer) As Boolean
        Call SetDGVMsgForm(TipStatus.Loading, UUTNo, InfoIndex)

        Dim Result As Boolean = Fr2.CopyFileToSD(SoureceFile, dir)

        Call SetDGVMsgForm(IIf(Result, TipStatus.PASS, TipStatus.FAIL), UUTNo, InfoIndex)
        If Result = False Then ErrMSg = "Copy File To SD Card Error:" & SoureceFile & Environment.NewLine & Fr2.ErrMsg

        Return Result
    End Function
    Public Function CheckSDCardFileExists(ByVal FileName As String, ByVal FileType As String, ByVal UUTNo As Integer, ByVal InfoIndex As Integer) As Boolean
        Call SetDGVMsgForm(TipStatus.Loading, UUTNo, InfoIndex)

        Dim Result As Boolean = Fr2.FileExists(FileName, FileType)

        Call SetDGVMsgForm(IIf(Result, TipStatus.PASS, TipStatus.FAIL), UUTNo, InfoIndex)
        If Result = False Then ErrMSg = "Check SD Card Error:" & FileName & Environment.NewLine & Fr2.ErrMsg

        Return Result
    End Function



    Public Function CheckSDCardFileCRC(ByVal Filetype As FileTypes, ByVal FileName As String, ByVal UUTNo As Integer, ByVal InfoIndex As Integer, ByVal DBCRC As String, Optional ByVal IsBeforeCRC As Boolean = False) As Boolean
        Call SetDGVMsgForm(TipStatus.Loading, -1, InfoIndex)

        Dim Value As String = Fr2.GetFileCRC32(FileName)
        'Dim Value As String = "CRC123456"
        Dim Result As Boolean = False


        Select Case Filetype
            Case FileTypes.FRB
                Result = True
            Case FileTypes.PRJ
                If Value = DBCRC Then
                    Result = True
                Else
                    Result = False
                End If
        End Select

        If IsBeforeCRC Then
            Call SetDGVMsgForm(IIf(Result, TipStatus.CRCSame, TipStatus.CRCNotSame), -1, InfoIndex)
        Else
            Call SetDGVMsgForm(IIf(Result, TipStatus.PASS, TipStatus.FAIL), -1, InfoIndex)
        End If
        Call UpdateDGVMsgForm(Value)

        If Result = False Then ErrMSg = "Check SD Card CRC Error:" & FileName & Environment.NewLine & Fr2.ErrMsg

        Return Result
    End Function

    Public Function CheckFlashFileCanUse() As Boolean


    End Function

#End Region

#Region "Message"
    Public Enum TipStatus
        PASS
        FAIL
        Loading
        CRCSame
        CRCNotSame
        'None
    End Enum
    Private FrmMSg As FormMsg
    Public Sub ShowMgsForm(ByVal Infos As List(Of String))
        Call CloseMgsForm()
        FrmMSg = New FormMsg(UUTNum, Infos)
        FrmMSg.Show()

        Call SetTipMsgForm(TipStatus.Loading)
    End Sub
    Public Sub EndMsgForm(ByVal Result As Boolean)
        If Result Then
            SetTipMsgForm(TipStatus.PASS)
            Call CloseMgsForm()
        Else
            SetTipMsgForm(TipStatus.FAIL)
        End If
    End Sub
    Public Sub CloseMgsForm()
        Try
            FrmMSg.Close()
        Catch ex As Exception

        End Try


    End Sub
    Public Sub SetTipMsgForm(ByVal TipStatus As TipStatus)

        FrmMSg.SetTip(TipStatus)

    End Sub
    Public Sub SetDGVMsgForm(ByVal TipStatus As TipStatus, ByVal UUTNo As Integer, ByVal InfoIndex As Integer)
        FrmMSg.SetDGVStatus(TipStatus, UUTNo, InfoIndex)
    End Sub
    Public Sub UpdateDGVMsgForm(ByVal Msg As String)
        Try
            FrmMSg.SetDGVStatus2(Msg)
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' 显示传送文件的进度
    ''' </summary>
    ''' <param name="file_len"></param>
    ''' <param name="file_pos"></param>
    Public Sub ShowProgressStatus(ByVal file_len As Integer, ByVal file_pos As Integer)
        'RaiseEvent currentprogress(file_len, file_pos)

        Dim Msg As String = file_pos & "/" & file_len

        MyTest.UpdateDGVMsgForm(Msg)
    End Sub
#End Region

#End Region

End Class

