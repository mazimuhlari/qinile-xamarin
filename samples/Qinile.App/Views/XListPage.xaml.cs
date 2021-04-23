using System;
using Qinile.App.Services;
using Qinile.App.ViewModels;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Qinile.App.Views
{
    public partial class XListPage : ContentPage
    {
        public XListPage()
        {
            try
            {
                ViewModel = new XListPageViewModel(new XService(Configuration.API_BASE_URL, Configuration.API_RESOURCE_URL));
                InitializeComponent();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }

        protected override async void OnAppearing()
        {
            await ViewModel.Initialize();
        }

        public XListPageViewModel ViewModel
        {
            get { return BindingContext as XListPageViewModel; }
            set { BindingContext = value; }
        }
    }
}