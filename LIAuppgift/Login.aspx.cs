namespace WebFormsIdentity
{
    using System;
    using System.Web;
    using LIAuppgift.Business.EntityFramework;
    using LIAuppgift.Models.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;

    // User login
    public partial class Login : System.Web.UI.Page
    {
        // Check if user is authenticated
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    StatusText.Text = string.Format("Hello {0}!!", User.Identity.GetUserName());
                    LoginStatus.Visible = true;
                    LogoutButton.Visible = true;
                }
                else
                {
                    LoginForm.Visible = true;
                }
            }
        }

        // Sign in method form
        protected void SignIn(object sender, EventArgs e)
        {
            var userStore = new UserStore<CustomUser>(new EPiServerDbContext());
            var userManager = new UserManager<CustomUser>(userStore);
            var user = userManager.Find(Email.Text, Password.Text);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                StatusText.Text = "Invalid user name or password.";
                LoginStatus.Visible = true;
            }
        }

        // Sign out
        protected void SignOut(object sender, EventArgs e)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}