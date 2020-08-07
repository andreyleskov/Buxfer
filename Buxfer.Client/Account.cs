using System;
using System.Diagnostics;

namespace Buxfer.Client
{
    /// <summary>
    ///     An account represents a real-life financial account you may own at a bank or credit card institution.
    /// </summary>
    [DebuggerDisplay("{Name}: {Balance}")]
    public class Account 
    {
        public int Id { get; set; } 
        public int TotalCreditLine { get; set; } 
        public decimal? AvailableCredit { get; set; } 
        public decimal? InterestRate { get; set; } 
        public string Name { get; set; }

 
        public string Bank { get; set; }

        public decimal Balance { get; set; }

    
        public string Currency { get; set; }

    
        public DateTime? LastSynced { get; set; }
    }
}