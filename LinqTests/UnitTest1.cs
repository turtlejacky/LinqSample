using System;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500, "Odd-e");

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void TestMyWhere()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyWhere(p => p.Price > 200 && p.Price < 500 && p.Supplier == "Odd-e").ToList();

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void TestEmployee()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees.MyWhere(e => e.Age > 25 && e.Age <= 40).ToList();

            var expected = new List<Employee>()
            {
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Test_SetUrls()
        {
            var urls = RepositoryFactory.GetUrls();

            var actual = urls.MySelect(url => url.Replace("http:", "https:") + ":80").ToList();

            var expected = new List<String>()
            {
                 "https://tw.yahoo.com:80",
                 "https://facebook.com:80",
                 "https://twitter.com:80",
                 "https://github.com:80"
        };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Test_GetLength()
        {
            var urls = RepositoryFactory.GetUrls();

            var actual = urls.MySelect(x => x.Length).ToList();

            var expected = new List<int>()
            {
                19,20,19,17
            };
            expected.ToExpectedObject().ShouldEqual(actual);
            urls.Select(x => x.Length);
        }
        [TestMethod]
        public void Test_Add91Port()
        {
            var urls = RepositoryFactory.GetUrls();

            var actual = urls.Add91Prot().ToList();

            var expected = new List<string>()
            {
                "http://tw.yahoo.com:91",
                "https://facebook.com",
                "https://twitter.com:91",
                "http://github.com"
            };
            expected.ToExpectedObject().ShouldEqual(actual);
            urls.Select(x => x.Length);
        }
        [TestMethod]
        public void Test_GetEngineer_Salary()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees.MyWhere(x => x.Role == RoleType.Engineer)
                .MySelect(x => x.MonthSalary);

            var expected = new List<int>()
            {
                100,140,280,120,250
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        [TestMethod]
        public void Test_TakeProduct()
        {
            var products = RepositoryFactory.GetProducts();

            int count = 3;
            var actual = products.MyTake(count);

            var expected = products.Take(count).ToList();
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        [TestMethod]
        public void TestEmployeeSkip()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees.MySkip(6).ToList();

            var expected = new List<Employee>()
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void TestEmployeeSkipWhile()
        {
            var employees = RepositoryFactory.GetProducts();

            var actual = employees.MySkipWhile(x => x.Price < 300).ToList();

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" },
                new Product{Id=5, Cost=51, Price=510, Supplier="Momo" },
                new Product{Id=6, Cost=61, Price=610, Supplier="Momo" },
                new Product{Id=7, Cost=71, Price=710, Supplier="Yahoo" },
                new Product{Id=8, Cost=18, Price=780, Supplier="Yahoo" },
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void TestProductMySun()
        {
            var employees = RepositoryFactory.GetProducts();

            var actual = employees.MySum(x => x.Price);

            var expected = 3650;

            Assert.IsTrue(actual == expected);
            //expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void TestEmployeeTakeWhile()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees.MyTakeWhile(x => x.Age > 30).ToList();

            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void TestProductGroupMySun()
        {
            var employees = RepositoryFactory.GetProducts();

            var actual = employees.MyGroupSum(3, x => x.Price);

            var expected = new List<int>
            {
                630, 1530, 1490
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        [TestMethod]
        public void TestProductGroupMySun2()
        {
            var products = RepositoryFactory.GetProducts();

            var actual = products.MyGroupSum(4, x => x.Price);

            var expected = new List<int>
            {
                1040,2610
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        [TestMethod]
        public void Any_has_record()
        {
            var employees = RepositoryFactory.GetProducts();

            var actual = employees.MyAny();

            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void TestAll()
        {
            var products = RepositoryFactory.GetProducts();

            var actual = products.MyAll(x => x.Price > 200);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void TestAll2()
        {
            var products = RepositoryFactory.GetProducts();

            var actual = products.MyAll(x => x.Price > 100);

            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void TestDistinct()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees.MySelect(x => x.Role).MyDistinct().ToList();
            var expected = employees.Select(x => x.Role).Distinct().ToList();
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

    }

    internal class WithoutLinq
    {
        public static List<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary, string supplier)
        {
            var ans = new List<Product>();
            foreach (var item in products)
            {
                if (item.Price > lowBoundary && item.Price < highBoundary && item.Supplier == supplier)
                    ans.Add(item);
            }
            return ans;
        }
    }

    static class EnumExtentions
    {
        public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> func)
        {
            foreach (var item in items)
            {
                if (func(item))
                    yield return item;
            }
        }

        public static IEnumerable<TResult> MySelect<TResult, TSource>(this IEnumerable<TSource> urls,
            Func<TSource, TResult> Selector)
        {
            foreach (var url in urls)
                yield return Selector(url);
        }


        public static IEnumerable<string> Add91Prot(this IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                yield return url.Contains("tw") ? url + ":91" : url;
            }
        }

        public static IEnumerable<TResult> MySelect<TResult, TSource>(this IEnumerable<TSource> urls,
            Func<TSource, int, TResult> Selector)
        {
            int index = 0;
            foreach (var url in urls)
            {
                yield return Selector(url, index);
                index++;
            }
        }


        public static IEnumerable<TSource> MyTake<TSource>(this IEnumerable<TSource> products, int count)
        {
            var i = 0;
            foreach (var product in products)
            {
                if (i < count)
                    yield return product;
                i++;
            }

        }

        public static IEnumerable<TSource> MySkip<TSource>(this IEnumerable<TSource> emps, int count)
        {
            if (emps.Count() < count)
            {
            }

            int index = 0;
            foreach (var emp in emps)
            {
                if (index >= count)
                    yield return emp;
                index++;
            }
        }

        public static IEnumerable<TSource> MySkipWhile<TSource>(this IEnumerable<TSource> sources,
            Func<TSource, bool> func)
        {
            bool IsKeepRunning = true;
            foreach (var source in sources)
            {
                if (!func(source) || IsKeepRunning == false)
                {
                    yield return source;
                    IsKeepRunning = false;
                }
            }
        }

        public static int MySum<TSource>(this IEnumerable<TSource> sources, Func<TSource, int> func)
        {
            int result = 0;
            foreach (var source in sources)
            {
                result += func(source);
            }
            return result;
        }

        public static IEnumerable<int> MyGroupSum<TSource>(this IEnumerable<TSource> sources, int nums,
            Func<TSource, int> func)
        {
            var i = 1;
            var result = 0;
            foreach (var source in sources)
            {
                result += func(source);
                if (i % nums == 0)
                {
                    yield return result;
                    result = 0;
                }
                i++;

            }
            if (sources.Count() % nums != 0)
                yield return result;

        }


        public static IEnumerable<TSource> MyTakeWhile<TSource>(this IEnumerable<TSource> sources,
            Func<TSource, bool> func)
        {
            bool IsKeepRunning = true;
            foreach (var source in sources)
            {
                if (IsKeepRunning && func(source))
                {
                    yield return source;
                }
                else
                {
                    IsKeepRunning = false;
                }
            }
        }


        public static bool MyAny<TSource>(this IEnumerable<TSource> sources)
        {
            return sources.GetEnumerator().MoveNext();
        }
        public static bool MyAll<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> func)
        {

            foreach (var source in sources)
            {
                if (!func(source))
                    return false;
            }
            return true;
        }
        public static IEnumerable<TSource> MyDistinct<TSource>(this IEnumerable<TSource> sources)
        {
            Dictionary<TSource, int> mydic = new Dictionary<TSource, int>();
            foreach (var source in sources)
            {
                if (!mydic.ContainsKey(source))
                {
                    yield return source;
                    mydic.Add(source, 1);
                }
            }
        }
    }
}
