using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Qinile.Core.Models;
using RestSharp;
using Xamarin.Essentials;

namespace Qinile.Core.Services
{
    public class UpdateService<M, U, I> : IUpdateService<M, U, I> where M : BaseModel<I>
    {
        private readonly RestClient client;

        public string baseUrl;
        public string resourceUrl;
        public string cacheKey;

        public UpdateService(string baseUrl, string resourceUrl, string cacheKey)
        {
            this.baseUrl = baseUrl;
            this.resourceUrl = resourceUrl;
            this.cacheKey = cacheKey;
            client = new RestClient(baseUrl);
        }

        public virtual async Task<Meta<M>> UpdateItemAsync(I id, U item)
        {
            var request = new RestRequest(resourceUrl + "/{id}", Method.PUT);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);
            request.AddUrlSegment("id", id);
            request.AddJsonBody(item);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<M>>(content);

            return meta;
        }
    }
}