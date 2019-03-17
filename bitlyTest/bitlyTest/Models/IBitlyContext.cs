using MongoDB.Driver;

namespace bitlyTest.Models
{
    public interface IBitlyContext
    {
        IMongoCollection<TransformationData> TransformationData { get; }
    }
}