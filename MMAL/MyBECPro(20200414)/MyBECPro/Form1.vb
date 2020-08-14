Imports BitronConfig
Imports AtosF
Imports Microsoft.VisualBasic.FileIO
Imports Bitron.DataTracer


Public Class Form1

    Public SiteNo As Integer = 0
    Public MyConf As New BitronConfig.BitronConfig(AppDomain.CurrentDomain.BaseDirectory & "\FunzConf.XML")
    Public MySys As New SystemOperate
    Public myPara As New BitronConfig.BitronParaConfig(AppDomain.CurrentDomain.BaseDirectory & "\ParaConfig.XML")
    Public Cap As ClassFEASA.EBrightLevel

    Public FeasaUsing As String = ""

    Public TraxResult As List(Of FujiTraxData)
    Public myTraceHelper As New DataTraceHelper(PlantLocations.China)

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

    Public FeasaCollectList As New List(Of FeasaCollect)

    Public FailFlag As Boolean

    Private Sub Btn_Load_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConf.LoadGeneralPara()
        MyConf.LoadHardwarePara()
        MyConf.LoadLEDPara()
        MyConf.LoadVESIONI()
        MySys.LoadAllHardwares()

        myPara.Initial()

        InitialAllControls()
        InitialDatagridView()
    End Sub

    Private Sub InitialAllControls()
        ComboBrightLevel.Items.Clear()
        ComboBrightLevel.Items.AddRange([Enum].GetNames(GetType(ClassFEASA.EBrightLevel)))
        ComboBrightLevel.SelectedIndex = 0
    End Sub

    Private Sub InitialDatagridView()

    End Sub

    Dim ComOpenState As Boolean = False
    Private Sub Btn_OpenCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim CodeBitron = 30100646 'Suport use CodeBitron 30100646
        Dim FeasaUsed As List(Of String) = GetAllUsedFeasa(SiteNo, CodeBitron) 'Get All use feasa
        Dim FeasaAddr As Integer = GetUsedFeasaAddr(FeasaUsed) 'Get Addr



        ComOpenState = Not ComOpenState
        PicComOpen.Image = My.Resources.LEDYellow
        Application.DoEvents()

        Dim Res As Boolean
        If ComOpenState Then
            Res = SystemOperate.Device_FEASA.OpenCom(FeasaAddr)
        Else
            Res = SystemOperate.Device_FEASA.CloseCom(FeasaAddr)
        End If
        Call SetState(PicComOpen, ComOpenState, Res)
    End Sub

    Private Sub SetState(ByVal ImgName As PictureBox, ByVal State As Boolean, ByVal Result As Boolean)
        If Result Then
            If State Then
                ImgName.Image = My.Resources.LEDGreeN
            Else
                ImgName.Image = My.Resources.LEDGray
            End If
        Else
            ImgName.Image = My.Resources.LEDRed
        End If
    End Sub

    Private Sub Radio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Radio1.Click, Radio2.Click, Radio3.Click, Radio4.Click, Radio5.Click, Radio6.Click, Radio7.Click, Radio8.Click
        Dim Rad As RadioButton = CType(sender, RadioButton)
        Select Case Rad.Name
            Case Radio1.Name
                SiteNo = 1
            Case Radio2.Name
                SiteNo = 2
            Case Radio3.Name
                SiteNo = 3
            Case Radio4.Name
                SiteNo = 4
            Case Radio5.Name
                SiteNo = 5
            Case Radio6.Name
                SiteNo = 6
            Case Radio7.Name
                SiteNo = 7
            Case Radio8.Name
                SiteNo = 8
        End Select
    End Sub

    Private Sub BtnGetSerialNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim StrFeasa As String = ""
        Dim Res As Boolean = SystemOperate.Device_FEASA.GetSerial(StrFeasa)
        If Res Then
            TextFeasaNo.Text = StrFeasa
        Else
            TextFeasaNo.Text = "Not Found"
        End If
    End Sub

    Private Sub Btn_Capture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Cap As ClassFEASA.EBrightLevel = [Enum].Parse(GetType(ClassFEASA.EBrightLevel), ComboBrightLevel.SelectedItem)
        Btn_Capture.BackColor = Color.Yellow
        Dim Res As Boolean = SystemOperate.Device_FEASA.Capture(Cap)
        If Res Then
            Btn_Capture.BackColor = Color.Green
        Else
            Btn_Capture.BackColor = Color.Red
        End If
    End Sub

    Private Sub Btn_GetAllHSIs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ChNum As Integer = 0
        Dim H() As Double
        Dim S() As Integer
        Dim I() As Integer
        Dim AbsInt() As Double
        Dim Wave() As Integer
        Dim Cx() As Double
        Dim Cy() As Double

        Dim Res As Boolean = GetAllFeasaPara(ChNum, H, S, I, AbsInt, Wave, Cx, Cy)

        UpdataDGV(ChNum, H, S, I, AbsInt, Wave, Cx, Cy)
    End Sub

    Public Function GetAllFeasaPara(ByRef ChNum As Integer, ByRef H() As Double, ByRef S() As Integer, ByRef I() As Integer, ByRef AbsInt() As Double, ByRef Wave() As Integer, ByRef Cx() As Double, ByRef Cy() As Double)

        For Each T In MyConf.GENERAL.Name_Feasa
            If T.Name = TextFeasaNo.Text.Trim Then
                ChNum = T.NumCh
            End If
        Next

        Dim Res As Boolean = SystemOperate.Device_FEASA.GetHSIAll(ChNum, H, S, I)
        Res = SystemOperate.Device_FEASA.GetAbsoluIntensity(ChNum, AbsInt)
        Res = SystemOperate.Device_FEASA.GetWavlength(ChNum, Wave)
        Res = SystemOperate.Device_FEASA.GetCxCy(ChNum, Cx, Cy)
        Return Res
    End Function

    Private Sub UpdataDGV(ByVal Num As Integer, ByVal H() As Double, ByVal S() As Integer, ByVal I() As Integer, ByVal AbsI() As Double, ByVal Wav() As Integer, ByVal Cx() As Double, ByVal Cy() As Double)
        DGVResult.Rows.Clear()
        For t As Integer = 0 To Num - 1
            DGVResult.Rows.Add()
            DGVResult.Rows(t).Cells(0).Value = t + 1
            If H.Count > 0 Then
                DGVResult.Rows(t).Cells(1).Value = H(t).ToString
            End If
            If S.Count > 0 Then
                DGVResult.Rows(t).Cells(2).Value = S(t).ToString
            End If
            If I.Count > 0 Then
                DGVResult.Rows(t).Cells(3).Value = I(t).ToString
            End If
            If AbsI.Count > 0 Then
                DGVResult.Rows(t).Cells(4).Value = AbsI(t).ToString
            End If
            If Wav.Count > 0 Then
                DGVResult.Rows(t).Cells(5).Value = Wav(t).ToString
            End If
            If Cx.Count > 0 Then
                DGVResult.Rows(t).Cells(6).Value = Cx(t).ToString
            End If
            If Cy.Count > 0 Then
                DGVResult.Rows(t).Cells(7).Value = Cy(t).ToString
            End If
        Next

    End Sub

    ''' <summary>
    ''' Use Daisy Chain Mode to transfer another Feasa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub Btn_ChangeFeasa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim FeasaName As String = TextFeasaChange.Text
        Dim Res As Boolean = SystemOperate.Device_FEASA.Busfree
        Res = SystemOperate.Device_FEASA.BusgetFeasa(FeasaName)
        If Res Then
            Btn_ChangeFeasa.BackColor = Color.Green
        Else
            Btn_ChangeFeasa.BackColor = Color.Red
        End If
    End Sub

    Private Sub Btn_Validate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Validate.Click
        'ValidataFeasa(SiteNo, Code_Bitron)
        Dim Site = SiteNo 'Support use Site No
        Dim CodeBitron = 30100646 'Suport use CodeBitron 30100646
        Dim FeasaUsed As List(Of String) = GetAllUsedFeasa(Site, CodeBitron) 'Get All use feasa
        Dim FeasaAddr As Integer = GetUsedFeasaAddr(FeasaUsed) 'Get Addr
        Dim Res = SystemOperate.Device_FEASA.OpenCom(FeasaAddr) 'Open Feasa
        Res = CollectFeasaValue(FeasaUsed)
        Res = SystemOperate.Device_FEASA.CloseCom()
        StartCheckLEDs(Site, CodeBitron)
    End Sub

    Private Sub Btn_StartCalibration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_StartCalibration.Click
        Dim Site = SiteNo 'Support use Site No
        Dim CodeBitron = 30100646 'Suport use CodeBitron 30100646
        Dim FeasaUsed As List(Of String) = GetAllUsedFeasa(Site, CodeBitron) 'Get All use feasa
        Dim FeasaAddr As Integer = GetUsedFeasaAddr(FeasaUsed) 'Get Addr
        Dim Res = SystemOperate.Device_FEASA.OpenCom(FeasaAddr) 'Open Feasa
        StartCalibrationLEDs(SiteNo, CodeBitron, TextBoxFile.Text)
        Res = SystemOperate.Device_FEASA.CloseCom()
    End Sub

    Public Function StartCalibrationLEDs(ByVal SiteNo As Integer, ByVal Code_Bitron As String, ByVal sPath As String) As Boolean
        'Dim FileName As String = AppDomain.CurrentDomain.BaseDirectory + "FeasaCalibration.CSV"
        Dim FileName As String = sPath
        Dim delimiter As String = ","
        Dim fields As String()
        Dim LedMeasure As New Hashtable
        Using parser As New TextFieldParser(FileName)
            parser.SetDelimiters(delimiter)
            While Not parser.EndOfData
                fields = parser.ReadFields()
                LedMeasure.Add(fields(0), fields)
            End While
        End Using

        For Each Ver In MyConf.VERSIONIS
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
    ''' Get all FeasaUse in test
    ''' </summary>
    ''' <param name="SiteNo"></param>
    ''' <param name="Code_Biton"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllUsedFeasa(ByVal SiteNo As Integer, ByVal Code_Biton As String) As List(Of String)
        Dim XX = From C In MyConf.VERSIONIS Where C.CodeBitron = Code_Biton Select C
        If XX.Count > 0 Then
            Dim f As List(Of String) = (From c In XX(0).Sites(SiteNo - 1).SITE Select c.Name Distinct).ToList
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

    Public Function GetFeasaAddr(ByVal NodeFeasa As String) As Integer
        Dim Addr As Integer = -1
        Dim XX = From C In MyConf.GENERAL.Name_Feasa Where C.Name = NodeFeasa Select C
        If XX.Count > 0 Then
            Addr = XX(0).Addr
        End If
        Return Addr
    End Function

    Public Function GetCurrentFeasaIndex(ByVal FeasaName) As Integer
        Dim Index As Integer = -1
        Dim XX = From C In MyConf.GENERAL.Name_Feasa Where C.Name = FeasaName Select C
        If XX.Count > 0 Then
            Index = XX(0).Index
        End If
        Return Index
    End Function

    Public Function StartCheckLEDs(ByVal SiteNo As Integer, ByVal Code_Bitron As String) As Boolean
        For Each Ver In MyConf.VERSIONIS
            If Ver.CodeBitron = Code_Bitron Then 'Select Product(Correspond code bitron)
                For Each stp In Ver.Sites        'Select Site Steps
                    If stp.SiteNo = SiteNo Then
                        For Each j In stp.SITE
                            Dim CurrentIndex As Integer = GetCurrentFeasaIndex(j.Name)
                            Dim Ch As Integer = j.CH - 1
                            Dim TempRegisterVF As Double = 0
                            HoldRegisterRead(j.VFRegister, TempRegisterVF, "")
                            ValidateVF(j.Name, SiteNo, j.BinName, j.Code, TempRegisterVF, j.VFOffset)
                            ValidateHSI(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).H(Ch), FeasaCollectList(CurrentIndex).S(Ch), FeasaCollectList(CurrentIndex).I(Ch), j.HueOffset, j.SatOffset, j.IntensityOffset)
                            ValidateAbsIntensity(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).AbsI(Ch), j.IntensityOffset)
                            ValidateWavelength(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).Wave(Ch), j.DominantWavOffset)
                            ValidateCXCY(j.LEDName, SiteNo, j.BinName, j.Code, FeasaCollectList(CurrentIndex).Cx(Ch), FeasaCollectList(CurrentIndex).Cy(Ch), j.CxOffset, j.CyOffset)
                        Next
                    End If
                Next
            End If
        Next
    End Function

    Public Function ValidateHSI(ByVal LEDName As String, ByVal NumSite As String, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetHSI_H As Double, ByVal FeasaGetHSI_S As Integer, ByVal FeasaGetHSI_I As Integer, Optional ByVal h_Offset As Double = 0, Optional ByVal s_Offset As Integer = 0, Optional ByVal i_Offset As Integer = 0) As Boolean
        If SiteResultRead(NumSite) = TEST_RESULT_PASS Then
            Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
            Dim TeminalHSI_H As Double = FeasaGetHSI_H + h_Offset
            Dim TeminalHSI_S As Integer = FeasaGetHSI_S + s_Offset
            Dim TeminalHSI_I As Int32 = FeasaGetHSI_I + i_Offset
            If xx.Count > 0 Then
                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
                If t.Count > 0 Then
                    If t(0).Hue_SH >= TeminalHSI_H And t(0).Hue_SL <= TeminalHSI_H Then
                        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_H, t(0).Hue_SH, t(0).Hue_SL, "_HUE")
                    Else
                        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_H, t(0).Hue_SH, t(0).Hue_SL, "_HUE")
                    End If

                    If t(0).Sat_SH >= TeminalHSI_S And t(0).Sat_SL <= TeminalHSI_S Then
                        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_S, t(0).Sat_SH, t(0).Sat_SL, "_SAT")
                    Else
                        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_S, t(0).Sat_SH, t(0).Sat_SL, "_SAT")
                    End If

                    If t(0).Intensity_SH >= TeminalHSI_I And t(0).Intensity_SL <= TeminalHSI_I Then
                        Call PrintResultMsg(PASS, NumSite, LEDName, TeminalHSI_I, t(0).Intensity_SH, t(0).Intensity_SL, "_INTENSITY")
                    Else
                        Call PrintResultMsg(FAIL, NumSite, LEDName, TeminalHSI_I, t(0).Intensity_SH, t(0).Intensity_SL, "_INTENSITY")
                    End If
                Else
                    MsgBox("Not Fund " & BinName & "or " & Code & "in Config!")
                End If
            Else
                MsgBox("HSI Config Error!")
            End If
        End If
    End Function

    Public Function ValidateAbsIntensity(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetAbsint As Double, Optional ByVal Offset_Abs As Double = 0) As Boolean
        If SiteResultRead(NumSite) = TEST_RESULT_PASS Then
            Dim TerminalAbs As Double = FeasaGetAbsint + Offset_Abs
            Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
            If xx.Count > 0 Then
                Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
                If t(0).Intensity_SH >= TerminalAbs And t(0).Intensity_SL <= TerminalAbs Then
                    PrintResultMsg(PASS, NumSite, LEDName, TerminalAbs, t(0).Intensity_SH, t(0).Intensity_SL, "_AbsIntensity")
                Else
                    PrintResultMsg(FAIL, NumSite, LEDName, TerminalAbs, t(0).Intensity_SH, t(0).Intensity_SL, "_AbsIntensity")
                End If
            Else
                MsgBox("HSI Config Error!")
            End If
        End If
    End Function

    Public Function ValidateWavelength(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetWav As Double, Optional ByVal Offset_Wav As Double = 0) As Boolean
        Dim TerminalWav As Double = FeasaGetWav + Offset_Wav
        If FailFlag = FAIL Then Return False
        Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
        If xx.Count > 0 Then
            Dim t = From D In xx(0).BinRanges Where D.BinName = BinName Select D
            If t(0).DominantWav_SH >= TerminalWav And t(0).DominantWav_SL <= TerminalWav Then
                PrintResultMsg(PASS, NumSite, LEDName, TerminalWav, t(0).DominantWav_SH, t(0).DominantWav_SL, "_DominantWave")
            Else
                PrintResultMsg(FAIL, NumSite, LEDName, TerminalWav, t(0).DominantWav_SH, t(0).DominantWav_SL, "_DominantWave")
            End If
        Else
            MsgBox("HSI Config Error!")
        End If
    End Function

    Public Function ValidateCXCY(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetCx As Double, ByVal FeasaGetCy As Double, Optional ByVal Offset_Cx As Double = 0, Optional ByVal Offset_Cy As Double = 0) As Boolean
        Dim Res As Boolean = False
        Dim coor As Coordinate

        Dim TeminalCx As Double = FeasaGetCx + Offset_Cx
        Dim TeminalCy As Double = FeasaGetCy + Offset_Cy

        Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
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
            PrintResultMsg(PASS, NumSite, LEDName, FormatFeasaCxCy(TeminalCx), FormatFeasaCxCy(TeminalCy), "-", "-", "_Cxcy")
        Else
            PrintResultMsg(FAIL, NumSite, LEDName, FormatFeasaCxCy(TeminalCx), FormatFeasaCxCy(TeminalCy), "-", "-", "_Cxcy")
        End If
    End Function

    Public Function FormatFeasaCxCy(ByVal Val As Double) As String
        Return Val.ToString("0.0000")
    End Function

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

    Public Function ValidateVF(ByVal LEDName As String, ByVal NumSite As Integer, ByVal BinName As String, ByVal Code As String, ByVal FeasaGetVF As Double, Optional ByVal Offset_Vf As Double = 0) As Boolean
        Dim TerminalVF As Double = FeasaGetVF + Offset_Vf
        Dim Res As Boolean = False
        Dim xx = From C In MyConf.LEDS Where C.Code = Code Select C
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
            SystemOperate.Device_FEASA.Capture(ClassFEASA.EBrightLevel.Auto)
            ChNum = GetFeasaNum(obj)
            FeasaCollect.FeasaName = obj
            Res = GetAllFeasaPara(ChNum, FeasaCollect.H, FeasaCollect.S, FeasaCollect.I, FeasaCollect.AbsI, FeasaCollect.Wave, FeasaCollect.Cx, FeasaCollect.Cy)
            If Not Res Then
                Return False
            End If
            FeasaCollectList.Add(FeasaCollect)
        Next
        Return Res
    End Function

    Public Sub PrintRemarkFail(ByVal NumTest As Integer, ByVal Remark As String, ByVal TpList As String, ByVal Valore As String, ByVal Sh_Max As String, ByVal Sh_Min As String, ByVal Unit As String, ByVal site As Integer)
        MsgDispService("FVT TEST#" + NumTest.ToString + "      FAIL (-)", 0)
        MsgDispService(Remark + " /" + site.ToString, 0)
        MsgDispService("TP: " + TpList, 0)
        MsgDispService("VALUE: " + Valore.ToString + " " + Unit, 0)
        MsgDispService("Max: " + Sh_Max.ToString + Unit + " / Min: " + Sh_Min.ToString + Unit, 0)
        MsgDispService(Chr(13), 0)
    End Sub

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


    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxFile.Click
        Dim OpenFile As New OpenFileDialog
        OpenFile.FileName = ".csv"
        If OpenFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBoxFile.Text = OpenFile.FileName
        End If
    End Sub

    Private Sub Btn_LoadPara_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_LoadPara.Click
        If myPara.IsCheckRes Then
            CheckResistor("1", "34619299003c20102860")
        Else
            MsgBox("Please Check Paraconfig.xml enable Res or not!")
        End If
    End Sub

