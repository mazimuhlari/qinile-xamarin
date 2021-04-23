using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Qinile.Core.Data;
using Qinile.Core.Extensions;
using MvvmHelpers;
using Xamarin.Forms;

namespace Qinile.Core.ViewModels
{
    public class ListPageBaseViewModel<T> : BaseViewModel where T : class
    {
        #region fields
        private IEnumerable<T> _latestItems;
        #endregion

        #region properties
        public readonly IDataService<T> _service;
        public IEnumerable<T> InitialItems { get; set; }
        public ObservableRangeCollection<Group<string, T>> GroupedItems { get; set; }
        public ObservableRangeCollection<T> Items { get; set; }
        public string Query { get; set; }
        #endregion

        #region Commands
        public ICommand RefreshCommand { get; private set; }
        public ICommand OnItemTappedCommand { get; private set; }
        public ICommand OnItemDeleteTappedCommand { get; private set; }
        public ICommand OnCreateMenuTappedCommand { get; private set; }
        #endregion

        public ListPageBaseViewModel(IDataService<T> service)
        {
            _latestItems = new List<T>();
            _service = service;

            Initialize();

            RefreshCommand = new Command(async () => await OnRefreshCommand());
            OnItemTappedCommand = new Command<T>(async (obj) => await OnItemTapped(obj));
            OnItemDeleteTappedCommand = new Command<string>(async (id) => await OnItemDeleteTapped(id));
            OnCreateMenuTappedCommand = new Command(async () => await OnCreateMenuTapped());

            (this).ToObservable(x => x.Query).Subscribe(x =>
            {
                try
                {
                    IsBusy = true;
                    if (x != null)
                    {
                        HandleSearch(x);
                    }
                    else
                    {
                        HandleSearch(string.Empty);
                    }
                }
                finally
                {
                    IsBusy = false;
                }

            });

        }

        public virtual async Task OnRefreshCommand()
        {
            await Initialize();
        }

        public virtual async Task OnItemTapped(T item)
        {

        }

        public virtual async Task OnItemDeleteTapped(string id)
        {
            IsBusy = true;
            var observable = await _service.RemoveItemAsync(id);
            observable.Subscribe(items =>
            {
                _latestItems = items;
                UpdateItems(items);
                IsBusy = false;
                InitialItems = items;
            });
            await Task.FromResult(IsBusy);
            IsBusy = false;
        }

        public virtual async Task OnCreateMenuTapped()
        {

        }

        public virtual async Task Initialize()
        {
            IsBusy = true;
            var observable = _service.GetLatestItems();
            observable.Subscribe(items =>
            {
                _latestItems = items;
                UpdateItems(items);
                IsBusy = false;
                InitialItems = items;
            });
            await Task.FromResult(IsBusy);
            IsBusy = false;
        }

        public virtual void HandleSearch(string x)
        {
            var list = _service.SearchItems(_latestItems, x);
            UpdateItems(list);
        }

        public void UpdateItems(IEnumerable<T> items)
        {
            if (items == null)
                items = new List<T>();

            Items = new ObservableRangeCollection<T>(items);

            if (_service != null)
                GroupedItems = new ObservableRangeCollection<Group<string, T>>(_service.GroupItems(items.ToList()));
        }
    }
}