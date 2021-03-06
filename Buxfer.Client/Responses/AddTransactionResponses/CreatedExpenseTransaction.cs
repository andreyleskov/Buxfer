using System.Collections.Generic;

namespace Buxfer.Client.Responses.AddTransactionResponses
{
    public class CreatedExpenseTransaction
    {
        public int id { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string normalizedDate { get; set; }
        public string type { get; set; }
        public string transactionType { get; set; }
        public int rawTransactionType { get; set; }
        public int amount { get; set; }
        public int expenseAmount { get; set; }
        public int accountId { get; set; }
        public string accountName { get; set; }
        public string tags { get; set; }
        public List<string> tagNames { get; set; }
        public string status { get; set; }
        public bool isFutureDated { get; set; }
        public bool isPending { get; set; }
        public string sortDate { get; set; }
    }
}