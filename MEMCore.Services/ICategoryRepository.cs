using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEMCore.Services
{
    public interface ICategoryRepository
    {
        //Task<IDictionary<int, string>> GetCategoriesAsync(bool IsSorted);
        Task<IEnumerable<MEMCore.Domain.ExpenseCategory>> GetCategoriesAsync(bool IsSorted);
        Task<IEnumerable<Domain.ExpenseCategory>> GetCategoriesAsync();
        //Task<IDictionary<int, string>> GetExpenseCategoryAsync(int id);
        Task<KeyValuePair<int, string>> GetExpenseCategoryAsync_KeyValue(int CategoryID);
        Task<Domain.ExpenseCategory> GetExpenseCategoryAsync(int CategoryID);
    }
}
