using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bitlyTest.Models
{
    public class TranformationData
    {
        public TranformationData(long id, string originalUrl, string userGuid, int redirectsCount)
        {
            Id = id;
            OriginalUrl = originalUrl;
            RedirectsCount = redirectsCount;
            UserGuid = userGuid;
        }

        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public string OriginalUrl { get; set; }
        public int RedirectsCount { get; set; }
        public string UserGuid { get; set; }
    }
}