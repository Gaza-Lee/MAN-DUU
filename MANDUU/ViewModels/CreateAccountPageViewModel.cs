using CommunityToolkit.Maui.Alerts;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
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
        private readonly INavigationService _navigationService;
        #region Variables
        private string _firstName { get; set; }
        private string _lastName { get; set; }
        private string _phoneNumber { get; set; }
        private string _email { get; set; }
        private string _password { get; set; }
        private string _confirmPassword { get; set; }

        #endregion


        public ICommand ProceedCommand { get; set; }

        public CreateAccountPageViewModel(IUserService userService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = userService;
            ProceedCommand = new Command(async () => await OnProceed());
        }

        #region Properties

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    if (!InputValidation.IsValidName(_firstName))
                    {
                        ShowToast("Invalid first name");
                    }
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    if (!InputValidation.IsValidName(_lastName))
                    {
                        ShowToast("Invalid last name");
                    }
                    OnPropertyChanged(nameof(LastName));
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
                    if (!InputValidation.IsValidPhoneNumber(_phoneNumber))
                    {
                        ShowToast("Invalid phone number");
                    }
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    if (!InputValidation.IsValidEmail(_email))
                    {
                        ShowToast("Invalid email address");
                    }
                    OnPropertyChanged(nameof(Email));
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
                    if (!InputValidation.IsValidPassword(_password))
                    {
                        ShowToast("Invalid password");
                    }
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }
        #endregion

        #region Methods

        private bool IsPasswordMatch()
        {
            return Password == ConfirmPassword;
        }

        private async Task OnProceed()
        {
            IsBusy = true;

            try
            {
                //Validation checks
                if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                    string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    ShowToast("Please fill in all fields.");
                    return;
                }

                if (!InputValidation.IsValidName(FirstName))
                {
                    ShowToast("Invalid first name");
                    return;
                }
                if (!InputValidation.IsValidName(LastName))
                {
                    ShowToast("Invalid last name");
                    return;
                }
                if (!InputValidation.IsValidPhoneNumber(PhoneNumber))
                {
                    ShowToast("Invalid phone number");
                    return;
                }
                if (!InputValidation.IsValidEmail(Email))
                {
                    ShowToast("Invalid email address");
                    return;
                }
                if (!InputValidation.IsValidPassword(Password))
                {
                    ShowToast("Weak password");
                    return;
                }
                if (!IsPasswordMatch())
                {
                    ShowToast("Passwords do not match");
                    return;
                }

                // Register the user
                var registrationSuccess = await _userService.RegisterAsync(
                    FirstName,
                    LastName,
                    Email,
                    PhoneNumber,
                    Password
                );

                if (registrationSuccess)
                {
                    ShowToast("Account created successfully!");

                    /*Navigate to Verification Page to verify email or phone number
                    but due to not using real backend services, we will navigate to HomePage directly*/

                    //await _navigationService.NavigateToAsync("verificationpage");

                    await _navigationService.NavigateToAsync("//main/home");
                }
                else
                {
                    ShowToast("Registration failed. Email or phone number may already be in use.");
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

    }

    #endregion
}
