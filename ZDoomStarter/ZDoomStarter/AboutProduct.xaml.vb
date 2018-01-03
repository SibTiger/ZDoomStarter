' About Product [UI]
' ---------------------------------------
' =======================================
' This window will only provide information regarding this program.
' Information such as version, name, and description.
' This is a small window with no user-interaction other than to close
' the window itself.
' ---------------------------------------




Public Class AboutProduct
#Region "Declarations and Initializations"
    Private productName As String           ' Product Name
    Private productVersion As String        ' Product Version
    Private productCopyright As String      ' Product Copyright
    Private productDescription As String    ' Product Description
    Private Const authorName As String _
        = "Nicholas ""Tiger"" Gautier"      ' Author; I tried to set this in the project properties, but I don't think it is possible.
#End Region




    ' Constructor Window
    ' ------------------------------------------
    ' This is the default constructor for this window.
    ' Within this constructor, we will also setup the required data as necessary.
    ' -----------------------
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Initialize the data for use.
        productName = My.Application.Info.ProductName           ' Product name
        productVersion = My.Application.Info.Version.ToString() ' Product version
        productCopyright = My.Application.Info.Copyright        ' Product copyright
        productDescription = My.Application.Info.Description    ' Product description
    End Sub




    ' Window Load [EVENT: Form Load]
    ' ------------------------------------------
    ' This function will automatically execute once the window has been fully rendered
    Private Sub Window_Load() Handles MyBase.Loaded
        ' Setup all of the UI components
        LoadProductDetails()
    End Sub




    ' Load Product Details
    ' ------------------------------------------
    ' This function is dedicated to initializing labels and other UI components with product information and details.
    Private Sub LoadProductDetails()
        ' Product name with Author name
        LabelProductName.Content = productName + " By " + authorName
        ' Product version
        LabelProductVersion.Content = "Version: " + productVersion
        ' Product copyright
        LabelProductCopyright.Content = productCopyright
        ' Description
        TextBlockProductDescription.Text = productDescription
    End Sub
End Class
