using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03_FluentAPIWithBuddy
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }

    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            Property(p => p.FirstName).HasMaxLength(30);
            Property(p => p.LastName).HasMaxLength(30);
            Property(p => p.MiddleName)
                .HasMaxLength(1)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                var success = context.Database.CreateIfNotExists();
                if (success)
                {
                    Console.WriteLine("New database created!");
                }
                else
                {
                    Console.WriteLine("Database exists or creation failed");
                }
            }
            Console.ReadKey();
        }
    }
}