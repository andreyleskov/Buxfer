namespace Buxfer.Client.Transactions
{
    public class IncomeTransaction : Transaction
    {
        public IncomeTransaction()
        {
            Type = TransactionType.Income;
        }
    }
}