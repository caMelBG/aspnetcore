using huncho.Data.Models;
using huncho.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace huncho.Views.Shared.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IRepository<Product> _productRepository;

        public NavigationMenuViewComponent(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            var categories = _productRepository
                .GetAll()
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
