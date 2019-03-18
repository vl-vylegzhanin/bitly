using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace bitlyTest.Repositories
{
    public class UrisRepository : IUrisRepository
    {
        private readonly IBitlyContext _context;
        public UrisRepository(IBitlyContext context)
        {
            _context = context;
        }

        public async Task SaveUrl(RedirectionData redirectionData)
        {
            await _context.TransformationData.InsertOneAsync(redirectionData);
        }

        public async Task<string> GetOriginalLinkByTrimmerUrl(int trimmedUrlId)
        {
            var filter = Builders<RedirectionData>.Filter.Eq(m => m.Id, trimmedUrlId);
            var queryResult =  await _context.TransformationData
                                             .Find(filter)
                                             .FirstOrDefaultAsync();

            return queryResult.OriginalUrl;
        }

        public async Task<List<RedirectionData>> GetTransformedData()
        {
            var queryResult = await _context.TransformationData
                                            .Find(_ => true)
                                            .ToListAsync();
            return queryResult;
        }

        public async Task<long> GetNextId()
        {
            return await _context.TransformationData.CountDocumentsAsync(new BsonDocument()) + 1;
        }

        public async Task Increment(int id)
        {
            var filter = Builders<RedirectionData>.Filter.Eq(m => m.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<RedirectionData>();
            await _context.TransformationData.UpdateOneAsync(filter, updateDefinition.Inc(nameof(RedirectionData.RedirectsCount), 1));
        }
    }
}
