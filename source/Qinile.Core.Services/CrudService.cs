using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Essentials;

namespace Qinile.Core.Services
{
    public class CrudService<T, K, V> : ICrudService<T, K, V> where T : class
    {
        private readonly RestClient client;

        public string baseUrl;
        public string resourceUrl;
        public string cacheKey;

        public CrudService(string baseUrl, string resourceUrl, string cacheKey)
        {
            this.baseUrl = baseUrl;
            this.resourceUrl = resourceUrl;
            this.cacheKey = cacheKey;
            client = new RestClient(baseUrl);
        }

        public virtual async Task<Meta<T>> CreateItemAsync(K item)
        {
            var request = new RestRequest(resourceUrl, Method.POST);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);
            request.AddJsonBody(item);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<T>>(content);

            return meta;
        }

        public virtual async Task<Meta<T>> DeleteItemAsync(string id)
        {
            var request = new RestRequest(resourceUrl + "/{id}", Method.DELETE);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);
            request.AddUrlSegment("id", id);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<T>>(content);

            return meta;
        }

        public virtual async Task<Meta<T>> GetItemAsync(string id)
        {
            var request = new RestRequest(resourceUrl + "/{id}", Method.GET);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);
            request.AddUrlSegment("id", id);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<T>>(content);

            return meta;
        }

        public virtual async Task<Meta<IEnumerable<T>>> GetItemsAsync()
        {
            var items = await BlobCache.Secure.GetObject<IEnumerable<T>>(cacheKey)
                .Catch(Observable.Return(new List<T>()));

            var request = new RestRequest(resourceUrl, Method.GET);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<IEnumerable<T>>>(content);

            return meta;
        }

        public virtual async Task<Meta<T>> UpdateItemAsync(string id, V item)
        {
            var request = new RestRequest(resourceUrl + "/{id}", Method.PUT);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);
            request.AddUrlSegment("id", id);
            request.AddJsonBody(item);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<T>>(content);

            return meta;
        }
    }
}
