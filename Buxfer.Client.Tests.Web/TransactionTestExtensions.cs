using Buxfer.Client.Responses;
using FluentAssertions;

namespace Buxfer.Client.Tests.Web
{
    public static class TransactionTestExtensions
    {
        public static void ShouldBeLike(this ExtendedTransaction createdTransaction, Transaction transaction)
        {
            createdTransaction.id.Should().BePositive();
            createdTransaction.amount.Should().Be(transaction.Amount);
            createdTransaction.description.Should().Be(transaction.Description);
            createdTransaction.tags.Should().Be(transaction.Tags ?? "");
            createdTransaction.type.Should().Be(transaction.Type);
            createdTransaction.accountId.Should().Be(transaction.AccountId);
            createdTransaction.transactionType.Should().Be(transaction.Type);
            createdTransaction.sortDate.Should().Be(transaction.Date.Date);
            createdTransaction.isPending.Should().Be(false);
            createdTransaction.isFutureDated.Should().Be(false);
        }
    }
}