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
        }

        
        [Test]
        public void CalculateTotal_WithAudio10Discount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3,carItem4 };
            var discountStrategy = new CategoryDiscountStrategy(new List<string> { "Audio" }, 10);
            var calc = new Calculator(discountStrategy, _productRepository);
            Assert.AreEqual(1132, calc.CalculateTotal(cart));
        }

        [Test]
        public void CalculateTotal_WithApple5Discount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3, carItem4 };
            var discountStrategy = new SupplierDiscountStrategy("Apple", 5);
            var calc = new Calculator(discountStrategy, _productRepository);
            Assert.AreEqual(1134, calc.CalculateTotal(cart));
        }

        [Test]
        public void CalculateTotal_WithFreeShippingDiscount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3, carItem4 };
            var discountStrategy = new FreeShippingDiscountStrategy();
            var calc = new Calculator(discountStrategy, _productRepository);
            Assert.AreEqual(1114, calc.CalculateTotal(cart));
        }

        [Test]
        public void CalculateTotal_WithInvalidDiscount_ReturnsCorrectTotal()
        {
            var carItem1 = new CartItem { ProductId = 1, UnitQuantity = 2 };
            var carItem2 = new CartItem { ProductId = 2, UnitQuantity = 1 };
            var carItem3 = new CartItem { ProductId = 3, UnitQuantity = 1 };
            var carItem4 = new CartItem { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1, carItem2, carItem3, carItem4 };
            var discountStrategy = new CategoryDiscountStrategy(new List<string> { "InvalidCategory" }, 10);
            var calc = new Calculator(discountStrategy, _productRepository);
            Assert.AreEqual(1114, calc.CalculateTotal(cart));
        }
    }
}