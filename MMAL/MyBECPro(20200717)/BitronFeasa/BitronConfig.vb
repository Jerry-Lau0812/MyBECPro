Imports System.Xml

''' <summary>
''' 提供底层XML节点分类
''' </summary>
''' <remarks></remarks>
Public Class BitronConfig
    Dim Xdoc As XmlDocument
    Private myXml As New XMLBase
    Private myGeneral As New CGeneral
    Private myHardware As IList(Of CHW) = New List(Of CHW)
    Private myLed As IList(Of CLED) = New List(Of CLED)
    Private myVersioni As IList(Of CVersioni) = New List(Of CVersioni)

    Public Property GENERAL As CGeneral
        Get
            Return myGeneral
        End Get
        Set(ByVal value As CGeneral)
            myGeneral = value
        End Set
    End Property

    Public Property HARDWARES As List(Of CHW)
        Get
            Return myHardware
        End Get
        Set(ByVal value As List(Of CHW))
            myHardware = value
        End Set
    End Property

    Public Property LEDS As List(Of CLED)
        Get
            Return myLed
        End Get
        Set(ByVal value As List(Of CLED))
            myLed = value
        End Set
    End Property

    Public Property VERSIONIS As List(Of CVersioni)
        Get
            Return myVersioni
        End Get
        Set(ByVal value As List(Of CVersioni))
            myVersioni = value
        End Set
    End Property

    Public Sub New(Optional ByVal StrXmlPath As String = "\FunzConf.XML")
        Xdoc = New XmlDocument()
        'Xdoc.Load(AppDomain.CurrentDomain.BaseDirectory & StrXmlPath)
        Xdoc.Load(StrXmlPath)
        myLed.Clear()
        myHardware.Clear()
        myLed.Clear()
    End Sub

    Public Sub InitialAllInfo()
        Try
            LoadGeneralPara()
            LoadHardwarePara()
            LoadLEDPara()
            LoadVESIONI()
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Public Sub LoadGeneralPara()
        Try
            'General Parameter Initial
            With myGeneral
                .GetBinCodeFromFujiTrax = CType(myXml.RicercaXML("GENERAL", "GetBinCodeFromFujiTrax"), Integer)
                .UseCompleteCode = CType(myXml.RicercaXML("GENERAL", "UseCompleteCode"), String)
                .Barcode_On = CType(myXml.RicercaXML("GENERAL", "BARCODE_ON"), Integer)
                .Figure = CType(myXml.RicercaXML("GENERAL", "BARCODE_ON"), Integer)
                .NameSystem = CType(myXml.RicercaXML("GENERAL", "NAMESYSTEM"), String)
                .DB_Connection = CType(myXml.RicercaXML("GENERAL", "DB_CONNECTION_STRING"), String)
                .Debug = CType(myXml.RicercaXML("GENERAL", "DEBUG"), Integer)
                .SerialGolden = CType(myXml.RicercaXML("GENERAL", "SERIALGOLDEN"), String)
                .Fase = CType(myXml.RicercaXML("GENERAL", "FASE"), String)
                .ID_Rack = CType(myXml.RicercaXML("GENERAL", "ID_RACK"), Integer)
                .Lan = CType(myXml.RicercaXML("GENERAL", "LAN"), Integer)
                .ComNum = CType(myXml.RicercaXML("GENERAL", "COMNUM"), Integer)
                .FeasaNum = CType(myXml.RicercaXML("GENERAL", "FEASANUM"), Integer)
            End With
            Call LoadGeneralFeasaNameParas()
        Catch
            MsgBox("LoadGeneralPara Failed!")
        End Try
    End Sub

    Private Sub LoadGeneralFeasaNameParas()
        Try
            Dim filePath As String = "FUNZCONF/GENERAL/NAME_FEASAS"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim NameFeasa As New Name_Feasa
                    For Each childNode As XmlNode In PreNode.ChildNodes
                        Select Case childNode.Name
                            Case "INDEX"
                                NameFeasa.Index = childNode.InnerText()
                            Case "Addr"
                                NameFeasa.Addr = childNode.InnerText()
                            Case "NAME"
                                NameFeasa.Name = childNode.InnerText()
                            Case "NUMCH"
                                NameFeasa.NumCh = childNode.InnerText()
                            Case "SIDE"
                                NameFeasa.Side = childNode.InnerText()
                            Case Else
                        End Select
                    Next
                    myGeneral.Name_Feasa.Add(NameFeasa)
                Next
            End If
        Catch ex As Exception
            MsgBox("LoadGeneralFeasaNameParas Failed!")
        End Try
    End Sub

    Public Sub LoadHardwarePara()
        Try
            Dim filePath As String = "FUNZCONF/HARDWARES"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim chardware As New CHW
                    chardware.AttrType = PreNode.Attributes("type").InnerXml
                    chardware.AttrName = PreNode.Attributes("name").InnerXml
                    chardware.AttrDesc = PreNode.Attributes("desc").InnerXml
                    For Each childNode As XmlNode In PreNode.ChildNodes
                        Dim cDev As New CDevice
                        cDev.Index = childNode.Attributes("index").InnerXml
                        cDev.Name = childNode.Attributes("name").InnerXml
                        cDev.Addr = childNode.Attributes("addr").InnerXml
                        For Each cchilNode As XmlNode In childNode
                            Select Case cchilNode.Name
                                Case "PORT"
                                    cDev.Port = cchilNode.InnerText()
                                Case "VELOCITA"
                                    cDev.Velocita = cchilNode.InnerText()
                                Case "PATITA"
                                    cDev.Parita = cchilNode.InnerText()
                                Case "NUM_BIT"
                                    cDev.Num_Bit = cchilNode.InnerText()
                                Case "STOP_BIT"
                                    cDev.Stop_Bit = cchilNode.InnerText()
                                Case Else
                            End Select
                        Next
                        chardware.Device.Add(cDev)
                    Next
                    myHardware.Add(chardware)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub LoadLEDPara()
        Try
            Dim filePath As String = "FUNZCONF/LEDS"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim cLED As New CLED
                    For Each childNode As XmlNode In PreNode.ChildNodes
                        Select Case childNode.Name
                            Case "CODE"
                                cLED.Code = childNode.InnerText()
                            Case "COLOR"
                                cLED.Color = childNode.InnerText()
                            Case "BIN_RANGE"
                                Dim cRange As New BinRange
                                For Each cchildNode As XmlNode In childNode.ChildNodes
                                    Select Case cchildNode.Name
                                        Case "BIN_NAME"
                                            cRange.BinName = cchildNode.InnerText()
                                        Case "HUE_SL"
                                            cRange.Hue_SL = cchildNode.InnerText()
                                        Case "HUE_SH"
                                            cRange.Hue_SH = cchildNode.InnerText()
                                        Case "SAT_SL"
                                            cRange.Sat_SL = cchildNode.InnerText()
                                        Case "SAT_SH"
                                            cRange.Sat_SH = cchildNode.InnerText()
                                        Case "ENABLE_CXCY"
                                            cRange.EnableCXCY = cchildNode.InnerText()
                                        Case "CXCY_RANGE_AREA"
                                            Dim cCXCYArea As New CXCYRangeArea
                                            For Each ccchildNode As XmlNode In cchildNode.ChildNodes
                                                Select Case ccchildNode.Name
                                                    Case "POINT"
                                                        cCXCYArea.Point = ccchildNode.InnerText
                                                    Case "CX"
                                                        cCXCYArea.CX = ccchildNode.InnerText
                                                    Case "CY"
                                                        cCXCYArea.CY = ccchildNode.InnerText
                                                    Case "NOTE"
                                                        cCXCYArea.NOTE = ccchildNode.InnerText
                                                    Case Else
                                                End Select
                                            Next
                                            cRange.CXCYRange.Add(cCXCYArea)
                                        Case "INTENSITY_SL"
                                            cRange.Intensity_SL = cchildNode.InnerText()
                                        Case "INTENSITY_SH"
                                            cRange.Intensity_SH = cchildNode.InnerText()
                                        Case "DOMINANTWAVE_SL"
                                            cRange.DominantWav_SL = cchildNode.InnerText()
                                        Case "DOMINANTWAVE_SH"
                                            cRange.DominantWav_SH = cchildNode.InnerText()
                                        Case "VF_SL"
                                            cRange.VF_SL = cchildNode.InnerText()
                                        Case "VF_SH"
                                            cRange.VF_SH = cchildNode.InnerText()
                                        Case Else
                                    End Select
                                Next
                                cLED.BinRanges.Add(cRange)
                            Case "RANGE_BRIGHTNESS"
                                cLED.RangeBright = childNode.InnerText()
                            Case Else
                        End Select
                    Next
                    myLed.Add(cLED)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub LoadVESIONI()
        Try
            Dim filePath As String = "FUNZCONF/VERSIONIS"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim cVer As New CVersioni
                    For Each cchildNode As XmlNode In PreNode.ChildNodes
                        Select Case cchildNode.Name
                            Case "ID"
                                cVer.ID = cchildNode.InnerText
                            Case "DESCRIZIONE"
                                cVer.Descrizione = cchildNode.InnerText
                            Case "CODE_BITRON"
                                cVer.CodeBitron = cchildNode.InnerText
                            Case "CODICE_CLIENTE"
                                cVer.CodiceClient = cchildNode.InnerText
                            Case "CODICE_ECLIPSE"
                                cVer.CodiceEclips = cchildNode.InnerText
                            Case "BARCODE"
                                cVer.Barcode = cchildNode.InnerText
                            Case "SEQUENZA"
                                cVer.Sequenza = cchildNode.InnerText
                            Case "SITES"
                                For Each ccchildNode As XmlNode In cchildNode
                                    Dim csi As New CSite
                                    csi.Description = ccchildNode.Attributes("Description").InnerText
                                    csi.Enable = ccchildNode.Attributes("Enable").InnerText
                                    csi.SiteNo = ccchildNode.Attributes("SITENO").InnerText
                                    csi.FeasaAddr = ccchildNode.Attributes("FEASAAddr").InnerText
                                    For Each cPartNode As XmlNode In ccchildNode
                                        Dim Cparts As New CParts
                                        Cparts.Enable = cPartNode.Attributes("Enable").InnerText
                                        Cparts.Index = cPartNode.Attributes("INDEX").InnerText
                                        Cparts.TaskLabel = cPartNode.Attributes("TaskLabel").InnerText

                                        For Each cFesNode As XmlNode In cPartNode
                                            Dim cfes As New CFeasa
                                            cfes.Name = cFesNode.Attributes("Name").InnerText
                                            For Each cfesNameNode As XmlNode In cFesNode
                                                Select Case cfesNameNode.Name
                                                    Case "ID"
                                                        cfes.ID = cfesNameNode.InnerText
                                                    Case "CH"
                                                        cfes.CH = cfesNameNode.InnerText
                                                    Case "LEDNAME"
                                                        cfes.LEDName = cfesNameNode.InnerText
                                                    Case "ENABLE"
                                                        cfes.Enable = cfesNameNode.InnerText
                                                    Case "ENABLECXCY"
                                                        cfes.EnableCxCy = cfesNameNode.InnerText
                                                    Case "ENABLEHUE"
                                                        cfes.EnableHue = cfesNameNode.InnerText
                                                    Case "ENABLESAT"
                                                        cfes.EnableSat = cfesNameNode.InnerText
                                                    Case "ENABLEABSINT"
                                                        cfes.EnableAbsInt = cfesNameNode.InnerText
                                                    Case "ENABLEWAV"
                                                        cfes.EnableWav = cfesNameNode.InnerText
                                                    Case "ENABLEVF"
                                                        cfes.EnableVF = cfesNameNode.InnerText
                                                    Case "BINNAME"
                                                        cfes.BinName = cfesNameNode.InnerText
                                                    Case "CODE"
                                                        cfes.Code = cfesNameNode.InnerText
                                                    Case "SITE"
                                                        cfes.Site = cfesNameNode.InnerText
                                                    Case "HUE_OFFSET"
                                                        cfes.HueOffset = cfesNameNode.InnerText
                                                    Case "SAT_OFFSET"
                                                        cfes.SatOffset = cfesNameNode.InnerText
                                                    Case "CX_OFFSET"
                                                        cfes.CxOffset = cfesNameNode.InnerText
                                                    Case "CY_OFFSET"
                                                        cfes.CyOffset = cfesNameNode.InnerText
                                                    Case "INTENSITY_OFFSET"
                                                        cfes.IntensityOffset = cfesNameNode.InnerText
                                                    Case "DOMINANTWAVE_OFFSET"
                                                        cfes.DominantWavOffset = cfesNameNode.InnerText
                                                    Case "VF_REGISTER"
                                                        cfes.VFRegister = cfesNameNode.InnerText
                                                    Case "VF_OFFSET"
                                                        cfes.VFOffset = cfesNameNode.InnerText
                                                    Case "POWER_START"
                                                        cfes.PowerStart = cfesNameNode.InnerText
                                                    Case "POWER_END"
                                                        cfes.PowerEnd = cfesNameNode.InnerText
                                                    Case "TON"
                                                        cfes.Ton = cfesNameNode.InnerText
                                                    Case "NOTE"
                                                        cfes.Note = cfesNameNode.InnerText
                                                End Select
                                            Next
                                            Cparts.SITE.Add(cfes)
                                        Next
                                        csi.PART.Add(Cparts)
                                    Next
                                    cVer.Sites.Add(csi)
                                Next
                        End Select
                    Next
                    myVersioni.Add(cVer)
                Next
            End If
        Catch

        End Try
    End Sub

