using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Qinile.Core.ViewModels
{
    public class ViewPageViewModel<T> : BaseViewModel where T : class
    {
        #region Commands
        public ICommand OnDeleteMenuTappedCommand { get; private set; }
        #endregion

        public ViewPageViewModel()
        {
            OnDeleteMenuTappedCommand = new Command(async () => await OnDeleteMenuTapped());
        }

        public virtual async Task OnDeleteMenuTapped()
        {

        }
    }
}