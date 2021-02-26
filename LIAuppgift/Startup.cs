﻿using Microsoft.Owin;

[assembly: OwinStartup(typeof(LIAuppgift.Startup))]

namespace LIAuppgift
{
    using System;
    using Microsoft.Owin;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.Cookies;
    using EPiServer.Cms.UI.AspNetIdentity;
    using Microsoft.AspNet.Identity.Owin;
    using Owin;
    using LIAuppgift.Models.Entities;

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