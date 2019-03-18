using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UrisController : ControllerBase
    {
        private readonly IUriHandler _uriHandler;

        public UrisController(IUriHandler uriHandler)
        {
            _uriHandler = uriHandler;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var result = await _uriHandler.GetUrlsWithTransitionStatistics();
            return Ok(result);
        }
    }
}
