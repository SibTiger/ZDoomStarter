' Main Window [UI]
' ---------------------------------------
' =======================================
' This is our main entry point to this program.
' This class is designed to allow the user to quickly setup and start
' their game with minimal interaction as possible.
' ---------------------------------------




Class MainWindow
#Region "Declarations and Initializations"
    ' This list will hold all source port entries available to this program.
    ' NOTE: Property (or {get; set;}) is adjacent to a function call, hence the different
    ' naming scheme.
    Public Property SourcePortList As New List(Of SourcePort)

    ' ----

    ' This list will hold all IWAD entries available to this program.
    Public Property IWADList As New List(Of IWAD)

    ' ----

    ' This string will hold the PWAD directory that is available to this program.
    Public Property PWADPath As String
#End Region




    ' Window Load [EVENT: Form Load]
    ' ------------------------------------------
    ' This function will automatically execute once the window has been fully rendered
    Private Sub Window_Load() Handles MyBase.Loaded
#Region "TESTING: Source Port List"
        ' Temporary Testing; remove when saving is possible
        Dim Engine1 As New SourcePort
        Dim Engine2 As New SourcePort

        With Engine1
            .AbsolutePath = ".\ZDoom.exe"
            .CustomNotes = "Optional Notes"
            .NiceName = "ZDoom"
        End With

        With Engine2
            .AbsolutePath = ".\Zandronum.exe"
            .CustomNotes = "Online Testing"
            .NiceName = "Zandronum"
        End With

        SourcePortList.Add(Engine1)
        SourcePortList.Add(Engine2)
#End Region

#Region "TESTING: IWAD List"
        Dim IWAD1 As New IWAD
        Dim IWAD2 As New IWAD

        With IWAD1
            .AbsolutePath = ".\ZDoom\IWAD\Doom2.wad"
            .CustomNotes = "Doom 2: Hell on Earth"
            .NiceName = "Doom II"
        End With

        With IWAD2
            .AbsolutePath = ".\ZDoom\IWAD\Heretic.wad"
            .CustomNotes = "Heretic: Shadow of the Serpent Riders"
            .NiceName = "Heretic"
        End With

        IWADList.Add(IWAD1)
        IWADList.Add(IWAD2)
#End Region

#Region "TESTING: PWAD Directory"
        PWADPath = "C:\WADs"
#End Region
    End Sub




    ' UI ELEMENTS
    ' =================================================
    ' =================================================
    ' =================================================



    ' File Menu: Destroy
    ' ------------------------------------------
    ' This will close the program properly.
    Private Sub Menu_Exit_Click(sender As Object, e As RoutedEventArgs) Handles FileMenuExit.Click
        ' When clicked, just immediately close the program.
        Close()
    End Sub




    ' Configure File Menu: Configure Source Ports
    ' ------------------------------------------
    Private Sub ConfigureMenuSourcePorts_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuSourcePorts.Click

        ' Create the form instance
        Dim newWindowInstance As New ConfigSourcePorts(SourcePortList)

        ' Dim the parent window; visually show that it is not available
        '  for activity
        Me.Opacity = 0.5        ' OPACITY EXPERIMENTAL ONLY

        ' This will open the desired window
        '  but will also change focus from
        '  Parent to this new window instance.
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting; visually show
        '  that it is now active.
        Me.Opacity = 1.0        ' OPACITY EXPERIMENTAL ONLY

        ' ===================================
        ' ===================================

        ' When the user finishes with that window, determine if the source port list needs to be updated.
        '   When true, update the source port list
        '   When false, ignore all changes and keep the existing list as-is.
        If (newWindowInstance.updateSourcePortList = True) Then
            ' Update the source port list
            SourcePortList = newWindowInstance.DisplayEngineList
        End If
    End Sub




    ' Configure File Menu: Configure IWAD's
    ' ------------------------------------------
    Private Sub ConfigureMenuIWADs_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuIWADs.Click
        ' Create the form instance
        Dim newWindowInstance As New ConfigIWADs(IWADList)

        ' Dim the parent window; visually show that it is not available for activity
        Me.Opacity = 0.5        ' OPACITY EXPERIMENTAL ONLY

        ' This will open the desired window by will also change
        ' focus from Parent to this new window instance
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting; visually show that it is now active.
        Me.Opacity = 1.0        ' OPACITY EXPERIEMENTAL ONLY

        ' ===================================
        ' ===================================

        ' When the user finishes with that window, determine if the IWAD list needs to be updated.
        '   When true, update the IWAD list
        '   When false, ignore all changes and keep the existing list as-is.
        If (newWindowInstance.updateIWADList = True) Then
            ' Update the source port list
            IWADList = newWindowInstance.DisplayIWADList
        End If
    End Sub




    ' Configure File Menu: Configure PWAD Directory Path
    ' ------------------------------------------
    Private Sub ConfigureMenuPWADsDir_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuPWADsDir.Click
        ' Create the form instance
        Dim newWindowInstance As New ConfigPWADPath(PWADPath)

        ' Dim the parent window; visually show that it is not available for activity.
        Me.Opacity = 0.5        ' OPACITY EXPERIMENTAL ONLY

        ' This will open the desired window but will
        '   also change focus from Parent to this new
        '   window instance.
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting;
        '   visually show that it is now active.
        Me.Opacity = 1.0        ' OPACITY EXPERIEMNTAL ONLY

        ' ===================================
        ' ===================================

        ' When the user finishes with that window,
        ' determine if the Then source port list needs To be updated.
        '   When true, update the PWAD Directory.
        '   When false, ignore all changes and keep the existing path.
        If (newWindowInstance.updatePWADPath = True) Then
            ' Update the PWAD Directory
            PWADPath = newWindowInstance.PWADPath
        End If
    End Sub




    ' Help File Menu: Allow the end-user to check for updates
    ' ------------------------------------------
    Private Sub HelpMenuUpdatesGitHub_Click(sender As Object, e As RoutedEventArgs)
        ' URL for updates
        Dim updateURL As String = "https://github.com/SibTiger/ZDoomStarter/releases"

        ' This will merely open a hyper-link to this project's Releases page on GitHub.
        ' NOTE: This should use the end-user's default web-browser.
        Process.Start(updateURL)
    End Sub




    ' Help File Menu: Display software information
    ' ------------------------------------------
    Private Sub HelpMenuAbout_Click(sender As Object, e As RoutedEventArgs)
        ' TODO: Implement this
    End Sub
End Class
