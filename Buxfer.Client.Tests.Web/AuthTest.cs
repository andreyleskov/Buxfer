using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Buxfer.Client.Tests.Web
{

    public class TemporaryEnvironment : IDisposable
    {
        private readonly IDictionary<string, string> _cleanUpScope;
        public TemporaryEnvironment(IDictionary<string,string> variables)
        {
            _cleanUpScope = variables.ToDictionary(pair => pair.Key, pair =>
            {
                
                 SetVariable(pair.Key, pair.Value, out var oldValue);
                 return oldValue;
            });
        }

        private static bool SetVariable(string variable, string variableValue, out string oldValue)
        {
            oldValue = Environment.GetEnvironmentVariable(variable);
            Environment.SetEnvironmentVariable(variable, variableValue);
            return oldValue != null;
        }

        public void Dispose()
        {
            foreach (var varToClean in _cleanUpScope)
            {
                if(varToClean.Value!=null)
                    Console.WriteLine($"Restoring env variable {varToClean.Key} to {varToClean.Value}");
                else
                    Console.WriteLine($"Deleting env variable {varToClean.Key}");

                Environment.SetEnvironmentVariable(varToClean.Key, varToClean.Value);
            }
        }
    }
    
    
    [TestFixture]

    public class SecretsManagerTests
    {

        [Test]
        public void PrintEnvSettings()
        {
            var envLoadedSettings = SecretManager.LoadSettings();
            var stringSettings = JsonConvert.SerializeObject(envLoadedSettings);
            Console.WriteLine("Loaded secret settings");
            Console.WriteLine(stringSettings);
        }
        
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