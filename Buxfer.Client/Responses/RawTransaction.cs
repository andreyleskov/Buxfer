using System;
using System.Collections.Generic;

namespace Buxfer.Client.Responses
{
    /// <summary>
    ///     Returned from API, has some duplicate fields
    /// </summary>
    public class RawTransaction
    {
        public int id { get; set; } //same in Transaction
        public string description { get; set; } //same in Transaction

        /// <summary>
        ///     day number and Month representation, seems to be a date for UI
        /// </summary>
        public string date { get; set; } //same in Transaction

        public TransactionType type { get; set; } //same in Transaction
        public decimal amount { get; set; } //same in Transaction
        public int accountId { get; set; } //same in Transaction

        public string accountName { get; set; } //same in Transaction
        public string tags { get; set; } //same in Transaction
        public List<string> tagNames { get; set; } //same in Transaction
        public TransactionStatus status { get; set; } //same in Transaction
        public bool isPending { get; set; } //same in Transaction

        // public string ExtraInfo { get; set; }//missing here, exists in Transaction

        public TransactionType transactionType { get; set; } //not Presented in Transaction
        public int rawTransactionType { get; set; } //not Presented in Transaction
        public DateTime normalizedDate { get; set; } //not Presented in Transaction
        public decimal expenseAmount { get; set; } //not Presented in Transaction
        public AccountInfo fromAccount { get; set; } //not Presented in Transaction
        public AccountInfo toAccount { get; set; } //not Presented in Transaction


        public bool isFutureDated { get; set; } //not Presented in Transaction

        public DateTime sortDate { get; set; } //not Presented in Transaction
    }
}