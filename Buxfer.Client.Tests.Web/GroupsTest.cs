using System.Threading.Tasks;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Groups")]
    public class GroupsTest
    {
        [Test]
        public async Task GetGroups_NoArgs_AllGroups()
        {
            var target = TestClientFactory.BuildClient();
            var groups = await target.GetGroups();

            foreach (var group in groups)
            {
                Assert.IsNotNull(group.Id);
                Assert.IsNotNull(group.Name);
                Assert.IsNotNull(group.Members);

                foreach (var member in group.Members)
                {
                    Assert.IsNotNull(member.Id);
                    Assert.IsNotNull(member.Name);
                }
            }
        }
    }
}