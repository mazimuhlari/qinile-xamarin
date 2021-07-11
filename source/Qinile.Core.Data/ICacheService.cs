using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qinile.Core.Data
{
    public interface ICacheService<M, I> where M : class
    {
        Task<bool> Exists();
        Task<M> GetObject();
        Task<List<M>> GetObjects();
        Task<M> GetObjectById(I id);
        Task<System.Reactive.Unit> SaveObject(M obj);
        Task<System.Reactive.Unit> SaveObjects(List<M> objs);
        Task<int> UpdateObject(M obj);
        Task<int> UpdateObjects(List<M> objs);
        Task<int> DeleteObject(I id);
        Task<int> DeleteObjects(List<I> ids);
    }
}