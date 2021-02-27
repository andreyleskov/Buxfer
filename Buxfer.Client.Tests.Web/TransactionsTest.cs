using System;
using System.Linq;
using System.Threading.Tasks;
using Buxfer.Client.Responses;
using Buxfer.Client.Transactions;
using FluentAssertions;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Transactions")]
    public class TransactionsTest
    {
        private readonly string BuxferClientAutoTestsTag;

        public TransactionsTest()
        {
            BuxferClientAutoTestsTag = SecretManager.LoadSettings().TagName;
        }

        private static async Task<T> Load<T>(BuxferClient target, int id, string tag)
        {
            var loadedTransactions = await target.GetTransactions(f => f.TagName = tag);
            var loadedTransaction = loadedTransactions.Entities.Should()
                .Contain(t => t.Id == id).Subject;
            return loadedTransaction.Should().BeOfType<T>().Subject;
        }

        private static async Task<RawTransaction> LoadByTagAndId(BuxferClient target, int id, string tag)
        {
            var loadedTransactions = await target.GetRawTransactions(f => f.TagName = tag);
            var loadedTransaction = loadedTransactions.Entities.Should()
                .Contain(t => t.Id == id).Subject;
            return loadedTransaction;
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

            var actual = await target.AddTransactionAsSms(transaction);
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
        public async Task GetRawTransactions_NoArgs_First25Transactions()
        {
            var target = TestClientFactory.BuildClient();
            var actual = await target.GetRawTransactions();

            Assert.AreNotEqual(0, actual.TotalCount);
            Assert.AreNotEqual(0, actual.Entities.Count());
            var first = actual.Entities[0];
            Assert.IsNotNull(first.AccountId);
            DateTime.TryParse(first.Date, out var date).Should().BeTrue();
            Assert.AreNotEqual(1, date.Year);
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

            //can load some specialized transactions, not just general type
            actual.Entities.Should().Contain(e => e.GetType() != typeof(Transaction));
        }

        [Test]
        public async Task GetTransactions_Page2_DiffResultPage1()
        {
            var target = TestClientFactory.BuildClient();
            var filter = new TransactionFilter();
            var actual = await target.GetTransactions(filter);
            var page1FirstTransaction = actual.Entities.First();

            filter.Page = 2;
            var page2 = await target.GetTransactions(filter);
            var page2FirstTransaction = page2.Entities.First();

            Assert.AreNotEqual(page1FirstTransaction.Id, page2FirstTransaction.Id);
        }

        [Test]
        public async Task Given_expense_When_add_it_Then_it_is_recorded()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new ExpenseTransaction
            {
                Description = "Test expense transaction from Buxfer",
                Amount = 1.0m,
                AccountId = settings.AccountId,
                Date = DateTime.Now
            };
            transaction.TagNames.Add(BuxferClientAutoTestsTag);

            var createdTransaction = await target.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);

            createdTransaction.AccountId.Should().Be(transaction.AccountId);

            var loadedTransaction =
                await Load<ExpenseTransaction>(target, createdTransaction.Id, transaction.TagNames.First());
            loadedTransaction.ShouldBeLike(transaction);
        }

        [Test]
        public async Task Given_income_When_add_it_Then_it_is_accepted()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new IncomeTransaction
            {
                Description = "Test income transaction from Buxfer",
                Amount = 1.0m,
                AccountId = settings.AccountId,
                Date = DateTime.Now
            };
            transaction.TagNames.Add(BuxferClientAutoTestsTag);


            var createdTransaction = await target.AddTransaction(transaction);

            createdTransaction.ShouldBeLike(transaction);
            createdTransaction.Amount.Should().Be(transaction.Amount);
            createdTransaction.AccountId.Should().Be(transaction.AccountId);

            var loadedTransaction =
                await Load<IncomeTransaction>(target, createdTransaction.Id, transaction.TagNames.First());

            loadedTransaction.ShouldBeLike(transaction);
        }

        [Test]
        [Ignore("Cannot create such a transaction even in Buxfer UI ")]
        public async Task Given_paidForFriend_When_add_it_Then_it_is_accepted()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Given_refund_When_add_it_Then_it_is_accepted()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new RefundTransaction
            {
                Description = "Test refund transaction from Buxfer",
                Amount = 6.0m,
                Date = DateTime.Now,
                AccountId = settings.AccountId
            };
            transaction.TagNames.Add(BuxferClientAutoTestsTag);


            var createdTransaction = await target.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);

            createdTransaction.Amount.Should().Be(transaction.Amount);
            createdTransaction.AccountId.Should().Be(createdTransaction.AccountId);

            var loadedTransaction =
                await Load<RefundTransaction>(target, createdTransaction.Id, transaction.TagNames.First());
            loadedTransaction.ShouldBeLike(transaction);
        }

        [Test]
        [Ignore("Cannot create such a transaction even in Buxfer UI ")]
        public async Task Given_sharedBill_When_add_it_Then_it_is_accepted()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Given_transfer_with_only_from_account_When_add_it_Then_it_is_accepted()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new TransferTransaction
            {
                Description = "Test transfer transaction with only source account from Buxfer",
                Amount = 5.0m,
                FromAccountId = settings.AccountId,
                Date = DateTime.Now
            };
            transaction.TagNames.Add(BuxferClientAutoTestsTag);

            var createdTransaction = await target.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);

            createdTransaction.Amount.Should().Be(transaction.Amount);
            createdTransaction.AccountId.Should().Be(transaction.FromAccountId);
            createdTransaction.ToAccountId.Should().Be(0);

            var loadedTransaction =
                await Load<TransferTransaction>(target, createdTransaction.Id, transaction.TagNames.First());

            loadedTransaction.ShouldBeLike(transaction);
        }

        [Test]
        public async Task Given_transfer_with_only_to_account_When_add_it_Then_it_is_accepted()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new TransferTransaction
            {
                Description = "Test transfer transaction with only destination account from Buxfer",
                Amount = 5.0m,
                ToAccountId = settings.AccountId,
                Date = DateTime.Now
            };
            transaction.TagNames.Add(BuxferClientAutoTestsTag);

            var createdTransaction = await target.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);

            createdTransaction.Amount.Should().Be(transaction.Amount);
            createdTransaction.ToAccountId.Should().Be(transaction.ToAccountId);
            createdTransaction.AccountId.Should().Be(transaction.ToAccountId);
            createdTransaction.FromAccountId.Should().Be(0);

            var loadedTransaction =
                await Load<TransferTransaction>(target, createdTransaction.Id, transaction.TagNames.First());
            loadedTransaction.ShouldBeLike(transaction);
        }

        [Test]
        public async Task Given_transfer_with_two_accounts_When_add_it_Then_it_is_accepted()
        {
            var target = TestClientFactory.BuildClient(out var settings);
            var transaction = new TransferTransaction
            {
                Description = "Test transfer transaction from Buxfer",
                Amount = 5.0m,
                FromAccountId = settings.AccountId,
                ToAccountId = settings.AnotherAccountId,
                Date = DateTime.Now
            };
            transaction.TagNames.Add(BuxferClientAutoTestsTag);

            var createdTransaction = await target.AddTransaction(transaction);
            createdTransaction.ShouldBeLike(transaction);

            createdTransaction.Amount.Should().Be(transaction.Amount);
            createdTransaction.FromAccountId.Should().Be(transaction.FromAccountId);
            createdTransaction.ToAccountId.Should().Be(transaction.ToAccountId);
            createdTransaction.AccountId.Should().Be(0);

            var loadedTransaction =
                await Load<TransferTransaction>(target, createdTransaction.Id, transaction.TagNames.First());
            loadedTransaction.ShouldBeLike(transaction);
        }
    }
}