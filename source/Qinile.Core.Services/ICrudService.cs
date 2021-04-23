namespace Qinile.Core.Services
{
    public interface ICrudService<T, K, V> : ICreateService<T, K>, IReadService<T>, IUpdateService<T, V>, IDeleteService<T> where T : class
    {

    }
}