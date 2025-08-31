using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels.Base
{
    public interface IBaseViewModel 
    {
        public INavigationService NavigationService { get; }

        public IAsyncRelayCommand InitializeAsyncCommand { get; }

        public bool IsBusy { get; }

        public bool IsInitialized { get; }

        Task InitializeAsync();

    }
}
