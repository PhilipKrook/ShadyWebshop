<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebFormsIdentity.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>- Shady Webshop</title>
    <link rel="shortcut icon" type="image/ico" href="~/Static/gfx/anonymous2.png" />
    <link href="~/Static/css/styles.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/modernizr-2.6.2.js"></script>
</head>
<body>

    <%--Header--%>
    <div class="">
        <div class="">
            <div class="">
                <a class="" href="./">Shady Webshop</a>
                <div class="">
                    <a class="" href="./CartPage">
                        <img src="./Static/gfx/basket.png" class="basketico" /></a>
                    <a class="" href="/Login.aspx">
                        <img src="./Static/gfx/account.png" class="accountico" /></a>
                </div>
            </div>
        </div>
    </div>

    <%--Register form--%>
    <form id="form1" runat="server">
        <div>
            <h4>Register a new user</h4>
            <hr />
            <p>
                <asp:Literal runat="server" ID="StatusMessage" />
            </p>

            <div>
                <asp:Label runat="server" AssociatedControlID="UserName">User name</asp:Label>
                <div><asp:TextBox runat="server" ID="UserName" /></div>
            </div>

            <div>
                <asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label>
                <div><asp:TextBox runat="server" ID="Email" />
                </div>
            </div>
            <div>
                <asp:Label runat="server" AssociatedControlID="FirstName">First name</asp:Label>
                <div><asp:TextBox runat="server" ID="FirstName" /></div>
            </div>

            <div>
                <asp:Label runat="server" AssociatedControlID="LastName">Last name</asp:Label>
                <div><asp:TextBox runat="server" ID="LastName" /></div>
            </div>

            <div>
                <asp:Label runat="server" AssociatedControlID="PhoneNumber">Phone number</asp:Label>
                <div><asp:TextBox runat="server" ID="PhoneNumber" /></div>
            </div>

            <div>
                <asp:Label runat="server" AssociatedControlID="StreetAddress">Street address</asp:Label>
                <div><asp:TextBox runat="server" ID="StreetAddress" /></div>
            </div>

            <div>
                <asp:Label runat="server" AssociatedControlID="City">City</asp:Label>
                <div><asp:TextBox runat="server" ID="City" /></div>
            </div>

            <div>
                <asp:Label runat="server" AssociatedControlID="PostCode">Post code</asp:Label>
                <div><asp:TextBox runat="server" ID="PostCode" /></div>
            </div>

            <div>
                <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                <div><asp:TextBox runat="server" ID="Password" TextMode="Password" /></div>
            </div>
            <div>
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
                <div><asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" /></div>
            </div>

            <div>
                <div>
                    <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" />
                </div>
            </div>
        </div>
    </form>

    <%--Footer--%>
    <div class="">
        <hr />
        <footer>
            <div>
                <p>&copy; <%= DateTime.Now.Year %> - Shady Webshop by Philip Krook</p>
            </div>
            <div>
                <a class="" href="./en/contact-us">Contact us</a>
                <a class="" href="./en/about-us">About us</a>
            </div>
        </footer>
    </div>

</body>
</html>
