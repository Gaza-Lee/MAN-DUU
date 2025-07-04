using MANDUU.RegexValidation;
using MANDUU.ViewModels.Base;
using Microsoft.Extensions.Options;
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
        
        private string _emailORPhone;
        private string _password;

        public ICommand ProceedCommand { get; }
        public ICommand ForgetPasswordCommand { get; }


        public SignInPageViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
            ForgetPasswordCommand = new Command(async () => await OnForgetPassword());
        }

        #region Properties

        public string EmailOrPhone
        {
            get => _emailORPhone;
            set
            {
                if(_emailORPhone != value)
                {
                    _emailORPhone = value;
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

            if (string.IsNullOrWhiteSpace(EmailOrPhone) || 
                string.IsNullOrWhiteSpace(Password))
            {
                ShowToast("Fields must not be empty");
                IsBusy = false;
                return;
            }

            //Chack if Email or Password is valid type
            bool isEmailLike = EmailOrPhone.Contains("@");
            bool isValidEmail = InputValidation.IsValidEmail(EmailOrPhone);
            bool isValidPhone = InputValidation.IsValidPhoneNumber(EmailOrPhone);

            if (isEmailLike && !isValidEmail)
            {
                ShowToast("Invalid email");
                IsBusy = false;
                return;
            }
            if (!isValidPhone)
            {
                ShowToast("Invalid Phone");
                IsBusy = false;
                return;
            }


            if (!InputValidation.IsValidPassword(Password))
            {
                ShowToast("Invalid Password");
                IsBusy = false;
                return;
            }


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
