using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qinile.Core.Services
{
    public interface IReadService<T> where T : class
    {
        Task<Meta<T>> GetItemAsync(string id);
        Task<Meta<IEnumerable<T>>> GetItemsAsync();
    }
}