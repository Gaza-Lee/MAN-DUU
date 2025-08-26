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
    public class LandingPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ICommand GetStartedCommand { get; }
        public ICommand SkipCommand { get; }

        public LandingPageViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            GetStartedCommand = new Command (async () => await OnGetStarted());
            SkipCommand = new Command (async () => await OnSkip());
        }

        private async Task OnSkip()
        {
            var isAuthenticated = await _userService.IsUserAuthenticatedAsync();
            if (isAuthenticated)
            {
                await _navigationService.NavigateToAsync("//main/home");
            }
            else
            {
                await _navigationService.NavigateToAsync("//main//home");
            }
        }

        private async Task OnGetStarted()
        {
            await _navigationService.NavigateToAsync("//createaccountorsigninpage");
        }

        public async Task OnAppearingAsync()
        {
            // Check if user is already authenticated and redirect automatically
            var isAuthenticated = await _userService.IsUserAuthenticatedAsync();

            if (isAuthenticated)
            {
                // Auto-redirect to main app
                await _navigationService.NavigateToAsync("//main/home");
            }
        }
    }
}
