using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04_Relationship_ManyToManyWithPayload
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<PersonCompany> Companies { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PersonCompany> People { get; set; }
    }

    public class PersonCompany
    {
        public int Id { get; set; }

        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public bool Current { get; set; }
        public string Position { get; set; }

        public Person People { get; set; }
        public Company Companies { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
            var initializer = new DropCreateDatabaseAlways<Context>();
            Database.SetInitializer(initializer);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Company> Companies { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.Initialize(true);

                var trump = new Person { LastName = "Trump", Companies = new List<PersonCompany>() };
                var obama = new Person { LastName = "Obama", Companies = new List<PersonCompany>() };
                var bush = new Person { LastName = "Bush", Companies = new List<PersonCompany>() };

                var ibm = new Company { Name = "IBM", People = new List<PersonCompany>() };
                var intel = new Company { Name = "Intel", People = new List<PersonCompany>() };
                var amd = new Company { Name = "AMD", People = new List<PersonCompany>() };

                trump.Companies.Add(new PersonCompany { Companies = ibm, FromYear = 2000, ToYear = 2010 });
                intel.People.Add(new PersonCompany { People = bush, FromYear = 2010, ToYear = 2020 });
                intel.People.Add(new PersonCompany { People = obama, FromYear = 2011, ToYear = 2021 });

                context.People.Add(trump);
                context.People.Add(obama);
                context.People.Add(bush);

                context.Companies.Add(ibm);
                context.Companies.Add(intel);
                context.Companies.Add(amd);

                context.SaveChanges();

                Console.WriteLine("Working history");
                foreach (var p in context.People)
                {
                    Console.WriteLine($"{p.LastName}'s working history:");
                    foreach (var pc in p.Companies)
                        Console.WriteLine($"{pc.Companies.Name}: From {pc.FromYear} to {pc.ToYear}");
                }

                Console.WriteLine("\r\nCompany history");
                foreach (var c in context.Companies)
                {
                    Console.WriteLine($"{c.Name} history:");
                    foreach (var pc in c.People)
                        Console.WriteLine($"{pc.People.LastName}: From {pc.FromYear} to {pc.ToYear}");
                }
            }
            Console.ReadKey();
        }
    }
}