﻿using MongoDB.Driver;

namespace bitlyTest.Models
{
    public class BitlyContext : IBitlyContext
    {
        private readonly IMongoDatabase _db;
        public BitlyContext(MongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<TranformationData> TransformationData => _db.GetCollection<TranformationData>("TranformationData");
    }
}
