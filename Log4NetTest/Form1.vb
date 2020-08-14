Imports log4net
Public Class Form1
    Private Shared ReadOnly Log As log4net.ILog = _
Log4Net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Private Sub Btn_Test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Test.Click
        Call Logs4Net(LogLevel._Info, "Msg")
    End Sub


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
        End Select
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
