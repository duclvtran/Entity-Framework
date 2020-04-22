using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace P04_Relationship
{
    internal class Program
    {
        //reference navigation
        public class Person
        {
            public int PersonId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Gender { get; set; }
            public IList<Email> Emails { get; set; }
            public virtual ICollection<Company> Companies { get; set; }
        }

        public class Company
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public virtual ICollection<Person> People { get; set; }
        }

        public class Email
        {
            public int EmailId { get; set; }
            public string EmailAddress { get; set; }
            //public Person Person { get; set; }
        }

        //=============================

        //collection navigation
        public class Person1
        {
            public int Person1Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public IList<Email1> Email1s { get; set; }
            public virtual ICollection<Company1> Companies { get; set; }
        }

        public class Company1
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public virtual ICollection<Person1> People { get; set; }
        }

        public class Email1
        {
            public int Email1Id { get; set; }
            public string EmailAddress { get; set; }
        }

        //=============================
        //annotation attribute
        public class Person2
        {
            public int Person2Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
        }

        public class Email2
        {
            public int Email2Id { get; set; }
            public string EmailAddress { get; set; }

            // dùng annotation attribute nếu tên khóa và trường navigation không theo quy ước
            //[ForeignKey("Owner")] // hoặc
            [ForeignKey(nameof(Owner))]
            public int RefToPerson { get; set; }

            public Person2 Owner { get; set; }
        }

        //=============================

        //fluent API
        public class Person3
        {
            public int Person3Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public IList<Email3> Email3s { get; set; }
        }

        public class Email3
        {
            public int Email3Id { get; set; }
            public string EmailAddress { get; set; }

            public int RefToPerson { get; set; } // tên này đặt không theo quy tắc
            public Person3 Owner { get; set; }
        }

        //=============================
        //fluent API With Buddy
        public class Person4
        {
            public int Person4Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public IList<Email4> Email4s { get; set; }
        }

        public class Email4
        {
            public int Email4Id { get; set; }
            public string EmailAddress { get; set; }

            public int RefToPerson { get; set; } // tên này đặt không theo quy tắc
            public Person4 Owner { get; set; }
        }

        //Có thể sử dụng 1 trong 2 cách cấu hình bên dưới
        public class Person4Map : EntityTypeConfiguration<Person4>
        {
            public Person4Map()
            {
                // Cấu hình quan hệ từ phía "một"
                HasMany(p => p.Email4s)
                    .WithRequired(e => e.Owner)
                    .HasForeignKey(e => e.RefToPerson)
                    .WillCascadeOnDelete(); //Đồng thời xóa trong Table Email(map) sau khi xóa 1 PersionId tương ứng
            }
        }

        public class Person1Map : EntityTypeConfiguration<Person1>
        {
            public Person1Map()
            {
                //Cấu hình quan hệ từ phía "nhiều"
                HasMany(p => p.Companies)
                    .WithMany(c => c.People)
                    .Map(pc =>
                    {
                        pc.MapLeftKey("RefPersonId");
                        pc.MapRightKey("RefCompanyId");
                        pc.ToTable("PersonWithCompany");
                    });
            }
        }

        public class Email4Map : EntityTypeConfiguration<Email4>
        {
            public Email4Map()
            {
                //Cấu hình quan hệ từ phía "nhiều"
                HasRequired(e => e.Owner)
                  .WithMany(p => p.Email4s)
                  .HasForeignKey(e => e.RefToPerson)
                  .WillCascadeOnDelete();//Đồng thời xóa trong Table Email(map) sau khi xóa 1 PersionId tương ứng
            }
        }

        //=============================
        public class MyInitializer : CreateDatabaseIfNotExists<Context>
        {
            protected override void Seed(Context context)
            {
                context.People.AddRange(new[]
                {
                new Person{ FirstName = "Donald", LastName = "Trump"},
                new Person{ FirstName = "Barack", LastName = "Obama"},
                new Person{ FirstName = "George", LastName = "Bush"},
                new Person{ FirstName = "Bill", LastName = "Clinton"},
            });
                context.SaveChanges();
            }
        }

        public class Context : DbContext
        {
            public Context() : base("name=ConnectionString")
            {
                var initializer = new MyInitializer();
                Database.SetInitializer(initializer);
            }

            public DbSet<Person> People { get; set; }
            public DbSet<Email> Emails { get; set; }
            public DbSet<Person1> People1 { get; set; }
            public DbSet<Email1> Email1s { get; set; }
            public DbSet<Person2> People2 { get; set; }
            public DbSet<Email2> Email2s { get; set; }
            public DbSet<Person3> People3 { get; set; }
            public DbSet<Email3> Email3s { get; set; }
            public DbSet<Person4> People4 { get; set; }
            public DbSet<Email4> Email4s { get; set; }
            public DbSet<Company> Companies { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                //modelBuilder.Entity<Email3>()
                //.HasRequired<Person3>(e => e.Owner)
                //.WithMany(p => p.Email3s)
                //.HasForeignKey<int>(e => e.RefToPerson);

                modelBuilder.Configurations.Add(new Email4Map());
                modelBuilder.Configurations.Add(new Person1Map());

                // hoặc
                //modelBuilder.Configurations.Add(new PersonMap());
            }
        }

        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                var trump = new Person { FirstName = "Donald", LastName = "Trump" };
                trump.Emails = new List<Email>
                {
                    new Email { EmailAddress = "donald.trump@gmail.com" },
                    new Email { EmailAddress = "trump.donald@yandex.ru" }
                };

                var obama = new Person { FirstName = "Barack", LastName = "Obama" };
                obama.Emails = new List<Email>
                {
                    new Email { EmailAddress = "barack.obama@gmail.com" },
                    new Email { EmailAddress = "obama.barack@yandex.ru" }
                };

                var mail = new Email { EmailAddress = "president@the_white_house.us" };
                trump.Emails.Add(mail);
                obama.Emails.Add(mail);

                context.People.Add(trump);
                context.People.Add(obama);
                context.SaveChanges();

                var people = context.People.ToArray();
                foreach (var p in people)
                {
                    Console.WriteLine($"First person: {p.FirstName} {p.LastName}");
                }
            }

            Console.ReadKey();
        }
    }
}