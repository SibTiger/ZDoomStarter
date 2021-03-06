﻿' Main Window [UI]
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

    ' This list will hold the basic Skill Level used in Doom.  Understandably this particular
    ' skill level list is not really valid with Heretic or Hexen, but it is merely impossible
    ' to determine the skill level dynamically as other PWADs can merely alter them in the
    ' (Z)MAPINFO or GAMEINFO ZDoom lump file.  As such, we will just stay to the basic and
    ' commonly known skill level list.
    Public Property SkillLevelList As New List(Of String)(New String() {"I'm too young to die",
                                                                        "Hey, Not too rough",
                                                                        "Hurt me plenty",
                                                                        "Ultra-Violence",
                                                                        "Nightmare!"})

    ' ----

    ' This list will hold PWADs that the user adds into the PWAD load list.
    Private PWADList As New List(Of PWAD)

    ' ----

    ' This variable, when used, determines that an item from a list within the UI
    ' environment was never set or selected.  This is mainly useful when refreshing
    ' a UI Component.
    Private Const selectItemNotAvailable As Int32 = -1

    ' ----

    ' PWADList Index that was selected from the ListView.  This is mainly useful for removing
    ' the requested PWAD off the List.
    Private selectedPWADListItem As Int32 = selectItemNotAvailable

    ' ----

    ' Selected Source Port from the Combo Box.  This is mainly useful for finding the engine
    ' that the user has requested to use.
    Private selectedSourcePortID As Int32 = selectItemNotAvailable

    ' ----

    ' Selected IWAD from the Combo Box.  This is mainly useful for finding the IWAD that the
    ' user has requested to use.
    Private selectedIWADID As Int32 = selectItemNotAvailable

    ' ----

    ' Selected Skill Level from the Combo Box.  This is mainly useful for finding the Skill Level
    ' that the user has requested to play.
    Private selectedSkillLevelID As Int32 = selectItemNotAvailable
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

        ' Populate the Source Port ComboBox
        RenderComboBoxSourcePort()

        ' Populate the IWAD ComboBox
        RenderComboBoxIWAD()

        ' Populate the Skill Level ComboBox
        RenderComboBoxSkillLevel()
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




    ' UI ELEMENTS: File Menu
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
        ' Declarations and Initializations
        ' ----------------------------------
        ' Create the form instance
        Dim newWindowInstance As New ConfigSourcePorts(SourcePortList)
        ' ----------------------------------

        ' Dim the parent window; visually show that it is not available
        '  for activity
        Me.Opacity = 0.5

        ' This will open the desired window
        '  but will also change focus from
        '  Parent to this new window instance.
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting; visually show
        '  that it is now active.
        Me.Opacity = 1.0

        ' ===================================
        ' ===================================

        ' When the user finishes with that window, determine if the source port list needs to be updated.
        '   When true, update the source port list
        '   When false, ignore all changes and keep the existing list as-is.
        If (newWindowInstance.updateSourcePortList = True) Then
            ' Update the source port list
            SourcePortList = newWindowInstance.DisplayEngineList

            ' Refresh the source port combo box
            RefreshComboBoxSourcePort()
        End If
    End Sub




    ' Configure File Menu: Configure IWAD's
    ' ------------------------------------------
    Private Sub ConfigureMenuIWADs_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuIWADs.Click
        ' Declarations and Initializations
        ' ----------------------------------
        ' Create the form instance
        Dim newWindowInstance As New ConfigIWADs(IWADList)
        ' ----------------------------------

        ' Dim the parent window; visually show that it is not available for activity
        Me.Opacity = 0.5

        ' This will open the desired window by will also change
        ' focus from Parent to this new window instance
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting; visually show that it is now active.
        Me.Opacity = 1.0

        ' ===================================
        ' ===================================

        ' When the user finishes with that window, determine if the IWAD list needs to be updated.
        '   When true, update the IWAD list
        '   When false, ignore all changes and keep the existing list as-is.
        If (newWindowInstance.updateIWADList = True) Then
            ' Update the IWAD list
            IWADList = newWindowInstance.DisplayIWADList

            ' Refresh the IWAD Combo box
            RefreshComboBoxIWAD()
        End If
    End Sub




    ' Configure File Menu: Configure PWAD Directory Path
    ' ------------------------------------------
    Private Sub ConfigureMenuPWADsDir_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuPWADsDir.Click
        ' Create the form instance
        Dim newWindowInstance As New ConfigPWADPath(PWADPath)

        ' Dim the parent window; visually show that it is not available for activity.
        Me.Opacity = 0.5

        ' This will open the desired window but will
        '   also change focus from Parent to this new
        '   window instance.
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting;
        '   visually show that it is now active.
        Me.Opacity = 1.0

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

        ' Refresh the Source Port Combo Box
        RefreshComboBoxSourcePort()

        ' Refresh the IWAD Combo Box
        RefreshComboBoxIWAD()

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




    ' Help File Menu: Allow the end-user to view the ZDoom Wiki (Command Line Documentation)
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
        Me.Opacity = 0.5

        ' This will open the desired window but will
        '   also change focus from Parent to this new
        '   window instance.
        newWindowInstance.ShowDialog()

        ' Restore the parent window's opacity setting;
        '   visually show that it is now active.
        Me.Opacity = 1.0
    End Sub




    ' UI ELEMENTS: Game Play Environment
    ' =================================================
    ' =================================================
    ' =================================================




    ' Render Source Port Combo Box
    ' ------------------------------------------
    ' When called, this function will populate the Source Port Combo Box using the
    ' Source Port list.
    ' Because it seems as if I can not get the index when the user _selects_ an item
    ' from the ComboBox but the 'string', we will formulate a string to be unique.
    ' If we only use the engine name (or NiceName), it is possible that the list
    ' might contain duplicates but the binaries could vary in versions.  For example
    ' Zandronum 1.0 and Zandronum 2.0.  To avoid this conflict, we will artificially add
    ' the following: an index number (natural number, user friendly), engine name, and custom notes.
    ' The index number will assure we use the right engine that was selected from the list, the
    ' custom notes will only be useful for the end-user as a means of knowing what they selected.
    Private Sub RenderComboBoxSourcePort()
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 1       ' Artificial index key
        ' ----------------------------------

        ' Scan through the Source Port list and add each entry into the Combo Box UI Component.
        For Each i As SourcePort In SourcePortList
            ' Add the item into the combo box.
            ComboBoxSourcePort.Items.Add(CStr(indexCounter) + ") " + i.NiceName + " - " + i.CustomNotes)

            ' Update the index key.
            indexCounter += 1
        Next
    End Sub




    ' Clear Source Port Combo Box
    ' ------------------------------------------
    ' This function will clear all entries within the ComboBox.
    ' This can be useful when refreshing the list.
    Private Sub ClearComboBoxSourcePort()
        ComboBoxSourcePort.Items.Clear()
    End Sub




    ' Refresh Source Port Combo Box
    ' ------------------------------------------
    ' When called, this function will refresh the Source Port Combo Box
    ' by utilizing the Clear and Render functions.  This is ideal if in
    ' case the Source Port list has been updated.
    Private Sub RefreshComboBoxSourcePort()
        ClearComboBoxSourcePort()       ' Clear the combo box
        RenderComboBoxSourcePort()      ' Regenerate the entries within the combo box.
        selectedSourcePortID =
            selectItemNotAvailable      ' Revert the cached selection to the default error number.
    End Sub




    ' ComboBox Source Port [EVENT: SelectionChanged]
    ' ------------------------------------------
    ' When the user selects an entry from the ComboBox, we can not easily get
    ' the index from that selection - but only the string.  With that, we used
    ' a work around to circumvent this functionality.  The work around is as
    ' follows: Index Key (natural numbering scheme) + engine name + notes
    ' With that information, we will use that string and test the values against
    ' the Source Port list.
    ' NOTE: The index key must be translated to the Whole Number scheme.
    Private Sub ComboBoxSourcePort_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 1       ' Index Key; Natural number
        ' ----------------------------------

        ' Scan through all entries in the Source Port list
        For Each i As SourcePort In SourcePortList
            ' Find that one entry that matches what the user selected against the source port list.
            If (ComboBoxSourcePort.SelectedItem = (CStr(indexCounter) + ") " + i.NiceName + " - " + i.CustomNotes)) Then
                ' Found the entry within the list.  Capture the actual Index Key (whole number)
                ' as we will need this for later.
                selectedSourcePortID = indexCounter - 1
            End If

            indexCounter += 1       ' Increment the index key.
        Next
    End Sub




    ' Render IWAD Combo Box
    ' ------------------------------------------
    ' When called, this function will populate the IWAD Combo Box using the IWAD list.
    ' Because it seems as if I can not get the index when the user _selects_ an item
    ' from the ComboBox but the 'string', we will formulate a string to be unique.
    ' If we only use the file name (or NiceName), it is possible that the list
    ' might contain duplicates but the files could vary in versions.  For example
    ' Knee Deep in ZDoom 1.0 and Knee Deep in ZDoom 1.1.  To avoid this conflict, we will artificially add
    ' the following: an index number (natural number, user friendly), file name, and custom notes.
    ' The index number will assure we use the right file that was selected from the list, the
    ' custom notes will only be useful for the end-user as a means of knowing what they selected.
    Private Sub RenderComboBoxIWAD()
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 1       ' Artificial index key
        ' ----------------------------------

        ' Scan through the IWAD list and add each entry into the Combo Box UI Component.
        For Each i As IWAD In IWADList
            ' Add the item into the combo box.
            ComboBoxIWAD.Items.Add(CStr(indexCounter) + ") " + i.NiceName + " - " + i.CustomNotes)

            ' Update the index key
            indexCounter += 1
        Next
    End Sub




    ' IWAD Combo Box [EVENT: Selection Changed]
    ' ------------------------------------------
    ' When the user selects an entry from the ComboBox, we can not easily get
    ' the index from that selection - but only the string.  With that, we used
    ' a work around to circumvent this functionality.  The work around is as
    ' follows: Index Key (natural numbering scheme) + IWAD name + notes
    ' With that information, we will use that string and test the values against
    ' the IWAD list.
    ' NOTE: The index key must be translated to the Whole Number scheme.
    Private Sub ComboBoxIWAD_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 1       ' Index Key; Natural number
        ' ----------------------------------

        ' Scan through all entries in the IWAD list
        For Each i As IWAD In IWADList
            ' Find that one entry that matches what the user selected against the IWAD list.
            If (ComboBoxIWAD.SelectedItem = (CStr(indexCounter) + ") " + i.NiceName + " - " + i.CustomNotes)) Then
                ' Found the entry within the list.  Capture the actual Index Key (whole number)
                ' as we will need this for later.
                selectedIWADID = indexCounter - 1
            End If

            indexCounter += 1       ' Increment the index key
        Next
    End Sub




    ' Refresh IWAD Combo Box
    ' ------------------------------------------
    ' When called, this function will refresh the IWAD Combo Box
    ' by utilizing the Clear and Render functions.  This is ideal if in
    ' case the IWAD list has been updated.
    Private Sub RefreshComboBoxIWAD()
        ClearComboBoxIWAD()         ' Clear the combo box
        RenderComboBoxIWAD()        ' Regenerate the entries within the combo box
        selectedIWADID =
            selectItemNotAvailable  ' Revert the cached selection to the default error number.
    End Sub




    ' Clear IWAD Combo Box
    ' ------------------------------------------
    ' This function will clear all entries within the ComboBox.
    ' This can be useful when refreshing the list.
    Private Sub ClearComboBoxIWAD()
        ComboBoxIWAD.Items.Clear()
    End Sub




    ' Render Skill Level Combo Box
    ' ------------------------------------------
    ' When called, this function will populate the Skill Level Combo Box.
    ' To be consistent, we will follow the same convention with the other
    ' combo boxes.  Thus, index key + Skill Level
    Private Sub RenderComboBoxSkillLevel()
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 1       ' Counter
        ' ----------------------------------

        ' Scan through each entry within the skill level list.
        For Each i As String In SkillLevelList
            ' Add the entry into the combo list.
            ComboBoxSkillLevel.Items.Add(CStr(indexCounter) + ") " + i)

            ' Increment the counter
            indexCounter += 1
        Next
    End Sub




    ' Skill Level Combo Box [EVENT: SelectionChanged]
    ' ------------------------------------------
    ' When the event raises, this function will find the desired skill level and save the
    ' index key for use later on.
    Private Sub ComboBoxSkillLevel_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 1       ' Counter
        ' ----------------------------------

        ' Scan through each entry within the Skill Level list
        For Each i As String In SkillLevelList
            ' Find the selected skill level from the list
            If (ComboBoxSkillLevel.SelectedItem = (CStr(indexCounter) + ") " + i)) Then
                ' Found the entry, save the actual index key.
                selectedSkillLevelID = indexCounter
            End If

            indexCounter += 1       ' Increment the index key.
        Next
    End Sub




    ' PWAD View List [EVENT: Selection Changed]
    ' ------------------------------------------
    ' When this event raises, we will automatically cache the entry index that was selected by the user.
    ' We may need this index when the user removes an item from the PWAD list.
    Private Sub ViewListPWAD_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        selectedPWADListItem = ListViewPWADs.SelectedIndex
    End Sub




    ' Clear PWAD List View
    ' ------------------------------------------
    ' When this function is called, this function will clear all entries that was made in the ListView
    ' UI Component.
    Private Sub ClearViewListPWAD()
        ListViewPWADs.Items.Clear()
    End Sub




    ' Render PWAD ViewList
    ' ------------------------------------------
    ' This function will regenerate the PWAD ViewList with the latest changes in the PWAD list.
    Private Sub RenderViewListPWAD()
        ' Scan through the PWAD list and add them to the ViewList UI Component
        For Each i As PWAD In PWADList
            ListViewPWADs.Items.Add(i.NiceName)
        Next
    End Sub




    ' Update PWAD View List
    ' ------------------------------------------
    ' When called, this will refresh the ViewList UI component.
    ' This is useful when updating the ViewList with the latest changes against the list.
    Private Sub UpdateViewListPWAD()
        ClearViewListPWAD()         ' Thrash all entries within the UI component
        RenderViewListPWAD()        ' Add in the new PWAD List into the UI component
        selectedPWADListItem =
            selectItemNotAvailable  ' Revert the selected entry to its default value - not selected value.
    End Sub




    ' Button Add PWAD [EVENT: Click]
    ' ------------------------------------------
    ' When the user requests to add a new PWAD to the list, this function will be called.
    ' This function will allow the user to quickly add new PWADs to the PWAD list and will
    ' append the changes to the PWAD List View UI component.
    Private Sub ButtonAddPWAD_Click(sender As Object, e As RoutedEventArgs)
        ' Declarations and Initializations
        ' ----------------------------------
        Dim browseFileDialog As New _
            Microsoft.Win32.OpenFileDialog()    ' Create an instance of the OpenFileDialog
        ' ----------------------------------

        ' If the user provided a path to their PWAD directory, then we will simply use that.
        ' This is only for convenience sakes.
        If ((Not (PWADPath = Nothing)) And (Not (PWADPath = ""))) Then
            browseFileDialog.InitialDirectory = PWADPath
        End If

        If (browseFileDialog.ShowDialog()) Then
            ' Successful result; immediately store the information directly into the PWAD list.
            PWADList.Add(New PWAD() With {
                .AbsolutePath = browseFileDialog.FileName,
                .NiceName = browseFileDialog.SafeFileName
                })
        End If

        ' Update the PWAD View List UI component
        UpdateViewListPWAD()
    End Sub




    ' Button Remove PWAD [EVENT: Click]
    ' ------------------------------------------
    ' This function will be called when the user requests a selected PWAD to be removed from the list.
    ' This requires the user to first select an entry from the PWAD ViewList UI component and then from there
    ' it is possible to remove the PWAD from the list.  Once removed, the view list will be refreshed.
    Private Sub ButtonRemovePWAD_Click(sender As Object, e As RoutedEventArgs)
        ' Make sure that the user actually clicked an item from the list.
        If (selectedPWADListItem > selectItemNotAvailable) Then
            ' Remove the entry from the list.
            PWADList.RemoveAt(selectedPWADListItem)

            ' Update the ViewList UI Component
            UpdateViewListPWAD()
        End If
    End Sub




    ' Clear PWAD List
    ' ------------------------------------------
    ' This function, when called, will thrash the entire PWAD List.
    Private Sub ClearPWADList()
        PWADList.Clear()
    End Sub




    ' Button Clear [EVENT: Click]
    ' ------------------------------------------
    ' When the user requests for all fields to be cleared, this function will take place.
    ' This function will revert all selected items and other UI components to default values,
    ' provide a clean-slate.  Their user settings are not touched in this process, only the game
    ' play settings are altered.
    Private Sub ButtonClear_Clicked(sender As Object, e As RoutedEventArgs)
        ' Reset the User-Selected indexes (mainly from Combo Boxes)
        selectedIWADID = selectItemNotAvailable         ' Revert IWAD selection
        selectedSourcePortID = selectItemNotAvailable   ' Revert Source Port selection
        selectedSkillLevelID = selectItemNotAvailable   ' Revert Skill Level selection

        ' Clear the text from the Combo Box UI components.  This will show
        ' that nothing was selected yet.
        ComboBoxSkillLevel.SelectedValue = ""   ' Skill Level
        ComboBoxIWAD.SelectedValue = ""         ' IWAD
        ComboBoxSourcePort.SelectedValue = ""   ' Source Port

        ' Clear the PWAD List
        ClearPWADList()         ' Thrash the PWAD List
        UpdateViewListPWAD()    ' Refresh the PWAD ViewList UI Component

        ' Clear the Custom Parameter Text Box UI component
        TextBoxCustomParameters.Text = Nothing

        ' Uncheck all available flags
        CheckBoxFastMonsters.IsChecked = False      ' Fast Monsters
        CheckBoxMonstersRespawn.IsChecked = False   ' Monsters Respawn
        CheckBoxDeathmatch.IsChecked = False        ' Deathmatch
        CheckBoxAVG.IsChecked = False               ' Austin Virtual Gaming
        CheckBoxNoMusic.IsChecked = False           ' No Music
        CheckBoxNoSFX.IsChecked = False             ' No Sound Effects
        CheckBoxNoMultimedia.IsChecked = False      ' No Music nor Sound Effects
        CheckBoxUseOldStartup.IsChecked = False     ' No Startup Screen (Use old Doom startup)
    End Sub




    ' Button Launch [EVENT: Click]
    ' ------------------------------------------
    ' When the user requests to start their game, this function will execute.
    ' This function will assure that all information is ready from the user and
    ' take all the available information - and transform it so the engine can
    ' understand the arguments.
    ' NOTE: Not all source ports are a like, but we assume that ZDoom and its
    ' children (forks) are being used and the likelihood of the settings being
    ' accepted.  If the engine does not accept a setting, then the user must act
    ' accordingly.
    Private Sub ButtonLaunch_Clicked(sender As Object, e As RoutedEventArgs)
        ' Declarations and Initializations
        ' ----------------------------------
        ' Launch Builder Instantiation
        Dim lConstructor As New LaunchBuilder(selectedSkillLevelID,
                                              selectedSourcePortID,
                                              selectedIWADID,
                                              selectItemNotAvailable,
                                              IWADList,
                                              PWADList,
                                              SourcePortList,
                                              TextBoxCustomParameters,
                                              CheckBoxFastMonsters,
                                              CheckBoxMonstersRespawn,
                                              CheckBoxDeathmatch,
                                              CheckBoxAVG,
                                              CheckBoxNoMusic,
                                              CheckBoxNoSFX,
                                              CheckBoxNoMultimedia,
                                              CheckBoxUseOldStartup)
        Dim pathBinaryFile As String            ' Holds the source port path.
        Dim pathIWADFile As String              ' Holds the IWAD path.
        Dim pwadsInclusion As String            ' Holds the entire PWAD load sequence.
        Dim customParameters As String          ' Holds the custom parameters.
        Dim gameFlags As String                 ' Holds the game play flags.
        Dim executeParameters As String         ' Holds the final parameter set that will be used with the source port.
        Dim executeFilePath As String           ' Holds the location of the Batch file that holds all the environment settings.
        ' ----------------------------------

        ' Make sure that the bare minimum requirements are fulfilled.
        If (lConstructor.LaunchBuilderRequiredFields()) Then
            ' Missing one or more required fields
            MessageBox.Show("Please be sure that the following is provided:" & vbCrLf & "    Source Port" & vbCrLf & "    IWAD" & vbCrLf & "    Skill Level",
                "Missing one or more fields",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation)
            Return  ' We are missing some information that is required, unable to continue - the user must provide the missing information.
        End If

        ' SOURCE PORT
        ' -----------
        ' Get the absolute path of the source port engine
        pathBinaryFile = lConstructor.LaunchBuilderFindExecutablePath()

        ' Test to make sure that the executable exists
        If ((pathBinaryFile Is "!ERR") Or (lConstructor.LaunchBuilderCheckFileExists(pathBinaryFile))) Then
            ' File does not exists or there was an error
            MessageBox.Show("The selected source port path or file does not exist!" & vbCrLf & "File: " + pathBinaryFile,
                            "File does not Exist!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error)
            Return  ' An error occurred, leave the function and avoid any further execution.
        End If

        ' INTERNAL WAD
        ' -----------
        ' Get the absolute path of the IWAD file
        pathIWADFile = lConstructor.LaunchBuilderFindIWADPath()

        ' Test to make sure that the executable exists
        If ((pathIWADFile Is "!ERR") Or (lConstructor.LaunchBuilderCheckFileExists(pathIWADFile))) Then
            ' File does not exists or there was an error
            MessageBox.Show("The selected IWAD path or file does not exist!" & vbCrLf & "File: " + pathIWADFile,
                            "File does not Exist!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error)
            Return  ' An error occurred, leave the function and avoid any further execution.
        End If

        ' PATCH WAD
        ' -----------
        ' Get the entire PWAD load sequence
        pwadsInclusion = lConstructor.LaunchBuilderPWADInclusion()

        ' Custom Parameters
        ' -----------
        customParameters = lConstructor.LaunchBuilderCustomParameters()

        ' Game Play Flags
        ' -----------
        gameFlags = lConstructor.LaunchBuilderGamePlayFlags()

        ' Store the concatenated parameters into one large variable
        executeParameters = lConstructor.LaunchBuilderConstructorBuilder(pathIWADFile, pwadsInclusion, selectedSkillLevelID, gameFlags, customParameters)

        ' Generate the Batch file
        executeFilePath = lConstructor.LaunchBuilderMakeBatchFile(pathBinaryFile, executeParameters)

        ' Execute the batch file
        Process.Start(executeFilePath).WaitForExit()

        ' Delete the batch file
        lConstructor.LaunchBuilderDeleteBatchFile(executeFilePath)
    End Sub
End Class
