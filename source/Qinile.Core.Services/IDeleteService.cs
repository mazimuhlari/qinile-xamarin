using System.Threading.Tasks;
using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public interface IDeleteService<M, I> where M : BaseModel<I>
    {
        Task<Meta<M>> DeleteItemAsync(I id);
    }
}