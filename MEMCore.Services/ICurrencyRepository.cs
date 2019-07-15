using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEMCore.Services
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Domain.Currency>> GetCurrencyAsync(bool IsSorted);
        Task<Domain.Currency> GetCurrencyAsync(int CurID);
    }
}
