namespace Buxfer.Client
{
    public class RefundCreationRequest:TransactionCreationRequest
    {
        public override string Type { get; } = "refund";
    }
}