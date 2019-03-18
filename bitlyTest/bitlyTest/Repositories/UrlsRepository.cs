using System.Collections.Generic;
using System.Threading.Tasks;
using bitlyTest.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace bitlyTest.Repositories
{
    public class UrlsRepository : IUrlsRepository
    {
        private readonly IBitlyContext _context;
        public UrlsRepository(IBitlyContext context)
        {
            _context = context;
        }

        public async Task SaveShortLink(TransformationData transformationData)
        {
            await _context.TransformationData.InsertOneAsync(transformationData);
        }

        public async Task<string> GetOriginalLinkByTrimmerUrl(int trimmedUrlId)
        {
            var filter = Builders<TransformationData>.Filter.Eq(m => m.Id, trimmedUrlId);
            var queryResult =  await _context.TransformationData
                                             .Find(filter)
                                             .FirstOrDefaultAsync();

            return queryResult.OriginalUrl;
        }

        public async Task<List<TransformationData>> GetTransformedData()
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
            var filter = Builders<TransformationData>.Filter.Eq(m => m.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<TransformationData>();
            await _context.TransformationData.UpdateOneAsync(filter, updateDefinition.Inc(nameof(TransformationData.RedirectsCount), 1));
        }
    }
}
