' Configure PWAD Directory [UI]
' ---------------------------------------
' =======================================
' This class is designed to allow the end-user to customize the
' directory location of where the PWADs are located.
' This program will not do another drastic with this information,
' but merely designed to make life easier for the end-user when
' he or she adds PWADs into their game setup.
' ---------------------------------------




Public Class ConfigPWADPath
#Region "Declarations and Initializations"
    ' This will be available and set when the user selects a new directory that
    ' holds the user's PWAD resources.  This variable is only meant for caching data, but not permanent storage.
    Private newDirectoryRequest As String

    ' This will hold the current (and, if modified) PWAD directory
    '  This will be automatically set when this window is initialized; the
    '  parent window will set this variable in the New() function.
    Public Property PWADPath As String

    ' Exit flag; when the user clicks on either cancel or okay
    ' button, this variable will dictate rather the PWAD path will
    ' be updated or remain as-is.
    ' Meaning of the values:
    '   Cancel = False
    '       Nothing is updated
    '   OK = True
    '       Update the PWAD path
    Public updatePWADPath As Boolean
#End Region




    ' Constructor Window
    ' ------------------------------------------
    ' This is the default constructor for this window.
    ' This is ideal for transferring data from one end to another.
    ' -----------------------
    ' Parameters
    '   pwadDirectory [String]
    '       Contains the PWAD directory path.
    Public Sub New(ByVal pwadDirectory As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Cache the PWAD Directory
        PWADPath = pwadDirectory

        ' Update the TextBox that contains the PWAD path.
        RefreshDirectory()
    End Sub




    ' Refresh Directory
    ' ------------------------------------------
    ' This function will refresh the text box and provide information regarding
    ' the PWAD directory.
    ' If in case the directory was never set or cleared, than try to provide a message
    ' that is understandable to the user - only for presentation sakes.
    Private Sub RefreshDirectory()
        ' Declarations and Initializations
        ' ---------------------------------
        Dim defaultEmptyMSG As String = "<< empty >>"
        ' ---------------------------------

        ' First check to make sure that the string is not empty (or Nothing).
        ' If it is nothing or not set, then set it accordingly.
        If PWADPath Is Nothing Then
            ' If the text is 'Nothing'; provide something meaningful
            TextBoxDirectoryPath.Text = defaultEmptyMSG
        ElseIf PWADPath Is "" Then
            ' If the text is empty; provide something meaningful
            ' Technically, this shouldn't really happen - but support it
            '  just in case.
            TextBoxDirectoryPath.Text = defaultEmptyMSG
        Else
            ' Provide the path as the text
            TextBoxDirectoryPath.Text = PWADPath
        End If
    End Sub




    ' Select New Directory
    ' ------------------------------------------
    ' This function will allow the end-user to select a new directory that houses their PWADs.  When a new path has been selected, this function will store that path into a temporary variable for other member functions to utilize.  That information is understood to be available when this function returns 'True' to the calling function.  When nothing is selected, no data is kept - but returns 'False'
    ' ----
    ' DEPENDENCY NOTE:
    '   Because WPF does NOT include an folder browser dialog, like with OpenFileDialog(),
    '   we will have To use the FolderBrowserDialog() from the Forms.
    '   More information is provided here:
    '   https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/walkthrough-hosting-a-windows-forms-control-in-wpf-by-using-xaml
    '   REFERENCES REQUIRED:
    '       >> WindowsFormIntegration
    '       >> System.Windows.Forms
    ' -----------------------
    ' Output:
    '   Boolean
    '       True = A valid directory has been chosen and cached for future use.
    '       False = No directory was selected, thus no data is available for use.
    Function SelectNewDirectory() As Boolean
        ' Declarations and Initializations
        ' ---------------------------------
        ' WARNING: REFERENCES
        Dim directoryBrowser As New System.Windows.Forms.FolderBrowserDialog()
        ' ---------------------------------

        If (directoryBrowser.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
            ' If the user selected a path, then save it
            newDirectoryRequest = directoryBrowser.SelectedPath

            Return True
        Else
            newDirectoryRequest = Nothing

            Return False
        End If
    End Function




    ' UI ELEMENTS
    ' =================================================
    ' =================================================
    ' =================================================




    ' Browse Directory [Button]
    ' ------------------------------------------
    ' This function will allow the end-user to provide information as
    ' to what absolute directory path contains the PWADs.  This will allow
    ' the user to easily customize their game; without this, the 
    ' inconvenience could deter the user away.
    Private Sub ButtonBrowse_Click(sender As Object, e As RoutedEventArgs) Handles ButtonBrowse.Click
        ' Ask the user for the directory and check to see if anything was selected.
        If (SelectNewDirectory()) Then
            ' If the user selected a path, then save it
            PWADPath = newDirectoryRequest
        Else
            ' If in case the request was denied or the user canceled,
            '   display a message stating that the change will not take place.
            MessageBox.Show("Unable to change PWAD Directory!",
                            "Change PWAD Directory Failure",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error)
        End If
    End Sub




    ' Clear Directory [Button]
    ' ------------------------------------------
    ' When clicked, this will clear the current PWAD directory location.
    ' Once cleared, refresh the TextBox so the user is aware that it was changed.
    Private Sub ButtonClear_Click(sender As Object, e As RoutedEventArgs) Handles ButtonClear.Click
        ' Set the directory to 'Nothing'.
        PWADPath = Nothing

        ' Refresh the text box
        RefreshDirectory()
    End Sub




    ' OK - Submit [Button]
    ' ------------------------------------------
    ' When clicked, this will close the window but also save the settings as requested.
    Private Sub ButtonOK_Click(sender As Object, e As RoutedEventArgs) Handles ButtonOK.Click
        ' Mark that we want to save the value
        updatePWADPath = True

        ' Close the window
        Close()
    End Sub




    ' Cancel [Button]
    ' ------------------------------------------
    ' When clicked, this will close the window but will discard the settings
    ' regardless if it was modified or not.
    Private Sub ButtonCancel_Click(sender As Object, e As RoutedEventArgs) Handles ButtonCancel.Click
        ' Mark that we do not want to save the settings
        updatePWADPath = False

        ' Close the window
        Close()
    End Sub
End Class
