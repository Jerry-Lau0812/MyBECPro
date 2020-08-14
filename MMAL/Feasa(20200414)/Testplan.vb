Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.FileIO
Imports System.Collections.Generic
Imports Bitron.DataTracer
Imports BitronConfig
Imports System.Xml
Imports System.Linq
Imports System.String



Module modTestplan

    '******Feasa Related******
    Public Myconf As New BitronConfig.BitronConfig(AppDomain.CurrentDomain.BaseDirectory & "\FunzConf.XML")

    Public myPara As New BitronConfig.BitronParaConfig(AppDomain.CurrentDomain.BaseDirectory & "\ParaConfig.XML")

    Public MySys As New SystemOperate
    Public Cap As ClassFEASA.EBrightLevel
    Public FeasaUsing As String = ""
    '******Feasa Related******

    Public ProgNo As String
    Public FailFlag As Integer

    Public NumSite As Integer
    Public Numbay As Integer

    Public BinName As New Hashtable()
    Public VFBinName As New Hashtable()

    Public Structure TraxMsg
        Public PartNo As String
        Public BinCode As String
    End Structure

    Public Structure Coordinate
        Dim X1 As Double
        Dim Y1 As Double
        Dim X2 As Double
        Dim Y2 As Double
        Dim X3 As Double
        Dim Y3 As Double
        Dim X4 As Double
        Dim Y4 As Double
    End Structure

    Dim FeasaCollectList = New List(Of FeasaCollect)

    Dim LEDCount As Integer

    Dim TraxResult As List(Of FujiTraxData)
    Private myTraceHelper As New DataTraceHelper(PlantLocations.China)


    Public Enum CurTypes
        CurTurn
        CurTail
        Tail_LED_chain
    End Enum

    Public Function Testplan() As Integer

        '初始化测试参数
        Dim PCBDatamatrix As String = ""
        Dim ProductNo As String = ""
        FailFlag = PASS
        Dim ICTTestFlag As Integer = RESULT_FAIL

        '获取当前使用的BAY及使用的Site
        UseBayRead(Numbay)
        UseSiteRead(NumSite)


        '原计划从ICT传结果至测试程序TPGM中，暂时Eric未找到方法
        'ICTTestFlag = UseSiteRead(NumSite)
        'If ICTTestFlag = RESULT_FAIL Then
        '    FailFlag = FAIL
        'End If

        '获取产品二维码
        Call CheckBarcode(PCBDatamatrix)

        '获取Fujitrax结果
        Call CheckFujiResult(PCBDatamatrix)

        '获取产品号
        Call CheckVarient(ProductNo)

        Call LightAllLED()

        '测试电阻
        Call CheckResistor(NumSite, PCBDatamatrix)

        '测试电流
        Call CheckCurr(NumSite, PCBDatamatrix)

        '校准Feasa
        'Call CheckCalibrationFeasa(Numbay, NumSite, ProductNo)

        '测试Feasa
        Call CheckFeasa(Numbay, NumSite, ProductNo, PCBDatamatrix)

        

        '关闭所有LED
        Call CloseAllLED()


        If FailFlag = PASS Then
            MsgPrintLog("FUCTIONAL TEST PASS...UUT" & NumSite, 0)
            SiteResultWrite(NumSite, RESULT_PASS)
        Else
            MsgPrintLog("FUCTIONAL TEST Fail...UUT" & NumSite, 0)
            SiteResultWrite(NumSite, RESULT_FAIL)
        End If

        'RunAnlTaskLabel("", "LD0ON", "LD0ON")

        TplanResultSet(FailFlag)

        Testplan = 1

    End Function




