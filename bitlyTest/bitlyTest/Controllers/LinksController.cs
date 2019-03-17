using System.Collections.Generic;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiVersion("1.0")]
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
    }
}
