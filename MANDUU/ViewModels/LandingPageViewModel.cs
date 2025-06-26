using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class LandingPageViewModel : BaseViewModel
    {
        public ICommand GetStartedCommand { get; }

        public LandingPageViewModel()
        {
            GetStartedCommand = new Command (async () => await OnGetStarted());
        }

        private async Task OnGetStarted()
        {
            IsBusy = true;
            await Task.Delay(2000);
            await Shell.Current.GoToAsync("CreateAccountOrSignInPage");
            IsBusy = false;
        }
    }
}
