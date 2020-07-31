using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{

    public class TemporaryEnvironment : IDisposable
    {
        private readonly IDictionary<string, bool> _cleanUpScope;
        public TemporaryEnvironment(IDictionary<string,string> variables)
        {
            _cleanUpScope = variables.ToDictionary(pair => pair.Key, pair => SetVariable(pair.Key, pair.Value));
        }

        private static bool SetVariable(string variable, string variableValue)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            Environment.SetEnvironmentVariable(variable, variableValue);
            return value != null;
        }

        public void Dispose()
        {
            foreach (var varToClean in _cleanUpScope.Where(pair => pair.Value == false))
                Environment.SetEnvironmentVariable(varToClean.Key, null);
        }
    }
    
    
    [TestFixture]

    public class SecretsManagerTests
    {
        [Test]
        public void Given_env_Variables_When_building_secrets_Then_variables_are_fetched()
        {
            var settings = new SecretSettings()
            {
                AccountId = 1,
                AccountName = "test",
                AnotherAccountId = 34,
                APIToken = "234",
                Password = "heh",
                TagId = "123",
                TagName = "yey",
                UserId = "123"
            };
            
            var prefix = SecretManager.EnvironmentVariablePrefix;
            var variables = new Dictionary<string,string>()
            {
                {$"{prefix}AccountId",settings.AccountId.ToString()},
                {$"{prefix}AccountName",settings.AccountName},
                {$"{prefix}AnotherAccountId",settings.AnotherAccountId.ToString()},
                {$"{prefix}APIToken",settings.APIToken},
                {$"{prefix}Password",settings.Password},
                {$"{prefix}TagId",settings.TagId},
                {$"{prefix}TagName",settings.TagName},
                {$"{prefix}UserId",settings.UserId},
            };

            using var tempEnv = new TemporaryEnvironment(variables);
            var envLoadedSettings = SecretManager.LoadSettings();
            envLoadedSettings.Should().BeEquivalentTo(settings);
        }
    }
    
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
                    .And.Message.EqualTo("Email or username does not match an existing account."),
                async () => { await target.GetTransactions(); });
        }
    }
}