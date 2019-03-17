using bitlyTest.Models;

namespace bitlyTest
{
    public class ServerConfig
    {
        public MongoDbConfig MongoDB { get; set; } = new MongoDbConfig();
    }
}
