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
    }    
}