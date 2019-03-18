using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Models;

namespace bitlyTest.Repositories
{
    public interface IUrisRepository
    {
        Task SaveUrl(RedirectionData redirectionData);
        Task<string> GetOriginalLinkByTrimmerUrl(int trimmedUrlId);
        Task<List<RedirectionData>> GetTransformedData();
        Task<long> GetNextId();
        Task Increment(int id);
    }
}