#Region "Check Resistor"

    Public Sub CheckResistor(ByVal SiteNo As String, ByVal DataMatrxi As String)
        StartCheckRess(SiteNo)
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
        'ValidateRes(SiteNo, "M3-JZ", "15701280", 1.2, 1.3, 1.0)
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
        StartCheckCurrs(SiteNo)
    End Sub

    Public Function StartCheckCurrs(ByVal SiteNo As Integer) As Boolean
        For Each stp In myPara.TestCurStep        'Select Site Steps
            Dim TempV As Double
            Dim GRes As Double
            RunAnlTaskLabel("", stp.RunLabel, stp.RunLabel)
            DvmRead(DVM1, TempV, MU_VOLT)
            GRes = TempV * 1000 '统一单位
            Dim BinName As String = GetBINAccordingLabelCur(stp.Combination)
            ValidateCur(SiteNo, BinName, GRes)
        Next
    End Function

    Public Function GetBINAccordingLabelCur(ByVal StrCombination As String) As String
        Dim RetBin As String = ""
        Dim TComBin = GetCurBINFromLabel(StrCombination) '获取有几个LED灯组成BIN,相同LED使用同一个Res BIN
        For Each TC In TComBin
            For Each Fj In TraxResult
                If Fj.LEDName = TC Then
                    RetBin = RetBin & "-" & Fj.BinCode.Substring(0, 2)
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





    Private Sub Btn_FujiTrax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_FujiTrax.Click
        TxtFuji.Text = ""
        TraxResult = myTraceHelper.GetFuJiTraxDatas(TextBoxFuji.Text)
        If TraxResult.Count = 0 Then
        Else
            For Each t In TraxResult
                TxtFuji.AppendText(t.SN & " " & t.LEDName & " " & t.PartNo & " " & t.BinCode & " " & t.TestDate & vbCrLf)
            Next
        End If
    End Sub

    Private Sub Btn_GetCur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_GetCur.Click

        If myPara.IsCheckCur Then
            StartCheckCurrs(SiteNo)
        Else
            MsgBox("Please Check Paraconfig.xml enable Cur or not!")
        End If
    End Sub

    Private Sub BtnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConfig.Click
        System.Diagnostics.Process.Start(TextBoxXMLTool.Text, "FunzConf.xml")
    End Sub

    Private Sub TextBoxXMLTool_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxXMLTool.TextChanged
       
    End Sub

    Private Sub TextBoxXMLTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxXMLTool.Click
        Dim OpenFile As New OpenFileDialog
        OpenFile.FileName = ".exe"
        If OpenFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBoxXMLTool.Text = OpenFile.FileName
            Dim wr As System.IO.StreamWriter
            'wr.Write(TextBoxXMLTool.Text, False)
        End If
    End Sub

    Private Sub Btn_OpenCom_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_OpenCom.Click
        Dim CodeBitron As String 'Suport use CodeBitron 30100646

        CodeBitron = "30100646_30100647"

        Dim FeasaUsed As List(Of String) = GetAllUsedFeasa(SiteNo, CodeBitron) 'Get All use feasa
        Dim FeasaAddr As Integer = GetUsedFeasaAddr(FeasaUsed) 'Get Addr

        ComOpenState = Not ComOpenState
        PicComOpen.Image = My.Resources.LEDYellow
        Application.DoEvents()

        Dim Res As Boolean
        If ComOpenState Then
            Res = SystemOperate.Device_FEASA.OpenCom(FeasaAddr)
        Else
            Res = SystemOperate.Device_FEASA.CloseCom(FeasaAddr)
        End If
        Call SetState(PicComOpen, ComOpenState, Res)
    End Sub


    Private Sub BtnGetSerialNo_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGetSerialNo.Click
        Dim StrFeasa As String = ""
        Dim Res As Boolean = SystemOperate.Device_FEASA.GetSerial(StrFeasa)
        If Res Then
            TextFeasaNo.Text = StrFeasa
        Else
            TextFeasaNo.Text = "Not Found"
        End If
    End Sub

    Private Sub Btn_ChangeFeasa_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ChangeFeasa.Click
        Dim FeasaName As String = TextFeasaChange.Text
        Dim Res As Boolean = SystemOperate.Device_FEASA.Busfree
        Res = SystemOperate.Device_FEASA.BusgetFeasa(FeasaName)
        If Res Then
            Btn_ChangeFeasa.BackColor = Color.Green
        Else
            Btn_ChangeFeasa.BackColor = Color.Red
        End If
    End Sub

    Private Sub Btn_Capture_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Capture.Click
        Dim Cap As ClassFEASA.EBrightLevel = [Enum].Parse(GetType(ClassFEASA.EBrightLevel), ComboBrightLevel.SelectedItem)
        Btn_Capture.BackColor = Color.Yellow
        Dim Res As Boolean = SystemOperate.Device_FEASA.Capture(Cap)
        If Res Then
            Btn_Capture.BackColor = Color.Green
        Else
            Btn_Capture.BackColor = Color.Red
        End If
    End Sub

    Private Sub Btn_GetAllValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_GetAllValues.Click
        Dim ChNum As Integer = 0
        Dim H() As Double
        Dim S() As Integer
        Dim I() As Integer
        Dim AbsInt() As Double
        Dim Wave() As Integer
        Dim Cx() As Double
        Dim Cy() As Double

        Dim Res As Boolean = GetAllFeasaPara(ChNum, H, S, I, AbsInt, Wave, Cx, Cy)

        UpdataDGV(ChNum, H, S, I, AbsInt, Wave, Cx, Cy)
    End Sub

End Class
