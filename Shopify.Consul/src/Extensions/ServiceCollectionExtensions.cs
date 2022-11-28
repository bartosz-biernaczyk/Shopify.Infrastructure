using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopify.Consul.Builders;
using Shopify.Consul.Http;
using Shopify.Consul.Models;
using Shopify.Consul.Options;
using Shopify.Consul.Services;

namespace Shopify.Consul.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection serviceCollection, Func<IConsulOptionsBuilderInitial, IConsulOptionsBuilderFinal> configuration)
        {
            var finalBuilder = configuration(new ConsulOptionsBuilder());

            return serviceCollection.AddConsul(finalBuilder.Build());
        }

        public static IServiceCollection AddConsul(this IServiceCollection serviceCollection, string consulSection = "consul")
        {
            var options = serviceCollection.BuildServiceProvider()
                .GetRequiredService<IConfiguration>()
                .GetOptions<ConsulOptions>(consulSection);

            return serviceCollection.AddConsul(options);
        }

        public static IServiceCollection AddConsul(this IServiceCollection serviceCollection, ConsulOptions options)
        {
            serviceCollection.AddSingleton(options);

            if (!options.Enabled)
            {
                return serviceCollection;
            }

            ServiceDetails serviceDetails = new()
            {
                Name = options.ServiceName,
                ID = $"{options.ServiceName}:{Guid.NewGuid()}",
                Address = options.ServiceAddress,
                Port = options.Port,
                Tags = options.Tags
            };

            if (!string.IsNullOrEmpty(options.PingUrl))
            {
                serviceDetails.Checks = new[] {
                new ServiceCheck()
                {
                    Method = "GET",
                    HTTP = options.PingUrl,
                    Interval = $"{options.PingInterval.TotalSeconds}s",
                    Timeout = $"{options.Timeout.TotalSeconds}s",
                    SuccessBeforePassing = 1,
                    FailuresBeforeWarning = 1,
                    FailuresBeforeCritical = 2,
                    DeregisterCriticalServiceAfter = $"{options.DeregisterAfter.TotalMinutes}m"
                }
                };
            }

            serviceCollection.AddSingleton(serviceDetails);
            serviceCollection.AddTransient<IServiceRegistryProvider, ServiceRegistryProvider>();
            serviceCollection.AddHttpClient<IConsulService, ConsulService>("consul-service", client => client.BaseAddress = new Uri(options.ConsulUrl));

            if (options.UseHttpClient)
            {
                serviceCollection.AddHttpClient("consul")
                .AddHttpMessageHandler(serviceProvider => new ServiceDiscoveryMessageHandler(serviceProvider.GetRequiredService<IServiceRegistryProvider>()));
            }

            serviceCollection.AddHostedService<ConsulHostedService>();

            return serviceCollection;
        }
    }
}
