using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace bitlyTest.Builders
{
    public class RedirectionResultBuilder : IRedirectionResultBuilder
    {
        private readonly HttpContext _httpContext;
        private string _targetLink;
        private readonly Dictionary<string, StringValues> _headers = new Dictionary<string, StringValues>();

        public RedirectionResultBuilder(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public IRedirectionResultBuilder WithTargetLink(string targetLink)
        {
            _targetLink = targetLink;
            return this;
        }

        public IRedirectionResultBuilder WithCustomHeader(string name, string value)
        {
            _headers.Add(name, value);
            return this;
        }

        public RedirectResult Please()
        {
            var targetLink = _targetLink ?? string.Empty;
            InitializeHeaders();

            return new RedirectResult(targetLink, true);
        }

        private void InitializeHeaders()
        {
            _headers.ToList().ForEach(x => _httpContext.Response.Headers.Add(x));
        }
    }
}