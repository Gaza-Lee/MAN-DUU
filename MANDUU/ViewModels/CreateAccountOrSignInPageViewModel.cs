using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class CreateAccountOrSignInPageViewModel : BaseViewModel
    {
        public ICommand SignInCommand {  get; set; }
        public ICommand CreateAccountCommand { get; set; }

        public CreateAccountOrSignInPageViewModel()
        {
            SignInCommand = new Command(async () => await OnSignIn());
            CreateAccountCommand = new Command(async() => await OnCreateAccount());
        }

        public async Task OnSignIn()
        {
            await Shell.Current.GoToAsync("SignInPage");
        }

        public async Task OnCreateAccount()
        {
            await Shell.Current.GoToAsync("CreateAccountPage");
        }

    }
}
