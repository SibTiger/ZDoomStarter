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




    ' UI ELEMENTS
    ' =================================================
    ' =================================================
    ' =================================================
End Class
