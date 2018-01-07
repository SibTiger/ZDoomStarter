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

    ' This list will hold the basic Skill Level used in Doom.  Understandably this particular
    ' skill level list is not really valid with Heretic or Hexen, but it is merely impossible
    ' to determine the skill level dynamically as other PWADs can merely alter them in the
    ' (Z)MAPINFO or GAMEINFO ZDoom lump file.  As such, we will just stay to the basic and
    ' commonly known skill level list.
    Public Property SkillLevelList As New List(Of String)(New String() {"Hey, not too rough",
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
                selectedSkillLevelID = indexCounter - 1
            End If

            indexCounter += 1       ' Increment the index key.
        Next
    End Sub



    Private Sub ViewListPWAD_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        selectedPWADListItem = ListViewPWADs.SelectedIndex
    End Sub

    Private Sub ClearViewListPWAD()
        ListViewPWADs.Items.Clear()
    End Sub

    Private Sub RenderViewListPWAD()
        For Each i As PWAD In PWADList
            ListViewPWADs.Items.Add(i.NiceName)
        Next
    End Sub

    Private Sub UpdateViewListPWAD()
        ClearViewListPWAD()
        RenderViewListPWAD()
    End Sub

    Private Sub ButtonAddPWAD_Click(sender As Object, e As RoutedEventArgs)
        ' Declarations and Initializations
        ' ----------------------------------
        Dim browseFileDialog As New _
            Microsoft.Win32.OpenFileDialog()    ' Create an instance of the OpenFileDialog
        ' ----------------------------------

        browseFileDialog.InitialDirectory = PWADPath

        If (browseFileDialog.ShowDialog()) Then
            ' Successful result
            PWADList.Add(New PWAD() With {
                .AbsolutePath = browseFileDialog.FileName,
                .NiceName = browseFileDialog.SafeFileName
                })

        End If
        UpdateViewListPWAD()
    End Sub


    Private Sub ButtonRemovePWAD_Click(sender As Object, e As RoutedEventArgs)
        If ((Not (selectedPWADListItem = -1)) And (PWADList.Count >= selectedPWADListItem)) Then
            PWADList.RemoveAt(selectedPWADListItem)
        End If
        UpdateViewListPWAD()
    End Sub

    Private Sub ButtonClear_Clicked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub ButtonLaunch_Clicked(sender As Object, e As RoutedEventArgs)
        Dim executeCommand As New ProcessStartInfo
        'executeCommand.FileName = ""
        'executeCommand.Arguments = ""

        MsgBox(LaunchBuilder_ExecutablePath)
        MsgBox(LaunchBuilder_Arguments())

        'Process.Start(executeCommand)
    End Sub

    Private Function LaunchBuilder_ExecutablePath() As String
        Dim indexCounter As Int32 = 0

        For Each i As IWAD In IWADList
            If (selectedIWADID = indexCounter) Then
                Return i.AbsolutePath
            End If
        Next
    End Function

    Private Function LaunchBuilder_Arguments() As String
        Dim indexCounter As Int32 = 0
        Dim iwadPath As String = ""
        Dim pwadPaths As String = ""
        Dim gamePlayStyle As String = ""
        Dim arguments As String = ""

        ' Get the path of the IWAD
        For Each i As IWAD In IWADList
            If (indexCounter = selectedIWADID) Then
                iwadPath = i.AbsolutePath
            End If
            indexCounter += 1
        Next

        ' Reset the counter
        indexCounter = 0

        'Get the path of the PWADs
        If (Not (PWADList.Count = Nothing)) Then
            For Each i As PWAD In PWADList
                ' If in case path variable is empty
                If (Not (pwadPaths = Nothing)) Then
                    pwadPaths = i.AbsolutePath
                Else
                    pwadPaths = pwadPaths + ", " + i.AbsolutePath
                End If
            Next
        End If

        ' Check the check boxes
        If (CheckBoxFastMonsters.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "-fast"
            Else
                gamePlayStyle = gamePlayStyle + " -fast"
            End If
        End If

        If (CheckBoxFastMonsters.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "-nomonsters"
            Else
                gamePlayStyle = gamePlayStyle + " -nomonsters"
            End If
        End If

        If (CheckBoxDeathmatch.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "-deathmatch"
            Else
                gamePlayStyle = gamePlayStyle + " -deathmatch"
            End If
        End If

        If (CheckBoxAVG.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "-avg"
            Else
                gamePlayStyle = gamePlayStyle + " -avg"
            End If
        End If

        If (CheckBoxNoMusic.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "-nomusic"
            Else
                gamePlayStyle = gamePlayStyle + " -nomusic"
            End If
        End If

        If (CheckBoxNoSFX.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "nosfx"
            Else
                gamePlayStyle = gamePlayStyle + " -nosfx"
            End If
        End If

        If (CheckBoxNoMultimedia.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "-nosound"
            Else
                gamePlayStyle = gamePlayStyle + " -nosound"
            End If
        End If

        If (CheckBoxUseOldStartup.IsChecked = True) Then
            If (gamePlayStyle = Nothing) Then
                gamePlayStyle = "-nostartup"
            Else
                gamePlayStyle = gamePlayStyle + " -nostartup"
            End If
        End If

        arguments = "-iwad " + iwadPath

        If (Not (pwadPaths = Nothing)) Then
            arguments = arguments + " -file " + pwadPaths
        End If

        If (Not (gamePlayStyle = Nothing)) Then
            arguments = arguments + " " + gamePlayStyle
        End If

        If (Not (TextBoxCustomParameters.Text = Nothing)) Then
            arguments = arguments + " " + TextBoxCustomParameters.Text
        End If

        arguments = arguments + " -skill " + selectedSkillLevelID

        Return arguments
    End Function
End Class
