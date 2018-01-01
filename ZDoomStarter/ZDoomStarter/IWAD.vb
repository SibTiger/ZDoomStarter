' Internal WAD (IWAD)
' ---------------------------------------
' =======================================
' This class is designed to hold basic information regarding the
' IWAD's that is available to this program.
' ---------------------------------------




Public Class IWAD
#Region "Declarations and Initializations"
    Private _absolutePath As String     ' Absolute path to that executable
    Private _niceName As String         ' Name of that executable
    Private _customNotes As String      ' Optional notes provided by the end-user
#End Region




    ' Default Constructor
    ' -------------------------------------------------
    ' The default constructor for this class; implicit values will be given once instantiated.
    Public Sub New()
        _absolutePath = Nothing     ' Assign it as 'Nothing'
        _niceName = Nothing         ' Assign it as 'Nothing'
        _customNotes = Nothing      ' Assign it as 'Nothing'
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
    ' Allow the usage or alteration of the 'Custom Notes' variable
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
