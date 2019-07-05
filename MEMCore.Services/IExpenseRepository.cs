using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEMCore.Services
{
    interface IExpenseRepository
    {
        //IEnumerable<Domain.Expense> GetExpenses();
        //Domain.Expense GetExpense(int id);

       Task<IEnumerable<Domain.Expense>> GetExpensesAsync();
       Task<Domain.Expense> GetExpensesAsync(int id);
       Task<IEnumerable<Domain.Expense>> GetExpensesAsync(DateTime from , DateTime to);
       Task<int> NewExpensesAsync(Domain.Expense expense);
       Task<int> DeleteExpensesAsync(int expenseId);
       Task<int> EditExpensesAsync(Domain.Expense expense, int id);
       void DeleteIT(string message, int times = 1, int lineBreaks = 1);
    }
}
