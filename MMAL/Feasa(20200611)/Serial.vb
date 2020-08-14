Imports System.IO.Ports
'Classe che indicizza l'oggetto serial port
Public Class Serial
    Inherits SerialPort
    Private m As Integer

    Public Sub New(ByVal Index As Integer)
        m = Index
    End Sub
    Public Property Index() As Integer
        Get
            Return m
        End Get
        Set(ByVal value As Integer)
            m = value
        End Set
    End Property
End Class
