<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebFormsIdentity.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>- Shady Webshop</title>
    <link rel="shortcut icon" type="image/ico" href="~/Static/gfx/anonymous2.png" />
    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Static/css/Style.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/modernizr-2.6.2.js"></script>
</head>
<body>

    <%--Header--%>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <a class="navbar-brand" href="./">Shady Webshop</a>
                <div class="navbar-icon">
                    <a class="navbar-icon-basket" href="./CartPage">
                        <img src="./Static/gfx/basket.png" class="basketico" /></a>
                    <a class="navbar-icon-account" href="/Login.aspx">
                        <img src="./Static/gfx/account.png" class="accountico" /></a>
                </div>
            </div>
        </div>
    </div>

    <%--Login form--%>
    <form id="form1" runat="server">
        <div>
            <h4>Log In</h4>
            <hr />
            <asp:PlaceHolder runat="server" ID="LoginStatus" Visible="false">
                <p>
                    <asp:Literal runat="server" ID="StatusText" />
                </p>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="LoginForm" Visible="false">
                <div>
                    <asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label>
                    <div><asp:TextBox runat="server" ID="Email" /></div>
                </div>
                <div>
                    <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                    <div><asp:TextBox runat="server" ID="Password" TextMode="Password" /></div>
                </div>
                <div>
                    <div><asp:Button runat="server" OnClick="SignIn" Text="Log in" /></div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="LogoutButton" Visible="false">
                <div>
                    <div><asp:Button runat="server" OnClick="SignOut" Text="Log out" /></div>
                </div>
            </asp:PlaceHolder>
        </div>
    </form>

    <%--Footer--%>
    <div class="container body-content">
        <hr />
        <footer>
            <p>&copy; <%= DateTime.Now.Year %> - Shady Webshop by Philip Krook</p>
        </footer>
    </div>

</body>
</html>
