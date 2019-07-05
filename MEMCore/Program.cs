using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MEMCore
{
    class Program
    {
        private static MEMCore.Data.ExpenseContext _expContext;

        static void Main(string[] args)
        {
            Console.WriteLine("Synchronous Approach");
            Console.WriteLine("Monthly expenses management system!");

            try
            {

                _expContext = new MEMCore.Data.ExpenseContext();
                
                //*This approach applicable if you have database context available*//
                _expContext.GetService<ILoggerFactory>().AddProvider(new Data.MEMLoggerProvider());
                _expContext.Database.Migrate();

                //EditExpense(1);
                //Test methods from main
                //NewExpense();
                //NewExpenseWithoutDetails();
                DeleteExpense(9);

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.WriteLine("Press any key to close.."); Console.ReadKey();

        }
        private static int NewExpense()
        {
            int result = -1;
            try
            {
                var oExp = new Domain.Expense();
                oExp.ExpenseTitle = "2nd This expense for a test";
                oExp.ExpensesAmount = 191.10;
                oExp.ExpenseDate = new DateTime(2019, 12, 18, 16, 20, 20);
                oExp.CurrencyId = 1;
                oExp.ExpenseCategoryId = 2;
                oExp.ExpenseDetail = new Domain.ExpenseDetail { Detail = "Dummy after audit-able columns" };
                _expContext.Add(oExp);
                result = _expContext.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        private static int NewExpenseWithoutDetails()
        {
            int result = -1;
            try
            {
                var oExp = new Domain.Expense();
                oExp.ExpenseTitle = "1st expense for a test";
                oExp.ExpensesAmount = 191.10;
                oExp.ExpenseDate = new DateTime(2019, 12, 18, 16, 20, 20);
                oExp.CurrencyId = 1;
                oExp.ExpenseCategoryId = 1;
                _expContext.Add(oExp);
                result = _expContext.SaveChanges();
            }
            catch (Exception) { throw; }
            return result;
        }
        private static int EditExpense(int id)
        {
            int result = -1;
            try
            {
                //var oExp = new Domain.Expense();
                //oExp.ExpenseTitle = "An update expense test";
                //oExp.ExpensesAmount = 510.20;
                //oExp.ExpenseDate = new DateTime(2020, 12, 18, 16, 20, 20);
                //oExp.CurrencyId = 1;
                //oExp.ExpenseCategoryId = 2;
                //oExp.ExpenseDetail = new Domain.ExpenseDetail { Detail = "Dummy after audit-able columns" };

                var entiry = _expContext.Expenses.Include(d => d.ExpenseDetail).FirstOrDefault(x => x.Id == id);

                if (entiry != null)
                {
                    entiry.ExpenseTitle = "--An update expense test";
                    entiry.ExpensesAmount = 519.65;
                    entiry.ExpenseDate = new DateTime(2019, 7, 4, 20, 20, 20);
                    entiry.ExpenseCategoryId = 7;
                    entiry.CurrencyId = 7;
                    entiry.updateDate = DateTime.UtcNow;
                    entiry.ExpenseDetail = new Domain.ExpenseDetail { Detail = "updating the details+++++++++++++++++++++++" };
                   // _expContext.Update(oExp);
                    result = _expContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        private static List<Domain.Expense> GetExpenses()
        {
            var result = new List<Domain.Expense>();
            try
            {
                var quary = _expContext.Expenses.OrderByDescending(x => x.ExpenseDate)
                    .Include(s => s.Category)
                    .Include(s => s.Currency)
                    .Include(s => s.ExpenseDetail);
                result = quary.ToList();
                foreach (var cur in quary)
                {
                    Console.Write(cur.ExpenseTitle + " ");
                    // Console.Write(cur.ExpenseDetail.Detail?? "");
                    Console.Write(cur.Category.Category ?? "");
                    Console.WriteLine(cur.Currency.CurrencyName ?? "");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return result;
        }
        private static Domain.Expense GetExpensesById(int id)
        {
            var result = new Domain.Expense();
            try
            {
                var quary = _expContext.Expenses.OrderByDescending(x => x.ExpenseDate)
                    .Include(s => s.Category)
                    .Include(s => s.Currency)
                    //.Include(s => s.ExpenseDetail);
                    .Where(x => x.Id == id).FirstOrDefault();

                if (quary != null)
                    result = quary;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return result;
        }
        private static int DeleteExpense(int id)
        {
            int result = -1;
            try
            {
                var oExp = _expContext.Expenses.FirstOrDefault(x => x.Id == id);
                if (oExp != null)
                {
                    _expContext.Expenses.Attach(oExp);
                    _expContext.Expenses.Remove(oExp);
                    _expContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        private static IDictionary<int, string> GetCurrency()
        {
            var vCurrency = new Dictionary<int, string>();
            try
            {
                var quary = _expContext.Currencies.OrderBy(x => x.CurrencyName);
                foreach (var cur in quary)
                {
                    vCurrency.TryAdd(cur.Id, cur.CurrencyName);
                    //vCurrency.Add(new KeyValuePair<int, string>(2, "DK"));
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return vCurrency;
        }
        private static IDictionary<int, string> GetCategory()
        {
            var vCategory = new Dictionary<int, string>();
            try
            {
                var quary = _expContext.Categories.OrderBy(x => x.Category);
                foreach (var cur in quary)
                    vCategory.TryAdd(cur.Id, cur.Category.First().ToString().ToUpper() + cur.Category.Substring(1));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return vCategory;
        }

    }
}
