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
    public partial class ResetPswVerificationViewModel : BaseViewModel
    {
        public ResetPswVerificationViewModel(INavigationService navigationService): base (navigationService)
        {
        }

        [RelayCommand]
        private async Task ProceedAsync()
        {
            await IsBusyFor (async () =>
            {
                // Navigate to New Password Page
                await NavigationService.NavigateToAsync("//newpasswordpage");
            });
        }
    }
}
