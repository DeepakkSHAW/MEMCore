using System;
using MEMCore.Domain;
using Microsoft.EntityFrameworkCore;
namespace MEMCore.Data
{
    public class ExpenseContext : DbContext
    {

        public DbSet<ExpenseCategory> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ForAccessDB: https://www.nuget.org/packages/EntityFrameworkCore.Jet/
            //Enum: https://medium.com/agilix/entity-framework-core-enums-ee0f8f4063f2

            optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MEMCoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
             ,options => options.MaxBatchSize(30)
             );
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //**Using Fluent API Approach**//

            modelBuilder.Entity<Expense>()
                .Property(c => c.ExpenseTitle)
                
                .IsRequired(true)
                .HasMaxLength(50);

            modelBuilder.Entity<Expense>()
                .Property(c => c.ExpenseDate)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .Property(c => c.Signature)
                .HasMaxLength(2)
                .IsRequired(false);

            modelBuilder.Entity<Expense>()
                .Property(c => c.inDate)
                .IsRequired()
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Expense>()
                .Property(c => c.updateDate)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ExpenseDetail>()
                .Property(c => c.Detail)
                .HasMaxLength(250)
                .IsRequired(false)
                
                ;

            modelBuilder.Entity<ExpenseCategory>()
            .Property(c => c.Category)
            .IsRequired()
            .HasMaxLength(50)
            ;
            modelBuilder.Entity<Currency>()
                .Property(c => c.CurrencyName)
                .IsRequired(true)
                .HasMaxLength(3)
                ;
        //        public enum Signature { DK = 1, RS = 2, JS = 3, DS = 4 }
        //modelBuilder.Entity<Expense>()
        //        .Property(c => c.signature)
        //        .HasConversion(x => (int)x, x => (Signature)x);

                            //*Seeding Data*//
            modelBuilder.Entity<ExpenseCategory>().HasData(
                new ExpenseCategory() { Id = 1, Category = "Grocery" },
                new ExpenseCategory() { Id = 2, Category = "Restaurants" },
                new ExpenseCategory() { Id = 3, Category = "Transportation" },
                new ExpenseCategory() { Id = 4, Category = "Gifts" },
                new ExpenseCategory() { Id = 5, Category = "Medical" },
                new ExpenseCategory() { Id = 6, Category = "Insurance" },
                new ExpenseCategory() { Id = 7, Category = "Clothing" },
                new ExpenseCategory() { Id = 8, Category = "Education" },
                new ExpenseCategory() { Id = 9, Category = "Utilities" },
                new ExpenseCategory() { Id = 10, Category = "Shelter" },
            new ExpenseCategory() { Id = 11, Category = "Personal" },
            new ExpenseCategory() { Id = 12, Category = "Kids schooling" },
            new ExpenseCategory() { Id = 13, Category = "Household items" },
            new ExpenseCategory() { Id = 14, Category = "Fun money" },
            new ExpenseCategory() { Id = 15, Category = "Office exp" },
            new ExpenseCategory() { Id = 16, Category = "Personal" },
            new ExpenseCategory() { Id = 17, Category = "Miscellaneous" }
            );

            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, CurrencyName = "INR" },
                new Currency() { Id = 2, CurrencyName = "USD" },
                new Currency() { Id = 3, CurrencyName = "EUR" },
                new Currency() { Id = 4, CurrencyName = "SFR" },
                new Currency() { Id = 5, CurrencyName = "AUD" },
                new Currency() { Id = 6, CurrencyName = "SGD" },
                new Currency() { Id = 7, CurrencyName = "GBP"}
                );
        }
    }
}
