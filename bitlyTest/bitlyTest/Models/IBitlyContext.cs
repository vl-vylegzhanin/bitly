using MongoDB.Driver;

namespace bitlyTest.Models
{
    public interface IBitlyContext
    {
        IMongoCollection<TranformationData> TransformationData { get; }
    }
}