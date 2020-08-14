Imports System.IO
Imports System.IO.Directory
Public Class ClassFileCollect
    Public FilePath As String = ""
    Public FileDesPath As String
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Collect all exist file in directory
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFiles() As List(Of String)
        Dim FList As New List(Of String)
        Dim di As IO.DirectoryInfo = New IO.DirectoryInfo(FilePath)

        Dim dii() As IO.DirectoryInfo = di.GetDirectories

        ''取所有文件
        Dim fii() As IO.FileInfo = di.GetFiles("*.csv", IO.SearchOption.AllDirectories)

        If fii.Count > 0 Then
            For Each fi In fii
                FList.Add(fi.ToString())
            Next
        Else

        End If

        Return FList
    End Function

    ''' <summary>
    ''' Analyse data
    ''' </summary>
    ''' <param name="File"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnalyseData(ByVal File As String) As ClassTestItem
        Dim StrReader As String = ""
        Dim StrArray() As String

        Dim cTestItem As New ClassTestItem
        Try
            StrReader = My.Computer.FileSystem.ReadAllText(FilePath & "\" & File)
            StrArray = StrReader.Split(vbCrLf) '
            cTestItem.mStationID = StrArray(0).Split(";")(1)
            cTestItem.mOpetator = StrArray(1).Split(";")(1)
            cTestItem.mBenchSN = StrArray(2).Split(";")(1)
            cTestItem.mUUTSN = StrArray(3).Split(";")(1)
            cTestItem.mBatchSN = StrArray(4).Split(";")(1)
            cTestItem.mTestSocketIndex = StrArray(5).Split(";")(1)
            cTestItem.mVersionSW = StrArray(6).Split(";")(1)
            cTestItem.mVersionHW = StrArray(7).Split(";")(1)

            Dim cSubList As New List(Of CSubTestItem)
            For t As Integer = 10 To StrArray.Count - 2
                Try
                    Dim citem As New CSubTestItem
                    citem.TestSequence = StrArray(t).Split(";")(0)
                    citem.TestName = StrArray(t).Split(";")(1)
                    citem.TestValue = StrArray(t).Split(";")(5)
                    citem.TestResult = StrArray(t).Split(";")(2)
                    citem.TestDate = StrArray(t).Split(";")(3) & " " & StrArray(t).Split(";")(4)
                    cSubList.Add(citem)
                Catch
                End Try
            Next
            cTestItem.mItem = cSubList
            cTestItem.mTestStatus = StrArray(StrArray.Count - 2).Split(";")(1)
        Catch ex As Exception

        End Try
        Return cTestItem
    End Function

    Public Class ClassTestItem
        Public mStationID As String
        Public mOpetator As String
        Public mBenchSN As String
        Public mUUTSN As String
        Public mBatchSN As String
        Public mTestSocketIndex As String
        Public mVersionSW As String
        Public mVersionHW As String
        Public mTestStatus As String

        Public mItem As New List(Of CSubTestItem)
        Public Sub New()
            mItem.Clear()
        End Sub
    End Class

    Public Class CSubTestItem
        Public TestSequence As String
        Public TestName As String
        Public TestValue As String
        Public TestResult As String
        Public TestDate As String
    End Class



    Public Function MoveToDesFile(ByVal File As String) As Boolean
        Dim SrcFile As String = FilePath & "\" & File
        Dim DesFile As String = FileDesPath & "\" & File
        Dim Res As Boolean = False
        Try
            If System.IO.File.Exists(DesFile) Then
                System.IO.File.Delete(DesFile)
            End If
            System.IO.File.Move(FilePath & "\" & File, FileDesPath & "\" & File)
            Res = True
        Catch
            Res = False
        End Try
        Return Res
    End Function



End Class
