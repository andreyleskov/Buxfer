using System.Collections.Generic;

namespace Buxfer.Client.Responses
{
    public class ExtendedTransactionResponse : SuccessResponseBase
    {
        public int NumTransactions { get; set; }
        public List<ExtendedTransaction> Transactions { get; internal set; }
    }
}