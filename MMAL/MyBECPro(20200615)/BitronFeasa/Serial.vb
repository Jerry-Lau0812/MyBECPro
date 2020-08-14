Imports System.IO.Ports
Imports System.IO
Imports System.Runtime.Serialization.Formatters

'Classe che indicizza l'oggetto serial port
Public Class Serial
    Inherits SerialPort

    Public Sub New()
    End Sub

    Private _index As Integer
    Public Property Index() As Integer
        Get
            Return _index
        End Get
        Set(ByVal value As Integer)
            _index = value
        End Set
    End Property

    Private _NeedOpen As Boolean
    Public Property NeedOpen() As String
        Get
            Return _NeedOpen
        End Get
        Set(ByVal value As String)
            _NeedOpen = value
        End Set
    End Property

    Private _CommPort As Integer
    Public Property PortNum() As Integer
        Get
            Return _CommPort
        End Get
        Set(ByVal value As Integer)
            _CommPort = value
        End Set
    End Property

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _ComType As Integer
    Public Property TypeID() As Integer
        Get
            Return _ComType
        End Get
        Set(ByVal value As Integer)
            _ComType = value
        End Set
    End Property

    Private _Addr As Integer
    Public Property Address() As Integer
        Get
            Return _Addr
        End Get
        Set(ByVal value As Integer)
            _Addr = value
        End Set
    End Property

End Class
