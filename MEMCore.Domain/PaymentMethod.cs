using System.ComponentModel;

namespace MEMCore.Domain
{
    public enum PaymentMethod
    {
        [Description("Cash")] Cash = 0,
        [Description("Card")] Card = 1,
        [Description("Mobile Payments")] MobilePayments = 3,
        [Description("Direct Deposit")] DirectDeposit = 4,
        [Description("PayPal")] Paypal = 5,
        [Description("Bank Cheque")] BankCheque = 6,
        [Description("Prepaid Cards")] PrepaidCards = 7,
        [Description("E wallets")] Ewallets = 8,
        [Description("BankTransfers")] BankTransfers = 9,
        [Description("Others")] Others = 10
    }
}
