using System;
using System.Diagnostics;

namespace Buxfer.Client
{
    /// <summary>
    ///     Represents a transaction reminder.
    /// </summary>
    [DebuggerDisplay("{Description}: {Amount} {DueDateDescription}")]
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
        public ReminderAccount Account { get; set; } 
        public int? FromAccountId { get; set; } 
        public int? ToAccountId { get; set; } 
        public ReminderAccount FromAccount { get; set; }
        public ReminderAccount ToAccount { get; set; }
    }
}