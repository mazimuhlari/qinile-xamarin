namespace Qinile.Core.Services
{
    public interface IReadAndUpdateService<T, V> : IReadService<T>, IUpdateService<T, V> where T : class
    {

    }
}
