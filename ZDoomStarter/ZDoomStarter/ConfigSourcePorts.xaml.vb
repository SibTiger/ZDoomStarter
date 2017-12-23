Public Class ConfigSourcePorts
    ' Declarations and Initializations
    ' -------------------------------------------------
    Public Property displayEngineList As New List(Of SourcePort)
    ' -------------------------------------------------


    ' Window Load [EVENT: Form Load]
    ' ------------------------------------------
    ' This function will automatically execute once the window has been fully rendered
    Public Sub Window_Load() Handles MyBase.Loaded
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

        displayEngineList.Add(Engine1)
        displayEngineList.Add(Engine2)


        DataContext = Me
    End Sub


    ' TODO Finish implementing this
    ' Cache the list index and then use it when 'Remove' is requested.
    ' References:
    ' https://stackoverflow.com/questions/1367665/wpf-listview-selectionchanged-event
    ' https://stackoverflow.com/questions/8693897/how-do-i-get-the-selecteditem-or-selectedindex-of-listview-in-vb-net
    Private Sub ListSourcePorts_ItemSelected()
        MsgBox("HIT!")
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
