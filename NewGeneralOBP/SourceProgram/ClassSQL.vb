Imports System.Data.SqlClient
Imports System.Text
Imports CheckProgramLock

Public Class ClassSQL

    Private SqlHelper As DotNet.Utilities.SqlHelper
    Private TableName = "BTROBP_Infos"
    Private EvostaLinkTableName As String = "DAB_Evosta_Data_Result"
    Private ConnectString As String = "Data Source=10.150.10.11\reparto;Persist Security Info=false;Integrated Security=false;Initial Catalog=Maintenance;User ID=reparto;Password=Reparto2014"

    Public Sub New()
        SqlHelper = New DotNet.Utilities.SqlHelper(ConnectString)
    End Sub

    Public Sub GetFlashInfos(ByVal BitronCode As String, ByRef BinFile As String, ByRef ScriptFile As String, ByRef BinFileCRC As String, ByRef ScriptFileCRC As String, ByRef Usable As Boolean)

        Usable = False

        Dim Sql As String = String.Format(String.Format("SELECT [FlashBinName],[FlashScriptName],[BinCRC],[ScriptCRC],[Usable] FROM {0} WHERE BitronCode='{1}'", TableName, BitronCode))
        Dim dt As DataTable = SqlHelper.Fill(Sql)

        If (dt.Rows.Count = 0) Then
            Throw New Exception(String.Format("FlashRuuner Script Info Error:没有{0}的相关信息", BitronCode))
        ElseIf dt.Rows.Count = 1 Then
            BinFile = dt.Rows(0)(0).ToString()
            ScriptFile = dt.Rows(0)(1).ToString()
            BinFileCRC = dt.Rows(0)(2).ToString()
            ScriptFileCRC = dt.Rows(0)(3).ToString()
            Usable = IIf(dt.Rows(0)(4).ToString().ToUpper() = "YES", True, False)
        Else
            Throw New Exception(String.Format("FlashRuuner Script Info Error:查询到多条{0}的信息", BitronCode))
        End If
    End Sub


    Public Sub CloseDB()
        Try
            SqlHelper.Close()
        Catch ex As Exception

        End Try


    End Sub
    Public Sub Dispose()
        Try
            SqlHelper.Dispose()
        Catch ex As Exception

        End Try

    End Sub
End Class
