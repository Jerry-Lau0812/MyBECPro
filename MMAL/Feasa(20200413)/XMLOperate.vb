Imports System.Xml
Public Class XmlOperate
    Dim mXmlDoc As New System.Xml.XmlDocument
    Public FunzConf As XmlElement
    Public XmlFile As String

    Public Sub New(ByVal File As String)
        MyClass.XmlFile = File
        MyClass.mXmlDoc.Load(MyClass.XmlFile)       '加载配置文件   
        FunzConf = mXmlDoc.DocumentElement
    End Sub
    '功能：创建节点    
    '参数：node--节点       element--元素名            
    '返回：元素值   字符型    
    '             $--表示出错误    
    '    Public Function CreateNode(ByVal node As String) As Boolean
    '        On Error GoTo Err
    '        Dim Flag As Boolean = False
    '        Dim root As XmlNode = mXmlDoc.SelectSingleNode("MMAL")

    '        For Each xNode In root
    '            If xNode.Name = "C" + node Then
    '                Flag = False
    '                Exit For
    '            Else
    '                Flag = True
    '            End If
    '        Next

    '        If Flag Then
    '            Dim XmlNode As XmlElement = mXmlDoc.CreateElement("C" + node)
    '            XmlNode.InnerText = "0"
    '            root.InsertAfter(XmlNode, root.LastChild)
    '            mXmlDoc.Save(MyClass.XmlFile)
    '        End If

    '        Return True
    'Err:
    '        Return False
    '    End Function


    '功能：取得元素值    
    '参数：node--节点       element--元素名            
    '返回：元素值   字符型    
    '             $--表示出错误    
    Public Function GetElement(ByVal node As String, ByVal element As String) As String
        On Error GoTo Err
        Dim mXmlNode As System.Xml.XmlNode = mXmlDoc.SelectSingleNode("//" + node)

        '读数据    
        Dim xmlNode As System.Xml.XmlNode = mXmlNode.SelectSingleNode(element)
        Return xmlNode.InnerText.ToString
Err:
        Return "$"
    End Function


    Public Overloads Function RicercaXML(ByVal Xpath As String, ByVal Elemento As String)
        Dim View As XmlNodeList
        Dim Ris As String
        'restituisce l'elemento 0 dellaricerca
        Try

            View = FunzConf.SelectNodes(Xpath)

            Ris = View.ItemOf(0)(Elemento).InnerText
        Catch
            Ris = "-1"
            'Esito(0) = False : Esito(1) = False
            'MsgBox("Attenzione impossibile leggere il file di configurazione", MsgBoxStyle.Critical)
        End Try
        'Dim View As XmlNodeList = ConfigHW.SelectNodes("rs232[impiego='CUBLOC']")
        Return Ris
    End Function

    Public Overloads Function RicercaXML(ByVal Xpath As String)
        Dim View As XmlNodeList
        Dim Ris As String
        'restituisce l'elemento 0 dellaricerca
        Try

            View = FunzConf.SelectNodes(Xpath)

            Ris = View.Count
        Catch
            Ris = "-1"
            'Esito(0) = False : Esito(1) = False
            'MsgBox("Attenzione impossibile leggere il file di configurazione", MsgBoxStyle.Critical)
        End Try
        'Dim View As XmlNodeList = ConfigHW.SelectNodes("rs232[impiego='CUBLOC']")
        Return Ris
    End Function

    Public Overloads Function RicercaXML(ByVal Xpath As String, ByVal Elemento As String, ByVal NumElement As Integer)
        Dim View As XmlNodeList
        Dim Ris As String
        'restituisce l'elemento 0 dellaricerca
        Try
            View = FunzConf.SelectNodes(Xpath)
            Ris = View.ItemOf(NumElement)(Elemento).InnerText
        Catch
            Ris = "-1"
            'Esito(0) = False : Esito(1) = False
            'MsgBox("Attenzione impossibile leggere il file di configurazione", MsgBoxStyle.Critical)
        End Try
        'Dim View As XmlNodeList = ConfigHW.SelectNodes("rs232[impiego='CUBLOC']")
        Return Ris
    End Function

    '    
    '功能：保存元素值    
    '参数：node--节点名称     element--元素名       val--值    
    '返回：True--保存成功     False--保存失败    
    Public Function SaveElement(ByVal node As String, ByVal element As String, ByVal val As String) As Boolean
        On Error GoTo err
        Dim mXmlNode As System.Xml.XmlNode = mXmlDoc.SelectSingleNode("//" + node)
        Dim xmlNodeNew As System.Xml.XmlNode

        xmlNodeNew = mXmlNode.SelectSingleNode(element)
        xmlNodeNew.InnerText = val
        mXmlDoc.Save(MyClass.XmlFile)
        Return True
err:
        Return False
    End Function

    Public Function TH_Resistor(ByVal sBinCode As String) As String
        Return Me.RicercaXML("LEDBIN[BIN='" & sBinCode & "']", "RESISTOR_OHM")
    End Function

    Public Function TH_BPartNo(ByVal sBinCode As String) As String
        Return Me.RicercaXML("LEDBIN[BIN='" & sBinCode & "']", "BITRONPART")
    End Function

    Public Function Test_Count() As Integer
        Return Me.RicercaXML("TESTResSTEP")
    End Function

    Public Function TH_VF(ByVal sPartNo As Integer, ByVal Parameter As String) As Single
        Return Me.RicercaXML("LEDVF/VFBinRange[VFBin='" & VFBinName(sPartNo) & "']", Parameter)
    End Function


    'Public Function TH_VF(ByVal sVF As String, ByRef HighLimit As Single, ByRef LowLimit As Single) As Boolean
    '    Dim Res As Boolean = False
    '    Try
    '        LowLimit = Me.GetElement("LEDVF", sVF & "_L")
    '        HighLimit = Me.GetElement("LEDVF", sVF & "_H")
    '        Res = True
    '    Catch ex As Exception
    '        Res = False
    '    End Try
    '    Return Res
    'End Function
    Public Function VF_OFFSET(ByVal PartNum As Integer) As String
        Return Me.RicercaXML("LEDVF[PartNo='" & PartNum.ToString & "']", "VFOFFSET")
    End Function


    Public Function GT_StepRes(ByVal sStepNo As Integer) As String
        Return Me.RicercaXML("TESTResSTEP[STEPNO='" & sStepNo.ToString & "']", "RES")
    End Function

    Public Function GT_StepBin(ByVal sStepNo As Integer) As String
        Return Me.RicercaXML("TESTResSTEP[STEPNO='" & sStepNo.ToString & "']", "BIN")
    End Function

    Public Function GT_BinTu_H(ByVal sBin As String) As String
        Return Me.RicercaXML("CurI_TURN[BIN_TU='" & sBin & "']", "TU_H")
    End Function

    Public Function GT_BinTu_L(ByVal sBin As String) As String
        Return Me.RicercaXML("CurI_TURN[BIN_TU='" & sBin & "']", "TU_L")
    End Function

    Public Function GT_BinTa_H(ByVal sBin As String) As String
        Return Me.RicercaXML("CurI_TAIL[BIN_TA='" & sBin & "']", "TA_H")
    End Function

    Public Function GT_BinTa_L(ByVal sBin As String) As String
        Return Me.RicercaXML("CurI_TAIL[BIN_TA='" & sBin & "']", "TA_L")
    End Function

    Public Function GT_BinTurn() As String
        Return Me.GetElement("TESTCurSTEP", "BIN_TU")
    End Function

    Public Function GT_BinTAIL() As String
        Return Me.GetElement("TESTCurSTEP", "BIN_TA")
    End Function





End Class
