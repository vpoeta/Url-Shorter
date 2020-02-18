using MediatR;

namespace UrlShorter.Services.Application.UrlMapping.Commands.CreateUrlMapping
{
    public class CreateUrlMappingCommand : IRequest<string>
    {
        public string LongUrl { get; set; }
    }
}
