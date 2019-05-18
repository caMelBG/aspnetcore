using huncho.Data.Models;
using huncho.Data.Repositories;
using huncho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace huncho.Controllers
{
    public class OrderController : Controller
    {
        private IRepository<Order> _orderRepository;
        private Cart _cart;

        public OrderController(IRepository<Order> orderRepository, Cart cart)
        {
            _orderRepository = orderRepository;
            _cart = cart;
        }

        [Authorize]
        public ViewResult Index()
        {
            return View(_orderRepository.GetAll().Where(o => !o.Shipped));
        }

        [Authorize]
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                order.Shipped = true;
                _orderRepository.Insert(order);
            }
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Items = _cart.Items.ToArray();
                _orderRepository.Insert(order);
                return RedirectToAction(nameof(Completed));
            }
            return View(order);    
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
