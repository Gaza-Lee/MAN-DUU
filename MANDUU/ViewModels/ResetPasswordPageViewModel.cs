using MANDUU.RegexValidation;
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
        #region Variables
        private string _email;
        #endregion

        public ICommand ProceedCommand { get; set; }

        public ResetPasswordPageViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
        }

        #region Properties
        public  string Email
        {
            get => _email;
            set
            {
                if(_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Methods
        private async Task OnProceed()
        {
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(Email))
            {
                ShowToast("Field can't be empty");
                IsBusy = false;
                return;
            }

            if (!InputValidation.IsValidEmail(Email))
            {
                ShowToast("Invalid email");
                IsBusy = false;
                return;
            }

            await Shell.Current.GoToAsync("ResetPswVerificationPage");
            IsBusy = false;
        }
        #endregion
    }
}
