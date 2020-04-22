using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Title = "HOGWARTS SCHOOL";

            using (var context = new UniversityContext())
            {
                Loading.LazyLoading(context);
                //Loading.LazyLoadingMARS(context);
                Loading.EagerLoadingQuery(context);
                Loading.EagerLoading(context);
                Loading.EagerLoadingMultiLevel(context);
            }

            Console.ReadKey();
        }

        //private static void LazyLoading(UniversityContext context)
        //{
        //    var course = context.Courses.FirstOrDefault(c => c.Name.Contains("Magic"));
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine($"Course name: {course.Name.ToUpper()}");
        //    Console.ResetColor();

        //    if (course.Teacher != null)
        //    {
        //        var t = course.Teacher;
        //        Console.ForegroundColor = ConsoleColor.Yellow;
        //        Console.WriteLine($"by {t.Qualification} {t.FirstName} {t.LastName}");
        //        Console.ResetColor();
        //    }

        //    if (course.Students != null && course.Students.Count > 0)
        //    {
        //        Console.WriteLine("# Students enrolled in this course:");
        //        foreach (var s in course.Students)
        //        {
        //            Console.WriteLine($"  -> {s.FirstName} {s.LastName}");
        //        }
        //    }
        //}
    }
}