using System.Linq;
using System.Threading.Tasks;
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

            Assert.AreNotEqual(0, budgets.Count());
            foreach (var budget in budgets)
            {
                Assert.IsNotNull(budget.Id);
                Assert.IsNotNull(budget.Name);
                Assert.IsNotNull(budget.Period);
                Assert.AreEqual(budget.Balance, budget.Limit - budget.Spent);
            }
        }
    }
}