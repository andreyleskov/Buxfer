using Microsoft.Extensions.Logging;

namespace Buxfer.Client.Tests.Web
{
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