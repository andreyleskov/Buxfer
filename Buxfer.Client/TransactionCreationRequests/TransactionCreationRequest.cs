using System;

namespace Buxfer.Client
{
    public abstract class TransactionCreationRequest
    {
        public string Description { get; set; }
        public decimal  Amount { get;  set;}
        public string  AccountId { get; set; }
        public string  FromAccountId { get; set; }
        public string  ToAccountId { get;  set;}
        public DateTime  Date { get; set; }
        public string Tags { get; set; }
        public TransactionStatus Status { get; set;}
        public abstract string Type { get; }
    }
}