using System;
using System.ComponentModel.DataAnnotations;

namespace MEMCore.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max Length of Expense Title is 50")]
        public String ExpenseTitle { get; set; }
        public double ExpensesAmount { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }
        [Required]
        [StringLength(2, ErrorMessage ="The {0}'s Max length can't exceed {1} characters.", MinimumLength =1)]
        //"The {0} must be at least {2} characters long."
        public string Signature { get; set; }
        public string ExpenseDetail { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
    }
}
