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
            
        }

        public Task PopAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
    }
}
