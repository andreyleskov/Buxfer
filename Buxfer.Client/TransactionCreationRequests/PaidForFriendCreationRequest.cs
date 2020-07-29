namespace Buxfer.Client
{
    public class PaidForFriendCreationRequest:TransactionCreationRequest
    {
        public override string Type { get; } = "paidForFriend";
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