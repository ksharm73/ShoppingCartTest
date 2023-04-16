using System.Collections.Generic;

namespace ShoppingCart.Pocos
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public double WholesalePrice { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; }
        public string Supplier { get; set; }
    }
}
