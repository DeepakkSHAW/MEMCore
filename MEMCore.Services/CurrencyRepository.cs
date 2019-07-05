using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MEMCore.Services
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private MEMCore.Data.ExpenseContext _expContext;
        public CurrencyRepository()
        {
            _expContext = new MEMCore.Data.ExpenseContext();
        }

        public async Task<IDictionary<int, string>> GetCurrencyAsync(bool sorted)
        {
            var vCurency= new Dictionary<int, string>();
            var quary = from c in _expContext.Currencies select c;

            if (sorted)
                quary = quary.OrderBy(x => x.CurrencyName);

            foreach (var cur in await quary.ToListAsync())
                vCurency.TryAdd(cur.Id, cur.CurrencyName);

            return vCurency;
        }

        public async Task<KeyValuePair<int, string>> GetCurrencyAsync(int id)
        {
            var key = id;
            var value = string.Empty;

            var quary = await _expContext.Currencies
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (quary != null)
            {
                key = quary.Id;
                value = quary.CurrencyName;
            }
            return new KeyValuePair<int, string>(key, value);
        }
    }
}
