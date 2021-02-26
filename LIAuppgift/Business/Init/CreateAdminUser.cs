namespace LIAuppgift.Business.Init
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using LIAuppgift.Business.EntityFramework;
    using LIAuppgift.Models.Entites;
    using LIAuppgift.Models.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule), typeof(DatabaseMigrationsInitialization))] // , typeof(CustomSmrtDatabaseMigrationInitialization))]
    public class CreateAdminUser : IInitializableModule
    {
        private static readonly string[] Roles = { "WebAdmins", "WebEditors", "Creators", "Deleters", "Editors", "MasterEditors", "Publishers", "Readers", "ReadOnlyUsers", "SMTEditors", "SuperAdmins", "SuperEditors", "KanthalEditors", "AdditiveEditors" };
        
        public void Initialize(InitializationEngine context)
        {
            using (UserStore<CustomUser> store = new UserStore<CustomUser>(new EPiServerDbContext()))
            {
                // If there's already a user, then we don't need a seed
                if (!store.Users.Any(x => x.UserName == "EpiMvcUser1"))
                {
                    var createdUser = this.CreateUser(store, "EpiMvcUser1", "EpimvcUser1234!", "epimvcuser1@mvc.se");
                    this.AddUserToRoles(store, createdUser, Roles);
                    store.UpdateAsync(createdUser).GetAwaiter().GetResult();
                }
            }
        }

        private CustomUser CreateUser(UserStore<CustomUser> store, string username, string password, string email)
        {
            IPasswordHasher hasher = new PasswordHasher();
            string passwordHash = hasher.HashPassword(password);
            CustomUser applicationUser = new CustomUser
            {
                Email = email,
                EmailConfirmed = true,
                LockoutEnabled = true,
                IsApproved = true,
                UserName = username,
                PasswordHash = passwordHash,
                FirstName = "test firstname",
                LastName = "lastname test",
                StreetAddress = "test streetaddress",
                City = "test city",
                PostCode = "postcode test",
                CreationDate = DateTime.Now,                
                SecurityStamp = Guid.NewGuid().ToString(),                
            };
            store.CreateAsync(applicationUser).GetAwaiter().GetResult();

            // Get the user associated with our username
            CustomUser createdUser = store.FindByNameAsync(username).GetAwaiter().GetResult();
            return createdUser;
        }
    
        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }        

        private void AddUserToRoles(UserStore<CustomUser> store, CustomUser user, string[] roles)
        {
            IUserRoleStore<CustomUser, string> userRoleStore = store;
            using (var roleStore = new RoleStore<IdentityRole>(new EPiServerDbContext()))
            {
                IList<string> userRoles = userRoleStore.GetRolesAsync(user).GetAwaiter().GetResult();
                foreach (string roleName in roles)
                {
                    if (roleStore.FindByNameAsync(roleName).GetAwaiter().GetResult() == null)
                    {
                        roleStore.CreateAsync(new IdentityRole { Name = roleName }).GetAwaiter().GetResult();
                    }
                    if (!userRoles.Contains(roleName))
                    {
                        userRoleStore.AddToRoleAsync(user, roleName).GetAwaiter().GetResult();
                    }
                }
            }
        }        
    }
}