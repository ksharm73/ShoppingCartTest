using ShoppingCart.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Interfaces
{
    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(List<Product> products);
    }
}
