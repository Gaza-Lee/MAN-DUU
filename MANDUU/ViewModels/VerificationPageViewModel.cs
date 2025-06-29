using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class VerificationPageViewModel : BaseViewModel
    {
        private string VerificationCode { get; set; }
        
        public ICommand ProceedCommand { get; set; }


        public VerificationPageViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
        }

        #region Methods
        private async Task OnProceed()
        {
            IsBusy = true;
            await Task.Delay(1000);
            await Shell.Current.GoToAsync("HomePage");
            IsBusy = false;
        }
        #endregion
    }
}
