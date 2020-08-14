Imports System.Xml
''' <summary>
''' 提供底层XML文件读取
''' </summary>
''' <remarks></remarks>
Public Class XMLBase
    Private DocFunzConf As New XmlDocument()
    Private FunzConf As XmlElement
    Private DocDatiTmp As New XmlDocument()
    Private DatiTmp As XmlElement()

    Public Sub New()
        Try
            DocFunzConf.Load(AppDomain.CurrentDomain.BaseDirectory & "\FunzConf.XML")
            FunzConf = DocFunzConf.DocumentElement
        Catch ex As Exception
            MsgBox(ex.Message + Chr(13) + "Error in File Configuration")
        End Try
    End Sub

    Public Overloads Function RicercaXML(ByVal Xpath As String, ByVal Elemento As String) As String
        Dim View As XmlNodeList
        Dim Ris As String
        Try
            View = FunzConf.SelectNodes(Xpath)
            Ris = View.ItemOf(0)(Elemento).InnerText
        Catch ex As Exception
            Ris = "-1"
            MsgBox("Attenzione impossibile leggere il file di configurazione", MsgBoxStyle.Critical)
        End Try
        Return Ris
    End Function

    Public Overloads Function RicercaXML(ByVal Xpath As String, ByVal Elemento As String, ByVal NumElement As Integer)
        Dim View As XmlNodeList
        Dim Ris As String
        Try
            View = FunzConf.SelectNodes(Xpath)
            Ris = View.ItemOf(NumElement)(Elemento).InnerText
        Catch
            Ris = "-1"
            MsgBox("Attenzione impossibile leggere il file di configurazione", MsgBoxStyle.Critical)
        End Try
        Return Ris
    End Function
End Class
