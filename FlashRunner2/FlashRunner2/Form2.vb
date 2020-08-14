Public Class Form2
    Delegate Sub TransferStatus(ByVal file_len As Integer, ByVal file_pos As Integer)
    Public CurrentpPogress As TransferStatus = New TransferStatus(AddressOf ShowProgressStatus)


    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CurrentpPogress = New TransferStatus(AddressOf ShowProgressStatus)
    End Sub



    Public Sub ShowProgressStatus(ByVal file_len As Integer, ByVal file_pos As Integer)
        Dim Msg As String = file_pos & "/" & file_len
        'ProgressBar1.Minimum = 0
        'ProgressBar1.Maximum = file_len
        'ProgressBar1.Value = file_pos
        Lbl_Msg.Text = Msg
        'ShowProgressStatus = 0
        Application.DoEvents()
    End Sub
End Class