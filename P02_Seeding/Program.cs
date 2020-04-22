using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_Seeding
{
    public class Person
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class MyInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            context.People.AddRange(new[]
            {
                new Person{ FirstName = "Donald", LastName = "Trump"},
                new Person{ FirstName = "Barack", LastName = "Obama"},
                new Person{ FirstName = "George", LastName = "Bush"},
                new Person{ FirstName = "Bill", LastName = "Clinton"},
            });
            context.SaveChanges();
        }
    }

    public class Context : DbContext
    {
        public Context() : base("name=S03_connectionString")
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
            //Title = "Data Seeding";

            using (var context = new Context())
            {
                var people = context.People.ToArray();
                foreach (var p in people)
                {
                    Console.WriteLine($"First person: {p.FirstName} {p.LastName}");
                }
            }

            Console.ReadKey();
        }
    }
}