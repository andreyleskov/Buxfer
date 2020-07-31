using System.Collections.Generic;

namespace Buxfer.Client.Responses
{
    public class RawTransactionResponse : SuccessResponseBase
    {
        public int NumTransactions { get; set; }
        public List<RawTransaction> Transactions { get; internal set; }
    }
}