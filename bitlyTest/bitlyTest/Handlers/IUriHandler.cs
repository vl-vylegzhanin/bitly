﻿using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Handlers
{
    public interface IUriHandler
    {
        Task<string> SaveUri(RequestPayload payload);
        Task<RedirectResult> GetOriginalUrlById(HttpContext httpContext, int id);
        Task<IEnumerable<TranformationData>> GetUrlsWithTransitionStatistics(string userGuid);
    }
}