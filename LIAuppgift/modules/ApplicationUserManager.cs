using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Shell.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIAuppgift.modules
{
    public static class ApplicationUserManager
    {
        public static ApplicationUserManager<TUser> CreateApplicationUserManager<TUser>(IdentityFactoryOptions<ApplicationUserManager<TUser>> options, IOwinContext context) where TUser : IdentityUser, IUIUser, new()
        {
            var manager = new ApplicationUserManager<TUser>(new UserStore<TUser>(context.Get<ApplicationDbContext<TUser>>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<TUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Change Password hasher
            // manager.PasswordHasher = new SqlPasswordHasher();

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var provider = context.Get<ApplicationOptions>().DataProtectionProvider.Create("EPiServerAspNetIdentity");
            manager.UserTokenProvider = new DataProtectorTokenProvider<TUser>(provider);

            return manager;
        }
    }
}