using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class UserProfileViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        [ObservableProperty] private string _fullName;
        [ObservableProperty] private string _profileImage;

        public UserProfileViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
        }

        public async Task InitializeAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user != null)
            {
                FullName = $"{user.FirstName} {user.LastName}";
                ProfileImage = user.ProfilePicture;
            }
            else
            {
                FullName = "Guest";
                ProfileImage = "fashion.png";
            }
        }

        [RelayCommand]
        private async Task GoToFAQAsync()
        {
            await _navigationService.NavigateToAsync("faqpage");
        }

        [RelayCommand]
        private async Task GoToSettingsAsync()
        {
            await _navigationService.NavigateToAsync("settingspage");
        }

        [RelayCommand]
        private async Task GoToFavoritesAsync()
        {
            await _navigationService.NavigateToAsync("favoritespage");
        }

        [RelayCommand]
        private async Task GoToGetVerifiedAsync()
        {
            await _navigationService.NavigateToAsync("getverifiedpage");
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            bool confirmLogout = await Application.Current.MainPage.DisplayAlert(
                "Confirm Logout",
                "Are you sure you want to log out?",
                "Yes",
                "No");

            if (confirmLogout)
            {
                var success = await _userService.LogoutAsync();
                if (success)
                {
                    await _navigationService.NavigateToAsync("//createaccountorsigninpage");

                    //Success message
                    await Application.Current.MainPage.DisplayAlert(
                        "Logged Out",
                        "You have been successfully logged out.",
                        "OK");
                }
            }
        }
    }
}
