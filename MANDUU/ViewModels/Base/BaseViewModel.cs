using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels.Base
{
    public abstract partial class BaseViewModel : ObservableObject, IBaseViewModel
    {
        private long _isBusy;

        [ObservableProperty] 
        private bool _isInitialized;

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            InitializeAsyncCommand =
                new AsyncRelayCommand(
                    async () =>
                    {
                        await IsBusyFor(InitializeAsync);
                        IsInitialized = true;
                    },
                    AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
        }

        public bool IsBusy => Interlocked.Read(ref _isBusy) > 0;

        public INavigationService NavigationService { get; }

        public IAsyncRelayCommand InitializeAsyncCommand { get; }

        public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
        {
        }

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected async Task IsBusyFor(Func<Task> unitOfWork)
        {
            Interlocked.Increment(ref _isBusy);
            OnPropertyChanged(nameof(IsBusy));

            try
            {
                await unitOfWork();
            }
            finally
            {
                Interlocked.Decrement(ref _isBusy);
                OnPropertyChanged(nameof(IsBusy));
            }
        }


        public async void ShowToast(string message, CommunityToolkit.Maui.Core.ToastDuration duration = CommunityToolkit.Maui.Core.ToastDuration.Short)
        {
            try
            {
                await Toast.Make(message, duration).Show();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error showing toast: {ex.Message}");
            }
        }
    }
}
