'Imports BitronConfig
'Imports Bitron.DataTracer
'Imports Microsoft.VisualBasic.FileIO

'Public Class TESTFEASA
'    Dim tsTemp As TimeSpan
'    Dim dtMydate As DateTime
'    Public MyConf As New BitronConfig
'    Public MySys As New SystemOperate
'    Public NumSite As Integer
'    Public Numbay As Integer

'    Dim TBin As String


'    Dim ProgUso As String
'    Dim FailFlag As Integer


'    Public Structure Coordinate
'        Dim X1 As Double
'        Dim Y1 As Double
'        Dim X2 As Double
'        Dim Y2 As Double
'        Dim X3 As Double
'        Dim Y3 As Double
'        Dim X4 As Double
'        Dim Y4 As Double
'    End Structure

'    Public Enum CurTypes
'        CurTurn
'        CurTail
'        Tail_LED_chain
'    End Enum

'    Public Enum ComTypes
'        Feasa
'        Scanner
'    End Enum

'    'Dim DataHelper As New DataTraceHelper(PlantLocations.China)

'    'Public Function RunFeasaCheck(ByVal ProgUso As String, ByVal SN As String) As Integer
'    '    Dim Res As Boolean
'    '    'dtMydate = DateTime.Now '用于测试此部分运行时间



'    '    UseSiteRead(NumSite)
'    '    UseSiteWrite(NumSite)
'    '    UseBayRead(Numbay)

'    '    For T = 1 To 4
'    '        If Numbay = 1 And T = 1 Then NumSite = 1
'    '        If Numbay = 1 And T = 2 Then NumSite = 2
'    '        If Numbay = 2 And T = 3 Then NumSite = 3
'    '        If Numbay = 2 And T = 4 Then NumSite = 4
'    '        SyncParallelExec()
'    '    Next


'    '    FailFlag = PASS
'    '    Call MyConf.InitialAllInfo()

'    '    'Dim SN As String
'    '    'SN = Space$(20)
'    '    ''If SiteResultRead(NumSite) <> TEST_RESULT_PASS Then
'    '    ''    Return
'    '    ''End If

'    '    'SerialNumberRead(NumSite, SN)

'    '    'SN = "12919442706C20102860"

'    '    Dim GoldenSampleCode As String = ""
'    '    If My.Computer.FileSystem.FileExists(AppDomain.CurrentDomain.BaseDirectory & "MMAL\MatrixUUT" + NumSite.ToString + ".txt") = True Then
'    '        GoldenSampleCode = My.Computer.FileSystem.ReadAllText(AppDomain.CurrentDomain.BaseDirectory & "MMAL\MatrixUUT" + NumSite.ToString + ".txt")
'    '        GoldenSampleCode = GoldenSampleCode.Substring(0, 20)
'    '    End If

'    '    RunaTask(NumSite, "POWERON", "POWERON")

'    '    'MsgBox(GoldenSampleCode)


'    '    If GoldenSampleCode = SN Then ''Calibration
'    '        Call FeasaCalibration(NumSite, ProgUso, SN)
'    '        'If Res = True Then
'    '        '    FailFlag = PASS
'    '        'Else
'    '        '    FailFlag = FAIL
'    '        'End If
'    '    Else
'    '        FeasaValidation(NumSite, ProgUso, SN)
'    '    End If


'    '    RunaTask(NumSite, "POWEROFF", "POWEROFF")

'    '    Return FailFlag

'    '    'Return FailFlag
'    '    'tsTemp = dtMydate.Subtract(DateTime.Now)
'    'End Function


'    Public Sub RunaTask(ByVal Site As String, ByVal LabelStart As String, ByVal LabelEnd As String)
'        Dim Meas As Integer
'        UseSiteWrite(Site)

'        Meas = RunAnlTaskLabel("", LabelStart, LabelEnd)
'        If Meas = 0 Then
'            MsgPrintLog("Runa: " + LabelStart + " Board:" + Site.ToString + " Pass", 0)
'        Else
'            MsgPrintLog("Runa: " + LabelStart + " Board:" + Site.ToString + " FAIL", 0)
'            MsgBox("Execute analogue task : " + LabelStart + " Error")
'        End If
'    End Sub

'    Public Class ClsFeasa
'        Dim Res As Boolean = False
'        Public Hue() As Double
'        Public Stu() As Integer
'        Public Ity() As Integer
'        Public AbsInt() As Double
'        Public Wav() As Integer
'        Public Cx() As Double
'        Public Cy() As Double
'        Dim TempFeasa As String = ""

'        Public Function FeasaOpen(Optional ByVal SiteNo As Integer = 0) As Boolean
'            Return SystemOperate.Device_FEASA.OpenCom(SiteNo)
'        End Function

'        Public Function FeasaClose(Optional ByVal SiteNo As Integer = 0) As Boolean
'            Return SystemOperate.Device_FEASA.OpenCom(SiteNo)
'        End Function

'        Public Function FeasaChange(ByVal FeasaName As String) As Boolean
'            Return SystemOperate.Device_FEASA.BusgetFeasa(FeasaName)
'        End Function

'        Public Function FeasaCaptureLevel(Optional ByVal brightlevel As ClassFEASA.EBrightLevel = ClassFEASA.EBrightLevel.Auto) As Boolean
'            Return SystemOperate.Device_FEASA.Capture(brightlevel)
'        End Function

'        Public Function FeasaGetAllHSI(ByVal FeasaChNum As Integer) As Boolean
'            Return SystemOperate.Device_FEASA.GetHSIAll(FeasaChNum, Hue, Stu, Ity, ClassFEASA.EBrightLevel.Auto)
'        End Function

'        Public Function FeasaGetAllIntensity(ByVal FeasaChNum As Integer) As Boolean
'            Return SystemOperate.Device_FEASA.GetAbsoluIntensity(FeasaChNum, AbsInt)
'        End Function