#Region "Function Test"

    ''' <summary>
    ''' 获取并判断产品二维码
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckBarcode(ByRef Barcode As String)
        If FailFlag = PASS Then
            Dim PCBNo As String
            PCBNo = Space$(20)
            Try
                SerialNumberRead(NumSite, PCBNo)
                MsgPrintLog("Debug-NumSite Using:" & NumSite & " " & PCBNo, 0)
                FailFlag = PASS
            Catch
                FailFlag = FAIL
            End Try
            Barcode = PCBNo
        End If
    End Sub

    ''' <summary>
    ''' Check If can get result from FujiTrax
    ''' </summary>
    ''' <param name="DataMatrix"></param>
    ''' <remarks></remarks>
    Public Sub CheckFujiResult(ByVal DataMatrix As String)
        If FailFlag = PASS Then
            TraxResult = myTraceHelper.GetFuJiTraxDatas(DataMatrix)
            If TraxResult.Count > 0 Then
                FailFlag = PASS
            Else
                FailFlag = FAIL
            End If
        End If
    End Sub

    ''' <summary>
    ''' 获取产品号
    ''' </summary>
    ''' <param name="ProgNo"></param>
    ''' <remarks></remarks>
    Public Sub CheckVarient(ByRef ProgNo As String)
        Dim VariantName As String = New String(" ", 20)
        Dim TrimVariantName As String = ""
        If FailFlag = PASS Then
            Try
                VariantCompositionRead(VariantName)
                TrimVariantName = VariantName.Substring(0, VariantName.IndexOf(Chr(0)))
                'TrimVariantName = VariantName.Substring(0, 5)
                ProgNo = TrimVariantName
                MsgPrintLog("Debug-Varient:" & NumSite & " " & ProgNo, 0)
                FailFlag = PASS
            Catch
                FailFlag = FAIL
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Light LED
    ''' </summary>
    ''' <param name="ProgNo"></param>
    ''' <remarks></remarks>
    Public Sub LightAllLED()
        If FailFlag = PASS Then
            Try
                Dim Meas As Integer = RunAnlTaskLabel("", "PWON", "PWON")
                If Meas = PASS Then
                    MsgPrintLog("Light All LED Success:" & NumSite & " " & ProgNo, 0)
                    FailFlag = PASS
                Else
                    MsgPrintLog("Light All LED Fail:" & NumSite & " " & ProgNo, 0)
                    FailFlag = FAIL
                End If
            Catch
                FailFlag = FAIL
            End Try
        End If
    End Sub

    Public Sub CloseAllLED()
        'If FailFlag = PASS Then
        Try
            Dim Meas As Integer = RunAnlTaskLabel("", "PWOFF", "PWOFF")
            If Meas = PASS Then
                MsgPrintLog("Close All LED Success:" & NumSite & " " & ProgNo, 0)
                'FailFlag = PASS
            Else
                MsgPrintLog("Close All LED Fail:" & NumSite & " " & ProgNo, 0)
                'FailFlag = FAIL
            End If
        Catch
            'FailFlag = FAIL
        End Try
        'End If
    End Sub

    Public Function StartCalibrationLEDs(ByVal SiteNo As Integer, ByVal Code_Bitron As String, ByVal sPath As String) As Boolean

        Dim FileName As String = sPath
        Dim delimiter As String = ","
        Dim fields As String()
        Dim LedMeasure As New Hashtable
        Try
            Using parser As New TextFieldParser(FileName)
                parser.SetDelimiters(delimiter)
                While Not parser.EndOfData
                    fields = parser.ReadFields()
                    LedMeasure.Add(fields(0), fields)
                End While
            End Using
        Catch
            MsgPrintLog("Please check file exist or not: " & sPath, 0)
        End Try

        For Each Ver In Myconf.VERSIONIS
            If Ver.CodeBitron = Code_Bitron Then 'Select Product(Correspond code bitron)
                For Each stp In Ver.Sites        'Select Site Steps
                    If stp.SiteNo = SiteNo Then
                        For Each j In stp.SITE
                            Dim LEDName As String = j.LEDName
                            Dim CalAbsInt As Double = CType(LedMeasure(LEDName)(3), Double)
                            Dim CalAbsWav As Integer = CType(LedMeasure(LEDName)(4), Integer)
                            Dim CalCx As Double = CType(LedMeasure(LEDName)(1), Double)
                            Dim CalCy As Double = CType(LedMeasure(LEDName)(2), Double)
                            FeasaCalibrationAbsIntensity(j.Name, j.CH, CalAbsInt)
                            FeasaCalibrationWav(j.Name, j.CH, CalAbsWav)
                            FeasaCalibrationCxCy(j.Name, j.CH, CalCx, CalCy)
                        Next
                    End If
                Next
            End If
        Next
    End Function

    Public Function FeasaCalibrationAbsIntensity(ByVal FeasaName As String, ByVal Ch As Integer, ByVal ValiAbsint As Double)
        If FeasaUsing <> FeasaName Then '如果前后两通道不为同一个Feasa,则更改feasa
            FeasaUsing = FeasaName
            SystemOperate.Device_FEASA.BusgetFeasa(FeasaUsing)
        End If
        Return SystemOperate.Device_FEASA.CalibrationAbsIntensity(Ch, ValiAbsint)
    End Function

    Public Function FeasaCalibrationWav(ByVal FeasaName As String, ByVal Ch As Integer, ByVal ValiWave As Double)
        If FeasaUsing <> FeasaName Then '如果前后两通道不为同一个Feasa,则更改feasa
            FeasaUsing = FeasaName
            SystemOperate.Device_FEASA.BusgetFeasa(FeasaName)
        End If
        Return SystemOperate.Device_FEASA.CalibrationWavelength(Ch, ValiWave)
    End Function

    Public Function FeasaCalibrationCxCy(ByVal FeasaName As String, ByVal Ch As Integer, ByVal ValiCx As Double, ByVal ValiCy As Double)
        If FeasaUsing <> FeasaName Then '如果前后两通道不为同一个Feasa,则更改feasa
            FeasaUsing = FeasaName
            SystemOperate.Device_FEASA.BusgetFeasa(FeasaName)
        End If
        Return SystemOperate.Device_FEASA.CalibrationCxCy(Ch, ValiCx, ValiCy)
    End Function


    ''' <summary>
    ''' Feasa测试
    ''' </summary>
    ''' <param name="SiteNo"></param>
    ''' <param name="ProgNo"></param>
    ''' <remarks></remarks>
    Public Sub CheckFeasa(ByVal BayNo As Integer, ByVal SiteNo As Integer, ByVal ProgNo As String, ByVal Barcode As String)
        Dim FeasaUsed As List(Of String)
        Dim FeasaAddr As Integer
        'ProgNo = "30100646"
        MsgPrintLog(ProgNo, 0)
        If FailFlag = PASS Then
            FeasaUsed = GetAllUsedFeasa(SiteNo, ProgNo)
            'For Each t In FeasaUsed ‘Used For Debug
            '    MsgPrintLog(t, 0)
            'Next
            FeasaAddr = GetUsedFeasaAddr(FeasaUsed)
            Dim Res = SystemOperate.Device_FEASA.OpenCom(FeasaAddr)

            If Not Res Then
                MsgPrintLog(FeasaAddr & "Open Success!", 0)
            End If

            If Res Then
                Res = CollectFeasaValue(FeasaUsed)
            End If

            If Not Res Then
                MsgPrintLog(FeasaAddr & "CollectFeasaValue Err!", 0)
            End If

            'If Res Then
            Try
                SystemOperate.Device_FEASA.CloseCom()
            Catch
                MsgPrintLog(FeasaAddr & "Close Success!", 0)
            End Try
            'End If

            If Res Then
                StartCheckLEDs(SiteNo, ProgNo, Barcode)
            End If

            If FailFlag = PASS Then
                FailFlag = PASS
                MsgPrintLog("Bay:" & BayNo & " NumSite:" & SiteNo & " FeasaComAddr:" & FeasaAddr, 0)
            Else
                FailFlag = FAIL
            End If
        End If
    End Sub


    Public Sub CheckCalibrationFeasa(ByVal BayNo As Integer, ByVal SiteNo As Integer, ByVal ProgNo As String)
        Dim FeasaUsed As List(Of String)
        Dim FeasaAddr As Integer

        Dim CaliFile As String = AppDomain.CurrentDomain.BaseDirectory & "FeasaCalibration/" & "CALIBRATION_SITE" & SiteNo & ".CSV"
        'MsgPrintLog(ProgNo, 0)
        If FailFlag = PASS Then
            FeasaUsed = GetAllUsedFeasa(SiteNo, ProgNo)
            FeasaAddr = GetUsedFeasaAddr(FeasaUsed)
            Dim Res = SystemOperate.Device_FEASA.OpenCom(FeasaAddr)

            If Not Res Then
                MsgPrintLog(FeasaAddr & "Open Success!", 0)
            End If

            If Res Then
                Res = CollectFeasaValue(FeasaUsed)
            End If

            If Not Res Then
                MsgPrintLog(FeasaAddr & "CollectFeasaValue Err!", 0)
            End If

            If Res Then
                StartCalibrationLEDs(SiteNo, ProgNo, CaliFile)
            End If

            If Res Then
                Try
                    Res = SystemOperate.Device_FEASA.CloseCom()
                Catch
                    Res = False
                End Try
            End If

            If Res Then
                FailFlag = PASS
                MsgPrintLog("Bay:" & BayNo & " NumSite:" & SiteNo & " FeasaComAddr:" & FeasaAddr, 0)
            Else
                FailFlag = FAIL
            End If
        End If
    End Sub


    Public Function GetCurrentFeasaIndex(ByVal FeasaName) As Integer
        Dim Index As Integer = -1
        Dim XX = From C In MyConf.GENERAL.Name_Feasa Where C.Name = FeasaName Select C
        If XX.Count > 0 Then
            Index = XX(0).Index
        End If
        Return Index
    End Function


    Public Function StartCheckLEDs(ByVal SiteNo As Integer, ByVal Code_Bitron As String, ByVal PCBMatrix As String) As Boolean
        For Each Ver In Myconf.VERSIONIS
            If Ver.CodeBitron = Code_Bitron Then 'Select Product(Correspond code bitron)
                For Each stp In Ver.Sites        'Select Site Steps
                    If stp.SiteNo = SiteNo Then
                        For Each j In stp.SITE
                            Dim CurrentIndex As Integer = GetCurrentFeasaIndex(j.Name)
                            Dim Ch As Integer = j.CH - 1
                            Dim TempRegisterVF As Double = 0
                            RunAnlTaskLabel("", j.LEDName, j.LEDName)
                            HoldRegisterRead(j.VFRegister, TempRegisterVF, "")
                            If Myconf.GENERAL.GetBinCodeFromFujiTrax = "0" Then
                                ValidateVF(j.LEDName, SiteNo, j.BinName, j.Code, TempRegisterVF, j.VFOffset)
                                ValidateHSI(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).H(Ch), FeasaCollectList(CurrentIndex).S(Ch), FeasaCollectList(CurrentIndex).I(Ch), j.HueOffset, j.SatOffset, j.IntensityOffset)
                                ValidateAbsIntensity(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).AbsI(Ch), j.IntensityOffset)
                                ValidateWavelength(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).Wave(Ch), j.DominantWavOffset)
                                'ValidateCXCY(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).Cx(Ch), FeasaCollectList(CurrentIndex).Cy(Ch), j.CxOffset, j.CyOffset)
                            Else
                                Try
                                    j.BinName = GetTraxResultAccording(PCBMatrix, j.LEDName).BinCode
                                    j.Code = GetTraxResultAccording(PCBMatrix, j.LEDName).PartNo
                                Catch
                                    FailFlag = FAIL 'Test Fail
                                    MsgPrintLog("Numsite" & NumSite.ToString & " GetFujiTrx Failed! Please check FunzConfiguration or FujiTrax!", 0)
                                End Try
                                ValidateVF(j.LEDName, SiteNo, j.BinName, j.Code, TempRegisterVF, j.VFOffset)
                                ValidateHSI(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).H(Ch), FeasaCollectList(CurrentIndex).S(Ch), FeasaCollectList(CurrentIndex).I(Ch), j.HueOffset, j.SatOffset, j.IntensityOffset)
                                ValidateAbsIntensity(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).AbsI(Ch), j.IntensityOffset)
                                ValidateWavelength(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).Wave(Ch), j.DominantWavOffset)
                                'ValidateCXCY(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).Cx(Ch), FeasaCollectList(CurrentIndex).Cy(Ch), j.CxOffset, j.CyOffset)
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Function

    Public Function GetTraxResultAccording(ByVal SN As String, ByVal LEDName As String) As TraxMsg
        Dim Trax As New TraxMsg
        Dim TraxRes As FujiTraxData

        If LEDName = "" Then
            FailFlag = FAIL
            MsgPrintLog("Numsite" & NumSite.ToString & " GetFujiTrx Failed!", 0)
            Return Trax
        End If


        Dim TmpLEDName = LEDName.Replace("DL", "LED")
        TraxRes = myTraceHelper.GetFuJiTraxData(SN, TmpLEDName)
        Trax.BinCode = TraxRes.BinCode
        Trax.PartNo = TraxRes.PartNo

        Return Trax
    End Function

    ''' <summary>
    ''' VF测试
    ''' </summary>
    ''' <param name="LEDName"></param>
    ''' <param name="NumSite"></param>
    ''' <param name="BinName"></param>
    ''' <param name="Code"></param>
    ''' <param name="FeasaGetVF"></param>
    ''' <param name="Offset_Vf"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub ValidateVF(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetVF As Double, Optional ByVal Offset_Vf As Double = 0)
        Dim Res As Boolean = False
        If FailFlag = PASS Then
            Dim TerminalVF As Double = FeasaGetVF + Offset_Vf
            Dim xx = From C In Myconf.LEDS Where C.Code = Code Select C
            If xx.Count > 0 Then
                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
                If t(0).VF_SH > TerminalVF And t(0).VF_SL < TerminalVF Then
                    PrintResultMsg(PASS, NumSite, LEDName, TerminalVF, t(0).VF_SH.ToString, t(0).VF_SL.ToString, "_VF")
                    Res = True
                Else
                    PrintResultMsg(FAIL, NumSite, LEDName, TerminalVF, t(0).VF_SH.ToString, t(0).VF_SL.ToString, "_VF")
                    Res = False
                End If
            End If
        Else

        End If
        If Res Then
            FailFlag = PASS
        Else
            FailFlag = FAIL
        End If
    End Sub

    ''' <summary>
    ''' 标准结果打印
    ''' </summary>
    ''' <param name="TestFlag"></param>
    ''' <param name="NumSite"></param>
    ''' <param name="LEDName"></param>
    ''' <param name="Value"></param>
    ''' <param name="Hi_Limit"></param>
    ''' <param name="Lo_Limit"></param>
    ''' <param name="TestItem"></param>
    ''' <remarks></remarks>
    Private Sub PrintResultMsg(ByVal TestFlag As Boolean, ByVal NumSite As Integer, ByVal LEDName As String, ByVal Value As Object, ByVal Hi_Limit As Object, ByVal Lo_Limit As Object, ByVal TestItem As String)
        If TestFlag = PASS Then
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " PASS", 0)
            ObsDatalogTest(1, LEDName, TestItem, PASS, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
        Else
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " FAIL", 0)
            PrintRemarkFail(1, LEDName + TestItem, "0 0", Value.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
            ObsDatalogTest(1, LEDName, TestItem, FAIL, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
            SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
        End If
    End Sub

    Private Sub PrintResultMsg(ByVal TestFlag As Boolean, ByVal NumSite As Integer, ByVal LEDName As String, ByVal Value_Cx As Object, ByVal Value_Cy As Object, ByVal Hi_Limit As Object, ByVal Lo_Limit As Object, ByVal TestItem As String)
        If TestFlag = PASS Then
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " PASS", 0)
            ObsDatalogTest(1, LEDName, TestItem, PASS, Value_Cx.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
            ObsDatalogTest(1, LEDName, TestItem, PASS, Value_Cy.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
        Else
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " FAIL", 0)
            PrintRemarkFail(1, LEDName + "_Cx", "0 0", Value_Cx.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
            PrintRemarkFail(1, LEDName + "_Cy", "0 0", Value_Cy.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
            ObsDatalogTest(1, LEDName, "_Cx", FAIL, Value_Cx.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
            ObsDatalogTest(1, LEDName, "_Cy", FAIL, Value_Cy.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
            SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
        End If
    End Sub

    Public Sub PrintRemarkFail(ByVal NumTest As Integer, ByVal Remark As String, ByVal TpList As String, ByVal Valore As String, ByVal Sh_Max As String, ByVal Sh_Min As String, ByVal Unit As String, ByVal site As Integer)
        MsgDispService("FVT TEST#" + NumTest.ToString + "      FAIL (-)", 0)
        MsgDispService(Remark + " /" + site.ToString, 0)
        MsgDispService("TP: " + TpList, 0)
        MsgDispService("VALUE: " + Valore.ToString + " " + Unit, 0)
        MsgDispService("Max: " + Sh_Max.ToString + Unit + " / Min: " + Sh_Min.ToString + Unit, 0)
        MsgDispService(Chr(13), 0)
    End Sub


    ''' <summary>
    ''' 检查HSI结果 HSI
    ''' </summary>
    ''' <param name="LEDName"></param>
    ''' <param name="NumSite"></param>
    ''' <param name="BinName"></param>
    ''' <param name="Code"></param>
    ''' <param name="FeasaGetHSI_H"></param>
    ''' <param name="FeasaGetHSI_S"></param>
    ''' <param name="FeasaGetHSI_I"></param>
    ''' <param name="h_Offset"></param>
    ''' <param name="s_Offset"></param>
    ''' <param name="i_Offset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub ValidateHSI(ByVal LEDName As String, ByVal NumSite As String, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetHSI_H As Double, ByVal FeasaGetHSI_S As Integer, ByVal FeasaGetHSI_I As Integer, Optional ByVal h_Offset As Double = 0, Optional ByVal s_Offset As Integer = 0, Optional ByVal i_Offset As Integer = 0)
        Dim Res As Boolean = False
        If FailFlag = PASS Then
            Dim xx = From C In Myconf.LEDS Where C.Code = Code Select C
            Dim TeminalHSI_H As Double = FeasaGetHSI_H + h_Offset
            Dim TeminalHSI_S As Integer = FeasaGetHSI_S + s_Offset
            Dim TeminalHSI_I As Int32 = FeasaGetHSI_I + i_Offset
            If xx.Count > 0 Then
                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
                If t.Count > 0 Then
                    If t(0).Hue_SH >= TeminalHSI_H And t(0).Hue_SL <= TeminalHSI_H Then
                        Res = True
                        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_H, t(0).Hue_SH, t(0).Hue_SL, "_HUE")
                    Else
                        Res = False
                        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_H, t(0).Hue_SH, t(0).Hue_SL, "_HUE")
                    End If

                    If Res Then
                        If t(0).Sat_SH >= TeminalHSI_S And t(0).Sat_SL <= TeminalHSI_S Then
                            Res = True
                            Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_S, t(0).Sat_SH, t(0).Sat_SL, "_SAT")
                        Else
                            Res = False
                            Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_S, t(0).Sat_SH, t(0).Sat_SL, "_SAT")
                        End If
                    End If

                    'If Res Then
                    '    If t(0).Intensity_SH >= TeminalHSI_I And t(0).Intensity_SL <= TeminalHSI_I Then
                    '        Res = True
                    '        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_I, t(0).Intensity_SH, t(0).Intensity_SL, "_INTENSITY")
                    '    Else
                    '        Res = False
                    '        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_I, t(0).Intensity_SH, t(0).Intensity_SL, "_INTENSITY")
                    '    End If
                    'End If
                Else
                    Res = False
                    MsgBox("Not Fund " & BinName & "or " & Code & "in Config!")
                End If
            Else
                Res = False
                MsgBox("HSI Config Error!")
            End If
            If Res Then
                FailFlag = PASS
            Else
                FailFlag = FAIL
            End If
        End If


    End Sub

    ''' <summary>
    ''' 检查绝对亮度 AbsIntensity
    ''' </summary>
    ''' <param name="LEDName"></param>
    ''' <param name="NumSite"></param>
    ''' <param name="BinName"></param>
    ''' <param name="Code"></param>
    ''' <param name="FeasaGetAbsint"></param>
    ''' <param name="Offset_Abs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub ValidateAbsIntensity(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetAbsint As Double, Optional ByVal Offset_Abs As Double = 0)
        Dim Res As Boolean = False
        If FailFlag = PASS Then
            Dim TerminalAbs As Double = FeasaGetAbsint + Offset_Abs
            Dim xx = From C In Myconf.LEDS Where C.Code = Code Select C
            If xx.Count > 0 Then
                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
                If t(0).Intensity_SH >= TerminalAbs And t(0).Intensity_SL <= TerminalAbs Then
                    Res = True
                    PrintResultMsg(PASS, NumSite, LEDName, TerminalAbs, t(0).Intensity_SH, t(0).Intensity_SL, "_AbsIntensity")
                Else
                    Res = False
                    'MsgBox(Numbay & " " & NumSite & " " & FeasaGetAbsint)
                    PrintResultMsg(FAIL, NumSite, LEDName, TerminalAbs, t(0).Intensity_SH, t(0).Intensity_SL, "_AbsIntensity")
                End If
            Else
                MsgBox("HSI Config Error!")
            End If
        End If
        If Res Then
            FailFlag = PASS
        Else
            FailFlag = FAIL
        End If
    End Sub

    ''' <summary>
    ''' 检查波长 Wavelength
    ''' </summary>
    ''' <param name="LEDName"></param>
    ''' <param name="NumSite"></param>
    ''' <param name="BinName"></param>
    ''' <param name="Code"></param>
    ''' <param name="FeasaGetWav"></param>
    ''' <param name="Offset_Wav"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub ValidateWavelength(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetWav As Double, Optional ByVal Offset_Wav As Double = 0)
        Dim Res As Boolean = False
        If FailFlag = PASS Then
            Dim TerminalWav As Double = FeasaGetWav + Offset_Wav
            Dim xx = From C In Myconf.LEDS Where C.Code = Code Select C
            If xx.Count > 0 Then
                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
                If t(0).DominantWav_SH >= TerminalWav And t(0).DominantWav_SL <= TerminalWav Then
                    Res = True
                    PrintResultMsg(PASS, NumSite, LEDName, TerminalWav, t(0).DominantWav_SH, t(0).DominantWav_SL, "_DominantWave")
                Else
                    Res = False
                    PrintResultMsg(FAIL, NumSite, LEDName, TerminalWav, t(0).DominantWav_SH, t(0).DominantWav_SL, "_DominantWave")
                End If
            Else
                MsgBox("HSI Config Error!")
            End If
        End If
        If Res Then
            FailFlag = PASS
        Else
            FailFlag = FAIL
        End If
    End Sub

    ''' <summary>
    ''' 检查CxCy
    ''' </summary>
    ''' <param name="LEDName"></param>
    ''' <param name="NumSite"></param>
    ''' <param name="BinName"></param>
    ''' <param name="Code"></param>
    ''' <param name="FeasaGetCx"></param>
    ''' <param name="FeasaGetCy"></param>
    ''' <param name="Offset_Cx"></param>
    ''' <param name="Offset_Cy"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub ValidateCXCY(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetCx As Double, ByVal FeasaGetCy As Double, Optional ByVal Offset_Cx As Double = 0, Optional ByVal Offset_Cy As Double = 0)
        Dim Res As Boolean = False
        Dim coor As Coordinate

        Dim TeminalCx As Double = FeasaGetCx + Offset_Cx
        Dim TeminalCy As Double = FeasaGetCy + Offset_Cy

        If FailFlag = PASS Then
            Dim xx = From C In Myconf.LEDS Where C.Code = Code Select C
            If xx.Count > 0 Then
                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D

                If t.Count > 0 Then
                    coor.X1 = t(0).CXCYRange(0).CX
                    coor.Y1 = t(0).CXCYRange(0).CY
                    coor.X2 = t(0).CXCYRange(1).CX
                    coor.Y2 = t(0).CXCYRange(1).CY
                    coor.X3 = t(0).CXCYRange(2).CX
                    coor.Y3 = t(0).CXCYRange(2).CY
                    coor.X4 = t(0).CXCYRange(3).CX
                    coor.Y4 = t(0).CXCYRange(3).CY
                    Res = CheckCxCyInArea(coor, TeminalCx, TeminalCy)
                Else
                    MsgBox("CxCy Config Error!")
                End If
            End If

            If Res Then
                Res = True
                PrintResultMsg(PASS, NumSite, LEDName, FormatFeasaCxCy(TeminalCx), FormatFeasaCxCy(TeminalCy), "-", "-", "_Cxcy")
            Else
                Res = False
                PrintResultMsg(FAIL, NumSite, LEDName, FormatFeasaCxCy(TeminalCx), FormatFeasaCxCy(TeminalCy), "-", "-", "_Cxcy")
            End If
        End If
        If Res Then
            FailFlag = True
        Else
            FailFlag = False
        End If
    End Sub

    Public Function FormatFeasaCxCy(ByVal Val As Double) As String
        Return Val.ToString("0.0000")
    End Function

    ''' <summary>
    ''' CxCy校验函数
    ''' </summary>
    ''' <param name="Coordinate"></param>
    ''' <param name="XLed"></param>
    ''' <param name="YLed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckCxCyInArea(ByVal Coordinate As Coordinate, ByVal XLed As Double, ByVal YLed As Double) As Boolean
        Dim Slope As Double         'Slope Line XY
        Dim SlopeLed As Double      'Slope Line Led Bin
        Dim XQInt As Double         'Q X-Intercept (to use for vertical line)
        Dim XQIntLed As Double      'Q X-Intercept Led Bin (to use for vertical line)
        Dim YQInt As Double         'Q Y-Intercept (to use for horizontal line)
        Dim YQIntLed As Double      'Q Y-Intercept Led Bin (to use for horizontal line)
        Dim BinLineUP As Boolean
        Dim BinLineDown As Boolean
        Dim BinLineRight As Boolean
        Dim BinLineLeft As Boolean
        Dim BINResult As Boolean
        BinLineUP = False
        BinLineDown = False
        BinLineRight = False
        BinLineLeft = False
        BINResult = False

        XLed = XLed + 0.00000001    'to avoid DIV/0
        YLed = YLed + 0.00000001    'to avoid DIV/0

        ' Calculate slope for Horizontal line:          (coordinate.y1-coordinate.y2) / (coordinate.x1-coordinate.x2)

        ' Calculate slope for Vertical line:            (coordinate.x1-coordinate.x2) / (coordinate.y1-coordinate.y2)

        ' Calculate QY-Intercept for Horizontal Line :  coordinate.y1 - ((coordinate.y1-coordinate.y2)/(coordinate.x1-coordinate.x2)*coordinate.x1)

        ' Calculate QX-Intercept for Vrtical Line :     coordinate.x1 - ((coordinate.x1-coordinate.x2)/(coordinate.y1-coordinate.y2)*coordinate.y1)

        Try

            '=========================================================================================================

            'Calculate Horizontal Slope and Y_Intercept line coordinate.x1,coordinate.y1  to coordinate.x2,coordinate.x2   (Upper LIne)



            Slope = (Coordinate.Y1 - Coordinate.Y2) / (Coordinate.X1 - Coordinate.X2)                       'Calculate Slope line coordinate.x1,coordinate.y1 to coordinate.x2,coordinate.y2

            SlopeLed = (Coordinate.Y1 - YLed) / (Coordinate.X1 - XLed)                'Calculate Slope line coordinate.x1,coordinate.y1 to XY Led to check

            YQInt = Coordinate.Y1 - ((Coordinate.Y1 - Coordinate.Y2) / (Coordinate.X1 - Coordinate.X2) * Coordinate.X1)           'Calculate Y-Intercept line coordinate.x1,coordinate.y1 to coordinate.x2,coordinate.y2

            YQIntLed = Coordinate.Y1 - ((Coordinate.Y1 - YLed) / (Coordinate.X1 - XLed) * Coordinate.X1)    'Calculate Y-Intercept line coordinate.x1,coordinate.y1 to XY Led to check



            'IMPORTANT:

            ' Check if XY Led to check is Down to Upper horizontal line :

            ' Position XY ok :  IF (XLed > coordinate.x1) then  SlopeLed < Slope &&  YQIntLed > YQInt

            '   Else    SlopeLed > Slope &&  YQIntLed < YQInt



            If XLed > Coordinate.X1 Then

                If (YQIntLed > YQInt) And (SlopeLed < Slope) Then BinLineUP = True

            Else

                If (YQIntLed < YQInt) And (SlopeLed > Slope) Then BinLineUP = True

            End If
            '=========================================================================================================

            '=========================================================================================================

            'Calculate Vertical Slope and Y_Intercept  line coordinate.X4,coordinate.Y4  to coordinate.X3,coordinate.X3   (Lower LIne)

            Slope = (Coordinate.Y4 - Coordinate.Y3) / (Coordinate.X4 - Coordinate.X3)                       'Calculate Slope line coordinate.X4,coordinate.Y4 to coordinate.X3,coordinate.Y3

            SlopeLed = (Coordinate.Y4 - YLed) / (Coordinate.X4 - XLed)                'Calculate Slope line coordinate.X4,coordinate.Y4 to XY Led to check

            YQInt = Coordinate.Y4 - ((Coordinate.Y4 - Coordinate.Y3) / (Coordinate.X4 - Coordinate.X3) * Coordinate.X4)           'Calculate Y-Intercept line coordinate.X4,coordinate.Y4 to coordinate.X3,coordinate.Y3

            YQIntLed = Coordinate.Y4 - ((Coordinate.Y4 - YLed) / (Coordinate.X4 - XLed) * Coordinate.X4)    'Calculate Y-Intercept line coordinate.X4,coordinate.Y4 to XY Led to check

            'IMPORTANT:

            ' Check if XY Led to check is Up to L0wer horizontal line :

            ' Position XY ok :  IF (XLed > coordinate.X4) then  SlopeLed < Slope &&  YQIntLed < YQInt

            '   Else    SlopeLed < Slope &&  YQIntLed > YQInt

            If XLed > Coordinate.X4 Then

                If (YQIntLed < YQInt) And (SlopeLed > Slope) Then BinLineDown = True

            Else

                If (YQIntLed > YQInt) And (SlopeLed < Slope) Then BinLineDown = True

            End If

            '=========================================================================================================

            '=========================================================================================================

            'Calculate Vertical Slope and X_Intercept line coordinate.X3,coordinate.Y3  to coordinate.x2,coordinate.x2   (Right Line)

            Slope = (Coordinate.X3 - Coordinate.X2) / (Coordinate.Y3 - Coordinate.Y2)                       'Calculate Slope line coordinate.X3,coordinate.Y3 to coordinate.x2,coordinate.y2
            SlopeLed = (Coordinate.X3 - XLed) / (Coordinate.Y3 - YLed)                'Calculate Slope line coordinate.X3,coordinate.Y3 to XY Led to check
            XQInt = Coordinate.X3 - ((Coordinate.X3 - Coordinate.X2) / (Coordinate.Y3 - Coordinate.Y2) * Coordinate.Y3)           'Calculate X-Intercept line coordinate.X3,coordinate.Y3 to coordinate.x2,coordinate.y2
            XQIntLed = Coordinate.X3 - ((Coordinate.X3 - XLed) / (Coordinate.Y3 - YLed) * Coordinate.Y3)    'Calculate X-Intercept line coordinate.X3,coordinate.Y3 to XY Led to check
            'IMPORTANT:

            ' Check if XY Led to check is Left to Right Vertical line :

            ' Position XY ok :  IF (YLed > coordinate.Y3) then  SlopeLed < Slope &&  XQIntLed > YQInt

            '   Else    SlopeLed > Slope &&  YQIntLed < XQInt



            If YLed > Coordinate.Y3 Then

                If (XQIntLed > XQInt) And (SlopeLed < Slope) Then BinLineRight = True

            Else

                If (XQIntLed < XQInt) And (SlopeLed > Slope) Then BinLineRight = True

            End If



            '=========================================================================================================

            '=========================================================================================================

            'Calculate Vertical Slope and X_Intercept line coordinate.X4,coordinate.Y4  to coordinate.x1,coordinate.x1   (Left Line)



            Slope = (Coordinate.X4 - Coordinate.X1) / (Coordinate.Y4 - Coordinate.Y1)                       'Calculate Slope line coordinate.X4,coordinate.Y4 to coordinate.x1,coordinate.y1

            SlopeLed = (Coordinate.X4 - XLed) / (Coordinate.Y4 - YLed)                'Calculate Slope line coordinate.X4,coordinate.Y4 to XY Led to check

            XQInt = Coordinate.X4 - ((Coordinate.X4 - Coordinate.X1) / (Coordinate.Y4 - Coordinate.Y1) * Coordinate.Y4)           'Calculate X-Intercept line coordinate.X4,coordinate.Y4 to coordinate.x1,coordinate.y1

            XQIntLed = Coordinate.X4 - ((Coordinate.X4 - XLed) / (Coordinate.Y4 - YLed) * Coordinate.Y4)    'Calculate X-Intercept line coordinate.X4,coordinate.Y4 to XY Led to check



            'IMPORTANT:

            ' Check if XY Led to check is Right  to Left Vertical line :

            ' Position XY ok :  IF (YLed > coordinate.Y4) then  SlopeLed < Slope &&  XQIntLed < YQInt

            '   Else    SlopeLed < Slope &&  YQIntLed > XQInt



            If YLed > Coordinate.Y4 Then

                If (XQIntLed < XQInt) And (SlopeLed > Slope) Then BinLineLeft = True

            Else

                If (XQIntLed > XQInt) And (SlopeLed < Slope) Then BinLineLeft = True

            End If



            '=========================================================================================================

            '=========================================================================================================


            If BinLineUP And BinLineDown And BinLineRight And BinLineLeft Then
                BINResult = True
            Else
                BINResult = False
            End If
        Catch
            BINResult = False
        End Try
        Return BINResult
    End Function



    ''' <summary>
    ''' Collect All Feasa Channel values based on used site.
    ''' </summary>
    ''' <param name="FeasaUse"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CollectFeasaValue(ByVal FeasaUse As List(Of String)) As Boolean

        FeasaCollectList.Clear()
        Dim ColFEASA As Integer = FeasaUse.Count - 1
        Dim ChNum As Integer = -1
        Dim Res As Boolean = False
        For Each obj In FeasaUse
            Dim FeasaCollect As New FeasaCollect
            Res = SystemOperate.Device_FEASA.BusgetFeasa(obj)
            If Not Res Then
                MsgBox("Change Feasa to Fail: " & obj)
                Return False
            End If

            SystemOperate.Device_FEASA.Capture(ClassFEASA.EBrightLevel.Auto) '是否要做个处理方法，对于不同的要Capture LEVEL收集不同的CODE
            'Threading.Thread.Sleep(1000)
            ChNum = GetFeasaNum(obj)
            FeasaCollect.FeasaName = obj
            Res = GetAllFeasaPara(obj, ChNum, FeasaCollect.H, FeasaCollect.S, FeasaCollect.I, FeasaCollect.AbsI, FeasaCollect.Wave, FeasaCollect.Cx, FeasaCollect.Cy)

            'If NumSite = 2 Then
            '    MsgBox(FeasaCollect.FeasaName)

            '    MsgBox("3:" & FeasaCollect.AbsI(3).ToString)
            '    MsgBox("4:" & FeasaCollect.AbsI(4).ToString)
            '    MsgBox("5:" & FeasaCollect.AbsI(5).ToString)

            'End If

            'If NumSite = 5 Then
            '    MsgBox(FeasaCollect.FeasaName)

            '    MsgBox("3:" & FeasaCollect.AbsI(3).ToString)
            '    MsgBox("4:" & FeasaCollect.AbsI(4).ToString)
            '    MsgBox("5:" & FeasaCollect.AbsI(5).ToString)

            'End If

            If Not Res Then
                Return False
            End If
            
            FeasaCollectList.Add(FeasaCollect)
        Next
        Return Res
    End Function



    ''' <summary>
    ''' Get all feasa count base one site
    ''' </summary>
    ''' <param name="NodeFeasa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFeasaNum(ByVal NodeFeasa As String) As Integer
        Dim Num As Integer = -1
        Dim XX = From C In MyConf.GENERAL.Name_Feasa Where C.Name = NodeFeasa Select C
        If XX.Count > 0 Then
            Num = XX(0).NumCh
        End If
        Return Num
    End Function

    Public Function GetAllFeasaPara(ByVal FeasaObj As String, ByRef ChNum As Integer, ByRef H() As Double, ByRef S() As Integer, ByRef I() As Integer, ByRef AbsInt() As Double, ByRef Wave() As Integer, ByRef Cx() As Double, ByRef Cy() As Double)
        For Each T In Myconf.GENERAL.Name_Feasa
            If T.Name = FeasaObj Then
                ChNum = T.NumCh
            End If
        Next

        Dim Res As Boolean = SystemOperate.Device_FEASA.GetHSIAll(ChNum, H, S, I)
        Res = SystemOperate.Device_FEASA.GetAbsoluIntensity(ChNum, AbsInt, SITENo:=NumSite)
        Res = SystemOperate.Device_FEASA.GetWavlength(ChNum, Wave)
        Res = SystemOperate.Device_FEASA.GetCxCy(ChNum, Cx, Cy)
        Return Res
    End Function


    ''' <summary>
    ''' Get all FeasaUse in test
    ''' </summary>
    ''' <param name="SiteNo"></param>
    ''' <param name="Code_Biton"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllUsedFeasa(ByVal SiteNo As Integer, ByVal Code_Biton As String) As List(Of String)
        Dim XX = From C In Myconf.VERSIONIS Where C.CodeBitron = Code_Biton Select C
        If XX.Count > 0 Then
            Dim f As List(Of String) = (From d In XX(0).Sites(SiteNo - 1).SITE Select d.Name Distinct).ToList
            Return f
        End If
    End Function

    ''' <summary>
    ''' In a site test condition, it should be same addr of Device Feasa
    ''' </summary>
    ''' <param name="FeasaUse"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUsedFeasaAddr(ByVal FeasaUse As List(Of String)) As Integer
        Dim iAddr As Integer = -1
        If FeasaUse.Count < 1 Then
            Return -1
        End If
        For t As Integer = 0 To FeasaUse.Count - 1
            If t = 0 Then
                iAddr = GetFeasaAddr(FeasaUse(t))
            Else
                If iAddr <> GetFeasaAddr(FeasaUse(t)) Then
                    Return -1
                Else
                    iAddr = GetFeasaAddr(FeasaUse(t))
                End If
            End If
        Next
        Return iAddr
    End Function

    ''' <summary>
    ''' 根据FeasaName（e.g 75L3）获取Com地址
    ''' </summary>
    ''' <param name="NodeFeasa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFeasaAddr(ByVal NodeFeasa As String) As Integer
        Dim Addr As Integer = -1
        Dim XX = From C In Myconf.GENERAL.Name_Feasa Where C.Name = NodeFeasa Select C
        If XX.Count > 0 Then
            Addr = XX(0).Addr
        End If
        Return Addr
    End Function



