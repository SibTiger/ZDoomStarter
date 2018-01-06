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
        ' Declarations and Initializations
        ' ----------------------------------
        ' Create an instance of the Registry class.  We will need this to read\write to or from the registry
        Dim invokeRegistry As New WindowsRegistry()
        ' ----------------------------------

        ' Fetch the PWAD Directory
        PWADPath = invokeRegistry.ReadRegistryPWADDirectory()

        ' Fetch the Source Port list
        SourcePortList = invokeRegistry.ReadRegistrySourcePort()

        ' Fetch the IWAD list
        IWADList = invokeRegistry.ReadRegistryGameData()
    End Sub




    ' Window Exit
    ' ------------------------------------------
    ' This function will automatically be called once the main program is terminating.
    Private Sub Window_Exit()
        ' Declarations and Initializations
        ' ----------------------------------
        ' Create an instance of the Registry class.  We will need this to read\write to or from the registry
        Dim invokeRegistry As New WindowsRegistry()
        ' ----------------------------------

        ' Save the user's settings to the Windows Registry
        invokeRegistry.WriteRegistry(PWADPath, SourcePortList, IWADList)
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




    ' File Menu: Reset User's settings to default
    ' ------------------------------------------
    Private Sub FileMenuThrashSettings_Click(sender As Object, e As RoutedEventArgs)
        ' Declarations and Initializations
        ' ----------------------------------
        ' Create an instance of the Registry class.  We will need this to read\write to or from the registry
        Dim invokeRegistry As New WindowsRegistry()
        ' ----------------------------------

        ' Thrash the Registry
        invokeRegistry.DeleteRegistryKeys()

        ' Thrash the Source Port List
        SourcePortList.Clear()

        ' Thrash the IWAD List
        IWADList.Clear()

        ' Clear the PWAD Directory
        PWADPath = ""

        ' Tell the user that the settings has been reset.
        MsgBox("Default settings has been applied!")
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




    ' Help File Menu: Allow the end-user to view the the ZDoom Wiki (Command Line Documentation)
    ' ------------------------------------------
    Private Sub HelpMenuWikiZDoom_Click(sender As Object, e As RoutedEventArgs)
        ' URL for updates
        Dim issueURL As String = "https://zdoom.org/wiki/Command_line_parameters"

        ' This will merely open a hyper-link to this project's Issues page on GitHub.
        ' NOTE: This should use the end-user's default web-browser.
        Process.Start(issueURL)
    End Sub




    ' Help File Menu: Allow the end-user to report an issue
    ' ------------------------------------------
    Private Sub HelpMenuReportIssueGitHub_Click(sender As Object, e As RoutedEventArgs)
        ' URL for updates
        Dim issueURL As String = "https://github.com/SibTiger/ZDoomStarter/issues"

        ' This will merely open a hyper-link to this project's Issues page on GitHub.
        ' NOTE: This should use the end-user's default web-browser.
        Process.Start(issueURL)
    End Sub




    ' Help File Menu: Allow the end-user to view the Wiki page
    ' ------------------------------------------
    Private Sub HelpMenuWikiGitHub_Click(sender As Object, e As RoutedEventArgs)
        ' URL for updates
        Dim issueURL As String = "https://github.com/SibTiger/ZDoomStarter/wiki"

        ' This will merely open a hyper-link to this project's Wiki page on GitHub.
        ' NOTE: This should use the end-user's default web-browser.
        Process.Start(issueURL)
    End Sub




    ' Help File Menu: Display software information
    ' ------------------------------------------
    Private Sub HelpMenuAbout_Click(sender As Object, e As RoutedEventArgs)
        ' Create the form instance
        Dim newWindowInstance As New AboutProduct()

        ' Dim the parent window; visually show that it is not available for activity.
        Me.Opacity = 0.5        ' OPACITY EXPERIMENTAL ONLY

        ' This will open the desired window but will
        '   also change focus from Parent to this new
        '   window instance.
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting;
        '   visually show that it is now active.
        Me.Opacity = 1.0        ' OPACITY EXPERIEMNTAL ONLY
    End Sub

    Private Sub ComboBoxSourcePort_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub


    Private Sub ComboBoxSourcePort_Loaded()

    End Sub

    Private Sub ComboBoxIWAD_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub


    Private Sub ComboBoxIWAD_Loaded()

    End Sub


    Private Sub ComboBoxSkilLevel_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub


    Private Sub ComboBoxSkillLevel_Loaded()

    End Sub


    Private Sub ViewListPWAD_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub CheckBoxFastMonsters_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CheckBoxMonstersRespawn_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CheckBoxDeathmatch_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CheckBoxAVG_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CheckBoxNoMusic_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CheckBoxNoSFX_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CheckBoxNoMultimedia_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CheckBoxUseOldStartUp_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub ButtonClear_Clicked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub ButtonLaunch_Clicked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub ButtonAddPWAD_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub ButtonRemovePWAD_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
