using huncho.Data.Models;
using System.Collections.Generic;

namespace huncho.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PageInfo PageInfo{ get; set; }

        public string Category { get; set; }
    }
}
