using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UrlShorter.Services.Common;
using UrlShorter.Services.Domain.Repositories;
using UrlShorter.Services.Persistence.Mongo.Repositories;

namespace UrlShorter.Services.Persistence.Mongo.Common
{
    public static class Extensions
    {
        public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
                 configuration.GetOptions<MongoDbOptions>("mongo"));

            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped(provider =>
            {
                var options = provider.GetRequiredService<MongoDbOptions>();
                var client = provider.GetRequiredService<MongoClient>();
                return client.GetDatabase(options.Database);
            });

            services.AddScoped<IMongoDbInitializer, MongoDbInitializer>();

            services.AddScoped<IMongoDbSeeder, MongoDbSeeder>();

        }
    }
}