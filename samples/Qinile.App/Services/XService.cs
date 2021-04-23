using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Qinile.App.Contracts;
using Qinile.App.Models;
using Qinile.Core;
using Qinile.Core.Data;
using Qinile.Core.Services;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Essentials;

namespace Qinile.App.Services
{
    public class XService : CrudService<XModel, Create, Update>, IXService
    {
        private readonly RestClient client;
        private const string CACHE_KEY = "items";

        public XService(string baseUrl, string resourceUrl) : base(baseUrl, resourceUrl, CACHE_KEY)
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
            return null;
        }

        public IEnumerable<Group<string, XModel>> GroupItems(List<XModel> items)
        {
            return null;
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