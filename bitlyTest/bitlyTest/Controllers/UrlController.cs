using System;
using System.Threading.Tasks;
using bitlyTest.Models;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlsRepository _urlsRepository;

        public UrlController(IUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/urls/{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var originalUrl = await _urlsRepository.GetOriginalLinkByTrimmerUrl(id);

            if (originalUrl == null)
                return BadRequest("No such Id");

            await _urlsRepository.Increment(id);
            return Redirect(originalUrl);
            
        }

        [HttpPost]
        [Route("api/v{version:apiVersion}/urls/")]
        public async Task<ActionResult> Post([FromBody] RequestPayload payload)
        {
            var uriCheckResult = Uri.TryCreate(payload.Url, UriKind.Absolute, out var uriResult);

            if (!uriCheckResult)
                BadRequest("Url parameter is wrong");

            var id = await _urlsRepository.GetNextId();
            var transformationData = new TransformationData(id, payload.Url, 0);
            await _urlsRepository.SaveShortLink(transformationData);
            return Ok();
        }
    }
}