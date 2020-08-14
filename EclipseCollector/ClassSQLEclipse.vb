Imports System.Data.SqlClient
Imports System.Text
Imports System.IO

Public Class ClassSQLEclipse
    Private dbHelper As SqlHelper
    Public Sub New()
        'Dim connectionString As String = "Data Source=10.150.10.11\Eclipse;Initial Catalog=Monitor;User ID=ishi;Password=!Bitron123!"
        Dim connectionString As String = "Data Source=10.150.10.11\Eclipse;Initial Catalog=Monitor;User ID=prodmachine;Password=Produzione123"
        dbHelper = New SqlHelper(connectionString)
    End Sub
    Public Enum CheckLevels
        All = 0
        Last_One_PreviousPhase = 1
        Last_Two_PreviousPhase = 2
        Last_Three_PreviousPhase = 3
        Last_Four_PreviousPhase = 4
        Last_Five_PreviousPhase = 5
    End Enum

    Public Function ChangePhaseSetup(ByVal InternalCode As String, ByVal PhaseID As String) As Boolean
        Dim names(3) As String
        Dim values(3) As Object
        names(0) = "PhaseID"
        names(1) = "InternalCode"
        names(2) = "BatchSize"
        names(3) = "CheckAll"

        values(0) = PhaseID
        values(1) = InternalCode
        values(2) = 1000
        values(3) = 0
        Dim Result As Integer = dbHelper.ExecuteProcedure_ReturnValue("Eclipse.dbo.ChangePhaseSetup", dbHelper.MakeParameters(names, values))
        Return IIf(Result = 1, True, False)


    End Function

    Public Function GetPanelSerials(ByVal SN As String) As List(Of String)
        Dim TblSerial As DataTable = dbHelper.ExecuteDataTable("SELECT * FROM Eclipse.dbo.GetPanelSerials ('" & SN & "')")
        Dim SNList As New List(Of String)

        For Each row As DataRow In TblSerial.Rows
            SNList.Add(row.Item(0).ToString)
        Next

        Return SNList
    End Function

    Public Function CheckPhasePrev(ByVal BoardSN As String, ByVal InternalCode As String, ByVal Level As Integer, ByVal PhaseID As String) As Boolean
        '*************************************************************************
        'For example
        'If you have this line:  Lased DMX --> SMD --> AOI ---> ICT --> Your bench
        'If from Your Bench you use Level=1, you will check only ICT
        'If from Your Bench you use Level=2, you will check ICT and AOI
        'If from Your Bench you use Level=3, you will check ICT and AOI and SMD
        'If from Your Bench you use Level=0, you will check the whole process.. So, in this case, ICT and AOI and SMD and Laser DMX
        '*************************************************************************

        '' Please remember @CheckLevels when equal 1 is mean check only last previous phase, when equal to 0 mean check all.


        Dim names(3) As String
        Dim values(3) As Object
        names(0) = "Board"
        names(1) = "PhaseName"
        names(2) = "IdParts"
        names(3) = "CheckLevels"

        values(0) = BoardSN
        values(1) = PhaseID
        values(2) = InternalCode
        values(3) = Level

        Dim Result As Integer = -1
        Result = dbHelper.ExecuteProcedure_ReturnValue("Monitor.dbo.CheckPhasePrev", dbHelper.MakeParameters(names, values))

        Return IIf(Result = 0, True, False)
    End Function



    Public Function StoreTestData(ByVal Paras As List(Of String)) As Boolean
        Dim ParaNum As Integer = Paras.Count - 1
        Dim names(ParaNum) As String
        Dim values(ParaNum) As Object
        names(0) = "TestName"
        names(1) = "AssemblyFormID"
        names(2) = "IdParts"
        names(3) = "MSN"
        names(4) = "TestDateTime"
        names(5) = "RackID"
        names(6) = "PositionInRack"
        names(7) = "Test"
        names(8) = "TestSoftVer"
        names(9) = "TestHWVer"
        names(10) = "Item"
        names(11) = "Measure"
        names(12) = "MeasureDigit"
        names(13) = "UnitMeasure"
        names(14) = "ResultMeasure"

        'If Paras.Count = 16 Then
        '    names(15) = "Result"
        '    For t As Integer = 0 To 15
        '        values(t) = Paras(t)
        '    Next
        'Else
        '    For t As Integer = 0 To 14
        '        values(t) = Paras(t)
        '    Next
        'End If

        For t As Integer = 0 To ParaNum
            If t = 15 Then
                names(15) = "Result"
            Else
            End If
            values(t) = Paras(t)
        Next
        Dim Result As Integer = -1
        Result = dbHelper.ExecuteProcedure_ReturnValue("Monitor.dbo.InsertData", dbHelper.MakeParameters(names, values))
        Return IIf(Result = 0, True, False)
    End Function


    Public Function InsertIntoVwEclipseLinkIdTraceability(ByVal NewLabelSerial As String, ByVal OldSerial As String, ByVal PhaseName As String, ByVal IDParts As String, ByVal MOperator As Integer) As Boolean


        Dim names(5) As String
        Dim values(5) As Object
        names(0) = "Container"
        names(1) = "Child"
        names(2) = "PhaseName"
        names(3) = "IdParts"
        names(4) = "FeedDate"
        names(5) = "Operator"

        values(0) = NewLabelSerial
        values(1) = OldSerial
        values(2) = PhaseName
        values(3) = IDParts
        values(4) = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        values(5) = MOperator

        Dim Result As Integer = -1
        Result = dbHelper.ExecuteProcedure_ReturnValue("Monitor.dbo.InsertIntoVwEclipseLinkIdTraceability", dbHelper.MakeParameters(names, values))


        Return IIf(Result = 0 Or Result = 2627, True, False)
    End Function


    Public Function GetDBSystemDate() As String
        Dim StrCmd As String = "Select  convert(varchar(50),CURRENT_TIMESTAMP,109) AS myNOW"
        Dim StrDate As String = dbHelper.ExecuteScalar(StrCmd)
        Return StrDate
    End Function

    Public Function SqlProc1() As String
        Dim Value As Integer
        Dim connectionString As String = "Data Source=BITRON-DB1\BITRONDB1;Initial Catalog=Monitor;User ID=Prodmachine;Password=Produzione123"

        '定义数据链接部分省略, myConn为链接对象  
        Dim MyConn As New SqlConnection
        MyConn.ConnectionString = connectionString
        MyConn.Open()

        Dim myCommand As New SqlClient.SqlCommand("Eclipse.dbo.ChangePhaseSetup", MyConn)
        With myCommand
            .CommandType = CommandType.StoredProcedure
            Dim Result As SqlParameter = .Parameters.Add("RETURN_VALUE", SqlDbType.Int)
            Result.Direction = ParameterDirection.ReturnValue

            .Parameters.Add("@PhaseID", SqlDbType.Int).Value = 6202493
            .Parameters.Add("@InternalCode", SqlDbType.VarChar, 20).Value = "20630508"
            .Parameters.Add("@BatchSize", SqlDbType.Int).Value = 20
            .Parameters.Add("@CheckAll", SqlDbType.Bit).Value = 0
            .ExecuteNonQuery()

            Value = Result.Value
        End With

        MyConn.Close()

        Return Value
    End Function
End Class