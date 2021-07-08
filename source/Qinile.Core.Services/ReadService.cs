using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Newtonsoft.Json;
using Qinile.Core.Models;
using RestSharp;
using Xamarin.Essentials;

namespace Qinile.Core.Services
{
    public class ReadService<M, I> : IReadService<M, I> where M : BaseModel<I> where I : struct
    {
        private readonly RestClient client;

        public string baseUrl;
        public string resourceUrl;
        public string cacheKey;

        public ReadService(string baseUrl, string resourceUrl, string cacheKey)
        {
            this.baseUrl = baseUrl;
            this.resourceUrl = resourceUrl;
            this.cacheKey = cacheKey;
            client = new RestClient(baseUrl);
        }

        public virtual async Task<Meta<M>> GetItemAsync(I id)
        {
            var request = new RestRequest(resourceUrl + "/{id}", Method.GET);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);
            request.AddUrlSegment("id", id);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<M>>(content);

            return meta;
        }

        public virtual async Task<Meta<IEnumerable<M>>> GetItemsAsync()
        {
            var items = await BlobCache.Secure.GetObject<IEnumerable<M>>(cacheKey)
                .Catch(Observable.Return(new List<M>()));

            var request = new RestRequest(resourceUrl, Method.GET);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<IEnumerable<M>>>(content);

            return meta;
        }
    }
}