﻿// <auto-generated />
using System;
using MEMCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MEMCore.Data.Migrations
{
    [DbContext(typeof(ExpenseContext))]
    [Migration("20190723061024_added payment method as enum")]
    partial class addedpaymentmethodasenum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("MEMCore.Domain.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrencyName = "INR"
                        },
                        new
                        {
                            Id = 2,
                            CurrencyName = "USD"
                        },
                        new
                        {
                            Id = 3,
                            CurrencyName = "EUR"
                        },
                        new
                        {
                            Id = 4,
                            CurrencyName = "SFR"
                        },
                        new
                        {
                            Id = 5,
                            CurrencyName = "AUD"
                        },
                        new
                        {
                            Id = 6,
                            CurrencyName = "SGD"
                        },
                        new
                        {
                            Id = 7,
                            CurrencyName = "GBP"
                        });
                });

            modelBuilder.Entity("MEMCore.Domain.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrencyId");

                    b.Property<int>("ExpenseCategoryId");

                    b.Property<DateTime>("ExpenseDate");

                    b.Property<string>("ExpenseTitle")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("ExpensesAmount");

                    b.Property<int>("PaymentMethod");

                    b.Property<string>("Signature")
                        .HasMaxLength(2);

                    b.Property<DateTime>("inDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("updateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ExpenseCategoryId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("MEMCore.Domain.ExpenseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Grocery"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Restaurants"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Transportation"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Gifts"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Medical"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Insurance"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Clothing"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Education"
                        },
                        new
                        {
                            Id = 9,
                            Category = "Utilities"
                        },
                        new
                        {
                            Id = 10,
                            Category = "Shelter"
                        },
                        new
                        {
                            Id = 11,
                            Category = "Personal"
                        },
                        new
                        {
                            Id = 12,
                            Category = "Kids schooling"
                        },
                        new
                        {
                            Id = 13,
                            Category = "Household items"
                        },
                        new
                        {
                            Id = 14,
                            Category = "Fun money"
                        },
                        new
                        {
                            Id = 15,
                            Category = "Office exp"
                        },
                        new
                        {
                            Id = 16,
                            Category = "Personal"
                        },
                        new
                        {
                            Id = 17,
                            Category = "Miscellaneous"
                        });
                });

            modelBuilder.Entity("MEMCore.Domain.ExpenseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Detail")
                        .HasMaxLength(250);

                    b.Property<int>("ExpenseId");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId")
                        .IsUnique();

                    b.ToTable("ExpenseDetail");
                });

            modelBuilder.Entity("MEMCore.Domain.Expense", b =>
                {
                    b.HasOne("MEMCore.Domain.Currency", "Currency")
                        .WithMany("Expenses")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MEMCore.Domain.ExpenseCategory", "Category")
                        .WithMany("Expenses")
                        .HasForeignKey("ExpenseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MEMCore.Domain.ExpenseDetail", b =>
                {
                    b.HasOne("MEMCore.Domain.Expense", "Expense")
                        .WithOne("ExpenseDetail")
                        .HasForeignKey("MEMCore.Domain.ExpenseDetail", "ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
