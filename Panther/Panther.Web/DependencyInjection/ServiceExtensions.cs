using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panther.Clients.Steam;

namespace Panther.Web.DependencyInjection
{
    public static class ServiceExtensions
    {
        private const string API_KEY_SECTION = "ApiKeys";
        private const string API_BASE_ADDRESSES = "ApiBaseAddresses";

        private const string STEAM_KEY = "Steam";

        public static IServiceCollection ConfigureInternalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection apiKeySection = configuration.GetSection(API_KEY_SECTION);
            IConfigurationSection apiBaseAddressSection = configuration.GetSection(API_BASE_ADDRESSES);

            services
                .AddTransient<ISteamClient, SteamClient>(s => new SteamClient(apiBaseAddressSection[STEAM_KEY], apiKeySection[STEAM_KEY]));

            return services;
        }
    }
}
