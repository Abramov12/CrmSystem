
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrmComputerModel.Models.Tests
{
    [TestClass()]
    public class ShopComputerModelTests
    {
        [TestMethod()]
        public void StartTest()
        {
            //AAA
            //Arrange-input
            var model = new ShopComputerModel();
            model.Start();
            Thread.Sleep(1000);
            //Act-work

            //Assert-compare

        }
    }
}