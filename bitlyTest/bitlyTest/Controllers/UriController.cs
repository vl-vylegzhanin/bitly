using System;
using System.Threading.Tasks;
using bitlyTest.Handlers;
using bitlyTest.Models;
using Microsoft.AspNetCore.Http;
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
            var userGuid = Request.Cookies["bitlyTestUserGuid"];
            payload.UserId = userGuid;

            var uriCheckResult = payload.Uri.StartsWith("http://") || payload.Uri.StartsWith("https://");

            if (uriCheckResult && Uri.IsWellFormedUriString(payload.Uri, UriKind.RelativeOrAbsolute))
                BadRequest("Uri parameter has wrong type");

            var userIdentifier = await _uriHandler.SaveUri(payload);
            SetCookie("bitlyTestUserGuid", userIdentifier);

            return Ok();
        }

        private void SetCookie(string key, string value)
        {
            var option = new CookieOptions { Expires = DateTime.Now.AddMonths(1) };

            Response.Cookies.Append(key, value, option);
        }
    }
}