'        Public Function FeasaGetAllWavelength(ByVal FeasaChNum As Integer) As Boolean
'            Return SystemOperate.Device_FEASA.GetWavlength(FeasaChNum, Wav)
'        End Function

'        Public Function FeasaGetAllCxCy(ByVal FeasaChNum As Integer) As Boolean
'            Return SystemOperate.Device_FEASA.GetCxCy(FeasaChNum, Cx, Cy)
'        End Function

'        Public Function FeasaCalibrationWav(ByVal FeasaName As String, ByVal Ch As Integer, ByVal ValiWave As Double)
'            If TempFeasa <> FeasaName Then '如果前后两通道不为同一个Feasa,则更改feasa
'                TempFeasa = FeasaName
'                FeasaChange(FeasaName)
'            End If
'            Return SystemOperate.Device_FEASA.CalibrationWavelength(Ch, ValiWave)
'        End Function

'        Public Function FeasaCalibrationAbsIntensity(ByVal FeasaName As String, ByVal Ch As Integer, ByVal ValiAbsint As Double)
'            If TempFeasa <> FeasaName Then '如果前后两通道不为同一个Feasa,则更改feasa
'                TempFeasa = FeasaName
'                FeasaChange(FeasaName)
'            End If
'            Return SystemOperate.Device_FEASA.CalibrationAbsIntensity(Ch, ValiAbsint)
'        End Function

'        Public Function FeasaCalibrationCxCy(ByVal FeasaName As String, ByVal Ch As Integer, ByVal ValiCxCy As ClassFEASA.CxCy)
'            If TempFeasa <> FeasaName Then '如果前后两通道不为同一个Feasa,则更改feasa
'                TempFeasa = FeasaName
'                FeasaChange(FeasaName)
'            End If
'            Return SystemOperate.Device_FEASA.CalibrationCxCy(Ch, ValiCxCy)
'        End Function
'    End Class

'    'Private Function FeasaCalibration(ByVal SiteNum As Integer, ByVal Code_Bitron As String, ByVal SN As String) As Integer
'    '    Dim FileName As String = AppDomain.CurrentDomain.BaseDirectory + "FeasaCalibration.CSV"
'    '    'Dim FileName1 As String = "D:\Bitron Project\MMAL\NewFrameWorkMMAL\New Feasa Frame\BitonConfigTest\BitonConfigTest\bin\Debug\FeasaCalibration.CSV"
'    '    Dim delimiter As String = ","
'    '    Dim fields As String()
'    '    Dim Res As Boolean = False
'    '    Call MySys.LoadAllHardwares()
'    '    Dim Scom As String = ""

'    '    Res = SystemOperate.Device_FEASA.GetSerial(Scom)

'    '    Dim FeasaIndex As New Hashtable()

'    '    Dim LedMeasure As New Hashtable()


'    '    Using parser As New TextFieldParser(FileName)
'    '        parser.SetDelimiters(delimiter)
'    '        While Not parser.EndOfData
'    '            fields = parser.ReadFields()
'    '            LedMeasure.Add(fields(0), fields)
'    '        End While
'    '    End Using

'    '    Dim FeasaName As List(Of String) = GetFeasaCount(SiteNum, Code_Bitron)
'    '    Dim ListFeasa As List(Of ClsFeasa) = New List(Of ClsFeasa)

'    '    Dim Count As Integer = 0

'    '    For Each fs In FeasaName
'    '        Dim NameFeasa As String = ""
'    '        Dim ChFeasa As Integer = 0
'    '        GetFeasaByFeasaName(fs, NameFeasa, ChFeasa)
'    '        Dim CLF As New ClsFeasa
'    '        Res = CLF.FeasaChange(NameFeasa)
'    '        CLF.FeasaGetAllHSI(ChFeasa)
'    '        CLF.FeasaGetAllIntensity(ChFeasa)
'    '        CLF.FeasaGetAllWavelength(ChFeasa)
'    '        CLF.FeasaGetAllCxCy(ChFeasa)
'    '        ListFeasa.Add(CLF)
'    '        FeasaIndex.Add(Count, fs)
'    '        Dim T As Integer = FeasaIndex(fs)
'    '        Count += 1
'    '    Next

'    '    Dim TempFeasaCls As New BitronConfig.CFeasa
'    '    Dim TempRegisterVF As Double = 0

'    '    'Dim TraxResultList As New List(Of FujiTraxData)
'    '    If MyConf.GENERAL.GetBinCodeFromFujiTrax = "1" Then
'    '        'TraxResultList = DataHelper.GetFuJiTraxDatas(SN)
'    '    Else
'    '        'Do nothing
'    '    End If

'    '    'tsTemp = dtMydate.Subtract(DateTime.Now)

'    '    Dim TempFeasa As String = ""

'    '    Dim CaliTempCxCy As ClassFEASA.CxCy

'    '    For Each t In MyConf.VERSIONIS 'Right or Left
'    '        If t.CodeBitron = Code_Bitron Then
'    '            For Each i In t.Sites
'    '                If i.SiteNo = SiteNum Then
'    '                    For Each j In i.SITE
'    '                        TempFeasaCls.BinName = j.BinName
'    '                        TempFeasaCls.Name = j.Name
'    '                        TempFeasaCls.ID = j.ID
'    '                        TempFeasaCls.CH = j.CH
'    '                        TempFeasaCls.LEDName = j.LEDName
'    '                        TempFeasaCls.Code = j.Code
'    '                        TempFeasaCls.HueOffset = j.HueOffset
'    '                        TempFeasaCls.SatOffset = j.SatOffset
'    '                        TempFeasaCls.IntensityOffset = j.IntensityOffset
'    '                        TempFeasaCls.CxOffset = j.CxOffset
'    '                        TempFeasaCls.CyOffset = j.CyOffset
'    '                        TempFeasaCls.IntensityOffset = j.IntensityOffset
'    '                        TempFeasaCls.DominantWavOffset = j.DominantWavOffset
'    '                        TempFeasaCls.VFRegister = j.VFRegister
'    '                        'HoldRegisterRead(TempFeasaCls.VFRegister,TempRegisterVF,"")
'    '                        TempFeasaCls.VFOffset = j.VFOffset

