using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEMExplorer
{
    class MEMExplorerModels
    {
    }
    public class Category
    {
        public int id { get; set; }
        public string categoryName { get; set; }
    }
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
    }
    public class Expense
    {
        public int Id { get; set; }
        public String ExpenseTitle { get; set; }
        public double ExpensesAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Signature { get; set; }
        public string ExpenseDetail { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
    }
}
