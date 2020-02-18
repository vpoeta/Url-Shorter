using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using UrlMappingEntity = UrlShorter.Services.Domain.Entities.UrlMapping;
using CustomValidationException = UrlShorter.Services.Application.Common.Exceptions.CustomValidationException;
using UrlShorter.Services.Domain.Repositories;
using UrlShorter.Services.Common;
using System.IO;
using UrlShorter.Services.Application.Common.Exceptions;

namespace UrlShorter.Services.Application.UrlMapping.Commands.CreateUrlMapping
{
    public class CreateUrlMappingCommandHandler : IRequestHandler<CreateUrlMappingCommand, string>
    {
        private const string clientEndpoint = "http://localhost:4200/";
        private const int intervalMinutes = 10;

        private readonly CreateUrlMappingCommandValidator _validator;
        private readonly IUrlMappingRepository _urlMappingRepository;
        private readonly ICodeService _codeService;
        private readonly IDateTime _dateProvider;

        public CreateUrlMappingCommandHandler(IUrlMappingRepository urlMappingRepository, ICodeService codeService, IDateTime dateProvider)
        {

            // TODO :: Check Fluent Validation with MediatR
            _validator = new CreateUrlMappingCommandValidator();
            _urlMappingRepository = urlMappingRepository ?? throw new ArgumentNullException(nameof(urlMappingRepository));
            _codeService = codeService ?? throw new ArgumentNullException(nameof(codeService));
            _dateProvider = dateProvider ?? throw new ArgumentNullException(nameof(dateProvider));
        }

        public async Task<string> Handle(CreateUrlMappingCommand request, CancellationToken cancellationToken)
        {
            ValidationResult results = _validator.Validate(request);

            if (!results.IsValid)
            {
                throw new CustomValidationException(results.Errors.First().ErrorMessage);
            }

            var attempts = 0;
            string _out;
            do
            {
                var code = await _codeService.GenerateCodeAsync();
                var entity = new UrlMappingEntity(
                    code,
                    Path.Combine(clientEndpoint, code),
                    request.LongUrl,
                    _dateProvider.Now.AddMinutes(intervalMinutes));

                try
                {
                    attempts++;
                    _out = await CreateUrlMapping(entity);
                    break;
                }
                catch (Exception ex)
                {
                    if (attempts == 3)
                        throw new CreateUrlMappingException("Error: create url mapping service not available", ex);
                }
            } while (true);

            return _out;
        }

        private async Task<string> CreateUrlMapping(UrlMappingEntity entity)
        {
            var beforeEntity = await _urlMappingRepository.FindAndExtend(entity);
            if (beforeEntity != null)
                return beforeEntity.ShortLink;

            await _urlMappingRepository.AddAsync(entity);
            return entity.ShortLink;
        }
    }
}
