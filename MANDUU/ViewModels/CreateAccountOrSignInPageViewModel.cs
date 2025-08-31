using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MANDUU.Services;
using CommunityToolkit.Mvvm.Input;

namespace MANDUU.ViewModels
{
    public partial class CreateAccountOrSignInPageViewModel : BaseViewModel
    {      
        public CreateAccountOrSignInPageViewModel(INavigationService navigationService): base(navigationService)
        {
        }

        [RelayCommand]
        private async Task SignInAsync()
        {
            await NavigationService.NavigateToAsync("signinpage");
        }


        [RelayCommand]
        private async Task OnCreateAccount()
        {
            await NavigationService.NavigateToAsync("createaccountpage");
        }

    }
}