#Region "General"
    Public Class CGeneral
        Private mGetBinCodeFromFujiTrax As Integer

        Private mUseCompleteCode As String

        Private mBarcode_On As Integer
        Private mFigure As Integer
        Private mNameSystem As String
        Private mDB_Connection As String
        Private mDebug As Integer
        Private mSerailGolden As String
        Private mFASE As String
        Private mID_Rack As Integer
        Private mLan As Integer
        Private mComNum As Integer
        Private mFeasaNum As Integer
        Private mName_Feasas As List(Of Name_Feasa) = New List(Of Name_Feasa)


        Public Property GetBinCodeFromFujiTrax As Integer
            Get
                Return mGetBinCodeFromFujiTrax
            End Get
            Set(ByVal value As Integer)
                mGetBinCodeFromFujiTrax = value
            End Set
        End Property

        Public Property UseCompleteCode As String
            Get
                Return mUseCompleteCode
            End Get
            Set(ByVal value As String)
                mUseCompleteCode = value
            End Set
        End Property


        Public Property Barcode_On As Integer
            Get
                Return mBarcode_On
            End Get
            Set(ByVal value As Integer)
                mBarcode_On = value
            End Set
        End Property

        Public Property Figure As Integer
            Get
                Return mFigure
            End Get
            Set(ByVal value As Integer)
                mFigure = value
            End Set
        End Property

        Public Property NameSystem As String
            Get
                Return mNameSystem
            End Get
            Set(ByVal value As String)
                mNameSystem = value
            End Set
        End Property

        Public Property DB_Connection As String
            Get
                Return mDB_Connection
            End Get
            Set(ByVal value As String)
                mDB_Connection = value
            End Set
        End Property

        Public Property Debug As Integer
            Get
                Return mDebug
            End Get
            Set(ByVal value As Integer)
                mDebug = value
            End Set
        End Property

        Public Property SerialGolden As String
            Get
                Return mSerailGolden
            End Get
            Set(ByVal value As String)
                mSerailGolden = value
            End Set
        End Property

        Public Property Fase As String
            Get
                Return mFASE
            End Get
            Set(ByVal value As String)
                mFASE = value
            End Set
        End Property

        Public Property ID_Rack As Integer
            Get
                Return mID_Rack
            End Get
            Set(ByVal value As Integer)
                mID_Rack = value
            End Set
        End Property

        Public Property Lan As Integer
            Get
                Return mLan
            End Get
            Set(ByVal value As Integer)
                mLan = value
            End Set
        End Property

        Public Property ComNum As Integer
            Get
                Return mComNum
            End Get
            Set(ByVal value As Integer)
                mComNum = value
            End Set
        End Property

        Public Property FeasaNum As Integer
            Get
                Return mFeasaNum
            End Get
            Set(ByVal value As Integer)
                mFeasaNum = value
            End Set
        End Property

        Public Property Name_Feasa As List(Of Name_Feasa)
            Get
                Return mName_Feasas
            End Get
            Set(ByVal value As List(Of Name_Feasa))
                mName_Feasas = value
            End Set
        End Property

        Public Sub New()
            mName_Feasas.Clear()
        End Sub


    End Class
    Public Class Name_Feasa
        Private mAddr As Integer
        Private mIndex As Integer
        Private mNumber As Integer
        Private mName As String
        Private mNumCh As Integer
        Private mSide As String

        Public Property Index As Integer
            Get
                Return mIndex
            End Get
            Set(ByVal value As Integer)
                mIndex = value
            End Set
        End Property

        Public Property Addr As Integer
            Get
                Return mAddr
            End Get
            Set(ByVal value As Integer)
                mAddr = value
            End Set
        End Property

        Public Property Number As Integer
            Get
                Return mNumber
            End Get
            Set(ByVal value As Integer)
                mNumber = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property

        Public Property NumCh As Integer
            Get
                Return mNumCh
            End Get
            Set(ByVal value As Integer)
                mNumCh = value
            End Set
        End Property

        Public Property Side As String
            Get
                Return mSide
            End Get
            Set(ByVal value As String)
                mSide = value
            End Set
        End Property
    End Class
