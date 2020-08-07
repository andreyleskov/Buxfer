using System;
using System.Diagnostics;

namespace Buxfer.Client
{
    public enum BudgetType
    {
        Expense=1,Income=2
    }

    public enum PeriodUnits
    {
        Week, Day, Year, Month, Custom, Never
    }
    /// <summary>
    ///     Budgets let you control your expenses and plan your savings.
    ///     You can set weekly, monthly and yearly limits on specific expense categories, and receive alerts whenever you
    ///     exceed those limits.
    /// </summary>
    [DebuggerDisplay("{Name}: {Limit} - {Spent} = {Balance}")]
    public class Budget
    {
        public string EditMode { get; set; } 
        public int? PeriodSize { get; set; } 
        public PeriodUnits? PeriodUnit { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime? StopDate { get; set; } 
        public int BudgetId { get; set; }
        public BudgetType Type { get; set; } 
        public Tag Tag { get; set; } 
        public bool IsRolledOver { get; set; } 
        public int EventId { get; set; } 
        public int Id { get; set; }
        public string Name { get; set; }
        public string TagId { get; set; }
        public decimal Limit { get; set; }
        public decimal Spent { get; set; }
        public decimal Balance { get; set; }
        public string Period { get; set; }
    }
}