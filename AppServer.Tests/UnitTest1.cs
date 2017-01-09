using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppServer.Tests
{
    [TestClass]
    public class SimpleUnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            //string first_name = VKUser.ParseName(VkControllers.GetNameByID("41691507"), 1);
          //  Assert.AreEqual("Наталия", first_name);
        }
    }

   /* public class MyClass 
    {
        private IDiscountHelper discounter;
        private static int counter = 0;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
            System.Diagnostics.Debug.WriteLine(
                string.Format("Экземпляр класса LinqValueCalculator №{0} создан", ++counter));
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }*/
}
