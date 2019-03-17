using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bitlyTest.Models
{
    public class TransformationData
    {
        public TransformationData() { }

        public TransformationData(long id, string originalUrl, int redirectsCount)
        {
            Id = id;
            OriginalUrl = originalUrl;
            RedirectsCount = redirectsCount;
        }

        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public string OriginalUrl { get; set; }
        public int RedirectsCount { get; set; }
    }
}