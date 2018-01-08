' Launch Builder
' ---------------------------------------
' =======================================
' This class is designed to offer flexibility with setting up
' the game environment as requested by the end user.
' ---------------------------------------




Public Class LaunchBuilder
#Region "Declarations and Initializations"
    Private internalWADList As New List(Of IWAD)        ' IWAD List<IWAD>
    Private patchWADList As New List(Of PWAD)           ' PWAD List<PWAD>
    Private sourcePortList As New List(Of SourcePort)   ' Source Port List<SourcePort>
    Private selectItemNotAvailable As Int32             ' The default value when nothing was selected yet.
    Private selectedSkillLevelID As Int32               ' Requested Skill level
    Private selectedSourcePortID As Int32               ' Requested source port
    Private selectedIWADID As Int32                     ' Requested IWAD
    ' ----
    Private TextBoxCustomParameters As TextBox          ' [UI] Custom Parameters
    Private CheckBoxFastMonsters As CheckBox            ' [UI] Fast Monsters
    Private CheckBoxMonstersRespawn As CheckBox         ' [UI] Monsters Respawn
    Private CheckBoxDeathmatch As CheckBox              ' [UI] Deathmatch
    Private CheckBoxAVG As CheckBox                     ' [UI] Austin Virtual Gaming
    Private CheckBoxNoMusic As CheckBox                 ' [UI] No Music
    Private CheckBoxNoSFX As CheckBox                   ' [UI] No Sound Effects
    Private CheckBoxNoMultimedia As CheckBox            ' [UI] No Multimedia
    Private CheckBoxUseOldStartup As CheckBox           ' [UI] Use Old Startup
