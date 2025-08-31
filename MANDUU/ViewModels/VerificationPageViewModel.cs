using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public partial class VerificationPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _verificationCode;
        


        public VerificationPageViewModel(INavigationService navigationService):base (navigationService)
        {
        }

        [RelayCommand]
        private async Task ProceedAsync()
        {
           await IsBusyFor (async () =>
           {
               // For now just navigate to home page
               await NavigationService.NavigateToAsync("//main/home");
           });
        }
    }
}
