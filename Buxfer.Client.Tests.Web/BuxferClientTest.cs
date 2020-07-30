using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    public class BuxferClientTest
    {
        [Test]
        public void Login_InvalidCredentials_Exception()
        {
            var logger = LoggerFactory.Create(c => c.AddConsole()).CreateLogger<AuthTest>();
            var target = new BuxferClient("john@doe.com", "dohdoh", logger);
            Assert.ThrowsAsync(Is.TypeOf<BuxferException>()
                    .And.Message.EqualTo("Email or username does not match an existing account."),
                async () => await target.Login()
            );
        }

        [Test]
        public async Task Login_ValidCredentials_Authenticated_And_returns_Token()
        {
            var logger = LoggerFactory.Create(c => c.AddConsole()).CreateLogger<AuthTest>();

            var target = new BuxferClient("andrey.leskov@gmail.com", SecretManager.LoadSettings().Password, logger);
            var token = await target.Login();
            Console.WriteLine($"token is {token}");
            Assert.NotNull(token);
            Assert.IsTrue(target.Authenticated);
        }
        
    }
}