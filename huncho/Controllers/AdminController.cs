using huncho.Data.Models;
using huncho.Data.Repositories;
using huncho.Data.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace huncho.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IRepository<Product> _productRepository;

        public AdminController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();

            return View(products);
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            _productRepository.Delete(productId);
            _productRepository.Save();
            TempData["Message"] = $"Product was deleted";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => View(nameof(Edit), new Product());

        [HttpGet]
        public IActionResult Edit(int productId)
        {
            var product = _productRepository.GetById(productId);

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                this.SaveProduct(product);
                TempData["Message"] = $"{product.Name} has been saved";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }

        private void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _productRepository.Insert(product);
            }
            else
            {
                var dbEntry = _productRepository.GetById(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _productRepository.Save();
        }
    }
}