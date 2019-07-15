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

        public async Task<IEnumerable<Domain.Currency>> GetCurrencyAsync(bool sorted)
        {
            var quary = from c in _expContext.Currencies select c;

            if (sorted)
                quary = quary.OrderBy(x => x.CurrencyName);

            return await quary.ToListAsync();
        }

        public async Task<Domain.Currency> GetCurrencyAsync(int id)
        {
            var quary = await _expContext.Currencies
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return quary;
        }
    }
}
