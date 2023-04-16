using ShoppingCart.Interfaces;

namespace ShoppingCart.Implementation
{
    public class ShippingCalculator : IShippingCalculator
    {
        public decimal CalcShipping(decimal cartTotal)
        {
            if (cartTotal < 20) return 7;
            if (cartTotal < 40) return 5;
            return 0;
        }
    }
}