using System.Threading.Tasks;
using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public interface ICreateService<M, C, I> where M : BaseModel<I> where I : struct
    {
        Task<Meta<M>> CreateItemAsync(C item);
    }
}