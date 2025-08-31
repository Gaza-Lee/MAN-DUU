using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.RegexValidation;
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
    public partial class ResetPasswordPageViewModel: BaseViewModel
    {
        #region Observable properties

        [ObservableProperty]
        private string _email;
        #endregion

        public ResetPasswordPageViewModel(INavigationService navigationService): base(navigationService)
        {
        }

        
        [RelayCommand]
        private async Task ProceedAsync()
        {
            await IsBusyFor(async () =>
            {
                // Validate email
                if (string.IsNullOrWhiteSpace(Email))
                {
                    ShowToast("Email must not be empty");
                    return;
                }
                if (!InputValidation.IsValidEmail(Email))
                {
                    ShowToast("Please enter a valid email address.");
                    return;
                }
                // For now just navigate to Verification Page
                await NavigationService.NavigateToAsync("newpasswordpage");
            });
        }
    }
}
