using Microsoft.Extensions.Configuration;

namespace OrderManagementSystem.Core
{
    public static class ConfigurationHelpers
    {
        public static string ReadSetting(this IConfiguration configuration, string key)
        {
            try
            {
                var appSettings = configuration.GetSection($"AppSettings:{key}").Value;
                return string.IsNullOrWhiteSpace(appSettings) ? "" : appSettings;

            }
            catch (Exception ex)
            {
                throw new Exception($"Program Ayar Okuma :{ex.Message}; ");
            }
        }
    }
}
