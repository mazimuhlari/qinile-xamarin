using System.Threading.Tasks;
using System.Windows.Input;
using Qinile.Core.Data;
using Xamarin.Forms;

namespace Qinile.Core.ViewModels
{
    public class ViewPageViewModel<T> : BaseViewModel where T : class
    {
        #region properties
        public readonly IDataService<T> _service;
        #endregion

        #region Commands
        public ICommand OnDeleteMenuTappedCommand { get; private set; }
        #endregion

        public ViewPageViewModel(IDataService<T> service)
        {
            _service = service;
            OnDeleteMenuTappedCommand = new Command(async () => await OnDeleteMenuTapped());
        }

        public virtual async Task OnDeleteMenuTapped()
        {

        }
    }
}