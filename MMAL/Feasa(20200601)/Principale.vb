
'Programmazione Meter enel gem mediante Programmatori Algocraft
'===============================================================
'Viola Angelo 18/03/2013 - (Dettagli programmazione in Prog.Vb)
'================================================================
'Modulo che contiene il main e tutte le routine specifiche dell'applciazione di test 

Imports System.IO.Ports
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Management
Imports System.Text.RegularExpressions
Imports System
Imports System.IO



Public Module Principale
    'Read INI FILE
    Public Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
    Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String, _
    ByVal lpKeyName As String, ByVal lpDefault As String, _
    ByVal lpReturnedString As String, ByVal nSize As Int32, _
    ByVal lpFileName As String) As Int32
    '==================================================================
    Public Test As New Test
    Private M As Threading.Mutex
    Public Const PassAddr = "h90000011"
    Public Const FailAddr = "h90000012"
    Public Function FormatSerialAlgocraft(ByVal Serial As String) As String
        Dim Num As Integer
        Dim HexFormat As String
        Try
            For Num = 0 To Serial.Length - 1
                HexFormat = HexFormat + "h" + Hex(Asc(Serial(Num))).ToString.PadRight(2, "0") + " "
            Next Num
            Return HexFormat.Substring(0, HexFormat.Length - 1)
        Catch
            Return ""
        End Try


    End Function

    Public Structure SDatiProd
        Dim Desc As String
        Dim CodBitron As String
        Dim CodClient As String
        Dim RelSwuP As String
        Dim RelSwFram As String
        Dim RelSwModem As String
    End Structure
    Public Structure SEsThread
        Dim EsitoProg As Boolean
        Dim RispProg As String
    End Structure
    Public Structure SMisDb
        Dim Misura As String
        Dim EsitoMis As String
        Dim UnitMis As String
        Dim CODE_BITRON As String
    End Structure
    Public Structure SParameter
        Dim Bay As Integer
        Dim Site As Integer
        Dim Project As String
        Dim Bank As Integer
        Dim Checksum As String
    End Structure
    Public Structure Cooordinate
        Dim X1 As Double
        Dim Y1 As Double
        Dim X2 As Double
        Dim Y2 As Double
        Dim X3 As Double
        Dim Y3 As Double
        Dim X4 As Double
        Dim Y4 As Double
    End Structure
    Public NumFail As Integer
    Public NumPass As Integer
    Public NumCicli As Integer
    Public ProgUso As String
    Public DatiProd As SDatiProd
    Public ContaTest As Integer
    Public Esito(1) As Boolean
    Public EsitoThread(1) As SEsThread
    Public NumError(10) As Integer
    Public Matrix(1) As String
    Private PortAdam() As String = {"1", "2"}
    Public ThreadExecution As Boolean = False
    Public Freq(1) As Double
    Public CalByte(1) As Integer
    Public MeasDefault(1) As String
    Public StartTime As DateTime
    Public StopTime As DateTime
    Public Thr1 As System.Threading.Thread
    Public Thr2 As System.Threading.Thread
    Public MisDb(1)() As SMisDb
    Public Comflag(1) As String
    Public Parameter As SParameter


    Public Function RotateMSN(ByVal Dato As String) As String
        Dim T As Integer
        Dim DatoRota As String
        For T = Dato.Length - 1 To 0 Step -1
            DatoRota = DatoRota + Dato(T)
        Next T
        Return DatoRota
    End Function

    Private Sub LogMppc(ByVal Site As Integer, ByVal value As String)
        Try
            My.Computer.FileSystem.WriteAllText("c:\bitron\setwn\log\Log.txt", Now + "-" + value + ControlChars.NewLine, True)
        Catch
        End Try
    End Sub

    Public Function FormatByteCrypt(ByVal ByteCrypt() As Byte, ByVal Start As Integer) As String
        Dim Num As Integer
        Dim HexFormat As String
        Try
            For Num = Start To Start + 31
                HexFormat = HexFormat + "h" + Hex(ByteCrypt(Num)).ToString.PadLeft(2, "0") + " "
            Next Num
            Return HexFormat.Substring(0, HexFormat.Length - 1)
        Catch
            Return ""
        End Try

    End Function
    Public Function Roll(ByVal Dato As String) As String
        Dim DatoRoll As String
        Dim T As Integer
        Try
            For T = Dato.Length - 1 To 0 Step -1
                DatoRoll = DatoRoll + Dato(T)
            Next
            Return DatoRoll
        Catch
            DatoRoll = ""
        End Try

    End Function


    Public Function ReadSerialNumber(ByVal UUT As Integer) As String
        Dim Result As String = Space(16)
        Dim ValidResult As String = ""
        Try
            GetPrivateProfileString("TestStatus", "SN" + UUT.ToString, "Nothing", Result, Len(Result), "c:\bitron\SetWN\input.ini")
            ValidResult = Result.Substring(0, 15)
        Catch
        End Try
        If IsNumeric(ValidResult) = False Then
            MsgBox("Matrix code not present", MsgBoxStyle.Critical)
            Esito(0) = False
            ValidResult = ""
        End If
        Return ValidResult

    End Function



    Public Sub LeggePar()

        Dim RispFlash As String
        Dim Par As String
        Dim SplitPar(10) As String
        Try
            'bay :1 / 2
            'Prject : "Stringa senza Wnp"
            'Bank:"Numero banco da indirizzare"

            'parametri di default se non si passano parametri
            If Debug() = 1 Then Parameter.Bay = 1
            If Trace() = 1 Then MsgBox("LeggeParameter")
            Par = Command()

            SplitPar = Par.Split(",")
            'MsgBox("Primo parametro " + SplitPar(0))

            If UBound(SplitPar) > 0 Then
                Parameter.Bay = CType(SplitPar(0), Integer) - 1
                'Parameter.Site = SplitPar(1) '1 On 
            End If
            If Trace() = 1 Then MsgBox("Bay:" + (Parameter.Bay + 1).ToString)
            'If Trace() = 1 Then MsgBox("site: " + Parameter.Site.ToString)
        Catch
            MsgBox("Load Parameter Fail " + Par + "END")
            End
        End Try
    End Sub
    Public Sub Apricom(ByVal Utilizzo As String)
        Dim Rs232Set As New List(Of String)
        Rs232Set.Add(Test.RicercaXML(FileXML.FunzConf, "COM[DESCRIZIONE='" & Utilizzo & "']", "NUM_COM"))
        Rs232Set.Add(Test.RicercaXML(FileXML.FunzConf, "COM[DESCRIZIONE='" & Utilizzo & "']", "VELOCITA"))
        Rs232Set.Add(Test.RicercaXML(FileXML.FunzConf, "COM[DESCRIZIONE='" & Utilizzo & "']", "PARITA"))
        Rs232Set.Add(Test.RicercaXML(FileXML.FunzConf, "COM[DESCRIZIONE='" & Utilizzo & "']", "NUM_BIT"))
        Rs232Set.Add(Test.RicercaXML(FileXML.FunzConf, "COM[DESCRIZIONE='" & Utilizzo & "']", "STOP_BIT"))

        Test.AperturaCom(Rs232Set)

        Rs232Set = Nothing
    End Sub
    Private Sub Avvio()
        'Dim first As Boolean
        'M = New Threading.Mutex(True, Application.ProductName, first)
        'If (first) Then
        '    M.ReleaseMutex()
        'Else
        '    MessageBox.Show("Sw gestion FVT gi?in esecuzione", FrmPannello.Text)
        '    FrmPannello.Close()
        '    End
        'End If
    End Sub

    Public Sub EseguiTest()


        'VisNewTest(11)
        '  If Lan() = 1 Then  ' StoreDb()



    End Sub


    Public Sub IniteRst()

    End Sub
    Private Function Noer() As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & ContaTest.ToString.PadLeft(2, "0") & "]", "NOER")
    End Function
    Public Function ComPort(ByVal Nome As String) As String
        Return CType(Test.RicercaXML(FileXML.FunzConf, "COM[CODE_BITRON='" & Nome & "']", "NUM_COM"), Integer)
    End Function
    Public Function Debug() As String
        Return CType(Test.RicercaXML(FileXML.FunzConf, "GENERAL", "DEBUG"), Integer)
    End Function
    Public Function SerialGolden(ByVal UUT As Integer) As String
        If UUT = 1 Then
            Return CType(Test.RicercaXML(FileXML.FunzConf, "GENERAL", "SERIALGOLDEN1"), String)
        ElseIf UUT = 2 Then
            Return CType(Test.RicercaXML(FileXML.FunzConf, "GENERAL", "SERIALGOLDEN2"), String)
        End If
    End Function
    Public Function IP_WN_Site1() As String
        Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "IP_WN_SITE1")
    End Function
    Public Function PortUsbUda() As Integer
        Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "PORTUSBUDA")
    End Function
    Public Function IP_WN_Site2() As String
        Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "IP_WN_SITE2")
    End Function
    Public Function COM() As String
        Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "COM")
    End Function
    Public Function Ip_Counter() As String
        Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "IP_COUNTER")
    End Function
    Public Function Trace() As String
        Return CType(Test.RicercaXML(FileXML.FunzConf, "GENERAL", "TRACE"), Integer)
    End Function
    Public Function Noer(ByVal Index As Integer) As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & Index.ToString.PadLeft(2, "0") & "]", "NOER")
    End Function

    Public Function Remark()
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & ContaTest.ToString.PadLeft(2, "0") & "]", "REMARK")
    End Function
    Public Function Remark(ByVal Index As Integer)
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & Index.ToString.PadLeft(2, "0") & "]", "REMARK")
    End Function
    Public Function Sequenza()
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso & "]", "SEQUENZA")
    End Function
    Public Function SH(ByVal Index As Integer) As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & Index.ToString.PadLeft(2, "0") & "]", "SH")
    End Function
    Public Function TH_Feasa(ByVal Parameter As String, ByVal CH As Integer) As Integer
        'Restituisce soglie Feasa in funzione del canale
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA1[CH=" & CH & "]", Parameter)
    End Function
    Public Function TH_FeasaID(ByVal Parameter As String, ByVal Id As Integer) As Double
        'Restituisce soglie Feasa in funzione del canale
        Dim Codeled As Integer
        Codeled = CType(TH_FeasaStringID("CODE", Id), Integer)
        'BinName(Codeled)
        Return Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']", Parameter)
    End Function
