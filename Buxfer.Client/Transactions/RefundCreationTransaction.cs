namespace Buxfer.Client
{
    public class RefundTransaction:Transaction
    {
        public RefundTransaction()
        {
            Type = TransactionType.Refund;
        }
    }
}