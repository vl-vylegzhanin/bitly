using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Models;

namespace bitlyTest.Repositories
{
    public interface IUrisRepository
    {
        Task SaveUrl(TranformationData tranformationData);
        Task<string> GetOriginalLinkById(int id);
        Task<List<TranformationData>> GetTransformedData();
        Task<List<TranformationData>> GetTransformedDataByUserId(string userGuid);
        Task<long> GetNextId();
        Task Increment(int id);
    }
}
