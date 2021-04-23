using System.Threading.Tasks;
using System.Windows.Input;
using Qinile.Core.Data;
using Xamarin.Forms;

namespace Qinile.Core.ViewModels
{
    public class CreatePageBaseViewModel<T> : BaseViewModel where T : class
    {
        #region properties
        public readonly IDataService<T> _service;
        #endregion

        #region Commands
        public ICommand OnSaveMenuTappedCommand { get; private set; }
        #endregion

        public CreatePageBaseViewModel(IDataService<T> service)
        {
            _service = service;
            OnSaveMenuTappedCommand = new Command(async () => await OnSaveMenuTapped());
        }

        public virtual async Task OnSaveMenuTapped()
        {

        }
    }
}