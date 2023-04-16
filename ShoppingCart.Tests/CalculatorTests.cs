using System.Collections.Generic;
using NUnit.Framework;
using ShoppingCart.Implementation;
using ShoppingCart.Interfaces;
using ShoppingCart.Pocos;

namespace ShoppingCart.Tests
{
    public class Tests
    {
        private ProductRepository _productRepository;
        private CouponRepository _couponRepository;
        private IShippingCalculator _shippingCalculator;

        [SetUp]
        public void Init()
        {
            
            var headPhones = new Product { Id = 1, Name = "Headphones", WholesalePrice = 2, Price = 10, Categories=new List<string> { "Accessory", "Electronic", "Audio" },Supplier="Apple" };
            var laptop = new Product { Id = 2, Name = "Laptop", WholesalePrice = 800, Price = 1000, Categories = new List<string> {  "Electronic" }, Supplier = "Dell" };
            var usbCable = new Product { Id = 3, Name = "Usb Cable", WholesalePrice = 180, Price = 4, Categories = new List<string> { "Accessory" }, Supplier = "Apple" };
            var monitor = new Product { Id = 4, Name = "Monitor", WholesalePrice = 180, Price = 100, Categories = new List<string> { "Electronic" }, Supplier = "HP" };


            _productRepository = new ProductRepository();
            _productRepository.Add(usbCable);
            _productRepository.Add(laptop);
            _productRepository.Add(headPhones);
            _productRepository.Add(monitor);

            var audioCategoryCoupon = new Coupon { Code = "AUDIO10", DiscountStrategy = new CategoryDiscountStrategy(new List<string> { "Audio" }, 10) };
            var appleSupplierCoupon = new Coupon { Code = "APPLE5", DiscountStrategy = new SupplierDiscountStrategy("Apple", 5) };
            var freeShippingCoupon = new Coupon { Code = "FREESHIPPING", DiscountStrategy = new FreeShippingDiscountStrategy() };
            //var invalidCategoryCoupon = new Coupon { Code = "XYZ", DiscountStrategy = new CategoryDiscountStrategy(new List<string> { "InvalidCategory" }, 10) };

            _couponRepository = new CouponRepository();
            _couponRepository.Add(audioCategoryCoupon);
            _couponRepository.Add(appleSupplierCoupon);
            _couponRepository.Add(freeShippingCoupon);
            //_couponRepository.Add(invalidCategoryCoupon);

            _shippingCalculator= new ShippingCalculator();
        }

        /*
         * Product Id 1 : Subtotal : 20 discount: 2 (10%) Total=18
         * Product Id 2 : Subtotal : 1000 discount:0 Total = 1000
         * Product Id 3 : Subtotal : 4 discount : 0 Total = 4
         * Product Id 4 : Subtotal : 100 discount : 0 Total = 100
         * Shipping cost : 0 (runnning total is greater than 40)
         * Total : 1122
         */
        [Test]
        public void CalculateTotal_WithAudio10Discount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3,carItem4 };            
            var calc = new Calculator(_couponRepository, _productRepository, _shippingCalculator);
            string couponCode = "AUDIO10";
            Assert.AreEqual(1122, calc.CalculateTotal(cart,couponCode));
        }

        /*
        * Product Id 1 : Subtotal : 20 discount: 1 (5%) Total=19
        * Product Id 2 : Subtotal : 1000 discount:0 Total = 1000
        * Product Id 3 : Subtotal : 4 discount : 0.2 (5%) Total = 3.8
        * Product Id 4 : Subtotal : 100 discount : 0 Total = 100
        * Shipping cost : 0 (runnning total is greater than 40)
        * Total : 1122.8
        */
        [Test]
        public void CalculateTotal_WithApple5Discount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3, carItem4 };
           // var discountStrategy = new SupplierDiscountStrategy("Apple", 5);
            var calc = new Calculator(_couponRepository, _productRepository,_shippingCalculator);
            string couponCode = "APPLE5";
            Assert.AreEqual(1122.8, calc.CalculateTotal(cart,couponCode));
        }

        /*
        * Product Id 1 : Subtotal : 20 discount: 0 Total=20
        * Product Id 2 : Subtotal : 1000 discount:0 Total = 1000
        * Product Id 3 : Subtotal : 4 discount : 0  Total = 4
        * Product Id 4 : Subtotal : 100 discount : 0 Total = 100
        * Shipping cost : 0 (free shipping coupon)
        * Total : 1124
        */
        [Test]
        public void CalculateTotal_WithFreeShippingDiscount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3, carItem4 };
           // var discountStrategy = new FreeShippingDiscountStrategy();
            var calc = new Calculator(_couponRepository, _productRepository, _shippingCalculator);
            string couponCode = "FREESHIPPING";
            Assert.AreEqual(1124, calc.CalculateTotal(cart, couponCode));
        }
        /*
        * Product Id 1 : Subtotal : 20 discount: 0 Total=20
        * Product Id 2 : Subtotal : 1000 discount:0 Total = 1000
        * Product Id 3 : Subtotal : 4 discount : 0  Total = 4
        * Product Id 4 : Subtotal : 100 discount : 0 Total = 100
        * Shipping cost : 0 (runnning total is greater than 40)
        * Total : 1124
        */
        [Test]
        public void CalculateTotal_WithInvalidDiscount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3, carItem4 };
           // var discountStrategy = new CategoryDiscountStrategy(new List<string> { "InvalidCategory" }, 10);
            var calc = new Calculator(_couponRepository, _productRepository, _shippingCalculator);
            string couponCode = "XYZ";
            Assert.AreEqual(1124, calc.CalculateTotal(cart, couponCode));
        }
    }
}