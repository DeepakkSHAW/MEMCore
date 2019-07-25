using System;

namespace MEMCore.Domain
{
    public class Expense
    {
        public int Id { get; set; }
        public String ExpenseTitle { get; set; }
        //public String ExpensesDetails { get; set; }
        public double ExpensesAmount { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.Now;
        public string Signature { get; set; }
        //only for audit purposes
        public DateTime inDate { get; private set; }
        //only for audit purposes
        public DateTime updateDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentType PaymentType { get; set; }
        public int ExpenseCategoryId { get; set; }
        public int CurrencyId { get; set; }
        //Navigation properties
        public ExpenseDetail ExpenseDetail { get; set; }
        public ExpenseCategory Category { get; set; }
        public Currency Currency { get; set; }
        
    }
}
