using UrlShorter.Services.Domain.Entities;

namespace UrlShorter.Services.Persistence.Mongo.Documents
{
    public static class Extensions
    {

        public static UrlMapping AsEntity(this UrlMappingDocument document)
            => new UrlMapping(document.Code, document.ShortLink, document.LongLink, document.ExpireAt.ToUniversalTime());

        public static UrlMappingDocument AsDocument(this UrlMapping entity)
            => new UrlMappingDocument
            {
                Code = entity.Code,
                ShortLink = entity.ShortLink,
                LongLink = entity.LongLink,
                ExpireAt = entity.ExpireAt
            };
    }
}
