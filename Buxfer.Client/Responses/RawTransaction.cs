using System;
using System.Collections.Generic;

namespace Buxfer.Client.Responses
{
    /// <summary>
    ///     Returned from API, has some duplicate fields
    /// </summary>
    public class RawTransaction
    {
        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.Id"/>
        /// </summary>
        public int Id { get; set; } 
        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.Description"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Day number and Month representation, seems to be a date for UI
        /// </summary>
        public string Date { get; set; } 

        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.Type"/>
        /// </summary>
        public TransactionType Type { get; set; }
        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.Amount"/>
        /// </summary>
        public decimal Amount { get; set; } 
        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.AccountId"/>
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.AccountName"/>
        /// </summary>
        public string AccountName { get; set; } 
        /// <summary>
        /// Not populated automatically as in Transaction
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.TagNames"/>
        /// </summary>
        public List<string> TagNames { get; set; } 
        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.Status"/>
        /// </summary>
        public TransactionStatus Status { get; set; }
        /// <summary>
        /// Mapped to the same <see cref="Buxfer.Client.Transaction"/> <see cref="Buxfer.Client.Transaction.IsPending"/>
        /// </summary>
        public bool IsPending { get; set; } 
        /// <summary>
        /// Not presented in <see cref="Buxfer.Client.Transaction"/> as it duplicates <see cref="Buxfer.Client.Transaction.Type"/>
        /// </summary>
        public TransactionType TransactionType { get; set; } 
        /// <summary>
        /// Not presented in <see cref="Buxfer.Client.Transaction"/> as it duplicates <see cref="Buxfer.Client.Transaction.Type"/> as int value
        /// </summary>
        public int RawTransactionType { get; set; } 
        /// <summary>
        /// Presented in <see cref="Buxfer.Client.Transaction"/> as <see cref="Buxfer.Client.Transaction.Date"/> 
        /// </summary>
        public DateTime NormalizedDate { get; set; } 
        /// <summary>
        /// Presented in <see cref="Buxfer.Client.Transaction"/> as <see cref="Buxfer.Client.Transaction.Amount"/> 
        /// </summary>
        public decimal ExpenseAmount { get; set; } 
        /// <summary>
        /// Presented in <see cref="Buxfer.Client.Transactions.TransferTransaction"/> as <see cref="Buxfer.Client.Transactions.TransferTransaction.FromAccountId"/> 
        /// </summary>
        public AccountInfo FromAccount { get; set; } 
        /// <summary>
        /// Presented in <see cref="Buxfer.Client.Transactions.TransferTransaction"/> as <see cref="Buxfer.Client.Transactions.TransferTransaction.ToAccountId"/> 
        /// </summary>
        public AccountInfo ToAccount { get; set; } 
        /// <summary>
        /// Not presented in <see cref="Buxfer.Client.Transaction"/>  
        /// </summary>
        public bool IsFutureDated { get; set; } 
        /// <summary>
        /// Not presented in <see cref="Buxfer.Client.Transaction"/> 
        /// </summary>
        public DateTime SortDate { get; set; } 
    }
}