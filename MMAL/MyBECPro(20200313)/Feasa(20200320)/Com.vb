Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Public Class Com
    'classe per la gestione della Com 
    'Il pic dovra` avere sempre la con ID=0 
    Private TempBuf As String
    Private TempBufValido As String
    Private StartRx As Boolean = False
    Public WithEvents TimerMsg As New Timer
    Private EndRx As Boolean = False
    Private TempoTimeout As Integer
    Private DatoValido As New ArrayList
    'Public Rs232(0) As Serial
    Public Rs232(0) As Serial
    Public Rs232_(0) As Serial
    Private TestAvviato As Boolean = False
    Private StartCHR As String = "!~<"' "#"
    Private EndCHR As String = ">~!" '"*"
    Private NumCHR As Integer
  Private EnBufOnTimeout As Boolean = False 'abilita restituzione del bufffer se si ?arrivati alla condizione timeout
  Private NumCharRestituiti As Integer 'Numero di caratteri restituiti dalla com

    Public Enum OpenFile
        Append = True
        Write = False
    End Enum
    Public Enum Verso
        PcTrasmette = 0
        PcRiceve = 1
    End Enum
    Public Sub AperturaCom(ByVal SetCom As List(Of String))
        Dim Risposta As String
        Try
            Rs232(UBound(Rs232)) = New Serial(UBound(Rs232))
            Rs232(UBound(Rs232)).PortName = "COM" & SetCom(0)
            Rs232(UBound(Rs232)).BaudRate = SetCom(1)
            Rs232(UBound(Rs232)).Parity = SetCom(2) 'Parity.None =0
            Rs232(UBound(Rs232)).DataBits = SetCom(3)
            Rs232(UBound(Rs232)).StopBits = SetCom(4) 'StopBits.One 1
            Rs232(UBound(Rs232)).Encoding = System.Text.Encoding.GetEncoding(1252) 'per inviare caratteri Asci>128
            Rs232(UBound(Rs232)).Open()
            Rs232(UBound(Rs232)).ReadTimeout = 1000
            Rs232(UBound(Rs232)).DtrEnable = True
            AddHandler Rs232(UBound(Rs232)).DataReceived, AddressOf Com_DataReceived
            ReDim Preserve Rs232(UBound(Rs232) + 1)
        Catch
            MsgBox("Cannot open com " + "COM" & SetCom(0), MsgBoxStyle.Critical)
            SiteResultWrite(1, RESULT_FAIL) : FailFlag = RESULT_FAIL
            SiteResultWrite(2, RESULT_FAIL)
        End Try
      
    End Sub

    Public Sub AperturaCom_(ByVal SetCom As List(Of String))
        Dim Risposta As String
        Try
            Rs232_(UBound(Rs232_)) = New Serial(UBound(Rs232_))
            Rs232_(UBound(Rs232_)).PortName = "COM" & SetCom(0)
            Rs232_(UBound(Rs232_)).BaudRate = SetCom(1)
            Rs232_(UBound(Rs232_)).Parity = SetCom(2) 'Parity.None =0
            Rs232_(UBound(Rs232_)).DataBits = SetCom(3)
            Rs232_(UBound(Rs232_)).StopBits = SetCom(4) 'StopBits.One 1
            Rs232_(UBound(Rs232_)).Encoding = System.Text.Encoding.GetEncoding(1252) 'per inviare caratteri Asci>128
            Rs232_(UBound(Rs232_)).Open()
            Rs232_(UBound(Rs232_)).ReadTimeout = 1000
            Rs232_(UBound(Rs232_)).DtrEnable = True
            AddHandler Rs232_(UBound(Rs232_)).DataReceived, AddressOf Com_DataReceived
            ReDim Preserve Rs232_(UBound(Rs232_) + 1)
        Catch
            MsgBox("Cannot open com " + "COM" & SetCom(0), MsgBoxStyle.Critical)
            SiteResultWrite(1, RESULT_FAIL) : FailFlag = RESULT_FAIL
            SiteResultWrite(2, RESULT_FAIL)
        End Try

    End Sub
    
    
    Public Sub Com_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)

        Select Case DirectCast(sender, Serial).Index
      Case 0
        Principale.Test.Sleep(100)
        RiceveFeasa(DirectCast(sender, Serial).Index)
        '   RiceveBarcode(StartCHR, EndCHR, DirectCast(sender, Serial).Index)
            Case 1
                Pic(StartCHR, EndCHR, DirectCast(sender, Serial).Index)
            Case 2
                Pic(StartCHR, EndCHR, DirectCast(sender, Serial).Index)
            Case 3
                RiceveBarcode(StartCHR, EndCHR, DirectCast(sender, Serial).Index)
            Case 4
                RiceveBarcode(StartCHR, EndCHR, DirectCast(sender, Serial).Index)
            Case 5
                RiceveFlash(DirectCast(sender, Serial).Index)
        End Select
    End Sub
    Private Sub RiceveFlash(ByVal NumCom As Integer)
        'Dati inviati dal FlashProg
        Dim ByteRx As String
        Dim DatiRx As New ArrayList
        ByteRx = Rs232(NumCom).ReadExisting
        ByteRx = TempBuf & ByteRx
        TempBuf = ByteRx

        If ByteRx.Length = NumCHR Then
            'DatoValido.Add(ByteRx.Substring(0, Nchr))
            'LogDato(ByteRx.Substring(0, Nchr), OpenFile.Append, Verso.PcRiceve)
            DatoValido.Add(ByteRx)
            LogDato(ByteRx, OpenFile.Append, Verso.PcRiceve)
            TempBuf = ""
        End If
    End Sub

    Private Sub RiceveFeasa(ByVal NumCom As Integer)
        'Dati inviati dal FlashProg
        Dim ByteRx As String
        Dim DatiRx As New ArrayList
        If NumCom = 1 Then
            ByteRx = Rs232_(0).ReadExisting
        Else
            ByteRx = Rs232(NumCom).ReadExisting
        End If
        TempBuf = ByteRx
        '  Principale.Test.Sleep(100)
        If ByteRx.IndexOf(Chr(4)) > -1 Or ByteRx.Length >= NumCharRestituiti Then DatoValido.Add(ByteRx) : NumCharRestituiti = 0 'Or ByteRx.IndexOf(Chr(10)) > -1
        '  LogDato(ByteRx, OpenFile.Append, Verso.PcRiceve)

        'End If
  End Sub

    Private Sub Pic(ByVal ChrStart As String, ByVal ChrEnd As String, ByVal NumCom As Integer)
        'Dati inviati dal Pic
        Dim ByteRx As String
        Dim DatiRx As New ArrayList
        Dim T As Integer
        Dim T2 As Integer
        Dim Pos As Integer
        Dim DatiProces As String
        Dim Cancel As Boolean = False
        'Regex("(?<=\#)[^*]+(?=\*)")
        ByteRx = Rs232(NumCom).ReadExisting
        ByteRx = TempBuf & ByteRx
        'rimuove tutti i caratteri prima del #
        If InStr(ByteRx, ChrStart) > 0 AndAlso ByteRx.Substring(0, 1) <> ChrStart Then
            ByteRx = ByteRx.Substring(InStr(ByteRx, ChrStart) - 1)
        End If
        ' se ?presente un cancelletto prima di un asterisco rimuove tutti i caratteri prima del 2?
        For T = 0 To InStr(ByteRx, ChrEnd) - 1
            If ByteRx.Substring(T, 1) = ChrStart Then T2 = T2 + 1 : Pos = T
        Next T
        If T2 >= 2 Then ByteRx = ByteRx.Substring(Pos)
        Dim Reg As New Regex("(?<=\" & ChrStart & ")[^" & ChrEnd & "]+(?=\" & ChrEnd & ")") 'caratteri compresi tra # e *    
        For Each RegSp As Match In Reg.Matches(ByteRx)
            DatiRx.Add(RegSp.Groups(0).Value)
        Next
        For T = 0 To DatiRx.Count - 1
            LogDato(DatiRx(T), OpenFile.Append, Verso.PcRiceve)
            DatoValido.Add(DatiRx(T))
            DatiProces = DatiProces + DatiRx(T).ToString
        Next T
        If DatiProces <> "" Then
            TempBuf = ByteRx.Substring(DatiProces.Length + DatiRx.Count * 2)
        Else
            TempBuf = ByteRx
        End If

    End Sub
    Private Sub RiceveBarcode(ByVal ChrStart As String, ByVal ChrEnd As String, ByVal NumCom As Integer)
        'Dati inviati dal Pic
        Dim ByteRx As String
        Dim DatiRx As New ArrayList
        Dim T As Integer
        Dim T2 As Integer
        Dim Pos As Integer
        Dim DatiProces As String
        Dim Cancel As Boolean = False
        Const STX = ""
        'Regex("(?<=\#)[^*]+(?=\*)")
        ByteRx = Rs232(NumCom).ReadExisting
        ByteRx = TempBuf & ByteRx
        'rimuove tutti i caratteri prima del #
        If InStr(ByteRx, ChrStart) > 0 AndAlso ByteRx.Substring(0, 1) <> ChrStart Then
            ByteRx = ByteRx.Substring(InStr(ByteRx, ChrStart) - 1)
        End If
        ' se ?presente un cancelletto prima di un asterisco rimuove tutti i caratteri prima del 2?
        For T = 0 To InStr(ByteRx, ChrEnd) - 1
            If ByteRx.Substring(T, 1) = ChrStart Then T2 = T2 + 1 : Pos = T
        Next T
        If T2 >= 2 Then ByteRx = ByteRx.Substring(Pos)

        'introduce carattere di start dopo il cancelletto
        ByteRx = ByteRx.Replace("#", "#" + STX)
        Dim Reg As New Regex("(?<=\" & ChrStart & ")[^" & ChrEnd & "]+(?=\" & ChrEnd & ")") 'caratteri compresi tra # e *    
        For Each RegSp As Match In Reg.Matches(ByteRx)
            DatiRx.Add(RegSp.Groups(0).Value)
        Next
        For T = 0 To DatiRx.Count - 1
            LogDato(DatiRx(T), OpenFile.Append, Verso.PcRiceve)
            DatoValido.Add(DatiRx(T))
            DatiProces = DatiProces + DatiRx(T).ToString
        Next T
        If DatiProces <> "" Then
            TempBuf = ByteRx.Substring(DatiProces.Length + DatiRx.Count * 2)
        Else
            TempBuf = ByteRx
        End If

    End Sub
   
    Public Sub LogDato(ByVal Dato As String, ByVal FileOpen As OpenFile, ByVal InviaRicevi As Verso)

        Try
            If InviaRicevi = Verso.PcTrasmette Then
                My.Computer.FileSystem.WriteAllText(CurDir() + "\Ricevuto.txt", "-------->PC-trasmette:" + Now.ToLongTimeString + "-" + Dato + Chr(13) + Chr(10), FileOpen)
            Else
                My.Computer.FileSystem.WriteAllText(CurDir() + "\Ricevuto.txt", "<-PC-riceve:" + Now.ToLongTimeString + "-" + Dato + Chr(13) + Chr(10), FileOpen)
            End If

        Catch
        End Try

    End Sub
    Public Sub ClearFileLog()
        Try
            My.Computer.FileSystem.WriteAllText(CurDir() + "\Ricevuto.txt", "", OpenFile.Write)
        Catch
        End Try

    End Sub
    Public Sub Sleep(ByVal Ms As Integer)
        System.Threading.Thread.Sleep(Ms)
    End Sub
    Public Sub SvuotaBuffer()
        Dim ByteRx As String
        Try
            TempBuf = ""
            For T As Integer = 0 To UBound(Rs232) - 1
                ByteRx = Rs232(T).ReadExisting
            Next T
        Catch
        End Try

    End Sub
    Public Function Ricevi(ByVal Tempo As Integer)
        Dim Dato As String = ""
        Tempo = Tempo * 1000
        While DatoValido.Count = 0 And Tempo > 0
            Application.DoEvents()
      Sleep(10)
      Tempo = Tempo - 10
        End While
        If Tempo > 0 Then
            Dato = DatoValido.Item(0)
            DatoValido.RemoveAt(0)
        Else
            If EnBufOnTimeout = True Then
                Dato = TempBuf
                TempBuf = ""
                EnBufOnTimeout = False
            End If
        End If
        StartCHR = "!~<"
        EndChr = ">~!"

        Return Dato

    End Function


    Private Sub TimerMsg_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerMsg.Disposed

    End Sub
    

    Private Sub TimerMsg_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerMsg.Tick
  
    End Sub


    Public Overloads Sub Invia(ByVal Dato As String)
        Dim Risposta As String
        Dim T As Integer
        SvuotaBuffer()
        'StartCHR = "#"
        'EndCHR = "*"
        'StartCHR = "#"
        'EndCHR = "*"
        StartCHR = "!~<"
        EndCHR = ">~!"
        Rs232(0).Write("!~<" & Dato & ">~!")
        '  Rs232(0).Write("!~<READY>~!")
        ' For T = 0 To Dato.Length - 1
        ' Sleep(2)
        ' Rs232(0).Write(Dato.Substring(T, 1))
        ' Next T
        'Sleep(2)
        'Rs232(0).Write(">~!")
        LogDato(Dato, OpenFile.Append, Verso.PcTrasmette)
    End Sub
    Public Overloads Sub Invia(ByVal Dato As String, ByVal ChrStart As String, ByVal ChrEnd As String)
        Dim Risposta As String
        Dim T As Integer
        SvuotaBuffer()
        StartCHR = ChrStart
        EndCHR = ChrEnd
        Rs232(0).Write("#")
        For T = 0 To Dato.Length - 1
            Sleep(2)
            Rs232(0).Write(Dato.Substring(T, 1))
        Next T
        Sleep(2)
        Rs232(0).Write("*")
        LogDato(Dato, OpenFile.Append, Verso.PcTrasmette)
    End Sub

    Public Overloads Sub Invia(ByVal Dato As String, ByVal NumCom As Integer)
        Dim Risposta As String
        Dim T As Integer
        Try
            StartCHR = "#"
            EndCHR = "*"
            SvuotaBuffer()
            For T = 0 To Dato.Length - 1
                Sleep(2)
                Rs232(NumCom).Write(Dato(T))
            Next T
            LogDato(Dato, OpenFile.Append, Verso.PcTrasmette)
        Catch
        End Try
    End Sub

    Public Overloads Sub Invia(ByVal Dato As String, ByVal NumCom As Integer, ByVal ChrStart As String, ByVal ChrEnd As String)
        Dim Risposta As String
        Dim T As Integer
        SvuotaBuffer()
        StartCHR = ChrStart
        EndCHR = ChrEnd
        For T = 0 To Dato.Length - 1
            Sleep(2)
            Rs232(NumCom).Write(Dato(T))
        Next T
        LogDato(Dato, OpenFile.Append, Verso.PcTrasmette)
    End Sub
    Public Overloads Sub Invia(ByVal Dato As String, ByVal NumCom As Integer, ByVal BufOnTimeout As Boolean)
        Dim Risposta As String
        Dim T As Integer
        SvuotaBuffer()
        EnBufOnTimeout = BufOnTimeout ' se true restituisce tutto il buffer anche se si ?arrivati al timeout 
        Rs232(NumCom).Write(Dato)
        'LogDato(Dato, OpenFile.Append, Verso.PcTrasmette)
    End Sub
  Public Overloads Sub Invia(ByVal Dato As String, ByVal NumCom As Integer, ByVal NumChar As Integer)
    Dim Risposta As String
    Dim T As Integer
    NumCharRestituiti = NumChar
    SvuotaBuffer()
        'EnBufOnTimeout = BufOnTimeout ' se true restituisce tutto il buffer anche se si ?arrivati al timeout 

        Rs232(NumCom).Write(Dato)


        'LogDato(Dato, OpenFile.Append, Verso.PcTrasmette)
    End Sub

  Public Function ReadV(ByVal Port As String)

  End Function
    Public Sub New()
        TimerMsg.Interval = 10
        TimerMsg.Enabled = True

    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        'chiude tutte le porte aperte
        For T As Integer = 0 To UBound(Rs232) - 1
            Try
                Rs232(T).Close()
            Catch

            End Try
        Next (T)

    End Sub
End Class
