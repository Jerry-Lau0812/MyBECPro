Public Class Form1

    Private Sub Btn_Hex2Asci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Hex2Asci.Click
        Dim StrRaw As String = TxtStrRaw.Text
        TxtStrRes.Text = HexStringToAsciiString(StrRaw)
    End Sub


    Public Function HexStringToAsciiString(ByVal hexString As String) As String
        Dim array() As String = hexString.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        For idx As Integer = 0 To array.Length - 1
            array(idx) = Chr(CInt(String.Format("&h{0}", array(idx))))
        Next
        Return String.Join(String.Empty, array)
    End Function


    Public Function AsciiStringToHexString(ByVal asciiString As String) As String
        Dim ascii() As Byte = System.Text.Encoding.Default.GetBytes(asciiString)
        Dim count As Integer = ascii.Length
        Dim hexArray(count - 1) As String
        For idx As Integer = 0 To count - 1
            hexArray(idx) = ascii(idx).ToString("x2")
        Next
        Return String.Join(" ", hexArray)
    End Function



    Private Sub Btn_Compare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Compare.Click
        Dim sIndex As List(Of String)
        Dim s As String = TxtRaw.Text
        Dim r As String = TxtTarget.Text
        sIndex = StrCompare(r, s)
        TxtDiff.Text = PerrIndex(sIndex)
    End Sub


    ''' <summary>
    ''' Point Initial Touch Matrix
    ''' </summary>
    ''' <param name="SIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PerrIndex(ByVal SIndex As List(Of String)) As String
        Dim STip As String = ""
        Dim TmpIndex() As String
        Dim ArrIndex(,) As String = {{"PL1", "PL2", "PL3", "PL7", "PL8", "PL9", "PL10", "PL11"},
                                       {"PL12", "PL13", "PL14", "PL15", "PL16", "PL17", "PL18", "PL19"},
                                       {"PL20", "PL21", "PL22", "PL23", "PL24", "PL25", "PL26", "PL27"},
                                       {"PL28", "PL29", "PL30", "PL31", "PL32", "PL33", "PL34", "PL35"},
                                       {"PL36", "SW1", "ADDA1", "ADD2", "ADD3", "-", "-", "-"}}
        If SIndex.Count > 0 Then
            For Each i In SIndex
                TmpIndex = i.Split(",")
                STip &= ArrIndex(TmpIndex(0), TmpIndex(1)) & " "
            Next
            Return STip & "Err"
        Else
            Return STip
        End If
    End Function

    ''' <summary>
    ''' Process deal with wrong touch
    ''' </summary>
    ''' <param name="StrTarget"></param>
    ''' <param name="StrRaw"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function StrCompare(ByVal StrTarget As String, ByVal StrRaw As String) As List(Of String)
        Dim LstBT As New List(Of String)
        Dim S As String = DealRawData(StrRaw)
        Dim T As String = DealRawData(StrTarget)
        Dim SS() As String = S.Split(" ")
        Dim TT() As String = T.Split(" ")

        Dim BYTEIndex As Integer = 0 '记录哪个字节出问题
        Dim BITIndex As Integer = 0 '记录哪个位有问题

        For i As Integer = 0 To 4
            If SS(i) <> TT(i) Then
                BYTEIndex = i

                Dim SR As Integer = "&h" & SS(BYTEIndex)
                Dim ST As Integer = "&h" & TT(BYTEIndex)

                Dim BSR As String = Convert.ToString(SR, 2).PadLeft(8, "0") '转换为“0000000”形式
                Dim BST As String = Convert.ToString(ST, 2).PadLeft(8, "0") '转换为“0000000”形式

                For j As Integer = 0 To 7
                    If BSR(j) <> BST(j) Then
                        LstBT.Add(BYTEIndex & "," & (7 - j).ToString)
                    End If
                Next

            End If
        Next
        Return LstBT
    End Function

    ''' <summary>
    ''' Deal With 5 Bytes
    ''' </summary>
    ''' <param name="Raw"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DealRawData(ByVal Raw As String) As String
        Dim StrRet As String = ""
        Dim StrFlag As String = "C9 3E 4E 0B 92 03 01 01"
        If Raw = "" Then Return ""
        If Raw.Contains(StrFlag) Then
            StrRet = Raw.Substring(Raw.IndexOf(StrFlag) + 30, 14)
        End If
        Return StrRet
    End Function



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Week() As String = {&H7, &H1, &H2, &H3, &H4, &H5, &H6}
        Dim NowTime As Date
        NowTime = DateTime.Now
        Dim t = NowTime.DayOfWeek
        Dim S = Week(t)
    End Sub
End Class
