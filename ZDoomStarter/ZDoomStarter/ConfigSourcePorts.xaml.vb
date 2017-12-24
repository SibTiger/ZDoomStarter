Public Class ConfigSourcePorts
    ' Declarations and Initializations
    ' -------------------------------------------------
    ' This list will hold all source port entries available to this program.
    ' NOTE: Property (or {get; set;}) is adjacent to a function call, hence the different
    ' naming scheme.
    Public Property DisplayEngineList As New List(Of SourcePort)

    ' Hold the index highlighted by the end-user from the ViewList UI component.
    ' Default value will be '-1', this is to signify that nothing was selected.
    Private viewListSelectedIndex As Int32 = -1
    ' -------------------------------------------------




    ' Window Load [EVENT: Form Load]
    ' ------------------------------------------
    ' This function will automatically execute once the window has been fully rendered
    Public Sub Window_Load() Handles MyBase.Loaded
        ' Initialize the UI components within the window.
        InitializeComponent()

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

        ' Update the data
        DataContext = Me
    End Sub




    ' List Source Ports - Item Selected [EVENT]
    ' ------------------------------------------
    ' This function will be automatically called when the user clicks
    ' an item from the ViewList.
    ' When clicked, this will cache the index that was selected.  We may
    ' use that index when the user clicks on the 'Remove' UI button.
    Private Sub ListSourcePorts_ItemSelected()
        ' Update the cached index.
        viewListSelectedIndex = ListSourcePorts.SelectedIndex
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
