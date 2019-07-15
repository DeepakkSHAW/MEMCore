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
        private MEMCore.Data.ExpenseContext _expContext;

        public ExpenseRepository()
        {
            _expContext = new MEMCore.Data.ExpenseContext();
            _expContext.Database.Migrate(); //DK: Need to change this
        }
        public async Task<Domain.Expense> GetExpensesAsync(int id)
        {
            //_expContext = new MEMCore.Data.ExpenseContext();
            var result = new Domain.Expense();
            //using (_expContext.Expenses)
            //{
                var quary = await _expContext.Expenses.OrderByDescending(x => x.ExpenseDate)
                    .Include(s => s.Category)
                    .Include(s => s.Currency)
                    .Include(s => s.ExpenseDetail)
                    .Where(x => x.Id == id).FirstOrDefaultAsync();
            //}
                if (quary != null)
                    result = quary;

                return result;
        }
        public async Task<bool> ExpenseExist(int id)
        {
            bool found = false;
            var oExp = await _expContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (oExp.Id > 0) found = true;
            return found;
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
           // _expContext = new MEMCore.Data.ExpenseContext();
            return await _expContext.Expenses.OrderByDescending(x => x.ExpenseDate)
                    .Include(s => s.Category)
                    .Include(s => s.Currency)
                    .Include(s => s.ExpenseDetail)
                    .ToListAsync();
        }
        public async Task<IEnumerable<Expense>> GetExpensesAsync(DateTime from, DateTime to)
        {
            return await _expContext.Expenses.OrderByDescending(x => x.ExpenseDate)
                    .Include(s => s.Category)
                    .Include(s => s.Currency)
                    .Include(s => s.ExpenseDetail)
                    .Where(d => d.ExpenseDate >= from && d.ExpenseDate <= to)
                    .ToListAsync();
        }
        public void DeleteIT(string message, int numTimes, int numLineBreaks)
        {
            DateTime t = DateTime.Now.AddMonths(-1);
            for (int i = 0; i < numTimes; ++i)
            {
                Console.Write(message);
                for (int lb = 0; lb < numLineBreaks; ++lb)
                    Console.WriteLine();
            }

        }
        public async Task<int> NewExpensesAsync(Domain.Expense expense)
        {
            var result = -1;
            _expContext.Add(expense);
            result = await _expContext.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteExpensesAsync(int id)
        {
            var result = -1;
            var oExp = _expContext.Expenses.FirstOrDefault(x => x.Id == id);
            if (oExp != null)
            {
                _expContext.Expenses.Attach(oExp);
                _expContext.Expenses.Remove(oExp);
                result = await _expContext.SaveChangesAsync();
            }
            return result;
        }
        public async Task<int> EditExpensesAsync(Domain.Expense expenseToUpdate, int id)
        {
            var result = -1;
            var entiry = _expContext.Expenses.Include(d => d.ExpenseDetail).FirstOrDefault(x => x.Id == id);

            if (entiry != null)
            {
                entiry.ExpenseTitle = expenseToUpdate.ExpenseTitle;
                entiry.ExpensesAmount = expenseToUpdate.ExpensesAmount;
                entiry.ExpenseDate = expenseToUpdate.ExpenseDate;
                entiry.ExpenseCategoryId = expenseToUpdate.ExpenseCategoryId;
                entiry.CurrencyId = expenseToUpdate.CurrencyId;
                entiry.updateDate = DateTime.UtcNow;
                entiry.ExpenseDetail = expenseToUpdate.ExpenseDetail;
                entiry.Signature = expenseToUpdate.Signature;
                // _expContext.Update(oExp);
                result = await _expContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
