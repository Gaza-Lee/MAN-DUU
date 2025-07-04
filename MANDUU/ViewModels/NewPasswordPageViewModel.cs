using MANDUU.RegexValidation;
using MANDUU.ViewModels.Base;
using Microsoft.Extensions.Configuration;
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
        private string _newPassword;
        private string _confirmNewPassword;
        #endregion

        #region Commands
        public ICommand ProceedCommand { get; }
        #endregion

        public NewPasswordPageViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
        }

        #region Properties 
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set
            {
                if (_confirmNewPassword != value)
                {
                    _confirmNewPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion


        #region Methods
        public bool IsPasswordMatch()
        {
            return NewPassword == ConfirmNewPassword;
        }
        private async Task OnProceed()
        {
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(NewPassword)|| string.IsNullOrWhiteSpace(ConfirmNewPassword))
            {
                ShowToast("All fields are required");
                IsBusy = false;
                return;
            }

            if (!InputValidation.IsValidPassword(NewPassword))
            {
                ShowToast("Weak password");
                IsBusy = false;
                return;
            }

            if (!IsPasswordMatch())
            {
                ShowToast("Password do not match");
                IsBusy = false;
                return;
            }

            ShowToast("Password Changed successfully");

            await Task.Delay(1000);
            await Shell.Current.GoToAsync("HomePage");
            IsBusy = false;
        }
        #endregion

    }
}
