using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IUserService _userService;

        public NavigationService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task InitializeAsync()
        {
            //check authentication status
            bool isAuthenticated = await _userService.IsUserAuthenticatedAsync();

            if (isAuthenticated)
            {
                //Navigate to main page
                await NavigateToAsync("//main/home");
            }
            else
            {
                //Navigate to landing page (Landing page as default)
                await NavigateToAsync("//landing");

            }
        } 

        public async Task NavigateToAsync(string route, IDictionary<string, object> parameters = null)
        {
            if (parameters != null)
            {
                await Shell.Current.GoToAsync(route, true, parameters);
            }
            else
            {
                await Shell.Current.GoToAsync(route, true);
            }
        }

        public Task PopAsync()
        {
            return Shell.Current.GoToAsync("..", true);
        }

    }
}
