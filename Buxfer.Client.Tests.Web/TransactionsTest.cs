using System;
using System.Linq;
using System.Threading.Tasks;
using Buxfer.Client.Responses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Transactions")]
    public class TransactionsTest
    {
        [Test]
        public async Task Given_expense_When_add_it_Then_it_is_recorded()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new ExpenseTransaction
            {
                Description = "Test expense transaction from Buxfer",
                Amount = 1.0m,
                AccountId = settings.AccountId,
                Date = DateTime.Now,
                Tags = "Buxfer.Client auto tests"
            };

            var createdTransaction = await target.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);
            
            createdTransaction.expenseAmount.Should().Be(transaction.Amount);

            var loadedTransaction = await LoadByTagAndId(target, transaction.Tags, createdTransaction.id);
            loadedTransaction.ShouldBeLike(transaction);
        }

        [Test]
        public async Task Given_expense_with_wrong_accounts_When_add_it_Then_receive_parse_error_response()
        {
            var client = TestClientFactory.BuildClient(out var settings);
            var transaction = new ExpenseTransaction
            {
                Description = "Test expense transaction with wrong account from Buxfer 2",
                Amount = 1.0m,
                Date = DateTime.Now,
                Tags = "Buxfer.Client auto tests",
            };

            var createdTransaction = await client.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);
            
            createdTransaction.expenseAmount.Should().Be(transaction.Amount);

            var loadedTransaction = await LoadByTagAndId(client, transaction.Tags, createdTransaction.id);
            loadedTransaction.ShouldBeLike(transaction);
        }

        
        private static async Task<ExtendedTransaction> LoadByTagAndId(BuxferClient target, string tags, int id)
        {
            var loadedTransactions = await target.GetExtendedTransactions(f => f.TagName = tags);
            var loadedTransaction = loadedTransactions.Entities.Should()
                .Contain(t => t.id == id).Subject;
            return loadedTransaction;
        }

        [Test]
        public async Task Given_income_When_add_it_Then_it_is_accepted()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new IncomeTransaction
            {
                Description = "Test expense transaction from Buxfer",
                Amount = 1.0m,
                AccountId = settings.AccountId,
                Date = DateTime.Now,
                Tags = "Buxfer.Client auto tests"
            };

            var createdTransaction = await target.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);
            
            createdTransaction.expenseAmount.Should().Be(transaction.Amount);

            var loadedTransaction = await LoadByTagAndId(target, transaction.Tags, createdTransaction.id);
            loadedTransaction.ShouldBeLike(transaction);
        }
        [Test]
        public async Task Given_transfer_When_add_it_Then_it_is_accepted()
        {
            throw new NotImplementedException();
        }
        [Test]
        public async Task Given_refund_When_add_it_Then_it_is_accepted()
        {
            throw new NotImplementedException();
        }
        [Test]
        public async Task Given_paidForFriend_When_add_it_Then_it_is_accepted()
        {
            throw new NotImplementedException();
        }
        [Test]
        public async Task Given_sharedBill_When_add_it_Then_it_is_accepted()
        {
            throw new NotImplementedException();
        }
        [Test]
        public async Task Given_malformed_expense_When_add_it_Then_receive_parse_error_response()
        {
            throw new NotImplementedException();
        }
        
       
        [Test]
        public async Task AddLegacy_Transaction_Transaction_Added()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new Transaction
            {
                Description = "Test transaction from BuxferSharp.FunctionalTest",
                Amount = 1,
                AccountName = settings.AccountName,
                Date = DateTime.Now.AddYears(1)
            };

            var actual = await target.AddTransaction(transaction);
            Assert.IsTrue(actual);

            var filter = new TransactionFilter
            {
                AccountName = settings.AccountName
            };
            var lastTransactions = await target.GetTransactions(filter);
            var last = lastTransactions.Entities.First();

            Assert.AreEqual(transaction.Description, last.Description);
            Assert.AreEqual(transaction.Amount, last.Amount);
            Assert.AreEqual(transaction.AccountName, last.AccountName);
        }

        [Test]
        public async Task GetTransactions_FilterByDate_FilteredTransactions()
        {
            var target = TestClientFactory.BuildClient();
            var filter = new TransactionFilter();
            filter.StartDate = DateTime.UtcNow.AddDays(-20);
            filter.EndDate = DateTime.UtcNow.AddDays(-10);
            var actual = await target.GetTransactions(filter);

            Assert.AreNotEqual(0, actual.TotalCount);

            foreach (var t in actual.Entities)
            {
                Assert.IsTrue(t.Date >= filter.StartDate.Value);
                Assert.IsTrue(t.Date <= filter.EndDate.Value);
            }
        }

        [Test]
        public async Task GetTransactions_FilterByString_FilteredTransactions()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var filter = new TransactionFilter();
            filter.AccountName = settings.AccountName;
            var actual = await target.GetTransactions(filter);

            Assert.AreNotEqual(0, actual.TotalCount);

            foreach (var t in actual.Entities)
                if (t.Type != TransactionType.Transfer)
                    Assert.AreEqual(settings.AccountName, t.AccountName);
        }

        [Test]
        public async Task GetTransactions_FilterByTag_FilteredTransactions()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var filter = new TransactionFilter();
            filter.TagId = settings.TagId;
            var actual = await target.GetTransactions(filter);

            Assert.AreNotEqual(0, actual.TotalCount);

            foreach (var t in actual.Entities) Assert.IsTrue(t.TagNames.Contains(settings.TagName));

            filter.TagId = null;
            filter.TagName = settings.TagName;
            actual = await target.GetTransactions(filter);

            Assert.AreNotEqual(0, actual.TotalCount);

            foreach (var t in actual.Entities) Assert.IsTrue(t.TagNames.Contains(settings.TagName));
        }

        [Test]
        public async Task GetTransactions_NoArgs_Firts25Transactions()
        {
            var target = TestClientFactory.BuildClient();
            var actual = await target.GetTransactions();

            Assert.IsTrue(target.Authenticated);
            Assert.AreNotEqual(0, actual.TotalCount);
            Assert.AreNotEqual(0, actual.Entities.Count());
            var first = actual.Entities[0];

            Assert.IsNotNull(first.AccountId);
            Assert.AreNotEqual(1, first.Date.Year);
        }

        [Test]
        public async Task GetTransactions_Page2_DiffResultPage1()
        {
            var target = TestClientFactory.BuildClient();
            var filter = new TransactionFilter();
            var actual = await target.GetTransactions(filter);
            var page1FirstTransaction = actual.Entities.First();

            filter.Page = 2;
            actual = await target.GetTransactions(filter);
            var page2FirstTransaction = actual.Entities.First();

            Assert.AreNotEqual(page1FirstTransaction.Id, page2FirstTransaction.Id);
        }
    }
}