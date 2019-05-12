using huncho.Data.Models;
using huncho.Data.Repositories;
using huncho.Extensions;
using huncho.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace huncho.Controllers
{
    public class CartController : Controller
    {
        private IRepository<Product> _productRepository;
        private Cart _cart;

        public CartController(IRepository<Product> productRepository, Cart cart)
        {
            _productRepository = productRepository;
            _cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            var viewModel = new CartIndexViewModel
            {
                Cart = _cart,
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
                _cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _productRepository.GetAll()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                _cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [Obsolete]
        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        [Obsolete]
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
