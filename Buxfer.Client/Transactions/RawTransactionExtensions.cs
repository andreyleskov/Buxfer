using System;
using System.Data.Common;
using Buxfer.Client.Responses;

namespace Buxfer.Client.Transactions
{
    public static class RawTransactionExtensions
    {
        public static TransferTransaction ToTransfer(this RawTransaction raw)
        {
            return SetCommonFields(new TransferTransaction
            {
                FromAccountId = raw.FromAccount?.Id ?? 0,
                ToAccountId = raw.ToAccount?.Id ?? 0
            }, raw);
        }

        private static T SetCommonFields<T>(T t, RawTransaction raw) where T : Transaction
        {
            if (!DateTime.TryParse(raw.Date, out var date))
            {
                throw new Exception($"Cannot parse transaction {raw.Id} date");
            }
            
            t.AccountId = raw.AccountId;
            t.AccountName = raw.AccountName;
            t.Amount = raw.Amount;
            t.Date = date;
            t.Description = raw.Description;
            t.Id = raw.Id;
            t.IsPending = raw.IsPending;
            t.Type = raw.Type;
            t.Status = raw.Status;
            t.TagNames = raw.TagNames;
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