#End Region



    ' Constructor Window
    ' ------------------------------------------
    ' This is the constructor for this class.
    ' This is ideal for transferring data from one end to another.
    ' -----------------------
    ' Parameters:
    '   skillLevelID [Int32]
    '   sourcePortID [Int32]
    '   internalWADID [Int32]
    '   nothingSelectedID [Int32]
    '   IWADList [List<IWAD>]
    '   PWADList [List<PWAD>]
    '   SourcePortList [List<SourcePort>]
    '   customParameters [UI: TextBox]
    '   flagFastMonsters [UI: CheckBox]
    '   flagMonstersRespawn [UI: CheckBox]
    '   flagDeathmatch [UI: CheckBox]
    '   flagAVG [UI: CheckBox]
    '   flagNoMusic [UI: CheckBox]
    '   flagNoSFX [UI: CheckBox]
    '   flagNoMultimedia [UI: CheckBox]
    '   flagOldStartup [UI: CheckBox]
    Public Sub New(skillLevelID As Int32,
                   sourcePortID As Int32,
                   internalWADID As Int32,
                   nothingSelectedID As Int32,
                   IWADList As List(Of IWAD),
                   PWADList As List(Of PWAD),
                   SourcePortList As List(Of SourcePort),
                   customParameters As TextBox,
                   flagFastMonsters As CheckBox,
                   flagMonstersRespawn As CheckBox,
                   flagDeathmatch As CheckBox,
                   flagAVG As CheckBox,
                   flagNoMusic As CheckBox,
                   flagNoSFX As CheckBox,
                   flagNoMultimedia As CheckBox,
                   flagOldStartup As CheckBox)
        internalWADList = New List(Of IWAD)(IWADList)
        patchWADList = New List(Of PWAD)(PWADList)
        Me.sourcePortList = New List(Of SourcePort)(SourcePortList)
        selectItemNotAvailable = nothingSelectedID
        selectedSkillLevelID = skillLevelID
        selectedSourcePortID = sourcePortID
        selectedIWADID = internalWADID
        ' ----
        TextBoxCustomParameters = customParameters
        CheckBoxFastMonsters = flagFastMonsters
        CheckBoxMonstersRespawn = flagMonstersRespawn
        CheckBoxDeathmatch = flagDeathmatch
        CheckBoxAVG = flagAVG
        CheckBoxNoMusic = flagNoMusic
        CheckBoxNoSFX = flagNoSFX
        CheckBoxNoMultimedia = flagNoMultimedia
        CheckBoxUseOldStartup = flagOldStartup
    End Sub




    ' Launch Builder: Delete Batch File
    ' ------------------------------------------
    ' This function will delete the batch file that was used for generating the entire request.
    ' -----------------------
    ' Parameters:
    '   fileAbsPath [String]
    '       Absolute file path of the batch script.
    Public Sub LaunchBuilderDeleteBatchFile(fileAbsPath As String)
        ' Delete the file if possible
        If (System.IO.File.Exists(fileAbsPath)) Then
            ' File exists, delete it.
            My.Computer.FileSystem.DeleteFile(fileAbsPath)
        End If
    End Sub




    ' Launch Builder: Make Batch File
    ' ------------------------------------------
    ' This function will generate a batch file that will execute the commands that the user requested.
    ' This is merely a work around as I am not particularly sure as to how to get the commands to work
    ' correctly in VB.net.  I really dislike this approach and this should be axed once I can figure this out.
    ' -----------------------
    ' Parameters:
    '   executable [String]
    '       The executable that is to be executed
    '   parameters [String]
    '       The executable parameters
    Public Function LaunchBuilderMakeBatchFile(executable As String, parameters As String) As String
        ' Declarations and Initializations
        ' ----------------------------------
        ' Instantiate StreamWriter
        Dim file As System.IO.StreamWriter
        ' Compile the executable and the parameters into one statement
        Dim exeCommand As String = executable + " " + parameters
        ' Batch file name, something unique that shouldn't conflict with the end-user's data.
        Dim filePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop +
            "\__" + My.Application.Info.ProductName + "_loadgame.bat"
        ' Enforce the UTF8 and disallow the BOM.  Without this setup: BOM is thrown in the text file - cmd is not able to understand it, and
        ' all text defaults to latest Unicode standards.  With what we are working with - I don't know if the engines remotely support it.
        Dim utf8WithoutByteOrderMark As New System.Text.UTF8Encoding(False)
        ' ----------------------------------

        ' Initialize the stream to the desired file
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, False, utf8WithoutByteOrderMark)

        ' Write the command to the batch script file
        file.WriteLine(exeCommand)

        ' Close the stream
        file.Close()

        ' Return the absolute file path of the Batch script
        Return filePath
    End Function




    ' Launch Builder: Constructor Builder
    ' ------------------------------------------
    ' This function is designed to catenate all arguments provided - into one single string.
    ' But the string must be formatted in such a way that ZDoom can understand it.  To do this,
    ' we will use the ZDoom's CommandLine parameters provided in the link:
    '   https://zdoom.org/wiki/Command_line_parameters
    ' -----------------------
    ' Parameters
    '   fileIWAD [String]
    '       The absolute path of the IWAD file itself.
    '   filePWADs [String]
    '       The absolute paths of the PWAD files (if any).
    '   skillLevel [Integer]
    '       The skill level number
    '   gameFlags [String]
    '       The game play flags
    '   customParameters [String]
    '       Customized parameters that the end-user provided.
    ' -----------------------
    ' Output
    '   Executable Argument [String]
    '       Concatenated string that contains all values that should
    '       be understandable With ZDoom Or its child forks.
    Public Function LaunchBuilderConstructorBuilder(fileIWAD As String,
                                                            filePWADs As String,
                                                            skillLevel As Integer,
                                                            gameFlags As String,
                                                            customParameters As String) As String
        ' Declarations and Initializations
        ' ----------------------------------
        Dim catenatedString As String   ' This will hold all of the
        ' ----------------------------------

        ' SETUP: Internal WAD
        ' ========================
        catenatedString = "-iwad " + Chr(34) + fileIWAD + Chr(34)

        ' SETUP: Skill Level
        ' ========================
        catenatedString = catenatedString + " -skill " + CStr(skillLevel)

        ' SETUP: Patch WAD
        ' ========================
        ' Check if any PWADs were included, if not - proceed without.
        If (Not (filePWADs = Nothing)) Then
            ' Include the requested PWADs
            catenatedString = catenatedString + " -file " + filePWADs
        End If

        ' SETUP: Game Flags
        ' ========================
        ' Check if any flags were set, if not - proceed without.
        If (Not (gameFlags = Nothing)) Then
            ' Include the requested flags
            catenatedString = catenatedString + " " + gameFlags
        End If

        ' SETUP: Custom Parameters
        ' ========================
        ' Check if the user provided any parameters, if not - proceed without.
        If (Not (customParameters = Nothing)) Then
            ' Include the requested parameters
            catenatedString = catenatedString + " " + customParameters
        End If

        ' Now finally return the constructed string
        Return catenatedString
    End Function




    ' Launch Builder: Required Fields
    ' ------------------------------------------
    ' This function will make sure that the all of the required fields are populated.
    ' At a bare minimum, we must assure that the fields are populated:
    '   > Source port
    '   > IWAD
    '   > Skill Level
    ' All other fields are not required but acceptable for use.
    ' -----------------------
    ' Output
    '   State [Bool]
    '       True = One or more fields are not complete.
    '       False = All required fields are provided
    Public Function LaunchBuilderRequiredFields() As Boolean
        If ((selectedSkillLevelID = selectItemNotAvailable) Or
                (selectedSourcePortID = selectItemNotAvailable) Or
                (selectedIWADID = selectItemNotAvailable)) Then
            ' One or more required fields are not set.
            Return True
        Else
            ' All required fields are populated
            Return False
        End If
    End Function




    ' Launch Builder: Find Executable Path
    ' ------------------------------------------
    ' This function is designed to find the absolute path
    ' of the executable binary file from the Source Port list
    ' and return that to the calling function.
    ' This seems redundant to me as I could have swore I could just
    ' invoke the list with an index key, but I guess that is not possible?
    ' -----------------------
    ' Output
    '   File Path [String]
    '       The absolute file path of the executable file
    '       that was selected.
    '       NOTE: '!ERR' will be returned if an error occurs
    '               mainly when the scan fails.
    Public Function LaunchBuilderFindExecutablePath() As String
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 0       ' Index key
        ' ----------------------------------

        ' Scan the Source Port list and find the desired engine
        ' that the user choose to utilize.
        For Each i As SourcePort In sourcePortList
            ' Did we find the right index key that matches?
            If (selectedSourcePortID = indexCounter) Then
                ' We found the right key, now return the path of
                ' that executable.
                Return i.AbsolutePath
            End If

            ' Update the counter
            indexCounter += 1
        Next

        ' If in case something goes horribly wrong, return an error
        ' message.
        Return "!ERR"
    End Function




    ' Launch Builder: Find IWAD Path
    ' ------------------------------------------
    ' This function is designed to find the absolute path
    ' of the IWAD file from the IWAD list and return that
    ' to the calling function.
    ' -----------------------
    ' Output
    '   File Path [String]
    '       The absolute file path of the IWAD file that was
    '       selected.
    '       NOTE: '!ERR' will be returned if an error occurs
    '               mainly when the scan fails.
    Public Function LaunchBuilderFindIWADPath() As String
        ' Declarations and Initializations
        ' ----------------------------------
        Dim indexCounter As Int32 = 0       ' Index key
        ' ----------------------------------

        ' Scan the IWAD list and find the desired game file
        ' that the user choose to utilize.
        For Each i As IWAD In internalWADList
            ' Did we find the right index key that matches?
            If (selectedIWADID = indexCounter) Then
                ' We found the right key, now return the path of
                ' that IWAD file.
                Return i.AbsolutePath
            End If

            ' Update the counter
            indexCounter += 1
        Next

        ' If in case something goes horribly wrong, return an error
        ' message.
        Return "!ERR"
    End Function




    ' Launch Builder: Check File Exists
    ' ------------------------------------------
    ' This function is designed to merely determine if a file
    ' exists within its given absolute path.
    ' -----------------------
    ' Parameters
    '   dataFile [String]
    '       The absolute path with the binary or datafile.
    '       Example: C:\Programs\Games\GZDoom\gzdoom.exe
    ' -----------------------
    ' Output
    '   State [Bool]
    '       True = An error occurred or the file does not exist within
    '               the given path.
    '       False = File exists
    Public Function LaunchBuilderCheckFileExists(dataFile As String) As Boolean
        If (System.IO.File.Exists(dataFile)) Then
            ' The file exists
            Return False
        Else
            ' The file was not found or some issue occurred.
            Return True
        End If
    End Function




    ' Launch Builder: PWAD Inclusion
    ' ------------------------------------------
    ' This function is designed to grab all PWADs that the user
    ' requested to use in this instance.  We will scan through
    ' the PWADList and put all of the PWADs paths into one string
    ' that can be used when invoking the source port engine.
    ' -----------------------
    ' Output
    '   PWAD paths (combined)
    '       This will hold all PWAD paths combined into one string.
    '       For example: "C:\myWad.wad, C:\UTNT.pk3, C:\NewMonsters.deh"
    '       NOTE: If no PWAD was requested, than 'Nothing' will be returned.
    Public Function LaunchBuilderPWADInclusion() As String
        ' Declarations and Initializations
        ' ----------------------------------
        Dim catenateString As String = Nothing    ' This will hold all of the PWAD paths
        ' ----------------------------------

        If (Not (patchWADList.Count = 0)) Then
            ' PWADList contains one or more PWADs to load
            ' Scan through each index of the PWADList and store
            ' all of the PWAD absolute paths into a catenate variable.
            For Each i As PWAD In patchWADList
                ' If this is the first entry, then just throw the value without any formatting.
                If (catenateString = Nothing) Then
                    catenateString = Chr(34) + i.AbsolutePath + Chr(34)
                Else
                    ' with other values existing, append it.
                    catenateString = catenateString + " " + Chr(34) + i.AbsolutePath + Chr(34)
                End If
            Next

            ' Return our concatenated string
            Return catenateString
        Else
            ' Nothing was included; no PWADs to load
            Return Nothing
        End If
    End Function




    ' Launch Builder: Custom Parameters
    ' ------------------------------------------
    ' This function will merely get the custom parameters and return
    ' the parameter, if any was provided by the end-user.
    ' If in case we need to do any formations\transformations or use
    ' a list (which I would recommend in the next major version, if any)
    ' we will need this function to be available and ready for use.
    ' -----------------------
    ' Output
    '   Custom Parameters [String]
    '       This hold the requested custom parameters.
    '       NOTE: if no parameters are giving, then 'Nothing' will be returned.
    Public Function LaunchBuilderCustomParameters() As String
        If (TextBoxCustomParameters.Text = Nothing) Then
            ' Nothing was provided in the TextBox
            Return Nothing
        Else
            ' Return the parameters the user provided
            Return TextBoxCustomParameters.Text
        End If
    End Function




    ' Launch Builder: Game Play Flags
    ' ------------------------------------------
    ' This function is designed to inspect what game play flags were
    ' set and compile each flag, if true\checked, to a concatenated string
    ' that common ZDoom engines can understand.
    ' -----------------------
    ' Output
    '   Game play flags [String]
    '       This will provide the flags that the user requested in a
    '       concatenated value.
    '       NOTE: If no flags were provided, then 'Nothing' will be returned.
    Public Function LaunchBuilderGamePlayFlags() As String
        ' Declarations and Initializations
        ' ----------------------------------
        Dim gameFlags As String = Nothing
        ' ----------------------------------

        ' Fast Monsters
        ' ================
        If (CheckBoxFastMonsters.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "-fast"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -fast"
            End If
        End If

        ' Monsters Respawn
        ' ================
        If (CheckBoxMonstersRespawn.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "-respawn"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -respawn"
            End If
        End If

        ' Deathmatch
        ' ================
        If (CheckBoxDeathmatch.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "-deathmatch"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -deathmatch"
            End If
        End If

        ' Austin Virtual Gaming
        ' ================
        If (CheckBoxAVG.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "-avg"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -avg"
            End If
        End If

        ' No Music
        ' ================
        If (CheckBoxNoMusic.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "-nomusic"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -nomusic"
            End If
        End If

        ' No Sound Effects
        ' ================
        If (CheckBoxNoSFX.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "nosfx"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -nosfx"
            End If
        End If

        ' No Multimedia [No Sound Effects & No Music]
        ' ================
        If (CheckBoxNoMultimedia.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "-nosound"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -nosound"
            End If
        End If

        ' Use Old Startup
        ' ================
        If (CheckBoxUseOldStartup.IsChecked = True) Then
            If (gameFlags = Nothing) Then
                ' First flag to be set, throw the flag as is.
                gameFlags = "-nostartup"
            Else
                ' If other values exists, concatenate them.
                gameFlags = gameFlags + " -nostartup"
            End If
        End If

        ' Return the desired flags
        Return gameFlags
    End Function
End Class
