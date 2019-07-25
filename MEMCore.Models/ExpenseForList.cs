using System;

namespace MEMCore.Models
{
    public class ExpenseForList
    {
        public string ExpenseTitle { get; set; }
        public double ExpensesAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Signature { get; set; }
        public string ExpenseDetail { get; set; }
        // [JsonIgnore]
        //[JsonConverter(typeof(StringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        // [JsonIgnore]
        //[JsonConverter(typeof(StringEnumConverter))]
        public PaymentType PaymentType { get; set; }
        //public string PaymentMethodValue { get; set; }
        //public string PaymentTypeValue { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
    }
}