'    '                        Dim Key As ICollection = FeasaIndex.Keys
'    '                        For Each k In Key
'    '                            If FeasaIndex(k) = j.Name Then
'    '                                If MyConf.GENERAL.GetBinCodeFromFujiTrax = "0" Then 'Do not get LED Bincode and Material Number from FujiTrax
'    '                                    ''ValidateAbsIntensity(TempFeasaCls.Name, SiteNum, TempFeasaCls.BinName, TempFeasaCls.Code, ListFeasa(k).AbsInt(TempFeasaCls.CH - 1), TempFeasaCls.IntensityOffset)
'    '                                ElseIf MyConf.GENERAL.GetBinCodeFromFujiTrax = "1" Then 'Get LED Bincode and Material Number from FujiTrax
'    '                                    Dim XX = From C In TraxResultList Where C.LEDName = "LED" & j.LEDName.Replace("DL", "") Select C
'    '                                    If XX.Count > 0 Then
'    '                                        '校准绝对亮度
'    '                                        ListFeasa(k).FeasaCalibrationAbsIntensity(MyConf.GENERAL.Name_Feasa(j.Name.Substring(5, 1)).Name, TempFeasaCls.CH, CType(LedMeasure(j.LEDName)(3), Double))

'    '                                        '校准波长
'    '                                        ListFeasa(k).FeasaCalibrationWav(MyConf.GENERAL.Name_Feasa(j.Name.Substring(5, 1)).Name, TempFeasaCls.CH, CType(LedMeasure(j.LEDName)(4), Double))

'    '                                        'ValidateAbsIntensity(TempFeasaCls.Name & "_" & XX(0).LEDName, SiteNum, XX(0).BinCode, XX(0).PartNo, ListFeasa(k).AbsInt(TempFeasaCls.CH - 1), TempFeasaCls.IntensityOffset)
'    '                                        'ValidateWavelength(TempFeasaCls.Name & "_" & XX(0).LEDName, SiteNum, XX(0).BinCode, XX(0).PartNo, ListFeasa(k).Wav(TempFeasaCls.CH - 1), TempFeasaCls.DominantWavOffset)

'    '                                    End If
'    '                                End If


'    '                                '设计思路为查找当前LED所用Feasa的名字，是否与前一名字相同，不相同更改当前Feasa。同时传入的参数有当前所用参数的CH,FEASA名称，查找校验文件，更改当前通道Feasa偏差。

'    '                                ''校准绝对亮度
'    '                                'ListFeasa(k).FeasaCalibrationAbsIntensity(MyConf.GENERAL.Name_Feasa(j.Name.Substring(5, 1)).Name, TempFeasaCls.CH, CType(LedMeasure(TempFeasaCls.LEDName)(4), Double))

'    '                                ''校准CxCy
'    '                                'CaliTempCxCy.Cx = CType(LedMeasure(TempFeasaCls.LEDName)(1), Double)
'    '                                'CaliTempCxCy.Cy = CType(LedMeasure(TempFeasaCls.LEDName)(2), Double)
'    '                                'ListFeasa(k).FeasaCalibrationCxCy(MyConf.GENERAL.Name_Feasa(j.Name.Substring(5, 1)).Name, TempFeasaCls.CH, CaliTempCxCy)

'    '                                ''校准波长
'    '                                'ListFeasa(k).FeasaCalibrationWav(MyConf.GENERAL.Name_Feasa(j.Name.Substring(5, 1)).Name, TempFeasaCls.CH, CType(LedMeasure(TempFeasaCls.LEDName)(5), Double))

'    '                            End If
'    '                        Next
'    '                    Next
'    '                End If
'    '            Next
'    '        End If
'    '    Next
'    '    Return FailFlag
'    '    'SystemOperate.Device_FEASA.CloseCom()
'    'End Function

'    Private Function FeasaValidation(ByVal SiteNum As Integer, ByVal Code_Bitron As String, ByVal SN As String) As Integer

'        If SiteResultRead(SiteNum) = TEST_RESULT_PASS Then

'            Dim Res As Boolean = False
'            Call MySys.LoadAllHardwares()
'            Dim Scom As String = ""
'            Res = SystemOperate.Device_FEASA.GetSerial(Scom)
'            Dim FeasaIndex As New Hashtable()
'            Dim FeasaName As List(Of String) = GetFeasaCount(SiteNum, Code_Bitron)
'            Dim ListFeasa As List(Of ClsFeasa) = New List(Of ClsFeasa)

'            '一次捕捉所有用到的Feasa通道的H.S.I，Intensity，CxCy
'            Dim Count As Integer = 0
'            Try
'                For Each fs In FeasaName
'                    Dim NameFeasa As String = ""
'                    Dim ChFeasa As Integer = 0
'                    GetFeasaByFeasaName(fs, NameFeasa, ChFeasa)
'                    Dim CLF As New ClsFeasa
'                    Res = CLF.FeasaChange(NameFeasa)
'                    CLF.FeasaGetAllHSI(ChFeasa)
'                    CLF.FeasaGetAllIntensity(ChFeasa)
'                    CLF.FeasaGetAllWavelength(ChFeasa)
'                    'CLF.FeasaGetAllCxCy(ChFeasa)
'                    ListFeasa.Add(CLF)
'                    FeasaIndex.Add(Count, fs)
'                    Dim T As Integer = FeasaIndex(fs)
'                    Count += 1
'                Next
'            Catch ex As Exception
'                MsgBox("Feasa Capture Error! " & ex.ToString)
'            End Try


'            Dim TempFeasaCls As New BitronConfig.CFeasa
'            Dim TempRegisterVF As Double = 0

