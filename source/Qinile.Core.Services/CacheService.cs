using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using MoreLinq;
using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public class CacheService<T, I> where T : BaseModel<I> where I : struct
    {
        public string cacheKey;

        public CacheService(string cacheKey)
        {
            this.cacheKey = cacheKey;
        }

        public async Task<bool> Exists()
        {
            var keys = await BlobCache.Secure.GetAllKeys();
            return keys.Contains(cacheKey);
        }

        public async Task<T> GetObject()
        {
            var obj = default(T);
            var isCacheExists = await Exists();

            if (isCacheExists)
                obj = await BlobCache.Secure.GetObject<T>(cacheKey);

            return obj;
        }

        public async Task<List<T>> GetObjects()
        {
            var isCacheExists = await Exists();

            if (isCacheExists)
                return await BlobCache.Secure.GetObject<List<T>>(cacheKey);

            return new List<T>();
        }

        public async Task<T> GetObjectById(I id)
        {
            var obj = default(T);
            var isCacheExists = await Exists();

            if (isCacheExists)
            {
                var items = await BlobCache.Secure.GetObject<List<T>>(cacheKey);
                obj = items.FirstOrDefault(e => e.Id.Equals(id));
            }

            return obj;

        }

        public async Task<System.Reactive.Unit> SaveObject(T obj)
        {
            var items = await GetObjects();
            items.Add(obj);
            var uniques = items.Distinct();
            return await BlobCache.Secure.InsertObject(cacheKey, uniques);
        }

        public async Task<System.Reactive.Unit> SaveObjects(List<T> objs)
        {
            var items = await GetObjects();
            items.AddRange(objs);
            var uniques = items.DistinctBy(e => e.Id);
            return await BlobCache.Secure.InsertObject(cacheKey, uniques);
        }

        public async Task<int> UpdateObject(T obj)
        {
            var items = await GetObjects();
            var affected = await DeleteObject(obj.Id);

            await SaveObject(obj);

            return affected;
        }

        public async Task<int> UpdateObjects(List<T> objs)
        {
            var items = await GetObjects();
            var ids = objs.Select(item => item.Id).ToList();
            var affected = await DeleteObjects(ids);

            items.AddRange(objs);
            await SaveObjects(items);

            return affected;
        }

        public async Task<int> DeleteObject(I id)
        {
            var isCacheExists = await Exists();

            if (isCacheExists)
            {
                var items = await GetObjects();
                var affected = items.RemoveAll(item => item.Id.Equals(id));

                await BlobCache.Secure.InvalidateObject<T>(cacheKey);
                await SaveObjects(items);

                return affected;
            }

            return 0;
        }

        public async Task<int> DeleteObjects(List<I> ids)
        {
            var isCacheExists = await Exists();

            if (isCacheExists)
            {
                var items = await GetObjects();
                var affected = items.RemoveAll(x => ids.Contains(x.Id));

                await BlobCache.Secure.InvalidateObject<T>(cacheKey);
                await SaveObjects(items);

                return affected;
            }

            return 0;
        }
    }
}
