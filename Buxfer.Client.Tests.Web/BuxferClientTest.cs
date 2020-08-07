using System;
using System.Threading.Tasks;
using FluentAssertions;
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
                    .And.Message.Contain("Email or username does not match an existing account."),
                async () => await target.Login()
            );
        }

        [Test]
        [Category("Password")]
        public async Task Login_ValidCredentials_Authenticated_And_returns_Token()
        {
            var logger = LoggerFactory.Create(c => c.AddConsole()).CreateLogger<AuthTest>();

            var target = new BuxferClient("andrey.leskov@gmail.com", SecretManager.LoadSettings().Password, logger);
            var token = await target.Login();
            Console.WriteLine($"token is {token}");
            Assert.NotNull(token);
            Assert.IsTrue(target.Authenticated);
        }

        [Test]
        public async Task Login_withAPITokenWorks()
        {
            var logger = LoggerFactory.Create(c => c.AddConsole()).CreateLogger<AuthTest>();

            var apiToken = SecretManager.LoadSettings().APIToken;
            var target = new BuxferClient(apiToken, logger);
            var token = await target.Login();
            token.Should().Be(apiToken);

            Assert.IsTrue(target.Authenticated);
        }
    }
}