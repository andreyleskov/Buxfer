namespace Buxfer.Client
{
    public class IncomeCreationRequest:TransactionCreationRequest
    {
        public override string Type { get; } = "income";
    }
}