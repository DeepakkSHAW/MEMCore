using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEMCore.Domain;

namespace MEMCoreAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Asynchronous Approach");
            Console.WriteLine("Monthly expenses management system!");

            try
            {
                var srv = new MEMCore.Services.ExpenseRepository();
                Task<IEnumerable<MEMCore.Domain.Expense>> asynCallGetExpenses = Task.Run(() => srv.GetExpensesAsyn());

                asynCallGetExpenses.Wait();

                var asynExp = asynCallGetExpenses.Result;
                foreach (var item in asynExp)
                {
                    Console.WriteLine($"Expense Id: {item.Id}");
                    Console.WriteLine($"Expense Title: {item.ExpenseTitle}");
                    //Console.WriteLine($"Expense Details: {item.ExpenseDetail.Detail}");
                    Console.WriteLine($"Expense Amount: {item.ExpensesAmount}");
                    Console.WriteLine($"Expense Date: {item.ExpenseDate}");
                    Console.WriteLine($"Expense Currency: {item.Currency.CurrencyName}");
                    Console.WriteLine($"Expense Category: {item.Category.Category}");
                    Console.WriteLine($"Expense Signature: {item.Signature}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.ReadKey();
        }
    }
}
