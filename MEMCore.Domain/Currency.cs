using System;
using System.Collections.Generic;
using System.Text;

namespace MEMCore.Domain
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}