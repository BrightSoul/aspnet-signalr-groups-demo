<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="AspNetSignalRGroupsDemo.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" class="container form-inline" runat="server">
        <h1>Login to the chat</h1>
        <div><label>Username</label></div>
        <asp:TextBox ID="Username" CssClass="form-control" style="max-width:300px;" runat="server" />
        <asp:Button ID="LoginButton" CssClass="btn btn-primary" Text="Entra" runat="server" />
    </form>
</body>
</html>
