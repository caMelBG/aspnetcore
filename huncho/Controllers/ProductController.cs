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

        public ViewResult Index([FromRoute] int page = 1)
        {
            var products = _productRepository
                .GetAll()
                .OrderBy(x => x.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var pageInfo = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _productRepository.GetAll().Count()
            };

            var viewModel = new ProductsListViewModel
            {
                Products = products,
                PageInfo = pageInfo
            };

            return View(viewModel);
        }
    }
}
