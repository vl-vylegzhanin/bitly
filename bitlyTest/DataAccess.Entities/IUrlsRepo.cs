using System.Collections.Generic;

namespace DataAccess.Entities
{
    public interface IUrlsRepo
    {
        void SaveShortLink(string originalUrl, string trimmedUrl);
        string GetOriginalLinkByTrimmerUrl(string trimmedUrl);
        List<TransformedData> GetTransformedData();
    }
}
