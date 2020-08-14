Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class SqlHelper
    ''' <summary>
    ''' 创建实例
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInstance() As DbProviderFactory
        Return SqlClientFactory.Instance
    End Function

#Region "public SqlHelper() 构造函数"
    ''' <summary>
    ''' 构造函数
    ''' </summary>
    Public Sub New()
    End Sub
#End Region

#Region "public SqlHelper(string connectionString) 构造函数,设置数据库连接"
    ''' <summary>
    ''' 构造函数,设置数据库连接
    ''' </summary>
    ''' <param name="connectionString">数据连接</param>
    Public Sub New(ByVal connectionString As String)
        Me.ConnectionString = connectionString
    End Sub
#End Region

#Region "public string GetDBDateTime() 获得数据库日期时间 执行SQL后的结果"
    ''' <summary>
    ''' 获得数据库日期时间
    ''' </summary>
    ''' <returns>日期时间</returns>
    Public Function GetDBDateTime(ByVal format As String) As String

        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Dim commandText As String = " SELECT " & " Getdate() "
        Dim _dateTime As String = DateTime.Parse(Me.ExecuteScalar(commandText, Nothing, CommandType.Text).ToString()).ToString(format)
        ' 自动关闭
        If Me.AutoOpenClose Then
            Me.Close()
        Else
            Me.m_dbCommand.Parameters.Clear()
        End If

        Return _dateTime
    End Function
#End Region

#Region "public string GetDBDateTime() 获得数据库日期时间 执行SQL后的结果"
    ''' <summary>
    ''' 获得数据库日期时间
    ''' </summary>
    ''' <returns>日期时间</returns>
    Public Function GetDBDateTime() As String

        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Dim commandText As String = " SELECT " & " Getdate() "
        'this.Open();
        Dim dateTime As String = Me.ExecuteScalar(commandText, Nothing, CommandType.Text).ToString()
        'this.Close();

        ' 自动关闭
        If Me.AutoOpenClose Then
            Me.Close()
        Else
            Me.m_dbCommand.Parameters.Clear()
        End If

        Return dateTime
    End Function
#End Region

#Region "string PlusSign(params string[] values) 获得Sql字符串相加符号"
    ''' <summary>
    '''  获得Sql字符串相加符号
    ''' </summary>
    ''' <param name="values">参数值</param>
    ''' <returns>字符加</returns>
    Public Function PlusSign(ByVal ParamArray values As String()) As String
        Dim returnValue As String = String.Empty
        For i As Integer = 0 To values.Length - 1
            returnValue += values(i) & " + "
        Next
        If Not [String].IsNullOrEmpty(returnValue) Then
            returnValue = returnValue.Substring(0, returnValue.Length - 3)
        Else
            returnValue = " + "
        End If
        Return returnValue
    End Function
#End Region

