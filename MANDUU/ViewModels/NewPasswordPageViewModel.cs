using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class NewPasswordPageViewModel : BaseViewModel
    {
        #region Variables
        private string _newPassword { get; set; }
        private string _confirmNewPassword { get; set; }
        #endregion

        public NewPasswordPageViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
        }


        #region Commands
        public ICommand ProceedCommand { get; }
        #endregion

        #region Methods
        private async Task OnProceed()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("HomePage");
            IsBusy = false;
        }
        #endregion

    }
}
