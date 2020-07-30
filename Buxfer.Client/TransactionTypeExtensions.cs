using System;

namespace Buxfer.Client
{
    public static class TransactionTypeExtensions
    {
        public static string ToApiString(this TransactionType type)
        {
            switch (type)
            {
                case TransactionType.Expense:return "expense";
                case TransactionType.Income: return "income";
                case TransactionType.Transfer: return "transfer";
                case TransactionType.Refund: return "refund";
                case TransactionType.PaidForFriend: return "paidForFriend";
                case TransactionType.SharedBill: return "sharedBill";
                case TransactionType.Loan: return "loan";
                case TransactionType.Settlement: return "settlement";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Cannot determine transactio type");
            }
        }
    }
}