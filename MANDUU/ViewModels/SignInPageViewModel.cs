using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class SignInPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        [ObservableProperty]
        private string _emailOrPhone;

        [ObservableProperty]
        private string _password;


        public SignInPageViewModel(IUserService userService, INavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
        }


        //On Proceed Button Click, validate user login using regex for valid email/phone and strong password
        [RelayCommand]
        private async Task ProceedAsync()
        {
            await IsBusyFor(async () =>
            {
                //Validation checks
                if (string.IsNullOrWhiteSpace(EmailOrPhone) ||
                    string.IsNullOrWhiteSpace(Password))
                {
                    ShowToast("Fields must not be empty");
                    return;
                }

                if (!PhoneNumberOrEmailvalidation())
                {
                    return;
                }

                //Login attempt
                var loginSuccessful = await _userService.LoginAsync(EmailOrPhone, Password);

                if (loginSuccessful)
                {
                    ShowToast("Welcome Back!");
                    await Task.Delay(1000);
                    await NavigationService.NavigateToAsync("//main/home");
                }
                else
                {
                    ShowToast("Invalid login credentials");
                }
            });
        }

        // Navigate to Forget Password Page
        [RelayCommand]
        private async Task ForgetPasswordAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    await NavigationService.NavigateToAsync("resetpasswordpage");
                }
                catch (Exception ex)
                {
                    ShowToast($"Navigation failed: {ex.Message}");
                }
            });
        }

        // Validate if input is a phone number or email, and check if they are valid formats
        private bool PhoneNumberOrEmailvalidation()
        {
            bool isValidEmail = InputValidation.IsValidEmail(EmailOrPhone);
            bool isValidPhone = InputValidation.IsValidPhoneNumber(EmailOrPhone);

            if (isValidEmail || isValidPhone)
                return true;

            if (EmailOrPhone.Contains("@"))
                ShowToast("Invalid Email Address");
            else if (EmailOrPhone.All(char.IsDigit) &&
                EmailOrPhone.Length >= 8 &&
                EmailOrPhone.Length <= 15)
            {
                ShowToast("Invalid Phone Number");
            }
            else
            {
                ShowToast("Enter a valid email or phone number");
            }

            return false;
        }
    }
}
