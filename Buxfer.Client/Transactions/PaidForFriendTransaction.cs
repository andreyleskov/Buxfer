namespace Buxfer.Client.Transactions
{
    public class PaidForFriendTransaction : Transaction
    {
        public PaidForFriendTransaction()
        {
            Type = TransactionType.PaidForFriend;
        }

        //TODO: create loan transaction in tests
        /// <summary>
        ///     uid | email
        /// </summary>
        public string PaidBy { get; set; }

        /// <summary>
        ///     uid | email
        /// </summary>
        public string PaidFor { get; set; }
    }
}