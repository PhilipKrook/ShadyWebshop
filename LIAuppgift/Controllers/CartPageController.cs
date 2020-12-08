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
            var cartViewModel = new CartPageViewModel();
            cartViewModel.CurrentPage = currentPage;

            var cartCookie = this.Request.Cookies.Get("cart");
            if (cartCookie == null || string.IsNullOrWhiteSpace(cartCookie.Value))
            {
                return View("~/Views/CartPage/Index.cshtml", cartViewModel);
            }

            var cartRepository = new CartRepository();
            var cartItems = cartRepository.Get(cartCookie.Value);
            cartViewModel.CartItems = cartItems;
            /*var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var cartProducts = contentLoader.GetChildren<ProductPage>(currentPage.ContentLink);            
            cartViewModel.CartProducts = (IEnumerable<CartPage>)cartProducts;*/

            return View("~/Views/CartPage/Index.cshtml", cartViewModel);
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

        public CartItemEntity Get(int id)
        {
            using (CartContext ctx = new CartContext())
            {
                var cartItems = ctx.CartItems.SingleOrDefault(x => x.Id == id);
                return cartItems;
            }
        }

        public IEnumerable<CartItemEntity> Get(string userId)
        {
            using (CartContext ctx = new CartContext())
            {
                var cartItems = ctx.CartItems.Where(x => x.UserId == userId);
                return cartItems;
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
        public string UserId { get; set; }
    }

    internal sealed class CartConfiguration : DbMigrationsConfiguration<CartContext>
    {
        public CartConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
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