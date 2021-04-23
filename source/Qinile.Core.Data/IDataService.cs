using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qinile.Core.Data
{
    public interface IDataService<T> where T : class
    {
        IObservable<IEnumerable<T>> GetLatestItems();
        IEnumerable<T> SearchItems(IEnumerable<T> items, string query);
        IEnumerable<Group<string, T>> GroupItems(List<T> items);
        Task<IObservable<IEnumerable<T>>> RemoveItemAsync(string id);
    }
}