#End Region

#Region "HARDWARES"
    
    Public Class CHW
        Private mAttrType As String
        Private mAttrName As String
        Private mAttrDesc As String
        Private mDevice As List(Of CDevice) = New List(Of CDevice)
        Public Property AttrType As Integer
            Get
                Return mAttrType
            End Get
            Set(ByVal value As Integer)
                mAttrType = value
            End Set
        End Property

        Public Property AttrName As String
            Get
                Return mAttrName
            End Get
            Set(ByVal value As String)
                mAttrName = value
            End Set
        End Property

        Public Property AttrDesc As String
            Get
                Return mAttrDesc
            End Get
            Set(ByVal value As String)
                mAttrDesc = value
            End Set
        End Property
        Public Property Device As List(Of CDevice)
            Get
                Return mDevice
            End Get
            Set(ByVal value As List(Of CDevice))
                mDevice = value
            End Set
        End Property
        Public Sub New()
            mDevice.Clear()
        End Sub
    End Class

    Public Class CDevice


        Private mIndex As Integer
        Private mName As String
        Private mAddr As Integer
        Private mPort As Integer
        Private mVelocita As String
        Private mParita As String
        Private mNum_Bit As String
        Private mStop_Bit As String

        


        Public Property Index As Integer
            Get
                Return mIndex
            End Get
            Set(ByVal value As Integer)
                mIndex = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property

        Public Property Addr As Integer
            Get
                Return mAddr
            End Get
            Set(ByVal value As Integer)
                mAddr = value
            End Set
        End Property

        Public Property Port As Integer
            Get
                Return mPort
            End Get
            Set(ByVal value As Integer)
                mPort = value
            End Set
        End Property

        Public Property Velocita As String
            Get
                Return mVelocita
            End Get
            Set(ByVal value As String)
                mVelocita = value
            End Set
        End Property

        Public Property Parita As String
            Get
                Return mParita
            End Get
            Set(ByVal value As String)
                mParita = value
            End Set
        End Property

        Public Property Num_Bit As String
            Get
                Return mNum_Bit
            End Get
            Set(ByVal value As String)
                mNum_Bit = value
            End Set
        End Property

        Public Property Stop_Bit As String
            Get
                Return mStop_Bit
            End Get
            Set(ByVal value As String)
                mStop_Bit = value
            End Set
        End Property
    End Class
