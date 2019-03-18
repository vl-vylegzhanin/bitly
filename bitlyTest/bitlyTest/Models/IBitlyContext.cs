using MongoDB.Driver;

namespace bitlyTest.Models
{
    public interface IBitlyContext
    {
        IMongoCollection<RedirectionData> TransformationData { get; }
    }
}