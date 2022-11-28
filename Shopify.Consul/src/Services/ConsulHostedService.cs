using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shopify.Consul.Models;

namespace Shopify.Consul.Services
{
    public class ConsulHostedService : IHostedService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<ConsulHostedService> logger;

        public ConsulHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<ConsulHostedService> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var consulService = scope.ServiceProvider.GetRequiredService<IConsulService>();
            var serviceDetails = scope.ServiceProvider.GetRequiredService<ServiceDetails>();

            logger.LogInformation("Registering service in Consul. ServiceName: '{serviceId}'", serviceDetails.ID);

            var registration = await consulService.RegisterAsync(serviceDetails, cancellationToken);

            if (!registration.IsSuccessStatusCode)
            {
                logger.LogError("Service registration was not successfull.");
                return;
            }

            logger.LogInformation("Service registered successfully.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var consulService = scope.ServiceProvider.GetRequiredService<IConsulService>();
            var serviceDetails = scope.ServiceProvider.GetRequiredService<ServiceDetails>();

            logger.LogInformation("Registering service in Consul. ServiceName: '{serviceId}'", serviceDetails.ID);

            var deregistrationResult = await consulService.DeregisterAsync(serviceDetails.ID!, cancellationToken);

            if (!deregistrationResult.IsSuccessStatusCode)
            {
                logger.LogError("Service deregistration was not successfull.");
                return;
            }

            logger.LogInformation("Service deregistered successfully.");
        }
    }
}
