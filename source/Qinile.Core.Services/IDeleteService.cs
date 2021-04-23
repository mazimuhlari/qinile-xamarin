using System.Threading.Tasks;

namespace Qinile.Core.Services
{
    public interface IDeleteService<T> where T : class
    {
        Task<Meta<T>> DeleteItemAsync(string id);
    }
}