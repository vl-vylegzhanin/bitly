using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Models;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlsRepository _urlsRepository;

        public UrlsController(IUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var result = await _urlsRepository.GetTransformedData();
            return Ok(result);
        }

        
    }
}
