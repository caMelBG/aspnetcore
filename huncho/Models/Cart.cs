using huncho.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace huncho.Models
{
    public class Cart
    {
        private List<CartLine> _items = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            var item = _items
                .Where(x => x.Product.ProductId == product.ProductId)
                .FirstOrDefault();

            if (item == null)
            {
                item = new CartLine
                {
                    Product = product,
                    Quantity = quantity
                };
                _items.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public virtual void RemoveItem(Product product)
        {
            _items.RemoveAll(x => x.Product.ProductId == product.ProductId);
        }

        public virtual decimal ComputeTotalValue()
        {
            return _items.Sum(x => x.Product.Price * x.Quantity);
        }

        public virtual void Clear() => _items.Clear();

        public virtual IEnumerable<CartLine> Items => _items;
    }

    public class CartLine
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
