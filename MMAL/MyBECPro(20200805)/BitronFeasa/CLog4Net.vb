Imports log4net
Public Class CLog4Net
    Private Shared ReadOnly Log As log4net.ILog = _
log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Public Enum LogLevel
        _Debug
        _Error
        _Fatal
        _Info
    End Enum

    Public Shared Sub Logs4Net(Optional ByVal Lv As LogLevel = LogLevel._Info, Optional ByVal Msg As String = "")
        Select Case Lv
            Case LogLevel._Info
                With Log
                    .Info(Msg)
                End With
            Case LogLevel._Fatal
                With Log
                    .Fatal(Msg)
                End With
            Case LogLevel._Error
                With Log
                    .Error(Msg)
                End With
            Case LogLevel._Debug
                With Log
                    .Debug(Msg)
                End With
        End Select
    End Sub
End Class
