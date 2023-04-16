using ShoppingCart.Interfaces;
using ShoppingCart.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Implementation
{
    public class CouponRepository : IRepository<Coupon>
    {
        private readonly List<Coupon> _coupons = new List<Coupon>();
        public void Add(Coupon item)
        {
            _coupons.Add(item);
        }

        public Coupon Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Coupon> GetAll()
        {
            return _coupons;
        }

        public Coupon GetByName(string name)
        {
            return _coupons.Where(x => x.Code == name).FirstOrDefault();
        }
    }
}
