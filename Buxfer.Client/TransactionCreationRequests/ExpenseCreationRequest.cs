namespace Buxfer.Client
{
    public class ExpenseCreationRequest:TransactionCreationRequest
    {
        public override string Type { get; } = "expense";
    }
}