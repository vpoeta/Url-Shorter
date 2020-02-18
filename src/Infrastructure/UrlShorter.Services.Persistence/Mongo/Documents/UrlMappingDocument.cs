using System;
using UrlShorter.Services.Common;
using MongoDB.Bson;

namespace UrlShorter.Services.Persistence.Mongo.Documents
{
    public class UrlMappingDocument : IIdentifiable
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string ShortLink { get; set; }

        public string LongLink { get; set; }

        public BsonDateTime ExpireAt { get; set; }
    }
}
