using System.Threading.Tasks;

namespace Qinile.Core.Services
{
    public interface ICreateService<T, K> where T : class
    {
        Task<Meta<T>> CreateItemAsync(K item);
    }
}