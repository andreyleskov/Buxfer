namespace Buxfer.Client
{
    public class RefundCreationTransaction:Transaction
    {
        public RefundCreationTransaction()
        {
            Type = TransactionType.Refund;
        }
    }
}