using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEMCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace MEMCore.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private MEMCore.Data.ExpenseContext _expContext;
        public CategoryRepository()
        {
            _expContext = new MEMCore.Data.ExpenseContext();
        }



        public async Task<IEnumerable<Domain.ExpenseCategory>> GetCategoriesAsync()
        {
            var quary = from c in _expContext.Categories select c;
            return await quary.ToListAsync();
        }

        public async Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync(bool IsSorted)
        {
            var vCategory = new Dictionary<int, string>();
            var quary = from c in _expContext.Categories select c;

            if (IsSorted)
                quary = quary.OrderBy(x => x.Category);

            return await quary.ToListAsync();
        }
        public async Task<KeyValuePair<int, string>> GetExpenseCategoryAsync_KeyValue(int id)
        {
            var key = id;
            var value = string.Empty;

            var quary = await _expContext.Categories
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (quary != null)
            {
                key = quary.Id;
                value = quary.Category.First().ToString().ToUpper() + quary.Category.Substring(1);
            }
            return new KeyValuePair<int, string>(key, value);
        }

        public async Task<Domain.ExpenseCategory> GetExpenseCategoryAsync(int id)
        {
            var quary = await _expContext.Categories
                .Where(x => x.Id == id).FirstOrDefaultAsync();
          return quary;
        }

        //public async Task<IDictionary<int, string>> GetCategoriesAsync(bool sorted)
        //{
        //    var vCategory = new Dictionary<int, string>();
        //    var quary = from c in _expContext.Categories select c;

        //    if (sorted)
        //        quary = quary.OrderBy(x => x.Category);

        //    foreach (var cur in await quary.ToListAsync())
        //        vCategory.TryAdd(cur.Id, cur.Category.First().ToString().ToUpper() + cur.Category.Substring(1));

        //    return vCategory;
        //}
        //An Alternative approach
        //public async Task<IDictionary<int, string>> GetExpenseCategoryAsync(int id)
        //{
        //    var vCategory = new Dictionary<int, string>();

        //    var quary = await _expContext.Categories
        //        .Where(x => x.Id == id).FirstOrDefaultAsync();

        //    if (quary != null)
        //        vCategory.TryAdd(quary.Id, quary.Category.First().ToString().ToUpper() + quary.Category.Substring(1));

        //    return vCategory;
        //}
    }
}
