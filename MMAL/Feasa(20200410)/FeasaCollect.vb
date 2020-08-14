Public Class FeasaCollect
    Private mFeasaName As String
    Private mH() As Double
    Private mS() As Integer
    Private mI() As Integer
    Private mAbsI() As Double
    Private mWave() As Integer
    Private mCx() As Double
    Private mCy() As Double

    Public Property FeasaName
        Set(ByVal value)
            mFeasaName = value
        End Set
        Get
            Return mFeasaName
        End Get
    End Property

    Public Property H
        Set(ByVal value)
            mH = value
        End Set
        Get
            Return mH
        End Get
    End Property

    Public Property S
        Set(ByVal value)
            mS = value
        End Set
        Get
            Return mS
        End Get
    End Property

    Public Property I
        Set(ByVal value)
            mI = value
        End Set
        Get
            Return mI
        End Get
    End Property

    Public Property AbsI
        Set(ByVal value)
            mAbsI = value
        End Set
        Get
            Return mAbsI
        End Get
    End Property

    Public Property Wave
        Set(ByVal value)
            mWave = value
        End Set
        Get
            Return mWave
        End Get
    End Property

    Public Property Cx
        Set(ByVal value)
            mCx = value
        End Set
        Get
            Return mCx
        End Get
    End Property

    Public Property Cy
        Set(ByVal value)
            mCy = value
        End Set
        Get
            Return mCy
        End Get
    End Property

End Class
