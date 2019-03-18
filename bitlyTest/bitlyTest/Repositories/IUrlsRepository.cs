using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Models;

namespace bitlyTest.Repositories
{
    public interface IUrlsRepository
    {
        Task SaveShortLink(TransformationData transformationData);
        Task<string> GetOriginalLinkByTrimmerUrl(int trimmedUrlId);
        Task<List<TransformationData>> GetTransformedData();
        Task<long> GetNextId();
        void Increment(int id);
    }
}
