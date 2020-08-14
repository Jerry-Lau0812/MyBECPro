Public Class Uda

    Private Const BoudRate = &H20 '9600
    Private HandlerUDA As UInt32 = 0

    Public Structure SVdBusFrameInfo
        Dim FrameInfoSize As UInt32
        Dim Flags As UInt32
        Dim TargetAddress As UInt32
        Dim AckType As UInt32
        Dim AckCommpartner As UInt32
        Dim Messagesize As UInt32
        Dim Checksum As UInt32
    End Structure

    Private Declare Function dbusOpenPort Lib "DLL\DBusDrv.dll" (ByVal UsbIDport As UInt32, ByVal PortCommandPartner As UInt32, ByVal boudrate As UInt32, ByRef HE As UInt32) As UInt32
    Private Declare Function dbusClosePort Lib "DLL\DBusDrv.dll" (ByVal He As UInt32) As UInt32
    Private Declare Function dbusSendFrame Lib "DLL\DBusDrv.dll" (ByVal He As UInt32, ByVal TergetAddress As UInt32, ByVal MessageDate() As Byte, ByVal MessageDataSize As UInt32) As UInt32
    Private Declare Function dbusGetFrameInfo Lib "DLL\DBusDrv.dll" (ByVal He As UInt32, ByRef DbusFrameInfo As SVdBusFrameInfo) As UInt32
    Private Declare Function dbusGetFrameData Lib "DLL\DBusDrv.dll" (ByVal He As UInt32, ByVal messageBuffer() As Byte, ByVal messageBufferSize As UInt32, ByVal retCopyCount As Int32) As UInt32
    Public Function OpenPortUda() As Integer
        Dim Result As UInt32 = 1
        'Return 0 if fok
        Try
            ''Result = dbusClosePort(HandlerUDA) '0 =OK

            Result = dbusOpenPort(&H100 + PortUsbUda(), &HC, BoudRate, HandlerUDA)
        Catch
            Result = 99
        End Try
        Return Result
    End Function
    Public Function ClosePortUda() As Integer
        Dim Result As UInt32 = 1
        'Return 0 if fok
        Try
            ''Result = dbusClosePort(HandlerUDA) '0 =OK

            Result = dbusClosePort(HandlerUDA)
        Catch
            Result = 99
        End Try
        Return Result
    End Function
    Public Function LedOn(ByVal NumLed As Integer) As Integer
        Dim ByteMessage(30) As Byte
        Dim Result As Integer
        Try
            ByteMessage(0) = &H51
            ByteMessage(1) = &H60
            ByteMessage(2) = NumLed
            Result = dbusSendFrame(HandlerUDA, &H11, ByteMessage, 3)
            Return Result

        Catch
            Return Result
        End Try
    End Function
    Public Function WriteData(ByVal Data As String, ByVal Sa As Integer)
        Dim Result As Integer = 1
        Dim ByteMessage(30) As Byte
        Dim VarRead As SVdBusFrameInfo
        Dim NumByte As Integer = Data.Length
        Dim T As Integer
        Try
            ByteMessage(0) = &HF3
            ByteMessage(1) = &H0
            ByteMessage(2) = &HFF
            Result = dbusSendFrame(HandlerUDA, &H10, ByteMessage, 3)
            Principale.Test.Sleep(100)
            If Result = 0 Then

                ByteMessage(0) = &HF2
                ByteMessage(1) = &H0
                ByteMessage(2) = NumByte
                ByteMessage(3) = &H0
                ByteMessage(4) = Sa

                For T = 5 To 5 + NumByte - 1
                    ByteMessage(T) = Asc(Data(T - 5)) 'Data 
                Next
                Result = dbusSendFrame(HandlerUDA, &H10, ByteMessage, 5 + NumByte)
                Principale.Test.Sleep(100)
            End If
        Catch
            Result = 1
        End Try
        Return Result
    End Function
    Public Function ReadData(ByVal Sa As Integer, ByVal Numbyte As Integer) As String
        Dim Result As UInt32
        Dim ByteMessage(30) As Byte
        Dim VarRead As SVdBusFrameInfo
        Dim Buffer(200) As Byte
        Dim Dati As String = ""
        Dim NumbyteRead As Integer
        Dim T As Integer
        Dim TimeoutRead As Integer = 10
        Try
            For T = 0 To 200
                Buffer(T) = 0
            Next T

            ByteMessage(0) = &HF3
            ByteMessage(1) = &H0
            ByteMessage(2) = &HFF
            Result = dbusSendFrame(HandlerUDA, &H10, ByteMessage, 3)
            Principale.Test.Sleep(100)
            If Result = 0 Then
                'address Memory
                ByteMessage(0) = &HF0
                ByteMessage(1) = &H0
                ByteMessage(2) = &HC0

                ByteMessage(3) = Numbyte 'num Byte 
                ByteMessage(4) = &H0
                ByteMessage(5) = Sa ' haddress
                Result = dbusSendFrame(HandlerUDA, &H10, ByteMessage, 6)
                VarRead.FrameInfoSize = System.Runtime.InteropServices.Marshal.SizeOf(VarRead)

                If Result = 0 Then
                    Result = 1
                    While Result <> 0 And TimeoutRead > 0
                        TimeoutRead = TimeoutRead - 1
                        Principale.Test.Sleep(100)
                        Result = dbusGetFrameInfo(HandlerUDA, VarRead)
                        Principale.Test.Sleep(100)
                        If Result = 0 And VarRead.Messagesize <> 0 Then
                            Result = dbusGetFrameData(HandlerUDA, Buffer, VarRead.Messagesize, NumbyteRead)
                            For T = 4 To 4 + Numbyte - 1
                                Dati = Dati + Chr(Buffer(T))
                            Next T
                        Else
                            Result = 1
                        End If
                    End While
                End If
            End If
        Catch
        End Try

        Return (Dati)

    End Function
    Public Function ReadIR() As String
        Dim Result As UInt32
        Dim ByteMessage(30) As Byte
        Dim VarRead As SVdBusFrameInfo
        Dim Buffer(200) As Byte
        Dim Dati As String = ""
        Dim NumbyteRead As Integer
        Dim T As Integer
        Dim TimeoutRead As Integer = 1000
        Try
            ByteMessage(0) = &HF3
            ByteMessage(1) = &H0
            ByteMessage(2) = &HFF
            Result = dbusSendFrame(HandlerUDA, &H10, ByteMessage, 3)
            Principale.Test.Sleep(100)

        
                VarRead.FrameInfoSize = System.Runtime.InteropServices.Marshal.SizeOf(VarRead)

                If Result = 0 Then
                    Result = 1
                    While Result <> 0 And TimeoutRead > 0
                        TimeoutRead = TimeoutRead - 1
                        Principale.Test.Sleep(100)
                        Result = dbusGetFrameInfo(HandlerUDA, VarRead)
                        Principale.Test.Sleep(100)
                        If Result = 0 And VarRead.Messagesize <> 0 Then
                            Result = dbusGetFrameData(HandlerUDA, Buffer, VarRead.Messagesize, NumbyteRead)
                        For T = 4 To 4 + NumbyteRead - 1
                            Dati = Dati + Chr(Buffer(T))
                        Next T
                        Else
                            Result = 1
                        End If
                    End While
                End If

        Catch
        End Try

        Return (Dati)
    End Function
End Class

