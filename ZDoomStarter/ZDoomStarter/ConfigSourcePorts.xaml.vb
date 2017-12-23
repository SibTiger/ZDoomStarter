Public Class ConfigSourcePorts
    ' Declarations and Initializations
    ' -------------------------------------------------
    Public displayEngineList As New List(Of SourcePort)
    ' -------------------------------------------------


    ' Window Load [EVENT: Form Load]
    ' ------------------------------------------
    ' This function will automatically execute once the window has been fully rendered
    Public Sub Window_Load() Handles MyBase.Loaded
        InitializeComponent()

        Dim Engine1 As New SourcePort

        With Engine1
            .absolutePath = ".\ZDoom.exe"
            .customNotes = "Optional Notes"
            .niceName = "ZDoom"
        End With

        displayEngineList.Add(Engine1)

        DataContext = Me
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
