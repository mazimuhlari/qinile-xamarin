using System.Collections.Generic;
using System.Threading.Tasks;
using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public interface IReadService<M, I> where M : BaseModel<I> where I : struct
    {
        Task<Meta<M>> GetItemAsync(I id);
        Task<Meta<IEnumerable<M>>> GetItemsAsync();
    }
}