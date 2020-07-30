namespace Buxfer.Client
{
    public class IncomeTransaction:Transaction
    {
        public IncomeTransaction()
        {
            Type= TransactionType.Income;
        }
    }
}