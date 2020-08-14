Public Class FormMsg
    Public MyUUTNum As Integer
    Public MyInfos As List(Of String) = New List(Of String)
    Public MyCurrentRow As Integer = 0
    Public ST As Stopwatch = New Stopwatch

    Public Sub New(UUTNum As Integer, Infos As List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        MyUUTNum = 1
        MyInfos = Infos

    End Sub

#Region "DGV Board"
    Public Sub SetDGVRowCols(DGV As DataGridView)
        Dim RowCount As Integer = MyUUTNum * MyInfos.Count  'Row count
        Dim ColCount As Integer = 3 'Column count

        DGV.Rows.Clear()
        DGV.Columns.Clear()

        DGV.RowCount = RowCount
        DGV.ColumnCount = ColCount

        DGV.Columns(0).HeaderText = "Step"
        DGV.Columns(1).HeaderText = "Description"
        DGV.Columns(2).HeaderText = "Status"
        DGV.Columns(0).Width = 50
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
        DGV.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


        DGV.Columns(2).Width = 100
        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet

        DGV.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
        DGV.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
        DGV.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable

        DGV.ClearSelection()
    End Sub
    Public Sub SetDGVInfo(DGV As DataGridView)
        Dim i As Integer = 0

        For Each Str As String In MyInfos

            If Str.StartsWith("-1") Then
                DGV.Rows(i).Cells(0).Value = (i + 1)
                DGV.Rows(i).Cells(1).Value = Str.Replace("-1", "")
                DGV.Rows(i).Cells(2).Value = "Ready"
                DGV.Rows(i).Cells(2).Style.BackColor = Color.FromArgb(211, 223, 240)
                i += 1
            Else
                For t As Integer = 0 To MyUUTNum - 1

                    DGV.Rows(i).Cells(0).Value = (i + 1)
                    'DGV.Rows(i).Cells(1).Value = "UUT" & t & " " & Str
                    DGV.Rows(i).Cells(1).Value = "" & Str
                    DGV.Rows(i).Cells(2).Value = "Ready"
                    DGV.Rows(i).Cells(2).Style.BackColor = Color.FromArgb(211, 223, 240)
                    i += 1
                Next
            End If
        Next

        DGV.ClearSelection()
    End Sub
    Public Sub AutoSetRowColSize(DGV As DataGridView)
        Dim RowCount As Integer = MyUUTNum * MyInfos.Count  'Row count
        Dim ColCount As Integer = 3 'Column count

        If RowCount = 0 Then Exit Sub
        If ColCount = 0 Then Exit Sub

        For t As Integer = 1 To RowCount
            DGV.Rows(t - 1).Height = (DGV.Height - 5) / RowCount
        Next
        For t As Integer = 1 To ColCount
            DGV.Columns(t - 1).Width = (DGV.Width - 5) / ColCount
        Next

        DGV.ClearSelection()

    End Sub
    Private Sub Frmpannello_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Call AutoSetRowColSize(dgvInfo)
    End Sub
    Private Sub FormMsg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call SetDGVRowCols(DGVInfo)
        Call SetDGVInfo(dgvInfo)

        Timer1.Enabled = True
        ST.Reset()
        ST.Start()
    End Sub

#End Region

    Public Sub SetTip(St As TipStatus)
        Select Case St
            Case TipStatus.FAIL
                LabTip.Text = "FAIL"
                LabTip.BackColor = Color.Red

            Case TipStatus.PASS
                LabTip.Text = "PASS"
                LabTip.BackColor = Color.Lime

            Case TipStatus.Loading
                LabTip.Text = "Loading..."
                LabTip.BackColor = Color.Yellow

        End Select

        Application.DoEvents()
    End Sub
    Public Sub SetDGVStatus(St As TipStatus, UUTNo As Integer, InfoIdex As Integer)
        Dim RowIndex = (UUTNo + (InfoIdex - 2) * MyUUTNum) + 2

        If UUTNo = -1 Then RowIndex = InfoIdex

        'Dim RowIndex = InfoIdex

        MyCurrentRow = RowIndex
        Select Case St
            Case TipStatus.FAIL
                dgvInfo.Rows(RowIndex).Cells(2).Value = "FAIL"
                dgvInfo.Rows(RowIndex).Cells(2).Style.BackColor = Color.Red

            Case TipStatus.PASS
                dgvInfo.Rows(RowIndex).Cells(2).Value = "PASS"
                dgvInfo.Rows(RowIndex).Cells(2).Style.BackColor = Color.Lime

            Case TipStatus.Loading
                dgvInfo.Rows(RowIndex).Cells(2).Value = "Loding..."
                dgvInfo.Rows(RowIndex).Cells(2).Style.BackColor = Color.Yellow

            Case TipStatus.CRCSame
                dgvInfo.Rows(RowIndex).Cells(2).Value = "Same"
                dgvInfo.Rows(RowIndex).Cells(2).Style.BackColor = Color.Lime

            Case TipStatus.CRCNotSame
                dgvInfo.Rows(RowIndex).Cells(2).Value = "NotSame"
                dgvInfo.Rows(RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

        End Select

        If RowIndex >= 10 Then
            dgvInfo.FirstDisplayedScrollingRowIndex = RowIndex - 5
        End If

        Application.DoEvents()
    End Sub
    Public Sub SetDGVStatus2(Msg As String)

        dgvInfo.Rows(MyCurrentRow).Cells(2).Value = Msg

        Application.DoEvents()


    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LabMsg.Text = "ElapsedTime:" & (ST.ElapsedMilliseconds / 1000).ToString("0.00") & "S"
    End Sub

End Class
Public Enum TipStatus
    PASS
    FAIL
    Loading
    CRCSame
    CRCNotSame
    'None
End Enum
