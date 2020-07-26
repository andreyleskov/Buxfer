using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Accounts")]
    public class AccountsTest
    {
        [Test]
        public async Task GetAccounts_NoArgs_AllAccounts()
        {
            var target = TestClientFactory.BuildClient();
            var accounts = await target.GetAccounts();

            Assert.AreNotEqual(0, accounts.Count());

            foreach (var account in accounts)
            {
                Assert.IsNotNull(account.Id);
                Assert.IsNotNull(account.Name);
                Assert.IsNotNull(account.Currency);
            }
        }
    }
}