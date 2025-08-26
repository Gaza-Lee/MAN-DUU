using MANDUU.RegexValidation;
using MANDUU.Services;
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
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;


        private string _emailORPhone;
        private string _password;

        public ICommand ProceedCommand { get; }
        public ICommand ForgetPasswordCommand { get; }


        public SignInPageViewModel(IUserService userService,INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = userService;
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

        private bool PhoneNumberOrEmailvalidation()
        {
            bool isValidEmail = InputValidation.IsValidEmail(EmailOrPhone);
            bool isValidPhone = InputValidation.IsValidPhoneNumber(EmailOrPhone);

            if (isValidEmail || isValidPhone)
                return true;

            if (EmailOrPhone.Contains("@"))
                ShowToast("Invalid Email Address");
            else if (EmailOrPhone.All(char.IsDigit) && EmailOrPhone.Length >= 8 && EmailOrPhone.Length <= 15)
            {
                ShowToast("Invalid Phone Number");
            }
            else
            {
                ShowToast("Enter a valid email or phone number");
            }

                return false;
        }

        private string ExtractEmailFromInput(string input)
        {
            // If input is already a valid email, return it
            if (InputValidation.IsValidEmail(input))
                return input;

            // If input is a phone number, try to find the user and get their email
            if (InputValidation.IsValidPhoneNumber(input))
            {
                return input; // Let UserService handle phone numbers
            }

            return input; // Fallback
        }

        private async Task OnProceed()
        {
            try
            {
                IsBusy = true;

                if (string.IsNullOrWhiteSpace(EmailOrPhone) ||
                    string.IsNullOrWhiteSpace(Password))
                {
                    ShowToast("Fields must not be empty");
                    IsBusy = false;
                    return;
                }

                if (!PhoneNumberOrEmailvalidation())
                {
                    IsBusy = false;
                    return;
                }

                if (!InputValidation.IsValidPassword(Password))
                {
                    ShowToast("Invalid Password");
                    IsBusy = false;
                    return;
                }

                var loginSuccessful = await _userService.LoginAsync(EmailOrPhone,Password);

                if (loginSuccessful)
                {
                    ShowToast("Welcome Back!");
                    await Task.Delay(2000);

                    await _navigationService.NavigateToAsync("//main/home");
                }
                else
                {
                    ShowToast("Invalid login credentials");
                }
            }
            catch (Exception ex)
            {
                ShowToast($"An error occurred: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private async Task OnForgetPassword()
        {
            try
            {
                IsBusy = true;
                await _navigationService.NavigateToAsync("//resetpasswordpage");
            }
            catch (Exception ex)
            {
                ShowToast($"Navigation failed: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