#Region "public string GetParameter(string parameter) 获得参数Sql表达式"
    ''' <summary>
    ''' 获得参数Sql表达式
    ''' </summary>
    ''' <param name="parameter">参数名称</param>
    ''' <returns>字符串</returns>
    Public Function GetParameter(ByVal parameter As String) As String
        Return " @" & parameter & " "
    End Function
#End Region

#Region "public IDbDataParameter MakeParameter(string targetFiled, object targetValue) 获取参数"
    ''' <summary>
    ''' 获取参数
    ''' </summary>
    ''' <param name="targetFiled">目标字段</param>
    ''' <param name="targetValue">值</param>
    ''' <returns>参数集</returns>
    Public Function MakeParameter(ByVal targetFiled As String, ByVal targetValue As Object) As IDbDataParameter
        Dim dbParameter As IDbDataParameter = Nothing
        If targetFiled IsNot Nothing Then
            dbParameter = Me.MakeInParam(targetFiled, If(targetValue Is Nothing, DBNull.Value, targetValue))
        End If
        Return dbParameter
    End Function
#End Region

#Region "public IDbDataParameter MakeInParam(string targetFiled, object targetValue) 获取参数实际方法"
    ''' <summary>
    ''' 获取参数实际方法
    ''' </summary>
    ''' <param name="targetFiled">目标字段</param>
    ''' <param name="targetValue">值</param>
    ''' <returns>参数</returns>
    Public Function MakeInParam(ByVal targetFiled As String, ByVal targetValue As Object) As IDbDataParameter
        Return New SqlParameter("@" & targetFiled, targetValue)
    End Function
#End Region

#Region "public IDbDataParameter[] MakeParameters(string[] targetFileds, Object[] targetValues) 获取参数数组集合"
    ''' <summary>
    ''' 获取参数数组集合
    ''' </summary>
    ''' <param name="targetFileds">目标字段</param>
    ''' <param name="targetValues">值</param>
    ''' <returns>参数集</returns>
    Public Function MakeParameters(ByVal targetFileds As String(), ByVal targetValues As [Object]()) As IDbDataParameter()
        ' 这里需要用泛型列表，因为有不合法的数组的时候
        Dim dbParameters As New List(Of IDbDataParameter)()
        If targetFileds IsNot Nothing AndAlso targetValues IsNot Nothing Then
            For i As Integer = 0 To targetFileds.Length - 1
                If targetFileds(i) IsNot Nothing AndAlso targetValues(i) IsNot Nothing AndAlso (Not (TypeOf targetValues(i) Is Array)) Then
                    dbParameters.Add(Me.MakeInParam(targetFileds(i), targetValues(i)))
                End If
            Next
        End If
        Return dbParameters.ToArray()
    End Function
#End Region

#Region "public IDbDataParameter[] MakeParameters(List<KeyValuePair<string, object>> parameters) 获取参数泛型列表"
    ''' <summary>
    ''' 获取参数泛型列表
    ''' </summary>
    ''' <param name="parameters">参数</param>
    ''' <returns>参数集</returns>
    Public Function MakeParameters(ByVal parameters As List(Of KeyValuePair(Of String, Object))) As IDbDataParameter()
        ' 这里需要用泛型列表，因为有不合法的数组的时候
        Dim dbParameters As New List(Of IDbDataParameter)()
        If parameters IsNot Nothing AndAlso parameters.Count > 0 Then
            For Each parameter As KeyValuePair(Of String, Object) In parameters
                If parameter.Key IsNot Nothing AndAlso parameter.Value IsNot Nothing AndAlso (Not (TypeOf parameter.Value Is Array)) Then
                    dbParameters.Add(MakeParameter(parameter.Key, parameter.Value))
                End If
            Next
        End If
        Return dbParameters.ToArray()
    End Function
#End Region

#Region "public IDbDataParameter MakeOutParam(string paramName, DbType dbType, int size) 获取输出参数"
    ''' <summary>
    ''' 获取输出参数
    ''' </summary>
    ''' <param name="paramName">参数</param>
    ''' <param name="dbType">数据类型</param>
    ''' <param name="size">长度</param>
    ''' <returns></returns>
    Public Function MakeOutParam(ByVal paramName As String, ByVal dbType As DbType, ByVal size As Integer) As IDbDataParameter
        Return MakeParameter(paramName, Nothing, dbType, size, ParameterDirection.Output)
    End Function
#End Region

#Region "public IDbDataParameter MakeInParam(string paramName, DbType dbType, int Size, object value) 获取输入参数"
    ''' <summary>
    ''' 获取输入参数
    ''' </summary>
    ''' <param name="paramName">参数</param>
    ''' <param name="dbType">数据类型</param>
    ''' <param name="Size">长度</param>
    ''' <param name="value">值</param>
    ''' <returns></returns>
    Public Function MakeInParam(ByVal paramName As String, ByVal dbType As DbType, ByVal Size As Integer, ByVal value As Object) As IDbDataParameter
        Return MakeParameter(paramName, value, dbType, Size, ParameterDirection.Input)
    End Function
#End Region

#Region "获取参数，包含详细参数设置 public IDbDataParameter MakeParameter(string parameterName, object parameterValue, DbType dbType, Int32 parameterSize, ParameterDirection parameterDirection)"
    ''' <summary>
    ''' 获取参数，包含详细参数设置
    ''' </summary>
    ''' <param name="parameterName">参数名</param>
    ''' <param name="parameterValue">值</param>
    ''' <param name="dbType">数据类型</param>
    ''' <param name="parameterSize">长度</param>
    ''' <param name="parameterDirection">参数类型</param>
    ''' <returns>参数</returns>
    Public Function MakeParameter(ByVal parameterName As String, ByVal parameterValue As Object, ByVal dbType As DbType, ByVal parameterSize As Int32, ByVal parameterDirection__1 As ParameterDirection) As IDbDataParameter
        Dim parameter As SqlParameter

        If parameterSize > 0 Then
            parameter = New SqlParameter(parameterName, ConvertToSqlDbType(dbType), parameterSize)
        Else
            parameter = New SqlParameter(parameterName, ConvertToSqlDbType(dbType))
        End If

        parameter.Direction = parameterDirection__1
        If Not (parameterDirection__1 = ParameterDirection.Output AndAlso parameterValue Is Nothing) Then
            parameter.Value = parameterValue
        End If

        Return parameter
    End Function
