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
            var actual = products.JoeyWhere(product => product.IsTopSaleProducts());

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


        [TestMethod]
        public void ToHttps()
        {
            var urls = RepositoryFactory.GetUrls();
            IEnumerable<string> actual = WithoutLinq.ToHttps(urls);
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }


        [TestMethod]
        public void JoeyWhere_JoeySelect()
        {
            var employees = RepositoryFactory.GetEmployees();
            var sqlActual = from e in employees
                where e.Age < 25
                select $"{e.Role}:{e.Name}";

            var actual = employees
                .JoeyWhere(e => e.Age < 25)
                .JoeySelect(e => $"{e.Role}:{e.Name}");

            foreach (var titleName in actual)
            {
                Console.WriteLine(titleName);
            }

            var expected = new List<string>()
            {
                "OP:Andy",
                "Engineer:Frank",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }


        [TestMethod]
        public void Take()
        {
            var employees = RepositoryFactory.GetEmployees();
            var act = WithoutLinq.JoeyTake(employees, 2);
            var expected = new List<Employee>
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Tom", Role = RoleType.Engineer, MonthSalary = 140, Age = 33, WorkingYear = 2.6},
            };

            expected.ToExpectedObject().ShouldEqual(act.ToList());
        }

        [TestMethod]
        public void Skip()
        {
            var employees = RepositoryFactory.GetEmployees();
            var act = WithoutLinq.JoeySkip(employees, 6);
            var expected = new List<Employee>
            {
                new Employee {Name = "Frank", Role = RoleType.Engineer, MonthSalary = 120, Age = 16, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
            };

            expected.ToExpectedObject().ShouldEqual(act.ToList());
        }


        [TestMethod]
        public void Paging_GetSum()
        {
            var employees = RepositoryFactory.GetEmployees();
            var act = WithoutLinq.GetSum(employees, 3, x => x.MonthSalary);
            var expected = new int[] {620, 540, 370};
            expected.ToExpectedObject().ShouldEqual(act.ToArray());
        }


        [TestMethod]
        public void TakeWhile()
        {
            var employees = RepositoryFactory.GetEmployees();
            var act = WithoutLinq.TakeWhile(employees, 2, x => x.MonthSalary > 150);
            var expected = new List<Employee>
            {
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
            };
            expected.ToExpectedObject().ShouldEqual(act.ToList());
        }

        [TestMethod]
        public void SkipWhile()
        {
            var employees = RepositoryFactory.GetEmployees();
            var act = WithoutLinq.SkipWhile(employees, 3, x => x.MonthSalary < 150);
            var expected = new List<Employee>
            {
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
                new Employee {Name = "Mary", Role = RoleType.OP, MonthSalary = 180, Age = 26, WorkingYear = 2.6},
                new Employee {Name = "Frank", Role = RoleType.Engineer, MonthSalary = 120, Age = 16, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
            };
            expected.ToExpectedObject().ShouldEqual(act.ToList());
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