#Region "Check Resistor"
    Public Sub CheckResistor(ByVal SiteNo As String, ByVal DataMatrxi As String)
        If FailFlag = PASS Then
            If myPara.IsCheckRes Then
                StartCheckRess(SiteNo)
            End If
        End If
    End Sub

    Public Function StartCheckRess(ByVal SiteNo As Integer) As Boolean
        For Each stp In myPara.TestResStep        'Select Site Steps
            Dim TempV As Double
            Dim GRes As Double
            RunAnlTaskLabel("", stp.RunLabel, stp.RunLabel)
            DvmRead(DVM1, TempV, MU_VOLT)
            GRes = TempV / stp.Modulux
            Dim BinName As String = GetBINAccordingLabel(stp.RunLabel)
            ValidateRes(SiteNo, BinName, GRes)
        Next
    End Function

    Public Function GetBINAccordingLabel(ByVal StrRunLbl As String) As String
        Dim RetBin As String = " "
        Dim TComBin = GetResBINFromLabel(StrRunLbl) '获取有几个LED灯组成BIN,相同LED使用同一个Res BIN
        For Each TC In TComBin
            For Each Fj In TraxResult
                If Fj.LEDName = TC Then
                    RetBin = Fj.BinCode.Substring(0, 2) & "-" & Fj.BinCode.Substring(3, 2)
                End If
            Next
        Next
        Return RetBin
    End Function

    Public Function GetResBINFromLabel(ByVal StrRunLbl As String) As String()
        Dim SCount() As String
        Try
            If StrRunLbl <> "" Then
                SCount = StrRunLbl.Split("_")
            End If
        Catch
        End Try
        Return SCount
    End Function

    Public Sub ValidateRes(ByVal NumSite As Integer, ByVal BinName As String, ByVal GetRes As Double)
        If FailFlag = PASS Then
            Try
                Dim TerminalRes As Double = GetRes
                Dim Res As Boolean = False
                Dim xx = From C In myPara.LEDRes Where C.BIN = BinName Select C
                If xx.Count > 0 Then
                    If xx(0).RESISTOR_H > TerminalRes And xx(0).RESISTOR_L < TerminalRes Then
                        PrintResultResMsg(PASS, NumSite, BinName, TerminalRes, xx(0).RESISTOR_H.ToString, xx(0).RESISTOR_L.ToString, " ohm")
                        Res = True
                    Else
                        PrintResultResMsg(FAIL, NumSite, BinName, TerminalRes, xx(0).RESISTOR_H.ToString, xx(0).RESISTOR_L.ToString, " ohm")
                        Res = False
                    End If
                End If
                If Res Then
                    FailFlag = PASS
                Else
                    FailFlag = FAIL
                End If
            Catch
                FailFlag = FAIL
            End Try
        End If
    End Sub

    Private Sub PrintResultResMsg(ByVal TestFlag As Boolean, ByVal NumSite As Integer, ByVal LEDName As String, ByVal Value As Object, ByVal Hi_Limit As Object, ByVal Lo_Limit As Object, ByVal TestItem As String)
        If TestFlag = PASS Then
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " PASS", 0)
            ObsDatalogTest(1, LEDName, TestItem, PASS, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
        Else
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " FAIL", 0)
            PrintRemarkFail(1, LEDName + TestItem, "0 0", Value.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
            ObsDatalogTest(1, LEDName, TestItem, FAIL, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
            SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
        End If
    End Sub

#End Region

#Region "Check Current"

    Public Sub CheckCurr(ByVal SiteNo As String, ByVal DataMatrxi As String)
        If FailFlag = PASS Then
            If myPara.IsCheckCur Then
                StartCheckCurrs(SiteNo)
            End If
        End If
    End Sub

    Public Function StartCheckCurrs(ByVal SiteNo As Integer) As Boolean
        For Each stp In myPara.TestCurStep        'Select Site Steps
            Dim TempV As Double
            Dim GRes As Double
            RunAnlTaskLabel("", stp.RunLabel, stp.RunLabel)
            DvmRead(DVM1, TempV, MU_VOLT)
            GRes = TempV * 1000 '统一单位
            Dim BinName As String = GetBINAccordingLabelCur(stp.Combination)
            Call ValidateCur(SiteNo, BinName, GRes)
        Next
    End Function

    Public Function GetBINAccordingLabelCur(ByVal StrCombination As String) As String
        Dim RetBin As String = ""
        Dim TComBin = GetCurBINFromLabel(StrCombination) '获取有几个LED灯组成BIN,相同LED使用同一个Res BIN
        For Each TC In TComBin
            For Each Fj In TraxResult
                If Fj.LEDName = TC Then
                    RetBin = Fj.BinCode.Substring(0, 2) & "-" & RetBin
                End If
            Next
        Next
        Return RetBin.Trim("-")
    End Function

    Public Function GetCurBINFromLabel(ByVal StrRunLbl As String) As String()
        Dim SCount() As String
        Try
            If StrRunLbl <> "" Then
                SCount = StrRunLbl.Split("_")
            End If
        Catch
        End Try
        Return SCount
    End Function

    Public Sub ValidateCur(ByVal NumSite As Integer, ByVal BinName As String, ByVal GetRes As Double)
        If FailFlag = PASS Then
            Try
                Dim TerminalRes As Double = GetRes
                Dim Res As Boolean = False
                Dim xx = From C In myPara.CurLimit Where C.BIN = BinName Select C
                If xx.Count > 0 Then
                    If xx(0).T_H > TerminalRes And xx(0).T_L < TerminalRes Then
                        PrintResultCurMsg(PASS, NumSite, BinName, TerminalRes, xx(0).T_H.ToString, xx(0).T_L.ToString, " ohm")
                        Res = True
                    Else
                        PrintResultCurMsg(FAIL, NumSite, BinName, TerminalRes, xx(0).T_H.ToString, xx(0).T_L.ToString, " ohm")
                        Res = False
                    End If
                End If
                If Res Then
                    FailFlag = PASS
                Else
                    FailFlag = FAIL
                End If
            Catch
                FailFlag = FAIL
            End Try
        End If
    End Sub

    Private Sub PrintResultCurMsg(ByVal TestFlag As Boolean, ByVal NumSite As Integer, ByVal LEDName As String, ByVal Value As Object, ByVal Hi_Limit As Object, ByVal Lo_Limit As Object, ByVal TestItem As String)
        If TestFlag = PASS Then
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " PASS", 0)
            ObsDatalogTest(1, LEDName, TestItem, PASS, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
        Else
            MsgPrintLog("UUT:" + NumSite.ToString + " " + LEDName + TestItem + " FAIL", 0)
            PrintRemarkFail(1, LEDName + TestItem, "0 0", Value.ToString, Hi_Limit.ToString, Lo_Limit.ToString, " NA", NumSite)
            ObsDatalogTest(1, LEDName, TestItem, FAIL, Value.ToString, Lo_Limit.ToString, Hi_Limit.ToString, "0,0", NumSite)
            SiteResultWrite(NumSite, RESULT_FAIL) : FailFlag = RESULT_FAIL
        End If
    End Sub

#End Region



#End Region



    ' ----------------------------------------------------------------------------
    '
    ' --- TESTPLAN Initialisation
    '
    ' This function is executed only one time when the test program is loaded.
    '
    Public Function TestplanInit() As Integer

        '
        ' --- INSERT YOUR CODE HERE ...
        '
        Myconf.LoadGeneralPara()
        Myconf.LoadHardwarePara()
        Myconf.LoadLEDPara()
        Myconf.LoadVESIONI()
        MySys.LoadAllHardwares()
        MyPara.Initial()

        TestplanInit = 1
    End Function


    ' ----------------------------------------------------------------------------
    '
    ' --- TESTPLAN End
    '
    ' This function is executed only one time when the test program ends.
    '
    Public Function TestplanEnd() As Integer

        '
        ' --- INSERT YOUR CODE HERE ...
        '

        TestplanEnd = 1
    End Function

End Module
