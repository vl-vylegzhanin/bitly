using bitlyTest.Models;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly IUrlsRepository _urlsRepository;

        public LinkController(IUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }

        [HttpGet]
        [Route("api/links/{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var originalUrl = _urlsRepository.GetOriginalLinkByTrimmerUrl(id);
            _urlsRepository.Increment(id);

            return Redirect(originalUrl.Result);
        }

        [HttpPut]
        [Route("api/links/{url}")]
        public ActionResult Put([FromRoute] string url)
        {
            var id = _urlsRepository.GetNextId();
            var transformationData = new TransformationData(id.Result, url, 0);
            _urlsRepository.SaveShortLink(transformationData);
            return Ok();
        }
    }
}