#End Region

#Region "private System.Data.SqlDbType ConvertToSqlDbType(System.Data.DbType dbType) 类型转换"
    ''' <summary>
    ''' 类型转换
    ''' </summary>
    ''' <param name="dbType">数据类型</param>
    ''' <returns>转换结果</returns>
    Private Function ConvertToSqlDbType(ByVal dbType As System.Data.DbType) As System.Data.SqlDbType
        Dim sqlParameter As New SqlParameter()
        sqlParameter.DbType = dbType
        Return sqlParameter.SqlDbType
    End Function
#End Region

#Region "public void SqlBulkCopyData(DataTable dataTable) 利用Net SqlBulkCopy 批量导入数据库,速度超快"
    ''' <summary>
    ''' 利用Net SqlBulkCopy 批量导入数据库,速度超快
    ''' </summary>
    ''' <param name="dataTable">源内存数据表</param>
    Public Sub SqlBulkCopyData(ByVal dataTable As DataTable)
        ' SQL 数据连接
        Dim conn As SqlConnection = Nothing
        ' 打开数据库
        Me.Open()

        ' 获取连接
        conn = DirectCast(GetDbConnection(), SqlConnection)

        Using tran As SqlTransaction = conn.BeginTransaction()
            ' 批量保存数据，只能用于Sql
            Dim sqlbulkCopy As New SqlBulkCopy(conn, SqlBulkCopyOptions.[Default], tran)
            ' 设置源表名称
            sqlbulkCopy.DestinationTableName = dataTable.TableName
            ' 设置超时限制
            sqlbulkCopy.BulkCopyTimeout = 1000

            For Each dtColumn As DataColumn In dataTable.Columns
                sqlbulkCopy.ColumnMappings.Add(dtColumn.ColumnName, dtColumn.ColumnName)
            Next
            Try
                ' 写入
                sqlbulkCopy.WriteToServer(dataTable)
                ' 提交事务
                tran.Commit()
            Catch
                tran.Rollback()
                sqlbulkCopy.Close()
            Finally
                sqlbulkCopy.Close()
                Me.Close()

            End Try
        End Using
    End Sub
#End Region

#Region "数据库连接必要条件参数"
    '数据库连接
    Private m_dbConnection As DbConnection = Nothing
    ''' <summary>
    ''' 数据库连接
    ''' </summary>
    Public Property DbConnection() As DbConnection
        Get
            If Me.m_dbConnection Is Nothing Then
                ' 若没打开，就变成自动打开关闭的

                Me.Open()
                If Not Me.InTransaction Then
                    Me.AutoOpenClose = True
                End If
            End If
            Return Me.m_dbConnection
        End Get
        Set(ByVal value As DbConnection)
            Me.m_dbConnection = value
        End Set
    End Property

    '命令
    Private m_dbCommand As DbCommand = Nothing
    ''' <summary>
    ''' 命令
    ''' </summary>
    Public Property DbCommand() As DbCommand
        Get
            Return Me.m_dbCommand
        End Get

        Set(ByVal value As DbCommand)
            Me.m_dbCommand = value
        End Set
    End Property

    '数据库适配器
    Private m_dbDataAdapter As DbDataAdapter = Nothing
    ''' <summary>
    ''' 数据库适配器
    ''' </summary>
    Public Property DbDataAdapter() As DbDataAdapter
        Get
            Return Me.m_dbDataAdapter
        End Get

        Set(ByVal value As DbDataAdapter)
            Me.m_dbDataAdapter = value
        End Set
    End Property

    ' 数据库连接
    Private m_connectionString As String = String.Empty
    ''' <summary>
    ''' 数据库连接
    ''' </summary>
    Public Property ConnectionString() As String
        Get
            Return Me.m_connectionString
        End Get
        Set(ByVal value As String)
            Me.m_connectionString = value
        End Set
    End Property

    Private dbTransaction As DbTransaction = Nothing

    ' 是否已在事务之中
    Private m_inTransaction As Boolean = False
    ''' <summary>
    ''' 是否已采用事务
    ''' </summary>
    Public Property InTransaction() As Boolean
        Get
            Return Me.m_inTransaction
        End Get

        Set(ByVal value As Boolean)
            Me.m_inTransaction = value
        End Set
    End Property

    '默认打开关闭数据库选项（默认为否）
    Private m_autoOpenClose As Boolean = False
    ''' <summary>
    ''' 默认打开关闭数据库选项（默认为否）
    ''' </summary>
    Public Property AutoOpenClose() As Boolean
        Get
            Return m_autoOpenClose
        End Get
        Set(ByVal value As Boolean)
            m_autoOpenClose = value
        End Set
    End Property

#End Region

#Region "public virtual IDbConnection GetDbConnection() 获取数据库连接"
    ''' <summary>
    ''' 获取数据库连接
    ''' </summary>
    ''' <returns>数据库连接</returns>
    Public Overridable Function GetDbConnection() As IDbConnection
        Return Me.m_dbConnection
    End Function
#End Region

#Region "public virtual IDbConnection Open() 获取数据库连接的方法"
    ''' <summary>
    ''' 这时主要的获取数据库连接的方法
    ''' </summary>
    ''' <returns>数据库连接</returns>
    Public Overridable Function Open() As IDbConnection

        ' 若是空的话才打开
        If Me.m_dbConnection Is Nothing OrElse Me.m_dbConnection.State = ConnectionState.Closed Then
            Me.m_dbConnection = GetInstance().CreateConnection()
            Me.m_dbConnection.ConnectionString = Me.ConnectionString
            Me.m_dbConnection.Open()
        End If
        Me.AutoOpenClose = False
        Return Me.m_dbConnection
    End Function
#End Region

#Region "public void Close() 关闭数据库连接"
    ''' <summary>
    ''' 关闭数据库连接
    ''' </summary>
    Public Sub Close()

        If Me.m_dbConnection IsNot Nothing Then
            Me.m_dbConnection.Close()
            Me.m_dbConnection.Dispose()
        End If

        Me.Dispose()
    End Sub
