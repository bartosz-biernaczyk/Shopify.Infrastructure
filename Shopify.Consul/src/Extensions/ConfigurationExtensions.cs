using Microsoft.Extensions.Configuration;

namespace Shopify.Consul.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
            where TOptions : class, new()
        {
            var options = new TOptions();

            var key = string.IsNullOrWhiteSpace(sectionName) ? GetDefaultName<TOptions>() : sectionName;

            configuration.GetSection(key).Bind(options);

            return options;
        }

        private static string GetDefaultName<TOptions>()
        {
            return typeof(TOptions)
                .Name
                .ToLowerInvariant()
                .Replace("options", "")
                .Replace("settings", "");
        }
    }
}
