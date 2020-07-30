using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
    public static class TestClientFactory
    {
        public static BuxferClient BuildClient(SecretSettings settings, ILogger logger = null)
        {
            logger ??= LoggerFactory.Create(c => c.AddConsole().SetMinimumLevel(LogLevel.Trace)).CreateLogger<AuthTest>();
            
            if(string.IsNullOrEmpty(settings.APIToken))
                 return new BuxferClient(settings.UserId, settings.Password, logger);
            
            return new BuxferClient(settings.APIToken,logger);
        }

        public static BuxferClient BuildClient(out SecretSettings setting, ILogger logger = null)
        {
            setting = SecretManager.LoadSettings();
            return BuildClient(setting, logger);
        }

        public static BuxferClient BuildClient(ILogger logger = null)
        {
            return BuildClient(out var setting, logger);
        }
    }
}