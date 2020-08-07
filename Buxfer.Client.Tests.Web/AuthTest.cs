using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    [Category("Auth")]
    public class AuthTest
    {
        [Test]
        public void Authenticator_InvalidCredentials_Exception()
        {
            var logger = LoggerFactory.Create(c => c.AddConsole()).CreateLogger<AuthTest>();
            var target = new BuxferClient("andrey.lesk@gmail.com", "bk", logger);

            Assert.ThrowsAsync(Is.TypeOf<BuxferException>()
                    .And.Message.Contain("Email or username does not match an existing account."),
                async () => { await target.GetTransactions(); });
        }
    }
}