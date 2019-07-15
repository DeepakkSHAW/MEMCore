using System;
using System.Collections.Generic;
using System.Text;

namespace MEMCore.Domain
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }
}
