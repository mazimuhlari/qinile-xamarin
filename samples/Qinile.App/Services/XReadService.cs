using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Newtonsoft.Json;
using Qinile.App.Models;
using Qinile.Core;
using Qinile.Core.Data;
using Qinile.Core.Services;
using RestSharp;
using Xamarin.Essentials;

namespace Qinile.App.Services
{
    public class XReadService : ReadService<XModel, string>, IXReadService
    {
        private const string CACHE_KEY = "reads";
        private readonly RestClient client;

        public XReadService(string baseUrl, string resourceUrl) : base(baseUrl, resourceUrl, CACHE_KEY)
        {
            client = new RestClient(baseUrl);
        }

        public IObservable<IEnumerable<XModel>> GetLatestItems()
        {
            return BlobCache.Secure.GetAndFetchLatest(cacheKey, GetListItemsAsync);
        }

        public async Task<IEnumerable<XModel>> GetListItemsAsync()
        {
            var meta = await GetItemsAsync();
            return meta.Data;
        }

        public IEnumerable<XModel> SearchItems(IEnumerable<XModel> items, string query)
        {
            query = string.IsNullOrEmpty(query) ? "" : query.ToLower();
            return items.Where(e =>
                e.Id.ToLower().Contains(query)).ToList();
        }

        public IEnumerable<Group<string, XModel>> GroupItems(List<XModel> items)
        {
            return from item in items
                   orderby item.DateCreated
                   group item by item.Id into itemGroup
                   select new Group<string, XModel>(itemGroup.Key, itemGroup.OrderBy(e => e.Id));
        }

        public async Task<IObservable<IEnumerable<XModel>>> RemoveItemAsync(string id)
        {
            var items = await BlobCache.Secure.GetObject<IEnumerable<XModel>>(cacheKey)
                .Catch(Observable.Return(new List<XModel>()));

            var request = new RestRequest($"{Configuration.API_RESOURCE_URL}/{id}", Method.DELETE);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("x-auth-token", token);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var obj = JsonConvert.DeserializeObject<XModel>(content);

            return GetLatestItems();
        }
    }
}