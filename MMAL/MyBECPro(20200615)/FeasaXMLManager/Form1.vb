Imports BitronConfig
Imports System.Xml

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Doc = New XmlDocument()
        Doc.Load("FunzConf.xml")
        Dim NodeList As XmlNodeList = Doc.SelectSingleNode("GENERAL").ChildNodes

    End Sub







End Class
