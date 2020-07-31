using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{
    [TestFixture]
    public class SecretsManagerTests
    {
        [Test]
        public void Given_env_Variables_When_building_secrets_Then_variables_are_fetched()
        {
            var settings = new SecretSettings
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
            var variables = new Dictionary<string, string>
            {
                {$"{prefix}AccountId", settings.AccountId.ToString()},
                {$"{prefix}AccountName", settings.AccountName},
                {$"{prefix}AnotherAccountId", settings.AnotherAccountId.ToString()},
                {$"{prefix}APIToken", settings.APIToken},
                {$"{prefix}Password", settings.Password},
                {$"{prefix}TagId", settings.TagId},
                {$"{prefix}TagName", settings.TagName},
                {$"{prefix}UserId", settings.UserId}
            };

            using var tempEnv = new TemporaryEnvironment(variables);
            var envLoadedSettings = SecretManager.LoadSettings();
            envLoadedSettings.Should().BeEquivalentTo(settings);
        }

        [Test]
        [Ignore("Not secure")]
        public void PrintEnvSettings()
        {
            var envLoadedSettings = SecretManager.LoadSettings();
            var stringSettings = JsonConvert.SerializeObject(envLoadedSettings);
            Console.WriteLine("Loaded secret settings");
            Console.WriteLine(stringSettings);
        }
    }
}