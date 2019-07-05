using System;
using System.IO;
using MEMCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MEMCore.Data
{
    public class ExpenseContext : DbContext
    {
        public DbSet<ExpenseCategory> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        private enum WhichDatabase { SQLSrv, SQLite}
        #region private methods
        public void AppConfigurationSetIntialValues()
        {
            //Referance: https://garywoodfine.com/configuration-api-net-core-console-application/
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            var _connectionString = root.GetSection("DatabaseSettings").GetSection("SQLiteConnectionString").Value;
            System.Diagnostics.Debug.WriteLine($"DB Connection { _connectionString } !");
            var version = root.GetSection("Version").Value;
            System.Diagnostics.Debug.WriteLine($"application version: {version}.");
        }
        private String GetConnectionString(WhichDatabase whichdb)
        {
            var connectionString = string.Empty;
            var connType = string.Empty;

            IConfiguration memconfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            switch (whichdb)
            {
                case WhichDatabase.SQLSrv:
                default:
                    connType = "SQLDBConnectionString";
                    break;

                case WhichDatabase.SQLite:
                    connType = "SQLiteConnectionString";
                    break;
            }

            connectionString = memconfig.GetSection("DatabaseSettings").GetSection(connType).Value;
            return connectionString;
        }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Reference: ForAccessDB>> https://www.nuget.org/packages/EntityFrameworkCore.Jet/
            //Reference: Enums >> https://medium.com/agilix/entity-framework-core-enums-ee0f8f4063f2

            //*SQL Server Settings*//
            //optionsBuilder
            //    .UseLoggerFactory(GetLoggerFactory()) //* Encapsulated approach to enable logging, when consumer does have exposed database context *//
            //    .UseSqlServer(GetConnectionString(WhichDatabase.SQLSrv), options => options.MaxBatchSize(30));

            optionsBuilder
    .UseLoggerFactory(GetLoggerFactory()) //* Encapsulated approach to enable logging, when consumer does have exposed database context *//
    .UseSqlite("Filename=MEMCore.db", options => options.MaxBatchSize(30));
            //GetConnectionString(WhichDatabase.SQLite)
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
                //.HasDefaultValueSql("getutcdate()");
                  .HasDefaultValueSql("CURRENT_TIMESTAMP"); //Changes for sqlite

            modelBuilder.Entity<Expense>()
                .Property(c => c.updateDate)
                //.HasDefaultValueSql("getutcdate()");
                .HasDefaultValueSql("CURRENT_TIMESTAMP");//Changes for sqlite

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
                new Currency() { Id = 7, CurrencyName = "GBP" }
                );
        }
    }
}
