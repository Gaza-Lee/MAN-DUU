using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public partial class NewPasswordPageViewModel : BaseViewModel
    {
        #region Observable properties
        [ObservableProperty]
        private string _newPassword;

        [ObservableProperty]
        private string _confirmNewPassword;
        #endregion

        public NewPasswordPageViewModel(INavigationService navigationService) :base(navigationService)
        {
            
        }
          
               
        private bool IsPasswordMatch()
        {
            return NewPassword == ConfirmNewPassword;
        }

        [RelayCommand]
        private async Task ProceedAsync()
        {
            await IsBusyFor (async () =>
            {
                // Validation checks
                if (string.IsNullOrWhiteSpace(NewPassword) ||
                    string.IsNullOrWhiteSpace(ConfirmNewPassword))
                {
                    ShowToast("Fields must not be empty");
                    return;
                }
                if (!InputValidation.IsValidPassword(NewPassword))
                {
                    ShowToast("Weak Password");
                    return;
                }
                if (!IsPasswordMatch())
                {
                    ShowToast("Passwords do not match");
                    return;
                }

                // For now just navigate to SignIn Page
                ShowToast("Password reset successful! Please sign in with your new password.");
                await Task.Delay(1000);
                await NavigationService.NavigateToAsync("//signinpage");
            });
        }

    }
}
