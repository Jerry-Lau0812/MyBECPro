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

    Dim TraxResult() As FujiTraxData

    Dim LEDCount As Integer

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

        'ICTTestFlag = UseSiteRead(NumSite)

        'If ICTTestFlag = RESULT_FAIL Then
        '    FailFlag = FAIL
        'End If


        '获取产品二维码
        Call CheckBarcode(PCBDatamatrix)

        '获取产品号
        'ReadVariant()
        Call CheckVarient(ProductNo)

        '测试Feasa
        Call CheckFeasa(Numbay, NumSite, ProductNo)

        '************Check Data From Fujitrax-Start*************''
        'PrincipaleMain()
        'RunDebugConsole() '''if debug on in funzconf.xml

        'Check Turn Tail Current
        'FVTFuncCur()

        'Check LED Resistor 
        'FVTTestResitor()

        'Check Led Parameters
        'Call FeasaTest()

        If FailFlag = PASS Then
            'MsgPrintLog("FUCTIONAL TEST PASS...UUT" & NumSite, 0)
            SiteResultWrite(NumSite, RESULT_PASS)
        Else
            'MsgPrintLog("FUCTIONAL TEST Fail...UUT" & NumSite, 0)
            SiteResultWrite(NumSite, RESULT_FAIL)
        End If

        'RunAnlTaskLabel("", "LD0ON", "LD0ON")

        TplanResultSet(FailFlag)

        Testplan = 1

    End Function


    Public Sub FeasaTest()
        If FailFlag = PASS Then
            'Dim FeasaUsed As List(Of String) = 
        Else

        End If
    End Sub


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
    ''' Feasa测试
    ''' </summary>
    ''' <param name="SiteNo"></param>
    ''' <param name="ProgNo"></param>
    ''' <remarks></remarks>
    Public Sub CheckFeasa(ByVal BayNo As Integer, ByVal SiteNo As Integer, ByVal ProgNo As String)
        Dim FeasaUsed As List(Of String)
        Dim FeasaAddr As Integer
        'ProgNo = "30100646"
        MsgPrintLog(ProgNo, 0)
        If FailFlag = PASS Then
            FeasaUsed = GetAllUsedFeasa(SiteNo, ProgNo)
            'For Each t In FeasaUsed
            '    MsgPrintLog(t, 0)
            'Next

            FeasaAddr = GetUsedFeasaAddr(FeasaUsed)

            Dim Res = SystemOperate.Device_FEASA.OpenCom(FeasaAddr)

            If Res Then
                MsgPrintLog(FeasaAddr & "Open Success!", 0)
            End If
            'Dim Res = True
            If Res Then
                FailFlag = PASS
                MsgPrintLog("Bay:" & BayNo & " NumSite:" & SiteNo & " FeasaComAddr:" & FeasaAddr, 0)
            Else
                FailFlag = FAIL
            End If
        End If
    End Sub

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
