using System;
using Microsoft.Extensions.Configuration;

namespace Buxfer.Client.Tests.Web
{
    public static class SecretManager
    {
        public static string EnvironmentVariablePrefix { get; } = "Buxfer_Client_Tests_Web_";

        public static SecretSettings LoadSettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<SecretSettings>()
                .AddEnvironmentVariables(s => s.Prefix = EnvironmentVariablePrefix)
                .Build();

            var settings = new SecretSettings();
            configuration.Bind(settings);

            if (string.IsNullOrEmpty(settings.UserId)
                || string.IsNullOrEmpty(settings.Password)
                || settings.AccountId == 0
                || string.IsNullOrEmpty(settings.AccountName)
                || string.IsNullOrEmpty(settings.TagId)
                || string.IsNullOrEmpty(settings.TagName))
                throw new InvalidOperationException(
                    $"You'll need to define some user secrets before run Buxfer web tests or environment variables with prefix {EnvironmentVariablePrefix}: " +
                    "UserId, Password, AccountId, AccountName, TagId and TagName.");

            return settings;
        }
    }
}