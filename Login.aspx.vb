Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        If Not String.IsNullOrEmpty(Username.Text) Then
            FormsAuthentication.RedirectFromLoginPage(Username.Text, False)
        End If
    End Sub

End Class