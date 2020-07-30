using System.Collections.Generic;

namespace Buxfer.Client.Responses
{
    public class ExtendedTransactionResponse : SuccessResponseBase
    {
        public int NumTransactions { get; set; }
        public List<RawTransaction> Transactions { get; internal set; }
    }
}