using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Essentials;

namespace Qinile.Core.Services
{
    public class CreateService<T, K> : ICreateService<T, K> where T : class
    {
        private readonly RestClient client;

        public string baseUrl;
        public string resourceUrl;
        public string cacheKey;

        public CreateService(string baseUrl, string resourceUrl, string cacheKey)
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
    }
}