#End Region

#Region "public void Dispose() 内存回收"
    ''' <summary>
    ''' 内存回收
    ''' </summary>
    Public Sub Dispose()
        If Me.m_dbCommand IsNot Nothing Then
            Me.m_dbCommand.Dispose()
        End If
        If Me.m_dbDataAdapter IsNot Nothing Then
            Me.m_dbDataAdapter.Dispose()
        End If
        If Me.dbTransaction IsNot Nothing Then
            Me.dbTransaction.Dispose()
        End If
        ' 关闭数据库连接
        If Me.m_dbConnection IsNot Nothing Then
            If Me.m_dbConnection.State <> ConnectionState.Closed Then
                Me.m_dbConnection.Close()
                Me.m_dbConnection.Dispose()
            End If
        End If
        Me.m_dbConnection = Nothing
    End Sub
#End Region

#Region "public virtual IDataReader ExecuteReader(string commandText) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <returns>结果集流</returns>
    Public Overridable Function ExecuteReader(ByVal commandText As String) As IDataReader
        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Me.m_dbCommand = Me.DbConnection.CreateCommand()
        Me.m_dbCommand.CommandType = CommandType.Text
        Me.m_dbCommand.CommandText = commandText

        Dim dbDataReader As DbDataReader = Nothing
        dbDataReader = Me.m_dbCommand.ExecuteReader(CommandBehavior.CloseConnection)

        Return dbDataReader
    End Function
#End Region

#Region "public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters); 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>结果集流</returns>
    Public Overridable Function ExecuteReader(ByVal commandText As String, ByVal dbParameters As IDbDataParameter()) As IDataReader
        Return Me.ExecuteReader(commandText, dbParameters, CommandType.Text)
    End Function
#End Region

#Region "public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <param name="commandType">命令分类</param>
    ''' <returns>结果集流</returns>
    Public Overridable Function ExecuteReader(ByVal commandText As String, ByVal dbParameters As IDbDataParameter(), ByVal commandType As CommandType) As IDataReader
        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Me.m_dbCommand = Me.DbConnection.CreateCommand()
        Me.m_dbCommand.CommandText = commandText
        Me.m_dbCommand.CommandType = commandType
        If Me.dbTransaction IsNot Nothing Then
            Me.m_dbCommand.Transaction = Me.dbTransaction
        End If

        If dbParameters IsNot Nothing Then
            Me.m_dbCommand.Parameters.Clear()
            For i As Integer = 0 To dbParameters.Length - 1
                If dbParameters(i) IsNot Nothing Then
                    Me.m_dbCommand.Parameters.Add(dbParameters(i))
                End If
            Next
        End If

        ' 这里要关闭数据库才可以的
        Dim dbDataReader As DbDataReader = Nothing
        dbDataReader = Me.m_dbCommand.ExecuteReader(CommandBehavior.CloseConnection)


        Return dbDataReader
    End Function