'            Dim TraxResultList As New List(Of FujiTraxData)
'            If MyConf.GENERAL.GetBinCodeFromFujiTrax = "1" Then
'                Try
'                    TraxResultList = DataHelper.GetFuJiTraxDatas(SN)
'                Catch
'                    MsgBox("Can't get information from FujiTrax!")
'                End Try
'            Else
'                'Do nothing
'            End If

'            'tsTemp = dtMydate.Subtract(DateTime.Now)

'            For Each t In MyConf.VERSIONIS 'Right or Left
'                If t.CodeBitron = Code_Bitron Then
'                    For Each i In t.Sites
'                        If i.SiteNo = SiteNum Then
'                            For Each j In i.SITE
'                                TempFeasaCls.BinName = j.BinName
'                                TempFeasaCls.Name = j.Name
'                                TempFeasaCls.ID = j.ID
'                                TempFeasaCls.CH = j.CH
'                                TempFeasaCls.LEDName = j.LEDName
'                                TempFeasaCls.Code = j.Code
'                                TempFeasaCls.HueOffset = j.HueOffset
'                                TempFeasaCls.SatOffset = j.SatOffset
'                                TempFeasaCls.IntensityOffset = j.IntensityOffset
'                                TempFeasaCls.CxOffset = j.CxOffset
'                                TempFeasaCls.CyOffset = j.CyOffset
'                                TempFeasaCls.IntensityOffset = j.IntensityOffset
'                                TempFeasaCls.DominantWavOffset = j.DominantWavOffset
'                                TempFeasaCls.VFRegister = j.VFRegister

'                                HoldRegisterRead(TempFeasaCls.VFRegister, TempRegisterVF, "")

'                                TempFeasaCls.VFOffset = j.VFOffset

'                                Dim Key As ICollection = FeasaIndex.Keys
'                                For Each k In Key
'                                    If FeasaIndex(k) = j.Name Then
'                                        If MyConf.GENERAL.GetBinCodeFromFujiTrax = "0" Then 'Do not get LED Bincode and Material Number from FujiTrax
'                                            ValidateHSI(TempFeasaCls.Name, SiteNum, TempFeasaCls.BinName, TempFeasaCls.Code, ListFeasa(k).Hue(TempFeasaCls.CH - 1), ListFeasa(k).Stu(TempFeasaCls.CH - 1), ListFeasa(k).Ity(TempFeasaCls.CH - 1), TempFeasaCls.HueOffset, TempFeasaCls.SatOffset, TempFeasaCls.IntensityOffset)
'                                            'ValidateCXCY(TempFeasaCls.Name, SiteNum, TempFeasaCls.BinName, TempFeasaCls.Code, ListFeasa(k).CxCy(TempFeasaCls.CH - 1), TempFeasaCls.CxOffset, TempFeasaCls.CyOffset)
'                                            ValidateAbsIntensity(TempFeasaCls.Name, SiteNum, TempFeasaCls.BinName, TempFeasaCls.Code, ListFeasa(k).AbsInt(TempFeasaCls.CH - 1), TempFeasaCls.IntensityOffset)
'                                            ''sValidateWavelength(TempFeasaCls.Name & "_" & XX(0).LEDName, SiteNum, XX(0).BinCode, XX(0).PartNo, ListFeasa(k).Wav(TempFeasaCls.CH - 1), TempFeasaCls.DominantWavOffset)
'                                            ValidateVF(TempFeasaCls.Name, SiteNum, TempFeasaCls.BinName, TempFeasaCls.Code, TempRegisterVF, TempFeasaCls.VFOffset)
'                                        ElseIf MyConf.GENERAL.GetBinCodeFromFujiTrax = "1" Then 'Get LED Bincode and Material Number from FujiTrax
'                                            Dim XX = From C In TraxResultList Where C.LEDName = "LED" & j.LEDName.Replace("DL", "") Select C
'                                            If XX.Count > 0 Then
'                                                ValidateHSI(TempFeasaCls.Name & "_" & XX(0).LEDName, SiteNum, XX(0).BinCode, XX(0).PartNo, ListFeasa(k).Hue(TempFeasaCls.CH - 1), ListFeasa(k).Stu(TempFeasaCls.CH - 1), ListFeasa(k).AbsInt(TempFeasaCls.CH - 1), TempFeasaCls.HueOffset, TempFeasaCls.SatOffset, TempFeasaCls.IntensityOffset)
'                                                ''ValidateCXCY(TempFeasaCls.Name, SiteNum, XX(0).BinCode, XX(0).PartNo, ListFeasa(k).CxCy(TempFeasaCls.CH - 1), TempFeasaCls.CxOffset, TempFeasaCls.CyOffset)
'                                                ValidateAbsIntensity(TempFeasaCls.Name & "_" & XX(0).LEDName, SiteNum, XX(0).BinCode, XX(0).PartNo, ListFeasa(k).AbsInt(TempFeasaCls.CH - 1), TempFeasaCls.IntensityOffset)
'                                                ValidateWavelength(TempFeasaCls.Name & "_" & XX(0).LEDName, SiteNum, XX(0).BinCode, XX(0).PartNo, ListFeasa(k).Wav(TempFeasaCls.CH - 1), TempFeasaCls.DominantWavOffset)
'                                                '' ValidateVF(TempFeasaCls.Name, SiteNum, XX(0).BinCode, XX(0).PartNo, TempRegisterVF, TempFeasaCls.VFOffset)
'                                            End If
'                                        End If
'                                    End If
'                                Next
'                            Next
'                        End If
'                    Next
'                End If
'            Next

'        End If

'        'SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
'        'Return FailFlag

