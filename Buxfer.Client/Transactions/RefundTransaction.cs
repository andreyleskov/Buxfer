namespace Buxfer.Client.Transactions
{
    public class RefundTransaction : Transaction
    {
        public RefundTransaction()
        {
            Type = TransactionType.Refund;
        }
    }
}