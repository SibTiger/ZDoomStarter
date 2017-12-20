Class MainWindow
    ' File Menu: Destroy
    ' ==========================================
    ' This will close the program properly.
    Private Sub Menu_Exit_Click(sender As Object, e As RoutedEventArgs) Handles FileMenuExit.Click
        ' When clicked, just immediately close the program.
        Close()
    End Sub


    ' Configure File Menu: Configure Source Ports
    ' ==========================================
    Private Sub ConfigureMenuSourcePorts_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuSourcePorts.Click
        ' Create the form instance
        Dim instance As New ConfigSourcePorts

        ' Open the form
        Me.Opacity = 0.5        ' OPACITY EXPERIMENTAL ONLY
        instance.ShowDialog()   ' This will open the desired form
        Me.Opacity = 1.0        ' OPACITY EXPERIMENTAL ONLY
    End Sub


    ' Configure File Menu: Configure IWAD's
    ' ==========================================
    Private Sub ConfigureMenuIWADs_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuIWADs.Click
        ' TODO: Implement this
    End Sub


    ' Configure File Menu: Configure PWAD Directory Path
    ' ==========================================
    Private Sub ConfigureMenuPWADsDir_Click(sender As Object, e As RoutedEventArgs) Handles ConfigureMenuPWADsDir.Click
        ' TODO: Implement this
    End Sub


    ' Help File Menu: Allow the end-user to check for updates
    ' ==========================================
    Private Sub HelpMenuUpdatesGitHub_Click(sender As Object, e As RoutedEventArgs)
        ' TODO: Implement this
    End Sub


    ' Help File Menu: Display software information
    ' ==========================================
    Private Sub HelpMenuAbout_Click(sender As Object, e As RoutedEventArgs)
        ' TODO: Implement this
    End Sub
End Class
