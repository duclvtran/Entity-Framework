using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_ManualMigration
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Alias { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
            var initializer = new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>();
            Database.SetInitializer(initializer);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
        }
    }
}