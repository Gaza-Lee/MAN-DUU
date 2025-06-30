using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class ResetPasswordPageViewModel: BaseViewModel
    {
        public ICommand ProceedCommand { get; set; }

        public ResetPasswordPageViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
        }

        #region Methods
        private async Task OnProceed()
        {
            IsBusy = false;
            await Shell.Current.GoToAsync("ResetPswVerificationPage");
        }
        #endregion
    }
}
