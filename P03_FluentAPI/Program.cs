using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03_FluentAPI
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(p => p.FirstName).HasMaxLength(30);
            modelBuilder.Entity<Person>().Property(p => p.LastName).HasMaxLength(30);
            modelBuilder.Entity<Person>().Property(p => p.MiddleName)
                .HasMaxLength(1)
                .IsFixedLength()
                .IsUnicode(false);

            /*
                Các phương thức tác dụng trên lớp entity
                    HasIndex() Chỉ định trường index
                    HasKey() Chỉ định trường khóa chính
                    HasMany() Chỉ định quan hệ 1-n hoặc n-n
                    HasOptional() Chỉ định quan hệ 1-0..1
                    HasRequired() Chỉ định quan hệ 1-1
                    Ignore() Chỉ định class không được ánh xạ thành bảng dữ liệu
                    Map() Chỉ định các cấu hình nâng cao
                    MapToStoredProcedures() Cấu hình để sử dụng INSERT, UPDATE and DELETE stored procedures
                    ToTable() Chỉ định tên bảng dữ liệu tương ứng

                Các phương thức tác dụng trên property
                    HasColumnAnnotation()
                    IsRequired() Chỉ định trường bắt buộc khi gọi SaveChanges()
                    IsConcurrencyToken() Configures the property to be used as an optimistic concurrency token
                    IsOptional() Trường tương ứng có thể nhận giá trị null
                    HasParameterName() Tên tham số sử dụng trong stored procedure tương ứng của property
                    HasDatabaseGeneratedOption() Chỉ định cách sinh dữ liệu tự động của cột trong cơ sở dữ liệu (computed, identity, none)
                    HasColumnOrder() Chỉ định thứ tự của cột tương ứng trong csdl
                    HasColumnType() Configures the data type of the corresponding column of a property in the database.
                    HasColumnName() Configures the corresponding column name of a property in the database.
                    IsConcurrencyToken() Configures the property to be used as an optimistic concurrency token.
            */
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                var success = context.Database.CreateIfNotExists();
                if (success)
                {
                    Console.WriteLine("New database created!");
                }
                else
                {
                    Console.WriteLine("Database exists or creation failed");
                }
            }
            Console.ReadKey();
        }
    }
}