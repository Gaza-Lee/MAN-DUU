using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public partial class CreateAccountPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        #region observable properties

        [ObservableProperty]
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _confirmPassword;

        #endregion

        public CreateAccountPageViewModel(IUserService userService, INavigationService navigationService): base(navigationService)
        {
            _userService = userService;
        }


        #region Methods

        private bool IsPasswordMatch()
        {
            return Password == ConfirmPassword;
        }


        [RelayCommand]
        private async Task ProceedAsync()
        {
            await IsBusyFor(async () =>
            {
                // validation checks
                if (string.IsNullOrWhiteSpace(FirstName) ||
                    string.IsNullOrWhiteSpace(LastName) ||
                    string.IsNullOrWhiteSpace(PhoneNumber) ||
                    string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Password) ||
                    string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    ShowToast("Fill in all Fields");
                    return;
                }

                if (!InputValidation.IsValidName(FirstName))
                {
                    ShowToast("Invalid First Name");
                    return;
                }

                if (!InputValidation.IsValidName(LastName))
                {
                    ShowToast("Invalid Last Name");
                    return;
                }

                if (!InputValidation.IsValidPhoneNumber(PhoneNumber))
                {
                    ShowToast("Invalid Phone Number");
                    return;
                }

                if (!InputValidation.IsValidEmail(Email))
                {
                    ShowToast("Invalid Email");
                    return;
                }

                if (!InputValidation.IsValidPassword(Password))
                {
                    ShowToast("Weak Password");
                    return;
                }
                if (!IsPasswordMatch())
                {
                    ShowToast("Passwords do not match");
                    return;
                }

                try
                {
                    var registrationSuccess = await _userService.RegisterAsync(
                        FirstName,
                        LastName,
                        PhoneNumber,
                        Email,
                        Password
                    );
                    if (registrationSuccess)
                    {
                        //Should Navigate user to Email Verification Page

                       /* await NavigationService.NavigateToAsync("verificationpage", new Dictionary<string, object>
                        {
                            { "email", Email }
                        }); */


                        ShowToast("Account created successfully");

                        //Application is not currently using email verification, so we will navigate user to home page

                        await NavigationService.NavigateToAsync("//main/home");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Failed", "User this account deatails already exist", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loggin in {ex.Message}");
                }
            });
        }

    }

    #endregion
}
