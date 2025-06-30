using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class SignInPageViewModel : BaseViewModel
    {
        private string _username;
        private string _email;
        private string _phoneNumber;
        private string _password;

        public ICommand ProceedCommand { get; }
        public ICommand ForgetPasswordCommand { get; }


        public SignInPageViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
            ForgetPasswordCommand = new Command(async () => await OnForgetPassword());
        }

        #region Properties
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
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

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods
        private async Task OnProceed()
        {
            IsBusy = true;
            await Task.Delay(2000);
            await Shell.Current.GoToAsync("VerificationPage");
            IsBusy = false;
        }

        private async Task OnForgetPassword()
        {
            IsBusy = false;
            await Shell.Current.GoToAsync("ResetPasswordPage");
        }
        #endregion
    }
}
