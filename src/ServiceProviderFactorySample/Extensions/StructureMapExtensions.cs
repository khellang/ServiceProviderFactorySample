using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace ServiceProviderFactorySample
{
    public static class StructureMapExtensions
    {
        public static IServiceCollection AddStructureMap(this IServiceCollection services)
        {
            return services.AddSingleton<IServiceProviderFactory<Registry>>(new StructureMapServiceProviderFactory());
        }

        public static IWebHostBuilder UseStructureMap(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(services => services.AddStructureMap());
        }

        private class StructureMapServiceProviderFactory : IServiceProviderFactory<Registry>
        {
            public Registry CreateBuilder(IServiceCollection services)
            {
                var registry = new Registry();

                registry.Populate(services);

                return registry;
            }

            public IServiceProvider CreateServiceProvider(Registry registry)
            {
                return new Container(registry).GetInstance<IServiceProvider>();
            }
        }
    }
}