#End Region

#Region "public virtual int ExecuteNonQuery(string commandText) 执行查询, SQL BUILDER 用了这个东西？参数需要保存, 不能丢失."
    ''' <summary>
    ''' 执行查询, SQL BUILDER 用了这个东西？参数需要保存, 不能丢失.
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <returns>影响行数</returns>
    Public Overridable Function ExecuteNonQuery(ByVal commandText As String) As Integer
        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If


        Me.m_dbCommand = Me.DbConnection.CreateCommand()
        Me.m_dbCommand.CommandType = CommandType.Text
        Me.m_dbCommand.CommandText = commandText
        Me.m_dbCommand.CommandTimeout = 600
        If Me.InTransaction Then
            Me.m_dbCommand.Transaction = Me.dbTransaction
        End If

        Dim returnValue As Integer = Me.m_dbCommand.ExecuteNonQuery()

        ' 自动打开
        If Me.AutoOpenClose Then
            Me.Close()
        End If

        Return returnValue
    End Function
#End Region

#Region "public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>影响行数</returns>
    Public Overridable Function ExecuteNonQuery(ByVal commandText As String, ByVal dbParameters As IDbDataParameter()) As Integer
        Return Me.ExecuteNonQuery(commandText, dbParameters, CommandType.Text)
    End Function
#End Region

#Region "public virtual int ExecuteNonQuery(string commandText, CommandType commandType) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="commandType">命令分类</param>
    ''' <returns>影响行数</returns>
    Public Overridable Function ExecuteNonQuery(ByVal commandText As String, ByVal commandType As CommandType) As Integer
        Return Me.ExecuteNonQuery(Me.dbTransaction, commandText, Nothing, commandType)
    End Function
#End Region

#Region "public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <param name="commandType">命令分类</param>
    ''' <returns>影响行数</returns>
    Public Overridable Function ExecuteNonQuery(ByVal commandText As String, ByVal dbParameters As IDbDataParameter(), ByVal commandType As CommandType) As Integer
        Return Me.ExecuteNonQuery(Me.dbTransaction, commandText, dbParameters, commandType)
    End Function
#End Region

#Region "public virtual int ExecuteNonQuery(IDbTransaction transaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="transaction">数据库事务</param>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <param name="commandType">命令分类</param>
    ''' <returns>影响行数</returns>
    Public Overridable Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal commandText As String, ByVal dbParameters As IDbDataParameter(), ByVal commandType As CommandType, Optional ByVal ReturnReturnValue As Boolean = False) As Integer
        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Me.m_dbCommand = Me.DbConnection.CreateCommand()
        Me.m_dbCommand.CommandText = commandText
        Me.m_dbCommand.CommandType = commandType
        If Me.dbTransaction IsNot Nothing Then
            Me.m_dbCommand.Transaction = Me.dbTransaction
        End If


        Dim ResultPara As New SqlParameter With {.ParameterName = "RETURN_VALUE", .SqlDbType = SqlDbType.Int, .Direction = ParameterDirection.ReturnValue}

        If dbParameters IsNot Nothing Then
            Me.m_dbCommand.Parameters.Clear()

            If ReturnReturnValue Then
                Me.m_dbCommand.Parameters.Add(ResultPara)
            End If
            For i As Integer = 0 To dbParameters.Length - 1
                Me.m_dbCommand.Parameters.Add(dbParameters(i))
            Next
        End If
        Dim returnValue As Integer = Me.m_dbCommand.ExecuteNonQuery()

        If ReturnReturnValue Then
            returnValue = ResultPara.Value
        End If


        ' 自动关闭
        If Me.AutoOpenClose Then
            Me.Close()
        Else
            Me.m_dbCommand.Parameters.Clear()
        End If

        Return returnValue
    End Function
#End Region

#Region "public virtual object ExecuteScalar(string commandText) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <returns>object</returns>
    Public Overridable Function ExecuteScalar(ByVal commandText As String) As Object
        Return Me.ExecuteScalar(commandText, Nothing, CommandType.Text)
    End Function
#End Region

#Region "public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>Object</returns>
    Public Overridable Function ExecuteScalar(ByVal commandText As String, ByVal dbParameters As IDbDataParameter()) As Object
        Return Me.ExecuteScalar(commandText, dbParameters, CommandType.Text)
    End Function
#End Region

#Region "public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <param name="commandType">命令分类</param>
    ''' <returns>Object</returns>
    Public Overridable Function ExecuteScalar(ByVal commandText As String, ByVal dbParameters As IDbDataParameter(), ByVal commandType As CommandType) As Object
        Return Me.ExecuteScalar(Me.dbTransaction, commandText, dbParameters, commandType)
    End Function