#End Region

#Region "LED"
    Public Class CXCYRangeArea
        Private mPoint As Integer
        Private mCx As Single
        Private mCy As Single
        Private mNote As String

        Public Property Point As Integer
            Get
                Return mPoint
            End Get
            Set(ByVal value As Integer)
                mPoint = value
            End Set
        End Property

        Public Property CX As Single
            Get
                Return mCx
            End Get
            Set(ByVal value As Single)
                mCx = value
            End Set
        End Property

        Public Property CY As Single
            Get
                Return mCy
            End Get
            Set(ByVal value As Single)
                mCy = value
            End Set
        End Property

        Public Property NOTE As String
            Get
                Return mNote
            End Get
            Set(ByVal value As String)
                mNote = value
            End Set
        End Property
    End Class

    Public Class BinRange
        Private mBinName As String
        Private mHue_SL As Double
        Private mHue_SH As Double
        Private mSat_SL As Double
        Private mSat_SH As Double
        Private mEnable_CxCy As Integer
        Private mIntensity_SL As Double
        Private mIntensity_SH As Double
        Private mDominantWav_SL As Double
        Private mDominantWav_SH As Double
        Private mVF_SL As Single
        Private mVF_SH As Single
        Private Note As String
        Private mCxCyranges As List(Of CXCYRangeArea) = New List(Of CXCYRangeArea)

        Public Property BinName As String
            Get
                Return mBinName
            End Get
            Set(ByVal value As String)
                mBinName = value
            End Set
        End Property

        Public Property Hue_SL As Double
            Get
                Return mHue_SL
            End Get
            Set(ByVal value As Double)
                mHue_SL = value
            End Set
        End Property

        Public Property Hue_SH As Double
            Get
                Return mHue_SH
            End Get
            Set(ByVal value As Double)
                mHue_SH = value
            End Set
        End Property

        Public Property Sat_SL As Double
            Get
                Return mSat_SL
            End Get
            Set(ByVal value As Double)
                mSat_SL = value
            End Set
        End Property

        Public Property Sat_SH As Double
            Get
                Return mSat_SH
            End Get
            Set(ByVal value As Double)
                mSat_SH = value
            End Set
        End Property

        Public Property EnableCXCY As Integer
            Get
                Return mEnable_CxCy
            End Get
            Set(ByVal value As Integer)
                mEnable_CxCy = value
            End Set
        End Property

        Public Property Intensity_SL As Double
            Get
                Return mIntensity_SL
            End Get
            Set(ByVal value As Double)
                mIntensity_SL = value
            End Set
        End Property

        Public Property Intensity_SH As Double
            Get
                Return mIntensity_SH
            End Get
            Set(ByVal value As Double)
                mIntensity_SH = value
            End Set
        End Property

        Public Property DominantWav_SL As Double
            Get
                Return mDominantWav_SL
            End Get
            Set(ByVal value As Double)
                mDominantWav_SL = value
            End Set
        End Property

        Public Property DominantWav_SH As Double
            Get
                Return mDominantWav_SH
            End Get
            Set(ByVal value As Double)
                mDominantWav_SH = value
            End Set
        End Property

        Public Property VF_SL As Single
            Get
                Return mVF_SL
            End Get
            Set(ByVal value As Single)
                mVF_SL = value
            End Set
        End Property

        Public Property VF_SH As Single
            Get
                Return mVF_SH
            End Get
            Set(ByVal value As Single)
                mVF_SH = value
            End Set
        End Property

        Public Property CXCYRange As List(Of CXCYRangeArea)
            Get
                Return mCxCyranges
            End Get
            Set(ByVal value As List(Of CXCYRangeArea))
                mCxCyranges = value
            End Set
        End Property

        Public Sub New()
            mCxCyranges.Clear()
        End Sub
    End Class

    Partial Class CLED
        Private mCode As String
        Private mColor As String
        Private mRangeBright As String
        Private mBinRanges As List(Of BinRange) = New List(Of BinRange)

        Public Property Code As String
            Get
                Return mCode
            End Get
            Set(ByVal value As String)
                mCode = value
            End Set
        End Property

        Public Property Color As String
            Get
                Return mColor
            End Get
            Set(ByVal value As String)
                mColor = value
            End Set
        End Property

        Public Property RangeBright As String
            Get
                Return mRangeBright
            End Get
            Set(ByVal value As String)
                mRangeBright = value
            End Set
        End Property

        Public Property BinRanges As List(Of BinRange)
            Get
                Return mBinRanges
            End Get
            Set(ByVal value As List(Of BinRange))
                mBinRanges = value
            End Set
        End Property

        Public Sub New()
            mBinRanges.Clear()
        End Sub
    End Class
