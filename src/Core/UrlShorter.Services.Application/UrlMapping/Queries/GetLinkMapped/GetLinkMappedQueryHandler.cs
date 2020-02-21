using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UrlShorter.Services.Application.Common.Exceptions;
using UrlShorter.Services.Domain.Repositories;

namespace UrlShorter.Services.Application.UrlMapping.Queries.GetLinkMapped
{
    public class GetLinkMappedQueryHandler : IRequestHandler<GetLinkMappedQuery, string>
    {
        private readonly IUrlMappingRepository _urlMappingRepository;

        public GetLinkMappedQueryHandler(IUrlMappingRepository urlMappingRepository)
        {
            _urlMappingRepository = urlMappingRepository ?? throw new ArgumentNullException(nameof(urlMappingRepository));
        }

        public async Task<string> Handle(GetLinkMappedQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.Code))
            { 
                throw new BadRequestException("Code cannot be null or empty string.");
            }

            var longLink = await _urlMappingRepository.GetLongLinkAsync(request.Code);

            if (string.IsNullOrEmpty(longLink))
            {
                throw new NotFoundException(nameof(longLink), request.Code);
            }

            return longLink;
        }
    }
}
