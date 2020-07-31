using Buxfer.Client.Responses;

namespace Buxfer.Client.Transactions
{
    public static class RawTransactionExtensions
    {
        public static TransferTransaction ToTransfer(this RawTransaction raw)
        {
            return SetCommonFields(new TransferTransaction
            {
                FromAccountId = raw.fromAccount?.Id ?? 0,
                ToAccountId = raw.toAccount?.Id ?? 0
            }, raw);
        }

        private static T SetCommonFields<T>(T t, RawTransaction raw) where T : Transaction
        {
            t.AccountId = raw.accountId;
            t.AccountName = raw.accountName;
            t.Amount = raw.amount;
            t.Date = raw.normalizedDate;
            t.Description = raw.description;
            t.Id = raw.id;
            t.IsPending = raw.isPending;
            t.Type = raw.type;
            t.Status = raw.status;
            t.TagNames = raw.tagNames;
            return t;
        }

        public static ExpenseTransaction ToExpense(this RawTransaction raw)
        {
            return SetCommonFields(new ExpenseTransaction(), raw);
        }

        public static IncomeTransaction ToIncome(this RawTransaction raw)
        {
            return SetCommonFields(new IncomeTransaction(), raw);
        }

        public static RefundTransaction ToRefund(this RawTransaction raw)
        {
            return SetCommonFields(new RefundTransaction(), raw);
        }

        public static LoanTransaction ToLoan(this RawTransaction raw)
        {
            return SetCommonFields(new LoanTransaction(), raw);
        }

        public static Transaction ToGeneral(this RawTransaction raw)
        {
            return SetCommonFields(new Transaction(), raw);
        }

        public static SharedBillTransaction ToSharedBill(this RawTransaction raw)
        {
            return SetCommonFields(new SharedBillTransaction(), raw);
        }

        public static PaidForFriendTransaction ToPaidForFriend(this RawTransaction raw)
        {
            return SetCommonFields(new PaidForFriendTransaction(), raw);
        }
    }
}