'        'If FailFlag = FAIL Then
'        '    If NumSite = 1 Then
'        '        MsgPrintLog("FUCTIONAL TEST FAIL...UUT1", 0)
'        '        RunAnlTaskLabel("", "LEDFL1", "")
'        '        Shell("C:\Bitron\SPEAMSMQ.exe FAIL:1")
'        '    ElseIf NumSite = 2 Then
'        '        MsgPrintLog("FUCTIONAL TEST FAIL...UUT2", 0)
'        '        RunAnlTaskLabel("", "LEDFL2", "")
'        '        Shell("C:\Bitron\SPEAMSMQ.exe FAIL:2")
'        '    ElseIf NumSite = 3 Then
'        '        MsgPrintLog("FUCTIONAL TEST FAIL...UUT3", 0)
'        '        RunAnlTaskLabel("", "LEDFL3", "")
'        '        Shell("C:\Bitron\SPEAMSMQ.exe FAIL:3")
'        '    ElseIf NumSite = 4 Then
'        '        MsgPrintLog("FUCTIONAL TEST FAIL...UUT4", 0)
'        '        RunAnlTaskLabel("", "LEDFL4", "")
'        '        Shell("C:\Bitron\SPEAMSMQ.exe FAIL:4")
'        '    End If
'        'Else
'        '    If NumSite = 1 Then
'        '        MsgPrintLog("FUCTIONAL TEST PASS...UUT1", 0)
'        '    ElseIf NumSite = 2 Then
'        '        MsgPrintLog("FUCTIONAL TEST PASS...UUT2", 0)
'        '    ElseIf NumSite = 3 Then
'        '        MsgPrintLog("FUCTIONAL TEST PASS...UUT3", 0)
'        '    ElseIf NumSite = 4 Then
'        '        MsgPrintLog("FUCTIONAL TEST PASS...UUT4", 0)
'        '    End If
'        'End If

'        'TplanResultSet(FailFlag)

'    End Function

'    Public Sub TurnTest()
'        Dim Result As Boolean = False
'        Dim Temp As Double = 0
'        RunAnlTaskLabel("", "PWON", "CUR_TU_T")
'        DvmRead(DVM1, Temp, MU_CURR)
'        CheckCur(CurTypes.CurTurn, Temp)
'    End Sub

'    Public Sub CheckCur(ByVal CurType As CurTypes, ByVal RealCur As Double)
'        Dim cxml As New XmlRistor(AppDomain.CurrentDomain.BaseDirectory & "\ParaConfig.xml")
'        Dim LowLimit As Single = 0
'        Dim HighLimit As Single = 0
'        Dim BinCode As String = String.Empty
'        Select Case CurType
'            Case CurTypes.CurTurn
'                'BinCode = cxml.GT_BinTurn 'Get From SQL
'                BinCode = TBin
'                LowLimit = cxml.GT_BinTu_L(BinCode)
'                HighLimit = cxml.GT_BinTu_H(BinCode)
'            Case CurTypes.Tail_LED_chain
'                'BinCode = cxml.GT_BinTAIL 'Get From SQL
'                BinCode = TBin
'                LowLimit = cxml.GT_BinTa_L(BinCode)
'                HighLimit = cxml.GT_BinTa_H(BinCode)
'        End Select
'        If RealCur * 1000 > HighLimit Or RealCur * 1000 < LowLimit Then
'            MsgPrintLog("FCT -  " & CurType.ToString & " " & (RealCur * 1000).ToString + "mA UUT: " + NumSite.ToString + " FAIL", 0)
'            MsgDispService(CurType.ToString & " Max: " + HighLimit.ToString + "  / Min: " + LowLimit.ToString, 0)
'            MsgDispService(Chr(13), 0)
'            MsgDispService("FCT -  " & CurType.ToString & "(" & BinCode & ")" & "-" & (RealCur * 1000).ToString + "mA UUT: " + NumSite.ToString + " FAIL", 0)
'            MsgDispService(Chr(13), 0)
'            ObsDatalogTest(1, CurType.ToString, " ", FAIL, (RealCur * 1000).ToString & " mA UUT:" & NumSite.ToString, LowLimit, HighLimit, "0,0", NumSite)
'            SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
'        Else
'            MsgPrintLog("FCT - " & CurType.ToString & " " & (RealCur * 1000).ToString + "mA UUT: " + NumSite.ToString + " PASS", 0)
'            MsgDispService(CurType.ToString & " Max: " + HighLimit.ToString + "  / Min: " + LowLimit.ToString, 0)
'            MsgDispService(Chr(13), 0)
'            ObsDatalogTest(1, CurType.ToString, " ", PASS, (RealCur * 1000).ToString & " mA UUT:" & NumSite.ToString, LowLimit, HighLimit, "0,0", NumSite)
'        End If
'    End Sub

'    Public Function ValidateHSI(ByVal LEDName As String, ByVal NumSite As String, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetHSI_H As Double, ByVal FeasaGetHSI_S As Integer, ByVal FeasaGetHSI_I As Int32, Optional ByVal h_Offset As Double = 0, Optional ByVal s_Offset As Integer = 0, Optional ByVal i_Offset As Integer = 0) As Boolean
'        If SiteResultRead(NumSite) = TEST_RESULT_PASS Then
'            Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
'            Dim TeminalHSI_H As Double = FeasaGetHSI_H + h_Offset
'            Dim TeminalHSI_S As Integer = FeasaGetHSI_S + s_Offset
'            Dim TeminalHSI_I As Int32 = FeasaGetHSI_I + i_Offset
'            If xx.Count > 0 Then
'                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
'                If t.Count > 0 Then
'                    If t(0).Hue_SH >= TeminalHSI_H And t(0).Hue_SL <= TeminalHSI_H Then
'                        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_H, t(0).Hue_SH, t(0).Hue_SL, "_HUE")
'                    Else
'                        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_H, t(0).Hue_SH, t(0).Hue_SL, "_HUE")
'                    End If

'                    If t(0).Sat_SH >= TeminalHSI_S And t(0).Sat_SL <= TeminalHSI_S Then
'                        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_S, t(0).Sat_SH, t(0).Sat_SL, "_SAT")
'                    Else
'                        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_S, t(0).Sat_SH, t(0).Sat_SL, "_SAT")
'                    End If

