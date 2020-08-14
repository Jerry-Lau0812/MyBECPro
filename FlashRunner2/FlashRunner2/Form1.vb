Public Class Form1
    Public Fr2 As New FlashInterface

    Private Sub Btn_Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Open.Click
        Dim Res As Boolean = False
        Res = Fr2.OpenCommunicationA()
        If Res Then
            Btn_Open.BackColor = Color.Green
        Else
            Btn_Open.BackColor = Color.Red
        End If
        'Fr2.PresentProgress = New FlashInterface.TransferStatus(AddressOf ShowCurrentStatus)
    End Sub

    Private Sub Btn_GetVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_GetVer.Click
        Dim StrVer As String = Fr2.GetFr2Version()
        Txt_Ver.Text = StrVer
    End Sub

    Private Sub Btn_TransPrj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_TransPrj.Click
        Dim Res As Boolean = False
        Dim SrcFile As String = ""
        OpenFileDialog1.ShowDialog()
        SrcFile = OpenFileDialog1.FileName
        If SrcFile <> "" Then
            If Fr2.CopyFileToSD(SrcFile, "PRJ") Then
            End If
        End If
    End Sub

    Public Delegate Sub ShowFunc()

    Public Sub ShowCurrentStatus(ByVal file_len As Integer, ByVal file_pos As Integer)
        Dim Msg As String = file_pos & "/" & file_len
        'ProgressBar1.Minimum = 0
        'ProgressBar1.Maximum = file_len
        'ProgressBar1.Value = file_pos
        Lbl_Msg.Text = Msg
        'ShowProgressStatus = 0
        Application.DoEvents()
    End Sub

    Public Sub DipValue(ByVal file_len As Integer, ByVal file_pos As Integer)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub



    Private Sub Btn_Delete_Pro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Delete_Pro.Click

    End Sub

    Dim PassCount As Integer = 0
    Dim FailCount As Integer = 0

    Private Sub Btn_Flash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Flash.Click
        'Dim t As Integer = 0
        'While t < 100
        Call FlashRunnerTest()
        't += 1
        'End While
        'Call FlashRunnerTest()
    End Sub

    Private Sub FlashRunnerTest()
        'Dim str As String = Fr2.FlashProgram("MASTER_BS0025_20191011.prj", "0001", TimeOut:=30)
        FlashResult.Clear()
        Txt_Result.Text = ""

        Dim FlashList As New List(Of String)
        'Dim ProjectMsg As String = "MASTER_BS0025_20191011.prj:0011"
        Dim ProjectMsg As String = TextFlashConfig.Text
        'Dim ProjectMaster As String = "MASTER_BS0025_20191011.prj:0011"
        'Dim ProjectSlave As String = "SLAVE_SS0012_20191011.prj:1100"
        'Dim ProjectName As String = ProjectMsg.Split(":")(0)

        Dim ProjectSiteConfig As String = ProjectMsg.Split(":")(1)

        FlashList.Add(ProjectMsg)
        'FlashList.Add(ProjectSlave)

        Dim Res As Boolean = False
        Res = Fr2.FlashMultiProgram(FlashList)

        If Not Res Then
            MsgBox("Start Failed.")
        End If


        Res = Fr2.Wait_FlashProgram_FlashRunner2_0("0011", FlashResult)

        If Not Res Then
            MsgBox("Wait Failed.")
        Else
            For t As Integer = 0 To 1
                Res = AnalyFlashResult(t)
            Next
        End If

        For Each Site As String In FlashResult
            Txt_Result.AppendText(Site & vbCrLf)

            If Site.Contains("PASS") Then
                PassCount += 1
            End If

            If Site.Contains("FAIL") Then
                FailCount += 1
            End If
        Next

        LblPassCount.Text = PassCount
        Lbl_FailCount.Text = FailCount
    End Sub



    Private Sub Btn_Analyse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Analyse.Click

    End Sub

    Private FlashResult As New List(Of String)
    Public Function AnalyFlashResult(ByVal ChannelNo As Integer) As Boolean
        Dim Result As Boolean = False
        Dim FlashResult_Channel As String = ""
        For Each a In FlashResult
            If a.Contains(ChannelNo) Then
                FlashResult_Channel = a.Substring(a.Length - 4, 4)
            End If
        Next

        Result = IIf(FlashResult_Channel = "PASS", True, False)

        Return Result
    End Function

    Private Sub Btn_CheckCRCPrj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_CheckCRCPrj.Click

    End Sub
End Class
