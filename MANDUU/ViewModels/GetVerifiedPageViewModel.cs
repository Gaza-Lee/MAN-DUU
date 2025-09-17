using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class GetVerifiedPageViewModel: BaseViewModel
    {
        private readonly IUserService _userService;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StepText))]
        [NotifyPropertyChangedFor(nameof(PercentText))]
        [NotifyPropertyChangedFor(nameof(ProgressValue))]
        [NotifyPropertyChangedFor(nameof(NextButtonText))]
        [NotifyPropertyChangedFor(nameof(CanShowPreviousButton))]
        private int _selectedTabIndex = 0;

        [ObservableProperty] private string _firstName;

        [ObservableProperty] private string _lastName;

        [ObservableProperty] private DateTime _dateOfBirth = DateTime.Today;

        [ObservableProperty] private string _email;

        [ObservableProperty] private string _phoneNumber;

        [ObservableProperty] private string _residentialAddress;

        


        public string StepText => $"Step {SelectedTabIndex + 1} of 4";
        public string PercentText => $"{(SelectedTabIndex + 1) * 25}%";
        public double ProgressValue => (SelectedTabIndex + 1) * 0.25;
        public string NextButtonText => SelectedTabIndex == 3 ? "Complete" : "Next";
        public bool CanShowPreviousButton => SelectedTabIndex > 0;

        public GetVerifiedPageViewModel(INavigationService navigationService, 
            IUserService userService): base(navigationService)
        { 
            _userService = userService;
        }


        [RelayCommand]
        private void Previous()
        {
            if (SelectedTabIndex > 0)
            {
                SelectedTabIndex--;
            }
        }


        [RelayCommand]
        private async Task NextAsync()
        {
            await IsBusyFor(async() => {
                bool isValid = false;

                switch (SelectedTabIndex)
                {
                    case 0:
                        if (!IsPersonalInfoFilled())
                        {
                            ShowToast("Please fill in all fields.");
                            return;
                        }
                        isValid = await isValidPerosnalInfo();
                        break;

                    default:
                        isValid = true;
                        break;
                }

                if (!isValid)
                {
                    return;
                }

                if (SelectedTabIndex < 3)
                {
                    SelectedTabIndex++;
                }
            });
        }

        #region PersonalInfo Validation
        private  bool IsPersonalInfoFilled()
        {
           return !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LastName) &&
                !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(PhoneNumber) &&
                DateOfBirth < DateTime.Today;
        }

        private async Task<bool> isValidPerosnalInfo()
        {
            if (!InputValidation.IsValidName(FirstName))
            {
                ShowToast("Invalid First Name");
                return false;
            };
            if (!InputValidation.IsValidName(LastName))
            {
                ShowToast("Invalid Last Name");
                return false;
            }
            if (!InputValidation.IsValidEmail(Email))
            {
                ShowToast("Invalid Email Address");
                return false;
            }
            if (!InputValidation.IsValidPhoneNumber(PhoneNumber))
            {
                ShowToast("Invalid Phone Number");
                return false;
            }

            var age = DateTime.Today.Year - DateOfBirth.Year;
            if (age < 15)
            {
                ShowToast("You must be at least 15 years old.");
                return false;
            }
            return true;
        }

        #endregion

        #region Inintialization

        private async Task GetCurrentUserDetailsAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            if(user != null)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                Email = user.Email;
                PhoneNumber = user.PhoneNumber;
            }
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            await GetCurrentUserDetailsAsync();
        }

        #endregion
    }
}