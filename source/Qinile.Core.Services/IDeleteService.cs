using System.Threading.Tasks;
using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public interface IDeleteService<M, I> where M : BaseModel<I> where I : struct
    {
        Task<Meta<M>> DeleteItemAsync(I id);
    }
}