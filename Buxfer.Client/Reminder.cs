using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Buxfer.Client
{
    
    public class Bank    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public bool CanSync { get; set; } 
        public bool RequireVerifiedEmail { get; set; } 
        public int SyncProvider { get; set; } 
        public int Status { get; set; } 
        public List<string> LoginFields { get; set; } 
    }

    public class Account2    {
        public int Type { get; set; } 
        public Bank Bank { get; set; } 
        public bool CanSync { get; set; } 
        public double Balance { get; set; } 
        public double BalanceInDefaultCurrency { get; set; } 
        public bool HasTransactions { get; set; } 
        public string Status { get; set; } 
        public bool NeedsSyncMigration { get; set; } 
        public bool CanEditHoldings { get; set; } 
        public bool IsInvestmentType { get; set; } 
        public object SyncId { get; set; } 
        public string SyncSaveMode { get; set; } 
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string LastSynced { get; set; } 
        public int TotalCreditLine { get; set; } 
    }

    /// <summary>
    ///     Represents a transaction reminder.
    /// </summary>
    [DebuggerDisplay("{Description}: {Amount} {Period}")]
    public class Reminder
    {
        public int Id { get; set; } 
        public DateTime? NextExecution { get; set; } 
        public string DueDateDescription { get; set; } 
        public int NumDaysForDueDate { get; set; } 
        public string EditMode { get; set; } 
        public int? PeriodSize { get; set; } 
        public PeriodUnits? PeriodUnit { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime? StopDate { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string Type { get; set; } 
        public int TransactionType { get; set; } 
        public double Amount { get; set; } 
        public int AccountId { get; set; } 
        public Account2 Account { get; set; } 
        public int? FromAccountId { get; set; } 
        public int? ToAccountId { get; set; } 
        public Account2 FromAccount { get; set; }
        public Account2 ToAccount { get; set; }
    }
}