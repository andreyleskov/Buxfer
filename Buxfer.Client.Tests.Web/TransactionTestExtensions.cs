using Buxfer.Client.Responses;
using FluentAssertions;

namespace Buxfer.Client.Tests.Web
{
    public static class TransactionTestExtensions
    {
        public static void ShouldBeLike(this RawTransaction createdTransaction, Transaction transaction)
        {
            createdTransaction.Id.Should().BePositive();
            createdTransaction.Amount.Should().Be(transaction.Amount);
            createdTransaction.Description.Should().Be(transaction.Description);
            createdTransaction.Tags.Should().Be(transaction.Tags ?? "");
            createdTransaction.Type.Should().Be(transaction.Type);
            createdTransaction.TransactionType.Should().Be(transaction.Type);
            createdTransaction.SortDate.Should().Be(transaction.Date.Date);
            createdTransaction.IsPending.Should().Be(false);
            createdTransaction.IsFutureDated.Should().Be(false);
        }

        public static void ShouldBeLike(this Transaction createdTransaction, Transaction transaction)
        {
            createdTransaction.Id.Should().BePositive();
            createdTransaction.Amount.Should().Be(transaction.Amount);
            createdTransaction.Description.Should().Be(transaction.Description);
            createdTransaction.TagNames.Should().BeEquivalentTo(transaction.TagNames);
            createdTransaction.Type.Should().Be(transaction.Type);
            createdTransaction.IsPending.Should().Be(false);
        }
    }
}