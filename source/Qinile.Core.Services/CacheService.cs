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
    public class CacheService<M, I> where M : BaseModel<I>
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

        public async Task<M> GetObject()
        {
            var obj = default(M);
            var isCacheExists = await Exists();

            if (isCacheExists)
                obj = await BlobCache.Secure.GetObject<M>(cacheKey);

            return obj;
        }

        public async Task<List<M>> GetObjects()
        {
            var isCacheExists = await Exists();

            if (isCacheExists)
                return await BlobCache.Secure.GetObject<List<M>>(cacheKey);

            return new List<M>();
        }

        public async Task<M> GetObjectById(I id)
        {
            var obj = default(M);
            var isCacheExists = await Exists();

            if (isCacheExists)
            {
                var items = await BlobCache.Secure.GetObject<List<M>>(cacheKey);
                obj = items.FirstOrDefault(e => e.Id.Equals(id));
            }

            return obj;

        }

        public async Task<System.Reactive.Unit> SaveObject(M obj)
        {
            var items = await GetObjects();
            items.Add(obj);
            var uniques = items.Distinct();
            return await BlobCache.Secure.InsertObject(cacheKey, uniques);
        }

        public async Task<System.Reactive.Unit> SaveObjects(List<M> objs)
        {
            var items = await GetObjects();
            items.AddRange(objs);
            var uniques = items.DistinctBy(e => e.Id);
            return await BlobCache.Secure.InsertObject(cacheKey, uniques);
        }

        public async Task<int> UpdateObject(M obj)
        {
            var items = await GetObjects();
            var affected = await DeleteObject(obj.Id);

            await SaveObject(obj);

            return affected;
        }

        public async Task<int> UpdateObjects(List<M> objs)
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

                await BlobCache.Secure.InvalidateObject<M>(cacheKey);
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

                await BlobCache.Secure.InvalidateObject<M>(cacheKey);
                await SaveObjects(items);

                return affected;
            }

            return 0;
        }
    }
}
