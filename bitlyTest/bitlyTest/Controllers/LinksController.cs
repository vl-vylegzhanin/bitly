using System;
using System.Collections.Generic;
using bitlyTest.Models;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IUrlsRepository _urlsRepository;

        public LinksController(IUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_urlsRepository.GetTransformedData());
        }

        [HttpPost]
        public ActionResult Post([FromBody] RequestPayload payload)
        {
            var uriCheckResult = Uri.TryCreate(payload.Url, UriKind.Absolute, out var uriResult);

            if (!uriCheckResult)
                BadRequest("Url parameter is wrong");

            var id = _urlsRepository.GetNextId();
            var transformationData = new TransformationData(id.Result, payload.Url, 0);
            _urlsRepository.SaveShortLink(transformationData);
            return Ok();
        }
    }
}
