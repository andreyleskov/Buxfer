namespace Buxfer.Client
{
    public class ReminderAccount    {
        public int Type { get; set; } 
        public Bank Bank { get; set; } 
        public bool CanSync { get; set; } 
        public decimal Balance { get; set; } 
        public bool BalanceInDefaultCurrency { get; set; } 
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
}