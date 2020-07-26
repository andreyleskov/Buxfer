using System.Threading.Tasks;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Loans")]
    public class LoansTest
    {
        [Test]
        public async Task GetLoans_NoArgs_AllLoans()
        {
            var target = TestClientFactory.BuildClient();
            var loans = await target.GetLoans();

            foreach (var loan in loans)
            {
                Assert.IsNotNull(loan.Description);
                Assert.IsNotNull(loan.Description);
            }
        }
    }
}