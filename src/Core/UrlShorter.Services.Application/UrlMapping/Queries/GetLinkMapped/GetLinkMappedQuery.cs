using MediatR;

namespace UrlShorter.Services.Application.UrlMapping.Queries.GetLinkMapped
{
    public class GetLinkMappedQuery : IRequest<string>
    {
        public string Code { get; set; }
    }
}
