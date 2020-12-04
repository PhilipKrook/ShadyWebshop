using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.ServiceLocation;
using LIAuppgift.Models.Pages;

namespace LIAuppgift.Controllers
{
    public class ProductPageController : PageController<ProductPage>
    {
        public ActionResult Index(ProductPage currentPage)
            {            
            return View("~/Views/ProductPage/Index.cshtml", currentPage);
        }

        [HttpPost]
        public ActionResult Index(string productId)
        {
            var cartRepository = new CartRepository();
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var productPage = contentRepository.Get<ProductPage>(new ContentReference(int.Parse(productId)));
            this.Request.Cookies;
            var cartItem = new CartItemEntity();
            cartItem.ProductId = int.Parse(productId);
            cartItem.Name = productPage.Name;
            cartItem.Price = int.Parse(productPage.Price);

            cartRepository.Add(cartItem);

            return View("~/Views/ProductPage/Index.cshtml", productPage);
        }
    }
}