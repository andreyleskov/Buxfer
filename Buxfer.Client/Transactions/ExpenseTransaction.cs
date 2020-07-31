namespace Buxfer.Client.Transactions
{
    public class ExpenseTransaction : Transaction
    {
        public ExpenseTransaction()
        {
            Type = TransactionType.Expense;
        }
    }
}