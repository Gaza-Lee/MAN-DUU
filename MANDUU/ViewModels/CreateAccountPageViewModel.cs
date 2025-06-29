using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class CreateAccountPageViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        

        public ICommand ProceedCommand { get; set; }

        public CreateAccountPageViewModel()
        {
            ProceedCommand = new Command(async() => await OnProceed());
        }

        #region Properties
        #endregion

        #region Methods
        private async Task OnProceed()
        {
            IsBusy = true;

            await Task.Delay(1000);
            await Shell.Current.GoToAsync("VerificationPage");

            IsBusy = false;
        }

        #endregion
    }
}
