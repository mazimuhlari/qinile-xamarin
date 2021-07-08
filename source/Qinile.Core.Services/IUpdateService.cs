using System.Threading.Tasks;
using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public interface IUpdateService<M, U, I> where M : BaseModel<I> where I : struct
    {
        Task<Meta<M>> UpdateItemAsync(I id, U item);
    }
}