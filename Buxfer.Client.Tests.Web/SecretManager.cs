using System;
using Microsoft.Extensions.Configuration;

namespace Buxfer.Client.Tests.Web
{
    public static class SecretManager
    {
        public static SecretSettings LoadSettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<SecretSettings>()
                .Build();

            var settings = new SecretSettings();
            configuration.Bind(settings);

            if (String.IsNullOrEmpty(settings.UserId)
                || String.IsNullOrEmpty(settings.Password)
                || settings.AccountId == 0
                || String.IsNullOrEmpty(settings.AccountName)
                || String.IsNullOrEmpty(settings.TagId)
                || String.IsNullOrEmpty(settings.TagName))
                throw new InvalidOperationException(
                    "You'll need to define some user secrets before run BuxferSharp.FunctionalTests: UserId, Password, AccountId, AccountName, TagId and TagName.");

            return settings;
        }
    }
}