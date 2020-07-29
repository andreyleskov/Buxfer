namespace Buxfer.Client
{
    public class TransferCreationRequest:TransactionCreationRequest
    {
        public override string Type { get; } = "transfer";
    }
}