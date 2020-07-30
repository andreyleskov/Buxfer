namespace Buxfer.Client
{
    public class SharedBillCreationTransaction:Transaction
    {
        public SharedBillCreationTransaction()
        {
            Type = TransactionType.SharedBill;
        }
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