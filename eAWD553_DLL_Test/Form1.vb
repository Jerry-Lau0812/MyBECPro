
Public Class Form1

    Private Sub Btn_Lin_Active_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Lin_Active.Click
        Dim Res As Boolean = False
        Dim err As String
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.MainAsync("LIN1", 19200, "D:\Bitron_Project\553 eAWD\NET_EE_LDF_LIN_BCM-CSWC_QVPM_R2.ldf", err)
    End Sub

    Private Sub Btn_LIN_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_LIN_Stop.Click
        Dim Res As Boolean = False
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.MainStop("LIN1")
    End Sub

    Private Sub Btn_LIN_Sleep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_LIN_Sleep.Click
        Dim Res As Boolean = False
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.Sleep("LIN1")
    End Sub

    Private Sub Btn_LIN_Wakeup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_LIN_Wakeup.Click
        Dim Res As Boolean = False
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.Wakeup("LIN1")
    End Sub

    Private Sub Btn_Command_QVPM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Command_QVPM.Click
        Dim Res As Boolean = False
        Dim Para As New eAWD553_BCM_LIN.Lv_Cluster_Configuration
        Para.configurationQVPM = &H3
        Para.soundEmittersEnable = &H1
        Para.vehicleSpeedVSOSig = &H140
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.Set_COMMAND_QVPM("LIN1", Para)
    End Sub

    Private Sub Btn_Get_FW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Get_FW.Click
        Dim Res As Boolean = False
        Dim Rec As New eAWD553_BCM_LIN.Status
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.Get_FW_VERSION("LIN1", eAWD553_BCM_LIN.VerType.HW, Rec)
    End Sub

    Private Sub Btn_ChangeSche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ChangeSche.Click
        Dim Res As Boolean = False
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.ChangeSchedules("LIN1", "Diag") 'Normal
    End Sub

    Private Sub Btn_GetFeedbackErr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_GetFeedbackErr.Click
        Dim Res As Boolean = False
        Dim Rec As New eAWD553_BCM_LIN.Status1
        Res = eAWD553_BCM_LIN.eAWD553_BCM_LIN.Get_FEEDBACK_QVPM("LIN1", Rec)
    End Sub
End Class