Public Function OFS(ByVal Parameter As String, ByVal Id As Integer) As Double
        'Restituisce soglie Feasa in funzione del canale
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA1[ID=" & Id & "]", Parameter)
End Function
    Public Function TH_FeasaCoordinate(ByRef Parameter As Cooordinate, ByVal Id As Integer) As Double
        'Restituisce soglie Feasa in funzione del canale
        Dim Codeled As Integer
        Codeled = CType(TH_FeasaStringID("CODE", Id), Integer)

        'BinName(15701058)' restituisce il bin
        Parameter.X1 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=1]", "CX")
        Parameter.X2 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=2]", "CX")
        Parameter.X3 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=3]", "CX")
        Parameter.X4 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=4]", "CX")

        Parameter.Y1 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=1]", "CY")
        Parameter.Y2 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=2]", "CY")
        Parameter.Y3 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=3]", "CY")
        Parameter.Y4 = Test.RicercaXML(FileXML.FunzConf, "LED/BIN_RANGE[BIN_NAME='" & BinName(Codeled) & "']/CXCY_RANGE_AREA[POINT=4]", "CY")

    End Function
    Public Function TH_FeasaString(ByVal Parameter As String, ByVal CH As Integer) As String
        'Restituisce soglie Feasa in funzionel del canale
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA1[CH=" & CH & "]", Parameter)
    End Function
    Public Function TH_FeasaStringID(ByVal Parameter As String, ByVal Id As Integer) As String
        'Restituisce soglie Feasa in funzionel del canale
        Dim T As String = "30100645"
        Principale.ProgUso = T
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA1[ID=" & Id & "]", Parameter)
    End Function
    Public Function TH_FeasaString_(ByVal Parameter As String, ByVal CH As Integer) As String
        'Restituisce soglie Feasa in funzionel del canale
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA2[CH=" & CH & "]", Parameter)
    End Function
    Public Function TH_FeasaStringID_(ByVal Parameter As String, ByVal Id As Integer) As String
        'Restituisce soglie Feasa in funzionel del canale
        Dim T As String = "30100645"
        Principale.ProgUso = T
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA2[ID=" & Id & "]", Parameter)
    End Function
    Public Function Ledcolor(ByVal CodeLed As String) As String
        'Restituisce soglie Feasa in funzionel del canale
        Return Test.RicercaXML(FileXML.FunzConf, "LED[CODE='" & CodeLed & "']", "COLOR")
    End Function
    Public Function SL(ByVal Index As Integer) As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & Index.ToString.PadLeft(2, "0") & "]", "SL")
    End Function
    Public Function Unit(ByVal Index As Integer) As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & Index.ToString.PadLeft(2, "0") & "]", "UNIT")
    End Function
    Public Function Note(ByVal Index As Integer) As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & Index.ToString.PadLeft(2, "0") & "]", "NOTE")
    End Function
    Public Function SH() As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & ContaTest.ToString.PadLeft(2, "0") & "]", "SH")
    End Function
    Private Function SL() As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & ContaTest.ToString.PadLeft(2, "0") & "]", "SL")
    End Function
    Private Function Unit() As String
        Return Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso.ToString & "]/TEST[NUM=" & ContaTest.ToString.PadLeft(2, "0") & "]", "UNIT")
    End Function
    Public Function Com_Flag_Enel(ByVal Uut As Integer) As String
        Return Test.RicercaXML(FileXML.FunzConf, "COM[CODE_BITRON='FLAG" & (Uut + 1).ToString & "']", "NUM_COM")
    End Function
    Public Function IdCom(ByVal CODE_BITRON As String) As Integer
        Return CType(Test.RicercaXML(FileXML.FunzConf, "COM[DESCRIZIONE='" & CODE_BITRON & "']", "ID_COM"), Integer)
    End Function
    Public Function StrConn() As String
        Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "DB_CONNECTION_STRING")
    End Function
    Private Function Figure() As String
        Return CType(Test.RicercaXML(FileXML.FunzConf, "GENERAL", "FIGURE"), Integer) - 1
    End Function
    Public Function Barcode() As String
        Return CType(Test.RicercaXML(FileXML.FunzConf, "GENERAL", "BARCODE_ON"), Integer)
    End Function
    Public Function SwuP()
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso & "]", "SWUP")
    End Function
    Public Function RelSw() As String
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & ProgUso & "']", "SWREL")
    End Function
    Public Function RangeBrightness() As Integer
        'Range globale per tutti i canali
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & ProgUso & "']", "RANGE_BRIGHTNESS")
    End Function
    Public Function RangeBrightness(ByVal NumChannel As Integer) As Integer
        'Range globale per tutti i canali
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA1[CH=" & NumChannel & "]", "RANGE")
    End Function
    Public Function RangeBrightnessID(ByVal ID As Integer) As Integer
        'Range globale per tutti i canali
        Dim CodeLed As String
        CodeLed = TH_FeasaStringID("CODE", ID)
        Return Test.RicercaXML(FileXML.FunzConf, "LED[CODE='" & CodeLed & "']", "RANGE_BRIGHTNESS")
    End Function
    Public Function CodiceCliente() As String
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & ProgUso & "']", "CODICE_CLIENTE")
    End Function
    Public Function RelHw() As String
        Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & ProgUso & "']", "HWREL")
    End Function
  Public Function MatrixLead()
    Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso & "]", "MATRIXLEAD")
  End Function
  Public Function MatrixLeadMeter()
    Dim ValLead As String
    ValLead = Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso & "]", "MATRIXLEAD")
    If ValLead = "" Then
      Return ""
    Else
      Return Chr(CType("&H" & MatrixLead(), Integer))
    End If


  End Function
  Public Function MatrixTerm()
    Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso & "]", "MATRIXTER")
  End Function
  Public Function SwFram()
    Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso & "]", "SWFRAM")
  End Function

  Public Function Cod_Eclipse()
    Return Test.RicercaXML(FileXML.FunzConf, "VERSIONI[ID=" & ProgUso & "]", "CODICE_ECLIPSE")
  End Function
  Public Function Fase() As String
    Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "FASE")
  End Function
  Public Function NAMESYSTEM() As String
    Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "NAMESYSTEM")
  End Function
  Public Function Id_Rack() As String
    Return Test.RicercaXML(FileXML.FunzConf, "GENERAL", "ID_RACK")
  End Function
  Private Sub Test1()


  End Sub



  Private Function PPM(ByVal Valore As Double) As Integer

    Dim CalcPPM As Double
    CalcPPM = CInt(1000000 * (Valore / 512 - 1))
    Return CalcPPM

  End Function



  Dim Misura As Single
  Dim Risposta As String







  Public Function MediaV(ByVal NumMis As Integer, ByVal Port As String)
    Dim T As Integer
    Dim Misure As Single
    Dim MisTot As Single
    Try
      For T = 1 To NumMis
        Misure = Math.Abs(CType(Test.ReadV(Port), Single))
        MisTot = MisTot + Misure
        Test.Sleep(10)
      Next T
      Return FormatNumber((MisTot / NumMis), 2)
    Catch
      Return 0
    End Try

  End Function


  Private Sub AbortThread(ByVal Thr As System.Threading.Thread)
    If Thr.IsAlive = False Then
      Try
        Thr.Abort()
      Catch
      End Try
    End If
  End Sub

  'Routine anciata dal thred Verifica comunicazione con il meter e scrive dati in flash mediante protocollo Flag



  Private Function FormatMsn(ByVal Msn As String)
    Dim T As Integer
    Dim Elab As String
    Dim Dato As String
    For T = 0 To Msn.Length - 1
      Dato = (Val(Msn(T)) + 30).ToString
      Elab = Elab + Dato 'Il data va inviato in esadecimale ma stringa 30=0"
    Next T
    Return Elab
  End Function
  Public Sub RunScriptFlashSNOR(ByVal UUT As Integer)
    'RUN script per porre la SPEAR in TRESTATE e programma la SNOR
    Dim P As System.Diagnostics.Process
    Dim Report As String
    Try
      Directory.SetCurrentDirectory("c:\bitron\flasher")
      P = Process.Start("c:\bitron\flasher\InpecoConnect.bat")
      P.WaitForExit() 'Attesa fine processo
      Report = My.Computer.FileSystem.ReadAllText("c:\bitron\flasher\ResultConnect.txt")
      If RegFindText(Report, "Connected to SPEAr320") = 1 Then
        P = Process.Start("c:\bitron\flasher\InpecoFlasher.bat")
        P.WaitForExit() 'Attesa fine processo
        Report = My.Computer.FileSystem.ReadAllText("c:\bitron\flasher\ResultFlasher.txt")
        If RegFindText(Report, "Verifying Checksum ... OK") <> 6 Then
          Esito(0) = False
          MsgDispService("UUT:" + UUT.ToString + " Flash SNOR FAIL", 0)
        End If

      Else
        Esito(0) = False
        MsgDispService("UUT:" + UUT.ToString + " USB Connection FAIL", 0)
      End If
    Catch
      Esito(0) = False
      MsgBox("Run script Fail", MsgBoxStyle.Critical)
    End Try

  End Sub
  Public Function RegFindText(ByVal Testo As String, ByVal StrRicerca As String) As Integer
    'Restituisce il numero dei match trovati in testo
    'attenzione escludere i chr(13) ne pattern di ricerca

    Try
      ' Dim regexPattern As String = "(\b" & "Verifying Checksum ... OK" & "\b)"
      Dim regexPattern As String = "(\b" & StrRicerca & "\b)"
      Dim regexResults As MatchCollection = Regex.Matches(Testo, regexPattern, RegexOptions.ExplicitCapture)
      Dim matchesCount As Integer = regexResults.Count
      If matchesCount > 0 Then
        'For Each elemento As Match In regexResults
        'MessageBox.Show(elemento.Value, "Ho trovato")
        'Next
      End If
      Return matchesCount

    Catch
      Return 0
    End Try


  End Function

End Module