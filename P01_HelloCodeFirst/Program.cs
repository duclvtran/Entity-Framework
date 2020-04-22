using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HelloCodeFirst
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                //Add(context, "Mot", "mot");
                //Add(context, "Hai", "hai");
                context.Database.CreateIfNotExists();
                Console.WriteLine("Before:");
                Retrieve(context);
                Console.WriteLine("Search:");
                SearchItem(context, "Barr");
                Console.WriteLine("Update:");
                SearchAndUpdateItem(context, "Barr");
                Delete(context);
                Console.WriteLine("After:");
                Retrieve(context);

                //Retrieve(context);

                //var person = new Person { FirstName = "Tran", LastName = "Vu" };
                //context.People.Add(person);
                //context.SaveChanges();
            }
            Console.ReadKey();
        }

        private static void Add(Context context, string firstname, string lastname)
        {
            var person = new Person { FirstName = firstname, LastName = lastname };
            context.People.Add(person);
            context.SaveChanges();
        }

        private static void Retrieve(Context context)
        {
            var people = context.People;
            foreach (var person in people)
            {
                Console.WriteLine($"[{person.PersonId}] {person.FirstName} {person.LastName}");
            }
        }

        private static void Update(Context context)
        {
            var person = context.People.FirstOrDefault();
            if (person != null)
            {
                person.FirstName = "Barrack1";
                person.LastName = "Obama";
                context.SaveChanges();
            }
        }

        private static void SearchItem(Context context, string str)
        {
            var people = context.People.Where(x => x.FirstName.Contains(str));

            foreach (var person in people)
            {
                Console.WriteLine($"[{person.PersonId}] {person.FirstName} {person.LastName}");
            }
        }

        private static void SearchAndUpdateItem(Context context, string str)
        {
            var people = context.People.Where(x => x.FirstName.Contains(str)).FirstOrDefault();
            people.LastName = "SOC";
            context.SaveChanges();
        }

        private static void Delete(Context context)
        {
            var people = context.People.FirstOrDefault();
            context.People.Remove(people);
            context.SaveChanges();
        }
    }

    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=S03_connectionString")
        {
        }

        public DbSet<Person> People { get; set; }
    }
}