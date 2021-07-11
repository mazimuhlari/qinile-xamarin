using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Qinile.Core.ViewModels
{
    public class EditPageBaseViewModel<T> : BaseViewModel where T : class
    {
        #region Commands
        public ICommand OnSaveMenuTappedCommand { get; private set; }
        #endregion

        public EditPageBaseViewModel()
        {
            OnSaveMenuTappedCommand = new Command(async () => await OnSaveMenuTapped());
        }

        public virtual async Task OnSaveMenuTapped()
        {

        }
    }
}