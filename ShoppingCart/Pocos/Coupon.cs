using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Pocos
{
    public class Coupon
    {
        public  string Code { get; set; }
        public IDiscountStrategy DiscountStrategy { get; set; }

        //public Coupon(string code, IDiscountStrategy discountStrategy)
        //{
        //    Code = code;
        //    DiscountStrategy = discountStrategy;
        //}
    }
}
