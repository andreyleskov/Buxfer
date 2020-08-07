using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Reminders")]
    public class RemindersTest
    {
        [Test]
        public async Task GetReminders_NoArgs_AllReminders()
        {
            var target = TestClientFactory.BuildClient();
            var reminders = await target.GetReminders();

            foreach (var reminder in reminders)
            {
                Assert.IsNotNull(reminder.AccountId);
                Assert.IsNotNull(reminder.Id);
                Assert.IsNotNull(reminder.Description);
                Assert.AreNotEqual(DateTime.MinValue, reminder.StartDate);
            }
        }
    }
}