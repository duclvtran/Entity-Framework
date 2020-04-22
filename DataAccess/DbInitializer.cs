using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<UniversityContext>
    {
        protected override void Seed(UniversityContext context)
        {
            var hogwarts = new School { Name = "Hogwarts" };
            context.Schools.Add(hogwarts);

            var departments = new Department[]
            {
            new Department { Name = "Information Technology", Office = "1011"},
            new Department { Name = "Computer Science", Office = "1012"},
            new Department { Name = "Networks and Commnunications", Office = "1013"}
            };
            hogwarts.Departments = departments;
            context.Departments.AddRange(departments);

            var specializations = new Specialization[]
            {
            new Specialization { Name = "Computer Networks"},
            new Specialization { Name = "Computer Systems"},
            new Specialization { Name = "Computer Science"},
            new Specialization { Name = "Cyber Security"},
            };
            context.Specializations.AddRange(specializations);

            var students = new Student[]
            {
            new Student { FirstName = "Harry", LastName = "Potter", Group = "Gryffindor", Specialization = specializations[0]},
            new Student { FirstName = "Hermione", LastName = "Granger", Group = "Gryffindor", Specialization = specializations[0]},
            new Student { FirstName = "Ron", LastName = "Weasley", Group = "Gryffindor", Specialization = specializations[0]},
            new Student { FirstName = "Draco", LastName = "Malfoy", Group = "Slytherin", Specialization = specializations[1]},
            new Student { FirstName = "Luna", LastName = "Lovegood", Group = "Ravenclaw", Specialization = specializations[1]},
            new Student { FirstName = "Neville", LastName = "Longbottom", Group = "Gryffindor", Specialization = specializations[2]},
            new Student { FirstName = "Cho", LastName = "Chang", Group = "Ravenclaw", Specialization = specializations[2]},
            new Student { FirstName = "Percy", LastName = "Weasly", Group = "Gryffindor", Specialization = specializations[3]},
            };
            context.People.AddRange(students);

            var teachers = new Teacher[]
            {
            new Teacher { FirstName = "Severus", LastName = "Snape", Qualification = "PhD", Department = departments[0]},
            new Teacher { FirstName = "Albus", LastName = "Dumbledore", Qualification = "PhD", Department = departments[0], IsDean = true},
            new Teacher { FirstName = "Minerva", LastName = "McGonagall", Qualification = "PhD", Department = departments[1], IsDean = true},
            new Teacher { FirstName = "Rubeus", LastName = "Hagrid", Qualification = "PhD", Department = departments[2], IsDean = true},
            new Teacher { FirstName = "Argus", LastName = "Filch", Qualification = "PhD", Department = departments[2]},
            new Teacher { FirstName = "Filius", LastName = "Flitwick", Qualification = "PhD", Department = departments[0]},
            };
            context.People.AddRange(teachers);

            var courses = new Course[]
            {
            new Course { Name = "Potions", Teacher = teachers[0], Students = students, Credit = 3 },
            new Course { Name = "History of Magic", Teacher = teachers[1], Students = students, Credit = 4 },
            new Course { Name = "Transfiguration", Teacher = teachers[2], Students = students, Credit = 3 },
            new Course { Name = "Defence Against the Dark Arts", Teacher = teachers[0], Students = students, Credit = 5 },
            new Course { Name = "Charms", Teacher = teachers[0], Students = students, Credit = 3 },
            new Course { Name = "Astronomy", Teacher = teachers[1], Students = students, Credit = 4 },
            new Course { Name = "Herbology", Teacher = teachers[3], Students = students, Credit = 4 },
            new Course { Name = "Flying", Teacher = teachers[3], Students = students, Credit = 5 },
            new Course { Name = "Arithmancy", Teacher = teachers[3], Students = students, Credit = 5 },
            new Course { Name = "Muggle Studies", Teacher = teachers[4], Students = students, Credit = 2 },
            new Course { Name = "Divination", Teacher = teachers[4], Students = students, Credit = 3 },
            new Course { Name = "Study of Ancient Runes", Teacher = teachers[5], Students = students, Credit = 4 },
            new Course { Name = "Care of Magical Creatures", Teacher = teachers[5], Students = students, Credit = 5 },
            };
            context.Courses.AddRange(courses);

            context.SaveChanges();
        }
    }
}