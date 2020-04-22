using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05_Inheritance_TablePerType
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [Table("Students")]
    public class Student : Person
    {
        public string Major { get; set; }
        public string Specialization { get; set; }
    }

    [Table("Teachers")]
    public class Teacher : Person
    {
        public string Department { get; set; }
        public string Qualification { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
        }

        public DbSet<Person> People { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            //Title = "Table per Type";
            using (var ctx = new Context())
            {
                if (!ctx.Database.Exists())
                {
                    ctx.Database.Initialize(false);

                    ctx.People.AddRange(new Student[]
                    {
                        new Student { FirstName = "Donald", LastName = "Trump", Major = "Computer Science", Specialization = "Information Systems"},
                        new Student { FirstName = "Barack", LastName = "Obama", Major = "Computer Science", Specialization = "Sofware Engineering"},
                        new Student { FirstName = "George", LastName = "Bush", Major = "Computer Science", Specialization = "System Administration"},
                                            });

                    ctx.People.AddRange(new Teacher[]
                    {
                        new Teacher { FirstName = "Vladimir", LastName = "Putin", Department = "KGB", Qualification = "PhD"},
                        new Teacher { FirstName = "Boris", LastName = "Yeltsin", Department = "KGB", Qualification = "PhD"},
                    });

                    ctx.SaveChanges();
                }

                // Polymorphic Queries
                var people = ctx.People.ToList();
                Console.WriteLine("People:");
                foreach (var p in people)
                {
                    Console.Write($"{p.FirstName} {p.LastName}");
                    if (p is Teacher)
                    {
                        Console.WriteLine(" (teacher)");
                    }
                    else
                    {
                        Console.WriteLine(" (student)");
                    }
                }

                // Non-polymorphic Queries
                var students = ctx.People.OfType<Student>().ToList();
                Console.WriteLine("\r\nStudents:");
                foreach (var s in students)
                {
                    Console.WriteLine($"{s.FirstName} {s.LastName}, studying {s.Major}, specialized in {s.Specialization}");
                }

                var teachers = ctx.People.OfType<Teacher>().ToList();
                Console.WriteLine("\r\nTeachers:");
                foreach (var t in teachers)
                {
                    Console.WriteLine($"{t.FirstName} {t.LastName} ({t.Qualification}), working at {t.Department}");
                }
            }

            Console.ReadKey();
        }
    }
}