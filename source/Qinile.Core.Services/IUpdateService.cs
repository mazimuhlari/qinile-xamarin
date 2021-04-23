using System.Threading.Tasks;

namespace Qinile.Core.Services
{
    public interface IUpdateService<T, V> where T : class
    {
        Task<Meta<T>> UpdateItemAsync(string id, V item);
    }
}