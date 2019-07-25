using System.ComponentModel;

namespace MEMCore.Domain
{
    public enum PaymentType
    {
        [Description("Paid")] Paid = 0,
        [Description("Reimbursed")] Reimbursed = 1,
        [Description("Refunded")] Refunded = 2
    }
}
