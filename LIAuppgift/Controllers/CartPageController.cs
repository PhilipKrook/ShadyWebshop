using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer;

namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using EPiServer.ServiceLocation;
    using Models.Pages;
    using Models.ViewModels;
    using EPiServer.Core;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Migrations;
    using EPiServer.Framework.Initialization;
    using EPiServer.Framework;

    public class CartPageController : PageController<CartPage>
    {
        public ActionResult Index(CartPage currentPage)
        {
            return View("~/Views/CartPage/Index.cshtml", currentPage);
        }
    }    
    
    public class CartRepository
    {
        public void Add(CartItemEntity item)
        {
            using (CartContext ctx = new CartContext())
            {
                ctx.CartItems.Add(item);                
                ctx.SaveChanges();                
            }
        }
    }     

    public class CartContext : DbContext
    {
        public CartContext() : base("name=EPiServerDB")
        {
        
        }
        public DbSet<CartItemEntity> CartItems { get; set; }
    }

    public class CartItemEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string userId { get; set; }
    }

    internal sealed class CartConfiguration : DbMigrationsConfiguration<CartContext>
    {
        public CartConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
           /*  this.ContextKey = "Kommunal.Intranat.Domain.Personalization.PersonalizationDbContext"; */
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }

    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class MigrationsInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CartContext, CartConfiguration>());
        }
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}