'                    If t(0).Intensity_SH >= TeminalHSI_I And t(0).Intensity_SL <= TeminalHSI_I Then
'                        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_I, t(0).Intensity_SH, t(0).Intensity_SL, "_INTENSITY")
'                    Else
'                        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_I, t(0).Intensity_SH, t(0).Intensity_SL, "_INTENSITY")
'                    End If
'                Else
'                    MsgBox("Not Fund " & BinName & "or " & Code & "in Config!")
'                End If
'            Else
'                MsgBox("HSI Config Error!")
'            End If
'        End If
'    End Function

'    Private Sub PrintResultMsg(ByVal TestFlag As Boolean, ByVal NumSite As Integer, ByVal LEDName As String, ByVal Value As Object, ByVal Hi_Limit As Object, ByVal Lo_Limit As Object, ByVal TestItem As String)
'        If TestFlag = PASS Then
'            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " PASS", 0)
'            ObsDatalogTest(1, LEDName, TestItem, PASS, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
'        Else
'            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " FAIL", 0)
'            PrintRemarkFail(1, LEDName + TestItem, "0 0", Value.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
'            ObsDatalogTest(1, LEDName, TestItem, FAIL, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
'            SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
'        End If
'    End Sub

'    Private Sub PrintResultMsg(ByVal TestFlag As Boolean, ByVal NumSite As Integer, ByVal LEDName As String, ByVal Value_Cx As Object, ByVal Value_Cy As Object, ByVal Hi_Limit As Object, ByVal Lo_Limit As Object, ByVal TestItem As String)
'        If TestFlag = PASS Then
'            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " PASS", 0)
'            ObsDatalogTest(1, LEDName, TestItem, PASS, Value_Cx.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
'            ObsDatalogTest(1, LEDName, TestItem, PASS, Value_Cy.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
'        Else
'            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " FAIL", 0)
'            PrintRemarkFail(1, LEDName + "_Cx", "0 0", Value_Cx.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
'            PrintRemarkFail(1, LEDName + "_Cy", "0 0", Value_Cy.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
'            ObsDatalogTest(1, LEDName, "_Cx", FAIL, Value_Cx.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
'            ObsDatalogTest(1, LEDName, "_Cy", FAIL, Value_Cy.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
'            SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
'        End If
'    End Sub

'    Public Sub PrintRemarkFail(ByVal NumTest As Integer, ByVal Remark As String, ByVal TpList As String, ByVal Valore As String, ByVal Sh_Max As String, ByVal Sh_Min As String, ByVal Unit As String, ByVal site As Integer)
'        MsgDispService("FVT TEST#" + NumTest.ToString + "      FAIL (-)", 0)
'        MsgDispService(Remark + " /" + site.ToString, 0)
'        MsgDispService("TP: " + TpList, 0)
'        MsgDispService("VALUE: " + Valore.ToString + " " + Unit, 0)
'        MsgDispService("Max: " + Sh_Max.ToString + Unit + " / Min: " + Sh_Min.ToString + Unit, 0)
'        MsgDispService(Chr(13), 0)
'    End Sub

'    Public Function ValidateAbsIntensity(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetAbsint As Double, Optional ByVal Offset_Abs As Double = 0) As Boolean
'        If SiteResultRead(NumSite) = TEST_RESULT_PASS Then
'            Dim TerminalAbs As Double = FeasaGetAbsint + Offset_Abs
'            Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
'            If xx.Count > 0 Then
'                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
'                If t(0).Intensity_SH >= TerminalAbs And t(0).Intensity_SL <= TerminalAbs Then
'                    PrintResultMsg(PASS, NumSite, LEDName, TerminalAbs, t(0).Intensity_SH, t(0).Intensity_SL, "_AbsIntensity")
'                Else
'                    PrintResultMsg(FAIL, NumSite, LEDName, TerminalAbs, t(0).Intensity_SH, t(0).Intensity_SL, "_AbsIntensity")
'                End If
'            Else
'                MsgBox("HSI Config Error!")
'            End If
'        End If
'    End Function

'    Public Function ValidateWavelength(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetWav As Double, Optional ByVal Offset_Wav As Double = 0) As Boolean
'        Dim TerminalWav As Double = FeasaGetWav + Offset_Wav
'        If FailFlag = FAIL Then Return False
'        Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
'        If xx.Count > 0 Then
'            Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
'            If t(0).DominantWav_SH >= TerminalWav And t(0).DominantWav_SL <= TerminalWav Then
'                PrintResultMsg(PASS, NumSite, LEDName, TerminalWav, t(0).DominantWav_SH, t(0).DominantWav_SL, "_DominantWave")
'            Else
'                PrintResultMsg(FAIL, NumSite, LEDName, TerminalWav, t(0).DominantWav_SH, t(0).DominantWav_SL, "_DominantWave")
'            End If
'        Else
'            MsgBox("HSI Config Error!")
'        End If
'    End Function

'    Public Function ValidateCXCY(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetCxCy As ClassFEASA.CxCy, Optional ByVal Offset_Cx As Double = 0, Optional ByVal Offset_Cy As Double = 0) As Boolean
'        Dim Res As Boolean = False
'        Dim coor As Coordinate

'        Dim TeminalCx As Double = FeasaGetCxCy.Cx + Offset_Cx
'        Dim TeminalCy As Double = FeasaGetCxCy.Cy + Offset_Cy

'        Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
'        If xx.Count > 0 Then
'            Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D

'            If t.Count > 0 Then
'                coor.X1 = t(0).CXCYRange(0).CX
'                coor.Y1 = t(0).CXCYRange(0).CY
'                coor.X2 = t(0).CXCYRange(1).CX
'                coor.Y2 = t(0).CXCYRange(1).CY
'                coor.X3 = t(0).CXCYRange(2).CX
'                coor.Y3 = t(0).CXCYRange(2).CY
'                coor.X4 = t(0).CXCYRange(3).CX
'                coor.Y4 = t(0).CXCYRange(3).CY
'                Res = CheckCxCyInArea(coor, TeminalCx, TeminalCy)
'            Else
'                MsgBox("CxCy Config Error!")
'            End If
'        End If

