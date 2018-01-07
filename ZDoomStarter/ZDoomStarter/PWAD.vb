Public Class PWAD
#Region "Declarations and Initializations"
    Private _absolutePath As String     ' Absolute path to that executable
    Private _niceName As String         ' Name of that executable
#End Region



    ' Default Constructor
    ' -------------------------------------------------
    ' The default constructor for this class; implicit values will be given once instantiated.
    Public Sub New()
        _absolutePath = Nothing     ' Assign it as 'Nothing'
        _niceName = Nothing         ' Assign it as 'Nothing'
    End Sub




    ' Absolute Path [get; set;]
    ' -------------------------------------------------
    ' Allow the usage or alteration of the 'Absolute Path' variable.
    Public Property AbsolutePath() As String
        Get
            ' Return the value of the absolute path
            Return _absolutePath
        End Get
        Set(value As String)
            ' Set the value of the absolute path
            _absolutePath = value
        End Set
    End Property




    ' Nice Name [get; set;]
    ' -------------------------------------------------
    ' Allow the usage or alteration of the 'Nice Name' variable.
    Public Property NiceName() As String
        Get
            ' Return the value of the nice name
            Return _niceName
        End Get
        Set(value As String)
            ' Set the value of the nice name
            _niceName = value
        End Set
    End Property
End Class
