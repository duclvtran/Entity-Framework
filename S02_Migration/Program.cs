using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S02_Migration
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
            //Command enable Migrations: enable-migrations –EnableAutomaticMigration:$true
            var initializer = new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>();
            Database.SetInitializer(initializer);
        }

        public DbSet<Person> people { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                //context.people.Add(new Person { FirstName = "mot", LastName = "mot" });
                //context.people.Add(new Person { FirstName = "hai", LastName = "hai" });
                //context.SaveChanges();

                var people = context.people.ToList();
                foreach (var item in people)
                {
                    Console.WriteLine($"{item.FirstName} {item.LastName}");
                }
            }
            Console.ReadKey();
        }
    }
}