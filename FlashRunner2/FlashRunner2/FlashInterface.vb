Imports System.Runtime.InteropServices
Imports System.Text
Imports System.IO


Public Class FlashInterface
    Private Const DllName As String = "FR_COMM.dll"
    Private fr_handle As Integer
    Private answer As Integer
    Private ANSI As Encoding = Encoding.GetEncoding(1252) '拉丁字符集
    Private ErrMsg As String = ""

    '用于FR_SendFileA函数的委托
    Public Delegate Sub TransferStatus(ByVal file_len As Integer, ByVal file_pos As Integer)
    Public PresentProgress As TransferStatus


#Region "DllBase"
    'FR_COM Function'
    <DllImport(DllName)>
    Private Shared Sub FR_GetLastErrorMessageA(ByRef error_msg As Byte, ByRef string_len As ULong)
    End Sub

    <DllImport(DllName)>
    Private Shared Function FR_OpenCommunicationW(ByVal com_port As Integer, ByVal com_settings As Integer) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_OpenCommunicationA(ByVal com_port As String, ByVal com_settings As String) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_CloseCommunicationW(ByVal handle As Integer) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_CloseCommunicationA(ByVal handle As Integer) As Integer
    End Function

    ' ** Commands transmission
    <DllImport(DllName)>
    Private Shared Function FR_SendCommandW(ByVal handle As Integer, ByVal command As Integer) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_SendCommandA(ByVal handle As Integer, ByVal command As String) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_GetAnswerW(ByVal handle As Integer, ByVal answer As Byte(), ByVal maxlen As Integer, ByVal timeout_ms As Integer) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_GetAnswerA(ByVal handle As Integer, ByVal answer As Byte(), ByVal maxlen As Integer, ByVal timeout_ms As Integer) As Integer
    End Function

    '** File Transfer
    <DllImport(DllName)>
    Private Shared Function FR_SendFileW(ByVal handle As Integer, ByVal protocol As Integer, ByVal src_filename As Integer, ByVal dst_path As Integer, ByVal callback As [Delegate]) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_SendFileA(ByVal handle As Integer, ByVal protocol As String, ByVal src_filename As String, ByVal dst_path As String, ByVal callback As [Delegate]) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_GetFileW(ByVal handle As Integer, ByVal protocol As Integer, ByVal src_filename As Integer, ByVal dst_path As Integer, ByVal callback As [Delegate]) As Integer
    End Function

    <DllImport(DllName)>
    Private Shared Function FR_GetFileA(ByVal handle As Integer, ByVal protocol As String, ByVal src_filename As String, ByVal dst_path As String, ByVal callback As [Delegate]) As Integer
    End Function
#End Region

