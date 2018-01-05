' Windows Registry Functionality
' ---------------------------------------
' =======================================
' This class is designed to perform Registry operations safely
' and assure that the data is saved and loaded correctly with
' minimal conflicts.  Since we are working with I/O and more
' importantly, the Registry, we must make sure that we are
' dealing with the data appropriately and efficiently with no
' conflicts.
' ---------------------------------------




Public Class WindowsRegistry
#Region "Declarations and Initializations"
    ' REGISTRY SubKey and Key VARIABLES
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




    ' Constructor Window
    ' ------------------------------------------
    ' This is the default constructor for this window.
    ' This is ideal for transferring data from one end to another.
    Public Sub New()
        ' Nothing to transfer
    End Sub




#Region "Read from Registry"
    ' Read Registry: Check Root Key Exists
    ' ------------------------------------------
    ' This function is dedicated to determining if the program environment (or mainly, user preferences)
    ' might be available in the system registry.  If the root subkey exists within the default location,
    ' then we will just assume that data exists and is available for access.  If the root subkey does not
    ' exists within the default location, then we assume that the nothing is available for us in the
    ' system registry.
    ' -----------------------
    ' Output
    '   Boolean
    '       True = Program's root subkey exists and data could be available for us to use.
    '       False = Program's root subkey does not exists, thus nothing is available for us to use.
    Private Function ReadRegistryCheckExists() As Boolean
        If (Not (My.Computer.Registry.CurrentUser.OpenSubKey(regkeyProgramRoot, False) Is Nothing)) Then
            ' If the root subkey exists, then we know that user configuration might be available for use.
            Return True
        Else
            ' If the root subkey does not exist, then there is nothing available for use.
            Return False
        End If
    End Function




    ' Read Registry: PWAD Directory
    ' ------------------------------------------
    ' This function will capture the PWAD directory from the Windows Registry and then 
    ' return that value back to the calling function.  If in case no data is available,
    ' then 'Nothing' will be returned.
    ' -----------------------
    ' Output
    '   PWAD Directory [String]
    '       Returns the previously saved location of the PWAD directory.
    '       If no data exists, then 'Nothing' will be returned.
    Public Function ReadRegistryPWADDirectory() As String
        ' Check to make sure that data is available, then return the appropriate data back to the calling function.
        If (ReadRegistryCheckExists()) Then
            ' Return the PWAD Path from the Registry
            Return My.Computer.Registry.CurrentUser.OpenSubKey(regkeyProgramRoot, False).GetValue("PWADPath")
        Else
            ' Nothing is available
            Return Nothing
        End If
    End Function




    ' Read Registry: Source Port
    ' ------------------------------------------
    ' This function will capture the Source Port list from the Windows Registry and then
    ' return that list back to the calling function.  If in case no data is available,
    ' then an empty list will be returned.
    ' -----------------------
    ' Output
    '   Source Port List [List<SourcePort>]
    Public Function ReadRegistrySourcePort() As List(Of SourcePort)
        ' Declarations and Initializations
        ' ----------------------------------
        Dim engineList As New List(Of SourcePort)
        ' ----------------------------------

        ' Check to make sure that data is available, then return the appropriate data back to the calling function.
        If (ReadRegistryCheckExists()) Then
            ' Retrieve the Source Port list size from the Windows Registry
            Dim engineListSize As Int32 =
                My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, False).GetValue("Size")

            ' Check to make sure that there exists at least one engine within the list.
            ' If in case there is no engine provided within the list, then we will skip it.
            If (engineListSize > 0) Then

                ' Scan through the registry and retrieve the necessary data
                For i As Integer = 0 To (engineListSize - 1)
                    ' Capture the data from the Registry save it to the list directly.
                    engineList.Add(New SourcePort() With {
                        .NiceName = My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, False).GetValue(engineKeyName + CStr(i) + "NiceName"),
                        .CustomNotes = My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, False).GetValue(engineKeyName + CStr(i) + "CustomNotes"),
                        .AbsolutePath = My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, False).GetValue(engineKeyName + CStr(i) + "AbsolutePath")
                                       })
                Next
            End If
        End If

        ' Return the list with or without entries
        Return engineList
    End Function




    ' Read Registry: IWAD (Game Data)
    ' ------------------------------------------
    ' This function will capture the IWAD list from the Windows Registry and then
    ' return that list back to the calling function.  If in case no data is available,
    ' then an empty list will be returned.
    ' -----------------------
    ' Output
    '   Game Data List [List<IWAD>]
    Public Function ReadRegistryGameData() As List(Of IWAD)
        ' Declarations and Initializations
        ' ----------------------------------
        Dim gameDataList As New List(Of IWAD)
        ' ----------------------------------

        ' Check to make sure that data is available, then return the appropriate data back to the calling function.
        If (ReadRegistryCheckExists()) Then
            ' Retrieve the IWAD list size from the Windows Registry
            Dim gameDataListSize As Int32 = My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, False).GetValue("Size")

            ' Check to make sure that there exists at least one IWAD within the list.
            ' If in case there is no IWAD provided within the list, then we will skip it.
            If (gameDataListSize > 0) Then

                ' Scan through the registry and retrieve the necessary data
                For i As Integer = 0 To (gameDataListSize - 1)
                    ' Capture the data from the Registry save it to the list directly.
                    gameDataList.Add(New IWAD() With {
                        .NiceName = My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, False).GetValue(iwadKeyName + CStr(i) + "NiceName"),
                        .CustomNotes = My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, False).GetValue(iwadKeyName + CStr(i) + "CustomNotes"),
                        .AbsolutePath = My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, False).GetValue(iwadKeyName + CStr(i) + "AbsolutePath")
                                 })
                Next
            End If
        End If

        ' Return the list with or without entries
        Return gameDataList
    End Function
