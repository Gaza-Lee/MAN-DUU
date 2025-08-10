using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class NavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
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
