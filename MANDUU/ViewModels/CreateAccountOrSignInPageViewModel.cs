using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MANDUU.Services;

namespace MANDUU.ViewModels
{
    public class CreateAccountOrSignInPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand SignInCommand {  get; set; }
        public ICommand CreateAccountCommand { get; set; }

        public CreateAccountOrSignInPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SignInCommand = new Command(async () => await OnSignIn());
            CreateAccountCommand = new Command(async() => await OnCreateAccount());
        }

        public async Task OnSignIn()
        {
            await _navigationService.NavigateToAsync("signinpage");
        }

        public async Task OnCreateAccount()
        {
            await _navigationService.NavigateToAsync("createaccountpage");
        }

    }
}
