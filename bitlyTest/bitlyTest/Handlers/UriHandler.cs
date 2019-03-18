using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Builders;
using bitlyTest.Models;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Handlers
{
    public class UriHandler : IUriHandler
    {
        private readonly IUrisRepository _urisRepository;

        public UriHandler(IUrisRepository urisRepository)
        {
            _urisRepository = urisRepository;
        }

        public async Task<RedirectResult> GetOriginalUrlById(HttpContext httpContext, int id)
        {
            var originalUrl = await _urisRepository.GetOriginalLinkByTrimmerUrl(id);

            if (originalUrl != null)
            {
                await _urisRepository.Increment(id);
                return new RedirectionResultBuilder(httpContext).WithTargetLink(originalUrl).Please();
            }

            return null;
        }
        
        public async Task<IEnumerable<RedirectionData>> GetUrlsWithTransitionStatistics()
        {
            var queryResult = await _urisRepository.GetTransformedData();
            return queryResult;
        }

        public async Task SaveUri(RequestPayload payload)
        {
            var id = await _urisRepository.GetNextId();
            var transformationData = new RedirectionData(id, payload.Uri, 0);
            await _urisRepository.SaveUrl(transformationData);
        }
    }
}
