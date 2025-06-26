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
        public ICommand SignIn {  get; set; }
        public ICommand CreateAccount { get; set; }

        public CreateAccountOrSignInPageViewModel()
        {

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
