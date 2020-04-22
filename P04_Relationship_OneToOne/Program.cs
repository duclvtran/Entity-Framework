using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04_Relationship_OneToOne
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Account Account { get; set; }
    }

    public class Account
    {
        //Cách 1: Khai báo attribute ForeignKey
        [ForeignKey("Person")]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual Person Person { get; set; }
    }

    public class Contact
    {
        //Cách 1: Khai báo attribute ForeignKey
        [ForeignKey("Person")]
        public int Id { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }

        public virtual Person Person { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
            var initializer = new DropCreateDatabaseAlways<Context>();
            Database.SetInitializer(initializer);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        //Cách 2: fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOptional(p => p.Contact)
                .WithRequired(c => c.Person);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.Initialize(force: false);

                var trump = new Person
                {
                    FirstName = "Donald",
                    LastName = "Trump",
                    Contact = new Contact
                    {
                        Address = "Washington DC",
                        Email = "donald.trump@gmail.com",
                        Phone = "0123.456.789"
                    },
                    Account = new Account
                    {
                        UserName = "trump",
                        Password = "******"
                    }
                };
                context.People.Add(trump);

                var obama = new Person { FirstName = "Barack", LastName = "Obama" };
                var contact = new Contact { Address = "Washington DC", Email = "barack.obama@gmail.com" };
                var account = new Account { UserName = "obama", Password = "***" };
                obama.Account = account;
                obama.Contact = contact;
                context.People.Add(obama);

                var bush = new Person { FirstName = "George", LastName = "Bush" };
                var bushContact = new Contact { Address = "Washington DC", Email = "george.bush@gmail.com", Person = bush };
                var bushAccount = new Account { UserName = "bush", Person = bush };
                bush.Account = bushAccount;
                bush.Contact = bushContact;
                context.People.Add(bush);

                context.SaveChanges();

                foreach (var p in context.People)
                {
                    Console.WriteLine($"{p.FirstName} {p.LastName}, from {p.Contact.Address}");
                    Console.WriteLine($"Account: {p.Account.UserName}");
                }

                foreach (var c in context.Contacts)
                {
                    Console.WriteLine($"{c.Person.FirstName} {c.Person.LastName}, from {c.Address}");
                    Console.WriteLine($"Account: {c.Person.Account.UserName}");
                }
            }

            Console.ReadKey();
        }
    }
}