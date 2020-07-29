namespace Buxfer.Client
{
    public class LoanCreationRequest:TransactionCreationRequest
    {
        public override string Type { get; } = "loan";
        /// <summary>
        /// uid | email
        /// </summary>
        public string LoanedBy { get; set; }
        /// <summary>
        /// uid | email
        /// </summary>
        public string BorrowedBy { get; set; }
    }
}