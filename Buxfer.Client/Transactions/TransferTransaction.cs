using Buxfer.Client.Responses;

namespace Buxfer.Client
{
    public class TransferTransaction:Transaction
    {
        public TransferTransaction()
        {
            Type  = TransactionType.Transfer;  
        }
        public int  FromAccountId { get; set; }
        public int  ToAccountId { get;  set;}
    }

    public static class RawTransactionExtensions
    {
        public static TransferTransaction ToTransfer(this RawTransaction raw)
        {
            return new TransferTransaction()
            {
                AccountId = raw.accountId,
                AccountName = raw.accountName,
                Amount = raw.amount,
                Date = raw.date,
                Description = raw.description,
                FromAccountId = raw.fromAccount?.Id ?? 0,
                ToAccountId = raw.toAccount?.Id ?? 0,
                Id = raw.id,
                IsPending = raw.isPending,
                Type = raw.type,
                Status = raw.status,
                TagNames = raw.tagNames
            };
        }
    }
}