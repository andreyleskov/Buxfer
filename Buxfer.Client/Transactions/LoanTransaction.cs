namespace Buxfer.Client
{
    public class LoanTransaction:Transaction
    {
        public LoanTransaction()
        {
            Type = TransactionType.Loan;
        }
        //TODO: create loan transaction in tests
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