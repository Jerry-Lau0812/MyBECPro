'Classe che contiene main tutte le routine standard per la gestione del test
'Gestione visulaizzazione dati su pannello pass fali / grafico
'Routine di Verifica presenza strumenti
'Gestione reportistica


Imports System.Xml
Imports System.IO.Ports

Public Enum FileXML
    FunzConf = 1
    DatiTmp = 2
    Test = 3
End Enum

Public Class Test
    Inherits Com
    Public DocFunzConf As New XmlDocument()
    Public Funzconf As XmlElement
    Private DocDatiTmp As New XmlDocument()
    Private DatiTmp As XmlElement

    Public Overloads Function RicercaXML(ByVal File As FileXML, ByVal Xpath As String, ByVal Elemento As String)
        Dim View As XmlNodeList
        Dim Ris As String
        'restituisce l'elemento 0 dellaricerca
        Try

            Select Case File
                Case FileXML.FunzConf
                    View = Funzconf.SelectNodes(Xpath)
                Case FileXML.DatiTmp
                    View = DatiTmp.SelectNodes(Xpath)
        
            End Select
            Ris = View.ItemOf(0)(Elemento).InnerText
        Catch
            Ris = "-1"
            'Esito(0) = False : Esito(1) = False
            'MsgBox("Attenzione impossibile leggere il file di configurazione", MsgBoxStyle.Critical)
        End Try
        'Dim View As XmlNodeList = ConfigHW.SelectNodes("rs232[impiego='CUBLOC']")
        Return Ris

    End Function
    Public Overloads Function RicercaXML(ByVal File As FileXML, ByVal Xpath As String, ByVal Elemento As String, ByVal NumElement As Integer)
        Dim View As XmlNodeList
        Dim Ris As String
        Try
            'restituisce l'elemento n dellaricerca
            Select Case File
                Case FileXML.FunzConf
                    View = Funzconf.SelectNodes(Xpath)
                Case FileXML.DatiTmp
                    View = DatiTmp.SelectNodes(Xpath)

            End Select
            Ris = View.ItemOf(NumElement)(Elemento).InnerText
        Catch
            Ris = "-1"
            Esito(0) = False : Esito(1) = False
            MsgBox("Attenzione impossibile leggere il file di configurazione", MsgBoxStyle.Critical)
        End Try
        'Dim View As XmlNodeList = ConfigHW.SelectNodes("rs232[impiego='CUBLOC']")
        Return Ris

    End Function

    Public Function NumRecordXML(ByVal File As FileXML, ByVal Xpath As String)
        Dim View As XmlNodeList
        Dim Ris As String
        Try
            Select Case File
                Case FileXML.FunzConf
                    View = Funzconf.SelectNodes(Xpath)

                Case FileXML.DatiTmp
                    View = DatiTmp.SelectNodes(Xpath)
 
            End Select
            Ris = View.Count
        Catch
            Ris = "-1"
        End Try
        'Dim View As XmlNodeList = ConfigHW.SelectNodes("rs232[impiego='CUBLOC']")
        Return Ris

    End Function


    Public Sub New()
        Try
            DocFunzConf.Load(AppDomain.CurrentDomain.BaseDirectory & "\FunzConf.XML")
            Funzconf = DocFunzConf.DocumentElement

            DocDatiTmp.Load(AppDomain.CurrentDomain.BaseDirectory & "\DatiTmp.xml")
            DatiTmp = DocDatiTmp.DocumentElement

        Catch ex As Exception
            MsgBox(ex.Message + Chr(13) + "Errore di lettura file di configurazione")
            LogErr("test.new")
            End

        End Try
    End Sub
    Public Sub LogErr(ByVal Testo As String)
        Try
            My.Computer.FileSystem.WriteAllText("c:\bitron\setwn\LogErr.txt", Now.ToString + "-" + Testo, False)
        Catch
        End Try
    End Sub
    Public Function VerComPic()
        Dim Risposta As String
        Dim NumPic As Integer
        Dim Er As Integer = 1
        Dim T As Integer
        NumPic = RicercaXML(FileXML.FunzConf, "GENERAL", "NUM_PIC")
        Principale.Test.Invia("READY")
        T = 1
        While T <= NumPic And Er = 1
            Risposta = Principale.Test.Ricevi(1)
            If Risposta = "" Then
                Er = -1
            End If
            T = T + 1
        End While
        Return Er
    End Function
    Public Function VerComMatrix(ByVal UUT As Integer)
        Dim Risposta As String
        Principale.Test.Invia("LON" + Chr(13), 2 + UUT)
        Sleep(2)
        Principale.Test.Invia("LOFF" + Chr(13), 2 + UUT)
        Risposta = Principale.Test.Ricevi(1)
        If Risposta <> "" Then
            Return 1
        Else
            Return 0
        End If

    End Function
    Public Function VerComAdam()
        Dim Risposta As String

        For T As Integer = 1 To 1
            Invia("$0" & T.ToString & "M" + Chr(13), IdCom("ADAM"), "!", Chr(13))
            Risposta = Ricevi(2)
            If Risposta = "0" & (T).ToString & "4117" Then
                Return 1
            Else
                Return 0
            End If
            Sleep(5)
        Next T
    End Function

    Public Sub MemProguso()
        Try
            Dim View As XmlNodeList = DatiTmp.SelectNodes("DATI")
            View.ItemOf(0)("PROG_SEL").InnerText = ProgUso
            DocDatiTmp.Save("c:\bitron\setwn" + "\DatiTmp.xml")
        Catch
            LogErr("Test-memproguso")
        End Try
    End Sub
    Public Sub MemPassFail()
        Try
            Dim View As XmlNodeList = DatiTmp.SelectNodes("DATI")
            View.ItemOf(0)("NUM_PASS").InnerText = NumPass
            View.ItemOf(0)("NUM_FAIL").InnerText = NumFail
            DocDatiTmp.Save("c:\bitron\setwn" + "\DatiTmp.xml")
        Catch
            LogErr("Test-MemPassFail")
        End Try
    End Sub


   
   
End Class