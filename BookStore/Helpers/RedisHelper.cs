using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Redis;

namespace BookStore.Helpers
{
    public static class RedisHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("cache-yy.redis.cache.windows.net,abortConnect=false,ssl=true,password=9ud+d5xcBpfTfAf7H7Ft1V+/8pmSBfg58rrPulLVZ+0=");
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public static bool HasKey(string key)
        {
            IDatabase cache = Connection.GetDatabase();
            if (key == String.Empty)
            {
                key = "_thinkHowToCacheAll";
            }
            return cache.KeyExists(key);
        }

        public static string GetFromCache(string key)
        {
            IDatabase cache = Connection.GetDatabase();
            if (key == String.Empty)
            {
                key = "_thinkHowToCacheAll";
            }
            return cache.StringGet(key);
        }

        public static void Set(string key, string data)
        {
            IDatabase cache = Connection.GetDatabase();
            if (key == String.Empty)
            {
                key = "_thinkHowToCacheAll";
            }

            cache.StringSet(key, data);
        }

        public static void Invalidate()
        {
            var endpoints = Connection.GetEndPoints();
            var server = Connection.GetServer(endpoints.First());
            IDatabase cache = Connection.GetDatabase();
            var keys = server.Keys();
            foreach (var key in keys)
            {
                cache.KeyDelete(key);
            }
        }
    }
}