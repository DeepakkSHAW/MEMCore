using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEMCore.Services
{
    public interface ICategoryRepository
    {
        Task<IDictionary<int, string>> GetCategoriesAsync(bool foo);
        //Task<IDictionary<int, string>> GetExpenseCategoryAsync(int id);
        Task<KeyValuePair<int, string>> GetExpenseCategoryAsync(int CategoryID);
    }
}
