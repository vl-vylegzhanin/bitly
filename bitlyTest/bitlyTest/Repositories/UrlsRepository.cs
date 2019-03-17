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
            return await _context.TransformationData
                                 .Find(_ => true)
                                 .ToListAsync();
        }

        public async Task<long> GetNextId()
        {
            return await _context.TransformationData.CountDocumentsAsync(new BsonDocument()) + 1;
        }

        public void Increment(int id)
        {
            var filter = Builders<TransformationData>.Filter.Eq(m => m.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<TransformationData>();
            _context.TransformationData.FindOneAndUpdate<TransformationData>(filter, updateDefinition.Inc("RedirectsCount", 1));
        }
    }
}