#End Region

#Region "public virtual object ExecuteScalar(IDbTransaction transaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询"
    ''' <summary>
    ''' 执行查询
    ''' </summary>
    ''' <param name="transaction">数据库事务</param>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <param name="commandType">命令分类</param>
    ''' <returns>Object</returns>
    Public Overridable Function ExecuteScalar(ByVal transaction As IDbTransaction, ByVal commandText As String, ByVal dbParameters As IDbDataParameter(), ByVal commandType As CommandType) As Object

        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Me.m_dbCommand = Me.DbConnection.CreateCommand()
        Me.m_dbCommand.CommandText = commandText
        Me.m_dbCommand.CommandType = commandType
        If Me.dbTransaction IsNot Nothing Then
            Me.m_dbCommand.Transaction = Me.dbTransaction
        End If
        If dbParameters IsNot Nothing Then
            Me.m_dbCommand.Parameters.Clear()
            For i As Integer = 0 To dbParameters.Length - 1
                If dbParameters(i) IsNot Nothing Then
                    Me.m_dbCommand.Parameters.Add(dbParameters(i))
                End If
            Next
        End If
        Dim returnValue As Object = Nothing

        Try
            returnValue = Me.m_dbCommand.ExecuteScalar()
        Catch
        End Try

        ' 自动关闭
        If Me.AutoOpenClose Then
            Me.Close()
        Else
            Me.m_dbCommand.Parameters.Clear()
        End If
        Return returnValue
    End Function
#End Region

#Region "public virtual DataTable Fill(string commandText) 填充数据表"
    ''' <summary>
    ''' 填充数据表
    ''' </summary>
    ''' <param name="commandText">查询</param>
    ''' <returns>数据表</returns>
    Public Overridable Function Fill(ByVal commandText As String) As DataTable
        Dim dataTable As New DataTable("DotNet")
        Return Me.Fill(dataTable, commandText, Nothing, CommandType.Text)
    End Function
#End Region

#Region "public virtual DataTable Fill(DataTable dataTable, string commandText) 填充数据表"
    ''' <summary>
    ''' 填充数据表
    ''' </summary>
    ''' <param name="dataTable">目标数据表</param>
    ''' <param name="commandText">查询</param>
    ''' <returns>数据表</returns>
    Public Overridable Function Fill(ByVal dataTable As DataTable, ByVal commandText As String) As DataTable
        Return Me.Fill(dataTable, commandText, Nothing, CommandType.Text)
    End Function
#End Region

#Region "public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters) 填充数据表"
    ''' <summary>
    ''' 填充数据表
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>数据表</returns>
    Public Overridable Function Fill(ByVal commandText As String, ByVal dbParameters As IDbDataParameter()) As DataTable
        Dim dataTable As New DataTable("DotNet")
        Return Me.Fill(dataTable, commandText, dbParameters, CommandType.Text)
    End Function
#End Region

#Region "public virtual DataTable Fill(DataTable dataTable, string commandText, IDbDataParameter[] dbParameters) 填充数据表"
    ''' <summary>
    ''' 填充数据表
    ''' </summary>
    ''' <param name="dataTable">目标数据表</param>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>数据表</returns>
    Public Overridable Function Fill(ByVal dataTable As DataTable, ByVal commandText As String, ByVal dbParameters As IDbDataParameter()) As DataTable
        Return Me.Fill(dataTable, commandText, dbParameters, CommandType.Text)
    End Function
#End Region

