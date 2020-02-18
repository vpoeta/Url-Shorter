using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UrlShorter.Services.Application.UrlMapping.Commands.CreateUrlMapping
{
    public class CreateUrlMappingCommandValidator : AbstractValidator<CreateUrlMappingCommand>
    {
        public CreateUrlMappingCommandValidator()
        {
            RuleFor(x => x.LongUrl).NotEmpty()
                .WithMessage("Please ensure that you have entered your Link");

            RuleFor(x => x.LongUrl).Custom((uriInput, context) => {
                Uri uriResult;

                if (Uri.TryCreate(uriInput, UriKind.Absolute, out uriResult))
                {
                    if (!(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    {
                        context.AddFailure("The input must to be a valid Link!");
                    }
                }
                else
                {
                    context.AddFailure("The input must to be a valid Link!");
                }

            });


        }
    }
}
