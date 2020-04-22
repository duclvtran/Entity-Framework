using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06_TableSplitting
{
    //[Table("People")] // sử dụng attribute Table hoặc fluent api ToTable
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Account Account { get; set; }
    }

    //[Table("People")] // sử dụng attribute Table hoặc fluent api ToTable
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual Person Person { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .ToTable("People") // sử dụng attribute Table hoặc fluent api ToTable
                .HasRequired(p => p.Account)
                .WithRequiredPrincipal(a => a.Person);

            modelBuilder.Entity<Account>()
                .ToTable("People"); // sử dụng attribute Table hoặc fluent api ToTable
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}