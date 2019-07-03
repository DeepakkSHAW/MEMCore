using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace MEMCore.Services
{
    public class ExpenseRepository : IExpenseRepository
    {
        private static MEMCore.Data.ExpenseContext _expContext;

        public ExpenseRepository()
        {
            _expContext = new MEMCore.Data.ExpenseContext();
        }

        public async Task<Domain.Expense> GetExpenseAsync(int id)
        {
            //_expContext = new MEMCore.Data.ExpenseContext();
            var result = new Domain.Expense();

                var quary = await _expContext.Expenses.OrderByDescending(x => x.ExpenseDate)
                    .Include(s => s.Category)
                    .Include(s => s.Currency)
                    .Include(s => s.ExpenseDetail)
                    .Where(x => x.Id == id).FirstOrDefaultAsync();

                if (quary != null)
                    result = quary;

                return result;
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsyn()
        {
           // _expContext = new MEMCore.Data.ExpenseContext();
            return await _expContext.Expenses.OrderByDescending(x => x.ExpenseDate)
                    .Include(s => s.Category)
                    .Include(s => s.Currency)
                    .Include(s => s.ExpenseDetail).ToListAsync();
        }
    }
}
