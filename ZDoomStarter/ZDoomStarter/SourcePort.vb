﻿Public Class SourcePort
    ' Declarations and Initializations
    ' -------------------------------------------------
    Public Property absolutePath As String      ' Absolute path to that executable
    Public Property niceName As String          ' Name of that executable
    Public Property customNotes As String       ' Optional notes provided by the end-user
    ' -------------------------------------------------




    ' Default Constructor
    ' -------------------------------------------------
    ' The default constructor for this class; implicit values will be given once instantiated.
    Public Sub New()
        absolutePath = Nothing      ' Null Value
        niceName = Nothing          ' Null Value
        customNotes = Nothing       ' Null Value
    End Sub




    ' Constructor
    ' -------------------------------------------------
    ' Explicit constructor for this class; this will set the values accordingly when instantiated properly.
    Public Sub New(path As String,
                   name As String,
                   note As String)
        absolutePath = path
        niceName = name
        customNotes = note
    End Sub
End Class
