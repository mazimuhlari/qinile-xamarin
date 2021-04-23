using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Qinile.Core.Data
{
    public class Group<K, T> : ObservableCollection<T>
    {
        public K Key { get; }
        public Group(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}