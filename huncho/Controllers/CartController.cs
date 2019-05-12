using huncho.Data.Models;
using huncho.Data.Repositories;
using huncho.Extensions;
using huncho.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace huncho.Controllers
{
    public class CartController : Controller
    {
        private IRepository<Product> _productRepository;

        public CartController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index(string returnUrl)
        {
            var viewModel = new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl,
            };
            return View(viewModel);
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _productRepository.GetAll()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _productRepository.GetAll()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveItem(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
