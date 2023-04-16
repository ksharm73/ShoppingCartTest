using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Interfaces;
using ShoppingCart.Pocos;

namespace ShoppingCart.Implementation
{
    public class Calculator
    {
        private readonly IRepository<Product> _productStore;
        private readonly IShippingCalculator _shippingCalculator;
        //private readonly IDiscountStrategy _discountStrategy;
        private readonly IRepository<Coupon> _couponStore;

        /*public Calculator(IShippingCalculator shippingCalculator, IRepository<Product> productStore)
        {
            _productStore = productStore;
            _shippingCalculator = shippingCalculator;
        }*/
        public Calculator(IRepository<Coupon> couponstore,IRepository<Product> productstore, IShippingCalculator shippingCalculator)
        {
            //_discountStrategy = discountStrategy;
            _productStore = productstore;
            _couponStore = couponstore;
            _shippingCalculator = shippingCalculator;
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
        public decimal CalculateTotal(IList<CartItem> cart, string couponCode)
        {
            var _coupon = _couponStore.GetByName(couponCode);
            decimal runningTotal = 0;
            foreach (var item in cart)
            {
                decimal discountAmount = 0;
                var product = _productStore.Get(item.ProductId);
                if (product != null)
                {
                    decimal subtotal = product.Price;
                    if(_coupon != null)
                        discountAmount = _coupon.DiscountStrategy.CalculateDiscount(new List<Product> { product});
                   
                    runningTotal += item.UnitQuantity * (subtotal - discountAmount);
                }
            }
            decimal shippingCost = _coupon?.DiscountStrategy is FreeShippingDiscountStrategy ? 0 : _shippingCalculator.CalcShipping(runningTotal);
            return runningTotal+shippingCost;
        }
    }
}