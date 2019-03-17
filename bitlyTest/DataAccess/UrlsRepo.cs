using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess
{
    public class UrlsRepo : IUrlsRepo
    {
        public void SaveShortLink(string originalUrl, string trimmedUrl)
        {
            throw new System.NotImplementedException();
        }

        public string GetOriginalLinkByTrimmerUrl(string trimmedUrl)
        {
            throw new System.NotImplementedException();
        }

        public List<TransformedData> GetTransformedData()
        {
            throw new System.NotImplementedException();
        }
    }
}
