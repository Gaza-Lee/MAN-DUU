using CommunityToolkit.Maui.Alerts;
using MANDUU.RegexValidation;
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
    public class CreateAccountPageViewModel : BaseViewModel
    {
        #region Variables
        private string _firstName { get; set; }
        private string _lastName { get; set; }
        private string _phoneNumber { get; set; }
        private string _email { get; set; }
        private string _password { get; set; }
        private string _confirmPassword { get; set; }

        #endregion
        

        public ICommand ProceedCommand { get; set; }

        public CreateAccountPageViewModel()
        {
            ProceedCommand = new Command(async() => await OnProceed());
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
                if(_phoneNumber != value)
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
                if(_email != value)
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
                if(_password != value)
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
                if(_confirmPassword != value)
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

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(_confirmPassword))
            {
                ShowToast("All fields are required");
                IsBusy = false;
                return;
            }

            
            if (!InputValidation.IsValidName(FirstName))
            {
                ShowToast("Invalid first name");
                IsBusy = false;
                return;
            }
            if (!InputValidation.IsValidName(LastName))
            {
                ShowToast("Invalid last name");
                IsBusy = false;
                return;
            }
            if (!InputValidation.IsValidPhoneNumber(PhoneNumber))
            {
                ShowToast("Invalid phone number");
                IsBusy = false;
                return;
            }
            if (!InputValidation.IsValidEmail(Email))
            {
                ShowToast("Invalid email address");
                IsBusy = false;
                return;
            }
            if (!InputValidation.IsValidPassword(Password))
            {
                ShowToast("weak password");
                IsBusy = false;
                return;
            }

            if (!IsPasswordMatch())
            {
                ShowToast("Passwords do not match");
                IsBusy = false;
                return;
            }

            await Shell.Current.GoToAsync("VerificationPage");

            IsBusy = false;
        }

        #endregion
    }
}
