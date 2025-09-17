using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels.CheckoutAndPayment
{
    public partial class MobileMoneyViewModel: BaseViewModel
    {
        private readonly CartService _cartService;
        private readonly IUserService _userService;

        //For now hardcoded network services
        [ObservableProperty]
        private ObservableCollection<string> _networkProviders = new() { "MTN Mobile Money", "Telecel", "Airtel Tigo" };

        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private decimal _overallTotal;

        public MobileMoneyViewModel(INavigationService navigationService, 
            CartService cartService, IUserService userService): base (navigationService)
        {
            _cartService = cartService;
            _userService = userService;

        }

        private bool CanMakePayment()
        {
            return !string.IsNullOrWhiteSpace(PhoneNumber);
        }

        [RelayCommand]
        private async Task MakePaymentAsync()
        {
            if (!CanMakePayment())
            {
                ShowToast("All fields are required");
                return;
            }

            if (!InputValidation.IsValidPhoneNumber(PhoneNumber))
            {
                ShowToast("Enter a valid Phone number");
                return;
            }

            /*In using API can further decide how payment for each network should behave but for
            now all payment network should just follow the same procedure*/

            //Notify user to confirm payment
            var confirmPayment = await Shell.Current.DisplayAlert(
                "Confirm Payment",
                "You are about to make payment for your order",
                "Confirm", "Cancel");

            if (confirmPayment)
            {
                await _cartService.ClearCartAsync();
                //navigate user to main page
                await NavigationService.NavigateToAsync("//main/home");
            }
        }
    }
}
