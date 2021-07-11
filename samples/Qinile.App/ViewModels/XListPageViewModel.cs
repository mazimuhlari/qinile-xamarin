using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Qinile.App.Models;
using Qinile.App.Services;
using Qinile.Core.Data;
using Qinile.Core.Services;
using Qinile.Core.ViewModels;

namespace Qinile.App.ViewModels
{
    public class XListPageViewModel : ListPageBaseViewModel<XModel>, IDataService<XModel>
    {
        private readonly IXReadService _service;

        public XListPageViewModel() : base()
        {
            _service = new XReadService(Configuration.API_BASE_URL, Configuration.API_RESOURCE_URL);
        }

        public IObservable<IEnumerable<XModel>> GetLatestItems()
        {
            return _service.GetLatestItems();
        }

        public IEnumerable<Group<string, XModel>> GroupItems(List<XModel> items)
        {
            return _service.GroupItems(items);
        }

        public Task<IObservable<IEnumerable<XModel>>> RemoveItemAsync(string id)
        {
            return _service.RemoveItemAsync(id);
        }

        public IEnumerable<XModel> SearchItems(IEnumerable<XModel> items, string query)
        {
            return _service.SearchItems(items, query);
        }
    }
}