#Region "public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 填充数据表"
    ''' <summary>
    ''' 填充数据表
    ''' </summary>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="commandType">命令分类</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>数据表</returns>
    Public Overridable Function Fill(ByVal commandText As String, ByVal dbParameters As IDbDataParameter(), ByVal commandType As CommandType) As DataTable
        Dim dataTable As New DataTable("DotNet")
        Return Me.Fill(dataTable, commandText, dbParameters, commandType)
    End Function
#End Region

#Region "public virtual DataTable Fill(DataTable dataTable, string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 填充数据表"
    ''' <summary>
    ''' 填充数据表
    ''' </summary>
    ''' <param name="dataTable">目标数据表</param>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <param name="commandType">命令分类</param>
    ''' <returns>数据表</returns>
    Public Overridable Function Fill(ByVal dataTable As DataTable, ByVal commandText As String, ByVal dbParameters As IDbDataParameter(), ByVal commandType As CommandType) As DataTable
        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Me.m_dbCommand = Me.DbConnection.CreateCommand()
        Me.m_dbCommand.CommandTimeout = Me.DbConnection.ConnectionTimeout
        Me.m_dbCommand.CommandText = commandText
        Me.m_dbCommand.CommandType = commandType
        If Me.InTransaction Then
            ' 这个不这么写，也不行，否则运行不能通过的
            Me.m_dbCommand.Transaction = Me.dbTransaction
        End If
        Me.m_dbDataAdapter = Me.GetInstance().CreateDataAdapter()
        Me.m_dbDataAdapter.SelectCommand = Me.m_dbCommand
        If (dbParameters IsNot Nothing) AndAlso (dbParameters.Length > 0) Then
            Me.m_dbCommand.Parameters.AddRange(dbParameters)
        End If
        Me.m_dbDataAdapter.Fill(dataTable)
        Me.m_dbDataAdapter.SelectCommand.Parameters.Clear()

        ' 自动关闭
        If Me.AutoOpenClose Then
            Me.Close()
        End If

        Return dataTable
    End Function
#End Region

#Region "public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName) 填充数据权限"
    ''' <summary>
    ''' 填充数据权限
    ''' </summary>
    ''' <param name="dataSet">目标数据权限</param>
    ''' <param name="commandText">查询</param>
    ''' <param name="tableName">填充表</param>
    ''' <returns>数据权限</returns>
    Public Overridable Function Fill(ByVal dataSet As DataSet, ByVal commandText As String, ByVal tableName As String) As DataSet
        Return Me.Fill(dataSet, CommandType.Text, commandText, tableName, Nothing)
    End Function
#End Region

#Region "public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName, IDbDataParameter[] dbParameters) 填充数据权限"
    ''' <summary>
    ''' 填充数据权限
    ''' </summary>
    ''' <param name="dataSet">数据权限</param>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="tableName">填充表</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>数据权限</returns>
    Public Overridable Function Fill(ByVal dataSet As DataSet, ByVal commandText As String, ByVal tableName As String, ByVal dbParameters As IDbDataParameter()) As DataSet
        Return Me.Fill(dataSet, CommandType.Text, commandText, tableName, dbParameters)
    End Function
#End Region

#Region "public virtual DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, IDbDataParameter[] dbParameters) 填充数据权限"
    ''' <summary>
    ''' 填充数据权限
    ''' </summary>
    ''' <param name="dataSet">数据权限</param>
    ''' <param name="commandType">命令分类</param>
    ''' <param name="commandText">sql查询</param>
    ''' <param name="tableName">填充表</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>数据权限</returns>
    Public Overridable Function Fill(ByVal dataSet As DataSet, ByVal commandType As CommandType, ByVal commandText As String, ByVal tableName As String, ByVal dbParameters As IDbDataParameter()) As DataSet

        ' 自动打开
        If Me.DbConnection Is Nothing Then
            Me.AutoOpenClose = True
            Me.Open()
        ElseIf Me.DbConnection.State = ConnectionState.Closed Then
            Me.Open()
        End If

        Me.m_dbCommand = Me.DbConnection.CreateCommand()

        Me.m_dbCommand.CommandText = commandText
        Me.m_dbCommand.CommandType = commandType
        If (dbParameters IsNot Nothing) AndAlso (dbParameters.Length > 0) Then
            Me.m_dbCommand.Parameters.AddRange(dbParameters)
        End If

        Me.m_dbDataAdapter = Me.GetInstance().CreateDataAdapter()
        Me.m_dbDataAdapter.SelectCommand = Me.m_dbCommand
        Me.m_dbDataAdapter.Fill(dataSet, tableName)

        If Me.AutoOpenClose Then
            Me.Close()
        Else
            Me.m_dbDataAdapter.SelectCommand.Parameters.Clear()
        End If

        Return dataSet
    End Function
