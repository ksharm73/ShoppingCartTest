using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Interfaces;
using ShoppingCart.Pocos;

namespace ShoppingCart.Implementation
{
    public class Calculator
    {
        private readonly IRepository<Product> _productStore;
        //private readonly IShippingCalculator _shippingCalculator;
        private readonly IDiscountStrategy _discountStrategy;

        /*public Calculator(IShippingCalculator shippingCalculator, IRepository<Product> productStore)
        {
            _productStore = productStore;
            _shippingCalculator = shippingCalculator;
        }*/
        public Calculator(IDiscountStrategy discountStrategy,IRepository<Product> productstore)
        {
            _discountStrategy = discountStrategy;
            _productStore = productstore;
        }

        /*public double Total(IList<CartItem> cart)
        {
            if (cart == null || cart.Count == 0) return 0;

            double runningTotal = 0;
            foreach (var item in cart)
            {
                var product = _productStore.Get(item.ProductId);
                if (product != null)
                {
                    runningTotal += item.UnitQuantity * product.Price;
                }
            }

            return runningTotal + _shippingCalculator.CalcShipping(runningTotal);
        }*/
        public decimal CalculateTotal(IList<CartItem> cart)
        {
            var Products = new List<Product>();
            decimal runningTotal = 0;
            foreach (var item in cart)
            {
                var product = _productStore.Get(item.ProductId);
                if (product != null)
                {
                    decimal subtotal = product.Price;
                    decimal discountAmount = _discountStrategy.CalculateDiscount(new List<Product> { product});
                   
                    runningTotal += item.UnitQuantity * (subtotal - discountAmount);
                }
            }
            decimal shippingCost = _discountStrategy is FreeShippingDiscountStrategy ? 0 : 10;
            return runningTotal+shippingCost;
        }
    }
}