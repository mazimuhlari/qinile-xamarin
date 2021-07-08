using Qinile.Core.Models;

namespace Qinile.Core.Services
{
    public interface ICrudService<M, C, U, I> : ICreateService<M, C, I>, IReadService<M, I>, IUpdateService<M, U, I>, IDeleteService<M, I> where M : BaseModel<I>
    {

    }
}