using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Budgets")]
    public class BudgetsTest
    {
        [Test]
        public async Task GetBudgets_NoArgs_AllBudgets()
        {
            var target = TestClientFactory.BuildClient();
            var budgets = await target.GetBudgets();

            budgets.Should().NotBeEmpty();
            budgets.Select(b => b.Id).Should().OnlyContain(b => b > 0);
            budgets.Select(b => b.Name).Should().OnlyContain(n => !string.IsNullOrEmpty(n));
            budgets.Should().OnlyContain(b => b.Balance == b.Limit - b.Spent);
        }
    }
}