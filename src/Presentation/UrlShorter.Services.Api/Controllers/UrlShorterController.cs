using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UrlShorter.Services.Application.Common.Exceptions;
using UrlShorter.Services.Application.UrlMapping.Commands.CreateUrlMapping;
using UrlShorter.Services.Application.UrlMapping.Queries.GetLinkMapped;

namespace UrlShorter.Services.Api.Controllers
{
    public class UrlShorterController : BaseController
    {
        private readonly ILogger<UrlShorterController> _logger;

        public UrlShorterController(ILogger<UrlShorterController> logger) : base()
        {
            _logger = logger;
        }

        // GET: api/UrlShorter
        [HttpGet]
        [Route("{code}")]
        public async Task<ActionResult<string>> Get(string code)
        {
            var longLink = await Mediator.Send(new GetLinkMappedQuery { Code = code});
            return Ok(new { longLink });
        }

        // POST: api/UrlShorter
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] CreateUrlMappingCommand command)
        {
            string _shortLink;
            try
            {
                _shortLink = await Mediator.Send(command);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogWarning($"UrlShorter Service Create Url Shorter: Command : {@command} ErrorMessage: {@ex.Message}");
                return BadRequest();
            }
            catch (CreateUrlMappingException ex)
            {
                _logger.LogError($"UrlShorter Service Create Url Shorter: Command : {@command} ErrorMessage: {@ex.Message}");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            return Ok(new { shortLink = _shortLink });
        }
    }
}
