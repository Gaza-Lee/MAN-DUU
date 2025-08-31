using CommunityToolkit.Mvvm.Input;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public partial class LandingPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public LandingPageViewModel(IUserService userService, INavigationService navigationService): base(navigationService)
        {
            _userService = userService;
        }


        // Skip User to Homme Page if User already logged, still skip user to home page if the user just wants to explore the app.
        // User will be identified as guest user.
        [RelayCommand]
        private async Task SkipAsync()
        {
            var isAuthenticated = await _userService.IsUserAuthenticatedAsync();
            if ( isAuthenticated)
            {
                await NavigationService.NavigateToAsync("//main/home");
            }
            else
            {
                await NavigationService.NavigateToAsync("//main//home");
            }
        }

        //Natigate user to CreateAccountOrSignInPage

        [RelayCommand]
        private async Task GetStartedAsync()
        {
            await NavigationService.NavigateToAsync("//createaccountorsigninpage");
        }



        // Auto-redirect to main app if user already logged in
        public override async Task InitializeAsync()
        {
            var isAuthenticated = await _userService.IsUserAuthenticatedAsync();

            if (isAuthenticated)
            {
                // Auto-redirect to main app
                await NavigationService.NavigateToAsync("//main/home");
            }
            await base.InitializeAsync();
        }
    }
}
