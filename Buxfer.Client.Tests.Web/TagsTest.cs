using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Tags")]
    public class TagsTest
    {
        [Test]
        public async Task GetTags_NoArgs_AllTags()
        {
            var target = TestClientFactory.BuildClient();
            var tags = await target.GetTags();

            Assert.AreNotEqual(0, tags.Count());

            foreach (var tag in tags)
            {
                Assert.IsNotNull(tag.Id);
                Assert.IsNotNull(tag.Name);
            }
        }
    }
}