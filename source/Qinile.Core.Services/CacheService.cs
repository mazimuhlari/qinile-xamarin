using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Qinile.Core.Models;
using MoreLinq;

namespace Qinile.Core.Services
{
    public class CacheService<T, I> where T : BaseModel<I> where I : struct
    {
        public async Task<bool> Contains(string key)
        {
            var keys = await BlobCache.Secure.GetAllKeys();
            return keys.Contains(key);
        }

        public async Task<T> GetObject(string key)
        {
            var obj = default(T);
            var isCacheExists = await Contains(key);

            if (isCacheExists)
                obj = await BlobCache.Secure.GetObject<T>(key);

            return obj;
        }

        public async Task<List<T>> GetObjects(string key)
        {
            var isCacheExists = await Contains(key);

            if (isCacheExists)
                return await BlobCache.Secure.GetObject<List<T>>(key);

            return new List<T>();
        }

        public async Task<T> GetObjectById(string key, I id)
        {
            var obj = default(T);
            var isCacheExists = await Contains(key);

            if (isCacheExists)
            {
                var items = await BlobCache.Secure.GetObject<List<T>>(key);
                obj = items.FirstOrDefault(e => e.Id.Equals(id));
            }

            return obj;

        }

        public async Task<System.Reactive.Unit> SaveObject(string key, T obj)
        {
            var items = await GetObjects(key);
            items.Add(obj);
            var uniques = items.Distinct();
            return await BlobCache.Secure.InsertObject(key, uniques);
        }

        public async Task<System.Reactive.Unit> SaveObjects(string key, List<T> objs)
        {
            var items = await GetObjects(key);
            items.AddRange(objs);
            var uniques = items.DistinctBy(e => e.Id);
            return await BlobCache.Secure.InsertObject(key, uniques);
        }

        public async Task<int> UpdateObject(string key, T obj)
        {
            var items = await GetObjects(key);
            var affected = await DeleteObject(key, obj.Id);

            await SaveObject(key, obj);

            return affected;
        }

        public async Task<int> UpdateObjects(string key, List<T> objs)
        {
            var items = await GetObjects(key);
            var ids = objs.Select(item => item.Id).ToList();
            var affected = await DeleteObjects(key, ids);

            items.AddRange(objs);
            await SaveObjects(key, items);

            return affected;
        }

        public async Task<int> DeleteObject(string key, I id)
        {
            var isCacheExists = await Contains(key);

            if (isCacheExists)
            {
                var items = await GetObjects(key);
                var affected = items.RemoveAll(item => item.Id.Equals(id));

                await BlobCache.Secure.InvalidateObject<T>(key);
                await SaveObjects(key, items);

                return affected;
            }

            return 0;
        }

        public async Task<int> DeleteObjects(string key, List<I> ids)
        {
            var isCacheExists = await Contains(key);

            if (isCacheExists)
            {
                var items = await GetObjects(key);
                var affected = items.RemoveAll(x => ids.Contains(x.Id));

                await BlobCache.Secure.InvalidateObject<T>(key);
                await SaveObjects(key, items);

                return affected;
            }

            return 0;
        }
    }
}
