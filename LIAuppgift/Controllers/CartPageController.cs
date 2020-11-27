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

    public class CartPageController : PageController<CartPage>
    {
        public ActionResult Index(CartPage currentPage)
        {
            return View("~/Views/CartPage/Index.cshtml", currentPage);
        }
    }

    public class Program
    {
    
    static void Main(string[] args)
    {

        using (CartContext ctx = new CartContext())
        {
            var addedCartItem = new CartItem() { itemName = "Sony TV" };

            ctx.CartItems.Add(addedCartItem);
            ctx.SaveChanges();

                Console.Write("Item saved successfully!");
                Console.ReadLine();
            }
    }

    public class CartContext : DbContext
    {
        public CartContext() : base()
        {
        }
        public DbSet<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int itemAmount { get; set; }
        public int itemPrice { get; set; }
    }
    }
}