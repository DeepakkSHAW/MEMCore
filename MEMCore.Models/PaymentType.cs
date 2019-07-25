using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace MEMCore.Models
{
    // [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentType
    {
        [Description("Paid")] Paid = 0,
        [Description("Reimbursed")] Reimbursed = 1,
        [Description("Refunded")] Refunded = 2
    }
}
