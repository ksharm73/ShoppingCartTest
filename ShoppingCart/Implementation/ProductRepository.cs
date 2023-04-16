using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Interfaces;
using ShoppingCart.Pocos;

namespace ShoppingCart.Implementation
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly List<Product> _products = new List<Product>();
        public void Add(Product item)
        {
            _products.Add(item);
        }

        public Product Get(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllById(List<int> id)
        {
            return _products.Where(x => id.Contains(x.Id)).ToList();
        }

        public Product GetByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}