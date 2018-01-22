Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Generato all'avvio dell'applicazione
        RouteConfig.RegisterRoutes(RouteTable.Routes)
    End Sub
End Class