'        If Res Then
'            PrintResultMsg(PASS, NumSite, LEDName, FormatFeasaCxCy(TeminalCx), FormatFeasaCxCy(TeminalCy), "-", "-", "_Cxcy")
'        Else
'            PrintResultMsg(FAIL, NumSite, LEDName, FormatFeasaCxCy(TeminalCx), FormatFeasaCxCy(TeminalCy), "-", "-", "_Cxcy")
'        End If
'    End Function

'    Public Function FormatFeasaCxCy(ByVal Val As Double) As String
'        Return Val.ToString("0.0000")
'    End Function

'    Public Function CheckCxCyInArea(ByVal Coordinate As Coordinate, ByVal XLed As Double, ByVal YLed As Double) As Boolean
'        Dim Slope As Double         'Slope Line XY
'        Dim SlopeLed As Double      'Slope Line Led Bin
'        Dim XQInt As Double         'Q X-Intercept (to use for vertical line)
'        Dim XQIntLed As Double      'Q X-Intercept Led Bin (to use for vertical line)
'        Dim YQInt As Double         'Q Y-Intercept (to use for horizontal line)
'        Dim YQIntLed As Double      'Q Y-Intercept Led Bin (to use for horizontal line)
'        Dim BinLineUP As Boolean
'        Dim BinLineDown As Boolean
'        Dim BinLineRight As Boolean
'        Dim BinLineLeft As Boolean
'        Dim BINResult As Boolean
'        BinLineUP = False
'        BinLineDown = False
'        BinLineRight = False
'        BinLineLeft = False
'        BINResult = False

'        XLed = XLed + 0.00000001    'to avoid DIV/0
'        YLed = YLed + 0.00000001    'to avoid DIV/0

'        ' Calculate slope for Horizontal line:          (coordinate.y1-coordinate.y2) / (coordinate.x1-coordinate.x2)

'        ' Calculate slope for Vertical line:            (coordinate.x1-coordinate.x2) / (coordinate.y1-coordinate.y2)

'        ' Calculate QY-Intercept for Horizontal Line :  coordinate.y1 - ((coordinate.y1-coordinate.y2)/(coordinate.x1-coordinate.x2)*coordinate.x1)

'        ' Calculate QX-Intercept for Vrtical Line :     coordinate.x1 - ((coordinate.x1-coordinate.x2)/(coordinate.y1-coordinate.y2)*coordinate.y1)

'        Try

'            '=========================================================================================================

'            'Calculate Horizontal Slope and Y_Intercept line coordinate.x1,coordinate.y1  to coordinate.x2,coordinate.x2   (Upper LIne)



'            Slope = (Coordinate.Y1 - Coordinate.Y2) / (Coordinate.X1 - Coordinate.X2)                       'Calculate Slope line coordinate.x1,coordinate.y1 to coordinate.x2,coordinate.y2

'            SlopeLed = (Coordinate.Y1 - YLed) / (Coordinate.X1 - XLed)                'Calculate Slope line coordinate.x1,coordinate.y1 to XY Led to check

'            YQInt = Coordinate.Y1 - ((Coordinate.Y1 - Coordinate.Y2) / (Coordinate.X1 - Coordinate.X2) * Coordinate.X1)           'Calculate Y-Intercept line coordinate.x1,coordinate.y1 to coordinate.x2,coordinate.y2

'            YQIntLed = Coordinate.Y1 - ((Coordinate.Y1 - YLed) / (Coordinate.X1 - XLed) * Coordinate.X1)    'Calculate Y-Intercept line coordinate.x1,coordinate.y1 to XY Led to check



'            'IMPORTANT:

'            ' Check if XY Led to check is Down to Upper horizontal line :

'            ' Position XY ok :  IF (XLed > coordinate.x1) then  SlopeLed < Slope &&  YQIntLed > YQInt

'            '   Else    SlopeLed > Slope &&  YQIntLed < YQInt



'            If XLed > Coordinate.X1 Then

'                If (YQIntLed > YQInt) And (SlopeLed < Slope) Then BinLineUP = True

'            Else

'                If (YQIntLed < YQInt) And (SlopeLed > Slope) Then BinLineUP = True

'            End If
'            '=========================================================================================================

'            '=========================================================================================================

'            'Calculate Vertical Slope and Y_Intercept  line coordinate.X4,coordinate.Y4  to coordinate.X3,coordinate.X3   (Lower LIne)

'            Slope = (Coordinate.Y4 - Coordinate.Y3) / (Coordinate.X4 - Coordinate.X3)                       'Calculate Slope line coordinate.X4,coordinate.Y4 to coordinate.X3,coordinate.Y3

'            SlopeLed = (Coordinate.Y4 - YLed) / (Coordinate.X4 - XLed)                'Calculate Slope line coordinate.X4,coordinate.Y4 to XY Led to check

'            YQInt = Coordinate.Y4 - ((Coordinate.Y4 - Coordinate.Y3) / (Coordinate.X4 - Coordinate.X3) * Coordinate.X4)           'Calculate Y-Intercept line coordinate.X4,coordinate.Y4 to coordinate.X3,coordinate.Y3

'            YQIntLed = Coordinate.Y4 - ((Coordinate.Y4 - YLed) / (Coordinate.X4 - XLed) * Coordinate.X4)    'Calculate Y-Intercept line coordinate.X4,coordinate.Y4 to XY Led to check

'            'IMPORTANT:

'            ' Check if XY Led to check is Up to L0wer horizontal line :

'            ' Position XY ok :  IF (XLed > coordinate.X4) then  SlopeLed < Slope &&  YQIntLed < YQInt

'            '   Else    SlopeLed < Slope &&  YQIntLed > YQInt

'            If XLed > Coordinate.X4 Then

'                If (YQIntLed < YQInt) And (SlopeLed > Slope) Then BinLineDown = True

'            Else

