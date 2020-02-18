using System.Threading.Tasks;

namespace UrlShorter.Services.Persistence.Mongo.Common
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}