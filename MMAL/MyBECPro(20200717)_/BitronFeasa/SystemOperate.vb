Imports System.ComponentModel
Imports BitronConfig
Imports System.Xml
Imports System.IO.Ports

Public Enum HardwareTypes
    <Description("FEASA")>
    FEASA = 1
    <Description("IT6332")>
    IT6332 = 2
End Enum



Public Class SystemOperate

    Dim XdocHd As New XmlDocument
    'Private HardConf As New XmlElement

    Public SysCom As New List(Of Serial)

    Public Shared Device_FEASA As New ClassFEASA

    Public Sub New()
        Try
            XdocHd.Load(AppDomain.CurrentDomain.BaseDirectory & "\FunzConf.XML")
        Catch ex As Exception
            MsgBox(ex.Message + Chr(13) + "Error in File Configuration")
        End Try
    End Sub


    Public Sub LoadAllHardwares()
        Dim filepath As String = "FUNZCONF/HARDWARES"
        Dim rootNode As XmlNode = XdocHd.SelectSingleNode(filepath)
        'Dim rootNode As XmlNodeList = XdocHd.SelectNodes(filepath)
        Dim hardwaretype As String
        Dim hardwareName As String
        Dim ComTypePara As New List(Of Serial)
        Dim Count As Integer = 0

        If Not rootNode Is Nothing Then
            For Each preNode As XmlNode In rootNode
                For Each childNode As XmlNode In preNode.ChildNodes
                    Dim chw As New Serial
                    chw.TypeID = preNode.Attributes("type").InnerXml
                    'chw.Name = preNode.Attributes("name").InnerXml
                    chw.Index = childNode.Attributes("index").InnerXml
                    chw.Address = childNode.Attributes("addr").InnerXml
                    chw.Name = childNode.Attributes("name").InnerXml
                    For Each cchildNode As XmlNode In childNode.ChildNodes
                        Select Case cchildNode.Name
                            Case "PORT"
                                chw.PortName = "COM" & cchildNode.InnerText
                            Case "VELOCITA"
                                chw.BaudRate = cchildNode.InnerText
                            Case "PARITA"
                                Select Case cchildNode.InnerText
                                    Case "N"
                                        chw.Parity = Parity.None
                                    Case Else
                                End Select
                            Case "NUM_BIT"
                                chw.DataBits = cchildNode.InnerText
                            Case "STOP_BIT"
                                chw.StopBits = cchildNode.InnerText
                            Case "DTR"
                                Select Case cchildNode.InnerText
                                    Case ""
                                        chw.DtrEnable = False
                                    Case "1"
                                        chw.DtrEnable = True
                                End Select
                            Case "RTS"
                                Select Case cchildNode.InnerText
                                    Case ""
                                        chw.RtsEnable = False
                                    Case "1"
                                        chw.RtsEnable = True
                                End Select
                        End Select
                    Next
                    ComTypePara.Add(chw)
                Next
            Next

            For Each com As Serial In ComTypePara
                Dim Comlist As New List(Of String)
                Select Case com.TypeID
                    Case HardwareTypes.FEASA
                        Comlist.Add(com.Index)
                        Comlist.Add(com.Address)
                        Comlist.Add(com.PortName)
                        Comlist.Add(com.BaudRate)
                        Comlist.Add(com.DataBits)
                        Comlist.Add(com.Parity)
                        Comlist.Add(com.StopBits)
                        Comlist.Add(com.DtrEnable)
                        Comlist.Add(com.RtsEnable)
                        Device_FEASA.AperturaCom(Comlist)
                End Select
            Next

        End If
    End Sub

    Public Sub DisposeAllHardwares()

    End Sub


End Class


#Region "HWconfig class from xml"
Public Class HWCom
    Private mDeviceType As Integer
    Private mDeviceIndex As Integer
    Private mDeviceName As Integer
    Private mDeviceAddr As Integer

    Private mPortName As String
    Private mBaudRate As String
    Private mDataBits As Integer
    Private mParity As IO.Ports.Parity
    Private mStopBit As IO.Ports.StopBits
    Private mDTS As Integer
    Private mRTS As Integer

    Public Property DeviceType As Integer
        Get
            Return mDeviceType
        End Get
        Set(ByVal value As Integer)
            mDeviceAddr = value
        End Set
    End Property

    Public Property DeviceIndex As Integer
        Get
            Return mDeviceIndex
        End Get
        Set(ByVal value As Integer)
            mDeviceIndex = value
        End Set
    End Property

    Public Property DeviceAddr As Integer
        Get
            Return mDeviceAddr
        End Get
        Set(ByVal value As Integer)
            mDeviceAddr = value
        End Set
    End Property

    Public Property PortName As String
        Get
            Return mPortName
        End Get
        Set(ByVal value As String)
            mPortName = value
        End Set
    End Property

    Public Property BaudRate As String
        Get
            Return mBaudRate
        End Get
        Set(ByVal value As String)
            mBaudRate = value
        End Set
    End Property

    Public Property DataBits As Integer
        Get
            Return mDataBits
        End Get
        Set(ByVal value As Integer)
            mDataBits = value
        End Set
    End Property

    Public Property Parity As IO.Ports.Parity
        Get
            Return mParity
        End Get
        Set(ByVal value As IO.Ports.Parity)
            mParity = value
        End Set
    End Property

    Public Property Stopbit As IO.Ports.Parity
        Get
            Return mStopBit
        End Get
        Set(ByVal value As IO.Ports.Parity)
            mStopBit = value
        End Set
    End Property

    Public Property RTS As Integer
        Get
            Return mRTS
        End Get
        Set(ByVal value As Integer)
            mRTS = value
        End Set
    End Property

    Public Property DTS As Integer
        Get
            Return mDTS
        End Get
        Set(ByVal value As Integer)
            mDTS = value
        End Set
    End Property

End Class
#End Region