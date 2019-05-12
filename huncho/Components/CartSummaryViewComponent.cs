using Microsoft.AspNetCore.Mvc;

namespace huncho.Views.Shared.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Models.Cart _cart;

        public CartSummaryViewComponent(Models.Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
