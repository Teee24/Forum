using HangfirePractice.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangfirePractice
{
    public static class StockDataCache
    {
        private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
        private static readonly object LockObject = new object();
        private const string CacheKey = "StockDataBuffer";

        public static void AddData(IEnumerable<ETFInfo> data)
        {
            lock (LockObject)
            {
                var existingData = GetData();
                existingData.AddRange(data);

                Cache.Set(CacheKey, existingData);
                Console.WriteLine($"Added {data.Count()} items to cache at {DateTime.Now}");
            }
        }

        public static List<ETFInfo> GetAndClearData()
        {
            lock (LockObject)
            {
                var data = GetData();
                Cache.Remove(CacheKey);
                return data;
            }
        }

        // 只獲取暫存中的數據（不清除）
        public static List<ETFInfo> GetData()
        {
            lock (LockObject)
            {
                if (!Cache.TryGetValue(CacheKey, out List<ETFInfo> data))
                {
                    data = new List<ETFInfo>();
                    Cache.Set(CacheKey, data);
                }
                return data;
            }
        }
    }
}
