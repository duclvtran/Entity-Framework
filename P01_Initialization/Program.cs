using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_Initialization
{
    public class Context : DbContext
    {
        public Context() : base()
        {
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            bool success = false;
            using (var context = new Context())
            {
                success = context.Database.CreateIfNotExists();
            }

            Console.WriteLine($"Database creation: {success}");
            Console.ReadKey();
        }
    }
}