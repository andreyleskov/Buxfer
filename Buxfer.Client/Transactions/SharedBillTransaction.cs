namespace Buxfer.Client.Transactions
{
    public class SharedBillTransaction : Transaction
    {
        public SharedBillTransaction()
        {
            Type = TransactionType.SharedBill;
        }

        //TODO: create loan transaction in tests
        /// <summary>
        ///     [{"email", "amount"}] JSON-formatted array
        /// </summary>
        public string Payers { get; set; }

        /// <summary>
        ///     [{"email", "amount"}] JSON-formatted array
        /// </summary>
        public string Sharers { get; set; }

        public bool IsEvenSplit { get; set; }
    }
}