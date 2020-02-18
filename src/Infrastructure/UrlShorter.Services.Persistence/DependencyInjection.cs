using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShorter.Services.Common;
using UrlShorter.Services.Persistence.Mongo.Common;
using MongoDB.Driver;
using UrlShorter.Services.Persistence.Mongo.Documents;
using UrlShorter.Services.Infrastructure;
using UrlShorter.Services.Persistence.Mongo.Repositories;
using UrlShorter.Services.Domain.Repositories;

namespace UrlShorter.Services.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddMongo(configuration);

            services.AddMongoRepository<UrlMappingDocument>("UrlMapping");
            return services;
        }

        public static void AddMongoRepository<TEntity>(this IServiceCollection services, string collectionName)
            where TEntity : IIdentifiable
        {
            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped<IUrlMappingRepository, UrlMappingMongoRepository>();
            
            services.AddScoped<IUrlMappingRepository>(provider =>
            {
                var database = provider.GetRequiredService<IMongoDatabase>();
                return new UrlMappingMongoRepository(database, collectionName);
            });
        }
    }
}
