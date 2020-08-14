Module Fvt
    Public Principale As New Principale
    Public Test As New Test

    Public Sub Main()
        Application.EnableVisualStyles()
        Principale.Avvio()
        ' Keyence(0) = New LanKeyence
        'Keyence(1) = New LanKeyence
        'Apricom("PIC")
        Principale.Apricom("FEASA")
        'OpenComProg("PROG1", 0)
        'OpenComProg("PROG2", 1)
        ' Apricom("MATRIX1")
        'Apricom("MATRIX2")
        Principale.ReadFeasa(0)
        FrmPannello.Show()
        principale.Test.CaricaDatiTmp()
        FrmPannello.Refresh()
        ' FrmPannello.LblRevSW.Text = "Rev. 1.0"
        If FrmIni.ShowDialog() = DialogResult.OK Then
            FrmPannello.LblStato(0).Text = "READY"
            FrmPannello.LblStato(1).Text = "READY"
            Application.Run(FrmPannello)
        Else
            FrmPannello.Close()
            wn_comm_wrapper.WN_CloseCommPortW(comm_handle(0))
            'wn_comm_wrapper.WN_CloseCommPortW(comm_handle(1))
        End If

    End Sub



End Module
