using ShoppingCart.Interfaces;
using ShoppingCart.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Implementation
{
    public class SupplierDiscountStrategy : IDiscountStrategy
    {
        private string Supplier;
        private decimal DiscountPercentage;

        public SupplierDiscountStrategy(string supplier, decimal discountPercentage)
        {
            Supplier = supplier;
            DiscountPercentage = discountPercentage;
        }

        public decimal CalculateDiscount(List<Product> products)
        {
            decimal discountAmount = 0;
            foreach (var product in products)
            {
                if (product.Supplier == Supplier)
                {
                    discountAmount += product.Price * DiscountPercentage / 100;
                }
            }
            return discountAmount;
        }
    }

    public class FreeShippingDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(List<Product> products)
        {
            return 0;
        }
    }

    public class CategoryDiscountStrategy : IDiscountStrategy
    {
        private List<string> Categories;
        private decimal DiscountPercentage;

        public CategoryDiscountStrategy(List<string> categories, decimal discountPercentage)
        {
            Categories = categories;
            DiscountPercentage = discountPercentage;
        }

        public decimal CalculateDiscount(List<Product> products)
        {
            decimal discountAmount = 0;
            foreach (var product in products)
            {
                if (product.Categories.Intersect(Categories).Any())
                {
                    discountAmount += product.Price * DiscountPercentage / 100;
                }
            }
            return discountAmount;
        }
    }
}
