using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrmComputerModel.Models.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {
            //Arrange-Input
            Customer customer = new Customer("testuser");
            Product product1 = new Product("pr1", 100, 2);
            Product product2 = new Product("pr2", 200, 20);
            Cart cart = new Cart(customer);
            var Expectedcart = new List<Product>()
            {
                product1,product1,product2
            };

            //Act-Work
            cart.AddProduct(product1);
            cart.AddProduct(product1);
            cart.AddProduct(product2);
            var cartresult = cart.GetAll();
            //Assert-Compare
            Assert.AreEqual(Expectedcart.Count, cart.GetAll().Count);
            for (int i = 0; i < Expectedcart.Count; i++)
            {
                Assert.AreEqual(Expectedcart[i], cartresult[i]);
            }
        }
    }
}