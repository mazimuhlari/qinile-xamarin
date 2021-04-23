using System;
using System.ComponentModel;

namespace Qinile.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties
        public string PageTitle { get; set; }
        public bool IsConnected { get; set; }
        public bool IsBusy { get; set; }
        public bool IsRefreshing { get; set; }
        public DateTime LastRefreshed { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}