using MongoDB.Driver;

namespace bitlyTest.Models
{
    public class BitlyContext : IBitlyContext
    {
        private readonly IMongoDatabase _db;
        public BitlyContext(MongoDbCredentials config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<TransformationData> TransformationData => _db.GetCollection<TransformationData>("TransformationData");
    }
}
