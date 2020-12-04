using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;

namespace LIAuppgift.Controllers
{
    using EPiServer.ServiceLocation;
    using Models.Pages;
    public class ProductPageController : PageController<ProductPage>
    {
        public ActionResult Index(ProductPage currentPage)
        {
            var cartRepository = new CartRepository();
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var productId = currentPage.ContentLink.ID.ToString();
            var productPage = contentRepository.Get<ProductPage>(new ContentReference(int.Parse(productId)));

            var cartItem = new CartItemEntity();
            cartItem.ProductId = int.Parse(productId);
            cartItem.Name = productPage.Name;
            cartItem.Price = int.Parse(productPage.Price);

            cartRepository.Add(cartItem);
            return View("~/Views/ProductPage/Index.cshtml", currentPage);
        }

        [HttpPost]
        public ActionResult Submit(string productId)
        {
            var cartRepository = new CartRepository();
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var productPage = contentRepository.Get<ProductPage>(new ContentReference(int.Parse(productId)));

            var cartItem = new CartItemEntity();
            cartItem.ProductId = int.Parse(productId);
            cartItem.Name = productPage.Name;
            cartItem.Price = int.Parse(productPage.Price);

            cartRepository.Add(cartItem);

            return View("~/Views/ProductPage/Index.cshtml", productPage);
        }
    }
}