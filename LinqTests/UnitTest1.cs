using System;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using JoeyIsFat;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts().ToList();
            var actual = products.FindProduct(product => product.IsTopSaleProducts());

            var expected = new List<Product>()
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }


        [TestMethod]
        public void find_products_that_price_between_200_and_500_by_where()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Where(p => p.Price > 200 && p.Price < 500 && p.Cost > 30);

            var expected = new List<Product>()
            {
                //new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
    }
}

internal static class YourOwnLinq
{
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,
        Predicate<TSource> predicate)
    {
        var index = 0;

        foreach (var item in source)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,
        Func<TSource, int, bool> predicate)
    {
        var index = 0;

        foreach (var item in source)
        {
            if (predicate(item, index))
            {
                yield return item;
            }
        }
    }
}