#Region "Base Function"
    ''' <summary>与FlashRunner建立连接</summary>
    ''' <returns>True/Success   
    '''          False/Fail</returns>
    ''' <remarks></remarks>
    Public Function OpenCommunicationA() As Boolean
        Dim Res As Boolean = False
        Try
            fr_handle = FR_OpenCommunicationA("LAN", "192.168.1.100:1234")
            If fr_handle = 0 Then
                Call GetLastErrMsg()
                Res = False
            Else
                Res = True
            End If
        Catch ex As Exception
            Res = False
            ErrMsg = "Close CommunicationA Fail: " & ex.Message
        End Try
        Return Res
    End Function

    ''' <summary>
    ''' 关闭与FlashRunner连接
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CloseCommunicationA() As Boolean
        Dim Res As Boolean = False
        Try

            If FR_CloseCommunicationA(fr_handle) <> 0 Then
                Call GetLastErrMsg()
                Res = False
            Else
                Res = True
            End If
        Catch ex As Exception
            Res = False
            ErrMsg = "Close CommunicationA Fail: " & ex.Message
        End Try
        fr_handle = 0
        Return Res
    End Function

    ''' <summary>
    ''' 获取错误信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetLastErrMsg()
        Dim Err(512) As Byte
        Dim Len As Integer = Err.Length
        FR_GetLastErrorMessageA(Err(0), Len)
        'Me.ErrMsg = DotNet.Utilities.StringHelper.DeleteChar(ANSI.GetString(Err))
    End Sub

    ''' <summary>
    ''' 发送命令到FlushRunner
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="stepMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCmd_DLL(ByVal cmd As String, Optional ByVal stepMsg As String = "") As Boolean
        'Send Command to FlashRunner
        answer = FR_SendCommandA(fr_handle, cmd & Chr(32) & Chr(13))
        'answer = FR_SendCommandA(fr_handle, cmd & Chr(13))
        Return IIf(answer = 0, True, False)
    End Function


    ''' <summary>
    ''' 获取FlushRunner返回的结果
    ''' </summary>
    ''' <param name="Timeout"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnswer_DLL(Optional ByVal Timeout As Single = 1) As String
      

        Dim StrRead As String = ""
        Dim ReadCmds(100) As Byte

        Call FR_GetAnswerA(fr_handle, ReadCmds, 255, Timeout * 1000)

        StrRead = ANSI.GetString(ReadCmds)

        Try
            My.Computer.FileSystem.WriteAllText("1.log",StrRead,True)
        Catch

        End Try
     

        Return StrRead
    End Function

    ''' <summary>
    ''' 发送命令并接受结果
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Timeout"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendRec_DLL(ByVal Cmd As String, Optional ByVal Timeout As Single = 3, Optional ByRef Msg As String = "") As Boolean
        Dim TestOK As Boolean = False
        Dim Retry As Integer = 0
        Dim Result As Boolean = SendCmd_DLL(Cmd)

        Dim Rec As String = GetAnswer_DLL(Timeout)
        Msg = Rec

        Dim RecValue As Boolean = IIf(Rec.Contains(">") And Result = True, True, False)
        If RecValue = False Then Call GetLastErrMsg()

        Return RecValue
    End Function


    ''' <summary>
    ''' 删除特定文件夹下面的所有文件
    ''' </summary>
    ''' <param name="Ch"></param>
    ''' <param name="dir"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelectFileFormSD(ByVal Ch As Integer, ByVal dir As String) As Boolean
        Dim Cmd As String = Ch & Chr(32) & "FSDELDIR" & Chr(32) & dir
        'Dim Cmd As String = Ch & Chr(32) & "FSDELDIR" & dir
        Dim Result As Boolean = SendRec_DLL(Cmd)
        Return Result
    End Function
    ''' <summary>
    ''' 检查FlushRunner中文件是否存在
    ''' </summary>
    ''' <param name="Ch"></param>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FileExists(ByVal Ch As Integer, ByVal FileName As String) As Boolean
        Dim Cmd As String = Ch & Chr(32) & "FSEXIST" & Chr(32) & FileName
        Dim Result As Boolean = SendRec_DLL(Cmd)
        Return Result
    End Function

    ''' <summary>
    ''' '发送文件到特定的文件夹中
    ''' </summary>
    ''' <param name="sourcePath"></param>
    ''' <param name="destinationPath"></param>
    ''' <param name="protocol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyFileToSD(ByVal SourcePath As String, Optional ByVal DestinationPath As String = "\LOGS", Optional ByVal protocol As String = "YMODEM") As Boolean
        Dim Retry As Integer = 0
        Dim TestOK As Boolean = False
        ''Copty File To SD
        Do While Retry < 3 And Not TestOK
            answer = FR_SendFileA(fr_handle, protocol, SourcePath, DestinationPath, PresentProgress)
            TestOK = IIf(answer = 0, True, False)
            If Not TestOK Then
                Retry += 1
                Call GetLastErrMsg()
            End If
        Loop
        Return TestOK
    End Function

    ''' <summary>
    ''' Get File CRC
    ''' </summary>
    ''' <param name="Ch"></param>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    Public Function GetFileCRC32(ByVal Ch As Integer, ByVal FileName As String, Optional ByVal Timeout As Single = 10) As String
        Dim Rec As String = ""
        Dim Cmd As String = Ch & Chr(32) & "SGETCRC32 FILE" & Chr(32) & FileName
        Dim Result As Boolean = SendRec_DLL(Cmd, Timeout, Msg:=Rec)

        If Result Then
            Dim StartIndex As Integer = Rec.IndexOf("$")
            Dim CRC = Rec.Substring(StartIndex).Replace(">", "").Replace("$", "")
            Return CRC
        Else
            Return "FAIL"
        End If
    End Function

    ''' <summary>
    ''' 获取连接状态
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFr2Info() As String
        Dim Fr2Info(50) As Byte
        Dim Cmd As String = "#55*SPING"
        Dim Rec As String = ""

        Call SendCmd_DLL(Cmd)
        Call FR_GetAnswerA(fr_handle, Fr2Info, 255, 100)
        Rec = ANSI.GetString(Fr2Info)

        Return Rec
    End Function

    ''' <summary>
    ''' 获取版本信息
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFr2Version() As String
        Dim Fr2Info(50) As Byte
        Dim Cmd As String = "#55*SGETVER"
        Dim Rec As String = ""

        Call SendCmd_DLL(Cmd)
        Call FR_GetAnswerA(fr_handle, Fr2Info, 255, 100)
        Rec = ANSI.GetString(Fr2Info)
        Return Rec
    End Function

    Public Function FlashProgram(ByVal ProjectName As String, Optional ByVal Channel As String = "1111", Optional ByVal TimeOut As Integer = 15) As Boolean
        Dim ChannelNo As Integer = 0
        Dim IsCorrectChannel As Boolean = True
        Dim Result As Boolean = False

        Dim SStr As New List(Of String)

        For Each s In Channel
            If s <> "0" And s <> "1" Then
                IsCorrectChannel = False
            End If
        Next

        If IsCorrectChannel Then
            For i = 0 To Channel.Length - 1
                ChannelNo += Channel.Substring(i, 1) * (2 ^ (Channel.Length - i - 1))
            Next

            Dim SendCmd As String = "#" & ChannelNo & "|RUN" & Chr(32) & ProjectName & Chr(10)

            Try
                Result = SendCmd_DLL(SendCmd)
            Catch ex As Exception
                Result = False
            End Try
        End If
        Return Result
    End Function


    Public Function FlashMultiProgram(ByVal FlashList As List(Of String)) As Boolean
        Dim Result As String = ""
        For Each FlashInfo In FlashList
            Try
                Result = Me.FlashProgram(FlashInfo.Split(":")(0), FlashInfo.Split(":")(1), -1)
            Catch ex As Exception
                Result = False
            End Try
        Next
        Return Result
    End Function


    Public Function Wait_FlashProgram_FlashRunner2_0(ByVal SiteConfig As String, ByRef FlashResult As List(Of String)) As Boolean
        Dim RecStr As String = ""
        Dim FlashOK As Boolean
        Dim Channel As String = ""

        ' While Not FlashOK And Retry <= 2
        'RecStr = RiceveData_FlashRunner2_0("0011", 25)
        Dim SiteUse As Integer = 0
        For Each i In SiteConfig
            If i = "1" Then
                SiteUse += 1
            End If
        Next

        RecStr = RiceveData_FlashRunner2_0(SiteConfig, 25)

        Try
            If RecStr.Split("-").Length = SiteUse Then
                FlashResult.AddRange(RecStr.Split("-"))
                FlashOK = True
            End If
        Catch ex As Exception
            FlashOK = False
        End Try
        Return FlashOK
    End Function

    Private Function RiceveData_FlashRunner2_0(Optional ByVal Ch_Str As String = "1111", Optional ByVal Timeout As Single = 20) As String
        Dim Dato As String = ""
        Dim SStr As New List(Of String)
        SStr = Riceve_FlashRunner2_0(Ch_Str, Timeout)
        Try
            For Each s In SStr
                If s.Contains("!") Then
                    Dim p = s.Split(Chr(10))
                    Dato &= p(p.Count - 1).Substring(0, 2) & "FAIL-"
                Else
                    Dato &= s.Substring(s.Length - 4, 2) & "PASS-"
                End If
            Next
            Dato = Dato.Substring(0, Dato.Length - 1)
        Catch ex As Exception
            Dato = "Flash Err"
        End Try

        Return Dato
    End Function




    Private Function Riceve_FlashRunner2_0(Optional ByVal Ch_Str As String = "1111", Optional ByVal Timeout As Single = 30) As List(Of String)
        Dim ReadOK As Boolean = False
        Dim StrRead As String = ""
        Dim ReadList_Str As New List(Of String)
        Dim Ch_StrList As New List(Of String)
        Dim UsedChNum As Integer = 0

        For Each chStr In Ch_Str
            If chStr = "1" Then
                UsedChNum += 1
            End If
        Next

        Dim SW As Stopwatch = New Stopwatch()
        SW.Start()

        Try
            Dim ByteRead As Integer = 0

            ReadOK = False       'Reset Flag

            While Not ReadOK
                StrRead = GetAnswer_DLL(2)
                Dim Str As String = ""

                For Each S In StrRead
                    Str &= S
                    If S = "!" Or S = ">" Then
                        If Str.Contains("55|") Then
                        Else
                            ReadList_Str.Add(Str)
                        End If

                        Str = ""
                        StrRead = ""
                    End If
                Next

                If ReadList_Str.Count = UsedChNum Then
                    ReadOK = True
                End If

                If SW.ElapsedMilliseconds / 1000 > Timeout Then
                    ReadOK = True
                End If

                System.Threading.Thread.Sleep(10)
            End While

        Catch ex As Exception
            SW.Stop()
        End Try

        SW.Stop()

        Return ReadList_Str
    End Function


    ''' <summary>
    ''' 分析返回结果
    ''' </summary>
    ''' <param name="Values"></param>
    ''' <returns></returns>
    Private Function SplitFlashResult(ByVal Values() As Byte) As List(Of String)
        Dim FlashResult As String = ""
        Dim ListResult As List(Of String) = New List(Of String)
        Dim ReturnResult As List(Of String) = New List(Of String)
        'Dim StrRead = DotNet.Utilities.StringHelper.DeleteChar(ANSI.GetString(Values))
        Dim StrRead As String = ""

        '1.截取测试结果
        Dim Str As String = ""
        For Each S In StrRead
            Str &= S
            If S = "!" Or S = ">" Then
                ListResult.Add(Str)
                Str = ""
            End If
        Next

        '2.Check Result
        For Each S In ListResult
            If S.Contains(">") Or S.Contains("!") Then
                Dim Strs() As String = S.Split(" ")
                If Strs.Length >= 2 Then
                    Try
                        FlashResult = "CH" & Integer.Parse(Strs(0)) & "_" & IIf(Strs(1).Contains(">"), "OK", "NG")
                    Catch ex As Exception

                    End Try

                    If Not ReturnResult.Contains(FlashResult) Then ReturnResult.Add(FlashResult)
                End If
            End If
        Next

        Return ReturnResult

    End Function



#End Region







End Class
