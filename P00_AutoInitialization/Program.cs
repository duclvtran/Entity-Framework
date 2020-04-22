using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P00_AutoInitialization
{
    public class Person
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base()
        {
            var initializer = new DropCreateDatabaseIfModelChanges<Context>();
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
                Console.WriteLine($"Database exists? {context.Database.Exists()}");

                context.People.Add(new Person { FirstName = "Donald", LastName = "Trump" });
                context.People.Add(new Person { FirstName = "Barack", LastName = "Obama" });
                context.SaveChanges();

                var people = context.People.ToArray();

                Console.WriteLine("People list:");
                foreach (var p in people)
                {
                    Console.WriteLine($"{p.FirstName} {p.LastName}");
                }
            }
            Console.ReadKey();
        }
    }
}