namespace Buxfer.Client
{
    public class SharedBillCreationRequest:TransactionCreationRequest
    {
        public override string Type { get; } = "sharedBill";
        /// <summary>
        /// [{"email", "amount"}] JSON-formatted array
        /// </summary>
        public string Payers { get; set; }
        /// <summary>
        /// [{"email", "amount"}] JSON-formatted array
        /// </summary>
        public string Sharers { get; set; }
        public bool IsEvenSplit { get; set; }
    }
}