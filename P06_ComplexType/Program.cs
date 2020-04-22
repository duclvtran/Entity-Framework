using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06_ComplexType
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Address Address { get; set; }
    }

    public class Address
    {
        [Column("street")]
        [MaxLength(40)]
        [Required]
        public string Street { get; set; }

        [Column("city")]
        [MaxLength(40)]
        [Required]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("zip")]
        [MaxLength(10)]
        public string Zip { get; set; }
    }

    public class AddressMap : ComplexTypeConfiguration<Address>
    {
        public AddressMap()
        {
            Property(p => p.Street)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnName("Street");

            Property(p => p.Zip)
                .HasMaxLength(10)
                .HasColumnName("Zip")
                .IsUnicode(false);
        }
    }

    public class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMap());

            modelBuilder.ComplexType<Address>()
                .Property(p => p.State)
                .HasColumnName("State")
                .HasMaxLength(40);

            modelBuilder.ComplexType<Address>()
                .Property(p => p.City)
                .HasColumnName("City");
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Company> Companies { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}