using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrmComputerModel.Models.Tests
{
    [TestClass()]
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            //AAA
            //Arrange-input
            Product product1 = new Product()
            {
                ProductName = "Test1",
                ProductCount = 10,
                ProductPrice = 100,
                ProductId = 1
            };
            Product product2 = new Product()
            {
                ProductName = "Test2",
                ProductCount = 20,
                ProductPrice = 200,
                ProductId = 2
            };
            var seller = new Seller()
            {
                SellerId = 1,
                SellerName = "testseller",
            };
            var customer1 = new Customer()
            {
                CustomerId = 1,
                CustomerName = "testuser",
            };
            var customer2 = new Customer()
            {
                CustomerId = 2,
                CustomerName = "testuser2",
            };
            var desk1 = new CashDesk(1, seller, null)
            {
                MaxQueueLength = 10
            };
            var desk2 = new CashDesk(2, seller, null)
            {
                MaxQueueLength = 10
            };
            var desk3 = new CashDesk(3, seller, null)
            {
                MaxQueueLength = 10
            };
            var cart1 = new Cart(customer1);
            cart1.AddProduct(product1);
            cart1.AddProduct(product1);
            cart1.AddProduct(product2);

            var cart2 = new Cart(customer2);
            cart2.AddProduct(product2);
            cart2.AddProduct(product2);
            cart2.AddProduct(product2);

            //Act-work
            for (int i = 0; i < 12; i++)
            {
                desk1.Enqueue(new Cart(new Customer(i.ToString())));
            }
            desk2.Enqueue(cart1);
            desk3.Enqueue(cart2);
            var res1 = desk2.Dequeue();
            var res2 = desk3.Dequeue();

            //Assert-compare
            Assert.IsTrue(desk1.ExitCustomer == 1);
            Assert.IsTrue(res1 == (decimal)400.0);
            Assert.IsTrue(res2 == (decimal)600.0);
            Assert.IsTrue(product1.ProductCount == 8);
            Assert.IsTrue(product2.ProductCount == 16);
        }
    }
}