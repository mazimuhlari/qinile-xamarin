using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public interface IReadAndUpdateService<M, U, I> : IReadService<M, I>, IUpdateService<M, U, I> where M : BaseModel<I>
    {

    }
}
