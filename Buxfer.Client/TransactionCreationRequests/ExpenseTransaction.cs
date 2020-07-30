namespace Buxfer.Client
{
    public class ExpenseTransaction:Transaction
    {
        public ExpenseTransaction()
        {
            Type= TransactionType.Expense;
        }
    }
}