#End Region

#Region "public virtual int ExecuteProcedure(string procedureName) 执行存储过程"
    ''' <summary>
    ''' 执行存储过程
    ''' </summary>
    ''' <param name="procedureName">存储过程</param>
    ''' <returns>int</returns>
    Public Overridable Function ExecuteProcedure(ByVal procedureName As String) As Integer
        Return Me.ExecuteNonQuery(procedureName, Nothing, CommandType.StoredProcedure)
    End Function
#End Region

#Region "public virtual int ExecuteProcedure(string procedureName, IDbDataParameter[] dbParameters) 执行代参数的存储过程"
    ''' <summary>
    ''' 执行代参数的存储过程
    ''' </summary>
    ''' <param name="procedureName">存储过程名</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>影响行数</returns>
    Public Overridable Function ExecuteProcedure(ByVal procedureName As String, ByVal dbParameters As IDbDataParameter()) As Integer
        Return Me.ExecuteNonQuery(Me.dbTransaction, procedureName, dbParameters, CommandType.StoredProcedure)
    End Function
    Public Overridable Function ExecuteProcedure_ReturnValue(ByVal procedureName As String, ByVal dbParameters As IDbDataParameter()) As Integer
        Return Me.ExecuteNonQuery(Me.dbTransaction, procedureName, dbParameters, CommandType.StoredProcedure, True)
    End Function
#End Region

#Region "public virtual DataTable ExecuteProcedureForDataTable(string procedureName, string tableName, IDbDataParameter[] dbParameters) 执行存储过程返回数据表"
    ''' <summary>
    ''' 执行存储过程返回数据表
    ''' </summary>
    ''' <param name="procedureName">存储过程</param>
    ''' <param name="tableName">填充表</param>
    ''' <param name="dbParameters">参数集</param>
    ''' <returns>数据权限</returns>
    Public Overridable Function ExecuteProcedureForDataTable(ByVal procedureName As String, ByVal tableName As String, ByVal dbParameters As IDbDataParameter()) As DataTable
        Dim dataTable As New DataTable(tableName)
        Me.Fill(dataTable, procedureName, dbParameters, CommandType.StoredProcedure)
        Return dataTable
    End Function

    Public Overridable Function ExecuteDataTable(ByVal Cmd As String) As DataTable
        Dim dataTable As New DataTable("UserTable")
        Me.Fill(dataTable, Cmd)
        Return dataTable
    End Function
#End Region

#Region "public IDbTransaction BeginTransaction() 事务开始"
    ''' <summary>
    ''' 事务开始
    ''' </summary>
    ''' <returns>事务</returns>
    Public Function BeginTransaction() As IDbTransaction

        If Not Me.InTransaction Then
            ' 这里是不允许自动关闭了
            Me.AutoOpenClose = False
            Me.InTransaction = True
            ' this.dbCommand.Transaction = this.dbTransaction;
            Me.dbTransaction = Me.DbConnection.BeginTransaction()
        End If



        Return Me.dbTransaction
    End Function
#End Region

#Region "public void CommitTransaction() 提交事务"
    ''' <summary>
    ''' 提交事务
    ''' </summary>
    Public Sub CommitTransaction()
        If Me.InTransaction Then
            ' 事务已经完成了，一定要更新标志信息
            Me.InTransaction = False
            Me.dbTransaction.Commit()
        End If

    End Sub
#End Region

#Region "public void RollbackTransaction() 回滚事务"
    ''' <summary>
    ''' 回滚事务
    ''' </summary>
    Public Sub RollbackTransaction()

        If Me.InTransaction Then
            Me.InTransaction = False
            Me.dbTransaction.Rollback()
        End If

    End Sub
#End Region
#Region "public virtual bool IsTableExist(string tableName) 查询表是否存在"
    ''' <summary>
    ''' 查询表是否存在
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <returns></returns>
    Public Overridable Function IsTableExist(ByVal tableName As String) As Boolean
        Dim dataTable As DataTable = New DataTable("DotNet")
        Dim sql As String = " select id from sysobjects where id = object_id('" + tableName + "') and type = 'u'"
        dataTable = Me.Fill(sql)
        Dim count As Integer = dataTable.Rows.Count
        If (count > 0) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

End Class
