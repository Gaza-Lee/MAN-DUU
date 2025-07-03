using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels.Base
{
    public class BaseViewModel : IBaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void ShowToast(string message)
        {
            var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
        }

        public virtual Task InitializeAsync() => Task.CompletedTask;
    }
}
