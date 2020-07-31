namespace Buxfer.Client.Transactions
{
    public class TransferTransaction : Transaction
    {
        public TransferTransaction()
        {
            Type = TransactionType.Transfer;
        }

        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
    }
}