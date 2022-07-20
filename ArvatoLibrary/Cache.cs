using System;
using System.Collections.Concurrent;

namespace ArvatoLibrary {
    public static class Cache {
        private static ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();
        private static DateTime? timestamp = DateTime.UtcNow;
        private readonly static object cacheLock = new object();

        public static T Get<T>(string key)
            where T : class {

            if(cache.TryGetValue(key, out object value)) {
                return value as T;
            }

            return default (T);
        }

        public static bool Add<T>(string key, T value)
           where T : class {

            return cache.TryAdd(key, value);
        }

        public static void Clear() {
            lock (cacheLock) {
                cache = new ConcurrentDictionary<string, object>();
                timestamp = DateTime.UtcNow;
            }
        }
    }
}
