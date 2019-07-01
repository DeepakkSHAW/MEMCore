using System;
using System.Collections.Generic;
using System.Text;

namespace MEMCore.Domain
{
    public class ExpenseDetail
    {
        public int Id { get; set; }
        public String Detail { get; set; }
        public Expense Expense { get; set; }
        public int ExpenseId { get; set; }
    }
} 
