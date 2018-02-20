using StackExchange.Redis;

namespace MPDex.CacheRepository
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
