using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace EntityFrameworkExample
{
    public class Loading
    {
        #region Loading

        public static void LazyLoading(UniversityContext context)
        {
            //context.Configuration.LazyLoadingEnabled = false;
            var course = context.Courses.FirstOrDefault(c => c.Name.Contains("Magic"));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Course name: {course.Name.ToUpper()}");
            Console.ResetColor();

            if (course.Teacher != null)
            {
                var t = course.Teacher;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"by {t.Qualification} {t.FirstName} {t.LastName}");
                Console.ResetColor();
            }

            if (course.Students != null && course.Students.Count > 0)
            {
                Console.WriteLine("# Students enrolled in this course:");
                foreach (var s in course.Students)
                {
                    Console.WriteLine($"  -> {s.FirstName} {s.LastName}");
                }
            }
        }

        #endregion Loading

        public static void LazyLoadingMARS(UniversityContext context)
        {
            foreach (var course in context.Courses)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{course.Name.ToUpper()}");

                if (course.Teacher != null)
                {
                    var t = course.Teacher;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" ({t.Qualification} {t.FirstName} {t.LastName})");
                    Console.ResetColor();
                }
            }
        }

        public static void EagerLoading(UniversityContext context)
        {
            context.Configuration.LazyLoadingEnabled = false; // tắt chế độ lazy loading
            var courses = context.Courses.Include(c => c.Teacher); // lưu ý thêm using System.Data.Entity;

            foreach (var c in courses)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{c.Name.ToUpper()}");

                if (c.Teacher != null)
                {
                    var t = c.Teacher;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" ({t.Qualification} {t.FirstName} {t.LastName})");
                    Console.ResetColor();
                }

                if (c.Students != null && c.Students.Count > 0)
                {
                    Console.WriteLine("# Students enrolled in this course:");
                    foreach (var s in c.Students)
                    {
                        Console.WriteLine($"  -> {s.FirstName} {s.LastName}");
                    }
                }
            }
        }

        public static void EagerLoadingQuery(UniversityContext context)
        {
            context.Configuration.LazyLoadingEnabled = false; // tắt chế độ lazy loading
            var courses = context.Courses
                .Include(c => c.Teacher)
                .Where(c => c.Name.Contains("Magic"))
                .OrderBy(c => c.Name)
                ; // lưu ý thêm using System.Data.Entity;
            Console.WriteLine("### Magic courses in Hogwarts ###");
            foreach (var c in courses)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{c.Name.ToUpper()}");

                if (c.Teacher != null)
                {
                    var t = c.Teacher;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" ({t.Qualification} {t.FirstName} {t.LastName})");
                    Console.ResetColor();
                }
            }
        }

        public static void EagerLoadingMultiLevel(UniversityContext context)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var courses = context.Courses
                .Include(c => c.Teacher.Department);

            foreach (var c in courses)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{c.Name.ToUpper()}");

                if (c.Teacher != null)
                {
                    var t = c.Teacher;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" ({t.Qualification} {t.FirstName} {t.LastName}");
                    Console.ResetColor();
                    Console.Write(" from ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Department of {c.Teacher.Department.Name})");
                }
            }
        }

        public static void ExplicitLoading(UniversityContext context)
        {
            var course = context.Courses.FirstOrDefault(c => c.Name.Contains("Magic"));
            var entry = context.Entry(course);
            entry.Collection("Students").Load(); //Collection để tải dữ liệu quan hệ ở dạng danh sách object = 1-n
            //entry.Collection("Students").Load(); // tương đương entry.Collection(c=>c.Students)
            entry.Reference(c => c.Teacher).Load();//Reference nếu dữ liệu quan hệ chỉ là một object = 1-1
            //entry.Reference("Teacher").Load(); // tương đương entry.Reference(c=>c.Teacher)

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"Course name: {course.Name.ToUpper()}");
            ResetColor();

            if (course.Teacher != null)
            {
                var t = course.Teacher;
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine($"by {t.Qualification} {t.FirstName} {t.LastName}");
                ResetColor();
            }

            if (course.Students != null && course.Students.Count > 0)
            {
                WriteLine("# Students enrolled in this course:");
                foreach (var s in course.Students)
                {
                    WriteLine($"  -> {s.FirstName} {s.LastName}");
                }
            }
        }

        //Phương thức Reference và Collection cho phép kiểm tra xem các object quan hệ đã được tải lên chưa sử dụng property IsLoaded
        public static void ExplicitLoadingTestIsLoaded(UniversityContext context)
        {
            var course = context.Courses.FirstOrDefault(c => c.Name.Contains("Magic"));
            var entry = context.Entry(course);

            WriteLine($"Before: {entry.Collection(c => c.Students).IsLoaded}");

            entry.Collection(c => c.Students).Load();

            WriteLine($"After: {entry.Collection(c => c.Students).IsLoaded}");
        }

        //Truy vấn collection navigation property với Explicit loading
        public static void ExplicitLoadingCollectionQuery(UniversityContext context)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var course = context.Courses.FirstOrDefault(c => c.Name.Contains("Magic"));
            var entry = context.Entry(course);
            var query = entry.Collection(c => c.Students).Query();
            var gryffindor = query.Where(s => s.Group == "Gryffindor");
            gryffindor.Load();

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"Course name: {course.Name.ToUpper()}");

            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("# Gryffindor students enrolled in this course:");
            ResetColor();
            foreach (var s in gryffindor)
            {
                WriteLine($"  -> {s.FirstName} {s.LastName}");
            }
        }

        public static void ExplicitLoadingCollectionAggregation(UniversityContext context)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var course = context.Courses.FirstOrDefault(c => c.Name.Contains("Magic"));
            var entry = context.Entry(course);
            var query = entry.Collection(c => c.Students).Query();
            var count = query.Count();

            ForegroundColor = ConsoleColor.Green;
            Write($"Course name: {course.Name.ToUpper()}");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($" ({count} students enrolled)");
        }
    }
}