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
                //DeleteExpenses();
                NewExpenses();
                //EditExpenses();
                //ListAllCurrancy();
                //GetACurrancy();
                //ListAllCategories();
                //GetaCategory();
                ListAllExpenses();
                //ListAExpenses();
                ListExpensesFromToDates();

             }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.WriteLine("Press any key to close.."); Console.ReadKey();
        }
        public static void ListAllCurrancy()
        {
            //MEMCore.Services.ICurrencyRepository srv = new MEMCore.Services.CurrencyRepository();
            //var asynCallGetCurrancy = Task.Run(() => srv.GetCurrencyAsync(true));
            //asynCallGetCurrancy.Wait();
            //var result = asynCallGetCurrancy.Result;

            //Console.ForegroundColor = ConsoleColor.Blue;
            //foreach (var item in result)
            //    Console.WriteLine($"Category ID:{item.Key} has value {item.Value}.");

            Console.ResetColor();
        }
        public static void GetACurrancy()
        {
            //MEMCore.Services.ICurrencyRepository srv = new MEMCore.Services.CurrencyRepository();
            //var asynCallGetACur = Task.Run(() => srv.GetCurrencyAsync(3));
            //asynCallGetACur.Wait();
            //var result = asynCallGetACur.Result;
            //Console.WriteLine(result.Key);
            //Console.WriteLine(result.Value);
        }
        public static void ListAllCategories()
        {
            //MEMCore.Services.ICategoryRepository srv = new MEMCore.Services.CategoryRepository();
            //var asynCallGetCategories = Task.Run(() => srv.GetCategoriesAsync(true));
            //asynCallGetCategories.Wait();
            //var result = asynCallGetCategories.Result;

            //Console.ForegroundColor = ConsoleColor.Blue;
            //foreach (var item in result)
            //    Console.WriteLine($"Category ID:{item.Key} has value {item.Value}.");

            Console.ResetColor();
        }
        public static void GetaCategory()
        {
            //MEMCore.Services.ICategoryRepository srv = new MEMCore.Services.CategoryRepository();
            ////var result = srv.GetExpenseCategoryAsyn(5);
            ////Console.WriteLine(result);
            //var asynCallGetACat = Task.Run(() => srv.GetExpenseCategoryAsync(5));
            //asynCallGetACat.Wait();
            //var result = asynCallGetACat.Result;
            //Console.WriteLine(result.Key);
            //Console.WriteLine(result.Value);
        }
        public static void ListAllExpenses()
        {
            MEMCore.Services.IExpenseRepository srv = new MEMCore.Services.ExpenseRepository();
            Task<IEnumerable<MEMCore.Domain.Expense>> asynCallGetExpenses = Task.Run(() => srv.GetExpensesAsync());

            asynCallGetExpenses.Wait();

            var asynExp = asynCallGetExpenses.Result;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var item in asynExp)
            {
                Console.WriteLine($"Expense Id: {item.Id}");
                Console.WriteLine($"Expense Title: {item.ExpenseTitle}");
                Console.WriteLine($"Expense Details: {(item.ExpenseDetail == null ? null : item.ExpenseDetail.Detail)}");
                Console.WriteLine($"Expense Amount: {item.ExpensesAmount}");
                Console.WriteLine($"Expense Date: {item.ExpenseDate}");
                Console.WriteLine($"Expense Currency: {item.Currency.CurrencyName}");
                Console.WriteLine($"Expense Category: {item.Category.Category}");
                Console.WriteLine($"Expense Signature: {item.Signature}");
            }
            Console.ResetColor();
        }
        public static void ListAExpenses()
        {
            MEMCore.Services.IExpenseRepository srv = new MEMCore.Services.ExpenseRepository();
            Task<MEMCore.Domain.Expense> asynCallGetExpenses = Task.Run(() => srv.GetExpensesAsync(3));

            asynCallGetExpenses.Wait();

            var asynExp = asynCallGetExpenses.Result;

            Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Expense Id: {asynExp.Id}");
                Console.WriteLine($"Expense Title: {asynExp.ExpenseTitle}");
               // Console.WriteLine($"Expense Details: {asynExp.ExpenseDetail.Detail}");
                Console.WriteLine($"Expense Amount: {asynExp.ExpensesAmount}");
                Console.WriteLine($"Expense Date: {asynExp.ExpenseDate}");
                Console.WriteLine($"Expense Currency: {asynExp.Currency.CurrencyName}");
                Console.WriteLine($"Expense Category: {asynExp.Category.Category}");
                Console.WriteLine($"Expense Signature: {asynExp.Signature}");
            Console.ResetColor();
        }
        public static void ListExpensesFromToDates()
        {
            MEMCore.Services.IExpenseRepository srv = new MEMCore.Services.ExpenseRepository();
            var asynCallGetExpenses = Task.Run(() => srv.GetExpensesAsync(DateTime.Now.AddMonths(-1), DateTime.Now));

            asynCallGetExpenses.Wait();

            var asynExp = asynCallGetExpenses.Result;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var item in asynExp)
            {
                Console.WriteLine($"Expense Id: {item.Id}");
                Console.WriteLine($"Expense Title: {item.ExpenseTitle}");
                // Console.WriteLine($"Expense Details: {item.ExpenseDetail.Detail}");
                Console.WriteLine($"Expense Amount: {item.ExpensesAmount}");
                Console.WriteLine($"Expense Date: {item.ExpenseDate}");
                Console.WriteLine($"Expense Currency: {item.Currency.CurrencyName}");
                Console.WriteLine($"Expense Category: {item.Category.Category}");
                Console.WriteLine($"Expense Signature: {item.Signature}");

            }
            Console.ResetColor();
        }
        public static void NewExpenses()
        {
            var oExpenses = new MEMCore.Domain.Expense();
            oExpenses.ExpenseTitle = "++++ Added new expense async";
            oExpenses.ExpensesAmount = 100.20;
            oExpenses.ExpenseDate = new DateTime(2019, 7, 5, 10, 10, 10);
            oExpenses.CurrencyId = 1;
            oExpenses.ExpenseCategoryId = 5;
            oExpenses.ExpenseDetail = new MEMCore.Domain.ExpenseDetail { Detail = "after the fix" };
            oExpenses.Signature = "DK";

            MEMCore.Services.IExpenseRepository srv = new MEMCore.Services.ExpenseRepository();
            var asynCallGetExpenses = Task.Run(() => srv.NewExpensesAsync(oExpenses));

            asynCallGetExpenses.Wait();

            var asynExp = asynCallGetExpenses.Result;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Number of row affected: {asynExp}");
            Console.ResetColor();
        }
        public static void DeleteExpenses()
        {
            MEMCore.Services.IExpenseRepository srv = new MEMCore.Services.ExpenseRepository();
            var asynCallGetExpenses = Task.Run(() => srv.DeleteExpensesAsync(4));

            asynCallGetExpenses.Wait();

            var asynExp = asynCallGetExpenses.Result;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Total number of expenses deleted: {asynExp}");
            Console.ResetColor();
        }
        public static void EditExpenses()
        {
            var oExpenses = new MEMCore.Domain.Expense();
            oExpenses.ExpenseTitle = "--Update this expenses";
            oExpenses.ExpensesAmount = 7200.20;
            oExpenses.ExpenseDate = new DateTime(2018, 5, 5, 5, 5, 5);
            oExpenses.CurrencyId = 5;
            oExpenses.ExpenseCategoryId = 5;
            oExpenses.ExpenseDetail = new MEMCore.Domain.ExpenseDetail { Detail = "-- just an update++" };
            oExpenses.Signature = "JS";

            MEMCore.Services.IExpenseRepository srv = new MEMCore.Services.ExpenseRepository();
            var asynCallGetExpenses = Task.Run(() => srv.EditExpensesAsync(oExpenses, 1));

            asynCallGetExpenses.Wait();

            var asynExp = asynCallGetExpenses.Result;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Number of row affected after upate: {asynExp}");
            Console.ResetColor();
        }
    }
}
