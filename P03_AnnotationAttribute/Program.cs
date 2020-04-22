using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03_AnnotationAttribute
{
    [Table("Person", Schema = "main")]
    public class Person
    {
        [Key]
        [Column("person_code", Order = 1)]
        public int PersonCode { get; set; }

        [MaxLength(30, ErrorMessage = "Họ không dài quá 30 ký tự")]
        [Column("first_name", Order = 2)]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "Tên không dài quá 30 ký tự")]
        [Column("last_name", Order = 4)]
        public string LastName { get; set; }

        [StringLength(1, MinimumLength = 1)]
        [Column("middle_name", TypeName = "char", Order = 3)]
        public string MiddleName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
            //disable initializer
            //Database.SetInitializer<Context>(null);

            //var initializer = new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>();
            //Database.SetInitializer(initializer);
        }

        public DbSet<Person> People { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                //context.People.AddRange(new[]
                //{
                //    new Person{ FirstName = "Donald", LastName = "Trump"},
                //    new Person{ FirstName = "Barack", LastName = "Obama"},
                //    new Person{ FirstName = "George", LastName = "Bush"},
                //    new Person{ FirstName = "Bill", LastName = "Clinton"},
                //});
                //context.SaveChanges();

                var people = context.People.ToList();
                foreach (var item in people)
                {
                    Console.WriteLine($"{item.FirstName} {item.LastName}{item.FullName}");
                }
            }
            Console.ReadKey();
        }
    }
}