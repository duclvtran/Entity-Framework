using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P07_SimpleQueries
{
    public class Person
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class MyInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            context.People.AddRange(new[]
            {
                new Person{ FirstName = "Donald", LastName = "Trump", Age=1},
                new Person{ FirstName = "Barack", LastName = "Obama", Age=2},
                new Person{ FirstName = "George", LastName = "Bush", Age=1},
                new Person{ FirstName = "Bill", LastName = "Clinton", Age=3},
            });
            context.SaveChanges();
        }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
            var initializer = new MyInitializer();
            Database.SetInitializer(initializer);
        }

        public DbSet<Person> People { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                var people = context.People.ToArray();
                foreach (var p in people)
                {
                    Console.WriteLine($"({p.Id}) {p.FirstName} {p.LastName}, Age {p.Age}");
                }

                //Sắp xếp dữ liệu với LINQ to Entities
                var query = context.People
                    .OrderBy(c => c.Age)
                    .ThenBy(c => c.LastName);
                Console.WriteLine("### Sắp xếp dữ liệu với LINQ to Entities ###");
                foreach (var c in query)
                {
                    Console.WriteLine($"{c.FirstName} {c.LastName}, {c.LastName}");
                }

                //Lọc dữ liệu với LINQ to Entities
                var query1 = context.People
                    .Where(c => c.Age >= 2);
                //.Where(c => c.Credit >= 4) //.Where(c => c.Name.Contains("magic"));
                //.OrderBy(c => c.Credit)
                //.ThenBy(c => c.Name);

                Console.WriteLine("### Lọc dữ liệu với LINQ to Entities ###");
                foreach (var c in query1)
                {
                    Console.WriteLine($"({c.Id}) {c.FirstName} {c.LastName}, Age {c.LastName}");
                }

                //Projection với LINQ to Entities
                var query2 = context.People
                       .Where(c => c.Age > 3)
                       .OrderBy(c => c.FirstName)
                       .Select(c => c.FirstName);
                foreach (var c in query)
                {
                    Console.WriteLine($"{c}");
                }

                //Sử dụng Find
                var course = context.People.Find(13); // tìm kiếm theo Id = 13
                Console.WriteLine($"Course found: {course.FirstName} ({course.LastName} credits)");

                //Nhóm dữ liệu với LINQ to Entities
                var result = context.People.GroupBy(c => c.Age);
                foreach (var r in result)
                {
                    Console.WriteLine($"{r.Key}-Credit Courses:");
                    foreach (var c in r)
                    {
                        Console.WriteLine($"  -> {c.FirstName}");
                    }
                }
            }

            Console.ReadKey();
        }
    }
}