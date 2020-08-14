Imports System.IO

Public Class Form1

    Public SrcFile As String
    Public DesFile As String

    Public MyMemSQLEclipse As ClassSQLEclipse
    Public MyFileCollector As ClassFileCollect

    Public MyConfig As ClassConfig

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Call GetAllFiles()
    End Sub

    Public Sub GetAllFiles()
        Dim sw As New Stopwatch
        Dim Res As Boolean = False
        Dim Files As List(Of String)
        Dim PhaseID As String = CmdPhaseID.Text
        Files = MyFileCollector.GetFiles()
        If Files.Count > 0 Then
            For Each T In Files
                TxtMsg.AppendText(DateTime.Now & "  " & T.ToString)
                Dim TItem As New ClassFileCollect.ClassTestItem
                TItem = MyFileCollector.AnalyseData(T.ToString) 'Analyse
                sw.Restart()
                Res = StoreAnalyseData(TItem, TxtBitronCode.Text, CmdPhaseID.Text)
                If Res Then
                    TxtMsg.AppendText(" ---> PASS" & " " & sw.ElapsedMilliseconds & vbCrLf)
                Else
                    TxtMsg.AppendText(" ---> FAIL" & " " & sw.ElapsedMilliseconds & vbCrLf)
                End If
                MyFileCollector.MoveToDesFile(T.ToString)
            Next
        End If
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MyMemSQLEclipse = New ClassSQLEclipse()
        MyFileCollector = New ClassFileCollect()

        MyConfig = New ClassConfig()

        MyFileCollector.FilePath = MyConfig.SrcPath
        MyFileCollector.FileDesPath = MyConfig.DesPath

        If Directory.Exists(MyConfig.SrcPath) = False Then
            Directory.CreateDirectory(MyConfig.SrcPath)
        End If

        If Directory.Exists(MyConfig.DesPath) = False Then
            Directory.CreateDirectory(MyConfig.DesPath)
        End If

        TxtBitronCode.Text = MyConfig.BitronCode
        CmdPhaseID.Text = MyConfig.PhaseID
        Txt_Src_FileParh.Text = MyConfig.SrcPath
        Txt_Des_FileParh.Text = MyConfig.DesPath
        'Call ChangeFolder()

        Timer1.Enabled = True
    End Sub


    Public Class ClassConfig
        Public SrcPath As String
        Public DesPath As String
        Public PhaseID As String
        Public BitronCode As String
        Public Sub New()
            Dim t = System.IO.File.ReadAllLines("C:/config.txt")
            BitronCode = t(0).Split(";")(1)
            PhaseID = t(1).Split(";")(1)
            SrcPath = t(2).Split(";")(1)
            DesPath = t(3).Split(";")(1)
        End Sub
    End Class

    Private Sub ChangeFolder()
        SrcFile = Txt_Src_FileParh.Text
        DesFile = Txt_Des_FileParh.Text
        MyFileCollector.FilePath = SrcFile
        'MyFileCollector.FileDesPath = "E:\Report\TestClean"
        MyFileCollector.FileDesPath = DesFile
    End Sub

    Private Sub ButGetDBDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim PhaseID As String =
        Button2.BackColor = Color.Yellow
        Application.DoEvents()
        Dim Result As Boolean = MyMemSQLEclipse.ChangePhaseSetup("30415850", PhaseID)

        Button2.BackColor = IIf(Result, Color.Green, Color.Red)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim PhaseID As String = CmdPhaseID.Text
        Button2.BackColor = Color.Yellow
        Application.DoEvents()
        Dim Result As Boolean = MyMemSQLEclipse.ChangePhaseSetup(TxtBitronCode.Text, PhaseID)
        Button2.BackColor = IIf(Result, Color.Green, Color.Red)
    End Sub

    Private Sub Btn_StartCollect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_StartCollect.Click
        Timer1.Enabled = True
        StripStatus.Text = "Start Collecting"
        Btn_StartCollect.Enabled = False
        Btn_StopCollect.Enabled = True
    End Sub

    Private Sub Btn_StopCollect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_StopCollect.Click
        Timer1.Enabled = False
        StripStatus.Text = "Stop Collecting"
        Btn_StartCollect.Enabled = True
        Btn_StopCollect.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim PhaseID As String = CmdPhaseID.Text

        Dim UUT As Integer = 0
        Dim DataList As New List(Of String)
        DataList.Clear()

        Dim Matrix As String = "MR146570-83701700010001170920180000000438" 'TxtQueryTestRecord.Text

        DataList.Add(PhaseID) 'TestName
        DataList.Add("0") 'AssemblyFormID
        DataList.Add(TxtBitronCode.Text) 'IdParts

        DataList.Add(Matrix)
        DataList.Add("2020-03-01 10:14:00")
        DataList.Add("333")
        DataList.Add(UUT + 1)
        DataList.Add("LeakageFCT")
        'DataList.Add(DatiProd.Value(ProdOtherInfos.MachineName))
        DataList.Add("") 'SW
        DataList.Add("") 'HW

        DataList.Add("Item2")
        DataList.Add("995")
        DataList.Add(vbNullString)
        DataList.Add("-")
        DataList.Add("1") 'Whitch test item success
        DataList.Add("1") 'The finial test result

        Call MyMemSQLEclipse.StoreTestData(DataList)
    End Sub



    Public Function StoreAnalyseData(ByVal TestItem As ClassFileCollect.ClassTestItem, ByVal BitronCode As String, ByVal PhaseID As String) As Boolean
        Dim Res As Boolean = False
        Dim StationID As String = ""
        If TestItem.mStationID.Length > 10 Then                'Must less than 10
            StationID = TestItem.mStationID.ToString().Substring(0, 10)
        Else
        End If

        'Dim SW As New Stopwatch

        For Each tItem In TestItem.mItem
            'SW.Restart()
            If tItem Is TestItem.mItem.Last Then
                Res = StoreMesData(tItem, StationID, PhaseID, BitronCode, TestItem.mBenchSN, TestItem.mTestSocketIndex, TestItem.mUUTSN, tItem.TestDate, TestItem.mStationID, TestItem.mTestStatus, True)
            Else
                Res = StoreMesData(tItem, StationID, PhaseID, BitronCode, TestItem.mBenchSN, TestItem.mTestSocketIndex, TestItem.mUUTSN, tItem.TestDate, TestItem.mStationID, TestItem.mTestStatus, False)
            End If
            'TxtMsg.AppendText("Elipse Time:" & SW.ElapsedMilliseconds)
        Next
        Return Res
    End Function



    Public Function StoreMesData(ByVal TestItem As ClassFileCollect.CSubTestItem, ByVal StationID As String, ByVal PhaseID As String, ByVal BitronCode As String, ByVal BenchSN As String, ByVal UUTNo As String, _
        ByVal SN As String, ByVal TestDate As String, ByVal FCTFuncation As String, ByVal LastStatus As String, Optional ByVal LastItem As Boolean = False) As Boolean
        Dim DataList As New List(Of String)
        DataList.Clear()
        DataList.Add(PhaseID) 'TestName
        DataList.Add("0") 'AssemblyFormID
        DataList.Add(BitronCode) 'IdParts
        DataList.Add(SN)
        DataList.Add(TestDate) ''Dic(" ProdDate ") & " " & Dic("ProdTime")
        DataList.Add(BenchSN)
        DataList.Add(UUTNo)
        DataList.Add(StationID)
        DataList.Add("") 'SW
        DataList.Add("") 'HW

        DataList.Add((TestItem.TestSequence & ":" & TestItem.TestName).Trim)
        DataList.Add(TestItem.TestValue)
        DataList.Add(vbNullString)
        DataList.Add("-")
        If TestItem.TestResult = "Passed" Then
            DataList.Add("1") 'Whitch test item success
        Else
            DataList.Add("0") 'Whitch test item success
        End If

        If LastItem = True Then
            If LastStatus.Trim = "Pass" Then
                DataList.Add("1") 'The finial test result
            Else
                DataList.Add("0") 'The finial test result
            End If
        End If

        Return MyMemSQLEclipse.StoreTestData(DataList)
    End Function



    Private Sub Txt_Src_FileParh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Src_FileParh.Click
        Dim Dig As New FolderBrowserDialog
        If Dig.ShowDialog = DialogResult.OK Then
            Txt_Src_FileParh.Text = Dig.SelectedPath
            Call ChangeFolder()
        End If
    End Sub

    Private Sub Txt_Des_FileParh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Des_FileParh.Click
        Dim Dig As New FolderBrowserDialog
        If Dig.ShowDialog = DialogResult.OK Then
            Txt_Des_FileParh.Text = Dig.SelectedPath
            Call ChangeFolder()
        End If
    End Sub


    Public Sub fkill(ByVal pname As String)
        Dim pProcess As System.Diagnostics.Process
        For Each pProcess In System.Diagnostics.Process.GetProcesses()
            If pProcess.ProcessName.ToString().ToUpper = pname.ToUpper Then
                pProcess.Kill()
            End If
        Next
    End Sub
End Class
