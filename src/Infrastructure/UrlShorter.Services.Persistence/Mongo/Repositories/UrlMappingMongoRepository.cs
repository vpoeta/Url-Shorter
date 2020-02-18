using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrlShorter.Services.Domain.Entities;
using UrlShorter.Services.Domain.Repositories;
using UrlShorter.Services.Persistence.Mongo.Documents;

namespace UrlShorter.Services.Persistence.Mongo.Repositories
{
    public class UrlMappingMongoRepository : IUrlMappingRepository
    {
        protected IMongoCollection<UrlMappingDocument> Collection { get; }

        public UrlMappingMongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<UrlMappingDocument>(collectionName);
        }

        public async Task<string> GetLongLinkAsync(string code)
        { 
            var document = await Collection.Find(x=>x.Code == code).SingleOrDefaultAsync();
            return document?.LongLink;
        }

        public async Task<UrlMapping> FindAndExtend(UrlMapping entity)
        {
            var filter = Builders<UrlMappingDocument>.Filter.Eq("LongLink", entity.LongLink);
            var options = new FindOneAndUpdateOptions<UrlMappingDocument> { ReturnDocument = ReturnDocument.After };
            var documentBefore = await Collection.FindOneAndUpdateAsync(filter,
                                    Builders<UrlMappingDocument>.Update.Set(s => s.ExpireAt, entity.ExpireAt),
                                    options);

            return documentBefore?.AsEntity();
        }
        public async Task AddAsync(UrlMapping entity)
            => await Collection.InsertOneAsync(entity.AsDocument());
    }
}
