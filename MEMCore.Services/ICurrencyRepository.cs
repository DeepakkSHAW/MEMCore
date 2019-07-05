using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEMCore.Services
{
    public interface ICurrencyRepository
    {
        Task<IDictionary<int, string>> GetCurrencyAsync(bool foo);
        Task<KeyValuePair<int, string>> GetCurrencyAsync(int CurID);
    }
}
