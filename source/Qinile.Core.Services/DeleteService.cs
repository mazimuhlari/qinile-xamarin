using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Qinile.Core.Models;
using RestSharp;
using Xamarin.Essentials;

namespace Qinile.Core.Services
{
    public class DeleteService<M, I> : IDeleteService<M, I> where M : BaseModel<I>
    {
        private readonly RestClient client;

        public string baseUrl;
        public string resourceUrl;
        public string cacheKey;

        public DeleteService(string baseUrl, string resourceUrl, string cacheKey)
        {
            this.baseUrl = baseUrl;
            this.resourceUrl = resourceUrl;
            this.cacheKey = cacheKey;
            client = new RestClient(baseUrl);
        }

        public virtual async Task<Meta<M>> DeleteItemAsync(I id)
        {
            var request = new RestRequest(resourceUrl + "/{id}", Method.DELETE);
            var token = Preferences.Get(PreferenceKeys.TOKEN_KEY, null);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-auth-token", token);
            request.AddUrlSegment("id", id);

            IRestResponse response = await client.ExecuteAsync(request);
            var content = response.Content;
            var meta = JsonConvert.DeserializeObject<Meta<M>>(content);

            return meta;
        }
    }
}