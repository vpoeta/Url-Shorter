using System;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UrlShorter.Services.Application.Common.Behaviours;
using UrlShorter.Services.Common;

namespace UrlShorter.Services.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddInitializers(this IServiceCollection services, params Type[] initializers)
            => initializers == null
                ? services
                : services.AddTransient<IStartupInitializer, StartupInitializer>(c =>
                {
                    var startupInitializer = new StartupInitializer();
                    var validInitializers = initializers.Where(t => typeof(IInitializer).IsAssignableFrom(t));
                    foreach (var initializer in validInitializers)
                    {
                        startupInitializer.AddInitializer(c.GetService(initializer) as IInitializer);
                    }

                    return startupInitializer;
                });
    }
}
