using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qinile.Core.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicCollectionViewCardViewCell : ViewCell
    {
        public BasicCollectionViewCardViewCell()
        {
            InitializeComponent();
        }
    }
}
