using Qinile.App.Contracts;
using Qinile.App.Models;
using Qinile.Core.Data;
using Qinile.Core.Services;

namespace Qinile.App.Services
{
    public interface IXService : IDataService<XModel>, ICrudService<XModel, Create, Update>
    {

    }
}