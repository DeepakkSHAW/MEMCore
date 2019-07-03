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

       Task<IEnumerable<Domain.Expense>> GetExpensesAsyn();
       Task<Domain.Expense> GetExpenseAsync(int id);

    }
}
