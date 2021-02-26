namespace LIAuppgift.Models.Entites
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using EPiServer.Shell.Security;
    using Microsoft.AspNet.Identity.EntityFramework;    

    public class CustomUser : IdentityUser, IUIUser
    {
        [NotMapped]
        public string Username
        {
            get { return this.UserName; }
            set { this.UserName = value; }
        }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public string PasswordQuestion { get; set; }

        public string ProviderName { get; set; }

        public string Comment { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLoginDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLockoutDate { get; set; }

        public string StreetAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }
    }
}