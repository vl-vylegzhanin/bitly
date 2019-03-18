using bitlyTest.Models;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly IUrlsRepository _urlsRepository;

        public LinkController(IUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/links/{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var originalUrl = _urlsRepository.GetOriginalLinkByTrimmerUrl(id);
            _urlsRepository.Increment(id);

            return Redirect(originalUrl.Result);
        }
    }
}