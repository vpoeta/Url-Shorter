using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShorter.Services.Common;

namespace UrlShorter.Services.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<ICodeService, CodeService>();

            return services;
        }
    }
}
