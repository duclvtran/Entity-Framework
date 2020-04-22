using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04_Relationship_ManyToMany
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public IList<Email> Emails { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }

    public class Email
    {
        public int EmailId { get; set; }
        public string EmailAddress { get; set; }
        //public Person Person { get; set; }
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
        public Context() : base("name=ConnectionString")
        {
            var initializer = new MyInitializer();
            Database.SetInitializer(initializer);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Email3>()
            //.HasRequired<Person3>(e => e.Owner)
            //.WithMany(p => p.Email3s)
            //.HasForeignKey<int>(e => e.RefToPerson);

            // hoặc
            //modelBuilder.Configurations.Add(new PersonMap());
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                var trump = new Person { FirstName = "Donald", LastName = "Trump" };
                trump.Emails = new List<Email>
                {
                    new Email { EmailAddress = "donald.trump@gmail.com" },
                    new Email { EmailAddress = "trump.donald@yandex.ru" }
                };

                var obama = new Person { FirstName = "Barack", LastName = "Obama" };
                obama.Emails = new List<Email>
                {
                    new Email { EmailAddress = "barack.obama@gmail.com" },
                    new Email { EmailAddress = "obama.barack@yandex.ru" }
                };

                var mail = new Email { EmailAddress = "president@the_white_house.us" };
                trump.Emails.Add(mail);
                obama.Emails.Add(mail);

                context.People.Add(trump);
                context.People.Add(obama);
                context.SaveChanges();

                var people = context.People.ToArray();
                foreach (var p in people)
                {
                    Console.WriteLine($"First person: {p.FirstName} {p.LastName}");
                }
            }
        }
    }
}