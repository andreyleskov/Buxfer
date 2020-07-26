using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Buxfer.Client.Tests.Web
{
    public static class TestClientFactory
    {
        public static Settings LoadSettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Settings>()
                .Build();

            var settings = new Settings();
            configuration.Bind(settings);

            if (string.IsNullOrEmpty(settings.UserId)
                || string.IsNullOrEmpty(settings.Password)
                || settings.AccountId == 0
                || string.IsNullOrEmpty(settings.AccountName)
                || string.IsNullOrEmpty(settings.TagId)
                || string.IsNullOrEmpty(settings.TagName))
                throw new InvalidOperationException(
                    "You'll need to define some user secrets before run BuxferSharp.FunctionalTests: UserId, Password, AccountId, AccountName, TagId and TagName.");

            return settings;
        }

        public static BuxferClient BuildClient(Settings settings, ILogger logger = null)
        {
            logger ??= LoggerFactory.Create(c => c.AddConsole()).CreateLogger<AuthTest>();
            return new BuxferClient(settings.UserId, settings.Password, logger);
        }

        public static BuxferClient BuildClient(out Settings setting, ILogger logger = null)
        {
            setting = LoadSettings();
            return BuildClient(setting, logger);
        }

        public static BuxferClient BuildClient(ILogger logger = null)
        {
            return BuildClient(out var setting, logger);
        }
    }
}