Public Class ConfigSourcePorts
    ' Declarations and Initializations
    ' -------------------------------------------------
    ' This list will hold all source port entries available to this program.
    ' NOTE: Property (or {get; set;}) is adjacent to a function call, hence the different
    ' naming scheme.
    Public Property DisplayEngineList As New List(Of SourcePort)

    ' Default cached index value
    Private cachedIndexDefault As Int32 = -1

    ' Hold the index highlighted by the end-user from the ViewList UI component.
    ' Default value will be '-1', this is to signify that nothing was selected.
    Private viewListSelectedIndex As Int32 = cachedIndexDefault
    ' -------------------------------------------------




    ' Window Load [EVENT: Form Load]
    ' ------------------------------------------
    ' This function will automatically execute once the window has been fully rendered
    Private Sub Window_Load() Handles MyBase.Loaded
        ' Initialize the UI components within the window.
        InitializeComponent()

        ' TESTING REGION
        ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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

        DisplayEngineList.Add(Engine1)
        DisplayEngineList.Add(Engine2)

        RefreshViewList()
        ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    End Sub




    ' Render ViewList
    ' ------------------------------------------
    ' This function will populate the ViewList with the available
    ' data provided from the List<T>.
    ' -----------------------
    ' Parameters:
    '   availableData
    '       List<T>(SourcePort)
    '           A list of source ports that is available program.
    Private Sub RenderViewList(availableData As List(Of SourcePort))
        ' Scan through the list by its initial size to capture all the available source ports.
        For Each i As SourcePort In availableData
            ListSourcePorts.Items.Add(i)
        Next
    End Sub




    ' List Source Ports - Item Selected [EVENT]
    ' ------------------------------------------
    ' This function will be automatically called when the user clicks
    ' an item from the ViewList.
    ' When clicked, this will cache the index that was selected.  We may
    ' use that index when the user clicks on the 'Remove' UI button.
    Private Sub ListSourcePorts_ItemSelected()
        ' Cache the selected index
        viewListSelectedIndex = ListSourcePorts.SelectedIndex
    End Sub




    ' Clear ViewList
    ' ------------------------------------------
    ' When called, this function will merely thrash all contents
    ' within the ViewList.  This is primarily useful when updating
    ' the list after modifications were requested.
    Private Sub ClearDisplayList()
        ListSourcePorts.Items.Clear()
    End Sub




    ' Delete Item in List
    ' ------------------------------------------
    ' This function will expunge an item from the list
    ' -----------------------
    ' Parameters:
    '   itemIndex [Int32]
    '       The index of the item to be expunged from the list.
    '   dataList [List<T>(SourcePort)]
    '       The list to be modified directly.
    ' -----------------------
    ' Output:
    '   True = Error Occurred
    '   False = Successful operation
    Private Function ExpungeItem(itemIndex As Int32, dataList As List(Of SourcePort)) As Boolean
        ' Make sure that the list size is exactly or greater than the index
        ' selected.
        If (dataList.Count >= itemIndex) Then
            dataList.RemoveAt(itemIndex)    ' Expunge the item from the List<T>
            Return False    ' Successful
        End If

        Return True         ' Error
    End Function




    ' Refresh ViewList
    ' ------------------------------------------
    ' This function will refresh the ViewList UI component with
    ' the latest changes from the list.
    Private Sub RefreshViewList()
        ' Clear the ViewList UI Component
        ClearDisplayList()

        ' Regenerate the items for the ViewList
        RenderViewList(DisplayEngineList)
    End Sub




    ' UI ELEMENTS
    ' =================================================
    ' =================================================
    ' =================================================




    ' Cancel [BUTTON]
    ' ------------------------------------------
    ' When clicked, this will merely close the configuration window and cancel all pending alterations.
    Private Sub ButtonCancel_Click(sender As Object, e As RoutedEventArgs) Handles ButtonCancel.Click
        Close()     ' Close the child window
    End Sub




    ' Okay [BUTTON]
    ' ------------------------------------------
    ' When clicked, this will save all pending changes and close the configuration window.
    Private Sub ButtonOK_Click(sender As Object, e As RoutedEventArgs) Handles ButtonOK.Click
        Close()     ' Close the child window
    End Sub




    ' Expunge Entry [BUTTON]
    ' ------------------------------------------
    ' When clicked and an entry has been selected within the list, the entry (or row) will be thrashed off the array.
    Private Sub ButtonDelete_Click(sender As Object, e As RoutedEventArgs) Handles ButtonDelete.Click
        ' Make sure that /something/ from the ViewList was actually selected.
        ' If nothing was selected or valid, then nothing is to be performed.
        If (viewListSelectedIndex > cachedIndexDefault) Then
            ' Remove the item from the list as requested by the end-user
            If (ExpungeItem(viewListSelectedIndex, DisplayEngineList)) Then
                ' If in case an error occurred, display an error message.
                MessageBox.Show("UNABLE TO DELETE AT INDEX [ " + CStr(viewListSelectedIndex) + " ]!",
                        "Delete Operation Failure")
            Else
                ' Update the cached index to its default value.
                viewListSelectedIndex = cachedIndexDefault

                ' Refresh the ViewList UI Component
                RefreshViewList()
            End If  ' Expunge Operation
        End If      ' If selection is valid
    End Sub




    ' Add Entry [BUTTON]
    ' ------------------------------------------
    ' When clicked, this will allow the end-user to select an executable engine and add that to the list.
    Private Sub ButtonAdd_Click(sender As Object, e As RoutedEventArgs) Handles ButtonAdd.Click

    End Sub




    ' Source Ports Available [List Box]
    ' ------------------------------------------
    ' Displays all source ports available to this program; provided by the end-user (or through a configuration file or entity).
    Private Sub ListView_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ListSourcePorts.SelectionChanged

    End Sub
End Class
