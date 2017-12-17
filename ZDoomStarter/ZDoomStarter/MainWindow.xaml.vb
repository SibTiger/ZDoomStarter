Class MainWindow
    ' Main File Menu: File Menu
    Private Sub FileMenu_Click(sender As Object, e As RoutedEventArgs)
        ' Do nothing?
        ' Removing this causes compilation issues, I'll need to figure out why later.
    End Sub



    ' Destroy
    Private Sub Menu_Exit_Click(sender As Object, e As RoutedEventArgs) Handles FileMenuExit.Click
        ' When clicked, just immediately close the program.
        Close()
    End Sub

    ' Configure Source Ports
    Private Sub ConfigureMenuSourcePorts_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuSourcePorts.Click
        ' TODO: Implement this
    End Sub

    ' Configure IWAD's
    Private Sub ConfigureMenuIWADs_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuIWADs.Click
        ' TODO: Implement this
    End Sub

    ' Configure PWAD Directory Path
    Private Sub ConfigureMenuPWADsDir_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuPWADsDir.Click
        ' TODO: Implement this
    End Sub

    ' Allow the end-user to check for updates
    Private Sub HelpMenuUpdatesGitHub_Click(sender As Object, e As RoutedEventArgs)
        ' TODO: Implement this
    End Sub

    ' Display software information
    Private Sub HelpMenuAbout_Click(sender As Object, e As RoutedEventArgs)
        ' TODO: Implement this
    End Sub
End Class
