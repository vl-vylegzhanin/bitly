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

        public async Task SaveUrl(TranformationData tranformationData)
        {
            var filter = Builders<TranformationData>.Filter.And(Builders<TranformationData>.Filter.Eq(m => m.OriginalUrl, tranformationData.OriginalUrl),
                                                                             Builders<TranformationData>.Filter.Eq(m => m.UserGuid, tranformationData.UserGuid));

            var queryResult = await _context.TransformationData
                                            .Find(filter)
                                            .FirstOrDefaultAsync();

            if (queryResult == null)
                await _context.TransformationData.InsertOneAsync(tranformationData);

        }

        public async Task<string> GetOriginalLinkById(int id)
        {
            var filter = Builders<TranformationData>.Filter.Eq(m => m.Id, id);
            var queryResult =  await _context.TransformationData
                                             .Find(filter)
                                             .FirstOrDefaultAsync();

            return queryResult.OriginalUrl;
        }

        public async Task<List<TranformationData>> GetTransformedData()
        {
            var queryResult = await _context.TransformationData
                                            .Find(_ => true)
                                            .ToListAsync();
            return queryResult;
        }

        public async Task<List<TranformationData>> GetTransformedDataByUserId(string userGuid)
        {
            var filter = Builders<TranformationData>.Filter.Eq(m => m.UserGuid, userGuid);
            var queryResult = await _context.TransformationData
                                            .Find(filter)
                                            .ToListAsync();
            return queryResult;
        }

        public async Task<long> GetNextId()
        {
            return await _context.TransformationData.CountDocumentsAsync(new BsonDocument()) + 1;
        }

        public async Task Increment(int id)
        {
            var filter = Builders<TranformationData>.Filter.Eq(m => m.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<TranformationData>();
            await _context.TransformationData.UpdateOneAsync(filter, updateDefinition.Inc(nameof(TranformationData.RedirectsCount), 1));
        }
    }
}
