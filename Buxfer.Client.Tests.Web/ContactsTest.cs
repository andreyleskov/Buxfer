using System.Threading.Tasks;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Contacts")]
    public class ContactsTest
    {
        [Test]
        public async Task GetContacts_NoArgs_AllContacts()
        {
            var target = TestClientFactory.BuildClient();
            var contacts = await target.GetContacts();

            foreach (var contact in contacts)
            {
                Assert.IsNotNull(contact.Id);
                Assert.IsNotNull(contact.Name);
            }
        }
    }
}