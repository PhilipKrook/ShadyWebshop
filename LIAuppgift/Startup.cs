using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using EPiServer.Cms.UI.AspNetIdentity;
using LIAuppgift.Models.Entites;
using Microsoft.AspNet.Identity.Owin;
using System;

[assembly: OwinStartup(typeof(LIAuppgift.Startup))]

namespace LIAuppgift
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.AddCmsAspNetIdentity<CustomUser>();

            // Use cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login.aspx"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager<CustomUser>, CustomUser>(
                   validateInterval: TimeSpan.FromMinutes(30),
                   regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user))
                }
            });
        }
    }
}
