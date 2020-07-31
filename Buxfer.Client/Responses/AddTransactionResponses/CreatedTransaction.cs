using System;
using System.Collections.Generic;

namespace Buxfer.Client.Responses.AddTransactionResponses
{
    public class CreatedTransaction
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public DateTime normalizedDate { get; set; }
        public TransactionType type { get; set; }
        public TransactionType transactionType { get; set; }
        public int rawTransactionType { get; set; }
        public decimal amount { get; set; }
        public decimal expenseAmount { get; set; }
        public int accountId { get; set; }
        public string accountName { get; set; }
        public string tags { get; set; }
        public List<string> tagNames { get; set; }
        public TransactionStatus status { get; set; }
        public bool isFutureDated { get; set; }
        public bool isPending { get; set; }
        public DateTime sortDate { get; set; }
        public AccountInfo fromAccount { get; set; }
    }
}