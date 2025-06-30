using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class ResetPswVerificationViewModel : BaseViewModel
    {
        public ICommand ProceedCommand { get; set; }

        public ResetPswVerificationViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
        }

        private async Task OnProceed()
        {
            await Shell.Current.GoToAsync("NewPasswordPage");
        }
    }
}
