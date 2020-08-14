
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Public Class FrmSetup

    Public Shared Attivo As Boolean = False
    Public StrDispFeasa As New StringBuilder

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Risposta As String
        Try


        Catch
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Risposta As String
        Try

        Catch
        End Try
    End Sub

    Private Sub FrmSetup_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        principale.Esito(1) = False
        principale.Esito(0) = False
        FrmSetup.Attivo = False

    End Sub

    Private Sub FrmSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Attivo = True
        Me.LblSite.Text = "Site:" + NumSite.ToString
        Me.LblSite.Refresh()
        LoadGridFeasa()


    End Sub

    Private Sub ChkTimbro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)

    End Sub
    Private Sub EseguiCommand(ByVal NomeControllo As String, ByVal valore As Boolean)
        Dim Risposta As String
        Dim NumPic As String
        Dim Messaggio As String = ""

        Select Case NomeControllo
            Case "ChkProg1"
                Messaggio = "PRG1"
            Case "ChkProg2"
                Messaggio = "PRG2"
            Case "ChkLed1"
                Messaggio = "LED1"
            Case "ChkLed2"
                Messaggio = "LED2"
            Case "ChkStamp1"
                Messaggio = "TIM1"
            Case "ChkStamp2"
                Messaggio = "TIM2"
            Case "ChkPow1"
                Messaggio = "POW1"
            Case "ChkPow2"
                Messaggio = "POW2"
            Case "ChkSez"
                Messaggio = "SEZ"
        End Select

        If valore = True Then
            principale.Test.Invia(Messaggio + "1")
        Else
            principale.Test.Invia(Messaggio + "0")
        End If
        Risposta = principale.Test.Ricevi(2)
        If Risposta = "" Then MsgBox("PLC Comunication error", MsgBoxStyle.Critical)

    End Sub
    Private Sub LoadGridFeasa()
        Dim RowNum As Integer
        Dim LedNAme As String
        Dim Id As Integer = 1
        Dim T As Integer
        Dim NumSite As String
        Dim CodeLed As String

        LedNAme = TH_FeasaStringID("LEDNAME", Id)
        While LedNAme <> "-1"
            CodeLed = TH_FeasaStringID("CODE", Id)
            GridDispFeasa.Rows.Add()
            GridDispFeasa().Rows(RowNum).Cells(0).Value = TH_FeasaStringID("CH", Id)
            GridDispFeasa().Rows(RowNum).Cells(1).Value = LedNAme
            GridDispFeasa().Rows(RowNum).Cells(2).Value = Ledcolor(CodeLed)
            NumSite = TH_FeasaStringID("SITE", Id)
            GridDispFeasa().Rows(RowNum).Cells(3).Value = NumSite
            'If LedNAme <> "0" Then Me.CmbLed.Items.Add(Id.ToString.PadLeft(2, "0"))
            RowNum = RowNum + 1
            Id = Id + 1
            LedNAme = TH_FeasaStringID("LEDNAME", Id)
            ' GridDispFeasa().Rows(RowNum).Cells(1).Style.BackColor = Color.Red
        End While

        Id = 1
        'Carica combo on Led
        LedNAme = TH_FeasaStringID("LEDNAME", Id)
        While LedNAme <> "-1"
            If LedNAme <> "0" Then Me.CmbLed.Items.Add(LedNAme)
            Id = Id + 1
            LedNAme = TH_FeasaStringID("LEDNAME", Id)
        End While

    End Sub
    Private Sub ControlMove(ByVal Control As System.Object, ByVal UUT As String, ByVal Ope As String)
        Dim Risposta As String
        Try
            If Control.Checked = True Then
                principale.Test.Invia(Ope + UUT + "1")
            Else
                principale.Test.Invia(Ope + UUT + "0")
            End If
            Risposta = principale.Test.Ricevi(2)
            If Risposta = "" Then MsgBox("Il microcontrollore non risponde")
        Catch
            MsgBox("Comunication Error")
        End Try
    End Sub


    Private Sub ChkLockFix_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlMove(sender, "", "LOK")
    End Sub

    Private Sub ChkUpDown_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlMove(sender, "", "UPD")
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)
    End Sub

    Private Sub Out_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UutNum_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ChkPower1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)

    End Sub

    Private Sub ChkProg2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)
    End Sub

    Private Sub ChkStamp2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)
    End Sub

    Private Sub ChkLed2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)
    End Sub

    Private Sub ChkPower2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EseguiCommand(sender.name, sender.checked)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GroupBox5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ChkDsc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlMove(sender, "", "DSC")

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ChkSin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlMove(sender, "", "SIN")
    End Sub


    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub CheckBox1_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button52_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LblBattery2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LblBattery1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TabPage5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlMove(sender, "", "ACON")
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlMove(sender, "", "OFFAC")
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub LblPower_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub Label3_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub TabPage1_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub ChkSez_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ControlMove(sender, "", "SEZ")
    End Sub





    Private Sub Button59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button59.Click
        Dim Risposta As String
        Me.TxtDispfeasa.Text = ""
        Me.TxtDispfeasa.Refresh()
    Principale.Test.Invia("getSerial" + Chr(13), Principale.IdCom("FEASA"), 6)
        Risposta = Principale.Test.Ricevi(5)
    Me.TxtDispfeasa.Text = Risposta
    End Sub

    'Private Sub Button58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button58.Click
    '    Dim Risposta As String
    '    Dim ID As Integer = 1
    '    Dim Hue As String
    '    Dim Saturation As String
    '    Dim Intensity As String
    '    Dim StartPar As Integer
    '    Dim NomeLed As String
    '    Try
    '        Me.TxtDispfeasa.Text = ""
    '        Me.TxtDispfeasa.Refresh()
    '        modTestplan.ReadFeasa(0, CType(CmbRange.Text, Integer), "FEASA1") ' acquisisce dati canale 
    '        modTestplan.ReadFeasaXY(0, CType(CmbRange.Text, Integer), "FEASA1")
    '        modTestplan.Readwavelenght(0, CType(CmbRange.Text, Integer), "FEASA1")
    '        modTestplan.ReadAbsolutIntensity(0, CType(CmbRange.Text, Integer), "FEASA1")

    '        NomeLed = TH_FeasaStringID("LEDNAME", ID)
    '        While NomeLed <> "-1"
    '            GridDispFeasa().Rows(ID - 1).Cells(4).Value = Feasa(0).Hue(GridDispFeasa().Rows(ID - 1).Cells(0).Value - 1)
    '            GridDispFeasa().Rows(ID - 1).Cells(5).Value = Feasa(0).Saturation(GridDispFeasa().Rows(ID - 1).Cells(0).Value - 1)
    '            GridDispFeasa().Rows(ID - 1).Cells(6).Value = Feasa(0).intensity(GridDispFeasa().Rows(ID - 1).Cells(0).Value - 1)
    '            GridDispFeasa().Rows(ID - 1).Cells(7).Value = Feasa(0).Cx(GridDispFeasa().Rows(ID - 1).Cells(0).Value - 1)
    '            GridDispFeasa().Rows(ID - 1).Cells(8).Value = Feasa(0).Cy(GridDispFeasa().Rows(ID - 1).Cells(0).Value - 1)
    '            GridDispFeasa().Rows(ID - 1).Cells(9).Value = Feasa(0).wavelenght(GridDispFeasa().Rows(ID - 1).Cells(0).Value - 1)
    '            GridDispFeasa().Rows(ID - 1).Cells(10).Value = Feasa(0).AbsIntensity(GridDispFeasa().Rows(ID - 1).Cells(0).Value - 1)
    '            ID = ID + 1
    '            NomeLed = TH_FeasaStringID("LEDNAME", ID)
    '        End While
    '        Me.TxtDispfeasa.Text = "DONE"
    '        Me.TxtDispfeasa.Refresh()
    '    Catch

    '        MsgBox("Critical Aquire Error")
    '    End Try
    'End Sub




    'Private Sub Button1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim NomeLed As String
    '    ' NomeLed = Principale.Test.RicercaXML(FileXML.FunzConf, "VERSIONI[CODE_BITRON='" & Principale.ProgUso & "']/FEASA1[CH=" & CmbLed.Text & "]", "LEDNAME")
    '    NomeLed = CmbLed.Text
    '    PowerON(NomeLed)
    'End Sub

    Private Sub TabPage7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage7.Click

    End Sub

    'Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ReadFeasa(0, CType(Me.CmbRange.Text, Integer), "FEASA1")
    '    GridDispFeasa().Rows(CType(Me.CmbLed.Text, Integer) - 1).Cells(4).Value = Feasa(0).Hue(CType(Me.CmbLed.Text, Integer) - 1)
    '    GridDispFeasa().Rows(CType(Me.CmbLed.Text, Integer) - 1).Cells(5).Value = Feasa(0).Saturation(CType(Me.CmbLed.Text, Integer) - 1)
    '    GridDispFeasa().Rows(CType(Me.CmbLed.Text, Integer) - 1).Cells(6).Value = Feasa(0).intensity(CType(Me.CmbLed.Text, Integer) - 1)

    'End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim Risposta As String
        Me.TxtDispfeasa.Text = ""
        Me.TxtDispfeasa.Refresh()
    Principale.Test.Invia("getHW" + Chr(13), Principale.IdCom("FEASA"), 12)
    Risposta = Principale.Test.Ricevi(1)
        Me.TxtDispfeasa.Text = Risposta
    End Sub

    'Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '  PowerONBoard()
    'End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For T As Integer = 0 To 9

            GridDispFeasa().Rows(T).Cells(3).Value = ""
            GridDispFeasa().Rows(T).Cells(4).Value = ""
            GridDispFeasa().Rows(T).Cells(5).Value = ""
            GridDispFeasa().Rows(T).Cells(6).Value = ""
        Next T
    End Sub

    'Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '  Dim Result As Integer
    '  Me.TxtDispfeasa.Text = ""
    '  Me.TxtDispfeasa.Refresh()
    '  Result = modTestplan.Uda.LedOn(8)
    '  Me.TxtDispfeasa.Text = Result.ToString
    'End Sub

    'Private Sub Button8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '  Dim Result As Integer
    '  Me.TxtDispfeasa.Text = ""
    '  Me.TxtDispfeasa.Refresh()
    '  Result = modTestplan.Uda.LedOn(9)
    '  Me.TxtDispfeasa.Text = Result.ToString
    'End Sub

    'Private Sub Button9_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '  Dim Result As Integer
    '  Me.TxtDispfeasa.Text = ""
    '  Me.TxtDispfeasa.Refresh()
    '  Result = modTestplan.Uda.LedOn(0)
    '  Me.TxtDispfeasa.Text = Result.ToString
    'End Sub




 

  Private Sub TxtHwVer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

 

    'Private Sub Button14_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '  Dim Result As String

    '  Result = modTestplan.Uda.ReadIR

    'End Sub

  Private Sub LblSite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblSite.Click

  End Sub

    'Private Sub Button2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    PowerON("DL0")
    'End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    'Private Sub Button7_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
    '    StrDispFeasa.Length = 0
    '    Me.TxtDispfeasa.Text = ""
    '    Me.LblFeasa.Visible = True
    '    CalibrationCXCY()
    '    CalibrationIntensity()
    '    CalibrationWavelenght()
    '    Me.LblFeasa.Visible = False
    'End Sub
   

   
End Class