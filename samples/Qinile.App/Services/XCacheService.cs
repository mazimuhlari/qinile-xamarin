using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akavache;
using Qinile.App.Models;
using Qinile.Core.Data;

namespace Qinile.App.Services
{
    public class XCacheService : CacheService<XModel, string>, IXCacheService
    {
        public const string CACHE_KEY = "caches";

        public XCacheService() : base(CACHE_KEY)
        {
        }

        public IObservable<IEnumerable<XModel>> GetLatestItems()
        {
            return BlobCache.Secure.GetAndFetchLatest(cacheKey, GetListItemsAsync);
        }

        public async Task<IEnumerable<XModel>> GetListItemsAsync()
        {
            var meta = await GetObjects();
            return meta;
        }

        public IEnumerable<Group<string, XModel>> GroupItems(List<XModel> items)
        {
            return from item in items
                   orderby item.DateCreated
                   group item by item.Id into itemGroup
                   select new Group<string, XModel>(itemGroup.Key, itemGroup.OrderBy(e => e.Id));
        }

        public Task<IObservable<IEnumerable<XModel>>> RemoveItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<XModel> SearchItems(IEnumerable<XModel> items, string query)
        {
            query = string.IsNullOrEmpty(query) ? "" : query.ToLower();
            return items.Where(e =>
                e.Id.ToLower().Contains(query)).ToList();
        }
    }
}