#End Region

#Region "VERSIONI"
    Public Class CVersioni
        Private mID As Integer
        Private mDesCrizione As String
        Private mCodeBitron As String
        Private mCodiceClient As String
        Private mCodiceEclips As String
        Private mBarcode As String
        Private mSequenza As Integer
        Private mSite As List(Of CSite) = New List(Of CSite)

        Public Property ID As Integer
            Get
                Return mID
            End Get
            Set(ByVal value As Integer)
                mID = value
            End Set
        End Property

        Public Property Descrizione As String
            Get
                Return mDesCrizione
            End Get
            Set(ByVal value As String)
                mDesCrizione = value
            End Set
        End Property

        Public Property CodeBitron As String
            Get
                Return mCodeBitron
            End Get
            Set(ByVal value As String)
                mCodeBitron = value
            End Set
        End Property

        Public Property CodiceClient As String
            Get
                Return mCodiceClient
            End Get
            Set(ByVal value As String)
                mCodiceClient = value
            End Set
        End Property

        Public Property CodiceEclips As String
            Get
                Return mCodiceEclips
            End Get
            Set(ByVal value As String)
                mCodiceEclips = value
            End Set
        End Property

        Public Property Barcode As String
            Get
                Return mBarcode
            End Get
            Set(ByVal value As String)
                mBarcode = value
            End Set
        End Property

        Public Property Sequenza As Integer
            Get
                Return mSequenza
            End Get
            Set(ByVal value As Integer)
                mSequenza = value
            End Set
        End Property

        Public Property Sites As List(Of CSite)
            Get
                Return mSite
            End Get
            Set(ByVal value As List(Of CSite))
                mSite = value
            End Set
        End Property

        Public Sub New()
            mSite.Clear()
        End Sub
    End Class

    Public Class CSite
        Private mDescription As String
        Private mEnable As Integer
        Private mSiteNo As Integer
        Private mFeasaAddr As Integer
        Private mPart As List(Of CParts) = New List(Of CParts)

        Public Property Description As String
            Get
                Return mDescription
            End Get
            Set(ByVal value As String)
                mDescription = value
            End Set
        End Property
        Public Property Enable As Integer
            Get
                Return mEnable
            End Get
            Set(ByVal value As Integer)
                mEnable = value
            End Set
        End Property
        Public Property SiteNo As Integer
            Get
                Return mSiteNo
            End Get
            Set(ByVal value As Integer)
                mSiteNo = value
            End Set
        End Property
        Public Property FeasaAddr As Integer
            Get
                Return mFeasaAddr
            End Get
            Set(ByVal value As Integer)
                mFeasaAddr = value
            End Set
        End Property

        Public Property PART As List(Of CParts)
            Get
                Return mPart
            End Get
            Set(ByVal value As List(Of CParts))
                mPart = value
            End Set
        End Property
        Public Sub New()
            mPart.Clear()
        End Sub
    End Class

    Public Class CParts
        Private mEnable As Integer
        Private mIndex As String
        Private mTaskLabel As String
        Private mSite As List(Of CFeasa) = New List(Of CFeasa)

        Public Property Enable As Integer
            Get
                Return mEnable
            End Get
            Set(ByVal value As Integer)
                mEnable = value
            End Set
        End Property

        Public Property Index As String
            Get
                Return mIndex
            End Get
            Set(ByVal value As String)
                mIndex = value
            End Set
        End Property

        Public Property TaskLabel As String
            Get
                Return mTaskLabel
            End Get
            Set(ByVal value As String)
                mTaskLabel = value
            End Set
        End Property

        Public Property SITE As List(Of CFeasa)
            Get
                Return mSite
            End Get
            Set(ByVal value As List(Of CFeasa))
                mSite = value
            End Set
        End Property

        Public Sub New()
            mSite.Clear()
        End Sub
    End Class


    Public Class CFeasa
        Private mName As String
        Private mID As Integer
        Private mCH As Integer
        Private mLEDName As String
        Private mEnable As Integer

        Private mEnableCxCy As Integer
        Private mEnableHue As Integer
        Private mEnableSat As Integer
        Private mEnableInt As Integer
        Private mEnableWav As Integer
        Private mEnableVF As Integer

        Private mCode As String
        Private mBinName As String
        Private mSite As Integer
        Private mHueOffset As Double
        Private mSatOffset As Integer
        Private mCxOffset As Single
        Private mCyOffset As Single
        Private mIntensityOffset As Double
        Private mDominantWavOffset As Double
        Private mVFRegister As Integer
        Private mVFOffset As Single
        Private mPowerStart As String
        Private mPowerEnd As String
        Private mTon As Integer
        Private mNote As String


        Public Property Name As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property

        Public Property ID As Integer
            Get
                Return mID
            End Get
            Set(ByVal value As Integer)
                mID = value
            End Set
        End Property

        Public Property CH As Integer
            Get
                Return mCH
            End Get
            Set(ByVal value As Integer)
                mCH = value
            End Set
        End Property

        Public Property LEDName As String
            Get
                Return mLEDName
            End Get
            Set(ByVal value As String)
                mLEDName = value
            End Set
        End Property

        Public Property Enable As Integer
            Get
                Return mEnable
            End Get
            Set(ByVal value As Integer)
                mEnable = value
            End Set
        End Property


        Public Property EnableCxCy As Integer
            Get
                Return mEnableCxCy
            End Get
            Set(ByVal value As Integer)
                mEnableCxCy = value
            End Set
        End Property

        Public Property EnableHue As Integer
            Get
                Return mEnableHue
            End Get
            Set(ByVal value As Integer)
                mEnableHue = value
            End Set
        End Property

        Public Property EnableSat As Integer
            Get
                Return mEnableSat
            End Get
            Set(ByVal value As Integer)
                mEnableSat = value
            End Set
        End Property

        Public Property EnableAbsInt As Integer
            Get
                Return mEnableInt
            End Get
            Set(ByVal value As Integer)
                mEnableInt = value
            End Set
        End Property

        Public Property EnableWav As Integer
            Get
                Return mEnableWav
            End Get
            Set(ByVal value As Integer)
                mEnableWav = value
            End Set
        End Property

        Public Property EnableVF As Integer
            Get
                Return mEnableVF
            End Get
            Set(ByVal value As Integer)
                mEnableVF = value
            End Set
        End Property


        Public Property Code As String
            Get
                Return mCode
            End Get
            Set(ByVal value As String)
                mCode = value
            End Set
        End Property

        Public Property BinName As String
            Get
                Return mBinName
            End Get
            Set(ByVal value As String)
                mBinName = value
            End Set
        End Property

        Public Property Site As String
            Get
                Return mSite
            End Get
            Set(ByVal value As String)
                mSite = value
            End Set
        End Property

        Public Property HueOffset As Double
            Get
                Return mHueOffset
            End Get
            Set(ByVal value As Double)
                mHueOffset = value
            End Set
        End Property

        Public Property SatOffset As Integer
            Get
                Return mSatOffset
            End Get
            Set(ByVal value As Integer)
                mSatOffset = value
            End Set
        End Property


        Public Property CxOffset As Single
            Get
                Return mCxOffset
            End Get
            Set(ByVal value As Single)
                mCxOffset = value
            End Set
        End Property

        Public Property CyOffset As Single
            Get
                Return mCyOffset
            End Get
            Set(ByVal value As Single)
                mCyOffset = value
            End Set
        End Property

        Public Property IntensityOffset As Double
            Get
                Return mIntensityOffset
            End Get
            Set(ByVal value As Double)
                mIntensityOffset = value
            End Set
        End Property

        Public Property DominantWavOffset As Double
            Get
                Return mDominantWavOffset
            End Get
            Set(ByVal value As Double)
                mDominantWavOffset = value
            End Set
        End Property

        Public Property VFRegister As Integer
            Get
                Return mVFRegister
            End Get
            Set(ByVal value As Integer)
                mVFRegister = value
            End Set
        End Property

        Public Property VFOffset As Single
            Get
                Return mVFOffset
            End Get
            Set(ByVal value As Single)
                mVFOffset = value
            End Set
        End Property

        Public Property PowerStart As String
            Get
                Return mPowerStart
            End Get
            Set(ByVal value As String)
                mPowerStart = value
            End Set
        End Property

        Public Property PowerEnd As String
            Get
                Return mPowerEnd
            End Get
            Set(ByVal value As String)
                mPowerEnd = value
            End Set
        End Property

        Public Property Ton As Integer
            Get
                Return mTon
            End Get
            Set(ByVal value As Integer)
                mTon = value
            End Set
        End Property

        Public Property Note As String
            Get
                Return mNote
            End Get
            Set(ByVal value As String)
                mNote = value
            End Set
        End Property
    End Class

#End Region
End Class

