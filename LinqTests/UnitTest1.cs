using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500);

            var expected = new List<Product>()
            {
                //todo
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}

internal class WithoutLinq
{
    public static IEnumerable<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary)
    {
        // ReSharper disable once GenericEnumeratorNotDisposed

        foreach (var product in products)
        {
        }

        throw new NotImplementedException();
    }
}

internal class YourOwnLinq
{
}