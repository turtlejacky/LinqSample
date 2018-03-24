using System;
using System.Collections.Generic;
using LinqTests;

namespace JoeyIsFat
{
    internal static class WithoutLinq
    {
        public static IEnumerable<Product> FindProduct(this IEnumerable<Product> products,
            Func<Product, bool> predicate)
        {
            // ReSharper disable once GenericEnumeratorNotDisposed
            //= products.Where(p => p.Price > 200 && p.Price < 500);
            foreach (var product in products)
            {
                if (predicate(product))
                {
                    yield return product;
                }
            }
        }
    }
}