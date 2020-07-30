namespace Buxfer.Client
{
    public class PaidForFriendTransaction:Transaction
    {
        public PaidForFriendTransaction()
        {
            Type= TransactionType.PaidForFriend;
        }
        /// <summary>
        /// uid | email
        /// </summary>
        public string PaidBy { get; set; }
        /// <summary>
        /// uid | email
        /// </summary>
        public string PaidFor { get; set; }
    }
}