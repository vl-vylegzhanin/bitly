using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Builders;
using bitlyTest.Models;
using bitlyTest.Repositories;
using bitlyTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Handlers
{
    public class UriHandler : IUriHandler
    {
        private readonly IUrisRepository _urisRepository;
        private readonly IUserIdentifierManager _userIdentifierManager;

        public UriHandler(IUrisRepository urisRepository, IUserIdentifierManager userIdentifierManager)
        {
            _urisRepository = urisRepository;
            _userIdentifierManager = userIdentifierManager;
        }

        public async Task<RedirectResult> GetOriginalUrlById(HttpContext httpContext, int id)
        {
            var originalUrl = await _urisRepository.GetOriginalLinkById(id);

            if (originalUrl != null)
            {
                await _urisRepository.Increment(id);
                return new RedirectionResultBuilder(httpContext).WithTargetLink(originalUrl).Please();
            }

            return null;
        }
        
        public async Task<IEnumerable<TranformationData>> GetUrlsWithTransitionStatistics(string userGuid)
        {
            List<TranformationData> queryResult;

            if (string.IsNullOrEmpty(userGuid))
                queryResult = await _urisRepository.GetTransformedData();
            else
                queryResult = await _urisRepository.GetTransformedDataByUserId(userGuid);

            return queryResult;
        }

        public async Task<string> SaveUri(RequestPayload payload)
        {
            var id = await _urisRepository.GetNextId();
            var userIdentifier = _userIdentifierManager.GetOrGenerateUserIdentifier(payload.UserId);
            var transformationData = new TranformationData(id, payload.Uri, userIdentifier, 0);
            await _urisRepository.SaveUrl(transformationData);

            return userIdentifier;
        }
    }
}
