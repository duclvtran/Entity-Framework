using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06_EntitySplitting
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public byte[] Photo { get; set; }
        public byte[] CurriculumVitae { get; set; }
    }

    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            Map(p =>
            {
                p.Properties(ph => new { ph.Photo, ph.CurriculumVitae });
                p.ToTable("PeopleData");
            });

            Map(p =>
            {
                p.Properties(ph => new { ph.FirstName, ph.LastName });
                p.ToTable("People");
            });
        }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
            var initializer = new DropCreateDatabaseAlways<Context>();
            Database.SetInitializer(initializer);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
        }

        public DbSet<Person> People { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.Initialize(false);

                var trump = new Person
                {
                    FirstName = "Donald",
                    LastName = "Trump",
                    Photo = File.ReadAllBytes("C:\\Users\\default.DESKTOP-P7LEJI3\\Desktop\\VuTDL\\TEMP\\1.jpeg")
                };

                context.People.Add(trump);
                context.SaveChanges();
            }

            Console.Write("Press any key ...");
            Console.ReadKey();

            using (var context = new Context())
            {
                var trump = context.People.FirstOrDefault();
                File.WriteAllBytes("1.jpeg", trump.Photo);
                System.Diagnostics.Process.Start("1.jpeg");
            }

            Console.ReadKey();
        }
    }
}