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

    ' ----

    ' REGISTRY VARIABLES
    ' This will hold the Registry keys for this program.
    ' This can be useful for saving, loading, or thrashing the program's registry settings
    ' *******************
    ' When saving values from arrays (or lists), it is critical that the Registry key's are fully
    ' consistent.  If the keys are not managed sufficiently, then the data will NOT properly load
    ' when the program is executed at the next session.  Thus, the standard here will be the following:
    ' Source Port: EngineID + index number + variable name
    '   Example: EngineID + 4 + Name
    ' IWAD: GameDataID + index number + variable name
    '   Example: GameDataID + 8 + Name
    ' *******************
    Private regkeyProgramRoot As String =
            "Software\" +
            My.Application.Info.ProductName     ' Program Root within the System Registry.
    Private regKeySourcePort As String =
            regkeyProgramRoot + "\\Source Port" ' Source Port SubKey within the System Registry.
    Private regKeyIWAD As String =
            regkeyProgramRoot + "\\IWAD"        ' IWAD SubKey within the System Registry.
    Private Const engineKeyName As String = "EngineID"  ' Common key name for the Source Ports; useful for Lists or Arrays.
    Private Const iwadKeyName As String = "GameDataID"  ' Common key name for the IWAD game data; useful for Lists or Arrays.
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
        ReadRegistryKeys()
    End Sub




    ' Window Exit
    ' ------------------------------------------
    ' This function will automatically be called once the main program is terminating.
    Private Sub Window_Exit()
        ' Save the user's settings to the Windows Registry
        SaveRegistryKeys()
    End Sub




    ' Read Windows Registry Keys
    ' ------------------------------------------
    ' This function is dedicated to retrieving the user's previously saved
    ' settings And store them in this program's environment.
    Private Sub ReadRegistryKeys()

    End Sub




    ' Save Windows Registry Keys
    ' ------------------------------------------
    ' This function is dedicated to saving the user's current settings into the
    ' Windows Registry for later use.
    Private Sub SaveRegistryKeys()
        ' Declarations and Initialization
        ' --------------------------------
        Dim indexCounter As Int32 = 0       ' This will be used as a counter when processing through a list or an array.
        ' --------------------------------


        ' Assure that the subkeys exists
        My.Computer.Registry.CurrentUser.CreateSubKey(regkeyProgramRoot)
        My.Computer.Registry.CurrentUser.CreateSubKey(regKeySourcePort)
        My.Computer.Registry.CurrentUser.CreateSubKey(regKeyIWAD)

        ' Provide the program version that last wrote to the Registry.
        My.Computer.Registry.CurrentUser.OpenSubKey(regkeyProgramRoot, True).SetValue("Version", My.Application.Info.Version)

        ' Set the firstRun; this will signify rather or not there is any user saved presets.
        My.Computer.Registry.CurrentUser.OpenSubKey(regkeyProgramRoot, True).SetValue("FirstRun", "false")

        ' Store the PWAD Directory
        My.Computer.Registry.CurrentUser.OpenSubKey(regkeyProgramRoot, True).SetValue("PWADPath", PWADPath)

        ' ----

        ' SOURCE PORT
        ' **************************************
        ' First, record the size of the list for proper loading during the next load.
        My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, True).SetValue("Size", SourcePortList.Count)

        If (SourcePortList.Count > 0) Then
            ' If there exists Source Port entries, then we will record each engine and the information.

            ' Mirror the data to the registry
            For Each i In SourcePortList
                ' NOTE: indexCounter must be casted as a string as it represents an integer datatype, without casting (or translating) to a String - it will not properly mirror the data to the registry (or in my experience, hang).  Thus, we will use the CStr() function to do this.

                ' MIRROR: Nice Name
                My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, True).SetValue(engineKeyName + CStr(indexCounter) + "NiceName", i.NiceName)

                ' MIRROR: Custom Notes
                My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, True).SetValue(engineKeyName + CStr(indexCounter) + "CustomNotes", i.CustomNotes)

                ' MIRROR: Absolute Path
                My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, True).SetValue(engineKeyName + CStr(indexCounter) + "AbsolutePath", i.AbsolutePath)

                ' Update the index counter for the next iteration (if any)
                indexCounter += 1
            Next

            ' Reset the indexCounter to zero
            indexCounter = 0
        End If

        ' IWAD GAME DATA
        ' **************************************
        ' First, record the size of the list for proper loading during the next load.
        My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, True).SetValue("Size", IWADList.Count)

        If (IWADList.Count > 0) Then
            ' If there exists IWAD entries, then we will record each engine and the information.

            ' Mirror the data to the registry
            For Each i In IWADList
                ' NOTE: indexCounter must be casted as a string as it represents an integer datatype, without casting (or translating) to a String - it will not properly mirror the data to the registry (or in my experience, hang).  Thus, we will use the CStr() function to do this.

                ' MIRROR: Nice Name
                My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, True).SetValue(iwadKeyName + CStr(indexCounter) + "NiceName", i.NiceName)

                ' MIRROR: Custom Notes
                My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, True).SetValue(iwadKeyName + CStr(indexCounter) + "CustomNotes", i.CustomNotes)

                ' MIRROR: Absolute Path
                My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, True).SetValue(iwadKeyName + CStr(indexCounter) + "AbsolutePath", i.AbsolutePath)

                ' Update the index counter for the next iteration (if any)
                indexCounter += 1
            Next

            ' Reset the indexCounter to zero
            indexCounter = 0
        End If

        MsgBox("Save complete!")
    End Sub




    ' Delete Windows Registry Keys
    ' ------------------------------------------
    ' This function is dedicated to deleting the User's previously stored settings
    ' in the Windows Registry.  When doing this, all settings will be reset to
    ' their Default values.
    Private Sub DeleteRegistryKeys()
        ' Delete the SubKey.  With deleting the SubKey, all other entries inside are deleted as well.
        My.Computer.Registry.CurrentUser.DeleteSubKey("Software\ZDoom Starter")
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
End Class
