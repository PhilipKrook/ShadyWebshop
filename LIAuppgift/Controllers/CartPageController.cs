﻿namespace LIAuppgift.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Mvc;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.Web.Mvc;
    using Models.Pages;
    using Models.ViewModels;

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
                var cartItems = ctx.CartItems.Where(x => x.UserId == userId).ToList();
                /*if (cartItems == null)
                {
                    // Create a new cart item if no cart item exists.                 
                    cartItems = new CartItems
                    {
                        ItemId = Guid.NewGuid().ToString(),
                        ProductId = id,
                        CartId = ShoppingCartId,
                        Product = _db.Products.SingleOrDefault(
                       p => p.ProductID == id),
                        Quantity = 1,
                        DateCreated = DateTime.Now
                    };

                    _db.ShoppingCartItems.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart,                  
                    // then add one to the quantity.                 
                    cartItem.Quantity++;
                }*/
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
        public int Quantity { get; set; }
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