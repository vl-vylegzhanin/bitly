using System;
using System.Threading.Tasks;
using bitlyTest.Handlers;
using bitlyTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    public class UriController : ControllerBase
    {
        private readonly IUriHandler _uriHandler;

        public UriController(IUriHandler uriHandler)
        {
            _uriHandler = uriHandler;
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/uris/{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var originalUrl = await _uriHandler.GetOriginalUrlById(HttpContext, id);

            if (originalUrl == null)
                return BadRequest("No such Id");

            return originalUrl;
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/v{version:apiVersion}/uris/")]
        public async Task<ActionResult> Post([FromBody] RequestPayload payload)
        {
            var uriCheckResult = payload.Uri.StartsWith("http://") || payload.Uri.StartsWith("https://");

            if (uriCheckResult && Uri.IsWellFormedUriString(payload.Uri, UriKind.RelativeOrAbsolute))
                BadRequest("Uri parameter has wrong type");

            await _uriHandler.SaveUri(payload);
            return Ok();
        }
    }
}