using System;
using System.Linq;

namespace MEMCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Monthly expenses management system!");
            var exp = new MEMCore.Data.ExpenseContext();
            try
            {
                //var oCur = new Domain.Currency();
                //oCur.CurrencyName = "TTM";
                //var oCat = new Domain.ExpenseCategory { Category = "CNN" };
                //exp.Add(oCat);
                //exp.Add(oCur);

                var quary = exp.Expenses.Where(s => s.ExpenseTitle.Contains("a"));
                var rd = quary.ToList();
                foreach (var v in rd)
                {
                    Console.WriteLine(v.ExpenseTitle);
                }
                //exp.Add(GetExpense());
                //exp.Add(GetExpenseWithoutDetails());
                //exp.SaveChanges();

            }
            catch (Exception ex)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.ReadKey();

        }
        private static Domain.Expense GetExpense()
        {
            var oExp = new Domain.Expense();
            oExp.ExpenseTitle = "2nd This expense for a test";
            oExp.ExpensesAmount = 191.10;
            oExp.ExpenseDate = new DateTime(2019, 12, 18, 16, 20, 20);
            oExp.CurrencyId = 1;
            oExp.ExpenseCategoryId = 2;
            oExp.ExpenseDetail = new Domain.ExpenseDetail { Detail = "Dummy after audit-able columns" };
            return oExp;
        }
        private static Domain.Expense GetExpenseWithoutDetails()
        {
            var oExp = new Domain.Expense();
            oExp.ExpenseTitle = "1st expense for a test";
            oExp.ExpensesAmount = 191.10;
            oExp.ExpenseDate = new DateTime(2019, 12, 18, 16, 20, 20);
            oExp.CurrencyId = 1;
            oExp.ExpenseCategoryId = 1;
            return oExp;
        }
    }
}
