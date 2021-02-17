using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using EPiServer.Cms.UI.AspNetIdentity;
using Microsoft.AspNet.Identity.Owin;
using System;
using LIAuppgift.Models.Entities;

[assembly: OwinStartup(typeof(LIAuppgift.Startup))]

namespace LIAuppgift
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Add CMS integration for ASP.NET Identity
            app.AddCmsAspNetIdentity<CustomUser>();

            // Use Cookie authenticatin
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login"),
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
