using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P08_LocalData
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
            // Title = "Local Data";

            using (var context = new Context())
            {
                LoadQuery(context);
                Local(context);
                Load(context);
            }

            Console.ReadKey();
        }

        //Lay data tu Local khi chuong trinh chay
        private static void Local(Context context)
        {
            foreach (var p in context.People)
            {
                Console.WriteLine($"{p.FirstName}");
            }

            var count = context.People.Local.Count;
            Console.WriteLine($"Courses provided in Hogwarts: {count}");
        }

        //Chu dong lay du lieu voi phuong thuc Local
        private static void Load(Context context)
        {
            context.People.Load(); // chú ý using System.Data.Entity;
            var count = context.People.Local.Count;
            Console.WriteLine($"Courses provided in Hogwarts: {count}");
        }

        //Chu dong lay du lieu voi phuong thuc Local + Filter
        private static void LoadQuery(Context context)
        {
            var query = context.People.Where(c => c.Age >= 2);
            query.Load();
            var count = context.People.Local.Count;
            Console.WriteLine($"Courses provided in Hogwarts: {count}");
            foreach (var c in context.People.Local)
            {
                Console.WriteLine($"{c.FirstName} ({c.LastName} credits)");
            }
        }

        private static void LocalQuery(Context context)
        {
            context.People.Load();

            Console.WriteLine("### Sorted by name ###");
            var sortedCourses = context.People.Local
                .OrderBy(c => c.FirstName);
            foreach (var c in sortedCourses)
            {
                Console.WriteLine($"{c.FirstName} ({c.Age} credits)");
            }
            Console.WriteLine($"** {context.People.Local.Count} courses found **");

            Console.WriteLine("\r\n### Magic courses ###");
            var magicCourses = context.People.Local
                .Where(c => c.FirstName.Contains("Obama"));
            foreach (var c in magicCourses)
            {
                Console.WriteLine($"{c.FirstName} ({c.Age} credits)");
            }
            Console.WriteLine($"** {magicCourses.Count()} courses found **");

            Console.WriteLine("\r\n### Grouped by credit ###");
            var groups = context.People.Local
                .GroupBy(c => c.Age)
                .Select(
                    g => new
                    {
                        GroupName = $"{g.Key}-Credit Courses",
                        GroupItems = g.OrderBy(c => c.FirstName)
                    }
                );
            foreach (var g in groups)
            {
                Console.WriteLine($"+{g.GroupName}:");
                foreach (var c in g.GroupItems)
                {
                    Console.WriteLine($"  -> {c.FirstName}");
                }
            }
        }
    }
}