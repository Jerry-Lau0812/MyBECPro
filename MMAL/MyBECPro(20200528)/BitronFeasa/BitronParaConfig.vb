Imports System.Xml
Public Class BitronParaConfig
    Private Xdoc As XmlDocument

    Private mLEDRES As New List(Of CLEDRES)
    Private mCurLimit As New List(Of CCurLimIT)
    Private mTestResStep As New List(Of CTestResSTEP)
    Private mTestCurStep As New List(Of CTestCurSTEP)

    Private mEnableFujiTrax As Boolean
    Private mEnableRes As Boolean
    Private mEnableCur As Boolean

    Public Property IsFujiTrax As Boolean
        Get
            Return mEnableFujiTrax
        End Get
        Set(ByVal value As Boolean)
            mEnableFujiTrax = value
        End Set
    End Property


    Public Property IsCheckRes As Boolean
        Set(ByVal value As Boolean)
            mEnableRes = value
        End Set
        Get
            Return mEnableRes
        End Get
    End Property

    Public Property IsCheckCur As Boolean
        Set(ByVal value As Boolean)
            mEnableCur = value
        End Set
        Get
            Return mEnableCur
        End Get
    End Property

    Public Property LEDRes As List(Of CLEDRES)
        Set(ByVal value As List(Of CLEDRES))
            mLEDRES = value
        End Set
        Get
            Return mLEDRES
        End Get
    End Property

    Public Property CurLimit As List(Of CCurLimIT)
        Set(ByVal value As List(Of CCurLimIT))
            mCurLimit = value
        End Set
        Get
            Return mCurLimit
        End Get
    End Property

    Public Property TestResStep As List(Of CTestResSTEP)
        Set(ByVal value As List(Of CTestResSTEP))
            mTestResStep = value
        End Set
        Get
            Return mTestResStep
        End Get
    End Property

    Public Property TestCurStep As List(Of CTestCurSTEP)
        Set(ByVal value As List(Of CTestCurSTEP))
            mTestCurStep = value
        End Set
        Get
            Return mTestCurStep
        End Get
    End Property





    Public Sub New(Optional ByVal StrXmlPath As String = "\ParaConfig.XML")
        Xdoc = New XmlDocument()
        'Xdoc.Load(AppDomain.CurrentDomain.BaseDirectory & StrXmlPath)
        Xdoc.Load(StrXmlPath)

        mLEDRES.Clear()
        mCurLimit.Clear()
        mTestResStep.Clear()
        mTestCurStep.Clear()
    End Sub

    Public Sub Initial()
        LoadCommon()
        LoadLEDRes()
        LoadCurLimit()
        LoadTestResStep()
        LoadTestCurStep()
    End Sub

    Public Sub LoadCommon()
        Try

            mEnableFujiTrax = GetElement("MMAL/ENABLEFUJITRAX")

            mEnableRes = GetElement("MMAL/ENABLERES")

            mEnableCur = GetElement("MMAL/ENABLECUR")

        Catch ex As Exception

        End Try
    End Sub

    Public Function GetElement(ByVal StrXml As String) As String
        Dim Node As XmlNode = Xdoc.SelectSingleNode(StrXml)
        Return Node.InnerText
    End Function

    Public Sub LoadLEDRes()
        Try
            Dim filePath As String = "MMAL/LEDRESS"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim cRes As New CLEDRES
                    For Each SubNode As XmlNode In PreNode
                        Select Case SubNode.Name
                            Case "BIN"
                                cRes.BIN = SubNode.InnerText
                            Case "BITRONPART"
                                cRes.BITRONPART = SubNode.InnerText
                            Case "RESISTOR_OHM_H"
                                cRes.RESISTOR_H = SubNode.InnerText
                            Case "RESISTOR_OHM_L"
                                cRes.RESISTOR_L = SubNode.InnerText
                        End Select
                    Next
                    mLEDRES.Add(cRes)
                Next
            End If
        Catch
        End Try
    End Sub


    Public Sub LoadCurLimit()
        Try
            Dim filePath As String = "MMAL/Current_LimitS"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim cCur As New CCurLimIT
                    For Each SunNode In PreNode
                        Select Case SunNode.Name
                            Case "BIN"
                                cCur.BIN = SunNode.InnerText
                            Case "T_H"
                                cCur.T_H = SunNode.InnerText
                            Case "T_L"
                                cCur.T_L = SunNode.InnerText
                        End Select
                    Next
                    mCurLimit.Add(cCur)
                Next

            End If
        Catch
        End Try
    End Sub

    Public Sub LoadTestResStep()
        Try
            Dim filePath As String = "MMAL/TESTResStepS"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim cResStep As New CTestResSTEP
                    For Each SubNode In PreNode
                        Select Case SubNode.Name
                            Case "STEPNo"
                                cResStep.STEPNo = SubNode.InnerText
                            Case "RunLabel"
                                cResStep.RunLabel = SubNode.InnerText
                            Case "Modulux"
                                cResStep.Modulux = SubNode.InnerText
                            Case "Formula"
                                cResStep.Formula = SubNode.InnerText
                            Case "BIN"
                                cResStep.BinName = SubNode.InnerText
                        End Select
                    Next
                    mTestResStep.Add(cResStep)
                Next
            End If
        Catch
        End Try
    End Sub

    Public Sub LoadTestCurStep()
        Try
            Dim filePath As String = "MMAL/TESTCurStepS"
            Dim RootNode As XmlNode = Xdoc.SelectSingleNode(filePath)
            If Not RootNode Is Nothing Then
                For Each PreNode As XmlNode In RootNode
                    Dim cCurStep As New CTestCurSTEP
                    For Each SubNode In PreNode
                        Select Case SubNode.Name
                            Case "STEPNo"
                                cCurStep.STEPNo = SubNode.InnerText
                            Case "Description"
                                cCurStep.Description = SubNode.InnerText
                            Case "RunLabel"
                                cCurStep.RunLabel = SubNode.InnerText
                            Case "Combination"
                                cCurStep.Combination = SubNode.InnerText
                            Case "BIN"
                                cCurStep.BinName = SubNode.InnerText
                        End Select
                    Next
                    mTestCurStep.Add(cCurStep)
                Next
            End If
        Catch
        End Try
    End Sub


    Public Class CLEDRES
        Private mTestID As String
        Private mBIN As String
        Private mBitronpart As String
        Private mResistor_H As String
        Private mResistor_L As String

        Public Property BIN
            Set(ByVal value)
                mBIN = value
            End Set
            Get
                Return mBIN
            End Get
        End Property

        Public Property BITRONPART
            Set(ByVal value)
                mBitronpart = value
            End Set
            Get
                Return mBitronpart
            End Get
        End Property

        Public Property RESISTOR_H
            Set(ByVal value)
                mResistor_H = value
            End Set
            Get
                Return mResistor_H
            End Get
        End Property

        Public Property RESISTOR_L
            Set(ByVal value)
                mResistor_L = value
            End Set
            Get
                Return mResistor_L
            End Get
        End Property

    End Class


    Public Class CCurLimIT
        Private mBIN As String
        Private mT_H As String
        Private mT_L As String

        Public Property BIN
            Set(ByVal value)
                mBIN = value
            End Set
            Get
                Return mBIN
            End Get
        End Property

        Public Property T_H
            Set(ByVal value)
                mT_H = value
            End Set
            Get
                Return mT_H
            End Get
        End Property

        Public Property T_L
            Set(ByVal value)
                mT_L = value
            End Set
            Get
                Return mT_L
            End Get
        End Property

    End Class


    Public Class CTestResSTEP
        Private mSTEPNo As String
        Private mRunlabel As String
        Private mModulux As String
        Private mFormula As String
        Private mBinName As String

        Public Property STEPNo
            Set(ByVal value)
                mSTEPNo = value
            End Set
            Get
                Return mSTEPNo
            End Get
        End Property

        Public Property RunLabel
            Set(ByVal value)
                mRunlabel = value
            End Set
            Get
                Return mRunlabel
            End Get
        End Property

        Public Property Modulux
            Set(ByVal value)
                mModulux = value
            End Set
            Get
                Return mModulux
            End Get
        End Property

        Public Property Formula
            Set(ByVal value)
                mFormula = value
            End Set
            Get
                Return mFormula
            End Get
        End Property

        Public Property BinName
            Set(ByVal value)
                mBinName = value
            End Set
            Get
                Return mBinName
            End Get
        End Property

    End Class


    Public Class CTestCurSTEP
        Private mSTEPNo As String
        Private mDescription As String
        Private mRunLabel As String
        Private mCombination As String
        Private mBinName As String
        Public Property STEPNo
            Set(ByVal value)
                mSTEPNo = value
            End Set
            Get
                Return mSTEPNo
            End Get
        End Property

        Public Property Description
            Set(ByVal value)
                mDescription = value
            End Set
            Get
                Return mDescription
            End Get
        End Property

        Public Property RunLabel
            Set(ByVal value)
                mRunLabel = value
            End Set
            Get
                Return mRunLabel
            End Get
        End Property

        Public Property Combination
            Set(ByVal value)
                mCombination = value
            End Set
            Get
                Return mCombination
            End Get
        End Property

        Public Property BinName
            Set(ByVal value)
                mBinName = value
            End Set
            Get
                Return mBinName
            End Get
        End Property

    End Class

End Class

