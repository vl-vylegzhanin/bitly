using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Builders
{
    public interface IRedirectionResultBuilder
    {
        IRedirectionResultBuilder WithTargetLink(string targetLink);
        IRedirectionResultBuilder WithCustomHeader(string name, string value);
        RedirectResult Please();
    }
}
