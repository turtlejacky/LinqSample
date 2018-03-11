using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LinqTests
{
    internal class RepositoryFactory
    {
        public IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product{Id=1, Cost=11, Price=110, Supplier="Odd-e" },
                new Product{Id=2, Cost=21, Price=210, Supplier="Yahoo" },
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" },
                new Product{Id=5, Cost=51, Price=510, Supplier="Momo" },
                new Product{Id=6, Cost=61, Price=610, Supplier="Momo" },
                new Product{Id=7, Cost=71, Price=710, Supplier="Yahoo" },
            };
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Andy", Role=RoleType.OP, MonthSalary=80, Age=22, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };
        }
    }

    internal class Employee
    {
        public string Name { get; set; }
        public RoleType Role { get; set; }
        public int MonthSalary { get; set; }
        public int Age { get; set; }
        public double WorkingYear { get; set; }
    }

    internal class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Cost { get; set; }
        public string Supplier { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}

internal enum RoleType
{
    Engineer,
    OP,
    Manager
}