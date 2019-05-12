using huncho.Data.Models;
using huncho.Data.Repositories;
using huncho.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace huncho.Controllers
{
    public class ProductController : Controller
    {
        private const int PageSize = 4;
        private IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ViewResult Index(string category, int page = 1)
        {
            var products = _productRepository
                .GetAll()
                .Where(x => (x.Category != null && x.Category == category) || string.IsNullOrEmpty(category))
                .OrderBy(x => x.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var pageInfo = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
            };

            if (category == null)
            {
                pageInfo.TotalItems = _productRepository.GetAll().Count();
            }
            else
            {
                pageInfo.TotalItems = _productRepository.GetAll().Count(x => x.Category == category);
            }

            var viewModel = new ProductsListViewModel
            {
                Products = products,
                PageInfo = pageInfo,
                Category = category,
            };

            return View(viewModel);
        }
    }
}
