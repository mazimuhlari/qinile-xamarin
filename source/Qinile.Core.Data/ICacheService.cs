using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qinile.Core.Data
{
    public interface ICacheService<T, M> where T : class
    {
        Task<IEnumerable<T>> GetItemsAsync();
        Task<IEnumerable<T>> SearchItemsAsync(IEnumerable<T> items, string query);
        Task<IEnumerable<Group<string, T>>> GroupItemsAsync(List<T> items);
        Task<IEnumerable<T>> RemoveItemAsync(M id);
    }
}