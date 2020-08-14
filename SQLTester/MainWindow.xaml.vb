
Imports System.Data

Class MainWindow
    Dim Sql As New SqlDataProvider.SqlDatabase("Data Source=BECPC210\SQLEXPRESS; database=BEC_Task_Manager; User Id=sa; Password=Aiyanxia0;")
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim Res As Boolean = False
        Dim returnValue As Integer
        Dim Para() As Object
        Dim Cmd As String = "insert into dbo.BEC_Groups values('New Product Introduction','NPI')"
        Res = Sql.ExecuteNonQuery(Cmd, System.Data.CommandType.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Dim Res As Boolean
        Dim Reader As IDataReader
        Dim Cmd As String = "Select * from dbo.BEC_Groups"
        Reader = Sql.ExecuteReader(Cmd, System.Data.CommandType.Text)
        Res = Reader.Read
        Dim T = Reader("Name")
    End Sub

    Private Sub Button2_Click(sender As Object, e As RoutedEventArgs) Handles button2.Click


    End Sub



    Private Function GetDataTableFromIDataReader(ByVal reader As IDataReader) As DataTable
        Dim dt As New DataTable()
        Dim init = False
        dt.BeginLoadData()
        Dim vals As New Object
        While (reader.Read())

            If (Not init) Then
                init = True
                Dim fieldCount As Integer = reader.FieldCount
                For i As Integer = 0 To fieldCount
                    dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i))
                    vals = New Object[fieldCount]
                       Next
            End If
            reader.GetValues(vals);
    dt.LoadDataRow(vals, True);
  End While
        reader.Close();
  dt.EndLoadData();
  Return dt;
    End Function
End Class
