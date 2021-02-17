namespace WebFormsIdentity
{
    using LIAuppgift.Models.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using System;
    using System.Linq;
    using System.Web;

    public partial class Register : System.Web.UI.Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<ApplicationUser>();
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() {
                // UserName = UserName.Text, 
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Email = Email.Text, 
                PhoneNumber = PhoneNumber.Text, 
                StreetAddress = StreetAddress.Text,
                City = City.Text,
                PostCode = PostCode.Text
            };

            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                StatusMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}