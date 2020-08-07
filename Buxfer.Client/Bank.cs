using System.Collections.Generic;

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
}