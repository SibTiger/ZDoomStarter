Public Class SourcePort
    ' Declarations and Initializations
    ' -------------------------------------------------
    Private _absolutePath As String      ' Absolute path to that executable
    Private _niceName As String          ' Name of that executable
    Private _customNotes As String       ' Optional notes provided by the end-user
    ' -------------------------------------------------




    ' Default Constructor
    ' -------------------------------------------------
    ' The default constructor for this class; implicit values will be given once instantiated.
    Public Sub New()
        _absolutePath = Nothing      ' Null Value
        _niceName = Nothing          ' Null Value
        _customNotes = Nothing       ' Null Value
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




    ' Customary Notes [get; set;]
    ' -------------------------------------------------
    ' Allow the usage or alteration of the 'Custom Notes' variable.
    Public Property CustomNotes() As String
        Get
            ' Return the value of the custom notes
            Return _customNotes
        End Get
        Set(value As String)
            ' Set the value of the custom notes
            _customNotes = value
        End Set
    End Property
End Class
