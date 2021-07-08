using System;
using Qinile.App.Models;
using Qinile.Core.Services;

namespace Qinile.App.Services
{
    public class XCacheService : CacheService<XModel, Guid>, IXCacheService
    {
        public const string CACHE_KEY = "items";

        public XCacheService() : base(CACHE_KEY)
        {
        }
    }
}