'                If (YQIntLed > YQInt) And (SlopeLed < Slope) Then BinLineDown = True

'            End If

'            '=========================================================================================================

'            '=========================================================================================================

'            'Calculate Vertical Slope and X_Intercept line coordinate.X3,coordinate.Y3  to coordinate.x2,coordinate.x2   (Right Line)

'            Slope = (Coordinate.X3 - Coordinate.X2) / (Coordinate.Y3 - Coordinate.Y2)                       'Calculate Slope line coordinate.X3,coordinate.Y3 to coordinate.x2,coordinate.y2
'            SlopeLed = (Coordinate.X3 - XLed) / (Coordinate.Y3 - YLed)                'Calculate Slope line coordinate.X3,coordinate.Y3 to XY Led to check
'            XQInt = Coordinate.X3 - ((Coordinate.X3 - Coordinate.X2) / (Coordinate.Y3 - Coordinate.Y2) * Coordinate.Y3)           'Calculate X-Intercept line coordinate.X3,coordinate.Y3 to coordinate.x2,coordinate.y2
'            XQIntLed = Coordinate.X3 - ((Coordinate.X3 - XLed) / (Coordinate.Y3 - YLed) * Coordinate.Y3)    'Calculate X-Intercept line coordinate.X3,coordinate.Y3 to XY Led to check
'            'IMPORTANT:

'            ' Check if XY Led to check is Left to Right Vertical line :

'            ' Position XY ok :  IF (YLed > coordinate.Y3) then  SlopeLed < Slope &&  XQIntLed > YQInt

'            '   Else    SlopeLed > Slope &&  YQIntLed < XQInt



'            If YLed > Coordinate.Y3 Then

'                If (XQIntLed > XQInt) And (SlopeLed < Slope) Then BinLineRight = True

'            Else

'                If (XQIntLed < XQInt) And (SlopeLed > Slope) Then BinLineRight = True

'            End If



'            '=========================================================================================================

'            '=========================================================================================================

'            'Calculate Vertical Slope and X_Intercept line coordinate.X4,coordinate.Y4  to coordinate.x1,coordinate.x1   (Left Line)



'            Slope = (Coordinate.X4 - Coordinate.X1) / (Coordinate.Y4 - Coordinate.Y1)                       'Calculate Slope line coordinate.X4,coordinate.Y4 to coordinate.x1,coordinate.y1

'            SlopeLed = (Coordinate.X4 - XLed) / (Coordinate.Y4 - YLed)                'Calculate Slope line coordinate.X4,coordinate.Y4 to XY Led to check

'            XQInt = Coordinate.X4 - ((Coordinate.X4 - Coordinate.X1) / (Coordinate.Y4 - Coordinate.Y1) * Coordinate.Y4)           'Calculate X-Intercept line coordinate.X4,coordinate.Y4 to coordinate.x1,coordinate.y1

'            XQIntLed = Coordinate.X4 - ((Coordinate.X4 - XLed) / (Coordinate.Y4 - YLed) * Coordinate.Y4)    'Calculate X-Intercept line coordinate.X4,coordinate.Y4 to XY Led to check



'            'IMPORTANT:

'            ' Check if XY Led to check is Right  to Left Vertical line :

'            ' Position XY ok :  IF (YLed > coordinate.Y4) then  SlopeLed < Slope &&  XQIntLed < YQInt

'            '   Else    SlopeLed < Slope &&  YQIntLed > XQInt



'            If YLed > Coordinate.Y4 Then

'                If (XQIntLed < XQInt) And (SlopeLed > Slope) Then BinLineLeft = True

'            Else

'                If (XQIntLed > XQInt) And (SlopeLed < Slope) Then BinLineLeft = True

'            End If



'            '=========================================================================================================

'            '=========================================================================================================


'            If BinLineUP And BinLineDown And BinLineRight And BinLineLeft Then
'                BINResult = True
'            Else
'                BINResult = False
'            End If
'        Catch
'            BINResult = False
'        End Try
'        Return BINResult
'    End Function

'    Public Function ValidateVF(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetVF As Double, Optional ByVal Offset_Vf As Double = 0) As Boolean
'        If FailFlag = FAIL Then Return False
'        Dim TerminalVF As Double = FeasaGetVF + Offset_Vf
'        Dim Res As Boolean = True
'        Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
'        If xx.Count > 0 Then
'            Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
'            If t(0).VF_SH > TerminalVF And t(0).VF_SL < TerminalVF Then
'                PrintResultMsg(PASS, NumSite, LEDName, TerminalVF, t(0).VF_SH.ToString, t(0).VF_SL.ToString, "_VF")
'                Res = Res And True
'            Else
'                PrintResultMsg(FAIL, NumSite, LEDName, TerminalVF, t(0).VF_SH.ToString, t(0).VF_SL.ToString, "_VF")
'                Res = Res And False
'            End If
'        End If
'        If Res Then
'            FailFlag = PASS
'        Else
'            FailFlag = FAIL
'        End If
'        Return Res
'    End Function

'    Public Function GetFeasaCount(ByVal NumSite As Integer, ByVal Code_Bitron As String) As List(Of String)
'        Dim XX = From C In MyConf.VERSIONIS Where C.CodeBitron = Code_Bitron Select C
'        If XX.Count > 0 Then
'            Dim aaa As List(Of String) = (From c In XX(0).Sites(NumSite - 1).SITE Select c.Name Distinct).ToList
'            Return aaa
'        End If
'    End Function

'    Public Function GetFeasaByFeasaName(ByVal FeasaName As String, ByRef FeasaSerial As String, ByRef FeasaCh As Integer)
'        Dim NumIndex As Integer = Convert.ToInt16(FeasaName.Replace("FEASA", ""))
'        Dim XX = From C In MyConf.GENERAL.Name_Feasa Where C.Number = NumIndex Select C
'        If XX.Count > 0 Then
'            FeasaSerial = XX(0).Name
'            FeasaCh = XX(0).NumCh
'        End If
'    End Function

'End Class