#End Region




#Region "Save to Registry"
    ' Write Registry
    ' ------------------------------------------
    ' This function will centralize how the write functionality operates.  When writing to the registry, this function must be called
    ' and will save the data accordingly.
    ' -----------------------
    ' Parameters
    '   PWADPath [String]
    '       This will hold the desired directory that contains the PWADs
    '   engineList [List<SourcePort>]
    '       This will hold the Source Port list and all the data associated with the SourcePort class.
    '   gameDataList [List<IWAD>]
    '       This will hold the IWAD list and all the data associated with the IWAD class.
    Public Sub WriteRegistry(ByVal PWADPath As String,
                             ByVal engineList As List(Of SourcePort),
                             ByVal gameDataList As List(Of IWAD))

        ' Generate the necessary SubKeys
        WriteRegistryMakeSubKeys()

        ' Generate the common data
        WriteRegistryGenerateCommonData(PWADPath)

        ' Generate the source port list
        WriteRegistryGenerateSourcePortList(engineList)

        ' Generate the IWAD list
        WriteRegistryGenerateIWADList(gameDataList)
    End Sub




    ' Write Registry: Make SubKeys
    ' ------------------------------------------
    ' This function will generate the necessary registry subkeys that is necessary for this program.
    ' This will assure that the data is kept organized and in a neatly fashion.
    Private Sub WriteRegistryMakeSubKeys()
        My.Computer.Registry.CurrentUser.CreateSubKey(regkeyProgramRoot)    ' Program root SubKey
        My.Computer.Registry.CurrentUser.CreateSubKey(regKeySourcePort)     ' Source Ports SubKey
        My.Computer.Registry.CurrentUser.CreateSubKey(regKeyIWAD)           ' IWADs SubKey
    End Sub




    ' Write Registry: Generate Common Data
    ' ------------------------------------------
    ' This function will generate common data that will be available at the program's root subkey
    ' within the Windows Registry.
    ' -----------------------
    ' Parameters
    '   PWADPath [String]
    '       This will hold the current desired directory that houses PWADs
    Private Sub WriteRegistryGenerateCommonData(ByVal PWADPath As String)
        ' Provide the program version that last wrote to the Registry.
        My.Computer.Registry.CurrentUser.OpenSubKey(regkeyProgramRoot, True).SetValue("Version", My.Application.Info.Version)

        ' Store the PWAD Directory into the Windows Registry
        My.Computer.Registry.CurrentUser.OpenSubKey(regkeyProgramRoot, True).SetValue("PWADPath", PWADPath)
    End Sub




    ' Write Registry: Generate Source Port List
    ' ------------------------------------------
    ' This function will transform the source port list that is stored locally within this program and mirror it to the Windows Registry.
    ' By shifting the data to the Windows Registry - and class oriented, we will need to assure that the data is consistent and will be known
    ' when the data is read from the registry.
    ' -----------------------
    ' Parameters
    '   engineList [List<SourcePort>]
    '       This will hold the Source Port list and all the data associated with the SourcePort class.
    Private Sub WriteRegistryGenerateSourcePortList(ByVal engineList As List(Of SourcePort))
        ' Declarations and Initialization
        ' --------------------------------
        Dim indexCounter As Int32 = 0       ' This will be used as a counter when processing through a list
        Dim listSize As Int32 =
            engineList.Count                ' This will hold the size of the engine list.
        ' --------------------------------

        ' First, record the size of the list for proper loading during the next load.
        My.Computer.Registry.CurrentUser.OpenSubKey(regKeySourcePort, True).SetValue("Size", listSize)

        If (listSize > 0) Then
            ' If there exists Source Port entries, then we will record each engine and the information.

            ' Mirror the data to the registry
            For Each i In engineList
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
        End If
    End Sub




    ' Write Registry: Generate IWAD (Game Data) List
    ' ------------------------------------------
    ' This function will transform the IWAD list that is stored locally within this program and mirror it to the Windows Registry.
    ' By shifting the data to the Windows Registry - and class oriented, we will need to assure that the data is consistent and will be known
    ' when the data is read from the registry.
    ' -----------------------
    ' Parameters
    '   gameDataList [List<IWAD>]
    '       This will hold the IWAD list and all the data associated with the IWAD class.
    Private Sub WriteRegistryGenerateIWADList(ByVal gameDataList As List(Of IWAD))
        ' Declarations and Initialization
        ' --------------------------------
        Dim indexCounter As Int32 = 0       ' This will be used as a counter when processing through a list
        Dim listSize As Int32 =
            gameDataList.Count              ' This will hold the size of the engine list.
        ' --------------------------------

        ' First, record the size of the list for proper loading during the next load.
        My.Computer.Registry.CurrentUser.OpenSubKey(regKeyIWAD, True).SetValue("Size", listSize)

        If (listSize > 0) Then
            ' If there exists IWAD entries, then we will record each IWAD and the associated information.

            ' Mirror the data to the registry
            For Each i In gameDataList
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
        End If
    End Sub
#End Region




#Region "Delete Registry SubKey Root"
    ' Delete Windows Registry Keys
    ' ------------------------------------------
    ' This function is dedicated to deleting the User's previously stored settings
    ' in the Windows Registry.  To accomplish this, we will delete the program's root
    ' SubKey by thrashing the entire tree.  All data that was previously stored - will
    ' be deleted from the Windows Registry.
    Public Sub DeleteRegistryKeys()
        ' Delete the root SubKey.  With deleting the SubKey, all other entries inside are deleted as well.
        My.Computer.Registry.CurrentUser.DeleteSubKeyTree(regkeyProgramRoot)
    End Sub
#End Region
End Class
